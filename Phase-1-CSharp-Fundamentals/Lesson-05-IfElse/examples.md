# 💻 Lesson 05 — Ví dụ thực hành: If/Else

---

## Ví dụ 1: Xếp loại học lực

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhập điểm trung bình (0-10): ");

        if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Điểm không hợp lệ!");
            Console.ResetColor();
            return;
        }

        // Xếp loại — kiểm tra từ CAO xuống THẤP
        string grade;
        string emoji;
        ConsoleColor color;

        if (score >= 9.0)
        {
            grade = "Xuất sắc";
            emoji = "🏆";
            color = ConsoleColor.Magenta;
        }
        else if (score >= 8.0)
        {
            grade = "Giỏi";
            emoji = "🌟";
            color = ConsoleColor.Green;
        }
        else if (score >= 6.5)
        {
            grade = "Khá";
            emoji = "👍";
            color = ConsoleColor.Cyan;
        }
        else if (score >= 5.0)
        {
            grade = "Trung bình";
            emoji = "📝";
            color = ConsoleColor.Yellow;
        }
        else if (score >= 3.5)
        {
            grade = "Yếu";
            emoji = "⚠️";
            color = ConsoleColor.DarkYellow;
        }
        else
        {
            grade = "Kém";
            emoji = "❌";
            color = ConsoleColor.Red;
        }

        Console.ForegroundColor = color;
        Console.WriteLine($"\nĐiểm: {score:F1} → {grade} {emoji}");
        Console.ResetColor();

        // Thêm nhận xét
        if (score >= 8.0)
            Console.WriteLine("Tiếp tục phát huy! 💪");
        else if (score >= 5.0)
            Console.WriteLine("Cần cố gắng thêm!");
        else
            Console.WriteLine("Cần nỗ lực rất nhiều!");
    }
}
```

### 📝 Giải thích:

- Kiểm tra từ `9.0 → 8.0 → 6.5 → 5.0 → 3.5` (cao → thấp)
- Mỗi nhánh gán 3 biến cùng lúc: `grade`, `emoji`, `color`
- Dùng `ConsoleColor` cho output trực quan

---

## Ví dụ 2: Guard Clause — Xử lý đơn hàng

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== XỬ LÝ ĐƠN HÀNG ===\n");

        Console.Write("Tên khách hàng: ");
        string? customerName = Console.ReadLine()?.Trim();

        Console.Write("Số lượng: ");
        int.TryParse(Console.ReadLine(), out int quantity);

        Console.Write("Đơn giá: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);

        Console.Write("Mã giảm giá (Enter để bỏ qua): ");
        string? coupon = Console.ReadLine()?.Trim();

        // === Guard Clauses — kiểm tra và return sớm ===
        if (string.IsNullOrWhiteSpace(customerName))
        {
            PrintError("Tên khách hàng không được trống!");
            return;
        }
        if (quantity <= 0)
        {
            PrintError("Số lượng phải lớn hơn 0!");
            return;
        }
        if (price <= 0)
        {
            PrintError("Đơn giá phải lớn hơn 0!");
            return;
        }

        // === Logic chính — không nested! ===
        decimal subtotal = price * quantity;
        decimal discount = 0m;

        // Xử lý mã giảm giá
        if (!string.IsNullOrEmpty(coupon))
        {
            if (coupon.Equals("SALE10", StringComparison.OrdinalIgnoreCase))
                discount = subtotal * 0.10m;
            else if (coupon.Equals("SALE20", StringComparison.OrdinalIgnoreCase))
                discount = subtotal * 0.20m;
            else
                Console.WriteLine($"⚠️ Mã \"{coupon}\" không hợp lệ — bỏ qua.");
        }

        decimal total = subtotal - discount;

        // In kết quả
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ Đơn hàng cho: {customerName}");
        Console.WriteLine($"   {quantity} × {price:N0} = {subtotal:N0} VNĐ");
        if (discount > 0)
            Console.WriteLine($"   Giảm giá ({coupon}): -{discount:N0} VNĐ");
        Console.WriteLine($"   Tổng cộng: {total:N0} VNĐ");
        Console.ResetColor();
    }

    static void PrintError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ {msg}");
        Console.ResetColor();
    }
}
```

### 📝 Bài học:

- 3 guard clauses ở đầu → loại bỏ input sai sớm
- Logic chính ở cuối → **không bị nested** → dễ đọc
- `string.Equals(..., StringComparison.OrdinalIgnoreCase)` → so sánh không phân biệt hoa/thường

---

## Ví dụ 3: Nested If — Hệ thống phân quyền

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== HỆ THỐNG PHÂN QUYỀN ===\n");

        Console.Write("Username: ");
        string? username = Console.ReadLine()?.Trim();

        Console.Write("Role (admin/manager/staff/guest): ");
        string? role = Console.ReadLine()?.Trim().ToLower();

        Console.Write("Tài khoản active? (y/n): ");
        bool isActive = Console.ReadLine()?.Trim().ToLower() == "y";

        Console.Write("Đã xác thực 2FA? (y/n): ");
        bool has2FA = Console.ReadLine()?.Trim().ToLower() == "y";

        Console.WriteLine("\n--- Kết quả phân quyền ---");

        // Guard clause
        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("❌ Username không hợp lệ!");
            return;
        }
        if (!isActive)
        {
            Console.WriteLine("❌ Tài khoản bị khóa! Liên hệ admin.");
            return;
        }

        // Phân quyền theo role
        if (role == "admin")
        {
            if (has2FA)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("🔑 ADMIN — Full quyền:");
                Console.WriteLine("   ✅ Quản lý user");
                Console.WriteLine("   ✅ Xóa dữ liệu");
                Console.WriteLine("   ✅ Cấu hình hệ thống");
                Console.WriteLine("   ✅ Xem báo cáo");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ ADMIN — Yêu cầu xác thực 2FA để truy cập đầy đủ!");
                Console.WriteLine("   ✅ Xem báo cáo (chỉ đọc)");
                Console.WriteLine("   ❌ Quản lý user — cần 2FA");
                Console.WriteLine("   ❌ Xóa dữ liệu — cần 2FA");
            }
        }
        else if (role == "manager")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("👔 MANAGER:");
            Console.WriteLine("   ✅ Xem báo cáo");
            Console.WriteLine("   ✅ Duyệt yêu cầu");
            Console.WriteLine("   ❌ Quản lý user");
            Console.WriteLine("   ❌ Xóa dữ liệu");
        }
        else if (role == "staff")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("👤 STAFF:");
            Console.WriteLine("   ✅ Xem dữ liệu cá nhân");
            Console.WriteLine("   ✅ Tạo yêu cầu");
            Console.WriteLine("   ❌ Xem báo cáo");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("👻 GUEST — Chỉ xem trang công khai.");
        }

        Console.ResetColor();
    }
}
```

---

## Ví dụ 4: Pattern Matching (C# 9+)

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== PATTERN MATCHING DEMO ===\n");

        Console.Write("Nhập tuổi: ");
        if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
        {
            Console.WriteLine("❌ Tuổi không hợp lệ!");
            return;
        }

        // --- Relational pattern (C# 9+) ---
        string category = age switch
        {
            < 3 => "👶 Sơ sinh",
            < 13 => "🧒 Thiếu nhi",
            < 18 => "🧑 Thiếu niên",
            < 30 => "👨 Thanh niên",
            < 50 => "🧔 Trung niên",
            < 65 => "👴 Tiền hưu",
            _ => "🏖️ Nghỉ hưu"
        };

        Console.WriteLine($"Tuổi {age} → {category}");

        // --- is pattern ---
        Console.Write("\nNhập giá trị bất kỳ: ");
        string? input = Console.ReadLine();

        if (input is null or "")
        {
            Console.WriteLine("Không nhập gì!");
        }
        else if (int.TryParse(input, out int num))
        {
            // Dùng relational pattern để phân loại
            if (num is > 0 and <= 100)
                Console.WriteLine($"{num} nằm trong khoảng 1-100");
            else if (num is < 0)
                Console.WriteLine($"{num} là số âm");
            else
                Console.WriteLine($"{num} lớn hơn 100 hoặc bằng 0");
        }
        else
        {
            Console.WriteLine($"\"{input}\" là chuỗi ký tự");
        }

        // --- Ứng dụng: Giá vé ---
        Console.WriteLine("\n--- Giá vé theo tuổi ---");
        decimal ticketPrice = age switch
        {
            < 6 => 0m,                  // Miễn phí
            >= 6 and < 12 => 50000m,     // Trẻ em
            >= 12 and < 18 => 80000m,    // Thiếu niên
            >= 18 and < 60 => 120000m,   // Người lớn
            >= 60 => 60000m,             // Người cao tuổi
            _ => 120000m
        };

        Console.WriteLine($"Tuổi {age}: Vé = {ticketPrice:N0} VNĐ");
        if (ticketPrice == 0)
            Console.WriteLine("🎁 Miễn phí cho trẻ dưới 6 tuổi!");
    }
}
```

### 📝 Giải thích:

| Cú pháp | Ý nghĩa |
|---------|---------|
| `age switch { < 3 => "..." }` | Switch expression — gán giá trị dựa trên pattern |
| `< 3` | Relational pattern — nhỏ hơn 3 |
| `>= 6 and < 12` | Kết hợp 2 điều kiện |
| `is null or ""` | Kiểm tra null hoặc rỗng |
| `is > 0 and <= 100` | Trong khoảng 1-100 |
| `_` | Default — trường hợp còn lại |

---

## Ví dụ 5: Ứng dụng tổng hợp — Tính thuế thu nhập cá nhân

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║   TÍNH THUẾ THU NHẬP CÁ NHÂN       ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        // Nhập thu nhập
        Console.Write("Thu nhập hàng tháng (VNĐ): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal income) || income < 0)
        {
            Console.WriteLine("❌ Thu nhập không hợp lệ!");
            return;
        }

        // Nhập số người phụ thuộc
        Console.Write("Số người phụ thuộc: ");
        if (!int.TryParse(Console.ReadLine(), out int dependents) || dependents < 0)
        {
            Console.WriteLine("❌ Số người phụ thuộc không hợp lệ!");
            return;
        }

        // Các mức giảm trừ
        const decimal PERSONAL_DEDUCTION = 11000000m;       // Giảm trừ bản thân
        const decimal DEPENDENT_DEDUCTION = 4400000m;       // Giảm trừ mỗi người phụ thuộc
        const decimal INSURANCE_RATE = 0.105m;              // BHXH + BHYT + BHTN = 10.5%

        // Tính
        decimal insurance = income * INSURANCE_RATE;
        decimal totalDeduction = PERSONAL_DEDUCTION + (DEPENDENT_DEDUCTION * dependents);
        decimal taxableIncome = income - insurance - totalDeduction;

        // Thuế lũy tiến
        decimal tax = 0m;
        string taxBracket = "";

        if (taxableIncome <= 0)
        {
            tax = 0m;
            taxBracket = "Không chịu thuế";
        }
        else if (taxableIncome <= 5000000m)
        {
            tax = taxableIncome * 0.05m;
            taxBracket = "Bậc 1 (5%)";
        }
        else if (taxableIncome <= 10000000m)
        {
            tax = 5000000m * 0.05m
                + (taxableIncome - 5000000m) * 0.10m;
            taxBracket = "Bậc 2 (10%)";
        }
        else if (taxableIncome <= 18000000m)
        {
            tax = 5000000m * 0.05m
                + 5000000m * 0.10m
                + (taxableIncome - 10000000m) * 0.15m;
            taxBracket = "Bậc 3 (15%)";
        }
        else if (taxableIncome <= 32000000m)
        {
            tax = 5000000m * 0.05m
                + 5000000m * 0.10m
                + 8000000m * 0.15m
                + (taxableIncome - 18000000m) * 0.20m;
            taxBracket = "Bậc 4 (20%)";
        }
        else
        {
            tax = 5000000m * 0.05m
                + 5000000m * 0.10m
                + 8000000m * 0.15m
                + 14000000m * 0.20m
                + (taxableIncome - 32000000m) * 0.25m;
            taxBracket = "Bậc 5+ (25%+)";
        }

        decimal netIncome = income - insurance - tax;

        // In kết quả
        Console.WriteLine("\n════════════════════════════════════════");
        Console.WriteLine($"  Thu nhập:           {income,18:N0} VNĐ");
        Console.WriteLine($"  Bảo hiểm (10.5%):  {insurance,18:N0} VNĐ");
        Console.WriteLine($"  Giảm trừ bản thân: {PERSONAL_DEDUCTION,18:N0} VNĐ");
        Console.WriteLine($"  Giảm trừ PT ({dependents} người):{DEPENDENT_DEDUCTION * dependents,13:N0} VNĐ");
        Console.WriteLine("  ──────────────────────────────────────");
        Console.WriteLine($"  Thu nhập chịu thuế:{taxableIncome,18:N0} VNĐ");
        Console.WriteLine($"  Bậc thuế:          {taxBracket}");

        Console.ForegroundColor = taxableIncome <= 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"  Thuế TNCN:         {tax,18:N0} VNĐ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  ══════════════════════════════════════");
        Console.WriteLine($"  THU NHẬP THỰC NHẬN:{netIncome,18:N0} VNĐ");
        Console.ResetColor();
    }
}
```

### 📝 Bài học:

- Thuế lũy tiến = chuỗi `if...else if` kinh điển
- Mỗi bậc tính riêng, **cộng dồn** từ bậc trước
- Guard clause ở đầu → validate input
- Format alignment `{value,18:N0}` → bảng đẹp
