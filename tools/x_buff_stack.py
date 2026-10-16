#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_buff_stack.py —— ★★ 天赋数值表全量解析 + 与描述占位符对接"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'buff_stack_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

p = os.path.join(W, 'work', 'cfg_buff_stack.json')
j = json.load(open(p, encoding='utf-8'))
lst = j['list']
P('cfg_buff_stack.json  条目 %d' % len(lst))

# 天赋 ID 表
tid = json.load(open(os.path.join(W,'work','talent_id_clean.json'), encoding='utf-8'))
byid = {}
for n, ids in tid.items():
    for i in ids: byid.setdefault(i, []).append(n)
name2id = {}
for n, ids in tid.items():
    for i in ids: name2id.setdefault(n, i)

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
names = {}; descs = {}
for k, v in LI.items():
    m = re.match(r'activity/talent/buff_(\d+)_name$', k)
    if m: names[int(m.group(1))] = v
    m = re.match(r'activity/talent/buff_(\d+)_desc$', k)
    if m: descs[int(m.group(1))] = v

# ---- 全量列表 ----
P('')
P('=' * 74); P('全部天赋数值条目'); P('=' * 74)
rows = []
for e in lst:
    i_ = e.get('id', '')
    ps = e.get('param', [])
    tid_ = name2id.get(i_, '')
    nv = names.get(tid_, {}) if tid_ else {}
    dv = descs.get(tid_, {}) if tid_ else {}
    # 格式: param[0] 的键
    keys = list(ps[0].keys()) if ps and isinstance(ps[0], dict) else []
    rows.append({
        'internal_id': i_, 'talent_id': tid_,
        'zh_name': nv.get('zh-CN',''), 'en_name': nv.get('en-US',''),
        'levels': len(ps), 'param_keys': ','.join(keys),
        'zh_desc': dv.get('zh-CN','').replace('\n',' '),
        'params_json': json.dumps(ps, ensure_ascii=False),
    })
P('  行数 %d' % len(rows))

# 打印全表
P('')
P('%-6s %-28s %-16s %-5s %-30s' % ('ID', 'internal_id', '中文名', '层数', '参数键'))
for r in rows:
    P('%-6s %-28s %-16s %-5d %s' % (r['talent_id'], r['internal_id'][:28],
      r['zh_name'][:16], r['levels'], r['param_keys'][:30]))

with open(os.path.join(W,'work','TALENT_VALUES.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
    w.writeheader()
    for r in rows: w.writerow(r)
P('')
P('WROTE work/TALENT_VALUES.csv (%d 行)' % len(rows))

# ---- 统计 ----
P('')
P('=' * 74); P('统计'); P('=' * 74)
P('  有 talent_id 的 %d' % sum(1 for r in rows if r['talent_id']))
P('  有中文名 %d' % sum(1 for r in rows if r['zh_name']))
lv = collections.Counter(r['levels'] for r in rows)
P('  层数分布: %s' % dict(lv))
kc = collections.Counter()
for r in rows:
    for k in r['param_keys'].split(','):
        if k: kc[k] += 1
P('  参数键 top20:')
for k, v in kc.most_common(20):
    P('     %-24s %d' % (k, v))

# ---- 完整数值展开（每个等级一行）----
P('')
P('=' * 74); P('展开为 等级 × 参数 长表'); P('=' * 74)
flat = []
for r in rows:
    ps = json.loads(r['params_json'])
    for li, pv in enumerate(ps, 1):
        for k, v in pv.items():
            flat.append({
                'talent_id': r['talent_id'], 'internal_id': r['internal_id'],
                'zh_name': r['zh_name'], 'level': li, 'param': k, 'value': v,
            })
P('  扁平行数 %d' % len(flat))
with open(os.path.join(W,'work','TALENT_VALUES_FLAT.csv'),'w',newline='',encoding='utf-8-sig') as f:
    w = csv.DictWriter(f, fieldnames=['talent_id','internal_id','zh_name','level','param','value'])
    w.writeheader()
    for r in flat: w.writerow(r)
P('  WROTE work/TALENT_VALUES_FLAT.csv')

# ---- 与描述对接示例 ----
P('')
P('=' * 74); P('★ 描述占位符 ↔ 数值 对接示例'); P('=' * 74)
for r in rows:
    if not r['talent_id'] or not r['zh_desc']: continue
    if '{0}' not in r['zh_desc']: continue
    ps = json.loads(r['params_json'])
    if not ps: continue
    P('')
    P('  [%s] %s  (ID %s)' % (r['internal_id'], r['zh_name'], r['talent_id']))
    P('     描述: %s' % r['zh_desc'][:100])
    for li, pv in enumerate(ps, 1):
        P('      Lv%d: %s' % (li, json.dumps(pv, ensure_ascii=False)))
    if len(rows) > 40: break

o.close()
print('WROTE work/buff_stack_log.txt + TALENT_VALUES.csv + TALENT_VALUES_FLAT.csv')
