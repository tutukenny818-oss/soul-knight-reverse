# Soul Knight Reverse Engineering

元气骑士（Soul Knight）8.6.0 逆向工程成果 —— 加密破解、机制分析、数据提取。

## 内容

| 目录 | 说明 |
|---|---|
| `docs/` | 逆向文档（加密破解、伤害公式、机制链条） |
| `docs/disasm/` | 关键函数反汇编证据 |
| `tools/` | 分析工具 |
| `data/` | 结构化数据 |
| `config_tables/` | 游戏原始配置表（18 张） |
| `dump/` | 核心类型摘录 |
| `lua_source/` | 解出的天赋 Lua 源码 |

## 主要成果

### 1. 加密体系

| 文件 | 算法 | 状态 |
|---|---|---|
| `config.ab` | LZ4HC + XOR（周期 130） | ✅ |
| `global-metadata.dat` | 掩码逐字节 XOR | ✅ |
| `libil2cpp.so` | XOR（周期 0x10000） | ✅ |
| 存档（12 个） | DES/CBC/PKCS7 | ✅ |
| `runtime_lua/*.bytes` | CBC / 8 字节 / 固定 IV | 模式已确证 |

### 2. 伤害计算

```c
最终伤害 = max(0, round_half_even( max(倍率, 0) × 基础伤害 ))
减伤后   = 原伤害 × (1 - clamp(减伤率, 0, 1))
```

取整使用 **银行家舍入**。

### 3. 防御公式

```
实际防御 = round_half_even( sign(x) × 6.74 × log10(0.5×|x| + 0.972) )
```

### 4. 其余机制链

| 链 | 核心 |
|---|---|
| 冷却缩减 | 递减公式，槽位 8 = 0.05 / 槽位 3 = 0.02 |
| 移动速度 | 三段式，Bonus 0.3 / Loss 0.15 / 因子 15.0 |
| 攻击速度 | 因子默认 30.0（槽位 14） |
| 护甲回复 | 速度修正 + 概率触发 |

### 5. 关键方法论：IFix fallback

```asm
bl   IsPatched
tbz  w0, #0, <FALLBACK>    ; ← 未 patch 时跳这里 = 原始实现
```

游戏所有 hot 方法都被 IFix 覆盖，但 fallback 分支保留原始实现。
**535 个函数**的原始实现可读。

### 6. ACE 反调试绕过

```bash
su 0 setprop init.svc.adbd stopped
su 0 setprop ro.debuggable 0
```

配合零 Frida 内存读取（`dd /proc/PID/mem`）。

## 工具依赖

```
python 3.13+
capstone
pycryptodome
```

## 说明

本仓库为逆向学习记录。所有分析对象为本地样本，未修改、未分发任何游戏资源。
