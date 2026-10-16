#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_fill_names.py —— 用 Buff_name_<ID> / Buff_info_<ID> 补全 1xxx/2xxx/3xxx 段的中文名"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'fill_names_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))

# 收集所有 Buff_*_<ID> 形式
buf = collections.defaultdict(dict)
PATS = {
    'Buff_name_': 'name', 'Buff_info_': 'info', 'Buff_upgrade_': 'upgrade',
    'Buff_desc_': 'desc', 'Buff_icon_': 'icon',
}
for k, v in LI.items():
    for pre, tag in PATS.items():
        if k.startswith(pre):
            rest = k[len(pre):]
            try: i = int(rest)
            except Exception: continue
            buf[i][tag] = v
            break

P('收集到 ID %d 个' % len(buf))
ids = sorted(buf)
P('  范围 %d .. %d' % (ids[0], ids[-1]))
P('  段: 1xxx=%d 2xxx=%d 3xxx=%d 4xxx=%d' % (
    sum(1 for i in ids if 1000<=i<2000),
    sum(1 for i in ids if 2000<=i<3000),
    sum(1 for i in ids if 3000<=i<4000),
    sum(1 for i in ids if 4000<=i<5000)))

P('')
P('=' * 74); P('★ Buff_name_<ID> 全表'); P('=' * 74)
P('  %-6s %-20s %-28s %s' % ('ID', '中文名', '英文名', '中文说明'))
rows = []
for i in ids:
    e = buf[i]
    nv = e.get('name', {}); iv = e.get('info', {}); uv = e.get('upgrade', {})
    zh = nv.get('zh-CN',''); en = nv.get('en-US','')
    iz = iv.get('zh-CN','').replace('\n',' '); ie = iv.get('en-US','').replace('\n',' ')
    uz = uv.get('zh-CN','').replace('\n',' ')
    P('  %-6d %-20s %-28s %s' % (i, zh[:20], en[:28], iz[:60]))
    rows.append({
        'buff_id': i, 'zh_name': zh, 'en_name': en,
        'zh_info': iz, 'en_info': ie, 'zh_upgrade': uz,
        'zh_TW': nv.get('zh-TW',''), 'ja': nv.get('ja',''), 'ko': nv.get('ko',''),
    })

with open(os.path.join(W,'work','BUFF_ID_TABLE.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('')
P('  WROTE work/BUFF_ID_TABLE.csv (%d 行)' % len(rows))

# ---- 合并到 TALENT_FINAL_MASTER ----
P('')
P('=' * 74); P('★ 合并中文名到总表'); P('=' * 74)
mp = os.path.join(W,'work','TALENT_FINAL_MASTER.csv')
if os.path.isfile(mp):
    mrows = list(csv.DictReader(open(mp, encoding='utf-8-sig')))
    P('  总表 %d 行' % len(mrows))
    bt = {r['buff_id']: r for r in rows}
    filled = 0
    for r in mrows:
        if r['zh_name']: continue
        try: i = int(r['talent_id'])
        except Exception: continue
        if i in bt:
            b = bt[i]
            r['zh_name'] = b['zh_name']
            r['en_name'] = b['en_name']
            r['zh_desc'] = b['zh_info']
            r['en_desc'] = b['en_info']
            r['tw_name'] = b['zh_TW']
            r['ja_name'] = b['ja']
            r['ko_name'] = b['ko']
            filled += 1
    P('  补全 %d 行' % filled)
    with open(mp,'w',newline='',encoding='utf-8-sig') as f:
        w = csv.DictWriter(f, fieldnames=list(mrows[0].keys()))
        w.writeheader()
        for r in mrows: w.writerow(r)
    P('  WROTE work/TALENT_FINAL_MASTER.csv')
    # 统计
    for col in ('zh_name','zh_desc','internal_name','params'):
        n = sum(1 for r in mrows if r[col])
        P('    %-16s %3d / %d  (%.1f%%)' % (col, n, len(mrows), 100*n/len(mrows)))

# ---- 重新生成 wikitext ----
P('')
P('=' * 74); P('重新生成 wikitext'); P('=' * 74)
if os.path.isfile(mp):
    w = []
    w.append('{{游戏机制|天赋}}')
    w.append('')
    w.append("本表由游戏数据文件解析生成，共 %d 条。'''数值列为游戏内真实参数。'''" % len(mrows))
    w.append('')
    w.append('{| class="wikitable sortable" style="font-size:90%"')
    w.append('! ID !! 内部名 !! 中文名 !! 英文名 !! 描述 !! 等级数 !! 数值')
    for r in mrows:
        def esc(s): return (s or '').replace('|','&#124;').replace('\n',' ')
        w.append('|-')
        w.append('| %s || <code>%s</code> || %s || %s || %s || %s || <code>%s</code>' % (
            r['talent_id'], r['internal_name'], esc(r['zh_name']), esc(r['en_name']),
            esc(r['zh_desc']), r['levels'], esc(r['params'])[:220]))
    w.append('|}')
    txt = '\n'.join(w)
    open(os.path.join(W,'work','WIKI_TALENTS_FINAL.wikitext'),'w',encoding='utf-8').write(txt)
    P('  WROTE work/WIKI_TALENTS_FINAL.wikitext (%d 字符)' % len(txt))

o.close()
print('WROTE work/fill_names_log.txt + BUFF_ID_TABLE.csv')
