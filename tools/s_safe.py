#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_safe.py —— 【★ 安全文字 MOD】带「安全性判定」的替换器

═══════════════════════════════════════════════════════════════════════
 为什么要「安全判定」
═══════════════════════════════════════════════════════════════════════
 上一轮踩的坑：
     把 '退出' 改成 '溜了'  →  游戏报「读取用户数据失败，请尝试重试」

 原因：metadata 的 `stringLiteral` 段里，字符串**混用了两种用途**：
     (A) 界面显示文本        → 改了只影响显示 ✓ 安全
     (B) 序列化数据的键名/标识符 → 改了破坏数据读写  ✗ 危险

 判定规则（本脚本实现）：
   ✗ 长度 < 6 字节            太短，极可能是标识符
   ✗ 只含 ASCII 字母数字下划线 像标识符
   ✗ 首字符是 `,`/`/`/`_`/`-` 典型的键名/路径前缀
   ✗ 出现在 `字段名列表` 形态里（`A:,B:,C:`） 序列化 schema
   ✗ 含 `{0}` `{1}` 之外的格式符 可能是代码模板
   ✓ 含中文 + 中文标点 + 长度 >= 12 字节   界面文本
   ✓ 含 UI 词汇（提示/确认/取消/点击/恭喜…）

═══════════════════════════════════════════════════════════════════════
 用法
═══════════════════════════════════════════════════════════════════════
  python tools\\s_safe.py --find                       # 列出安全可改的候选
  python tools\\s_safe.py --find --ui                   # 只看 UI 词汇类
  python tools\\s_safe.py --check "某段文字"            # 判定某条是否安全
  python tools\\s_safe.py --apply "原文" "新文"          # 改一条（等长校验）
  python tools\\s_safe.py --restore                    # 恢复原版
"""
import argparse
import os
import re
import struct
import sys

sys.path.insert(0, os.path.join(r'D:\元气骑士mod', 'tools'))
sys.stdout.reconfigure(encoding='utf-8', errors='replace')

from s_meta3 import dec, enc, load_mask, CIPHER
from s_mod2 import deploy

W = r'D:\元气骑士mod'
STORE = os.path.join(W, 'work', 'modkit')
ORIG = os.path.join(STORE, 'global-metadata.orig.dat')
SL_OFF, SL_SZ = 256, 614512
SLD_OFF = 614768

UIWORDS = ('提示', '确认', '取消', '点击', '恭喜', '失败', '成功', '请',
           '无法', '需要', '完成', '获得', '解锁', '开启', '关闭', '选择',
           '继续', '返回', '重新', '正在', '已经', '是否', '可以')

BAD_PREFIX = (',', '/', '_', '-', '[', '?', '{', '}', '<', '>', '.', '!')


def is_safe(t):
    """返回 (是否安全, 原因)"""
    b = t.encode('utf-8')
    if len(b) < 12:
        return False, '太短(<12B)'
    if t[0] in BAD_PREFIX:
        return False, '首字符像标识符/路径 (0x%02X)' % b[0]
    # 纯 ASCII
    if all(0x20 <= ord(c) < 0x7f for c in t):
        return False, '纯 ASCII（像标识符）'
    # 无中文
    if not re.search(r'[\u4e00-\u9fff]', t):
        return False, '无中文'
    # 字段名列表形态 A:,B:,C:
    if re.match(r'^[\w]+:\s*(,[\w]+:)+', t):
        return False, '字段名列表(schema)'
    # 含大量英文标识符
    ascii_ident = re.findall(r'[A-Za-z_][A-Za-z0-9_]{2,}', t)
    if len(ascii_ident) >= 3:
        return False, '含 %d 个英文标识符' % len(ascii_ident)
    # 看起来像代码/模板且有英文
    if re.search(r'[A-Za-z]{4,}', t) and not re.search(r'[。，！？]', t):
        return False, '含英文单词但无中文标点'
    return True, 'OK'


def literals(p):
    out = []
    n = SL_SZ // 8
    for i in range(n):
        a = SL_OFF + i * 8
        ln, di = struct.unpack_from('<ii', p, a)
        wa = SLD_OFF + di
        if ln <= 0 or ln > 6000 or wa + ln > len(p):
            continue
        try:
            t = p[wa:wa + ln].decode('utf-8')
        except Exception:
            continue
        out.append((i, a, ln, wa, t))
    return out


def find(ui_only=False, limit=80):
    p = dec(open(CIPHER, 'rb').read(), load_mask())
    ls = literals(p)
    ok = []
    for i, a, ln, wa, t in ls:
        s, why = is_safe(t)
        if not s:
            continue
        if ui_only and not any(w in t for w in UIWORDS):
            continue
        ok.append((i, a, ln, wa, t))
    print()
    print('=' * 96)
    print(' 安全的可改字符串：%d / %d 条' % (len(ok), len(ls)))
    print('=' * 96)
    for i, a, ln, wa, t in ok[:limit]:
        print('  [%6d] %4dB @0x%-8X %s' % (i, ln, wa, t[:72]))
    if len(ok) > limit:
        print('  …还有 %d 条' % (len(ok) - limit))
    return ok


def check(t):
    s, why = is_safe(t)
    print('  %r' % t)
    print('     %s  %s' % ('✓ 安全' if s else '✗ 危险', why))
    p = dec(open(CIPHER, 'rb').read(), load_mask())
    hits = [x for x in literals(p) if x[4] == t]
    print('     出现 %d 次' % len(hits))
    for i, a, ln, wa, t2 in hits[:6]:
        print('        [%d] 表@0x%X 数据@0x%X len=%d' % (i, a, wa, ln))
    return hits


def build(pairs, dry=False):
    """pairs = [(old, new), ...]，全部必须 UTF-8 等长且安全"""
    c = open(CIPHER, 'rb').read()
    m = load_mask()
    p = bytearray(dec(c, m))
    ls = literals(bytes(p))
    idx = {}
    for i, a, ln, wa, t in ls:
        idx.setdefault(t, []).append((a, ln, wa))
    print()
    print('=' * 96)
    print(' 构建安全文字 MOD')
    print('=' * 96)
    rep = []
    for old, new in pairs:
        ob, nb = old.encode('utf-8'), new.encode('utf-8')
        s, why = is_safe(old)
        print()
        print('  %r → %r' % (old, new))
        print('     原串安全性: %s (%s)' % ('✓' if s else '✗ 危险!', why))
        if len(ob) != len(nb):
            print('     ✗ 长度不符 %d vs %d' % (len(ob), len(nb)))
            continue
        if old not in idx:
            print('     ✗ 未找到')
            continue
        cnt = 0
        for a, ln, wa in idx[old]:
            if bytes(p[wa:wa + ln]) != ob:
                continue
            p[wa:wa + ln] = nb
            rep.append((old, new, wa))
            cnt += 1
        print('     替换 %d 处' % cnt)
    if not rep:
        print()
        print('  无有效替换')
        return None
    nc = enc(bytes(p), m)
    nd = sum(1 for i in range(len(c)) if nc[i] != c[i])
    out = os.path.join(STORE, 'meta_safe.dat')
    open(out, 'wb').write(nc)
    print()
    print('  与原始密文差异 %d 字节' % nd)
    print('  已写 %s' % out)
    if dry:
        print('  （--dry：未部署）')
        return out
    deploy(out, '安全文字 mod')
    return out


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--find', action='store_true')
    ap.add_argument('--ui', action='store_true')
    ap.add_argument('--check', default=None)
    ap.add_argument('--apply', nargs=2, action='append',
                    metavar=('OLD', 'NEW'))
    ap.add_argument('--dry', action='store_true')
    ap.add_argument('--restore', action='store_true')
    ap.add_argument('--limit', type=int, default=80)
    a = ap.parse_args()
    if a.restore:
        deploy(ORIG, '原版')
        return 0
    if a.check:
        check(a.check)
        return 0
    if a.apply:
        build([tuple(x) for x in a.apply], a.dry)
        return 0
    find(a.ui, a.limit)
    return 0


if __name__ == '__main__':
    sys.exit(main())
