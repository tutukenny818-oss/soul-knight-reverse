#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_apk_md5.py —— 算所有 APK 的 md5，找出重复"""
import sys, io, os, hashlib, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', '_apk_md5.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

apks = []
for dp, dn, fn in os.walk(W):
    for f in fn:
        if f.lower().endswith('.apk'):
            p = os.path.join(dp, f)
            apks.append((os.path.getsize(p), os.path.relpath(p, W), p))
apks.sort(reverse=True)
P('APK %d 个，共 %.2f GB' % (len(apks), sum(a[0] for a in apks)/1073741824))
P('')
P('=' * 78); P('计算 md5（前 8MB + 文件大小 做快速指纹）'); P('=' * 78)
# 全量 md5 太慢（12GB），用「大小 + 头尾各 4MB」做指纹
groups = collections.defaultdict(list)
for sz, rel, p in apks:
    h = hashlib.md5()
    h.update(str(sz).encode())
    with open(p, 'rb') as f:
        h.update(f.read(4*1024*1024))
        f.seek(max(0, sz - 4*1024*1024))
        h.update(f.read(4*1024*1024))
    fp = h.hexdigest()[:16]
    groups[fp].append((sz, rel))
    P('  %-56s %12.1f MB  fp=%s' % (rel, sz/1048576, fp))

P('')
P('=' * 78); P('★ 重复分组'); P('=' * 78)
dupe_sz = 0
keep = []
for fp, lst in sorted(groups.items(), key=lambda x: -len(x[1])):
    if len(lst) > 1:
        P('')
        P('  指纹 %s  ×%d  (每组保留 1 个)' % (fp, len(lst)))
        lst.sort(key=lambda x: x[1])
        for i, (sz, rel) in enumerate(lst):
            mark = '★保留' if i == 0 else '  可删'
            P('     [%s] %-52s' % (mark, rel))
            if i > 0: dupe_sz += sz
        keep.append(lst[0])
    else:
        keep.append(lst[0])
P('')
P('  ★ 可删除重复: %.2f GB' % (dupe_sz/1073741824))
P('  ★ 保留: %d 个文件' % len(keep))
P('')
P('  保留清单:')
for sz, rel in sorted(keep, reverse=True):
    P('     %10.1f MB  %s' % (sz/1048576, rel))

import json
json.dump({'keep': [r for _, r in keep],
           'dupe_gb': round(dupe_sz/1073741824, 2)},
          open(os.path.join(W,'work','_apk_keep.json'),'w',encoding='utf-8'),
          ensure_ascii=False, indent=1)
o.close()
print('WROTE work/_apk_md5.txt')
