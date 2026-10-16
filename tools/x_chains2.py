#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_chains2.py —— 四条链公式还原（符号表已恢复）"""
import sys, io, os, re, json, struct, contextlib
sys.path.insert(0, r'D:\元气骑士mod\tools')
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'chains2_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

import x_vautil as V
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

# ★ 先列出每条链的所有候选（用精确名字）
P('=' * 78); P('1. 四条链的候选函数'); P('=' * 78)
CHAINS = {
 '冷却缩减': ['UsesSkillHasteCooldown','SkillHasteUsesDiminishingCooldownFormula',
              'ExistingAdditiveRateAndSkillHasteRemainEffective',
              'ApplyCooldownReductionTalentDelta','ResolveCooldownReductionLoss',
              'StartCooldown','SetSkillCooldown','GetSkillHaste',
              'GetSkillCd','GetSkillCooldown','GetCooldownReduction',
              'GetCooldownReductionPercent','ResolveCooldown','GetFinalCooldown'],
 '移动速度': ['ResolveMoveSpeed','ResolveIdleMoveSpeed','ResolveMoveSpeedBonus',
              'ResolveMoveSpeedLoss','FreeFollowIdleMoveUsesHalfSpeed',
              'FreeFollowChaseMoveUsesFullSpeed','ApplyMoveSpeedDown',
              'GetSelfMoveSpeedFactor','AddMoveSpeedPercent',
              'ResolveMecha2MoveSpeedMultiplier','GetMoveSpeed','GetFinalMoveSpeed'],
 '攻击速度': ['ResolveWeaponSpeed','GetSelfAttackSpeedFactor',
              'ApplyWeaponAttackSpeedFactor','AddAttackSpeedFactor',
              'GetAttackSpeedFactor','GetFinalAttackSpeed','ResolveAttackSpeed',
              'ApplyAttackSpeedDown','RefreshAttackSpeed'],
 '护甲回复': ['ArmorRestoreSpeed','SlowDownArmorRestoreSpeed',
              'ArmorRecoveryProbability','RecoverArmorInShield',
              'Skill1AddExtraArmor','GetArmorRestoreSpeed',
              'GetArmorRestoreProbability','AddArmor','GetMaxArmor'],
}
found = {}
for chain, syms in CHAINS.items():
    P('')
    P('── %s ──' % chain)
    lst = []
    for s in syms:
        hits = V.find_va(s, exact=True)
        if not hits:
            hits = V.find_va(s, exact=False, limit=6)
        for va, nm in hits[:6]:
            lst.append((va, nm))
    seen = set()
    for va, nm in sorted(lst):
        if va in seen: continue
        seen.add(va)
        P('   0x%08X  %s' % (va, nm))
    found[chain] = sorted(set(lst))

# ★ 反汇编每条链的关键函数
P('')
P('#' * 78); P('# 2. 关键函数反汇编'); P('#' * 78)
KEY = {
 '冷却缩减': ['UsesSkillHasteCooldown','SkillHasteUsesDiminishingCooldownFormula',
              'ExistingAdditiveRateAndSkillHasteRemainEffective'],
 '移动速度': ['ResolveMoveSpeed','ResolveMoveSpeedBonus','ResolveMoveSpeedLoss'],
 '攻击速度': ['ResolveWeaponSpeed','GetSelfAttackSpeedFactor','AddAttackSpeedFactor'],
 '护甲回复': ['ArmorRestoreSpeed','SlowDownArmorRestoreSpeed','ArmorRecoveryProbability'],
}
for chain, syms in KEY.items():
    P('')
    P('=' * 78); P('# %s' % chain); P('=' * 78)
    for s in syms:
        hits = V.find_va(s, exact=True)
        if not hits: continue
        for va, nm in hits[:2]:
            f = fb(va)
            P('')
            P('─' * 76)
            P('★ %s @ 0x%08X  fb=0x%X' % (nm, va, f or 0))
            P('─' * 76)
            d = safe_dis(f if f else va, 80)
            for l in d: P(l)
            # 常量
            cs = []
            for l in d:
                m = re.search(r'fmov\s+[sd]\d+,\s*#(-?[\d.]+)', l)
                if m: cs.append('fmov %s' % m.group(1))
                m2 = re.search(r'fmul\s+\w+,\s*\w+,\s*\w+', l)
            if cs: P('  ★ 浮点常量: %s' % cs)

o.close()
print('WROTE work/chains2_log.txt')
