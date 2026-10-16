#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_scan_utf16.py —— ★★★★★ UTF-16LE 扫描（子 agent 的关键发现）
   之前只扫 ASCII 全空 → 子 agent 发现 UTF-16LE 里整片 Lua 源码
   目标：
     1) UTF-16LE 的 Lua 模块头（local X = TalentBase.create(...)）
     2) UTF-16LE 的 on_remove 尾部
     3) ★ 8 字符 ASCII 的 UTF-16LE 形式（DES 密钥候选）
     4) 用 CBC 定锚表验证
"""
import sys, io, os, re, json, collections, struct
W = r'D:\元气骑士mod'
D = os.path.join(W, 'work', 'keyscan')
o = io.open(os.path.join(W, 'work', 'scan_utf16_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

files = sorted(f for f in os.listdir(D) if f.endswith('.bin') and
               os.path.getsize(os.path.join(D, f)) > 0)
P('段 %d, 总 %.0f MB' % (len(files),
  sum(os.path.getsize(os.path.join(D, f)) for f in files)/1048576))

# ---- UTF-16LE 模式搜索 ----
def u16(s):
    return s.encode('utf-16-le')

PATS = {
    'on_remove':     u16('on_remove'),
    'Buffs.remove':  u16('Buffs.remove'),
    'emBuff':        u16('emBuff'),
    'app.talent':    u16('app.talent'),
    'app.buff':      u16('app.buff'),
    'TalentBase':    u16('TalentBase'),
    'BuffBase':      u16('BuffBase'),
    'local ':        u16('local '),
    'function ':     u16('function '),
    'return ':       u16('return '),
    'require':       u16('require'),
    'CS.emBuff':     u16('CS.emBuff'),
    'Decrypt':       u16('Decrypt'),
    'PASSWORD':      u16('PASSWORD'),
    'sT0r3':         u16('sT0r3'),
    'soulKnight':    u16('soulKnight'),
    'iambo':         u16('iambo'),
    'Ahbool':        u16('Ahbool'),
}
res = collections.defaultdict(list)
for f in files:
    b = open(os.path.join(D, f), 'rb').read()
    for name, pat in PATS.items():
        st = 0; cnt = 0
        while cnt < 400:
            i = b.find(pat, st)
            if i < 0: break
            res[name].append((f, i))
            st = i + 2; cnt += 1

P('')
P('=' * 74); P('★ UTF-16LE 命中统计'); P('=' * 74)
for name in PATS:
    lst = res.get(name, [])
    if lst:
        byf = collections.Counter(f for f, _ in lst)
        P('  %-16s %5d 处, %d 个段' % (name, len(lst), len(byf)))
        for f, c in byf.most_common(4):
            P('        %-46s %d' % (f, c))

# ---- 提取 Lua 源码 ----
P('')
P('=' * 74); P('★ 提取 UTF-16LE Lua 源码片段'); P('=' * 74)
ALL = []
NAMES = set()
for f in files:
    b = open(os.path.join(D, f), 'rb').read()
    for m in re.finditer(u16('local '), b):
        i = m.start()
        # 向后取 400 字节，按 UTF-16 解码，遇到非 ASCII 停
        seg = b[i:i+600]
        try:
            t = seg.decode('utf-16-le', 'ignore')
        except Exception:
            continue
        # 只留可打印
        t = ''.join(ch for ch in t if 32 <= ord(ch) < 127 or ch in '\r\n\t')
        if len(t) < 20: continue
        if ('TalentBase' in t or 'BuffBase' in t or 'require' in t or
            'create' in t or 'emBuff' in t):
            ALL.append((f, i, t[:300]))
P('  候选片段 %d' % len(ALL))
seen = set()
for f, i, t in ALL[:120]:
    k = t[:70]
    if k in seen: continue
    seen.add(k)
    P('')
    P('  %s @0x%X' % (f, i))
    P('    %s' % t[:280])
    # 提取模块名
    for m in re.finditer(r'TalentBase\.create\(CS\.emBuff\.(\w+)', t):
        NAMES.add(m.group(1))
    for m in re.finditer(r'BuffBase\.create\(CS\.emBuff\.(\w+)', t):
        NAMES.add(m.group(1))
    for m in re.finditer(r'local (\w+) = ', t):
        NAMES.add(m.group(1))

P('')
P('  ★ 提取到的模块名 %d 个:' % len(NAMES))
P('     %s' % ', '.join(sorted(NAMES)[:80]))

# ---- 8 字符 UTF-16LE ASCII 密钥候选 ----
P('')
P('=' * 74); P('★ UTF-16LE 8 字符 ASCII 候选（DES 密钥）'); P('=' * 74)
cands = set()
for f in files:
    b = open(os.path.join(D, f), 'rb').read()
    # 找 UTF-16LE 的 8 个 ASCII 字符 = 16 字节，每字节低位 ASCII 高位 0
    n = len(b)
    i = 0
    while i < n - 16:
        ok = True
        for k in range(8):
            c = b[i+k*2]; h = b[i+k*2+1]
            if h != 0 or not (33 <= c < 127):
                ok = False; break
        if ok:
            cands.add(b[i:i+16].decode('utf-16-le'))
            i += 2
        else:
            i += 1
P('  候选 %d 个' % len(cands))
# 过滤：只要看起来像密钥的（非纯字典词）
import re as _re
interesting = [c for c in cands if
               _re.search(r'[0-9@_!]', c) or
               any(x in c.lower() for x in ['key','pass','salt','soul','knight',
                                             'chilly','iambo','ahbool','des','aes',
                                             'crypt','secret','lua','game','buff'])]
P('  含密钥特征的 %d 个:' % len(interesting))
for c in sorted(interesting)[:100]:
    P('     %r' % c)

# 保存全部候选
json.dump(sorted(cands), open(os.path.join(W,'work','UTF16_8CHAR.json'),'w',
          encoding='utf-8'), ensure_ascii=False, indent=1)
P('  WROTE work/UTF16_8CHAR.json')

# ---- ★ 用这些候选 + CBC oracle 暴力 ----
P('')
P('=' * 74); P('★ 用 UTF-16 候选做 DES 暴力'); P('=' * 74)
try:
    from Crypto.Cipher import DES
    import base64 as B64
    def b64d(x):
        s = bytes(c for c in x if c not in b'\r\n\t ')
        try: return B64.b64decode(s, validate=False)
        except Exception:
            try: return B64.b64decode(s + b'='*((-len(s))%4), validate=False)
            except Exception: return None
    SRC = os.path.join(W,'work','lua_device_src')
    DM = {}
    for dp, dn, fn in os.walk(SRC):
        for f2 in fn:
            if not f2.endswith('.bytes'): continue
            pp = os.path.join(dp, f2)
            rel = os.path.relpath(pp, SRC).replace('\\','/')
            d = b64d(open(pp,'rb').read())
            if d: DM[rel] = d
    # oracle
    ALIGN = {('app/buff/CriticalExplosion.bytes'):2, 'app/talent/CriticalExplosion.bytes':2,
             'app/buff/DisplacementBlast.bytes':2, 'app/talent/DisplacementBlast.bytes':2,
             'app/buff/SkillHaste.bytes':7, 'app/talent/SkillHaste.bytes':7,
             'app/buff/SpikedShell.bytes':4, 'app/buff/Reforge.bytes':8,
             'app/buff/SwordQi.bytes':8, 'app/buff/LifeSiphon.bytes':7}
    def pt_tail(n):
        return ('function %s.on_remove(player)\n'
                '    Buffs.remove(player, CS.emBuff.%s)\n'
                'end\n\n\n\n\n\n\nreturn %s\n' % (n, n, n)).encode()
    ORACLES = []
    for rel, padlen in ALIGN.items():
        if rel not in DM: continue
        n = rel.split('/')[-1][:-6]
        c = DM[rel]; pt = pt_tail(n)
        total = len(pt) + padlen
        start = len(c) - total
        if start < 0 or start % 8: continue
        full = pt + bytes([padlen]) * padlen
        nb = total // 8
        for i in range(1, nb):
            ORACLES.append((c[start+i*8:start+(i+1)*8],
                            c[start+(i-1)*8:start+i*8],
                            full[i*8:(i+1)*8]))
    P('  oracle %d 对' % len(ORACLES))
    FLT = ORACLES[:4]
    hits = []
    tested = 0
    for s in sorted(cands):
        k = s.encode('utf-8')[:8]
        if len(k) != 8: continue
        tested += 1
        try:
            d_ = DES.new(k, DES.MODE_ECB)
            if all(bytes(a ^ b for a, b in zip(d_.decrypt(cl), cp)) == pl
                   for cl, cp, pl in FLT):
                hits.append(s); P('  ★★★★ DES 命中! %r' % s)
        except Exception: pass
    P('  测试 %d, 命中 %d' % (tested, len(hits)))

    # 也用 ASCII 8 字符（从所有段提取）
    P('')
    P('  ASCII 8 字符候选扫描')
    ascii8 = set()
    for f in files:
        b = open(os.path.join(D, f), 'rb').read()
        for m in re.finditer(rb'[\x21-\x7e]{8}', b):
            ascii8.add(m.group())
        if len(ascii8) > 500000: break
    P('  候选 %d' % len(ascii8))
    n2 = 0
    for s in list(ascii8):
        n2 += 1
        try:
            d_ = DES.new(s, DES.MODE_ECB)
            if all(bytes(a ^ b for a, b in zip(d_.decrypt(cl), cp)) == pl
                   for cl, cp, pl in FLT):
                hits.append(s.decode()); P('  ★★★★ ASCII DES 命中! %r' % s)
        except Exception: pass
    P('  测试 %d, 命中 %d' % (n2, len(hits)))
    if hits:
        json.dump(hits, open(os.path.join(W,'work','FOUND_KEYS_UTF16.json'),'w',
                  encoding='utf-8'), ensure_ascii=False, indent=1)
        P('  WROTE work/FOUND_KEYS_UTF16.json')
except Exception as e:
    P('  暴力失败: %s' % e)

o.close()
print('WROTE work/scan_utf16_log.txt')
