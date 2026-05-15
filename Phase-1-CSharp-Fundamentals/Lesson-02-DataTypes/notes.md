# 📝 Lesson 02 — Notes: Data Types

---

## 🧠 Tóm tắt kiến thức

### 1. Hai nhóm kiểu dữ liệu:

```
VALUE TYPES (Stack)              REFERENCE TYPES (Heap)
─────────────────               ──────────────────────
int, long, short, byte          string
float, double, decimal          object
bool, char                      array, class
struct, enum                    interface, delegate
```

### 2. Bảng chọn kiểu nhanh:

```
Cần gì?                → Dùng gì?
────────────────────────────────────
Số đếm thông thường    → int ⭐
Số rất lớn (ID, timestamp) → long
Số thực (khoa học)     → double ⭐
Tiền tệ               → decimal 💰
Đúng/Sai               → bool
1 ký tự                → char
Chuỗi văn bản          → string
```

### 3. Ép kiểu:

```
Nhỏ → Lớn (Implicit):  byte → short → int → long → float → double
                        Tự động, AN TOÀN

Lớn → Nhỏ (Explicit):  double → int → byte
                        Phải dùng (kiểu), CÓ THỂ MẤT DỮ LIỆU

String → Số:            int.Parse("25")     — crash nếu sai
                        int.TryParse(s, out r) — AN TOÀN ⭐
                        Convert.ToInt32(s)   — crash nếu sai
```

### 4. Nullable:

```csharp
int? age = null;           // Nullable int
age ?? 0                   // Nếu null → dùng 0
age.HasValue               // true/false
age.Value                  // Lấy giá trị (nếu có)
```

---

## ⚠️ Những lỗi dễ quên

| # | Lỗi | Hậu quả | Cách tránh |
|---|------|---------|-----------|
| 1 | `double` cho tiền | Sai số (0.1+0.2 ≠ 0.3) | Dùng `decimal` |
| 2 | `int / int` mong kết quả thực | `7/2 = 3` thay vì `3.5` | Ép `(double)` trước |
| 3 | So sánh `double` bằng `==` | Có thể false dù "bằng" | Dùng epsilon/`Math.Abs` |
| 4 | Overflow int | Số quay ngược, kết quả sai | Dùng `long` hoặc `checked` |
| 5 | `(int)9.99` = `9` | Mất phần thập phân | Dùng `Math.Round()` nếu cần làm tròn |
| 6 | `int.Parse("abc")` | Exception crash | Dùng `TryParse` |
| 7 | Quên hậu tố `f`/`m` | `float x = 3.14` lỗi | `3.14f` cho float, `3.14m` cho decimal |

---

## 💡 Mindset quan trọng

### 1. "Chọn kiểu dữ liệu = quyết định kiến trúc"
- Chọn sai kiểu → bug ẩn, khó tìm, tốn thời gian
- Ví dụ: dùng `double` cho tiền → sai số tích lũy qua triệu giao dịch

### 2. "int / int = int — LUÔN NHỚ"
- Đây là lỗi #1 của người mới học C# từ JavaScript
- JS: `7 / 2 = 3.5` (tự động)
- C#: `7 / 2 = 3` (cắt thập phân)

### 3. "TryParse là bạn thân"
- Khi xử lý input người dùng → **LUÔN** dùng `TryParse`
- Không bao giờ `Parse` trực tiếp input chưa validate

### 4. "Overflow là kẻ giết người thầm lặng"
- Chương trình **không crash** — kết quả **sai âm thầm**
- Dùng `checked` hoặc kiểu lớn hơn khi nghi ngờ

### 5. "Nullable = thực tế"
- Database có NULL, API trả về null, form chưa điền → nullable
- Không xử lý null = `NullReferenceException` (lỗi #1 trong C#)

---

## ✅ Checklist "Đã hiểu chưa?"

- [ ] Tôi biết sự khác biệt giữa Value Type và Reference Type
- [ ] Tôi biết chọn kiểu số phù hợp (int/long/double/decimal)
- [ ] Tôi hiểu vì sao `decimal` bắt buộc cho tiền tệ
- [ ] Tôi biết bẫy `int / int = int` (integer division)
- [ ] Tôi biết cách ép kiểu: implicit, explicit, Convert, Parse, TryParse
- [ ] Tôi hiểu `(int)9.99 = 9` (cắt, không làm tròn)
- [ ] Tôi biết overflow là gì và cách phòng tránh
- [ ] Tôi biết dùng nullable types (`int?`, `string?`)
- [ ] Tôi biết dùng `??` (null-coalescing operator)
- [ ] Tôi hiểu vì sao KHÔNG so sánh `double` bằng `==`

---

## 🔗 Liên kết kiến thức

```
Lesson 01: Variables
    ↓
Lesson 02: Data Types ← BẠN Ở ĐÂY
    ↓
Lesson 03: Operators (phép toán trên các kiểu dữ liệu)
    ↓
Lesson 08: Methods (parameter types, return types)
    ↓
Lesson 11: Memory Basics (Stack vs Heap chi tiết)
    ↓
Lesson 14: Exception Handling (bắt FormatException, OverflowException)
    ↓
Lesson 29: Generic (kiểu dữ liệu tổng quát)
```

> 📌 **Bài tiếp theo:** Lesson 03 — Operators: Các phép toán, toán tử so sánh, toán tử logic, toán tử đặc biệt trong C#.
