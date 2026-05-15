# 📘 Lesson 02 — Data Types (Kiểu dữ liệu) trong C#

---

## 1. Tại sao cần hiểu sâu về Data Types?

Ở Lesson 01, bạn đã biết khai báo biến với `int`, `string`, `double`... Nhưng câu hỏi quan trọng hơn là:

> 🔑 **Vì sao C# có NHIỀU kiểu số khác nhau? Tại sao không dùng 1 kiểu `number` như JavaScript?**

**Câu trả lời:** Vì mỗi kiểu chiếm **lượng bộ nhớ khác nhau** và có **phạm vi giá trị khác nhau**. Chọn đúng kiểu = tiết kiệm bộ nhớ + tránh bug.

### Ví dụ thực tế:

Tưởng tượng bạn có 3 loại hộp:
- 📦 Hộp nhỏ (byte): chứa được số từ 0 đến 255
- 📦 Hộp vừa (int): chứa được số đến ~2 tỷ
- 📦 Hộp lớn (long): chứa được số đến ~9 triệu tỷ

Nếu bạn chỉ cần lưu **tuổi người** (0–150), dùng hộp lớn (`long`) = **lãng phí bộ nhớ**!

---

## 2. Hai nhóm kiểu dữ liệu chính

C# chia kiểu dữ liệu thành **2 nhóm lớn**:

```
┌─────────────────────────────────────────────────┐
│             KIỂU DỮ LIỆU TRONG C#               │
├────────────────────┬────────────────────────────┤
│   VALUE TYPES      │    REFERENCE TYPES         │
│  (Kiểu giá trị)    │    (Kiểu tham chiếu)       │
├────────────────────┼────────────────────────────┤
│ • int, long, short │ • string                   │
│ • float, double    │ • object                   │
│ • decimal          │ • array (int[], string[])  │
│ • bool             │ • class (tự tạo)           │
│ • char             │ • interface                │
│ • byte             │ • delegate                 │
│ • struct           │                            │
│ • enum             │                            │
├────────────────────┼────────────────────────────┤
│ Lưu ở: STACK       │ Lưu ở: HEAP                │
│ Copy = bản sao     │ Copy = copy địa chỉ        │
└────────────────────┴────────────────────────────┘
```

### So sánh với JavaScript:

- JS: Tất cả số đều là `number` (64-bit floating point) — đơn giản nhưng **lãng phí bộ nhớ**
- C#: Có nhiều kiểu số — phức tạp hơn nhưng **hiệu quả** và **chính xác** hơn

---

## 3. Các kiểu số nguyên (Integer Types)

### Bảng chi tiết:

| Kiểu | Kích thước | Phạm vi | Dùng khi |
|------|-----------|---------|----------|
| `byte` | 1 byte | 0 → 255 | Tuổi, mức độ, màu RGB |
| `sbyte` | 1 byte | -128 → 127 | Hiếm dùng |
| `short` | 2 bytes | -32,768 → 32,767 | Số vừa phải |
| `ushort` | 2 bytes | 0 → 65,535 | Số dương vừa |
| **`int`** | **4 bytes** | **-2.1 tỷ → 2.1 tỷ** | **Mặc định cho số nguyên** |
| `uint` | 4 bytes | 0 → 4.2 tỷ | Số dương lớn |
| `long` | 8 bytes | -9.2 triệu tỷ → 9.2 triệu tỷ | ID database, timestamp |
| `ulong` | 8 bytes | 0 → 18.4 triệu tỷ | Số cực lớn |

### Tiền tố `u` = unsigned (không dấu):
- `int` = có số âm
- `uint` = chỉ số dương (0 trở lên) — "u" = unsigned

### Tiền tố `s` = signed (có dấu):
- `byte` = 0 → 255 (mặc định unsigned)
- `sbyte` = -128 → 127 (signed)

### 📏 Quy tắc chọn kiểu số nguyên:

```
Cần lưu gì?                    → Dùng kiểu nào?
─────────────────────────────────────────────────
Tuổi, số lượng nhỏ (0-255)     → byte
Đếm, index, số thông thường    → int ⭐ (mặc định)
ID trong database               → long
Số cực lớn, không âm            → ulong
```

> 💡 **Best Practice:** Khi không chắc, **dùng `int`**. Chỉ dùng kiểu khác khi có lý do rõ ràng.

---

## 4. Các kiểu số thực (Floating-Point Types)

| Kiểu | Kích thước | Độ chính xác | Hậu tố | Dùng khi |
|------|-----------|-------------|--------|----------|
| `float` | 4 bytes | ~6-7 chữ số | `f` | Game, đồ họa (cần nhanh) |
| **`double`** | **8 bytes** | **~15-16 chữ số** | không cần | **Mặc định cho số thực** |
| `decimal` | 16 bytes | ~28-29 chữ số | `m` | **Tiền tệ, tài chính** |

### Vì sao `double` KHÔNG nên dùng cho tiền?

```csharp
// BUG kinh điển với double!
double a = 0.1;
double b = 0.2;
double result = a + b;

Console.WriteLine(result);         // 0.30000000000000004  ← SAI!
Console.WriteLine(result == 0.3);  // False  ← NGUY HIỂM!
```

```csharp
// decimal — chính xác!
decimal a = 0.1m;
decimal b = 0.2m;
decimal result = a + b;

Console.WriteLine(result);         // 0.3  ← ĐÚNG!
Console.WriteLine(result == 0.3m); // True  ← AN TOÀN!
```

**Giải thích bản chất:**
- `double` lưu số theo **dạng nhị phân** (binary) → một số số thập phân không thể biểu diễn chính xác
- `decimal` lưu số theo **dạng thập phân** (base-10) → chính xác cho phép tính tiền
- Cái giá: `decimal` **chậm hơn** `double` khoảng 20 lần → chỉ dùng khi cần chính xác

### 📏 Quy tắc chọn kiểu số thực:

```
Cần lưu gì?                    → Dùng kiểu nào?
─────────────────────────────────────────────────
Tính toán khoa học, đồ họa      → double ⭐ (mặc định)
Game, cần tiết kiệm bộ nhớ     → float
Tiền tệ, tài chính             → decimal 💰 (BẮT BUỘC)
```

---

## 5. Kiểu `bool`

```csharp
bool isActive = true;
bool hasPermission = false;
```

- Chỉ có 2 giá trị: `true` hoặc `false`
- Chiếm **1 byte** trong bộ nhớ (dù chỉ cần 1 bit)
- Dùng cho: điều kiện, trạng thái, flag

### So sánh với JavaScript:

```javascript
// JS — truthy/falsy phức tạp
if (0)         // false
if ("")        // false
if (null)      // false
if (undefined) // false
if ("hello")   // true — string không rỗng = truthy
if (1)         // true — số khác 0 = truthy
```

```csharp
// C# — NGHIÊM NGẶT
if (0)         // ❌ LỖI COMPILE! int không phải bool
if ("")        // ❌ LỖI COMPILE! string không phải bool
if (isActive)  // ✅ OK — isActive là bool
if (age > 18)  // ✅ OK — so sánh trả về bool
```

> 🔑 **Trong C#, `if` chỉ chấp nhận `bool`.** Không có truthy/falsy như JS. Đây là điểm **rất dễ nhầm** khi chuyển từ JS sang C#.

---

## 6. Kiểu `char`

```csharp
char grade = 'A';        // Nháy đơn — 1 ký tự duy nhất
char symbol = '★';       // Unicode
char newLine = '\n';     // Escape character
```

- `char` = 1 ký tự, **2 bytes** (UTF-16)
- Dùng **nháy đơn** `' '`
- `string` = nhiều ký tự, dùng **nháy kép** `" "`

### Khác biệt với JS:

JavaScript **không có** kiểu `char`. Một ký tự trong JS vẫn là `string`:

```javascript
let ch = 'A';         // JS: vẫn là string, không phải char
typeof ch;            // "string"
```

```csharp
char ch = 'A';        // C#: kiểu char
string str = "A";     // C#: kiểu string
// ch và str là KHÁC KIỂU!
```

### Mỗi `char` đều có mã số (ASCII/Unicode):

```csharp
char letter = 'A';
int ascii = (int)letter;  // Ép kiểu char → int
Console.WriteLine(ascii); // 65

char fromCode = (char)66; // Ép kiểu int → char
Console.WriteLine(fromCode); // B
```

---

## 7. Kiểu `string` — Đặc biệt!

`string` là **Reference Type** nhưng **hành xử** gần giống Value Type:

```csharp
string name1 = "Minh";
string name2 = name1;    // Copy giá trị, KHÔNG phải copy địa chỉ
name2 = "Hùng";

Console.WriteLine(name1); // "Minh" — KHÔNG bị ảnh hưởng
Console.WriteLine(name2); // "Hùng"
```

### ⚠️ String là Immutable (bất biến):

```csharp
string greeting = "Hello";
greeting.ToUpper();                   // Trả về "HELLO" nhưng...
Console.WriteLine(greeting);          // Vẫn là "Hello"!

greeting = greeting.ToUpper();        // Phải gán lại!
Console.WriteLine(greeting);          // Bây giờ mới là "HELLO"
```

> 🔑 **Mọi thao tác trên string đều tạo string MỚI**, không thay đổi string cũ. Giống JS!

---

## 8. Ép kiểu (Type Casting)

### 8.1 Implicit Casting (Tự động — An toàn)

Khi chuyển từ kiểu **nhỏ → lớn**, C# tự động ép kiểu:

```csharp
int myInt = 100;
long myLong = myInt;      // ✅ int → long (tự động)
float myFloat = myLong;   // ✅ long → float (tự động)
double myDouble = myFloat; // ✅ float → double (tự động)
```

**Thứ tự tự động:** `byte → short → int → long → float → double`

Tại sao an toàn? Vì kiểu lớn hơn **luôn chứa được** giá trị của kiểu nhỏ hơn.

### 8.2 Explicit Casting (Thủ công — Có thể mất dữ liệu)

Khi chuyển từ kiểu **lớn → nhỏ**, phải ép kiểu thủ công:

```csharp
double myDouble = 9.78;
int myInt = (int)myDouble;    // Ép kiểu bằng (kiểu)
Console.WriteLine(myInt);     // 9 — MẤT phần thập phân!

long bigNumber = 3000000000;
int smallNumber = (int)bigNumber;  // ⚠️ Có thể mất dữ liệu nếu số quá lớn!
```

### 8.3 Dùng phương thức Convert

```csharp
// Convert class — an toàn hơn
string ageText = "25";
int age = Convert.ToInt32(ageText);     // string → int
double height = Convert.ToDouble("1.75"); // string → double
bool isOk = Convert.ToBoolean("true");   // string → bool

Console.WriteLine(age);      // 25
Console.WriteLine(height);   // 1.75
Console.WriteLine(isOk);     // True
```

### 8.4 Dùng Parse và TryParse

```csharp
// Parse — ném exception nếu sai format
int age = int.Parse("25");        // ✅ OK
// int bad = int.Parse("hello");  // ❌ Exception! FormatException

// TryParse — AN TOÀN, trả về true/false
bool success = int.TryParse("25", out int result);
Console.WriteLine(success);  // True
Console.WriteLine(result);   // 25

bool fail = int.TryParse("hello", out int result2);
Console.WriteLine(fail);     // False
Console.WriteLine(result2);  // 0 (giá trị mặc định)
```

### So sánh các cách ép kiểu:

| Cách | Khi nào dùng | Ném exception? |
|------|-------------|----------------|
| `(int)x` | Số → số (biết chắc an toàn) | Không (nhưng mất dữ liệu) |
| `Convert.ToInt32(x)` | Chuyển đổi tổng quát | Có, nếu sai format |
| `int.Parse(s)` | String → số (biết chắc hợp lệ) | Có, nếu sai format |
| `int.TryParse(s, out r)` | String → số (không chắc) | **Không** — trả true/false |

> 💡 **Best Practice:** Dùng `TryParse` khi xử lý input từ người dùng — AN TOÀN nhất!

---

## 9. Overflow — Tràn số

Khi giá trị vượt quá phạm vi của kiểu dữ liệu:

```csharp
byte maxByte = 255;
// maxByte = 256;  // ❌ LỖI COMPILE — vượt phạm vi byte

// Nhưng nếu tính toán runtime:
byte a = 200;
byte b = 100;
// byte c = a + b;  // ❌ LỖI — a + b = 300, vượt phạm vi byte
// Thực ra C# tự chuyển thành int khi cộng, nên lỗi là do gán int vào byte

int maxInt = int.MaxValue;  // 2,147,483,647
Console.WriteLine(maxInt);
Console.WriteLine(maxInt + 1);  // -2,147,483,648 — TRÀN SỐ! 😱
```

### Giải thích Overflow:

```
int.MaxValue = 2,147,483,647 (binary: 0111...1111)
         + 1 = ??? 

Binary:  0111 1111 ... 1111
       + 0000 0000 ... 0001
       = 1000 0000 ... 0000  ← bit đầu = 1 = số ÂM!
       = -2,147,483,648
```

> 🔑 Tràn số là **lỗi logic nghiêm trọng** — chương trình **không báo lỗi** nhưng **kết quả sai**!

### Cách phòng tránh:

```csharp
// Dùng checked để C# ném exception khi overflow
checked
{
    int max = int.MaxValue;
    // int overflow = max + 1;  // ❌ OverflowException!
}

// Hoặc dùng kiểu lớn hơn
long safeResult = (long)int.MaxValue + 1;  // ✅ OK
```

---

## 10. Giá trị mặc định (Default Values)

| Kiểu | Giá trị mặc định |
|------|------------------|
| `int`, `long`, `short`, `byte` | `0` |
| `float`, `double`, `decimal` | `0.0` |
| `bool` | `false` |
| `char` | `'\0'` (null character) |
| `string` | `null` |

```csharp
// Chỉ áp dụng cho field của class (không phải local variable!)
// Local variable BẮT BUỘC phải gán giá trị trước khi dùng

// Dùng default keyword:
int x = default;       // 0
bool y = default;      // false
string z = default;    // null
```

---

## 11. Nullable Types — Cho phép null

Value types bình thường **không thể** là `null`:

```csharp
int age = null;  // ❌ LỖI! int không thể null
```

Thêm `?` để cho phép null:

```csharp
int? age = null;       // ✅ OK — nullable int
double? price = null;  // ✅ OK — nullable double
bool? isActive = null; // ✅ OK — nullable bool

// Kiểm tra có giá trị không
if (age.HasValue)
{
    Console.WriteLine(age.Value);
}
else
{
    Console.WriteLine("Chưa có tuổi");
}

// Toán tử ?? — null-coalescing (giá trị mặc định nếu null)
int actualAge = age ?? 0;  // Nếu age null → dùng 0
Console.WriteLine(actualAge);
```

### So sánh với JavaScript:

```javascript
// JS — mọi thứ có thể là null/undefined
let age = null;       // OK
let name = undefined; // OK
let x = age ?? 0;     // Nullish coalescing — giống C#!
```

> 🔑 C# phân biệt rõ: Value type **mặc định không null**. Muốn null phải dùng `?`. Điều này giúp **tránh NullReferenceException** — lỗi phổ biến nhất trong C#.

---

## 12. Sai lầm phổ biến

### ❌ Sai lầm 1: Dùng `double` cho tiền

```csharp
double price = 19.99;  // ❌ Có thể bị sai số!
decimal price = 19.99m; // ✅ Chính xác
```

### ❌ Sai lầm 2: Chia số nguyên mong đợi kết quả thực

```csharp
int a = 7;
int b = 2;
int result = a / b;         // 3 — không phải 3.5!
double result2 = a / b;     // 3.0 — VẪN KHÔNG PHẢI 3.5!
double result3 = (double)a / b; // 3.5 — phải ép kiểu TRƯỚC khi chia!
```

### ❌ Sai lầm 3: Dùng `==` để so sánh double

```csharp
double x = 0.1 + 0.2;
if (x == 0.3)  // ❌ Có thể false vì sai số!
{
    // Có thể không bao giờ chạy vào đây
}

// Cách đúng: so sánh với sai số cho phép (epsilon)
if (Math.Abs(x - 0.3) < 0.0001)  // ✅
{
    Console.WriteLine("Gần bằng 0.3");
}
```

### ❌ Sai lầm 4: Quên overflow

```csharp
int big = 2000000000;
int result = big * 2;  // Overflow! Kết quả sai âm thầm
long result = (long)big * 2;  // ✅ An toàn
```

---

## 13. Best Practices

1. **Mặc định dùng `int`** cho số nguyên, `double` cho số thực
2. **Dùng `decimal`** cho **mọi thứ liên quan đến tiền**
3. **Dùng `TryParse`** khi convert từ string (input người dùng)
4. **Cẩn thận phép chia số nguyên** — ép kiểu trước khi chia
5. **Không so sánh `double` bằng `==`** — dùng epsilon
6. **Dùng `long`** khi số có thể lớn (ID, timestamp, file size)
7. **Dùng `checked`** khi lo ngại overflow
8. **Dùng nullable `?`** khi giá trị có thể không tồn tại
