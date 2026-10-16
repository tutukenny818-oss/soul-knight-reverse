#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_talent_final.py —— ★ 最终天赋表：补全剩余 ID 映射 + 完整交付
   问题：112 条 name 里有一批内部名是 "?"（4045 等），因为 tidy 表里没这些 ID
   解法：用 desc 里的语义关键词反查 runtime_lua 名单
"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'talent_final_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
BUF = set(man['buff']); TAL = set(man['talent'])
KNOWN = sorted(BUF | TAL)

tid = json.load(open(os.path.join(W,'work','talent_id_clean.json'), encoding='utf-8'))
byid = {}
for n, ids in tid.items():
    for i in ids:
        byid.setdefault(i, []).append(n)

# ---- 收集 name / desc ----
names = {}; descs = {}; descs_en = {}
for k, v in LI.items():
    m = re.match(r'activity/talent/buff_(\d+)_name$', k)
    if m: names[int(m.group(1))] = v
    m = re.match(r'activity/talent/buff_(\d+)_desc$', k)
    if m: descs[int(m.group(1))] = v

allid = sorted(set(names) | set(descs))
P('ID 总数 %d （name %d, desc %d）' % (len(allid), len(names), len(descs)))

# ---- 已映射 vs 未映射 ----
mapped = {}   # id -> [internal names]
unmapped = []
for i in allid:
    ns = byid.get(i, [])
    if ns: mapped[i] = ns
    else: unmapped.append(i)
P('  已映射 %d / 未映射 %d' % (len(mapped), len(unmapped)))
P('  未映射 ID: %s' % unmapped)

# ---- 用中文名反查 runtime_lua 名单 ----
P('')
P('=' * 74); P('用中文名/英文名反查未映射 ID 的内部名'); P('=' * 74)
# 手工映射表（基于语义判断）
MANUAL = {
    4003: 'StayFocused',      # 专注时刻 - 可能不在 runtime_lua
    4012: 'SwitchWeaponAmplification',   # 切枪增幅 - 在 runtime_lua！
    4020: 'PenetratingRifling',          # 穿心膛线 - 在 runtime_lua！
    4026: 'Sanda',                       # 散打 - 在 runtime_lua！
    4033: 'CriticalExplosion',           # 暴击爆破 - 在 runtime_lua！
    4034: 'ThrowingExpert',              # 投掷专家 - 在 runtime_lua！
    4037: 'ThrowingBarrage',             # 暗器齐发 - 在 runtime_lua！
    4045: 'FocusedBeam',                 # 聚焦射线 - 在 runtime_lua！
    4051: 'SwordQi',                     # 剑气 - 在 runtime_lua！
    4055: 'SkillAdvancement',            # 技能进阶 - 在 runtime_lua！
    4057: 'PracticeMakesPerfect',        # 熟能生巧 - 在 runtime_lua！
    4063: 'SwiftPower',                  # 更快更强 - 在 runtime_lua！
    4064: 'KineticStrike',               # 动能打击 - 在 runtime_lua！
    4067: 'LifeSiphon',                  # 生命汲取 - 在 runtime_lua！
    4069: 'WarriorHeart',                # 勇者之心
    4071: 'RockSolid',                   # 坚若磐石 - 在 runtime_lua！
    4074: 'BestPartner',                 # 最好的伙伴 - 在 runtime_lua！
    4080: 'SpikeProtection',             # 尖刺防护 - 在 runtime_lua！
    4084: 'MissileBarrage',              # 飞弹连发 - 在 runtime_lua！
    4088: 'PoisonNeedleBarrage',         # 毒针齐射 - 在 runtime_lua！
    4101: 'SmallBigGain',                # 以小博大 - 在 runtime_lua！
    4102: 'DisplacementBlast',           # 位移爆破 - 在 runtime_lua！
    4105: 'Windrunner',                  # 疾行生风
    4110: 'PotionEnhancement',           # 恢复强化 - 在 runtime_lua！
    4111: 'NextGenerationSupport',       # 次时代增援 - 在 runtime_lua！
    4115: 'SpikedShell',                 # 尖刺外壳 - 在 runtime_lua！
}
P('  手工映射 %d 条' % len(MANUAL))
for i in sorted(MANUAL):
    ok = '✓在runtime_lua' if MANUAL[i] in KNOWN else '✗不在'
    P('    %d -> %-30s %s' % (i, MANUAL[i], ok))

# 合并
final_map = dict(mapped)
for i, n in MANUAL.items():
    if i not in final_map:
        final_map[i] = [n]
    elif n not in final_map[i]:
        final_map[i].append(n)

# ---- 反查未覆盖的 runtime_lua 名字 ----
covered = set()
for ns in final_map.values():
    covered |= set(ns)
leftover = sorted(set(KNOWN) - covered)
P('')
P('=' * 74); P('runtime_lua 名单里还没对上 ID 的（%d 个）' % len(leftover)); P('=' * 74)
for i in range(0, len(leftover), 3):
    P('  ' + '  '.join('%-30s' % x for x in leftover[i:i+3]))

# ---- 生成最终表 ----
P('')
P('=' * 74); P('★ 最终天赋表'); P('=' * 74)
rows = []
for i in allid:
    ns = final_map.get(i, ['?'])
    nv = names.get(i, {}); dv = descs.get(i, {})
    rows.append({
        'talent_id': i,
        'internal_name': ','.join(ns),
        'zh_name': nv.get('zh-CN', ''),
        'en_name': nv.get('en-US', ''),
        'zh_desc': dv.get('zh-CN', '').replace('\n', ' '),
        'en_desc': dv.get('en-US', '').replace('\n', ' '),
        'zh_TW': nv.get('zh-TW', ''),
        'ja': nv.get('ja', ''),
        'ko': nv.get('ko', ''),
        'in_runtime_lua': any(n in KNOWN for n in ns),
    })
with open(os.path.join(W,'work','TALENT_MASTER.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('  WROTE work/TALENT_MASTER.csv (%d 行)' % len(rows))

# 统计
P('')
incnt = sum(1 for r in rows if r['in_runtime_lua'])
P('  有 runtime_lua 实现: %d / %d' % (incnt, len(rows)))
P('  有中文名: %d' % sum(1 for r in rows if r['zh_name']))
P('  有中文描述: %d' % sum(1 for r in rows if r['zh_desc']))

# 打印完整表
P('')
P('%-6s %-32s %-16s %s' % ('ID', '内部名', '中文名', '中文描述'))
for r in rows:
    P('%-6d %-32s %-16s %s' % (r['talent_id'], r['internal_name'][:32],
      r['zh_name'][:16], r['zh_desc'][:80]))

# ---- 也导出所有非 4xxx 段 ----
P('')
P('=' * 74); P('其他段的 buff 名（10xx/20xx/30xx）'); P('=' * 74)
other = []
for k, v in LI.items():
    m = re.match(r'activity/(?:talent|season|mode)/buff_(\d+)_name$', k)
    if m and not (4000 <= int(m.group(1)) < 5000):
        other.append((int(m.group(1)), v.get('zh-CN',''), v.get('en-US',''), k))
other.sort()
P('  条目 %d' % len(other))
for i, zh, en, k in other[:60]:
    P('  %-6d %-20s %s' % (i, zh[:20], en[:40]))

o.close()
print('WROTE work/talent_final_log.txt + TALENT_MASTER.csv')
