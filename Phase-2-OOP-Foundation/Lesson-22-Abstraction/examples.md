# 💻 Bài 22: Abstraction — Ví dụ thực hành

> **Tất cả ví dụ đều chạy được. Hãy tạo Console App và copy-paste để thử!**

---

## Ví dụ 1: Abstract Shape — Class trừu tượng cơ bản 🔷

> **Mục tiêu:** Abstract class với abstract methods và concrete methods kết hợp.

```csharp
using System;

namespace AbstractShapeDemo
{
    // ═══════════════════════════════════════
    // ABSTRACT CLASS: Shape
    // ═══════════════════════════════════════
    abstract class Shape
    {
        // Properties — abstract class CÓ THỂ có fields/properties
        public string Name { get; set; }
        public string Color { get; set; }

        // Constructor — abstract class CÓ THỂ có constructor
        protected Shape(string name, string color)
        {
            Name = name;
            Color = color;
        }

        // ═══ ABSTRACT METHODS — class con PHẢI override ═══
        public abstract double GetArea();
        public abstract double GetPerimeter();
        public abstract void Draw();

        // ═══ CONCRETE METHODS — class con DÙNG LUÔN ═══
        public void PrintInfo()
        {
            Console.WriteLine("┌──────────────────────────────");
            Console.WriteLine($"│ 📐 {Name}");
            Console.WriteLine($"│ 🎨 Màu: {Color}");
            Console.WriteLine($"│ 📏 Diện tích: {GetArea():F2}");    // Gọi abstract!
            Console.WriteLine($"│ 📐 Chu vi: {GetPerimeter():F2}");  // Gọi abstract!
            Console.Write("│ ");
            Draw();                                                    // Gọi abstract!
            Console.WriteLine("└──────────────────────────────");
        }

        // Template method — dùng abstract methods bên trong
        public string GetSummary()
        {
            return $"{Name} ({Color}) - S={GetArea():F2}, P={GetPerimeter():F2}";
        }
    }

    // ═══════════════════════════════════════
    // CIRCLE — Implement đầy đủ
    // ═══════════════════════════════════════
    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius, string color)
            : base("Hình tròn", color)
        {
            Radius = radius;
        }

        public override double GetArea()
            => Math.PI * Radius * Radius;

        public override double GetPerimeter()
            => 2 * Math.PI * Radius;

        public override void Draw()
            => Console.WriteLine($"🔵 Vẽ hình tròn, bán kính = {Radius}");
    }

    // ═══════════════════════════════════════
    // RECTANGLE — Implement đầy đủ
    // ═══════════════════════════════════════
    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height, string color)
            : base("Hình chữ nhật", color)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
            => Width * Height;

        public override double GetPerimeter()
            => 2 * (Width + Height);

        public override void Draw()
            => Console.WriteLine($"🟦 Vẽ HCN {Width} x {Height}");
    }

    // ═══════════════════════════════════════
    // TRIANGLE — Implement đầy đủ
    // ═══════════════════════════════════════
    class Triangle : Shape
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public Triangle(double a, double b, double c, string color)
            : base("Tam giác", color)
        {
            A = a; B = b; C = c;
        }

        public override double GetArea()
        {
            double s = (A + B + C) / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }

        public override double GetPerimeter()
            => A + B + C;

        public override void Draw()
            => Console.WriteLine($"🔺 Vẽ tam giác ({A}, {B}, {C})");
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   ABSTRACT SHAPE DEMO                ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // ❌ Không thể tạo object từ abstract class!
            // Shape s = new Shape("test", "red");  // Lỗi compile!

            // ✅ Tạo object từ class con
            Shape[] shapes = new Shape[]
            {
                new Circle(5, "Đỏ"),
                new Rectangle(4, 6, "Xanh"),
                new Triangle(3, 4, 5, "Vàng"),
                new Circle(3, "Tím"),
                new Rectangle(8, 2, "Cam")
            };

            // Polymorphism — PrintInfo() gọi abstract methods
            foreach (Shape shape in shapes)
            {
                shape.PrintInfo();
                Console.WriteLine();
            }

            // Tổng kết
            Console.WriteLine("═══ TỔNG KẾT ═══");
            double totalArea = 0;
            foreach (Shape shape in shapes)
            {
                Console.WriteLine($"  {shape.GetSummary()}");
                totalArea += shape.GetArea();
            }
            Console.WriteLine($"\n  📊 Tổng diện tích: {totalArea:F2}");

            // Tìm hình có diện tích lớn nhất
            Shape largest = shapes[0];
            foreach (Shape shape in shapes)
            {
                if (shape.GetArea() > largest.GetArea())
                    largest = shape;
            }
            Console.WriteLine($"  🏆 Hình lớn nhất: {largest.GetSummary()}");
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   ABSTRACT SHAPE DEMO                ║
╚══════════════════════════════════════╝

┌──────────────────────────────
│ 📐 Hình tròn
│ 🎨 Màu: Đỏ
│ 📏 Diện tích: 78.54
│ 📐 Chu vi: 31.42
│ 🔵 Vẽ hình tròn, bán kính = 5
└──────────────────────────────
...
```

---

## Ví dụ 2: Game Interfaces — IPlayable, ISaveable 🎮

> **Mục tiêu:** Nhiều interfaces cho game objects — 1 class implement nhiều interface.

```csharp
using System;

namespace GameInterfacesDemo
{
    // ═══════════════════════════════════════
    // INTERFACES — Định nghĩa "khả năng"
    // ═══════════════════════════════════════

    interface IPlayable
    {
        bool IsPlaying { get; }
        void Play();
        void Pause();
        void Stop();
    }

    interface ISaveable
    {
        DateTime LastSaved { get; }
        bool Save(string filePath);
        bool Load(string filePath);
    }

    interface IDamageable
    {
        int Health { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        void TakeDamage(int amount);
        void Heal(int amount);
    }

    interface IMovable
    {
        double X { get; }
        double Y { get; }
        double Speed { get; }
        void MoveTo(double x, double y);
    }

    // ═══════════════════════════════════════
    // GAME CHARACTER — Implement nhiều interfaces
    // ═══════════════════════════════════════
    class GameCharacter : IDamageable, IMovable, ISaveable
    {
        public string Name { get; set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsAlive => Health > 0;
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Speed { get; private set; }
        public DateTime LastSaved { get; private set; }

        public GameCharacter(string name, int maxHealth, double speed)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Speed = speed;
            X = 0;
            Y = 0;
        }

        // IDamageable implementation
        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
            Console.WriteLine($"  💥 {Name} nhận {amount} damage! " +
                            $"HP: {Health}/{MaxHealth}");
            if (!IsAlive) Console.WriteLine($"  ☠️ {Name} đã ngã xuống!");
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
            Console.WriteLine($"  💚 {Name} hồi {amount} HP! " +
                            $"HP: {Health}/{MaxHealth}");
        }

        // IMovable implementation
        public void MoveTo(double x, double y)
        {
            double distance = Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
            double time = distance / Speed;
            Console.WriteLine($"  🏃 {Name} di chuyển ({X:F0},{Y:F0}) → ({x:F0},{y:F0}) " +
                            $"[{distance:F1}m, {time:F1}s]");
            X = x;
            Y = y;
        }

        // ISaveable implementation
        public bool Save(string filePath)
        {
            LastSaved = DateTime.Now;
            Console.WriteLine($"  💾 Lưu {Name} vào {filePath} " +
                            $"[HP:{Health}, Pos:({X:F0},{Y:F0})]");
            return true;
        }

        public bool Load(string filePath)
        {
            Console.WriteLine($"  📂 Tải {Name} từ {filePath}");
            return true;
        }
    }

    // ═══════════════════════════════════════
    // MUSIC PLAYER — Implement IPlayable + ISaveable
    // ═══════════════════════════════════════
    class MusicPlayer : IPlayable, ISaveable
    {
        public string CurrentSong { get; set; }
        public bool IsPlaying { get; private set; }
        public DateTime LastSaved { get; private set; }

        public MusicPlayer(string song)
        {
            CurrentSong = song;
        }

        public void Play()
        {
            IsPlaying = true;
            Console.WriteLine($"  🎵 Đang phát: {CurrentSong}");
        }

        public void Pause()
        {
            IsPlaying = false;
            Console.WriteLine($"  ⏸ Tạm dừng: {CurrentSong}");
        }

        public void Stop()
        {
            IsPlaying = false;
            Console.WriteLine($"  ⏹ Dừng phát: {CurrentSong}");
        }

        public bool Save(string filePath)
        {
            LastSaved = DateTime.Now;
            Console.WriteLine($"  💾 Lưu playlist vào {filePath}");
            return true;
        }

        public bool Load(string filePath)
        {
            Console.WriteLine($"  📂 Tải playlist từ {filePath}");
            return true;
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        // Method nhận INTERFACE — hoạt động với BẤT KỲ class nào implement
        static void SaveAll(ISaveable[] items, string basePath)
        {
            Console.WriteLine("═══ LƯU TẤT CẢ ═══");
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Save($"{basePath}/save_{i}.dat");
            }
        }

        static void DamageAll(IDamageable[] targets, int damage)
        {
            Console.WriteLine($"═══ GÂY {damage} DAMAGE CHO TẤT CẢ ═══");
            foreach (IDamageable target in targets)
            {
                target.TakeDamage(damage);
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   GAME INTERFACES DEMO               ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo game characters
            GameCharacter hero = new GameCharacter("Warrior", 100, 5.0);
            GameCharacter mage = new GameCharacter("Mage", 60, 3.0);
            MusicPlayer bgm = new MusicPlayer("Battle Theme.mp3");

            // === Test IDamageable ===
            Console.WriteLine("── IDamageable ──");
            hero.TakeDamage(30);
            mage.TakeDamage(20);
            hero.Heal(15);

            // Mảng IDamageable — chỉ chứa objects implement IDamageable
            IDamageable[] targets = { hero, mage };
            DamageAll(targets, 25);

            // === Test IMovable ===
            Console.WriteLine("\n── IMovable ──");
            hero.MoveTo(10, 20);
            mage.MoveTo(5, 15);

            // Dùng IMovable type
            IMovable[] movables = { hero, mage };
            foreach (IMovable m in movables)
            {
                m.MoveTo(0, 0);  // Về gốc
            }

            // === Test ISaveable ===
            Console.WriteLine("\n── ISaveable ──");
            // GameCharacter VÀ MusicPlayer đều ISaveable!
            ISaveable[] saveables = { hero, mage, bgm };
            SaveAll(saveables, "saves");

            // === Test IPlayable ===
            Console.WriteLine("\n── IPlayable ──");
            bgm.Play();
            bgm.Pause();
            bgm.Play();
            bgm.Stop();

            // === Kiểm tra interface với is ===
            Console.WriteLine("\n── Type Checking ──");
            object[] everything = { hero, mage, bgm };
            foreach (object obj in everything)
            {
                Console.Write($"  {obj.GetType().Name}: ");
                if (obj is IDamageable) Console.Write("Damageable ");
                if (obj is IMovable) Console.Write("Movable ");
                if (obj is ISaveable) Console.Write("Saveable ");
                if (obj is IPlayable) Console.Write("Playable ");
                Console.WriteLine();
            }
        }
    }
}
```

**Output mong đợi:**
```
── IDamageable ──
  💥 Warrior nhận 30 damage! HP: 70/100
  💥 Mage nhận 20 damage! HP: 40/60
  💚 Warrior hồi 15 HP! HP: 85/100
═══ GÂY 25 DAMAGE CHO TẤT CẢ ═══
  💥 Warrior nhận 25 damage! HP: 60/100
  💥 Mage nhận 25 damage! HP: 15/60

── Type Checking ──
  GameCharacter: Damageable Movable Saveable
  GameCharacter: Damageable Movable Saveable
  MusicPlayer: Saveable Playable
```

---

## Ví dụ 3: Employee System — Abstract Class + Interface kết hợp 👔

> **Mục tiêu:** Abstract class cho shared behavior + Interface cho capabilities khác nhau.

```csharp
using System;

namespace EmployeeSystemDemo
{
    // ═══════════════════════════════════════
    // INTERFACES — Capabilities
    // ═══════════════════════════════════════

    interface IPayable
    {
        decimal CalculateSalary();
        void PrintPaySlip();
    }

    interface IEvaluable
    {
        int PerformanceScore { get; set; }   // 1-10
        string Evaluate();
    }

    interface IPromotable
    {
        int Level { get; set; }
        bool Promote();
    }

    // ═══════════════════════════════════════
    // ABSTRACT CLASS: Employee
    // ═══════════════════════════════════════
    abstract class Employee : IPayable, IEvaluable
    {
        // Properties chung
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int YearsOfExperience { get; set; }
        public int PerformanceScore { get; set; }

        // Constructor
        protected Employee(string id, string name, string department, int years)
        {
            Id = id;
            Name = name;
            Department = department;
            YearsOfExperience = years;
            PerformanceScore = 5;
        }

        // Abstract — mỗi loại tính lương khác nhau
        public abstract decimal CalculateSalary();
        public abstract string GetEmployeeType();

        // Concrete — dùng chung
        public void PrintPaySlip()
        {
            decimal salary = CalculateSalary();
            Console.WriteLine("┌────────────────────────────────────");
            Console.WriteLine($"│ 📋 PHIẾU LƯƠNG — {GetEmployeeType()}");
            Console.WriteLine($"│ ID: {Id}");
            Console.WriteLine($"│ Tên: {Name}");
            Console.WriteLine($"│ Phòng ban: {Department}");
            Console.WriteLine($"│ Kinh nghiệm: {YearsOfExperience} năm");
            Console.WriteLine($"│ 💰 Lương: {salary:N0}đ");
            Console.WriteLine("└────────────────────────────────────");
        }

        public string Evaluate()
        {
            return PerformanceScore switch
            {
                >= 9 => "🌟 Xuất sắc",
                >= 7 => "✅ Tốt",
                >= 5 => "👍 Khá",
                >= 3 => "⚠️ Cần cải thiện",
                _    => "❌ Không đạt"
            };
        }

        public void ShowEvaluation()
        {
            Console.WriteLine($"  {Name}: Score {PerformanceScore}/10 — {Evaluate()}");
        }
    }

    // ═══════════════════════════════════════
    // FULL-TIME EMPLOYEE — Có thể thăng chức
    // ═══════════════════════════════════════
    class FullTimeEmployee : Employee, IPromotable
    {
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public double TaxRate { get; set; }
        public int Level { get; set; }

        public FullTimeEmployee(string id, string name, string dept,
                               int years, decimal baseSalary)
            : base(id, name, dept, years)
        {
            BaseSalary = baseSalary;
            Bonus = 0;
            TaxRate = 0.1;
            Level = 1;
        }

        public override decimal CalculateSalary()
        {
            decimal gross = BaseSalary + Bonus;
            decimal levelBonus = BaseSalary * Level * 0.05m;
            return (gross + levelBonus) * (1 - (decimal)TaxRate);
        }

        public override string GetEmployeeType() => "Nhân viên Chính thức";

        public bool Promote()
        {
            if (PerformanceScore >= 7 && YearsOfExperience >= Level)
            {
                Level++;
                Console.WriteLine($"  🎉 {Name} thăng cấp lên Level {Level}!");
                return true;
            }
            Console.WriteLine($"  ⚠️ {Name} chưa đủ điều kiện thăng cấp");
            return false;
        }
    }

    // ═══════════════════════════════════════
    // PART-TIME EMPLOYEE — Không thể thăng chức
    // ═══════════════════════════════════════
    class PartTimeEmployee : Employee
    {
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }

        public PartTimeEmployee(string id, string name, string dept,
                               int hours, decimal hourlyRate)
            : base(id, name, dept, 0)
        {
            HoursWorked = hours;
            HourlyRate = hourlyRate;
        }

        public override decimal CalculateSalary()
        {
            decimal overtime = HoursWorked > 160
                ? (HoursWorked - 160) * HourlyRate * 1.5m
                : 0;
            int normalHours = Math.Min(HoursWorked, 160);
            return normalHours * HourlyRate + overtime;
        }

        public override string GetEmployeeType() => "Nhân viên Bán thời gian";
    }

    // ═══════════════════════════════════════
    // INTERN — Thực tập sinh
    // ═══════════════════════════════════════
    class Intern : Employee
    {
        public string Mentor { get; set; }
        public decimal Stipend { get; set; }   // Phụ cấp

        public Intern(string id, string name, string dept,
                     string mentor, decimal stipend)
            : base(id, name, dept, 0)
        {
            Mentor = mentor;
            Stipend = stipend;
        }

        public override decimal CalculateSalary() => Stipend;

        public override string GetEmployeeType() => "Thực tập sinh";
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   EMPLOYEE SYSTEM — ABSTRACTION     ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo nhân viên
            FullTimeEmployee ft1 = new FullTimeEmployee(
                "FT001", "Nguyễn Văn A", "Engineering", 5, 20_000_000);
            ft1.Bonus = 3_000_000;
            ft1.PerformanceScore = 8;

            FullTimeEmployee ft2 = new FullTimeEmployee(
                "FT002", "Trần Thị B", "Marketing", 3, 15_000_000);
            ft2.PerformanceScore = 9;

            PartTimeEmployee pt1 = new PartTimeEmployee(
                "PT001", "Lê Văn C", "Support", 120, 80_000);
            pt1.PerformanceScore = 7;

            Intern intern1 = new Intern(
                "IN001", "Phạm Thị D", "Engineering", "Nguyễn Văn A", 4_000_000);
            intern1.PerformanceScore = 6;

            // === Mảng Employee[] — Abstract class polymorphism ===
            Employee[] allEmployees = { ft1, ft2, pt1, intern1 };

            Console.WriteLine("═══ PHIẾU LƯƠNG ═══\n");
            decimal totalSalary = 0;
            foreach (Employee emp in allEmployees)
            {
                emp.PrintPaySlip();  // Polymorphism — mỗi loại in khác nhau
                totalSalary += emp.CalculateSalary();
                Console.WriteLine();
            }
            Console.WriteLine($"📊 Tổng chi phí lương: {totalSalary:N0}đ\n");

            // === IEvaluable — Đánh giá hiệu suất ===
            Console.WriteLine("═══ ĐÁNH GIÁ HIỆU SUẤT ═══");
            foreach (Employee emp in allEmployees)
            {
                emp.ShowEvaluation();  // IEvaluable
            }

            // === IPromotable — Chỉ FullTimeEmployee ===
            Console.WriteLine("\n═══ XÉT THĂNG CHỨC ═══");
            foreach (Employee emp in allEmployees)
            {
                // Pattern matching: chỉ xử lý nếu implement IPromotable
                if (emp is IPromotable promotable)
                {
                    promotable.Promote();
                }
                else
                {
                    Console.WriteLine($"  ℹ️ {emp.Name} ({emp.GetEmployeeType()}) " +
                                    "— không áp dụng thăng chức");
                }
            }

            // === Tìm nhân viên có lương cao nhất ===
            Console.WriteLine("\n═══ THỐNG KÊ ═══");
            Employee highest = allEmployees[0];
            foreach (Employee emp in allEmployees)
            {
                if (emp.CalculateSalary() > highest.CalculateSalary())
                    highest = emp;
            }
            Console.WriteLine($"  🏆 Lương cao nhất: {highest.Name} " +
                            $"({highest.CalculateSalary():N0}đ)");

            // Đếm theo interface
            int payable = 0, evaluable = 0, promotable2 = 0;
            foreach (Employee emp in allEmployees)
            {
                if (emp is IPayable) payable++;
                if (emp is IEvaluable) evaluable++;
                if (emp is IPromotable) promotable2++;
            }
            Console.WriteLine($"  💰 IPayable: {payable}");
            Console.WriteLine($"  📊 IEvaluable: {evaluable}");
            Console.WriteLine($"  🎯 IPromotable: {promotable2}");
        }
    }
}
```

---

## Ví dụ 4: Repository Pattern — IRepository Interface 🗄️

> **Mục tiêu:** Interface `IRepository<T>` với nhiều implementation — pattern quan trọng trong thực tế!

```csharp
using System;

namespace RepositoryPatternDemo
{
    // ═══════════════════════════════════════
    // MODEL: Product
    // ═══════════════════════════════════════
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Price:N0}đ (Kho: {Stock})";
        }
    }

    // ═══════════════════════════════════════
    // INTERFACE: IRepository
    // ═══════════════════════════════════════
    interface IRepository
    {
        void Add(Product product);
        Product GetById(int id);
        Product[] GetAll();
        bool Update(Product product);
        bool Delete(int id);
        int Count();
    }

    // ═══════════════════════════════════════
    // IMPLEMENTATION 1: InMemoryRepository
    // (Lưu trong bộ nhớ — dùng để test)
    // ═══════════════════════════════════════
    class InMemoryRepository : IRepository
    {
        private Product[] products;
        private int count;

        public InMemoryRepository(int capacity = 100)
        {
            products = new Product[capacity];
            count = 0;
        }

        public void Add(Product product)
        {
            if (count < products.Length)
            {
                products[count] = product;
                count++;
                Console.WriteLine($"  📝 [Memory] Thêm: {product.Name}");
            }
        }

        public Product GetById(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id)
                    return products[i];
            }
            return null;
        }

        public Product[] GetAll()
        {
            Product[] result = new Product[count];
            Array.Copy(products, result, count);
            return result;
        }

        public bool Update(Product product)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == product.Id)
                {
                    products[i] = product;
                    Console.WriteLine($"  ✏️ [Memory] Cập nhật: {product.Name}");
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id)
                {
                    string name = products[i].Name;
                    // Dịch các phần tử
                    for (int j = i; j < count - 1; j++)
                        products[j] = products[j + 1];
                    count--;
                    Console.WriteLine($"  🗑️ [Memory] Xóa: {name}");
                    return true;
                }
            }
            return false;
        }

        public int Count() => count;
    }

    // ═══════════════════════════════════════
    // IMPLEMENTATION 2: FileRepository
    // (Giả lập lưu vào file)
    // ═══════════════════════════════════════
    class FileRepository : IRepository
    {
        private Product[] products;
        private int count;
        private string filePath;

        public FileRepository(string path, int capacity = 100)
        {
            filePath = path;
            products = new Product[capacity];
            count = 0;
            Console.WriteLine($"  📁 [File] Kết nối file: {filePath}");
        }

        public void Add(Product product)
        {
            if (count < products.Length)
            {
                products[count] = product;
                count++;
                Console.WriteLine($"  📝 [File → {filePath}] Ghi: {product.Name}");
            }
        }

        public Product GetById(int id)
        {
            Console.WriteLine($"  🔍 [File] Đọc file tìm ID={id}...");
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id) return products[i];
            }
            return null;
        }

        public Product[] GetAll()
        {
            Console.WriteLine($"  📂 [File] Đọc tất cả từ {filePath}");
            Product[] result = new Product[count];
            Array.Copy(products, result, count);
            return result;
        }

        public bool Update(Product product)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == product.Id)
                {
                    products[i] = product;
                    Console.WriteLine($"  ✏️ [File → {filePath}] Ghi đè: {product.Name}");
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id)
                {
                    string name = products[i].Name;
                    for (int j = i; j < count - 1; j++)
                        products[j] = products[j + 1];
                    count--;
                    Console.WriteLine($"  🗑️ [File → {filePath}] Xóa: {name}");
                    return true;
                }
            }
            return false;
        }

        public int Count() => count;
    }

    // ═══════════════════════════════════════
    // IMPLEMENTATION 3: LoggingRepository
    // (Decorator — wrap repository khác + thêm logging)
    // ═══════════════════════════════════════
    class LoggingRepository : IRepository
    {
        private IRepository innerRepo;  // Chứa repository khác!

        public LoggingRepository(IRepository repository)
        {
            innerRepo = repository;
        }

        public void Add(Product product)
        {
            Console.WriteLine($"  📋 LOG: Add({product.Name}) called at {DateTime.Now:HH:mm:ss}");
            innerRepo.Add(product);
        }

        public Product GetById(int id)
        {
            Console.WriteLine($"  📋 LOG: GetById({id}) called");
            return innerRepo.GetById(id);
        }

        public Product[] GetAll()
        {
            Console.WriteLine($"  📋 LOG: GetAll() called");
            return innerRepo.GetAll();
        }

        public bool Update(Product product)
        {
            Console.WriteLine($"  📋 LOG: Update({product.Id}) called");
            return innerRepo.Update(product);
        }

        public bool Delete(int id)
        {
            Console.WriteLine($"  📋 LOG: Delete({id}) called");
            return innerRepo.Delete(id);
        }

        public int Count() => innerRepo.Count();
    }

    // ═══════════════════════════════════════
    // PRODUCT SERVICE — Phụ thuộc vào IRepository (ABSTRACTION!)
    // ═══════════════════════════════════════
    class ProductService
    {
        private IRepository repository;  // ← Interface, không phải class cụ thể!

        public ProductService(IRepository repo)
        {
            repository = repo;
        }

        public void AddProduct(Product product)
        {
            repository.Add(product);
        }

        public void ShowAllProducts()
        {
            Product[] all = repository.GetAll();
            Console.WriteLine($"\n  ═══ DANH SÁCH SẢN PHẨM ({repository.Count()} items) ═══");
            foreach (Product p in all)
            {
                Console.WriteLine($"  {p}");
            }
        }

        public void FindExpensiveProducts(decimal minPrice)
        {
            Product[] all = repository.GetAll();
            Console.WriteLine($"\n  ═══ SẢN PHẨM GIÁ ≥ {minPrice:N0}đ ═══");
            foreach (Product p in all)
            {
                if (p.Price >= minPrice)
                    Console.WriteLine($"  {p}");
            }
        }

        public decimal GetTotalInventoryValue()
        {
            Product[] all = repository.GetAll();
            decimal total = 0;
            foreach (Product p in all)
            {
                total += p.Price * p.Stock;
            }
            return total;
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   REPOSITORY PATTERN DEMO            ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // === Scenario 1: InMemory Repository ===
            Console.WriteLine("══ SCENARIO 1: InMemory Repository ══");
            IRepository memoryRepo = new InMemoryRepository();
            ProductService service1 = new ProductService(memoryRepo);

            service1.AddProduct(new Product(1, "iPhone 15", 25_000_000, 50));
            service1.AddProduct(new Product(2, "MacBook Air", 30_000_000, 20));
            service1.AddProduct(new Product(3, "AirPods Pro", 5_000_000, 100));
            service1.ShowAllProducts();

            // === Scenario 2: File Repository ===
            Console.WriteLine("\n══ SCENARIO 2: File Repository ══");
            IRepository fileRepo = new FileRepository("products.dat");
            ProductService service2 = new ProductService(fileRepo);

            service2.AddProduct(new Product(1, "Samsung S24", 22_000_000, 30));
            service2.AddProduct(new Product(2, "Galaxy Buds", 3_500_000, 80));
            service2.ShowAllProducts();

            // === Scenario 3: Logging + Memory (Decorator) ===
            Console.WriteLine("\n══ SCENARIO 3: Logging Repository ══");
            IRepository loggingRepo = new LoggingRepository(new InMemoryRepository());
            ProductService service3 = new ProductService(loggingRepo);

            service3.AddProduct(new Product(1, "PS5", 15_000_000, 10));
            service3.AddProduct(new Product(2, "Nintendo Switch", 8_000_000, 25));
            service3.ShowAllProducts();

            // === Tổng kết: cùng 1 ProductService, 3 repository khác nhau! ===
            Console.WriteLine("\n═══ TỔNG KẾT ═══");
            Console.WriteLine($"  💰 Kho hàng Memory: {service1.GetTotalInventoryValue():N0}đ");
            Console.WriteLine($"  💰 Kho hàng File:   {service2.GetTotalInventoryValue():N0}đ");
            Console.WriteLine($"  💰 Kho hàng Logging: {service3.GetTotalInventoryValue():N0}đ");
            Console.WriteLine("\n  🎯 ProductService KHÔNG THAY ĐỔI khi đổi repository!");
            Console.WriteLine("  → Đây là sức mạnh của Abstraction + Interface! 🚀");
        }
    }
}
```

---

## Ví dụ 5: Abstract Class nhiều tầng + Interface mở rộng 🏢

> **Mục tiêu:** Kết hợp abstract class nhiều tầng với multiple interfaces — pattern thực tế.

```csharp
using System;

namespace MultiLevelAbstractionDemo
{
    // ═══════════════════════════════════════
    // INTERFACES
    // ═══════════════════════════════════════
    interface IPrintable
    {
        void Print();
    }

    interface IExportable
    {
        string ExportToText();
        string ExportToCsv();
    }

    // ═══════════════════════════════════════
    // ABSTRACT LEVEL 1: Document (tầng cao nhất)
    // ═══════════════════════════════════════
    abstract class Document : IPrintable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PageCount { get; protected set; }

        protected Document(string title, string author)
        {
            Title = title;
            Author = author;
            CreatedDate = DateTime.Now;
            PageCount = 1;
        }

        // Abstract — mỗi loại document hiển thị khác nhau
        public abstract string GetContent();
        public abstract string GetDocumentType();

        // Concrete — chia sẻ cho tất cả
        public void ShowHeader()
        {
            Console.WriteLine($"  📄 {GetDocumentType()}: {Title}");
            Console.WriteLine($"     Tác giả: {Author}");
            Console.WriteLine($"     Ngày tạo: {CreatedDate:dd/MM/yyyy}");
            Console.WriteLine($"     Số trang: {PageCount}");
        }

        // IPrintable
        public void Print()
        {
            Console.WriteLine("  🖨️ Đang in...");
            ShowHeader();
            Console.WriteLine($"     Nội dung: {GetContent()[..Math.Min(50, GetContent().Length)]}...");
            Console.WriteLine("  ✅ In hoàn tất!");
        }
    }

    // ═══════════════════════════════════════
    // ABSTRACT LEVEL 2: Report (tầng giữa — vẫn abstract)
    // ═══════════════════════════════════════
    abstract class Report : Document, IExportable
    {
        public string Department { get; set; }
        public string Period { get; set; }

        protected Report(string title, string author, string dept, string period)
            : base(title, author)
        {
            Department = dept;
            Period = period;
        }

        // Implement 1 abstract method, để lại 1 cho class con
        public override string GetDocumentType() => "Báo cáo";

        // Thêm abstract method riêng
        public abstract decimal GetTotalAmount();

        // IExportable
        public string ExportToText()
        {
            return $"{GetDocumentType()} | {Title} | {Department} | " +
                   $"{Period} | {GetTotalAmount():N0}đ";
        }

        public abstract string ExportToCsv();  // Để class con implement
    }

    // ═══════════════════════════════════════
    // CONCRETE: SalesReport
    // ═══════════════════════════════════════
    class SalesReport : Report
    {
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
        public int OrderCount { get; set; }

        public SalesReport(string author, string dept, string period,
                          decimal revenue, decimal cost, int orders)
            : base("Báo cáo Doanh thu", author, dept, period)
        {
            Revenue = revenue;
            Cost = cost;
            OrderCount = orders;
            PageCount = 3;
        }

        public override string GetContent()
        {
            return $"Doanh thu: {Revenue:N0}đ | Chi phí: {Cost:N0}đ | " +
                   $"Lợi nhuận: {Revenue - Cost:N0}đ | Đơn hàng: {OrderCount}";
        }

        public override decimal GetTotalAmount() => Revenue;

        public override string ExportToCsv()
        {
            return $"{Title},{Department},{Period},{Revenue},{Cost},{OrderCount}";
        }
    }

    // ═══════════════════════════════════════
    // CONCRETE: ExpenseReport
    // ═══════════════════════════════════════
    class ExpenseReport : Report
    {
        public decimal TotalExpense { get; set; }
        public decimal Budget { get; set; }

        public ExpenseReport(string author, string dept, string period,
                            decimal expense, decimal budget)
            : base("Báo cáo Chi phí", author, dept, period)
        {
            TotalExpense = expense;
            Budget = budget;
            PageCount = 2;
        }

        public override string GetContent()
        {
            decimal remaining = Budget - TotalExpense;
            string status = remaining >= 0 ? "Trong ngân sách" : "VƯỢT ngân sách!";
            return $"Chi phí: {TotalExpense:N0}đ | Budget: {Budget:N0}đ | " +
                   $"Còn lại: {remaining:N0}đ | {status}";
        }

        public override decimal GetTotalAmount() => TotalExpense;

        public override string ExportToCsv()
        {
            return $"{Title},{Department},{Period},{TotalExpense},{Budget}";
        }
    }

    // ═══════════════════════════════════════
    // CONCRETE: Letter (kế thừa Document trực tiếp)
    // ═══════════════════════════════════════
    class Letter : Document
    {
        public string Recipient { get; set; }
        public string Body { get; set; }

        public Letter(string author, string recipient, string body)
            : base("Thư", author)
        {
            Recipient = recipient;
            Body = body;
        }

        public override string GetContent()
        {
            return $"Kính gửi: {Recipient}\n{Body}\nTrân trọng,\n{Author}";
        }

        public override string GetDocumentType() => "Thư";
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   MULTI-LEVEL ABSTRACTION DEMO       ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo documents
            Document[] documents = new Document[]
            {
                new SalesReport("Nguyễn A", "Sales", "T5/2026",
                    500_000_000, 300_000_000, 1500),
                new ExpenseReport("Trần B", "HR", "T5/2026",
                    120_000_000, 150_000_000),
                new Letter("Lê C", "Ban Giám đốc",
                    "Đề xuất tăng ngân sách marketing Q3/2026"),
                new SalesReport("Phạm D", "Online", "T5/2026",
                    200_000_000, 80_000_000, 3000),
            };

            // === Print tất cả — Document polymorphism ===
            Console.WriteLine("═══ IN TẤT CẢ TÀI LIỆU ═══\n");
            foreach (Document doc in documents)
            {
                doc.Print();
                Console.WriteLine();
            }

            // === Export — chỉ Report mới IExportable ===
            Console.WriteLine("═══ EXPORT BÁO CÁO ═══\n");
            Console.WriteLine("  --- TEXT ---");
            foreach (Document doc in documents)
            {
                if (doc is IExportable exportable)
                {
                    Console.WriteLine($"  {exportable.ExportToText()}");
                }
            }

            Console.WriteLine("\n  --- CSV ---");
            foreach (Document doc in documents)
            {
                if (doc is Report report)
                {
                    Console.WriteLine($"  {report.ExportToCsv()}");
                }
            }

            // === Thống kê ===
            Console.WriteLine("\n═══ THỐNG KÊ ═══");
            decimal totalRevenue = 0;
            decimal totalExpense = 0;
            int reportCount = 0;

            foreach (Document doc in documents)
            {
                if (doc is SalesReport sales)
                {
                    totalRevenue += sales.Revenue;
                    reportCount++;
                }
                else if (doc is ExpenseReport expense)
                {
                    totalExpense += expense.TotalExpense;
                    reportCount++;
                }
            }

            Console.WriteLine($"  📊 Tổng báo cáo: {reportCount}");
            Console.WriteLine($"  💰 Tổng doanh thu: {totalRevenue:N0}đ");
            Console.WriteLine($"  💸 Tổng chi phí: {totalExpense:N0}đ");
            Console.WriteLine($"  📈 Lợi nhuận ròng: {totalRevenue - totalExpense:N0}đ");

            // Hierarchy minh họa
            Console.WriteLine("\n═══ HIERARCHY ═══");
            Console.WriteLine("  Document (abstract) + IPrintable");
            Console.WriteLine("  ├── Report (abstract) + IExportable");
            Console.WriteLine("  │   ├── SalesReport (concrete)");
            Console.WriteLine("  │   └── ExpenseReport (concrete)");
            Console.WriteLine("  └── Letter (concrete)");
        }
    }
}
```

---

## 📊 Tổng kết các ví dụ

| Ví dụ | Concept chính | Highlights |
|-------|---------------|------------|
| Abstract Shape | Abstract class cơ bản | abstract method + concrete method |
| Game Interfaces | Multiple interfaces | IDamageable, IMovable, ISaveable |
| Employee System | Abstract class + Interface | IPayable, IPromotable, kết hợp |
| Repository Pattern | Interface cho abstraction | Dependency Inversion, Decorator |
| Multi-level | Nhiều tầng abstract + interface | Hierarchy 3 tầng, IExportable |

> **Tip:** Ví dụ 4 (Repository) là pattern BẠN SẼ GẶP RẤT NHIỀU trong thực tế! Hãy hiểu kỹ! 🚀
