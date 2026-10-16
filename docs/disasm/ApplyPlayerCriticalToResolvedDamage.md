# ApplyPlayerCriticalToResolvedDamage / OnEnemyHit

**地址**：`0x06478B74` / `0x06478DA0`
**IFix fallback**：`0x06478BC4` / `0x06478DF0`

## 反汇编（两者 fallback 完全相同）

```asm
mov  x0, x21
mov  x1, x19
bl   #0x647861c        ; ★ ResolveWeaponLevel(bat, obj)
mov  w1, w20           ; 暴击标志
mov  x2, x19
mov  w3, w0            ; ★ w3 = 武器等级结果
mov  x0, x21
b    #0x6478XX0        ; → 主逻辑（w3 作为参数传入）
```

## 关键结论

**暴击伤害与武器等级强耦合** —— `ResolveWeaponLevel` 的结果参与暴击计算。

