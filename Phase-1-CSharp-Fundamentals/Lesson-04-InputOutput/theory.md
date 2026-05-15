# 📘 Lesson 04 — Input/Output (Nhập/Xuất dữ liệu) trong C#

---

## 1. Tại sao cần Input/Output?

Các bài trước bạn đều **hardcode** dữ liệu — giá trị cố định trong code. Trong thực tế, chương trình cần:

- **Nhận dữ liệu** từ người dùng (Input)
- **Xử lý** dữ liệu
- **Hiển thị** kết quả (Output)

```
┌──────────┐     ┌───────────┐     ┌──────────┐
│  INPUT   │ ──▶ │  XỬ LÝ    │ ──▶ │  OUTPUT  │
│ (Nhập)   │     │ (Logic)   │     │ (Xuất)   │
└──────────┘     └───────────┘     └──────────┘
  Bàn phím        Operators          Console
  Console.         if/else
  ReadLine()       Tính toán
```

> 🔑 Đây là mô hình **IPO (Input → Process → Output)** — nền tảng của MỌI chương trình.

---

## 2. Output — Xuất dữ liệu ra console

### 2.1 `Console.Write()` vs `Console.WriteLine()`

```csharp
// Console.Write() — in ra nhưng KHÔNG xuống dòng
Console.Write("Hello ");
Console.Write("World");
// Output: Hello World (trên 1 dòng)

// Console.WriteLine() — in ra VÀ xuống dòng
Console.WriteLine("Hello");
Console.WriteLine("World");
// Output:
// Hello
// World
```

### So sánh với JavaScript:

| C# | JavaScript | Xuống dòng? |
|----|-----------|-------------|
| `Console.Write()` | `process.stdout.write()` | ❌ Không |
| `Console.WriteLine()` | `console.log()` | ✅ Có |

### 2.2 Các cách format output

#### Cách 1: Nối chuỗi bằng `+`

```csharp
string name = "Minh";
int age = 25;
Console.WriteLine("Tên: " + name + ", Tuổi: " + age);
// Output: Tên: Minh, Tuổi: 25
```

❌ **Nhược điểm:** Dài, khó đọc, dễ thiếu dấu cách.

#### Cách 2: String Interpolation `$"..."` ⭐ KHUYẾN KHÍCH

```csharp
string name = "Minh";
int age = 25;
Console.WriteLine($"Tên: {name}, Tuổi: {age}");
// Output: Tên: Minh, Tuổi: 25
```

✅ **Ưu điểm:** Gọn, dễ đọc, giống template literal JS.

#### Cách 3: `String.Format()`

```csharp
string name = "Minh";
int age = 25;
Console.WriteLine(String.Format("Tên: {0}, Tuổi: {1}", name, age));
// {0} = tham số đầu tiên (name), {1} = tham số thứ 2 (age)
```

#### Cách 4: Composite formatting

```csharp
Console.WriteLine("Tên: {0}, Tuổi: {1}", "Minh", 25);
// Giống String.Format nhưng trực tiếp trong WriteLine
```

### 2.3 Format Specifiers — Định dạng số

| Specifier | Ý nghĩa | Ví dụ | Output |
|-----------|---------|-------|--------|
| `N0` | Số nguyên có dấu phẩy | `{1234567:N0}` | `1,234,567` |
| `N2` | Số thực 2 decimal | `{3.14159:N2}` | `3.14` |
| `F2` | Fixed-point 2 decimal | `{3.14159:F2}` | `3.14` |
| `C` | Currency (tiền tệ) | `{1234:C}` | `$1,234.00` |
| `P0` | Percentage | `{0.15:P0}` | `15%` |
| `P2` | Percentage 2 decimal | `{0.1567:P2}` | `15.67%` |
| `D5` | Padding số 0 | `{42:D5}` | `00042` |
| `X` | Hexadecimal | `{255:X}` | `FF` |

```csharp
decimal price = 1500000m;
double rate = 0.0825;
int id = 42;

Console.WriteLine($"Giá: {price:N0} VNĐ");     // Giá: 1,500,000 VNĐ
Console.WriteLine($"Thuế: {rate:P2}");           // Thuế: 8.25%
Console.WriteLine($"ID: {id:D6}");               // ID: 000042
```

### 2.4 Alignment — Căn chỉnh

```csharp
// {value, width} — width dương = căn phải, âm = căn trái
Console.WriteLine($"|{"Tên",-15}|{"Tuổi",5}|{"Lương",15}|");
Console.WriteLine($"|{"Minh",-15}|{25,5}|{15000000,15:N0}|");
Console.WriteLine($"|{"Hùng",-15}|{30,5}|{20000000,15:N0}|");
```

Output:
```
|Tên            | Tuổi|          Lương|
|Minh           |   25|     15,000,000|
|Hùng           |   30|     20,000,000|
```

### 2.5 Escape Characters

| Escape | Ý nghĩa | Ví dụ |
|--------|---------|-------|
| `\n` | Xuống dòng | `"Dòng 1\nDòng 2"` |
| `\t` | Tab | `"Cột 1\tCột 2"` |
| `\\` | Dấu `\` | `"C:\\Users"` |
| `\"` | Dấu `"` | `"Anh ta nói \"Hello\""` |

```csharp
// Verbatim string — bỏ qua escape (dùng @ trước chuỗi)
string path = @"C:\Users\Documents\file.txt";  // Không cần \\
string multiLine = @"Dòng 1
Dòng 2
Dòng 3";  // Xuống dòng tự nhiên

// Raw string literal (C# 11+) — dùng 3 dấu nháy kép
string json = """
    {
        "name": "Minh",
        "age": 25
    }
    """;
```

### So sánh với JavaScript:

```javascript
// JS template literal
let name = "Minh";
console.log(`Tên: ${name}`);   // backtick `

// JS multiline
let text = `Dòng 1
Dòng 2`;
```
7
```csharp
// C# string interpolation
string name = "Minh";
Console.WriteLine($"Tên: {name}");  // dấu $ trước nháy kép

// C# multiline
string text = @"Dòng 1
Dòng 2";
```

---

## 3. Input — Nhận dữ liệu từ người dùng

### 3.1 `Console.ReadLine()`

```csharp
Console.Write("Nhập tên: ");       // Không xuống dòng — con trỏ chờ cùng dòng
string? input = Console.ReadLine(); // Đọc 1 dòng text, trả về string?

Console.WriteLine($"Xin chào {input}!");
```

```
Nhập tên: Minh█           ← Người dùng gõ "Minh" rồi Enter
Xin chào Minh!
```

### ⚠️ Lưu ý quan trọng:

1. **`ReadLine()` luôn trả về `string?`** (có thể null)
2. **Mọi input đều là string** — cần **ép kiểu** nếu muốn số
3. Nếu người dùng nhấn Enter mà không gõ gì → trả về `""` (chuỗi rỗng)
4. Nếu input stream đóng → trả về `null`

### 3.2 Chuyển đổi Input sang số

#### ❌ Cách nguy hiểm: `int.Parse()`

```csharp
Console.Write("Nhập tuổi: ");
int age = int.Parse(Console.ReadLine()!);  // Crash nếu user nhập "abc"!
```

#### ✅ Cách an toàn: `int.TryParse()` ⭐

```csharp
Console.Write("Nhập tuổi: ");
string? input = Console.ReadLine();

if (int.TryParse(input, out int age))
{
    Console.WriteLine($"Tuổi của bạn: {age}");
}
else
{
    Console.WriteLine("❌ Vui lòng nhập số hợp lệ!");
}
```

### Giải thích `TryParse` chi tiết:

```csharp
bool success = int.TryParse(input, out int result);
//   ↑                        ↑          ↑
//   |                        |          └── Biến chứa kết quả (nếu thành công)
//   |                        └── String cần chuyển đổi
//   └── true nếu thành công, false nếu thất bại
```

| Input | success | result |
|-------|---------|--------|
| `"25"` | `true` | `25` |
| `"abc"` | `false` | `0` |
| `"12.5"` | `false` | `0` (int không có thập phân) |
| `""` | `false` | `0` |
| `null` | `false` | `0` |

### 3.3 Chuyển đổi sang các kiểu khác

```csharp
// double
double.TryParse(input, out double height);

// decimal
decimal.TryParse(input, out decimal price);

// bool
bool.TryParse(input, out bool isActive);  // "true" hoặc "false"

// Nhiều giá trị trên 1 dòng (split)
Console.Write("Nhập 3 số (cách dấu cách): ");
string? line = Console.ReadLine();
string[] parts = line!.Split(' ');  // Tách bằng dấu cách

int.TryParse(parts[0], out int a);
int.TryParse(parts[1], out int b);
int.TryParse(parts[2], out int c);

Console.WriteLine($"Tổng: {a + b + c}");
```

### 3.4 `Console.ReadKey()` — Đọc 1 phím

```csharp
Console.Write("Nhấn phím bất kỳ để tiếp tục...");
ConsoleKeyInfo key = Console.ReadKey();  // Chờ 1 phím
Console.WriteLine();
Console.WriteLine($"Bạn nhấn: {key.KeyChar}");
Console.WriteLine($"Phím: {key.Key}");

// Ứng dụng: Menu lựa chọn
Console.WriteLine("Chọn: 1-Thêm, 2-Sửa, 3-Xóa");
ConsoleKeyInfo choice = Console.ReadKey(true);  // true = không hiện ký tự

if (choice.Key == ConsoleKey.D1)      // Phím '1'
    Console.WriteLine("→ Bạn chọn: Thêm");
else if (choice.Key == ConsoleKey.D2)
    Console.WriteLine("→ Bạn chọn: Sửa");
else if (choice.Key == ConsoleKey.D3)
    Console.WriteLine("→ Bạn chọn: Xóa");
```

---

## 4. Validation Input — Kiểm tra dữ liệu nhập

### 4.1 Pattern: Hỏi lại cho đến khi nhập đúng

```csharp
int age;

// Vòng lặp hỏi lại nếu nhập sai
while (true)
{
    Console.Write("Nhập tuổi (1-120): ");
    string? input = Console.ReadLine();

    if (int.TryParse(input, out age) && age >= 1 && age <= 120)
    {
        break;  // Nhập đúng → thoát vòng lặp
    }

    Console.WriteLine("❌ Tuổi không hợp lệ! Thử lại.");
}

Console.WriteLine($"✅ Tuổi: {age}");
```

### 4.2 Pattern: Xác nhận Yes/No

```csharp
Console.Write("Bạn có chắc chắn? (y/n): ");
string? confirm = Console.ReadLine()?.Trim().ToLower();

if (confirm == "y" || confirm == "yes")
{
    Console.WriteLine("✅ Đã xác nhận!");
}
else
{
    Console.WriteLine("❌ Đã hủy.");
}
```

### 4.3 Pattern: Validate chuỗi không rỗng

```csharp
string name;

while (true)
{
    Console.Write("Nhập tên: ");
    string? input = Console.ReadLine()?.Trim();

    if (!string.IsNullOrWhiteSpace(input))
    {
        name = input;
        break;
    }

    Console.WriteLine("❌ Tên không được để trống!");
}

Console.WriteLine($"Xin chào {name}!");
```

---

## 5. Console Formatting — Trang trí console

### 5.1 Màu sắc

```csharp
// Đổi màu chữ
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("✅ Thành công!");

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("❌ Lỗi!");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("⚠️ Cảnh báo!");

// Reset về mặc định
Console.ResetColor();
Console.WriteLine("Bình thường");

// Đổi màu nền
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("  MENU CHÍNH  ");
Console.ResetColor();
```

### 5.2 Xóa màn hình & Beep

```csharp
Console.Clear();               // Xóa toàn bộ console
Console.Beep();                // Tiếng beep
Console.Beep(800, 200);       // Beep tần số 800Hz, 200ms
Console.Title = "My App";     // Đổi tiêu đề cửa sổ console
```

### 5.3 Con trỏ

```csharp
Console.SetCursorPosition(10, 5);  // Đặt con trỏ tại cột 10, dòng 5
Console.Write("★");

Console.CursorVisible = false;  // Ẩn con trỏ nhấp nháy
```

---

## 6. Sai lầm phổ biến

### ❌ Sai lầm 1: Quên ép kiểu input

```csharp
Console.Write("Nhập số: ");
string input = Console.ReadLine()!;
// int result = input + 5;  // ❌ LỖI! input là string, không phải int

int number = int.Parse(input);  // Phải ép kiểu trước
int result = number + 5;        // ✅ OK
```

### ❌ Sai lầm 2: Dùng Parse thay vì TryParse

```csharp
// ❌ Crash nếu user nhập "abc"
int age = int.Parse(Console.ReadLine()!);

// ✅ An toàn
if (int.TryParse(Console.ReadLine(), out int age))
{
    // Xử lý
}
```

### ❌ Sai lầm 3: Quên `!` hoặc `?` cho ReadLine()

```csharp
// ReadLine() trả về string? (nullable)
string? input = Console.ReadLine();   // ✅ Đúng kiểu
string input = Console.ReadLine()!;   // ✅ Dùng ! nếu chắc chắn không null

// string input = Console.ReadLine();  // ⚠️ Warning: có thể null
```

### ❌ Sai lầm 4: Không Trim() input

```csharp
Console.Write("Nhập tên: ");
string name = Console.ReadLine()!;
// Nếu user gõ "  Minh  " (có khoảng trắng)
// name.Length = 8, không phải 4!

string cleanName = Console.ReadLine()!.Trim();  // ✅ Xóa khoảng trắng đầu/cuối
```

### ❌ Sai lầm 5: Nhầm `Console.Write` và `Console.WriteLine`

```csharp
// Muốn input cùng dòng với prompt:
Console.WriteLine("Nhập tên: ");  // ❌ Input ở dòng kế — xấu
Console.Write("Nhập tên: ");      // ✅ Input cùng dòng — đẹp
```

---

## 7. Best Practices

1. **Luôn dùng `TryParse`** khi nhận input số — KHÔNG BAO GIỜ tin tưởng user
2. **Luôn `Trim()`** input — user có thể gõ khoảng trắng thừa
3. **Dùng `Console.Write`** cho prompt (để input cùng dòng)
4. **Validate mọi input** — kiểm tra null, rỗng, phạm vi
5. **Dùng vòng lặp** để hỏi lại khi input sai
6. **Dùng string interpolation `$"..."`** cho output — gọn và rõ ràng
7. **Dùng format specifiers** cho số — `N0`, `F2`, `P0`
8. **Thêm màu sắc** cho UX tốt hơn — xanh = OK, đỏ = lỗi
