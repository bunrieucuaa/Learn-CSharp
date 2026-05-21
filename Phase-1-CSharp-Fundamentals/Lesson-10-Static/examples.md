# 💻 Lesson 10 — Ví dụ thực hành: Static

---

## Ví dụ 1: Static vs Instance — So sánh trực quan

```csharp
using System;

class Student
{
    // STATIC — thuộc CLASS, chia sẻ cho tất cả
    static string schoolName = "THPT Lê Quý Đôn";
    static int totalStudents = 0;

    // INSTANCE — thuộc MỖI object, riêng biệt
    string name;
    double score;

    public Student(string name, double score)
    {
        this.name = name;
        this.score = score;
        totalStudents++;  // Mỗi học sinh mới → đếm tăng
    }

    // STATIC method — liên quan đến class
    public static string GetSchoolName() => schoolName;
    public static int GetTotalStudents() => totalStudents;

    // INSTANCE method — liên quan đến MỖI học sinh
    public string GetInfo() => $"{name} - {score:F1} điểm";
    public string GetGrade() => score switch
    {
        >= 8 => "Giỏi", >= 6.5 => "Khá", >= 5 => "TB", _ => "Yếu"
    };
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== STATIC vs INSTANCE ===\n");

        // Static — gọi TRƯỚC khi tạo bất kỳ object nào
        Console.WriteLine($"Trường: {Student.GetSchoolName()}");
        Console.WriteLine($"Tổng HS: {Student.GetTotalStudents()}");  // 0

        // Tạo objects (instances)
        Student s1 = new Student("Minh", 8.5);
        Student s2 = new Student("Hùng", 7.0);
        Student s3 = new Student("Lan", 9.2);

        // Instance — gọi trên TỪNG object
        Console.WriteLine($"\n{s1.GetInfo()} [{s1.GetGrade()}]");
        Console.WriteLine($"{s2.GetInfo()} [{s2.GetGrade()}]");
        Console.WriteLine($"{s3.GetInfo()} [{s3.GetGrade()}]");

        // Static — cập nhật tự động
        Console.WriteLine($"\nTổng HS: {Student.GetTotalStudents()}");  // 3

        // ❌ SAI: Student.GetInfo()     — không thể gọi instance method qua class
        // ❌ SAI: s1.GetTotalStudents() — không nên gọi static qua instance
    }
}
```

### 📝 Memory visualization:

```
STATIC AREA                    HEAP (Objects)
┌────────────────────┐        ┌──────────────┐
│ Student.schoolName │        │ s1:          │
│ = "THPT LQĐ"      │        │  name="Minh" │
│                    │        │  score=8.5   │
│ Student.total = 3  │        └──────────────┘
└────────────────────┘        ┌──────────────┐
                              │ s2:          │
                              │  name="Hùng" │
                              │  score=7.0   │
                              └──────────────┘
                              ┌──────────────┐
                              │ s3:          │
                              │  name="Lan"  │
                              │  score=9.2   │
                              └──────────────┘
```

---

## Ví dụ 2: Static Class — Utility Helpers

```csharp
using System;

// static class — KHÔNG tạo object được, chỉ chứa static members
static class InputHelper
{
    public static string ReadString(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int val) && val >= min && val <= max)
                return val;
            Console.WriteLine($"  ❌ Nhập số từ {min} đến {max}!");
        }
    }

    public static decimal ReadDecimal(string prompt, decimal min = 0, decimal max = decimal.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal val) && val >= min && val <= max)
                return val;
            Console.WriteLine($"  ❌ Nhập số từ {min:N0} đến {max:N0}!");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        Console.Write($"{prompt} (y/n): ");
        return Console.ReadLine()?.Trim().ToLower() == "y";
    }
}

static class FormatHelper
{
    public static string Currency(decimal amount) => $"{amount:N0} VNĐ";
    public static string Percent(double value) => $"{value:P1}";
    public static string PadCenter(string text, int width)
    {
        int padding = width - text.Length;
        int left = padding / 2;
        return text.PadLeft(left + text.Length).PadRight(width);
    }
}

class Program
{
    static void Main()
    {
        // Dùng utility classes
        string name = InputHelper.ReadString("Tên: ");
        int age = InputHelper.ReadInt("Tuổi: ", 1, 120);
        decimal salary = InputHelper.ReadDecimal("Lương: ", 1000000);

        Console.WriteLine($"\n{name}, {age} tuổi");
        Console.WriteLine($"Lương: {FormatHelper.Currency(salary)}");
        Console.WriteLine(FormatHelper.PadCenter("=== KẾT QUẢ ===", 30));

        // ❌ var helper = new InputHelper();  // LỖI! Không thể new static class
    }
}
```

### 📝 Bài học:

| Static class | Chức năng | Tại sao static? |
|-------------|----------|-----------------|
| `InputHelper` | Đọc input + validate | Không cần state, chỉ là tool |
| `FormatHelper` | Format dữ liệu | Không cần state, chỉ là tool |
| `Console` (.NET) | Xuất/nhập | Chỉ có 1 console |
| `Math` (.NET) | Toán học | Công thức không đổi |

---

## Ví dụ 3: Static Field — Singleton Counter & ID Generator

```csharp
using System;

class IdGenerator
{
    private static int lastId = 0;  // Static → dùng chung, tự tăng

    public static string NextId(string prefix = "ID")
    {
        lastId++;
        return $"{prefix}{lastId:D4}";  // ID0001, ID0002, ...
    }

    public static int GetLastId() => lastId;
    public static void Reset() => lastId = 0;
}

class AppLogger
{
    private static int logCount = 0;
    private static string[] logs = new string[100];

    public static void Log(string message)
    {
        if (logCount >= 100) return;

        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        logs[logCount] = $"[{timestamp}] {message}";
        logCount++;
    }

    public static void ShowLogs()
    {
        Console.WriteLine($"\n📋 Logs ({logCount} entries):");
        for (int i = 0; i < logCount; i++)
            Console.WriteLine($"  {logs[i]}");
    }
}

class Program
{
    static void Main()
    {
        // ID Generator — mỗi lần gọi tự tăng
        Console.WriteLine("=== ID Generator ===");
        Console.WriteLine(IdGenerator.NextId("ORD"));   // ORD0001
        Console.WriteLine(IdGenerator.NextId("ORD"));   // ORD0002
        Console.WriteLine(IdGenerator.NextId("USR"));   // USR0003

        // Logger — ghi log toàn app
        AppLogger.Log("App started");
        AppLogger.Log($"Generated {IdGenerator.GetLastId()} IDs");

        Console.WriteLine(IdGenerator.NextId("PRD"));   // PRD0004
        AppLogger.Log("Product created");

        AppLogger.ShowLogs();
    }
}
```

### 📝 Bài học:

- `lastId` là `static` → **tự tăng** qua các lần gọi (không reset)
- `logs[]` là `static` → **tích lũy** data trong toàn bộ chương trình
- Đây là pattern **"shared state"** — hữu ích cho counter, logger, config

---

## Ví dụ 4: Static Constructor & Config

```csharp
using System;

class AppConfig
{
    public static string AppName { get; private set; } = "";
    public static string Version { get; private set; } = "";
    public static decimal TaxRate { get; private set; }
    public static int MaxUsers { get; private set; }

    // Static constructor — chạy 1 lần duy nhất
    static AppConfig()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("[Config] Loading configuration...");

        // Trong thực tế sẽ đọc từ file/database
        AppName = "MiniShop";
        Version = "2.0.1";
        TaxRate = 0.10m;
        MaxUsers = 50;

        Console.WriteLine("[Config] ✅ Loaded successfully!");
        Console.ResetColor();
    }
}

class Program
{
    static void Main()
    {
        // Lần đầu truy cập → static constructor tự chạy
        Console.WriteLine($"Welcome to {AppConfig.AppName} v{AppConfig.Version}");
        Console.WriteLine($"Tax rate: {AppConfig.TaxRate:P0}");
        Console.WriteLine($"Max users: {AppConfig.MaxUsers}");

        // Lần sau truy cập → KHÔNG gọi lại constructor
        Console.WriteLine($"\n{AppConfig.AppName} is running...");
    }
}
```

### Output:

```
[Config] Loading configuration...
[Config] ✅ Loaded successfully!
Welcome to MiniShop v2.0.1
Tax rate: 10%
Max users: 50

MiniShop is running...
```

---

## Ví dụ 5: Ứng dụng tổng hợp — Mini Store với Static Architecture

```csharp
using System;

static class StoreConfig
{
    public const string STORE_NAME = "🏪 MiniMart";
    public const decimal TAX_RATE = 0.10m;
    public const decimal MEMBER_DISCOUNT = 0.05m;
    public const int MAX_PRODUCTS = 20;
}

static class StoreData
{
    // "Database" — mảng tĩnh
    public static string[] Names = new string[StoreConfig.MAX_PRODUCTS];
    public static decimal[] Prices = new decimal[StoreConfig.MAX_PRODUCTS];
    public static int[] Stocks = new int[StoreConfig.MAX_PRODUCTS];
    public static int Count = 0;

    // Seed data
    static StoreData()
    {
        AddProduct("Mì gói", 5000m, 50);
        AddProduct("Nước suối", 8000m, 30);
        AddProduct("Bánh mì", 15000m, 20);
        AddProduct("Sữa tươi", 28000m, 15);
        AddProduct("Trứng (10)", 35000m, 10);
    }

    public static void AddProduct(string name, decimal price, int stock)
    {
        if (Count >= StoreConfig.MAX_PRODUCTS) return;
        Names[Count] = name;
        Prices[Count] = price;
        Stocks[Count] = stock;
        Count++;
    }
}

static class StoreUI
{
    public static void ShowHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"╔═══════════════════════════════════╗");
        Console.WriteLine($"║  {StoreConfig.STORE_NAME,-31} ║");
        Console.WriteLine($"╚═══════════════════════════════════╝");
        Console.ResetColor();
    }

    public static void ShowProducts()
    {
        Console.WriteLine($"\n{"#",3} {"Sản phẩm",-15} {"Giá",12} {"Tồn",5}");
        Console.WriteLine(new string('─', 38));
        for (int i = 0; i < StoreData.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1,3} {StoreData.Names[i],-15} {StoreData.Prices[i],12:N0} {StoreData.Stocks[i],5}");
        }
    }

    public static void PrintLine() => Console.WriteLine(new string('─', 38));
}

static class StoreLogic
{
    public static decimal CalculateTotal(decimal subtotal, bool isMember)
    {
        decimal discount = isMember ? subtotal * StoreConfig.MEMBER_DISCOUNT : 0m;
        decimal afterDiscount = subtotal - discount;
        decimal tax = afterDiscount * StoreConfig.TAX_RATE;
        return afterDiscount + tax;
    }

    public static bool HasStock(int productIndex, int quantity)
        => productIndex >= 0
           && productIndex < StoreData.Count
           && StoreData.Stocks[productIndex] >= quantity;

    public static void ReduceStock(int productIndex, int quantity)
        => StoreData.Stocks[productIndex] -= quantity;
}

class Program
{
    static void Main()
    {
        StoreUI.ShowHeader();
        StoreUI.ShowProducts();

        // Bán hàng
        Console.Write("\nChọn SP (số thứ tự): ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > StoreData.Count)
        { Console.WriteLine("❌ Không hợp lệ!"); return; }

        int index = choice - 1;

        Console.Write("Số lượng: ");
        if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
        { Console.WriteLine("❌ Không hợp lệ!"); return; }

        if (!StoreLogic.HasStock(index, qty))
        { Console.WriteLine("❌ Không đủ hàng!"); return; }

        Console.Write("Thẻ thành viên? (y/n): ");
        bool isMember = Console.ReadLine()?.Trim().ToLower() == "y";

        // Tính tiền
        decimal subtotal = StoreData.Prices[index] * qty;
        decimal total = StoreLogic.CalculateTotal(subtotal, isMember);

        StoreLogic.ReduceStock(index, qty);

        // In hóa đơn
        StoreUI.PrintLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  {StoreData.Names[index]} × {qty}");
        Console.WriteLine($"  Tạm tính: {subtotal:N0} VNĐ");
        if (isMember)
            Console.WriteLine($"  Giảm TV:  -{subtotal * StoreConfig.MEMBER_DISCOUNT:N0} VNĐ");
        Console.WriteLine($"  VAT:      +{(subtotal - (isMember ? subtotal * StoreConfig.MEMBER_DISCOUNT : 0)) * StoreConfig.TAX_RATE:N0} VNĐ");
        Console.WriteLine($"  TỔNG:     {total:N0} VNĐ");
        Console.ResetColor();
    }
}
```

### 📝 Kiến trúc Static:

```
StoreConfig (static class)     ← Hằng số
StoreData (static class)       ← "Database" + seed data
StoreUI (static class)         ← Hiển thị
StoreLogic (static class)      ← Tính toán
Program (class)                ← Điều phối (Main)
```

> 🔑 Đây là cách tổ chức code **trước khi học OOP**. Sau Phase 2, sẽ chuyển sang class + object mạnh mẽ hơn.
