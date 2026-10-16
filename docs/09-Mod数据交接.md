# 元气骑士 8.6.0 Mod 工程 —— 交接文档

> 生成时间：2026-10-16
> 工作目录：`D:\元气骑士mod`
> 状态：**已打通存档修改 + 配置层找到入口（待验证）**

---

# 第一部分：总目标与现状

## 1.1 目标

让元气骑士能玩 mod：**改数值、加角色、解锁内容**，自己和群友都能玩上。

## 1.2 一句话现状

```
APK 本体     ← 【死路】ACE 保护，改一个字节就杀进程
   ↓
外部 metadata ← 【活路】能改显示文本
   ↓
私有目录配置  ← 【待验证】15 个明文 JSON，但会被 CDN 覆盖
   ↓
存档         ← 【已完成】完全可用
```

**策略：越往外越松，逐层往外找可下手的口子。**

---

# 第二部分：环境

## 2.1 两台设备

| | 真机 | 雷电模拟器 |
|---|---|---|
| 串号 | `3437717763001KZ` | `emulator-5554` |
| 型号 | vivo V2171A (PD2171) | LDPlayer 9 |
| 系统 | Android 15 / SDK 35 | Android 9 / API 28 |
| ABI | arm64-v8a（原生） | x86_64 + **libhoudini 翻译层** |
| Root | ❌ 不可行 | ✅ **可用** |
| Frida | ❌ ACE 会杀 | ❌ 不兼容 |
| 用途 | 测试真实环境 | **主力：配置/存档操作** |

**真机 root 不可行的证据**：`ro.boot.flash.locked=1`、`verifiedbootstate=green`、`user/release-keys`，vivo 无官方 BL 解锁通道。

**雷电路径**：`F:\leidian\LDPlayer9\ldconsole.exe launch --index 0`

## 2.2 adb

```
ADB = F:\AndroidToolchain\sdk\platform-tools\adb.exe
```

⚠️ adb 会间歇掉线，需 `adb kill-server; adb start-server`

## 2.3 ★ su 调用的正确姿势（踩过坑）

```
✗ su 0 sh -c 'cmd'   → su 把整串当参数，输出丢失
✗ su -c cmd          → adb 拆参，su 报 invalid option -- r
✓ su 0 <cmd> <arg1> <arg2> ...   每个参数分开传
```

**Python 里读设备文件必须用二进制捕获**（`text=True` 会被 CRLF 破坏）：

```python
p = subprocess.run([ADB, '-s', SER, 'shell', 'su', '0', 'base64', f],
                   capture_output=True)   # ← 不要 text=True
data = base64.b64decode(b''.join(p.stdout.split()))
```

## 2.4 游戏信息

```
包名      com.ChillyRoom.DungeonShooter
版本      8.6.0 (versionCode 80600)
APK       5570618_20260905032720_Soul_Knight_release_chill.apk  (778,502,561 B)
minSdk    23
targetSdk 30
架构      arm64-v8a + armeabi-v7a
```

**隐私页坐标**（横向 framebuffer 空间，2340×1080）：`(1471, 849)`
**Unity 开始更新**：`(1462, 828)`
**vivo 安装弹窗**：复选框 `(540,2100)` / 继续安装 `(540,2247)`

---

# 第三部分：游戏保护机制

## 3.1 三层保护

### ① ACE（腾讯反作弊）—— 保护 APK 本体

**运行时结构**：
```
libil2cpp.so / libunity.so 的 DT_INIT (0x10BCE590) 是 ACE 注入段
  bl 0x10BCE1C4        ; 静态初始化 + .rodata 解密 + 跑全部校验点
  ldr x10, [0x10BD3000+0x7C0]  ; = g_tprt_pfn_array
  br x2
```

**完整性清单**：`assets/__tpinfo.tsc` = 59,112 B = 12B 头 + **4925 条 × 12B**

```
头部: magic(8B) 00 7f 54 7f 53 7f 43 00 + LE u32 记录数 (4925)
记录: 3 个 LE u32 = (col0, col1, col2)
语义: col1 = file_size，col2 = ZIP 中央目录的 CRC32
```

**独立验证结果（100% 命中）**：
```
(size,crc) 双匹配         : 4925 / 4925 = 100.0%
col2 在 ZIP CRC 集合里    : 4925 / 4925 = 100.0%
col1 在 ZIP size 集合里   : 4925 / 4925 = 100.0%
res/ 条目 1320 个，在清单里 72 个 = 5.5%  ← ★ 1248 个 ACE 盲区
```

`assets/__tpinfo.tsc.sig` = 256 B（RSA-2048 签名）⇒ **清单本身改不了**

**覆盖率解释了一切实测**：
- 2375 个 `.ab` **全在**清单 ✓
- 2375 个 `.manifest` **也在** ✓
- `_AssetBundleSize.txt` / `hot_update_manifest.json` **不在** ✓（所以改它安全）

### ② 自定义加密 —— 保护数据

见第四部分。

### ③ FileSync / 热更 —— 保护运行时配置

```
[FileSync]Http Download: /data/user/0/<pkg>/files/VersionConfigs/.../GameEventConfig.json
[ForcePatchManager] 动态配置下载成功
[ForcePatchManager] 热更检查：LocalFileIsComplete
```

CDN：`http://cdn.chillyroom.com/game/soulknight/hot_update`（明文 HTTP）

---

# 第四部分：已破解的加密算法

## 4.1 config.ab（APK 内游戏配置）✅

```
容器: UnityFS v8 → LZ4HC → 解压出 2,338,364 B
内容: 18 个 TextAsset
加密: XOR，周期 130
密钥: "soulKnight"（10 字节，循环 13 次填满 130）
key[0:32] hex = 736f756c4b6e69676874736f756c4b6e69676874736f756c4b6e69676874736f
格式: CSV（不是 JSON）
```

**破解过程**：文件头 4 字节相同 ⇒ 固定密钥流 → 扫周期 → 频率分析恢复密钥

**已导出** → `work/cfgab/*.csv`（2.23 MB，18 张表）：

| 表 | 行数 | 关键列 |
|---|---|---|
| characters | 25 | Key,Path,Hp,MaxHp,Speed,SpeedRate,Critical,Energy,MaxEnergy,Atk |
| enemies | 592 | Key,Path,LevelKey,EnemyId,Hp,Speed,IsBoss,BossRoom,... |
| weapons | 1046 | Key,Path,OriginalWeapon,Price,Materials,FusionList |
| skills | 147 | Key,SkinType,Value(JSON) |
| skins | 1006 | Key,ControllerPath |
| items | 804 | Key,Type,SubType,DataStr |
| map_levels | 257 | Level/Bgm/Floors/Walls/Obstacles/Enemies/Rooms |
| weapons_drop | 325 | Key,Group,Weapon,Weight,CorrectValue |
| 其余 10 张 | — | achievements/pets/mounts/npc/... |

工具：`tools/s_cfgdump.py`

## 4.2 global-metadata.dat（IL2CPP 元数据）✅

```
加密: 掩码法（逐字节 XOR，掩码 0x00 或 0x66）
掩码: work/modkit/mask.bin（13,860,848 个非零字节）
解密: plain = cipher ^ mask
加密: cipher = plain ^ mask
```

**双向自检 0 差异。**

- 原始密文备份：`work/modkit/global-metadata.orig.dat`
- 解密后：`work/deace/global-metadata_clean.dat`（55,398,120 B，magic 0xFAB11BAF，version 29）

**结构（v29）**：
```
stringLiteral        off=256        size=614512   → 76,814 条×8B (i32 len, i32 dataIdx)
stringLiteralData    off=614768     size=2143120
string               off=2757888    size=12296420 → 453,769 条名字
methods              off=16152608   size=13970376
fieldDefaultValues   off=30266024   size=1917120  → 159,760 条×12B
fieldAndParameterDefaultValueData off=32183144  size=2817240
fields               off=39559244   size=4221648  → 351,804 个×12B
typeDefinitions      off=47251812   size=4180264  → 47,503 个×88B
```

★ **陷阱**：`fields` 表的 nameIndex 用**字节偏移**（相对字符串表起点），不是序号。

工具：`tools/s_meta3.py`（--selftest / --info / --fields）

## 4.3 libil2cpp.so / libunity.so（第二层加密）✅

```
加密: XOR，周期 0x10000
密钥: 0x6F
窗口: libil2cpp [0x3000, 0x7000)  /  libunity [0xA000, 0xE000)
```

**验证**：file 0x5DAC2C 原始 `0xC57A6C8F` → XOR 0x6F → `0xAA1503E0` = `mov x0, x21`（真指令）

**★ 关键坑**：槽位是**周期对齐**（相对 0x10000），不是绝对 0x400 网格。
判据：`raw >= 0.90` 明文不动；否则 XOR 0x6F 后 `dec >= 0.95` 且 `dec-raw > 0.20` 为密文。

工具：`tools/s_decrypt_v2.py`（周期对齐全量解密）

## 4.4 存档文件 ✅（完全可用）

```
算法: DES/CBC/PKCS7
IV  = b'Ahbool\x00\x00'          （恒定）
key = b'iambo\x00\x00\x00'       （item_data / setting / monsrise /
                                  mall_reload / weapon_evolution / season）
key = b'crst1\x00\x00\x00'       （statistic）
```

**Roundtrip 验证 5/5 逐字节相同。实机验证通过。**

工具：`tools/s_saveedit.py`

## 4.5 luban.bytes ⚠️（部分破解）

```
加密: PBKDF2(口令, salt=keySalt(14B), iterations=10000) → AES
实现: GG.Extensions.SecurityExtensions (TypeDefIndex 44125)
      Iterations = 10000
      SaltKeyEncrypt @0x0DC6A884 / SaltKeyDecrypt @0x0DC6ABF4
      AesEncryption  @0x0DC6AEC0 / AesDecryption  @0x0DC6B544

口令: ★ 已提取 = "AZhenSoulKnightd@orz" (20 字符)
salt: ❌ 未取出（位置已知，见下）
```

**口令提取链路（完整证据）**：
```
1. 反汇编 ConfigData::.cctor @0x08B6B610:
     0x08B6B634  ldr x22, [x22, #0x3a8]   ; x22 = GOT[0xFF233A8]
     0x08B6B684  ldr x9,  [x22]           ; x9 = *x22
     0x08B6B68C  str x9,  [x8]            ; static_fields[0] = Key

2. RELA 解析（.rela.dyn 共 1,817,321 条）:
     0xFF233A8 [R_AARCH64_RELATIVE] addend = 0x10494600
     0xFF233B0 [RELATIVE] addend = 0x1047BDE0   （keySalt 字段句柄）
     0xFF232B8 [RELATIVE] addend = 0x1037D4C0   （ConfigData klass）

3. addend 处是 IL2CPP usage token 表，[0] = 0xA0005405

4. usage 编码（双重验证）:  usage = 0xA0000000 | (idx*2 + 1)
     ⇒ idx = (0x5405 - 1) / 2 = 10754  ⇒  "AZhenSoulKnightd@orz"

5. 交叉验证: 该串在 metadata 0xCF4D4，邻居是 A_Coin/A_Gem/A_coin/A_pet_atk
```

**keySalt 未取出的原因**：
```
.cctor 里: mov w1, #0xe（14 字节）+ InitializeArray
RELA 链: 0x1047BDE0 → token 0x80000115 → usageType 4 = FieldInfo, index 138
但字段 #138 名叫 "no"，且无 fieldDefaultValues 条目
⇒ IL2CPP v29 里 InitializeArray 的数据不在 metadata 默认值表
```

**已排除**（都有实测证据）：
- 压缩：zlib / raw-deflate / gzip / lzma×2 / bz2 / lz4 / brotli **全败**
- 周期 XOR：周期 1..4096 全扫，零值率最大 ×1.06（噪声）
- 41 万密钥池 × 763,618 组合 × {DES, AES-128/192/256, RC4} × {ECB, CBC}：**0 命中**
- 口令的 266 种派生（PBKDF2/HMAC/哈希/拼接/填充/反转/大小写/子串）：**0 命中**

**副产物**：`work/luban_out/luban_schema.json` = **334 张表 / 333 bean / 2316 字段 + 每表 __ID__ 哈希**（不需解密即可用）

## 4.6 IFix 热更补丁 ⚠️

**关键发现**：`GameConfigManager.get_SecretKey` 的真实实现**不在 libil2cpp.so 里**。

```asm
0x828747C  mov  w0, #0xBB5E
0x8287480  movk w0, #1, lsl #16      ; w0 = 0x1BB5E = 113502 = patch 索引
0x8287485  bl   #0x95BFCD0           ; IsPatched(idx)
0x828748C  tbz  w0, #0, #0x82874B8
0x8287490  bl   0x95BFC54            ; GetPatch(idx)
0x82874B4  b    0x9209140            ; __Gen_Wrap_215 ← 真实实现
0x82874B8  ; AOT 桩，只读 GameConfig 字段
```

- 全 .so 有 14,712 个 `__Gen_Wrap_N`，但 **`__Gen_Wrap_215` 落在 0x92258D0，该处 0/8 条合法指令** ⇒ IFix 运行时写入桩位
- **⇒ 密钥派生逻辑完全不在静态镜像中，任何静态穷举都无法覆盖**

**补丁文件**（未加密）：
```
work/gamedata/skpull/NewForcePatchConfigs/
    9c8decac80eb1629ca11bb2503e61c91_20261013164951.bytes  (595,396 B)
头部: "IFix.ILFixInterfaceBridge, Assembly-CSharp, Version=0.0..."
```
⚠️ 字符串池格式与预期不符（`[u8 len][bytes]×N` 解不出），IL 流只扫到 9 个有效 `ldstr`。

**同样机制也在**：`ConfigData.LoadByteBuf @0x8B6B3D0` → `w0=0x2E7D(11901) → IsPatched → __Gen_Wrap_13184`

---

# 第五部分：死路清单（已验证，不要再试）

| 做法 | 结果 | 证据 |
|---|---|---|
| 改 APK 内 `.ab` | **4 秒静默退出** | Unity 日志 0 行 |
| 改 APK 内 `.so` | 同上 | — |
| 改 APK 内 `metadata` | 同上 | — |
| 改 dex | 同上 | — |
| `.ab` 重压（UnityPy 验证结构合法） | 死 | — |
| `.ab` + CRC 碰撞 | 能进关卡但数值还原 | 热更覆盖 |
| 同步 `hot_update_manifest.json` 的 md5 | 不是那堵墙 | Z1/Z2 都死 |
| 换桩库（架空 ACE） | 解密器停摆 → **SIGILL** | tombstone 落在第二层密文区 |
| 原始 ACE + 8 处分支 patch | **4 秒 SIGBUS** | 分支在受保护段内 |
| Frida 动态注入 | ACE 检测 → 杀进程 | `monitor.cc:436 lock_count_` 断言 |
| AOSP 模拟器 | 翻译层崩（原包也崩） | `libndk_translation` 栈指针自检失败 |
| 劫持 CDN | 失败 | 改 6 处域名后照样从官方下载 |
| 外部目录放配置文件 | 不被读取 | 10 个候选位置 0 命中 |
| **断网** | **不可行** | 游戏必须联网才能登录 |

**核心结论**：APK 本体是铁板一块。ACE 的 `__tpinfo.tsc` 清单 + RSA 签名，改不了。

---

# 第六部分：活路清单（可用通道）

## 6.1 ★ 路 A：存档修改（**已完成，立即可用**）

**位置**：
```
/sdcard/Android/data/<pkg>/files/il2cpp/Metadata/  ← metadata（外部可写）
/data/data/<pkg>/files/                            ← 存档（需 root）
```

**存档文件清单**（雷电实测，35 个 `.data`）：
```
item_data_87489914_.data          3500 B  → 角色/武器/皮肤解锁状态（77 keys）
statistic.data                    9720 B  → 统计（158 keys）
season_data_87489914_.data       19244 B  → 赛季（70 keys）
setting_87489914_.data            1600 B  → 设置（35 keys）
task_87489914_.data               2328 B  → 任务（12 keys）
misc_data_87489914_.data           728 B  → 杂项（17 keys）
monsrise_data_87489914_.data       556 B  → 怪物图鉴（18 keys）
escape_run_save_87489914_.data     952 B  → 逃生存档（30 keys）
escape_season_data_87489914_.data 1504 B  → 逃生赛季（38 keys）
pvp_data_87489914_.data            608 B  → PVP（10 keys）
bp_data_87489914_.data             120 B  → 通行证（3 keys）
weapon_evolution_data.data         128 B  → 武器进化（4 keys）
```

**已解密到** `work/emu_save/*.json`

**工具用法**：
```bash
python tools\s_saveedit.py --pull --decrypt     # 拉取+解密
python tools\s_saveedit.py --unlock-all         # 解锁
python tools\s_saveedit.py --set item_data data magicWellFishedTimes 777
python tools\s_saveedit.py --encrypt --push     # 加密+回写
python tools\s_saveedit.py --restore            # 从备份还原
```
- roundtrip **5/5 逐字节相同**
- 实机验证通过
- 回写前自动备份到 `work/save_backup/<时间戳>/`

## 6.2 路 B：外部 metadata 通道

```
/sdcard/Android/data/<pkg>/files/il2cpp/Metadata/global-metadata.dat
```

**三重实证**：
1. adb 可写、不需要 root、**ACE 完全不参与**（在 APK 之外）
2. 游戏**会读**（破坏中部 4096 字节 → `Fatal signal 11 in tid UnityMain`）
3. 游戏**不覆盖**（改动 md5 存活）

**实证案例**：改 3 处「凉屋账号」→「元气骑士」→ 游戏正常登录进主界面

**能改**：显示文本（⚠️ 必须**等长替换**）

**★ 陷阱**：metadata 字符串混用两种用途
- **(A) 界面显示文本** → 改了只影响显示 ✅
- **(B) 序列化数据的键名/标识符** → 改了破坏读写 ❌

实测：`继续游戏`(12B,纯显示) → `猫猫骑士` 安全；
      `退出`(6B,太短) → `溜了` 后游戏报「读取用户数据失败」

**安全可改的字符串：810 条**（纯中文+标点+20~600B+无英文标识符）

工具：`tools/s_safe.py`（--find / --check / --apply / --restore）

## 6.3 ★ 路 C：私有目录明文配置（**最有潜力，待验证**）

**位置**：
```
/data/data/<pkg>/files/VersionConfigs/BaseConfig/TapTap/TapTap_Branch/  (16 个)
/data/data/<pkg>/files/VersionConfigs/BaseConfig/TapTap/chillyroom/     (2 个)
/data/data/<pkg>/files/VersionConfigs/BaseConfig/Master/                (2 个)
/data/data/<pkg>/files/loc/                                             (2 个)
```

**15 个配置类（全部明文 JSON）**：

| 文件 | 大小 | configClassName | 内容 |
|---|---|---|---|
| `TrialKnightConfig.json` | 1695 B | NewDynamicConfig.TrialKnightConfig | **list[{CharacterId, SkillId, TimeStart, TimeEnd}]** |
| `ChillyTvConfig_CN.json` | 4589 B | ChillyTvConfig | showType/quanWatchTv/channelByOrder |
| `FishChipStoreConfig.json` | 3547 B | NewDynamicConfig.FishChipStoreConfig | 商店 |
| `SeasonStoreConfig.json` | 2644 B | NewDynamicConfig.SeasonStoreConfig | 赛季商店 |
| `DialogConfig.json` | 1906 B | NewDynamicConfig.DialogConfig | 对话框 |
| `SenstiveConfig.json` | 1490 B | NewDynamicConfig.SenstiveConfig | IsLockSandbox/LockReason |
| `AppFilingConfig.json` | 1210 B | NewDynamicConfig.AppFilingConfig | 备案 |
| `CloudSaveConfig.json` | 1017 B | NewDynamicConfig.CloudSaveConfig | 云存档 |
| `SelfCustomServiceConfig.json` | 508 B | NewDynamicConfig.SelfCustomServiceConfig | 客服 |
| `MultiGameConfig.json` | 253 B | NewDynamicConfig.MultiGameConfig | 联机 |
| `NewGiftConfig.json` | 244 B | NewDynamicConfig.NewGiftConfig | 新手礼包 |
| `HiringConfig.json` | 232 B | NewDynamicConfig.HiringConfig | 招募 |
| `SpecialBGMSkinConfig.json` | 223 B | NewDynamicConfig.SpecialBGMSkinConfig | BGM皮肤 |
| `GameEventConfig.json` | 202 B | RGScript.Util.NewConfigs.Configs.GameEventConfig | showGhostDog |
| `NetI2Config.json` | 190 B | NewDynamicConfig.NetI2Config | 网络 |

**每个 JSON 的结构**：
```json
{
  "configClassName": "NewDynamicConfig.XXX",
  "CompareValue": N,        ← 版本号
  "IsForceOrAdd": true,     ← 强制
  "CanMerge": false,
  "IsRelyLogin": true,
  ... 业务字段 ...
}
```

**★ 已验证的：** `manifest.json` 有 md5 校验，实测 **15/15 全对**。

**★ 当前卡点**：改了会被覆盖
```
[FileSync]Http Download: .../GameEventConfig.json
```
- 实测：改 `showGhostDog: false→true` + 同步 manifest md5 → 启动 → **被还原**
- **来源是 CDN**（我一度以为是 APK 内副本，但那次「断网」测试是无效的）

**★ 下一步**：用 `/etc/hosts` 挡住 `cdn.chillyroom.com`（**不要全断网，游戏必须联网登录**）

工具：`tools/s_pull_all.py`（root 全量拉取）/ `tools/s_cfgtest.py` / `s_cfgtest2.py`

## 6.4 路 D：官方自带离线实现（未利用）

`dump.cs` 里直接有：
```csharp
dump.cs:183423  public class EmptyLogin : ILogin {}   // 空实现
dump.cs:183427  public class EmptyInit  : Init  {}   // 空实现
```

`LifeCycleConfig` 的配置驱动字段：
```
StartStateInitAgentFullName   @0x18
NewAccountLoginAgentFullName  @0x20
```
运行时指向 `"RGScript.Util.LifeCycle.EmptyInit"` / `"...EmptyLogin"` 即切官方离线分支。

热更强更开关（全是可写静态字段）：
```
ForcePatchManager.needForceUpdateGame         @0x3D  = 0
ForcePatchManager.hasPassStartupProcess       @0x08  = 1
ForcePatchManager.lastCodeLubanCheckResult    @0x20  = type2
ForcePatchManager.needCheckDynamicConfigAfterEnterRoom @0x3C = 0
```

## 6.5 路 E：游戏内置调试后门（未利用）

**DebugUI 框架**（stringLiteral 里有）：
```
DebugUICanvas / DebugUIPersistentCanvas / Enable Debug Button 1
"DebugUI prefab is missing a DebugUIHandler for: "
"
"
类: DebugUI.Widget:33953 / Container:33944 / Foldout:33946 / HBox:33947
    VBox:33948 / Table:33950 / Button:33956 / Value:33957 / BoolField:33961
```

**Mock 系列**（dump.cs 里的类型，但多为空实现）：
```
Mock Login / Mock Logout / Mock FetchCloudSaveData / Mock SaveCloudSaveData
Mock PayInspect / Mock RestoreAllUnConsumeOrder（=恢复未消费订单=补发内购）
MockAgent:6904 / MockEventAdder:6905 / MockGameScene:6906
MockMapManager:6907 / MockRoomObjectManager:6908 / MockUICanvas:6910
MockOnlineGameService:11205 / MockLoginAndCloudSaveAgent:16531
```

**PlayerPrefs 键名**（在 stringLiteral 里）：
```
unlock_debug_tool          @0x26968F
useDebugConfigInBuild      @0x1EC9EEB
useDebugConfigInEditor     @0x1EC9ED4
OpenDebugConsole           @0x4DB255
```

**兑换码系统**：`RGSecretKeyList : Singleton<RGSecretKeyList>` 有 `List<RGSecretKey> secretkey_list @0x20`
（`RGSecretKey` = key_type/key/reward/expiry_date/hasOtherReward/hero/skinIndex/pickables）

## 6.6 路 F：云存档工具（现成的）

- [CRDE](https://crde.pages.dev/)：**活跃**，只需凉屋账号，不用 root，改存档/解锁角色皮肤
- [SKDEditor](https://github.com/HTCheater/SKDEditor)：已停止维护

---

# 第七部分：wiki 对照成果

## 7.1 数据获取 ✅

**灰机 wiki**（用户指定只要这个）：
```
站点: https://yqqs.huijiwiki.com
入口: Data: 命名空间（ns=3500），每页一个 JSON
访问: ★ curl_cffi + impersonate="chrome131" 绕过 Cloudflare
      （普通 requests/urllib → 403 "Enable JavaScript and cookies"）
下载: 834 页，0 失败
分类: weapon 571 / monster 191 / boss 51 / weapon_dxt 20
```

**可用指纹**：`chrome131` / `chrome124` / `chrome116` / `edge101` / `firefox133` / `safari17_0`
**不可用**：`chrome`（默认）/ `chrome120`

工具：`tools/s_huiji2.py`（--test / --list / --pull / --stats）

**数据存放**：`work/wiki_huiji/*.json`

**Fandom wiki**（辅助）：
```
站点: https://soul-knight.fandom.com
下载: 2003 页，1070 页含 infobox
存放: work/wiki/*.wiki
工具: tools/s_wiki.py / s_wikiparse.py
```

## 7.2 ★ 核心发现 1：Boss 血量差 1.5 倍

```
灰机 BossLife  =  游戏 Hp  ×  1.5
```

**反向验证（最强证据）**：46 个灰机 BossLife，每一个 ÷1.5 都能在游戏里找到对应值：
```
46 / 46 = 100%，0 例外
```
正向：游戏 62 个 Boss，×1.5 后 58 个命中（93.5%），4 个例外全是特殊 Boss。

**灰机页自述**：
> 「异世界传送专用黑卡车测量了所有 boss 的血量，确认为 **0.68/1/1.2**。
> 考虑到以后还有比例变动的可能，只上传一个血量然后计算。」

⇒ 它存的是**归一化基数**，游戏存的是某个难度的实际值。**同源，只是口径不同。**

**一一对应的例子**：

| 游戏 Key | Hp | ×1.5 | 灰机 BossId | NameEn |
|---|---|---|---|---|
| `boss_dead_cell_giant` | 512 | 768 | 11033 | The Giant |
| `boss22` | 460 | 690 | 11032 | Prehistoric Colossus |
| `boss23` | 530 | 795 | 11031 | Gold Mask |
| `boss28` | 1000 | 1500 | 13031 | Iron Pirate King Level |
| `boss29` | 2000 | 3000 | 13032 | Iron Will Wavebreaker |
| `boss24` | 690 | 1035 | 12111 | Cave Ice Bug |
| `boss18` | 820 | 1230 | 13013 | Anubis |

**4 个例外**：

| 游戏 Key | Hp | 说明 |
|---|---|---|
| `boss_bossrush_final_bad` | 2500 | 试炼模式终极 Boss，wiki 未收录 |
| `boss_dream_dog` | 16 | 梦境小狗，非战斗 Boss |
| `boss_fel_lord` | 5 | 邪王，血量占位值 |
| `boss_warlord` | 999999 | 占位值（实际血量由脚本控制） |

## 7.3 ★ 核心发现 2：小怪是双字段（不是 1.5 倍）

```
灰机:  Health = 8   EliteHealth = 32     ← 一个条目两个字段
游戏:  e_miner01  Hp=8                   ← 普通版
       ex_miner01 Hp=32                  ← 精英版（ex_ 前缀）
```

**是 1:1。**

| 对照 | 命中率 |
|---|---|
| `e_` ⨯ `Health` | **91.4%** |
| `ex_` ⨯ `EliteHealth` | **80.9%** |
| （错位）`e_` ⨯ `EliteHealth` | 明显更低 ✓ |
| （错位）`ex_` ⨯ `Health` | 明显更低 ✓ |

**命中样例**：

| 游戏基名 | 普通(游戏/灰机) | 精英(游戏/灰机) | MonsterId |
|---|---|---|---|
| alien01 | 25/25 ✓ | 48/48 ✓ | 30101 |
| boar01 | 8/8 ✓ | 26/26 ✓ | 10209 |
| battery01 | 15/15 ✓ | 60/60 ✓ | 20211 |
| dark_knight | 48/48 ✓ | 96/96 ✓ | 20110 |
| frog | 9/9 ✓ | 40/40 ✓ | 20308 |

## 7.4 编号映射规则

**灰机**（来自 `Project:Json数据` 页）：
```
BossId/MonsterId = <大关1><场景2><序号…>
第一大关：冰原01 森林02 遗迹03 机械遗迹04
第二大关：城堡01 地下城堡02 女巫森林03 奇械城04；溶洞11 沼泽12 墓穴13
第三大关：外星基地01 火山02 泥油海岛03
第四大关：远古山脉遗迹01 古战场02

Cellar（地窖）编号：A初武/手刀 B手枪 C步枪 D霰弹 E火箭 F激光
  G弓弩 H近战 I法杖 J投掷 K杂项 L指环 M传说 U不显示在地窖 V升级版
  例 B1101 = 手枪-白品-01
```

**游戏侧**：`enemies.csv` 的 `LevelKey` 形如 `1A`/`2B`/`3C`，
`BossRoom` 路径为 `Level/1/A/Boss/...` ⇒ 字母 A=场景01、B=02、C=03
⇒ 与灰机编号规则一致，可建立映射。

## 7.5 武器自洽性（灰机内部）

| 品质 | 数量 | 伤害范围 | 伤害均值 | 耗蓝均值 | 暴击均值 |
|---|---|---|---|---|---|
| 白色 | 150 | 0~12 | 4.6 | 1.8 | 8.6 |
| 绿色 | 119 | 1~14 | 5.3 | 1.5 | 15.8 |
| 蓝色 | 94 | 1~24 | 6.2 | 2.3 | 16.4 |
| 紫色 | 85 | 0~32 | 6.6 | 3.5 | 12.3 |
| 橙色 | 45 | 0~25 | 7.1 | 4.1 | 15.0 |

**品质越高伤害越高 → wiki 内部自洽 ✓**

**武器类型分布**：杂项125 / 近战110 / 机枪70 / 法杖48 / 激光39 / 投掷38 /
霰弹33 / 传说30 / 手枪26 / 弓24 / 火箭17 / 狙击枪14 / 弩10 / 指环7

**地窖编号分布**：A41 / B20 / C70 / D29 / E15 / F37 / G32 / H86 / I34 /
J28 / K94 / L4 / M30 / U5 / V42

## 7.6 角色映射（42 个）

**方法**：用 `skills.csv` 里的**资源路径**反查身份（游戏自己的数据，不是猜的）：
```
c22/skill 1 的 Value 含 "Skin/Character/Airbender/Skin_11/..."
⇒ c22 = Airbender（50 处路径证据）
```

**完整映射（c00~c41）**：

| 编号 | 内部名 | 有数值? | | 编号 | 内部名 | 有数值? |
|---|---|---|---|---|---|---|
| c00 | Knight | ✓ | | c21 | SpecialForces | ✓ |
| c01 | Ranger | ✓ | | c22 | Airbender | ✓ |
| c02 | Mage | ✓ | | c23 | Warlock | ✓ |
| c03 | Assassin | ✓ | | c24 | Miner | ✗ |
| c04 | Alchemist | ✓ | | c25 | TrapMaster | ✗ |
| c05 | Engineer | ✓ | | c26 | CostumePrince | ✗ |
| c06 | Vampire | ✓ | | c27 | Doctor | ✗ |
| c07 | Paladin | ✓ | | c28 | SwordMaster | ✗ |
| c08 | Elves | ✓ | | c29 | Lancer | ✗ |
| c09 | Werewolf | ✓ | | c30 | Warliege | ✗ |
| c10 | Priest | ✓ | | c31 | ArcaneKnight | ✗ |
| c11 | Druid | ✓ | | c32 | Astrologist | ✗ |
| c12 | Robot | ✓ | | c33 | Fighter | ✗ |
| c13 | Viking | ✓ | | c34 | Joker | ✗ |
| c14 | Necromancer | ✓ | | c35 | Bard | ✗ |
| c15 | Officer | ✓ | | c36 | Shooter | ✗ |
| c16 | Taoist | ✓ | | c37 | AiGirl | ✗ |
| c17 | Transcendent | ✓ | | c38 | Captain | ✗ |
| c18 | Envoy | ✓ | | c39 | YinYang | ✗ |
| c19 | ?（未识别） | ✓ | | c40 | GunsExpert | ✗ |
| c20 | Ninja | ✓ | | c41 | LadyChef | ✗ |

**⚠️ 重要**：`characters.csv` 只有 **c00~c23** 有数值，c24~c41 不在该表。
游戏实际有 **42 个角色**（`skills.csv` 里 c00~c41 完整）。真正的完整数值应在 `luban.bytes`。

## 7.7 wiki 对照结论

**没发现灰机有确凿的错误数据。能严格验证的部分都对得上。**

**唯一不一致**：Airbender
| | HP | 能量 | 暴击 | 近战 |
|---|---|---|---|---|
| 游戏 | 9 | 150 | 10 | 5 |
| Fandom | 4 | 160 | 15 | 5 |

**但不能断言 wiki 错** —— `characters.csv` 只有 24/42 个角色，真实值可能在 luban 里。

**报告**：`work/S_WIKI_FULL.md`（319 行）/ `S_HUIJI_CHECK.md` / `S_WIKI_CHECK.md`

## 7.8 wiki 对照的剩余缺口

| 缺口 | 原因 |
|---|---|
| 武器数值对照 | 游戏 `weapons.csv` **没有伤害列**，真值在未解密的 `luban.bytes` |
| 角色数值 | 灰机 **没有 character 类型**（只有 weapon/monster/boss） |
| 小怪变体 | 灰机按变体拆分（Miner 有 7 个 ID），游戏一行一个 |
| 小怪剩余 ~9% | 同上 |
| Boss 个体身份 | 游戏 Hp 大量重复（400 出现 15 次）→ 只能确定「候选组」 |

---

# 第八部分：当前卡点

```
                    ┌──────────────────────────────┐
   我们改的配置  →  │  FileSync 从 CDN 下回来覆盖   │  ← 卡在这
                    └──────────────────────────────┘
                                  ↑
                        [FileSync]Http Download
                                  ↑
                    ★ 不能全断网（游戏必须联网登录）
```

**正解**：用 `/etc/hosts` 只挡 `cdn.chillyroom.com`，放行登录服务器。

⚠️ **我犯过的错误**（避免重犯）：
- 我用 `ping` 判断网络 → **ping 不通 ≠ 没网**（环境屏蔽 ICMP 但放行 TCP）
- 我的「断网」命令**没生效**（`svc` 超时、`iptables` rc=1），但当时没检查返回值

---

# 第九部分：下一步任务

## P0：验证路 C（配置层）

```
1. 用 hosts 挡 CDN:
     echo "127.0.0.1 cdn.chillyroom.com" >> /system/etc/hosts
     （需重新挂载 /system 为 rw）
2. ★ 验证拦截真的生效（用 TCP 测，不要用 ping）
3. 改一个无害配置（GameEventConfig.showGhostDog）
4. 同步 manifest.json 的 md5
5. 启动游戏 → 看改动是否保住
```

**如果保住** → 配置层打开，15 个 JSON 都能改
**如果保不住** → 挖 `FileSync` 实现（用 `x_vautil.py` 反汇编）

## P1：luban.bytes 收尾

缺 `keySalt` 的 14 字节。位置已知（字段 #138），但 IL2CPP v29 不放在默认值表。

**方法**：读活进程内存（雷电 root）
```
got   = read8(bias + 0xFF232B8)      # 应为 bias + 0x1037D4C0
klass = read8(got)
sf    = read8(klass + 0xB8)
salt  = read8(sf + 8)                # byte[] 对象
长度  = read4(salt + 0x18)
数据  = read(salt + 0x20, 长度)
```
⚠️ 当前实测：GOT 槽读到 NULL（il2cpp 还没 lazy-resolve），addend 处是 token 不是指针

## P2：wiki 剩余工作

1. 小怪变体映射做细（补最后 9%）
2. Boss 身份唯一化（用 LevelKey + 数值双约束）
3. 写「元气骑士数据手册」（灰机 834 页 + 游戏 18 张表）

## P3：利用路 D / E

1. 找 `LifeCycleConfig` 的配置入口（切官方离线分支）
2. 触发 DebugUI（`unlock_debug_tool` 等 PlayerPrefs 键）
3. 研究兑换码系统

---

# 第十部分：资产清单

## 10.1 目录规模

```
tools        1.13 GB   1732 个文件
work         2.61 GB   5448 个文件
decompiled   0.84 GB   4807 个文件
mod          0.75 GB      3 个文件
```

**磁盘**：D: 50 GB / C: 11.4 GB / E: 23.1 GB / F: 48.8 GB

## 10.2 核心资产（必留）

| 路径 | 说明 |
|---|---|
| `work/cfgab/*.csv` | **游戏 18 张配置表**（2.23 MB，已解密） |
| `work/cfgab/xor_key_130.bin` | config.ab 的 XOR 密钥 |
| `work/modkit/mask.bin` | **metadata 加密掩码**（54 MB） |
| `work/modkit/global-metadata.orig.dat` | metadata 原始密文备份 |
| `work/deace/global-metadata_clean.dat` | metadata 解密后（55 MB） |
| `work/deace/libil2cpp_p3.so` | libil2cpp 解密后（278 MB，反汇编用） |
| `work/ilib_orig.so` | libil2cpp 原始版（278 MB） |
| `decompiled/dump_full/dump.cs` | **47,503 个类型定义**（26 MB） |
| `work/wiki_huiji/*.json` | **灰机 834 页**（835 个文件） |
| `work/luban_out/luban_schema.json` | 334 张表 / 2316 字段结构（453 KB） |
| `work/luban_key.txt` | luban 口令 = `AZhenSoulKnightd@orz` |
| `work/emu_save/*.json` | 雷电上解密的存档 |
| `work/gamedata/skpull/` | 真机拉的游戏数据（290 文件） |
| `work/gamedata_emu/` | 雷电拉的全部文件（349 个） |
| `work/save/*.json` | 真机存档解密后 |
| `work/save_backup/` | 存档备份 |
| `work/cfg_backup/` | 配置备份 |
| `5570618_20260905032720_Soul_Knight_release_chill.apk` | 原包（778 MB） |

## 10.3 核心工具（814 个 .py 里真正重要的）

| 工具 | 功能 |
|---|---|
| `s_saveedit.py` | ★ **存档编辑器**（--pull/--decrypt/--set/--unlock-all/--push/--restore） |
| `s_cfgdump.py` | ★ **config.ab 全量解密**（18 表） |
| `s_meta3.py` | ★ metadata 掩码加解密 + 结构解析 |
| `x_vautil.py` | ★ **IL2CPP 反汇编模块**（dis / find_va_kw / nm_of / callers_of） |
| `x_relakey.py` | ★ RELA 解析 + usage 解码（解出 luban 口令的） |
| `s_huiji2.py` | ★ **灰机抓取**（curl_cffi 绕 CF） |
| `s_wiki_full.py` | ★ wiki 对照报告生成 |
| `s_pull_all.py` | ★ root 全量拉取游戏数据 |
| `s_emu_save.py` | ★ 存档批量解密 |
| `s_decrypt_v2.py` | .so 周期对齐解密 |
| `s_safe.py` | 安全文字 MOD（metadata 字符串） |
| `s_cfgtest.py` | 配置修改持久化测试 |
| `s_minsdk.py` | APK AXML 解析 |
| `s_agre*.py` / `s_drive2.py` | 设备自动化（坐标系感知点击） |

⚠️ **`x_vautil.py` 依赖两个文件**（清盘时容易误删）：
```
work/deace/libil2cpp_p3.so        （278 MB，解密后）
work/deace/global-metadata_clean.dat（55 MB）
```
重建方法见 4.2 / 4.3 节。

---

# 第十一部分：血泪教训

## 11.1 测试方法学

| 坑 | 教训 |
|---|---|
| **`ps -A` 经 adb 偶发返回空** | 进程检测要三重冗余（ps -A / pidof / pgrep） |
| **不检查命令返回值** | `svc`/`iptables` 失败了我没发现，导致整轮测试无效 |
| **用 ping 判断网络** | ping 不通 ≠ 没网（屏蔽 ICMP 但放行 TCP），要用 TCP 测 |
| **截图 md5 判定 tap** | 按钮按下高亮也会让 md5 变 → 假阳性；要看元素是否消失 |
| **坐标系统一** | uiautomator 报**竖屏**坐标，`input tap` 用**横向 framebuffer** 坐标 |
| **回写前不备份** | 存档/配置改动前必须备份 |

## 11.2 代码坑

| 坑 | 说明 |
|---|---|
| **PowerShell 吃引号** | `python -c "..."` 里的引号会被吃掉 → 一律写成 .py 文件 |
| **PowerShell 无 heredoc** | 用 write 工具建文件 |
| **`$PID`/`$Args`/`$HOME` 是只读自动变量** | 赋值静默失败 |
| **PowerShell 管道会提前关闭子进程** | — |
| **`text=True` 破坏二进制** | 读设备文件必须用二进制 capture + base64 |
| **`dd bs=1` 遇段边界中断** | 改页对齐 `bs=4096 skip=ADDR/4096` |
| **`su 0 sh -c` 吞输出** | 每个参数分开传 |
| **中文路径下 aapt2/dexdump -o 可能失败** | — |
| **`os.remove()` 对 UnityPy 加载过的文件 PermissionError** | 句柄未释放 |

## 11.3 游戏机制坑

| 坑 | 说明 |
|---|---|
| **metadata 字符串混用两种用途** | 显示文本 ✅ / 序列化键名 ❌，改错破坏数据 |
| **metadata 改动必须等长** | 改长度会破坏后续索引 |
| **`.manifest` 的 `CRC:` 也在 ACE 清单里** | 不能改 |
| **`fields` 表 nameIndex 用字节偏移** | 不是序号 |
| **周期对齐不是绝对网格** | 槽位相对 0x10000 对齐 |
| **ACE 静默退出无 tombstone** | 用 `prctl(PR_SET_DUMPABLE,0)` 屏蔽 |
| **换包重装报 dexopt 错** | 先 `uninstall` 再 `install --no-incremental` |
| **模拟器 /data 满了 pm uninstall 也失败** | `-wipe-data` 冷启动 |

## 11.4 方法论教训

**最重要的是：验证要独立、要有对照。**

- 「原包在 AOSP 模拟器上也崩」这个对照实验，避免了把平台问题误判成我们的改动
- 「诱饵帧表 oracle」（10 帧逐字节相同）证明了崩溃是确定性的
- 「1.5 倍关系的反向验证 46/46」比正向更有说服力
- **但反过来**：我多次用不可靠的判据（ping、截图 md5、不检查返回值）得出过错误结论

---

# 第十二部分：关键约定

- **不要创建 git commit / push**
- **回复用简体中文**
- 改存档/配置前**必须备份**
- 测试前先确认设备在线、命令真的成功了
- 遇到不确定的，先做**对照实验**

---

# 附录：快速上手

## A.1 读游戏配置（最快的方式）

```bash
# 18 张表已经在本地了
ls work/cfgab/*.csv

# 查角色
python -c "import csv; [print(r) for r in csv.reader(open('work/cfgab/characters.csv',encoding='utf-8'))]"

# 查灰机 wiki
ls work/wiki_huiji/*.json
```

## A.2 改存档

```bash
python tools\s_saveedit.py --pull --decrypt    # 拉取+解密
python tools\s_saveedit.py --unlock-all        # 解锁
python tools\s_saveedit.py --encrypt --push    # 回写
```

## A.3 反汇编 IL2CPP

```python
import sys; sys.path.insert(0, r'D:\元气骑士mod\tools')
import x_vautil as V
V.dis(0x828747C, 40)          # 反汇编
V.find_va_kw('get_SecretKey')  # 按名字找
V.nm_of(0x828747C)             # 地址反查名字
```

## A.4 抓灰机 wiki

```bash
python tools\s_huiji2.py --test              # 测指纹
python tools\s_huiji2.py --imp chrome131 --pull  # 全量下载
```
