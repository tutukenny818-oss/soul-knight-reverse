#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_late_attach3.py —— ★★★★★★★★ 延迟注入 v3（frida-server 用原名）
   进展：v2 证明【启动 frida 后游戏不被杀】—— 延迟注入方案可行！
   问题：改名后的二进制连接被拒
   修正：用原 fs86 + 非默认端口
"""
import subprocess, sys, os, io, time, json
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
W = r'D:\元气骑士mod'
PKG = 'com.ChillyRoom.DungeonShooter'
PORT = 30013
o = io.open(os.path.join(W, 'work', 'late_attach3_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def raw(args, timeout=600):
    r = subprocess.run([ADB,'shell']+args, capture_output=True, timeout=timeout)
    return r.stdout.decode('utf-8','replace'), r.stderr.decode('utf-8','replace')

def getpid():
    out,_ = raw(['ps','-A'])
    for l in out.splitlines():
        if 'DungeonShooter' in l and ':' not in l:
            for x in l.split():
                if x.isdigit() and 100 < int(x) < 400000: return x
    return None

tpid = getpid()
P('PID = %s' % tpid)
if not tpid: o.close(); sys.exit(1)
out,_ = raw(['su','0','grep','libil2cpp','/proc/%s/maps' % tpid])
n_seg = sum(1 for l in out.splitlines() if 'libil2cpp' in l)
P('il2cpp 段数 = %d' % n_seg)

# ---- 启动 frida（原名）----
P('')
P('=' * 74); P('★ 启动 frida-server（原名 fs86, 非默认端口）'); P('=' * 74)
raw(['su','0','sh','-c','killall -9 fs86 fs64 zzsvc 2>/dev/null'])
time.sleep(3)
subprocess.Popen([ADB,'shell','su','0','/data/local/tmp/fs86','-l','0.0.0.0:%d'%PORT],
                 stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL, stdin=subprocess.DEVNULL)
time.sleep(10)
subprocess.run([ADB,'forward','tcp:%d'%PORT,'tcp:%d'%PORT],capture_output=True)

# 检查 fs86 进程
out,_ = raw(['su','0','ps','-A'])
n_fs = sum(1 for l in out.splitlines() if 'fs86' in l)
P('  fs86 进程数: %d' % n_fs)

p2 = getpid()
P('  游戏状态: %s' % ('存活 pid=%s' % p2 if p2 else '★ 已被杀'))
if not p2:
    P('  游戏被杀，实验终止'); o.close(); sys.exit(1)

# ---- 连接 ----
import frida
dev = None
for att in range(5):
    try:
        dev = frida.get_device_manager().add_remote_device('127.0.0.1:%d' % PORT)
        ps = dev.enumerate_processes()
        P('  ★ frida 连接成功: %d 进程' % len(ps))
        break
    except Exception as e:
        P('  尝试 %d: %s' % (att+1, str(e)[:60]))
        time.sleep(4)
if dev is None:
    P('  ✗ frida 连不上'); o.close(); sys.exit(1)

# 再确认游戏
p3 = getpid()
P('  连接后游戏: %s' % ('存活 pid=%s' % p3 if p3 else '★ 已被杀'))
if not p3:
    o.close(); sys.exit(1)

# ---- attach ----
P('')
P('=' * 74); P('★ attach'); P('=' * 74)
tgt = [p for p in ps if p.name == PKG]
if not tgt:
    P('  找不到进程'); o.close(); sys.exit(1)
tpid = tgt[0].pid
P('  attach pid=%d' % tpid)

SCRIPT = r'''
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
        onEnter: function(a) {
            this.n = null; try { this.n = a[0].readCString(); } catch(e){}
            this.o = a[1];
        },
        onLeave: function(r) {
            if (this.n === 'init.svc.adbd' || this.n === 'ro.debuggable' ||
                this.n === 'service.adb.tcp.port') {
                try { this.o.writeUtf8String(''); r.replace(0); } catch(e){}
            }
        }
    });
    send({t:'anti'});
}
var hud = false;
function hookAll() {
    if (hud) return true;
    var m = null;
    Process.enumerateModules().forEach(function(x){
        if (x.name.indexOf('libil2cpp') >= 0 && !m) m = x;
    });
    if (!m) return false;
    hud = true;
    send({ t:'mod', base: m.base.toString(), size: m.size });
    var T = [
        [0x09CBC644,'DESCrypt::Decrypt'],[0x075ACCE8,'DecryptSource'],
        [0x09209140,'__Gen_Wrap_215'],[0x06E57494,'DecryptXor'],
        [0x06E574F4,'Decrypt_1'],[0x06E57B10,'Decrypt_2'],
        [0x06E57CF0,'DecryptDES'],[0x06E58BCC,'DecryptRijndael'],
        [0x07F1E688,'DecryptBytesToBytes'],[0x07F76CD8,'ApplyRuntimeLuaPatch'],
        [0x07F6A01C,'LoadRuntimeLua'],[0x07F762B4,'DefaultLoadCodeAndLuban'],
        [0x0828747C,'get_SecretKey_stub'],[0x07F69F64,'LoadCodeAndLubanAndLocalize'],
        [0x0DC6ABF4,'SaltKeyDecrypt'],[0x0DC6B544,'AesDecryption'],
    ];
    var n = 0;
    T.forEach(function(t){
        var a = m.base.add(t[0]);
        try {
            if (a.readU32() === 0) { send({t:'zero', n:t[1]}); return; }
            Interceptor.attach(a, {
                onEnter: function(args) {
                    var rec = { t:'call', n:t[1],
                        a0:args[0].toString(), a1:args[1].toString(),
                        a2:args[2].toString(), a3:args[3].toString() };
                    [0,1,2,3].forEach(function(i){
                        try { var s = args[i].readUtf8String(96);
                            if (s && s.length>=3 && /^[\x20-\x7e]+$/.test(s)) rec['s'+i]=s; } catch(e){}
                        try { var u = args[i].readUtf16String(96);
                            if (u && u.length>=3 && /^[\x20-\x7e]+$/.test(u)) rec['u'+i]=u; } catch(e){}
                    });
                    send(rec);
                },
                onLeave: function(ret) {
                    var v = ret.toString();
                    if (v && v !== '0x0') {
                        var out = { t:'ret', n:t[1], v:v };
                        try { var s = ret.readUtf8String(80);
                            if (s && /^[\x20-\x7e]+$/.test(s)) out.s=s; } catch(e){}
                        try { var u = ret.readUtf16String(80);
                            if (u && /^[\x20-\x7e]+$/.test(u)) out.u=u; } catch(e){}
                        send(out);
                    }
                }
            });
            n++;
        } catch(e) {}
    });
    send({ t:'hookedn', n:n });
    return true;
}
hookAll();
var iv = setInterval(function(){ if (hookAll()) clearInterval(iv); }, 2000);
rpc.exports = { ping: function(){ return 'ok'; } };
'''
try:
    sess = dev.attach(tpid)
    P('  attach OK')
    sc = sess.create_script(SCRIPT)
    calls = []
    def on_msg(m, d):
        if m.get('type') == 'send':
            p = m['payload']; t = p.get('t')
            if t == 'anti': P('  ★ 反检测生效')
            elif t == 'mod': P('  ★ il2cpp base=%s size=%d' % (p['base'], p['size']))
            elif t == 'hookedn': P('  ★★ hook %d 个函数' % p['n'])
            elif t == 'zero': P('  zero %s' % p['n'])
            elif t == 'call':
                calls.append(p); P('  ★★★ 调用 %s' % p['n'])
                P('       a0=%s a1=%s a2=%s a3=%s' % (p['a0'],p['a1'],p['a2'],p['a3']))
                for k in sorted(p):
                    if k[0] in 'su' and k[1:].isdigit(): P('       %s = %r' % (k, p[k]))
            elif t == 'ret':
                P('  ★★★ 返回 %s = %s' % (p['n'], p['v']))
                if 's' in p: P('       str   = %r' % p['s'])
                if 'u' in p: P('       utf16 = %r' % p['u'])
        else:
            P('  ERR %s' % json.dumps(m, ensure_ascii=False)[:250])
    sc.on('message', on_msg)
    sc.load()
    P('  script loaded')
except Exception as e:
    P('  attach 失败: %s' % e); o.close(); sys.exit(1)

P('')
P('=' * 74); P('★ 监控（5 分钟）'); P('=' * 74)
for i in range(20):
    time.sleep(15)
    alive = getpid()
    P('  %3ds: %s  (捕获 %d)' % ((i+1)*15, '存活' if alive else '★ 已被杀', len(calls)))
    if not alive: break
    if len(calls) > 80: break

P('')
P('=' * 74); P('★ 结果'); P('=' * 74)
P('  捕获 %d 次' % len(calls))
json.dump(calls, open(os.path.join(W,'work','LATE3_CAPTURE.json'),'w',
          encoding='utf-8'), ensure_ascii=False, indent=1, default=str)
cands = set()
for c in calls:
    for k in c:
        if k[0] in 'su' and k[1:].isdigit() and 4 <= len(c[k]) <= 64:
            cands.add(c[k])
P('  候选字符串 %d 个:' % len(cands))
for c in sorted(cands)[:100]: P('     %r' % c)
P('  WROTE work/LATE3_CAPTURE.json')
o.close()
print('WROTE work/late_attach3_log.txt')
