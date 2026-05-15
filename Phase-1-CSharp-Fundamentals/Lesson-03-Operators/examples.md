# 💻 Lesson 03 — Ví dụ thực hành: Operators

---

## Ví dụ 1: Arithmetic Operators — Máy tính cơ bản

```csharp
using System;

class Program
{
    static void Main()
    {
        int a = 17;
        int b = 5;

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      MÁY TÍNH ARITHMETIC            ║");
        Console.WriteLine("╠══════════════════════════════════════╣");
        Console.WriteLine($"║  a = {a}, b = {b}                       ║");
        Console.WriteLine("╠══════════════════════════════════════╣");
        Console.WriteLine($"║  a + b  = {a + b,3}  (Cộng)              ║");
        Console.WriteLine($"║  a - b  = {a - b,3}  (Trừ)              ║");
        Console.WriteLine($"║  a * b  = {a * b,3}  (Nhân)             ║");
        Console.WriteLine($"║  a / b  = {a / b,3}  (Chia nguyên)      ║");
        Console.WriteLine($"║  a % b  = {a % b,3}  (Chia lấy dư)      ║");
        Console.WriteLine("╠══════════════════════════════════════╣");

        // Giải thích phép chia
        Console.WriteLine("║                                      ║");
        Console.WriteLine("║  📝 Giải thích phép chia:            ║");
        Console.WriteLine($"║  17 / 5 = 3 (dư 2)                   ║");
        Console.WriteLine($"║  → Thương: {a / b}                        ║");
        Console.WriteLine($"║  → Dư:     {a % b}                        ║");
        Console.WriteLine($"║  → Kiểm tra: 5 × 3 + 2 = {b * (a / b) + a % b}     ║");
        Console.WriteLine("╠══════════════════════════════════════╣");

        // Chia với double
        Console.WriteLine("║                                      ║");
        Console.WriteLine("║  ⚠️ So sánh int vs double:           ║");
        Console.WriteLine($"║  int:    17 / 5   = {a / b}                ║");
        Console.WriteLine($"║  double: 17.0 / 5 = {17.0 / 5}            ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
}
```

### 🖥️ Output:

```
╔══════════════════════════════════════╗
║      MÁY TÍNH ARITHMETIC            ║
╠══════════════════════════════════════╣
║  a = 17, b = 5                       ║
╠══════════════════════════════════════╣
║  a + b  =  22  (Cộng)               ║
║  a - b  =  12  (Trừ)                ║
║  a * b  =  85  (Nhân)               ║
║  a / b  =   3  (Chia nguyên)        ║
║  a % b  =   2  (Chia lấy dư)        ║
╠══════════════════════════════════════╣
║  📝 17 / 5 = 3 (dư 2)               ║
║  → Kiểm tra: 5 × 3 + 2 = 17        ║
╠══════════════════════════════════════╣
║  ⚠️ int: 17/5 = 3                   ║
║     double: 17.0/5 = 3.4            ║
╚══════════════════════════════════════╝
```

### 📝 Giải thích:

| Phép tính | Ý nghĩa |
|-----------|---------|
| `17 / 5 = 3` | Chia nguyên — cắt phần thập phân |
| `17 % 5 = 2` | Số dư — `17 = 5×3 + 2` |
| `17.0 / 5 = 3.4` | Chia thực — vì `17.0` là `double` |

---

## Ví dụ 2: Modulo `%` — Ứng dụng thực tế

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ỨNG DỤNG CỦA MODULO (%) ===\n");

        // --- 1. Kiểm tra chẵn/lẻ ---
        Console.WriteLine("--- Kiểm tra chẵn/lẻ ---");
        for (int i = 1; i <= 10; i++)
        {
            string type = (i % 2 == 0) ? "Chẵn" : "Lẻ";
            Console.WriteLine($"  {i,2} → {type}");
        }

        // --- 2. Kiểm tra chia hết ---
        Console.WriteLine("\n--- Số chia hết cho 3 (từ 1-20) ---");
        for (int i = 1; i <= 20; i++)
        {
            if (i % 3 == 0)
                Console.Write($"{i} ");
        }
        Console.WriteLine();

        // --- 3. Lấy chữ số ---
        Console.WriteLine("\n--- Tách chữ số ---");
        int number = 1234;
        int ones = number % 10;          // 4
        int tens = (number / 10) % 10;   // 3
        int hundreds = (number / 100) % 10; // 2
        int thousands = number / 1000;    // 1

        Console.WriteLine($"  Số {number}:");
        Console.WriteLine($"  Hàng nghìn: {thousands}");
        Console.WriteLine($"  Hàng trăm:  {hundreds}");
        Console.WriteLine($"  Hàng chục:  {tens}");
        Console.WriteLine($"  Hàng đơn vị: {ones}");

        // --- 4. Chuyển đổi giây → phút:giây ---
        Console.WriteLine("\n--- Chuyển đổi thời gian ---");
        int totalSeconds = 3725;
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        Console.WriteLine($"  {totalSeconds} giây = {hours}h {minutes}m {seconds}s");
        // 3725 giây = 1h 2m 5s

        // --- 5. Tạo pattern lặp ---
        Console.WriteLine("\n--- Pattern lặp (index % 3) ---");
        string[] colors = { "🔴", "🟢", "🔵" };
        for (int i = 0; i < 9; i++)
        {
            Console.Write($"{colors[i % 3]} ");  // Lặp vòng: 0,1,2,0,1,2,...
        }
        Console.WriteLine();
    }
}
```

### 📝 Giải thích:

| Ứng dụng | Kỹ thuật |
|----------|----------|
| Chẵn/lẻ | `n % 2 == 0` |
| Chia hết | `n % k == 0` |
| Lấy chữ số cuối | `n % 10` |
| Chuyển đổi đơn vị | `totalSec % 60` = giây dư |
| Pattern lặp | `i % array.Length` = index vòng tròn |

---

## Ví dụ 3: Pre-increment vs Post-increment

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== PRE vs POST INCREMENT ===\n");

        // --- Post-increment: x++ ---
        Console.WriteLine("--- Post-increment (x++) ---");
        int a = 5;
        Console.WriteLine($"  a ban đầu:          {a}");

        int result1 = a++;   // Lấy a (5) trước, rồi tăng a lên 6
        Console.WriteLine($"  result1 = a++:      {result1}  (lấy a=5 trước)");
        Console.WriteLine($"  a sau đó:           {a}   (đã tăng lên 6)");

        // --- Pre-increment: ++x ---
        Console.WriteLine("\n--- Pre-increment (++x) ---");
        int b = 5;
        Console.WriteLine($"  b ban đầu:          {b}");

        int result2 = ++b;   // Tăng b lên 6 trước, rồi lấy b (6)
        Console.WriteLine($"  result2 = ++b:      {result2}  (tăng b=6 trước, rồi lấy)");
        Console.WriteLine($"  b sau đó:           {b}");

        // --- Minh họa rõ hơn ---
        Console.WriteLine("\n--- So sánh trực tiếp ---");
        int x = 10, y = 10;

        Console.WriteLine($"  x = {x}, y = {y}");
        Console.WriteLine($"  x++ = {x++}  →  x bây giờ = {x}");  // In 10, x = 11
        Console.WriteLine($"  ++y = {++y}  →  y bây giờ = {y}");  // In 11, y = 11

        // --- Trong vòng lặp (cả 2 cho kết quả giống nhau) ---
        Console.WriteLine("\n--- Trong vòng for (giống nhau!) ---");
        Console.Write("  i++: ");
        for (int i = 0; i < 5; i++) Console.Write($"{i} ");

        Console.Write("\n  ++i: ");
        for (int i = 0; i < 5; ++i) Console.Write($"{i} ");
        Console.WriteLine();

        Console.WriteLine("\n💡 Trong vòng for, i++ và ++i cho kết quả GIỐNG NHAU!");
    }
}
```

---

## Ví dụ 4: Comparison & Logical Operators

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== HỆ THỐNG KIỂM TRA ĐIỀU KIỆN ===\n");

        // Thông tin khách hàng
        int age = 22;
        bool hasMembership = true;
        decimal balance = 500000m;
        decimal ticketPrice = 150000m;
        bool isWeekend = true;

        // --- Comparison ---
        Console.WriteLine("--- Comparison Operators ---");
        Console.WriteLine($"  Tuổi: {age}");
        Console.WriteLine($"  age >= 18: {age >= 18}");     // true
        Console.WriteLine($"  age == 22: {age == 22}");     // true
        Console.WriteLine($"  age != 30: {age != 30}");     // true
        Console.WriteLine($"  age < 16:  {age < 16}");      // false

        // --- Logical: Kiểm tra mua vé ---
        Console.WriteLine("\n--- Logical Operators: Mua vé ---");

        // Điều kiện mua vé: đủ 18 tuổi VÀ đủ tiền
        bool canBuy = (age >= 18) && (balance >= ticketPrice);
        Console.WriteLine($"  Đủ tuổi VÀ đủ tiền? {canBuy}");  // true

        // Được giảm giá: là thành viên HOẶC là cuối tuần
        bool hasDiscount = hasMembership || isWeekend;
        Console.WriteLine($"  Có giảm giá? {hasDiscount}");  // true

        // Tính giá vé
        decimal discount = hasDiscount ? 0.20m : 0m;  // Giảm 20% nếu có
        decimal finalPrice = ticketPrice * (1 - discount);
        decimal remaining = balance - finalPrice;

        Console.WriteLine($"\n  Giá gốc:      {ticketPrice:N0} VNĐ");
        Console.WriteLine($"  Giảm giá:     {discount:P0}");       // P0 = percentage format
        Console.WriteLine($"  Giá sau giảm: {finalPrice:N0} VNĐ");
        Console.WriteLine($"  Số dư còn:    {remaining:N0} VNĐ");

        // --- Kết hợp nhiều điều kiện ---
        Console.WriteLine("\n--- Điều kiện phức tạp ---");

        // VIP: >= 18 tuổi VÀ là thành viên VÀ số dư >= 1 triệu
        bool isVIP = (age >= 18) && hasMembership && (balance >= 1000000m);
        Console.WriteLine($"  Là VIP? {isVIP}");  // false (balance < 1M)

        // Được vào free: dưới 6 tuổi HOẶC trên 65 tuổi
        bool isFree = (age < 6) || (age > 65);
        Console.WriteLine($"  Vào free? {isFree}");  // false

        // KHÔNG phải thành viên
        bool isGuest = !hasMembership;
        Console.WriteLine($"  Là khách? {isGuest}");  // false
    }
}
```

### 📝 Giải thích:

| Biểu thức | Cách đọc |
|-----------|---------|
| `(age >= 18) && (balance >= ticketPrice)` | Đủ tuổi **VÀ** đủ tiền |
| `hasMembership \|\| isWeekend` | Là thành viên **HOẶC** cuối tuần |
| `!hasMembership` | **KHÔNG** phải thành viên |
| `{discount:P0}` | Format phần trăm: `0.20` → `20%` |

---

## Ví dụ 5: Null-related Operators

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== NULL OPERATORS DEMO ===\n");

        // --- ?? (Null-coalescing) ---
        Console.WriteLine("--- ?? Null-coalescing ---");

        string? username = null;
        string displayName = username ?? "Khách";
        Console.WriteLine($"  username: {(username == null ? "null" : username)}");
        Console.WriteLine($"  displayName: {displayName}");  // "Khách"

        string? username2 = "NguyenA";
        string displayName2 = username2 ?? "Khách";
        Console.WriteLine($"\n  username2: {username2}");
        Console.WriteLine($"  displayName2: {displayName2}");  // "NguyenA"

        // --- ??= (Null-coalescing assignment) ---
        Console.WriteLine("\n--- ??= Null-coalescing Assignment ---");

        string? config = null;
        Console.WriteLine($"  config trước: {(config == null ? "null" : config)}");

        config ??= "default_value";  // config là null → gán
        Console.WriteLine($"  config sau ??=: {config}");  // "default_value"

        config ??= "other_value";    // config KHÔNG null → KHÔNG gán
        Console.WriteLine($"  config sau ??= lần 2: {config}");  // vẫn "default_value"

        // --- ?. (Null-conditional) ---
        Console.WriteLine("\n--- ?. Null-conditional ---");

        string? email = "user@example.com";
        string? nullEmail = null;

        // An toàn — không crash khi null
        int? emailLength = email?.Length;
        int? nullEmailLength = nullEmail?.Length;

        Console.WriteLine($"  email?.Length:     {emailLength}");      // 16
        Console.WriteLine($"  nullEmail?.Length: {(nullEmailLength == null ? "null" : nullEmailLength.ToString())}"); // null

        // --- Kết hợp ?. và ?? ---
        Console.WriteLine("\n--- Kết hợp ?. và ?? ---");

        string? address = null;
        int addressLength = address?.Length ?? 0;
        string upperAddress = address?.ToUpper() ?? "KHÔNG CÓ ĐỊA CHỈ";

        Console.WriteLine($"  address?.Length ?? 0: {addressLength}");         // 0
        Console.WriteLine($"  address?.ToUpper() ?? ...: {upperAddress}");     // "KHÔNG CÓ ĐỊA CHỈ"

        // --- Ứng dụng thực tế: Profile người dùng ---
        Console.WriteLine("\n--- Ứng dụng: User Profile ---");

        string? firstName = "Minh";
        string? middleName = null;
        string? lastName = "Nguyễn";
        string? nickname = null;
        int? age = null;

        string fullName = $"{lastName} {middleName ?? ""} {firstName}".Trim();
        string display = nickname ?? fullName;
        string ageDisplay = age.HasValue ? $"{age} tuổi" : "Chưa cung cấp";

        Console.WriteLine($"  Họ tên:     {fullName}");
        Console.WriteLine($"  Hiển thị:   {display}");
        Console.WriteLine($"  Tuổi:       {ageDisplay}");
    }
}
```

### 📝 Giải thích:

| Operator | Cách đọc |
|----------|---------|
| `a ?? b` | "Nếu `a` null thì dùng `b`" |
| `a ??= b` | "Nếu `a` null thì **gán** `a = b`" |
| `a?.Method()` | "Nếu `a` không null thì gọi `Method()`, null thì trả null" |
| `a?.Length ?? 0` | "Lấy Length nếu có, không thì 0" |

---

## Ví dụ 6: Ứng dụng tổng hợp — Hệ thống tính giá phòng khách sạn

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║     HỆ THỐNG TÍNH GIÁ PHÒNG KHÁCH SẠN   ║");
        Console.WriteLine("╚════════════════════════════════════════════╝\n");

        // === Thông tin đặt phòng ===
        string guestName = "Trần Văn B";
        int nights = 3;
        bool isWeekend = true;
        bool isMember = true;
        string? couponCode = "SUMMER20";
        int? loyaltyPoints = 1500;

        // === Giá phòng ===
        const decimal BASE_PRICE = 800000m;          // Giá cơ bản/đêm
        const decimal WEEKEND_SURCHARGE = 0.15m;     // Phụ thu cuối tuần 15%
        const decimal MEMBER_DISCOUNT = 0.10m;       // Giảm thành viên 10%
        const decimal COUPON_DISCOUNT = 0.20m;       // Giảm coupon 20%
        const int POINTS_THRESHOLD = 1000;           // Điểm tối thiểu để dùng
        const decimal POINTS_DISCOUNT = 50000m;      // Giảm khi dùng điểm

        // === Tính toán ===

        // Bước 1: Giá cơ bản × số đêm
        decimal subtotal = BASE_PRICE * nights;

        // Bước 2: Phụ thu cuối tuần (nếu có)
        decimal weekendFee = isWeekend ? subtotal * WEEKEND_SURCHARGE : 0m;

        // Bước 3: Giảm giá thành viên (nếu có)
        decimal memberDiscount = isMember ? subtotal * MEMBER_DISCOUNT : 0m;

        // Bước 4: Giảm giá coupon (nếu có)
        bool hasCoupon = couponCode != null && couponCode.Length > 0;
        decimal couponDiscount = hasCoupon ? subtotal * COUPON_DISCOUNT : 0m;

        // Bước 5: Giảm giá điểm thưởng (nếu đủ điểm)
        bool canUsePoints = (loyaltyPoints ?? 0) >= POINTS_THRESHOLD;
        decimal pointsDiscount = canUsePoints ? POINTS_DISCOUNT : 0m;

        // Bước 6: Chỉ được chọn 1 loại giảm giá lớn nhất
        decimal bestDiscount = Math.Max(memberDiscount, Math.Max(couponDiscount, pointsDiscount));
        string discountType = bestDiscount == couponDiscount ? $"Coupon ({couponCode})"
                            : bestDiscount == memberDiscount ? "Thành viên"
                            : bestDiscount == pointsDiscount ? "Điểm thưởng"
                            : "Không có";

        // Bước 7: Tổng cộng
        decimal total = subtotal + weekendFee - bestDiscount;
        decimal perNight = total / nights;

        // === In hóa đơn ===
        Console.WriteLine($"  Khách:          {guestName}");
        Console.WriteLine($"  Số đêm:         {nights}");
        Console.WriteLine($"  Cuối tuần:      {(isWeekend ? "Có" : "Không")}");
        Console.WriteLine($"  Thành viên:     {(isMember ? "Có" : "Không")}");
        Console.WriteLine($"  Coupon:         {couponCode ?? "Không có"}");
        Console.WriteLine($"  Điểm thưởng:   {loyaltyPoints?.ToString("N0") ?? "Không có"}");
        Console.WriteLine("  ────────────────────────────────");
        Console.WriteLine($"  Giá cơ bản:     {subtotal,15:N0} VNĐ");
        Console.WriteLine($"  Phụ thu Weekend: {weekendFee,14:N0} VNĐ");
        Console.WriteLine($"  Giảm giá:       {bestDiscount,15:N0} VNĐ ({discountType})");
        Console.WriteLine("  ────────────────────────────────");
        Console.WriteLine($"  TỔNG CỘNG:      {total,15:N0} VNĐ");
        Console.WriteLine($"  Trung bình/đêm: {perNight,15:N0} VNĐ");
    }
}
```

### 📝 Giải thích kỹ thuật sử dụng:

| Operator | Dùng ở đâu |
|----------|-----------|
| `*`, `/` | Tính giá, trung bình |
| `? :` (ternary) | Tính phụ thu, giảm giá |
| `&&` | Kiểm tra coupon hợp lệ |
| `?? 0` | Xử lý điểm thưởng null |
| `?.ToString()` | An toàn khi hiển thị nullable |
| `Math.Max()` | Tìm giảm giá lớn nhất |
| `:N0` | Format tiền tệ |

> 💡 **Bài học:** Một ví dụ thực tế đơn giản đã dùng gần hết tất cả operators! Hiểu rõ operators = viết logic business được.
