#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_cipher_detect.py —— ★ 判定加密模式：ECB vs CBC vs 流密码
   决定性测试：
     若 ECB：相同明文块 ⟹ 相同密文块（跨文件、跨位置都成立）
     若 CBC（固定 IV）：相同明文前缀 ⟹ 相同密文（仅前缀）
     若 流密码：无块结构，密文块与其位置强相关

   关键观察：
     9306c37c734a1a7e 在 87 个 talent 的 offset 0 —— 说明这 87 个文件明文首块相同
     但 ElementalCycle 的 354 块命中 0 —— 说明它的密钥流与 CriticalExplosion 不同
     ⇒ 混合模式？
"""
import sys, io, os, re, json, base64, collections, hashlib
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'cipher_detect_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

def b64d(b):
    s = bytes(c for c in b if c not in b'\r\n\t ')
    try: return base64.b64decode(s, validate=False)
    except Exception:
        try: return base64.b64decode(s + b'='*((-len(s))%4), validate=False)
        except Exception: return None

SRC = os.path.join(W,'work','lua_device_src')
D = {}
for dp, dn, fn in os.walk(SRC):
    for f in fn:
        if not f.endswith('.bytes'): continue
        p = os.path.join(dp, f)
        rel = os.path.relpath(p, SRC).replace('\\','/')
        d = b64d(open(p,'rb').read())
        if d: D[rel] = d
P('密文 %d' % len(D))

# ---- 测试 1：同位置块跨文件一致性 ----
P('')
P('=' * 74); P('测试1: 相同 offset 的块跨文件一致性'); P('=' * 74)
for off in (0, 8, 16, 24, 32, 40, 48, 56, 64, 72, 80, 88, 96):
    blks = collections.Counter()
    n = 0
    for rel, d in D.items():
        if len(d) > off + 8:
            blks[d[off:off+8]] += 1; n += 1
    if n:
        top, cnt = blks.most_common(1)[0]
        P('  off %3d: %4d 文件, 唯一 %3d, top ×%-3d (%s)' % (off, n, len(blks), cnt, top.hex()))

# ---- 测试 2：talent 全体的 offset 0..96 是否都相同 ----
P('')
P('=' * 74); P('测试2: talent 文件 offset 0-96 逐字节对比'); P('=' * 74)
tal = [(r, d) for r, d in D.items() if r.startswith('app/talent/') and len(d) >= 104]
tal.sort()
P('  样本 %d 个 talent（>=104 B）' % len(tal))
P('  %-38s %s' % ('file', 'off0-31'))
for rel, d in tal[:25]:
    P('  %-38s %s' % (rel.split('/')[-1], d[:32].hex(' ')))
P('')
# 统计每个 offset 的唯一值
for off in range(0, 104, 8):
    vals = set(d[off:off+8] for _, d in tal if len(d) > off+8)
    P('  off %3d: 唯一 %d' % (off, len(vals)))

# ---- 测试 3：★ 关键 —— 用已知明文反推密钥流，检查是否跨文件相同 ----
P('')
P('=' * 74); P('测试3: ★ 已知明文反推密钥流，检查跨文件一致性'); P('=' * 74)
VERIFIED = ['CriticalExplosion','ExplosiveDevice','LifeSiphon','Reforge','SpikedShell',
            'SkillHaste','MovementCharge','SwordQi','SpikeProtection','DisplacementBlast',
            'PartyTime','EnergySiphon']
def pt_tail(n):
    return ('function %s.on_remove(player)\n'
            '    Buffs.remove(player, CS.emBuff.%s)\n'
            'end\n\n\n\n\n\n\nreturn %s\n' % (n, n, n)).encode()

P('  对每个文件，从尾部反推密钥流（假设明文尾部格式统一）')
P('  %-38s %-6s %-6s %s' % ('file', 'ctB', 'ptB', '密钥流前 16 字节'))
for n in VERIFIED:
    for pre in ('app/buff/', 'app/talent/'):
        rel = pre + n + '.bytes'
        if rel not in D: continue
        c = D[rel]; p = pt_tail(n)
        if len(p) > len(c): 
            P('  %-38s %-6d %-6d (明文比密文长!)' % (rel.split('/')[-1], len(c), len(p)))
            continue
        # 对齐：密文尾部 len(p) 字节
        seg = c[-len(p):]
        ks = bytes(a ^ b for a, b in zip(seg, p))
        P('  %-38s %-6d %-6d %s' % (rel.split('/')[-1], len(c), len(p), ks[:16].hex(' ')))

# ---- 测试 4：密钥流是否跨文件相同 ----
P('')
P('=' * 74); P('测试4: ★★ 密钥流跨文件对比（决定性）'); P('=' * 74)
KS = {}
for n in VERIFIED:
    for pre in ('app/buff/','app/talent/'):
        rel = pre + n + '.bytes'
        if rel not in D: continue
        c = D[rel]; p = pt_tail(n)
        if len(p) <= len(c):
            seg = c[-len(p):]
            KS[rel] = bytes(a ^ b for a, b in zip(seg, p))
keys = list(KS)
P('  收集 %d 个密钥流' % len(keys))
if len(keys) >= 2:
    base = keys[0]
    P('  基准: %s (%d B)' % (keys[0], len(KS[base])))
    for k in keys[1:]:
        m = min(len(KS[base]), len(KS[k]))
        same = sum(1 for i in range(m) if KS[base][i] == KS[k][i])
        P('    %-40s 相同 %d/%d (%.0f%%)' % (k.split('/')[-1], same, m, 100*same/m))

# ---- 测试 5：同文件内密钥流是否重复（流密码周期检测）----
P('')
P('=' * 74); P('测试5: 密钥流内部周期检测'); P('=' * 74)
for rel in keys[:8]:
    ks = KS[rel]
    best = (0, 0)
    for per in range(1, min(200, len(ks)//2)):
        same = sum(1 for i in range(len(ks)-per) if ks[i] == ks[i+per])
        if same > best[0]: best = (same, per)
    P('  %-40s 长 %d  最佳周期 %d (匹配 %d)' % (rel.split('/')[-1], len(ks), best[1], best[0]))

o.close()
print('WROTE work/cipher_detect_log.txt')
