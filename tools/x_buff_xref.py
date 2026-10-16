#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_buff_xref.py —— ★ 旧版 Buff ↔ 新版天赋 ID 交叉映射
   依据：Buff_info_00 (穿心膛线) ↔ buff_4020_name (穿心膛线)
   方法：用中文名/英文名匹配两个系统
"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'buff_xref_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))

# 旧版
old_name = {}; old_info = {}
for k, v in LI.items():
    m = re.match(r'^Buff_name_(\d+)$', k)
    if m: old_name[int(m.group(1))] = v
    m = re.match(r'^Buff_info_(\d+)$', k)
    if m: old_info[int(m.group(1))] = v

# 新版
new_name = {}; new_desc = {}
for k, v in LI.items():
    m = re.match(r'^activity/talent/buff_(\d+)_name$', k)
    if m: new_name[int(m.group(1))] = v
    m = re.match(r'^activity/talent/buff_(\d+)_desc$', k)
    if m: new_desc[int(m.group(1))] = v

P('旧版 name %d  info %d' % (len(old_name), len(old_info)))
P('新版 name %d  desc %d' % (len(new_name), len(new_desc)))

# ---- 按中文名匹配 ----
P('')
P('=' * 74); P('★ 中文名匹配（旧版 ↔ 新版）'); P('=' * 74)
zh_new = {}
for i, v in new_name.items():
    t = v.get('zh-CN','').strip()
    if t: zh_new.setdefault(t, []).append(i)
en_new = {}
for i, v in new_name.items():
    t = v.get('en-US','').strip().lower()
    if t: en_new.setdefault(t, []).append(i)

matched = []
for i in sorted(old_name):
    v = old_name[i]
    zh = v.get('zh-CN','').strip()
    en = v.get('en-US','').strip()
    newids = zh_new.get(zh, []) or en_new.get(en.lower(), [])
    if newids:
        matched.append((i, zh, en, newids))
P('  匹配上 %d 条' % len(matched))
P('')
P('  %-6s %-16s %-24s %s' % ('oldID', '中文名', '英文名', '新版ID'))
for i, zh, en, nids in matched:
    P('  %-6d %-16s %-24s %s' % (i, zh[:16], en[:24], nids))

with open(os.path.join(W,'work','BUFF_XREF.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.writer(f); w.writerow(['old_buff_id','zh_name','en_name','new_talent_ids'])
    for r in matched: w.writerow(r)
P('')
P('  WROTE work/BUFF_XREF.csv')

# ---- 用旧版补全新版缺失的 ID ----
P('')
P('=' * 74); P('★ 用旧版表补全新版缺失的 8 个天赋'); P('=' * 74)
tid = json.load(open(os.path.join(W,'work','talent_id_clean.json'), encoding='utf-8'))
byid = {}
for n, ids in tid.items():
    for i in ids: byid.setdefault(i, []).append(n)
MISS = ['BattleHardened','BraveHeart','ElementalCritical','FireShield','IceShield',
        'MovementCharge','PoisonShield','ShotgunIncrease']
for n in MISS:
    P('')
    P('  --- %s ---' % n)
    # 在旧版表里找英文名匹配
    for i in sorted(old_name):
        v = old_name[i]
        en = v.get('en-US','')
        zh = v.get('zh-CN','')
        info = old_info.get(i, {})
        P('     old %-4d %-18s %-26s %s' % (i, zh[:18], en[:26],
          info.get('zh-CN','')[:50]))

# ---- 旧版全表补充 desc ----
P('')
P('=' * 74); P('★ 旧版完整表（name + info + upgrade）'); P('=' * 74)
old_up = {}
for k, v in LI.items():
    m = re.match(r'^Buff_upgrade_(\d+)$', k)
    if m: old_up[int(m.group(1))] = v

allids = sorted(set(old_name) | set(old_info) | set(old_up))
P('  总 ID %d' % len(allids))
rows = []
for i in allids:
    nv = old_name.get(i, {}); iv = old_info.get(i, {}); uv = old_up.get(i, {})
    rows.append({
        'id': i, 'zh_name': nv.get('zh-CN',''), 'en_name': nv.get('en-US',''),
        'zh_info': iv.get('zh-CN','').replace('\n',' '),
        'en_info': iv.get('en-US','').replace('\n',' '),
        'zh_upgrade': uv.get('zh-CN','').replace('\n',' '),
        'tw_name': nv.get('zh-TW',''), 'ja': nv.get('ja',''), 'ko': nv.get('ko',''),
    })
with open(os.path.join(W,'work','OLD_BUFF_FULL.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('  WROTE work/OLD_BUFF_FULL.csv (%d 行)' % len(rows))

# 完整打印
P('')
P('  %-6s %-16s %-22s %s' % ('ID', '中文名', '英文名', '中文说明'))
for r in rows:
    P('  %-6s %-16s %-22s %s' % (r['id'], r['zh_name'][:16], r['en_name'][:22],
      r['zh_info'][:60]))

o.close()
print('WROTE work/buff_xref_log.txt + BUFF_XREF.csv + OLD_BUFF_FULL.csv')
