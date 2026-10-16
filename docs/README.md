# 文档索引

Soul Knight 8.6.0 逆向工程记录。

## 阅读顺序

| # | 文档 | 内容 |
|---|---|---|
| 1 | [**QUICKSTART.md**](QUICKSTART.md) | **从零复现的最小步骤** |
| 2 | [01-总报告.md](01-总报告.md) | **全景总览** |
| 3 | [05-加密头破解.md](05-加密头破解.md) | 56 字节加密头明文破解 |

## 深入机制

| # | 文档 | 内容 |
|---|---|---|
| 4 | [02-伤害公式.md](02-伤害公式.md) | 最终伤害/减伤/暴击的完整还原 |
| 5 | [04-暴击机制-简化版.md](04-暴击机制-简化版.md) | 暴击的通俗解释 |
| 6 | [10-其余四条机制链.md](10-其余四条机制链.md) | 冷却/移速/攻速/护甲公式 |

## 加密分析

| # | 文档 | 内容 |
|---|---|---|
| 7 | [06-CBC模式确证.md](06-CBC模式确证.md) | `runtime_lua` 加密模式分析 |
| 8 | [07-runtime_lua检索报告.md](07-runtime_lua检索报告.md) | 检索结论与工具清单 |

## 流水线

| # | 文档 | 内容 |
|---|---|---|
| 9 | [08-逆向流水线交接.md](08-逆向流水线交接.md) | 从 APK 到 dump.cs 的完整流程 |

## 证据链

| 目录 | 内容 |
|---|---|
| [`disasm/`](disasm/) | 关键函数反汇编摘录（12 篇） |

---

## 核心结论速查

### 加密体系

| 文件 | 算法 | 密钥/参数 |
|---|---|---|
| `config.ab` | LZ4HC + XOR | 周期 130，key = `soulKnight` |
| `global-metadata.dat` | 掩码逐字节 XOR | 掩码 0x00 / 0x66 |
| `libil2cpp.so` | XOR | 周期 0x10000，key = 0x6F |
| 存档（12 个） | DES/CBC/PKCS7 | — |
| `runtime_lua/*.bytes` | CBC / 8 字节 / 固定 IV | 模式已确证 |

### 伤害公式

```c
最终伤害 = max(0, round_half_even( max(倍率, 0) × 基础伤害 ))
减伤后   = 原伤害 × (1 - clamp(减伤率, 0, 1))
```

### 防御公式

```
实际防御 = round_half_even( sign(x) × 6.74 × log10(0.5×|x| + 0.972) )
```

### 参数槽位（统一抽象）

| 槽位 | 用途 | 默认值 |
|---|---|---|
| 3 | 冷却（副） | 0.02 |
| 8 | 冷却（主） | 0.05 |
| 14 | 自身攻速因子 | 30.0 |
| 15 | 自身移速因子 | 15.0 |

### 关键方法论：IFix fallback

```asm
bl   IsPatched
tbz  w0, #0, <FALLBACK>    ; ← 未 patch 时跳这里 = 原始实现
```

---

## 数据文件

| 文件 | 内容 |
|---|---|
| `data/TALENT_FINAL_MASTER.csv` | 天赋总表 202 条 |
| `data/TALENT_VALUES.csv` | 天赋数值（1-5 级） |
| `data/LUA_PARAMS_EXTRA.csv` | Lua 源码级真实数值 |
| `data/talent_id_clean.json` | 内部名 ↔ ID 映射 |
| `data/runtime_lua_manifest.json` | 253 个 Lua 模块清单 |
| `data/ecb_layers.json` | 天赋继承树 |
| `data/cbc_codebook.json` | CBC 定锚表 |

---

## 工具

| 工具 | 作用 |
|---|---|
| `tools/x_vautil.py` | **反汇编核心**（capstone ARM64） |
| `tools/x_map3_run.py` | **重建符号表** |
| `tools/x_fallback_extract.py` | **提取 IFix fallback** |
| `tools/s_cfgdump.py` | 解 `config.ab` |
| `tools/s_meta3.py` | 解 `global-metadata.dat` |
| `tools/s_decrypt_v2.py` | 解 `.so` |
| `tools/s_saveedit.py` | 存档编解码 |
| `tools/x_scan_utf16.py` | 内存扫描（UTF-16LE） |
| `tools/x_dump_extract2.py` | 从 dump.cs 提取核心类 |

---

## 目录对应

```
本仓库                  原始工作区
├── docs/          ←  work/S_*.md
├── data/          ←  work/*.json + *.csv
├── tools/         ←  tools/*.py
├── config_tables/ ←  work/cfgab/
├── gamedata/      ←  work/gamedata_out/
└── lua_source/    ←  work/lua_utf16/ + lua_extra/
```
