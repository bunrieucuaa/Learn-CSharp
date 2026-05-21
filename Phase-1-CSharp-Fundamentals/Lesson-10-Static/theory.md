# 📘 Lesson 10 — Static trong C#

---

## 1. Static là gì?

Keyword `static` nghĩa là: **thuộc về CLASS, không thuộc về object (instance)**.

```
┌──────────────── CLASS (bản thiết kế) ────────────────┐
│                                                       │
│   static members → tồn tại NGAY KHI chương trình chạy│
│   ┌─────────────┐                                     │
│   │ static field │  ← 1 bản duy nhất, chia sẻ chung  │
│   │ static method│  ← gọi qua TÊN CLASS              │
│   └─────────────┘                                     │
│                                                       │
│   instance members → tạo mới MỖI KHI tạo object      │
│   ┌─────────────┐   ┌─────────────┐                  │
│   │ Object A     │   │ Object B     │  ← mỗi cái riêng│
│   │ field riêng  │   │ field riêng  │                  │
│   └─────────────┘   └─────────────┘                  │
└───────────────────────────────────────────────────────┘
```

### Analogy đời thực:

| Concept | Ví dụ |
|---------|-------|
| `static` field | **Quy định chung** của trường học (giờ vào: 7h) — áp dụng cho TẤT CẢ học sinh |
| instance field | **Tên, điểm** của MỖI học sinh — mỗi người khác nhau |
| `static` method | **Thông báo trường** — không cần biết là ai, gửi chung |
| instance method | **Xem điểm cá nhân** — phải biết là HỌC SINH NÀO |

---

## 2. Tại sao cần biết Static?

Từ Lesson 01 đến giờ, bạn đã dùng `static` mọi nơi:

```csharp
class Program
{
    static void Main()  // ← static!
    {
        // ...
    }

    static int Add(int a, int b)  // ← static!
    {
        return a + b;
    }
}
```

**Tại sao?** Vì chưa học OOP (class/object), mọi thứ phải là `static` — gọi trực tiếp qua tên class mà không cần tạo object.

> 🔑 Sau khi học OOP (Phase 2), bạn sẽ viết **cả static lẫn instance members**. Bài này giúp hiểu rõ sự khác biệt.

---

## 3. Static Field — Biến chia sẻ

```csharp
class Counter
{
    static int count = 0;  // 1 bản duy nhất — chia sẻ cho TẤT CẢ

    public static void Increment()
    {
        count++;
    }

    public static int GetCount() => count;
}

// Gọi qua TÊN CLASS — không cần tạo object
Counter.Increment();
Counter.Increment();
Counter.Increment();
Console.WriteLine(Counter.GetCount());  // 3
```

### Memory — Static nằm ở đâu?

```
MEMORY
┌──────────────────────────────────────┐
│ STATIC AREA (tạo khi app khởi động) │
│  Counter.count = 3                   │
│  Program.Main()                      │
│  Math.PI = 3.14159...                │
├──────────────────────────────────────┤
│ STACK (biến local, tạm thời)         │
│  int x = 10;                         │
│  string name = "Minh";               │
├──────────────────────────────────────┤
│ HEAP (objects, tạo bằng new)         │
│  (chưa có — sẽ học ở OOP)            │
└──────────────────────────────────────┘
```

> 🔑 Static members nằm trong vùng nhớ riêng, **tồn tại từ đầu đến cuối chương trình**.

---

## 4. Static Method — Gọi không cần object

### 4.1 Static method bạn đã dùng:

```csharp
// Tất cả đều là STATIC methods!
Console.WriteLine("Hello");        // Console.WriteLine
int.TryParse("42", out int n);     // int.TryParse
Math.Sqrt(16);                     // Math.Sqrt
Math.Max(10, 20);                  // Math.Max
string.IsNullOrEmpty(s);           // string.IsNullOrEmpty
```

> 💡 Gọi bằng `ClassName.MethodName()` — đây là dấu hiệu nhận biết static method.

### 4.2 Viết static method:

```csharp
class MathHelper
{
    // Static method — gọi bằng MathHelper.Add()
    public static int Add(int a, int b) => a + b;
    public static int Max(int a, int b) => a > b ? a : b;
    public static bool IsEven(int n) => n % 2 == 0;
    public static double CircleArea(double r) => Math.PI * r * r;
}

// Gọi:
int sum = MathHelper.Add(5, 3);          // 8
double area = MathHelper.CircleArea(5);  // 78.54
```

### 4.3 Static method KHÔNG THỂ truy cập instance member:

```csharp
class Example
{
    int instanceField = 10;         // Instance — thuộc object
    static int staticField = 20;    // Static — thuộc class

    static void StaticMethod()
    {
        Console.WriteLine(staticField);   // ✅ OK — static thấy static
        // Console.WriteLine(instanceField); // ❌ LỖI! Static không thấy instance
        // Vì sao? Instance field cần OBJECT, nhưng static method không có object!
    }

    void InstanceMethod()
    {
        Console.WriteLine(staticField);   // ✅ OK — instance thấy static
        Console.WriteLine(instanceField); // ✅ OK — instance thấy instance
    }
}
```

### Quy tắc truy cập:

| Từ \ Đến | Static member | Instance member |
|-----------|--------------|-----------------|
| **Static method** | ✅ Được | ❌ Không |
| **Instance method** | ✅ Được | ✅ Được |

> 🔑 **Static không thấy instance** vì static tồn tại TRƯỚC khi bất kỳ object nào được tạo.

---

## 5. Static Class — Class không tạo object được

```csharp
// static class — KHÔNG THỂ tạo instance (new)
static class Validator
{
    public static bool IsValidAge(int age) => age >= 0 && age <= 150;
    public static bool IsValidEmail(string email) => email.Contains('@');
    public static bool IsNotEmpty(string? s) => !string.IsNullOrWhiteSpace(s);
}

// Gọi:
Validator.IsValidAge(25);           // true
Validator.IsValidEmail("a@b.com");  // true

// var v = new Validator();  // ❌ LỖI! Không thể new static class
```

### Khi nào dùng static class?

- ✅ Utility / Helper methods (không cần state)
- ✅ Extension methods (sẽ học sau)
- ✅ Hằng số nhóm lại

```csharp
static class AppConfig
{
    public const string APP_NAME = "My Store";
    public const string VERSION = "1.0.0";
    public const decimal TAX_RATE = 0.10m;
    public const int MAX_ITEMS = 100;
}

Console.WriteLine($"{AppConfig.APP_NAME} v{AppConfig.VERSION}");
```

### Ví dụ static class trong .NET:

| Class | Mục đích |
|-------|---------|
| `Console` | Xuất/nhập dữ liệu |
| `Math` | Phép toán |
| `Convert` | Chuyển đổi kiểu |
| `Environment` | Thông tin hệ thống |
| `File` | Thao tác file |

---

## 6. Static Constructor — Chạy 1 lần duy nhất

```csharp
class Database
{
    public static string ConnectionString { get; private set; } = "";

    // Static constructor — chạy 1 lần khi class được dùng lần đầu
    static Database()
    {
        Console.WriteLine("📦 Loading config...");
        ConnectionString = "Server=localhost;Database=MyDB";
        Console.WriteLine("✅ Database config loaded!");
    }

    public static void Query(string sql)
    {
        Console.WriteLine($"Executing: {sql}");
    }
}

// Lần gọi đầu tiên → static constructor chạy trước
Database.Query("SELECT * FROM Users");
// Output:
// 📦 Loading config...
// ✅ Database config loaded!
// Executing: SELECT * FROM Users

// Lần gọi sau → static constructor KHÔNG chạy lại
Database.Query("SELECT * FROM Products");
// Output: Executing: SELECT * FROM Products
```

### Đặc điểm static constructor:

- Không có `public/private` — tự động gọi
- Chạy **1 lần duy nhất** — lần đầu class được dùng
- Dùng để khởi tạo static fields

---

## 7. So sánh Static vs Instance (Preview OOP)

```csharp
class BankAccount
{
    // STATIC — chia sẻ cho TẤT CẢ tài khoản
    static decimal interestRate = 0.05m;   // Lãi suất ngân hàng — giống nhau
    static int totalAccounts = 0;          // Đếm tổng số TK

    // INSTANCE — riêng MỖI tài khoản
    string ownerName;
    decimal balance;

    // Constructor (sẽ học kỹ ở OOP)
    public BankAccount(string name, decimal initialBalance)
    {
        ownerName = name;
        balance = initialBalance;
        totalAccounts++;  // ← Mỗi TK mới, đếm tăng lên
    }

    // STATIC method — liên quan đến CLASS
    public static int GetTotalAccounts() => totalAccounts;
    public static void SetInterestRate(decimal rate) => interestRate = rate;

    // INSTANCE method — liên quan đến MỖI object
    public decimal GetBalance() => balance;
    public void Deposit(decimal amount) => balance += amount;
    public decimal CalculateInterest() => balance * interestRate;  // Dùng cả static lẫn instance!
}
```

```
BankAccount class
├── static interestRate = 0.05  ← 1 bản, dùng chung
├── static totalAccounts = 2     ← 1 bản, đếm chung
│
├── Object "Minh":  balance = 10,000,000  ← riêng
├── Object "Hùng":  balance = 5,000,000   ← riêng
```

---

## 8. So sánh với JavaScript

```javascript
// JS — static trong class
class MathHelper {
    static PI = 3.14159;                // Static field
    static add(a, b) { return a + b; }  // Static method
}
MathHelper.add(5, 3);  // Gọi qua tên class

// JS — không có static class (dùng object literal)
const Config = {
    APP_NAME: "My Store",
    VERSION: "1.0.0"
};
```

```csharp
// C# — static class
static class MathHelper
{
    public const double PI = 3.14159;
    public static int Add(int a, int b) => a + b;
}
MathHelper.Add(5, 3);
```

| | C# | JavaScript |
|--|-----|-----------|
| `static` keyword | ✅ Có | ✅ Có (ES6+) |
| Static class | ✅ `static class` (không new được) | ❌ Không có (dùng object literal) |
| Static constructor | ✅ Có | ❌ Không có |
| Truy cập | `ClassName.Method()` | `ClassName.method()` |

---

## 9. `static` trong Program.cs — Top-level statements

### .NET 6+ Top-level Statements:

```csharp
// Program.cs — .NET 6+
// Không cần class Program, không cần static void Main()
Console.WriteLine("Hello!");  // Chạy trực tiếp!

// Nhưng nếu viết method → vẫn phải viết bên ngoài hoặc dùng local function
int Add(int a, int b) => a + b;  // Local function — OK!
Console.WriteLine(Add(5, 3));
```

### So sánh 2 cách viết:

```csharp
// Cách 1: Truyền thống (Lesson 01-09 dùng cách này)
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello!");
    }

    static int Add(int a, int b) => a + b;
}

// Cách 2: Top-level statements (.NET 6+)
Console.WriteLine("Hello!");
int Add(int a, int b) => a + b;
```

> 💡 Top-level statements tự động biên dịch thành `static void Main()` bên dưới. Cả 2 cách đều OK.

---

## 10. Sai lầm phổ biến

### ❌ Dùng static cho mọi thứ:

```csharp
// ❌ Biến class-level static quá nhiều → "global state"
static string name;
static int age;
static decimal balance;
// → Khó test, khó mở rộng, dễ bug!

// ✅ Chỉ dùng static khi THẬT SỰ cần chia sẻ hoặc utility
```

### ❌ Truy cập instance từ static:

```csharp
class Bad
{
    int data = 10;
    static void Print()
    {
        // Console.WriteLine(data);  // ❌ LỖI!
    }
}
```

### ❌ Nhầm static field = "biến cục bộ":

```csharp
static int counter = 0;

static void Process()
{
    counter++;  // ⚠️ counter TÍCH LŨY giữa các lần gọi!
                // Khác local variable (reset mỗi lần gọi)
}
```

### ❌ Static class khi cần state phức tạp:

```csharp
// ❌ Static class với state phức tạp → spaghetti code
static class UserManager
{
    static string[] names = new string[100];
    static int[] ages = new int[100];
    static decimal[] balances = new decimal[100];
    // → Sẽ thay bằng class + OOP ở Phase 2
}
```

---

## 11. Best Practices

1. **Static cho utility/helper** không cần state: `Math`, `Validator`, `StringHelper`
2. **`const` cho hằng số** biết giá trị lúc compile
3. **Static field hạn chế** — chỉ dùng cho config, counter, shared state
4. **Tránh quá nhiều static** — dấu hiệu cần OOP
5. **Static class cho nhóm hằng số**: `AppConfig`, `ErrorMessages`
6. **Static constructor cho khởi tạo 1 lần**: load config, đọc file
7. **Khi nào bỏ static?** → Khi học OOP, chuyển dần sang instance methods
