#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_kpa_correct.py —— ★★★ 正确的 KPA：先用跨文件一致性确定 padding 对齐
   已知：
     84 个 talent 文件的前 56 字节密文完全相同 ⟹ 明文前 56 字节相同
     12 个文件的尾部明文已知（格式统一）
   正确做法：
     talent 文件 560 B 的（1 个）与 87 组不一致 → 是特例
     对每个已验证文件，尝试所有 padding 对齐，选【能使已知明文块匹配】
"""
import sys, io, os, re, json, base64, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'kpa_correct_log.txt'), 'w', encoding='utf-8')
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

VERIFIED = ['CriticalExplosion','ExplosiveDevice','LifeSiphon','Reforge','SpikedShell',
            'SkillHaste','MovementCharge','SwordQi','SpikeProtection','DisplacementBlast',
            'PartyTime','EnergySiphon']
def pt_tail(n):
    return ('function %s.on_remove(player)\n'
            '    Buffs.remove(player, CS.emBuff.%s)\n'
            'end\n\n\n\n\n\n\nreturn %s\n' % (n, n, n)).encode()

# ---- ★ 关键改进：明文可能是 p + PKCS7 padding，或明文长度恰好整除 8 ----
P('')
P('=' * 74); P('★ 正确 KPA：遍历明文尾部在密文中的对齐'); P('=' * 74)
P('  对每个文件，构造 [明文尾][padding]，找出能与"已知密文块"一致的组合')
P('')

# 先用【最可信】的一个文件做基准：SkillHaste（只有 28 块，28 B*8=224 B）
# 明文尾 113 B，若密文 224 B，则明文总长 ≤ 224 B
# ⇒ 头部 + 尾 = 224 B ⇒ 头部 ≈ 111 B
# 而明文头 56 B 已知固定，所以主体 ≈ 55 B

results = {}
for n in VERIFIED:
    for pre in ('app/buff/', 'app/talent/'):
        rel = pre + n + '.bytes'
        if rel not in D: continue
        c = D[rel]; pt = pt_tail(n)
        nb = len(c) // 8
        # 明文总长 = 密文长（PKCS7 补齐后）
        # 明文尾 pt 位于明文末尾（padding 之前）
        # padding 长度 1..8
        best = []
        for padlen in range(1, 9):
            full_pt_len = len(pt) + padlen
            if full_pt_len > len(c): continue
            # 明文尾部对齐：pt 结束位置 = len(c) - padlen
            start = len(c) - padlen - len(pt)
            if start < 0 or start % 8 != 0: continue
            seg = c[start:start+len(pt)]
            ks = bytes(a ^ b for a, b in zip(seg, pt))
            # 检查 ks 是否为"看起来正确"的密钥流：不应有太多 0
            nz = sum(1 for x in ks if x != 0)
            best.append((start % 8, padlen, start, nz, ks[:16]))
        if best:
            results[rel] = best

P('  %-44s %s' % ('file', '可行的对齐组合 (start%%8, padlen, start, 非零数, ks[:16])'))
for rel in sorted(results):
    for b in results[rel][:3]:
        P('  %-44s %s' % (rel.split('/')[-1][:-6], b[:4]))

# ---- ★★ 用跨文件一致性确定正确答案 ----
P('')
P('=' * 74); P('★★ 跨文件一致性检验（找正确的对齐）'); P('=' * 74)
# 若 ECB：明文块相同的部分，密钥流应相同（因为 ECB 无密钥流概念）
# 正确检验：对每个文件，用 [明文尾 + padding] 与之对应的密文，逐块对比
# 若 ECB 且明文尾相同 ⟹ 密文块应相同
# 但明文尾带文件名，所以不完全相同 —— 除了 "function " 等固定部分

# 换思路：直接检验 padding 是否正确
P('  检验：若 padding 正确，则倒数第 padlen 个密文块也应能被验证')
for rel in sorted(results):
    n = rel.split('/')[-1][:-6]
    pt = pt_tail(n)
    c = D[rel]
    for (_, padlen, start, nz, ks16) in results[rel][:1]:
        P('  %-40s padlen=%d start=%d' % (n, padlen, start))

# ---- ★★★ 最强检验：用"明文尾部版本2"（内存里的真实文本） ----
P('')
P('=' * 74); P('★★★ 用内存实证的完整明文（含空白）做基准'); P('=' * 74)
# 内存里抓到的真实文本（seg0207）：
# "function CriticalExplosion.on_remove(player)\n    Buffs.remove(player, CS.emBuff.CriticalExplosion)\nend\n\n\n\n\n\n\nreturn CriticalExplosion\n"
# 注意：内存里的换行数是 7 个（\n\n\n\n\n\n\n），我用的是同样
# 但可能内存里的实际是 8 个换行或其他

# 直接对 CriticalExplosion 做全遍历
rel = 'app/buff/CriticalExplosion.bytes'
if rel in D:
    c = D[rel]
    n = 'CriticalExplosion'
    P('  %s: 密文 %d B = %d 块' % (n, len(c), len(c)//8))
    # 明文尾候选（不同换行数）
    for nl in range(1, 12):
        pt = ('function %s.on_remove(player)\n'
              '    Buffs.remove(player, CS.emBuff.%s)\n'
              'end\n' % (n, n)) + b'\n' * nl + ('return %s\n' % n).encode()
        for padlen in range(1, 9):
            total = len(pt) + padlen
            if total > len(c): continue
            start = len(c) - total
            if start % 8 != 0: continue
            seg = c[start:start+len(pt)]
            ks = bytes(a ^ b for a, b in zip(seg, pt))
            # 找第一个可解块
            first = None
            for i in range(0, len(ks) - 7, 8):
                if ks[i:i+8] != b'\x00'*8:
                    first = ks[i:i+8]; break
            if first:
                P('     nl=%2d pad=%d start=%4d  ks[0:8]=%s' % (nl, padlen, start, first.hex(' ')))

o.close()
print('WROTE work/kpa_correct_log.txt')
