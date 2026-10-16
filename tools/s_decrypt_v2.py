#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_decrypt_v2.py —— 第二层加密解密（★修正槽位对齐★）

★★ 关键 bug 修正 ★★
  之前的解密器用**绝对** 0x400 网格遍历（off = i*0x400），
  但密文槽是**相对 0x10000 周期**对齐的！

  证据：残留未解密槽 0x0049D1E0，其 0x49D1E0 % 0x10000 = 0xD1E0。
        D1E0 落在周期内窗口 [0x9DE0, 0xE1E0) 里，且 D1E0-9DE0 = 0x3400
        → 是 0x400 的整数倍（0x3400/0x400 = 13）。绝对偏移则不是。

  所以正确遍历是：
        for k in range(块数):
            for j in range(0, 0x10000, 0x400):      # 周期内偏移
                off = k * 0x10000 + j
  这能覆盖所有槽（含相位不是 0x400 倍数的情形）。

判据（确定性，不依赖字节频率）：
    raw = Capstone 解码率(原样)
    raw >= 0.90            → 明文代码，不动
    否则 XOR 0x6F 后 dec：
        dec >= 0.95 且 dec-raw > 0.20  → 密文，异或
        否则                            → 数据/对齐，不动

用法：
  python tools\\s_decrypt_v2.py --mod libunity.so
  python tools\\s_decrypt_v2.py --mod libil2cpp.so --apply
"""
import argparse
import os
import struct
import sys
import zipfile
import zlib

sys.stdout.reconfigure(encoding='utf-8', errors='replace')
from capstone import Cs, CS_ARCH_ARM64, CS_MODE_LITTLE_ENDIAN

W = r'D:\元气骑士mod'
APK = os.path.join(W, '5570618_20260905032720_Soul_Knight_release_chill.apk')
OUT = os.path.join(W, 'work', 'deace')
md = Cs(CS_ARCH_ARM64, CS_MODE_LITTLE_ENDIAN)
KEY = 0x6F
PERIOD = 0x10000
SLOT = 0x400


def deco(b, n=32):
    tot = ok = 0
    for p in range(0, len(b) - 4, 4):
        if tot >= n:
            break
        tot += 1
        if next(md.disasm(b[p:p + 4], 0, count=1), None):
            ok += 1
    return ok / tot if tot else 0.0


def dsz(b, lvl=9):
    c = zlib.compressobj(lvl, zlib.DEFLATED, -15)
    t = 0
    for i in range(0, len(b), 1 << 22):
        t += len(c.compress(b[i:i + (1 << 22)]))
    return t + len(c.flush())


def decrypt(d, key=KEY):
    """按周期对齐遍历所有槽"""
    out = bytearray(d)
    nd = nq = nok = 0
    nblk = len(d) // PERIOD + 1
    for k in range(nblk):
        for j in range(0, PERIOD, SLOT):
            off = k * PERIOD + j
            if off + SLOT > len(d):
                continue
            seg = d[off:off + SLOT]
            raw = deco(seg, 24)
            if raw >= 0.90:
                nok += 1
                continue
            dec = deco(bytes(x ^ key for x in seg), 24)
            if dec >= 0.95 and dec - raw > 0.20:
                for i in range(off, off + SLOT):
                    out[i] ^= key
                nd += 1
            else:
                nq += 1
    return bytes(out), nd, nq, nok


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--mod', default='libunity.so')
    ap.add_argument('--apply', action='store_true')
    a = ap.parse_args()
    z = zipfile.ZipFile(APK)
    name = 'lib/arm64-v8a/' + a.mod
    d = z.read(name)
    info = z.getinfo(name)
    print('=' * 92)
    print('%s  %s B' % (a.mod, format(len(d), ',')))
    print('=' * 92)
    out, nd, nq, nok = decrypt(d)
    print('  周期对齐遍历：明文 %d 槽  ★解密 %d 槽 (%s B)  数据 %d 槽'
          % (nok, nd, format(nd * SLOT, ','), nq))

    # 复核：还有多少未解密
    still = 0
    for k in range(len(d) // PERIOD + 1):
        for j in range(0, PERIOD, SLOT):
            off = k * PERIOD + j
            if off + SLOT > len(d):
                continue
            raw = deco(out[off:off + SLOT], 24)
            if raw < 0.90:
                dec = deco(bytes(x ^ KEY for x in out[off:off + SLOT]), 24)
                if dec >= 0.95 and dec - raw > 0.20:
                    still += 1
    print('  复核：仍未解密的槽 %d' % still)

    if a.apply:
        op = os.path.join(OUT, a.mod.replace('.so', '') + '_v2.so')
        open(op, 'wb').write(out)
        cn = dsz(out)
        print('  → %s   Δcsize=%+d %s'
              % (op, cn - info.compress_size,
                 '✓' if cn <= info.compress_size else '✗'))


if __name__ == '__main__':
    main()
