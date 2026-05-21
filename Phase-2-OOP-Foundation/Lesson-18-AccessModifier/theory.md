# 📘 Lesson 18 — Access Modifier (Bộ điều khiển truy cập) trong C#

---

## 1. Vấn đề: `public` field quá nguy hiểm

```csharp
class BankAccount
{
    public decimal Balance = 0;  // ❌ public → ai cũng sửa được!
}

var acc = new BankAccount();
acc.Balance = -999999999;  // 💥 Số dư ÂM! Không ai kiểm tra!
```

> 🔑 **Access Modifier** = kiểm soát AI ĐƯỢC QUYỀN truy cập field/method.

---

## 2. Các Access Modifier

| Modifier | Ai thấy? | Ký hiệu |
|----------|---------|---------|
| `public` | TẤT CẢ | 🌍 |
| `private` | CHỈ trong class | 🔒 |
| `protected` | Class + class con | 🛡️ (học ở Inheritance) |
| `internal` | Cùng project/assembly | 📦 |
| `protected internal` | Cùng project HOẶC class con | 📦🛡️ |

```
┌────────────────── Class BankAccount ──────────────────┐
│                                                        │
│  private decimal balance;     🔒 Chỉ class này thấy   │
│  private string pin;          🔒                       │
│                                                        │
│  public decimal GetBalance()  🌍 Ai cũng gọi được     │
│  public bool Deposit(...)     🌍                       │
│  private bool ValidatePin()   🔒 Chỉ dùng nội bộ     │
│                                                        │
└────────────────────────────────────────────────────────┘
       ↑                                    ↑
    Bên ngoài chỉ thấy public        private = ẩn hoàn toàn
```

---

## 3. `private` — Bảo vệ data

```csharp
class BankAccount
{
    // ===== PRIVATE — ẩn khỏi bên ngoài =====
    private string accountId;
    private decimal balance;
    private string pin;

    public BankAccount(string id, string pin, decimal initialBalance)
    {
        this.accountId = id;
        this.pin = pin;
        this.balance = initialBalance > 0 ? initialBalance : 0;
    }

    // ===== PUBLIC — giao diện cho bên ngoài =====
    public string GetAccountId() => accountId;
    public decimal GetBalance() => balance;

    public bool Deposit(decimal amount)
    {
        if (amount <= 0) return false;
        balance += amount;
        return true;
    }

    public bool Withdraw(decimal amount, string inputPin)
    {
        if (!ValidatePin(inputPin)) return false;   // Kiểm tra PIN
        if (amount <= 0 || amount > balance) return false;
        balance -= amount;
        return true;
    }

    // ===== PRIVATE method — logic nội bộ =====
    private bool ValidatePin(string inputPin) => pin == inputPin;
}

// Sử dụng:
var acc = new BankAccount("ACC001", "1234", 5000000m);

acc.Deposit(1000000m);                         // ✅ Public method
acc.Withdraw(500000m, "1234");                // ✅ Public method

Console.WriteLine(acc.GetBalance());           // ✅ 5500000
// Console.WriteLine(acc.balance);             // ❌ LỖI! private
// Console.WriteLine(acc.pin);                 // ❌ LỖI! private
// acc.balance = -999;                         // ❌ LỖI! Không sửa trực tiếp
```

---

## 4. Property — Thay thế Get/Set methods

### Cú pháp Property:

```csharp
class Student
{
    // ===== PROPERTY — cách viết hiện đại =====
    public string Name { get; set; }      // Auto property
    public int Age { get; set; }
    public double Score { get; set; }

    // Property với validation (full property)
    private decimal _salary;
    public decimal Salary
    {
        get { return _salary; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Lương không được âm!");
            _salary = value;
        }
    }

    // Readonly property (chỉ đọc)
    public string Grade => Score >= 8 ? "Giỏi" : Score >= 5 ? "TB" : "Yếu";

    // Property với private set (đọc ngoài, sửa trong)
    public string Id { get; private set; }
}
```

### Auto Property vs Full Property:

```csharp
// Auto Property — gọn, không cần validate
public string Name { get; set; }

// Tương đương (compiler tự tạo):
private string _name;
public string Name
{
    get { return _name; }
    set { _name = value; }
}
```

```csharp
// Full Property — có logic/validate
private int _age;
public int Age
{
    get => _age;
    set
    {
        if (value < 0 || value > 150)
            throw new ArgumentOutOfRangeException();
        _age = value;
    }
}
```

### Truy cập Property giống field:

```csharp
Student s = new Student();
s.Name = "Minh";            // Gọi setter
Console.WriteLine(s.Name);  // Gọi getter
s.Score = 8.5;
Console.WriteLine(s.Grade); // Readonly — chỉ getter
// s.Id = "XXX";            // ❌ LỖI! private set
```

> 🔑 **Property = field + kiểm soát**. Bên ngoài dùng như field, bên trong có logic.

---

## 5. Convention — Naming

```csharp
class Product
{
    // Private field: _camelCase hoặc camelCase
    private string _name;
    private decimal _price;

    // Public property: PascalCase
    public string Name
    {
        get => _name;
        set => _name = value ?? "";
    }

    public decimal Price
    {
        get => _price;
        set => _price = value >= 0 ? value : 0;
    }

    // Auto property: PascalCase
    public int Stock { get; set; }
    public string Category { get; set; } = "Chung";

    // Readonly computed: PascalCase
    public decimal Value => Price * Stock;
    public bool IsInStock => Stock > 0;
}
```

| Loại | Naming | Ví dụ |
|------|--------|-------|
| Private field | `_camelCase` | `_balance`, `_name` |
| Public property | `PascalCase` | `Balance`, `Name` |
| Method | `PascalCase` | `GetTotal()`, `IsValid()` |
| Parameter | `camelCase` | `amount`, `studentName` |

---

## 6. So sánh với JavaScript

```javascript
// JS — # cho private (ES2022)
class BankAccount {
    #balance = 0;        // Private field
    #pin;

    constructor(pin, balance) {
        this.#pin = pin;
        this.#balance = balance;
    }

    get balance() { return this.#balance; }  // Getter
    // set balance(v) { ... }                 // Setter

    deposit(amount) {
        if (amount > 0) this.#balance += amount;
    }
}
```

```csharp
// C# — private + property
class BankAccount
{
    private decimal balance;
    private string pin;

    public BankAccount(string pin, decimal balance)
    {
        this.pin = pin;
        this.balance = balance;
    }

    public decimal Balance => balance;  // Readonly property

    public void Deposit(decimal amount)
    {
        if (amount > 0) balance += amount;
    }
}
```

| | C# | JavaScript |
|--|-----|-----------|
| Private | `private` keyword | `#` prefix |
| Default | `private` (mặc định) | `public` (mặc định) |
| Property | `get; set;` (built-in) | `get/set` methods |
| Validation | Trong setter | Trong setter |
| Protected | `protected` | Không có |
| Internal | `internal` | Không có |

---

## 7. Mặc định là `private`

```csharp
class Example
{
    string name;          // Mặc định = private!
    void DoSomething() { }  // Mặc định = private!

    // Tường minh:
    private string name;
    private void DoSomething() { }
}
```

> 🔑 **Quy tắc vàng**: Field luôn `private`, expose qua Property hoặc Method `public`.

---

## 8. Sai lầm phổ biến

### ❌ Field public:
```csharp
public decimal Balance;          // ❌ Ai cũng sửa!
public decimal Balance { get; private set; } // ✅ Đọc được, không sửa ngoài
```

### ❌ Quên `value` trong setter:
```csharp
set { _age = value; }   // ✅ value = giá trị đang được gán
set { _age = _age; }    // ❌ Gán cho chính nó!
```

### ❌ Không validate trong setter:
```csharp
public int Age { get; set; }     // ❌ Cho phép Age = -100!
// ✅ Full property với validate
```

---

## 9. Best Practices

1. **Field luôn `private`** — expose qua Property
2. **Auto Property** khi không cần validate: `public string Name { get; set; }`
3. **Full Property** khi cần validate: `set { if (...) _field = value; }`
4. **`private set`** cho property chỉ đọc từ ngoài: `public string Id { get; private set; }`
5. **Computed property** cho giá trị tính toán: `public decimal Total => Price * Qty;`
6. **`_camelCase`** cho private field, **`PascalCase`** cho property
7. **Constructor** gán giá trị ban đầu, Property kiểm soát thay đổi sau
