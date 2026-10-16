# 技能急速冷却公式

## `ExistingAdditiveRateAndSkillHasteRemainEffective` @ 0x075FA218 (fb 0x075FA290)

```asm
0x075FA2B0  adrp x8, #0x2cc1000
0x075FA2B4  ldr  s0, [x8, #0x6e8]     ; ★ 0x2CC16E8 = 0.05
0x075FA2B8  mov  w1, #8               ; ★ 槽位 8
0x075FA2C4  bl   #0x6c8b568           ; SetBaseValue(obj, 8, 0.05)

0x075FA2C8  adrp x8, #0x2cc1000
0x075FA2CC  ldr  s0, [x8, #0x6ec]     ; ★ 0x2CC16EC = 0.02
0x075FA2D0  mov  w1, #3               ; ★ 槽位 3
0x075FA2DC  bl   #0x6c8b568           ; SetBaseValue(obj, 3, 0.02)

0x075FA2E0  fmov s0, wzr              ; 0.0
0x075FA2E4  mov  w1, #5               ; ★ 槽位 5
0x075FA2F0  bl   #0x6c8b568           ; SetBaseValue(obj, 5, 0.0)
```

## `UsesSkillHasteCooldown` @ 0x07FC1930 (fb 0x07FC1968)

```asm
0x07FC1968  sub  w8, w19, #1
0x07FC196C  cmp  w8, #2
0x07FC1970  cset w0, lo               ; (type-1) < 2
0x07FC1978  ret
```

**⟹ 只有 type ∈ {1,2,3} 的技能使用技能急速公式。**

## 常量

| 地址 | 值 |
|---|---|
| 0x2CC16E8 | 0.05 |
| 0x2CC16EC | 0.02 |
| 0x2CC16F0 | 65535.0 |
| 0x2CC16E4 | 1.055 |
| 0x2CC16E0 | 0.949237 |
