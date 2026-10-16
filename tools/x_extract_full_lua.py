#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_extract_full_lua.py —— ★★★★★★ 从 UTF-16LE 内存提取【完整 Lua 源码】
   突破：找到 on_add 主体，含真实参数（skill_haste = 25, melee_size_factor = 0.25）
   策略：
     1) 在 anon_rw 段里定位所有 "local <Name> = " 起点
     2) 从起点向后读到 "return <Name>" 结尾
     3) 反转 UTF-16LE → ASCII
     4) 去重、按模块名归类
"""
import sys, io, os, re, json, collections
W = r'D:\元气骑士mod'
D = os.path.join(W, 'work', 'keyscan')
OUTD = os.path.join(W, 'work', 'lua_utf16')
os.makedirs(OUTD, exist_ok=True)
o = io.open(os.path.join(W, 'work', 'extract_full_lua_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
NAMES = sorted(set(man['buff']) | set(man['talent']) | set(man['common']))
P('目标模块名 %d 个' % len(NAMES))

files = sorted(f for f in os.listdir(D) if f.endswith('.bin') and
               os.path.getsize(os.path.join(D, f)) > 0)
P('段 %d, %.0f MB' % (len(files),
  sum(os.path.getsize(os.path.join(D, f)) for f in files)/1048576))

def u16(s): return s.encode('utf-16-le')

# ---- 提取每段里的 UTF-16LE Lua 文本 ----
# 方法：把整段按 UTF-16LE 解码（在 ASCII 区域），然后找模块结构
modules = {}   # name -> best text

for f in files:
    b = open(os.path.join(D, f), 'rb').read()
    # 找所有 "local " 位置
    positions = [m.start() for m in re.finditer(u16('local '), b)]
    if not positions: continue
    for i in positions:
        # 向后扩：从 i 开始，逐 UTF-16 字符读，遇到连续非 ASCII 停
        j = i
        chars = []
        n = len(b)
        while j < n - 1 and len(chars) < 6000:
            c = b[j]; h = b[j+1]
            if h == 0 and (32 <= c < 127 or c in (9, 10, 13)):
                chars.append(chr(c)); j += 2
            elif h == 0 and c == 0:
                break
            else:
                # 非 ASCII：检查是否是「少量噪声」还是「文本结束」
                # 允许最多 8 个连续非 ASCII（可能是拼接）
                k = j; bad = 0
                while k < n-1 and bad < 8:
                    c2 = b[k]; h2 = b[k+1]
                    if h2 == 0 and (32 <= c2 < 127 or c2 in (9,10,13)): break
                    bad += 1; k += 2
                if bad >= 8: break
                # 跳过噪声
                j = k
                continue
        t = ''.join(chars)
        if len(t) < 60: continue
        # 检查是否像 Lua 模块
        if not re.search(r'(TalentBase|BuffBase)\.create\(CS\.emBuff\.(\w+)', t) and \
           not re.search(r'function (\w+)\.on_(add|remove)', t):
            continue
        # 找模块名
        m = re.search(r'local (\w+) = (?:TalentBase|BuffBase)\.create', t)
        if not m:
            m = re.search(r'function (\w+)\.on_add', t)
        if not m: continue
        name = m.group(1)
        if name not in NAMES: continue
        # 裁到 return <Name> 结尾
        tail = 'return %s' % name
        k = t.find(tail)
        if k > 0:
            t = t[:k + len(tail)]
        # 去重：保留最长的
        if name not in modules or len(t) > len(modules[name][1]):
            modules[name] = (f, t)

P('')
P('=' * 74); P('★ 提取到 %d 个模块的 UTF-16 Lua 源码' % len(modules)); P('=' * 74)
P('')
rows = []
for name in sorted(modules):
    f, t = modules[name]
    # 行数
    lines = [l for l in t.replace('\r', '').split('\n') if l.strip()]
    rows.append((name, len(t), len(lines), f))
    P('  %-34s %6d B  %3d 行  %s' % (name, len(t), len(lines), f[:40]))
P('')
P('  总 %d 个模块' % len(rows))

# 保存
for name in sorted(modules):
    f, t = modules[name]
    with io.open(os.path.join(OUTD, '%s.lua' % name), 'w', encoding='utf-8') as fp:
        fp.write(t.replace('\r', ''))
P('')
P('  WROTE work/lua_utf16/*.lua (%d 个文件)' % len(modules))

# 打印完整样本
P('')
P('=' * 74); P('★ 完整源码样本'); P('=' * 74)
for name in sorted(modules, key=lambda x: -len(modules[x][1]))[:8]:
    f, t = modules[name]
    P('')
    P('--- %s (%d B, %s) ---' % (name, len(t), f[:30]))
    P(t.replace('\r', '')[:2000])

o.close()
print('WROTE work/extract_full_lua_log.txt  +  work/lua_utf16/')
