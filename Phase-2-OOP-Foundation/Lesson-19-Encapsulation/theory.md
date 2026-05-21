# 📘 Lesson 19 — Encapsulation (Tính đóng gói) trong C#

---

## 1. Encapsulation là gì?

**Encapsulation** (Tính đóng gói) = **Đóng gói data + behavior vào class**, ẩn implementation details, chỉ expose những gì cần thiết.

> 🔑 Encapsulation là **trụ cột đầu tiên** của OOP (4 trụ cột: Encapsulation, Inheritance, Polymorphism, Abstraction).

```
╔══════════════════════════════════════════════════════════════╗
║                    ENCAPSULATION                             ║
║                                                              ║
║   ┌─────────────────────────────────────────────┐            ║
║   │              Class BankAccount              │            ║
║   │                                             │            ║
║   │  🔒 PRIVATE (ẩn bên trong)                  │            ║
║   │  ├── decimal balance                        │            ║
║   │  ├── string pin                             │            ║
║   │  ├── List<Transaction> history              │            ║
║   │  ├── bool ValidatePin()                     │            ║
║   │  └── void LogTransaction()                  │            ║
║   │                                             │            ║
║   │  🌍 PUBLIC (API cho bên ngoài)              │            ║
║   │  ├── decimal Balance { get; }               │            ║
║   │  ├── bool Deposit(amount)                   │            ║
║   │  ├── bool Withdraw(amount, pin)             │            ║
║   │  └── string GetStatement()                  │            ║
║   │                                             │            ║
║   └─────────────────────────────────────────────┘            ║
║                                                              ║
║   Bên ngoài CHỈ thấy PUBLIC API                             ║
║   Implementation bên trong có thể thay đổi thoải mái        ║
╚══════════════════════════════════════════════════════════════╝
```

### Encapsulation gồm 2 ý chính:

| Ý | Mô tả | Ví dụ |
|---|-------|-------|
| **Bundling** | Gom data + methods vào 1 class | `BankAccount` chứa `balance` + `Deposit()` |
| **Information Hiding** | Ẩn chi tiết, expose API | `balance` private, `Balance` property public |

---

## 2. Ba cấp độ Encapsulation

### 📦 Level 1: Field Encapsulation (private field + property)

```csharp
class Employee
{
    // ===== ẨN: private fields =====
    private decimal _salary;

    // ===== LỘ: public property với validation =====
    public decimal Salary
    {
        get => _salary;
        set => _salary = value >= 0 ? value : throw new ArgumentException("Lương không âm!");
    }
}
```

> Bạn đã học cách này ở **Lesson 18**! Đây là level cơ bản nhất.

### 🔧 Level 2: Method Encapsulation (private helper methods)

```csharp
class OrderProcessor
{
    // PUBLIC — API cho bên ngoài
    public bool ProcessOrder(Order order)
    {
        if (!ValidateOrder(order)) return false;    // Private helper
        decimal total = CalculateTotal(order);       // Private helper
        ApplyDiscount(order);                        // Private helper
        return ChargePayment(total);                 // Private helper
    }

    // PRIVATE — logic nội bộ, bên ngoài không cần biết
    private bool ValidateOrder(Order order) { /* ... */ }
    private decimal CalculateTotal(Order order) { /* ... */ }
    private void ApplyDiscount(Order order) { /* ... */ }
    private bool ChargePayment(decimal amount) { /* ... */ }
}
```

```
Bên ngoài thấy:
    processor.ProcessOrder(order)  ← 1 method đơn giản

Bên trong ẩn:
    ValidateOrder → CalculateTotal → ApplyDiscount → ChargePayment
    (4 bước phức tạp, nhưng caller không cần biết!)
```

### 🏢 Level 3: Class Encapsulation (internal class)

```csharp
// Chỉ dùng TRONG project này
internal class DatabaseHelper
{
    internal string ConnectionString { get; set; }
    internal void ExecuteQuery(string sql) { /* ... */ }
}

// Bên ngoài project → dùng public class
public class UserService
{
    private readonly DatabaseHelper _db = new();  // Ẩn implementation

    public User GetUser(int id) { /* dùng _db bên trong */ }
}
```

> `internal` = chỉ thấy trong cùng project/assembly. Hữu ích cho thư viện (library).

---

## 3. Property Patterns nâng cao

### 3.1 Validation Property (ôn lại + nâng cao)

```csharp
class Product
{
    private string _name = "";
    private decimal _price;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên sản phẩm không được rỗng!");
            if (value.Length > 100)
                throw new ArgumentException("Tên tối đa 100 ký tự!");
            _name = value.Trim();
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Giá không được âm!");
            if (value > 999_999_999m)
                throw new ArgumentException("Giá quá lớn!");
            _price = value;
        }
    }
}
```

### 3.2 Computed Property (ôn lại)

```csharp
class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }

    // Computed — tự tính, không lưu trữ
    public double Area => Width * Height;
    public double Perimeter => 2 * (Width + Height);
    public bool IsSquare => Width == Height;
}
```

### 3.3 init accessor (C# 9+) — Gán lúc khởi tạo, sau đó readonly

```csharp
class UserProfile
{
    // init = chỉ gán được trong constructor hoặc object initializer
    public string Username { get; init; }
    public string Email { get; init; }
    public DateTime CreatedAt { get; init; }

    // Vẫn có thể thay đổi
    public string DisplayName { get; set; }
}

// Sử dụng:
var user = new UserProfile
{
    Username = "minh_dev",          // ✅ init — gán lúc tạo
    Email = "minh@email.com",       // ✅ init
    CreatedAt = DateTime.Now,       // ✅ init
    DisplayName = "Minh"            // ✅ set
};

user.DisplayName = "Minh Nguyễn";   // ✅ set — thay đổi được
// user.Username = "hacker";        // ❌ LỖI! init — không sửa được sau khi tạo
// user.Email = "hack@evil.com";    // ❌ LỖI!
```

```
init vs private set vs readonly:
┌──────────────┬─────────────────┬──────────────────┬──────────────┐
│              │ Constructor     │ Object Init      │ Sau khi tạo  │
├──────────────┼─────────────────┼──────────────────┼──────────────┤
│ { get; set; }│ ✅ Gán được     │ ✅ Gán được      │ ✅ Sửa được  │
│ private set  │ ✅ Gán được     │ ❌ Không         │ ❌ Từ ngoài  │
│ init         │ ✅ Gán được     │ ✅ Gán được      │ ❌ Không     │
│ readonly     │ ✅ Gán được     │ ❌ Không         │ ❌ Không     │
└──────────────┴─────────────────┴──────────────────┴──────────────┘
```

### 3.4 required (C# 11+) — Bắt buộc phải gán khi khởi tạo

```csharp
class Customer
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string Phone { get; set; } = "";  // Optional
}

// Sử dụng:
var c1 = new Customer { Name = "Minh", Email = "minh@mail.com" };  // ✅
// var c2 = new Customer { Name = "Lan" };  // ❌ LỖI! Email là required
// var c3 = new Customer();                 // ❌ LỖI! Thiếu Name và Email
```

---

## 4. Immutable Objects Pattern

**Immutable** = object KHÔNG THỂ thay đổi sau khi tạo. Mọi field là readonly.

```csharp
class Money
{
    public decimal Amount { get; }          // Không có set → readonly
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Số tiền không âm!");
        Amount = amount;
        Currency = currency ?? "VND";
    }

    // Muốn "thay đổi"? → Tạo object MỚI
    public Money Add(decimal value) => new Money(Amount + value, Currency);
    public Money Subtract(decimal value) => new Money(Amount - value, Currency);

    public override string ToString() => $"{Amount:N0} {Currency}";
}

// Sử dụng:
var price = new Money(500000m, "VND");
var newPrice = price.Add(100000m);  // Tạo object MỚI, price không đổi

Console.WriteLine(price);     // 500,000 VND  (không đổi!)
Console.WriteLine(newPrice);  // 600,000 VND  (object mới)
```

```
MEMORY — Immutable:

Stack                    Heap
┌──────────┐            ┌──────────────────┐
│ price ───────────────→│ Amount: 500000   │  ← Không bao giờ đổi
│          │            │ Currency: "VND"  │
├──────────┤            └──────────────────┘
│ newPrice ────────────→┌──────────────────┐
│          │            │ Amount: 600000   │  ← Object MỚI
└──────────┘            │ Currency: "VND"  │
                        └──────────────────┘
```

### Tại sao dùng Immutable?

| Lợi ích | Giải thích |
|---------|-----------|
| **Thread-safe** | Nhiều thread đọc cùng lúc, không ai sửa → an toàn |
| **Predictable** | Giá trị không bị thay đổi bất ngờ |
| **Dễ debug** | Biết chắc giá trị không đổi sau khi tạo |
| **Hash key** | Có thể dùng làm key trong Dictionary (vì hash không đổi) |

---

## 5. Information Hiding Principle

**Information Hiding** = ẩn "HOW" (cách làm), chỉ expose "WHAT" (làm gì).

```csharp
class PasswordManager
{
    // BÊN NGOÀI chỉ biết:
    public bool VerifyPassword(string input, string hashedPassword) { /* ... */ }
    public string HashPassword(string password) { /* ... */ }

    // BÊN TRONG ẩn hoàn toàn:
    private string GenerateSalt() { /* ... */ }
    private string ApplyBcrypt(string input, string salt) { /* ... */ }
    private int _costFactor = 12;
    private string _algorithm = "bcrypt";
}
```

```
Bên ngoài:
    "Tôi muốn hash password"  →  HashPassword("abc123")  →  "$2b$12$..."
    Caller KHÔNG CẦN BIẾT: salt, bcrypt, costFactor, algorithm

Lợi ích:
    → Ngày mai đổi từ bcrypt sang argon2?
    → CHỈ SỬA BÊN TRONG, bên ngoài KHÔNG ẢNH HƯỞNG!
```

---

## 6. Encapsulation vs "chỉ dùng private" — TẠI SAO?

Nhiều người nghĩ: *"Encapsulation = private field, xong!"*. **SAI!** Encapsulation sâu hơn nhiều.

### ❌ Chỉ dùng private — chưa đủ:

```csharp
class BadExample
{
    private int _x;

    public int GetX() => _x;
    public void SetX(int value) => _x = value;  // 🤔 Thêm get/set nhưng KHÔNG validate
    // → Tương đương public field, vô nghĩa!
}
```

### ✅ Encapsulation thật sự:

```csharp
class GoodExample
{
    private int _x;

    public int X
    {
        get => _x;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException();
            _x = value;
            OnValueChanged();  // Trigger side effect
        }
    }

    private void OnValueChanged() { /* logging, notification, etc */ }
}
```

### Bảng so sánh:

| | Chỉ private | Encapsulation thật sự |
|--|-------------|----------------------|
| Ẩn field | ✅ | ✅ |
| Validate input | ❌ | ✅ |
| Business rules | ❌ | ✅ |
| Thay đổi implementation | ❌ Khó | ✅ Dễ dàng |
| Maintainability | ❌ Thấp | ✅ Cao |

### Lợi ích thật sự:

1. **Maintainability** — Sửa bên trong KHÔNG ảnh hưởng bên ngoài
2. **Flexibility** — Đổi cách lưu trữ, đổi logic, thêm logging... thoải mái
3. **Validation** — Đảm bảo data luôn hợp lệ
4. **Consistency** — Business rules nằm 1 chỗ, không rải rác

```csharp
// Ví dụ: Đổi cách tính thuế → CHỈ sửa 1 chỗ
class Invoice
{
    private decimal _subtotal;
    private decimal _taxRate = 0.1m;

    public decimal Tax => _subtotal * _taxRate;        // Trước: 10%
    // public decimal Tax => CalculateProgressiveTax();  // Sau: thuế lũy tiến
    // → Bên ngoài dùng .Tax vẫn KHÔNG ĐỔI!
}
```

---

## 7. Ví dụ thực tế — TV Remote

```
╔═══════════════════════════════════════════════════════╗
║                 ENCAPSULATION = TV                    ║
║                                                       ║
║   🎮 REMOTE (Public API)      📺 CIRCUITS (Private)  ║
║   ┌──────────────────┐       ┌──────────────────┐    ║
║   │ [Power]  [Mute]  │       │ Transistors      │    ║
║   │ [Vol +]  [Vol -] │       │ Capacitors       │    ║
║   │ [Ch ▲]   [Ch ▼]  │       │ Signal Decoder   │    ║
║   │ [1][2][3]        │       │ Audio Amplifier   │    ║
║   └──────────────────┘       │ Video Processor   │    ║
║                              └──────────────────┘    ║
║                                                       ║
║   Bạn bấm nút → TV hoạt động                        ║
║   Bạn KHÔNG CẦN BIẾT mạch điện bên trong!           ║
║                                                       ║
║   Samsung đổi chip mới → Bạn vẫn bấm remote như cũ  ║
╚═══════════════════════════════════════════════════════╝
```

```csharp
class Television
{
    // 🔒 PRIVATE — "mạch điện" ẩn bên trong
    private bool _isOn;
    private int _volume = 20;
    private int _channel = 1;
    private string _signalType = "Digital";

    // 🌍 PUBLIC — "nút remote" cho người dùng
    public bool IsOn => _isOn;
    public int Volume => _volume;
    public int Channel => _channel;

    public void TogglePower() => _isOn = !_isOn;

    public void VolumeUp()
    {
        if (!_isOn) return;
        if (_volume < 100) _volume++;
    }

    public void VolumeDown()
    {
        if (!_isOn) return;
        if (_volume > 0) _volume--;
    }

    public void ChangeChannel(int ch)
    {
        if (!_isOn) return;
        if (ch >= 1 && ch <= 999) _channel = ch;
    }

    // Ngày mai đổi từ Digital sang IPTV?
    // → Sửa private _signalType, thêm logic bên trong
    // → Public API KHÔNG ĐỔI! Người dùng vẫn bấm nút như cũ.
}
```

---

## 8. So sánh C# vs JavaScript

```javascript
// JavaScript — Encapsulation
class BankAccount {
    // Private fields (ES2022)
    #balance = 0;
    #transactionLog = [];

    constructor(initialBalance) {
        if (initialBalance > 0) this.#balance = initialBalance;
    }

    // Public getter
    get balance() { return this.#balance; }

    // Public methods
    deposit(amount) {
        if (amount <= 0) throw new Error("Amount must be positive");
        this.#balance += amount;
        this.#log("deposit", amount);
    }

    // Private method
    #log(type, amount) {
        this.#transactionLog.push({ type, amount, date: new Date() });
    }
}
```

```csharp
// C# — Encapsulation
class BankAccount
{
    // Private fields
    private decimal _balance;
    private readonly List<string> _transactionLog = new();

    public BankAccount(decimal initialBalance)
    {
        if (initialBalance > 0) _balance = initialBalance;
    }

    // Public property (readonly)
    public decimal Balance => _balance;

    // Public methods
    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive");
        _balance += amount;
        Log("deposit", amount);
    }

    // Private method
    private void Log(string type, decimal amount)
    {
        _transactionLog.Add($"{DateTime.Now}: {type} {amount:N0}");
    }
}
```

| | C# | JavaScript |
|--|----|-----------| 
| Private field | `private` keyword | `#` prefix |
| Private method | `private` keyword | `#` prefix |
| Property | `get; set;` (built-in) | `get/set` methods |
| init-only | `{ get; init; }` | ❌ Không có |
| required | `required` modifier | ❌ Không có |
| Immutable | `readonly` + no setter | `Object.freeze()` |
| Access levels | 5 levels (public → private) | 2 levels (public / #private) |
| Default access | `private` | `public` |
| Enforced at | **Compile time** ✅ | **Runtime** ❌ |

> 🔑 C# enforce encapsulation tại **compile time** — lỗi phát hiện TRƯỚC khi chạy. JS kiểm tra lúc runtime.

---

## 9. Sai lầm phổ biến & Best Practices

### ❌ Sai lầm 1: Public field (không encapsulation)

```csharp
class User
{
    public string Name;        // ❌ Public field → ai cũng sửa
    public int Age;            // ❌ Cho phép Age = -100!
}
```

### ❌ Sai lầm 2: Property nhưng không validate

```csharp
class User
{
    public string Name { get; set; }  // 🤔 Auto property nhưng không validate
    public int Age { get; set; }      // 🤔 Cho phép Age = -100!
    // → Chỉ "bọc" private field, nhưng KHÔNG bảo vệ gì
}
```

### ❌ Sai lầm 3: Expose internal collection

```csharp
class Order
{
    public List<string> Items { get; set; } = new();  // ❌ Bên ngoài sửa list!
}

var order = new Order();
order.Items.Clear();  // 💥 Xóa hết items!
order.Items = null;   // 💥 Gán null!
```

### ✅ Sửa đúng:

```csharp
class Order
{
    private readonly List<string> _items = new();

    // Trả về bản copy readonly
    public IReadOnlyList<string> Items => _items.AsReadOnly();
    public int ItemCount => _items.Count;

    // Kiểm soát qua method
    public void AddItem(string item)
    {
        if (string.IsNullOrWhiteSpace(item)) return;
        _items.Add(item);
    }

    public bool RemoveItem(string item) => _items.Remove(item);
}
```

### ✅ Best Practices:

| # | Quy tắc | Ví dụ |
|---|---------|-------|
| 1 | Field luôn `private` | `private decimal _balance;` |
| 2 | Validate trong property setter | `set { if (value < 0) throw ...; }` |
| 3 | Collection → expose `IReadOnly` | `public IReadOnlyList<T> Items => ...` |
| 4 | Private helper methods | Chia logic phức tạp thành private methods |
| 5 | Immutable khi có thể | `{ get; }` hoặc `{ get; init; }` |
| 6 | required cho mandatory props | `public required string Name { get; set; }` |
| 7 | API tối giản | Chỉ expose những gì CẦN THIẾT |
| 8 | Naming convention | `_camelCase` private, `PascalCase` public |

---

## 10. Tổng kết

```
┌──────────────────────────────────────────────────────────────┐
│                    ENCAPSULATION                             │
│                                                              │
│   1. BUNDLING    — Gom data + behavior vào class             │
│   2. HIDING      — Ẩn implementation, expose API             │
│   3. PROTECTING  — Validate, business rules                  │
│   4. FLEXIBILITY — Đổi bên trong không ảnh hưởng bên ngoài  │
│                                                              │
│   Tools:                                                     │
│   ├── private fields                                         │
│   ├── public/private set properties                          │
│   ├── init / required                                        │
│   ├── private methods                                        │
│   ├── internal classes                                       │
│   └── IReadOnlyCollection                                    │
│                                                              │
│   Lesson 18 → CÁI GÌ (tools)                                │
│   Lesson 19 → TẠI SAO + CÁCH DÙNG (design)                  │
└──────────────────────────────────────────────────────────────┘
```
