# 💻 Lesson 14 — Ví dụ thực hành: Exception Handling

---

## Ví dụ 1: try-catch cơ bản — Xử lý input an toàn

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ TRY-CATCH CƠ BẢN ═══\n");

        // ── Demo 1: Parse lỗi ──
        Console.Write("Nhập 1 số: ");
        try
        {
            int number = int.Parse(Console.ReadLine()!);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Số hợp lệ: {number}");
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Không phải số!");
        }
        catch (OverflowException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Số quá lớn / quá nhỏ!");
        }
        finally
        {
            Console.ResetColor();
            Console.WriteLine("🔚 Input xử lý xong.\n");
        }

        // ── Demo 2: Chia cho 0 ──
        Console.Write("Nhập mẫu số: ");
        try
        {
            int divisor = int.Parse(Console.ReadLine()!);
            int result = 100 / divisor;
            Console.WriteLine($"100 / {divisor} = {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("❌ Không thể chia cho 0!");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Nhập số!");
        }

        // ── Demo 3: Array out of bounds ──
        int[] arr = { 10, 20, 30 };
        Console.Write($"\nNhập index (0-{arr.Length - 1}): ");
        try
        {
            int idx = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"arr[{idx}] = {arr[idx]}");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine($"❌ Index phải từ 0 đến {arr.Length - 1}!");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Nhập số!");
        }

        // ── Demo 4: Null reference ──
        string? name = null;
        try
        {
            Console.WriteLine($"Độ dài tên: {name!.Length}");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\n❌ NullReferenceException — name là null!");
            Console.WriteLine($"   Fix: name?.Length ?? 0 = {name?.Length ?? 0}");
        }
    }
}
```

---

## Ví dụ 2: TryParse Pattern — Input robust

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ TRYPARSE vs PARSE ═══\n");

        // ❌ Cách cũ: Parse + try-catch (chậm, xấu)
        Console.WriteLine("--- Cách cũ: Parse ---");
        Console.Write("Nhập tuổi: ");
        try
        {
            int age1 = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"Tuổi: {age1}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Lỗi format!");
        }

        // ✅ Cách mới: TryParse (nhanh, sạch)
        Console.WriteLine("\n--- Cách mới: TryParse ---");
        int age2 = ReadInt("Nhập tuổi: ", 0, 150);
        Console.WriteLine($"Tuổi: {age2}");

        double salary = ReadDouble("Nhập lương: ", 0, 1_000_000_000);
        Console.WriteLine($"Lương: {salary:N0}");

        // ✅ Custom TryParse
        Console.Write("\nNhập email: ");
        if (TryParseEmail(Console.ReadLine(), out string email))
            Console.WriteLine($"✅ Email: {email}");
        else
            Console.WriteLine("❌ Email không hợp lệ!");
    }

    // Robust input — dùng TryParse, không cần try-catch
    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int val) && val >= min && val <= max)
                return val;
            Console.WriteLine($"  ❌ Nhập số từ {min} đến {max}!");
        }
    }

    static double ReadDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double val) && val >= min && val <= max)
                return val;
            Console.WriteLine($"  ❌ Nhập số từ {min:N0} đến {max:N0}!");
        }
    }

    // Custom TryParse pattern
    static bool TryParseEmail(string? input, out string email)
    {
        email = "";
        if (string.IsNullOrWhiteSpace(input)) return false;
        input = input.Trim().ToLower();
        int at = input.IndexOf('@');
        if (at <= 0 || at >= input.Length - 3) return false;
        if (input.LastIndexOf('.') <= at) return false;
        email = input;
        return true;
    }
}
```

---

## Ví dụ 3: throw & Guard Clause

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ THROW & GUARD CLAUSE ═══\n");

        // Test 1: Input hợp lệ
        try
        {
            decimal price = CalculateTotal("iPhone 15", 25990000m, 2);
            Console.WriteLine($"Tổng: {price:N0} VNĐ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
        }

        // Test 2: Tên rỗng
        try
        {
            CalculateTotal("", 100, 1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"❌ {ex.GetType().Name}: {ex.Message}");
        }

        // Test 3: Giá âm
        try
        {
            CalculateTotal("Test", -100, 1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"❌ {ex.GetType().Name}: {ex.Message}");
        }

        // Test 4: Số lượng = 0
        try
        {
            CalculateTotal("Test", 100, 0);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"❌ {ex.GetType().Name}: {ex.Message}");
        }
    }

    static decimal CalculateTotal(string productName, decimal price, int quantity)
    {
        // Guard clauses — fail fast
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Tên sản phẩm không được rỗng!", nameof(productName));

        if (price <= 0)
            throw new ArgumentException("Giá phải > 0!", nameof(price));

        if (quantity <= 0 || quantity > 1000)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng phải từ 1-1000!");

        // Logic chính — chỉ đến đây nếu input OK
        decimal subtotal = price * quantity;
        decimal tax = subtotal * 0.10m;
        return subtotal + tax;
    }
}
```

---

## Ví dụ 4: Custom Exception

```csharp
using System;

// ── Custom Exceptions ──
class InsufficientBalanceException : Exception
{
    public decimal Balance { get; }
    public decimal Amount { get; }
    public decimal Shortage => Amount - Balance;

    public InsufficientBalanceException(decimal balance, decimal amount)
        : base($"Số dư {balance:N0} không đủ rút {amount:N0} (thiếu {amount - balance:N0})")
    {
        Balance = balance;
        Amount = amount;
    }
}

class AccountLockedException : Exception
{
    public string AccountId { get; }
    public AccountLockedException(string accountId)
        : base($"Tài khoản {accountId} đã bị khóa!") => AccountId = accountId;
}

// ── Bank Account Logic ──
class Program
{
    static string[] ids = { "ACC001", "ACC002" };
    static decimal[] balances = { 5000000m, 1000000m };
    static bool[] locked = { false, true };

    static void Main()
    {
        Console.WriteLine("═══ CUSTOM EXCEPTION ═══\n");

        // Test cases
        TestWithdraw("ACC001", 3000000m);  // ✅ OK
        TestWithdraw("ACC001", 5000000m);  // ❌ Thiếu tiền
        TestWithdraw("ACC002", 100000m);   // ❌ TK bị khóa
        TestWithdraw("ACC999", 100000m);   // ❌ TK không tồn tại
    }

    static void TestWithdraw(string accountId, decimal amount)
    {
        try
        {
            Withdraw(accountId, amount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Rút {amount:N0} từ {accountId} thành công!");
        }
        catch (AccountLockedException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"🔒 {ex.Message}");
        }
        catch (InsufficientBalanceException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"💸 {ex.Message}");
            Console.WriteLine($"   → Chỉ có thể rút tối đa: {ex.Balance:N0}");
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    static void Withdraw(string accountId, decimal amount)
    {
        // Tìm tài khoản
        int idx = Array.IndexOf(ids, accountId);
        if (idx == -1)
            throw new ArgumentException($"Tài khoản {accountId} không tồn tại!");

        // Kiểm tra khóa
        if (locked[idx])
            throw new AccountLockedException(accountId);

        // Kiểm tra số dư
        if (amount > balances[idx])
            throw new InsufficientBalanceException(balances[idx], amount);

        // Rút tiền
        balances[idx] -= amount;
    }
}
```

---

## Ví dụ 5: Ứng dụng tổng hợp — Calculator robust

```csharp
using System;

class Program
{
    static string[] history = new string[50];
    static int historyCount = 0;

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════╗");
        Console.WriteLine("║   🧮 ROBUST CALCULATOR    ║");
        Console.WriteLine("╚═══════════════════════════╝");
        Console.ResetColor();

        string? choice;
        do
        {
            Console.WriteLine("\n1. Tính  2. Lịch sử  0. Thoát");
            Console.Write("Chọn: ");
            choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": Calculate(); break;
                case "2": ShowHistory(); break;
            }
        } while (choice != "0");
    }

    static void Calculate()
    {
        try
        {
            double a = ReadNumber("Số A: ");
            char op = ReadOperator();
            double b = ReadNumber("Số B: ");

            double result = Compute(a, b, op);

            string entry = $"{a} {op} {b} = {result:G10}";
            AddHistory(entry);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  → {entry}");
            Console.ResetColor();
        }
        catch (DivideByZeroException)
        {
            PrintError("Không thể chia cho 0!");
        }
        catch (ArgumentException ex)
        {
            PrintError(ex.Message);
        }
        catch (OverflowException)
        {
            PrintError("Kết quả quá lớn!");
        }
    }

    static double Compute(double a, double b, char op)
    {
        return op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => b != 0 ? a / b : throw new DivideByZeroException(),
            '%' => b != 0 ? a % b : throw new DivideByZeroException(),
            '^' => Math.Pow(a, b),
            _ => throw new ArgumentException($"Phép tính '{op}' không hợp lệ!")
        };
    }

    static double ReadNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double val))
                return val;
            PrintError("Phải nhập số!");
        }
    }

    static char ReadOperator()
    {
        char[] valid = { '+', '-', '*', '/', '%', '^' };
        while (true)
        {
            Console.Write("Phép tính (+,-,*,/,%,^): ");
            string? input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input) && Array.IndexOf(valid, input[0]) >= 0)
                return input[0];
            PrintError("Phép tính không hợp lệ!");
        }
    }

    static void AddHistory(string entry)
    {
        if (historyCount < history.Length)
        {
            history[historyCount] = entry;
            historyCount++;
        }
    }

    static void ShowHistory()
    {
        if (historyCount == 0) { PrintError("Chưa có lịch sử!"); return; }
        Console.WriteLine($"\n📋 Lịch sử ({historyCount}):");
        for (int i = historyCount - 1; i >= 0; i--)
            Console.WriteLine($"  {historyCount - i,2}. {history[i]}");
    }

    static void PrintError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ❌ {msg}");
        Console.ResetColor();
    }
}
```

### 📝 Bài học:

| Exception | Nơi bắt | Xử lý |
|-----------|---------|--------|
| `DivideByZeroException` | `Calculate()` | In lỗi, tiếp tục |
| `ArgumentException` | `Calculate()` | In lỗi operator sai |
| `OverflowException` | `Calculate()` | In lỗi số quá lớn |
| **Không bắt:** Input sai | `ReadNumber()` | Dùng `TryParse` + loop → không cần exception |

> 🔑 **Kết hợp TryParse + Exception**: TryParse cho input user (bình thường), Exception cho lỗi logic (bất thường).
