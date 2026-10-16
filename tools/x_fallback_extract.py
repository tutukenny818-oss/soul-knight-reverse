#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_fallback_extract.py —— ★★★★★★★★ 挖掘 IFix 桩的 fallback 分支（原始实现）
   重大修正：
     IFix 桩模式：
       bl IsPatched
       tbz w0, #0, <FALLBACK>   ← w0==0（未patch）时跳 FALLBACK
       ...（patch 路径，跳到 __Gen_Wrap_N）
       <FALLBACK>:              ← ★ 这里就是原始实现！
   所以：所有"桩"函数其实都有可读的原始实现
"""
import sys, io, os, json, re, contextlib
sys.path.insert(0, r'D:\元气骑士mod\tools')
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'fallback_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

import x_vautil as V

def safe_dis(va, n=200):
    buf = io.StringIO()
    try:
        with contextlib.redirect_stdout(buf):
            d = V.dis(va, n)
        return d if isinstance(d, list) else []
    except BaseException:
        return []

def parse_addr(line):
    m = re.match(r'\s*0x([0-9A-Fa-f]+)', line)
    return int(m.group(1), 16) if m else None

def find_fallback(va, name=''):
    """返回 (fallback_addr, 完整反汇编)"""
    d = safe_dis(va, 30)
    if not d: return None, []
    txt = [str(x) for x in d]
    for i, line in enumerate(txt):
        if 'IsPatched' in line:
            # 找下一条 tbz
            for j in range(i+1, min(i+3, len(txt))):
                m = re.search(r'tbz\s+w0,\s*#0,\s*#(0x[0-9A-Fa-f]+)', txt[j])
                if m:
                    return int(m.group(1), 16), txt
    return None, txt

# ══════════ 1. 收集所有函数 ══════════
P('=' * 74); P('1. 收集符号'); P('=' * 74)
allnames = {}
for kw in ['Damage', 'Critic', 'Critical', 'Attack', 'Hurt', 'Hp', 'Armor']:
    try:
        for it in V.find_va_kw(kw):
            try:
                va, nm = int(it[0]), str(it[1])
            except Exception:
                continue
            allnames.setdefault(nm, va)
    except Exception:
        pass
P('  符号 %d 个' % len(allnames))

# ══════════ 2. 找有 fallback 的函数 ══════════
P('')
P('=' * 74); P('2. ★ 找 fallback 分支'); P('=' * 74)
withfb = []
for n, va in sorted(allnames.items()):
    fb, txt = find_fallback(va, n)
    if fb:
        withfb.append((n, va, fb))
P('  有 fallback 的函数 %d 个:' % len(withfb))
for n, va, fb in withfb:
    P('     %-50s 0x%08X → fb 0x%08X' % (n, va, fb))

# ══════════ 3. ★ 反汇编关键函数的 fallback ══════════
P('')
P('=' * 74); P('3. ★★★ 关键函数的 FALLBACK（原始实现）'); P('=' * 74)
KEY = ['CalculateFinalDamage', 'CalculateCriticalDamage', 'ModifyDamage',
       'GetCriticDamageFactor', 'GetCriticFactor', 'ApplyDamage',
       'GetDamage', 'GetEffectiveDamage', 'GetFinalDamage',
       'ResolveDamageRatio', 'ApplyCriticalResultDamageModifier',
       'ApplyNonCriticalDamageReduction', 'ApplyPlayerCriticalToResolvedDamage',
       'GetExtraDamage', 'GetRuntimeDamageBonus', 'GetAddDamagePercent',
       'HurtDamage', 'ApplyAttackDamageMultiplierTalentDelta']
for name in KEY:
    if name not in allnames: continue
    va = allnames[name]
    fb, txt = find_fallback(va, name)
    P('')
    P('─' * 78)
    P('★ %s @ 0x%08X' % (name, va))
    if fb:
        P('  ★ FALLBACK @ 0x%08X（原始实现）' % fb)
    else:
        P('  （无 fallback）')
    P('─' * 78)
    if fb:
        d = safe_dis(fb, 120)
        for line in d:
            P(str(line).rstrip())
    else:
        for line in txt:
            P(str(line).rstrip())

o.close()
print('WROTE work/fallback_log.txt')
