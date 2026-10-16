#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_patch_frida.py —— ★★★★★★★★ 字节级 patch frida-server 绕过 ACE 检测
   原理（Florida 做的事，但直接改字节，不需要编译）：
     ACE 检测的是【字符串/线程名/socket 名】特征，不是二进制哈希
     只需把非协议特征等长替换，即可绕过
   分类：
     不可改（协议层，改了客户端连不上）：
       frida:rpc、re.frida.*、/re/frida/*
     可改（纯标识）：
       线程名：gum-js-loop、gmain、pool-frida
       socket：frida-helper-
       进程名：frida-server、frida-helper
       路径：/data/local/tmp/frida-helper-
"""
import sys, io, os, re, json, shutil, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'patch_frida_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

SRC = os.path.join(W, 'work', 'fs86_orig')
b = bytearray(open(SRC, 'rb').read())
P('源文件 %d B' % len(b))

# ══════ 1. 列出所有特征 ══════
P('')
P('=' * 78); P('1. 特征字符串盘点'); P('=' * 78)
PATS = [
 # ---- 可改（纯标识）----
 (b'frida-server',   b'zzzzz-server',  '进程名'),
 (b'frida-helper-64',b'zzzzz-helper-64','helper 进程名 64'),
 (b'frida-helper-32',b'zzzzz-helper-32','helper 进程名 32'),
 (b'frida-helper-',  b'zzzzz-helper-', 'helper 名前缀'),
 (b'/data/local/tmp/frida-helper-', b'/data/local/tmp/zzzzz-helper-', 'helper 路径'),
 (b'gum-js-loop',    b'zum-js-loop',   'JS 线程名'),
 (b'pool-frida',     b'pool-zzzzz',    '线程池名'),
 (b'gum-js-loop',    b'zum-js-loop',   '同'),
 (b'frida_agent',    b'zzzzz_agent',   'agent 符号'),
]
# ---- 不可改（协议）----
PROTO = [b'frida:rpc', b're.frida', b'/re/frida', b'frida-gadget', b'frida_agent_main',
         b'frida_promise', b'frida-error-quark', b'frida_context',
         b'frida_virtual_stream', b'frida_zymbiote']

P('')
P('可改（纯标识）:')
for old, new, desc in PATS:
    if len(old) != len(new):
        P('  ⚠ 长度不等跳过: %r → %r' % (old, new)); continue
    n = b.count(old)
    if n: P('  %-32s ×%-4d  →  %-32s  (%s)' % (old.decode(), n, new.decode(), desc))
P('')
P('不可改（协议层）:')
for p in PROTO:
    n = b.count(p)
    if n: P('  %-32s ×%d' % (p.decode('ascii','replace'), n))

# ══════ 2. 执行替换 ══════
P('')
P('=' * 78); P('2. ★ 执行字节替换'); P('=' * 78)
total = 0
for old, new, desc in PATS:
    if len(old) != len(new): continue
    n = b.count(old)
    if not n: continue
    b = bytearray(bytes(b).replace(old, new))
    total += n
    P('  ✓ %-32s %d 处' % (old.decode(), n))
P('')
P('  总替换 %d 处' % total)

# ══════ 3. 验证 ══════
P('')
P('=' * 78); P('3. 验证'); P('=' * 78)
P('  替换后残留 "frida" 出现次数: %d' % b.count(b'frida'))
# 检查协议层是否完好
for p in PROTO:
    n = b.count(p)
    if n: P('  ✓ 协议保留 %-28s ×%d' % (p.decode('ascii','replace'), n))

# 列出残留的 frida 上下文
P('')
P('  残留特征（前 30 类）:')
ctx = collections.Counter()
i = 0
while True:
    i = b.find(b'frida', i)
    if i < 0: break
    lo = i
    while lo > 0 and 32 <= b[lo-1] < 127: lo -= 1
    hi = i
    while hi < len(b) and 32 <= b[hi] < 127: hi += 1
    ctx[bytes(b[lo:hi]).decode('ascii','replace')[:70]] += 1
    i += 1
for s, c in ctx.most_common(30):
    P('     ×%-4d %r' % (c, s))

# ══════ 4. 保存 ══════
OUT = os.path.join(W, 'work', 'fs86_patched')
open(OUT, 'wb').write(bytes(b))
P('')
P('  WROTE %s (%d B)' % (OUT, len(b)))
P('  原 md5 vs 新 md5 不同: %s' % (len(bytes(b)) == len(open(SRC,'rb').read())))

o.close()
print('WROTE work/patch_frida_log.txt')
