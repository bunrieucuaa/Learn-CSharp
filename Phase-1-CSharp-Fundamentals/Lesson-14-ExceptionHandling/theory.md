# 📘 Lesson 14 — Exception Handling (Xử lý ngoại lệ) trong C#

---

## 1. Exception là gì?

**Exception** = lỗi xảy ra **lúc chạy** (runtime), khiến chương trình dừng đột ngột nếu không xử lý.

```csharp
int[] arr = { 1, 2, 3 };
Console.WriteLine(arr[10]);    // 💥 IndexOutOfRangeException — CRASH!
```

```
Không có Exception Handling:
  Input → Xử lý → 💥 CRASH → User mất data, app tắt

Có Exception Handling:
  Input → Xử lý → ⚠️ Lỗi → Bắt lỗi → Thông báo → Tiếp tục chạy
```

> 🔑 Exception Handling không **tránh** lỗi, mà **kiểm soát** khi lỗi xảy ra.

---

## 2. Các Exception phổ biến

| Exception | Nguyên nhân | Ví dụ |
|-----------|-------------|-------|
| `NullReferenceException` | Truy cập object = null | `string? s = null; s.Length;` |
| `IndexOutOfRangeException` | Index ngoài mảng | `arr[arr.Length]` |
| `FormatException` | Parse sai format | `int.Parse("abc")` |
| `InvalidCastException` | Ép kiểu sai | `(int)(object)"hello"` |
| `DivideByZeroException` | Chia cho 0 | `10 / 0` |
| `OverflowException` | Vượt giới hạn kiểu | `checked { int.MaxValue + 1 }` |
| `StackOverflowException` | Đệ quy vô tận | `void F() { F(); }` |
| `FileNotFoundException` | File không tồn tại | `File.ReadAllText("x.txt")` |
| `ArgumentException` | Tham số không hợp lệ | Logic sai |
| `ArgumentNullException` | Tham số = null | Truyền null vào method |

---

## 3. `try-catch` — Bắt Exception

### Cú pháp cơ bản:

```csharp
try
{
    // Code có thể gây lỗi
    int result = 10 / 0;
}
catch (Exception ex)
{
    // Xử lý khi lỗi xảy ra
    Console.WriteLine($"Lỗi: {ex.Message}");
}
```

### Bắt exception cụ thể:

```csharp
try
{
    Console.Write("Nhập số: ");
    int number = int.Parse(Console.ReadLine()!);
    int result = 100 / number;
    Console.WriteLine($"100 / {number} = {result}");
}
catch (FormatException)
{
    Console.WriteLine("❌ Không phải số!");
}
catch (DivideByZeroException)
{
    Console.WriteLine("❌ Không thể chia cho 0!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Lỗi khác: {ex.Message}");
}
```

### Thứ tự catch QUAN TRỌNG:

```csharp
// ✅ Cụ thể trước, chung sau
catch (FormatException ex) { ... }       // Cụ thể
catch (ArgumentException ex) { ... }     // Cụ thể hơn Exception
catch (Exception ex) { ... }             // Bắt tất cả — LUÔN CUỐI CÙNG

// ❌ SAI — Exception bắt hết, các catch sau vô dụng
// catch (Exception ex) { ... }
// catch (FormatException ex) { ... }  // Không bao giờ chạy!
```

> 🔑 Catch **cụ thể trước**, `Exception` (chung) **luôn cuối cùng** (hoặc không dùng).

---

## 4. `finally` — Luôn chạy dù có lỗi hay không

```csharp
try
{
    Console.WriteLine("Đang xử lý...");
    int result = 10 / 0;  // 💥 Lỗi
    Console.WriteLine("Xong!");  // ← KHÔNG chạy
}
catch (DivideByZeroException)
{
    Console.WriteLine("❌ Chia cho 0!");
}
finally
{
    Console.WriteLine("🔚 Finally — LUÔN chạy!");
    // Dùng cho: đóng file, đóng connection, dọn dẹp
}
```

### Output:

```
Đang xử lý...
❌ Chia cho 0!
🔚 Finally — LUÔN chạy!
```

### Khi nào cần `finally`?

| Tình huống | Dùng finally cho |
|-----------|------------------|
| Đọc file | Đóng file |
| Kết nối database | Đóng connection |
| Lock tài nguyên | Unlock |
| Hiển thị loading | Ẩn loading spinner |

---

## 5. `throw` — Ném Exception

### 5.1 Ném lại exception:

```csharp
try
{
    DoSomething();
}
catch (Exception ex)
{
    Console.WriteLine($"Logged: {ex.Message}");
    throw;  // Ném lại cho caller xử lý tiếp
    // throw ex;  // ⚠️ Cũng được nhưng MẤT stack trace gốc!
}
```

### 5.2 Ném exception mới:

```csharp
static void SetAge(int age)
{
    if (age < 0)
        throw new ArgumentException("Tuổi không thể âm!");

    if (age > 150)
        throw new ArgumentOutOfRangeException(nameof(age), "Tuổi không hợp lệ!");

    Console.WriteLine($"Tuổi: {age}");
}

// Gọi:
try
{
    SetAge(-5);  // 💥 ArgumentException
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Lỗi: {ex.Message}");
}
```

### 5.3 Guard Clause với throw:

```csharp
static decimal CalculateDiscount(decimal price, int quantity)
{
    // Guard clauses — validate đầu vào
    if (price <= 0)
        throw new ArgumentException("Giá phải > 0", nameof(price));
    if (quantity <= 0)
        throw new ArgumentException("Số lượng phải > 0", nameof(quantity));

    // Logic chính — chỉ đến đây nếu input hợp lệ
    decimal total = price * quantity;
    return total > 1000000 ? total * 0.10m : 0m;
}
```

---

## 6. Custom Exception — Tạo exception riêng

```csharp
// Kế thừa Exception (sẽ học kỹ ở OOP)
class InsufficientBalanceException : Exception
{
    public decimal CurrentBalance { get; }
    public decimal RequestedAmount { get; }

    public InsufficientBalanceException(decimal balance, decimal amount)
        : base($"Số dư {balance:N0} không đủ để rút {amount:N0}")
    {
        CurrentBalance = balance;
        RequestedAmount = amount;
    }
}

// Sử dụng:
static void Withdraw(decimal balance, decimal amount)
{
    if (amount > balance)
        throw new InsufficientBalanceException(balance, amount);

    Console.WriteLine($"Rút thành công {amount:N0}. Còn: {balance - amount:N0}");
}

try
{
    Withdraw(500000, 1000000);
}
catch (InsufficientBalanceException ex)
{
    Console.WriteLine($"❌ {ex.Message}");
    Console.WriteLine($"   Thiếu: {ex.RequestedAmount - ex.CurrentBalance:N0}");
}
```

---

## 7. Exception Properties

```csharp
try
{
    int[] arr = { 1, 2, 3 };
    Console.WriteLine(arr[10]);
}
catch (Exception ex)
{
    Console.WriteLine($"Message:    {ex.Message}");
    Console.WriteLine($"Type:       {ex.GetType().Name}");
    Console.WriteLine($"Source:     {ex.Source}");
    Console.WriteLine($"StackTrace: {ex.StackTrace}");

    // InnerException — exception bên trong (nếu có)
    if (ex.InnerException != null)
        Console.WriteLine($"Inner: {ex.InnerException.Message}");
}
```

---

## 8. `TryParse` Pattern — Tránh Exception khi parse

```csharp
// ❌ Dùng Parse → có thể throw FormatException
try
{
    int n = int.Parse("abc");  // 💥
}
catch (FormatException) { }

// ✅ Dùng TryParse → KHÔNG throw, trả bool
if (int.TryParse("abc", out int n))
    Console.WriteLine($"Số: {n}");
else
    Console.WriteLine("Không phải số!");
```

### Try pattern cho method riêng:

```csharp
// Áp dụng pattern "Try" cho method của mình
static bool TryParseEmail(string? input, out string email)
{
    email = "";
    if (string.IsNullOrWhiteSpace(input)) return false;

    input = input.Trim();
    int atIndex = input.IndexOf('@');
    if (atIndex <= 0 || atIndex >= input.Length - 1) return false;
    if (!input.Contains('.')) return false;

    email = input.ToLower();
    return true;
}

// Gọi:
if (TryParseEmail("user@mail.com", out string email))
    Console.WriteLine($"Email hợp lệ: {email}");
else
    Console.WriteLine("Email không hợp lệ!");
```

---

## 9. Khi nào dùng Exception vs Validation?

### ✅ Dùng Exception cho:
- Lỗi **không lường trước được** (file không tồn tại, network lỗi)
- Lỗi **nghiêm trọng** cần báo cho caller
- Vi phạm **contract** (method nhận input sai logic)

### ✅ Dùng Validation cho:
- Input từ user (có thể sai — bình thường!)
- Kiểm tra điều kiện đơn giản
- Tránh tạo exception không cần thiết

```csharp
// ❌ Dùng exception cho validation thông thường — CHẬM, thừa
try
{
    int age = int.Parse(Console.ReadLine()!);
    if (age < 0) throw new Exception("Tuổi âm");
}
catch { ... }

// ✅ Dùng validation + TryParse
if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
    Console.WriteLine($"Tuổi: {age}");
else
    Console.WriteLine("Tuổi không hợp lệ!");
```

> 🔑 **Exception = expensive (tốn tài nguyên)**. Không dùng cho control flow bình thường!

---

## 10. So sánh với JavaScript

```javascript
// JS
try {
    JSON.parse("invalid");
} catch (error) {
    console.log(error.message);
} finally {
    console.log("Done");
}

// Throw
throw new Error("Something wrong");

// Custom error
class MyError extends Error {
    constructor(msg) { super(msg); this.name = "MyError"; }
}
```

```csharp
// C#
try
{
    int.Parse("invalid");
}
catch (FormatException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Done");
}

throw new ArgumentException("Something wrong");
```

| | C# | JavaScript |
|--|-----|-----------|
| Cú pháp | `try-catch-finally` | `try-catch-finally` |
| Kiểu exception | Typed (class hierarchy) | Untyped (thường `Error`) |
| Catch cụ thể | ✅ `catch (FormatException)` | ❌ Chỉ `catch (e)` |
| Custom | `class X : Exception` | `class X extends Error` |
| `finally` | ✅ Giống | ✅ Giống |
| `throw` | `throw new Exception()` | `throw new Error()` |
| Try pattern | `TryParse` (built-in) | Không có (tự viết) |

> 🔑 **C# mạnh hơn**: catch được exception CỤ THỂ → xử lý chính xác hơn.

---

## 11. Sai lầm phổ biến

### ❌ Catch rồi nuốt (swallow):
```csharp
try { ... }
catch (Exception) { }  // ❌ Nuốt lỗi! Không biết đã lỗi gì!

// ✅ Ít nhất log lại
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] {ex.Message}");
}
```

### ❌ Catch Exception quá rộng:
```csharp
// ❌ Bắt hết — không biết lỗi gì cụ thể
catch (Exception ex) { Console.WriteLine(ex.Message); }

// ✅ Catch cụ thể trước
catch (FileNotFoundException) { /* file missing */ }
catch (UnauthorizedAccessException) { /* no permission */ }
catch (Exception ex) { /* fallback */ }
```

### ❌ Dùng exception cho validation:
```csharp
// ❌ CHẬM
try { int age = int.Parse(input); }
catch { Console.WriteLine("Sai!"); }

// ✅ NHANH
if (!int.TryParse(input, out int age))
    Console.WriteLine("Sai!");
```

### ❌ `throw ex` thay vì `throw`:
```csharp
catch (Exception ex)
{
    // throw ex;   // ❌ MẤT stack trace gốc!
    throw;         // ✅ GIỮ stack trace gốc
}
```

---

## 12. Best Practices

1. **Catch cụ thể** — `FormatException`, `ArgumentException` trước `Exception`
2. **Không nuốt exception** — ít nhất log `ex.Message`
3. **`TryParse` thay `Parse`** cho user input
4. **`throw` thay `throw ex`** để giữ stack trace
5. **`finally` cho cleanup** — đóng file, connection
6. **Guard clause + throw** cho method validation
7. **Custom exception** khi cần thông tin thêm
8. **Không dùng exception cho control flow** — tốn performance
9. **Validate sớm** — fail fast, thông báo rõ ràng
10. **`using` statement** cho resource cleanup (sẽ học thêm ở Phase 5)
