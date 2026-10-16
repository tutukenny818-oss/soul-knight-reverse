#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_clean_test.py —— ★★★★★★★★ 干净对照实验
   发现：之前启动失败是【端口冲突】（5 个残留 fs86 占着默认端口）
   现在：环境已清干净
   目标：测 3 个版本，每个用【独立端口】+ 【启动前杀干净】
     ① 原版 fs86（基线）
     ② fs86_min（保守 patch：线程名+socket名）
     ③ fs86_patched（激进 patch：含符号名）
"""
import subprocess, sys, os, io, time, re
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'clean_test_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def raw(args, timeout=300):
    try:
        r = subprocess.run([ADB,'shell']+args, capture_output=True, timeout=timeout)
        return r.stdout.decode('utf-8','replace')
    except Exception:
        return 'TIMEOUT'

def killall_all():
    """彻底杀所有相关进程"""
    out = raw(['su','0','ps','-A'])
    n = 0
    for l in out.splitlines():
        f = l.split()
        if len(f) > 1 and f[1].isdigit():
            # 匹配我们的测试名
            for nm in ('fs86','fs86m','fs86p','fs86o','fs86_min','fs86_patched'):
                if nm in l and 'grep' not in l:
                    raw(['su','0','kill','-9',f[1]]); n += 1
                    break
    time.sleep(2)
    return n

def clean_name(nm):
    """删除所有以 nm 开头的文件"""
    raw(['su','0','sh','-c','rm -f /data/local/tmp/%s* 2>/dev/null' % nm])

# ══════ 准备 ══════
P('=' * 78); P('0. 环境清理'); P('=' * 78)
k = killall_all()
P('  杀掉 %d 个进程' % k)
out = raw(['su','0','ps','-A'])
res = [l for l in out.splitlines() if any(x in l for x in ('fs86','frida')) and 'grep' not in l]
P('  残留: %d' % len(res))

# ══════ 逐版本测试 ══════
CASES = [
 ('orig',    'fs86_t1', os.path.join(W,'work','fs86_orig'),   30311, '原版（基线）'),
 ('min',     'fs86_t2', os.path.join(W,'work','fs86_min.bin'), 30312, '保守 patch（15 处）'),
 ('patched', 'fs86_t3', os.path.join(W,'work','fs86_patched'), 30313, '激进 patch（52 处）'),
]
results = []
for tag, remote_name, local, port, desc in CASES:
    P('')
    P('=' * 78); P('★ 测试 %s: %s' % (tag, desc)); P('=' * 78)
    if not os.path.isfile(local):
        P('  ✗ 本地文件不存在: %s' % local); results.append((tag, -1)); continue

    # 上传
    r = subprocess.run([ADB,'push', local, '/data/local/tmp/'+remote_name],
                       capture_output=True, timeout=900)
    raw(['su','0','chmod','755','/data/local/tmp/'+remote_name])
    P('  已上传 (%d B)' % os.path.getsize(local))

    # 启动（独立端口）
    subprocess.Popen([ADB,'shell','su','0','/data/local/tmp/'+remote_name,
                      '-l','0.0.0.0:%d' % port],
                     stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL,
                     stdin=subprocess.DEVNULL)
    time.sleep(10)

    # 检查
    out = raw(['su','0','ps','-A'])
    lines = [l for l in out.splitlines() if remote_name in l and 'grep' not in l]
    P('  进程数 = %d' % len(lines))
    for l in lines[:3]:
        P('     %s' % l.strip()[:90])

    # 试连接
    ok = False
    if lines:
        subprocess.run([ADB,'forward','tcp:%d'%port,'tcp:%d'%port], capture_output=True)
        try:
            import frida
            dev = frida.get_device_manager().add_remote_device('127.0.0.1:%d' % port)
            ps = dev.enumerate_processes()
            P('  ★ frida 连接成功: %d 进程' % len(ps))
            ok = True
        except Exception as e:
            P('  ✗ 连接失败: %s' % str(e)[:70])
    results.append((tag, len(lines) if ok else (0 if not lines else -2)))

    # 清理
    killall_all()
    time.sleep(2)

# ══════ 汇总 ══════
P('')
P('=' * 78); P('★ 汇总'); P('=' * 78)
for tag, r in results:
    status = '✓ 可运行 + 可连接' if r > 0 and r != -2 else ('⚠ 进程在但连不上' if r == -2 else '✗ 启动失败')
    P('  %-10s %s' % (tag, status))

o.close()
print('WROTE work/clean_test_log.txt')
