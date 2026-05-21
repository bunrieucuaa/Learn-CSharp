# 💻 Lesson 18 — Ví dụ: Access Modifier & Property

---

## Ví dụ 1: private field + public property

```csharp
using System;

class Student
{
    // Private fields
    private static int lastId = 0;
    private string _name = "";
    private double _score;

    // Auto property
    public int Id { get; private set; }

    // Full property — validation
    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim();
    }

    public double Score
    {
        get => _score;
        set => _score = value is >= 0 and <= 10 ? value : _score; // Giữ nguyên nếu sai
    }

    // Computed (readonly) properties
    public string Grade => Score switch
    {
        >= 8 => "Giỏi", >= 6.5 => "Khá", >= 5 => "TB", _ => "Yếu"
    };
    public bool IsPass => Score >= 5;

    // Constructor
    public Student(string name, double score)
    {
        Id = ++lastId;
        Name = name;    // Gọi setter → validate
        Score = score;  // Gọi setter → validate
    }

    public void Print() =>
        Console.WriteLine($"  [{Id:D3}] {Name,-15} {Score,5:F1}  {Grade,-5} {(IsPass ? "✅" : "❌")}");
}

class Program
{
    static void Main()
    {
        var s1 = new Student("Minh", 8.5);
        var s2 = new Student("", 6.0);        // Name → "Unknown"
        var s3 = new Student("Lan", 15);       // Score → 0 (invalid)

        Console.WriteLine("═══ DANH SÁCH ═══");
        s1.Print(); s2.Print(); s3.Print();

        // Property hoạt động như field
        s1.Score = 9.0;   // ✅ Gọi setter
        s1.Name = "  Minh Nguyễn  ";  // ✅ Auto trim
        // s1.Id = 999;   // ❌ LỖI! private set

        Console.WriteLine("\nSau sửa:");
        s1.Print();
    }
}
```

---

## Ví dụ 2: BankAccount hoàn chỉnh

```csharp
using System;

class BankAccount
{
    private static int _nextId = 1000;
    private string _pin;
    private int _failedAttempts = 0;

    // Properties
    public string AccountNumber { get; private set; }
    public string Owner { get; private set; }
    public decimal Balance { get; private set; }
    public bool IsLocked { get; private set; }

    // Computed
    public string Status => IsLocked ? "🔒 Khóa" : "✅ Hoạt động";

    // Constructor
    public BankAccount(string owner, string pin, decimal initialDeposit = 0)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Tên chủ TK không được rỗng!");
        if (pin.Length != 4 || !int.TryParse(pin, out _))
            throw new ArgumentException("PIN phải 4 chữ số!");
        if (initialDeposit < 0)
            throw new ArgumentException("Số tiền không được âm!");

        AccountNumber = $"VN{++_nextId}";
        Owner = owner;
        _pin = pin;
        Balance = initialDeposit;
    }

    // Public methods — giao diện
    public bool Deposit(decimal amount)
    {
        if (IsLocked || amount <= 0) return false;
        Balance += amount;
        return true;
    }

    public bool Withdraw(decimal amount, string pin)
    {
        if (IsLocked) return false;
        if (!VerifyPin(pin)) return false;
        if (amount <= 0 || amount > Balance) return false;
        Balance -= amount;
        return true;
    }

    public bool ChangePin(string oldPin, string newPin)
    {
        if (!VerifyPin(oldPin)) return false;
        if (newPin.Length != 4) return false;
        _pin = newPin;
        return true;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"  {AccountNumber}  {Owner,-12}  {Balance,14:N0} VNĐ  {Status}");
    }

    // Private methods — logic nội bộ
    private bool VerifyPin(string input)
    {
        if (input != _pin)
        {
            _failedAttempts++;
            if (_failedAttempts >= 3) IsLocked = true;
            return false;
        }
        _failedAttempts = 0;
        return true;
    }
}

class Program
{
    static void Main()
    {
        var acc = new BankAccount("Nguyễn Minh", "1234", 10000000m);
        acc.PrintStatement();

        acc.Deposit(5000000m);
        Console.WriteLine($"\nGửi 5tr → Dư: {acc.Balance:N0}");

        acc.Withdraw(3000000m, "1234");
        Console.WriteLine($"Rút 3tr → Dư: {acc.Balance:N0}");

        // Sai PIN 3 lần → khóa
        acc.Withdraw(100, "0000");
        acc.Withdraw(100, "0000");
        acc.Withdraw(100, "0000");
        Console.WriteLine($"\nStatus: {acc.Status}");

        // Bảo mật: không thể truy cập
        // Console.WriteLine(acc._pin);      // ❌ private
        // acc.Balance = 999999999;           // ❌ private set
    }
}
```

---

## Ví dụ 3: Product với computed properties

```csharp
using System;

class Product
{
    private decimal _price;
    private int _stock;

    public string Name { get; set; } = "";
    public string Category { get; set; } = "Chung";

    public decimal Price
    {
        get => _price;
        set => _price = value >= 0 ? value : throw new ArgumentException("Giá không âm!");
    }

    public int Stock
    {
        get => _stock;
        set => _stock = value >= 0 ? value : throw new ArgumentException("Tồn kho không âm!");
    }

    // Computed properties — tính từ fields khác
    public decimal InventoryValue => Price * Stock;
    public bool IsInStock => Stock > 0;
    public string StockStatus => Stock switch
    {
        0 => "🔴 Hết hàng",
        <= 5 => "🟡 Sắp hết",
        _ => "🟢 Còn hàng"
    };

    public Product(string name, decimal price, int stock, string category = "Chung")
    {
        Name = name; Price = price; Stock = stock; Category = category;
    }

    public void Print() =>
        Console.WriteLine($"  {Name,-18} {Price,12:N0}  {Stock,4}  {StockStatus,-12}  Value: {InventoryValue:N0}");
}

class Program
{
    static void Main()
    {
        Product[] products = {
            new Product("iPhone 15", 25990000m, 10, "Phone"),
            new Product("AirPods", 5990000m, 3, "Audio"),
            new Product("Case", 250000m, 0, "Accessory"),
        };

        Console.WriteLine($"  {"Tên",-18} {"Giá",12}  {"SL",4}  {"Status",-12}  Value");
        Console.WriteLine(new string('─', 70));
        foreach (var p in products) p.Print();
    }
}
```

---

# ✏️ Bài tập + 🏆 Challenge

## Bài 1: Refactor public → private + Property
Chuyển class Lesson 15 (tất cả field public) sang private + Property. Thêm validation.

## Bài 2: Class Employee hoàn chỉnh
Private: `_baseSalary`, `_bonus`, `_taxRate`. Property: `Name`, `Department`, `BaseSalary` (validate > 0), `NetSalary` (computed: base + bonus - tax).

## Bài 3: Inventory System
Class `InventoryItem` với private fields, properties có validate, computed `Value`, `Status`. Mảng 10 items, thống kê.

## Challenge: Hệ thống quản lý tài khoản ngân hàng 🏦
Class `Account` hoàn chỉnh: private fields, public properties (private set), PIN verify, khóa TK, lịch sử GD (private array), computed properties (Status, TotalTransactions). TẤT CẢ field phải private. KHÔNG được truy cập trực tiếp data từ ngoài class.

| Tiêu chí | Mức độ |
|----------|--------|
| Tất cả field private | ⭐⭐⭐ |
| Property với validate | ⭐⭐⭐ |
| Computed properties | ⭐⭐ |
| Private methods cho logic nội bộ | ⭐⭐ |
