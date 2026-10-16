#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""x_img_shrink.py —— 把 PNG 压到 1MB 以内
   策略（按优先级，尽量保质量）：
     1) 先试 PNG 优化（色彩量化 + 压缩级别）—— 保持 PNG 格式
     2) 不够就转 JPEG（质量从高往低试）
     3) 保底缩尺寸
"""
import sys, io, os
from PIL import Image
SRC = r"C:\Users\lenovo\.dsh\attachments\v1\objects\14\143dd7a6c63d8c71c27bf9579fa47b8c75aff9090673e9272641e517d86a9828"
OUTD = r"D:\元气骑士mod\work"
TARGET = 1024 * 1024          # 1 MB
o = io.open(os.path.join(OUTD, '_img_shrink.txt'), 'w', encoding='utf-8')
def P(*a):
    s = ' '.join(str(x) for x in a)
    print(s); o.write(s+'\n'); o.flush()

img = Image.open(SRC)
P('原图: %s  %dx%d  %s' % (img.format, img.width, img.height, img.mode))
P('原大小: %.1f KB' % (os.path.getsize(SRC)/1024))
P('')

# ── 1. 转 RGBA/RGB 便于处理 ──
if img.mode in ('P', 'LA'):
    img = img.convert('RGBA')
has_alpha = img.mode in ('RGBA', 'LA') or (img.mode == 'P' and 'transparency' in img.info)
P('透明通道: %s' % has_alpha)

results = []

# ══════ 方案 A: PNG 优化（保格式）══════
P('=' * 66); P('方案 A: PNG 优化'); P('=' * 66)
for colors in (256, 192, 128, 96, 64):
    im2 = img.copy()
    if im2.mode == 'RGBA':
        # 量化时保留 alpha
        im2 = im2.quantize(colors=colors, method=Image.Quantize.FASTOCTREE)
    else:
        im2 = im2.convert('RGB').quantize(colors=colors, method=Image.Quantize.FASTOCTREE)
    p = os.path.join(OUTD, '_test_png_%d.png' % colors)
    im2.save(p, 'PNG', optimize=True, compress_level=9)
    sz = os.path.getsize(p)
    P('  %3d 色 → %7.1f KB  %s' % (colors, sz/1024, '✓ 达标' if sz < TARGET else ''))
    results.append(('png', colors, p, sz, im2.size))
    if sz < TARGET:
        break
    os.remove(p)

# ══════ 方案 B: JPEG（质量递减）══════
P('')
P('=' * 66); P('方案 B: JPEG'); P('=' * 66)
bg = Image.new('RGB', img.size, (255, 255, 255))
if img.mode == 'RGBA':
    bg.paste(img, mask=img.split()[-1])
else:
    bg = img.convert('RGB')
for q in (95, 90, 85, 80, 75, 70, 65, 60):
    p = os.path.join(OUTD, '_test_jpg_%d.jpg' % q)
    bg.save(p, 'JPEG', quality=q, optimize=True, progressive=True, subsampling=0)
    sz = os.path.getsize(p)
    P('  q=%2d → %7.1f KB  %s' % (q, sz/1024, '✓ 达标' if sz < TARGET else ''))
    results.append(('jpg', q, p, sz, bg.size))
    if sz < TARGET:
        break
    os.remove(p)

# ══════ 方案 C: WebP ══════
P('')
P('=' * 66); P('方案 C: WebP'); P('=' * 66)
for q in (95, 90, 85, 80, 75):
    p = os.path.join(OUTD, '_test_webp_%d.webp' % q)
    img.save(p, 'WEBP', quality=q, method=6)
    sz = os.path.getsize(p)
    P('  q=%2d → %7.1f KB  %s' % (q, sz/1024, '✓ 达标' if sz < TARGET else ''))
    results.append(('webp', q, p, sz, img.size))
    if sz < TARGET:
        break
    os.remove(p)

# ══════ 选最优（优先 PNG，其次 JPEG，最后 WebP）══════
P('')
P('=' * 66); P('结果'); P('=' * 66)
ok = [r for r in results if r[3] < TARGET]
if not ok:
    P('  ✗ 所有方案都 >1MB，需缩尺寸')
    o.close(); sys.exit(1)

# 排序：格式优先 PNG > JPEG > WebP
PRI = {'png': 0, 'jpg': 1, 'webp': 2}
ok.sort(key=lambda x: (PRI[x[0]], -x[3]))
fmt, q, path, sz, size = ok[0]
P('  ★ 最优: %s q=%s  %.1f KB  %dx%d' % (fmt.upper(), q, sz/1024, size[0], size[1]))

final_ext = {'png':'.png','jpg':'.jpg','webp':'.webp'}[fmt]
final = os.path.join(OUTD, 'soulknight_meme_1mb%s' % final_ext)
if os.path.exists(final): os.remove(final)
os.rename(path, final)

# 清理测试文件
for f, qq, p, s, sz2 in results:
    if os.path.exists(p) and p != final:
        try: os.remove(p)
        except Exception: pass

P('  输出: %s' % final)
P('  大小: %.1f KB  (%.2f MB)' % (os.path.getsize(final)/1024, os.path.getsize(final)/1048576))
P('  原图: %.1f KB' % (os.path.getsize(SRC)/1024))
P('  压缩率: %.1f%%' % (100 * os.path.getsize(final) / os.path.getsize(SRC)))
P('')
P('  ✓ 小于 1MB')

o.close()
print('WROTE work/_img_shrink.txt')
