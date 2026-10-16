#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_read_consts.py —— 读四条链的常量 + 反汇编剩余链"""
import sys, io, os, re, json, struct, contextlib
sys.path.insert(0, r'D:\元气骑士mod\tools')
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'read_consts_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

import x_vautil as V
blob = V.blob
LOADS = [(0x0,0x0,0x59D2C2C),(0x59D6C2C,0x59D2C2C,0x9B78FB4),
         (0xF553BE0,0xF54BBE0,0xAAF098),(0x10006C78,0xFFFAC78,0x96B3D8),
         (0x10BCC000,0x10968000,0x3550),(0x10BD0000,0x10968000,0x3848),
         (0x10BD4000,0x10968000,0x5688),(0x10BDC000,0x1096C000,0x112D0)]
def readf(va):
    for v, f, sz in LOADS:
        if v <= va < v + sz:
            p = f + (va - v)
            if p+4 <= len(blob): return struct.unpack_from('<f', blob, p)[0]
    return None
def safe_dis(va, n=110):
    buf = io.StringIO()
    try:
        with contextlib.redirect_stdout(buf):
            d = V.dis(va, n)
        return [str(x).rstrip() for x in d] if isinstance(d, list) else []
    except BaseException:
        return []
def fb(va):
    d = safe_dis(va, 30)
    for i, l in enumerate(d):
        if 'IsPatched' in l:
            for j in range(i+1, min(i+3, len(d))):
                m = re.search(r'tbz\s+w0,\s*#0,\s*#(0x[0-9A-Fa-f]+)', d[j])
                if m: return int(m.group(1), 16)
    return None

# ══════ 1. ★ 冷却公式常量 ══════
P('=' * 78); P('★★★ 冷却公式常量'); P('=' * 78)
for va, desc in [
    (0x2CC16E8, '参数8 基准值（SkillHasteUsesDiminishingCooldownFormula）'),
    (0x2CC16EC, '参数3 基准值'),
    (0x2CC16F0, '邻近'),
    (0x2CC16E4, '邻近'),
    (0x2CC16E0, '邻近'),
]:
    v = readf(va)
    P('  0x%08X = %-16s  %s' % (va, ('%.8f' % v) if v is not None else '?', desc))

# ══════ 2. SkillHasteUsesDiminishingCooldownFormula（无 fallback，直接看）══════
P('')
P('=' * 78); P('★ SkillHasteUsesDiminishingCooldownFormula @ 0x075F6FA4'); P('=' * 78)
P('  （fb=0 说明它不是 IFix 桩，直接反汇编）')
d = safe_dis(0x075F6FA4, 150)
for l in d: P(l)

# ══════ 3. 其余三条链 ══════
P('')
P('=' * 78); P('★ 移动速度 / 攻击速度 / 护甲'); P('=' * 78)
TARGETS = [
 ('ResolveMoveSpeedBonus',      0x0692CBDC),
 ('ResolveMoveSpeedLoss',       0x0692C808),
 ('AddMoveSpeedPercent',        0x069DA1A4),
 ('GetSelfMoveSpeedFactor',     0x068BDABC),
 ('GetSelfAttackSpeedFactor',   0x068BD544),
 ('AddAttackSpeedFactor',       0x074A7FEC),
 ('ArmorRestoreSpeed',          0x079966E4),
 ('SlowDownArmorRestoreSpeed',  0x079970BC),
 ('ArmorRecoveryProbability',   0x0833FF64),
 ('RecoverArmorInShield',       0x0981D9D8),
]
for nm, va in TARGETS:
    f = fb(va)
    P('')
    P('─' * 76)
    P('★ %s @ 0x%08X  fb=0x%X' % (nm, va, f or 0))
    P('─' * 76)
    d = safe_dis(f if f else va, 70)
    for l in d: P(l)
    # 常量
    cs = []
    for l in d:
        m = re.search(r'fmov\s+[sd]\d+,\s*#(-?[\d.]+)', l)
        if m: cs.append(m.group(1))
        m2 = re.search(r'\[x\d+,\s*#(0x[0-9a-fA-F]+)\]', l)
    if cs: P('  ★ fmov 常量: %s' % cs)

o.close()
print('WROTE work/read_consts_log.txt')
