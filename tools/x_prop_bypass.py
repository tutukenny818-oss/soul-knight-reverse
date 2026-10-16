#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_prop_bypass.py —— ★ 直接改系统属性绕过 libtprt 检测
   检测点（来自 xuanworld 分析）：
     __system_property_get("init.svc.adbd")    → running
     __system_property_get("service.adb.tcp.port")
     __system_property_get("ro.debuggable")     → 1
     maps 里命中 libzygisk.so
     Unix socket 名带 frida-
   绕过思路（无需 LSPosed）：
     1) 用 resetprop 改属性（需 Magisk）→ 无
     2) 用 setprop 改（只读属性改不了）→ 试
     3) ★ 在 frida 注入后立刻 hook __system_property_get，让它返回空
     4) ★★ 或者：用 ptrace 在启动前改内存
     5) ★★★ 更简单：用 frida-gadget（不是 frida-server，无 frida- 特征）
"""
import subprocess, sys, os, io, time
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'prop_bypass_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def raw(args, timeout=120):
    r = subprocess.run([ADB,'shell']+args, capture_output=True, timeout=timeout)
    return r.stdout.decode('utf-8','replace'), r.stderr.decode('utf-8','replace')

P('=== 1) 当前属性 ===')
for p in ['init.svc.adbd','service.adb.tcp.port','ro.debuggable','ro.secure',
          'ro.build.type','ro.build.tags']:
    out, _ = raw(['su','0','getprop',p])
    P('  %-26s = %r' % (p, out.strip()))

P('')
P('=== 2) 尝试 setprop 修改 ===')
for p, v in [('init.svc.adbd','stopped'), ('service.adb.tcp.port',''),
             ('ro.debuggable','0')]:
    out, err = raw(['su','0','setprop', p, v])
    P('  setprop %-24s %-10s → %s' % (p, repr(v), (out+err).strip()[:60]))
P('')
P('  修改后:')
for p in ['init.svc.adbd','service.adb.tcp.port','ro.debuggable']:
    out, _ = raw(['su','0','getprop',p])
    P('    %-26s = %r' % (p, out.strip()))

P('')
P('=== 3) 检查 resetprop（Magisk）===')
for c in ['resetprop','magisk','ksud']:
    out, _ = raw(['su','0','which',c])
    P('  %-12s %s' % (c, out.strip() or '(无)'))

P('')
P('=== 4) 检查 frida-gadget 可用性 ===')
out, _ = raw(['su','0','ls','-la','/data/local/tmp/'])
for l in out.splitlines():
    if any(x in l.lower() for x in ['frida','fs86','fs64','gadget','florida']):
        P('  %s' % l.strip()[:110])

P('')
P('=== 5) 检查模拟器特性（可用宿主改属性）===')
out, _ = raw(['getprop','ro.kernel.qemu'])
P('  ro.kernel.qemu = %r' % out.strip())
out, _ = raw(['getprop','ro.hardware'])
P('  ro.hardware = %r' % out.strip())
out, _ = raw(['getprop','ro.product.name'])
P('  ro.product.name = %r' % out.strip())
out, _ = raw(['getprop','ro.build.flavor'])
P('  ro.build.flavor = %r' % out.strip())

P('')
P('=== 6) 检测点自检：直接读属性文件 ===')
for f in ['/system/build.prop', '/default.prop']:
    out, _ = raw(['su','0','cat',f])
    for l in out.splitlines():
        if 'debuggable' in l or 'adbd' in l:
            P('  %s : %s' % (f, l.strip()[:80]))

P('')
P('=== 结论 ===')
P('  若 ro.debuggable 可改 → 直接改')
P('  若不可改 → 用 frida hook __system_property_get')

o.close()
print('WROTE work/prop_bypass_log.txt')
