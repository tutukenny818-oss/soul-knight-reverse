# CalculateFinalDamage

**地址**：`0x0666213C`
**IFix fallback**：`0x0666218C`

## 反汇编

```asm
0x0666218C  cbz   x19, #0x666219c      ; this == NULL → return 0
0x06662190  cbz   x20, #0x66621a4      ; 倍率对象 == NULL → ratio = 1.0
0x06662194  ldr   s0, [x20, #0x28]     ; ratio = 倍率对象[0x28]
0x066621A4  fmov  s0, #1.00000000      ; ratio = 1.0
0x066621A8  ldr   s1, [x19, #0x3c]     ; base = this[0x3C]
0x066621B8  fmax  s8, s0, s2           ; ★ m = fmaxf(ratio, 0.0)
0x066621BC  scvtf s9, s1               ; base_f = (float)base
0x066621E0  fmul  s9, s8, s9           ; v = m * base_f
0x06662200  bl    #0x6231a14           ; libm round()
; —— 银行家舍入 ——
0x06662238  fcvtzs x8, d0
0x0666223C  fadd   d1, d0, d1          ; +1.0
0x06662240  tst    x8, #1              ; ★ 奇偶判定
0x06662244  fcsel  d0, d0, d1, eq      ; 偶不动，奇 ±1
0x06662278  bic    w0, w8, w8, asr #31 ; ★ 清符号位 = max(0, x)
```

## 还原公式

```c
int CalculateFinalDamage(DamageObject* self, DamageRatio* ratioObj)
{
    if (self == NULL) return 0;
    float ratio = (ratioObj == NULL) ? 1.0f : *(float*)(ratioObj + 0x28);
    float base  = (float)*(int*)(self + 0x3C);
    float v = fmaxf(ratio, 0.0f) * base;
    // round half to even
    double d = (double)v;
    ...
    return max(0, (int)d);
}
```

## 关键结论

| 项 | 值 |
|---|---|
| 字段 `this+0x3C` | 基础伤害 int32 |
| 字段 `ratioObj+0x28` | 倍率 float32 |
| 取整 | **银行家舍入**（非向下取整） |
| 下限 | 0（`bic` 清符号位） |
