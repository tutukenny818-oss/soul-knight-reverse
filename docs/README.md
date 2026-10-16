# 文档索引

本目录包含 Soul Knight 8.6.0 逆向工程的完整记录。

---

## 阅读顺序

### 🚀 第一次看 → 从这里开始

| # | 文档 | 内容 |
|---|---|---|
| 1 | [**QUICKSTART.md**](QUICKSTART.md) | **从零复现的最小步骤**（含环境、命令、预期输出） |
| 2 | [01-总报告.md](01-总报告.md) | **全景总览**：做了什么、破了什么、还剩什么 |
| 3 | [05-加密头破解.md](05-加密头破解.md) | 最精彩的突破：56 字节加密头明文 |

### 🔬 想深入机制

| # | 文档 | 内容 |
|---|---|---|
| 4 | [02-伤害公式.md](02-伤害公式.md) | 最终伤害/减伤/暴击的完整反汇编还原 |
| 5 | [03-伤害顺位与wiki对账.md](03-伤害顺位与wiki对账.md) | 累加累乘顺序 + **wiki 的 2 处错误** |
| 6 | [04-暴击机制-简化版.md](04-暴击机制-简化版.md) | 暴击的通俗解释（推荐先看这篇） |
| 7 | [10-其余四条机制链.md](10-其余四条机制链.md) | **冷却/移速/攻速/护甲公式** |

### 🛠️ 想动手

| # | 文档 | 内容 |
|---|---|---|
| 7 | [06-CBC模式确证.md](06-CBC模式确证.md) | `runtime_lua` 加密模式的分析过程 |
| 8 | [07-runtime_lua检索报告.md](07-runtime_lua检索报告.md) | 全网检索结论 + 现有工具清单 |
| 9 | [08-逆向流水线交接.md](08-逆向流水线交接.md) | 从 APK 到 dump.cs 的完整流水线 |
| 10 | [09-Mod数据交接.md](09-Mod数据交接.md) | Mod 注入路线（含失败记录） |

### 📐 证据链

| 目录 | 内容 |
|---|---|
| [`disasm/`](disasm/) | 关键函数的反汇编摘录 |

---

## 核心结论速查

### 加密体系

| 文件 | 算法 | 密钥/参数 |
|---|---|---|
| `config.ab` | LZ4HC + XOR | 周期 130，key = `soulKnight` |
| `global-metadata.dat` | 掩码逐字节 XOR | 掩码 0x00 或 0x66 |
| `libil2cpp.so` | XOR | 周期 0x10000，key = 0x6F |
| 存档（12 个） | DES/CBC/PKCS7 | `iambo` / `Ahbool` / `crst1` |
| `runtime_lua/*.bytes` | CBC / 8 字节 / 固定 IV | ⚠️ 密钥未取出 |

### 伤害公式

```c
最终伤害 = max(0, round_half_even( max(倍率, 0) × 基础伤害 ))
减伤后   = 原伤害 × (1 - clamp(减伤率, 0, 1))
```

### 防御公式

```
实际防御 = round_half_even( sign(x) × 6.74 × log10(0.5×|x| + 0.972) )
```

### 关键方法论

**IFix fallback 提取** —— 游戏所有 hot 方法都被 IFix 热更覆盖，但 fallback 分支保留了原始实现：

```asm
bl   IsPatched
tbz  w0, #0, <FALLBACK>     ; ← 未 patch 时跳这里
...
<FALLBACK>:                  ; 原始代码完整可读
```

**535 个函数**的原始实现可读。工具：`tools/x_fallback_extract.py`

---

## 数据文件说明

| 文件 | 内容 |
|---|---|
| `data/TALENT_FINAL_MASTER.csv` | 天赋总表 202 条 × 13 列 |
| `data/TALENT_VALUES.csv` | 天赋数值（1-5 级参数） |
| `data/LUA_PARAMS_EXTRA.csv` | **Lua 源码级真实数值** |
| `data/talent_id_clean.json` | 内部名 ↔ ID 映射 |
| `data/runtime_lua_manifest.json` | 253 个 Lua 模块清单 |
| `data/cfg_buff_stack.json` | 天赋参数原始表 |
| `data/ecb_layers.json` | 天赋继承树（4 层） |
| `data/cbc_codebook.json` | CBC 定锚表（5351 条） |

---

## 工具说明

| 工具 | 作用 | 依赖 |
|---|---|---|
| `tools/x_vautil.py` | **反汇编核心**（capstone ARM64） | `work/deace/libil2cpp_p3.so` |
| `tools/x_fallback_extract.py` | **提取 IFix fallback** | 同上 |
| `tools/s_cfgdump.py` | 解 `config.ab` | APK |
| `tools/s_meta3.py` | 解 `global-metadata.dat` | APK |
| `tools/s_decrypt_v2.py` | 解 `.so` | APK |
| `tools/s_saveedit.py` | 存档编解码 | 设备 |
| `tools/x_scan_utf16.py` | **内存扫描（UTF-16LE）** | 设备 |
| `tools/x_dump_extract.py` | 从 dump.cs 提取核心类 | dump.cs |

> ⚠️ 部分工具需要 `work/deace/` 下的解密产物（不在本仓库，见 Release）

---

## 目录对应关系

```
本仓库                          原始工作区
├── docs/                  ←  work/S_*.md + HANDOFF.md
├── data/                  ←  work/*.json + *.csv
├── tools/                 ←  tools/*.py
└── lua_source/            ←  work/lua_utf16/ + work/lua_extra/
```
