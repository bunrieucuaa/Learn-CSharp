# 📝 Lesson 07 — Notes: Loop

---

## 🧠 Tóm tắt

### 4 loại vòng lặp:

```csharp
// for — biết trước số lần
for (int i = 0; i < n; i++) { ... }

// while — lặp khi điều kiện đúng
while (condition) { ... }

// do-while — chạy ít nhất 1 lần
do { ... } while (condition);

// foreach — duyệt collection (chỉ đọc)
foreach (var item in collection) { ... }
```

### break vs continue:

```
break    = THOÁT vòng lặp hoàn toàn
continue = BỎ QUA lần lặp này, sang lần tiếp
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Vòng lặp vô tận (quên cập nhật) | Luôn có biến thay đổi điều kiện |
| 2 | Off-by-one (`<` vs `<=`) | Xác định rõ: bắt đầu/kết thúc ở đâu |
| 3 | Sửa collection trong foreach | Dùng `for` nếu cần sửa |
| 4 | `break` chỉ thoát 1 vòng | Dùng flag hoặc `return` cho nested |
| 5 | Nested loop > 3 cấp | Tách method |

---

## ✅ Checklist

- [ ] Biết `for` / `while` / `do-while` / `foreach`
- [ ] Biết `break` (thoát) vs `continue` (bỏ qua)
- [ ] Biết vòng lặp lồng nhau (nested loop)
- [ ] Biết tránh vòng lặp vô tận
- [ ] Biết khi nào dùng `for` vs `foreach`
- [ ] Có thể viết Fibonacci, kiểm tra số nguyên tố
- [ ] Có thể dùng vòng lặp cho menu tương tác

---

## 🔗 Liên kết

```
Lesson 06: Switch
    ↓
Lesson 07: Loop ← BẠN Ở ĐÂY
    ↓
Lesson 08: Methods (tách logic vòng lặp thành hàm tái sử dụng)
```
