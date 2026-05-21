# 📝 Lesson 10 — Notes: Static

---

## 🧠 Tóm tắt

### Static = thuộc CLASS, không thuộc object

```csharp
// Static field — 1 bản duy nhất, chia sẻ
static int count = 0;

// Static method — gọi bằng ClassName.Method()
static int Add(int a, int b) => a + b;

// Static class — không thể tạo object (new)
static class Helper { ... }

// Static constructor — chạy 1 lần khi class được dùng lần đầu
static ClassName() { ... }

// const — static ngầm định, giá trị cố định compile-time
const double PI = 3.14159;
```

### Quy tắc truy cập:

```
Static method  → chỉ thấy static members
Instance method → thấy CẢ static + instance
```

### Memory:

```
Static Area: tạo khi app chạy, hủy khi app kết thúc
Stack: biến local, tạm thời
Heap: objects (tạo bằng new) — sẽ học ở OOP
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Static method truy cập instance field | Không được — phải truyền qua parameter |
| 2 | New static class | Không được — `static class` không tạo object |
| 3 | Quá nhiều static field (global state) | Hạn chế — dùng cho config/counter |
| 4 | Nhầm static field = local var | Static TÍCH LŨY, local RESET mỗi lần gọi |
| 5 | Dùng static khi cần OOP | Chuyển sang class + object ở Phase 2 |

---

## 📊 Khi nào dùng Static?

| Dùng Static ✅ | Không dùng ❌ |
|---------------|--------------|
| Utility/Helper methods | Logic liên quan đến data cụ thể |
| Config constants | State phức tạp (nên dùng OOP) |
| Counter, ID generator | Khi cần nhiều instances |
| Logger (toàn app) | Khi cần inheritance |
| Extension methods | Khi cần dependency injection |

---

## ✅ Checklist

- [ ] Hiểu static = thuộc class, không thuộc object
- [ ] Biết static field chia sẻ, tích lũy giữa các lần gọi
- [ ] Biết static method gọi bằng `ClassName.Method()`
- [ ] Biết static method KHÔNG thể truy cập instance members
- [ ] Biết static class KHÔNG thể tạo object
- [ ] Biết static constructor chạy 1 lần duy nhất
- [ ] Biết const là static ngầm định
- [ ] Biết khi nào nên/không nên dùng static
- [ ] Biết tổ chức code bằng static classes (Config/Data/UI/Logic)
- [ ] Hiểu memory: static area vs stack vs heap

---

## 🔗 Liên kết

```
Lesson 09: Scope
    ↓
Lesson 10: Static ← BẠN Ở ĐÂY
    ↓
Lesson 11: Memory Basics (stack, heap, value vs reference)
    ↓
Lesson 12: Arrays
    ↓
Lesson 13: String
    ↓
Lesson 14: Exception Handling
```

> 📌 **Bài tiếp:** Lesson 11 — Memory Basics: Hiểu sâu stack/heap, value type vs reference type, garbage collection.
