#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_minimal_hook.py —— ★★★★★★★★★ 最小痕迹 hook
   已验证：延迟注入流程完全打通（il2cpp 加载 → frida attach → 存活 45s）
   唯一问题：装 16 个 Interceptor hook 后被杀（45s）
   策略（逐级降低痕迹）：
     方案 A：只 hook 1 个最关键的函数（DESCrypt::Decrypt）
     方案 B：不 hook，只在 attach 后立刻 dump 内存（读密钥）
     方案 C：hook 后立刻读内存（30 秒内完成）
   先做方案 B/A 组合：attach → 只 hook 1 个 → 立刻读内存
"""
import subprocess, sys, os, io, time, json, base64, re
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
W = r'D:\元气骑士mod'
PKG = 'com.ChillyRoom.DungeonShooter'
PORT = 30031
o = io.open(os.path.join(W, 'work', 'minimal_hook_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def raw(args, timeout=900):
    r = subprocess.run([ADB,'shell']+args, capture_output=True, timeout=timeout)
    return r.stdout.decode('utf-8','replace'), r.stderr.decode('utf-8','replace')

def getpid():
    out,_ = raw(['ps','-A'])
    for l in out.splitlines():
        if 'DungeonShooter' in l and ':' not in l:
            for x in l.split():
                if x.isdigit() and 100 < int(x) < 400000: return x
    return None

# 1) 启动游戏
P('=' * 74); P('1. 启动游戏（无 frida）'); P('=' * 74)
raw(['su','0','sh','-c','killall -9 fs86 fs64 2>/dev/null'])
raw(['su','0','am','force-stop',PKG])
time.sleep(3)
raw(['su','0','setprop','init.svc.adbd','stopped'])
raw(['su','0','setprop','ro.debuggable','0'])
time.sleep(2)
raw(['su','0','monkey','-p',PKG,'-c','android.intent.category.LAUNCHER','1'])
pid = None
for i in range(40):
    time.sleep(5)
    pid = getpid()
    if pid: break
P('  PID = %s' % pid)
if not pid: o.close(); sys.exit(1)

# 2) 等 il2cpp
P('')
P('=' * 74); P('2. 等 il2cpp'); P('=' * 74)
for i in range(60):
    time.sleep(5)
    if not getpid(): P('  已死'); break
    out,_ = raw(['su','0','grep','libil2cpp','/proc/%s/maps' % pid])
    n = sum(1 for l in out.splitlines() if 'libil2cpp' in l)
    if n > 0:
        P('  ★ il2cpp %d 段 (%ds)' % (n, (i+1)*5)); break
P('  等 40s...')
time.sleep(40)
if not getpid(): o.close(); sys.exit(1)

# 3) 启动 frida
P('')
P('=' * 74); P('3. 启动 frida'); P('=' * 74)
subprocess.Popen([ADB,'shell','su','0','/data/local/tmp/fs86','-l','0.0.0.0:%d'%PORT],
                 stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL, stdin=subprocess.DEVNULL)
time.sleep(10)
subprocess.run([ADB,'forward','tcp:%d'%PORT,'tcp:%d'%PORT],capture_output=True)

import frida
dev = None
for att in range(6):
    try:
        dev = frida.get_device_manager().add_remote_device('127.0.0.1:%d' % PORT)
        ps = dev.enumerate_processes(); P('  frida OK: %d' % len(ps)); break
    except Exception as e:
        P('  %d: %s' % (att+1, str(e)[:50])); time.sleep(4)
if dev is None: o.close(); sys.exit(1)
if not getpid(): P('  游戏已被杀'); o.close(); sys.exit(1)

tgt = [p for p in ps if p.name == PKG]
if not tgt: o.close(); sys.exit(1)
tpid = tgt[0].pid

# ============ 方案 A：只 hook 1 个函数 ============
P('')
P('=' * 74); P('★ 方案 A：只 hook DESCrypt::Decrypt（1 个）'); P('=' * 74)
SCRIPT_A = r'''
function findExp(name) {
    var out = null;
    Process.enumerateModules().forEach(function(m){
        if (out) return;
        try { var a = m.findExportByName(name); if (a) out = a; } catch(e){}
    });
    return out;
}
var pget = findExp('__system_property_get');
if (pget) {
    Interceptor.attach(pget, {
        onEnter: function(a){ this.n=null; try{this.n=a[0].readCString();}catch(e){} this.o=a[1]; },
        onLeave: function(r){
            if (this.n==='init.svc.adbd'||this.n==='ro.debuggable'||this.n==='service.adb.tcp.port') {
                try { this.o.writeUtf8String(''); r.replace(0); } catch(e){}
            }
        }
    });
    send({t:'anti'});
}
// 只 hook 一个
var m = null;
Process.enumerateModules().forEach(function(x){ if(x.name.indexOf('libil2cpp')>=0 && !m) m=x; });
if (!m) { send({t:'noil2cpp'}); }
else {
    send({t:'mod', base:m.base.toString()});
    var a = m.base.add(0x09CBC644);
    try {
        Interceptor.attach(a, {
            onEnter: function(args) {
                var rec = {t:'call', n:'DESCrypt::Decrypt',
                    a1:args[1].toString(), a2:args[2].toString()};
                try{var s=args[1].readUtf8String(64); if(s&&s.length>=3&&/^[\x20-\x7e]+$/.test(s)) rec.s1=s;}catch(e){}
                try{var s=args[2].readUtf8String(64); if(s&&s.length>=3&&/^[\x20-\x7e]+$/.test(s)) rec.s2=s;}catch(e){}
                try{var u=args[2].readUtf16String(64); if(u&&u.length>=3) rec.u2=u;}catch(e){}
                send(rec);
            }
        });
        send({t:'hooked', n:'DESCrypt::Decrypt'});
    } catch(e) { send({t:'hookfail', e:String(e)}); }
}
rpc.exports={ping:function(){return 'ok';}};
'''
try:
    sess = dev.attach(tpid)
    P('  attach OK')
    sc = sess.create_script(SCRIPT_A)
    calls = []
    def on_msg(m, d):
        if m.get('type') == 'send':
            p = m['payload']; t = p.get('t')
            if t == 'anti': P('  ★ 反检测')
            elif t == 'mod': P('  ★ il2cpp base=%s' % p['base'])
            elif t == 'hooked': P('  ★ hook %s 成功' % p['n'])
            elif t == 'hookfail': P('  ✗ %s' % p['e'][:60])
            elif t == 'noil2cpp': P('  无 il2cpp')
            elif t == 'call':
                calls.append(p); P('  ★★★ 调用! a1=%s a2=%s' % (p.get('a1'), p.get('a2')))
                for k in ('s1','s2','u2'):
                    if k in p: P('       %s = %r' % (k, p[k]))
        else: P('  ERR %s' % str(m)[:200])
    sc.on('message', on_msg)
    sc.load()
    P('  script loaded')
    # 监控（短周期）
    killed_at = None
    for i in range(12):   # 最多 90 秒
        time.sleep(7.5)
        alive = getpid()
        P('  %4ds: %s (%d 次)' % (int((i+1)*7.5), '存活' if alive else '★ 已被杀', len(calls)))
        if not alive:
            killed_at = int((i+1)*7.5); break
        if len(calls) > 5: break
    P('')
    P('  ★ 存活时长: %s 秒' % (killed_at if killed_at else '>90'))
except Exception as e:
    P('  A 失败: %s' % e)

o.close()
print('WROTE work/minimal_hook_log.txt')
