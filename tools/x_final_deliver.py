#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_final_deliver.py —— ★ 终极交付：所有系统词条汇总 + wiki 投喂包"""
import sys, io, os, re, json, csv, collections
W = r'D:\元气骑士mod'
o = io.open(os.path.join(W, 'work', 'final_deliver_log.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

LI = json.load(open(os.path.join(W,'work','gamedata_out','localize_index.json'), encoding='utf-8'))
OUTD = os.path.join(W, 'work', 'WIKI_DELIVER')
os.makedirs(OUTD, exist_ok=True)

def esc(s):
    return (s or '').replace('|', '&#124;').replace('\n', ' ').replace('{', '{{').replace('}', '}}')

def mktable(title, rows, cols, header):
    w = ['{{游戏机制}}', '', '== %s ==' % title, '', '共 %d 条。' % len(rows), '']
    w.append('{| class="wikitable sortable" style="font-size:90%"')
    w.append('! ' + ' !! '.join(header))
    for r in rows:
        w.append('|-')
        w.append('| ' + ' || '.join(esc(str(r.get(c, ''))) for c in cols))
    w.append('|}')
    return '\n'.join(w)

# ============ 1. 天赋总表 ============
mp = os.path.join(W,'work','TALENT_FINAL_MASTER.csv')
rows = list(csv.DictReader(open(mp, encoding='utf-8-sig')))
txt = mktable('天赋一览表', rows,
              ['talent_id','internal_name','zh_name','en_name','zh_desc','levels','params'],
              ['ID','内部名','中文名','英文名','描述','等级数','数值参数'])
open(os.path.join(OUTD,'天赋一览表.wikitext'),'w',encoding='utf-8').write(txt)
P('  天赋一览表.wikitext  %d 条  %d 字符' % (len(rows), len(txt)))

# ============ 2. 天赋数值表 ============
rows_v = [r for r in rows if r['params']]
txt = mktable('天赋数值表', rows_v,
              ['talent_id','zh_name','levels','params'],
              ['ID','中文名','等级数','各等级参数'])
open(os.path.join(OUTD,'天赋数值表.wikitext'),'w',encoding='utf-8').write(txt)
P('  天赋数值表.wikitext  %d 条' % len(rows_v))

# ============ 3. 雇佣兵天赋 ============
tn = {}; td = {}
for k, v in LI.items():
    if k.startswith('Troop2/Talent/Name/'):
        tn[k.split('/')[-1]] = v
    elif k.startswith('Troop2/Talent/Desc/'):
        td[k.split('/')[-1]] = v
troop = []
for n in sorted(tn):
    troop.append({'id': n, 'zh_name': tn[n].get('zh-CN',''),
                  'en_name': tn[n].get('en-US',''),
                  'zh_desc': td.get(n, {}).get('zh-CN','')})
txt = mktable('雇佣兵天赋（Troop）', troop,
              ['id','zh_name','en_name','zh_desc'],
              ['内部ID','中文名','英文名','描述'])
open(os.path.join(OUTD,'雇佣兵天赋.wikitext'),'w',encoding='utf-8').write(txt)
P('  雇佣兵天赋.wikitext  %d 条' % len(troop))

# ============ 4. 旧版 Buff（挑战因子）============
bp = os.path.join(W,'work','BUFF_ID_TABLE.csv')
if os.path.isfile(bp):
    br = list(csv.DictReader(open(bp, encoding='utf-8-sig')))
    txt = mktable('挑战因子 / 旧版 Buff', br,
                  ['buff_id','zh_name','en_name','zh_info','zh_upgrade'],
                  ['ID','中文名','英文名','说明','升级效果'])
    open(os.path.join(OUTD,'挑战因子.wikitext'),'w',encoding='utf-8').write(txt)
    P('  挑战因子.wikitext  %d 条' % len(br))

# ============ 5. 按命名空间导出 ============
P('')
P('=' * 74); P('按命名空间导出'); P('=' * 74)
groups = collections.defaultdict(list)
for k, v in LI.items():
    top = k.split('/')[0] if '/' in k else '_root'
    groups[top].append((k, v))

big = sorted(groups.items(), key=lambda x: -len(x[1]))
for g, lst in big:
    if len(lst) < 20: continue
    safe = re.sub(r'[^A-Za-z0-9_]', '_', g)[:36]
    with open(os.path.join(OUTD, 'NS_%s.csv' % safe),'w',newline='',encoding='utf-8-sig') as f:
        w = csv.writer(f)
        w.writerow(['key','zh-CN','zh-TW','en-US','ja','ko','es','de','fr','ru'])
        for k, v in sorted(lst):
            w.writerow([k, v.get('zh-CN',''), v.get('zh-TW',''), v.get('en-US',''),
                        v.get('ja',''), v.get('ko',''), v.get('es',''),
                        v.get('de',''), v.get('fr',''), v.get('ru','')])
P('  导出 %d 个命名空间 CSV' % sum(1 for g, l in big if len(l) >= 20))

# ============ 6. 汇总说明 ============
readme = []
readme.append('# 灰机 wiki 投喂包')
readme.append('')
readme.append('本包由游戏数据文件完整解析生成，可直接用于灰机 wiki 编辑。')
readme.append('')
readme.append('## 目录说明')
readme.append('')
readme.append('| 文件 | 内容 | 条数 |')
readme.append('|---|---|---|')
readme.append('| `天赋一览表.wikitext` | 天赋完整表格（MediaWiki 格式，可直接粘贴） | %d |' % len(rows))
readme.append('| `天赋数值表.wikitext` | 天赋 1-5 级真实参数 | %d |' % len(rows_v))
readme.append('| `雇佣兵天赋.wikitext` | Troop2 雇佣兵天赋系统 | %d |' % len(troop))
readme.append('| `挑战因子.wikitext` | 挑战因子 / 旧版 Buff | %d |' % len(br) if os.path.isfile(bp) else '')
readme.append('| `NS_*.csv` | 按游戏系统分类的全量多语言词条 | — |')
readme.append('')
readme.append('## 数据来源')
readme.append('')
readme.append('1. `localize_index.json`（17,794 词条 × 15 语言）— 游戏完整本地化表')
readme.append('2. `cfg_buff_stack.json` — 天赋参数表（1-5 级）')
readme.append('3. `talent_id_clean.json` — 内存提取的内部名↔ID 映射')
readme.append('4. `ecb_layers.json` — 天赋继承层级（从加密块结构推导）')
readme.append('5. `BUFF_XREF.csv` — 旧版/新版 ID 交叉映射')
readme.append('')
readme.append('## 数据可信度')
readme.append('')
readme.append('- **中文名 / 描述**：100%% 来自游戏自带本地化表，与游戏内显示一致')
readme.append('- **天赋数值**：来自游戏 `cfg_buff_stack.json`，为运行时真实参数')
readme.append('- **内部名↔ID**：来自游戏内存明文，已交叉验证')
readme.append('- **继承层级**：由加密块结构推导（ECB 特性），逻辑自洽')
readme.append('')
readme.append('## 已知缺口')
readme.append('')
readme.append('- 1xxx/2xxx 段（挑战因子）的 %d 个条目缺中文名' % sum(1 for r in rows if not r['zh_name']))
readme.append('- 天赋数值仅 %d/%d 条（其余天赋参数在未解密文件中）' % (len(rows_v), len(rows)))
open(os.path.join(OUTD,'README.md'),'w',encoding='utf-8').write('\n'.join(readme))
P('')
P('WROTE work/WIKI_DELIVER/README.md')

# 统计
P('')
P('=' * 74); P('投喂包汇总'); P('=' * 74)
tot = 0
for f in sorted(os.listdir(OUTD)):
    sz = os.path.getsize(os.path.join(OUTD, f))
    tot += sz
    P('  %-44s %10d B' % (f, sz))
P('  ─────────────────────────────────────────────────')
P('  总计 %d 个文件, %.1f MB' % (len(os.listdir(OUTD)), tot/1048576))

o.close()
print('WROTE work/final_deliver_log.txt + work/WIKI_DELIVER/')
