#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_survey.py —— 全盘清点，为清理做准备
   只读，不删任何东西
"""
import sys, io, os, json, hashlib, collections, time
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', '_survey_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

def fmt(n):
    for u in ('B','KB','MB','GB'):
        if n < 1024: return '%.1f %s' % (n, u)
        n /= 1024
    return '%.1f TB' % n

# ══════ 1. 顶层结构 ══════
P('=' * 78); P('1. 顶层结构'); P('=' * 78)
total = 0
for name in sorted(os.listdir(W)):
    p = os.path.join(W, name)
    if os.path.isfile(p):
        sz = os.path.getsize(p)
        total += sz
        P('  %-42s %12s  [文件]' % (name, fmt(sz)))
    else:
        sz = 0; nf = 0
        for dp, dn, fn in os.walk(p):
            for f in fn:
                try: sz += os.path.getsize(os.path.join(dp, f)); nf += 1
                except Exception: pass
        total += sz
        P('  %-42s %12s  %6d 文件' % (name + '/', fmt(sz), nf))
P('')
P('  总计 %s' % fmt(total))

# ══════ 2. work/ 子目录明细 ══════
P('')
P('=' * 78); P('2. work/ 子目录明细（按大小）'); P('=' * 78)
wd = os.path.join(W, 'work')
subs = []
for name in sorted(os.listdir(wd)):
    p = os.path.join(wd, name)
    if not os.path.isdir(p): continue
    sz = 0; nf = 0
    for dp, dn, fn in os.walk(p):
        for f in fn:
            try: sz += os.path.getsize(os.path.join(dp, f)); nf += 1
            except Exception: pass
    subs.append((sz, nf, name))
subs.sort(reverse=True)
P('  %-40s %12s %8s' % ('目录', '大小', '文件数'))
for sz, nf, name in subs:
    P('  %-40s %12s %8d' % (name + '/', fmt(sz), nf))

# ══════ 3. work/ 根目录文件（按类型）══════
P('')
P('=' * 78); P('3. work/ 根文件统计'); P('=' * 78)
ext = collections.defaultdict(lambda: [0, 0])
for f in os.listdir(wd):
    p = os.path.join(wd, f)
    if not os.path.isfile(p): continue
    e = os.path.splitext(f)[1].lower() or '(无扩展名)'
    sz = os.path.getsize(p)
    ext[e][0] += 1; ext[e][1] += sz
for e, (c, sz) in sorted(ext.items(), key=lambda x: -x[1][1]):
    P('  %-16s %6d 个  %12s' % (e, c, fmt(sz)))

# ══════ 4. tools/ 统计 ══════
P('')
P('=' * 78); P('4. tools/ 统计'); P('=' * 78)
td = os.path.join(W, 'tools')
if os.path.isdir(td):
    py = [f for f in os.listdir(td) if f.endswith('.py')]
    other = [f for f in os.listdir(td) if not f.endswith('.py')]
    sz = sum(os.path.getsize(os.path.join(td, f)) for f in os.listdir(td)
             if os.path.isfile(os.path.join(td, f)))
    P('  .py 脚本: %d 个' % len(py))
    P('  其他文件: %d 个' % len(other))
    P('  总大小:   %s' % fmt(sz))
    # 子目录
    for name in sorted(os.listdir(td)):
        p = os.path.join(td, name)
        if os.path.isdir(p):
            s2 = 0
            for dp, dn, fn in os.walk(p):
                for f in fn:
                    try: s2 += os.path.getsize(os.path.join(dp, f))
                    except Exception: pass
            P('    子目录 %-24s %10s' % (name + '/', fmt(s2)))

# ══════ 5. 大文件 Top 30 ══════
P('')
P('=' * 78); P('5. 大文件 Top 30'); P('=' * 78)
allf = []
for dp, dn, fn in os.walk(W):
    for f in fn:
        p = os.path.join(dp, f)
        try: allf.append((os.path.getsize(p), os.path.relpath(p, W)))
        except Exception: pass
allf.sort(reverse=True)
for sz, rel in allf[:30]:
    P('  %12s  %s' % (fmt(sz), rel[:96]))

o.close()
print('WROTE work/_survey_log.txt')
