# 💻 Lesson 06 — Ví dụ thực hành: Switch

---

## Ví dụ 1: Menu nhà hàng — Switch truyền thống

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║     🍜 MENU NHÀ HÀNG        ║");
        Console.WriteLine("╠══════════════════════════════╣");
        Console.WriteLine("║  1. Phở bò          55,000  ║");
        Console.WriteLine("║  2. Bún chả          50,000  ║");
        Console.WriteLine("║  3. Cơm tấm          45,000  ║");
        Console.WriteLine("║  4. Bánh mì           25,000  ║");
        Console.WriteLine("║  5. Nước uống         15,000  ║");
        Console.WriteLine("╚══════════════════════════════╝");

        Console.Write("\nChọn món (1-5): ");
        int.TryParse(Console.ReadLine(), out int choice);

        Console.Write("Số lượng: ");
        int.TryParse(Console.ReadLine(), out int qty);

        if (qty <= 0) { Console.WriteLine("❌ Số lượng phải > 0!"); return; }

        string foodName;
        decimal price;

        switch (choice)
        {
            case 1:
                foodName = "Phở bò";
                price = 55000m;
                break;
            case 2:
                foodName = "Bún chả";
                price = 50000m;
                break;
            case 3:
                foodName = "Cơm tấm";
                price = 45000m;
                break;
            case 4:
                foodName = "Bánh mì";
                price = 25000m;
                break;
            case 5:
                foodName = "Nước uống";
                price = 15000m;
                break;
            default:
                Console.WriteLine("❌ Món không tồn tại!");
                return;
        }

        decimal total = price * qty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ {foodName} × {qty} = {total:N0} VNĐ");
        Console.ResetColor();
    }
}
```

---

## Ví dụ 2: Switch Expression — Máy tính đơn giản

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhập số 1: ");
        double.TryParse(Console.ReadLine(), out double a);

        Console.Write("Phép tính (+, -, ×, ÷): ");
        string? op = Console.ReadLine()?.Trim();

        Console.Write("Nhập số 2: ");
        double.TryParse(Console.ReadLine(), out double b);

        // Switch Expression — gọn!
        var (result, valid) = op switch
        {
            "+" => (a + b, true),
            "-" => (a - b, true),
            "*" or "×" => (a * b, true),          // Multiple patterns
            "/" or "÷" when b != 0 => (a / b, true),  // when guard
            "/" or "÷" => (0.0, false),            // Chia cho 0
            _ => (0.0, false)
        };

        if (valid)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ {a} {op} {b} = {result:F4}");
        }
        else if (op == "/" || op == "÷")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Không thể chia cho 0!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Phép tính \"{op}\" không hợp lệ!");
        }
        Console.ResetColor();
    }
}
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `"*" or "×"` | Match nhiều giá trị |
| `when b != 0` | Guard — thêm điều kiện |
| `var (result, valid)` | Tuple deconstruction — nhận 2 giá trị cùng lúc |

---

## Ví dụ 3: Tuple Pattern — Oẳn tù tì

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("🎮 OẲN TÙ TÌ\n");

        string[] options = { "Kéo", "Búa", "Bao" };
        Random rng = new Random();

        Console.WriteLine("Chọn: 1-Kéo, 2-Búa, 3-Bao");
        Console.Write("Lựa chọn: ");

        if (!int.TryParse(Console.ReadLine(), out int playerChoice) || playerChoice < 1 || playerChoice > 3)
        {
            Console.WriteLine("❌ Lựa chọn không hợp lệ!"); return;
        }

        int computerChoice = rng.Next(1, 4);  // 1-3

        string playerHand = options[playerChoice - 1];
        string computerHand = options[computerChoice - 1];

        Console.WriteLine($"\n🧑 Bạn: {playerHand}");
        Console.WriteLine($"🤖 Máy: {computerHand}");

        // Tuple Pattern — so sánh 2 giá trị cùng lúc!
        string result = (playerChoice, computerChoice) switch
        {
            var (p, c) when p == c => "🤝 Hòa!",

            (1, 3) => "🎉 Bạn thắng! (Kéo cắt Bao)",
            (2, 1) => "🎉 Bạn thắng! (Búa đập Kéo)",
            (3, 2) => "🎉 Bạn thắng! (Bao bọc Búa)",

            _ => "😢 Bạn thua!"
        };

        ConsoleColor color = result.Contains("thắng") ? ConsoleColor.Green
                           : result.Contains("thua") ? ConsoleColor.Red
                           : ConsoleColor.Yellow;

        Console.ForegroundColor = color;
        Console.WriteLine($"\n{result}");
        Console.ResetColor();
    }
}
```

### 📝 Giải thích Tuple Pattern:

```csharp
(playerChoice, computerChoice) switch
{
    (1, 3) => "...",   // Player = Kéo (1), Computer = Bao (3) → Thắng
    var (p, c) when p == c => "...",  // Khi 2 giá trị bằng nhau → Hòa
    _ => "..."         // Còn lại → Thua
};
```

> 💡 Thay vì viết 9 cặp `if (player == 1 && computer == 3)`, tuple pattern giải quyết gọn gàng!

---

## Ví dụ 4: Relational Pattern — Tính cước giao hàng

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TÍNH CƯỚC GIAO HÀNG ===\n");

        Console.Write("Trọng lượng (kg): ");
        if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
        { Console.WriteLine("❌ Trọng lượng không hợp lệ!"); return; }

        Console.Write("Khoảng cách (km): ");
        if (!double.TryParse(Console.ReadLine(), out double distance) || distance <= 0)
        { Console.WriteLine("❌ Khoảng cách không hợp lệ!"); return; }

        Console.Write("Loại hàng (1-Thường, 2-Dễ vỡ, 3-Đông lạnh): ");
        int.TryParse(Console.ReadLine(), out int type);

        Console.Write("Giao nhanh? (y/n): ");
        bool isExpress = Console.ReadLine()?.Trim().ToLower() == "y";

        // Phí theo trọng lượng (switch expression + relational)
        decimal weightFee = weight switch
        {
            <= 1 => 15000m,
            <= 3 => 20000m,
            <= 5 => 30000m,
            <= 10 => 45000m,
            <= 20 => 65000m,
            _ => 65000m + (decimal)(weight - 20) * 3000m  // Mỗi kg thêm 3,000
        };

        // Phí theo khoảng cách
        decimal distanceFee = distance switch
        {
            <= 5 => 10000m,
            <= 20 => 10000m + (decimal)(distance - 5) * 2000m,
            <= 50 => 40000m + (decimal)(distance - 20) * 1500m,
            _ => 85000m + (decimal)(distance - 50) * 1000m
        };

        // Phụ thu loại hàng
        decimal typeFee = type switch
        {
            1 => 0m,
            2 => weightFee * 0.3m,     // Dễ vỡ: +30%
            3 => weightFee * 0.5m,     // Đông lạnh: +50%
            _ => 0m
        };

        string typeName = type switch
        {
            1 => "Thường",
            2 => "Dễ vỡ (+30%)",
            3 => "Đông lạnh (+50%)",
            _ => "Thường"
        };

        // Phụ thu giao nhanh
        decimal subtotal = weightFee + distanceFee + typeFee;
        decimal expressFee = isExpress ? subtotal * 0.5m : 0m;
        decimal total = subtotal + expressFee;

        // In kết quả
        Console.WriteLine("\n─────────────────────────────────");
        Console.WriteLine($"  Trọng lượng:   {weight:F1} kg");
        Console.WriteLine($"  Khoảng cách:   {distance:F1} km");
        Console.WriteLine($"  Loại hàng:     {typeName}");
        Console.WriteLine($"  Giao nhanh:    {(isExpress ? "Có (+50%)" : "Không")}");
        Console.WriteLine("─────────────────────────────────");
        Console.WriteLine($"  Phí trọng lượng: {weightFee,12:N0} VNĐ");
        Console.WriteLine($"  Phí khoảng cách: {distanceFee,12:N0} VNĐ");
        if (typeFee > 0) Console.WriteLine($"  Phụ thu loại:    {typeFee,12:N0} VNĐ");
        if (expressFee > 0) Console.WriteLine($"  Phụ thu nhanh:   {expressFee,12:N0} VNĐ");
        Console.WriteLine("─────────────────────────────────");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  TỔNG CƯỚC:       {total,12:N0} VNĐ");
        Console.ResetColor();
    }
}
```

### 📝 Bài học:

- `weight switch { <= 1 => 15000m, <= 3 => 20000m, ... }` — relational pattern thay if/else if
- `_` trong biểu thức → tính công thức cho trường hợp vượt ngưỡng
- Kết hợp nhiều switch expression cho từng yếu tố

---

## Ví dụ 5: Ứng dụng tổng hợp — Hệ thống chấm điểm thi

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║   HỆ THỐNG CHẤM ĐIỂM THI           ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        Console.Write("Họ tên thí sinh: ");
        string? name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("❌ Tên không hợp lệ!"); return; }

        Console.Write("Môn thi (toan/ly/hoa/anh/van): ");
        string? subject = Console.ReadLine()?.Trim().ToLower();

        Console.Write("Điểm (0-10): ");
        if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10)
        { Console.WriteLine("❌ Điểm không hợp lệ!"); return; }

        // Tên môn đầy đủ — switch expression
        string subjectFull = subject switch
        {
            "toan" => "Toán học",
            "ly" => "Vật lý",
            "hoa" => "Hóa học",
            "anh" => "Tiếng Anh",
            "van" => "Ngữ văn",
            _ => "Không xác định"
        };

        // Hệ số môn
        double coefficient = subject switch
        {
            "toan" => 2.0,
            "anh" => 1.5,
            _ => 1.0
        };

        // Xếp loại — relational pattern
        string grade = score switch
        {
            >= 9.0 => "A+",
            >= 8.5 => "A",
            >= 8.0 => "B+",
            >= 7.0 => "B",
            >= 6.0 => "C+",
            >= 5.0 => "C",
            >= 4.0 => "D",
            _ => "F"
        };

        // Nhận xét — tuple pattern (điểm, môn)
        string comment = (grade, subject) switch
        {
            ("A+" or "A", "toan") => "🏆 Xuất sắc! Tiềm năng toán học!",
            ("A+" or "A", _) => "🌟 Xuất sắc! Tiếp tục phát huy!",
            ("B+" or "B", _) => "👍 Tốt! Cần cải thiện thêm.",
            ("C+" or "C", _) => "📝 Trung bình. Cần nỗ lực hơn.",
            ("D", _) => "⚠️ Yếu. Cần ôn tập lại.",
            ("F", _) => "❌ Không đạt. Thi lại.",
            _ => ""
        };

        // Kết quả đậu/rớt
        bool passed = score >= 5.0;
        decimal weightedScore = (decimal)(score * coefficient);

        // In kết quả
        ConsoleColor gradeColor = grade switch
        {
            "A+" or "A" => ConsoleColor.Magenta,
            "B+" or "B" => ConsoleColor.Green,
            "C+" or "C" => ConsoleColor.Yellow,
            _ => ConsoleColor.Red
        };

        Console.WriteLine($"\n{'═', 0}══════════════════════════════════════");
        Console.WriteLine($"  Thí sinh:     {name}");
        Console.WriteLine($"  Môn thi:      {subjectFull} (hệ số {coefficient:F1})");
        Console.WriteLine($"  Điểm:         {score:F1}");
        Console.WriteLine($"  Điểm quy đổi: {weightedScore:F1}");

        Console.ForegroundColor = gradeColor;
        Console.WriteLine($"  Xếp loại:     {grade}");
        Console.WriteLine($"  Kết quả:      {(passed ? "ĐẠT ✅" : "KHÔNG ĐẠT ❌")}");
        Console.ResetColor();

        Console.WriteLine($"  Nhận xét:     {comment}");
        Console.WriteLine($"{'═', 0}══════════════════════════════════════");
    }
}
```

### 📝 Tổng hợp kỹ thuật:

| Kỹ thuật | Dùng ở đâu |
|----------|-----------|
| Switch expression đơn giản | Tên môn, hệ số |
| Relational pattern | Xếp loại điểm |
| Tuple pattern | Nhận xét (điểm + môn) |
| `or` pattern | Gộp nhiều grade: `"A+" or "A"` |
| Switch cho ConsoleColor | Màu theo xếp loại |
