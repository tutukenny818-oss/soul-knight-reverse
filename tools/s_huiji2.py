#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_huiji2.py —— ★★★ 用 curl_cffi 绕过 Cloudflare 接灰机 wiki

═══════════════════════════════════════════════════════════════════════
 为什么用 curl_cffi
═══════════════════════════════════════════════════════════════════════
 灰机（yqqs.huijiwiki.com）挂了 Cloudflare，普通 requests/urllib 被 403：
     HTTP 403 "Enable JavaScript and cookies to continue"

 但 Cloudflare 的免费版主要靠 **TLS 指纹（JA3）** 识别机器人。
 `curl_cffi` 用 curl-impersonate 复刻真实浏览器的 TLS 握手，
 所以能拿到 200 —— **这是最轻量的方案**（不需要开浏览器）。

 环境已确认：curl_cffi 0.16.3 已安装。

═══════════════════════════════════════════════════════════════════════
 灰机的 Data: 命名空间（ns=3500）—— 结构化游戏数据
═══════════════════════════════════════════════════════════════════════
 每页就是一个 JSON，字段与游戏配置表一一对应：
   https://yqqs.huijiwiki.com/index.php?title=Data:玄冥.json&action=raw
   → {"NameEN":"Xuan Ming","Type":"机枪",...,"data_type":"weapon"}

 data_type 分类：weapon / weapon_dxt / character / boss / monster
 字段（见 Project:Json数据）：
   weapon     : ArticlePage,Name,NameEN,Type,SubType,Quality,Attack,
                Energy,Critical,Offset,Cellar,isForgeable,isRandom,
                Iron/Gear/Battery/Wood/Grain/Fertilize/Magic*
   character  : ArticlePage,NameEN,InitHealth,InitArmor,InitEnergy,
                Critical,BHDmg,Buff,InitWeapon,InitWeaponU,
                CharacterCellar,isHero
   boss       : ArticlePage,NameEn,BossLife,Biome,Floor,RedWeapon,BossId
   monster    : ArticlePage,NameEn,Health,EliteHealth,Biome,Level,
                Weight,GoldenFrame,MonsterId

 ★ 这些字段名与游戏侧数据**高度可对照**：
      wiki InitHealth ⨯ characters.csv Hp
      wiki Attack     ⨯ weapons 表（在 luban）
      wiki BossLife   ⨯ enemies.csv Hp

用法：
   python tools\\s_huiji2.py --test            # 测能不能通
   python tools\\s_huiji2.py --list            # 枚举全部 Data 页
   python tools\\s_huiji2.py --pull            # 全量下载
   python tools\\s_huiji2.py --stats           # 统计
"""
import argparse
import json
import os
import sys
import time

sys.stdout.reconfigure(encoding='utf-8', errors='replace')

W = r'D:\元气骑士mod'
OUT = os.path.join(W, 'work', 'wiki_huiji')
BASE = 'https://yqqs.huijiwiki.com'

# 浏览器指纹（curl_cffi 支持的名字）
IMPERSONATE = ['chrome', 'chrome131', 'chrome124', 'chrome120',
               'chrome116', 'edge101', 'firefox133', 'safari17_0']


def get_session(imp):
    from curl_cffi import requests as creq
    s = creq.Session(impersonate=imp)
    s.headers.update({
        'Accept': 'application/json, text/html, */*;q=0.9',
        'Accept-Language': 'zh-CN,zh;q=0.9,en;q=0.8',
        'Referer': BASE + '/',
    })
    return s


def test():
    print('=' * 92)
    print(' 测试各浏览器指纹能否绕过 Cloudflare')
    print('=' * 92)
    url = BASE + '/api.php?action=query&meta=siteinfo&format=json'
    ok = None
    for imp in IMPERSONATE:
        try:
            s = get_session(imp)
            r = s.get(url, timeout=30)
            mark = '✓' if r.status_code == 200 else '✗'
            print('  %s %-14s HTTP %s  %d B' % (mark, imp, r.status_code,
                                                  len(r.content)))
            if r.status_code == 200 and ok is None:
                ok = imp
                try:
                    j = r.json()
                    print('       sitename=%r'
                          % j.get('query', {}).get('general', {}).get(
                              'sitename'))
                except Exception:
                    pass
        except Exception as e:
            print('  ✗ %-14s 异常 %s' % (imp, str(e)[:70]))
    print()
    if ok:
        print('  ★ 可用指纹: %s' % ok)
    else:
        print('  ✗ 全部失败 —— 需要换方案（浏览器 / DrissionPage）')
    return ok


def api(s, params):
    import urllib.parse
    qs = '&'.join('%s=%s' % (k, urllib.parse.quote(str(v)))
                  for k, v in params.items())
    r = s.get('%s/api.php?%s' % (BASE, qs), timeout=45)
    if r.status_code != 200:
        return None, 'HTTP %s' % r.status_code
    try:
        return r.json(), None
    except Exception as e:
        return None, str(e)[:80]


def list_data(imp):
    os.makedirs(OUT, exist_ok=True)
    s = get_session(imp)
    print('=' * 92)
    print(' 枚举 Data: 命名空间（ns=3500）')
    print('=' * 92)
    titles = []
    cont = None
    for i in range(60):
        p = {'action': 'query', 'list': 'allpages', 'apnamespace': '3500',
             'aplimit': '500', 'format': 'json', 'formatversion': '2'}
        if cont:
            p['apcontinue'] = cont
        j, err = api(s, p)
        if err:
            print('  ✗ %s' % err)
            break
        pgs = j.get('query', {}).get('allpages', [])
        for x in pgs:
            titles.append(x['title'])
        cont = j.get('continue', {}).get('apcontinue')
        print('     已取 %d…' % len(titles))
        if not cont:
            break
        time.sleep(0.3)
    if titles:
        p = os.path.join(OUT, 'data_titles.txt')
        open(p, 'w', encoding='utf-8').write('\n'.join(titles))
        print('  ✓ 共 %d 条 → %s' % (len(titles), p))
        print()
        print('  ▸ 前 30 条:')
        for t in titles[:30]:
            print('     %s' % t)
    return titles


def pull(imp, titles, limit=None):
    os.makedirs(OUT, exist_ok=True)
    if limit:
        titles = titles[:limit]
    s = get_session(imp)
    print()
    print('=' * 92)
    print(' 下载 %d 个 Data 页 JSON' % len(titles))
    print('=' * 92)
    ok = fail = 0
    for i, t in enumerate(titles):
        safe = ''.join(c if c.isalnum() or c in '-_().（）' else '_'
                       for c in t)[:120]
        p = os.path.join(OUT, safe)
        if os.path.exists(p) and os.path.getsize(p) > 2:
            ok += 1
            continue
        import urllib.parse
        u = '%s/index.php?title=%s&action=raw' % (
            BASE, urllib.parse.quote(t.replace(' ', '_')))
        try:
            r = s.get(u, timeout=45)
            if r.status_code == 200 and r.content[:1] in (b'{', b'['):
                open(p, 'wb').write(r.content)
                ok += 1
            else:
                fail += 1
        except Exception:
            fail += 1
        if (i + 1) % 50 == 0:
            print('     %d/%d ok=%d fail=%d' % (i + 1, len(titles), ok, fail))
        time.sleep(0.2)
    print('  ✓ ok=%d fail=%d' % (ok, fail))
    return ok


def stats():
    files = [f for f in os.listdir(OUT)
             if f != 'data_titles.txt'] if os.path.isdir(OUT) else []
    if not files:
        print('  还没下载')
        return
    types = {}
    js = 0
    samples = []
    for f in files:
        try:
            j = json.loads(open(os.path.join(OUT, f), encoding='utf-8').read())
        except Exception:
            continue
        js += 1
        dt = j.get('data_type', '?')
        types[dt] = types.get(dt, 0) + 1
        if len(samples) < 4:
            samples.append((f, j))
    print('  文件 %d，合法 JSON %d' % (len(files), js))
    print()
    print('  ▸ data_type 分布:')
    for k, v in sorted(types.items(), key=lambda x: -x[1]):
        print('     %-20s %5d' % (k, v))
    print()
    print('  ▸ 样本:')
    for f, j in samples:
        print('     %s' % f[:60])
        print('        %s' % json.dumps(j, ensure_ascii=False)[:240])


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--test', action='store_true')
    ap.add_argument('--list', action='store_true')
    ap.add_argument('--pull', action='store_true')
    ap.add_argument('--stats', action='store_true')
    ap.add_argument('--imp', default=None)
    ap.add_argument('--limit', type=int, default=None)
    a = ap.parse_args()

    imp = a.imp
    if a.test or not imp:
        imp = test() or 'chrome'
    if a.stats:
        stats()
        return 0
    tp = os.path.join(OUT, 'data_titles.txt')
    titles = None
    if os.path.exists(tp):
        titles = [x.strip() for x in
                  open(tp, encoding='utf-8').read().splitlines() if x.strip()]
    if a.list or not titles:
        titles = list_data(imp)
    if a.pull and titles:
        pull(imp, titles, a.limit)
        stats()
    elif titles:
        print()
        print('  已有 %d 条标题，加 --pull 下载' % len(titles))
    return 0


if __name__ == '__main__':
    sys.exit(main())
