# 攻击速度公式

## `GetSelfAttackSpeedFactor` @ 0x068BD544 (fb 0x068BD57C)

```asm
fmov s0, #30.00000000          ; ★ 默认 30.0
mov  w1, #0xe                  ; ★ 槽位 14
b    #0x68bed28                ; GetPercentParam
```

## `ResolveWeaponSpeed` @ 0x0635C100

```
ResolveWeaponLevel → GetSpeed → fmax s0, s0, #0
```

**只有下限 `max(x, 0)`，无上限钳位。**

> wiki 说的「10%/1000% 钳位」在上层调用方，不在此函数。
