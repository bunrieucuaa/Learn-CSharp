# 📝 Lesson 06 — Notes: Switch

---

## 🧠 Tóm tắt kiến thức

### 1. Switch truyền thống:

```csharp
switch (variable)
{
    case value1:
        // ...
        break;          // BẮT BUỘC!
    case value2:
    case value3:        // Gộp case
        // ...
        break;
    default:
        // ...
        break;
}
```

### 2. Switch Expression (C# 8+):

```csharp
var result = variable switch
{
    value1 => result1,
    value2 => result2,
    _ => defaultResult    // _ = default
};                        // Có dấu ;
```

### 3. Pattern Matching (C# 9+):

```csharp
// Relational
string grade = score switch { >= 9 => "A", >= 7 => "B", _ => "C" };

// when Guard
case >= 5.0 when isPassed: ...

// Tuple
(role, action) switch { ("admin", "delete") => "OK", ... };

// Type
data switch { int n => "Số", string s => "Chuỗi", _ => "Khác" };

// or Pattern
case "A+" or "A": ...
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Quên `break` | C# bắt buộc — compile error |
| 2 | Quên `_` trong switch expression | Phải cover hết case |
| 3 | Không normalize string | `Trim().ToLower()` trước switch |
| 4 | Dùng switch truyền thống cho phạm vi | Dùng switch expression + relational |
| 5 | Thứ tự pattern sai | Kiểm tra nghiêm ngặt nhất trước |

---

## 📊 Switch vs If/Else — Khi nào dùng gì?

| Tình huống | Dùng |
|-----------|------|
| So sánh 1 biến với nhiều giá trị cụ thể | `switch` ✅ |
| So sánh phạm vi (<, >, >=) | `switch expression` ✅ hoặc `if/else` |
| Điều kiện phức tạp (&&, \|\|) | `if/else` ✅ |
| Gán giá trị dựa trên pattern | `switch expression` ✅ |
| Phụ thuộc 2+ biến | Tuple pattern ✅ |
| 2-3 nhánh đơn giản | `if/else` hoặc `ternary` |
| Menu lựa chọn | `switch` ✅ |

---

## 📊 So sánh C# vs JavaScript Switch

| Đặc điểm | C# | JavaScript |
|-----------|-----|-----------|
| Fall through | ❌ Không cho phép | ✅ Cho phép (quên break) |
| `break` | Bắt buộc | Tùy chọn |
| Switch expression | ✅ Có (C# 8+) | ❌ Không có |
| Pattern matching | ✅ Rất mạnh | ❌ Không có |
| Tuple pattern | ✅ Có | ❌ Không có |
| Relational pattern | ✅ `< 18`, `>= 65` | ❌ Không có |

> 🔑 **C# switch mạnh hơn JS RẤT NHIỀU** nhờ pattern matching. Đây là một trong những tính năng nổi bật nhất của C# hiện đại.

---

## ✅ Checklist

- [ ] Tôi biết cú pháp switch truyền thống + break bắt buộc
- [ ] Tôi biết gộp nhiều case (multiple cases)
- [ ] Tôi biết switch expression với `=>`  và `_`
- [ ] Tôi biết relational pattern: `< 18`, `>= 65`
- [ ] Tôi biết `when` guard cho điều kiện phụ
- [ ] Tôi biết tuple pattern cho nhiều biến
- [ ] Tôi biết `or` pattern: `"A+" or "A"`
- [ ] Tôi biết khi nào dùng switch vs if/else
- [ ] Tôi luôn normalize string trước switch
- [ ] Tôi hiểu C# switch mạnh hơn JS nhờ pattern matching

---

## 🔗 Liên kết

```
Lesson 05: If/Else
    ↓
Lesson 06: Switch ← BẠN Ở ĐÂY
    ↓
Lesson 07: Loop (for, while, do-while, foreach)
    ↓
Lesson 08: Methods (tách logic thành hàm)
```

> 📌 **Bài tiếp:** Lesson 07 — Loop: Vòng lặp `for`, `while`, `do-while`, `foreach`, `break`, `continue`. Kết hợp với if/switch để xây dựng logic phức tạp.
