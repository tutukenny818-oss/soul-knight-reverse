#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_build_accurate_cb.py —— ★★★ 用正确对齐建高精度码本 + 验证 ECB
   正确对齐（已解出）：
     CriticalExplosion padlen=2, DisplacementBlast padlen=2, EnergySiphon padlen=1,
     ExplosiveDevice padlen=8, LifeSiphon padlen=7, MovementCharge padlen=3,
     PartyTime padlen=2, Reforge padlen=8, SkillHaste padlen=7, SpikedShell padlen=4,
     SwordQi padlen=8, SpikeProtection padlen=8
"""
import sys, io, os, re, json, base64, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'accurate_cb_log.txt'), 'w', encoding='utf-8')
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

# 已知正确对齐
ALIGN = {
    'app/buff/CriticalExplosion.bytes': 2,  'app/talent/CriticalExplosion.bytes': 2,
    'app/buff/DisplacementBlast.bytes': 2,  'app/talent/DisplacementBlast.bytes': 2,
    'app/buff/EnergySiphon.bytes': 1,       'app/talent/EnergySiphon.bytes': 1,
    'app/buff/ExplosiveDevice.bytes': 8,    'app/talent/ExplosiveDevice.bytes': 8,
    'app/buff/LifeSiphon.bytes': 7,         'app/talent/LifeSiphon.bytes': 7,
    'app/buff/MovementCharge.bytes': 3,     'app/talent/MovementCharge.bytes': 3,
    'app/buff/PartyTime.bytes': 2,          'app/talent/PartyTime.bytes': 2,
    'app/buff/Reforge.bytes': 8,            'app/talent/Reforge.bytes': 8,
    'app/buff/SkillHaste.bytes': 7,         'app/talent/SkillHaste.bytes': 7,
    'app/buff/SpikedShell.bytes': 4,        'app/talent/SpikedShell.bytes': 4,
    'app/buff/SwordQi.bytes': 8,            'app/talent/SwordQi.bytes': 8,
    'app/talent/SpikeProtection.bytes': 8,
}

def pt_tail(n):
    return ('function %s.on_remove(player)\n'
            '    Buffs.remove(player, CS.emBuff.%s)\n'
            'end\n\n\n\n\n\n\nreturn %s\n' % (n, n, n)).encode()

# ---- 建码本：用正确对齐提取明文的每一块 ----
CB = {}
for rel, padlen in ALIGN.items():
    if rel not in D: continue
    n = rel.split('/')[-1][:-6]
    c = D[rel]; pt = pt_tail(n)
    total = len(pt) + padlen
    start = len(c) - total
    if start < 0 or start % 8: continue
    # 明文 = pt + padding
    full = pt + bytes([padlen]) * padlen
    assert len(full) == total
    nb = total // 8
    for i in range(nb):
        cb = c[start + i*8: start + i*8 + 8]
        pb = full[i*8:(i+1)*8]
        if len(cb) == 8 and len(pb) == 8:
            if cb in CB and CB[cb] != pb:
                P('  ⚠ 冲突: %s 已映射 %r, 新 %r' % (cb.hex(), CB[cb], pb))
            CB[cb] = pb
P('★ 高精度码本 %d 条' % len(CB))
P('')
for cb, pb in list(CB.items())[:50]:
    P('   %s → %r' % (cb.hex(' '), pb))

# 保存
json.dump({cb.hex(): {'pt': pb.hex(),
                      'ascii': ''.join(chr(x) if 32 <= x < 127 else '.' for x in pb)}
           for cb, pb in CB.items()},
          open(os.path.join(W,'work','codebook_v4.json'),'w',encoding='utf-8'),
          ensure_ascii=False, indent=1)
P('')
P('WROTE work/codebook_v4.json')

# ---- ★★ ECB 验证：明文块相同 → 密文块相同？----
P('')
P('=' * 74); P('★★ ECB 一致性验证'); P('=' * 74)
# 检查码本里是否有"不同密文块映射到相同明文块"
bypt = collections.defaultdict(list)
for cb, pb in CB.items():
    bypt[pb].append(cb)
P('  不同明文块数 %d' % len(bypt))
dup = {k: v for k, v in bypt.items() if len(v) > 1}
P('  同一明文的多个密文块 %d 个:' % len(dup))
for pb, cbs in list(dup.items())[:20]:
    P('     %r ← %s' % (pb, ', '.join(c.hex() for c in cbs[:3])))

# ---- ★★★ 全库解码 ----
P('')
P('=' * 74); P('★★★ 全库解码（高精度码本）'); P('=' * 74)
tot_hit = 0; tot_blk = 0
best = []
dec = {}
for rel in sorted(D):
    d = D[rel]
    nb = len(d)//8
    out = []; hits = 0
    for k in range(nb):
        blk = d[k*8:k*8+8]
        if blk in CB:
            out.append(CB[blk]); hits += 1
        else:
            out.append('·'*8)
    dec[rel] = out
    tot_hit += hits; tot_blk += nb
    if hits: best.append((hits, nb, rel))
best.sort(reverse=True)
P('  总命中 %d / %d 块 (%.2f%%)' % (tot_hit, tot_blk, 100*tot_hit/max(1,tot_blk)))
P('')
for h, nb, rel in best[:30]:
    P('  %-52s %3d / %3d  (%.1f%%)' % (rel, h, nb, 100*h/nb))

# ---- 打印解码结果 ----
P('')
P('=' * 74); P('★ 解码预览'); P('=' * 74)
for h, nb, rel in best[:8]:
    P('')
    P('--- %s (%d/%d) ---' % (rel, h, nb))
    s = ''.join(x if x != '·'*8 else '??' for x in dec[rel])
    P('   %s' % s[:1200])

o.close()
print('WROTE work/accurate_cb_log.txt + codebook_v4.json')
