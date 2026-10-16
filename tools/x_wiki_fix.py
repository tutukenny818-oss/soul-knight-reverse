#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_wiki_fix.py —— 修正 wikitext 的 {0} 转义问题
   MediaWiki 里单个 { } 是安全的，只有 {{ }} 会被当模板
   所以 {0} 不需要转义成 {{0}}
"""
import sys, io, os, re, json, csv
W = r'D:\元气骑士mod'
OUTD = os.path.join(W, 'work', 'WIKI_DELIVER')
o = io.open(os.path.join(W, 'work', 'wiki_fix_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

def esc(s):
    """只转义管道符和换行；保留单个花括号"""
    return (s or '').replace('|', '&#124;').replace('\n', ' ')

def mktable(title, rows, cols, header, note=''):
    w = ['{{游戏机制}}', '']
    if title:
        w.append('== %s ==' % title)
        w.append('')
    w.append('共 %d 条。%s' % (len(rows), note))
    w.append('')
    w.append('{| class="wikitable sortable" style="font-size:90%"')
    w.append('! ' + ' !! '.join(header))
    for r in rows:
        w.append('|-')
        w.append('| ' + ' || '.join(esc(str(r.get(c, ''))) for c in cols))
    w.append('|}')
    return '\n'.join(w)

# ---- 1. 天赋一览表 ----
mp = os.path.join(W,'work','TALENT_FINAL_MASTER.csv')
rows = list(csv.DictReader(open(mp, encoding='utf-8-sig')))
txt = mktable('天赋一览表', rows,
              ['talent_id','internal_name','zh_name','en_name','zh_desc','levels','params'],
              ['ID','内部名','中文名','英文名','描述','等级数','数值参数'],
              '数值参数来自游戏 Lua 源码，为运行时真实值。')
open(os.path.join(OUTD,'天赋一览表.wikitext'),'w',encoding='utf-8').write(txt)
P('天赋一览表.wikitext  %d 条  %d 字符' % (len(rows), len(txt)))

# ---- 2. 天赋数值表 ----
rv = [r for r in rows if r['params']]
txt = mktable('天赋数值表', rv,
              ['talent_id','zh_name','levels','params'],
              ['ID','中文名','等级数','各等级参数'],
              '参数为游戏内 1-N 级的真实配置值。')
open(os.path.join(OUTD,'天赋数值表.wikitext'),'w',encoding='utf-8').write(txt)
P('天赋数值表.wikitext  %d 条' % len(rv))

# ---- 3. 雇佣兵天赋 ----
LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
tn = {}; td = {}
for k, v in LI.items():
    if k.startswith('Troop2/Talent/Name/'): tn[k.split('/')[-1]] = v
    elif k.startswith('Troop2/Talent/Desc/'): td[k.split('/')[-1]] = v
troop = []
for n in sorted(tn):
    troop.append({'id': n, 'zh_name': tn[n].get('zh-CN',''),
                  'en_name': tn[n].get('en-US',''),
                  'zh_desc': td.get(n, {}).get('zh-CN','')})
txt = mktable('雇佣兵天赋（Troop）', troop,
              ['id','zh_name','en_name','zh_desc'],
              ['内部ID','中文名','英文名','描述'])
open(os.path.join(OUTD,'雇佣兵天赋.wikitext'),'w',encoding='utf-8').write(txt)
P('雇佣兵天赋.wikitext  %d 条' % len(troop))

# ---- 4. 挑战因子 ----
bp = os.path.join(W,'work','BUFF_ID_TABLE.csv')
if os.path.isfile(bp):
    br = list(csv.DictReader(open(bp, encoding='utf-8-sig')))
    txt = mktable('挑战因子 / 旧版 Buff', br,
                  ['buff_id','zh_name','en_name','zh_info','zh_upgrade'],
                  ['ID','中文名','英文名','说明','升级效果'])
    open(os.path.join(OUTD,'挑战因子.wikitext'),'w',encoding='utf-8').write(txt)
    P('挑战因子.wikitext  %d 条' % len(br))

# ---- 5. ★ 新增：天赋真实数值（Lua 源码）----
lua_p = os.path.join(W,'work','LUA_PARAMS_EXTRA.csv')
if os.path.isfile(lua_p):
    lr = list(csv.DictReader(open(lua_p, encoding='utf-8-sig')))
    w = ['{{游戏机制|天赋}}', '', '== 天赋真实数值（Lua 源码级） ==', '',
         '以下数值直接来自游戏 Lua 源码 `app/buff/<Name>` 的 `on_add` 参数表，'
         '为运行时真实配置值。', '',
         '{| class="wikitable sortable" style="font-size:90%"']
    allk = set()
    for r in lr: allk |= set(r.keys())
    cols = ['module'] + sorted(k for k in allk if k != 'module')
    w.append('! ' + ' !! '.join(['模块'] + cols[1:]))
    for r in lr:
        w.append('|-')
        w.append('| ' + ' || '.join(esc(str(r.get(c, ''))) for c in cols))
    w.append('|}')
    txt = '\n'.join(w)
    open(os.path.join(OUTD,'天赋真实数值.wikitext'),'w',encoding='utf-8').write(txt)
    P('天赋真实数值.wikitext  %d 条' % len(lr))

# ---- 6. 天赋继承体系 ----
layers = json.load(open(os.path.join(W,'work','ecb_layers.json'), encoding='utf-8'))
w = ['{{游戏机制|天赋}}', '', '== 天赋代码继承体系 ==', '',
     '游戏的天赋模块（`app/talent/*.bytes`，共 118 个）基于统一的 Lua 基类。',
     '按代码复用程度分为 4 层，层级由模块前部的共享代码段推导。', '',
     '{| class="wikitable"', '! 层级 !! 说明 !! 成员数 !! 成员']
SHOWN = set()
for off_s in sorted(layers, key=lambda x: int(x)):
    off = int(off_s)
    if off < 56 or off % 16 != 0: continue
    v = layers[off_s]
    lvl = 'L1（一层特化）' if off < 72 else ('L2（二层特化）' if off < 88 else 'L3（三层特化）')
    if lvl in SHOWN: continue
    SHOWN.add(lvl)
    ms = sorted(v.get('members', []))
    if not ms: continue
    w.append('|-')
    w.append('| %s || 共享偏移 %d 处的代码块 || %d || %s' %
             (lvl, off, len(ms), '、'.join('<code>%s</code>' % m for m in ms[:20])))
w.append('|}')
w.append('')
w.append('其余 107 个天赋直接继承基类，无额外特化层。')
txt = '\n'.join(w)
open(os.path.join(OUTD,'天赋继承体系.wikitext'),'w',encoding='utf-8').write(txt)
P('天赋继承体系.wikitext')

# ---- 7. 热更新机制 ----
w = ['{{游戏机制}}', '', '== 热更新机制 ==', '',
     '游戏使用 XLua（Lua 5.3）实现机制热更新，核心机制为 `xlua.hotfix`。', '',
     '=== 模块清单 ===', '',
     '{| class="wikitable sortable"', '! 类别 !! 数量 !! 说明']
man = json.load(open(os.path.join(W,'work','runtime_lua_manifest.json'), encoding='utf-8'))
for k, desc in [('buff','Buff 实现（状态类机制）'),
                ('talent','天赋挂载点'),
                ('common','机制公共库'),
                ('localization','本地化')]:
    w.append('|-')
    w.append('| <code>app/%s/</code> || %d || %s' % (k, len(man.get(k, [])), desc))
w.append('|}')
w.append('')
w.append('=== xlua.hotfix 机制 ===')
w.append('')
w.append('```lua')
w.append('xlua.hotfix = function(cs, field, func)')
w.append('    -- 用 __Hotfix0_<method> 覆写 C# 方法')
w.append('    -- 支持 __Hotfix1..99 重载槽')
w.append('end')
w.append('```')
w.append('')
w.append('=== 天赋模块结构 ===')
w.append('')
w.append('```lua')
w.append("local TalentBase = require 'app.talent.TalentBase'")
w.append('local <Name> = TalentBase.create(CS.emBuff.<Name>)')
w.append("local Buffs = require 'app.buff.init'")
w.append('')
w.append('function <Name>.on_add(player)')
w.append('    Buffs.add(player, CS.emBuff.<Name>, "app.buff.<Name>", {')
w.append('        events = { "..." },')
w.append('        <数值参数> = <值>,')
w.append('        <资源路径> = "RGPrefab/..."')
w.append('    })')
w.append('end')
w.append('```')
txt = '\n'.join(w)
open(os.path.join(OUTD,'热更新机制.wikitext'),'w',encoding='utf-8').write(txt)
P('热更新机制.wikitext')

# ---- 8. README ----
rd = ['# 灰机 wiki 投喂包（最终版）', '',
      '由游戏数据文件完整解析生成，可直接用于灰机 wiki 编辑。', '',
      '## 主交付件', '',
      '| 文件 | 内容 | 条数 |', '|---|---|---|',
      '| `天赋一览表.wikitext` | 天赋完整表格（MediaWiki 格式，可直接粘贴） | %d |' % len(rows),
      '| `天赋数值表.wikitext` | 天赋 1-N 级参数 | %d |' % len(rv),
      '| `天赋真实数值.wikitext` | **Lua 源码级真实参数** | %d |' % (len(lr) if os.path.isfile(lua_p) else 0),
      '| `天赋继承体系.wikitext` | 4 层继承树 | 11 |',
      '| `雇佣兵天赋.wikitext` | Troop2 雇佣兵天赋 | %d |' % len(troop),
      '| `挑战因子.wikitext` | 挑战因子 / 旧版 Buff | %d |' % (len(br) if os.path.isfile(bp) else 0),
      '| `热更新机制.wikitext` | XLua 机制说明 | — |',
      '| `NS_*.csv` | 67 个游戏系统分类词条 | 17,794 |', '',
      '## 数据来源与可信度', '',
      '| 数据 | 来源 | 可信度 |', '|---|---|---|',
      '| 中文名/描述 | 游戏 `localize_index.json`（明文） | ★★★★★ |',
      '| 天赋数值（表） | 游戏 `cfg_buff_stack.json`（明文） | ★★★★★ |',
      '| 天赋真实数值（Lua） | 游戏内存 UTF-16LE 源码 | ★★★★★ |',
      '| 内部名↔ID | 游戏进程内存明文 | ★★★★★ |',
      '| 继承层级 | 加密块结构推导 | ★★★★☆ |', '',
      '## 附：其他数据（在上级目录）', '',
      '- `LOCALIZE_ALL.csv` — 全量 17794 词条 × 15 语言（15.3 MB）',
      '- `TALENT_FINAL_MASTER.csv` — 天赋总表 202 × 13 列',
      '- `LUA_PARAMS_EXTRA.csv` — 天赋真实数值',
      '- `TALENT_VALUES_FLAT.csv` — 天赋数值长表',
      '- `ecb_layers.json` — 天赋继承层级',
      '- `work/lua_utf16/` — 7 个模块完整 Lua 源码']
open(os.path.join(OUTD,'README.md'),'w',encoding='utf-8').write('\n'.join(rd))
P('README.md')

# ---- 汇总 ----
P('')
P('=' * 74); P('投喂包最终清单'); P('=' * 74)
tot = 0
for f in sorted(os.listdir(OUTD)):
    sz = os.path.getsize(os.path.join(OUTD, f))
    tot += sz
    if f.endswith('.wikitext') or f == 'README.md':
        P('  %-46s %10d B' % (f, sz))
P('  ---')
P('  总计 %d 个文件, %.1f MB' % (len(os.listdir(OUTD)), tot/1048576))

o.close()
print('WROTE work/wiki_fix_log.txt')
