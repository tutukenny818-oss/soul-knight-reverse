#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_all_systems.py —— 全量系统词条挖掘
   localize_index 有 17794 词条，按命名空间分类，产出所有游戏系统的完整词条
"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'all_systems_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
P('总词条 %d' % len(LI))

# ---- 命名空间统计 ----
P('')
P('=' * 74); P('顶层命名空间统计'); P('=' * 74)
ns = collections.Counter()
for k in LI:
    parts = k.split('/')
    ns[parts[0] if len(parts) > 1 else k.split('_')[0]] += 1
for k, v in ns.most_common(80):
    P('  %-36s %d' % (k, v))

# ---- Troop2/Talent 全表（雇佣兵/随从天赋）----
P('')
P('=' * 74); P('★ Troop2/Talent 完整表（雇佣兵/随从天赋）'); P('=' * 74)
troop_n = []; troop_d = []
for k, v in LI.items():
    if k.startswith('Troop2/Talent/Name/'):
        troop_n.append((k.split('/')[-1], v.get('zh-CN',''), v.get('en-US',''), v.get('zh-TW','')))
    elif k.startswith('Troop2/Talent/Desc/'):
        troop_d.append((k.split('/')[-1], v.get('zh-CN','')))
troop_n.sort(); troop_d.sort()
P('  Name %d, Desc %d' % (len(troop_n), len(troop_d)))
dd = {n: z for n, z in troop_d}
P('')
P('  %-28s %-16s %-24s %s' % ('id', '中文名', '英文名', '中文描述'))
for n, zh, en, tw in troop_n:
    P('  %-28s %-16s %-24s %s' % (n, zh[:16], en[:24], dd.get(n, '')[:60]))
with open(os.path.join(W,'work','TROOP_TALENTS.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.writer(f); w.writerow(['id','zh_name','en_name','zh_TW','zh_desc'])
    for n, zh, en, tw in troop_n:
        w.writerow([n, zh, en, tw, dd.get(n,'')])
P('')
P('  WROTE work/TROOP_TALENTS.csv (%d 行)' % len(troop_n))

# ---- 所有命名空间逐类打印 ----
P('')
P('=' * 74); P('各命名空间样本'); P('=' * 74)
groups = collections.defaultdict(list)
for k, v in LI.items():
    top = k.split('/')[0] if '/' in k else '_root'
    groups[top].append((k, v))
for g in sorted(groups, key=lambda x: -len(groups[x]))[:30]:
    lst = groups[g]
    P('')
    P('  === %s (%d) ===' % (g, len(lst)))
    for k, v in sorted(lst)[:12]:
        P('     %-62s | %-24s | %s' % (k[:62], v.get('zh-CN','')[:24], v.get('en-US','')[:36]))
    if len(lst) > 12:
        P('     ... 共 %d 条' % len(lst))

# ---- 全量导出分类 CSV ----
P('')
P('=' * 74); P('导出分类 CSV'); P('=' * 74)
outd = os.path.join(W, 'work', 'wiki_systems')
os.makedirs(outd, exist_ok=True)
cnt = 0
for g in groups:
    if len(groups[g]) < 5: continue
    safe = re.sub(r'[^A-Za-z0-9_]', '_', g)[:40]
    with open(os.path.join(outd, '%s.csv' % safe), 'w', newline='', encoding='utf-8-sig') as f:
        w = csv.writer(f)
        w.writerow(['key','zh-CN','zh-TW','en-US','ja','ko'])
        for k, v in sorted(groups[g]):
            w.writerow([k, v.get('zh-CN',''), v.get('zh-TW',''),
                        v.get('en-US',''), v.get('ja',''), v.get('ko','')])
    cnt += 1
P('  导出 %d 个分类 CSV 到 work/wiki_systems/' % cnt)

o.close()
print('WROTE work/all_systems_log.txt + TROOP_TALENTS.csv + work/wiki_systems/')
