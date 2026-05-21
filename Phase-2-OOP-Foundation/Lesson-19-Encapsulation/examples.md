# 💻 Lesson 19 — Ví dụ: Encapsulation (Tính đóng gói)

---

## Ví dụ 1: Before / After Encapsulation

### ❌ BEFORE — Không có encapsulation (public fields):

```csharp
using System;

class StudentBad
{
    public string Name;
    public int Age;
    public double Score;
    public string Grade;
}

class Program
{
    static void Main()
    {
        var s = new StudentBad();

        // 💥 Mọi thứ đều public → data sai thoải mái!
        s.Name = "";              // Tên rỗng?
        s.Age = -5;               // Tuổi âm?
        s.Score = 999;            // Điểm 999/10?
        s.Grade = "Siêu nhân";   // Grade tự gán bậy?

        Console.WriteLine($"Name: {s.Name}");
        Console.WriteLine($"Age: {s.Age}");
        Console.WriteLine($"Score: {s.Score}");
        Console.WriteLine($"Grade: {s.Grade}");

        // → Data KHÔNG HỢP LỆ, nhưng code chạy bình thường!
        // → Bug ẩn, khó debug!
    }
}
```

### ✅ AFTER — Encapsulation hoàn chỉnh:

```csharp
using System;

class StudentGood
{
    // ===== 🔒 PRIVATE — ẩn bên trong =====
    private static int _nextId = 0;
    private string _name = "";
    private int _age;
    private double _score;

    // ===== 🌍 PUBLIC PROPERTIES — API cho bên ngoài =====
    public int Id { get; }  // Readonly — gán 1 lần trong constructor

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên không được rỗng!");
            _name = value.Trim();
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 150)
                throw new ArgumentOutOfRangeException(nameof(Age), "Tuổi phải từ 0-150!");
            _age = value;
        }
    }

    public double Score
    {
        get => _score;
        set
        {
            if (value < 0 || value > 10)
                throw new ArgumentOutOfRangeException(nameof(Score), "Điểm phải từ 0-10!");
            _score = value;
        }
    }

    // ===== COMPUTED — tự tính, không gán được =====
    public string Grade => Score switch
    {
        >= 8.5 => "Giỏi",
        >= 7.0 => "Khá",
        >= 5.0 => "Trung bình",
        _ => "Yếu"
    };

    public bool IsPass => Score >= 5.0;

    // ===== CONSTRUCTOR =====
    public StudentGood(string name, int age, double score)
    {
        Id = ++_nextId;
        Name = name;    // → Gọi setter → validate
        Age = age;      // → Gọi setter → validate
        Score = score;  // → Gọi setter → validate
    }

    // ===== PUBLIC METHOD =====
    public void Print()
    {
        Console.WriteLine($"  [{Id:D3}] {Name,-15} Tuổi:{Age,3}  " +
                          $"Điểm:{Score,5:F1}  {Grade,-10} {(IsPass ? "✅" : "❌")}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ ENCAPSULATION — BEFORE vs AFTER ═══\n");

        // ✅ Data hợp lệ
        var s1 = new StudentGood("Nguyễn Minh", 20, 8.5);
        var s2 = new StudentGood("Trần Lan", 22, 6.0);
        var s3 = new StudentGood("Lê Hùng", 19, 4.0);

        s1.Print();
        s2.Print();
        s3.Print();

        // ✅ Sửa qua property → validate tự động
        s1.Score = 9.0;
        Console.WriteLine($"\n  Sau sửa điểm:");
        s1.Print();

        // ❌ Data sai → exception!
        Console.WriteLine("\n  Thử gán data sai:");
        try { var bad = new StudentGood("", 20, 5); }
        catch (Exception ex) { Console.WriteLine($"  ❌ {ex.Message}"); }

        try { s1.Age = -5; }
        catch (Exception ex) { Console.WriteLine($"  ❌ {ex.Message}"); }

        try { s2.Score = 15; }
        catch (Exception ex) { Console.WriteLine($"  ❌ {ex.Message}"); }

        // ❌ Computed property → không thể gán
        // s1.Grade = "Siêu nhân";   // LỖI COMPILE! Không có setter
        // s1.Id = 999;              // LỖI COMPILE! Readonly
    }
}
```

**Output:**
```
═══ ENCAPSULATION — BEFORE vs AFTER ═══

  [001] Nguyễn Minh     Tuổi: 20  Điểm:  8.5  Giỏi       ✅
  [002] Trần Lan        Tuổi: 22  Điểm:  6.0  Trung bình  ✅
  [003] Lê Hùng         Tuổi: 19  Điểm:  4.0  Yếu        ❌

  Sau sửa điểm:
  [001] Nguyễn Minh     Tuổi: 20  Điểm:  9.0  Giỏi       ✅

  Thử gán data sai:
  ❌ Tên không được rỗng!
  ❌ Tuổi phải từ 0-150! (Parameter 'Age')
  ❌ Điểm phải từ 0-10! (Parameter 'Score')
```

---

## Ví dụ 2: Immutable UserProfile Class

```csharp
using System;

class UserProfile
{
    // ===== TẤT CẢ readonly — object KHÔNG THỂ thay đổi =====
    public string Username { get; }
    public string Email { get; }
    public string DisplayName { get; }
    public DateTime CreatedAt { get; }

    // ===== COMPUTED =====
    public string MaskedEmail
    {
        get
        {
            var parts = Email.Split('@');
            if (parts.Length != 2) return "***";
            string name = parts[0];
            string masked = name.Length <= 2
                ? "***"
                : name[..2] + new string('*', name.Length - 2);
            return $"{masked}@{parts[1]}";
        }
    }

    public int AccountAgeDays => (DateTime.Now - CreatedAt).Days;

    // ===== CONSTRUCTOR — chỗ DUY NHẤT gán giá trị =====
    public UserProfile(string username, string email, string displayName)
    {
        // Validate tất cả
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username không được rỗng!");
        if (!email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ!");
        if (username.Length < 3)
            throw new ArgumentException("Username tối thiểu 3 ký tự!");

        Username = username.ToLower().Trim();
        Email = email.ToLower().Trim();
        DisplayName = string.IsNullOrWhiteSpace(displayName) ? username : displayName;
        CreatedAt = DateTime.Now;
    }

    // ===== "THAY ĐỔI" → tạo object MỚI =====
    public UserProfile WithDisplayName(string newName)
    {
        return new UserProfile(Username, Email, newName);
    }

    public void Print()
    {
        Console.WriteLine($"  👤 {DisplayName}");
        Console.WriteLine($"     Username : {Username}");
        Console.WriteLine($"     Email    : {MaskedEmail}");
        Console.WriteLine($"     Created  : {CreatedAt:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"     Age      : {AccountAgeDays} ngày");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ IMMUTABLE USER PROFILE ═══\n");

        var user = new UserProfile("minh_dev", "minh.nguyen@email.com", "Minh Nguyễn");
        user.Print();

        // ❌ KHÔNG THỂ sửa — tất cả readonly!
        // user.Username = "hacker";       // LỖI COMPILE!
        // user.Email = "hack@evil.com";   // LỖI COMPILE!

        // ✅ Muốn "đổi"? → Tạo bản mới
        var updatedUser = user.WithDisplayName("Minh N.");
        Console.WriteLine("\n  --- Sau 'update' ---");
        updatedUser.Print();

        Console.WriteLine("\n  --- User gốc KHÔNG ĐỔI ---");
        user.Print();

        // Validate
        Console.WriteLine("\n  --- Test validate ---");
        try { new UserProfile("ab", "test@mail.com", ""); }
        catch (Exception ex) { Console.WriteLine($"  ❌ {ex.Message}"); }

        try { new UserProfile("test", "invalid-email", "Test"); }
        catch (Exception ex) { Console.WriteLine($"  ❌ {ex.Message}"); }
    }
}
```

**Output:**
```
═══ IMMUTABLE USER PROFILE ═══

  👤 Minh Nguyễn
     Username : minh_dev
     Email    : mi*********@email.com
     Created  : 20/05/2026 09:30
     Age      : 0 ngày

  --- Sau 'update' ---
  👤 Minh N.
     Username : minh_dev
     Email    : mi*********@email.com
     Created  : 20/05/2026 09:30
     Age      : 0 ngày

  --- User gốc KHÔNG ĐỔI ---
  👤 Minh Nguyễn
     ...
```

---

## Ví dụ 3: BankAccount với Full Encapsulation

```csharp
using System;
using System.Collections.Generic;

class BankAccount
{
    // ===== 🔒 PRIVATE — tất cả data + logic ẩn bên trong =====
    private static int _nextId = 10000;
    private string _pin;
    private int _failedAttempts;
    private readonly List<string> _transactions = new();

    // ===== 🌍 PUBLIC PROPERTIES — readonly từ ngoài =====
    public string AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    public bool IsLocked { get; private set; }

    // ===== COMPUTED =====
    public string Status => IsLocked ? "🔒 Đã khóa" : "✅ Hoạt động";
    public int TransactionCount => _transactions.Count;

    // Transaction history — trả về copy readonly
    public IReadOnlyList<string> TransactionHistory => _transactions.AsReadOnly();

    // ===== CONSTRUCTOR =====
    public BankAccount(string owner, string pin, decimal initialDeposit = 0)
    {
        ValidateOwner(owner);
        ValidatePin(pin);
        if (initialDeposit < 0)
            throw new ArgumentException("Số tiền ban đầu không được âm!");

        AccountNumber = $"VCB-{++_nextId}";
        Owner = owner;
        _pin = pin;
        Balance = initialDeposit;

        if (initialDeposit > 0)
            RecordTransaction("OPEN", initialDeposit, "Mở tài khoản");
    }

    // ===== PUBLIC METHODS — API cho bên ngoài =====
    public bool Deposit(decimal amount)
    {
        if (IsLocked) { Console.WriteLine("  ⚠️ Tài khoản đã bị khóa!"); return false; }
        if (amount <= 0) { Console.WriteLine("  ⚠️ Số tiền phải > 0!"); return false; }

        Balance += amount;
        RecordTransaction("DEPOSIT", amount, "Nạp tiền");
        return true;
    }

    public bool Withdraw(decimal amount, string pin)
    {
        if (IsLocked) { Console.WriteLine("  ⚠️ Tài khoản đã bị khóa!"); return false; }
        if (!AuthenticatePin(pin)) return false;
        if (amount <= 0) { Console.WriteLine("  ⚠️ Số tiền phải > 0!"); return false; }
        if (amount > Balance) { Console.WriteLine("  ⚠️ Số dư không đủ!"); return false; }

        Balance -= amount;
        RecordTransaction("WITHDRAW", -amount, "Rút tiền");
        return true;
    }

    public bool Transfer(BankAccount target, decimal amount, string pin)
    {
        if (target == null) return false;
        if (!AuthenticatePin(pin)) return false;
        if (amount <= 0 || amount > Balance) return false;

        Balance -= amount;
        target.Balance += amount;

        RecordTransaction("TRANSFER_OUT", -amount, $"Chuyển → {target.AccountNumber}");
        target.RecordTransaction("TRANSFER_IN", amount, $"Nhận ← {AccountNumber}");
        return true;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"\n  ╔══════════════════════════════════════════╗");
        Console.WriteLine($"  ║  SỔ TÀI KHOẢN                           ║");
        Console.WriteLine($"  ╠══════════════════════════════════════════╣");
        Console.WriteLine($"  ║  Số TK  : {AccountNumber,-30}║");
        Console.WriteLine($"  ║  Chủ TK : {Owner,-30}║");
        Console.WriteLine($"  ║  Số dư  : {Balance,20:N0} VNĐ  ║");
        Console.WriteLine($"  ║  Status : {Status,-30}║");
        Console.WriteLine($"  ╠══════════════════════════════════════════╣");
        Console.WriteLine($"  ║  LỊCH SỬ GIAO DỊCH ({TransactionCount} GD)              ║");
        Console.WriteLine($"  ╠══════════════════════════════════════════╣");
        foreach (var t in _transactions)
            Console.WriteLine($"  ║  {t,-40}║");
        Console.WriteLine($"  ╚══════════════════════════════════════════╝");
    }

    // ===== 🔒 PRIVATE METHODS — logic nội bộ, ẩn hoàn toàn =====
    private bool AuthenticatePin(string inputPin)
    {
        if (inputPin != _pin)
        {
            _failedAttempts++;
            Console.WriteLine($"  ❌ Sai PIN! ({_failedAttempts}/3)");
            if (_failedAttempts >= 3)
            {
                IsLocked = true;
                RecordTransaction("LOCK", 0, "TK bị khóa do sai PIN");
                Console.WriteLine("  🔒 Tài khoản đã bị KHÓA!");
            }
            return false;
        }
        _failedAttempts = 0;
        return true;
    }

    private void RecordTransaction(string type, decimal amount, string desc)
    {
        string entry = $"{DateTime.Now:HH:mm} {type,-12} {amount,12:N0}  {desc}";
        _transactions.Add(entry);
    }

    private static void ValidateOwner(string owner)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Tên chủ TK không được rỗng!");
    }

    private static void ValidatePin(string pin)
    {
        if (pin == null || pin.Length != 4 || !int.TryParse(pin, out _))
            throw new ArgumentException("PIN phải là 4 chữ số!");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ BANK ACCOUNT — FULL ENCAPSULATION ═══");

        // Tạo tài khoản
        var acc1 = new BankAccount("Nguyễn Minh", "1234", 10_000_000m);
        var acc2 = new BankAccount("Trần Lan", "5678", 5_000_000m);

        // Giao dịch
        acc1.Deposit(3_000_000m);
        acc1.Withdraw(2_000_000m, "1234");
        acc1.Transfer(acc2, 1_000_000m, "1234");

        // In sao kê
        acc1.PrintStatement();
        acc2.PrintStatement();

        // ❌ Không thể truy cập trực tiếp!
        // acc1._pin = "0000";                   // LỖI! private
        // acc1.Balance = 999_999_999m;           // LỖI! private set
        // acc1._transactions.Clear();            // LỖI! private
        // acc1.AuthenticatePin("1234");          // LỖI! private method

        // ✅ Chỉ thấy readonly data
        Console.WriteLine($"\n  Số GD acc1: {acc1.TransactionCount}");
        Console.WriteLine($"  History (readonly):");
        foreach (var t in acc1.TransactionHistory)
            Console.WriteLine($"    {t}");
    }
}
```

---

## Ví dụ 4: Shopping Cart với Encapsulated Business Rules

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class CartItem
{
    public string ProductName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; private set; }
    public decimal Subtotal => UnitPrice * Quantity;

    public CartItem(string name, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên sản phẩm không được rỗng!");
        if (unitPrice <= 0)
            throw new ArgumentException("Giá phải > 0!");
        if (quantity <= 0)
            throw new ArgumentException("Số lượng phải > 0!");

        ProductName = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    internal void UpdateQuantity(int qty)
    {
        if (qty <= 0)
            throw new ArgumentException("Số lượng phải > 0!");
        Quantity = qty;
    }
}

class ShoppingCart
{
    // ===== 🔒 PRIVATE — data + business logic ẩn bên trong =====
    private readonly List<CartItem> _items = new();
    private string? _couponCode;
    private decimal _couponDiscount;
    private const decimal TAX_RATE = 0.1m;           // 10% VAT
    private const decimal FREE_SHIPPING = 500_000m;   // Miễn ship > 500k
    private const decimal SHIPPING_FEE = 30_000m;
    private const int MAX_ITEMS = 20;

    // ===== 🌍 PUBLIC PROPERTIES =====
    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public int ItemCount => _items.Count;
    public int TotalQuantity => _items.Sum(i => i.Quantity);
    public bool IsEmpty => _items.Count == 0;

    // ===== COMPUTED — business rules tự tính =====
    public decimal Subtotal => _items.Sum(i => i.Subtotal);
    public decimal Discount => _couponDiscount;
    public decimal Tax => (Subtotal - Discount) * TAX_RATE;
    public decimal ShippingFee => Subtotal >= FREE_SHIPPING ? 0 : SHIPPING_FEE;
    public decimal Total => Subtotal - Discount + Tax + ShippingFee;
    public string ShippingNote => ShippingFee == 0
        ? "🚚 Miễn phí vận chuyển!"
        : $"🚚 Ship: {SHIPPING_FEE:N0} (miễn ship cho đơn từ {FREE_SHIPPING:N0})";

    // ===== PUBLIC METHODS — API cho bên ngoài =====
    public bool AddItem(string name, decimal price, int qty = 1)
    {
        if (_items.Count >= MAX_ITEMS)
        {
            Console.WriteLine($"  ⚠️ Giỏ hàng tối đa {MAX_ITEMS} sản phẩm!");
            return false;
        }

        // Nếu đã có → tăng số lượng
        var existing = FindItem(name);
        if (existing != null)
        {
            existing.UpdateQuantity(existing.Quantity + qty);
            return true;
        }

        _items.Add(new CartItem(name, price, qty));
        return true;
    }

    public bool RemoveItem(string name)
    {
        var item = FindItem(name);
        if (item == null) return false;
        _items.Remove(item);
        ClearCouponIfEmpty();
        return true;
    }

    public bool UpdateQuantity(string name, int newQty)
    {
        var item = FindItem(name);
        if (item == null) return false;
        if (newQty <= 0) return RemoveItem(name);
        item.UpdateQuantity(newQty);
        return true;
    }

    public bool ApplyCoupon(string code)
    {
        if (IsEmpty) { Console.WriteLine("  ⚠️ Giỏ hàng trống!"); return false; }

        // Business rule: mỗi coupon có % giảm khác nhau
        var (valid, discount) = ValidateCoupon(code);
        if (!valid) { Console.WriteLine($"  ❌ Mã '{code}' không hợp lệ!"); return false; }

        _couponCode = code;
        _couponDiscount = Subtotal * discount;
        Console.WriteLine($"  🎫 Áp dụng mã '{code}' — Giảm {discount:P0}!");
        return true;
    }

    public void Clear()
    {
        _items.Clear();
        _couponCode = null;
        _couponDiscount = 0;
    }

    public void PrintReceipt()
    {
        Console.WriteLine("\n  ┌─────────────────────────────────────────────────┐");
        Console.WriteLine("  │              🛒 GIỎ HÀNG CỦA BẠN               │");
        Console.WriteLine("  ├───────────────────┬────────┬─────┬──────────────┤");
        Console.WriteLine("  │ Sản phẩm          │ Đơn giá│  SL │  Thành tiền  │");
        Console.WriteLine("  ├───────────────────┼────────┼─────┼──────────────┤");

        foreach (var item in _items)
        {
            Console.WriteLine($"  │ {item.ProductName,-17} │{item.UnitPrice,7:N0} │ {item.Quantity,3} │ {item.Subtotal,11:N0} │");
        }

        Console.WriteLine("  ├───────────────────┴────────┴─────┼──────────────┤");
        Console.WriteLine($"  │ Tạm tính                         │ {Subtotal,11:N0} │");
        if (_couponDiscount > 0)
            Console.WriteLine($"  │ Giảm giá ({_couponCode})              │-{Discount,11:N0} │");
        Console.WriteLine($"  │ VAT (10%)                        │ {Tax,11:N0} │");
        Console.WriteLine($"  │ {ShippingNote,-35}│ {ShippingFee,11:N0} │");
        Console.WriteLine("  ├────────────────────────────────────┼──────────────┤");
        Console.WriteLine($"  │ 💰 TỔNG CỘNG                      │ {Total,11:N0} │");
        Console.WriteLine("  └────────────────────────────────────┴──────────────┘");
    }

    // ===== 🔒 PRIVATE — business logic ẩn =====
    private CartItem? FindItem(string name)
    {
        return _items.FirstOrDefault(i =>
            i.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    private (bool isValid, decimal discountRate) ValidateCoupon(string code)
    {
        return code.ToUpper() switch
        {
            "SAVE10" => (true, 0.10m),
            "SAVE20" => (true, 0.20m),
            "VIP50" => (true, 0.50m),
            _ => (false, 0)
        };
    }

    private void ClearCouponIfEmpty()
    {
        if (IsEmpty)
        {
            _couponCode = null;
            _couponDiscount = 0;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ SHOPPING CART — ENCAPSULATED BUSINESS RULES ═══");

        var cart = new ShoppingCart();

        // Thêm sản phẩm
        cart.AddItem("iPhone 15", 25_990_000m, 1);
        cart.AddItem("AirPods Pro", 5_990_000m, 1);
        cart.AddItem("Ốp lưng", 250_000m, 2);

        cart.PrintReceipt();

        // Áp mã giảm giá
        cart.ApplyCoupon("SAVE10");
        cart.PrintReceipt();

        // Sửa số lượng
        cart.UpdateQuantity("Ốp lưng", 3);
        cart.AddItem("AirPods Pro", 5_990_000m, 1);  // Thêm 1 → tổng SL = 2

        cart.PrintReceipt();

        // ❌ Không thể can thiệp trực tiếp!
        // cart._items.Clear();              // LỖI! private
        // cart._couponDiscount = 999999;    // LỖI! private
        // cart.Items.Add(new CartItem());   // LỖI! IReadOnlyList

        // ✅ Chỉ đọc data qua properties
        Console.WriteLine($"\n  Items: {cart.ItemCount}, Qty: {cart.TotalQuantity}");
        Console.WriteLine($"  Total: {cart.Total:N0} VNĐ");
    }
}
```

---

## Ví dụ 5: Temperature Sensor — Encapsulation + init + required (C# 11)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class SensorConfig
{
    // required — BẮT BUỘC phải gán khi tạo
    public required string SensorId { get; init; }
    public required string Location { get; init; }

    // Optional — có default
    public double MinTemp { get; init; } = -40;
    public double MaxTemp { get; init; } = 85;
    public int MaxReadings { get; init; } = 100;
}

class TemperatureSensor
{
    // ===== 🔒 PRIVATE =====
    private readonly SensorConfig _config;
    private readonly List<double> _readings = new();
    private bool _isActive;

    // ===== 🌍 PUBLIC — readonly =====
    public string SensorId => _config.SensorId;
    public string Location => _config.Location;
    public bool IsActive => _isActive;
    public int ReadingCount => _readings.Count;

    // COMPUTED
    public double? CurrentTemp => _readings.Count > 0 ? _readings[^1] : null;
    public double? AverageTemp => _readings.Count > 0 ? _readings.Average() : null;
    public double? MinTemp => _readings.Count > 0 ? _readings.Min() : null;
    public double? MaxTemp => _readings.Count > 0 ? _readings.Max() : null;

    public string StatusEmoji => _isActive ? "🟢" : "🔴";

    // ===== CONSTRUCTOR =====
    public TemperatureSensor(SensorConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    // ===== PUBLIC API =====
    public void Activate() => _isActive = true;
    public void Deactivate() => _isActive = false;

    public bool RecordReading(double temperature)
    {
        if (!_isActive) return false;

        // Business rule: validate range
        if (temperature < _config.MinTemp || temperature > _config.MaxTemp)
        {
            Console.WriteLine($"  ⚠️ [{SensorId}] Nhiệt độ {temperature}°C ngoài phạm vi!");
            return false;
        }

        // Business rule: giới hạn số readings
        if (_readings.Count >= _config.MaxReadings)
            _readings.RemoveAt(0);  // Xóa reading cũ nhất

        _readings.Add(temperature);
        return true;
    }

    public void PrintReport()
    {
        Console.WriteLine($"  {StatusEmoji} Sensor [{SensorId}] — {Location}");
        Console.WriteLine($"     Readings : {ReadingCount}");
        Console.WriteLine($"     Current  : {CurrentTemp?.ToString("F1") ?? "N/A"}°C");
        Console.WriteLine($"     Average  : {AverageTemp?.ToString("F1") ?? "N/A"}°C");
        Console.WriteLine($"     Min/Max  : {MinTemp?.ToString("F1") ?? "N/A"} / {MaxTemp?.ToString("F1") ?? "N/A"}°C");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ TEMPERATURE SENSOR — INIT + REQUIRED ═══\n");

        // Tạo config với required + init
        var config = new SensorConfig
        {
            SensorId = "TEMP-001",        // required — bắt buộc
            Location = "Phòng Server",     // required — bắt buộc
            MaxTemp = 50,                  // Optional override
            MaxReadings = 10
        };

        // config.SensorId = "HACK";  // ❌ LỖI! init — không sửa được
        // config.Location = "???";   // ❌ LỖI! init

        var sensor = new TemperatureSensor(config);
        sensor.Activate();

        // Simulate readings
        double[] temps = { 25.5, 26.0, 27.3, 28.1, 26.8, 25.2, 24.9, 27.5 };
        foreach (var t in temps)
            sensor.RecordReading(t);

        // Nhiệt độ ngoài phạm vi
        sensor.RecordReading(60);   // ⚠️ > MaxTemp (50°C)

        sensor.PrintReport();

        // ❌ Không thể can thiệp!
        // sensor._readings.Clear();  // LỖI! private
        // sensor._isActive = false;  // LỖI! private
    }
}
```

**Output:**
```
═══ TEMPERATURE SENSOR — INIT + REQUIRED ═══

  ⚠️ [TEMP-001] Nhiệt độ 60°C ngoài phạm vi!
  🟢 Sensor [TEMP-001] — Phòng Server
     Readings : 8
     Current  : 27.5°C
     Average  : 26.4°C
     Min/Max  : 24.9 / 28.1°C
```
