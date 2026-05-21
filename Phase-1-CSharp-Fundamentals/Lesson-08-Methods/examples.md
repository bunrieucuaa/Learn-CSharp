# 💻 Lesson 08 — Ví dụ thực hành: Methods

---

## Ví dụ 1: Các loại method cơ bản

```csharp
using System;

class Program
{
    static void Main()
    {
        // 1. void, không tham số
        PrintHeader();

        // 2. void, có tham số
        Greet("Minh");
        Greet("Hùng");

        // 3. Có return
        int sum = Add(10, 20);
        Console.WriteLine($"10 + 20 = {sum}");

        // 4. Expression-bodied
        Console.WriteLine($"Max(7, 12) = {Max(7, 12)}");
        Console.WriteLine($"IsEven(4) = {IsEven(4)}");

        // 5. Default params
        PrintPrice(100000m);              // qty=1, tax=10%
        PrintPrice(100000m, 3);           // qty=3, tax=10%
        PrintPrice(100000m, 3, 0.05m);    // qty=3, tax=5%

        // 6. Named params
        PrintPrice(price: 50000m, taxRate: 0.08m);  // qty=1 (default)

        PrintFooter();
    }

    // void, không tham số
    static void PrintHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║   METHOD DEMO                ║");
        Console.WriteLine("╚══════════════════════════════╝\n");
        Console.ResetColor();
    }

    static void PrintFooter()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n════════ Kết thúc ════════");
        Console.ResetColor();
    }

    // void, có tham số
    static void Greet(string name) => Console.WriteLine($"👋 Xin chào {name}!");

    // Có return
    static int Add(int a, int b) => a + b;

    // Expression-bodied
    static int Max(int a, int b) => a > b ? a : b;
    static bool IsEven(int n) => n % 2 == 0;

    // Default params
    static void PrintPrice(decimal price, int qty = 1, decimal taxRate = 0.10m)
    {
        decimal subtotal = price * qty;
        decimal tax = subtotal * taxRate;
        decimal total = subtotal + tax;
        Console.WriteLine($"  {qty} × {price:N0} + tax({taxRate:P0}) = {total:N0} VNĐ");
    }
}
```

---

## Ví dụ 2: Tách logic từ Lesson trước thành Methods

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TÍNH BMI (Dùng Methods) ===\n");

        string name = ReadString("Họ tên: ");
        double weight = ReadDouble("Cân nặng (kg): ", 20, 300);
        double height = ReadDouble("Chiều cao (m): ", 0.5, 2.5);

        double bmi = CalculateBMI(weight, height);
        string category = GetBMICategory(bmi);
        ConsoleColor color = GetCategoryColor(bmi);

        // In kết quả
        PrintResult(name, weight, height, bmi, category, color);
    }

    // ===== INPUT METHODS =====
    static string ReadString(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    static double ReadDouble(string prompt, double min, double max)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"  ❌ Phải từ {min} đến {max}!");
        }
    }

    // ===== LOGIC METHODS =====
    static double CalculateBMI(double weight, double height)
        => weight / (height * height);

    static string GetBMICategory(double bmi) => bmi switch
    {
        < 18.5 => "Thiếu cân",
        < 25 => "Bình thường",
        < 30 => "Thừa cân",
        _ => "Béo phì"
    };

    static ConsoleColor GetCategoryColor(double bmi) => bmi switch
    {
        < 18.5 => ConsoleColor.Yellow,
        < 25 => ConsoleColor.Green,
        < 30 => ConsoleColor.Yellow,
        _ => ConsoleColor.Red
    };

    // ===== OUTPUT METHOD =====
    static void PrintResult(string name, double w, double h,
                            double bmi, string category, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"\n╔══════════════════════════╗");
        Console.WriteLine($"║  {name,-22} ║");
        Console.WriteLine($"║  {w:F1}kg / {h:F2}m             ║");
        Console.WriteLine($"║  BMI: {bmi:F1} → {category,-11} ║");
        Console.WriteLine($"╚══════════════════════════╝");
        Console.ResetColor();
    }
}
```

### 📝 Bài học:

| Method | Vai trò | Nguyên tắc |
|--------|---------|-----------|
| `ReadString()` | Đọc input string | **Reusable** — dùng lại cho mọi input string |
| `ReadDouble()` | Đọc input số + validate | **Reusable** — min/max khác nhau |
| `CalculateBMI()` | Tính toán thuần | **Pure function** — chỉ tính, không in |
| `GetBMICategory()` | Phân loại | **Single responsibility** |
| `PrintResult()` | Hiển thị | Tách riêng UI |

> 🔑 **Main() chỉ gọi methods** — đọc như một câu chuyện: Đọc tên → Đọc cân nặng → Tính BMI → In kết quả.

---

## Ví dụ 3: Method Overloading & Params

```csharp
using System;

class Program
{
    static void Main()
    {
        // Overloading — cùng tên, khác tham số
        Console.WriteLine("--- Overloading ---");
        Console.WriteLine($"Area(5):        {Area(5):F2}");         // Hình tròn
        Console.WriteLine($"Area(5, 3):     {Area(5, 3):F2}");     // Hình chữ nhật
        Console.WriteLine($"Area(5, 3, 4):  {Area(5, 3, 4):F2}");  // Tam giác

        // params — số lượng tham số linh hoạt
        Console.WriteLine("\n--- Params ---");
        Console.WriteLine($"Sum():      {Sum()}");
        Console.WriteLine($"Sum(1,2):   {Sum(1, 2)}");
        Console.WriteLine($"Sum(1..5):  {Sum(1, 2, 3, 4, 5)}");
        Console.WriteLine($"Avg(7,8,9): {Average(7, 8, 9):F2}");

        // out — trả nhiều giá trị
        Console.WriteLine("\n--- Out Parameter ---");
        if (TryParseMoney("1,500,000", out decimal money))
            Console.WriteLine($"Parsed: {money:N0} VNĐ");

        if (!TryParseMoney("abc", out _))  // _ = discard (không cần giá trị)
            Console.WriteLine("'abc' không phải số tiền hợp lệ!");
    }

    // Overloading: Tính diện tích
    static double Area(double radius)                          // Hình tròn
        => Math.PI * radius * radius;

    static double Area(double width, double height)            // Hình chữ nhật
        => width * height;

    static double Area(double a, double b, double c)           // Tam giác (Heron)
    {
        double s = (a + b + c) / 2;
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    // params
    static int Sum(params int[] numbers)
    {
        int total = 0;
        foreach (int n in numbers)
            total += n;
        return total;
    }

    static double Average(params double[] numbers)
        => numbers.Length == 0 ? 0 : Sum2(numbers) / numbers.Length;

    static double Sum2(double[] arr)
    {
        double total = 0;
        foreach (double n in arr) total += n;
        return total;
    }

    // out parameter
    static bool TryParseMoney(string input, out decimal result)
    {
        string cleaned = input.Replace(",", "").Replace(".", "").Trim();
        return decimal.TryParse(cleaned, out result);
    }
}
```

---

## Ví dụ 4: Recursion — Đệ quy

```csharp
using System;

class Program
{
    static void Main()
    {
        // Giai thừa
        Console.WriteLine($"5! = {Factorial(5)}");          // 120
        Console.WriteLine($"10! = {Factorial(10)}");        // 3628800

        // Fibonacci
        Console.Write("Fibonacci(10): ");
        for (int i = 0; i < 10; i++)
            Console.Write($"{Fibonacci(i)} ");
        Console.WriteLine();

        // Tính lũy thừa
        Console.WriteLine($"2^10 = {Power(2, 10)}");       // 1024

        // Đảo chuỗi
        Console.WriteLine($"Đảo 'Hello' = '{Reverse("Hello")}'"); // olleH

        // Đếm chữ số
        Console.WriteLine($"Chữ số của 12345: {CountDigits(12345)}"); // 5
    }

    static long Factorial(int n)
    {
        if (n <= 1) return 1;            // Base case
        return n * Factorial(n - 1);     // Recursive case
    }

    static int Fibonacci(int n)
    {
        if (n <= 0) return 0;            // Base case 1
        if (n == 1) return 1;            // Base case 2
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static long Power(int baseNum, int exp)
    {
        if (exp == 0) return 1;
        return baseNum * Power(baseNum, exp - 1);
    }

    static string Reverse(string s)
    {
        if (s.Length <= 1) return s;
        return Reverse(s[1..]) + s[0];   // Đệ quy: đảo phần còn lại + ký tự đầu
    }

    static int CountDigits(int n)
    {
        if (n < 10) return 1;
        return 1 + CountDigits(n / 10);
    }
}
```

### 📝 Mỗi đệ quy cần:

| Thành phần | Ý nghĩa |
|-----------|---------|
| **Base case** | Điều kiện dừng — PHẢI CÓ! |
| **Recursive case** | Gọi lại chính mình với input nhỏ hơn |
| **Tiến tới base case** | Mỗi lần gọi, input phải gần base case hơn |

---

## Ví dụ 5: Ứng dụng tổng hợp — Hệ thống quản lý (refactored)

```csharp
using System;

class Program
{
    // Data arrays
    static string[] names = new string[50];
    static double[] scores = new double[50];
    static int count = 0;

    static void Main()
    {
        string? choice;
        do
        {
            ShowMenu();
            choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": ListStudents(); break;
                case "3": SearchStudent(); break;
                case "4": ShowStatistics(); break;
                case "0": Console.WriteLine("👋 Tạm biệt!"); break;
                default: PrintError("Lựa chọn không hợp lệ!"); break;
            }

            if (choice != "0") Pause();
        } while (choice != "0");
    }

    // ===== UI METHODS =====
    static void ShowMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════╗");
        Console.WriteLine($"║  📋 QUẢN LÝ ({count}/50 HS)     ║");
        Console.WriteLine("╠════════════════════════════╣");
        Console.WriteLine("║  1. Thêm học sinh          ║");
        Console.WriteLine("║  2. Xem danh sách          ║");
        Console.WriteLine("║  3. Tìm kiếm               ║");
        Console.WriteLine("║  4. Thống kê               ║");
        Console.WriteLine("║  0. Thoát                  ║");
        Console.WriteLine("╚════════════════════════════╝");
        Console.ResetColor();
        Console.Write("\nChọn: ");
    }

    static void PrintError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ {msg}");
        Console.ResetColor();
    }

    static void PrintSuccess(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ {msg}");
        Console.ResetColor();
    }

    static void Pause()
    {
        Console.Write("\nNhấn Enter...");
        Console.ReadLine();
    }

    // ===== INPUT METHODS =====
    static string ReadString(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    static double ReadDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double val) && val >= min && val <= max)
                return val;
            PrintError($"Phải từ {min} đến {max}!");
        }
    }

    // ===== LOGIC METHODS =====
    static string GetGrade(double score) => score switch
    {
        >= 8 => "Giỏi",
        >= 6.5 => "Khá",
        >= 5 => "TB",
        _ => "Yếu"
    };

    static int FindByName(string keyword)
    {
        for (int i = 0; i < count; i++)
            if (names[i].Contains(keyword, StringComparison.OrdinalIgnoreCase))
                return i;
        return -1;
    }

    // ===== FEATURE METHODS =====
    static void AddStudent()
    {
        if (count >= 50) { PrintError("Danh sách đầy!"); return; }

        Console.WriteLine("\n--- Thêm học sinh ---");
        names[count] = ReadString("Tên: ");
        scores[count] = ReadDouble("Điểm (0-10): ", 0, 10);
        count++;
        PrintSuccess("Đã thêm!");
    }

    static void ListStudents()
    {
        if (count == 0) { PrintError("Chưa có ai!"); return; }

        Console.WriteLine($"\n{"#",3} {"Tên",-20} {"Điểm",6} {"Loại",-8}");
        Console.WriteLine(new string('─', 42));

        for (int i = 0; i < count; i++)
            Console.WriteLine($"{i + 1,3} {names[i],-20} {scores[i],6:F1} {GetGrade(scores[i]),-8}");
    }

    static void SearchStudent()
    {
        Console.Write("\nTìm tên: ");
        string? keyword = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(keyword)) return;

        bool found = false;
        for (int i = 0; i < count; i++)
        {
            if (names[i].Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  → {names[i]} — {scores[i]:F1} ({GetGrade(scores[i])})");
                found = true;
            }
        }
        if (!found) PrintError("Không tìm thấy!");
    }

    static void ShowStatistics()
    {
        if (count == 0) { PrintError("Chưa có dữ liệu!"); return; }

        double sum = 0, max = scores[0], min = scores[0];
        for (int i = 0; i < count; i++)
        {
            sum += scores[i];
            if (scores[i] > max) max = scores[i];
            if (scores[i] < min) min = scores[i];
        }

        Console.WriteLine($"\n📊 Thống kê ({count} HS):");
        Console.WriteLine($"  Trung bình: {sum / count:F2}");
        Console.WriteLine($"  Cao nhất:   {max:F1}");
        Console.WriteLine($"  Thấp nhất:  {min:F1}");
    }
}
```

### 📝 Kiến trúc code:

```
Main()
 ├── ShowMenu()           ← UI
 ├── AddStudent()         ← Feature
 │    ├── ReadString()    ← Input (reusable)
 │    ├── ReadDouble()    ← Input (reusable)
 │    └── PrintSuccess()  ← UI (reusable)
 ├── ListStudents()       ← Feature
 │    └── GetGrade()      ← Logic (pure function)
 ├── SearchStudent()      ← Feature
 └── ShowStatistics()     ← Feature
```

> 🔑 **Mỗi method làm 1 việc**, Main() chỉ điều phối. Code dễ đọc, dễ sửa, dễ test.
