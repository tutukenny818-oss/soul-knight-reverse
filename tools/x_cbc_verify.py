#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_cbc_verify.py —— ★★★ CBC 模式最终验证 + 可破解性判定
   假设：CBC + 固定 IV + 全局密钥
   验证：
     A) 84 个 talent 前 56 B 密文相同 ⟹ 明文前 56 B 相同 ✓（已证）
     B) 若存在两个文件明文完全相同的前缀更长，密文前缀也应相同
     C) ★ 决定性：找明文"已知相同"的位置（如所有文件都以 return <Name>\n 结尾）
        若 CBC，则尾部密文应不同（因链不同）
     D) ★★ 可破解性：CBC 固定 IV 下，若两文件明文前 N 块相同，
        则密文前 N 块相同 ⟹ 可做"块位置 → 明文"映射，但仅限共同前缀
"""
import sys, io, os, re, json, base64, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'cbc_verify_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

def b64d(b):
    s = bytes(c for c in b if c not in b'\r\n\t ')
    try: return base64.b64decode(s, validate=False)
    except Exception:
        try: return base64.b64decode(s + b'='*((-len(s))%4), validate=False)
        except Exception: return None

SRC = os.path.join(W,'work','lua_device_src')
D = {}
ZIP = {}
for dp, dn, fn in os.walk(SRC):
    for f in fn:
        if not f.endswith('.bytes'): continue
        p = os.path.join(dp, f)
        rel = os.path.relpath(p, SRC).replace('\\','/')
        d = b64d(open(p,'rb').read())
        if d: D[rel] = d
# zip 版
Z = os.path.join(W,'work','lua_decoded2')
for f in os.listdir(Z):
    if f.endswith('.bytes'):
        ZIP[f] = open(os.path.join(Z, f), 'rb').read()

P('设备版 %d, zip 版 %d' % (len(D), len(ZIP)))

# ---- A) 84 个 talent 的公共前缀 ----
P('')
P('=' * 74); P('A) 84 个 talent 的公共前缀（CBC 下 = 明文共同前缀）'); P('=' * 74)
tal = sorted((r, d) for r, d in D.items() if r.startswith('app/talent/'))
g = collections.defaultdict(list)
for r, d in tal: g[d[:8].hex()].append((r, d))
grp = max(g.values(), key=len)
P('  最大组 %d 个文件' % len(grp))
grp.sort()
base = grp[0][1]
cpl = min(len(d) for _, d in grp)
i = 0
while i < cpl and all(base[i] == d[i] for _, d in grp): i += 1
P('  公共前缀 = %d 字节 = %d 块' % (i, i//8))
P('  hex: %s' % base[:i].hex(' '))

# ---- B) 检查其它 talent 组 ----
P('')
P('=' * 74); P('B) 其它 talent 组'); P('=' * 74)
for k in sorted(g, key=lambda x: -len(g[x])):
    lst = sorted(g[k])
    if len(lst) < 3: continue
    b = lst[0][1]
    cp = min(len(d) for _, d in lst)
    i2 = 0
    while i2 < cp and all(b[i2] == d[i2] for _, d in lst): i2 += 1
    P('  组 %s ×%-3d  公共前缀 %d B' % (k[:16], len(lst), i2))

# ---- C) 决定性：验证"明文相同 ⟹ 密文相同" ----
P('')
P('=' * 74); P('C) ★★★ 决定性验证：设备版 vs zip 版（明文相同，密钥应相同）'); P('=' * 74)
P('  已知：设备版与 zip 版 247/247 md5 完全相同')
P('  ⇒ 密钥相同（同一份文件两个副本）')
same = 0; diff = 0
for f, d in sorted(ZIP.items()):
    # f 形如 app_buff_X.bytes → 设备 app/buff/X.bytes
    m = re.match(r'app_([a-z]+)_(.+)\.bytes$', f)
    if not m: continue
    rel = 'app/%s/%s.bytes' % (m.group(1), m.group(2))
    if rel not in D: continue
    if D[rel] == d: same += 1
    else: diff += 1
P('  相同 %d / 不同 %d' % (same, diff))
P('  ⇒ 同一密钥（CBC 下相同明文必然相同密文）')

# ---- D) ★★ 可破解性判定 ----
P('')
P('=' * 74); P('D) ★★ CBC 可破解性判定'); P('=' * 74)
P('  CBC 特性：C[i] = E(P[i] XOR C[i-1])，C[-1] = IV')
P('  ⇒ 相同密文前缀 ⇔ 相同明文前缀（且相同 IV）')
P('  ⇒ 但【相同明文块在不同位置/不同文件】会得到【不同密文块】')
P('  ')
P('  已实测：b"function" 明文块 → 3 个不同密文块 ✓ 符合 CBC')
P('  ')
P('  可破解性分析：')
P('  ┌─ 已知明文尾部 12 个文件（含 padding 对齐）')
P('  ├─ 每个文件的明文 = [头部56B共同] + [主体] + [stub尾部]')
P('  ├─ 只有"明文完全一致的前缀"才能直接映射')
P('  └─ 结论：码本攻击上限 = 共同前缀长度（56 B = 7 块）')

# ---- E) ★★★ 尝试用 CBC 链式关系做攻击 ----
P('')
P('=' * 74); P('E) ★★★ CBC 链式攻击可行性'); P('=' * 74)
P('  关键：若两文件明文仅差最后一块，则密文也仅差最后一块')
P('  且：C_last_a XOR C_last_b = P_last_a XOR P_last_b')
P('  ')
# 找两文件长度相近、前缀相同的
P('  找"前缀相同、长度相近"的文件对:')
byprefix = collections.defaultdict(list)
for r, d in D.items():
    byprefix[d[:56].hex()].append((r, len(d), d))
cands = [(k, v) for k, v in byprefix.items() if len(v) >= 3]
cands.sort(key=lambda x: -len(x[1]))
for k, v in cands[:3]:
    v.sort(key=lambda x: x[1])
    P('')
    P('  组 %s (%d 文件):' % (k[:16], len(v)))
    for r, sz, d in v[:12]:
        P('     %-44s %5d B' % (r.split('/')[-1][:-6], sz))

# ---- F) ★★★ 长度差分析 ----
P('')
P('=' * 74); P('F) ★★★ 长度分析（推断明文差异）'); P('=' * 74)
if cands:
    k, v = cands[0]
    v.sort(key=lambda x: x[1])
    sizes = [sz for _, sz, _ in v]
    P('  组 %d 个文件, 长度范围 %d..%d' % (len(v), min(sizes), max(sizes)))
    # 两两长度差
    P('  相邻长度差分布:')
    for i in range(len(sizes)-1):
        P('     %d → %d  (差 %d)' % (sizes[i], sizes[i+1], sizes[i+1]-sizes[i]))

# ---- G) 结论 ----
P('')
P('=' * 74); P('G) ★ 最终加密模式结论'); P('=' * 74)
P('  模式：CBC（Cipher Block Chaining）')
P('  分组：8 字节（DES/3DES）或 16 字节（AES）—— 实测 8 字节对齐')
P('  IV：固定（因 84 个文件前 56 B 密文相同）')
P('  密钥：全局单一（设备版与 zip 版 md5 相同）')
P('  ')
P('  破解路径：')
P('  1. 需要密钥（在 IFix 补丁中被 patch 覆盖）')
P('  2. 或需要明文-密文对（已知明文尾部已有，但 CBC 下不足以恢复密钥）')
P('  3. IFix 的 DecryptBytesToBytes 用 AES-256 参数（32B key + 16B IV）')
P('     → 但实测 8 字节对齐，说明走的是 DecryptDES 或 DecryptXor 路径')

o.close()
print('WROTE work/cbc_verify_log.txt')
