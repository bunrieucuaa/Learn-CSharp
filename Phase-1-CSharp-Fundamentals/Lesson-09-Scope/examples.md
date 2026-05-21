# 💻 Lesson 09 — Ví dụ thực hành: Scope & Lifetime

---

## Ví dụ 1: Minh họa các loại Scope

```csharp
using System;

class Program
{
    // ===== CLASS SCOPE =====
    static int globalCounter = 0;
    static string appName = "Scope Demo";

    static void Main()
    {
        Console.WriteLine($"=== {appName} ===\n");

        // ===== METHOD SCOPE =====
        int localVar = 100;
        Console.WriteLine($"[Main] localVar = {localVar}");
        Console.WriteLine($"[Main] globalCounter = {globalCounter}");

        // ===== BLOCK SCOPE =====
        if (localVar > 50)
        {
            int blockVar = 200;
            Console.WriteLine($"[Block] blockVar = {blockVar}");
            Console.WriteLine($"[Block] localVar = {localVar}");  // ✅ Thấy scope cha
            globalCounter++;
        }
        // Console.WriteLine(blockVar);  // ❌ blockVar đã chết!

        // ===== LOOP SCOPE =====
        for (int i = 0; i < 3; i++)
        {
            int loopTemp = i * 10;
            Console.WriteLine($"[Loop] i={i}, temp={loopTemp}");
            globalCounter++;
        }
        // Console.WriteLine(i);         // ❌ i đã chết!

        Console.WriteLine($"\n[Main] globalCounter = {globalCounter}");

        // Gọi method khác
        AnotherMethod();
        Console.WriteLine($"[Main] globalCounter sau AnotherMethod = {globalCounter}");
    }

    static void AnotherMethod()
    {
        // int localVar = 999;         // ✅ OK — khác method, cùng tên không sao
        Console.WriteLine($"[Another] globalCounter = {globalCounter}");
        globalCounter += 10;
        // Console.WriteLine(localVar); // ❌ localVar của Main() không thấy ở đây!
    }
}
```

### 📝 Output:

```
=== Scope Demo ===

[Main] localVar = 100
[Main] globalCounter = 0
[Block] blockVar = 200
[Block] localVar = 100
[Loop] i=0, temp=0
[Loop] i=1, temp=10
[Loop] i=2, temp=20

[Main] globalCounter = 4
[Another] globalCounter = 4
[Main] globalCounter sau AnotherMethod = 14
```

---

## Ví dụ 2: Value Type vs Reference Type khi truyền vào Method

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VALUE vs REFERENCE ===\n");

        // --- Value Type ---
        int number = 10;
        Console.WriteLine($"Trước ModifyValue: number = {number}");
        ModifyValue(number);
        Console.WriteLine($"Sau ModifyValue:   number = {number}");
        Console.WriteLine("→ Không đổi! Value type truyền bản COPY.\n");

        // --- Value Type + ref ---
        int score = 100;
        Console.WriteLine($"Trước ModifyRef: score = {score}");
        ModifyRef(ref score);
        Console.WriteLine($"Sau ModifyRef:   score = {score}");
        Console.WriteLine("→ ĐÃ đổi! ref truyền THAM CHIẾU.\n");

        // --- Reference Type (array) ---
        int[] arr = { 1, 2, 3 };
        Console.Write("Trước ModifyArray: ");
        PrintArray(arr);
        ModifyArray(arr);
        Console.Write("Sau ModifyArray:   ");
        PrintArray(arr);
        Console.WriteLine("→ ĐÃ đổi! Array là reference type.\n");

        // --- String (reference nhưng immutable!) ---
        string text = "Hello";
        Console.WriteLine($"Trước ModifyString: \"{text}\"");
        ModifyString(text);
        Console.WriteLine($"Sau ModifyString:   \"{text}\"");
        Console.WriteLine("→ Không đổi! String là IMMUTABLE.");
    }

    static void ModifyValue(int x) => x = 999;
    static void ModifyRef(ref int x) => x = 999;
    static void ModifyArray(int[] arr) => arr[0] = 999;
    static void ModifyString(string s) => s = "Changed";

    static void PrintArray(int[] arr)
    {
        Console.Write("[");
        for (int i = 0; i < arr.Length; i++)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(arr[i]);
        }
        Console.WriteLine("]");
    }
}
```

### 📝 Bảng tóm tắt:

| Kiểu | Truyền vào method | Sửa ảnh hưởng gốc? |
|------|-------------------|---------------------|
| `int`, `double`, `bool` (value) | Copy | ❌ Không |
| Value + `ref` | Tham chiếu | ✅ Có |
| `int[]`, `List<>` (reference) | Tham chiếu | ✅ Có |
| `string` (reference nhưng immutable) | Tham chiếu | ❌ Không (immutable) |

---

## Ví dụ 3: Scope Bug thường gặp & Cách fix

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SCOPE BUGS & FIXES ===\n");

        // --- Bug 1: Khai báo trong block, dùng ngoài ---
        Console.WriteLine("Bug 1: Block scope");

        int age = 20;

        // ❌ BUG: result khai báo trong if, dùng ngoài
        // if (age >= 18) { string result = "Đủ tuổi"; }
        // Console.WriteLine(result);  // LỖI!

        // ✅ FIX: Khai báo trước block
        string result = "";
        if (age >= 18)
            result = "Đủ tuổi";
        else
            result = "Chưa đủ tuổi";
        Console.WriteLine($"  {result}");

        // Hoặc dùng ternary:
        string result2 = age >= 18 ? "Đủ tuổi" : "Chưa đủ tuổi";
        Console.WriteLine($"  {result2}");

        // --- Bug 2: Biến loop dùng sau loop ---
        Console.WriteLine("\nBug 2: Loop variable");

        // ❌ BUG:
        // for (int i = 0; i < 10; i++) { }
        // Console.WriteLine(i);  // LỖI!

        // ✅ FIX:
        int lastIndex = -1;
        for (int i = 0; i < 10; i++)
        {
            lastIndex = i;
        }
        Console.WriteLine($"  lastIndex = {lastIndex}");

        // --- Bug 3: Nested scope confusion ---
        Console.WriteLine("\nBug 3: Nested scope");

        int total = 0;
        for (int i = 0; i < 3; i++)
        {
            int subtotal = (i + 1) * 100;  // subtotal tạo MỖI lần lặp
            total += subtotal;
            // subtotal RESET về giá trị mới mỗi iteration
            Console.WriteLine($"  i={i}: subtotal={subtotal}, total={total}");
        }
        // subtotal không tồn tại ở đây
        Console.WriteLine($"  Final total = {total}");
    }
}
```

---

## Ví dụ 4: Ứng dụng — Config & Scope tốt

```csharp
using System;

class Program
{
    // ===== Class-level: CHỈ cho những gì THẬT SỰ cần chia sẻ =====
    const decimal TAX_RATE = 0.10m;
    const decimal DISCOUNT_THRESHOLD = 500000m;
    const decimal DISCOUNT_RATE = 0.05m;

    static int orderCount = 0;  // Cần tracking giữa các lần gọi

    static void Main()
    {
        ProcessOrder("iPhone case", 150000m, 3);
        ProcessOrder("AirPods", 3500000m, 1);
        ProcessOrder("USB Cable", 50000m, 10);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\nTổng đơn hàng đã xử lý: {orderCount}");
        Console.ResetColor();
    }

    static void ProcessOrder(string item, decimal price, int qty)
    {
        orderCount++;  // Dùng class-level vì cần giữ giữa các lần gọi

        // Local variables — scope nhỏ nhất có thể
        decimal subtotal = price * qty;
        decimal discount = subtotal >= DISCOUNT_THRESHOLD ? subtotal * DISCOUNT_RATE : 0m;
        decimal afterDiscount = subtotal - discount;
        decimal tax = afterDiscount * TAX_RATE;
        decimal total = afterDiscount + tax;

        // In kết quả (biến chỉ sống trong method này)
        Console.WriteLine($"\n--- Đơn #{orderCount} ---");
        Console.WriteLine($"  {item} × {qty} = {subtotal:N0}");
        if (discount > 0)
            Console.WriteLine($"  Giảm: -{discount:N0}");
        Console.WriteLine($"  Thuế: +{tax:N0}");
        Console.WriteLine($"  Tổng: {total:N0} VNĐ");
    }
}
```

### 📝 Bài học:

| Biến | Scope | Lý do |
|------|-------|-------|
| `TAX_RATE`, `DISCOUNT_*` | Class (`const`) | Hằng số dùng khắp nơi |
| `orderCount` | Class (`static`) | Cần giữ giá trị giữa các lần gọi |
| `subtotal`, `discount`... | Method (local) | Chỉ dùng trong 1 lần xử lý |
| `item`, `price`, `qty` | Parameter | Truyền từ ngoài vào |
