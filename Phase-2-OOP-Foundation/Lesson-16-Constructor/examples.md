# 💻 Lesson 16 — Ví dụ: Constructor

---

## Ví dụ 1: Constructor cơ bản + Overloading

```csharp
using System;

class Product
{
    public string Name;
    public decimal Price;
    public int Stock;
    public string Category;

    // Constructor đầy đủ
    public Product(string name, decimal price, int stock, string category)
    {
        Name = name; Price = price; Stock = stock; Category = category;
    }

    // Overload: không có category
    public Product(string name, decimal price, int stock)
        : this(name, price, stock, "Chung") { }

    // Overload: chỉ tên + giá
    public Product(string name, decimal price)
        : this(name, price, 0, "Chung") { }

    public void Print() =>
        Console.WriteLine($"  {Name,-20} {Price,14:N0} VNĐ  Tồn:{Stock,4}  [{Category}]");
}

class Program
{
    static void Main()
    {
        var p1 = new Product("iPhone 15", 25990000m, 10, "Điện thoại");
        var p2 = new Product("AirPods Pro", 5990000m, 25);
        var p3 = new Product("USB-C Cable", 150000m);

        Console.WriteLine("═══ SẢN PHẨM ═══");
        p1.Print(); p2.Print(); p3.Print();
    }
}
```

---

## Ví dụ 2: Constructor Validation

```csharp
using System;

class Student
{
    public string Name;
    public int Age;
    public double Score;

    public Student(string name, int age, double score)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được rỗng!", nameof(name));
        if (age < 16 || age > 60)
            throw new ArgumentOutOfRangeException(nameof(age), "Tuổi phải từ 16-60!");
        if (score < 0 || score > 10)
            throw new ArgumentOutOfRangeException(nameof(score), "Điểm phải từ 0-10!");

        Name = name; Age = age; Score = score;
    }

    public string GetGrade() => Score >= 8 ? "Giỏi" : Score >= 5 ? "TB" : "Yếu";
    public void Print() => Console.WriteLine($"  {Name,-15} {Age,3}t  {Score:F1}  [{GetGrade()}]");
}

class Program
{
    static void Main()
    {
        // ✅ OK
        try { new Student("Minh", 22, 8.5).Print(); } catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
        try { new Student("Hùng", 25, 6.0).Print(); } catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }

        // ❌ Sai — constructor sẽ throw
        try { new Student("", 20, 5); } catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
        try { new Student("Test", 10, 5); } catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
        try { new Student("Test", 20, 15); } catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
    }
}
```

---

## Ví dụ 3: Static field + Constructor — Auto ID

```csharp
using System;

class Order
{
    private static int lastId = 0;  // Static: đếm chung tất cả orders

    public string Id;               // Instance: riêng mỗi order
    public string Customer;
    public decimal Total;
    public DateTime CreatedAt;

    public Order(string customer, decimal total)
    {
        lastId++;
        Id = $"ORD{lastId:D4}";
        Customer = customer;
        Total = total;
        CreatedAt = DateTime.Now;
    }

    public void Print() =>
        Console.WriteLine($"  {Id}  {Customer,-15}  {Total,14:N0} VNĐ  [{CreatedAt:HH:mm:ss}]");

    public static int GetTotalOrders() => lastId;
}

class Program
{
    static void Main()
    {
        var o1 = new Order("Minh", 500000m);
        var o2 = new Order("Hùng", 1200000m);
        var o3 = new Order("Lan", 350000m);

        Console.WriteLine("═══ ĐƠN HÀNG ═══");
        o1.Print(); o2.Print(); o3.Print();
        Console.WriteLine($"\nTổng đơn: {Order.GetTotalOrders()}");
    }
}
```

---

## Ví dụ 4: Object Initializer + Mảng Object

```csharp
using System;

class Task
{
    public string Title = "";
    public string Priority = "Medium";
    public bool IsDone = false;

    public void Toggle() => IsDone = !IsDone;
    public void Print(int i)
    {
        string check = IsDone ? "✅" : "⬜";
        var color = Priority switch { "High" => ConsoleColor.Red, "Low" => ConsoleColor.Gray, _ => ConsoleColor.White };
        Console.ForegroundColor = color;
        Console.WriteLine($"  {i}. {check} [{Priority,-6}] {Title}");
        Console.ResetColor();
    }
}

class Program
{
    static void Main()
    {
        // Object Initializer — gọn!
        Task[] tasks = {
            new Task { Title = "Học OOP Constructor", Priority = "High" },
            new Task { Title = "Làm bài tập Lesson 16", Priority = "High" },
            new Task { Title = "Review lại Phase 1", Priority = "Medium" },
            new Task { Title = "Đọc docs C#", Priority = "Low" },
        };

        tasks[0].Toggle();  // Mark done

        Console.WriteLine("═══ TODO LIST ═══");
        for (int i = 0; i < tasks.Length; i++)
            tasks[i].Print(i + 1);

        int done = 0;
        foreach (var t in tasks) if (t.IsDone) done++;
        Console.WriteLine($"\nHoàn thành: {done}/{tasks.Length}");
    }
}
```
