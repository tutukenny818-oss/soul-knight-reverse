# Soul Knight Reverse Engineering

元气骑士（Soul Knight）8.6.0 逆向工程成果。

## 内容

| 目录 | 说明 |
|---|---|
| `docs/` | 逆向文档（含 [**QUICKSTART.md**](docs/QUICKSTART.md) 和 [索引](docs/README.md)） |
| `docs/disasm/` | **关键函数反汇编证据**（8 篇） |
| `config_tables/` | **18 张配置表明文**（武器/敌人/角色/技能） |
| `dump/` | **核心类型摘录**（346 类，从 47503 类裁剪） |
| `gamedata/` | 武器/技能/角色明细 + 资产索引 |
| `tools/` | 分析工具（反汇编、解密、数据提取） |
| `data/` | 结构化数据（天赋表、本地化索引） |
| `lua_source/` | 已解出的天赋 Lua 源码 |

## 主要成果

### 1. 加密体系破解

| 文件 | 算法 | 状态 |
|---|---|---|
| `config.ab` | LZ4HC + XOR（周期130，key=`soulKnight`） | ✅ |
| `global-metadata.dat` | 掩码逐字节 XOR | ✅ |
| `libil2cpp.so` | XOR（周期 0x10000，key=0x6F） | ✅ |
| 存档 12 个 | DES/CBC/PKCS7 | ✅ |
| `runtime_lua/*.bytes` | CBC / 8字节 / 固定IV | ⚠️ 模式已确证，密钥未取 |

### 2. 伤害计算机制

```c
最终伤害 = max(0, round_half_even(max(倍率,0) × 基础伤害))
减伤后   = 原伤害 × (1 - clamp(减伤率, 0, 1))
```

- 取整用**银行家舍入**（不是向下取整）
- 暴击倍率默认 **1.0**（数据驱动，非硬编码）
- 暴击伤害与**武器等级**耦合（`ResolveWeaponLevel`）

### 3. 防御公式

```
实际防御 = round_half_even(sign(x) × 6.74 × log10(0.5×|x| + 0.972))
```

### 4. 关键方法论：IFix fallback

游戏的 IFix 热更桩模式：

```asm
bl   IsPatched
tbz  w0, #0, <FALLBACK>    ; ← 未 patch 时跳这里 = 原始实现
```

**535 个函数的原始实现可读。**

### 5. ACE 反 Frida 绕过

```bash
su 0 setprop init.svc.adbd stopped
su 0 setprop ro.debuggable 0
```

配合**零 Frida 内存读取**（`dd /proc/PID/mem`）可绕过进程保护。

## 工具依赖

```
python 3.13+
capstone
pycryptodome
curl_cffi      # 灰机 wiki 抓取
frida          # 可选
```

## 免责声明

本项目仅用于学习研究。所有分析对象为本地样本，未修改、未分发任何游戏资源。
