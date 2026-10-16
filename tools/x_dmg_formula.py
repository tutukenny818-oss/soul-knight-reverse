#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_dmg_formula.py —— ★★★★★★★★ 还原伤害计算公式（从 fallback 分支）"""
import sys, io, os, json, re, contextlib
sys.path.insert(0, r'D:\元气骑士mod\tools')
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'dmg_formula_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

import x_vautil as V
def safe_dis(va, n=200):
    buf = io.StringIO()
    try:
        with contextlib.redirect_stdout(buf):
            d = V.dis(va, n)
        return [str(x).rstrip() for x in d] if isinstance(d, list) else []
    except BaseException:
        return []

allnames = {}
for kw in ['Damage','Critic','Critical','Attack','Hurt','Armor','Hp','Health','Shield']:
    try:
        for it in V.find_va_kw(kw):
            try: allnames.setdefault(str(it[1]), int(it[0]))
            except Exception: pass
    except Exception: pass
P('符号 %d' % len(allnames))

def fallback(va):
    d = safe_dis(va, 30)
    for i, l in enumerate(d):
        if 'IsPatched' in l:
            for j in range(i+1, min(i+3, len(d))):
                m = re.search(r'tbz\s+w0,\s*#0,\s*#(0x[0-9A-Fa-f]+)', d[j])
                if m: return int(m.group(1), 16)
    return None

# 核心公式函数
CORE = [
 'CalculateFinalDamage','CalculateCriticalDamage','ApplyCriticalResultDamageModifier',
 'ApplyNonCriticalDamageReduction','ApplyPlayerCriticalToResolvedDamage',
 'ApplyPlayerCriticalOnEnemyHit','GetExtraDamage','GetRuntimeDamageBonus',
 'GetAddDamagePercent','GetMarkedExtraDamageFactor','GetMarkDamageReductionFactor',
 'HurtDamage','GetEffectiveDamage','MakeDamage','ModifyDamage',
 'ApplyAttackDamageMultiplierTalentDelta','AddCriticFactor','GetCriticFactor',
 'AlwaysCritical','AddCriticalHitRate','AddCriticalHitChance',
 'GetContactDamageMultiplier','GetFinisherDamageFactor','IsDeadlyDamage',
 'GetScaledConfiguredDamage','EnhanceBulletDamageFactor','GetBaseExplodeDamageValue',
 'RollCritical','ApplyCriticalResultDamageModifier','GetCriticDamageFactor',
]
P('')
P('=' * 78); P('★ 核心伤害函数（fallback 原始实现）'); P('=' * 78)
for name in CORE:
    if name not in allnames:
        P(''); P('  [%s] 未找到' % name); continue
    va = allnames[name]
    fb = fallback(va)
    P('')
    P('─' * 78)
    P('★ %s @ 0x%08X   fallback=0x%X' % (name, va, fb if fb else 0))
    P('─' * 78)
    d = safe_dis(fb if fb else va, 140)
    for l in d: P(l)

o.close()
print('WROTE work/dmg_formula_log.txt')
