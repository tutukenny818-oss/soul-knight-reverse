#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_cleanup_plan.py —— 生成清理方案（不删，只出清单）"""
import sys, io, os, json, collections, glob
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', '_cleanup_plan.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()
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
# 定义：保留 / 删除
# ══════════════════════════════════════════════════════════

# ★★★ 核心资产 —— 绝对保留
KEEP_CORE = [
 # 解密产物（重建一切的基础）
 'work/deace',                    # 解密后 .so + metadata（278MB + 55MB）
 'decompiled',                    # 原始 APK 解包 + dump.cs
 'work/cfgab',                    # 18 张配置表明文
 'work/dump_full',                # dump.cs
 # 最终交付
 'work/WIKI_DELIVER',             # ★ wiki 投喂包
 'work/LOCALIZE_ALL.csv',
 'work/TALENT_FINAL_MASTER.csv',
 'work/TALENT_VALUES.csv',
 'work/TALENT_VALUES_FLAT.csv',
 'work/TALENT_IDS.csv',
 'work/TROOP_TALENTS.csv',
 'work/BUFF_ID_TABLE.csv',
 'work/BUFF_FAMILY_V2.csv',
 'work/BUFF_XREF.csv',
 'work/OLD_BUFF_FULL.csv',
 'work/BUF_FAMILY.csv',
 'work/ecb_layers.json',
 'work/codebook_v2.json',
 'work/CBC_ORACLES_V2.json',
 'work/lua_utf16',                # ★ 7 个天赋完整源码
 'work/lua_extra',
 'work/lua_add_params',
 'work/LUA_PARAMS_EXTRA.csv',
 # 数据湖
 'work/gamedata_out',             # 武器/技能/角色/本地化/资产索引
 'work/wiki_mech',                # 55 页机制 wikitext + MECH_MAP
 'work/wiki_huiji',               # 灰机 835 JSON
 'work/wiki_cmp',                 # wiki 校验
 'work/save',                     # 存档（可用）
 'work/vcfg',                     # 明文配置 JSON
 # 关键报告
 'work/S_MASTER_REPORT.md',
 'work/S_DAMAGE_FORMULA.md',
 'work/S_DAMAGE_ORDER_VS_WIKI.md',
 'work/S_CRIT_SIMPLE.md',
 'work/S_HEADER_CRACKED.md',
 'work/S_CBC_CONFIRMED.md',
 'work/S_WIKI_DELIVERY.md',
 'work/RESEARCH_RUNTIME_LUA.md',
 'work/HANDOFF.md',
 'work/S_HANDOFF.md',
 'work/DELIVERABLES.md',
 'HANDOFF.md',
 # 核心工具
 'tools',
 # 原始 APK（只留 1 个）
 '5570618_20260905032720_Soul_Knight_release_chill.apk',
 'mod/gl_signed.apk',
 # Florida 构建环境（保留，可能还要用）
 'work/fs86_orig',
 'work/fs86_min.bin',
 'work/fs86_patched',
 # 待研究
 'work/patch_luban',
 'work/luban_out',
 'work/lua_device_src',           # 253 个密文（要重解）
 'work/lua_decoded2',
 'work/cfg',
 'work/gamedata',
 'work/known_plaintext',
 'work/dup_detect_log.txt',
]

# ★ 已知可删 —— 重复 APK（6.53 GB）
DEL_DUPE_APK = [
 'work/mod_build_char/base.cheat.apk',
 'work/mod_build_char/base.mod.apk',
 'work/mod_build_char/base.mod2.apk',
 'work/mod_build_char/v10/base.gold.apk',
 'work/mod_build_char/v11/base.golden_knight.apk',
 'work/mod_build_char/v6/base.icon.apk',
 'work/mod_build_char/v8/base.verify.apk',
 'work/skmod_out/batch_gold_3chars.apk',
 'work/mod_build_char/v13/newskin_knight_38.apk',
]

# ★ 可删 —— 内存 dump 原料（可随时重新生成，共约 4.5 GB）
DEL_MEMDUMP = [
 'work/ldmem3',      # 1.5 GB   1178 段
 'work/dg2',         # 729 MB
 'work/keyscan',     # 597 MB   （已提取完 7 个天赋）
 'work/td',          # 487 MB
 'work/memdump',     # 300 MB
 'work/lua_mem',     # 137 MB
 'work/livedump',    # 36 MB
 'work/luban_mem',   # 51 MB
 'work/_dl',         # 21 MB
 'work/_scan',
 'work/_abm',
 'work/lua_extract',
 'work/abtest',
 'work/ta_probe',
 'work/lctest',
 'work/lua_live_src',
 'work/runtime_lua',
 'work/lua_plain',
 'work/_test_lua_export',
 'work/lua_ab',
 'work/lua_dec_raw',
 'work/x_final',
 'work/lua_decoded',
 'work/lua_src_recovered',
 'work/module_text',
 'work/_re_probe',
 'work/_kt',
]

# ★ 可删 —— 中间产物 / 历史版本
DEL_INTERMEDIATE = [
 'work/mod_build_char/v2','work/mod_build_char/v5','work/mod_build_char/v6',
 'work/mod_build_char/v7','work/mod_build_char/v8','work/mod_build_char/v10',
 'work/mod_build_char/v11','work/mod_build_char/v12','work/mod_build_char/v13',
 'work/mod_build_char/v14','work/mod_build_char/_devz','work/mod_build_char/_save',
 'work/mod_build_char/hero',
 'work/skmod_out',
 'work/modkit',            # 500 MB（mask.bin 可从 s_meta3.py 重生成）
 'work/gamedata_emu',
 'work/emu','work/emu_save','work/config_ab_out','work/config_ab_plain',
 'work/_uabea','work/abx','work/ecb2','work/bench',
 'work/dumper','work/_mod','work/_rt','work/x_dec_assets',
 'work/mod_build',        # 旧 mod 管线（已被 mod_build_char 取代）
 'work/amod','work/forensic','work/EVIDENCE','work/handbook',
 'work/ldmem3',
]

P('=' * 78); P('清理方案'); P('=' * 78)
P('')

# 统计
def sumlist(lst):
    t = 0; exist = []
    for x in lst:
        p = os.path.join(W, x)
        if os.path.exists(p):
            s = dsize(p); t += s; exist.append((s, x))
    return t, exist

kd, kex = sumlist(KEEP_CORE)
P('★ 保留清单: %d 项, %s' % (len(kex), fmt(kd)))
P('')

P('=' * 78); P('可删除'); P('=' * 78)
total_del = 0
SECTIONS = [
 ('① 重复 APK（同一份的副本）', DEL_DUPE_APK),
 ('② 内存 dump 原料（可重新生成）', DEL_MEMDUMP),
 ('③ 中间产物 / 历史版本', DEL_INTERMEDIATE),
]
for title, lst in SECTIONS:
    s, ex = sumlist(lst)
    total_del += s
    P('')
    P('%s  —— %d 项, %s' % (title, len(ex), fmt(s)))
    for sz, x in sorted(ex, reverse=True)[:40]:
        P('     %10s  %s' % (fmt(sz), x))

P('')
P('=' * 78)
P('  合计可删: %s' % fmt(total_del))
P('  保留核心: %s' % fmt(kd))
P('  工作区总计: %s' % fmt(dsize(W)))
P('=' * 78)

# 保存清单
json.dump({'keep': KEEP_CORE,
           'del_dupe_apk': DEL_DUPE_APK,
           'del_memdump': DEL_MEMDUMP,
           'del_intermediate': DEL_INTERMEDIATE,
           'total_del_bytes': total_del,
           'total_keep_bytes': kd},
          open(os.path.join(W,'work','_cleanup_plan.json'),'w',encoding='utf-8'),
          ensure_ascii=False, indent=1)
P('')
P('  WROTE work/_cleanup_plan.json')

o.close()
print('WROTE work/_cleanup_plan.txt')
