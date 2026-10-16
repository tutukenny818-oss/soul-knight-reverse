#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_loc_master.py —— ★ 用 localize_index.json (17794 词条) 对接天赋 ID 表
   目标：产出 内部名 → 中文名 → ID 的完整三向对照表
"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'loc_master_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W, 'work', 'gamedata_out', 'localize_index.json'),
                    encoding='utf-8'))
P('localize_index 词条 %d' % len(LI))

# 天赋 ID 表
tid = json.load(open(os.path.join(W,'work','talent_id_clean.json'), encoding='utf-8'))
byid = {}
for n, ids in tid.items():
    for i in ids:
        byid.setdefault(i, []).append(n)

# ---- A) 找 buff_<ID>_name 家族 ----
P('')
P('=' * 74); P('A) buff_/talent_ 家族词条'); P('=' * 74)
pats = {
    'buff_ID_name': re.compile(r'^.*?buff_(\d+)_name$'),
    'buff_ID_desc': re.compile(r'^.*?buff_(\d+)_desc$'),
    'talent_ID_name': re.compile(r'^.*?talent_(\d+)_name$'),
    'other': None,
}
fam = collections.defaultdict(list)
for k in LI:
    for tag, pat in pats.items():
        if pat and pat.match(k):
            fam[tag].append(k); break
    else:
        if 'buff_' in k or 'talent_' in k:
            fam['other'].append(k)
for tag in fam:
    P('  %-18s %d 条' % (tag, len(fam[tag])))
    for k in sorted(fam[tag])[:25]:
        v = LI[k]
        P('     %-48s en=%-24s zh=%s' % (k[:48], v.get('en-US','')[:24], v.get('zh-CN','')[:30]))

# ---- B) 全量扫描所有含 talent/buff 的 key ----
P('')
P('=' * 74); P('B) 所有含 talent / buff 的 key（前 200）'); P('=' * 74)
allk = sorted(k for k in LI if 'talent' in k.lower() or 'buff' in k.lower())
P('  总数 %d' % len(allk))
for k in allk[:200]:
    v = LI[k]
    P('  %-60s | %-24s | %s' % (k[:60], v.get('zh-CN','')[:24], v.get('en-US','')[:40]))

# ---- C) 构建 内部名 → 中文 ----
P('')
P('=' * 74); P('C) ★ 内部名 → 中文名（用 ID 桥接）'); P('=' * 74)
rows = []
for k in allk:
    m = re.search(r'(?:buff|talent)_(\d+)_name$', k)
    if not m: continue
    i = int(m.group(1))
    names = byid.get(i, [])
    v = LI[k]
    rows.append((i, ','.join(names) if names else '?', v.get('zh-CN',''), v.get('en-US',''), k))
rows.sort()
P('  命中 %d 条' % len(rows))
P('')
P('  %-6s %-32s %-22s %s' % ('ID', '内部名', '中文', '英文'))
for i, n, zh, en, k in rows:
    P('  %-6d %-32s %-22s %s' % (i, n, zh[:22], en[:50]))

with open(os.path.join(W,'work','TALENT_LOCALIZED.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.writer(f)
    w.writerow(['talent_id','internal_name','zh_CN','en_US','loc_key'])
    for r in rows: w.writerow(r)
P('')
P('WROTE work/TALENT_LOCALIZED.csv (%d 行)' % len(rows))

# ---- D) 所有 key 的命名空间 ----
P('')
P('=' * 74); P('D) key 命名空间统计'); P('=' * 74)
ns = collections.Counter()
for k in LI:
    ns[k.split('/')[0] if '/' in k else k.split('_')[0]] += 1
for k, v in ns.most_common(60):
    P('  %-32s %d' % (k, v))

# ---- E) 保存全量表为 CSV ----
P('')
P('=' * 74); P('E) 导出全量多语言 CSV'); P('=' * 74)
langs = set()
for v in LI.values():
    if isinstance(v, dict): langs |= set(v.keys())
langs = sorted(langs)
P('  语言列 %d: %s' % (len(langs), langs))
with open(os.path.join(W,'work','LOCALIZE_ALL.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.writer(f)
    w.writerow(['key'] + langs)
    for k in sorted(LI):
        v = LI[k]
        if isinstance(v, dict):
            w.writerow([k] + [v.get(l, '') for l in langs])
P('  WROTE work/LOCALIZE_ALL.csv')

o.close()
print('WROTE work/loc_master_log.txt + TALENT_LOCALIZED.csv + LOCALIZE_ALL.csv')
