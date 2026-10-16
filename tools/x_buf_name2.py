#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_buf_name2.py —— ★ 用 Buff_info_NNNN_name 规则补全"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'buf_name2_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))

# 收集所有 Buf*_<tag>_<id>[_<sub>] 组合
# 目标：<ID> → {name, info, desc, upgrade, ...}
fam = collections.defaultdict(dict)
PATS = [
    (re.compile(r'^Buf{1,2}_name_(\d+)$', re.I), 'name'),
    (re.compile(r'^Buf{1,2}_info_(\d+)_name$', re.I), 'name2'),
    (re.compile(r'^Buf{1,2}_info_(\d+)$', re.I), 'info'),
    (re.compile(r'^Buf{1,2}_desc_(\d+)$', re.I), 'desc'),
    (re.compile(r'^Buf{1,2}_upgrade_(\d+)$', re.I), 'upgrade'),
    (re.compile(r'^Buf{1,2}_info_(\d+)_desc$', re.I), 'desc2'),
    (re.compile(r'^Buf{1,2}_(\d+)_name$', re.I), 'name3'),
]
for k, v in LI.items():
    for pat, tag in PATS:
        m = pat.match(k)
        if m:
            i = int(m.group(1))
            if tag not in fam[i] or not fam[i][tag].get('zh-CN'):
                fam[i][tag] = v
            break

P('ID 数 %d' % len(fam))
ids = sorted(fam)

P('')
P('=' * 74); P('★ 全量 Buf 家族（name + info）'); P('=' * 74)
rows = []
for i in ids:
    e = fam[i]
    nv = e.get('name') or e.get('name2') or e.get('name3') or {}
    iv = e.get('info') or {}
    dv = e.get('desc') or e.get('desc2') or {}
    uv = e.get('upgrade') or {}
    zh = nv.get('zh-CN',''); en = nv.get('en-US','')
    iz = iv.get('zh-CN','').replace('\n',' ')
    ie = iv.get('en-US','').replace('\n',' ')
    dz = dv.get('zh-CN','').replace('\n',' ')
    rows.append({'id': i, 'zh_name': zh, 'en_name': en, 'zh_info': iz,
                 'en_info': ie, 'zh_desc': dz, 'zh_upgrade': uv.get('zh-CN','').replace('\n',' '),
                 'zh_TW': nv.get('zh-TW',''), 'ja': nv.get('ja',''), 'ko': nv.get('ko','')})

named = [r for r in rows if r['zh_name']]
P('  有名字 %d / %d' % (len(named), len(rows)))
P('')
P('  %-6s %-24s %-28s %s' % ('ID', '中文名', '英文名', '中文说明'))
for r in rows:
    if not (r['zh_name'] or r['zh_info']): continue
    P('  %-6d %-24s %-28s %s' % (r['id'], r['zh_name'][:24], r['en_name'][:28],
      (r['zh_info'] or r['zh_desc'])[:60]))

with open(os.path.join(W,'work','BUF_FAMILY_V2.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('')
P('  WROTE work/BUF_FAMILY_V2.csv')

# ---- 合并 ----
P('')
P('=' * 74); P('★ 合并到总表'); P('=' * 74)
mp = os.path.join(W,'work','TALENT_FINAL_MASTER.csv')
mrows = list(csv.DictReader(open(mp, encoding='utf-8-sig')))
bym = {r['id']: r for r in rows}
f1 = f2 = 0
for r in mrows:
    try: i = int(r['talent_id'])
    except Exception: continue
    b = bym.get(i)
    if not b: continue
    if not r['zh_name'] and b['zh_name']:
        r['zh_name'] = b['zh_name']; f1 += 1
    if not r['zh_desc']:
        t = b['zh_info'] or b['zh_desc'] or b['zh_upgrade']
        if t: r['zh_desc'] = t; f2 += 1
    if not r['en_name'] and b['en_name']: r['en_name'] = b['en_name']
P('  补名字 %d, 补描述 %d' % (f1, f2))

with open(mp,'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(mrows[0].keys()))
    w.writeheader()
    for r in mrows: w.writerow(r)

P('')
P('=' * 74); P('最终覆盖率'); P('=' * 74)
for col in ('zh_name','zh_desc','internal_name','params'):
    n = sum(1 for r in mrows if r[col])
    P('  %-16s %3d / %d  (%.1f%%)' % (col, n, len(mrows), 100*n/len(mrows)))

left = [r for r in mrows if not r['zh_name']]
if left:
    P('')
    P('  仍未命名 %d:' % len(left))
    for r in left: P('    %-6s %s' % (r['talent_id'], r['internal_name']))

o.close()
print('WROTE work/buf_name2_log.txt + BUF_FAMILY_V2.csv')
