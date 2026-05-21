# 💻 Bài 24: Abstract Class Nâng Cao — Ví dụ thực hành

> **Tất cả ví dụ đều chạy được. Hãy tạo Console App và copy-paste để thử!**

---

## Ví dụ 1: Template Method — ReportGenerator 📊

> **Mục tiêu:** Abstract class định nghĩa skeleton (bộ khung), subclasses fill in các bước cụ thể. 3 loại report cùng flow nhưng khác output.

```csharp
using System;
using System.Collections.Generic;

namespace TemplateMethodDemo
{
    // ═══════════════════════════════════════════
    // ABSTRACT CLASS: ReportGenerator
    // Template Method Pattern — skeleton + abstract steps
    // ═══════════════════════════════════════════
    abstract class ReportGenerator
    {
        // Properties chung
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime GeneratedAt { get; private set; }

        protected ReportGenerator(string title, string author)
        {
            Title = title;
            Author = author;
        }

        // ═══ TEMPLATE METHOD — Bộ khung cố định ═══
        // Subclass KHÔNG override method này!
        public void Generate()
        {
            GeneratedAt = DateTime.Now;

            Console.WriteLine($"\n🔄 Đang tạo report [{GetFormatName()}]...\n");

            PrepareHeader();        // Step 1 — abstract
            BuildBody();            // Step 2 — abstract
            AddFooter();            // Step 3 — abstract
            Finalize();             // Step 4 — concrete (hook)

            Console.WriteLine($"\n✅ Report [{GetFormatName()}] hoàn thành!");
            Console.WriteLine(new string('─', 50));
        }

        // ═══ ABSTRACT STEPS — Subclass PHẢI implement ═══
        public abstract string GetFormatName();
        protected abstract void PrepareHeader();
        protected abstract void BuildBody();
        protected abstract void AddFooter();

        // ═══ HOOK METHOD — Có thể override nhưng không bắt buộc ═══
        protected virtual void Finalize()
        {
            Console.WriteLine("  [Finalize] Không có xử lý thêm.");
        }

        // ═══ SHARED HELPER — Dùng chung ═══
        protected string GetTimestamp()
            => GeneratedAt.ToString("dd/MM/yyyy HH:mm:ss");
    }

    // ═══════════════════════════════════════════
    // HTML REPORT
    // ═══════════════════════════════════════════
    class HtmlReport : ReportGenerator
    {
        public HtmlReport(string title, string author)
            : base(title, author) { }

        public override string GetFormatName() => "HTML";

        protected override void PrepareHeader()
        {
            Console.WriteLine("  <html>");
            Console.WriteLine("  <head>");
            Console.WriteLine($"    <title>{Title}</title>");
            Console.WriteLine($"    <meta author=\"{Author}\" />");
            Console.WriteLine($"    <meta date=\"{GetTimestamp()}\" />");
            Console.WriteLine("  </head>");
        }

        protected override void BuildBody()
        {
            Console.WriteLine("  <body>");
            Console.WriteLine($"    <h1>{Title}</h1>");
            Console.WriteLine($"    <p>Tác giả: {Author}</p>");
            Console.WriteLine("    <table>");
            Console.WriteLine("      <tr><th>Mục</th><th>Giá trị</th></tr>");
            Console.WriteLine("      <tr><td>Doanh thu</td><td>100,000,000đ</td></tr>");
            Console.WriteLine("      <tr><td>Chi phí</td><td>60,000,000đ</td></tr>");
            Console.WriteLine("      <tr><td>Lợi nhuận</td><td>40,000,000đ</td></tr>");
            Console.WriteLine("    </table>");
            Console.WriteLine("  </body>");
        }

        protected override void AddFooter()
        {
            Console.WriteLine($"  <footer>© 2025 - {Author}</footer>");
            Console.WriteLine("  </html>");
        }

        // Override hook — HTML cần minify
        protected override void Finalize()
        {
            Console.WriteLine("  [Finalize] Minifying HTML...");
        }
    }

    // ═══════════════════════════════════════════
    // PDF REPORT
    // ═══════════════════════════════════════════
    class PdfReport : ReportGenerator
    {
        public string FontFamily { get; set; }

        public PdfReport(string title, string author, string font = "Arial")
            : base(title, author)
        {
            FontFamily = font;
        }

        public override string GetFormatName() => "PDF";

        protected override void PrepareHeader()
        {
            Console.WriteLine("  ┌─────────────────────────────────────────┐");
            Console.WriteLine($"  │ 📄 PDF REPORT                          │");
            Console.WriteLine($"  │ Title: {Title,-32}│");
            Console.WriteLine($"  │ Author: {Author,-31}│");
            Console.WriteLine($"  │ Font: {FontFamily,-33}│");
            Console.WriteLine($"  │ Date: {GetTimestamp(),-33}│");
            Console.WriteLine("  ├─────────────────────────────────────────┤");
        }

        protected override void BuildBody()
        {
            Console.WriteLine("  │                                         │");
            Console.WriteLine("  │  📊 BÁO CÁO KINH DOANH                │");
            Console.WriteLine("  │  ─────────────────────                  │");
            Console.WriteLine("  │  • Doanh thu:  100,000,000đ            │");
            Console.WriteLine("  │  • Chi phí:     60,000,000đ            │");
            Console.WriteLine("  │  • Lợi nhuận:   40,000,000đ            │");
            Console.WriteLine("  │                                         │");
        }

        protected override void AddFooter()
        {
            Console.WriteLine("  │                                         │");
            Console.WriteLine($"  │ Trang 1/1          © 2025 {Author,-11}│");
            Console.WriteLine("  └─────────────────────────────────────────┘");
        }

        // Override hook — PDF cần compress
        protected override void Finalize()
        {
            Console.WriteLine("  [Finalize] Compressing PDF...");
        }
    }

    // ═══════════════════════════════════════════
    // CSV REPORT
    // ═══════════════════════════════════════════
    class CsvReport : ReportGenerator
    {
        public char Delimiter { get; set; }

        public CsvReport(string title, string author, char delimiter = ',')
            : base(title, author)
        {
            Delimiter = delimiter;
        }

        public override string GetFormatName() => "CSV";

        protected override void PrepareHeader()
        {
            Console.WriteLine($"  # Report: {Title}");
            Console.WriteLine($"  # Author: {Author}");
            Console.WriteLine($"  # Date: {GetTimestamp()}");
            Console.WriteLine($"  # Delimiter: '{Delimiter}'");
            Console.WriteLine();
            Console.WriteLine($"  Mục{Delimiter}Giá trị{Delimiter}Đơn vị");
        }

        protected override void BuildBody()
        {
            Console.WriteLine($"  Doanh thu{Delimiter}100000000{Delimiter}VND");
            Console.WriteLine($"  Chi phí{Delimiter}60000000{Delimiter}VND");
            Console.WriteLine($"  Lợi nhuận{Delimiter}40000000{Delimiter}VND");
        }

        protected override void AddFooter()
        {
            Console.WriteLine();
            Console.WriteLine($"  # End of report");
        }

        // Không override Finalize() → dùng default
    }

    // ═══════════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║  TEMPLATE METHOD — REPORT GENERATOR          ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            // Tạo mảng polymorphic — cùng kiểu ReportGenerator
            ReportGenerator[] reports = new ReportGenerator[]
            {
                new HtmlReport("Báo cáo Q1", "Nguyễn Văn A"),
                new PdfReport("Báo cáo Q1", "Nguyễn Văn A", "Times New Roman"),
                new CsvReport("Báo cáo Q1", "Nguyễn Văn A", ';')
            };

            // Cùng gọi Generate() — Template Method đảm bảo flow giống nhau
            foreach (ReportGenerator report in reports)
            {
                report.Generate();
            }

            // Thống kê
            Console.WriteLine("\n═══ THỐNG KÊ ═══");
            foreach (ReportGenerator report in reports)
            {
                Console.WriteLine($"  📄 [{report.GetFormatName()}] " +
                                $"by {report.Author} at {report.GeneratedAt:HH:mm:ss}");
            }
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════════════╗
║  TEMPLATE METHOD — REPORT GENERATOR          ║
╚══════════════════════════════════════════════╝

🔄 Đang tạo report [HTML]...

  <html>
  <head>
    <title>Báo cáo Q1</title>
  ...
  [Finalize] Minifying HTML...

✅ Report [HTML] hoàn thành!
──────────────────────────────────────────────────
...
```

---

## Ví dụ 2: Abstract Class + Interface — Game Characters 🎮

> **Mục tiêu:** Abstract class implements multiple interfaces. Warrior & Mage kế thừa abstract class, mỗi loại có cách attack/defend khác nhau.

```csharp
using System;

namespace AbstractPlusInterfaceDemo
{
    // ═══════════════════════════════════════════
    // INTERFACES — Định nghĩa capabilities
    // ═══════════════════════════════════════════
    interface IAttackable
    {
        int AttackPower { get; }
        string AttackType { get; }
        void Attack(GameCharacter target);
    }

    interface IDefendable
    {
        int DefenseRating { get; }
        int Defend(int incomingDamage);
    }

    interface IHealable
    {
        int HealPower { get; }
        void HealSelf();
    }

    // ═══════════════════════════════════════════
    // ABSTRACT CLASS — Implements IAttackable + IDefendable
    // Shared code + forced implementation
    // ═══════════════════════════════════════════
    abstract class GameCharacter : IAttackable, IDefendable
    {
        // ═══ SHARED STATE ═══
        public string Name { get; set; }
        public int Hp { get; protected set; }
        public int MaxHp { get; protected set; }
        public int Level { get; set; }
        public bool IsAlive => Hp > 0;

        // Constructor — shared initialization
        protected GameCharacter(string name, int maxHp, int level)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = maxHp;
            Level = level;
        }

        // ═══ ABSTRACT MEMBERS — Subclass PHẢI implement ═══

        // Abstract properties (từ interfaces)
        public abstract int AttackPower { get; }
        public abstract string AttackType { get; }
        public abstract int DefenseRating { get; }

        // Abstract methods
        public abstract void Attack(GameCharacter target);
        public abstract string GetClassName();

        // ═══ CONCRETE — Shared logic ═══

        // Defend có default logic — subclass CÓ THỂ override
        public virtual int Defend(int incomingDamage)
        {
            int actualDamage = Math.Max(1, incomingDamage - DefenseRating);
            Hp = Math.Max(0, Hp - actualDamage);
            return actualDamage;
        }

        // Shared methods
        public void TakeDamage(int damage)
        {
            int actual = Defend(damage);
            Console.WriteLine($"  💥 {Name} nhận {actual} damage " +
                            $"(blocked {damage - actual}) → HP: {Hp}/{MaxHp}");

            if (!IsAlive)
                Console.WriteLine($"  ☠️ {Name} đã bị hạ gục!");
        }

        public void ShowStatus()
        {
            string hpBar = BuildHpBar();
            Console.WriteLine($"  [{GetClassName()}] {Name} Lv.{Level}");
            Console.WriteLine($"  HP: {hpBar} {Hp}/{MaxHp}");
            Console.WriteLine($"  ⚔️ ATK: {AttackPower} ({AttackType})  🛡️ DEF: {DefenseRating}");
        }

        // Helper
        private string BuildHpBar()
        {
            int filled = (int)((double)Hp / MaxHp * 20);
            int empty = 20 - filled;
            return "[" + new string('█', filled) + new string('░', empty) + "]";
        }
    }

    // ═══════════════════════════════════════════
    // WARRIOR — Tanky, physical damage
    // ═══════════════════════════════════════════
    class Warrior : GameCharacter
    {
        public int Rage { get; private set; }

        public Warrior(string name, int level)
            : base(name, 150 + level * 20, level)
        {
            Rage = 0;
        }

        // Override abstract properties
        public override int AttackPower => 20 + Level * 5;
        public override string AttackType => "Physical";
        public override int DefenseRating => 15 + Level * 3;
        public override string GetClassName() => "⚔️ Warrior";

        // Override abstract method
        public override void Attack(GameCharacter target)
        {
            Rage += 10;
            int damage = AttackPower;

            // Rage bonus: cứ 30 Rage → x2 damage
            if (Rage >= 30)
            {
                damage *= 2;
                Rage = 0;
                Console.WriteLine($"  🔥 {Name} dùng FURY STRIKE! (x2 damage)");
            }
            else
            {
                Console.WriteLine($"  ⚔️ {Name} tấn công bằng kiếm! (Rage: {Rage})");
            }

            target.TakeDamage(damage);
        }

        // Override Defend — Warrior block thêm
        public override int Defend(int incomingDamage)
        {
            Rage += 5;  // Nhận damage tăng Rage
            return base.Defend(incomingDamage);  // Dùng logic cha
        }
    }

    // ═══════════════════════════════════════════
    // MAGE — Squishy, magic damage, can heal
    // ═══════════════════════════════════════════
    class Mage : GameCharacter, IHealable
    {
        public int Mana { get; private set; }
        public int MaxMana { get; private set; }

        public Mage(string name, int level)
            : base(name, 80 + level * 10, level)
        {
            MaxMana = 100 + level * 15;
            Mana = MaxMana;
        }

        // Override abstract properties
        public override int AttackPower => 30 + Level * 8;
        public override string AttackType => "Magic";
        public override int DefenseRating => 5 + Level * 1;
        public override string GetClassName() => "🔮 Mage";

        // IHealable
        public int HealPower => 15 + Level * 5;

        // Override abstract method
        public override void Attack(GameCharacter target)
        {
            if (Mana >= 20)
            {
                Mana -= 20;
                Console.WriteLine($"  🔥 {Name} phóng Fireball! (Mana: {Mana}/{MaxMana})");
                target.TakeDamage(AttackPower);
            }
            else
            {
                Console.WriteLine($"  🪄 {Name} dùng Staff Strike! (hết mana)");
                target.TakeDamage(AttackPower / 3);
            }
        }

        // IHealable implementation
        public void HealSelf()
        {
            if (Mana >= 30)
            {
                Mana -= 30;
                int healed = Math.Min(HealPower, MaxHp - Hp);
                Hp += healed;
                Console.WriteLine($"  💚 {Name} tự hồi {healed} HP! " +
                                $"(HP: {Hp}/{MaxHp}, Mana: {Mana}/{MaxMana})");
            }
            else
            {
                Console.WriteLine($"  ❌ {Name} không đủ mana để hồi!");
            }
        }

        public void ShowMana()
        {
            Console.WriteLine($"  🔮 Mana: {Mana}/{MaxMana}");
        }
    }

    // ═══════════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║  ABSTRACT CLASS + INTERFACE — GAME COMBAT    ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝\n");

            Warrior warrior = new Warrior("Arthas", 5);
            Mage mage = new Mage("Jaina", 5);

            // Show initial status
            Console.WriteLine("═══ TRẠNG THÁI BAN ĐẦU ═══");
            warrior.ShowStatus();
            Console.WriteLine();
            mage.ShowStatus();

            // === COMBAT ===
            Console.WriteLine("\n═══ CHIẾN ĐẤU ═══\n");

            // Round 1-3: Exchange attacks
            for (int round = 1; round <= 3; round++)
            {
                Console.WriteLine($"── Round {round} ──");
                warrior.Attack(mage);
                mage.Attack(warrior);

                // Mage heal nếu HP thấp
                if (mage.IsAlive && mage.Hp < mage.MaxHp / 2)
                {
                    mage.HealSelf();
                }
                Console.WriteLine();
            }

            // === Interface Polymorphism ===
            Console.WriteLine("═══ INTERFACE CHECK ═══");

            GameCharacter[] characters = { warrior, mage };
            foreach (GameCharacter ch in characters)
            {
                Console.Write($"  {ch.Name}: ");

                // Kiểm tra interfaces
                if (ch is IAttackable atk)
                    Console.Write($"ATK={atk.AttackPower} ");
                if (ch is IDefendable def)
                    Console.Write($"DEF={def.DefenseRating} ");
                if (ch is IHealable healer)
                {
                    Console.Write($"HEAL={healer.HealPower} ");
                    healer.HealSelf();
                }
                Console.WriteLine();
            }

            // Final status
            Console.WriteLine("\n═══ KẾT QUẢ ═══");
            foreach (GameCharacter ch in characters)
            {
                ch.ShowStatus();
                Console.WriteLine();
            }
        }
    }
}
```

**Output mong đợi:**
```
═══ TRẠNG THÁI BAN ĐẦU ═══
  [⚔️ Warrior] Arthas Lv.5
  HP: [████████████████████] 250/250
  ⚔️ ATK: 45 (Physical)  🛡️ DEF: 30

  [🔮 Mage] Jaina Lv.5
  HP: [████████████████████] 130/130
  ⚔️ ATK: 70 (Magic)  🛡️ DEF: 10

═══ CHIẾN ĐẤU ═══
── Round 1 ──
  ⚔️ Arthas tấn công bằng kiếm! (Rage: 10)
  💥 Jaina nhận 35 damage (blocked 10) → HP: 95/130
  🔥 Jaina phóng Fireball! (Mana: 155/175)
  💥 Arthas nhận 40 damage (blocked 30) → HP: 210/250
...
```

---

## Ví dụ 3: Abstract Properties — Employee Salary System 💼

> **Mục tiêu:** Abstract properties + abstract method CalculateSalary(). 3 loại nhân viên khác nhau: FullTime, PartTime, Freelancer.

```csharp
using System;

namespace AbstractPropertyDemo
{
    // ═══════════════════════════════════════════
    // ABSTRACT CLASS: Employee
    // Abstract properties + Abstract methods
    // ═══════════════════════════════════════════
    abstract class Employee
    {
        // ═══ CONCRETE properties ═══
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }

        // ═══ ABSTRACT PROPERTIES — Subclass PHẢI define ═══
        public abstract string EmployeeType { get; }
        public abstract int MaxLeaveDays { get; }
        public abstract string PayFrequency { get; }

        // ═══ ABSTRACT METHOD ═══
        public abstract decimal CalculateSalary();

        // Constructor
        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
            HireDate = DateTime.Now;
        }

        // ═══ CONCRETE — Template Method dùng abstract members ═══
        public void PrintPaySlip()
        {
            decimal salary = CalculateSalary();
            decimal tax = CalculateTax(salary);
            decimal net = salary - tax;

            Console.WriteLine("┌──────────────────────────────────────────┐");
            Console.WriteLine($"│ 📋 PHIẾU LƯƠNG                          │");
            Console.WriteLine("├──────────────────────────────────────────┤");
            Console.WriteLine($"│ ID:         {Id,-28}│");
            Console.WriteLine($"│ Tên:        {Name,-28}│");
            Console.WriteLine($"│ Loại:       {EmployeeType,-28}│");
            Console.WriteLine($"│ Chu kỳ:     {PayFrequency,-28}│");
            Console.WriteLine($"│ Ngày phép:  {MaxLeaveDays,-28}│");
            Console.WriteLine("├──────────────────────────────────────────┤");
            Console.WriteLine($"│ Lương gross: {salary,14:N0}đ            │");
            Console.WriteLine($"│ Thuế ({GetTaxRate() * 100:F0}%):   {tax,14:N0}đ            │");
            Console.WriteLine($"│ Lương net:   {net,14:N0}đ            │");
            Console.WriteLine("└──────────────────────────────────────────┘");
        }

        // Concrete helper — tính thuế
        protected virtual decimal GetTaxRate() => 0.1m;

        private decimal CalculateTax(decimal gross)
        {
            return gross * GetTaxRate();
        }

        // Override ToString
        public override string ToString()
            => $"[{EmployeeType}] {Name} — {CalculateSalary():N0}đ/{PayFrequency}";
    }

    // ═══════════════════════════════════════════
    // FULL-TIME EMPLOYEE
    // ═══════════════════════════════════════════
    class FullTimeEmployee : Employee
    {
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }

        // Override abstract properties
        public override string EmployeeType => "Chính thức";
        public override int MaxLeaveDays => 12;
        public override string PayFrequency => "Hàng tháng";

        public FullTimeEmployee(string id, string name, decimal baseSalary)
            : base(id, name)
        {
            BaseSalary = baseSalary;
            Allowance = 2_000_000;
            Bonus = 0;
        }

        public override decimal CalculateSalary()
        {
            return BaseSalary + Allowance + Bonus;
        }

        // Full-time có thuế suất cao hơn
        protected override decimal GetTaxRate() => 0.1m;
    }

    // ═══════════════════════════════════════════
    // PART-TIME EMPLOYEE
    // ═══════════════════════════════════════════
    class PartTimeEmployee : Employee
    {
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }

        // Override abstract properties
        public override string EmployeeType => "Bán thời gian";
        public override int MaxLeaveDays => 3;
        public override string PayFrequency => "Hai tuần";

        public PartTimeEmployee(string id, string name,
                               int hours, decimal hourlyRate)
            : base(id, name)
        {
            HoursWorked = hours;
            HourlyRate = hourlyRate;
        }

        public override decimal CalculateSalary()
        {
            decimal regular = Math.Min(HoursWorked, 80) * HourlyRate;
            decimal overtime = HoursWorked > 80
                ? (HoursWorked - 80) * HourlyRate * 1.5m
                : 0;
            return regular + overtime;
        }

        // Part-time thuế thấp hơn
        protected override decimal GetTaxRate() => 0.05m;
    }

    // ═══════════════════════════════════════════
    // FREELANCER
    // ═══════════════════════════════════════════
    class Freelancer : Employee
    {
        public string ProjectName { get; set; }
        public decimal ProjectFee { get; set; }
        public int CompletionPercent { get; set; }

        // Override abstract properties
        public override string EmployeeType => "Freelancer";
        public override int MaxLeaveDays => 0;     // Không có ngày phép
        public override string PayFrequency => "Theo dự án";

        public Freelancer(string id, string name,
                         string project, decimal fee)
            : base(id, name)
        {
            ProjectName = project;
            ProjectFee = fee;
            CompletionPercent = 0;
        }

        public override decimal CalculateSalary()
        {
            // Trả theo % hoàn thành
            return ProjectFee * CompletionPercent / 100m;
        }

        // Freelancer đóng thuế TNCN cao hơn
        protected override decimal GetTaxRate() => 0.15m;
    }

    // ═══════════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║  ABSTRACT PROPERTIES — EMPLOYEE SALARY       ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝\n");

            // Tạo nhân viên
            FullTimeEmployee ft = new FullTimeEmployee(
                "FT001", "Nguyễn Văn A", 20_000_000);
            ft.Bonus = 5_000_000;

            PartTimeEmployee pt = new PartTimeEmployee(
                "PT001", "Trần Thị B", 90, 100_000);

            Freelancer fl = new Freelancer(
                "FL001", "Lê Văn C", "Website Redesign", 50_000_000);
            fl.CompletionPercent = 60;

            // Mảng polymorphic — tất cả là Employee
            Employee[] employees = { ft, pt, fl };

            // In phiếu lương
            Console.WriteLine("═══ PHIẾU LƯƠNG ═══\n");
            foreach (Employee emp in employees)
            {
                emp.PrintPaySlip();  // Template method dùng abstract properties
                Console.WriteLine();
            }

            // Thống kê — dùng abstract properties
            Console.WriteLine("═══ THỐNG KÊ ═══");
            decimal totalPayroll = 0;
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"  {emp}");
                totalPayroll += emp.CalculateSalary();
            }

            Console.WriteLine($"\n  💰 Tổng quỹ lương: {totalPayroll:N0}đ");

            // So sánh ngày phép
            Console.WriteLine("\n═══ NGÀY PHÉP ═══");
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"  {emp.Name} ({emp.EmployeeType}): " +
                                $"{emp.MaxLeaveDays} ngày/năm");
            }
        }
    }
}
```

**Output mong đợi:**
```
═══ PHIẾU LƯƠNG ═══

┌──────────────────────────────────────────┐
│ 📋 PHIẾU LƯƠNG                          │
├──────────────────────────────────────────┤
│ ID:         FT001                        │
│ Tên:        Nguyễn Văn A                 │
│ Loại:       Chính thức                   │
│ Chu kỳ:     Hàng tháng                   │
│ Ngày phép:  12                           │
├──────────────────────────────────────────┤
│ Lương gross:    27,000,000đ              │
│ Thuế (10%):      2,700,000đ              │
│ Lương net:      24,300,000đ              │
└──────────────────────────────────────────┘
...
```

---

## Ví dụ 4: Multi-level Abstraction — Data Pipeline 🔄

> **Mục tiêu:** abstract DataProcessor → abstract FileProcessor → CSVProcessor, JSONProcessor. Minh họa abstract class nhiều tầng + sealed override.

```csharp
using System;

namespace MultiLevelAbstractionDemo
{
    // ═══════════════════════════════════════════
    // LEVEL 0: abstract DataProcessor (Gốc)
    // Định nghĩa pipeline cơ bản
    // ═══════════════════════════════════════════
    abstract class DataProcessor
    {
        public string ProcessorName { get; set; }
        public int RecordsProcessed { get; protected set; }
        public DateTime StartTime { get; private set; }

        protected DataProcessor(string name)
        {
            ProcessorName = name;
            RecordsProcessed = 0;
        }

        // Template method — pipeline cố định
        public void Process()
        {
            StartTime = DateTime.Now;
            Console.WriteLine($"\n🔄 [{ProcessorName}] Bắt đầu xử lý...");
            Console.WriteLine(new string('─', 45));

            string rawData = ExtractData();     // Abstract
            string[] records = ParseData(rawData); // Abstract
            TransformData(records);              // Abstract
            LoadData();                          // Abstract

            Console.WriteLine(new string('─', 45));
            Console.WriteLine($"✅ [{ProcessorName}] Hoàn thành! " +
                            $"({RecordsProcessed} records)");
        }

        // Abstract — mỗi processor khác nhau
        protected abstract string ExtractData();
        protected abstract string[] ParseData(string rawData);
        protected abstract void TransformData(string[] records);
        protected abstract void LoadData();

        // Concrete helper
        protected void Log(string step, string message)
        {
            Console.WriteLine($"  [{step}] {message}");
        }
    }

    // ═══════════════════════════════════════════
    // LEVEL 1: abstract FileProcessor
    // Implement ExtractData() + thêm abstract mới
    // ═══════════════════════════════════════════
    abstract class FileProcessor : DataProcessor
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }

        protected FileProcessor(string name, string filePath, string extension)
            : base(name)
        {
            FilePath = filePath;
            FileExtension = extension;
        }

        // ═══ SEALED OVERRIDE — File processors luôn extract giống nhau ═══
        // Subclass của FileProcessor KHÔNG ĐƯỢC override ExtractData
        protected sealed override string ExtractData()
        {
            Log("Extract", $"Đọc file: {FilePath}");
            Log("Extract", $"Format: {FileExtension}");

            // Simulate đọc file
            string simulatedData = GetSimulatedFileContent();
            Log("Extract", $"Đọc được {simulatedData.Length} bytes");

            return simulatedData;
        }

        // LoadData cũng sealed — luôn ghi ra console
        protected sealed override void LoadData()
        {
            Log("Load", $"Ghi {RecordsProcessed} records vào database");
            Log("Load", "✅ Lưu thành công!");
        }

        // Abstract mới — FileProcessor con phải cung cấp sample data
        protected abstract string GetSimulatedFileContent();

        // ParseData và TransformData vẫn abstract — delegate xuống con
    }

    // ═══════════════════════════════════════════
    // LEVEL 2: CSVProcessor (Concrete)
    // ═══════════════════════════════════════════
    class CsvProcessor : FileProcessor
    {
        public char Delimiter { get; set; }

        public CsvProcessor(string filePath, char delimiter = ',')
            : base("CSV Processor", filePath, ".csv")
        {
            Delimiter = delimiter;
        }

        protected override string GetSimulatedFileContent()
        {
            return "Name,Age,City\nAn,25,HCM\nBình,30,HN\nCúc,22,DN";
        }

        // Parse CSV
        protected override string[] ParseData(string rawData)
        {
            string[] lines = rawData.Split('\n');
            Log("Parse", $"CSV: {lines.Length} dòng (delimiter: '{Delimiter}')");

            // Bỏ header
            string[] records = new string[lines.Length - 1];
            for (int i = 1; i < lines.Length; i++)
            {
                records[i - 1] = lines[i];
                Log("Parse", $"  Row {i}: {lines[i]}");
            }

            RecordsProcessed = records.Length;
            return records;
        }

        // Transform CSV records
        protected override void TransformData(string[] records)
        {
            Log("Transform", "Chuẩn hóa dữ liệu CSV:");
            foreach (string record in records)
            {
                string[] fields = record.Split(Delimiter);
                if (fields.Length >= 3)
                {
                    Log("Transform", $"  {fields[0].Trim()} " +
                                    $"(tuổi {fields[1].Trim()}, " +
                                    $"TP {fields[2].Trim()})");
                }
            }
        }
    }

    // ═══════════════════════════════════════════
    // LEVEL 2: JSONProcessor (Concrete)
    // ═══════════════════════════════════════════
    class JsonProcessor : FileProcessor
    {
        public JsonProcessor(string filePath)
            : base("JSON Processor", filePath, ".json") { }

        protected override string GetSimulatedFileContent()
        {
            return "[{\"name\":\"An\",\"age\":25},{\"name\":\"Bình\",\"age\":30}]";
        }

        // Parse JSON (simplified)
        protected override string[] ParseData(string rawData)
        {
            Log("Parse", "JSON: Phân tích cấu trúc...");

            // Simplified parsing — đếm objects
            int count = 0;
            foreach (char c in rawData)
            {
                if (c == '{') count++;
            }

            string[] records = new string[count];
            for (int i = 0; i < count; i++)
            {
                records[i] = $"Object_{i + 1}";
                Log("Parse", $"  Object {i + 1} parsed");
            }

            RecordsProcessed = count;
            return records;
        }

        // Transform JSON records
        protected override void TransformData(string[] records)
        {
            Log("Transform", "Chuyển đổi JSON objects:");
            foreach (string record in records)
            {
                Log("Transform", $"  {record} → validated ✅");
            }
        }
    }

    // ═══════════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║  MULTI-LEVEL ABSTRACTION — DATA PIPELINE     ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            /*
                Hierarchy:
                  abstract DataProcessor        Level 0
                      │
                  abstract FileProcessor        Level 1
                      ├── CsvProcessor          Level 2 (concrete)
                      └── JsonProcessor         Level 2 (concrete)

                sealed override ở FileProcessor:
                  ExtractData() → con KHÔNG override được
                  LoadData()    → con KHÔNG override được
            */

            DataProcessor[] processors = new DataProcessor[]
            {
                new CsvProcessor("data/employees.csv"),
                new JsonProcessor("data/config.json"),
                new CsvProcessor("data/products.csv", ';')
            };

            // Process tất cả — cùng 1 pipeline, khác cách xử lý
            foreach (DataProcessor processor in processors)
            {
                processor.Process();
            }

            // Summary
            Console.WriteLine("\n═══ TỔNG KẾT ═══");
            int totalRecords = 0;
            foreach (DataProcessor p in processors)
            {
                Console.WriteLine($"  📄 {p.ProcessorName}: {p.RecordsProcessed} records");
                totalRecords += p.RecordsProcessed;
            }
            Console.WriteLine($"\n  📊 Tổng: {totalRecords} records đã xử lý");
        }
    }
}
```

**Output mong đợi:**
```
🔄 [CSV Processor] Bắt đầu xử lý...
─────────────────────────────────────────────
  [Extract] Đọc file: data/employees.csv
  [Extract] Format: .csv
  [Extract] Đọc được 43 bytes
  [Parse] CSV: 4 dòng (delimiter: ',')
  [Parse]   Row 1: An,25,HCM
  ...
  [Transform] Chuẩn hóa dữ liệu CSV:
  [Transform]   An (tuổi 25, TP HCM)
  ...
─────────────────────────────────────────────
✅ [CSV Processor] Hoàn thành! (3 records)
```

---

## Ví dụ 5: Phase 2 Tổng hợp — E-commerce System 🛒

> **Mục tiêu:** Kết hợp TẤT CẢ 4 trụ cột OOP — Encapsulation + Inheritance + Polymorphism + Abstraction. Abstract Product, interfaces IShippable/ITaxable/IDiscountable, concrete Electronics/Clothing, polymorphic Cart.

```csharp
using System;

namespace EcommerceSystemDemo
{
    // ═══════════════════════════════════════════
    // INTERFACES — Capabilities (CAN-DO)
    // ═══════════════════════════════════════════

    interface IShippable
    {
        double WeightKg { get; }
        string GetShippingMethod();
        decimal CalculateShippingCost();
    }

    interface ITaxable
    {
        decimal TaxRate { get; }
        decimal CalculateTax();
    }

    interface IDiscountable
    {
        int DiscountPercent { get; set; }
        decimal GetDiscountAmount();
    }

    // ═══════════════════════════════════════════
    // ABSTRACT CLASS: Product (IS-A)
    // Encapsulation + Abstraction
    // ═══════════════════════════════════════════
    abstract class Product : ITaxable
    {
        // ═══ ENCAPSULATION — private backing fields ═══
        private static int nextId = 1;
        private decimal price;

        // Properties
        public int Id { get; private set; }
        public string Name { get; set; }

        public decimal Price
        {
            get => price;
            set => price = value >= 0 ? value : throw new ArgumentException("Giá không hợp lệ!");
        }

        public int Stock { get; private set; }

        // ═══ ABSTRACT PROPERTIES ═══
        public abstract string Category { get; }
        public abstract decimal TaxRate { get; }

        // Constructor
        protected Product(string name, decimal price, int stock)
        {
            Id = nextId++;
            Name = name;
            Price = price;
            Stock = stock;
        }

        // ═══ ABSTRACT METHOD ═══
        public abstract string GetProductInfo();

        // ═══ CONCRETE — Shared logic ═══

        // ITaxable
        public decimal CalculateTax() => Price * TaxRate;

        // Encapsulation — controlled stock change
        public bool Purchase(int quantity)
        {
            if (quantity <= 0 || quantity > Stock)
                return false;

            Stock -= quantity;
            return true;
        }

        public void Restock(int quantity)
        {
            if (quantity > 0) Stock += quantity;
        }

        // Template method for display
        public void Display()
        {
            Console.WriteLine($"  [{Id}] {Name}");
            Console.WriteLine($"      Loại: {Category}");
            Console.WriteLine($"      Giá: {Price:N0}đ + Tax ({TaxRate * 100:F0}%): " +
                            $"{CalculateTax():N0}đ = {Price + CalculateTax():N0}đ");
            Console.WriteLine($"      Kho: {Stock} sản phẩm");
            Console.WriteLine($"      {GetProductInfo()}");
        }

        public override string ToString()
            => $"[{Category}] {Name} — {Price:N0}đ";
    }

    // ═══════════════════════════════════════════
    // ELECTRONICS — IShippable + IDiscountable
    // ═══════════════════════════════════════════
    class Electronics : Product, IShippable, IDiscountable
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }
        public double WeightKg { get; set; }
        public int DiscountPercent { get; set; }

        // Override abstract properties
        public override string Category => "Điện tử";
        public override decimal TaxRate => 0.10m;   // 10% VAT

        public Electronics(string name, decimal price, int stock,
                          string brand, int warranty, double weight)
            : base(name, price, stock)
        {
            Brand = brand;
            WarrantyMonths = warranty;
            WeightKg = weight;
            DiscountPercent = 0;
        }

        // Override abstract method
        public override string GetProductInfo()
            => $"🔌 {Brand} | Bảo hành: {WarrantyMonths} tháng | {WeightKg}kg";

        // IShippable
        public string GetShippingMethod()
        {
            if (WeightKg > 30) return "🚛 Vận chuyển hàng nặng";
            if (WeightKg > 5) return "📦 Chuyển phát nhanh";
            return "🏍️ Giao hàng tiết kiệm";
        }

        public decimal CalculateShippingCost()
        {
            decimal baseCost = 30_000;
            return baseCost + (decimal)(WeightKg * 5_000);
        }

        // IDiscountable
        public decimal GetDiscountAmount()
            => Price * DiscountPercent / 100m;
    }

    // ═══════════════════════════════════════════
    // CLOTHING — IShippable + IDiscountable
    // ═══════════════════════════════════════════
    class Clothing : Product, IShippable, IDiscountable
    {
        public string Size { get; set; }
        public string Material { get; set; }
        public double WeightKg { get; set; }
        public int DiscountPercent { get; set; }

        // Override abstract properties
        public override string Category => "Thời trang";
        public override decimal TaxRate => 0.05m;   // 5% VAT

        public Clothing(string name, decimal price, int stock,
                       string size, string material)
            : base(name, price, stock)
        {
            Size = size;
            Material = material;
            WeightKg = 0.5;
            DiscountPercent = 0;
        }

        public override string GetProductInfo()
            => $"👕 Size: {Size} | Chất liệu: {Material}";

        // IShippable
        public string GetShippingMethod() => "🏍️ Giao hàng tiết kiệm";
        public decimal CalculateShippingCost() => 20_000;

        // IDiscountable
        public decimal GetDiscountAmount()
            => Price * DiscountPercent / 100m;
    }

    // ═══════════════════════════════════════════
    // BOOK — Chỉ ITaxable (từ Product) — miễn ship
    // ═══════════════════════════════════════════
    class Book : Product
    {
        public string Author { get; set; }
        public int Pages { get; set; }

        public override string Category => "Sách";
        public override decimal TaxRate => 0.0m;   // Sách miễn thuế!

        public Book(string name, decimal price, int stock,
                   string author, int pages)
            : base(name, price, stock)
        {
            Author = author;
            Pages = pages;
        }

        public override string GetProductInfo()
            => $"📚 Tác giả: {Author} | {Pages} trang";
    }

    // ═══════════════════════════════════════════
    // SHOPPING CART — Polymorphism Hub
    // ═══════════════════════════════════════════
    class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetSubtotal()
        {
            decimal basePrice = Product.Price * Quantity;
            decimal tax = Product.CalculateTax() * Quantity;

            // Nếu có discount
            decimal discount = 0;
            if (Product is IDiscountable discountable)
                discount = discountable.GetDiscountAmount() * Quantity;

            return basePrice + tax - discount;
        }
    }

    class ShoppingCart
    {
        private CartItem[] items;
        private int count;

        public ShoppingCart()
        {
            items = new CartItem[20];
            count = 0;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product.Purchase(quantity))
            {
                items[count] = new CartItem(product, quantity);
                count++;
                Console.WriteLine($"  ✅ Thêm {quantity}x {product.Name} vào giỏ");
            }
            else
            {
                Console.WriteLine($"  ❌ Không đủ hàng: {product.Name}");
            }
        }

        public void ShowCart()
        {
            Console.WriteLine("\n┌═══════════════════════════════════════════════════┐");
            Console.WriteLine("│               🛒 GIỎ HÀNG                        │");
            Console.WriteLine("├───────────────────────────────────────────────────┤");

            decimal total = 0;
            decimal totalShipping = 0;
            decimal totalDiscount = 0;

            for (int i = 0; i < count; i++)
            {
                CartItem item = items[i];
                decimal subtotal = item.GetSubtotal();
                total += subtotal;

                Console.WriteLine($"│ {i + 1}. {item.Product.Name}");
                Console.WriteLine($"│    {item.Quantity}x {item.Product.Price:N0}đ " +
                                $"= {item.Product.Price * item.Quantity:N0}đ");

                // Tax info
                if (item.Product.TaxRate > 0)
                    Console.WriteLine($"│    + Tax {item.Product.TaxRate * 100:F0}%: " +
                                    $"{item.Product.CalculateTax() * item.Quantity:N0}đ");

                // Discount info (polymorphism via interface)
                if (item.Product is IDiscountable disc && disc.DiscountPercent > 0)
                {
                    decimal discAmt = disc.GetDiscountAmount() * item.Quantity;
                    totalDiscount += discAmt;
                    Console.WriteLine($"│    - Giảm {disc.DiscountPercent}%: " +
                                    $"-{discAmt:N0}đ");
                }

                // Shipping info (polymorphism via interface)
                if (item.Product is IShippable ship)
                {
                    decimal shipCost = ship.CalculateShippingCost();
                    totalShipping += shipCost;
                    Console.WriteLine($"│    🚚 {ship.GetShippingMethod()}: " +
                                    $"{shipCost:N0}đ ({ship.WeightKg}kg)");
                }

                Console.WriteLine($"│    → Subtotal: {subtotal:N0}đ");
                Console.WriteLine("│");
            }

            decimal grandTotal = total + totalShipping;

            Console.WriteLine("├───────────────────────────────────────────────────┤");
            Console.WriteLine($"│  Tạm tính:        {total,15:N0}đ               │");
            if (totalDiscount > 0)
                Console.WriteLine($"│  Đã giảm:        -{totalDiscount,14:N0}đ               │");
            Console.WriteLine($"│  Phí ship:         {totalShipping,14:N0}đ               │");
            Console.WriteLine($"│  ─────────────────────────────────                │");
            Console.WriteLine($"│  💰 TỔNG:         {grandTotal,15:N0}đ               │");
            Console.WriteLine("└═══════════════════════════════════════════════════┘");
        }
    }

    // ═══════════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  PHASE 2 TỔNG HỢP — E-COMMERCE SYSTEM               ║");
            Console.WriteLine("║  Encapsulation + Inheritance + Polymorphism +         ║");
            Console.WriteLine("║  Abstraction (Abstract Class + Interface)             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝\n");

            // === Tạo sản phẩm (Inheritance + Abstract properties) ===
            Electronics laptop = new Electronics(
                "Laptop Gaming ASUS", 25_000_000, 10,
                "ASUS", 24, 2.5);
            laptop.DiscountPercent = 10;    // 10% off

            Electronics phone = new Electronics(
                "iPhone 15", 30_000_000, 5,
                "Apple", 12, 0.2);

            Clothing shirt = new Clothing(
                "Áo Polo Premium", 500_000, 50,
                "L", "Cotton");
            shirt.DiscountPercent = 20;     // 20% off

            Clothing jeans = new Clothing(
                "Quần Jeans Slim", 800_000, 30,
                "M", "Denim");

            Book book = new Book(
                "Clean Code", 350_000, 100,
                "Robert C. Martin", 464);

            // === Catalog — Polymorphism (abstract Product) ===
            Product[] catalog = { laptop, phone, shirt, jeans, book };

            Console.WriteLine("═══ DANH MỤC SẢN PHẨM ═══\n");
            foreach (Product product in catalog)
            {
                product.Display();  // Polymorphism — mỗi loại display khác
                Console.WriteLine();
            }

            // === Interface-based operations ===
            Console.WriteLine("═══ SẢN PHẨM CÓ THỂ GIAO HÀNG ═══");
            foreach (Product p in catalog)
            {
                if (p is IShippable ship)
                {
                    Console.WriteLine($"  📦 {p.Name}: {ship.GetShippingMethod()} " +
                                    $"({ship.CalculateShippingCost():N0}đ)");
                }
            }

            Console.WriteLine("\n═══ SẢN PHẨM ĐANG GIẢM GIÁ ═══");
            foreach (Product p in catalog)
            {
                if (p is IDiscountable disc && disc.DiscountPercent > 0)
                {
                    Console.WriteLine($"  🏷️ {p.Name}: -{disc.DiscountPercent}% " +
                                    $"(giảm {disc.GetDiscountAmount():N0}đ)");
                }
            }

            // === Shopping Cart ===
            Console.WriteLine("\n═══ ĐẶT HÀNG ═══");
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(laptop, 1);
            cart.AddItem(shirt, 2);
            cart.AddItem(book, 1);
            cart.AddItem(phone, 1);

            cart.ShowCart();

            // === OOP Pillars Recap ===
            Console.WriteLine("\n═══ 4 TRỤ CỘT OOP TRONG VÍ DỤ NÀY ═══");
            Console.WriteLine("  🔒 ENCAPSULATION:");
            Console.WriteLine("     • Product.Price có validation (>= 0)");
            Console.WriteLine("     • Product.Stock chỉ thay đổi qua Purchase()/Restock()");
            Console.WriteLine("     • private static nextId — auto-increment ID");
            Console.WriteLine();
            Console.WriteLine("  🧬 INHERITANCE:");
            Console.WriteLine("     • Electronics, Clothing, Book kế thừa Product");
            Console.WriteLine("     • Dùng chung Display(), Purchase(), CalculateTax()");
            Console.WriteLine();
            Console.WriteLine("  🎭 POLYMORPHISM:");
            Console.WriteLine("     • Product[] chứa Electronics, Clothing, Book");
            Console.WriteLine("     • GetProductInfo() mỗi loại khác nhau");
            Console.WriteLine("     • Interface check: is IShippable, is IDiscountable");
            Console.WriteLine();
            Console.WriteLine("  🎨 ABSTRACTION:");
            Console.WriteLine("     • abstract Product — không thể new Product()");
            Console.WriteLine("     • abstract Category, TaxRate, GetProductInfo()");
            Console.WriteLine("     • IShippable, ITaxable, IDiscountable interfaces");
        }
    }
}
```

**Output mong đợi:**
```
═══ DANH MỤC SẢN PHẨM ═══

  [1] Laptop Gaming ASUS
      Loại: Điện tử
      Giá: 25,000,000đ + Tax (10%): 2,500,000đ = 27,500,000đ
      Kho: 10 sản phẩm
      🔌 ASUS | Bảo hành: 24 tháng | 2.5kg

  [2] iPhone 15
      Loại: Điện tử
      Giá: 30,000,000đ + Tax (10%): 3,000,000đ = 33,000,000đ
      Kho: 5 sản phẩm
      🔌 Apple | Bảo hành: 12 tháng | 0.2kg
...

═══ ĐẶT HÀNG ═══
  ✅ Thêm 1x Laptop Gaming ASUS vào giỏ
  ✅ Thêm 2x Áo Polo Premium vào giỏ
  ✅ Thêm 1x Clean Code vào giỏ
  ✅ Thêm 1x iPhone 15 vào giỏ

┌═══════════════════════════════════════════════════┐
│               🛒 GIỎ HÀNG                        │
├───────────────────────────────────────────────────┤
│ 1. Laptop Gaming ASUS
│    1x 25,000,000đ = 25,000,000đ
│    + Tax 10%: 2,500,000đ
│    - Giảm 10%: -2,500,000đ
│    🚚 📦 Chuyển phát nhanh: 42,500đ (2.5kg)
│    → Subtotal: 25,000,000đ
...
```

---

> **💡 Tổng kết 5 ví dụ:**
>
> | Ví dụ | Pattern | Điểm chính |
> |-------|---------|------------|
> | 1 | Template Method | Abstract class = skeleton + steps |
> | 2 | Abstract + Interface | Kết hợp IS-A + CAN-DO |
> | 3 | Abstract Properties | Properties cũng có thể abstract |
> | 4 | Multi-level + Sealed | abstract → abstract → concrete |
> | 5 | Phase 2 Tổng hợp | 4 trụ cột OOP hoạt động cùng nhau |

---

*"Thiết kế tốt = biết khi nào dùng abstract class, khi nào dùng interface, khi nào kết hợp."* 🏗️
