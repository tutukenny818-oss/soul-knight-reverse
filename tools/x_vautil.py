#!/usr/bin/env python3
# -*- coding: utf-8 -*-
r"""x_vautil.py —— 可复用的 IL2CPP 反汇编/符号解析模块

用法:
    import sys; sys.path.insert(0, r'D:\元气骑士mod\tools')
    from x_vautil import VA, nm_of, td_of, dis, find_va, va2off, blob

VA 映射: work\deace\libil2cpp_p3.so（完全解密版，VA-file=0x4000）
符号表: work\_x_map3.pkl  (VA2G / MN / TD / IMGS)  325,832 条
"""
import os
import pickle
import struct
import sys

from capstone import Cs, CS_ARCH_ARM64, CS_MODE_ARM

W = r'D:\元气骑士mod'
SO = os.path.join(W, 'work', 'deace', 'libil2cpp_p3.so')
MDD_PATH = os.path.join(W, 'work', 'deace', 'global-metadata_clean.dat')

blob = open(SO, 'rb').read()
MDD = open(MDD_PATH, 'rb').read()

_obj = pickle.load(open(os.path.join(W, 'work', '_x_map3.pkl'), 'rb'))
VA2G = _obj['VA2G']
MN = _obj['MN']
TD = _obj['TD']
IMGS = _obj['IMGS'] if 'IMGS' in _obj else None

STR_OFF = 0x2A1500
STR_LEN = 0xBBA0E4

md = Cs(CS_ARCH_ARM64, CS_MODE_ARM)
md.detail = True

# ── PT_LOAD 映射 ──
phoff = struct.unpack_from('<Q', blob, 0x20)[0]
phentsize = struct.unpack_from('<H', blob, 0x36)[0]
phnum = struct.unpack_from('<H', blob, 0x38)[0]
LOADS = []
for _i in range(phnum):
    _o = phoff + _i * phentsize
    _typ, _fl = struct.unpack_from('<II', blob, _o)
    _off, _va, _pa, _fsz, _msz, _al = struct.unpack_from('<QQQQQQ', blob, _o + 8)
    if _typ == 1:
        LOADS.append((_va, _off, _fsz, _fl))


def va2off(va):
    for vaddr, off, filesz, fl in LOADS:
        if vaddr <= va < vaddr + filesz:
            return off + (va - vaddr)
    return None


def s_at(off):
    p = STR_OFF + off
    if p < 0 or p >= len(MDD):
        return '?'
    e = MDD.find(b'\x00', p)
    return MDD[p:e].decode('utf-8', 'replace') if e > p else '?'


def nm_of(va):
    gi = VA2G.get(va)
    if gi is None or not (0 <= gi < len(MN)):
        return None
    return s_at(MN[gi][0])


def gi_of(va):
    return VA2G.get(va)


def td_of(va):
    """返回 (typeName, ns, typeDefIndex) 或 None"""
    gi = VA2G.get(va)
    if gi is None or not (0 <= gi < len(MN)):
        return None
    return MN[gi]


def enclosing(va, maxback=0x10000):
    """找 ≤va 最近的函数起点"""
    best = None
    for v in VA2G:
        if v <= va and (best is None or v > best):
            if va - v < maxback:
                best = v
    return best


def find_va(name, exact=True, limit=40):
    """按方法名找 VA"""
    out = []
    for va, gi in VA2G.items():
        if not (0 <= gi < len(MN)):
            continue
        n = s_at(MN[gi][0])
        if (n == name) if exact else (name in n):
            out.append((va, n))
            if len(out) >= limit:
                break
    return sorted(out)


def find_va_kw(kw, limit=300):
    """按子串找所有方法"""
    out = []
    for va, gi in VA2G.items():
        if not (0 <= gi < len(MN)):
            continue
        n = s_at(MN[gi][0])
        if kw in n:
            out.append((va, n))
    return sorted(out)[:limit]


def dis(va, n=80, show_calls=True, stop_ret=True, out=None, indent='  '):
    """反汇编 va 起 n 条指令。返回行列表。"""
    off = va2off(va)
    if off is None:
        ln = '%s!! 无法映射 VA 0x%X' % (indent, va)
        (out.append(ln) if out is not None else print(ln))
        return []
    lines = []
    cnt = 0
    for ins in md.disasm(blob[off:off + n * 4], va):
        ex = ''
        if show_calls:
            try:
                if ins.mnemonic in ('bl', 'b', 'blr') and ins.operands:
                    op = ins.operands[0]
                    if op.type == 2:      # imm
                        t = op.imm
                        nn = nm_of(t)
                        ex = '   ; %s' % (nn if nn else ('sub_0x%X' % t))
                    elif ins.mnemonic == 'blr':
                        ex = '   ; indirect'
            except Exception:
                pass
        lines.append('%s0x%08X  %-8s %-46s%s'
                     % (indent, ins.address, ins.mnemonic, ins.op_str, ex))
        cnt += 1
        if stop_ret and ins.mnemonic == 'ret' and cnt > 4:
            break
    if out is not None:
        out.extend(lines)
    else:
        print('\n'.join(lines))
    return lines


def calls_in(va, maxn=400, stop_ret=True):
    """收集函数内所有 bl/b 目标 (target, name)"""
    off = va2off(va)
    if off is None:
        return []
    out = []
    cnt = 0
    for ins in md.disasm(blob[off:off + maxn * 4], va):
        if ins.mnemonic in ('bl', 'b') and ins.operands:
            op = ins.operands[0]
            if op.type == 2:
                out.append((op.imm, nm_of(op.imm)))
        cnt += 1
        if stop_ret and ins.mnemonic == 'ret' and cnt > 4:
            break
    return out


def callers_of(va, limit=60):
    """全 .text + il2cpp 段扫 bl 到 va 的调用点（线性扫，慢但可靠）"""
    pat = struct.pack('<I', 0x94000000 | (((va - (0, 0)[0]) if False else 0)))
    # ARM64 BL: 0x94000000 | imm26 ; target = pc + imm26*4
    out = []
    for vaddr, off, filesz, fl in LOADS:
        if not (fl & 1):
            continue
        seg = blob[off:off + filesz]
        for i in range(0, len(seg) - 4, 4):
            w = struct.unpack_from('<I', seg, i)[0]
            if (w & 0xFC000000) != 0x94000000:
                continue
            imm = w & 0x03FFFFFF
            if imm & 0x02000000:
                imm -= 0x04000000
            tgt = vaddr + i + imm * 4
            if tgt == va:
                out.append(vaddr + i)
                if len(out) >= limit:
                    return out
    return out


def strfind(kw, minlen=2, maxlen=200):
    """metadata 字符串表里找含 kw 的串"""
    res = []
    i = STR_OFF
    end = STR_OFF + STR_LEN
    if isinstance(kw, str):
        kw = kw.encode()
    while i < end:
        e = MDD.find(b'\x00', i, end)
        if e < 0:
            break
        if minlen <= e - i <= maxlen and kw in MDD[i:e]:
            try:
                res.append(MDD[i:e].decode('utf-8'))
            except Exception:
                pass
        i = e + 1
    return res


if __name__ == '__main__':
    print('libil2cpp_p3.so %d B' % len(blob))
    print('PT_LOAD:')
    for vaddr, off, filesz, fl in LOADS:
        print('   0x%012X off=0x%012X sz=0x%-10X flags=%d' % (vaddr, off, filesz, fl))
    print('VA2G 条目 %d' % len(VA2G))
    print('MDD %d B' % len(MDD))
    for nm in ('DecryptSource', 'get_SecretKey', 'HandleManifest'):
        print('  %-20s -> %s' % (nm, ['0x%X' % a for a, _ in find_va(nm)][:5]))
