# 📝 Lesson 03 — Notes: Operators

---

## 🧠 Tóm tắt kiến thức

### 1. Arithmetic Operators:

```
+  Cộng       10 + 3 = 13
-  Trừ        10 - 3 = 7
*  Nhân       10 * 3 = 30
/  Chia       10 / 3 = 3 ⚠️ (int/int = int!)
%  Modulo     10 % 3 = 1
```

### 2. Assignment Operators:

```
=    Gán          x = 10
+=   Cộng gán     x += 5  →  x = x + 5
-=   Trừ gán      x -= 3  →  x = x - 3
*=   Nhân gán     x *= 2  →  x = x * 2
/=   Chia gán     x /= 4  →  x = x / 4
%=   Modulo gán   x %= 3  →  x = x % 3
```

### 3. Increment/Decrement:

```
x++  Post: dùng x, rồi tăng
++x  Pre:  tăng x, rồi dùng
x--  Post: dùng x, rồi giảm
--x  Pre:  giảm x, rồi dùng
```

### 4. Comparison (kết quả luôn là bool):

```
==   Bằng          5 == 5  → true
!=   Khác          5 != 3  → true
>    Lớn hơn       5 > 3   → true
<    Nhỏ hơn       5 < 3   → false
>=   Lớn hơn bằng  5 >= 5  → true
<=   Nhỏ hơn bằng  5 <= 3  → false
```

### 5. Logical:

```
&&   AND    Cả 2 true → true     (short-circuit)
||   OR     1 cái true → true    (short-circuit)
!    NOT    Đảo ngược
```

### 6. Ternary:

```csharp
condition ? valueIfTrue : valueIfFalse
```

### 7. Null Operators:

```csharp
??    Null-coalescing:    name ?? "default"
??=   Null-assign:        name ??= "default"
?.    Null-conditional:   name?.Length
```

---

## ⚠️ Những lỗi dễ quên

| # | Lỗi | Hậu quả | Cách tránh |
|---|------|---------|-----------|
| 1 | `int / int = int` | `7/2 = 3` thay vì `3.5` | Ép `(double)` trước khi chia |
| 2 | Nhầm `=` và `==` | Gán thay vì so sánh | C# báo lỗi compile (an toàn hơn JS) |
| 3 | Dùng `&` thay `&&` | Không short-circuit, crash khi null | Luôn dùng `&&` cho logic |
| 4 | `x++` trong biểu thức | Kết quả khó đoán | Tách `x++` riêng |
| 5 | Nested ternary sâu | Không ai đọc được | Dùng if/else khi > 2 nhánh |
| 6 | Quên thứ tự ưu tiên | Kết quả sai | Dùng ngoặc `()` cho rõ ràng |

---

## 💡 Mindset quan trọng

### 1. "Operator là nền tảng của mọi logic"
- Tính tiền, xét điều kiện, kiểm tra quyền → tất cả dùng operators
- Hiểu thứ tự ưu tiên = tránh bug logic

### 2. "Đơn giản hơn = tốt hơn"
- `x++; result = a + b;` tốt hơn `result = a + b++;`
- Code dễ đọc > code ngắn

### 3. "Null là kẻ thù #1"
- `?.` và `??` là vũ khí chống NullReferenceException
- Luôn nghĩ: "Biến này có thể null không?"

### 4. "`%` (modulo) mạnh hơn bạn nghĩ"
- Chẵn/lẻ, chia hết, tách chữ số, tạo pattern lặp, đổi đơn vị thời gian
- Là công cụ yêu thích trong phỏng vấn (FizzBuzz!)

---

## 📊 So sánh C# vs JavaScript

| Đặc điểm | C# | JavaScript |
|-----------|-----|-----------|
| `==` | Strict (luôn so kiểu) | Loose (tự ép kiểu) |
| `===` | Không có (không cần) | Strict equality |
| `int / int` | = int (cắt thập phân) | = number (có thập phân) |
| `if (0)` | ❌ Lỗi compile | Cho phép (falsy) |
| `?.` | Có (từ C# 6) | Có (từ ES2020) |
| `??` | Có (từ C# 2) | Có (từ ES2020) |
| `??=` | Có (từ C# 8) | Có (từ ES2021) |

---

## ✅ Checklist "Đã hiểu chưa?"

- [ ] Tôi biết `int / int = int` và cách khắc phục
- [ ] Tôi biết dùng `%` để kiểm tra chẵn/lẻ, chia hết, tách số
- [ ] Tôi hiểu sự khác biệt giữa `x++` và `++x`
- [ ] Tôi biết C# không có `===` (vì `==` đã strict)
- [ ] Tôi biết C# không cho `if (0)` hay `if ("")` (chỉ chấp nhận `bool`)
- [ ] Tôi hiểu `&&` short-circuit (dừng sớm nếu vế trái false)
- [ ] Tôi biết dùng ternary cho 2 nhánh đơn giản
- [ ] Tôi biết dùng `??`, `??=`, `?.` cho null handling
- [ ] Tôi biết thứ tự ưu tiên operators (hoặc biết dùng ngoặc cho an toàn)
- [ ] Tôi có thể giải FizzBuzz bằng `%`

---

## 🔗 Liên kết kiến thức

```
Lesson 01: Variables
    ↓
Lesson 02: Data Types
    ↓
Lesson 03: Operators ← BẠN Ở ĐÂY
    ↓
Lesson 04: Input/Output (nhận input → xử lý bằng operators → xuất output)
    ↓
Lesson 05: If/Else (dùng comparison + logical operators)
    ↓
Lesson 06: Switch (thay thế if/else phức tạp)
    ↓
Lesson 07: Loop (dùng ++, --, %, so sánh)
```

> 📌 **Bài tiếp theo:** Lesson 04 — Input/Output: Nhận dữ liệu từ người dùng bằng `Console.ReadLine()`, xử lý và xuất kết quả. Kết hợp TryParse + operators để xây dựng ứng dụng tương tác.
