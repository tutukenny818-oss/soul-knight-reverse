#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_meta3.py —— 【最终版】用「掩码法」精确加解密 metadata

═══════════════════════════════════════════════════════════════════════
 ★ 为什么用掩码法（本轮实测定案）
═══════════════════════════════════════════════════════════════════════
 对照 work/modkit/global-metadata.orig.dat（设备上的密文）
     与 work/deace/global-metadata_clean.dat（正确明文）：

     总字节     55,398,120
     差异字节   13,860,840   ← 全都满足 (cipher ^ clean) == 0x66
     相同字节   41,537,280   ← 本来就一样（不需要 XOR）
     其它差异   **0**

 ⇒ 加密不是「按窗口 XOR 0x66」，而是**按位置选择性地 XOR 0x66**。
   哪些位置被 XOR 由某个规则决定（相位窗口），但**我们不必知道规则**：

   ★ 掩码法：mask[i] = cipher[i] ^ clean[i]  （每个字节是 0x00 或 0x66）
            解密：plain  = cipher ^ mask
            加密：cipher = plain  ^ mask
   **精确、可逆、零猜测。**

 之前「周期窗 [0x1000,0x5000)」的模型是错的 —— 那只是初版探测时的近似，
 导致头部 8 字节没解开、结构读不出来。

 前 8 字节单独处理（魔数变换：密文 94437212184b9eec ↔ 明文 af1bb1fa1d000000）

═══════════════════════════════════════════════════════════════════════
 本脚本提供
═══════════════════════════════════════════════════════════════════════
  --buildmask   从 cipher+clean 生成掩码（存 work/modkit/mask.bin）
  --selftest    验证：mask 能精确还原 cipher↔clean（0 差异）
  --info        打印 metadata 结构（magic/version/各段 offset,size）
  --fields      列出 fieldDefaultValues（★ 数值注入候选）
  --grep X      过滤字段默认值

 后续：用 mask 就能安全地改明文再加密回去。
"""
import argparse
import os
import re
import struct
import sys

sys.path.insert(0, os.path.join(r'D:\元气骑士mod', 'tools'))
sys.stdout.reconfigure(encoding='utf-8', errors='replace')

W = r'D:\元气骑士mod'
CIPHER = os.path.join(W, 'work', 'modkit', 'global-metadata.orig.dat')
CLEAN = os.path.join(W, 'work', 'deace', 'global-metadata_clean.dat')
MASKF = os.path.join(W, 'work', 'modkit', 'mask.bin')
HDRLEN = 8

HDR = [
    ('stringLiteral', 0), ('stringLiteralData', 1), ('string', 2),
    ('events', 3), ('properties', 4), ('methods', 5),
    ('parameterDefaultValues', 6), ('fieldDefaultValues', 7),
    ('fieldAndParameterDefaultValueData', 8), ('fieldMarshaledSizes', 9),
    ('parameters', 10), ('fields', 11), ('genericParameters', 12),
    ('genericParameterConstraints', 13), ('genericContainers', 14),
    ('nestedTypes', 15), ('interfaces', 16), ('vtableMethods', 17),
    ('interfaceOffsets', 18), ('typeDefinitions', 19), ('images', 20),
    ('assemblies', 21), ('fieldRefs', 22), ('referencedAssemblies', 23),
    ('attributeData', 24), ('attributeDataRange', 25),
    ('unresolvedVirtualCallParameterTypes', 26),
    ('unresolvedVirtualCallParameterRanges', 27),
    ('windowsRuntimeTypeNames', 28),
]


def build_mask(save=True):
    ci = open(CIPHER, 'rb').read()
    cl = open(CLEAN, 'rb').read()
    assert len(ci) == len(cl), '长度不同'
    m = bytes(a ^ b for a, b in zip(ci, cl))
    if save:
        open(MASKF, 'wb').write(m)
    vals = {}
    for x in m:
        vals[x] = vals.get(x, 0) + 1
    print('  掩码已生成: %s' % MASKF)
    print('  掩码字节分布: %s' % {hex(k): format(v, ',')
                                  for k, v in sorted(vals.items())})
    print('  ⇒ 非零掩码字节数: %s'
          % format(len(m) - vals.get(0, 0), ','))
    return m, ci, cl


def load_mask():
    if os.path.exists(MASKF):
        return open(MASKF, 'rb').read()
    m, _, _ = build_mask()
    return m


def dec(cipher, mask):
    return bytes(a ^ b for a, b in zip(cipher, mask))


def enc(plain, mask):
    return bytes(a ^ b for a, b in zip(plain, mask))


def selftest():
    m, ci, cl = build_mask()
    print()
    p = dec(ci, m)
    nd = sum(1 for i in range(len(cl)) if p[i] != cl[i])
    print('  解密(cipher) vs clean : 差异 %d 字节  %s'
          % (nd, '✓' if nd == 0 else '✗'))
    c2 = enc(cl, m)
    nd2 = sum(1 for i in range(len(ci)) if c2[i] != ci[i])
    print('  加密(clean)  vs cipher: 差异 %d 字节  %s'
          % (nd2, '✓' if nd2 == 0 else '✗'))
    # 前 8 字节特判
    print('  前 8 字节: cipher=%s clean=%s'
          % (ci[:8].hex(), cl[:8].hex()))
    print('  （前 8 字节按魔数变换处理，不参与掩码）')
    return nd == 0 and nd2 == 0


def parse(p):
    magic = struct.unpack_from('<I', p, 0)[0]
    ver = struct.unpack_from('<i', p, 4)[0]
    sec = {}
    for name, idx in HDR:
        off, sz = struct.unpack_from('<ii', p, 8 + idx * 8)
        sec[name] = (off, sz)
    return magic, ver, sec


def info(show=True):
    p = open(CLEAN, 'rb').read()
    magic, ver, sec = parse(p)
    if show:
        print('=' * 96)
        print(' metadata 结构（用 clean.dat 明文）')
        print('=' * 96)
        print('  magic = 0x%08X  %s' % (magic, '✓' if magic == 0xFAB11BAF else '✗'))
        print('  version = %d' % ver)
        print()
        print('  %-46s %14s %10s' % ('段', 'offset', 'size'))
        for name, (off, sz) in sec.items():
            mark = ' ★' if 'DefaultValue' in name else ''
            print('  %-46s %14s %10s%s'
                  % (name, format(off, ','), format(sz, ','), mark))
    return p, magic, ver, sec


def fields(grep=None, limit=60):
    p, magic, ver, sec = info(show=False)
    off, sz = sec['fieldDefaultValues']
    dbase, dsz = sec['fieldAndParameterDefaultValueData']
    n = sz // 12
    print()
    print('=' * 96)
    print(' fieldDefaultValues: %d 条（显示前 %d）' % (n, limit))
    print('=' * 96)
    print('  %-7s %-11s %-9s %-9s %-9s %s'
          % ('#', '偏移', 'fieldIdx', 'typeIdx', 'doff', '值'))
    shown = 0
    for i in range(n):
        a = off + i * 12
        fi, ti, do = struct.unpack_from('<iii', p, a)
        va = dbase + do
        if va < 0 or va + 16 > len(p):
            continue
        raw = p[va:va + 16]
        i32 = struct.unpack_from('<i', raw, 0)[0]
        f32 = struct.unpack_from('<f', raw, 0)[0]
        s = None
        ln = struct.unpack_from('<i', raw, 0)[0]
        if 0 < ln <= 64 and va + 4 + ln <= len(p):
            cand = p[va + 4:va + 4 + ln]
            if all(0x20 <= x < 0x7f for x in cand):
                s = cand.decode('ascii')
        # 合理性检查
        if not (0 <= fi < 400000 and 0 <= ti < 400000):
            continue
        if grep:
            g = grep.lower()
            blob = ('%s %s %s' % (s or '', i32, f32)).lower()
            if g not in blob:
                continue
        desc = 'i32=%-12d f32=%-13.6g' % (i32, f32)
        if s:
            desc += ' str=%r' % s[:28]
        print('  %-7d 0x%-9X %-9d %-9d %-9d %s'
              % (i, a, fi, ti, do, desc))
        shown += 1
        if shown >= limit:
            break
    if shown == 0:
        print('  （无匹配）')
    return shown


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--buildmask', action='store_true')
    ap.add_argument('--selftest', action='store_true')
    ap.add_argument('--info', action='store_true')
    ap.add_argument('--fields', action='store_true')
    ap.add_argument('--grep', default=None)
    ap.add_argument('--limit', type=int, default=60)
    a = ap.parse_args()
    if a.buildmask:
        build_mask()
        return 0
    if a.selftest:
        return 0 if selftest() else 1
    if a.fields or a.grep:
        fields(a.grep, a.limit)
        return 0
    info()
    return 0


if __name__ == '__main__':
    sys.exit(main())
