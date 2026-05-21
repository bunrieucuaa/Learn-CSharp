# ✏️ Lesson 10 — Bài tập: Static

---

## Bài 1: Dự đoán Output 🔮

### Yêu cầu:
Dự đoán output **trên giấy** trước khi chạy:

**Câu A:**
```csharp
class Counter
{
    static int count = 0;
    public static void Add() => count++;
    public static int Get() => count;
}

Counter.Add(); //1
Counter.Add(); //2
Counter.Add(); //3
Console.WriteLine(Counter.Get());  // 3

Counter.Add(); //4
Console.WriteLine(Counter.Get());  // 4
```

**Câu B:**
```csharp
class Foo
{
    static int x = 10;
    int y = 20;

    static void StaticMethod()
    {
        Console.WriteLine(x);     // ?
        // Console.WriteLine(y);  // Chạy được không? Tại sao?
    }
}
```

**Câu C:**
```csharp
class IdGen
{
    static int id = 0;
    public static int Next() => ++id;
}

Console.WriteLine(IdGen.Next());  // 1
Console.WriteLine(IdGen.Next());  // 2
Console.WriteLine(IdGen.Next());  // 3
```

---

## Bài 2: Static Class — Thư viện Validator 🛡️

### Yêu cầu:
Tạo `static class Validator` với các method:

```csharp
static class Validator
{
    public static bool IsValidAge(int age) => age > 0 && age < 150;
    public static bool IsValidName(string? name){
        if(string.IsNullOrEmpty(name) && name.Length >= 2 && name.Length <= 50) return true;
        else return false;
    };        // Không rỗng, 2-50 ký tự
    public static bool IsValidEmail(string? email){
        if(email.Contains('@') && email.Contains('.')) return true;
        else return false;
    };      // Chứa @ và .
    public static bool IsValidPhone(string? phone){
        if(phone.Length == 10 && phone.StartWith("0")) return true;
        else return false;
    };      // 10 số, bắt đầu 0
    public static bool IsValidPassword(string? pass){
        
    };
    // ≥8, có hoa+thường+số
    public static bool IsInRange(int value, int min, int max){
        if(value >= min && value <= max) return true;
        else return false;
    };
    public static bool IsInRange(double value, double min, double max)  {
        if(value >= min && value <= max) return true;
        else return false;
    };  // Overload!
}
```

Test tất cả method với nhiều input (đúng + sai).

---

## Bài 3: Static Field — Hệ thống đếm 📊

### Yêu cầu:
Tạo class `OrderTracker` có:

```csharp
class OrderTracker
{
    // Static fields
    static int totalOrders = 0;
    static decimal totalRevenue = 0m;
    static string[] orderHistory = new string[100];

    // Static methods
    public static string CreateOrder(string product, decimal price, int qty);
    // → Tự sinh mã: ORD0001, ORD0002...
    // → Cộng dồn revenue
    // → Lưu vào history
    // → Return mã đơn

    public static void ShowSummary();
    // → Tổng đơn, tổng doanh thu, TB/đơn

    public static void ShowHistory();
    // → Liệt kê tất cả đơn hàng
}
```

Test: tạo 5 đơn hàng, xem summary, xem history.

---

## Bài 4: Static Architecture — Tách 4 static class 🏗️

### Yêu cầu:
Viết chương trình quản lý sinh viên, tách thành 4 static classes:

```
static class Config     → Hằng số (MAX, tên trường...)
static class Data       → Mảng data + CRUD methods
static class UI         → Hiển thị menu, bảng, thông báo
static class Logic      → Tính toán (GPA, xếp loại, thống kê)
```

**Chức năng:**
- Thêm sinh viên (tên, 3 điểm)
- Xem danh sách (bảng đẹp)
- Tính GPA tự động
- Xếp loại (Giỏi/Khá/TB/Yếu)
- Thống kê (TB, max, min, count theo loại)

**Quy tắc:**
- `Config` chỉ chứa `const`
- `Data` chỉ chứa mảng + CRUD
- `UI` chỉ chứa Console.Write/Read
- `Logic` chỉ chứa tính toán (không Write/Read)
- `Main()` ≤ 15 dòng

---

## Bài 5: So sánh Static vs Local — Thí nghiệm 🧪

### Yêu cầu:
Viết chương trình chứng minh sự khác biệt:

```csharp
class Experiment
{
    static int staticVar = 0;

    public static void TestStatic()
    {
        staticVar++;
        Console.WriteLine($"Static: {staticVar}");
    }

    public static void TestLocal()
    {
        int localVar = 0;
        localVar++;
        Console.WriteLine($"Local: {localVar}");
    }
}
```

Gọi mỗi method 5 lần, so sánh kết quả, giải thích TẠI SAO khác nhau.

In bảng so sánh:

```
│ Lần gọi │ Static │ Local │
├──────────┼────────┼───────┤
│    1     │   1    │   1   │
│    2     │   2    │   1   │
│    3     │   3    │   1   │
│    4     │   4    │   1   │
│    5     │   5    │   1   │
```
