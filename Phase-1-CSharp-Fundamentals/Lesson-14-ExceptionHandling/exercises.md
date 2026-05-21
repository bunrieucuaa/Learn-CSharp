# ✏️ Lesson 14 — Bài tập: Exception Handling

---

## Bài 1: Bắt Exception cơ bản 🎯

### Yêu cầu:
Viết chương trình gây ra và bắt 6 loại exception:

```csharp
// 1. FormatException        → Parse string không phải số
// 2. DivideByZeroException  → Chia cho 0
// 3. IndexOutOfRangeException → Truy cập ngoài mảng
// 4. NullReferenceException → Truy cập null
// 5. OverflowException      → Vượt int.MaxValue (dùng checked{})
// 6. InvalidCastException   → Ép kiểu sai (boxing)
```

Với mỗi loại:
- Gây lỗi có chủ ý
- Bắt bằng catch CỤ THỂ
- In: tên exception + message + giải thích
- Chương trình KHÔNG được crash

### Expected Output:
```
─── Test 1: FormatException ───
❌ FormatException: "Input string was not in a correct format."
→ Nguyên nhân: Parse "abc" thành int

─── Test 2: DivideByZeroException ───
❌ DivideByZeroException: "Attempted to divide by zero."
→ Nguyên nhân: 10 / 0
...

✅ Tất cả 6 exception đã bắt, chương trình vẫn chạy!
```

---

## Bài 2: Robust Input Library 🛡️

### Yêu cầu:
Viết library input hoàn chỉnh dùng TryParse (KHÔNG dùng try-catch cho input):

```csharp
static class Input
{
    public static int ReadInt(string prompt);
    public static int ReadInt(string prompt, int min, int max);
    public static double ReadDouble(string prompt, double min, double max);
    public static decimal ReadMoney(string prompt);      // ≥ 0, format N0
    public static DateTime ReadDate(string prompt);      // dd/MM/yyyy
    public static string ReadNonEmpty(string prompt);
    public static string ReadChoice(string prompt, params string[] options);
    public static bool Confirm(string prompt);           // y/n
}
```

Test bằng form đăng ký:
- Tên (không rỗng)
- Tuổi (1-120)
- Email (chứa @)
- Ngày sinh (dd/MM/yyyy)
- Lương (≥ 0)
- Phòng ban (HR/IT/Sales)
- Xác nhận (y/n)

---

## Bài 3: throw — Method Validation 🔒

### Yêu cầu:
Viết các method có guard clause throw exception:

```csharp
static string CreateUsername(string firstName, string lastName);
// Throw nếu: null, rỗng, < 2 ký tự, chứa số

static decimal ApplyDiscount(decimal price, double percent);
// Throw nếu: price ≤ 0, percent < 0 hoặc > 100

static double CalculateBMI(double weight, double height);
// Throw nếu: weight ≤ 0 hoặc > 500, height ≤ 0 hoặc > 3

static int[] CreateRange(int start, int end);
// Throw nếu: start > end, range > 10000
```

Test mỗi method với input đúng VÀ sai, bắt exception và in rõ ràng.

---

## Bài 4: Exception Properties Explorer 🔍

### Yêu cầu:
Viết chương trình tạo 1 exception (ví dụ gọi method A → B → C → throw), rồi in TẤT CẢ properties:

```
═══ EXCEPTION DETAILS ═══
Type:       IndexOutOfRangeException
Message:    Index was outside the bounds of the array.
Source:     ConsoleApp1
HelpLink:   (null)
HResult:    -2146233080

Stack Trace:
   at Program.MethodC() in Program.cs:line 25
   at Program.MethodB() in Program.cs:line 20
   at Program.MethodA() in Program.cs:line 15
   at Program.Main() in Program.cs:line 8

Target Site: MethodC
Inner Exception: (none)
```

### Bonus: Tạo exception với InnerException:
```csharp
catch (Exception ex)
{
    throw new InvalidOperationException("Xử lý thất bại", ex);
    // ex trở thành InnerException
}
```

---

## Bài 5: Custom Exception — Banking System 🏦

### Yêu cầu:
Tạo 4 custom exceptions:

```csharp
class InsufficientFundsException : Exception { ... }
class AccountLockedException : Exception { ... }
class InvalidPINException : Exception { ... }
class TransferLimitException : Exception { ... }
```

Viết hệ thống rút tiền ATM sử dụng tất cả:

```
1. Nhập số TK → nếu không tồn tại → ArgumentException
2. Nhập PIN → nếu sai 3 lần → AccountLockedException
3. Nhập số tiền → nếu không đủ → InsufficientFundsException
4. Nếu rút > 50,000,000 → TransferLimitException
5. Thành công → in hóa đơn
```

Mỗi exception phải có property riêng (ví dụ `InsufficientFundsException` có `Balance` và `Amount`).
