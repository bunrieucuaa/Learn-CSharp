# 💻 Lesson 01 — Ví dụ thực hành: Variables

---

## Ví dụ 1: Khai báo các kiểu dữ liệu cơ bản

```csharp
using System;

class Program
{
    static void Main()
    {
        // === Khai báo biến với kiểu dữ liệu rõ ràng ===

        int age = 25;                    // Số nguyên
        double height = 1.75;            // Số thực
        string name = "Nguyễn Văn A";    // Chuỗi ký tự
        bool isStudent = true;           // Đúng/Sai
        char bloodType = 'O';            // Một ký tự

        // === In ra màn hình ===

        Console.WriteLine("=== THÔNG TIN CÁ NHÂN ===");
        Console.WriteLine("Họ tên: " + name);           // Nối chuỗi bằng +
        Console.WriteLine("Tuổi: " + age);
        Console.WriteLine("Chiều cao: " + height + "m");
        Console.WriteLine("Là sinh viên: " + isStudent);
        Console.WriteLine("Nhóm máu: " + bloodType);
    }
}
```

### 🖥️ Output:

```
=== THÔNG TIN CÁ NHÂN ===
Họ tên: Nguyễn Văn A
Tuổi: 25
Chiều cao: 1.75m
Là sinh viên: True
Nhóm máu: O
```

### 📝 Giải thích:

| Dòng | Giải thích |
|------|-----------|
| `int age = 25;` | Tạo biến `age` kiểu số nguyên, gán giá trị 25 |
| `double height = 1.75;` | Tạo biến `height` kiểu số thực, gán 1.75 |
| `string name = "Nguyễn Văn A";` | Tạo biến `name` kiểu chuỗi |
| `bool isStudent = true;` | Tạo biến `isStudent` kiểu boolean |
| `char bloodType = 'O';` | Tạo biến `bloodType` kiểu ký tự — chú ý dùng **nháy đơn** |
| `Console.WriteLine(...)` | In ra console và xuống dòng (giống `console.log` trong JS) |
| `"Tuổi: " + age` | Nối chuỗi — C# tự động chuyển `int` thành `string` khi nối |

---

## Ví dụ 2: Dùng String Interpolation (cách hiện đại)

```csharp
using System;

class Program
{
    static void Main()
    {
        string product = "Laptop";
        decimal price = 15990000m;    // Chú ý hậu tố 'm' cho decimal
        int quantity = 3;
        decimal total = price * quantity;

        // Cách 1: Nối chuỗi bằng + (cổ điển)
        Console.WriteLine("Sản phẩm: " + product + " - Giá: " + price + " VNĐ");

        // Cách 2: String Interpolation (hiện đại - KHUYẾN KHÍCH)
        // Dùng dấu $ trước chuỗi, biến đặt trong { }
        Console.WriteLine($"Sản phẩm: {product}");
        Console.WriteLine($"Đơn giá: {price:N0} VNĐ");       // N0 = format số có dấu phẩy
        Console.WriteLine($"Số lượng: {quantity}");
        Console.WriteLine($"Tổng tiền: {total:N0} VNĐ");

        // Cách 3: String.Format (ít dùng hơn)
        Console.WriteLine(String.Format("Tổng: {0:N0} VNĐ", total));
    }
}
```

### 🖥️ Output:

```
Sản phẩm: Laptop - Giá: 15990000 VNĐ
Sản phẩm: Laptop
Đơn giá: 15,990,000 VNĐ
Số lượng: 3
Tổng tiền: 47,970,000 VNĐ
Tổng: 47,970,000 VNĐ
```

### 📝 Giải thích:

- `$"..."` — **String Interpolation**: cho phép nhúng biến trực tiếp vào chuỗi bằng `{biến}`
- Giống **template literal** trong JS: `` `Tên: ${name}` ``
- `{price:N0}` — format số: `N` = number format, `0` = 0 chữ số thập phân
- `decimal` dùng cho tiền tệ vì **chính xác hơn** `double` (tránh lỗi làm tròn)

---

## Ví dụ 3: Sử dụng `var` — Type Inference

```csharp
using System;

class Program
{
    static void Main()
    {
        // Compiler tự suy ra kiểu dữ liệu
        var city = "Hà Nội";        // → string
        var population = 8000000;    // → int
        var area = 3358.6;           // → double
        var isCapital = true;        // → bool

        Console.WriteLine($"Thành phố: {city}");
        Console.WriteLine($"Dân số: {population:N0}");
        Console.WriteLine($"Diện tích: {area} km²");
        Console.WriteLine($"Là thủ đô: {isCapital}");

        // Kiểm tra kiểu dữ liệu thực tế
        Console.WriteLine();
        Console.WriteLine("=== KIỂM TRA KIỂU DỮ LIỆU ===");
        Console.WriteLine($"city là kiểu: {city.GetType().Name}");           // String
        Console.WriteLine($"population là kiểu: {population.GetType().Name}"); // Int32
        Console.WriteLine($"area là kiểu: {area.GetType().Name}");           // Double
        Console.WriteLine($"isCapital là kiểu: {isCapital.GetType().Name}"); // Boolean

        // ❌ Thử đổi kiểu — sẽ bị lỗi compile nếu uncomment:
        // city = 123;  // Error: Cannot implicitly convert type 'int' to 'string'
    }
}
```

### 🖥️ Output:

```
Thành phố: Hà Nội
Dân số: 8,000,000
Diện tích: 3358.6 km²
Là thủ đô: True

=== KIỂM TRA KIỂU DỮ LIỆU ===
city là kiểu: String
population là kiểu: Int32
area là kiểu: Double
isCapital là kiểu: Boolean
```

### 📝 Giải thích:

- `.GetType().Name` cho bạn biết **kiểu thực tế** của biến
- `Int32` chính là `int`, `Double` chính là `double` — C# có 2 cách gọi (alias)
- Dù dùng `var`, kiểu vẫn **cố định** sau khi compiler suy ra

---

## Ví dụ 4: Hằng số `const` và thay đổi giá trị biến

```csharp
using System;

class Program
{
    static void Main()
    {
        // === Hằng số — không thể thay đổi ===
        const double PI = 3.14159265;
        const string SCHOOL_NAME = "Đại học Bách Khoa";

        // === Biến — có thể thay đổi ===
        int score = 0;
        Console.WriteLine($"Điểm ban đầu: {score}");

        score = 85;  // Gán lại giá trị mới
        Console.WriteLine($"Điểm sau khi thi: {score}");

        score = score + 10;  // Cộng thêm 10 vào giá trị hiện tại
        Console.WriteLine($"Điểm sau khi cộng bonus: {score}");

        score += 5;  // Viết tắt: score = score + 5
        Console.WriteLine($"Điểm cuối cùng: {score}");

        // Tính chu vi hình tròn
        double radius = 5.0;
        double circumference = 2 * PI * radius;
        Console.WriteLine($"\nTrường: {SCHOOL_NAME}");
        Console.WriteLine($"Bán kính: {radius}");
        Console.WriteLine($"Chu vi hình tròn: {circumference:F2}");  // F2 = 2 chữ số thập phân

        // ❌ Nếu uncomment dòng dưới sẽ bị lỗi compile:
        // PI = 3.15;  // Error: The left-hand side of an assignment must be a variable
    }
}
```

### 🖥️ Output:

```
Điểm ban đầu: 0
Điểm sau khi thi: 85
Điểm sau khi cộng bonus: 95
Điểm cuối cùng: 100

Trường: Đại học Bách Khoa
Bán kính: 5
Chu vi hình tròn: 31.42
```

### 📝 Giải thích:

| Biểu thức | Ý nghĩa |
|-----------|---------|
| `score = score + 10` | Lấy giá trị hiện tại + 10, rồi gán lại |
| `score += 5` | Viết tắt — giống `score = score + 5` |
| `const double PI` | Hằng số — không thể thay đổi sau khi khai báo |
| `{circumference:F2}` | Format số thực với 2 chữ số sau dấu phẩy |

---

## Ví dụ 5: Nhiều biến cùng kiểu & Swap giá trị

```csharp
using System;

class Program
{
    static void Main()
    {
        // === Khai báo nhiều biến cùng kiểu trên 1 dòng ===
        int x = 10, y = 20, z = 30;
        Console.WriteLine($"x = {x}, y = {y}, z = {z}");

        // === Swap (đổi chỗ) 2 biến ===
        Console.WriteLine("\n--- Trước khi swap ---");
        Console.WriteLine($"x = {x}, y = {y}");

        // Cần biến tạm để lưu giá trị
        int temp = x;    // temp = 10
        x = y;           // x = 20
        y = temp;        // y = 10

        Console.WriteLine("--- Sau khi swap ---");
        Console.WriteLine($"x = {x}, y = {y}");

        // === Swap bằng Tuple (cách hiện đại C# 7+) ===
        Console.WriteLine("\n--- Swap bằng Tuple ---");
        (x, y) = (y, x);  // Đổi chỗ không cần biến tạm!
        Console.WriteLine($"x = {x}, y = {y}");
    }
}
```

### 🖥️ Output:

```
x = 10, y = 20, z = 30

--- Trước khi swap ---
x = 10, y = 20
--- Sau khi swap ---
x = 20, y = 10

--- Swap bằng Tuple ---
x = 10, y = 20
```

### 📝 Giải thích:

- **Swap cổ điển**: cần biến `temp` trung gian. Đây là bài toán kinh điển trong lập trình.
- **Swap hiện đại**: `(x, y) = (y, x)` — C# 7+ hỗ trợ Tuple deconstruction
- Trong JS, bạn có thể dùng: `[x, y] = [y, x]` — tương tự!

---

## Ví dụ 6: Ứng dụng thực tế — Tính hóa đơn nhà hàng

```csharp
using System;

class Program
{
    static void Main()
    {
        // === Thông tin đơn hàng ===
        string customerName = "Trần Thị B";
        const decimal TAX_RATE = 0.10m;       // Thuế 10%
        const decimal SERVICE_FEE = 0.05m;    // Phí dịch vụ 5%

        // Các món ăn
        decimal phoPrice = 55000m;
        int phoQuantity = 2;

        decimal coffeePrice = 35000m;
        int coffeeQuantity = 3;

        decimal cakePrice = 45000m;
        int cakeQuantity = 1;

        // === Tính toán ===
        decimal subtotal = (phoPrice * phoQuantity)
                         + (coffeePrice * coffeeQuantity)
                         + (cakePrice * cakeQuantity);

        decimal tax = subtotal * TAX_RATE;
        decimal service = subtotal * SERVICE_FEE;
        decimal total = subtotal + tax + service;

        // === In hóa đơn ===
        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║       HÓA ĐƠN NHÀ HÀNG         ║");
        Console.WriteLine("╠══════════════════════════════════╣");
        Console.WriteLine($"║ Khách hàng: {customerName,-20}║");
        Console.WriteLine("╠══════════════════════════════════╣");
        Console.WriteLine($"║ Phở x{phoQuantity}        {phoPrice * phoQuantity,14:N0} ║");
        Console.WriteLine($"║ Cà phê x{coffeeQuantity}     {coffeePrice * coffeeQuantity,14:N0} ║");
        Console.WriteLine($"║ Bánh x{cakeQuantity}       {cakePrice * cakeQuantity,14:N0} ║");
        Console.WriteLine("╠══════════════════════════════════╣");
        Console.WriteLine($"║ Tạm tính:    {subtotal,14:N0} VNĐ ║");
        Console.WriteLine($"║ Thuế (10%):  {tax,14:N0} VNĐ ║");
        Console.WriteLine($"║ Dịch vụ (5%):{service,14:N0} VNĐ ║");
        Console.WriteLine("╠══════════════════════════════════╣");
        Console.WriteLine($"║ TỔNG CỘNG:   {total,14:N0} VNĐ ║");
        Console.WriteLine("╚══════════════════════════════════╝");
    }
}
```

### 🖥️ Output:

```
╔══════════════════════════════════╗
║       HÓA ĐƠN NHÀ HÀNG         ║
╠══════════════════════════════════╣
║ Khách hàng: Trần Thị B          ║
╠══════════════════════════════════╣
║ Phở x2              110,000     ║
║ Cà phê x3           105,000     ║
║ Bánh x1              45,000     ║
╠══════════════════════════════════╣
║ Tạm tính:        260,000 VNĐ    ║
║ Thuế (10%):       26,000 VNĐ    ║
║ Dịch vụ (5%):     13,000 VNĐ    ║
╠══════════════════════════════════╣
║ TỔNG CỘNG:       299,000 VNĐ    ║
╚══════════════════════════════════╝
```

### 📝 Giải thích:

| Kỹ thuật | Ý nghĩa |
|----------|---------|
| `decimal` | Dùng cho tiền tệ — chính xác, không bị lỗi làm tròn |
| `const decimal TAX_RATE = 0.10m` | Hằng số — thuế suất không đổi |
| `{subtotal,14:N0}` | Format: `14` = chiều rộng tối thiểu (căn phải), `N0` = number format không thập phân |
| `{customerName,-20}` | `-20` = chiều rộng 20, **căn trái** (dấu trừ = căn trái) |

> 💡 **Thực tế:** Trong công ty, code tính tiền **luôn dùng `decimal`**, không bao giờ dùng `double` vì `double` có thể bị sai số khi tính toán tiền tệ.
