#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_map3_run.py —— 跑 x_map3.py 的核心逻辑（去掉 tokva 验证）
   需要：decompiled/libil2cpp_MASTER.so + work/deace/global-metadata_clean.dat
"""
import os, pickle, struct, sys, io
W = r'D:\元气骑士mod'
SO = W + r'\decompiled\libil2cpp_MASTER.so'
MD = W + r'\work\deace\global-metadata_clean.dat'
OUT = W + r'\work\_x_map3.pkl'
o = io.open(W + r'\work\map3_run.txt', 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

so = open(SO, 'rb').read()
md = open(MD, 'rb').read()
P('so %d B, md %d B' % (len(so), len(md)))

SEGS = [
    ('.rodata',      0x0002cc1020, 0x0002cc1020, 0x0ec42cb),
    ('.text',        0x00059d6c2c, 0x00059d2c2c, 0x085bdbc),
    ('il2cpp',       0x00062329e8, 0x000622e9e8, 0x931ad84),
    ('.plt',         0x000f54d770, 0x000f549770, 0x0002470),
    ('.data.rel.ro', 0x000f553be0, 0x000f54bbe0, 0x08ed2e8),
    ('.got',         0x000fe41168, 0x000fe39168, 0x01c08d0),
    ('.data',        0x0010006c78, 0x000fffac78, 0x096b3d8),
]
def va2off(va):
    for nm, a, oo, s in SEGS:
        if s and a <= va < a + s:
            return oo + (va - a)
    return None
def rd(va, n):
    p = va2off(va)
    return so[p:p+n] if (p is not None and 0 <= p and p+n <= len(so)) else b''
def U64(va):
    x = rd(va, 8)
    return struct.unpack('<Q', x)[0] if len(x) == 8 else 0
def U32(va):
    x = rd(va, 4)
    return struct.unpack('<I', x)[0] if len(x) == 4 else 0
def CS(va, mn=400):
    p = va2off(va)
    if p is None: return None
    e = so.find(b'\x00', p, p+mn)
    return so[p:e].decode('utf-8', 'replace') if e > p else None

# ── header ──
T = {}
for i in range(32):
    a, b = struct.unpack('<II', md[8+i*8:16+i*8])
    T[i] = (a, b)
STR_O, STR_S = T[2]
MET_O, MET_S = T[5]
TYP_O, TYP_S = T[19]
IMG_O, IMG_S = T[20]
MSTR, STYPE, SIMG = 36, 88, 40
NMET = MET_S // MSTR
NTYP = TYP_S // STYPE
NIMG = IMG_S // SIMG
P('NMET %d, NTYP %d, NIMG %d' % (NMET, NTYP, NIMG))

def mstr(idx):
    if idx is None or idx >= STR_S: return None
    p = STR_O + idx
    e = md.find(b'\x00', p)
    return md[p:e].decode('utf-8', 'replace')

MN = [struct.unpack('<9I', md[MET_O+i*MSTR:MET_O+i*MSTR+36]) for i in range(NMET)]

TD = []
for i in range(NTYP):
    p = TYP_O + i*STYPE
    ni, nsi = struct.unpack('<2I', md[p:p+8])
    mstart = struct.unpack('<I', md[p+0x24:p+0x28])[0]
    mcnt = struct.unpack('<H', md[p+0x40:p+0x42])[0]
    TD.append((ni, nsi, mstart, mcnt))
P('TD %d 条' % len(TD))

IMGS = []
for i in range(NIMG):
    p = IMG_O + i*SIMG
    ni = struct.unpack('<I', md[p:p+4])[0]
    ts = struct.unpack('<I', md[p+8:p+12])[0]
    tc = struct.unpack('<I', md[p+12:p+16])[0]
    IMGS.append((mstr(ni), ts, tc))
P('IMGS %d' % len(IMGS))

# ── image → method range ──
img2range = {}
for i, (nm, ts, tc) in enumerate(IMGS):
    lo = hi = None
    for t in range(ts, min(ts+tc, NTYP)):
        ms, mc = TD[t][2], TD[t][3]
        if mc == 0: continue
        lo = ms if lo is None else min(lo, ms)
        hi = (ms+mc-1) if hi is None else max(hi, ms+mc-1)
    img2range[i] = (lo, hi, (hi-lo+1) if lo is not None else 0)
P('image methods 合计 %d (metadata %d)' % (sum(v[2] for v in img2range.values()), NMET))

# ── codeGenModules ──
CG = 0x00100c2438
mods = []
for i in range(190):
    p = U64(CG + i*8)
    if not p: continue
    nm = CS(U64(p))
    cnt = U32(p+8)
    mp = U64(p+0x10)
    mods.append((i, nm, cnt, mp))
P('codeGenModules %d' % len(mods))

name2img = {nm: i for i, (nm, ts, tc) in enumerate(IMGS) if nm}
mod2img = {i: name2img[nm] for i, nm, cnt, mp in mods if nm in name2img}

VA2G = {}
hit = miss = 0
for i, nm, cnt, mp in mods:
    im = mod2img.get(i)
    if im is None: miss += cnt; continue
    lo, hi, n = img2range[im]
    if n == 0: miss += cnt; continue
    for j in range(cnt):
        g = lo + j
        va = U64(mp + j*8)
        if va and g < NMET:
            VA2G.setdefault(va, g); hit += 1
P('VA→global %d (未对上 %d)' % (hit, miss))

pickle.dump({'VA2G': VA2G, 'MN': MN, 'TD': TD, 'IMGS': IMGS}, open(OUT, 'wb'))
P('')
P('WROTE %s (%.1f MB)' % (OUT, os.path.getsize(OUT)/1048576))

# ── 测试 ──
sys.path.insert(0, W + r'\tools')
import importlib, x_vautil
importlib.reload(x_vautil)
V = x_vautil
P('')
P('=== x_vautil 测试 ===')
P('  VA2G %d, MN %d, TD %d' % (len(V.VA2G), len(V.MN), len(V.TD)))
# 已知锚点
ANCHORS = [
    (0x0666213C, 'CalculateFinalDamage'),
    (0x06479754, 'CalculateCriticalDamage'),
    (0x0647990C, 'ApplyNonCriticalDamageReduction'),
    (0x06ED8684, 'PrintDefenceFormula'),
    (0x062AFE24, 'GetCriticFactor'),
    (0x08308728, 'AddCriticFactor'),
]
P('')
P('  锚点验证:')
for va, expect in ANCHORS:
    nm = V.nm_of(va)
    P('    0x%08X  %-34s %s' % (va, expect, ('✓ ' + nm) if nm else '✗ 无'))

# 关键函数查找
P('')
P('  链函数查找:')
for kw in ['ResolveMoveSpeed','ResolveWeaponSpeed','ArmorRestoreSpeed',
           'SkillHaste','Cooldown','MoveSpeed','AttackSpeed','Armor']:
    h = V.find_va_kw(kw)
    P('    %-22s %d 个' % (kw, len(h)))
    for va, nm in h[:4]:
        P('       0x%08X  %s' % (va, nm))

o.close()
print('WROTE work/map3_run.txt')
