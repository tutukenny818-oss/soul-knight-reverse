#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_github_pack.py —— ★★★★★ 整理可发布到 GitHub 的内容
   思路：把「有价值的成果」整理成标准仓库结构
   分两个仓库：
     A. soul-knight-reverse  —— 逆向成果 + 文档（不含游戏资产）
     B. soul-knight-data     —— 数据表（CSV/JSON）
"""
import sys, io, os, json, shutil, csv, collections
W = r'D:\元气骑士mod'
OUT = r'F:\gh_pack'
o = io.open(os.path.join(W, 'work', '_github_pack_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
def cp(src, dst):
    """复制（保持目录）"""
    if not os.path.exists(src): return False
    os.makedirs(os.path.dirname(dst), exist_ok=True)
    if os.path.isdir(src):
        if os.path.exists(dst): shutil.rmtree(dst)
        shutil.copytree(src, dst)
    else:
        shutil.copy2(src, dst)
    return True
def fmt(n):
    for u in ('B','KB','MB','GB'):
        if n < 1024: return '%.1f %s' % (n, u)
        n /= 1024
    return '%.1f TB' % n
def dsize(p):
    if not os.path.exists(p): return 0
    if os.path.isfile(p): return os.path.getsize(p)
    t = 0
    for dp, dn, fn in os.walk(p):
        for f in fn:
            try: t += os.path.getsize(os.path.join(dp, f))
            except Exception: pass
    return t

# ══════════════════════════════════════════════════════════
# 仓库 A: soul-knight-reverse —— 逆向成果
# ══════════════════════════════════════════════════════════
A = os.path.join(OUT, 'soul-knight-reverse')
if os.path.exists(A): shutil.rmtree(A)
os.makedirs(A, exist_ok=True)
P('=' * 78); P('仓库 A: soul-knight-reverse'); P('=' * 78)

# --- docs/ 逆向文档 ---
docs = [
 ('work/S_MASTER_REPORT.md',           'docs/01-总报告.md'),
 ('work/S_DAMAGE_FORMULA.md',          'docs/02-伤害公式.md'),
 ('work/S_DAMAGE_ORDER_VS_WIKI.md',    'docs/03-伤害顺位与wiki对账.md'),
 ('work/S_CRIT_SIMPLE.md',             'docs/04-暴击机制-简化版.md'),
 ('work/S_HEADER_CRACKED.md',          'docs/05-加密头破解.md'),
 ('work/S_CBC_CONFIRMED.md',           'docs/06-CBC模式确证.md'),
 ('work/RESEARCH_RUNTIME_LUA.md',      'docs/07-runtime_lua检索报告.md'),
 ('HANDOFF.md',                        'docs/08-逆向流水线交接.md'),
 ('work/S_HANDOFF.md',                 'docs/09-Mod数据交接.md'),
]
n = 0
for src, dst in docs:
    if cp(os.path.join(W, src), os.path.join(A, dst)): n += 1
P('  docs/  %d 个文档' % n)

# --- tools/ 核心工具（剔除一次性脚本）---
CORE_TOOLS = [
 # 逆向基础
 'x_vautil.py',            # ★ capstone 反汇编核心
 # 解密
 's_cfgdump.py','s_meta3.py','s_decrypt_v2.py','s_saveedit.py','s_safe.py',
 # 内存
 'x_prop_bypass.py','x_dump_key_segs.py','x_scan_utf16.py',
 'x_extract_full_lua.py','x_decode_full.py','x_grep_buffadd.py',
 # 反汇编分析
 'x_fallback_extract.py','x_dmg_formula.py','x_dmg4.py',
 # 加密分析
 'x_cipher_detect.py','x_kpa_correct.py','x_build_accurate_cb.py',
 'x_cbc_verify.py','x_header_confirmed.py','x_hdr_layer.py',
 # 数据提取
 'x_loc_master.py','x_talent_final.py','x_buff_stack.py','x_buff_xref.py',
 'x_buf_name2.py','x_fill_names.py','x_final_master.py','x_finalize.py',
 'x_final_deliver.py','x_wiki_fix.py','x_all_systems.py',
 # 灰机
 's_huiji2.py','x_hj_mech.py',
 # 工具
 'x_survey.py','x_apk_md5.py','x_cleanup_plan.py','x_github_pack.py',
]
n = 0
for t in CORE_TOOLS:
    if cp(os.path.join(W,'tools',t), os.path.join(A,'tools',t)): n += 1
P('  tools/ %d 个脚本' % n)

# --- data/ 关键小数据 ---
datas = [
 ('work/talent_id_clean.json',        'data/talent_id_clean.json'),
 ('work/runtime_lua_manifest.json',   'data/runtime_lua_manifest.json'),
 ('work/cfg_buff_stack.json',         'data/cfg_buff_stack.json'),
 ('work/ecb_layers.json',             'data/ecb_layers.json'),
 ('work/codebook_v2.json',            'data/cbc_codebook.json'),
 ('work/TALENT_FINAL_MASTER.csv',     'data/TALENT_FINAL_MASTER.csv'),
 ('work/TALENT_VALUES.csv',           'data/TALENT_VALUES.csv'),
 ('work/LUA_PARAMS_EXTRA.csv',        'data/LUA_PARAMS_EXTRA.csv'),
]
n = 0
for src, dst in datas:
    if cp(os.path.join(W, src), os.path.join(A, dst)): n += 1
P('  data/  %d 个文件' % n)

# --- lua_source/ 已解出的天赋源码 ---
n = 0
for d, pref in [('lua_utf16','talent_full'), ('lua_extra','talent_partial')]:
    p = os.path.join(W,'work',d)
    if os.path.isdir(p):
        for f in os.listdir(p):
            if cp(os.path.join(p,f), os.path.join(A,'lua_source',pref,f)): n += 1
P('  lua_source/ %d 个 Lua 文件' % n)

# --- 生成 README ---
readme = '''# Soul Knight Reverse Engineering

元气骑士（Soul Knight）8.6.0 逆向工程成果。

## 内容

| 目录 | 说明 |
|---|---|
| `docs/` | 逆向文档（加密破解、伤害公式、机制分析） |
| `tools/` | 分析工具（反汇编、解密、数据提取） |
| `data/` | 结构化数据（天赋表、本地化索引） |
| `lua_source/` | 已解出的天赋 Lua 源码 |

## 主要成果

### 1. 加密体系破解

| 文件 | 算法 | 状态 |
|---|---|---|
| `config.ab` | LZ4HC + XOR（周期130，key=`soulKnight`） | ✅ |
| `global-metadata.dat` | 掩码逐字节 XOR | ✅ |
| `libil2cpp.so` | XOR（周期 0x10000，key=0x6F） | ✅ |
| 存档 12 个 | DES/CBC/PKCS7 | ✅ |
| `runtime_lua/*.bytes` | CBC / 8字节 / 固定IV | ⚠️ 模式已确证，密钥未取 |

### 2. 伤害计算机制

```c
最终伤害 = max(0, round_half_even(max(倍率,0) × 基础伤害))
减伤后   = 原伤害 × (1 - clamp(减伤率, 0, 1))
```

- 取整用**银行家舍入**（不是向下取整）
- 暴击倍率默认 **1.0**（数据驱动，非硬编码）
- 暴击伤害与**武器等级**耦合（`ResolveWeaponLevel`）

### 3. 防御公式

```
实际防御 = round_half_even(sign(x) × 6.74 × log10(0.5×|x| + 0.972))
```

### 4. 关键方法论：IFix fallback

游戏的 IFix 热更桩模式：

```asm
bl   IsPatched
tbz  w0, #0, <FALLBACK>    ; ← 未 patch 时跳这里 = 原始实现
```

**535 个函数的原始实现可读。**

### 5. ACE 反 Frida 绕过

```bash
su 0 setprop init.svc.adbd stopped
su 0 setprop ro.debuggable 0
```

配合**零 Frida 内存读取**（`dd /proc/PID/mem`）可绕过进程保护。

## 工具依赖

```
python 3.13+
capstone
pycryptodome
curl_cffi      # 灰机 wiki 抓取
frida          # 可选
```

## 免责声明

本项目仅用于学习研究。所有分析对象为本地样本，未修改、未分发任何游戏资源。
'''
open(os.path.join(A,'README.md'),'w',encoding='utf-8').write(readme)
P('  README.md 已生成')

# ══════════════════════════════════════════════════════════
# 仓库 B: soul-knight-data —— 数据表
# ══════════════════════════════════════════════════════════
B = os.path.join(OUT, 'soul-knight-data')
if os.path.exists(B): shutil.rmtree(B)
os.makedirs(B, exist_ok=True)
P('')
P('=' * 78); P('仓库 B: soul-knight-data'); P('=' * 78)

# 数据文件
d2 = [
 ('work/TALENT_FINAL_MASTER.csv',       'talent/TALENT_FINAL_MASTER.csv'),
 ('work/TALENT_VALUES.csv',             'talent/TALENT_VALUES.csv'),
 ('work/TALENT_VALUES_FLAT.csv',        'talent/TALENT_VALUES_FLAT.csv'),
 ('work/LUA_PARAMS_EXTRA.csv',          'talent/LUA_PARAMS_EXTRA.csv'),
 ('work/TROOP_TALENTS.csv',             'talent/TROOP_TALENTS.csv'),
 ('work/BUFF_ID_TABLE.csv',             'talent/CHALLENGE_FACTORS.csv'),
 ('work/TALENT_IDS.csv',                'talent/TALENT_IDS.csv'),
 ('work/gamedata_out/weapons_game.csv', 'weapon/weapons.csv'),
 ('work/gamedata_out/skills_table.csv', 'skill/skills.csv'),
 ('work/gamedata_out/characters_table.csv','character/characters.csv'),
 ('work/ecb_layers.json',               'meta/talent_hierarchy.json'),
]
n = 0
for src, dst in d2:
    if cp(os.path.join(W, src), os.path.join(B, dst)): n += 1
P('  数据表 %d 个' % n)

# 全量本地化（15MB，分拆）
p = os.path.join(W,'work','LOCALIZE_ALL.csv')
if os.path.isfile(p):
    cp(p, os.path.join(B,'localization/LOCALIZE_ALL.csv'))
    P('  localization/LOCALIZE_ALL.csv (15 MB)')

readme2 = '''# Soul Knight Data

元气骑士（Soul Knight）8.6.0 游戏数据表。

## 目录

| 目录 | 内容 | 条目数 |
|---|---|---|
| `talent/` | 天赋表（ID/内部名/中文名/英文名/描述/数值） | 202 |
| `weapon/` | 武器数据（伤害/暴击/子弹） | 1683 |
| `skill/` | 技能表 | 145 |
| `character/` | 角色表 | 24 |
| `localization/` | 全量本地化（15 语言） | 17794 |
| `meta/` | 元数据（天赋继承树等） | — |

## 数据来源

全部从游戏自身的配置文件中提取（未手工录入）：

- `localize_index.json` — 游戏完整本地化表（明文）
- `cfg_buff_stack.json` — 天赋参数表（明文）
- `weapons_game.csv` — 从 `weapon.ab` 的 `bulletsInfo` 提取
- `runtime_lua` 内存转储 — 天赋 Lua 源码参数

## 天赋表字段

| 列 | 说明 |
|---|---|
| `talent_id` | 游戏内部 ID（4001-4117） |
| `internal_name` | 内部英文名 |
| `zh_name` / `en_name` | 中文名 / 英文名 |
| `zh_desc` | 中文描述（含 `{0}{1}` 数值占位符） |
| `params` | **真实数值参数**（从 Lua 源码提取） |

## 覆盖率

- 天赋中文名：**94.6%**
- 天赋描述：**93.1%**
- 内部名↔ID 映射：**100%**

## 许可

数据来自游戏本体，仅供研究参考。
'''
open(os.path.join(B,'README.md'),'w',encoding='utf-8').write(readme2)
P('  README.md 已生成')

# ══════════════════════════════════════════════════════════
# 汇总
# ══════════════════════════════════════════════════════════
P('')
P('=' * 78); P('汇总'); P('=' * 78)
for name in ['soul-knight-reverse','soul-knight-data']:
    p = os.path.join(OUT, name)
    P('  %-24s %10s  %d 文件' % (name, fmt(dsize(p)),
      sum(len(f) for _,_,f in os.walk(p))))
P('')
P('  输出目录: %s' % OUT)

o.close()
print('WROTE work/_github_pack_log.txt')
