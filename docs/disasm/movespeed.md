# 移动速度公式

## `AddMoveSpeedPercent` @ 0x069DA1A4 (fb 0x069DA1DC)

```asm
0x069DA1DC  mov  w0, #0x14            ; ★ 0x14 = 20
0x069DA1E0  ldp  x30, x19, [sp], #0x10
0x069DA1E4  ret
```

## `GetSelfMoveSpeedFactor` @ 0x068BDABC (fb 0x068BDAF4)

```asm
0x068BDAF4  fmov s0, #15.00000000     ; ★ 默认 15.0
0x068BDAF8  mov  w1, #0xf             ; ★ 槽位 15
0x068BDB04  b    #0x68bed28           ; GetPercentParam
```

## `ResolveMoveSpeedBonus` @ 0x0692CBDC (fb 0x0692CC1C)

```asm
0x0692CC24  bl   #0x692a638           ; TryGetConfig
0x0692CC34  ldr  s0, [x8, #0xcac]     ; ★ 0x2CC1CAC = 0.3
0x0692CC40  bl   #0x692a76c           ; ParsePositiveParam（保证非负）
0x0692CC48  ldr  s0, [x8, #0xcac]     ; 配置缺失也用 0.3
```

## `ResolveMoveSpeedLoss` @ 0x0692C808 (fb 0x0692C848)

```asm
0x0692C860  ldr  s0, [x8, #0x24]      ; ★ 0x2CC1024 = 0.15
0x0692C86C  bl   #0x692a76c           ; ParsePositiveParam
```

## 三段式

```
移速 = 基础 × (1 + Bonus(0.3) - Loss(0.15)) × 因子(默认 15.0)
```
