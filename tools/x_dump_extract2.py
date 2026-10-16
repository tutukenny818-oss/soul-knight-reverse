#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_dump_extract2.py —— 收紧关键词，目标 < 2 MB"""
import sys, io, os, re, json
W = r'D:\元气骑士mod'
SRC = os.path.join(W, 'decompiled', 'dump_full', 'dump.cs')
OUT = os.path.join(W, 'work', '_dump_essential.cs')
o = io.open(os.path.join(W, 'work', '_dump_extract2.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

# ★ 收紧：只匹配「名字里精确含这些片段」的类
MUST = [
 # 战斗核心（精确）
 'DamageResolver', 'CalculateFinalDamage', 'CalculateCriticalDamage',
 'ApplyCritical', 'ApplyNonCritical', 'ApplyPlayerCritical',
 'DamageInfo', 'DamageData', 'DamageType', 'DamageFactor',
 'CriticFactor', 'CriticDamage', 'ChanceBonus',
 'RoleAttribute', 'RGPlayer', 'RGCharacter', 'RGEnemy', 'RGBaseController',
 'RGEController', 'RGController', 'RGWeapon', 'RGGBullet',
 # 天赋
 'Talent', 'BuffStack', 'BuffInfo', 'BuffManager', 'BuffBase',
 # 加密（精确）
 'CryptUtil', 'SaltKey', 'AesEncryption', 'AesDecryption', 'PasswordHash',
 'Rfc2898', 'DecryptBytes', 'DecryptString',
 # 配置
 'ForcePatchConst', 'GameConfigParse', 'CsvLoader', 'GlobalizeManager',
 'GameConfigManager', 'RuntimeLuaSourceLoader', 'DebugLuaManager',
 # 武器/子弹关键
 'WeaponsDropData', 'CharacterData', 'BulletInfo',
 # ARAM 妖气
 'YaoQi',
]

lines = open(SRC, encoding='utf-8', errors='replace').read().split('\n')
P('总行数 %d' % len(lines))

CLS = re.compile(r'^\s*(?:\[.*\]\s*)?(?:public|internal|private)?\s*(?:sealed\s+|abstract\s+|static\s+|partial\s+)*(class|struct|enum|interface)\s+(\w+)')
out = []
kept = set()
i = 0
while i < len(lines):
    ln = lines[i]
    m = CLS.match(ln)
    if m:
        name = m.group(2)
        if any(k.lower() in name.lower() for k in MUST):
            j = i
            while j > 0 and (lines[j-1].strip().startswith('[') or
                             lines[j-1].strip().endswith('_c') or
                             lines[j-1].strip() == ''):
                j -= 1
            k = i; brace = 0; started = False
            while k < len(lines):
                brace += lines[k].count('{') - lines[k].count('}')
                if '{' in lines[k]: started = True
                if started and brace <= 0: break
                k += 1
            body = lines[max(0,j):k+1]
            if 3 < len(body) < 800:
                out.append('// ' + '-'*68)
                out.append('// %s  [%s]' % (name, m.group(1)))
                out.append('// ' + '-'*68)
                out.extend(body)
                out.append('')
                kept.add(name)
            i = k + 1
            continue
    i += 1

P('保留类 %d 个' % len(kept))

hdr = '''// ==============================================================================
// Soul Knight 8.6.0 — 核心战斗/天赋/加密 类型摘录
//
// 来源：Il2CppDumper 从解密后的 libil2cpp.so + global-metadata.dat 提取
// 原始：47,503 个类型（25.4 MB）
// 本文：%d 个核心类型
//
// 完整 dump 见 Release
// ==============================================================================

''' % len(kept)
open(OUT, 'w', encoding='utf-8').write(hdr + '\n'.join(out))
sz = os.path.getsize(OUT)
P('输出 %.2f MB' % (sz/1048576))
P('')
P('保留的类:')
for n in sorted(kept):
    P('  %s' % n)
json.dump(sorted(kept), open(os.path.join(W,'work','_dump_kept2.json'),'w',
          encoding='utf-8'), ensure_ascii=False, indent=1)
o.close()
print('WROTE work/_dump_extract2.txt')
