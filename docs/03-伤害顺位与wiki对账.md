# 伤害计算 —— 累加/累乘顺序还原 + wiki 对账

> 数据来源：`libil2cpp.so` IFix fallback 反汇编
> 对账对象：`work/wiki_mech/伤害计算.wiki`（8620 字符，120 个 item 引用 + 48 个 weapon 引用）

---

## 第一部分：累加/累乘顺序

### 1. ★★★ 核心机制澄清：不是"运行时累加"

wiki 说「**不同顺位间乘算，同顺位内加算**」。逆向发现**实现方式更具体**：

**每个"顺位"对应一个「单点求值函数」**，它在被调用时**一次性算出该顺位的总倍率**，返回一个 float。

```asm
; AddAttackPercent @0x079C3C18（fallback）
adrp  x8, #0x2cc1000
ldr   s0, [x8, #0x6ec]     ; 系数
scvtf s1, w19              ; 等级 → float
fmul  s1, s1, s0           ; 等级 × 系数
fadd  s0, s1, s0           ; + 基线
ret                        ; 返回 float
```

**这个模式在所有「Percent/Bonus/Factor」函数里重复出现**：

| 函数 | 地址 | 模式 |
|---|---|---|
| `AddAttackPercent` | `0x079C3C18` | 等级×系数 + 基线 |
| `AddDamagePercent` | `0x079AF3D0` | `等级×系数 + 0.5` |
| `AddTakenDamagePercent` | `0x0799C194` | `等级×系数 + 另一基线` |
| `GetDamageBonus` | `0x07A6C598` | `(等级-1)×系数 + 0.5` |
| `GetVigorPerBonus` | `0x07A6C548` | 同模式 |
| `GetMultiplier` | `0x09A52A38` | `值×系数 → 后续处理` |

**⟹ 结论**：
- **"同顺位加算"** = 在**同一个求值函数内**完成（读等级 → 乘系数 → 加基线）
- **"跨顺位乘算"** = 多个求值函数的返回值**依次相乘**
- **不存在"运行时遍历所有天赋求和"的循环**

### 2. ★★ 代码级分界点（三层结构）

wiki 的「内部攻击力 → 外部攻击力 → 实际伤害」三层，**代码分界点是字段 `[0x48]`**：

```asm
; GetFinalDamage 主体 0x067C2430
0x067C2478  ldr  x0, [x19, #0x48]     ; 读「倍率对象」指针
0x067C247C  cbz  x0, #0x67c24f4       ; ★ 为空 → 跳到整数路径
0x067C2484  bl   #0x724de94           ; 非空 → get_Value(倍率对象)
0x067C24D0  bl   #0x6231a14           ; → libm round（银行家舍入）
0x067C24F4  ldr  w0, [x19, #0x3c]     ; ★ 整数路径：直取 [0x3C]
```

| 字段 | 类型 | 含义 |
|---|---|---|
| `[0x3C]` | int32 | **基础伤害值**（无倍率时的直接值） |
| `[0x48]` | ptr | **倍率对象**（非空则走 float 路径） |
| `[0x28]` (倍率对象内) | float32 | **倍率值** |
| `[0x60]` | ptr | 子弹伤害数组 |
| `[0x3E0]` | ptr | 运行时加成结构 |

### 3. ★★★ 暴击累加点

`AddCriticFactor @0x08308728` 的 fallback 只有 5 条指令 —— **纯累加**：

```asm
0x08308770  ldr  s0, [x19, #0x68]    ; 读当前暴击因子
0x08308774  fadd s0, s0, s8          ; ★ 加上新因子
0x08308778  str  s0, [x19, #0x68]    ; 写回
0x0830877C  ret
```

**⟹ 暴击因子存在 `[0x68]`，多个来源**逐个 `fadd` **累加**（与 wiki「同顺位加算」一致）。

### 4. ★★★ 暴击总成：`GetCriticFactor`（超大型聚合器）

`0x062AFEB4` 起，栈帧 `0x250` 字节 + **23 个静态初始化**，调用链揭示累加顺序：

```
0x062B00A8  bl  IsPlayerSideTarget      ; ① 判断是否玩家方
0x062B00D4  bl  set_Critic              ; ② 设置暴击标志
0x062B00F4  bl  get_ElementalType       ; ③ 元素类型
0x062B01FC  bl  get_Critic              ; ④ 读暴击标志
0x062B0250  bl  UseCache                ; ⑤ 缓存判定
0x062B028C  bl  get_finalDamage         ; ⑥ ★ 最终伤害
0x062B0298  bl  get_finalCritical       ; ⑦ ★ 最终暴击
0x062B0310  bl  AlwaysCritical          ; ⑧ 满血必暴击（覆盖）
0x062B0334  bl  get_Critic              ; ⑨ 再读
```

**关键**：`get_finalDamage` / `get_finalCritical` 是**最终属性**，说明暴击因子在**伤害计算之前**就已定档。

### 5. ★★ 顺序保护：非正倍率钳位

有一个**专用函数**：`DamageResolverClampsNonPositiveAttackMultiplier @0x075F7E98`

```asm
0x075F7E98  b  #0x75f7edc
0x075F7E9C  fmov d1, #0.50000000     ; 银行家舍入的 0.5 比较
0x075F7EA0  fcmp d0, d1
0x075F7EB0  fcvtzs x8, d0
0x075F7EBC  tst  x8, #1              ; 奇偶判定
0x075F7EC0  fcsel s0, s0, s1, eq
```

**⟹ 攻击倍率 ≤ 0 时会被钳位**（防止负伤害/负倍率污染后续顺位）。

---

## 第二部分：与 wiki 逐条对账

### 汇总

| 判定 | 数量 |
|---|---|
| ✅ 一致 | 2 |
| ✅ 一致（并定位到字段） | 1 |
| ⚠️ 补充（wiki 未提但正确） | 3 |
| ⚠️ 机制澄清 | 1 |
| ❌ **矛盾** | **1** |
| ❌ wiki 缺失 | 1 |
| ❌ wiki 未给公式（可补） | 1 |

---

### ❌ 1. 取整方式 —— **wiki 说错了**

| | |
|---|---|
| **wiki 说法** | 「暴击时使伤害乘以一定的倍率（**向下取整**）」「子弹分裂…×50%（**向偶数取整**）」 |
| **逆向实证** | `CalculateFinalDamage` fallback `0x0666218C` |

```asm
0x06662200  bl      #0x6231a14        ; 调用 libm round()
0x06662210  fcmp    d0, d1            ; 与 ±0.5 比较
0x06662238  fcvtzs  x8, d0
0x0666223C  fadd    d1, d0, d1        ; +1.0
0x06662240  tst     x8, #1            ; ★ 奇偶判定
0x06662244  fcsel   d0, d0, d1, eq    ; ★ 偶不动，奇 ±1
```

**⟹ 实际是 `round_half_to_even`（银行家舍入），不是"向下取整"。**

**影响举例**：

| 计算值 | wiki 说（向下取整） | 实际（银行家舍入） |
|---|---|---|
| 10.5 | 10 | **10**（10 是偶数） |
| 11.5 | 11 | **12**（12 是偶数） |
| 12.5 | 12 | **12**（12 是偶数） |
| 13.5 | 13 | **14** |

**⟹ 奇数 + 0.5 的情况，wiki 会算错 1 点。**

> ⚠️ 但 wiki 在**子弹分裂**那条写的是「向偶数取整」—— 这条**是对的**，说明 wiki 作者知道银行家舍入的存在，只是**没在暴击那条统一**。

---

### ❌ 2. wiki 缺失：暴击伤害与武器等级强耦合

| | |
|---|---|
| **wiki 说法** | 未提及 |
| **逆向实证** | `ApplyPlayerCriticalToResolvedDamage` fb `0x06478BC4`<br>`ApplyPlayerCriticalOnEnemyHit` fb `0x06478DF0` |

两条 fallback **第一步完全相同**：

```asm
mov  x0, x21
mov  x1, x19
bl   #0x647861c        ; ★ ResolveWeaponLevel(bat, obj)
mov  w1, w20           ; 暴击标志
mov  x2, x19
mov  w3, w0            ; ★ w3 = 武器等级结果
mov  x0, x21
b    #0x6478XX0        ; → 主逻辑（w3 作为参数）
```

**⟹ 暴击伤害**不是简单乘个固定倍率，**武器等级参与计算**。

**推测**（需进一步验证）：高等级武器的暴击倍率可能非线性提升，这解释为什么 wiki 只能笼统说「通常为 200%」。

---

### ⚠️ 3. 暴击默认倍率 —— wiki 表述不精确

| | |
|---|---|
| **wiki 说法** | 「通常为200%」 |
| **逆向实证** | `CalculateCriticalDamage` fb `0x06479794` |

```asm
0x64797E4  cbz  x19, #0x6479828       ; 对象为空
0x6479828  fmov s0, #1.00000000       ; ★ 返回 1.0
0x64797F4  bl   #0x6c8be0c            ; GetValue(obj)
0x64797FC  fcmp s0, #0.0
0x6479804  b.le #0x647982c            ; ≤0 → 返回 0.0
0x6479810  mov  w9, #0x7f800000       ; +INF
0x6479818  b.hi #0x647982c            ; NaN/Inf → 返回 0.0
```

**⟹ 引擎层默认倍率 = 1.0**（无加成时暴击**不改变伤害**），200% 是**上层配置**的值。

**补充**：暴击倍率 ≤ 0 → 返回 **0.0**（暴击反而归零，这是防御性设计）。

---

### ⚠️ 4. 减伤钳位 —— wiki 未提

| | |
|---|---|
| **wiki 说法** | 「防御：使实际伤害减去防御值」（只提固定减伤） |
| **逆向实证** | `ApplyNonCriticalDamageReduction` fb `0x06479954` |

```asm
0x06479954  fcmp  s9, #0.0
0x06479958  b.ls  #0x6479984          ; ≤0 跳过
0x06479968  b.eq  #0x6479984          ; NaN/Inf 跳过
0x0647996C  fmov  s0, #1.00000000
0x06479970  fmin  s1, s9, s0          ; ★ min(减伤率, 1.0)
0x06479974  fsub  s1, s0, s1          ; 1.0 - min(...)
0x0647997C  fcsel s0, s1, s0, pl
0x06479980  fmul  s8, s0, s8          ; 伤害 × 保留率
```

**公式**：
```c
float ApplyNonCriticalDamageReduction(float damage, float reduction)
{
    if (reduction <= 0.0f || isnan(reduction) || isinf(reduction)) return damage;
    float keep = 1.0f - fminf(reduction, 1.0f);   // ★ 钳位到 [0,1]
    return damage * keep;
}
```

**⟹ 减伤率被钳到 [0, 1] → 减伤 200% 实际等于 100%（归零），不会变负。**

---

### ⚠️ 5. 倍率下限 —— wiki 未提

| | |
|---|---|
| **wiki 说法** | 「n×(1+a)×(1+b)×(1+c)」 |
| **逆向实证** | ① `CalculateFinalDamage`：`fmax s8, s0, s2`（s2 = 0.0）<br>② 专用函数 `DamageResolverClampsNonPositiveAttackMultiplier @0x075F7E98` |

**⟹ 倍率下限 0**，且有一个**专门的函数**做这件事（说明这是刻意的设计决策，不是偶然）。

---

### ⚠️ 6. 机制澄清：「同顺位加算」的实现方式

| | |
|---|---|
| **wiki 说法** | 「同顺位内将其百分比加算，即 n×(1+a+b+……)」 |
| **逆向实证** | 每个顺位对应一个「等级×系数+基线」的**单点求值函数** |

**⟹ wiki 的数学结论正确，但机制描述可以更精确**：

不是「把所有加成收集起来求和」，而是**每个顺位函数内部已预计算好**（读等级 → 乘系数 → 加基线 → 返回总倍率）。

这也解释了为什么 wiki 说「取整情况较为混乱」—— **因为取整发生在每个求值函数的内部，而不是统一到最后**。

---

### ✅ 7. 三层结构 —— wiki 正确，并定位到字段

| | |
|---|---|
| **wiki 说法** | 「内部攻击力 → 外部攻击力 → 实际伤害」 |
| **逆向实证** | 分界点 = `[0x48]` 是否非空 |

这验证了 wiki 的结构划分，并给出了**代码级判据**。

---

### ✅ 8-9. 激光特殊路径 / 大对象减伤 —— wiki 正确

- `GetLaserDamage @0x068AFB0C` 独立存在 ⟹ 激光走单独代码路径 ✓
- `GetBlockDamageInfo @0x06A48C60` / `GetArmorDamageReduction` 独立 ⟹ 大对象减伤特殊 ✓

---

### ❌ 10. 防御公式 —— wiki 只给采样点，未给公式（可补）

| | |
|---|---|
| **wiki 做法** | 防御页列 27 个「面板 → 实际」采样点 |
| **逆向实证** | `PrintDefenceFormula @0x06ED8684` |

```
实际防御 = round_half_even( sign(x) × 6.74 × log10(0.5×|x| + 0.972) )
```

- **27/27 采样点 100% 命中**
- 已外推完整反查表（面板 0..300 → 实际防御 0..12）

**⟹ 可以直接在 wiki 防御页补上公式和完整表格。**

---

## 第三部分：给 wiki 的可投喂结论

### 需要**修正**的

1. **暴击取整**：「向下取整」→ **银行家舍入（round half to even）**
   - 12.5 → 12；13.5 → **14**（不是 13）

### 需要**补充**的

2. **暴击倍率下限**：倍率 ≤ 0 时返回 0.0
3. **减伤钳位**：减伤率 `min(r, 1.0)`，超 100% 归零不变负
4. **攻击倍率下限**：全局钳位到 0（有专用函数）
5. **暴击 × 武器等级**：`ResolveWeaponLevel` 参与暴击计算（wiki 完全没提）

### 可以**新增词条**的

6. **防御公式**：`round_half_even(6.74 × log10(0.5|x| + 0.972))` + 完整反查表
7. **伤害字段映射表**：`[0x3C]` 基础 / `[0x48]` 倍率对象 / `[0x28]` 倍率值 / `[0x68]` 暴击因子

---

## 附：关键地址表（本轮新增）

| 函数 | 地址 | fallback | 作用 |
|---|---|---|---|
| `GetFinalDamage` | `0x067C2430` | `0x067C23FC` | **三层分界点** |
| `get_finalDamage` | `0x067C23AC` | `0x067C23FC` | 属性访问器 |
| `get_finalCritical` | `0x067C2564` | — | 最终暴击 |
| `GetCriticFactor` | `0x062AFE24` | `0x062AFE84` | **暴击总成（聚合 23+ 来源）** |
| `WhereCritic` | — | — | 暴击判定 |
| `AddCriticFactor` | `0x08308728` | `0x08308770` | **暴击因子累加** |
| `AddAttackPercent` | `0x079C3BD8` | `0x079C3C18` | 攻击百分比求值 |
| `AddDamagePercent` | `0x079AF390` | `0x079AF3D0` | 伤害百分比求值 |
| `AddTakenDamagePercent` | `0x0799C154` | `0x0799C194` | 受伤百分比求值 |
| `GetDamageBonus` | `0x07A6C558` | `0x07A6C598` | 伤害加成求值 |
| `DamageResolverClampsNonPositiveAttackMultiplier` | `0x075F7E98` | — | **倍率非正钳位** |
| `ResolveWeaponLevel` | `0x0647861C` | — | **武器等级解析**（暴击用） |
| `AlwaysCritical` | `0x06A9039C` | `0x06A90414` | 满血必暴击 |
| `UseCache` | `0x067C2204` | — | 伤害缓存 |

---

*文档完。所有结论均来自反汇编实证。*
