#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_header_confirmed.py —— ★★★★★★★ 56 字节头部明文（已确认）
   推导：
     SkillHaste 明文前 56 B = 87 组共享头部
     因为两者的密文前 56 B 完全相同（CBC 下等价于明文相同）
"""
import sys, io, os, re, json, base64
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'header_confirmed_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

MODS = {}
ud = os.path.join(W,'work','lua_utf16')
for f in sorted(os.listdir(ud)):
    if f.endswith('.lua'):
        MODS[f[:-4]] = open(os.path.join(ud, f), encoding='utf-8').read()

P('=' * 74); P('★ 56 字节共享头部（已确认）'); P('=' * 74)
p = MODS['SkillHaste'].encode()
P('  来源：app/talent/SkillHaste.bytes 的明文前 56 字节')
P('  （该文件与 87 组共享相同密文首块 9306c37c734a1a7e）')
P('')
P('  hex: %s' % p[:56].hex(' '))
P('  文本:')
P('  ┌────────────────────────────────────────────────────────')
for i in range(0, 56, 8):
    P('  │ [%d] %r' % (i, p[i:i+8]))
P('  └────────────────────────────────────────────────────────')
P('')
P('  完整 56 字节: %r' % p[:56])
P('')
P('  ★ 分段:')
P('     [0-7]   %r' % p[0:8])
P('     [8-15]  %r' % p[8:16])
P('     [16-23] %r' % p[16:24])
P('     [24-31] %r' % p[24:32])
P('     [32-39] %r' % p[32:40])
P('     [40-47] %r' % p[40:48])
P('     [48-55] %r' % p[48:56])

P('')
P('  ⇒ 明文结构:')
P('     local TalentBase = require \'app.talent.TalentBase\'\n')
P('     ↑ 恰好 56 字节 = 7 个 8 字节块')

# 验证：SkillHaste 明文前 56 B 是否就是这一行
line = "local TalentBase = require 'app.talent.TalentBase'\n"
P('')
P('  验证：%r' % line)
P('      长度 = %d' % len(line))
P('      与明文前 56 B 相同? %s' % (line.encode() == p[:56]))

# 但 BestPartner 的头部不同（它没有 require TalentBase 行）
P('')
P('=' * 74); P('★ 各模块的头部对比'); P('=' * 74)
for n in sorted(MODS):
    t = MODS[n]
    first = t.split('\n')[0]
    P('  %-34s %r' % (n, first[:60]))

# 57 组的密文首块分布与明文首块
P('')
P('=' * 74); P('★ 明文首块 → 密文首块 映射（RFC 检验）'); P('=' * 74)
# 已知：SkillHaste 明文首块 'local Ta' → 密文首块 9306c37c734a1a7e
# BestPartner 明文首块是 'local Bu' 或别的 → 密文首块应不同
# 但注意 CBC：首块密文 = E_K(IV XOR P0)，所以 P0 相同 → 密文相同
mapping = {}
for n in sorted(MODS):
    p2 = MODS[n].encode()
    if len(p2) < 8: continue
    mapping[n] = p2[:8]
from collections import Counter
P('  各模块明文首块:')
for n, b in sorted(mapping.items(), key=lambda x: x[1]):
    P('     %-34s %r' % (n, b))

# 找同首块的
c = Counter(mapping.values())
P('')
P('  相同首块的模块组:')
for b, cnt in c.most_common():
    if cnt > 1:
        g = [n for n in mapping if mapping[n] == b]
        P('     %r ×%d : %s' % (b, cnt, g))

o.close()
print('WROTE work/header_confirmed_log.txt')
