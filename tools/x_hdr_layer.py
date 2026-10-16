#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_hdr_layer.py —— ★★★★★★★ 按【多数派】计算分层共享前缀（修正）
   之前用「全严格」（所有文件都必须相同）→ 只有 8 字节
   实际是分层：85-87 个文件共享 0-48，11 个共享 56-64 ...
   正确方法：
     对每个 offset，找 top 块及其文件集
     然后取「最大的一致文件集」上的最长前缀
"""
import sys, io, os, re, json, base64, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'hdr_layer_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

def b64d(x):
    s = bytes(c for c in x if c not in b'\r\n\t ')
    try:
        return base64.b64decode(s, validate=False)
    except Exception:
        try:
            return base64.b64decode(s + b'='*((-len(s))%4), validate=False)
        except Exception:
            return None

SRC = os.path.join(W,'work','lua_device_src')
D = {}
for dp, dn, fn in os.walk(SRC):
    for f in fn:
        if not f.endswith('.bytes'): continue
        pp = os.path.join(dp, f)
        rel = os.path.relpath(pp, SRC).replace('\\','/')
        d = b64d(open(pp,'rb').read())
        if d: D[rel] = d

grp87 = sorted(r for r, d in D.items() if d[:8].hex() == '9306c37c734a1a7e')
P('87 组 %d 个文件' % len(grp87))

# ---- 逐 offset 的块分布 ----
P('')
P('=' * 74); P('★ 逐 offset 块分布'); P('=' * 74)
P('  %-5s %-6s %-6s %s' % ('off', '唯一', 'top数', 'top块'))
layers = {}
for off in range(0, 128, 8):
    c = collections.Counter()
    for r in grp87:
        d = D[r]
        if len(d) > off + 8: c[d[off:off+8]] += 1
    if not c: continue
    top, cnt = c.most_common(1)[0]
    layers[off] = (top, cnt)
    P('  %-5d %-6d %-6d %s' % (off, len(c), cnt, top.hex()))

# ---- ★★★ 确定最大的「一致文件集」----
P('')
P('=' * 74); P('★★★ 最大一致文件集上的最长前缀'); P('=' * 74)
# 从最大组开始，逐步缩小
cur_set = set(grp87)
cur_prefix = b''
for off in range(0, 128, 8):
    c = collections.Counter()
    for r in cur_set:
        d = D[r]
        if len(d) > off + 8: c[d[off:off+8]] += 1
    if not c: break
    top, cnt = c.most_common(1)[0]
    if cnt < len(cur_set) * 0.5:  # 低于半数就停
        break
    # 收窄到 top 块的文件集
    newset = set(r for r in cur_set if len(D[r]) > off+8 and D[r][off:off+8] == top)
    cur_set = newset
    cur_prefix += top
    P('  off %3d: 集 %3d 文件, 块 %s' % (off, len(cur_set), top.hex()))

P('')
P('  ★ 最长一致前缀 = %d B (%d 块)' % (len(cur_prefix), len(cur_prefix)//8))
P('  ★ 文件集 %d 个' % len(cur_set))
P('')
P('  文件集:')
for r in sorted(cur_set)[:40]:
    P('     %s' % r.split('/')[-1][:-6])

# ---- 用已知明文标定这个前缀 ----
P('')
P('=' * 74); P('★ 用已知明文标定'); P('=' * 74)
MODS = {}
ud = os.path.join(W,'work','lua_utf16')
for f in sorted(os.listdir(ud)):
    if f.endswith('.lua'):
        MODS[f[:-4]] = open(os.path.join(ud, f), encoding='utf-8').read()

in_set = []
for n in sorted(MODS):
    for pre in ('app/buff/', 'app/talent/'):
        rel = pre + n + '.bytes'
        if rel in cur_set:
            in_set.append((n, rel, MODS[n]))
            break
P('  集内已知明文模块 %d 个:' % len(in_set))
for n, rel, t in in_set:
    P('     %-34s %s' % (n, rel))

if in_set:
    texts = [t.encode() for _, _, t in in_set]
    mnp = min(len(t) for t in texts)
    i = 0
    while i < mnp and all(t[i] == texts[0][i] for t in texts): i += 1
    P('')
    P('  ★ 共同明文前缀 = %d B' % i)
    P('     %r' % texts[0][:i])
    P('')
    P('  ★★ 与一致密文前缀 %d B 比较' % len(cur_prefix))
    P('     → 明文前缀 %d B %s 密文前缀 %d B' % (
        i, '≥' if i >= len(cur_prefix) else '<', len(cur_prefix)))
    if i >= 8:
        P('')
        P('  ★★★ 前 %d 字节明文（可用于 %d 个文件）:' % (min(i, 56), len(cur_set)))
        P('     %r' % texts[0][:min(i, 56)])

# ---- 分层输出 ----
P('')
P('=' * 74); P('★ 分层结构（完整）'); P('=' * 74)
byoff = {}
for off in sorted(layers):
    top, cnt = layers[off]
    byoff[off] = (top, cnt)
    if cnt >= 3:
        files = [r.split('/')[-1][:-6] for r in grp87
                 if len(D[r]) > off+8 and D[r][off:off+8] == top]
        P('  offset %3d : %s ×%-3d' % (off, top.hex(), cnt))
        if cnt <= 12:
            for f in files: P('        %s' % f)

# ---- ★★★★ 生成最大 oracle ----
P('')
P('=' * 74); P('★★★★ 生成 oracle'); P('=' * 74)
ORACLES = []
# 用最长的重叠部分
n_ok = 0
if in_set:
    hdr = texts[0][:i]
    nb = len(hdr) // 8
    for r in cur_set:
        c = D[r]
        for k in range(1, min(nb, len(c)//8)):
            cl = c[k*8:(k+1)*8]; cp = c[(k-1)*8:k*8]; pl = hdr[k*8:(k+1)*8]
            if len(cl)==8 and len(cp)==8 and len(pl)==8:
                ORACLES.append((r, k, cl, cp, pl)); n_ok += 1
P('  从 %d 个文件 × %d 块 = %d 个 oracle' % (len(cur_set), len(hdr)//8, n_ok))

json.dump({'hdr_hex': (texts[0][:i].hex() if in_set else ''),
           'hdr': (texts[0][:i].decode('utf-8','replace') if in_set else ''),
           'len': i if in_set else 0,
           'files': sorted(cur_set),
           'oracles': [[r,k,cl.hex(),cp.hex(),pl.hex()] for r,k,cl,cp,pl in ORACLES[:50000]]},
          open(os.path.join(W,'work','CBC_ORACLES_V2.json'),'w',encoding='utf-8'),
          ensure_ascii=False, indent=1)
P('  WROTE work/CBC_ORACLES_V2.json')

o.close()
print('WROTE work/hdr_layer_log.txt')
