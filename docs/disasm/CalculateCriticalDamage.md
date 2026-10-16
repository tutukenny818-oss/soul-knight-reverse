# CalculateCriticalDamage

**地址**：`0x06479754`
**IFix fallback**：`0x06479794`

## 反汇编

```asm
0x064797E4  cbz  x19, #0x6479828       ; 对象为空
0x064797F4  bl   #0x6c8be0c            ; GetValue(obj)
0x064797FC  fcmp s0, #0.0
0x06479804  b.le #0x647982c            ; 值 <= 0 → 返回 0.0
0x06479818  b.hi #0x647982c            ; NaN/Inf → 返回 0.0
0x06479828  fmov s0, #1.00000000       ; ★ 默认倍率 1.0
```

## 还原公式

```c
float CalculateCriticalDamage(DamageObject* self, float inDamage)
{
    if (self == NULL) return 1.0f;        // ★ 默认 1.0，不是 2.0
    float v = GetValue(self, 0, NULL);
    if (v <= 0.0f) return 0.0f;           // 暴击反而不增伤
    if (isnan(v) || isinf(v)) return 0.0f;
    return v;
}
```

## 关键结论

- **代码里没有 2.0 / 200 这个常数**
- 200% 是**数据驱动的配置值**（在每个武器的配置里）
- 这解释了 wiki 为什么写「**通常**为 200%」
