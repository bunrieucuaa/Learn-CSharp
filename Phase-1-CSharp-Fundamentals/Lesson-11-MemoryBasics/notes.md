# 📝 Lesson 11 — Notes: Memory Basics

---

## 🧠 Tóm tắt

### Stack vs Heap:

```
Stack: nhỏ, nhanh, tự dọn, LIFO
  → value types, local vars, params

Heap: lớn, chậm hơn, GC dọn
  → reference types, objects, arrays, strings
```

### Value vs Reference:

```csharp
// VALUE TYPE — copy giá trị
int a = 10;
int b = a;     // b = 10 (bản riêng)
b = 99;        // a vẫn = 10

// REFERENCE TYPE — copy địa chỉ (share data)
int[] x = {1,2,3};
int[] y = x;   // y trỏ cùng data
y[0] = 99;     // x[0] cũng = 99!

// CLONE — copy data thật
int[] z = (int[])x.Clone();  // z riêng biệt
```

### Truyền vào method:

```
Value type         → copy       → gốc KHÔNG đổi
Value + ref        → tham chiếu → gốc ĐỔI
Reference + modify → share data → gốc ĐỔI
Reference + reassign → thay copy ref → gốc KHÔNG đổi
```

### Null:

```csharp
?.   // null-conditional: obj?.Prop
??   // null-coalescing: val ?? default
??=  // null-coalescing assign: val ??= default
int? // nullable value type
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Copy array = share data | Dùng `.Clone()` hoặc `Array.Copy` |
| 2 | NullReferenceException | `?.` và `??` trước khi truy cập |
| 3 | String tưởng sửa được | Immutable — gán lại: `s = s.ToUpper()` |
| 4 | Reassign trong method = không đổi gốc | Dùng `ref` nếu cần |
| 5 | Boxing tốn performance | Tránh dùng `object` khi biết kiểu |

---

## ✅ Checklist

- [ ] Biết Stack vs Heap — mỗi cái chứa gì
- [ ] Biết value type copy giá trị, reference type copy địa chỉ
- [ ] Biết array gán `=` là share, `.Clone()` là copy thật
- [ ] Biết string immutable (reference type nhưng hành vi như value)
- [ ] Biết null, `?.`, `??`, `??=`, `int?`
- [ ] Biết GC tự dọn Heap
- [ ] Biết boxing/unboxing và tránh dùng `object`
- [ ] Biết 4 cách truyền vào method và kết quả

---

## 🔗 Liên kết

```
Lesson 10: Static
    ↓
Lesson 11: Memory Basics ← BẠN Ở ĐÂY
    ↓
Lesson 12: Arrays
    ↓
Lesson 13: String
    ↓
Lesson 14: Exception Handling
```
