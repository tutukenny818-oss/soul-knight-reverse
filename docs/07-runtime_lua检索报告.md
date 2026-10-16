# 检索员交付报告 — 元气骑士 8.6.0 runtime_lua 加密公开解法检索 + 实验

**时间** 2026-10-16
**目标** 检索「元气骑士 / Soul Knight」热更包加密的公开解法 + Unity/IL2CPP 通用解法；重点回答：是否有人已解出 `runtime_lua` 的 Lua 热更加密、用什么密钥/算法。

---

## 〇、一句话结论

1. **网络上没有任何人公开解出元气骑士 `runtime_lua/*.bytes` 的第二层密钥。** 15+ 轮中英检索（Google / DuckDuckGo / Bing / GitHub code search / 看雪 / 52pojie / bbs.binmt / CSDN / 知乎 / 贴吧 / GameGuardian / iOSGods）只找到**存档层**（`game.data` 等）的公开解法，Lua 热更层 **零公开成果**。
2. **但检索到了两条此前缺失的关键情报**：① `libtprt.so` 反 Frida 的完整特征清单 + 现成绕过工具（Florida 等）；② `GameConfigParse.secret = "PASSWORD"` + `"1\"8ku|"` 这条**明文常量链**（本仓库在此之前只有部分记录）。
3. **本地新增 4 项决定性实验**，其中 2 项是**无需密钥就能推进的成果**（继承树指纹强化 + 87 文件共享 ≥696 B 密文前缀），2 项是**穷举否定结果**（37 万 + 51 万密钥池 × 3 算法 = 0 命中），可直接为父 agent 省掉整条暴力路径。

---

## 一、公开情报检索结果（按价值排序）

### ★★★ 1. libtprt.so 反 Frida 完整特征 + 现成绕过工具
**URL** https://www.xuanworld.top/2026/05/05/android-re-knight-frida/ （xuanworld.top，2026-05-05）

**libtprt 的检测点（已实测确认）：**

| 层 | 检测项 |
|---|---|
| Java | `BATTERY_CHANGED` 广播的 `plugged` 字段 → 判断 USB 连接 |
| Native | `__system_property_get("init.svc.adbd")`、`("service.adb.tcp.port")`、`("ro.debuggable")` |
| maps | `/proc/self/maps` 命中 `libzygisk.so` |
| 模块枚举 | `dl_iterate_phdr` / `dlsym` 扫 so |
| socket | Unix socket 名含 `frida-xxxx` |

**★ 最关键的操作要点：只要连着 adb 数据线就会闪退；拔掉数据线就不闪退。**

**现成绕过工具（全部开源）：**

| 工具 | 地址 | 作用 |
|---|---|---|
| **Florida** | https://github.com/Ylarod/Florida | 改版 frida-server（抹除全部 frida 字符串特征）→ **实测可绕过 libtprt** |
| UsbDetectionBypass | https://github.com/RytterMohn/UsbDetectionBypass | LSPosed 模块，Hook `BATTERY_CHANGED` + `__system_property_get` |
| EasyHook | https://github.com/RytterMohn/EasyHook | Windows 端 Android Frida 脚本 IDE |
| EnvScope | https://github.com/RytterMohn/EnvScope | 自检 APK，探测 Frida/Xposed/LSPosed/Zygisk |
| **RytterMohn/img** | https://cdn.jsdelivr.net/gh/RytterMohn/img/ | 该系列文章的全部截图（含 IDA 反编译图） |

**无 usb 时的替代方案（同一作者已实测）**：Termux + `su`，直接 `cat /proc/<pid>/mem` / `cat /proc/<pid>/map_files/<start>-<end>`。

**对应我们的未解问题**：父 agent 的 `monitor.cc:436 Check failed: lock_count_ == 0u` 静默杀进程，很可能就是 **adb 连线**触发的 usb 检测 → 拔线 + Florida 是当前最可行的 Frida 路径。

---

### ★★★ 2. 本仓库新增的明文常量链（这次检索才补全）
`decompiled\dump_full\dump.cs:519828`：
```csharp
public class GameConfigParse {                       // TypeDefIndex 36964
    private const string secret = "PASSWORD";        // 长度 8 → DESCrypt 原样使用
    private const string cypherText = "1\"8ku|";
    private static string m_currentPath;
    private static JObject m_gameConfigJObject;
}
```
配套（本仓库 `work/S_LUAKEY2.md` 已记录的解密结果）：
```
XorDec("1\"8ku|", "PASSWORD") = "abo123"
XorDec("\x11(55(#", "PASSWORD") = "Ahbool"   ← 存档 IV 来源
```
**推论（可操作）**：`Abo.CryptUtil.Decrypt` 的密钥不是硬编码——先 `XorDec(cipher, "PASSWORD")` 现算。若 `RuntimeLuaSourceLoader::DecryptSource @0x75ACCE8` 也走这条链，那么密钥就是**某个 XorDec 派生串**。→ 建议对 `Abo.CryptUtil.DecryptXor @0x6E57494` 的两个静态槽 `0xFE86D08` / `0xFE86D10` 直接解 RELA（本仓库已有 `r_dump_reloc.py` 可用）。

其它明文常量（dump.cs）：
| 常量 | 值 | 位置 |
|---|---|---|
| `ForcePatchConst.EncryptKey` | `"_@sT0r3SFif95"` | dump.cs:183839 |
| `StoreConfigEncryptKey` | `"_@sT0r3Cxnf19"` | dump.cs:99981 / 104247 |
| `CsvLoader<T>.SecretKey` | `"soulKnight"` | dump.cs:135331 |
| `GlobalizeManager.SecretKey` | `"soulKnight"` | dump.cs:188238 |
| `LunJianSocketManager.encryptKey`（他游） | `"spqh4hpstria0q9h"` | 看雪 288352 |
| `AboConfig.secretKey`（APK 内 abo.bytes 可解） | `"xdc*ef&xzz"` | 本仓库 S_XLUA_CRACK.md §3.6 |

**对应我们的未解问题**：`runtime_lua` 密钥的可能来源清单。

---

### ★★ 3. iOSGods 公开的存档密钥对照表（英文社区）
**URL** https://iosgods.com/topic/179016-soul-knight-latest-versions-600-almost-perfect-save-game/
帖子正文直接贴出：
```
{ "_@sT0r3Cxnf19": ["Fish chips. Unknown file"],
  "iambo": ["task.data", "item..."] }
```
→ 证实 `_@sT0r3Cxnf19` / `iambo` 属于**存档层**密钥族。（我已在 DES/3DES/Blowfish 下试过 `_@sT0r3S` / `_@sT0r3C` / `_@sT0r3SFif95` / `_@sT0r3Cxnf19` 前 8 字节，**均 0 命中**。）

---

### ★★ 4. 同类 XLua/lua 手游解密实录（可套用的方法论）
| 来源 | 关键信息 |
|---|---|
| 看雪 G33k3r（2025-08-10）https://bbs.kanxue.com/thread-287964.htm | Hook `tolua_loadbuffer` / `luaL_loadbuffer`；**dump 时 buffer 要 `add(0x20)` 跳过头部**；`name` 参数用 `NativeFunction` 调它自己的解密函数；字节码头 `1B 4C 4A`=LuaJIT、`1B 4C 75 61`=Luac |
| 看雪 ShiroMore（2024-01-15）https://bbs.kanxue.com/thread-280217-1.htm | 自定义 luajit 字节码还原公式：proto 头 `fl_inverse`；指令 `a1=~a1&0xff`、`a3=a3^idx&0xff`；字符串 `(~e&0xff)^i`（自反） |
| 看雪 wuhuaguo888（2025-09-02）https://bbs.kanxue.com/thread-288352.htm | **密钥 = AssetBundle 文件名**（纯 XOR）；AES-ECB key 硬编码在 lua 里（`spqh4hpstria0q9h`） |
| 博客园 dmeng2009 https://www.cnblogs.com/dmeng2009/p/11329258.html | lua 加密三大类总览 + XXTEA/opcode 改写还原流程 |
| 看雪 282259 https://bbs.kanxue.com/thread-282259.htm | opcode 修改后的 dump 反编译 |
| gist h080294 https://gist.github.com/h080294/53f954981d61d0fa0930fb83679cfd5a | 某手游 lua 解密脚本（`pyDes` + `base64`，前 16 B 为 IV） |
| 看雪 268574 https://bbs.kanxue.com/thread-268574.htm | Cocos2dlua Lua + 资源解密实战 |

---

### ★ 5. GitHub 仓库清单
| 仓库 | 内容 | 与我们的关系 |
|---|---|---|
| https://github.com/Suto-Commune/soul_knight | 元气骑士数据加密解密库（6 个存档文件） | 存档层，已有 |
| https://github.com/chenyq9/soul-knight-save-tool | 存档 XOR（key=`smg`, `t=((i%15)*(i%5))%92`），README 有完整代码 | 存档层，已有 |
| https://github.com/HTCheater/SKDEditor | Soul Knight Data Editor（已停更，`legacy` 分支有源码） | 存档层，**不含 Lua** |
| https://github.com/MlgmXyysd/IL2CppMemoryDumper | 内存 dump il2cpp（NekoYuzu 脚本） | 已在用 |
| https://github.com/F8LEFT/SoFixer · https://github.com/Chenyangming9/SoFixer | ELF 修复 | 已在用 |
| https://github.com/tencent/xlua | XLua 本体（查自定义 loader） | 通用参考 |
| https://github.com/marsinator358/luajit-decompiler-v2 | LuaJIT 反编译器 | 通用工具 |
| https://github.com/Ylarod/Florida | 反检测 frida-server | **★ 新发现，值得一试** |
| https://github.com/RytterMohn/EasyHook · UsbDetectionBypass · EnvScope | Frida 工具链 | **★ 新发现** |
| https://github.com/MlgmXyysd/IL2CppMemoryDumper | — | — |

**没有任何仓库涉及元气骑士的 `runtime_lua` / `app/buff` / `app/talent` Lua 解密。**

---

### ★ 6. 博客 / 文章清单
- https://tover.xyz/p/android-yuanqi/ — 2.5.1 完整逆向（DES + XOR 存档）
- https://www.neko.ink/2023/10/10/soul-knight-data-edit/ — 存档解密（key `smg`）
- https://www.neko.ink/2023/10/15/dump-il2cpp-executable-from-memory/ — 内存 dump il2cpp
- https://www.xuanworld.top/2026/01/09/android-re-knight/ — 8.x 最新版 Termux dump + 存档
- https://www.xuanworld.top/2026/05/05/android-re-knight-frida/ — **★ libtprt 反 Frida 全解**
- https://qnqn33.github.io/2026/07/31/… — 8.4.0 存档（`Abo.XorCrypt` / `CoreKit.Decrypt` 体系）
- https://www.52pojie.cn/thread-2084995-1-1.html — Rytter 8.6 版（含完整爆破脚本）
- https://www.52pojie.cn/thread-1844587-1-1.html — IL2CppMemoryDumper 原帖
- https://www.52pojie.cn/thread-1842883-1-1.html — 存档解密（1.8.4 远古版取未保护 .so）
- https://reimu.moe/2024/06/09/IL2Cpp-String-Literals/ — **★ metadata 版本号 0x18→0x1F 修正**（父 agent 已在用）
- https://bbs.kanxue.com/thread-273503.htm — global-metadata.dat / libil2cpp.so 解密修复
- https://bbs.kanxue.com/thread-282653.htm — il2cpp_class_dumper（魔改 il2cpp 动态 dump）
- https://bbs.kanxue.com/thread-288352.htm — 某修仙肉鸽协议逆向（XLua 密钥硬编码范式）

---

## 二、本地新增实验（本轮产出）

### 实验 0：文件结构普查（明确前提）
- 全库 **252 个 `.bytes`**（非 253；`lua_extract` 里另有 133 个重复副本）
- **252/252 base64 解码后长度 %8 == 0**（8 字节分组确证）
- 长度分布：min 56 B（`app/buff/IceShield.bytes`），max 13,608 B（`app/common/modifiers.bytes`）
- 香农熵：平均 **7.640**，max 7.986 → 高熵，确认已加密
- 单文件内 8 字节块重复：**0 / 252**（无重复块）
- 脚本：`work/probe_runtime_lua.py`、`work/scan_runtime_lua.py`、`work/deep_runtime_lua.py`

### ★ 实验 1：CBC（固定 IV）判定 —— 修正并强化了此前的 ECB 结论
| 判据 | 结果 | 含义 |
|---|---|---|
| 长度 %8 | 252/252 | 8 字节分组 |
| 同首块组内两两公共前缀长度 | **1021/1025 对是 8 的严格倍数**（56/64/72/88/96） | 块对齐 → 分组密码 |
| 同一 block1 值对应 >1 个不同 block0 | **0 例外（全库）** | **支持 CBC + 固定 IV**（ECB 下同一密文块应可来自不同明文块） |
| 块值出现在不同偏移 | **0 / 44,722** | 位置相关 → CBC 特征，非 ECB |
| 各位置 top 块值 | block[1] `3f4099e14ebbb113` ×86、block[2] ×85、block[3..5] ×85 | 固定 IV 导致前若干块高度聚集 |

**结论**：`runtime_lua/*.bytes` = **8 字节分组密码 + CBC + 固定 IV**（不是纯 ECB）。
脚本：`work/struct_runtime_lua.py`

### ★★ 实验 2：87 个文件共享 ≥696 字节密文前缀 = 代码继承树指纹
71 个不同首块，主要簇：

| 首块（hex） | 文件数 | 所属目录 | 组内最大公共前缀 |
|---|---|---|---|
| `9306c37c734a1a7e` | **87** | **全在 `app/talent/`** | **≥696 B（87 个块）** |
| `5629f1db2a0a40e8` | 11 | buff + common + talent | ≥2288 B |
| `390a47d5dbc67f15` | 9 | buff + talent | ≥1184 B |
| `0e624bc27a59f3ab` | 7 | buff + talent | ≥2768 B |
| `3224f2440127125c` | 6 | buff + talent | ≥1304 B |
| `e3270821978790b2` | 6 | buff + talent | ≥1432 B |
| `7bb41e9b24ad048f` | 5 | — | ≥2272 B |
| `5bdb4b5067b265f5` | 5 | — | ≥2736 B |

**含义（不需要密钥）**：密文前缀逐字节相同 ⇔ 明文前缀逐字节相同（同 key/IV）。87 个 talent 文件共享 87 个块完全相同的开头 → 只能是**共享的 Lua 基类/框架声明**（`local Talent = class("Talent", ...)` 加统一头部）。**继承层级可直接从密文读出。**
脚本：`work/kpa_runtime_lua.py`、`work/deep_runtime_lua.py`

### ★★ 实验 3：IV 无关的 CBC oracle + **370,799 密钥池 × 3 算法 = 0 命中**
**原理**：CBC 下 `P_i = D_K(C_i) XOR C_{i-1}`（i≥1），**完全不含 IV** → 只需猜对 key 即可从块 1 起还原明文，无需知道 IV。
**oracle 强度**：512 字节明文可打印率 ≥0.93（随机基线 0.386）→ 假阳性 ≈ 10⁻¹²。
**oracle 已自检通过**（`work/selfcheck_oracle.py`：用测试向量验证，正确 key 从块 1 起 100% 还原 `lent = {}\nlocal x = 1\n...`）。

| 密钥池 | 条数 | 来源 |
|---|---|---|
| 池 A | **371,580** | dump.cs 全量 8 B 窗口 + 18 个已解密 CSV + all work/ 配置 + **IFix 补丁 `9c8decac….bytes` 字符串池** + APK 内 400 文件文本窗 |
| 池 B | **513,093** | `global-metadata_clean.dat` 全部 74,284 条字符串字面量的逐字节滑动 8 B 窗口 |

| 算法 | 池 A | 池 B |
|---|---|---|
| DES-ECB | 0 | 0 |
| 3DES-ECB | 0 | 0 |
| Blowfish-ECB | 0 | 0 |

（合计 **2,653,000+ 次判定，0 命中**）
脚本：`work/ivfree_oracle.py`、`work/final_lua_key_attack.py`；结果 `work/ivfree_hits.txt`（空）

**推论（重要）**：由于池 B 覆盖了**所有 metadata 字符串字面量的所有 8 字节窗口**，因此
> **密钥不是任何 metadata 字符串字面量（或其 8 字节窗口 / 前缀）。**

### ★ 实验 4：文件名/路径当密钥（范式来自看雪 288352）—— 不成立
- 796 个文件名/路径/hash 候选 × 3 算法 → **0 命中**（阈值 0.93）
- 全局最高 pr 仅 **0.604**（`Automation` + Blowfish），远低于判定线，属噪声
- 脚本：`work/filename_key_test.py`

### 实验 5：`filename_key_test` 顺带否定 —— 自密钥（明文前 8 B = key）不成立

---

## 三、为什么静态穷举一定失败（机理）

本仓库 `work/S_LUAKEY2.md` 已用反汇编确证：

```
RuntimeLuaSourceLoader::DecryptSource   @ VA 0x75ACCE8
  ├─ 0x075ACD98  bl 0x828747C   GameConfigManager::get_SecretKey()
  ├─ 0x075ACDB4  x3 = [0xFE41698]   ← IV（ELF RELA 运行时填充）
  └─ 0x075ACDBC  bl 0x9CBC644   DESCrypt::Decrypt(b64, key, IV)

GameConfigManager::get_SecretKey  @ 0x828747C  ← AOT 桩
  0x828747C  mov  w0, #0xBB5E ; movk w0,#1,lsl#16   → idx = 0x1BB5E = 113502
  0x8287485  bl   #0x95BFCD0    IsPatched(idx)
  0x82874B4  b    0x9209140     __Gen_Wrap_215   ← 真实实现（IFix 运行时写入）
```
**`__Gen_Wrap_215 @ 0x92258D0` 处字节非法（capstone 解析 0/8 条合法指令）** → 真实密钥派生逻辑**不在静态 `libil2cpp.so` 里**，任何静态穷举都无法覆盖。这与我 260 万次判定 0 命中完全一致。

**由此得到唯一可行路径：运行时取密钥。**

---

## 四、给父 agent 的行动清单（按性价比）

| # | 动作 | 命令 / 要点 | 预期 |
|---|---|---|---|
| **1** | **Florida 替 frida-server + 拔掉 adb 数据线** | 下载 https://github.com/Ylarod/Florida 的对应 ABI；`./florida-server -l 0.0.0.0:PORT -D`；手机端 **不插线**，用 `frida -H IP:PORT` 连 | 若能进 → 直接 hook |
| **2** | hook `get_SecretKey` 真实体 | `libil2cpp.so + (0x9209140 - loadBias)`，`Interceptor.attach` 打印 `retval` 的 8 字节 | **密钥当场到手** |
| **2b** | 或 hook `RuntimeLuaSourceLoader::DecryptSource` | VA `0x75ACCE8`，打印 `args[1]`(key) / `args[2]`(iv) | 同效 |
| **2c** | 或 hook `DESCrypt::Decrypt` | VA `0x9CBC644`，打印 `args[1]`(base64) 与 `args[2]`(key) | 同效 |
| **3** | 无 Frida 时：Termux 读活进程内存 | `su` 后 `cat /proc/<pid>/mem`；在 `libil2cpp.so` 数据段搜 `^[A-Za-z0-9_@/]{8}$`，且**紧邻 DES key schedule（56 字节轮密钥）结构** | 中 |
| **4** | 解 `Abo.CryptUtil.DecryptXor @0x6E57494` 的静态槽 | `0xFE86D08` / `0xFE86D10` 的 RELA addend（用 `tools/r_dump_reloc.py`） | 中高（若 Lua 也走这条链）|
| **5** | 解 `GetMD5_Old` / `GetSignDefault` @ `0x6E57120‥0x6E58BCC` 的字符串槽 | 15 个方法全部可反汇编 | 中 |
| **6** | 反解 IFix 补丁 IL 段 | `NewForcePatchConfigs/9c8decac….bytes` 偏移 `0x11E4‥0x86800` **未加密**；结构 `[typeToken][methodToken][u8 len][name]`；可定位 `get_SecretKey` 的 IL | 高（但工作量大）|

**已被否定、不要重复的路**（本报告实证）：
- ❌ 纯 ECB 假设（实测是 CBC+固定 IV）
- ❌ 密钥 = metadata 字符串字面量的任意 8 字节窗口（513,093 组合全试）
- ❌ 密钥 = dump.cs / 配置 / IFix 池 / APK 文本窗的任意 8 字节窗口（371,580 组合全试）
- ❌ 密钥 = 文件名 / 相对路径 / 其 md5·sha1·sha256（796 组合）
- ❌ 密钥 = `_@sT0r3S` / `_@sT0r3SFif95` / `_@sT0r3Cxnf19` / `iambo` / `smg` / `Ahbool` / `abo123` / `PASSWORD` / `soulKnight` 及大小写补零变体
- ❌ Blowfish / 3DES 全试
- ❌ 纯 ASCII 暴力（6 字符全集 = 2.18×10⁹ 组合，按 486 key/s 需 ~52 天；**不实用**）

---

## 五、附：本轮新增/新增可复用脚本
| 脚本 | 用途 |
|---|---|
| `work/probe_runtime_lua.py` | 抽样 base64 解码 + 魔数/可打印率 + 候选密钥快筛 |
| `work/scan_runtime_lua.py` | 全库 .bytes 长度/对齐/块重复/首块分布普查 |
| `work/deep_runtime_lua.py` | 组内全量比较、逐位置熵、共同前缀分布 |
| `work/kpa_runtime_lua.py` | 首块已知明文攻击框架（Lua 头候选） |
| `work/struct_runtime_lua.py` | **★ ECB vs CBC 判定**（块位置/偏移/联合分布） |
| `work/selfcheck_oracle.py` | **★ CBC oracle 自检**（必跑，防止 oracle 实现错误） |
| `work/ivfree_oracle.py` | **★ IV 无关 CBC oracle + 371,580 密钥池 × 3 算法** |
| `work/final_lua_key_attack.py` | **★ metadata 全字符串滑动窗口 513,093 密钥 × 3 算法** |
| `work/filename_key_test.py` | 文件名/路径/hash 当密钥范式验证 |

**运行须知**：控制台需 `$env:PYTHONIOENCODING="utf-8"`，否则中文/unicode 输出会抛 `UnicodeEncodeError`（GBK 编解码器）。
