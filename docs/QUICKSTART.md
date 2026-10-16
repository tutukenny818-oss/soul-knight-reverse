# 快速开始

从零复现 Soul Knight 8.6.0 的逆向流程。

---

## 0. 环境准备

### 必需

```bash
python >= 3.10
pip install capstone pycryptodome
```

### 可选（设备相关）

```bash
pip install frida curl_cffi        # 动态注入 / wiki 抓取
```

### `requirements.txt`

```
capstone>=5.0
pycryptodome>=3.20
frida>=16.0          # 可选：动态分析
curl_cffi>=0.6       # 可选：灰机 wiki 抓取
Pillow>=10.0         # 可选：图片处理
```

---

## 1. 静态数据（不需要设备）

### 1.1 提取 `config.ab`

```bash
python tools/s_cfgdump.py
# 输出：18 张明文 CSV
```

**算法**：UnityFS → LZ4HC → XOR（周期 130，key = `soulKnight`）

### 1.2 解密 `global-metadata.dat`

```bash
python tools/s_meta3.py
# 输出：global-metadata_clean.dat（55 MB，magic 0xFAB11BAF）
```

**算法**：逐字节 XOR，掩码表 0x00 / 0x66（13,860,848 个非零字节）

### 1.3 解密 `libil2cpp.so`

```bash
python tools/s_decrypt_v2.py
# 输出：libil2cpp_p3.so（278 MB）
```

**算法**：XOR 周期 0x10000，key = 0x6F，窗口 [0x3000, 0x7000)

### 1.4 生成 `dump.cs`

用 Il2CppDumper：

```
Input:  libil2cpp_p3.so（解密后）
        global-metadata_clean.dat（解密后）
Config: DumpMethod = false
        DumpField  = true
        ForceVersion = 29.0     ← ★ 必须写 29.0，不能写 29.1
Output: dump.cs（47,503 类型 / 25.4 MB）
```

### 1.5 提取核心类型（本仓库的 `dump_essential.cs`）

```bash
python tools/x_dump_extract.py
# 25.4 MB → 0.39 MB（346 个核心类）
```

---

## 2. 反汇编分析

### 2.1 准备

需要 `work/deace/libil2cpp_p3.so`（解密后）。

```bash
python -c "
import sys; sys.path.insert(0, 'tools')
import x_vautil as V
print(V.dis(0x0666213C, 20))   # CalculateFinalDamage
"
```

### 2.2 提取 IFix fallback（★ 核心技巧）

```bash
python tools/x_fallback_extract.py
# 输出：535 个函数的原始实现
```

**原理**：所有 hot 方法被 IFix 覆盖，但 fallback 分支保留原实现。

### 2.3 还原伤害公式

```bash
python tools/x_dmg_formula.py
# 输出：CalculateFinalDamage / CalculateCriticalDamage / 减伤公式
```

---

## 3. 动态分析（需要 Android 设备）

### 3.1 绕过 ACE 属性检测

```bash
adb shell su 0 setprop init.svc.adbd stopped
adb shell su 0 setprop ro.debuggable 0
```

**效果**：游戏存活时间从 10 秒 → 150+ 秒

> ⚠️ 游戏启动时会重置这两个属性，需要在启动**之后**再设一次。

### 3.2 零 Frida 内存读取（推荐）

```bash
# 1. 启动游戏（不挂 frida）
adb shell su 0 monkey -p com.ChillyRoom.DungeonShooter \
    -c android.intent.category.LAUNCHER 1

# 2. 等 il2cpp 加载
adb shell su 0 grep libil2cpp /proc/<PID>/maps

# 3. dump 关键段
python tools/x_dump_key_segs.py
```

**为什么不用 frida**：ACE 检测到 frida 后会阻止 il2cpp 加载。

### 3.3 扫描 Lua 源码（★ 关键：UTF-16LE）

```bash
python tools/x_scan_utf16.py
# 输出：Lua 模块源码
```

**关键**：内存里的 Lua 源码是 **UTF-16LE** 编码。
只扫 ASCII 会得到 0 结果。

### 3.4 提取天赋参数

```bash
python tools/x_grep_buffadd.py
# 输出：Buffs.add(...) 的完整参数表
```

---

## 4. 存档操作

```bash
python tools/s_saveedit.py --pull        # 拉取
python tools/s_saveedit.py --decrypt     # 解密
python tools/s_saveedit.py --show <file> # 查看
python tools/s_saveedit.py --push        # 回写
```

**算法**：DES/CBC/PKCS7
- IV = `b'Ahbool\x00\x00'`
- key = `b'iambo\x00\x00\x00'`（item_data/setting 等）
- key = `b'crst1\x00\x00\x00'`（statistic）

---

## 5. 数据提取

```bash
python tools/x_loc_master.py      # 本地化表 ↔ 天赋 ID 对接
python tools/x_talent_final.py    # 天赋最终表
python tools/x_buff_stack.py      # 天赋数值
python tools/x_final_master.py    # 合并所有来源
python tools/x_finalize.py        # 生成 wikitext
```

---

## 6. 灰机 wiki 抓取

```bash
python tools/s_huiji2.py
# 需要 curl_cffi（绕 Cloudflare）
```

**可用的浏览器指纹**：
```
chrome116 / chrome110 / chrome104 / chrome101 / chrome100 / chrome99
edge101 / edge99 / firefox133
safari17_0 / safari15_5       ← 唯一能拿 HTML 的
```

**注意**：`chrome131 / chrome124 / chrome120` 已失效。

---

## 7. 常见问题

### Q: `x_vautil.py` 报错找不到文件

需要先准备 `work/deace/libil2cpp_p3.so`（解密后的 .so）。
见 §1.3。

### Q: 内存扫描结果为空

1. **确认用 UTF-16LE**（不是 ASCII）
2. 确认 il2cpp 已加载（`grep libil2cpp /proc/PID/maps`）
3. 确认游戏**没有挂 frida**（ACE 会阻止 il2cpp 加载）

### Q: 挂 frida 后 il2cpp 不加载

ACE 检测到了。方案：
- 用**零 Frida 方案**（§3.2）
- 或用 patch 过的 frida-server（需自己编译，见 [07](07-runtime_lua检索报告.md)）

### Q: `dump.cs` 里方法体都是空的

`DumpMethod` 被设成了 `false`。方法体需要真机 Frida dump `methodPointers`。

---

## 8. 完整的成功标准

跑完 §1 后你应该得到：

| 文件 | 大小 | 校验 |
|---|---|---|
| `config_decrypted/*.csv` | — | 18 张表，可用 Excel 打开 |
| `global-metadata_clean.dat` | 55,398,120 B | magic = `0xFAB11BAF` |
| `libil2cpp_p3.so` | 278,386,960 B | `file` 识别为 ELF aarch64 |
| `dump.cs` | 25.4 MB | 47,503 个类型 |

跑完 §3 后你应该得到：

| 文件 | 内容 |
|---|---|
| Lua 模块源码 | 至少 7 个完整模块（SkillHaste / EnergyRecycle 等） |
| 天赋参数 | `Buffs.add(...)` 的参数表 |

---

## 9. 相关文档

- [01-总报告](01-总报告.md) — 全景
- [02-伤害公式](02-伤害公式.md) — 公式还原
- [05-加密头破解](05-加密头破解.md) — 最精彩的一步
- [08-逆向流水线交接](08-逆向流水线交接.md) — 详细流水线
