# 📝 Lesson 09 — Notes: Scope & Lifetime

---

## 🧠 Tóm tắt

### Scope levels:

```
Class scope    → static fields, const → cả class thấy
Method scope   → local variables, params → chỉ trong method
Block scope    → biến trong if/for/while → chỉ trong {}
```

### Quy tắc vàng:

```
Biến chỉ sống trong cặp {} chứa nó.
Khai báo ở scope NHỎ NHẤT có thể.
```

### Value vs Reference:

```
Value (int, double, bool, struct):
  → Truyền vào method = COPY → sửa không ảnh hưởng gốc

Reference (array, class, string*):
  → Truyền vào method = THAM CHIẾU → sửa ảnh hưởng gốc
  * string immutable → sửa tạo object mới, gốc không đổi
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Dùng biến ngoài scope | Khai báo trước block |
| 2 | Quá nhiều class-level vars | Ưu tiên local + param |
| 3 | Quên value type = copy | Dùng `ref`/`out` nếu cần sửa |
| 4 | Biến loop dùng sau loop | Khai báo trước vòng lặp |
| 5 | Nhầm string sửa được | String immutable! |

---

## ✅ Checklist

- [ ] Biết 3 loại scope: class / method / block
- [ ] Biết biến chỉ sống trong `{}` chứa nó
- [ ] Biết C# không cho shadow biến local trong block con
- [ ] Biết value type truyền copy, reference type truyền tham chiếu
- [ ] Biết string là reference nhưng immutable
- [ ] Biết dùng `const` cho hằng số
- [ ] Biết khai báo biến ở scope nhỏ nhất có thể
- [ ] Biết khi nào dùng class-level vs local

---

## 🔗 Liên kết

```
Lesson 08: Methods
    ↓
Lesson 09: Scope ← BẠN Ở ĐÂY
    ↓
Lesson 10: String (xử lý chuỗi nâng cao)
```
