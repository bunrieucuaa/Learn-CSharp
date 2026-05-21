# 📘 Lesson 10 — String (Xử lý chuỗi nâng cao) trong C#

---

## 1. String cơ bản — Ôn tập nhanh

```csharp
string name = "Minh";
string empty = "";
string? nullable = null;

// String là IMMUTABLE (bất biến) — mọi thao tác tạo string MỚI
string upper = name.ToUpper();   // "MINH" — name vẫn là "Minh"
```

> 🔑 **Immutable** = không thể sửa string gốc. Mọi method trả về string MỚI.

### So sánh với JavaScript:

| | C# | JavaScript |
|--|-----|-----------|
| Immutable | ✅ Giống | ✅ Giống |
| Null vs empty | `null` ≠ `""` | `null` ≠ `""` ≠ `undefined` |
| Index | `str[0]` → `char` | `str[0]` → `string` |
| Template | `$"Hi {name}"` | `` `Hi ${name}` `` |
| So sánh | `==` (value) | `===` (strict) |

---

## 2. Các method String quan trọng nhất

### 2.1 Tìm kiếm & Kiểm tra

```csharp
string text = "Hello World, Hello C#!";

// Chứa
text.Contains("World");              // true
text.Contains("world");              // false (case-sensitive!)
text.Contains("world", StringComparison.OrdinalIgnoreCase); // true

// Bắt đầu / kết thúc bằng
text.StartsWith("Hello");            // true
text.EndsWith("C#!");                // true

// Vị trí
text.IndexOf("Hello");               // 0 (vị trí đầu tiên)
text.LastIndexOf("Hello");           // 13 (vị trí cuối cùng)
text.IndexOf("xyz");                 // -1 (không tìm thấy)

// Kiểm tra rỗng
string.IsNullOrEmpty(text);          // false
string.IsNullOrWhiteSpace("   ");    // true
```

### 2.2 Cắt & Trích xuất

```csharp
string text = "Hello World";

// Substring — cắt chuỗi con
text.Substring(6);                   // "World" (từ index 6 đến hết)
text.Substring(0, 5);               // "Hello" (từ 0, lấy 5 ký tự)

// Range operator (C# 8+) — hiện đại hơn
text[6..];                           // "World"
text[..5];                           // "Hello"
text[6..11];                         // "World"
text[^5..];                          // "World" (5 ký tự cuối)

// Ký tự
text[0];                             // 'H' (char, không phải string!)
text[^1];                            // 'd' (ký tự cuối)
text.Length;                         // 11
```

### So sánh Range với JavaScript:

```javascript
// JS — slice()
text.slice(6);        // "World"
text.slice(0, 5);     // "Hello"
text.slice(-5);       // "World" (5 ký tự cuối)
```

```csharp
// C# — Range operator
text[6..];            // "World"
text[..5];            // "Hello"
text[^5..];           // "World" (5 ký tự cuối)
```

### 2.3 Biến đổi

```csharp
string text = "  Hello World  ";

// Xóa khoảng trắng
text.Trim();                         // "Hello World"
text.TrimStart();                    // "Hello World  "
text.TrimEnd();                      // "  Hello World"

// Đổi hoa/thường
"hello".ToUpper();                   // "HELLO"
"HELLO".ToLower();                   // "hello"

// Thay thế
"Hello World".Replace("World", "C#");     // "Hello C#"
"aabbcc".Replace("b", "");                // "aacc" (xóa b)

// Xóa / Chèn
"Hello World".Remove(5);                  // "Hello"
"Hello World".Remove(5, 1);              // "HelloWorld" (xóa 1 ký tự tại 5)
"Hello World".Insert(5, " Beautiful");    // "Hello Beautiful World"

// Padding
"42".PadLeft(5, '0');                     // "00042"
"Hi".PadRight(10, '-');                   // "Hi--------"
```

### 2.4 Tách & Nối

```csharp
// Split — tách thành mảng
string csv = "Minh,25,HN,IT";
string[] parts = csv.Split(',');
// parts = ["Minh", "25", "HN", "IT"]

// Split với nhiều tùy chọn
"  Hello   World  ".Split(' ', StringSplitOptions.RemoveEmptyEntries);
// ["Hello", "World"] — bỏ phần tử rỗng

// Join — nối mảng thành chuỗi
string[] words = { "Hello", "World", "C#" };
string.Join(" ", words);              // "Hello World C#"
string.Join(", ", words);             // "Hello, World, C#"
string.Join("-", 1, 2, 3, 4, 5);     // "1-2-3-4-5"

// Concat
string.Concat("Hello", " ", "World"); // "Hello World"
```

### 2.5 So sánh

```csharp
string a = "hello";
string b = "HELLO";

// == (case-sensitive)
a == b;                               // false

// Equals (có option)
a.Equals(b, StringComparison.OrdinalIgnoreCase);    // true
string.Equals(a, b, StringComparison.OrdinalIgnoreCase); // true (null-safe)

// CompareTo (sắp xếp)
"apple".CompareTo("banana");          // < 0 (apple trước banana)
"banana".CompareTo("apple");          // > 0
"apple".CompareTo("apple");           // 0
```

---

## 3. `char` — Ký tự đơn

```csharp
char c = 'A';

// Kiểm tra loại ký tự
char.IsLetter('A');        // true
char.IsDigit('5');         // true
char.IsWhiteSpace(' ');    // true
char.IsUpper('A');         // true
char.IsLower('a');         // true
char.IsPunctuation('!');   // true

// Chuyển đổi
char.ToUpper('a');         // 'A'
char.ToLower('A');         // 'a'

// Duyệt từng ký tự trong string
string word = "Hello123";
int letters = 0, digits = 0;
foreach (char ch in word)
{
    if (char.IsLetter(ch)) letters++;
    else if (char.IsDigit(ch)) digits++;
}
Console.WriteLine($"Letters: {letters}, Digits: {digits}");
// Letters: 5, Digits: 3
```

---

## 4. `StringBuilder` — String có thể sửa

Mỗi lần nối string bằng `+` → tạo object MỚI → **chậm** với số lượng lớn.

```csharp
// ❌ CHẬM — tạo 1000 object string
string result = "";
for (int i = 0; i < 1000; i++)
{
    result += i.ToString() + ", ";  // Mỗi += tạo string mới!
}

// ✅ NHANH — StringBuilder sửa trực tiếp
using System.Text;

StringBuilder sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    sb.Append(i);
    sb.Append(", ");
}
string result = sb.ToString();
```

### Các method StringBuilder:

```csharp
StringBuilder sb = new StringBuilder();

sb.Append("Hello");            // Thêm cuối
sb.AppendLine(" World");       // Thêm + xuống dòng
sb.Insert(5, " Beautiful");    // Chèn tại vị trí
sb.Replace("Hello", "Hi");    // Thay thế
sb.Remove(0, 3);               // Xóa 3 ký tự từ vị trí 0
sb.Clear();                    // Xóa hết

string result = sb.ToString(); // Chuyển về string
sb.Length;                     // Độ dài hiện tại
```

### Khi nào dùng StringBuilder?

| Tình huống | Dùng |
|-----------|------|
| Nối < 10 chuỗi | `string` + `$""` |
| Nối trong vòng lặp | `StringBuilder` ✅ |
| Xây dựng HTML/JSON | `StringBuilder` ✅ |
| Nối cố định | `string.Concat` / `$""` |

---

## 5. String Formatting nâng cao

### 5.1 Composite Formatting:

```csharp
// Alignment + Format
Console.WriteLine("{0,-15} {1,10:N0} VNĐ", "iPhone", 25990000);
// "iPhone           25,990,000 VNĐ"

// Custom date format
DateTime now = DateTime.Now;
Console.WriteLine($"{now:dd/MM/yyyy HH:mm}");    // 16/05/2026 10:30
Console.WriteLine($"{now:dddd, dd MMMM yyyy}");  // Thứ Bảy, 16 Tháng Năm 2026
```

### 5.2 Custom number formats:

```csharp
int n = 42;
double pi = 3.14159;
decimal money = 1500000.5m;

Console.WriteLine($"{n:D5}");           // "00042"
Console.WriteLine($"{pi:#.##}");        // "3.14"
Console.WriteLine($"{money:#,##0.00}"); // "1,500,000.50"
Console.WriteLine($"{0.1567:0.00%}");   // "15.67%"
```

### 5.3 Verbatim & Raw strings:

```csharp
// Verbatim — @ prefix
string path = @"C:\Users\Documents\file.txt";
string multiLine = @"Line 1
Line 2
Line 3";

// Kết hợp $ và @
string name = "Minh";
string msg = $@"Xin chào {name},
Chào mừng bạn đến với hệ thống.
File: C:\Users\{name}\data.txt";

// Raw string literal (C# 11+) — 3+ dấu nháy
string json = """
    {
        "name": "Minh",
        "age": 25
    }
    """;
```

---

## 6. Regular Expressions — Giới thiệu

```csharp
using System.Text.RegularExpressions;

string email = "user@example.com";
string phone = "0912345678";

// Kiểm tra pattern
bool isEmail = Regex.IsMatch(email, @"^[\w.-]+@[\w.-]+\.\w+$");
bool isPhone = Regex.IsMatch(phone, @"^0\d{9}$");

Console.WriteLine($"Email valid: {isEmail}");   // true
Console.WriteLine($"Phone valid: {isPhone}");   // true

// Tìm tất cả số trong chuỗi
string text = "Tôi 25 tuổi, có 3 con, sống ở tầng 12";
MatchCollection matches = Regex.Matches(text, @"\d+");
foreach (Match m in matches)
    Console.Write($"{m.Value} ");
// Output: 25 3 12

// Thay thế
string result = Regex.Replace("Hello   World   C#", @"\s+", " ");
// "Hello World C#" — thay nhiều space thành 1
```

> 💡 Regex mạnh nhưng phức tạp. Biết cơ bản là đủ, sẽ dùng nhiều hơn khi làm backend.

---

## 7. Sai lầm phổ biến

### ❌ Nối string trong vòng lặp:
```csharp
// ❌ Chậm — tạo N object
string s = "";
for (int i = 0; i < 10000; i++)
    s += i;

// ✅ Nhanh — StringBuilder
var sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
    sb.Append(i);
```

### ❌ Quên string immutable:
```csharp
string name = "minh";
name.ToUpper();              // ❌ Kết quả bị bỏ! name vẫn = "minh"
name = name.ToUpper();       // ✅ Gán lại
```

### ❌ So sánh string không normalize:
```csharp
// ❌ "hello" != "HELLO"
if (input == "yes") { ... }

// ✅
if (input.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase)) { ... }
```

### ❌ NullReferenceException:
```csharp
string? s = null;
// int len = s.Length;        // ❌ CRASH!
int len = s?.Length ?? 0;     // ✅ An toàn
```

---

## 8. Best Practices

1. **`string.IsNullOrWhiteSpace()`** thay vì `== null` hoặc `== ""`
2. **`StringComparison.OrdinalIgnoreCase`** cho so sánh không phân biệt hoa/thường
3. **`StringBuilder`** khi nối nhiều chuỗi trong vòng lặp
4. **`$"..."`** (interpolation) cho format — rõ ràng nhất
5. **`Trim()`** mọi input từ user
6. **Range `[..]`** thay vì `Substring()` — hiện đại hơn
7. **`?.`** khi truy cập property/method trên nullable string
