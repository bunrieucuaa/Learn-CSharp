# 💻 Lesson 02 — Ví dụ thực hành: Data Types

---

## Ví dụ 1: Khám phá kích thước và phạm vi kiểu dữ liệu

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          BẢNG KÍCH THƯỚC KIỂU DỮ LIỆU C#              ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════╣");

        // === Kiểu số nguyên ===
        Console.WriteLine("║ --- KIỂU SỐ NGUYÊN ---                                 ║");
        Console.WriteLine($"║ byte    | {sizeof(byte)} byte  | {byte.MinValue,20} → {byte.MaxValue,-20} ║");
        Console.WriteLine($"║ short   | {sizeof(short)} bytes | {short.MinValue,20} → {short.MaxValue,-20} ║");
        Console.WriteLine($"║ int     | {sizeof(int)} bytes | {int.MinValue,20} → {int.MaxValue,-20} ║");
        Console.WriteLine($"║ long    | {sizeof(long)} bytes | {long.MinValue,20} → {long.MaxValue,-20} ║");

        Console.WriteLine("║                                                        ║");

        // === Kiểu số thực ===
        Console.WriteLine("║ --- KIỂU SỐ THỰC ---                                   ║");
        Console.WriteLine($"║ float   | {sizeof(float)} bytes | Độ chính xác: ~6-7 chữ số          ║");
        Console.WriteLine($"║ double  | {sizeof(double)} bytes | Độ chính xác: ~15-16 chữ số         ║");
        Console.WriteLine($"║ decimal | {sizeof(decimal)} bytes | Độ chính xác: ~28-29 chữ số         ║");

        Console.WriteLine("║                                                        ║");

        // === Kiểu khác ===
        Console.WriteLine("║ --- KIỂU KHÁC ---                                      ║");
        Console.WriteLine($"║ bool    | {sizeof(bool)} byte  | true / false                        ║");
        Console.WriteLine($"║ char    | {sizeof(char)} bytes | '{char.MinValue}' → U+FFFF (Unicode)          ║");

        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
    }
}
```

### 🖥️ Output:

```
╔══════════════════════════════════════════════════════════╗
║          BẢNG KÍCH THƯỚC KIỂU DỮ LIỆU C#              ║
╠══════════════════════════════════════════════════════════╣
║ --- KIỂU SỐ NGUYÊN ---                                 ║
║ byte    | 1 byte  |                    0 → 255                  ║
║ short   | 2 bytes |               -32768 → 32767                ║
║ int     | 4 bytes |          -2147483648 → 2147483647            ║
║ long    | 8 bytes | -9223372036854775808 → 9223372036854775807   ║
║                                                        ║
║ --- KIỂU SỐ THỰC ---                                   ║
║ float   | 4 bytes | Độ chính xác: ~6-7 chữ số          ║
║ double  | 8 bytes | Độ chính xác: ~15-16 chữ số         ║
║ decimal | 16 bytes | Độ chính xác: ~28-29 chữ số         ║
║                                                        ║
║ --- KIỂU KHÁC ---                                      ║
║ bool    | 1 byte  | true / false                        ║
║ char    | 2 bytes | '' → U+FFFF (Unicode)               ║
╚══════════════════════════════════════════════════════════╝
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `sizeof(int)` | Trả về kích thước (bytes) của kiểu dữ liệu |
| `int.MinValue` | Giá trị nhỏ nhất mà `int` có thể chứa |
| `int.MaxValue` | Giá trị lớn nhất mà `int` có thể chứa |
| `{value,20}` | Căn phải trong 20 ký tự |
| `{value,-20}` | Căn trái trong 20 ký tự |

---

## Ví dụ 2: Sự khác biệt giữa `double` và `decimal`

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SO SÁNH DOUBLE VÀ DECIMAL ===\n");

        // --- Test 1: 0.1 + 0.2 ---
        double dResult = 0.1 + 0.2;
        decimal mResult = 0.1m + 0.2m;

        Console.WriteLine("Test 1: 0.1 + 0.2");
        Console.WriteLine($"  double:  {dResult}");         // 0.30000000000000004
        Console.WriteLine($"  decimal: {mResult}");          // 0.3
        Console.WriteLine($"  double == 0.3?  {dResult == 0.3}");    // False ❌
        Console.WriteLine($"  decimal == 0.3? {mResult == 0.3m}");   // True ✅

        // --- Test 2: Tính tiền ---
        Console.WriteLine("\nTest 2: Tính tiền hàng");

        double dPrice = 19.99;
        double dQuantity = 3;
        double dTotal = dPrice * dQuantity;

        decimal mPrice = 19.99m;
        decimal mQuantity = 3;
        decimal mTotal = mPrice * mQuantity;

        Console.WriteLine($"  double:  19.99 × 3 = {dTotal}");   // Có thể bị sai số nhỏ
        Console.WriteLine($"  decimal: 19.99 × 3 = {mTotal}");   // 59.97 chính xác

        // --- Test 3: Cộng dồn nhiều lần ---
        Console.WriteLine("\nTest 3: Cộng 0.1 mười lần");

        double dSum = 0;
        for (int i = 0; i < 10; i++) dSum += 0.1;

        decimal mSum = 0;
        for (int i = 0; i < 10; i++) mSum += 0.1m;

        Console.WriteLine($"  double:  {dSum}");     // 0.9999999999999999
        Console.WriteLine($"  decimal: {mSum}");     // 1.0
        Console.WriteLine($"  double == 1.0?  {dSum == 1.0}");    // False ❌
        Console.WriteLine($"  decimal == 1.0? {mSum == 1.0m}");   // True ✅

        Console.WriteLine("\n💡 Kết luận: Luôn dùng DECIMAL cho tiền tệ!");
    }
}
```

### 📝 Giải thích:

- `0.1 + 0.2` với `double` cho kết quả `0.30000000000000004` — đây là **bug kinh điển** trong mọi ngôn ngữ dùng floating-point (cả JS!)
- `decimal` giải quyết vấn đề này vì lưu trữ theo hệ thập phân
- Trong ứng dụng thực tế: **sai 1 đồng** trong triệu giao dịch = **sai triệu đồng**

---

## Ví dụ 3: Ép kiểu (Type Casting)

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ÉP KIỂU TRONG C# ===\n");

        // --- 1. Implicit Casting (Tự động — Nhỏ → Lớn) ---
        Console.WriteLine("--- Implicit Casting (Tự động) ---");

        int myInt = 42;
        long myLong = myInt;       // int → long: tự động
        double myDouble = myLong;  // long → double: tự động

        Console.WriteLine($"int: {myInt} → long: {myLong} → double: {myDouble}");

        byte small = 100;
        int bigger = small;        // byte → int: tự động
        Console.WriteLine($"byte: {small} → int: {bigger}");

        // --- 2. Explicit Casting (Thủ công — Lớn → Nhỏ) ---
        Console.WriteLine("\n--- Explicit Casting (Thủ công) ---");

        double pi = 3.14159;
        int truncated = (int)pi;   // Cắt phần thập phân!
        Console.WriteLine($"double {pi} → int {truncated}");
        // Chú ý: KHÔNG làm tròn, chỉ CẮT

        double big = 9.99;
        int cut = (int)big;
        Console.WriteLine($"double {big} → int {cut}");
        // 9.99 → 9 (không phải 10!)

        // --- 3. Convert class ---
        Console.WriteLine("\n--- Convert class ---");

        string ageStr = "25";
        int age = Convert.ToInt32(ageStr);
        Console.WriteLine($"string \"{ageStr}\" → int {age}");

        string priceStr = "99.99";
        double price = Convert.ToDouble(priceStr);
        Console.WriteLine($"string \"{priceStr}\" → double {price}");

        string boolStr = "true";
        bool flag = Convert.ToBoolean(boolStr);
        Console.WriteLine($"string \"{boolStr}\" → bool {flag}");

        // --- 4. Parse vs TryParse ---
        Console.WriteLine("\n--- Parse vs TryParse ---");

        // Parse — khi biết chắc input hợp lệ
        int parsed = int.Parse("100");
        Console.WriteLine($"Parse \"100\" → {parsed}");

        // TryParse — khi KHÔNG chắc (an toàn)
        Console.WriteLine("\nThử TryParse với input hợp lệ:");
        bool ok1 = int.TryParse("200", out int value1);
        Console.WriteLine($"  TryParse(\"200\"): success = {ok1}, value = {value1}");

        Console.WriteLine("\nThử TryParse với input KHÔNG hợp lệ:");
        bool ok2 = int.TryParse("abc", out int value2);
        Console.WriteLine($"  TryParse(\"abc\"): success = {ok2}, value = {value2}");

        bool ok3 = int.TryParse("12.5", out int value3);
        Console.WriteLine($"  TryParse(\"12.5\"): success = {ok3}, value = {value3}");

        bool ok4 = int.TryParse("", out int value4);
        Console.WriteLine($"  TryParse(\"\"): success = {ok4}, value = {value4}");
    }
}
```

### 🖥️ Output:

```
=== ÉP KIỂU TRONG C# ===

--- Implicit Casting (Tự động) ---
int: 42 → long: 42 → double: 42
byte: 100 → int: 100

--- Explicit Casting (Thủ công) ---
double 3.14159 → int 3
double 9.99 → int 9

--- Convert class ---
string "25" → int 25
string "99.99" → double 99.99
string "true" → bool True

--- Parse vs TryParse ---
Parse "100" → 100

Thử TryParse với input hợp lệ:
  TryParse("200"): success = True, value = 200

Thử TryParse với input KHÔNG hợp lệ:
  TryParse("abc"): success = False, value = 0
  TryParse("12.5"): success = False, value = 0
  TryParse(""): success = False, value = 0
```

### 📝 Giải thích:

| Tình huống | Kết quả |
|-----------|---------|
| `(int)9.99` | → `9`, KHÔNG làm tròn, chỉ **cắt bỏ** phần thập phân |
| `TryParse("abc")` | → `false`, value = `0` (mặc định), **không crash** |
| `TryParse("12.5")` | → `false` vì `12.5` không phải số **nguyên** |
| `Parse("abc")` | → **Exception!** (chương trình crash nếu không bắt lỗi) |

---

## Ví dụ 4: Bẫy chia số nguyên (Integer Division)

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== BẪY CHIA SỐ NGUYÊN ===\n");

        int a = 7;
        int b = 2;

        // Chia 2 số int → kết quả là int (cắt phần thập phân!)
        int result1 = a / b;
        Console.WriteLine($"int / int:    {a} / {b} = {result1}");     // 3 ← SAI!

        // Gán vào double — VẪN SAI vì phép chia xảy ra trước khi gán!
        double result2 = a / b;
        Console.WriteLine($"→ double:     {a} / {b} = {result2}");     // 3.0 ← VẪN SAI!

        // Cách đúng 1: Ép 1 bên thành double
        double result3 = (double)a / b;
        Console.WriteLine($"(double)a / b: {a} / {b} = {result3}");   // 3.5 ✅

        // Cách đúng 2: Dùng literal double
        double result4 = 7.0 / 2;
        Console.WriteLine($"7.0 / 2:       = {result4}");              // 3.5 ✅

        // Ứng dụng: Tính trung bình điểm
        Console.WriteLine("\n=== TÍNH TRUNG BÌNH ĐIỂM ===");
        int math = 8, physics = 7, english = 9;
        int totalSubjects = 3;

        // SAI:
        double avgWrong = (math + physics + english) / totalSubjects;
        Console.WriteLine($"Sai:  ({math}+{physics}+{english}) / {totalSubjects} = {avgWrong}");  // 8.0

        // ĐÚNG:
        double avgRight = (double)(math + physics + english) / totalSubjects;
        Console.WriteLine($"Đúng: ({math}+{physics}+{english}) / {totalSubjects} = {avgRight}");  // 8.0

        // Thử với số lẻ hơn
        int score1 = 7, score2 = 8, score3 = 6;
        double avgWrong2 = (score1 + score2 + score3) / 3;
        double avgRight2 = (double)(score1 + score2 + score3) / 3;
        Console.WriteLine($"\nSai:  ({score1}+{score2}+{score3}) / 3 = {avgWrong2}");  // 7.0
        Console.WriteLine($"Đúng: ({score1}+{score2}+{score3}) / 3 = {avgRight2:F2}");  // 7.00

        Console.WriteLine("\n💡 Nhớ: int / int = int (cắt thập phân)!");
        Console.WriteLine("💡 Muốn kết quả thực → ép (double) trước khi chia!");
    }
}
```

### 📝 Bài học rút ra:

> 🔑 **`int / int = int`** — C# cắt phần thập phân, KHÔNG làm tròn. Đây là **lỗi logic** rất phổ biến ở người mới học C#. JavaScript không có lỗi này vì `number / number = number` (luôn có thập phân).

---

## Ví dụ 5: Overflow và checked

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== OVERFLOW DEMO ===\n");

        // --- int overflow ---
        int max = int.MaxValue;
        Console.WriteLine($"int.MaxValue = {max:N0}");  // 2,147,483,647
        Console.WriteLine($"int.MaxValue + 1 = {max + 1:N0}");  // -2,147,483,648 😱

        Console.WriteLine($"\nint.MinValue = {int.MinValue:N0}");  // -2,147,483,648
        Console.WriteLine($"int.MinValue - 1 = {int.MinValue - 1:N0}");  // 2,147,483,647 😱

        // --- byte overflow ---
        byte maxByte = 255;
        Console.WriteLine($"\nbyte.MaxValue = {maxByte}");
        byte overflowed = unchecked((byte)(maxByte + 1));  // 0
        Console.WriteLine($"byte 255 + 1 = {overflowed}");  // 0 — quay vòng!

        // --- Dùng checked để bắt overflow ---
        Console.WriteLine("\n--- Dùng checked ---");
        try
        {
            checked
            {
                int safMax = int.MaxValue;
                int willCrash = safMax + 1;  // ← Exception!
            }
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"✅ Bắt được overflow: {ex.Message}");
        }

        // --- Giải pháp: dùng kiểu lớn hơn ---
        Console.WriteLine("\n--- Giải pháp: dùng long ---");
        long safeResult = (long)int.MaxValue + 1;
        Console.WriteLine($"(long)int.MaxValue + 1 = {safeResult:N0}");  // 2,147,483,648 ✅
    }
}
```

### 📝 Giải thích:

- Overflow **không ném lỗi** theo mặc định — chương trình **chạy tiếp với kết quả sai**
- `checked { }` bật chế độ kiểm tra overflow → ném `OverflowException`
- Giải pháp thực tế: dùng kiểu lớn hơn (`long`) khi số có thể lớn

---

## Ví dụ 6: Nullable Types — Ứng dụng thực tế

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== HỒ SƠ NHÂN VIÊN (Nullable Demo) ===\n");

        // Nhân viên 1 — đầy đủ thông tin
        string name1 = "Nguyễn Văn A";
        int age1 = 28;
        double? salary1 = 15000000;     // Có lương
        string? department1 = "IT";     // Có phòng ban
        DateTime? startDate1 = new DateTime(2023, 3, 15);  // Có ngày bắt đầu
        int? bonus1 = 2000000;          // Có thưởng

        // Nhân viên 2 — thông tin chưa đầy đủ (mới phỏng vấn)
        string name2 = "Trần Thị B";
        int age2 = 24;
        double? salary2 = null;         // Chưa thỏa thuận lương
        string? department2 = null;     // Chưa phân phòng
        DateTime? startDate2 = null;    // Chưa xác định ngày bắt đầu
        int? bonus2 = null;             // Chưa có thưởng

        // In thông tin
        PrintEmployee(name1, age1, salary1, department1, startDate1, bonus1);
        Console.WriteLine();
        PrintEmployee(name2, age2, salary2, department2, startDate2, bonus2);
    }

    static void PrintEmployee(string name, int age, double? salary,
        string? department, DateTime? startDate, int? bonus)
    {
        Console.WriteLine($"👤 {name} (Tuổi: {age})");
        Console.WriteLine($"   Lương:     {(salary.HasValue ? $"{salary.Value:N0} VNĐ" : "Chưa xác định")}");
        Console.WriteLine($"   Phòng ban: {department ?? "Chưa phân công"}");
        Console.WriteLine($"   Ngày vào:  {(startDate.HasValue ? startDate.Value.ToString("dd/MM/yyyy") : "Chưa xác định")}");
        Console.WriteLine($"   Thưởng:    {(bonus.HasValue ? $"{bonus.Value:N0} VNĐ" : "Không có")}");

        // Tính tổng thu nhập
        double totalIncome = (salary ?? 0) + (bonus ?? 0);
        Console.WriteLine($"   Tổng TN:   {totalIncome:N0} VNĐ");
    }
}
```

### 🖥️ Output:

```
👤 Nguyễn Văn A (Tuổi: 28)
   Lương:     15,000,000 VNĐ
   Phòng ban: IT
   Ngày vào:  15/03/2023
   Thưởng:    2,000,000 VNĐ
   Tổng TN:   17,000,000 VNĐ

👤 Trần Thị B (Tuổi: 24)
   Lương:     Chưa xác định
   Phòng ban: Chưa phân công
   Ngày vào:  Chưa xác định
   Thưởng:    Không có
   Tổng TN:   0 VNĐ
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `double? salary = null` | Nullable double — lương có thể chưa xác định |
| `salary.HasValue` | Kiểm tra có giá trị hay null |
| `salary.Value` | Lấy giá trị (nếu có) |
| `department ?? "Chưa phân công"` | Nếu null → dùng giá trị mặc định |
| `(salary ?? 0)` | Nếu salary null → coi như 0 |

> 💡 **Thực tế:** Trong database và API, rất nhiều field có thể null (chưa điền, chưa xác định). Nullable types giúp C# xử lý trường hợp này **an toàn** thay vì crash.
