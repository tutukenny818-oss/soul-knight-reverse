#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
x_hj_mech.py v2 —— 灰机元气骑士维基「游戏机制」板块枚举与抓取
★ 关键：api.php 可以走通（chrome116/101/99/edge101/firefox133）。
  index.php 的 HTML 页面 403，所以一律走 API 拿 wikitext：
    action=query&prop=revisions&rvprop=content&rvslots=main&formatversion=2

用法:
  python tools\\x_hj_mech.py ns          # 列出所有命名空间
  python tools\\x_hj_mech.py scan        # 枚举主命名空间(0) + 各命名空间页数
  python tools\\x_hj_mech.py cats        # 抓全部页面的分类
  python tools\\x_hj_mech.py fetch <listfile>   # 按标题清单抓 wikitext
  python tools\\x_hj_mech.py one <title>        # 抓单页
"""
import json, os, sys, time, urllib.parse
sys.stdout.reconfigure(encoding='utf-8', errors='replace')

W = r'D:\元气骑士mod'
OUT = os.path.join(W, 'work', 'wiki_mech')
BASE = 'https://yqqs.huijiwiki.com'
IMP = 'chrome116'

from curl_cffi import requests as creq
S = creq.Session(impersonate=IMP)
S.headers.update({'Accept': 'application/json', 'Accept-Language': 'zh-CN,zh;q=0.9'})


def api(params, tries=3):
    qs = '&'.join('%s=%s' % (k, urllib.parse.quote(str(v))) for k, v in params.items())
    for _ in range(tries):
        try:
            r = S.get('%s/api.php?%s' % (BASE, qs), timeout=45)
            if r.status_code == 200 and r.content[:1] in (b'{', b'['):
                return r.json()
        except Exception:
            pass
        time.sleep(1.2)
    return None


def allpages(ns, limit=500):
    out, cont = [], None
    for _ in range(100):
        p = {'action': 'query', 'list': 'allpages', 'apnamespace': str(ns),
             'aplimit': str(limit), 'format': 'json', 'formatversion': '2'}
        if cont:
            p['apcontinue'] = cont
        j = api(p)
        if not j:
            break
        q = j.get('query', {})
        for x in q.get('allpages', []):
            out.append(x['title'])
        cont = j.get('continue', {}).get('apcontinue')
        if not cont:
            break
        time.sleep(0.15)
    return out


def wikitext(title):
    j = api({'action': 'query', 'prop': 'revisions', 'titles': title,
             'rvprop': 'content', 'rvslots': 'main',
             'format': 'json', 'formatversion': '2'})
    if not j:
        return None
    for pg in j.get('query', {}).get('pages', []):
        if 'missing' in pg:
            return None
        try:
            return pg['revisions'][0]['slots']['main']['content']
        except Exception:
            return None
    return None


def main():
    os.makedirs(OUT, exist_ok=True)
    mode = sys.argv[1] if len(sys.argv) > 1 else 'scan'

    if mode == 'ns':
        j = api({'action': 'query', 'meta': 'siteinfo',
                 'siprop': 'general|namespaces|statistics',
                 'format': 'json', 'formatversion': '2'})
        if not j:
            print('API FAIL'); return
        print('sitename =', j['query']['general'].get('sitename'))
        print('stats    =', json.dumps(j['query'].get('statistics', {}), ensure_ascii=False))
        for k, v in sorted(j['query']['namespaces'].items(), key=lambda x: int(x[0])):
            if int(k) < 0:
                continue
            print('  ns=%-6s %-24s canonical=%s' % (k, repr(v.get('name')), v.get('canonical')))

    elif mode == 'scan':
        nslist = [0, 4, 10, 12, 14, 3500]
        allmap = {}
        for ns in nslist:
            pg = allpages(ns)
            allmap[ns] = pg
            print('ns=%-6d pages=%d' % (ns, len(pg)))
            if ns == 0:
                for i in range(0, len(pg), 1000):
                    print('   ', ' | '.join(pg[i:i + 8])[:400])
        json.dump(allmap, open(os.path.join(OUT, 'ns_pages.json'), 'w', encoding='utf-8'),
                  ensure_ascii=False, indent=1)
        print('saved ->', os.path.join(OUT, 'ns_pages.json'))

    elif mode == 'cats':
        allmap = json.load(open(os.path.join(OUT, 'ns_pages.json'), encoding='utf-8'))
        pages = allmap.get('0', [])
        print('scanning categories of %d main pages' % len(pages))
        catmap = {}
        for i in range(0, len(pages), 50):
            chunk = pages[i:i + 50]
            j = api({'action': 'query', 'prop': 'categories', 'titles': '|'.join(chunk),
                     'cllimit': 'max', 'format': 'json', 'formatversion': '2'})
            if j:
                for pg in j.get('query', {}).get('pages', []):
                    for c in pg.get('categories', []):
                        catmap.setdefault(c['title'], []).append(pg.get('title'))
            if (i // 50) % 5 == 0:
                print('  %d/%d cats=%d' % (i, len(pages), len(catmap)))
            time.sleep(0.1)
        json.dump(catmap, open(os.path.join(OUT, 'cat_map.json'), 'w', encoding='utf-8'),
                  ensure_ascii=False, indent=1)
        print('分类数 =', len(catmap))
        for k, v in sorted(catmap.items(), key=lambda x: -len(x[1])):
            print('  %-44s %4d' % (k, len(v)))

    elif mode == 'fetch':
        lst = sys.argv[2]
        titles = [x.strip() for x in open(lst, encoding='utf-8').read().splitlines() if x.strip()]
        ok = 0
        for t in titles:
            safe = ''.join(c if c.isalnum() or c in '-_().（）' else '_' for c in t)[:120]
            p = os.path.join(OUT, safe + '.wiki')
            if os.path.exists(p) and os.path.getsize(p) > 10:
                ok += 1; continue
            txt = wikitext(t)
            if txt:
                open(p, 'w', encoding='utf-8').write(txt); ok += 1
            else:
                print('  ✗', t)
            time.sleep(0.15)
        print('fetched ok=%d / %d' % (ok, len(titles)))

    elif mode == 'one':
        t = sys.argv[2]
        txt = wikitext(t)
        print(txt if txt else 'FAILED')


if __name__ == '__main__':
    main()
