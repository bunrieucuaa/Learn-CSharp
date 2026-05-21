# 💻 Lesson 11 — Ví dụ thực hành: Memory Basics

---

## Ví dụ 1: Value Type vs Reference Type — Thí nghiệm

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ THÍ NGHIỆM MEMORY ═══\n");

        // ── TN1: Value Type — Copy độc lập ──
        Console.WriteLine("── TN1: Value Type (int) ──");
        int a = 10;
        int b = a;          // Copy GIÁ TRỊ
        b = 999;
        Console.WriteLine($"  a = {a}");   // 10 — không đổi
        Console.WriteLine($"  b = {b}");   // 999
        Console.WriteLine($"  → Copy value = 2 bản RIÊNG\n");

        // ── TN2: Reference Type — Chia sẻ data ──
        Console.WriteLine("── TN2: Reference Type (array) ──");
        int[] arrA = { 1, 2, 3 };
        int[] arrB = arrA;   // Copy REFERENCE (địa chỉ)
        arrB[0] = 999;
        Console.WriteLine($"  arrA[0] = {arrA[0]}");  // 999 — ĐÃ đổi!
        Console.WriteLine($"  arrB[0] = {arrB[0]}");  // 999
        Console.WriteLine($"  → Copy reference = CÙNG data!\n");

        // ── TN3: String — Immutable ──
        Console.WriteLine("── TN3: String (immutable) ──");
        string s1 = "Hello";
        string s2 = s1;
        s2 = "World";       // Tạo string MỚI, s2 trỏ sang đó
        Console.WriteLine($"  s1 = \"{s1}\"");  // "Hello" — không đổi
        Console.WriteLine($"  s2 = \"{s2}\"");  // "World"
        Console.WriteLine($"  → String immutable: sửa = tạo mới\n");

        // ── TN4: Array Clone ──
        Console.WriteLine("── TN4: Clone array (copy thật) ──");
        int[] original = { 10, 20, 30 };
        int[] cloned = (int[])original.Clone();
        cloned[0] = 999;
        Console.WriteLine($"  original[0] = {original[0]}");  // 10 — không đổi!
        Console.WriteLine($"  cloned[0]   = {cloned[0]}");    // 999
        Console.WriteLine($"  → Clone = copy DATA, 2 bản riêng\n");

        // ── TN5: Kiểm tra reference equality ──
        Console.WriteLine("── TN5: Reference Equality ──");
        int[] x = { 1, 2, 3 };
        int[] y = x;
        int[] z = (int[])x.Clone();
        Console.WriteLine($"  x == y: {ReferenceEquals(x, y)}");  // True — cùng reference
        Console.WriteLine($"  x == z: {ReferenceEquals(x, z)}");  // False — khác reference
    }
}
```

---

## Ví dụ 2: Truyền vào Method — 4 trường hợp

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ TRUYỀN VÀO METHOD ═══\n");

        // Case 1: Value type → Copy
        int num = 100;
        Console.WriteLine($"TRƯỚC ChangeValue: num = {num}");
        ChangeValue(num);
        Console.WriteLine($"SAU   ChangeValue: num = {num}");
        Console.WriteLine("→ Không đổi (copy)\n");

        // Case 2: Value type + ref → Sửa gốc
        int score = 100;
        Console.WriteLine($"TRƯỚC ChangeRef: score = {score}");
        ChangeRef(ref score);
        Console.WriteLine($"SAU   ChangeRef: score = {score}");
        Console.WriteLine("→ ĐÃ đổi (ref)\n");

        // Case 3: Reference type → Modify = đổi gốc
        int[] arr = { 1, 2, 3 };
        Console.Write("TRƯỚC ModifyArray: ");
        PrintArr(arr);
        ModifyArray(arr);
        Console.Write("SAU   ModifyArray: ");
        PrintArr(arr);
        Console.WriteLine("→ ĐÃ đổi (modify data qua reference)\n");

        // Case 4: Reference type → Reassign = KHÔNG đổi gốc
        int[] arr2 = { 10, 20, 30 };
        Console.Write("TRƯỚC ReassignArray: ");
        PrintArr(arr2);
        ReassignArray(arr2);
        Console.Write("SAU   ReassignArray: ");
        PrintArr(arr2);
        Console.WriteLine("→ Không đổi (reassign chỉ thay copy reference)");
    }

    static void ChangeValue(int x) => x = 999;
    static void ChangeRef(ref int x) => x = 999;
    static void ModifyArray(int[] arr) => arr[0] = 999;
    static void ReassignArray(int[] arr) => arr = new int[] { 100, 200, 300 };

    static void PrintArr(int[] arr)
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

| Trường hợp | Sửa gốc? | Tại sao |
|-----------|---------|---------|
| Value type | ❌ | Truyền copy giá trị |
| Value + `ref` | ✅ | Truyền tham chiếu biến gốc |
| Ref type + modify | ✅ | Cùng trỏ 1 data trên Heap |
| Ref type + reassign | ❌ | Chỉ thay copy reference, gốc giữ nguyên |

---

## Ví dụ 3: Null & NullReferenceException

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ NULL HANDLING ═══\n");

        // ── Null reference ──
        string? name = null;
        int[]? scores = null;

        // ❌ CRASH nếu không kiểm tra
        // Console.WriteLine(name.Length);
        // Console.WriteLine(scores[0]);

        // ✅ Cách 1: if check
        if (name != null)
            Console.WriteLine($"Name length: {name.Length}");
        else
            Console.WriteLine("Name is null!");

        // ✅ Cách 2: ?. (null-conditional)
        int? len = name?.Length;  // null nếu name null
        Console.WriteLine($"Name?.Length = {len?.ToString() ?? "null"}");

        // ✅ Cách 3: ?? (null-coalescing)
        string displayName = name ?? "Không tên";
        Console.WriteLine($"Display: {displayName}");

        // ✅ Cách 4: ??= (gán nếu null)
        name ??= "Default Name";
        Console.WriteLine($"After ??=: {name}");

        // ── Nullable Value Type ──
        Console.WriteLine("\n── Nullable Value Type ──");
        int? age = null;
        Console.WriteLine($"age = {age?.ToString() ?? "null"}");
        Console.WriteLine($"age.HasValue = {age.HasValue}");     // False

        age = 25;
        Console.WriteLine($"age = {age}");
        Console.WriteLine($"age.HasValue = {age.HasValue}");     // True
        Console.WriteLine($"age.Value = {age.Value}");           // 25

        int safeAge = age ?? 0;  // 25 (vì không null)
        Console.WriteLine($"safeAge = {safeAge}");
    }
}
```

---

## Ví dụ 4: Stack & Method Calls — Visualization

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ STACK VISUALIZATION ═══\n");
        Console.WriteLine("Main() bắt đầu");
        Console.WriteLine(StackTrace());

        int result = Multiply(3, 4);
        Console.WriteLine($"\nResult: {result}");
    }

    static int Multiply(int a, int b)
    {
        Console.WriteLine($"\n  Multiply({a}, {b}) được gọi");
        Console.WriteLine($"  {StackTrace()}");

        int sum = 0;
        for (int i = 0; i < b; i++)
        {
            sum = AddOne(sum, a);
        }
        return sum;
    }

    static int AddOne(int current, int value)
    {
        // Mỗi lần gọi AddOne → thêm 1 frame vào Stack
        return current + value;
    }

    static string StackTrace()
    {
        // Hiển thị stack giả lập
        var stack = Environment.StackTrace;
        string[] lines = stack.Split('\n');
        int depth = 0;
        foreach (string line in lines)
            if (line.Contains("Program.")) depth++;
        return $"Stack depth: {depth} frames";
    }
}

/*
  STACK tại thời điểm AddOne():
  ┌────────────────────┐
  │ AddOne()           │ ← TOP (frame mới nhất)
  │  current, value    │
  ├────────────────────┤
  │ Multiply()         │
  │  a=3, b=4, sum, i  │
  ├────────────────────┤
  │ Main()             │ ← BOTTOM (frame đầu tiên)
  │  result            │
  └────────────────────┘

  Khi AddOne() return → frame bị xóa
  Khi Multiply() return → frame bị xóa
  Khi Main() return → chương trình kết thúc
*/
```

---

## Ví dụ 5: Boxing/Unboxing & Performance

```csharp
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══ BOXING / UNBOXING ═══\n");

        // ── Boxing ──
        int number = 42;
        object boxed = number;       // Boxing: Stack → Heap

        Console.WriteLine($"number = {number}");
        Console.WriteLine($"boxed  = {boxed}");
        Console.WriteLine($"Cùng giá trị? {number == (int)boxed}");    // True
        Console.WriteLine($"Cùng reference? {ReferenceEquals(number, number)}"); // False (2 lần boxing!)

        // ── Unboxing ──
        int unboxed = (int)boxed;   // Unboxing: Heap → Stack
        Console.WriteLine($"unboxed = {unboxed}");

        // ❌ Unbox sai kiểu → CRASH!
        // double wrong = (double)boxed;  // InvalidCastException!
        // ✅ Đúng: convert rồi mới cast
        double correct = Convert.ToDouble(boxed);

        // ── Performance benchmark ──
        Console.WriteLine("\n── Performance ──");
        int iterations = 5_000_000;

        // Không boxing
        Stopwatch sw1 = Stopwatch.StartNew();
        int sum1 = 0;
        for (int i = 0; i < iterations; i++)
            sum1 += i;
        sw1.Stop();

        // Có boxing
        Stopwatch sw2 = Stopwatch.StartNew();
        object sum2 = 0;
        for (int i = 0; i < iterations; i++)
            sum2 = (int)sum2 + i;   // Unbox + Box mỗi lần!
        sw2.Stop();

        Console.WriteLine($"Không boxing: {sw1.ElapsedMilliseconds}ms");
        Console.WriteLine($"Có boxing:    {sw2.ElapsedMilliseconds}ms");
        Console.WriteLine($"→ Boxing chậm hơn ~{sw2.ElapsedMilliseconds / Math.Max(sw1.ElapsedMilliseconds, 1)}x");
    }
}
```
