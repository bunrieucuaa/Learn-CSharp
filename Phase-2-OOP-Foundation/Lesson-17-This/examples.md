# 💻 Lesson 17 — Ví dụ: `this`

---

## Ví dụ 1: `this` phân biệt field/parameter

```csharp
using System;

class Employee
{
    public string name;
    public string department;
    public decimal salary;

    public Employee(string name, string department, decimal salary)
    {
        this.name = name;             // this.name = field
        this.department = department; // name = parameter
        this.salary = salary;
    }

    public void Print() =>
        Console.WriteLine($"  {this.name,-15} {this.department,-10} {this.salary,14:N0}");
}

class Program
{
    static void Main()
    {
        var e1 = new Employee("Minh", "IT", 15000000m);
        var e2 = new Employee("Lan", "HR", 12000000m);
        e1.Print(); e2.Print();
    }
}
```

---

## Ví dụ 2: Fluent API — `return this`

```csharp
using System;

class Pizza
{
    private string size = "M";
    private string crust = "Thin";
    private string[] toppings = new string[10];
    private int toppingCount = 0;

    public Pizza SetSize(string size) { this.size = size; return this; }
    public Pizza SetCrust(string crust) { this.crust = crust; return this; }
    public Pizza AddTopping(string topping)
    {
        if (toppingCount < 10)
            toppings[toppingCount++] = topping;
        return this;
    }

    public void Print()
    {
        Console.WriteLine($"🍕 Pizza {size}, đế {crust}");
        Console.Write("   Topping: ");
        for (int i = 0; i < toppingCount; i++)
            Console.Write($"{toppings[i]}{(i < toppingCount - 1 ? ", " : "")}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Method chaining nhờ return this
        new Pizza()
            .SetSize("L")
            .SetCrust("Thick")
            .AddTopping("Pepperoni")
            .AddTopping("Mushroom")
            .AddTopping("Extra Cheese")
            .Print();

        new Pizza()
            .SetSize("S")
            .AddTopping("Hawaiian")
            .Print();
    }
}
```

---

## Ví dụ 3: `this` truyền object hiện tại

```csharp
using System;

class Player
{
    public string Name;
    public int Score;

    public Player(string name, int score) { Name = name; Score = score; }

    public bool IsStrongerThan(Player other) => this.Score > other.Score;

    public void Challenge(Player other)
    {
        Console.Write($"  {this.Name}({this.Score}) vs {other.Name}({other.Score}): ");
        if (this.IsStrongerThan(other))
            Console.WriteLine($"{this.Name} thắng!");
        else if (other.IsStrongerThan(this))  // Truyền this cho method của other
            Console.WriteLine($"{other.Name} thắng!");
        else
            Console.WriteLine("Hòa!");
    }
}

class Program
{
    static void Main()
    {
        var p1 = new Player("Minh", 85);
        var p2 = new Player("Hùng", 72);
        var p3 = new Player("Lan", 85);

        p1.Challenge(p2);  // Minh thắng
        p2.Challenge(p1);  // Minh thắng
        p1.Challenge(p3);  // Hòa
    }
}
```

---

# ✏️ Bài tập + 🏆 Challenge

## Bài 1: `this` trong Constructor
Tạo class `Car` (make, model, year, price) — tất cả parameter TRÙNG TÊN field. Dùng `this.` để phân biệt.

## Bài 2: Fluent Builder
Tạo class `HtmlBuilder` với methods: `SetTag()`, `AddClass()`, `SetText()`, `AddAttribute()`, `Build()` — tất cả `return this`. Xây HTML element: `<div class="box red" id="main">Hello</div>`.

## Bài 3: `this` truyền object
Tạo class `Vector2D` (x, y) với: `Add(Vector2D other)`, `Subtract(Vector2D other)`, `DistanceTo(Vector2D other)`, `IsCloserThan(Vector2D a, Vector2D b)` — sử dụng `this` để so sánh.

## Challenge: Fluent Query Builder
Xây class `StudentQuery`: `.From(students[])`, `.Where(field, operator, value)`, `.OrderBy(field)`, `.Take(n)`, `.Execute()` → trả mảng kết quả. Tất cả `return this`. Test với mảng 10 sinh viên.
