#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_dmg4.py —— 稳健版：捕获 V.dis 的 stdout 污染"""
import sys, io, os, json, re, contextlib
sys.path.insert(0, r'D:\元气骑士mod\tools')
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'dmg4_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

import x_vautil as V

hits = V.find_va_kw('Damage')
names = {}
for it in hits:
    try:
        va, name = int(it[0]), str(it[1])
    except Exception:
        continue
    names.setdefault(name, va)
P('唯一符号 %d' % len(names))

def safe_dis(va, n=90):
    buf = io.StringIO()
    try:
        with contextlib.redirect_stdout(buf):
            d = V.dis(va, n)
        return d if isinstance(d, list) else []
    except BaseException:
        return []

# 分类
real, stub, bad = [], [], []
for n, va in sorted(names.items()):
    d = safe_dis(va, 14)
    if not d:
        bad.append((n, va)); continue
    txt = '\n'.join(str(x) for x in d)
    (stub if 'IsPatched' in txt else real).append((n, va))

P('')
P('=' * 74)
P('真实 %d / 桩 %d / 无效 %d' % (len(real), len(stub), len(bad)))
P('=' * 74)
P('')
P('★ 真实实现（可反汇编）%d 个:' % len(real))
for n, va in real:
    P('   %-52s 0x%08X' % (n, va))
P('')
P('★ 桩（IFix 覆盖）%d 个:' % len(stub))
for n, va in stub:
    P('   %-52s 0x%08X' % (n, va))
P('')
P('★ 无效 RVA %d 个:' % len(bad))
for n, va in bad:
    P('   %-52s 0x%08X' % (n, va))

# 反汇编关键真实函数
KEY = ['CalculateFinalDamage','CalculateCriticalDamage','GetCriticDamageFactor',
       'ModifyDamage','ApplyDamage','GetFinalDamage','GetEffectiveDamage',
       'GetExtraDamage','GetMarkedExtraDamageFactor',
       'GetRuntimeDamageBonus','GetAddDamagePercent','GetMarkDamageReductionFactor',
       'GetScaledConfiguredDamage','GetBulletDamage','MakeDamage','CanApplyDamage',
       'ApplyNonCriticalDamageReduction','ApplyPlayerCriticalToResolvedDamage',
       'GetCriticFactor','GetCriticDamageFactor']
P('')
P('=' * 74); P('★ 核心函数反汇编'); P('=' * 74)
for name in KEY:
    if name not in names: continue
    va = names[name]
    d = safe_dis(va, 90)
    P('')
    P('─' * 76)
    P('★ %s @ 0x%08X' % (name, va))
    P('─' * 76)
    if not d:
        P('  （无效 RVA）'); continue
    for line in d:
        P(str(line).rstrip())
    if 'IsPatched' in '\n'.join(str(x) for x in d):
        P('  ⟹ ⚠ IFix 桩')

o.close()
print('WROTE work/dmg4_log.txt')
