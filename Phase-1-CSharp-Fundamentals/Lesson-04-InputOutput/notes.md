# 📝 Lesson 04 — Notes: Input/Output

---

## 🧠 Tóm tắt kiến thức

### 1. Output — Xuất dữ liệu:

```csharp
Console.Write("text");       // Không xuống dòng
Console.WriteLine("text");   // Có xuống dòng (giống console.log JS)
```

### 2. Format Output:

```csharp
// String Interpolation ⭐ (khuyến khích)
Console.WriteLine($"Tên: {name}, Tuổi: {age}");

// Nối chuỗi
Console.WriteLine("Tên: " + name + ", Tuổi: " + age);

// Composite format
Console.WriteLine("Tên: {0}, Tuổi: {1}", name, age);
```

### 3. Format Specifiers:

```
N0   → 1,234,567      (số có dấu phẩy, 0 thập phân)
N2   → 1,234.56       (2 thập phân)
F2   → 3.14           (fixed-point 2 chữ số)
P0   → 15%            (percentage)
D5   → 00042          (padding số 0)
C    → $1,234.00      (currency)
```

### 4. Alignment:

```csharp
$"{value,10}"    // Căn phải, rộng 10
$"{value,-10}"   // Căn trái, rộng 10
$"{value,10:N0}" // Căn phải + format
```

### 5. Input — Nhận dữ liệu:

```csharp
// Đọc string
string? input = Console.ReadLine();

// Đọc số (AN TOÀN)
if (int.TryParse(input, out int number))
{
    // Dùng number
}

// Đọc 1 phím
ConsoleKeyInfo key = Console.ReadKey();
```

### 6. Input Validation Pattern:

```csharp
int value;
while (true)
{
    Console.Write("Nhập: ");
    if (int.TryParse(Console.ReadLine(), out value) && value > 0)
        break;
    Console.WriteLine("❌ Không hợp lệ!");
}
```

### 7. Xử lý string input:

```csharp
input?.Trim()                              // Xóa khoảng trắng
input?.ToLower()                           // Chuyển thường
input?.ToUpper()                           // Chuyển hoa
string.IsNullOrWhiteSpace(input)           // Kiểm tra null/rỗng/trắng
input!.Split(' ')                          // Tách theo dấu cách
```

---

## ⚠️ Những lỗi dễ quên

| # | Lỗi | Hậu quả | Cách tránh |
|---|------|---------|-----------|
| 1 | `Parse` thay `TryParse` | Crash khi nhập sai | Luôn dùng `TryParse` |
| 2 | Quên `Trim()` | Khoảng trắng thừa ảnh hưởng so sánh | `ReadLine()?.Trim()` |
| 3 | `WriteLine` thay `Write` cho prompt | Input xuống dòng mới, xấu | `Write` cho prompt |
| 4 | Quên `?` cho `ReadLine()` | Warning nullable | `string? input = ReadLine()` |
| 5 | Không validate input | Crash hoặc logic sai | Luôn kiểm tra null, phạm vi |
| 6 | Quên `ResetColor()` | Màu sắc "lan" sang text sau | Luôn reset sau khi đổi màu |
| 7 | Input số thực với dấu phẩy | `double.TryParse("1,5")` có thể fail | Phụ thuộc culture setting |

---

## 💡 Mindset quan trọng

### 1. "KHÔNG BAO GIỜ tin tưởng user"
- User sẽ nhập bất cứ thứ gì: chữ, số âm, rỗng, khoảng trắng, ký tự đặc biệt
- **Luôn validate**. Luôn dùng TryParse. Luôn kiểm tra phạm vi.

### 2. "UX quan trọng ngay cả ở console"
- Prompt rõ ràng: "Nhập tuổi (1-120): " tốt hơn "Nhập tuổi: "
- Thông báo lỗi cụ thể: "Tuổi phải từ 1 đến 120" tốt hơn "Sai!"
- Dùng màu sắc: xanh = OK, đỏ = lỗi, vàng = cảnh báo

### 3. "Input → Process → Output"
- Mọi chương trình đều theo mô hình IPO
- Tách rõ 3 phần: nhận dữ liệu → xử lý → hiển thị

### 4. "Vòng lặp validation = pattern chuẩn"
```
while (true)
{
    Hỏi → Kiểm tra → Đúng: break / Sai: thông báo lỗi
}
```
Pattern này sẽ dùng **rất nhiều** trong thực tế.

---

## 📊 So sánh C# vs JavaScript

| Đặc điểm | C# Console | JavaScript Browser |
|-----------|-----------|-------------------|
| Output | `Console.WriteLine()` | `console.log()` |
| Input | `Console.ReadLine()` | `prompt()` |
| Input type | Luôn `string` | Luôn `string` |
| Ép kiểu | `int.TryParse()` | `parseInt()`, `Number()` |
| Multiline string | `@"..."` hoặc `"""..."""` | `` `...` `` |
| String format | `$"Tên: {name}"` | `` `Tên: ${name}` `` |
| Màu console | `ConsoleColor` | `console.log('%c...')` |

---

## ✅ Checklist "Đã hiểu chưa?"

- [ ] Tôi biết dùng `Console.Write()` vs `Console.WriteLine()`
- [ ] Tôi biết dùng string interpolation `$"...{var}..."`
- [ ] Tôi biết các format specifiers: `N0`, `F2`, `P0`
- [ ] Tôi biết alignment: `{value,10}` căn phải, `{value,-10}` căn trái
- [ ] Tôi biết `Console.ReadLine()` trả về `string?`
- [ ] Tôi luôn dùng `TryParse` thay vì `Parse` cho input
- [ ] Tôi biết pattern validation với vòng lặp `while`
- [ ] Tôi biết dùng `.Trim()`, `.ToLower()`, `.Split()` cho input
- [ ] Tôi biết đổi màu console với `ConsoleColor`
- [ ] Tôi có thể viết menu tương tác bằng `while + switch`

---

## 🔗 Liên kết kiến thức

```
Lesson 01-03: Variables, Data Types, Operators
    ↓
Lesson 04: Input/Output ← BẠN Ở ĐÂY
    ↓
Lesson 05: If/Else (xử lý điều kiện từ input)
    ↓
Lesson 06: Switch (menu lựa chọn từ input)
    ↓
Lesson 07: Loop (lặp lại đến khi input đúng)
    ↓
Lesson 08: Methods (tách code thành functions)
    ↓
Lesson 12: Arrays (lưu nhiều giá trị input)
```

> 📌 **Bài tiếp theo:** Lesson 05 — If/Else: Cấu trúc rẽ nhánh, xử lý điều kiện phức tạp, pattern matching, kết hợp với input để xây dựng logic business.
