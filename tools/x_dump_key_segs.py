#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_dump_key_segs.py —— ★★★★ 精确 dump 关键内存段
   目标段：
     1) libil2cpp 的 rw-p 段（含元数据 + 静态字段 = 密钥槽）
     2) [anon:libc_malloc] 段（Lua 堆）
     3) xlua 段
     4) anon rw 段（Lua 状态机）
   然后本地做：
     A) UTF-16LE 短串扫描（子 agent 的关键发现）
     B) ASCII 8 字符候选
     C) 用 CBC 定锚表做 oracle 验证
"""
import subprocess, sys, os, io, time, json
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
W = r'D:\元气骑士mod'
PKG = 'com.ChillyRoom.DungeonShooter'
o = io.open(os.path.join(W, 'work', 'dump_key_segs_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def raw(args, timeout=1800):
    r = subprocess.run([ADB,'shell']+args, capture_output=True, timeout=timeout)
    return r.stdout.decode('utf-8','replace'), r.stderr.decode('utf-8','replace')

out,_ = raw(['ps','-A'])
pid = None
for l in out.splitlines():
    if 'DungeonShooter' in l and ':' not in l:
        for x in l.split():
            if x.isdigit() and 100 < int(x) < 400000: pid = x; break
        if pid: break
P('PID = %s' % pid)
if not pid: o.close(); sys.exit(1)

# maps
raw(['su','0','dd','if=/proc/%s/maps' % pid, 'of=/data/local/tmp/_m3.txt'])
subprocess.run([ADB,'pull','/data/local/tmp/_m3.txt',
                os.path.join(W,'work','_live_maps3.txt')], capture_output=True)
maps = open(os.path.join(W,'work','_live_maps3.txt'), encoding='utf-8',
            errors='replace').read().splitlines()
P('maps %d 行' % len(maps))

# 选目标
targets = []
for l in maps:
    parts = l.split()
    if len(parts) < 2: continue
    rng, perms = parts[0], parts[1]
    if 'r' not in perms: continue
    try:
        a, b2 = rng.split('-'); lo, hi = int(a,16), int(b2,16)
    except Exception: continue
    sz = hi - lo
    path = parts[5] if len(parts) > 5 else ''
    targets.append((lo, hi, sz, perms, path))

P('')
P('= 关键段选择 =')
sel = []
for lo, hi, sz, pm, pth in targets:
    tag = None
    if 'libil2cpp' in pth and 'rw' in pm: tag = 'il2cpp_rw'
    elif 'xlua' in pth: tag = 'xlua'
    elif 'libc_malloc' in pth: tag = 'malloc'
    elif pth == '' and 'rw' in pm and sz > 65536: tag = 'anon_rw'
    if tag:
        sel.append((lo, hi, sz, pm, pth, tag))
sel.sort(key=lambda x: -x[2])
P('  选中 %d 段' % len(sel))
for lo, hi, sz, pm, pth, tag in sel[:30]:
    P('    %-12s 0x%-12x %8.2f MB  %s  %s' % (tag, lo, sz/1048576, pm, pth[:40]))

# dump（限制每段 64MB）
OST = os.path.join(W, 'work', 'keyscan')
os.makedirs(OST, exist_ok=True)
P('')
P('= dump =')
n = 0; tot = 0
for lo, hi, sz, pm, pth, tag in sel[:40]:
    if sz > 0x4000000: sz = 0x4000000
    if sz < 4096: continue
    remote = '/data/local/tmp/_d2.bin'
    raw(['su','0','dd','if=/proc/%s/mem' % pid, 'of=' + remote,
         'bs=1048576', 'skip=%d' % (lo//1048576),
         'count=%d' % (sz//1048576 + 1)], timeout=1200)
    loc = os.path.join(OST, '%s_%016x_%x.bin' % (tag, lo, sz))
    subprocess.run([ADB,'pull', remote, loc], capture_output=True, timeout=1200)
    if os.path.isfile(loc) and os.path.getsize(loc) > 0:
        fs = os.path.getsize(loc); n += 1; tot += fs
        P('  [%2d] %-12s 0x%-12x %8.2f MB' % (n, tag, lo, fs/1048576))
P('')
P('  dump %d 段, %.1f MB' % (n, tot/1048576))

# 保存 maps
json.dump([[lo,hi,sz,pm,pth,tag] for lo,hi,sz,pm,pth,tag in sel],
          open(os.path.join(W,'work','keyscan_segs.json'),'w'), indent=1)
o.close()
print('WROTE work/dump_key_segs_log.txt  +  work/keyscan/')
