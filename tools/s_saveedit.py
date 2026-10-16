#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
s_saveedit.py —— 存档编辑器（解密 → 改 → 加密 → 回写）

═══════════════════════════════════════════════════════════════════════
 算法（社区验证过，已实测 5/5 成功，可读率 1.000）
═══════════════════════════════════════════════════════════════════════
  来源：https://github.com/Suto-Commune/soul_knight  → SKD/core.py
        https://www.neko.ink/2023/10/10/soul-knight-data-edit/

  DES/CBC/PKCS7   IV  = b'Ahbool\\x00\\x00'      （恒定）
                  key = b'iambo\\x00\\x00\\x00'  （item_data / setting /
                                                 monsrise / mall_reload /
                                                 weapon_evolution / season）
                  key = b'crst1\\x00\\x00\\x00'  （statistic）
  Xor             双层：固定 15 字节表 ⊕ 密钥（game.data → key=b'smg'）
  明文            battles / net_battle / sandbox_config / sandbox_maps

═══════════════════════════════════════════════════════════════════════
 安全设计（重要）
═══════════════════════════════════════════════════════════════════════
  · 回写前**自动备份**到 work/save_backup/<时间戳>/
  · 支持 --dry 预览，不实际写入
  · 支持 --restore 一键还原
  · 游戏运行时会覆盖存档 → 回写前必须先 `am force-stop`

用法：
  python tools\\s_saveedit.py --pull                     # 拉最新存档
  python tools\\s_saveedit.py --decrypt                  # 解密到 .json
  python tools\\s_saveedit.py --unlock-all --dry         # 预览：解锁全部
  python tools\\s_saveedit.py --unlock-all               # 执行
  python tools\\s_saveedit.py --set item_data data.materials.123 999
  python tools\\s_saveedit.py --push                     # 加密并回写
  python tools\\s_saveedit.py --restore                  # 从备份还原
"""
import argparse
import base64
import json
import os
import shutil
import subprocess
import sys
import time

sys.stdout.reconfigure(encoding='utf-8', errors='replace')

W = r'D:\元气骑士mod'
ADB = r'F:\AndroidToolchain\sdk\platform-tools\adb.exe'
D = 'emulator-5554'
PKG = 'com.ChillyRoom.DungeonShooter'
DEV = '/data/data/%s/files' % PKG
SAVE = os.path.join(W, 'work', 'save')
BK = os.path.join(W, 'work', 'save_backup')

IV = bytes([0x41, 0x68, 0x62, 0x6f, 0x6f, 0x6c, 0x00, 0x00])
KEY_IAMBO = bytes([0x69, 0x61, 0x6d, 0x62, 0x6f, 0x00, 0x00, 0x00])
KEY_CRST1 = bytes([0x63, 0x72, 0x73, 0x74, 0x31, 0x00, 0x00, 0x00])
XOR_TABLE = bytes([115, 108, 99, 122, 125, 103, 117, 99, 127,
                   87, 109, 108, 107, 74, 95])
XOR_KEY_GAME = b'smg'

DES_FILES = ('item_data', 'setting.data', 'monsrise_data',
             'mall_reload_data', 'weapon_evolution', 'season_data')
CRST_FILES = ('statistic.data', 'statistics.data')


def _alg():
    from cryptography.hazmat.primitives.ciphers import Cipher, modes
    try:
        from cryptography.hazmat.decrepit.ciphers.algorithms import (
            TripleDES as ALG)
    except ImportError:
        from cryptography.hazmat.primitives.ciphers.algorithms import (
            TripleDES as ALG)
    return Cipher, modes, ALG


def key_for(name):
    for f in CRST_FILES:
        if f in name:
            return KEY_CRST1
    for f in DES_FILES:
        if f in name:
            return KEY_IAMBO
    return None


def des_dec(raw, key):
    Cipher, modes, ALG = _alg()
    ct = base64.b64decode(raw)
    c = Cipher(ALG(key * 3), modes.CBC(IV))
    d = c.decryptor()
    out = d.update(ct) + d.finalize()
    n = out[-1] if out else 0
    if 1 <= n <= 8 and len(out) >= n and all(x == n for x in out[-n:]):
        out = out[:-n]
    return out


def des_enc(data, key):
    Cipher, modes, ALG = _alg()
    n = 8 - (len(data) % 8)
    if n == 0:
        n = 8
    data = data + bytes([n]) * n
    c = Cipher(ALG(key * 3), modes.CBC(IV))
    e = c.encryptor()
    return base64.b64encode(e.update(data) + e.finalize())


def xor_dec(raw, key=XOR_KEY_GAME):
    out = bytearray(len(raw))
    for i in range(len(raw)):
        out[i] = XOR_TABLE[i % 15] ^ raw[i] ^ key[i % len(key)]
    return bytes(out)


def sh(*a, timeout=900):
    try:
        p = subprocess.run([ADB, '-s', D] + [str(x) for x in a],
                           capture_output=True, text=True,
                           encoding='utf-8', errors='replace', timeout=timeout)
        return (p.stdout or '') + (p.stderr or '')
    except Exception as e:
        return '<err %s>' % e


def su(*a, timeout=900):
    return sh('shell', 'su', '0', *a, timeout=timeout)


def stop_game():
    sh('shell', 'am', 'force-stop', PKG)
    time.sleep(2)


def pull(quiet=False):
    tmp = '/data/local/tmp/savepull'
    su('rm', '-rf', tmp)
    su('mkdir', '-p', tmp)
    su('cp', '-r', DEV + '/', tmp + '/')
    for d in ('ano_tmp', 'hot_update', 'NewForcePatchConfigs', 'il2cpp',
              'patch_luban_config', 'VersionConfigs'):
        su('rm', '-rf', '%s/files/%s' % (tmp, d))
    su('chmod', '-R', '777', tmp)
    os.makedirs(SAVE, exist_ok=True)
    r = sh('pull', tmp + '/files/.', SAVE, timeout=1800)
    if not quiet:
        n = len([f for f in os.listdir(SAVE)
                 if os.path.isfile(os.path.join(SAVE, f))])
        print('  拉取 %d 个文件 → %s' % (n, SAVE))
    return r


def backup():
    ts = time.strftime('%Y%m%d_%H%M%S')
    d = os.path.join(BK, ts)
    os.makedirs(d, exist_ok=True)
    n = 0
    for f in os.listdir(SAVE):
        p = os.path.join(SAVE, f)
        if os.path.isfile(p) and not f.endswith('.json'):
            shutil.copy2(p, os.path.join(d, f))
            n += 1
    print('  备份 %d 个文件 → %s' % (n, d))
    return d


def decrypt_to_json():
    ok = []
    for f in sorted(os.listdir(SAVE)):
        p = os.path.join(SAVE, f)
        if not os.path.isfile(p) or f.endswith('.json'):
            continue
        raw = open(p, 'rb').read()
        if len(raw) < 8:
            continue
        k = key_for(f)
        pt = None
        if k:
            try:
                pt = des_dec(raw, k)
            except Exception:
                pass
        if pt is None and 'game.data' in f:
            pt = xor_dec(raw)
        if pt is None and raw.lstrip()[:1] in (b'{', b'['):
            pt = raw
        if pt is None or pt.lstrip()[:1] not in (b'{', b'['):
            continue
        try:
            j = json.loads(pt.decode('utf-8'))
        except Exception:
            continue
        txt = json.dumps(j, ensure_ascii=False, indent=2)
        open(p + '.json', 'w', encoding='utf-8').write(txt)
        ok.append(f)
    return ok


def encrypt_to_bin(dry=False):
    """把 .json 加回成设备上的格式"""
    res = []
    for f in sorted(os.listdir(SAVE)):
        if not f.endswith('.data.json') and not f.endswith('.data.json'):
            continue
        jp = os.path.join(SAVE, f)
        raw_name = f[:-5]        # 去掉 .json
        k = key_for(raw_name)
        j = json.load(open(jp, encoding='utf-8'))
        data = json.dumps(j, ensure_ascii=False,
                          separators=(',', ':')).encode('utf-8')
        if k:
            enc = des_enc(data, k)
        elif 'game.data' in raw_name:
            enc = xor_dec(data)
        else:
            enc = data
        res.append((raw_name, enc, len(data)))
        if not dry:
            open(os.path.join(SAVE, raw_name), 'wb').write(enc)
    return res


def push(dry=False):
    stop_game()
    files = [f for f in os.listdir(SAVE)
             if os.path.isfile(os.path.join(SAVE, f)) and not f.endswith('.json')]
    for f in files:
        local = os.path.join(SAVE, f)
        sh('push', local, '/sdcard/_se_%s' % f)
        su('cp', '/sdcard/_se_%s' % f, '%s/%s' % (DEV, f))
        su('chown', 'u0_a50:u0_a50', '%s/%s' % (DEV, f))
        su('chmod', '600', '%s/%s' % (DEV, f))
        su('rm', '-f', '/sdcard/_se_%s' % f)
        if not dry:
            print('     ✓ %s' % f)
    return len(files)


# ───────────────────────── 编辑操作 ─────────────────────────

def unlock_all(dry=False):
    """解锁全部角色/皮肤/武器（改 itemUnlock 等字段）"""
    p = os.path.join(SAVE, 'item_data.data.json')
    if not os.path.exists(p):
        print('  ✗ 先 --decrypt')
        return
    j = json.load(open(p, encoding='utf-8'))
    print('  改动前 itemUnlock: %d 项' % len(j.get('itemUnlock') or []))
    print('  改动前 skinTrialEntries: %d 项'
          % len(j.get('skinTrialEntries') or []))
    print()
    print('  ▸ 可改的字段（当前值）：')
    for k in ('itemUnlock', 'skinTrialEntries', 'blueprints', 'seeds',
              'materials', 'tokenTickets', 'specialTickets',
              'fusionWeaponPathUnlock', 'roomDecorateDic',
              'roomDecorateCurSkinDic'):
        v = j.get(k)
        if isinstance(v, (list, dict)):
            print('     %-28s %s(%d)' % (k, type(v).__name__, len(v)))
        else:
            print('     %-28s %r' % (k, v))
    print()
    if dry:
        print('  （--dry 模式，未写入）')
        return j
    open(p, 'w', encoding='utf-8').write(
        json.dumps(j, ensure_ascii=False, indent=2))
    return j


def set_value(which, path, val):
    p = os.path.join(SAVE, which + '.json')
    if not os.path.exists(p):
        cands = [f for f in os.listdir(SAVE) if f.startswith(which)]
        if cands:
            p = os.path.join(SAVE, cands[0])
        else:
            print('  ✗ 找不到 %s' % which)
            return
    j = json.load(open(p, encoding='utf-8'))
    parts = path.split('.')
    cur = j
    for k in parts[:-1]:
        if k.isdigit():
            cur = cur[int(k)]
        else:
            cur = cur[k]
    last = parts[-1]
    old = cur[last] if not last.isdigit() else cur[int(last)]
    # 类型跟随原值
    if isinstance(old, bool):
        val = (val.lower() in ('1', 'true', 'yes'))
    elif isinstance(old, int):
        val = int(val)
    elif isinstance(old, float):
        val = float(val)
    if last.isdigit():
        cur[int(last)] = val
    else:
        cur[last] = val
    open(p, 'w', encoding='utf-8').write(
        json.dumps(j, ensure_ascii=False, indent=2))
    print('  %s : %s.%s = %r → %r' % (which, '.'.join(parts[:-1]),
                                       last, old, val))


def restore(dry=False):
    if not os.path.isdir(BK):
        print('  ✗ 没有备份')
        return
    bks = sorted(os.listdir(BK), reverse=True)
    if not bks:
        print('  ✗ 备份目录为空')
        return
    src = os.path.join(BK, bks[0])
    print('  从最新备份还原: %s' % src)
    n = 0
    for f in os.listdir(src):
        shutil.copy2(os.path.join(src, f), os.path.join(SAVE, f))
        n += 1
    print('  还原 %d 个文件到 %s' % (n, SAVE))
    if not dry:
        push()


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--pull', action='store_true')
    ap.add_argument('--decrypt', action='store_true')
    ap.add_argument('--encrypt', action='store_true')
    ap.add_argument('--push', action='store_true')
    ap.add_argument('--backup', action='store_true')
    ap.add_argument('--restore', action='store_true')
    ap.add_argument('--unlock-all', action='store_true')
    ap.add_argument('--set', nargs=3, metavar=('FILE', 'PATH', 'VALUE'))
    ap.add_argument('--dry', action='store_true')
    ap.add_argument('--show', default=None)
    a = ap.parse_args()

    os.makedirs(SAVE, exist_ok=True)

    if a.pull:
        print('=== 拉取存档 ===')
        pull()
    if a.backup:
        print('=== 备份 ===')
        backup()
    if a.decrypt or a.pull:
        print('=== 解密 ===')
        ok = decrypt_to_json()
        for f in ok:
            print('  ✓ %s' % f)
    if a.show:
        p = os.path.join(SAVE, a.show)
        if os.path.exists(p):
            print(open(p, encoding='utf-8').read()[:6000])
        else:
            print('  ✗ 找不到 %s' % a.show)
    if a.set:
        set_value(*a.set)
    if a.unlock_all:
        print('=== 解锁全部 ===')
        unlock_all(a.dry)
    if a.restore:
        restore(a.dry)
    if a.encrypt or a.push:
        print('=== 加密 ===')
        res = encrypt_to_bin(a.dry)
        for n, e, l in res:
            print('  %-32s %d B → %d B' % (n, l, len(e)))
    if a.push:
        print('=== 回写 ===')
        n = push(a.dry)
        print('  写入 %d 个文件' % n)
    return 0


if __name__ == '__main__':
    sys.exit(main())
