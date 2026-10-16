# GetCriticFactor（暴击总成）

**地址**：`0x062AFE24`
**IFix fallback**：`0x062AFE84`
**主体**：`0x062AFEB4`（栈帧 0x250 字节 + 23 个静态初始化）

## 调用链

```
0x062B00A8  bl  IsPlayerSideTarget      ; ① 判断玩家方
0x062B00D4  bl  set_Critic              ; ② 设置暴击标志
0x062B00F4  bl  get_ElementalType       ; ③ 元素类型
0x062B01FC  bl  get_Critic              ; ④ 读暴击标志
0x062B0250  bl  UseCache                ; ⑤ 缓存
0x062B028C  bl  get_finalDamage         ; ⑥ ★ 最终伤害
0x062B0298  bl  get_finalCritical       ; ⑦ ★ 最终暴击
0x062B0310  bl  AlwaysCritical          ; ⑧ 满血必暴击
0x062B0334  bl  get_Critic              ; ⑨ 再读
```

## 算术区（关键常量）

```asm
0x062B08C4  fmov  s1, #30.00000000      ; ★ 30.0（与精准暴击相关）
0x062B0900  fmov  s3, #30.00000000
```

## 关键结论

`GetCriticFactor` 是一个**聚合器**：从 23+ 个来源收集暴击因子。
`get_finalDamage` / `get_finalCritical` 是最终属性，
说明**暴击因子在伤害计算之前就已定档**。
