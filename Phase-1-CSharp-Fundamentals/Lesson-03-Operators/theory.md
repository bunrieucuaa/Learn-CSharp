# 📘 Lesson 03 — Operators (Toán tử) trong C#

---

## 1. Operator là gì?

Operator (toán tử) là **ký hiệu đặc biệt** dùng để thực hiện phép tính hoặc so sánh trên dữ liệu.

```csharp
int result = 10 + 5;    // '+' là operator
bool isEqual = 10 == 5; // '==' là operator
```

> 🔑 Bạn đã biết operator trong JS rồi. C# có **hầu hết giống JS**, nhưng có một số điểm **khác biệt quan trọng** — đặc biệt về kiểu dữ liệu.

---

## 2. Arithmetic Operators (Toán tử số học)

| Operator | Tên | Ví dụ | Kết quả |
|----------|-----|-------|---------|
| `+` | Cộng | `10 + 3` | `13` |
| `-` | Trừ | `10 - 3` | `7` |
| `*` | Nhân | `10 * 3` | `30` |
| `/` | Chia | `10 / 3` | `3` ⚠️ |
| `%` | Chia lấy dư (Modulo) | `10 % 3` | `1` |

### ⚠️ BẪY QUAN TRỌNG: Phép chia trong C#

```csharp
// Chia 2 số int → kết quả là int (CẮT phần thập phân)
int a = 10 / 3;           // 3 ← KHÔNG phải 3.33!
double b = 10 / 3;        // 3.0 ← VẪN SAI! Phép chia xảy ra TRƯỚC khi gán
double c = 10.0 / 3;      // 3.333... ✅ Vì 10.0 là double
double d = (double)10 / 3; // 3.333... ✅ Ép kiểu trước khi chia
```

### So sánh với JavaScript:

```javascript
// JS — tất cả số đều là floating-point
10 / 3    // 3.3333... — JS tự động cho kết quả thực
```

```csharp
// C# — phụ thuộc vào kiểu dữ liệu
10 / 3      // 3 — int / int = int
10.0 / 3    // 3.333... — double / int = double
```

> 🔑 **Nhớ:** `int / int = int`. Đây là bẫy #1 khi chuyển từ JS sang C#!

### Modulo `%` — Chia lấy dư:

```csharp
Console.WriteLine(10 % 3);   // 1  (10 = 3×3 + DƯ 1)
Console.WriteLine(15 % 5);   // 0  (15 chia hết cho 5)
Console.WriteLine(7 % 2);    // 1  (7 là số lẻ)

// Ứng dụng phổ biến:
// Kiểm tra chẵn/lẻ
bool isEven = (number % 2 == 0);

// Kiểm tra chia hết
bool isDivisible = (number % 5 == 0);

// Lấy chữ số cuối
int lastDigit = number % 10;    // 1234 % 10 = 4
```

---

## 3. Assignment Operators (Toán tử gán)

| Operator | Tương đương | Ví dụ |
|----------|------------|-------|
| `=` | Gán | `x = 10` |
| `+=` | `x = x + n` | `x += 5` → `x = x + 5` |
| `-=` | `x = x - n` | `x -= 3` → `x = x - 3` |
| `*=` | `x = x * n` | `x *= 2` → `x = x * 2` |
| `/=` | `x = x / n` | `x /= 4` → `x = x / 4` |
| `%=` | `x = x % n` | `x %= 3` → `x = x % 3` |

```csharp
int score = 100;
score += 10;    // 110
score -= 20;    // 90
score *= 2;     // 180
score /= 3;     // 60
score %= 7;     // 4  (60 = 7×8 + 4)
```

> 💡 Giống hệt JavaScript. Không có gì khác biệt ở đây.

---

## 4. Increment & Decrement (Tăng/Giảm 1)

| Operator | Tên | Ý nghĩa |
|----------|-----|---------|
| `x++` | Post-increment | Dùng x trước, rồi tăng 1 |
| `++x` | Pre-increment | Tăng 1 trước, rồi dùng x |
| `x--` | Post-decrement | Dùng x trước, rồi giảm 1 |
| `--x` | Pre-decrement | Giảm 1 trước, rồi dùng x |

```csharp
int a = 5;
Console.WriteLine(a++);  // In 5, SAU ĐÓ a = 6
Console.WriteLine(a);    // 6

int b = 5;
Console.WriteLine(++b);  // Tăng TRƯỚC: b = 6, rồi in 6
Console.WriteLine(b);    // 6
```

### Bẫy phổ biến:

```csharp
int x = 5;
int y = x++;    // y = 5 (lấy x trước), x = 6 (tăng sau)

int m = 5;
int n = ++m;    // m = 6 (tăng trước), n = 6 (lấy m sau)
```

> 💡 **Best Practice:** Tránh dùng `x++` trong biểu thức phức tạp. Viết tách riêng cho dễ đọc:
> ```csharp
> // ❌ Khó hiểu
> int result = a++ + ++b;
>
> // ✅ Dễ hiểu
> a++;
> b++;
> int result = a + b;
> ```

---

## 5. Comparison Operators (Toán tử so sánh)

| Operator | Ý nghĩa | Ví dụ | Kết quả |
|----------|---------|-------|---------|
| `==` | Bằng nhau | `5 == 5` | `true` |
| `!=` | Khác nhau | `5 != 3` | `true` |
| `>` | Lớn hơn | `5 > 3` | `true` |
| `<` | Nhỏ hơn | `5 < 3` | `false` |
| `>=` | Lớn hơn hoặc bằng | `5 >= 5` | `true` |
| `<=` | Nhỏ hơn hoặc bằng | `5 <= 3` | `false` |

### Kết quả luôn là `bool`:

```csharp
bool result = 10 > 5;     // true
bool check = "abc" == "xyz"; // false
```

### ⚠️ So sánh trong C# vs JavaScript:

```javascript
// JS — có == và === (loose vs strict equality)
5 == "5"     // true  — JS tự ép kiểu!
5 === "5"    // false — strict: khác kiểu = false
```

```csharp
// C# — chỉ có == (luôn strict!)
// 5 == "5"  // ❌ LỖI COMPILE! Không thể so sánh int với string
5 == 5       // true
"abc" == "abc" // true
```

> 🔑 **C# không có `===`** vì `==` trong C# đã **luôn nghiêm ngặt** rồi. Không tự ép kiểu khi so sánh. Đây là điểm **an toàn hơn** JS.

### So sánh `string`:

```csharp
string a = "Hello";
string b = "Hello";
Console.WriteLine(a == b);  // true — so sánh GIÁ TRỊ (đặc biệt cho string!)

// So sánh không phân biệt hoa/thường:
string x = "hello";
string y = "HELLO";
Console.WriteLine(x == y);  // false
Console.WriteLine(string.Equals(x, y, StringComparison.OrdinalIgnoreCase));  // true
```

---

## 6. Logical Operators (Toán tử logic)

| Operator | Tên | Ý nghĩa |
|----------|-----|---------|
| `&&` | AND | Cả 2 đều true → true |
| `\|\|` | OR | 1 trong 2 true → true |
| `!` | NOT | Đảo ngược giá trị |

### Bảng chân lý:

```
A      B      A && B    A || B    !A
────── ────── ──────── ──────── ──────
true   true   true     true     false
true   false  false    true     false
false  true   false    true     true
false  false  false    false    true
```

### Ví dụ thực tế:

```csharp
int age = 25;
bool hasID = true;
bool hasTicket = false;

// AND — cần TẤT CẢ điều kiện đúng
bool canEnter = age >= 18 && hasID;
Console.WriteLine($"Được vào? {canEnter}");  // true

// OR — cần ÍT NHẤT 1 điều kiện đúng
bool hasAccess = hasTicket || hasID;
Console.WriteLine($"Có quyền? {hasAccess}");  // true

// NOT — đảo ngược
bool isMinor = !(age >= 18);
Console.WriteLine($"Vị thành niên? {isMinor}");  // false
```

### Short-circuit Evaluation (Đánh giá rút gọn):

```csharp
// && — nếu vế trái FALSE → KHÔNG kiểm tra vế phải
bool result = false && SomeExpensiveMethod();  // SomeExpensiveMethod() KHÔNG được gọi

// || — nếu vế trái TRUE → KHÔNG kiểm tra vế phải
bool result = true || SomeExpensiveMethod();   // SomeExpensiveMethod() KHÔNG được gọi
```

> 💡 Giống hệt JavaScript. Short-circuit rất hữu ích để **tránh lỗi**:
> ```csharp
> // Kiểm tra null trước khi truy cập property
> if (name != null && name.Length > 0)
> {
>     // An toàn — nếu name null, vế phải không chạy
> }
> ```

---

## 7. Ternary Operator (Toán tử 3 ngôi)

```csharp
// Cú pháp: condition ? valueIfTrue : valueIfFalse

int age = 20;
string status = age >= 18 ? "Người lớn" : "Trẻ em";
Console.WriteLine(status);  // "Người lớn"
```

### Tương đương if/else:

```csharp
// Ternary (1 dòng)
string result = score >= 5 ? "Đậu" : "Rớt";

// If/else (nhiều dòng)
string result;
if (score >= 5)
    result = "Đậu";
else
    result = "Rớt";
```

### Lồng nhau (nested) — CẨN THẬN:

```csharp
// ❌ Khó đọc
string grade = score >= 9 ? "Giỏi" : score >= 7 ? "Khá" : score >= 5 ? "TB" : "Yếu";

// ✅ Dùng if/else khi phức tạp
string grade;
if (score >= 9) grade = "Giỏi";
else if (score >= 7) grade = "Khá";
else if (score >= 5) grade = "TB";
else grade = "Yếu";
```

> 💡 **Best Practice:** Chỉ dùng ternary cho điều kiện **đơn giản, 2 nhánh**. Phức tạp hơn → dùng if/else.

---

## 8. Null-related Operators (Toán tử xử lý null)

### 8.1 `??` — Null-coalescing Operator

```csharp
string? name = null;
string displayName = name ?? "Không tên";
Console.WriteLine(displayName);  // "Không tên"

int? age = null;
int actualAge = age ?? 0;   // Nếu null → dùng 0
```

### 8.2 `??=` — Null-coalescing Assignment

```csharp
string? name = null;
name ??= "Mặc định";    // Nếu name null → gán "Mặc định"
Console.WriteLine(name); // "Mặc định"

string? title = "Manager";
title ??= "Nhân viên";   // title KHÔNG null → KHÔNG gán
Console.WriteLine(title); // "Manager"
```

### 8.3 `?.` — Null-conditional Operator

```csharp
string? name = null;

// ❌ Crash nếu name null:
// int length = name.Length;  // NullReferenceException!

// ✅ An toàn:
int? length = name?.Length;   // null — không crash
Console.WriteLine(length);    // (nothing — null)

// Kết hợp với ??:
int safeLength = name?.Length ?? 0;  // null → 0
Console.WriteLine(safeLength);       // 0
```

### So sánh với JavaScript:

```javascript
// JS optional chaining (ES2020) — giống C# ?.
let name = null;
let length = name?.length;     // undefined
let safe = name?.length ?? 0;  // 0

// JS nullish coalescing (ES2020) — giống C# ??
let result = null ?? "default"; // "default"
```

> 🔑 C# có `?.` và `??` **trước JavaScript** nhiều năm! JS mới thêm ở ES2020.

---

## 9. Bitwise Operators (Toán tử bit) — Giới thiệu nhẹ

| Operator | Tên | Ý nghĩa |
|----------|-----|---------|
| `&` | AND | So sánh từng bit |
| `\|` | OR | So sánh từng bit |
| `^` | XOR | Khác nhau = 1 |
| `~` | NOT | Đảo bit |
| `<<` | Left Shift | Dịch trái (nhân 2) |
| `>>` | Right Shift | Dịch phải (chia 2) |

```csharp
int a = 5;     // Binary: 0101
int b = 3;     // Binary: 0011

Console.WriteLine(a & b);   // 1  (0001) — AND
Console.WriteLine(a | b);   // 7  (0111) — OR
Console.WriteLine(a ^ b);   // 6  (0110) — XOR
Console.WriteLine(a << 1);  // 10 (1010) — dịch trái = ×2
Console.WriteLine(a >> 1);  // 2  (0010) — dịch phải = ÷2
```

> 💡 Bạn sẽ ít dùng bitwise trong code thường ngày. Nhưng nó xuất hiện trong: flags/enum, permissions, game dev, networking. Biết là có, hiểu cơ bản là đủ.

---

## 10. Operator Precedence (Thứ tự ưu tiên)

Từ **cao → thấp**:

| Thứ tự | Operators |
|--------|-----------|
| 1 | `()` — ngoặc (cao nhất) |
| 2 | `!`, `++`, `--`, `(type)` — unary |
| 3 | `*`, `/`, `%` — nhân, chia |
| 4 | `+`, `-` — cộng, trừ |
| 5 | `<`, `>`, `<=`, `>=` — so sánh |
| 6 | `==`, `!=` — bằng, khác |
| 7 | `&&` — AND logic |
| 8 | `\|\|` — OR logic |
| 9 | `?:` — ternary |
| 10 | `=`, `+=`, `-=` ... — gán (thấp nhất) |

### Ví dụ:

```csharp
int result = 2 + 3 * 4;       // 14 (nhân trước cộng)
int result2 = (2 + 3) * 4;    // 20 (ngoặc ưu tiên cao nhất)

bool check = 5 > 3 && 10 < 20; // true (so sánh trước, AND sau)
```

> 💡 **Best Practice:** Khi không chắc thứ tự → **dùng ngoặc `()`** cho rõ ràng. Code dễ đọc hơn "code đúng nhưng khó hiểu".

---

## 11. Sai lầm phổ biến

### ❌ Sai lầm 1: Nhầm `=` và `==`

```csharp
int x = 5;
// if (x = 10)   // ❌ LỖI COMPILE trong C# — gán, không phải so sánh!
if (x == 10)     // ✅ So sánh
```

> 🔑 **C# bảo vệ bạn!** Trong JS, `if (x = 10)` chạy được (luôn true). C# báo lỗi compile.

### ❌ Sai lầm 2: Chia số nguyên không ép kiểu

```csharp
double avg = (8 + 7 + 9) / 3;     // 8.0 ← SAI! (24/3 = 8, int/int = int)
double avg = (double)(8 + 7 + 9) / 3;  // 8.0 ← Trường hợp này đúng tình cờ
double avg = (double)(7 + 8 + 6) / 3;  // 7.0 ← SAI vì 21/3 = 7 đúng, nhưng nếu không chia hết...

int a = 7, b = 8, c = 6;    // Tổng = 21
double wrong = (a + b + c) / 3;         // 7.0 — OK tình cờ
double right = (double)(a + b + c) / 3;  // 7.0 — đúng cách

int x = 7, y = 8, z = 9;    // Tổng = 24
double wrong2 = (x + y + z) / 3;         // 8.0 — OK tình cờ
// Nhưng nếu tổng = 25:
int m = 7, n = 9, p = 9;    // Tổng = 25
double wrong3 = (m + n + p) / 3;         // 8.0 ← SAI! Phải là 8.33...
double right3 = (double)(m + n + p) / 3;  // 8.333... ✅
```

### ❌ Sai lầm 3: Dùng `&` thay vì `&&`

```csharp
// & (bitwise AND) — LUÔN đánh giá cả 2 vế
// && (logical AND) — short-circuit: dừng sớm nếu vế trái false

string? name = null;
if (name != null & name.Length > 0)   // ❌ CRASH! Vế phải vẫn chạy dù name null
if (name != null && name.Length > 0)  // ✅ An toàn! Dừng ở vế trái nếu null
```

### ❌ Sai lầm 4: Nested ternary quá sâu

```csharp
// ❌ Không ai đọc được:
var r = a > b ? a > c ? a : c : b > c ? b : c;

// ✅ Dùng if/else:
int max;
if (a >= b && a >= c) max = a;
else if (b >= c) max = b;
else max = c;
```

---

## 12. Best Practices

1. **Dùng ngoặc `()`** khi kết hợp nhiều operator — rõ ràng hơn
2. **`&&` và `||`** thay vì `&` và `|` cho logic — có short-circuit
3. **Ternary chỉ cho 2 nhánh đơn giản** — phức tạp dùng if/else
4. **`?.` và `??`** thay vì kiểm tra null thủ công — code gọn hơn
5. **Tránh `++`/`--` trong biểu thức phức tạp** — tách riêng cho dễ đọc
6. **Luôn ép kiểu khi chia** nếu cần kết quả thực — `(double)a / b`
7. **`%` để kiểm tra chẵn/lẻ, chia hết** — kỹ thuật cơ bản nhưng rất hay dùng
