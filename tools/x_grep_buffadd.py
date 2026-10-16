#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_grep_buffadd.py —— ★★★★★★★ 在解码文本里直接搜 Buffs.add 调用
   目标：找到每个模块的 on_add 参数表（真实数值）
"""
import sys, io, os, re, json, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'grep_buffadd_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

files = [f for f in os.listdir(os.path.join(W,'work'))
         if f.startswith('_dec_') and f.endswith('.txt')]
P('解码文件 %d' % len(files))

man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
KNOWN = set(man['buff']) | set(man['talent']) | set(man['common'])

# ---- 搜 Buffs.add(player, CS.emBuff.<Name> ----
P('')
P('=' * 74); P('★ 搜索 Buffs.add 调用'); P('=' * 74)
hits = collections.defaultdict(list)
pat = re.compile(r'Buffs\.add\(player,\s*CS\.emBuff\.(\w+),')
for f in files:
    t = open(os.path.join(W,'work',f), encoding='utf-8', errors='replace').read()
    for m in pat.finditer(t):
        n = m.group(1)
        hits[n].append((f, m.start()))
P('  Buffs.add 命中的模块 %d 个:' % len(hits))
for n in sorted(hits, key=lambda x: -len(hits[x])):
    P('     %-34s ×%d' % (n, len(hits[n])))

# ---- 对每个命中，提取完整的 Buffs.add(...) 参数块 ----
P('')
P('=' * 74); P('★ 提取参数块'); P('=' * 74)
OUTD = os.path.join(W, 'work', 'lua_add_params')
os.makedirs(OUTD, exist_ok=True)
extracted = {}
for n, locs in hits.items():
    for f, pos in locs:
        t = open(os.path.join(W,'work',f), encoding='utf-8', errors='replace').read()
        # 从 pos 向后找匹配的 })
        seg = t[pos:pos+3000]
        # 括号配对
        depth = 0
        end = -1
        for i, ch in enumerate(seg):
            if ch == '{': depth += 1
            elif ch == '}':
                depth -= 1
                if depth == 0:
                    end = i + 1
                    break
        if end < 0: continue
        block = seg[:end]
        # 加上前后的上下文（找 on_add 起点）
        st = t.rfind('function %s.on_add' % n, max(0, pos-2000), pos)
        if st > 0: block_pre = t[st:pos]
        else: block_pre = ''
        full = block_pre + block
        if n not in extracted or len(full) > len(extracted[n][1]):
            extracted[n] = (f, full)

P('  提取 %d 个参数块' % len(extracted))
for n in sorted(extracted, key=lambda x: -len(extracted[x][1])):
    f, b = extracted[n]
    with io.open(os.path.join(OUTD, '%s.on_add.lua' % n), 'w', encoding='utf-8') as fp:
        fp.write(b)
    P('     %-34s %6d B' % (n, len(b)))

# ---- 打印所有参数块（重点）----
P('')
P('=' * 74); P('★ 所有参数块内容'); P('=' * 74)
for n in sorted(extracted):
    f, b = extracted[n]
    P('')
    P('--- %s ---' % n)
    P(b[:1500])

# ---- 提取所有 key = value ----
P('')
P('=' * 74); P('★ 全部数值参数'); P('=' * 74)
import csv
params = {}
for n in extracted:
    f, b = extracted[n]
    d = {}
    for m in re.finditer(r'(\w+)\s*=\s*(-?[\d.]+|"[^"]*"|true|false)', b):
        k2, v = m.group(1), m.group(2)
        if k2 in ('events', 'display'): continue
        d[k2] = v
    params[n] = d
    if d:
        P('')
        P('  【%s】' % n)
        for k2 in sorted(d):
            P('      %-42s = %s' % (k2, d[k2]))

rows = [{'module': n, **d} for n, d in params.items() if d]
if rows:
    allk = set()
    for r in rows: allk |= set(r.keys())
    cols = ['module'] + sorted(allk - {'module'})
    with open(os.path.join(W,'work','LUA_PARAMS_EXTRA.csv'),'w',newline='',
              encoding='utf-8-sig') as fp:
        w = csv.DictWriter(fp, fieldnames=cols)
        w.writeheader()
        for r in rows: w.writerow(r)
    P('')
    P('  WROTE work/LUA_PARAMS_EXTRA.csv (%d 模块)' % len(rows))

json.dump({k: v[1] for k, v in extracted.items()},
          open(os.path.join(W,'work','lua_add_params.json'),'w',encoding='utf-8'),
          ensure_ascii=False, indent=1)
P('  WROTE work/lua_add_params.json')

o.close()
print('WROTE work/grep_buffadd_log.txt')
