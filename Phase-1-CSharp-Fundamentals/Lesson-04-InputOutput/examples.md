# 💻 Lesson 04 — Ví dụ thực hành: Input/Output

---

## Ví dụ 1: Nhập thông tin cá nhân cơ bản

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== NHẬP THÔNG TIN CÁ NHÂN ===\n");

        // Nhập tên — string, không cần ép kiểu
        Console.Write("Họ tên: ");
        string name = Console.ReadLine()?.Trim() ?? "Không tên";

        // Nhập tuổi — cần ép sang int
        Console.Write("Tuổi: ");
        int age = 0;
        while (!int.TryParse(Console.ReadLine(), out age) || age < 1 || age > 120)
        {
            Console.Write("❌ Tuổi không hợp lệ (1-120). Nhập lại: ");
        }

        // Nhập chiều cao — cần ép sang double
        Console.Write("Chiều cao (m): ");
        double height = 0;
        while (!double.TryParse(Console.ReadLine(), out height) || height < 0.5 || height > 2.5)
        {
            Console.Write("❌ Chiều cao không hợp lệ (0.5-2.5m). Nhập lại: ");
        }

        // Nhập giới tính — chỉ chấp nhận M/F
        Console.Write("Giới tính (M/F): ");
        string gender = "";
        while (true)
        {
            gender = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (gender == "M" || gender == "F") break;
            Console.Write("❌ Chỉ nhập M hoặc F. Nhập lại: ");
        }

        // In kết quả
        Console.WriteLine("\n╔════════════════════════════╗");
        Console.WriteLine("║    THÔNG TIN CÁ NHÂN      ║");
        Console.WriteLine("╠════════════════════════════╣");
        Console.WriteLine($"║  Họ tên:    {name,-14} ║");
        Console.WriteLine($"║  Tuổi:      {age,-14} ║");
        Console.WriteLine($"║  Chiều cao: {height,-14:F2} ║");
        Console.WriteLine($"║  Giới tính: {(gender == "M" ? "Nam" : "Nữ"),-14} ║");
        Console.WriteLine("╚════════════════════════════╝");
    }
}
```

### 🖥️ Output mẫu:

```
=== NHẬP THÔNG TIN CÁ NHÂN ===

Họ tên: Nguyễn Văn A
Tuổi: abc
❌ Tuổi không hợp lệ (1-120). Nhập lại: 25
Chiều cao (m): 1.75
Giới tính (M/F): X
❌ Chỉ nhập M hoặc F. Nhập lại: M

╔════════════════════════════╗
║    THÔNG TIN CÁ NHÂN      ║
╠════════════════════════════╣
║  Họ tên:    Nguyễn Văn A   ║
║  Tuổi:      25             ║
║  Chiều cao: 1.75           ║
║  Giới tính: Nam            ║
╚════════════════════════════╝
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `Console.ReadLine()?.Trim()` | Đọc input, xóa khoảng trắng thừa, an toàn nếu null |
| `?? "Không tên"` | Nếu null → dùng giá trị mặc định |
| `while (!int.TryParse(...))` | Lặp hỏi lại cho đến khi nhập đúng số |
| `\|\| age < 1 \|\| age > 120` | Kiểm tra phạm vi hợp lệ |
| `.ToUpper()` | Chuyển thành chữ hoa để so sánh dễ hơn |
| `Console.Write()` | Prompt cùng dòng với input |

---

## Ví dụ 2: Máy tính bỏ túi

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════╗");
        Console.WriteLine("║    MÁY TÍNH BỎ TÚI      ║");
        Console.WriteLine("╚══════════════════════════╝\n");

        // Nhập số thứ nhất
        Console.Write("Nhập số thứ nhất: ");
        double num1;
        while (!double.TryParse(Console.ReadLine(), out num1))
        {
            Console.Write("❌ Không phải số! Nhập lại: ");
        }

        // Nhập phép tính
        Console.Write("Nhập phép tính (+, -, *, /): ");
        char op;
        while (true)
        {
            string? opInput = Console.ReadLine()?.Trim();
            if (opInput?.Length == 1 && "+-*/".Contains(opInput))
            {
                op = opInput[0];
                break;
            }
            Console.Write("❌ Phép tính không hợp lệ! Nhập lại (+, -, *, /): ");
        }

        // Nhập số thứ hai
        Console.Write("Nhập số thứ hai: ");
        double num2;
        while (!double.TryParse(Console.ReadLine(), out num2))
        {
            Console.Write("❌ Không phải số! Nhập lại: ");
        }

        // Kiểm tra chia cho 0
        if (op == '/' && num2 == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ LỖI: Không thể chia cho 0!");
            Console.ResetColor();
            return;  // Kết thúc chương trình
        }

        // Tính kết quả
        double result = op switch
        {
            '+' => num1 + num2,
            '-' => num1 - num2,
            '*' => num1 * num2,
            '/' => num1 / num2,
            _ => 0  // Không bao giờ xảy ra vì đã validate
        };

        // In kết quả
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ {num1} {op} {num2} = {result:F4}");
        Console.ResetColor();
    }
}
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `"+-*/".Contains(opInput)` | Kiểm tra ký tự có trong danh sách phép tính |
| `opInput[0]` | Lấy ký tự đầu tiên của string → `char` |
| `op switch { ... }` | Switch expression (C# 8+) — cách viết switch gọn |
| `_ => 0` | Default case trong switch expression |
| `return;` | Kết thúc method Main → thoát chương trình |
| `ConsoleColor.Red/Green` | Đổi màu chữ cho UX tốt hơn |

---

## Ví dụ 3: Format Output nâng cao — Bảng dữ liệu

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║              BẢNG GIÁ SẢN PHẨM                     ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");

        // Header
        Console.WriteLine($"║ {"STT",3} │ {"Sản phẩm",-18} │ {"Đơn giá",12} │ {"SL",3} │ {"Thành tiền",12} ║");
        Console.WriteLine("╠═════╪════════════════════╪══════════════╪═════╪══════════════╣");

        // Dữ liệu
        string[] names = { "iPhone 15 Pro", "AirPods Pro 2", "MacBook Air M3", "iPad Air" };
        decimal[] prices = { 28990000m, 5990000m, 32990000m, 16990000m };
        int[] quantities = { 1, 2, 1, 1 };

        decimal grandTotal = 0;

        for (int i = 0; i < names.Length; i++)
        {
            decimal lineTotal = prices[i] * quantities[i];
            grandTotal += lineTotal;

            Console.WriteLine($"║ {i + 1,3} │ {names[i],-18} │ {prices[i],12:N0} │ {quantities[i],3} │ {lineTotal,12:N0} ║");
        }

        Console.WriteLine("╠═════╧════════════════════╧══════════════╧═════╧══════════════╣");
        Console.WriteLine($"║ {"TỔNG CỘNG:",40} {grandTotal,12:N0} VNĐ ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        // Thêm thông tin VAT
        decimal vat = grandTotal * 0.10m;
        decimal finalTotal = grandTotal + vat;

        Console.WriteLine($"\n  Thuế VAT (10%): {vat,15:N0} VNĐ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  Tổng thanh toán: {finalTotal,14:N0} VNĐ");
        Console.ResetColor();
    }
}
```

### 📝 Giải thích format:

| Format | Ý nghĩa |
|--------|---------|
| `{i + 1, 3}` | Căn phải, chiều rộng 3 |
| `{names[i], -18}` | Căn trái, chiều rộng 18 |
| `{prices[i], 12:N0}` | Căn phải chiều rộng 12, format số có dấu phẩy |
| `{"TỔNG CỘNG:", 40}` | Căn phải string trong 40 ký tự |

---

## Ví dụ 4: Nhập nhiều giá trị — Split input

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TÍNH ĐIỂM TRUNG BÌNH ===\n");

        // Cách 1: Nhập từng môn
        Console.WriteLine("--- Cách 1: Nhập từng môn ---");
        Console.Write("Điểm Toán: ");
        double.TryParse(Console.ReadLine(), out double math);

        Console.Write("Điểm Lý: ");
        double.TryParse(Console.ReadLine(), out double physics);

        Console.Write("Điểm Hóa: ");
        double.TryParse(Console.ReadLine(), out double chemistry);

        double avg1 = (math + physics + chemistry) / 3;
        Console.WriteLine($"Trung bình: {avg1:F2}\n");

        // Cách 2: Nhập cùng 1 dòng (split bằng dấu cách)
        Console.WriteLine("--- Cách 2: Nhập cùng 1 dòng ---");
        Console.Write("Nhập 3 điểm (cách dấu cách): ");
        string? line = Console.ReadLine();

        if (line != null)
        {
            string[] parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 3
                && double.TryParse(parts[0], out double d1)
                && double.TryParse(parts[1], out double d2)
                && double.TryParse(parts[2], out double d3))
            {
                double avg2 = (d1 + d2 + d3) / 3;
                Console.WriteLine($"Điểm: {d1}, {d2}, {d3}");
                Console.WriteLine($"Trung bình: {avg2:F2}");
            }
            else
            {
                Console.WriteLine("❌ Vui lòng nhập đúng 3 số cách nhau bởi dấu cách!");
            }
        }

        // Cách 3: Nhập cách dấu phẩy
        Console.WriteLine("\n--- Cách 3: Nhập cách dấu phẩy ---");
        Console.Write("Nhập 3 điểm (cách dấu phẩy): ");
        string? line2 = Console.ReadLine();

        if (line2 != null)
        {
            string[] parts2 = line2.Split(',');

            if (parts2.Length == 3)
            {
                double sum = 0;
                bool allValid = true;

                for (int i = 0; i < parts2.Length; i++)
                {
                    if (double.TryParse(parts2[i].Trim(), out double score))
                    {
                        sum += score;
                        Console.WriteLine($"  Môn {i + 1}: {score}");
                    }
                    else
                    {
                        Console.WriteLine($"  Môn {i + 1}: ❌ Không hợp lệ");
                        allValid = false;
                    }
                }

                if (allValid)
                {
                    Console.WriteLine($"  Trung bình: {sum / 3:F2}");
                }
            }
        }
    }
}
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `line.Split(' ')` | Tách chuỗi theo dấu cách → mảng string |
| `line.Split(',')` | Tách theo dấu phẩy |
| `StringSplitOptions.RemoveEmptyEntries` | Bỏ qua các phần tử rỗng (nhiều dấu cách liên tiếp) |
| `parts[0].Trim()` | Xóa khoảng trắng ở phần tử đã tách |

---

## Ví dụ 5: Console Menu tương tác

```csharp
using System;

class Program
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            // Xóa màn hình và vẽ menu
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║      📱 MENU CHÍNH          ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. Tính BMI                ║");
            Console.WriteLine("║  2. Đổi nhiệt độ            ║");
            Console.WriteLine("║  3. Tính lãi suất           ║");
            Console.WriteLine("║  0. Thoát                   ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nChọn chức năng (0-3): ");
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    CalculateBMI();
                    break;
                case "2":
                    ConvertTemperature();
                    break;
                case "3":
                    CalculateInterest();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\n👋 Tạm biệt!");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Lựa chọn không hợp lệ!");
                    Console.ResetColor();
                    break;
            }

            if (running && choice != "0")
            {
                Console.Write("\nNhấn Enter để quay lại menu...");
                Console.ReadLine();
            }
        }
    }

    static void CalculateBMI()
    {
        Console.WriteLine("\n=== TÍNH BMI ===");

        Console.Write("Cân nặng (kg): ");
        if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
        {
            Console.WriteLine("❌ Cân nặng không hợp lệ!");
            return;
        }

        Console.Write("Chiều cao (m): ");
        if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
        {
            Console.WriteLine("❌ Chiều cao không hợp lệ!");
            return;
        }

        double bmi = weight / (height * height);
        string category = bmi < 18.5 ? "Thiếu cân"
                        : bmi < 25 ? "Bình thường"
                        : bmi < 30 ? "Thừa cân"
                        : "Béo phì";

        ConsoleColor color = bmi < 18.5 ? ConsoleColor.Yellow
                           : bmi < 25 ? ConsoleColor.Green
                           : bmi < 30 ? ConsoleColor.Yellow
                           : ConsoleColor.Red;

        Console.ForegroundColor = color;
        Console.WriteLine($"\nBMI: {bmi:F1} — {category}");
        Console.ResetColor();
    }

    static void ConvertTemperature()
    {
        Console.WriteLine("\n=== ĐỔI NHIỆT ĐỘ ===");
        Console.Write("Nhập °C: ");

        if (double.TryParse(Console.ReadLine(), out double celsius))
        {
            double fahrenheit = celsius * 9.0 / 5 + 32;
            double kelvin = celsius + 273.15;

            Console.WriteLine($"\n{celsius:F1}°C = {fahrenheit:F1}°F = {kelvin:F1}K");
        }
        else
        {
            Console.WriteLine("❌ Nhiệt độ không hợp lệ!");
        }
    }

    static void CalculateInterest()
    {
        Console.WriteLine("\n=== TÍNH LÃI SUẤT TIỀN GỬI ===");

        Console.Write("Số tiền gửi (VNĐ): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal principal) || principal <= 0)
        {
            Console.WriteLine("❌ Số tiền không hợp lệ!");
            return;
        }

        Console.Write("Lãi suất (%/năm): ");
        if (!double.TryParse(Console.ReadLine(), out double rate) || rate <= 0)
        {
            Console.WriteLine("❌ Lãi suất không hợp lệ!");
            return;
        }

        Console.Write("Số tháng gửi: ");
        if (!int.TryParse(Console.ReadLine(), out int months) || months <= 0)
        {
            Console.WriteLine("❌ Số tháng không hợp lệ!");
            return;
        }

        decimal interest = principal * (decimal)(rate / 100) * months / 12;
        decimal total = principal + interest;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nSố tiền gửi:  {principal,18:N0} VNĐ");
        Console.WriteLine($"Lãi suất:     {rate,18:F1}%/năm");
        Console.WriteLine($"Kỳ hạn:       {months,18} tháng");
        Console.WriteLine($"Tiền lãi:     {interest,18:N0} VNĐ");
        Console.WriteLine($"Tổng nhận:    {total,18:N0} VNĐ");
        Console.ResetColor();
    }
}
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `Console.Clear()` | Xóa màn hình trước khi vẽ lại menu |
| `while (running)` | Vòng lặp menu — chạy đến khi user chọn thoát |
| `Console.ReadLine()` sau mỗi chức năng | "Nhấn Enter để tiếp tục" — UX tốt hơn |
| Mỗi chức năng = 1 method | Tách code rõ ràng (sẽ học kỹ ở Lesson 08) |
| `return;` trong method | Thoát method sớm nếu input sai |

> 💡 **Đây là pattern phổ biến** cho console app: Menu → Chọn → Thực hiện → Quay lại menu.

---

## Ví dụ 6: Ứng dụng tổng hợp — Đăng ký tài khoản

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Title = "Đăng Ký Tài Khoản";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║     📋 ĐĂNG KÝ TÀI KHOẢN MỚI      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        // 1. Username (3-20 ký tự, không có khoảng trắng)
        string username;
        while (true)
        {
            Console.Write("\nUsername (3-20 ký tự): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                PrintError("Username không được trống!");
                continue;
            }
            if (input.Length < 3 || input.Length > 20)
            {
                PrintError($"Username phải 3-20 ký tự (hiện tại: {input.Length})");
                continue;
            }
            if (input.Contains(' '))
            {
                PrintError("Username không được chứa khoảng trắng!");
                continue;
            }

            username = input;
            break;
        }

        // 2. Email (phải chứa @ và .)
        string email;
        while (true)
        {
            Console.Write("Email: ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                PrintError("Email không được trống!");
                continue;
            }
            if (!input.Contains('@') || !input.Contains('.'))
            {
                PrintError("Email phải chứa @ và . (ví dụ: user@mail.com)");
                continue;
            }

            email = input;
            break;
        }

        // 3. Tuổi (13-100)
        int age;
        while (true)
        {
            Console.Write("Tuổi (13-100): ");
            if (int.TryParse(Console.ReadLine(), out age) && age >= 13 && age <= 100)
                break;

            PrintError("Tuổi phải từ 13 đến 100!");
        }

        // 4. Mật khẩu (>= 6 ký tự)
        string password;
        while (true)
        {
            Console.Write("Mật khẩu (≥6 ký tự): ");
            string? input = Console.ReadLine();

            if (input == null || input.Length < 6)
            {
                PrintError("Mật khẩu phải ít nhất 6 ký tự!");
                continue;
            }

            // Xác nhận mật khẩu
            Console.Write("Nhập lại mật khẩu: ");
            string? confirm = Console.ReadLine();

            if (input != confirm)
            {
                PrintError("Mật khẩu không khớp!");
                continue;
            }

            password = input;
            break;
        }

        // 5. Đồng ý điều khoản
        Console.Write("Đồng ý điều khoản? (y/n): ");
        string? agree = Console.ReadLine()?.Trim().ToLower();

        if (agree != "y" && agree != "yes")
        {
            PrintError("Bạn phải đồng ý điều khoản để đăng ký!");
            return;
        }

        // Hiển thị thông tin đăng ký
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║     ✅ ĐĂNG KÝ THÀNH CÔNG!          ║");
        Console.WriteLine("╠══════════════════════════════════════╣");
        Console.WriteLine($"║  Username: {username,-25} ║");
        Console.WriteLine($"║  Email:    {email,-25} ║");
        Console.WriteLine($"║  Tuổi:     {age,-25} ║");
        Console.WriteLine($"║  Mật khẩu: {new string('*', password.Length),-25} ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
    }

    // Helper method để in lỗi màu đỏ
    static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ❌ {message}");
        Console.ResetColor();
    }
}
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `string.IsNullOrWhiteSpace(input)` | Kiểm tra null, rỗng, hoặc toàn khoảng trắng |
| `input.Contains('@')` | Kiểm tra email có chứa @ |
| `input.Contains(' ')` | Kiểm tra có khoảng trắng không |
| `new string('*', password.Length)` | Tạo chuỗi `****` che mật khẩu |
| `continue` | Bỏ qua phần còn lại, quay lại đầu vòng lặp |
| `PrintError()` | Method helper — tái sử dụng code in lỗi |

> 💡 **Bài học:** Một form đăng ký đơn giản đã cần **rất nhiều validation**. Trong thực tế, validation còn phức tạp hơn (regex email, password strength, check trùng username...).
