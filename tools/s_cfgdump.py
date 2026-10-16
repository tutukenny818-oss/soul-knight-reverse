#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_cfgdump.py —— ★ 用 N=130 全量解密 config.ab 的 18 个 TextAsset

═══════════════════════════════════════════════════════════════════════
 突破过程
═══════════════════════════════════════════════════════════════════════
 1. s_cfgab2.py 从 config.ab 里抽出 18 个 TextAsset（名字全对得上 wiki）
 2. 观察头部：6/7 个文件开头 4 字节相同 ⇒ 固定密钥流（周期 XOR）
 3. s_cfgcrack.py 用「C[i]==C[i+N] 命中率」扫出高分周期（130 及其倍数）
 4. s_cfgcrack2.py 用频率分析恢复密钥流，**N=130 解出明文**：
       Key,SkinType,Value.string,string,string.c00/common,S
    ⇒ ★ 这是 **CSV 表头**！config.ab 里是 CSV，不是我之前以为的 JSON

═══════════════════════════════════════════════════════════════════════
 产物
═══════════════════════════════════════════════════════════════════════
  work/cfgab/<name>.csv     ← 18 个 CSV（武器/技能/物品/敌人/角色/皮肤…）
  work/cfgab/xor_key_130.bin ← 密钥流
  ⇒ 这些正是 wiki 记录的内容，可以逐项对照！

用法：
   python tools\\s_cfgdump.py                 # 全量导出
   python tools\\s_cfgdump.py --summary       # 只看统计
   python tools\\s_cfgdump.py --peek weapons  # 看某个表的前几行
"""
import argparse
import json
import os
import re
import struct
import sys
from collections import Counter

sys.stdout.reconfigure(encoding='utf-8', errors='replace')

W = r'D:\元气骑士mod'
CFGSRC = os.path.join(W, 'work', 'cfg')
OUT = os.path.join(W, 'work', 'cfgab')
N = 130

FREQ = {}


def init_freq():
    for c in b' ,"{}:[]0123456789':
        FREQ[c] = 12.0
    for c in b'etaoinshrdlu':
        FREQ[c] = 11.0
    for c in b'cmfwypvbgkjqxz':
        FREQ[c] = 9.0
    for c in b'ETAOINSHRDLUCMFWYPVBGKJQXZ':
        FREQ[c] = 8.0
    for c in range(0x20, 0x7f):
        FREQ.setdefault(c, 6.0)
    FREQ[0x0a] = 10.0
    FREQ[0x0d] = 3.0
    FREQ[0x09] = 3.0
    for c in range(0x80, 0x100):
        FREQ[c] = 4.0
    for c in range(0x00, 0x20):
        FREQ.setdefault(c, -12.0)
    FREQ[0x00] = -14.0


def load_assets():
    ap = os.path.join(CFGSRC, 'config.ab.decompressed')
    if not os.path.exists(ap):
        return None, []
    data = open(ap, 'rb').read()
    out = []
    n = len(data)
    p = 0
    while p < n - 8:
        ln = struct.unpack_from('<i', data, p)[0]
        if 3 <= ln <= 40:
            s = data[p + 4:p + 4 + ln]
            if all(32 <= c < 127 for c in s):
                nm = s.decode('ascii', 'replace')
                if re.match(r'^[A-Za-z_][A-Za-z0-9_]*$', nm):
                    q2 = (p + 4 + ln + 3) & ~3
                    if q2 + 4 <= n:
                        ln2 = struct.unpack_from('<i', data, q2)[0]
                        if 100 <= ln2 <= 5_000_000 and q2 + 4 + ln2 <= n:
                            out.append((nm, q2 + 4, ln2))
                            p = q2 + 4 + ln2
                            continue
        p += 1
    return data, out


def recover_key(c, N):
    key = bytearray(N)
    for j in range(N):
        col = c[j::N]
        cnt = Counter(col)
        best_k, best_s = 0, -1e18
        for k in range(256):
            s = 0.0
            for b, n in cnt.items():
                s += n * FREQ.get(b ^ k, -2.0)
            if s > best_s:
                best_s, best_k = s, k
        key[j] = best_k
    return bytes(key)


def xors(c, key):
    N = len(key)
    return bytes(c[i] ^ key[i % N] for i in range(len(c)))


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--summary', action='store_true')
    ap.add_argument('--peek', default=None)
    a = ap.parse_args()
    init_freq()
    os.makedirs(OUT, exist_ok=True)

    data, blobs = load_assets()
    if data is None:
        print('  ✗ 缺少 config.ab.decompressed')
        return 1

    print('=' * 94)
    print(' 解密 config.ab（XOR, N=%d）' % N)
    print('=' * 94)
    print('  TextAsset: %d 个' % len(blobs))
    print()

    # 用最长样本恢复 key
    nm0, off0, ln0 = max(blobs, key=lambda x: x[2])
    key = recover_key(data[off0:off0 + ln0], N)
    open(os.path.join(OUT, 'xor_key_%d.bin' % N), 'wb').write(key)
    print('  key[0:32] = %s' % key[:32].hex())
    print('  key 唯一字节: %d/%d' % (len(set(key)), N))
    print()

    ok = 0
    stats = []
    for nm, off, ln in blobs:
        c = data[off:off + ln]
        pt = xors(c, key)
        # 去掉开头的长度前缀（如果有）
        txt = pt.decode('utf-8', 'replace')
        # 判断是不是 CSV
        first = txt.split('\n', 1)[0]
        pr = sum(1 for x in pt[:1024] if x in (9, 10, 13) or 0x20 <= x < 0x7f
                 or x >= 0x80) / max(1, min(1024, len(pt)))
        iscsv = (',' in first and pr > 0.9)
        tag = 'CSV' if iscsv else ('TEXT' if pr > 0.9 else '??')
        lines = txt.count('\n')
        op = os.path.join(OUT, '%s.csv' % nm)
        open(op, 'w', encoding='utf-8', errors='replace').write(txt)
        stats.append((nm, ln, pr, tag, lines))
        if iscsv:
            ok += 1
        print('  %-24s %9s B  可读率%.3f  %-5s %7d 行'
              % (nm, format(ln, ','), pr, tag, lines))
    print()
    print('  成功 %d/%d   → %s' % (ok, len(blobs), OUT))
    print()
    print('=' * 94)
    print(' 各表表头（列名 = 游戏字段名）')
    print('=' * 94)
    for nm, ln, pr, tag, lines in stats:
        p = os.path.join(OUT, '%s.csv' % nm)
        try:
            t = open(p, encoding='utf-8', errors='replace').read()
            head = t.split('\n')[0].strip()
            print()
            print('  ▸ %s (%d 行)' % (nm, lines))
            print('      列: %s' % head[:400])
            for row in t.split('\n')[1:4]:
                if row.strip():
                    print('      行: %s' % row[:300])
        except Exception:
            pass

    if a.peek:
        p = os.path.join(OUT, '%s.csv' % a.peek)
        if os.path.exists(p):
            t = open(p, encoding='utf-8', errors='replace').read()
            print()
            print('=' * 94)
            print(' %s 前 40 行' % a.peek)
            print('=' * 94)
            for i, ln in enumerate(t.split('\n')[:40]):
                print('  %3d  %s' % (i, ln[:400]))
    return 0


if __name__ == '__main__':
    sys.exit(main())
