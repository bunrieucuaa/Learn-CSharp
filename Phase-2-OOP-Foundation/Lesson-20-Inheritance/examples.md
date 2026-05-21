# 💻 Lesson 20 — Ví dụ: Inheritance (Tính kế thừa)

---

## Ví dụ 1: Animal Hierarchy — virtual / override

```csharp
using System;

// ===== BASE CLASS =====
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    protected int Energy { get; set; }

    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
        Energy = 100;
    }

    // virtual — cho phép con override
    public virtual void MakeSound()
    {
        Console.WriteLine($"  {Name}: *tiếng động*");
    }

    public virtual void Eat(string food)
    {
        Energy += 20;
        Console.WriteLine($"  {Name} ăn {food}. Energy: {Energy}");
    }

    public virtual void Sleep()
    {
        Energy += 30;
        Console.WriteLine($"  {Name} đang ngủ... 💤 Energy: {Energy}");
    }

    public override string ToString()
    {
        return $"{Name} ({GetType().Name}, {Age} tuổi, Energy: {Energy})";
    }
}

// ===== DERIVED CLASS: DOG =====
class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, int age, string breed) : base(name, age)
    {
        Breed = breed;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"  🐕 {Name}: Gâu gâu!");
    }

    public override void Eat(string food)
    {
        base.Eat(food);  // Gọi cha trước (tăng energy)
        if (food.ToLower() == "xương")
            Console.WriteLine($"     {Name} vẫy đuôi vui vẻ! 🦴");
    }

    public void Fetch(string item)
    {
        Energy -= 15;
        Console.WriteLine($"  🐕 {Name} nhặt {item}! Energy: {Energy}");
    }

    public override string ToString()
    {
        return $"🐕 {Name} - {Breed} ({Age} tuổi, Energy: {Energy})";
    }
}

// ===== DERIVED CLASS: CAT =====
class Cat : Animal
{
    public bool IsIndoor { get; set; }

    public Cat(string name, int age, bool isIndoor = true) : base(name, age)
    {
        IsIndoor = isIndoor;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"  🐱 {Name}: Meo meo!");
    }

    public override void Sleep()
    {
        Energy += 50;  // Mèo ngủ nhiều hơn!
        Console.WriteLine($"  🐱 {Name} ngủ suốt ngày... 💤💤 Energy: {Energy}");
    }

    public void Purr()
    {
        Console.WriteLine($"  🐱 {Name}: Gừ gừ gừ... 😻");
    }

    public override string ToString()
    {
        string indoor = IsIndoor ? "indoor" : "outdoor";
        return $"🐱 {Name} ({indoor}, {Age} tuổi, Energy: {Energy})";
    }
}

// ===== DERIVED CLASS: BIRD =====
class Bird : Animal
{
    public double Wingspan { get; set; }  // Sải cánh (cm)

    public Bird(string name, int age, double wingspan) : base(name, age)
    {
        Wingspan = wingspan;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"  🐦 {Name}: Chíp chíp!");
    }

    public void Fly()
    {
        Energy -= 25;
        Console.WriteLine($"  🐦 {Name} bay lên trời! ✈️ Energy: {Energy}");
    }

    public override string ToString()
    {
        return $"🐦 {Name} (wingspan: {Wingspan}cm, {Age} tuổi, Energy: {Energy})";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ ANIMAL HIERARCHY — VIRTUAL / OVERRIDE ═══\n");

        // Tạo các con vật
        var dog = new Dog("Buddy", 3, "Corgi");
        var cat = new Cat("Mimi", 2, true);
        var bird = new Bird("Tweety", 1, 25);

        // In thông tin
        Console.WriteLine("--- Thông tin ---");
        Console.WriteLine(dog);   // Tự gọi ToString()
        Console.WriteLine(cat);
        Console.WriteLine(bird);

        // Polymorphism preview: gọi qua kiểu Animal
        Console.WriteLine("\n--- MakeSound (override) ---");
        Animal[] animals = { dog, cat, bird };
        foreach (var a in animals)
        {
            a.MakeSound();  // Mỗi con kêu KHÁC NHAU!
        }

        // Dog-specific
        Console.WriteLine("\n--- Dog actions ---");
        dog.Eat("xương");
        dog.Fetch("bóng");

        // Cat-specific
        Console.WriteLine("\n--- Cat actions ---");
        cat.Sleep();       // Mèo ngủ nhiều hơn (override)
        cat.Purr();

        // Bird-specific
        Console.WriteLine("\n--- Bird actions ---");
        bird.Fly();

        // Kế thừa: tất cả có Eat(), Sleep() từ Animal
        Console.WriteLine("\n--- Inherited methods ---");
        bird.Eat("hạt");      // Kế thừa + override
        bird.Sleep();          // Kế thừa từ Animal (không override)
    }
}
```

**Output:**
```
═══ ANIMAL HIERARCHY — VIRTUAL / OVERRIDE ═══

--- Thông tin ---
🐕 Buddy - Corgi (3 tuổi, Energy: 100)
🐱 Mimi (indoor, 2 tuổi, Energy: 100)
🐦 Tweety (wingspan: 25cm, 1 tuổi, Energy: 100)

--- MakeSound (override) ---
  🐕 Buddy: Gâu gâu!
  🐱 Mimi: Meo meo!
  🐦 Tweety: Chíp chíp!

--- Dog actions ---
  Buddy ăn xương. Energy: 120
     Buddy vẫy đuôi vui vẻ! 🦴
  🐕 Buddy nhặt bóng! Energy: 105

--- Cat actions ---
  🐱 Mimi ngủ suốt ngày... 💤💤 Energy: 150
  🐱 Mimi: Gừ gừ gừ... 😻

--- Bird actions ---
  🐦 Tweety bay lên trời! ✈️ Energy: 75

--- Inherited methods ---
  Tweety ăn hạt. Energy: 95
  Tweety đang ngủ... 💤 Energy: 125
```

---

## Ví dụ 2: Employee Hierarchy — base() + Constructor Chain

```csharp
using System;

// ===== BASE CLASS =====
class Employee
{
    private static int _nextId = 1000;

    public int Id { get; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal BaseSalary { get; protected set; }
    public DateTime HireDate { get; }

    // Computed
    public int YearsWorked => (DateTime.Now - HireDate).Days / 365;

    public Employee(string name, string department, decimal baseSalary)
    {
        Id = ++_nextId;
        Name = name;
        Department = department;
        BaseSalary = baseSalary > 0 ? baseSalary : 0;
        HireDate = DateTime.Now;
    }

    public virtual decimal CalculateBonus()
    {
        return BaseSalary * 0.1m;  // Default: 10%
    }

    public virtual decimal CalculateSalary()
    {
        return BaseSalary + CalculateBonus();
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"  [{Id}] {Name,-15} {Department,-12} " +
                          $"Base: {BaseSalary,12:N0}  Bonus: {CalculateBonus(),10:N0}  " +
                          $"Total: {CalculateSalary(),12:N0}");
    }

    public override string ToString()
    {
        return $"[{Id}] {Name} ({GetType().Name}) — {Department}";
    }
}

// ===== MANAGER — lương cao, bonus theo số nhân viên quản lý =====
class Manager : Employee
{
    public int TeamSize { get; set; }
    public string Level { get; set; }  // Junior/Senior/Director

    public Manager(string name, string department, decimal baseSalary,
                   int teamSize, string level = "Senior")
        : base(name, department, baseSalary)  // Gọi constructor cha
    {
        TeamSize = teamSize;
        Level = level;
    }

    public override decimal CalculateBonus()
    {
        decimal baseBonus = base.CalculateBonus();  // Gọi bonus cha (10%)
        decimal teamBonus = TeamSize * 500_000m;     // +500k per member
        decimal levelBonus = Level switch
        {
            "Director" => BaseSalary * 0.3m,
            "Senior" => BaseSalary * 0.15m,
            _ => BaseSalary * 0.05m
        };
        return baseBonus + teamBonus + levelBonus;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"        Level: {Level}, Team: {TeamSize} người");
    }
}

// ===== DEVELOPER — bonus theo skill level =====
class Developer : Employee
{
    public string ProgrammingLanguage { get; set; }
    public int SkillLevel { get; set; }  // 1-10

    public Developer(string name, string department, decimal baseSalary,
                     string language, int skillLevel)
        : base(name, department, baseSalary)
    {
        ProgrammingLanguage = language;
        SkillLevel = Math.Clamp(skillLevel, 1, 10);
    }

    public override decimal CalculateBonus()
    {
        decimal baseBonus = base.CalculateBonus();
        decimal skillBonus = SkillLevel * 1_000_000m;  // +1tr per skill level
        return baseBonus + skillBonus;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"        Lang: {ProgrammingLanguage}, Skill: {SkillLevel}/10");
    }
}

// ===== INTERN — lương thấp, không bonus =====
class Intern : Employee
{
    public string School { get; set; }
    public int DurationMonths { get; set; }

    public Intern(string name, string department, decimal baseSalary,
                  string school, int months)
        : base(name, department, baseSalary)
    {
        School = school;
        DurationMonths = months;
    }

    public override decimal CalculateBonus()
    {
        return 0;  // Intern không có bonus
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"        School: {School}, Duration: {DurationMonths} tháng");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ EMPLOYEE HIERARCHY — BASE() + CONSTRUCTOR CHAIN ═══\n");

        // Tạo nhân viên
        Employee[] employees = {
            new Manager("Trần Anh", "Engineering", 40_000_000m, 8, "Director"),
            new Manager("Lê Hoa", "Marketing", 30_000_000m, 5, "Senior"),
            new Developer("Nguyễn Minh", "Engineering", 25_000_000m, "C#", 8),
            new Developer("Phạm Lan", "Engineering", 22_000_000m, "JavaScript", 6),
            new Intern("Võ Hùng", "Engineering", 5_000_000m, "HCMUT", 6),
        };

        // Header
        Console.WriteLine($"  {"ID",-6} {"Tên",-15} {"Phòng",-12} " +
                          $"{"Base",12}  {"Bonus",10}  {"Total",12}");
        Console.WriteLine("  " + new string('─', 80));

        // In thông tin — mỗi loại tự tính lương/bonus khác nhau!
        foreach (var emp in employees)
        {
            emp.PrintInfo();
            Console.WriteLine();
        }

        // Tổng hợp
        decimal totalSalary = 0;
        foreach (var emp in employees)
            totalSalary += emp.CalculateSalary();

        Console.WriteLine($"  {"",48} TỔNG: {totalSalary,12:N0}");

        // Type check
        Console.WriteLine("\n--- Kiểm tra loại ---");
        foreach (var emp in employees)
        {
            string type = emp switch
            {
                Manager m => $"Manager (Level: {m.Level})",
                Developer d => $"Developer ({d.ProgrammingLanguage})",
                Intern i => $"Intern ({i.School})",
                _ => "Employee"
            };
            Console.WriteLine($"  {emp.Name,-15} → {type}");
        }
    }
}
```

**Output:**
```
═══ EMPLOYEE HIERARCHY — BASE() + CONSTRUCTOR CHAIN ═══

  ID     Tên             Phòng        Base          Bonus        Total
  ────────────────────────────────────────────────────────────────────────────
  [1001] Trần Anh        Engineering  40,000,000   20,000,000   60,000,000
        Level: Director, Team: 8 người

  [1002] Lê Hoa          Marketing    30,000,000   10,000,000   40,000,000
        Level: Senior, Team: 5 người

  [1003] Nguyễn Minh     Engineering  25,000,000   10,500,000   35,500,000
        Lang: C#, Skill: 8/10

  [1004] Phạm Lan        Engineering  22,000,000    8,200,000   30,200,000
        Lang: JavaScript, Skill: 6/10

  [1005] Võ Hùng         Engineering   5,000,000           0    5,000,000
        School: HCMUT, Duration: 6 tháng
```

---

## Ví dụ 3: Shape Hierarchy — ToString() Override + Computed Properties

```csharp
using System;

// ===== BASE CLASS =====
class Shape
{
    public string Name { get; protected set; }
    public string Color { get; set; }

    public Shape(string name, string color = "Đỏ")
    {
        Name = name;
        Color = color;
    }

    public virtual double CalculateArea() => 0;
    public virtual double CalculatePerimeter() => 0;

    public override string ToString()
    {
        return $"{Name} ({Color}) — S: {CalculateArea():F2}, C: {CalculatePerimeter():F2}";
    }

    public virtual void Draw()
    {
        Console.WriteLine($"  🎨 Vẽ {Name} màu {Color}");
    }
}

// ===== CIRCLE =====
class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius, string color = "Đỏ")
        : base("Hình tròn", color)
    {
        Radius = radius > 0 ? radius : 1;
    }

    public override double CalculateArea()
        => Math.PI * Radius * Radius;

    public override double CalculatePerimeter()
        => 2 * Math.PI * Radius;

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine($"       ╭───╮");
        Console.WriteLine($"      │  ●  │  R = {Radius}");
        Console.WriteLine($"       ╰───╯");
    }

    public override string ToString()
        => $"⭕ Hình tròn (R={Radius}, {Color}) — S: {CalculateArea():F2}";
}

// ===== RECTANGLE =====
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    // Computed
    public bool IsSquare => Math.Abs(Width - Height) < 0.001;

    public Rectangle(double width, double height, string color = "Xanh")
        : base("Hình chữ nhật", color)
    {
        Width = width > 0 ? width : 1;
        Height = height > 0 ? height : 1;
        if (IsSquare) Name = "Hình vuông";
    }

    public override double CalculateArea()
        => Width * Height;

    public override double CalculatePerimeter()
        => 2 * (Width + Height);

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine($"      ┌────────┐");
        Console.WriteLine($"      │        │  {Width} × {Height}");
        Console.WriteLine($"      └────────┘");
    }

    public override string ToString()
        => $"▬ {Name} ({Width}×{Height}, {Color}) — S: {CalculateArea():F2}";
}

// ===== TRIANGLE =====
class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    // Computed
    public string TriangleType => (SideA, SideB, SideC) switch
    {
        var (a, b, c) when a == b && b == c => "Đều",
        var (a, b, c) when a == b || b == c || a == c => "Cân",
        _ => "Thường"
    };

    public Triangle(double a, double b, double c, string color = "Vàng")
        : base("Tam giác", color)
    {
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Ba cạnh không tạo thành tam giác!");
        SideA = a; SideB = b; SideC = c;
    }

    public override double CalculateArea()
    {
        // Heron's formula
        double s = CalculatePerimeter() / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }

    public override double CalculatePerimeter()
        => SideA + SideB + SideC;

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine($"        △");
        Console.WriteLine($"       ╱ ╲     {TriangleType}");
        Console.WriteLine($"      ╱   ╲    ({SideA}, {SideB}, {SideC})");
        Console.WriteLine($"     ╱─────╲");
    }

    public override string ToString()
        => $"△ Tam giác {TriangleType} ({SideA},{SideB},{SideC}, {Color}) — S: {CalculateArea():F2}";
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ SHAPE HIERARCHY — TOSTRING() OVERRIDE ═══\n");

        Shape[] shapes = {
            new Circle(5, "Đỏ"),
            new Rectangle(8, 4, "Xanh"),
            new Rectangle(6, 6, "Tím"),           // Hình vuông!
            new Triangle(3, 4, 5, "Vàng"),
            new Triangle(5, 5, 5, "Cam"),           // Tam giác đều
        };

        // In ToString()
        Console.WriteLine("--- ToString() override ---");
        foreach (var s in shapes)
            Console.WriteLine($"  {s}");

        // Vẽ
        Console.WriteLine("\n--- Draw() override ---");
        foreach (var s in shapes)
        {
            s.Draw();
            Console.WriteLine();
        }

        // Thống kê
        Console.WriteLine("--- Thống kê ---");
        double totalArea = 0, totalPerimeter = 0;
        foreach (var s in shapes)
        {
            totalArea += s.CalculateArea();
            totalPerimeter += s.CalculatePerimeter();
        }
        Console.WriteLine($"  Tổng diện tích : {totalArea:F2}");
        Console.WriteLine($"  Tổng chu vi    : {totalPerimeter:F2}");
        Console.WriteLine($"  Số hình        : {shapes.Length}");
    }
}
```

---

## Ví dụ 4: Game Character System — Character → Warrior, Mage, Archer

```csharp
using System;
using System.Collections.Generic;

// ===== BASE CLASS =====
class Character
{
    private static int _nextId = 0;

    public int Id { get; }
    public string Name { get; set; }
    public int Level { get; protected set; }
    public int Experience { get; protected set; }

    // Stats — protected để con truy cập
    protected int BaseHp { get; set; }
    protected int BaseAtk { get; set; }
    protected int BaseDef { get; set; }

    // Computed — tính dựa trên level
    public int MaxHp => BaseHp + Level * 20;
    public int Atk => BaseAtk + Level * 5;
    public int Def => BaseDef + Level * 3;
    public int CurrentHp { get; protected set; }
    public bool IsAlive => CurrentHp > 0;
    public int ExpToNextLevel => Level * 100;

    public Character(string name, int hp, int atk, int def)
    {
        Id = ++_nextId;
        Name = name;
        Level = 1;
        Experience = 0;
        BaseHp = hp;
        BaseAtk = atk;
        BaseDef = def;
        CurrentHp = MaxHp;
    }

    public virtual string ClassName => "Character";

    public virtual void Attack(Character target)
    {
        int damage = Math.Max(1, Atk - target.Def);
        target.TakeDamage(damage);
        Console.WriteLine($"  ⚔️ {Name} tấn công {target.Name}! Damage: {damage}");
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = Math.Max(0, CurrentHp - damage);
        if (!IsAlive)
            Console.WriteLine($"  💀 {Name} đã bị hạ!");
    }

    public void Heal(int amount)
    {
        if (!IsAlive) { Console.WriteLine($"  ❌ {Name} đã chết, không thể hồi!"); return; }
        CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
        Console.WriteLine($"  💚 {Name} hồi {amount} HP! ({CurrentHp}/{MaxHp})");
    }

    public void GainExp(int exp)
    {
        Experience += exp;
        Console.WriteLine($"  ✨ {Name} +{exp} EXP ({Experience}/{ExpToNextLevel})");
        while (Experience >= ExpToNextLevel)
        {
            Experience -= ExpToNextLevel;
            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        Level++;
        CurrentHp = MaxHp;  // Full HP khi lên level
        Console.WriteLine($"  🎉 {Name} lên Level {Level}! HP: {MaxHp}, ATK: {Atk}, DEF: {Def}");
    }

    public virtual void UseSpecialSkill()
    {
        Console.WriteLine($"  {Name} không có skill đặc biệt.");
    }

    public void PrintStatus()
    {
        string hpBar = BuildHpBar(CurrentHp, MaxHp);
        Console.WriteLine($"  [{ClassName}] {Name} (Lv.{Level})");
        Console.WriteLine($"  HP: {hpBar} {CurrentHp}/{MaxHp}");
        Console.WriteLine($"  ATK: {Atk}  DEF: {Def}  EXP: {Experience}/{ExpToNextLevel}");
    }

    private string BuildHpBar(int current, int max)
    {
        int filled = (int)((double)current / max * 20);
        int empty = 20 - filled;
        return $"[{"█".PadRight(filled, '█')}{"░".PadRight(empty, '░')}]";
    }

    public override string ToString()
        => $"[{ClassName}] {Name} Lv.{Level} HP:{CurrentHp}/{MaxHp}";
}

// ===== WARRIOR — Tanker, high HP & DEF =====
class Warrior : Character
{
    public int ShieldBlock { get; private set; }

    public Warrior(string name) : base(name, hp: 150, atk: 20, def: 25)
    {
        ShieldBlock = 10;
    }

    public override string ClassName => "⚔️ Warrior";

    public override void Attack(Character target)
    {
        // Warrior: bonus damage khi HP thấp
        int damage = Math.Max(1, Atk - target.Def);
        if (CurrentHp < MaxHp / 2)
        {
            damage = (int)(damage * 1.5);
            Console.WriteLine($"  🔥 {Name} giận dữ! (HP thấp → +50% damage)");
        }
        target.TakeDamage(damage);
        Console.WriteLine($"  ⚔️ {Name} chém {target.Name}! Damage: {damage}");
    }

    public override void UseSpecialSkill()
    {
        ShieldBlock += 5;
        Heal(30);
        Console.WriteLine($"  🛡️ {Name} dùng Shield Wall! Block +5, Hồi 30 HP");
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        ShieldBlock += 2;
        Console.WriteLine($"     Shield Block: {ShieldBlock}");
    }
}

// ===== MAGE — High ATK, low DEF =====
class Mage : Character
{
    public int Mana { get; private set; }
    public int MaxMana { get; private set; }

    public Mage(string name) : base(name, hp: 80, atk: 35, def: 10)
    {
        MaxMana = 100;
        Mana = MaxMana;
    }

    public override string ClassName => "🧙 Mage";

    public override void Attack(Character target)
    {
        if (Mana >= 20)
        {
            // Magic attack — bỏ qua DEF
            Mana -= 20;
            int damage = Atk;
            target.TakeDamage(damage);
            Console.WriteLine($"  🔮 {Name} bắn phép vào {target.Name}! Damage: {damage} (ignore DEF)");
            Console.WriteLine($"     Mana: {Mana}/{MaxMana}");
        }
        else
        {
            // Normal attack khi hết mana
            base.Attack(target);
            Console.WriteLine($"     ⚠️ Hết mana! Đánh thường.");
        }
    }

    public override void UseSpecialSkill()
    {
        if (Mana >= 50)
        {
            Mana -= 50;
            Console.WriteLine($"  🌟 {Name} dùng Fireball! Damage AoE toàn bộ!");
            Console.WriteLine($"     Mana: {Mana}/{MaxMana}");
        }
        else
        {
            Console.WriteLine($"  ❌ Không đủ Mana! ({Mana}/{MaxMana}, cần 50)");
        }
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        MaxMana += 20;
        Mana = MaxMana;
        Console.WriteLine($"     Mana: {MaxMana}");
    }
}

// ===== ARCHER — Balanced, critical hit =====
class Archer : Character
{
    private readonly Random _rng = new();
    public int CritChance { get; private set; }  // %

    public Archer(string name) : base(name, hp: 100, atk: 28, def: 15)
    {
        CritChance = 25;
    }

    public override string ClassName => "🏹 Archer";

    public override void Attack(Character target)
    {
        int damage = Math.Max(1, Atk - target.Def);
        bool isCrit = _rng.Next(100) < CritChance;

        if (isCrit)
        {
            damage *= 2;
            Console.Write($"  💥 CRITICAL! ");
        }
        else
        {
            Console.Write($"  🏹 ");
        }

        target.TakeDamage(damage);
        Console.WriteLine($"{Name} bắn {target.Name}! Damage: {damage}");
    }

    public override void UseSpecialSkill()
    {
        CritChance = Math.Min(80, CritChance + 10);
        Console.WriteLine($"  🎯 {Name} dùng Eagle Eye! Crit: {CritChance}%");
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        CritChance += 5;
        Console.WriteLine($"     Crit Chance: {CritChance}%");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ GAME CHARACTER SYSTEM ═══\n");

        // Tạo nhân vật
        var warrior = new Warrior("Guts");
        var mage = new Mage("Gandalf");
        var archer = new Archer("Legolas");

        // In status ban đầu
        Console.WriteLine("--- Party Status ---");
        warrior.PrintStatus();
        Console.WriteLine();
        mage.PrintStatus();
        Console.WriteLine();
        archer.PrintStatus();

        // Combat!
        Console.WriteLine("\n--- COMBAT! ---");
        var enemy = new Character("Goblin", 60, 15, 8);
        Console.Write("  Enemy: ");
        Console.WriteLine(enemy);

        Console.WriteLine();
        warrior.Attack(enemy);
        mage.Attack(enemy);
        archer.Attack(enemy);

        // Special skills
        Console.WriteLine("\n--- Special Skills ---");
        warrior.UseSpecialSkill();
        mage.UseSpecialSkill();
        archer.UseSpecialSkill();

        // Gain EXP
        Console.WriteLine("\n--- Gain EXP ---");
        warrior.GainExp(80);
        warrior.GainExp(50);   // Level up!

        // Polymorphism: mảng Character chung
        Console.WriteLine("\n--- Final Status ---");
        Character[] party = { warrior, mage, archer };
        foreach (var c in party)
        {
            c.PrintStatus();
            Console.WriteLine();
        }
    }
}
```

**Output (partial):**
```
═══ GAME CHARACTER SYSTEM ═══

--- Party Status ---
  [⚔️ Warrior] Guts (Lv.1)
  HP: [████████████████████] 170/170
  ATK: 25  DEF: 28  EXP: 0/100

  [🧙 Mage] Gandalf (Lv.1)
  HP: [████████████████████] 100/100
  ATK: 40  DEF: 13  EXP: 0/100

  [🏹 Archer] Legolas (Lv.1)
  HP: [████████████████████] 120/120
  ATK: 33  DEF: 18  EXP: 0/100

--- COMBAT! ---
  Enemy: [Character] Goblin Lv.1 HP:80/80

  ⚔️ Guts chém Goblin! Damage: 17
  🔮 Gandalf bắn phép vào Goblin! Damage: 40 (ignore DEF)
     Mana: 80/100
  💥 CRITICAL! Legolas bắn Goblin! Damage: 50
  💀 Goblin đã bị hạ!
```
