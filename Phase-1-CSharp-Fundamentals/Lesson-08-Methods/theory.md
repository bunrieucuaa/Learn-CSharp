# 📘 Lesson 08 — Methods (Phương thức / Hàm) trong C#

---

## 1. Method là gì?

Method = **khối code được đặt tên**, có thể gọi lại nhiều lần. Giống **function** trong JavaScript.

```csharp
// Không có method → code lặp lại
Console.WriteLine("──────────────");
Console.WriteLine("  Xin chào!");
Console.WriteLine("──────────────");
// ... 50 dòng sau ...
Console.WriteLine("──────────────");
Console.WriteLine("  Xin chào!");
Console.WriteLine("──────────────");

// Có method → gọi lại
void SayHello()
{
    Console.WriteLine("──────────────");
    Console.WriteLine("  Xin chào!");
    Console.WriteLine("──────────────");
}

SayHello();  // Gọi lần 1
SayHello();  // Gọi lần 2
```

### Tại sao cần Method?

| Lý do | Giải thích |
|-------|-----------|
| **DRY** (Don't Repeat Yourself) | Viết 1 lần, dùng nhiều lần |
| **Đọc dễ** | `CalculateTotal()` rõ ràng hơn 20 dòng tính toán |
| **Dễ sửa** | Sửa 1 chỗ, có hiệu lực khắp nơi |
| **Dễ test** | Test từng method riêng lẻ |

---

## 2. Cú pháp khai báo Method

```csharp
access_modifier return_type MethodName(parameter_list)
{
    // Body
    return value;  // Nếu return_type không phải void
}
```

### Các thành phần:

```csharp
static int Add(int a, int b)
//  ↑    ↑    ↑      ↑
//  |    |    |      └── Parameters (tham số)
//  |    |    └── Tên method (PascalCase!)
//  |    └── Return type (kiểu trả về)
//  └── Access modifier (tạm thời dùng static)
{
    return a + b;  // Trả về kết quả
}

// Gọi:
int result = Add(5, 3);  // result = 8
//               ↑  ↑
//               └──└── Arguments (đối số truyền vào)
```

> 💡 **Parameter** = biến khai báo trong `()`. **Argument** = giá trị thực truyền vào khi gọi.

---

## 3. Các loại Method

### 3.1 Không trả về, không tham số: `void`

```csharp
static void PrintLine()
{
    Console.WriteLine("════════════════════");
}

PrintLine();  // In đường kẻ
```

### 3.2 Không trả về, có tham số:

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Xin chào {name}!");
}

Greet("Minh");   // Xin chào Minh!
Greet("Hùng");   // Xin chào Hùng!
```

### 3.3 Có trả về, có tham số:

```csharp
static double CalculateBMI(double weight, double height)
{
    return weight / (height * height);
}

double bmi = CalculateBMI(70, 1.75);
Console.WriteLine($"BMI: {bmi:F1}");  // BMI: 22.9
```

### 3.4 Return nhiều giá trị — Tuple:

```csharp
static (double min, double max, double avg) GetStats(double[] scores)
{
    double min = scores[0], max = scores[0], sum = 0;
    foreach (double s in scores)
    {
        if (s < min) min = s;
        if (s > max) max = s;
        sum += s;
    }
    return (min, max, sum / scores.Length);
}

// Gọi:
var stats = GetStats(new double[] { 7, 8.5, 6, 9 });
Console.WriteLine($"Min: {stats.min}, Max: {stats.max}, Avg: {stats.avg:F1}");
```

### So sánh với JavaScript:

```javascript
// JS — function
function add(a, b) {
    return a + b;
}
const add = (a, b) => a + b;  // Arrow function

// JS — return nhiều giá trị bằng object
function getStats(scores) {
    return { min: 0, max: 10, avg: 7.5 };
}
```

```csharp
// C# — method
static int Add(int a, int b)
{
    return a + b;
}

// C# — return tuple
static (int min, int max) GetMinMax(int[] arr) { ... }
```

| JavaScript | C# |
|-----------|-----|
| `function add(a, b)` | `static int Add(int a, int b)` |
| Không cần khai báo kiểu | PHẢI khai báo kiểu tham số + return |
| Arrow function `=>` | Expression-bodied `=>` (1 dòng) |
| Return object | Return tuple |

---

## 4. Expression-Bodied Method (1 dòng)

```csharp
// Method thường
static int Double(int x)
{
    return x * 2;
}

// Expression-bodied — gọn hơn (khi chỉ 1 biểu thức)
static int Double(int x) => x * 2;

static string GetGrade(double s) => s >= 5 ? "Đậu" : "Rớt";

static void PrintLine() => Console.WriteLine("─────────────");
```

> 💡 Giống **arrow function** trong JS: `const double = (x) => x * 2;`

---

## 5. Parameters nâng cao

### 5.1 Default Parameters (Giá trị mặc định):

```csharp
static decimal CalculateTotal(decimal price, int qty = 1, decimal taxRate = 0.10m)
{
    return price * qty * (1 + taxRate);
}

CalculateTotal(100000m);              // qty=1, taxRate=0.10
CalculateTotal(100000m, 3);           // qty=3, taxRate=0.10
CalculateTotal(100000m, 3, 0.08m);    // qty=3, taxRate=0.08
```

> ⚠️ Tham số có default **phải đặt SAU** tham số không có default!

### 5.2 Named Parameters:

```csharp
// Gọi bằng tên — rõ ràng hơn
CalculateTotal(price: 100000m, qty: 5, taxRate: 0.05m);

// Bỏ qua tham số giữa
CalculateTotal(100000m, taxRate: 0.05m);  // qty dùng default = 1
```

### 5.3 `params` — Số lượng tham số không cố định:

```csharp
static double Average(params double[] numbers)
{
    double sum = 0;
    foreach (double n in numbers)
        sum += n;
    return sum / numbers.Length;
}

// Gọi với bao nhiêu tham số cũng được
double avg1 = Average(7, 8, 9);           // 8.0
double avg2 = Average(5, 6, 7, 8, 9, 10); // 7.5
```

### 5.4 `out` — Trả về qua tham số:

```csharp
static bool TryDivide(int a, int b, out double result)
{
    if (b == 0)
    {
        result = 0;  // Phải gán giá trị cho out
        return false;
    }
    result = (double)a / b;
    return true;
}

// Gọi:
if (TryDivide(10, 3, out double result))
    Console.WriteLine($"Kết quả: {result:F2}");  // 3.33
```

> 💡 Pattern `TryXxx(input, out result)` rất phổ biến: `int.TryParse()`, `Dictionary.TryGetValue()`...

### 5.5 `ref` — Tham chiếu (sửa biến bên ngoài):

```csharp
static void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

int x = 10, y = 20;
Swap(ref x, ref y);
Console.WriteLine($"x={x}, y={y}");  // x=20, y=10
```

### So sánh `out`, `ref`, giá trị thường:

| | Giá trị thường | `ref` | `out` |
|--|---------------|-------|-------|
| Phải khởi tạo trước | ✅ | ✅ | ❌ |
| Method PHẢI gán giá trị | ❌ | ❌ | ✅ |
| Sửa ảnh hưởng bên ngoài | ❌ | ✅ | ✅ |
| Dùng khi | Truyền data vào | Sửa data 2 chiều | Trả thêm giá trị |

---

## 6. Method Overloading — Nạp chồng

Nhiều method **cùng tên**, khác **tham số**:

```csharp
static int Add(int a, int b) => a + b;
static double Add(double a, double b) => a + b;
static int Add(int a, int b, int c) => a + b + c;
static string Add(string a, string b) => a + " " + b;

// C# tự chọn đúng method dựa trên tham số
Console.WriteLine(Add(5, 3));          // 8 — gọi Add(int, int)
Console.WriteLine(Add(5.5, 3.2));      // 8.7 — gọi Add(double, double)
Console.WriteLine(Add(1, 2, 3));       // 6 — gọi Add(int, int, int)
Console.WriteLine(Add("Hello", "World")); // "Hello World" — gọi Add(string, string)
```

> 💡 **JavaScript không có overloading!** JS chỉ giữ bản khai báo cuối cùng. C# phân biệt bằng **số lượng và kiểu** tham số.

---

## 7. Recursion — Đệ quy

Method gọi **chính nó**:

```csharp
static int Factorial(int n)
{
    if (n <= 1) return 1;       // Base case — điều kiện dừng
    return n * Factorial(n - 1); // Recursive case — gọi chính mình
}

Console.WriteLine(Factorial(5));  // 120 = 5 × 4 × 3 × 2 × 1
```

### Phân tích:

```
Factorial(5)
= 5 × Factorial(4)
= 5 × 4 × Factorial(3)
= 5 × 4 × 3 × Factorial(2)
= 5 × 4 × 3 × 2 × Factorial(1)
= 5 × 4 × 3 × 2 × 1
= 120
```

### ⚠️ PHẢI có base case:

```csharp
// ❌ Không có base case → StackOverflowException!
static int Bad(int n)
{
    return n * Bad(n - 1);  // Gọi mãi, không bao giờ dừng!
}
```

---

## 8. Local Functions — Hàm cục bộ (C# 7+)

Method nằm **bên trong** method khác:

```csharp
static void ProcessOrder()
{
    decimal total = CalculateSubtotal(5, 20000m);
    decimal tax = CalculateTax(total);
    decimal final_ = total + tax;

    Console.WriteLine($"Tổng: {final_:N0}");

    // Local functions — chỉ dùng trong ProcessOrder
    decimal CalculateSubtotal(int qty, decimal price) => qty * price;
    decimal CalculateTax(decimal amount) => amount * 0.10m;
}
```

> 💡 Hữu ích khi helper logic chỉ dùng trong 1 method → không cần tạo method riêng ở class level.

---

## 9. Sai lầm phổ biến

### ❌ Method quá dài (> 30 dòng):
```csharp
// ❌ 1 method làm mọi thứ
static void DoEverything() { /* 200 dòng */ }

// ✅ Tách nhỏ
static void ReadInput() { ... }
static decimal Calculate() { ... }
static void DisplayResult() { ... }
```

### ❌ Quên `return`:
```csharp
static int Add(int a, int b)
{
    int sum = a + b;
    // ❌ Quên return! → Compile error
}
```

### ❌ Method làm quá nhiều việc:
```csharp
// ❌ Vừa tính, vừa in, vừa lưu file
static void CalculateAndPrintAndSave() { ... }

// ✅ Single Responsibility
static decimal Calculate() { ... }
static void Print(decimal result) { ... }
static void Save(decimal result) { ... }
```

---

## 10. Best Practices

1. **Tên method = động từ + danh từ**: `CalculateTotal()`, `GetUserName()`, `IsValid()`
2. **PascalCase** cho method public: `PrintReport()` (khác JS dùng camelCase)
3. **Một method làm MỘT việc** (Single Responsibility)
4. **Tối đa 3-4 tham số** — nhiều hơn → dùng object
5. **Tối đa 20-30 dòng** — dài hơn → tách method con
6. **Method nên `return` giá trị** hơn là `Console.WriteLine` trực tiếp
7. **Dùng default params** thay vì nhiều overload tương tự
8. **Expression-bodied** `=>` cho method 1 dòng
