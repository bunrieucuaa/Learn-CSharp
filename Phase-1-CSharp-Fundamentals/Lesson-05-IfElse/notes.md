# 📝 Lesson 05 — Notes: If/Else

---

## 🧠 Tóm tắt kiến thức

### Cú pháp:

```csharp
// if đơn
if (condition) { ... }

// if...else
if (condition) { ... } else { ... }

// if...else if...else
if (cond1) { ... }
else if (cond2) { ... }
else { ... }

// Ternary
var result = condition ? valueTrue : valueFalse;
```

### Pattern Matching (C# 9+):

```csharp
// Relational
if (age is >= 18 and < 65) { ... }

// Switch expression
string label = age switch
{
    < 18 => "Trẻ em",
    < 65 => "Người lớn",
    _ => "Cao tuổi"
};
```

### Guard Clause:

```csharp
if (badCondition) { return; }  // Thoát sớm
// Logic chính — không nested
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Thứ tự `else if` sai | Kiểm tra từ nghiêm ngặt nhất |
| 2 | Quên `{}` với nhiều dòng | LUÔN dùng `{}` |
| 3 | `=` thay vì `==` | C# báo lỗi compile (an toàn) |
| 4 | Nested if quá sâu | Guard clause / tách method |
| 5 | So sánh string không normalize | `.Trim().ToLower()` trước |

---

## ✅ Checklist

- [ ] Tôi biết cú pháp if, if-else, if-else if-else
- [ ] Tôi hiểu C# chỉ chấp nhận `bool` trong if (không truthy/falsy)
- [ ] Tôi biết kiểm tra từ điều kiện nghiêm ngặt nhất trong else if
- [ ] Tôi luôn dùng `{}` kể cả 1 dòng
- [ ] Tôi biết guard clause và khi nào dùng
- [ ] Tôi biết ternary operator và giới hạn của nó
- [ ] Tôi biết pattern matching `is`, `and`, `or`, switch expression
- [ ] Tôi biết tách điều kiện phức tạp thành biến có tên

---

## 🔗 Liên kết

```
Lesson 04: Input/Output
    ↓
Lesson 05: If/Else ← BẠN Ở ĐÂY
    ↓
Lesson 06: Switch (thay thế if/else khi so sánh giá trị cụ thể)
```

> 📌 **Bài tiếp:** Lesson 06 — Switch Case
