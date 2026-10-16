# ApplyNonCriticalDamageReduction

**地址**：`0x0647990C`
**IFix fallback**：`0x06479954`

## 反汇编

```asm
0x06479954  fcmp  s9, #0.0
0x06479958  b.ls  #0x6479984           ; 减伤率 <= 0 → 跳过
0x06479960  mvn   w8, w8
0x06479964  tst   w8, #0x7f800000      ; NaN/Inf 检查
0x06479968  b.eq  #0x6479984
0x0647996C  fmov  s0, #1.00000000
0x06479970  fmin  s1, s9, s0           ; ★ min(减伤率, 1.0) —— 钳位
0x06479974  fsub  s1, s0, s1           ; 1.0 - min(...) = 保留率
0x06479978  fcmp  s9, #0.0
0x0647997C  fcsel s0, s1, s0, pl
0x06479980  fmul  s8, s0, s8           ; 伤害 × 保留率
0x06479990  ret
```

## 还原公式

```c
float ApplyNonCriticalDamageReduction(float damage, float reduction)
{
    if (reduction <= 0.0f) return damage;
    if (isnan(reduction) || isinf(reduction)) return damage;
    float keep = 1.0f - fminf(reduction, 1.0f);   // ★ 钳位到 [0,1]
    return damage * keep;
}
```

## 关键结论

**减伤率被 `fminf(_, 1.0)` 钳位** → 减伤 200% 实际等于 100%（归零，不会变负）。
