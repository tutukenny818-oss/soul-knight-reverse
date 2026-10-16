#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_decode_full.py —— ★★★★★★★ 整段 UTF-16LE 解码 + 全量模块提取
   方法：
     1) 把 65MB 段整体按 UTF-16LE 解码（errors=ignore）
     2) 用正则找所有 lua 模块（function X.on_add ... return X）
     3) 提取所有模块
"""
import sys, io, os, re, json, collections
W = r'D:\元气骑士mod'
D = os.path.join(W, 'work', 'keyscan')
OUTD = os.path.join(W, 'work', 'lua_final_extract')
os.makedirs(OUTD, exist_ok=True)
o = io.open(os.path.join(W, 'work', 'decode_full_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
KNOWN = set(man['buff']) | set(man['talent']) | set(man['common'])

files = sorted(f for f in os.listdir(D) if f.endswith('.bin') and
               os.path.getsize(os.path.join(D, f)) > 0)
P('段 %d' % len(files))

mods = {}
for f in files:
    p = os.path.join(D, f)
    b = open(p, 'rb').read()
    if b.find(b'T\x00a\x00l\x00e\x00n\x00t\x00B\x00a\x00s\x00e\x00') < 0 and \
       b.find(b'B\x00u\x00f\x00f\x00s\x00.\x00') < 0:
        continue
    P('')
    P('=== %s (%.1f MB) ===' % (f, len(b)/1048576))
    # 整体解码
    try:
        t = b.decode('utf-16-le', 'ignore')
    except Exception as e:
        P('  解码失败 %s' % e); continue
    P('  解码后 %d 字符' % len(t))
    # 规范换行
    t = t.replace('\r', '')
    # 找所有模块：从 "local X = " 或 "function X.on_" 到 "return X"
    # 策略：找所有 "return <Name>" 的位置，向前找模块起点
    pats = [
        re.compile(r'(?:^|\n)(local\s+(\w+)\s*=\s*(?:TalentBase|BuffBase)\.create[^\n]*\n.{0,8000}?return\s+\2)\b', re.S),
        re.compile(r'(?:^|\n)(function\s+(\w+)\.on_add[^\n]*\n.{0,8000}?return\s+\2)\b', re.S),
    ]
    got = 0
    for pat in pats:
        for m in pat.finditer(t):
            body = m.group(1)
            name = m.group(2)
            if name not in KNOWN: continue
            if name not in mods or len(body) > len(mods[name][1]):
                mods[name] = (f, body)
                got += 1
    P('  模块 %d' % got)
    # 备用：直接找 "<Name>\n ... return <Name>"
    for m in re.finditer(r'\nfunction\s+(\w+)\.on_add\(player\)', t):
        n = m.group(1)
        if n not in KNOWN: continue
        st = m.start()
        # 向前找 local
        pre = t[max(0, st-2000):st]
        k = pre.rfind('local %s ' % n)
        if k >= 0: st = max(0, st-2000) + k
        en = t.find('return %s' % n, st)
        if en < 0: continue
        body = t[st:en + len('return %s' % n)]
        if len(body) > 8000: continue
        if n not in mods or len(body) > len(mods[n][1]):
            mods[n] = (f, body); got += 1

P('')
P('=' * 74); P('★★ 总计 %d 个模块' % len(mods)); P('=' * 74)
for n in sorted(mods, key=lambda x: -len(mods[x][1])):
    f, t = mods[n]
    P('  %-34s %7d B  %s' % (n, len(t), f[:40]))

# 保存
for n in mods:
    f, t = mods[n]
    with io.open(os.path.join(OUTD, '%s.lua' % n), 'w', encoding='utf-8') as fp:
        fp.write(t)
P('')
P('  WROTE work/lua_final_extract/*.lua (%d)' % len(mods))

# 保存整段解码结果（供后续）
for f in files:
    p = os.path.join(D, f)
    b = open(p, 'rb').read()
    if b.find(b'T\x00a\x00l\x00e\x00n\x00t\x00B\x00a\x00s\x00e\x00') >= 0:
        t = b.decode('utf-16-le', 'ignore').replace('\r', '')
        with io.open(os.path.join(W,'work','_dec_%s.txt' % f[:30]), 'w', encoding='utf-8') as fp:
            fp.write(t)
        P('  WROTE work/_dec_%s.txt (%d 字符)' % (f[:30], len(t)))

# 打印样本
P('')
P('=' * 74); P('★ 样本'); P('=' * 74)
for n in sorted(mods, key=lambda x: -len(mods[x][1]))[:8]:
    f, t = mods[n]
    P('')
    P('--- %s (%d B) ---' % (n, len(t)))
    P(t[:2000])

o.close()
print('WROTE work/decode_full_log.txt')
