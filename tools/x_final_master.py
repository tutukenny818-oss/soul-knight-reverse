#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_final_master.py —— ★ 终极天赋总表：合并所有来源
   来源：
     1) talent_id_clean.json（内存提取的 Name: ID）
     2) BUFF_XREF.csv（旧版↔新版匹配）
     3) MANUAL 映射（语义判断）
     4) localize_index（中文名/描述）
     5) cfg_buff_stack.json（数值）
     6) ecb_layers.json（继承层）
"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'final_master_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
layers = json.load(open(os.path.join(W,'work','ecb_layers.json'), encoding='utf-8'))
tid = json.load(open(os.path.join(W,'work','talent_id_clean.json'), encoding='utf-8'))
BUF = set(man['buff']); TAL = set(man['talent']); COM = set(man['common'])
KNOWN = BUF | TAL

# ID 映射（内存 + 手工 + xref）
byid = collections.defaultdict(set)
for n, ids in tid.items():
    for i in ids: byid[i].add(n)

MANUAL = {
    4003:'StayFocused', 4012:'SwitchWeaponAmplification', 4020:'PenetratingRifling',
    4026:'Sanda', 4033:'CriticalExplosion', 4034:'ThrowingExpert', 4037:'ThrowingBarrage',
    4045:'FocusedBeam', 4051:'SwordQi', 4055:'SkillAdvancement', 4057:'PracticeMakesPerfect',
    4063:'SwiftPower', 4064:'KineticStrike', 4067:'LifeSiphon', 4069:'WarriorHeart',
    4071:'RockSolid', 4074:'BestPartner', 4080:'SpikeProtection', 4084:'MissileBarrage',
    4088:'PoisonNeedleBarrage', 4101:'SmallBigGain', 4102:'DisplacementBlast',
    4105:'Windrunner', 4110:'PotionEnhancement', 4111:'NextGenerationSupport',
    4115:'SpikedShell',
}
for i, n in MANUAL.items():
    byid[i].add(n)

# xref
if os.path.isfile(os.path.join(W,'work','BUFF_XREF.csv')):
    for r in csv.DictReader(open(os.path.join(W,'work','BUFF_XREF.csv'), encoding='utf-8-sig')):
        try:
            nid = int(r['new_talent_ids'].strip('[]').split(',')[0])
        except Exception:
            continue
        byid[nid].add('(from old buff %s)' % r['old_buff_id'])

# 名称与描述
names = {}; descs = {}
for k, v in LI.items():
    m = re.match(r'activity/talent/buff_(\d+)_name$', k)
    if m: names[int(m.group(1))] = v
    m = re.match(r'activity/talent/buff_(\d+)_desc$', k)
    if m: descs[int(m.group(1))] = v

# 数值
vals = {}
p = os.path.join(W,'work','cfg_buff_stack.json')
if os.path.isfile(p):
    for e in json.load(open(p, encoding='utf-8'))['list']:
        vals[e['id']] = e.get('param', [])

# 层级
layer = {}
for off, v in layers.items():
    off = int(off)
    if off < 56: continue
    lvl = 'L1' if off < 72 else 'L2' if off < 88 else 'L3'
    for n in v.get('members', []): layer[n] = lvl

# 内部名 → 数值 key 反查
n2v = {}
for i in byid:
    for n in byid[i]:
        if n in vals: n2v[i] = vals[n]

# ---- 组装 ----
allids = sorted(set(names) | set(descs) | set(byid))
P('总 ID %d' % len(allids))
rows = []
for i in allids:
    ns = sorted(n for n in byid.get(i, []) if not n.startswith('('))
    nv = names.get(i, {}); dv = descs.get(i, {})
    v = vals.get(ns[0], []) if ns else []
    rows.append({
        'talent_id': i,
        'internal_name': ','.join(ns),
        'zh_name': nv.get('zh-CN',''),
        'tw_name': nv.get('zh-TW',''),
        'en_name': nv.get('en-US',''),
        'ja_name': nv.get('ja',''),
        'ko_name': nv.get('ko',''),
        'zh_desc': dv.get('zh-CN','').replace('\n',' '),
        'en_desc': dv.get('en-US','').replace('\n',' '),
        'layer': layer.get(ns[0], '') if ns else '',
        'levels': len(v),
        'params': json.dumps(v, ensure_ascii=False) if v else '',
        'has_runtime_lua': ('talent' if any(n in TAL for n in ns) else
                            'buff' if any(n in BUF for n in ns) else ''),
    })

with open(os.path.join(W,'work','TALENT_FINAL_MASTER.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('WROTE work/TALENT_FINAL_MASTER.csv (%d 行)' % len(rows))

# 统计
P('')
P('=' * 74); P('覆盖率统计'); P('=' * 74)
for col in ('zh_name','zh_desc','internal_name','layer','params','has_runtime_lua'):
    n = sum(1 for r in rows if r[col])
    P('  %-18s %3d / %d  (%.1f%%)' % (col, n, len(rows), 100*n/len(rows)))

# 打印
P('')
P('%-6s %-30s %-14s %-6s %-6s %s' % ('ID','内部名','中文名','层','数值','中文描述'))
for r in rows:
    P('%-6d %-30s %-14s %-6s %-6s %s' % (r['talent_id'], r['internal_name'][:30],
      r['zh_name'][:14], r['layer'] or '-', r['levels'] or '-', r['zh_desc'][:50]))

# ---- 生成最终 wiki wikitext ----
P('')
P('=' * 74); P('生成最终 wikitext'); P('=' * 74)
w = []
w.append('{{游戏机制|天赋}}')
w.append('')
w.append("本表由游戏数据文件解析生成，共 %d 条天赋。'''数值列为游戏内 1-5 级的真实参数。'''" % len(rows))
w.append('')
w.append('{| class="wikitable sortable" style="font-size:90%"')
w.append('! ID !! 内部名 !! 中文名 !! 英文名 !! 描述 !! 等级数 !! 数值 !! 层')
for r in rows:
    def esc(s): return (s or '').replace('|', '&#124;').replace('\n', ' ')
    w.append('|-')
    w.append('| %d || <code>%s</code> || %s || %s || %s || %s || <code>%s</code> || %s' % (
        r['talent_id'], r['internal_name'], esc(r['zh_name']), esc(r['en_name']),
        esc(r['zh_desc']), r['levels'] or '', esc(r['params'])[:200], r['layer'] or ''))
w.append('|}')
txt = '\n'.join(w)
open(os.path.join(W,'work','WIKI_TALENTS_FINAL.wikitext'),'w',encoding='utf-8').write(txt)
P('  WROTE work/WIKI_TALENTS_FINAL.wikitext (%d 字符)' % len(txt))

# ---- 数值表单独 wikitext ----
rows_v = [r for r in rows if r['params']]
w2 = []
w2.append('{{游戏机制|天赋}}')
w2.append('')
w2.append('== 天赋数值（各等级参数） ==')
w2.append('')
w2.append('游戏内天赋参数随天赋等级变化，下表为 1-5 级的真实数值。')
w2.append('')
w2.append('{| class="wikitable sortable" style="font-size:90%"')
w2.append('! ID !! 中文名 !! 等级数 !! 各等级参数')
for r in rows_v:
    w2.append('|-')
    w2.append('| %d || %s || %d || <code>%s</code>' % (
        r['talent_id'], r['zh_name'].replace('|','&#124;'), r['levels'],
        r['params'].replace('|','&#124;')))
w2.append('|}')
txt2 = '\n'.join(w2)
open(os.path.join(W,'work','WIKI_TALENT_VALUES.wikitext'),'w',encoding='utf-8').write(txt2)
P('  WROTE work/WIKI_TALENT_VALUES.wikitext (%d 字符)' % len(txt2))

o.close()
print('WROTE work/final_master_log.txt + TALENT_FINAL_MASTER.csv + 2 wikitext')
