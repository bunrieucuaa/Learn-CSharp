# 💻 Bài 21: Polymorphism — Ví dụ thực hành

> **Tất cả ví dụ đều chạy được. Hãy tạo Console App và copy-paste để thử!**

---

## Ví dụ 1: Shape Polymorphism — Hình học đa hình 🔷

> **Mục tiêu:** Mảng `Shape[]` chứa Circle, Rectangle, Triangle — tính Area() và Draw() đa hình.

```csharp
using System;

namespace ShapePolymorphism
{
    // ═══════════════════════════════════════
    // BASE CLASS: Shape
    // ═══════════════════════════════════════
    class Shape
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public Shape(string name, string color)
        {
            Name = name;
            Color = color;
        }

        // Virtual methods — class con sẽ override
        public virtual double GetArea()
        {
            return 0;
        }

        public virtual double GetPerimeter()
        {
            return 0;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"  Vẽ hình: {Name} (màu {Color})");
        }

        // Concrete method — dùng chung, KHÔNG override
        public void PrintInfo()
        {
            Console.WriteLine($"┌─── {Name} ───");
            Console.WriteLine($"│ Màu sắc  : {Color}");
            Console.WriteLine($"│ Diện tích: {GetArea():F2}");
            Console.WriteLine($"│ Chu vi   : {GetPerimeter():F2}");
            Console.Write("│ ");
            Draw();
            Console.WriteLine("└────────────────");
        }
    }

    // ═══════════════════════════════════════
    // CIRCLE
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
        {
            return Math.PI * Radius * Radius;
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"  Vẽ ⭕ bán kính {Radius}");
        }
    }

    // ═══════════════════════════════════════
    // RECTANGLE
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
        {
            return Width * Height;
        }

        public override double GetPerimeter()
        {
            return 2 * (Width + Height);
        }

        public override void Draw()
        {
            Console.WriteLine($"  Vẽ ▭ {Width}x{Height}");
        }
    }

    // ═══════════════════════════════════════
    // TRIANGLE
    // ═══════════════════════════════════════
    class Triangle : Shape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(double a, double b, double c, string color)
            : base("Tam giác", color)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }

        public override double GetArea()
        {
            // Công thức Heron
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }

        public override double GetPerimeter()
        {
            return SideA + SideB + SideC;
        }

        public override void Draw()
        {
            Console.WriteLine($"  Vẽ △ cạnh ({SideA}, {SideB}, {SideC})");
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   SHAPE POLYMORPHISM DEMO           ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Mảng Shape[] chứa nhiều loại hình khác nhau
            Shape[] shapes = new Shape[]
            {
                new Circle(5, "Đỏ"),
                new Rectangle(4, 6, "Xanh dương"),
                new Triangle(3, 4, 5, "Xanh lá"),
                new Circle(3, "Vàng"),
                new Rectangle(10, 2, "Tím")
            };

            // Polymorphism: 1 vòng lặp xử lý TẤT CẢ
            foreach (Shape shape in shapes)
            {
                shape.PrintInfo();
                Console.WriteLine();
            }

            // Tính tổng diện tích — polymorphism!
            double totalArea = 0;
            foreach (Shape shape in shapes)
            {
                totalArea += shape.GetArea();  // Mỗi shape tính theo cách riêng
            }
            Console.WriteLine($"📐 Tổng diện tích: {totalArea:F2}");

            // Đếm từng loại — is keyword
            int circles = 0, rectangles = 0, triangles = 0;
            foreach (Shape shape in shapes)
            {
                if (shape is Circle) circles++;
                else if (shape is Rectangle) rectangles++;
                else if (shape is Triangle) triangles++;
            }
            Console.WriteLine($"⭕ Hình tròn: {circles}");
            Console.WriteLine($"▭  Hình chữ nhật: {rectangles}");
            Console.WriteLine($"△  Tam giác: {triangles}");
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   SHAPE POLYMORPHISM DEMO           ║
╚══════════════════════════════════════╝

┌─── Hình tròn ───
│ Màu sắc  : Đỏ
│ Diện tích: 78.54
│ Chu vi   : 31.42
│   Vẽ ⭕ bán kính 5
└────────────────

┌─── Hình chữ nhật ───
│ Màu sắc  : Xanh dương
│ Diện tích: 24.00
│ Chu vi   : 20.00
│   Vẽ ▭ 4x6
└────────────────
...
```

---

## Ví dụ 2: Payment System — Hệ thống thanh toán 💳

> **Mục tiêu:** Nhiều phương thức thanh toán, xử lý đa hình qua `Payment.Process()`.

```csharp
using System;

namespace PaymentPolymorphism
{
    // ═══════════════════════════════════════
    // BASE CLASS: Payment
    // ═══════════════════════════════════════
    class Payment
    {
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(string customerName, decimal amount)
        {
            CustomerName = customerName;
            Amount = amount;
            PaymentDate = DateTime.Now;
        }

        public virtual bool Validate()
        {
            return Amount > 0;
        }

        public virtual void Process()
        {
            Console.WriteLine($"  Xử lý thanh toán {Amount:N0}đ cho {CustomerName}");
        }

        public virtual string GetReceipt()
        {
            return $"[{PaymentDate:HH:mm:ss}] {CustomerName} - {Amount:N0}đ";
        }

        public void ExecutePayment()
        {
            Console.WriteLine($"── Thanh toán: {GetType().Name} ──");

            if (!Validate())
            {
                Console.WriteLine("  ❌ Thanh toán không hợp lệ!");
                return;
            }

            Process();
            Console.WriteLine($"  📧 Biên lai: {GetReceipt()}");
            Console.WriteLine($"  ✅ Thành công!\n");
        }
    }

    // ═══════════════════════════════════════
    // CASH PAYMENT — Thanh toán tiền mặt
    // ═══════════════════════════════════════
    class CashPayment : Payment
    {
        public decimal ReceivedAmount { get; set; }

        public CashPayment(string customerName, decimal amount, decimal received)
            : base(customerName, amount)
        {
            ReceivedAmount = received;
        }

        public override bool Validate()
        {
            return base.Validate() && ReceivedAmount >= Amount;
        }

        public override void Process()
        {
            base.Process();
            decimal change = ReceivedAmount - Amount;
            Console.WriteLine($"  💵 Tiền mặt nhận: {ReceivedAmount:N0}đ");
            Console.WriteLine($"  💰 Tiền thối: {change:N0}đ");
        }

        public override string GetReceipt()
        {
            return base.GetReceipt() + " [TIỀN MẶT]";
        }
    }

    // ═══════════════════════════════════════
    // CARD PAYMENT — Thanh toán thẻ
    // ═══════════════════════════════════════
    class CardPayment : Payment
    {
        public string CardNumber { get; set; }
        public string CardType { get; set; }

        public CardPayment(string customerName, decimal amount,
                          string cardNumber, string cardType)
            : base(customerName, amount)
        {
            CardNumber = cardNumber;
            CardType = cardType;
        }

        public override bool Validate()
        {
            return base.Validate() && CardNumber.Length >= 12;
        }

        public override void Process()
        {
            string masked = "****-****-****-" + CardNumber[^4..];
            Console.WriteLine($"  💳 Thẻ {CardType}: {masked}");
            Console.WriteLine($"  🔄 Đang kết nối ngân hàng...");
            Console.WriteLine($"  📡 Giao dịch {Amount:N0}đ đã được chấp nhận");
        }

        public override string GetReceipt()
        {
            return base.GetReceipt() + $" [THẺ {CardType.ToUpper()}]";
        }
    }

    // ═══════════════════════════════════════
    // E-WALLET PAYMENT — Ví điện tử
    // ═══════════════════════════════════════
    class EWalletPayment : Payment
    {
        public string WalletName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal WalletBalance { get; set; }

        public EWalletPayment(string customerName, decimal amount,
                             string walletName, string phone, decimal balance)
            : base(customerName, amount)
        {
            WalletName = walletName;
            PhoneNumber = phone;
            WalletBalance = balance;
        }

        public override bool Validate()
        {
            return base.Validate() && WalletBalance >= Amount;
        }

        public override void Process()
        {
            Console.WriteLine($"  📱 Ví {WalletName} ({PhoneNumber})");
            Console.WriteLine($"  💰 Số dư trước: {WalletBalance:N0}đ");
            WalletBalance -= Amount;
            Console.WriteLine($"  💸 Thanh toán: -{Amount:N0}đ");
            Console.WriteLine($"  💰 Số dư sau: {WalletBalance:N0}đ");
        }

        public override string GetReceipt()
        {
            return base.GetReceipt() + $" [VÍ {WalletName.ToUpper()}]";
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
            Console.WriteLine("║   PAYMENT SYSTEM — ĐA HÌNH          ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Mảng Payment[] — chứa nhiều loại thanh toán
            Payment[] payments = new Payment[]
            {
                new CashPayment("Nguyễn Văn A", 150_000, 200_000),
                new CardPayment("Trần Thị B", 500_000, "1234567890123456", "Visa"),
                new EWalletPayment("Lê Văn C", 75_000, "MoMo", "0901234567", 200_000),
                new CashPayment("Phạm Thị D", 300_000, 100_000),  // Thiếu tiền!
            };

            // Polymorphism: cùng ExecutePayment() → behavior khác nhau
            foreach (Payment payment in payments)
            {
                payment.ExecutePayment();
            }

            // Pattern matching: xử lý riêng theo loại
            Console.WriteLine("═══ THỐNG KÊ ═══");
            decimal totalCash = 0, totalCard = 0, totalWallet = 0;

            foreach (Payment payment in payments)
            {
                if (payment is CashPayment cash && cash.Validate())
                    totalCash += cash.Amount;
                else if (payment is CardPayment card && card.Validate())
                    totalCard += card.Amount;
                else if (payment is EWalletPayment wallet && wallet.Validate())
                    totalWallet += wallet.Amount;
            }

            Console.WriteLine($"  💵 Tiền mặt:  {totalCash:N0}đ");
            Console.WriteLine($"  💳 Thẻ:       {totalCard:N0}đ");
            Console.WriteLine($"  📱 Ví điện tử: {totalWallet:N0}đ");
        }
    }
}
```

---

## Ví dụ 3: Notification System — Hệ thống thông báo 🔔

> **Mục tiêu:** Gửi thông báo qua nhiều kênh khác nhau — Email, SMS, Push — cùng 1 method `Send()`.

```csharp
using System;

namespace NotificationPolymorphism
{
    // ═══════════════════════════════════════
    // BASE CLASS: Notification
    // ═══════════════════════════════════════
    class Notification
    {
        public string Recipient { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSent { get; private set; }

        public Notification(string recipient, string title, string message)
        {
            Recipient = recipient;
            Title = title;
            Message = message;
            CreatedAt = DateTime.Now;
            IsSent = false;
        }

        public virtual void Send()
        {
            Console.WriteLine($"  📨 Gửi thông báo đến {Recipient}");
            IsSent = true;
        }

        public virtual string GetChannel()
        {
            return "General";
        }

        public void PrintStatus()
        {
            string status = IsSent ? "✅ Đã gửi" : "⏳ Chờ gửi";
            Console.WriteLine($"  [{GetChannel()}] {Title} → {Recipient} {status}");
        }
    }

    // ═══════════════════════════════════════
    // EMAIL NOTIFICATION
    // ═══════════════════════════════════════
    class EmailNotification : Notification
    {
        public string Subject { get; set; }
        public bool HasAttachment { get; set; }

        public EmailNotification(string email, string subject, string message,
                                bool hasAttachment = false)
            : base(email, subject, message)
        {
            Subject = subject;
            HasAttachment = hasAttachment;
        }

        public override void Send()
        {
            Console.WriteLine($"  📧 EMAIL gửi đến: {Recipient}");
            Console.WriteLine($"     Tiêu đề: {Subject}");
            Console.WriteLine($"     Nội dung: {Message}");
            if (HasAttachment)
                Console.WriteLine("     📎 Có file đính kèm");
            Console.WriteLine("     → Email đã gửi thành công!");
            IsSent = true;
        }

        public override string GetChannel() => "EMAIL";
    }

    // ═══════════════════════════════════════
    // SMS NOTIFICATION
    // ═══════════════════════════════════════
    class SmsNotification : Notification
    {
        public string PhoneNumber { get; set; }

        public SmsNotification(string phone, string title, string message)
            : base(phone, title, message)
        {
            PhoneNumber = phone;
        }

        public override void Send()
        {
            // SMS giới hạn 160 ký tự
            string smsContent = Message.Length > 160
                ? Message[..157] + "..."
                : Message;

            Console.WriteLine($"  📱 SMS gửi đến: {PhoneNumber}");
            Console.WriteLine($"     Nội dung ({smsContent.Length} ký tự): {smsContent}");
            Console.WriteLine("     → SMS đã gửi thành công!");
            IsSent = true;
        }

        public override string GetChannel() => "SMS";
    }

    // ═══════════════════════════════════════
    // PUSH NOTIFICATION
    // ═══════════════════════════════════════
    class PushNotification : Notification
    {
        public string DeviceToken { get; set; }
        public string Icon { get; set; }

        public PushNotification(string deviceToken, string title, string message,
                               string icon = "🔔")
            : base(deviceToken, title, message)
        {
            DeviceToken = deviceToken;
            Icon = icon;
        }

        public override void Send()
        {
            Console.WriteLine($"  📲 PUSH đến device: {DeviceToken[..8]}...");
            Console.WriteLine($"     {Icon} {Title}");
            Console.WriteLine($"     {Message}");
            Console.WriteLine("     → Push notification đã gửi!");
            IsSent = true;
        }

        public override string GetChannel() => "PUSH";
    }

    // ═══════════════════════════════════════
    // NOTIFICATION SERVICE — Dùng Polymorphism
    // ═══════════════════════════════════════
    class NotificationService
    {
        private Notification[] notifications;
        private int count;

        public NotificationService(int maxCapacity)
        {
            notifications = new Notification[maxCapacity];
            count = 0;
        }

        // Nhận BẤT KỲ loại Notification nào — Polymorphism!
        public void AddNotification(Notification notification)
        {
            if (count < notifications.Length)
            {
                notifications[count] = notification;
                count++;
            }
        }

        // Gửi tất cả — mỗi loại tự xử lý Send() theo cách riêng
        public void SendAll()
        {
            Console.WriteLine("═══ GỬI TẤT CẢ THÔNG BÁO ═══\n");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"--- Thông báo #{i + 1} ---");
                notifications[i].Send();   // POLYMORPHISM ở đây!
                Console.WriteLine();
            }
        }

        public void PrintReport()
        {
            Console.WriteLine("═══ BÁO CÁO ═══");
            int email = 0, sms = 0, push = 0;

            for (int i = 0; i < count; i++)
            {
                notifications[i].PrintStatus();

                // Dùng pattern matching để đếm
                if (notifications[i] is EmailNotification) email++;
                else if (notifications[i] is SmsNotification) sms++;
                else if (notifications[i] is PushNotification) push++;
            }

            Console.WriteLine($"\n  📧 Email: {email} | 📱 SMS: {sms} | 📲 Push: {push}");
            Console.WriteLine($"  📊 Tổng: {count} thông báo");
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
            Console.WriteLine("║   NOTIFICATION SYSTEM               ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            NotificationService service = new NotificationService(10);

            // Thêm nhiều loại thông báo khác nhau
            service.AddNotification(
                new EmailNotification("user@email.com", "Đơn hàng mới",
                    "Đơn hàng #1234 đã được xác nhận.", true));

            service.AddNotification(
                new SmsNotification("0901234567", "Mã OTP",
                    "Mã xác thực của bạn là 123456. Có hiệu lực trong 5 phút."));

            service.AddNotification(
                new PushNotification("abc123def456", "Flash Sale!",
                    "Giảm giá 50% trong 2 giờ tới!", "🔥"));

            service.AddNotification(
                new EmailNotification("admin@company.com", "Báo cáo tuần",
                    "Doanh thu tuần này tăng 15% so với tuần trước."));

            // Gửi tất cả — Polymorphism xử lý!
            service.SendAll();

            // Báo cáo
            service.PrintReport();
        }
    }
}
```

---

## Ví dụ 4: Game Attack System — Hệ thống tấn công trong Game ⚔️

> **Mục tiêu:** Nhân vật game có attack đa hình — Warrior, Mage, Archer tấn công khác nhau.

```csharp
using System;

namespace GamePolymorphism
{
    // ═══════════════════════════════════════
    // BASE CLASS: GameCharacter
    // ═══════════════════════════════════════
    class GameCharacter
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public bool IsAlive => Hp > 0;

        public GameCharacter(string name, int hp, int attack, int defense)
        {
            Name = name;
            Hp = hp;
            MaxHp = hp;
            AttackPower = attack;
            Defense = defense;
        }

        // Virtual — mỗi class tấn công khác nhau
        public virtual int Attack()
        {
            Console.WriteLine($"  ⚔️ {Name} tấn công cơ bản!");
            return AttackPower;
        }

        // Virtual — special attack khác nhau
        public virtual int SpecialAttack()
        {
            Console.WriteLine($"  💫 {Name} dùng chiêu đặc biệt!");
            return AttackPower * 2;
        }

        // Virtual — phòng thủ
        public virtual void Defend()
        {
            Console.WriteLine($"  🛡️ {Name} phòng thủ (DEF +{Defense})");
        }

        // Nhận sát thương
        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Hp = Math.Max(0, Hp - actualDamage);
            Console.WriteLine($"  💥 {Name} nhận {actualDamage} sát thương! " +
                            $"(HP: {Hp}/{MaxHp})");

            if (!IsAlive)
                Console.WriteLine($"  ☠️ {Name} đã bị hạ gục!");
        }

        // Hiển thị thanh HP
        public void ShowStatus()
        {
            int bars = (int)((double)Hp / MaxHp * 20);
            string hpBar = new string('█', bars) + new string('░', 20 - bars);
            Console.WriteLine($"  {Name,-12} [{hpBar}] {Hp}/{MaxHp} HP");
        }
    }

    // ═══════════════════════════════════════
    // WARRIOR — Chiến binh
    // ═══════════════════════════════════════
    class Warrior : GameCharacter
    {
        public int Rage { get; set; }

        public Warrior(string name)
            : base(name, 150, 20, 15)
        {
            Rage = 0;
        }

        public override int Attack()
        {
            Rage += 10;
            Console.WriteLine($"  ⚔️ {Name} chém kiếm! (Rage: {Rage})");
            return AttackPower + Rage / 5;
        }

        public override int SpecialAttack()
        {
            if (Rage >= 30)
            {
                Console.WriteLine($"  🔥 {Name} dùng FURY SLASH! (Rage: {Rage} → 0)");
                int damage = AttackPower * 3 + Rage;
                Rage = 0;
                return damage;
            }

            Console.WriteLine($"  ⚠️ {Name} chưa đủ Rage! (Cần 30, có {Rage})");
            return Attack();
        }

        public override void Defend()
        {
            Rage += 5;
            Console.WriteLine($"  🛡️ {Name} giơ khiên! (DEF: {Defense}, Rage +5)");
        }
    }

    // ═══════════════════════════════════════
    // MAGE — Pháp sư
    // ═══════════════════════════════════════
    class Mage : GameCharacter
    {
        public int Mana { get; set; }
        public int MaxMana { get; set; }

        public Mage(string name)
            : base(name, 80, 30, 5)
        {
            Mana = 100;
            MaxMana = 100;
        }

        public override int Attack()
        {
            if (Mana >= 10)
            {
                Mana -= 10;
                Console.WriteLine($"  🔮 {Name} phóng phép! (Mana: {Mana}/{MaxMana})");
                return AttackPower;
            }

            Console.WriteLine($"  🪄 {Name} đánh bằng gậy (hết mana)!");
            return AttackPower / 3;
        }

        public override int SpecialAttack()
        {
            if (Mana >= 40)
            {
                Mana -= 40;
                Console.WriteLine($"  ☄️ {Name} dùng METEOR STRIKE! " +
                                $"(Mana: {Mana}/{MaxMana})");
                return AttackPower * 4;
            }

            Console.WriteLine($"  ⚠️ {Name} không đủ Mana! (Cần 40, có {Mana})");
            return Attack();
        }

        public override void Defend()
        {
            Mana = Math.Min(MaxMana, Mana + 20);
            Console.WriteLine($"  🧘 {Name} thiền định, hồi Mana! " +
                            $"(Mana: {Mana}/{MaxMana})");
        }
    }

    // ═══════════════════════════════════════
    // ARCHER — Cung thủ
    // ═══════════════════════════════════════
    class Archer : GameCharacter
    {
        public int Arrows { get; set; }

        public Archer(string name)
            : base(name, 100, 25, 8)
        {
            Arrows = 15;
        }

        public override int Attack()
        {
            if (Arrows > 0)
            {
                Arrows--;
                Console.WriteLine($"  🏹 {Name} bắn tên! (Tên: {Arrows} còn lại)");
                return AttackPower;
            }

            Console.WriteLine($"  🗡️ {Name} rút dao đâm (hết tên)!");
            return AttackPower / 2;
        }

        public override int SpecialAttack()
        {
            if (Arrows >= 5)
            {
                Arrows -= 5;
                Console.WriteLine($"  🌧️ {Name} dùng ARROW RAIN! " +
                                $"(-5 tên, còn {Arrows})");
                return AttackPower * 3;
            }

            Console.WriteLine($"  ⚠️ {Name} không đủ tên! (Cần 5, có {Arrows})");
            return Attack();
        }

        public override void Defend()
        {
            Arrows = Math.Min(15, Arrows + 3);
            Console.WriteLine($"  🎯 {Name} nhặt tên! (Tên: {Arrows})");
        }
    }

    // ═══════════════════════════════════════
    // BATTLE SYSTEM — Dùng Polymorphism
    // ═══════════════════════════════════════
    class BattleArena
    {
        // Nhận GameCharacter — hoạt động với BẤT KỲ class con nào!
        public static void ExecuteTurn(GameCharacter attacker,
                                       GameCharacter target, string action)
        {
            int damage = 0;

            switch (action.ToLower())
            {
                case "attack":
                    damage = attacker.Attack();       // POLYMORPHISM!
                    target.TakeDamage(damage);
                    break;
                case "special":
                    damage = attacker.SpecialAttack(); // POLYMORPHISM!
                    target.TakeDamage(damage);
                    break;
                case "defend":
                    attacker.Defend();                 // POLYMORPHISM!
                    break;
            }
        }

        public static void ShowAllStatus(GameCharacter[] characters)
        {
            Console.WriteLine("\n  ═══ TRẠNG THÁI ═══");
            foreach (GameCharacter character in characters)
            {
                character.ShowStatus();

                // Pattern matching — hiển thị thông tin riêng
                if (character is Warrior w)
                    Console.WriteLine($"               Rage: {w.Rage}");
                else if (character is Mage m)
                    Console.WriteLine($"               Mana: {m.Mana}/{m.MaxMana}");
                else if (character is Archer a)
                    Console.WriteLine($"               Tên: {a.Arrows}");
            }
            Console.WriteLine();
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
            Console.WriteLine("║   ⚔️ GAME BATTLE — POLYMORPHISM     ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo đội hình — mảng GameCharacter[]
            GameCharacter[] heroes = new GameCharacter[]
            {
                new Warrior("Garen"),
                new Mage("Lux"),
                new Archer("Ashe")
            };

            GameCharacter boss = new Warrior("Dark Knight");
            boss.Hp = 300;
            boss.MaxHp = 300;
            boss.AttackPower = 35;

            // Hiển thị trạng thái ban đầu
            BattleArena.ShowAllStatus(heroes);

            // Turn 1: Tất cả tấn công boss
            Console.WriteLine("══ TURN 1: Tấn công! ══");
            foreach (GameCharacter hero in heroes)
            {
                BattleArena.ExecuteTurn(hero, boss, "attack");
            }
            boss.ShowStatus();

            // Turn 2: Warrior và Mage tấn công, Archer phòng thủ
            Console.WriteLine("\n══ TURN 2 ══");
            BattleArena.ExecuteTurn(heroes[0], boss, "attack");
            BattleArena.ExecuteTurn(heroes[1], boss, "special");
            BattleArena.ExecuteTurn(heroes[2], boss, "defend");

            // Turn 3: Special attacks!
            Console.WriteLine("\n══ TURN 3: Chiêu đặc biệt! ══");
            foreach (GameCharacter hero in heroes)
            {
                BattleArena.ExecuteTurn(hero, boss, "special");
            }

            // Kết quả
            Console.WriteLine("\n══ KẾT QUẢ ══");
            BattleArena.ShowAllStatus(heroes);
            Console.WriteLine("  BOSS:");
            boss.ShowStatus();

            if (!boss.IsAlive)
                Console.WriteLine("\n  🎉 CHIẾN THẮNG! Boss đã bị tiêu diệt!");
            else
                Console.WriteLine($"\n  ⚔️ Boss vẫn còn {boss.Hp} HP!");
        }
    }
}
```

---

## Ví dụ 5: Method Overloading — Compile-time Polymorphism 📐

> **Mục tiêu:** Hệ thống lại Method Overloading — cùng tên method, khác tham số.

```csharp
using System;

namespace OverloadingPolymorphism
{
    // ═══════════════════════════════════════
    // LOGGER — Ghi log với nhiều overload
    // ═══════════════════════════════════════
    class Logger
    {
        // Overload 1: Chỉ message
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} | {message}");
        }

        // Overload 2: Message + level
        public void Log(string message, string level)
        {
            string icon = level switch
            {
                "INFO"    => "ℹ️",
                "WARNING" => "⚠️",
                "ERROR"   => "❌",
                "SUCCESS" => "✅",
                _         => "📝"
            };
            Console.WriteLine($"[{level}] {DateTime.Now:HH:mm:ss} {icon} {message}");
        }

        // Overload 3: Message + level + source
        public void Log(string message, string level, string source)
        {
            Log($"[{source}] {message}", level);
        }

        // Overload 4: Exception
        public void Log(Exception ex)
        {
            Log($"Exception: {ex.Message}", "ERROR");
        }

        // Overload 5: Nhiều messages
        public void Log(string[] messages)
        {
            Console.WriteLine("── Batch Log ──");
            foreach (string msg in messages)
            {
                Log(msg);
            }
            Console.WriteLine("── End Batch ──");
        }
    }

    // ═══════════════════════════════════════
    // MATH HELPER — Nhiều overload tính toán
    // ═══════════════════════════════════════
    class MathHelper
    {
        // Max — 2 số
        public int Max(int a, int b)
        {
            Console.Write($"  Max({a}, {b}) = ");
            return a > b ? a : b;
        }

        // Max — 3 số
        public int Max(int a, int b, int c)
        {
            Console.Write($"  Max({a}, {b}, {c}) = ");
            return Max(Max(a, b), c);
        }

        // Max — mảng
        public int Max(int[] numbers)
        {
            Console.Write($"  Max([{string.Join(", ", numbers)}]) = ");
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max) max = numbers[i];
            }
            return max;
        }

        // Max — double
        public double Max(double a, double b)
        {
            Console.Write($"  Max({a:F1}, {b:F1}) = ");
            return a > b ? a : b;
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
            Console.WriteLine("║   METHOD OVERLOADING DEMO            ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // === Logger Overloading ===
            Console.WriteLine("═══ LOGGER ═══");
            Logger logger = new Logger();

            logger.Log("Ứng dụng khởi động");                    // Overload 1
            logger.Log("Kết nối database thành công", "SUCCESS");  // Overload 2
            logger.Log("Timeout kết nối", "WARNING", "Database"); // Overload 3
            logger.Log(new Exception("Null reference!"));          // Overload 4
            logger.Log(new string[]                                // Overload 5
            {
                "Bước 1: Khởi tạo",
                "Bước 2: Kết nối",
                "Bước 3: Xử lý"
            });

            // === MathHelper Overloading ===
            Console.WriteLine("\n═══ MATH HELPER ═══");
            MathHelper math = new MathHelper();

            int r1 = math.Max(10, 20);                       // 2 int
            Console.WriteLine(r1);

            int r2 = math.Max(10, 20, 15);                   // 3 int
            Console.WriteLine(r2);

            int r3 = math.Max(new int[] { 5, 3, 8, 1, 9 }); // mảng
            Console.WriteLine(r3);

            double r4 = math.Max(3.14, 2.71);                // 2 double
            Console.WriteLine($"{r4:F1}");

            Console.WriteLine("\n✅ Compiler tự chọn overload phù hợp tại compile-time!");
        }
    }
}
```

---

## 📊 Tổng kết các ví dụ

| Ví dụ | Loại Polymorphism | Concept chính |
|-------|-------------------|---------------|
| Shape | Runtime | Mảng Shape[], virtual/override |
| Payment | Runtime | Open/Closed, pattern matching |
| Notification | Runtime | Service class dùng polymorphism |
| Game Attack | Runtime | Nhiều tầng override, is/as |
| Logger/Math | Compile-time | Method Overloading |

> **Tip:** Chạy từng ví dụ, thử thêm class con mới và xem polymorphism hoạt động! 🚀
