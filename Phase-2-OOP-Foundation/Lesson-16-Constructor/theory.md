# 📘 Lesson 16 — Constructor (Hàm khởi tạo) trong C#

---

## 1. Vấn đề: Tạo object quá dài dòng

```csharp
// ❌ Lesson 15: phải gán từng field sau new
Student s = new Student();
s.Name = "Minh";
s.Age = 22;
s.Score = 8.5;
// Quên gán 1 field → bug!
```

**Giải pháp:** Constructor — method đặc biệt **chạy tự động khi `new`**.

```csharp
// ✅ Với constructor — gọn, an toàn
Student s = new Student("Minh", 22, 8.5);
```

---

## 2. Constructor là gì?

Constructor = method đặc biệt:
- **Cùng tên với class**
- **Không có return type** (kể cả `void`)
- **Tự động chạy** khi gọi `new`
- Dùng để **khởi tạo fields** cho object

```csharp
class Student
{
    public string Name;
    public int Age;
    public double Score;

    // Constructor
    public Student(string name, int age, double score)
    {
        Name = name;
        Age = age;
        Score = score;
    }
}

// Gọi:
Student s = new Student("Minh", 22, 8.5);
// → Constructor chạy → Name="Minh", Age=22, Score=8.5
```

---

## 3. Default Constructor

```csharp
class Product
{
    public string Name = "Unnamed";
    public decimal Price = 0;
}

// Nếu KHÔNG viết constructor → C# tự tạo "default constructor"
Product p = new Product();  // OK — default constructor (không tham số)
// Name = "Unnamed", Price = 0
```

> ⚠️ **Khi bạn viết BẤT KỲ constructor nào → default constructor BIẾN MẤT!**

```csharp
class Product
{
    public string Name;
    public decimal Price;

    // Có constructor tham số
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

// Product p = new Product();  // ❌ LỖI! Default constructor không còn!
Product p = new Product("iPhone", 25990000m);  // ✅ OK
```

---

## 4. Constructor Overloading

```csharp
class BankAccount
{
    public string Id;
    public string Owner;
    public decimal Balance;

    // Constructor 1: Đầy đủ
    public BankAccount(string id, string owner, decimal balance)
    {
        Id = id;
        Owner = owner;
        Balance = balance;
    }

    // Constructor 2: Không có balance (mặc định = 0)
    public BankAccount(string id, string owner)
    {
        Id = id;
        Owner = owner;
        Balance = 0;
    }

    // Constructor 3: Không tham số
    public BankAccount()
    {
        Id = "NEW";
        Owner = "Unknown";
        Balance = 0;
    }
}

// Gọi tùy constructor:
var acc1 = new BankAccount("ACC001", "Minh", 10000000m);
var acc2 = new BankAccount("ACC002", "Hùng");           // Balance = 0
var acc3 = new BankAccount();                             // Default values
```

---

## 5. Constructor Chaining (`this()`)

```csharp
class Employee
{
    public string Name;
    public string Department;
    public decimal Salary;

    // Constructor chính — đầy đủ
    public Employee(string name, string department, decimal salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    }

    // Gọi constructor khác bằng : this(...)
    public Employee(string name, string department)
        : this(name, department, 5000000m)   // Salary mặc định 5tr
    {
        // Code thêm nếu cần
    }

    public Employee(string name)
        : this(name, "Chưa phân", 5000000m)
    {
    }

    public Employee()
        : this("Chưa có tên")
    {
    }
}
```

```
new Employee()
    → this("Chưa có tên")
        → this("Chưa có tên", "Chưa phân", 5000000)
            → Constructor chính chạy
```

> 🔑 **Constructor chaining** tránh lặp code — mọi constructor đều đổ về 1 constructor chính.

---

## 6. Validation trong Constructor

```csharp
class Product
{
    public string Name;
    public decimal Price;
    public int Stock;

    public Product(string name, decimal price, int stock = 0)
    {
        // Validate ngay khi tạo — không cho tạo object sai
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được rỗng!", nameof(name));
        if (price < 0)
            throw new ArgumentException("Giá không được âm!", nameof(price));
        if (stock < 0)
            throw new ArgumentException("Tồn kho không được âm!", nameof(stock));

        Name = name;
        Price = price;
        Stock = stock;
    }
}

// Sử dụng:
var p1 = new Product("iPhone", 25990000m, 10);   // ✅ OK
// var p2 = new Product("", 100);                 // ❌ ArgumentException!
// var p3 = new Product("Test", -100);             // ❌ ArgumentException!
```

> 🔑 **Validate trong constructor** = đảm bảo object LUÔN ở trạng thái hợp lệ ngay từ đầu.

---

## 7. Object Initializer — Cú pháp nhanh

```csharp
class Config
{
    public string AppName = "";
    public string Version = "";
    public int MaxUsers = 100;

    // Cần default constructor (không tham số)
}

// Object Initializer — gán fields ngay khi new
var config = new Config
{
    AppName = "MyApp",
    Version = "2.0",
    MaxUsers = 50
};

// Tương đương:
// var config = new Config();
// config.AppName = "MyApp";
// config.Version = "2.0";
// config.MaxUsers = 50;
```

> 💡 Object Initializer thuận tiện cho class có nhiều field optional.

---

## 8. So sánh với JavaScript

```javascript
// JS — constructor trong class
class Student {
    constructor(name, score) {
        this.name = name;
        this.score = score;
    }
}
let s = new Student("Minh", 8.5);
```

```csharp
// C# — constructor
class Student
{
    public string Name;
    public double Score;

    public Student(string name, double score)
    {
        Name = name;
        Score = score;
    }
}
Student s = new Student("Minh", 8.5);
```

| | C# | JavaScript |
|--|-----|-----------|
| Tên constructor | Cùng tên class | `constructor` |
| Overloading | ✅ Nhiều constructor | ❌ Chỉ 1 (dùng default param) |
| Chaining | `this(...)` | Không có |
| Validation | throw trong constructor | throw trong constructor |
| Object Initializer | `new X { A = 1 }` | `{ a: 1 }` (object literal) |

---

## 9. Sai lầm phổ biến

### ❌ Quên default constructor biến mất:
```csharp
class Foo
{
    public Foo(int x) { }  // Viết constructor có tham số
}
// var f = new Foo();       // ❌ LỖI! Default constructor không còn!
// Fix: thêm public Foo() { }
```

### ❌ Không validate trong constructor:
```csharp
// ❌ Cho phép tạo object sai
var p = new Product("", -100, -5);  // Tên rỗng, giá âm, stock âm!

// ✅ Throw exception nếu sai
```

### ❌ Constructor quá phức tạp:
```csharp
// ❌ Constructor làm quá nhiều việc (I/O, network, tính toán nặng)
public Student(string name)
{
    Name = name;
    Console.WriteLine("Loading...");       // ❌
    // File.ReadAllText("data.txt");       // ❌
    // HttpClient.GetAsync("...");         // ❌
}
// ✅ Constructor chỉ khởi tạo fields. Logic phức tạp → method riêng.
```

---

## 10. Best Practices

1. **Constructor = khởi tạo fields** — không làm logic phức tạp
2. **Validate đầu vào** — throw exception nếu invalid
3. **Constructor chaining** — `this(...)` để tránh lặp code
4. **Default parameter** thay vì nhiều overload
5. **Object Initializer** cho class có nhiều optional fields
6. **Thêm default constructor** nếu cần cả constructor có tham số
