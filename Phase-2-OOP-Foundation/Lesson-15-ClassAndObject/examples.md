# 💻 Lesson 15 — Ví dụ thực hành: Class & Object

---

## Ví dụ 1: Class đầu tiên — Student

```csharp
using System;

class Student
{
    public string Name = "";
    public int Age = 0;
    public double Score = 0;

    public string GetGrade() => Score switch
    {
        >= 8 => "Giỏi", >= 6.5 => "Khá", >= 5 => "TB", _ => "Yếu"
    };

    public bool IsPass() => Score >= 5;

    public void PrintInfo()
    {
        var color = GetGrade() switch
        {
            "Giỏi" => ConsoleColor.Green,
            "Khá" => ConsoleColor.Cyan,
            "TB" => ConsoleColor.Yellow,
            _ => ConsoleColor.Red
        };
        Console.ForegroundColor = color;
        Console.WriteLine($"  {Name,-15} {Age,3} tuổi  {Score,5:F1}  [{GetGrade(),-4}] {(IsPass() ? "✅" : "❌")}");
        Console.ResetColor();
    }
}

class Program
{
    static void Main()
    {
        // Tạo objects
        Student s1 = new Student();
        s1.Name = "Nguyễn Minh";
        s1.Age = 22;
        s1.Score = 8.5;

        Student s2 = new Student();
        s2.Name = "Trần Hùng";
        s2.Age = 25;
        s2.Score = 6.0;

        Student s3 = new Student();
        s3.Name = "Lê Lan";
        s3.Age = 20;
        s3.Score = 4.0;

        // In thông tin
        Console.WriteLine("╔═══════════════════════════════════════════╗");
        Console.WriteLine("║          DANH SÁCH SINH VIÊN              ║");
        Console.WriteLine("╠═══════════════════════════════════════════╣");
        s1.PrintInfo();
        s2.PrintInfo();
        s3.PrintInfo();
        Console.WriteLine("╚═══════════════════════════════════════════╝");
    }
}
```

---

## Ví dụ 2: Class Product — Methods thực tế

```csharp
using System;

class Product
{
    public string Name = "";
    public decimal Price = 0;
    public int Stock = 0;
    public string Category = "";

    // Tính tổng tiền
    public decimal CalculateTotal(int quantity) => Price * quantity;

    // Kiểm tra còn hàng
    public bool IsInStock() => Stock > 0;
    public bool IsInStock(int quantity) => Stock >= quantity; // Overload!

    // Bán hàng
    public bool Sell(int quantity)
    {
        if (quantity <= 0 || quantity > Stock) return false;
        Stock -= quantity;
        return true;
    }

    // Nhập thêm hàng
    public void Restock(int amount)
    {
        if (amount > 0) Stock += amount;
    }

    // Áp giảm giá
    public decimal GetDiscountedPrice(double percent)
    {
        if (percent < 0 || percent > 100) return Price;
        return Price * (1 - (decimal)percent / 100);
    }

    // In thông tin
    public void Print()
    {
        string status = IsInStock() ? $"Còn {Stock}" : "HẾT HÀNG";
        Console.WriteLine($"  {Name,-20} {Price,14:N0} VNĐ  [{status}]  ({Category})");
    }
}

class Program
{
    static void Main()
    {
        Product p1 = new Product();
        p1.Name = "iPhone 15 Pro";
        p1.Price = 28990000m;
        p1.Stock = 10;
        p1.Category = "Điện thoại";

        Product p2 = new Product();
        p2.Name = "AirPods Pro 2";
        p2.Price = 5990000m;
        p2.Stock = 25;
        p2.Category = "Phụ kiện";

        Console.WriteLine("═══ SẢN PHẨM ═══");
        p1.Print();
        p2.Print();

        // Bán hàng
        Console.WriteLine($"\nBán 3 {p1.Name}...");
        if (p1.Sell(3))
        {
            Console.WriteLine($"✅ Thành công! Tổng: {p1.CalculateTotal(3):N0} VNĐ");
            Console.WriteLine($"   Còn lại: {p1.Stock}");
        }

        // Giảm giá
        Console.WriteLine($"\n{p2.Name} giảm 15%:");
        Console.WriteLine($"  Gốc: {p2.Price:N0} → Giảm: {p2.GetDiscountedPrice(15):N0}");
    }
}
```

---

## Ví dụ 3: Mảng Object — Quản lý danh sách

```csharp
using System;

class Contact
{
    public string Name = "";
    public string Phone = "";
    public string Email = "";
    public string Group = "Khác";

    public void Print(int index)
    {
        Console.WriteLine($"  {index,3}. {Name,-18} {Phone,-14} {Email,-25} [{Group}]");
    }
}

class Program
{
    static Contact[] contacts = new Contact[50];
    static int count = 0;

    static void Main()
    {
        // Seed data
        AddContact("Nguyễn An", "0912345678", "an@gmail.com", "Bạn bè");
        AddContact("Trần Bình", "0987654321", "binh@yahoo.com", "Công ty");
        AddContact("Lê Chi", "0901234567", "chi@outlook.com", "Bạn bè");
        AddContact("Phạm Dũng", "0976543210", "dung@company.vn", "Công ty");

        string? choice;
        do
        {
            Console.Clear();
            Console.WriteLine($"📇 DANH BẠ ({count}/50)\n");
            Console.WriteLine("1. Xem  2. Thêm  3. Tìm  4. Đếm theo nhóm  0. Thoát");
            Console.Write("Chọn: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ListContacts(); break;
                case "2": InputContact(); break;
                case "3": SearchContacts(); break;
                case "4": CountByGroup(); break;
            }

            if (choice != "0") { Console.Write("\nEnter..."); Console.ReadLine(); }
        } while (choice != "0");
    }

    static void AddContact(string name, string phone, string email, string group)
    {
        if (count >= contacts.Length) return;
        contacts[count] = new Contact();
        contacts[count].Name = name;
        contacts[count].Phone = phone;
        contacts[count].Email = email;
        contacts[count].Group = group;
        count++;
    }

    static void InputContact()
    {
        if (count >= contacts.Length) { Console.WriteLine("Đầy!"); return; }
        Console.Write("Tên: ");
        string name = Console.ReadLine()?.Trim() ?? "";
        Console.Write("SĐT: ");
        string phone = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Email: ");
        string email = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Nhóm (Bạn bè/Công ty/Gia đình/Khác): ");
        string group = Console.ReadLine()?.Trim() ?? "Khác";

        AddContact(name, phone, email, group);
        Console.WriteLine("✅ Đã thêm!");
    }

    static void ListContacts()
    {
        Console.WriteLine($"\n{"#",5} {"Tên",-18} {"SĐT",-14} {"Email",-25} Nhóm");
        Console.WriteLine(new string('─', 70));
        for (int i = 0; i < count; i++)
            contacts[i].Print(i + 1);
    }

    static void SearchContacts()
    {
        Console.Write("Tìm: ");
        string? keyword = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(keyword)) return;

        int found = 0;
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || contacts[i].Phone.Contains(keyword))
            {
                contacts[i].Print(++found);
            }
        }
        if (found == 0) Console.WriteLine("❌ Không tìm thấy!");
    }

    static void CountByGroup()
    {
        string[] groups = { "Bạn bè", "Công ty", "Gia đình", "Khác" };
        Console.WriteLine("\n📊 Thống kê theo nhóm:");
        foreach (string g in groups)
        {
            int c = 0;
            for (int i = 0; i < count; i++)
                if (contacts[i].Group.Equals(g, StringComparison.OrdinalIgnoreCase)) c++;
            if (c > 0)
                Console.WriteLine($"  {g,-12} {new string('█', c)} {c}");
        }
    }
}
```

---

## Ví dụ 4: Object interaction — 2 class tương tác

```csharp
using System;

class BankAccount
{
    public string Id = "";
    public string Owner = "";
    public decimal Balance = 0;

    public bool Deposit(decimal amount)
    {
        if (amount <= 0) return false;
        Balance += amount;
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance) return false;
        Balance -= amount;
        return true;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"  [{Id}] {Owner,-15} Số dư: {Balance,14:N0} VNĐ");
    }
}

class Transaction
{
    public string FromId = "";
    public string ToId = "";
    public decimal Amount = 0;
    public string Timestamp = "";
    public bool Success = false;

    public void Print()
    {
        string status = Success ? "✅" : "❌";
        Console.WriteLine($"  {status} {FromId} → {ToId}: {Amount:N0} VNĐ [{Timestamp}]");
    }
}

class Program
{
    static void Main()
    {
        // Tạo tài khoản
        BankAccount acc1 = new BankAccount();
        acc1.Id = "ACC001"; acc1.Owner = "Minh"; acc1.Balance = 10000000m;

        BankAccount acc2 = new BankAccount();
        acc2.Id = "ACC002"; acc2.Owner = "Hùng"; acc2.Balance = 5000000m;

        Console.WriteLine("═══ TRƯỚC GIAO DỊCH ═══");
        acc1.PrintStatement();
        acc2.PrintStatement();

        // Chuyển tiền — 2 objects tương tác
        Transaction tx = Transfer(acc1, acc2, 3000000m);
        tx.Print();

        Console.WriteLine("\n═══ SAU GIAO DỊCH ═══");
        acc1.PrintStatement();
        acc2.PrintStatement();
    }

    static Transaction Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        Transaction tx = new Transaction();
        tx.FromId = from.Id;
        tx.ToId = to.Id;
        tx.Amount = amount;
        tx.Timestamp = DateTime.Now.ToString("HH:mm:ss");

        if (from.Withdraw(amount) && to.Deposit(amount))
            tx.Success = true;
        else
        {
            from.Deposit(amount); // Hoàn lại nếu thất bại
            tx.Success = false;
        }
        return tx;
    }
}
```

### 📝 Bài học:

- `BankAccount` tự quản lý data (Balance) + behavior (Deposit/Withdraw)
- `Transaction` ghi lại kết quả giao dịch
- **Objects giao tiếp** qua method calls — không truy cập trực tiếp mảng global
