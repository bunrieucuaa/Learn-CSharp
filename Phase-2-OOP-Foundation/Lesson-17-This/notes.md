# 📝 Lesson 17 — Notes: `this`

---

## 🧠 Tóm tắt

```csharp
this.field = param;       // Phân biệt field/param trùng tên
this(args);               // Constructor chaining
return this;              // Fluent API / Method chaining
other.Method(this);       // Truyền object hiện tại
```

### Quy tắc:
- `this` = object hiện tại (cố định, khác JS)
- BẮT BUỘC khi field/param trùng tên
- BỎ được khi không cần (cho gọn)

---

## ✅ Checklist

- [ ] Biết `this` trỏ đến instance hiện tại
- [ ] Biết dùng `this.` khi field/parameter trùng tên
- [ ] Biết `this(...)` cho constructor chaining
- [ ] Biết `return this` cho Fluent API
- [ ] Biết C# `this` khác JS `this` (cố định vs thay đổi context)

## 🔗 Liên kết

```
Lesson 16: Constructor → Lesson 17: this ← ĐÂY → Lesson 18: Access Modifier
```
