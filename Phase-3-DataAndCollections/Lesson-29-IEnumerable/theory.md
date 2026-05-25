# 📘 Bài 29: IEnumerable<T> & Iteration — Nền tảng duyệt Collection

> **"Mọi collection trong C# đều có thể duyệt bằng foreach — nhờ IEnumerable<T>."**

---

## 📋 Mục lục

1. [IEnumerable\<T\> = Giao diện duyệt collection](#1-ienumerablet--giao-diện-duyệt-collection)
2. [IEnumerator\<T\> — MoveNext(), Current, Reset()](#2-ienumeratort--movenext-current-reset)
3. [Tại sao foreach hoạt động — Compiler giải mã](#3-tại-sao-foreach-hoạt-động--compiler-giải-mã)
4. [yield return — Tạo iterator đơn giản](#4-yield-return--tạo-iterator-đơn-giản)
5. [yield break — Dừng iterator](#5-yield-break--dừng-iterator)
6. [Deferred Execution — Không chạy ngay](#6-deferred-execution--không-chạy-ngay)
7. [Tự implement IEnumerable\<T\> cho class riêng](#7-tự-implement-ienumerablet-cho-class-riêng)
8. [IEnumerable vs ICollection vs IList — Hierarchy](#8-ienumerable-vs-icollection-vs-ilist--hierarchy)
9. [LINQ Preview — IEnumerable là nền tảng LINQ](#9-linq-preview--ienumerable-là-nền-tảng-linq)
10. [So sánh JS: Symbol.iterator, generators, for...of](#10-so-sánh-js-symboliterator-generators-forof)
11. [Best Practices](#11-best-practices)

---

## 1. IEnumerable\<T\> = Giao diện duyệt collection

> 📌 Mỗi khi bạn dùng `foreach`, .NET yêu cầu object phải implement **IEnumerable\<T\>**.

```csharp
// Tất cả đều implement IEnumerable<T>:
List<int> list = new List<int> { 1, 2, 3 };
int[] array = { 4, 5, 6 };
Dictionary<string, int> dict = new Dictionary<string, int>();
Queue<string> queue = new Queue<string>();
Stack<double> stack = new Stack<double>();

// → foreach hoạt động với TẤT CẢ!
foreach (int x in list)  { }   // ✅
foreach (int x in array) { }   // ✅
foreach (var kv in dict) { }   // ✅
```

### 📖 Định nghĩa IEnumerable\<T\>

```csharp
// Namespace: System.Collections.Generic
public interface IEnumerable<out T> : IEnumerable
{
    IEnumerator<T> GetEnumerator();   // Trả về "con trỏ duyệt"
}

// Phiên bản non-generic (từ System.Collections)
public interface IEnumerable
{
    IEnumerator GetEnumerator();
}
```

```
IEnumerable<T> chỉ có 1 method duy nhất:
╔═══════════════════════════════════════════╗
║   GetEnumerator() → IEnumerator<T>        ║
║                                           ║
║   "Cho tôi một con trỏ để duyệt qua      ║
║    từng phần tử của bạn."                 ║
╚═══════════════════════════════════════════╝
```

---

## 2. IEnumerator\<T\> — MoveNext(), Current, Reset()

> **IEnumerator\<T\>** là "con trỏ" duyệt qua từng phần tử.

```csharp
public interface IEnumerator<out T> : IEnumerator, IDisposable
{
    T Current { get; }          // Phần tử hiện tại
}

public interface IEnumerator
{
    bool MoveNext();            // Di chuyển đến phần tử tiếp theo
    object Current { get; }     // Phần tử hiện tại (non-generic)
    void Reset();               // Quay về đầu (ít dùng)
}
```

### 📊 Cách IEnumerator hoạt động

```
MoveNext() + Current:

  Trạng thái ban đầu:   [?]  →  1  →  2  →  3  →  [end]
                          ↑
                       con trỏ BẮT ĐẦU TRƯỚC phần tử đầu tiên

  MoveNext() lần 1:     [?]     1  →  2  →  3  →  [end]
                                ↑
                          Current = 1, return true

  MoveNext() lần 2:     [?]     1     2  →  3  →  [end]
                                      ↑
                          Current = 2, return true

  MoveNext() lần 3:     [?]     1     2     3  →  [end]
                                            ↑
                          Current = 3, return true

  MoveNext() lần 4:     [?]     1     2     3     [end]
                                                    ↑
                          return false → DỪNG!
```

---

## 3. Tại sao foreach hoạt động — Compiler giải mã

> 📌 **foreach không phải phép thuật** — compiler chuyển nó thành code dùng IEnumerator!

```csharp
// ═══ CÁI BẠN VIẾT ═══
foreach (int x in numbers)
{
    Console.WriteLine(x);
}

// ═══ CÁI COMPILER TẠO RA ═══
IEnumerator<int> enumerator = numbers.GetEnumerator();
try
{
    while (enumerator.MoveNext())        // Còn phần tử?
    {
        int x = enumerator.Current;      // Lấy phần tử hiện tại
        Console.WriteLine(x);
    }
}
finally
{
    enumerator.Dispose();                // Giải phóng
}
```

### 📊 Minh họa "bên trong" foreach

```
foreach (int x in new int[] { 10, 20, 30 })

Bước │ MoveNext() │ Current │ Hành động
─────┼────────────┼─────────┼──────────────────
  0  │ (khởi tạo) │   —     │ GetEnumerator()
  1  │ true       │  10     │ Console.Write(10)
  2  │ true       │  20     │ Console.Write(20)
  3  │ true       │  30     │ Console.Write(30)
  4  │ false      │   —     │ Thoát vòng lặp
  5  │ —          │   —     │ Dispose()
```

---

## 4. yield return — Tạo iterator đơn giản

> ⚡ **yield return** cho phép bạn tạo iterator mà KHÔNG cần viết class IEnumerator riêng.

### 📖 yield return là gì?

```csharp
// Thay vì tạo class IEnumerator phức tạp...
// → Dùng yield return: compiler TỰ ĐỘNG tạo IEnumerator cho bạn!

IEnumerable<int> GetNumbers()
{
    yield return 1;    // Trả về 1, TẠM DỪNG
    yield return 2;    // Khi gọi tiếp → trả 2, TẠM DỪNG
    yield return 3;    // Khi gọi tiếp → trả 3, TẠM DỪNG
}                      // Hết method → MoveNext() = false

foreach (int n in GetNumbers())
{
    Console.Write($"{n} ");   // Output: 1 2 3
}
```

### 🧠 yield return = "Lazy" — Tạo từng phần tử khi CẦN

```
Không dùng yield (Eager):           Dùng yield (Lazy):
─────────────────────────           ──────────────────────
List<int> GetAll()                  IEnumerable<int> GetAll()
{                                   {
    List<int> result = new();           yield return 1; // pause
    result.Add(1);                      yield return 2; // pause
    result.Add(2);                      yield return 3; // pause
    result.Add(3);                  }
    return result; ← TẤT CẢ 1 lúc
}                                   → Tạo 1 phần tử mỗi lần gọi
                                    → KHÔNG tạo List trung gian!
```

### 📊 Fibonacci Generator

```csharp
IEnumerable<long> Fibonacci()
{
    long a = 0, b = 1;

    while (true)              // Vô hạn! Nhưng KHÔNG sao
    {
        yield return a;       // Trả về giá trị hiện tại
        long temp = a;
        a = b;
        b = temp + b;
    }
}

// Chỉ lấy 10 số Fibonacci đầu tiên:
int count = 0;
foreach (long fib in Fibonacci())
{
    if (count++ >= 10) break;
    Console.Write($"{fib} ");
}
// Output: 0 1 1 2 3 5 8 13 21 34
```

---

## 5. yield break — Dừng iterator

> `yield break` = **"Tôi không có phần tử nào nữa, dừng!"**

```csharp
IEnumerable<int> GetPositiveNumbers(int[] numbers)
{
    foreach (int n in numbers)
    {
        if (n < 0)
            yield break;         // Gặp số âm → DỪNG HẲN
        yield return n;
    }
}

int[] data = { 1, 5, 3, -1, 7, 8 };
foreach (int n in GetPositiveNumbers(data))
{
    Console.Write($"{n} ");     // Output: 1 5 3 (dừng khi gặp -1)
}
```

```
yield return vs yield break:
╔══════════════════════════════════════════════════════╗
║ yield return value;  → Trả value, TẠM DỪNG iterator ║
║                        (sẽ tiếp tục khi MoveNext)    ║
║                                                      ║
║ yield break;         → KẾT THÚC iterator             ║
║                        (MoveNext() = false)           ║
╚══════════════════════════════════════════════════════╝
```

---

## 6. Deferred Execution — Không chạy ngay

> ⚠️ **Cực kỳ quan trọng:** Method dùng `yield return` KHÔNG chạy khi bạn gọi nó!

```csharp
IEnumerable<int> GetData()
{
    Console.WriteLine("→ Bắt đầu tạo dữ liệu...");
    yield return 1;
    Console.WriteLine("→ Tạo phần tử 2...");
    yield return 2;
    Console.WriteLine("→ Tạo phần tử 3...");
    yield return 3;
    Console.WriteLine("→ Hoàn tất!");
}

// Gọi method → KHÔNG có gì xảy ra!
IEnumerable<int> data = GetData();
Console.WriteLine("Đã gọi GetData()");
// Output: "Đã gọi GetData()"    ← Chỉ có dòng này!

// Duyệt → BÂY GIỜ mới chạy!
foreach (int x in data)
{
    Console.WriteLine($"Nhận: {x}");
}
```

```
Output thực tế:
──────────────────────────────
Đã gọi GetData()
→ Bắt đầu tạo dữ liệu...     ← Chạy KHI foreach bắt đầu
Nhận: 1
→ Tạo phần tử 2...            ← Chạy KHI MoveNext()
Nhận: 2
→ Tạo phần tử 3...
Nhận: 3
→ Hoàn tất!
```

### 📊 Deferred vs Immediate Execution

```
┌─────────────────────┬──────────────────────┬───────────────────────┐
│                     │ Deferred (Lazy)      │ Immediate (Eager)     │
├─────────────────────┼──────────────────────┼───────────────────────┤
│ Khi nào chạy?       │ Khi duyệt (foreach)  │ Khi gọi method       │
│ Dùng gì?            │ yield return         │ return List/Array     │
│ Bộ nhớ?             │ Ít (1 phần tử/lần)  │ Nhiều (toàn bộ)      │
│ Chuỗi vô hạn?      │ ✅ Được              │ ❌ Tràn bộ nhớ       │
│ Duyệt nhiều lần?    │ Chạy lại từ đầu     │ Dùng lại kết quả     │
│ Return type         │ IEnumerable<T>       │ List<T>, T[]          │
└─────────────────────┴──────────────────────┴───────────────────────┘
```

---

## 7. Tự implement IEnumerable\<T\> cho class riêng

> Muốn dùng `foreach` với class tự tạo? → Implement **IEnumerable\<T\>**!

```csharp
using System.Collections;
using System.Collections.Generic;

class StudentGroup : IEnumerable<Student>
{
    private List<Student> students = new List<Student>();

    public void Add(Student s) => students.Add(s);
    public int Count => students.Count;

    // ═══ IEnumerable<Student> ═══
    public IEnumerator<Student> GetEnumerator()
    {
        // Cách 1: Dùng yield return
        foreach (Student s in students)
        {
            yield return s;
        }

        // Cách 2: Delegate cho List
        // return students.GetEnumerator();
    }

    // ═══ IEnumerable (non-generic) — BẮT BUỘC implement ═══
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();    // Gọi lại generic version
    }
}

// Sử dụng:
StudentGroup group = new StudentGroup();
group.Add(new Student("An", 8.5));
group.Add(new Student("Bình", 9.0));

// ✅ foreach hoạt động nhờ IEnumerable<Student>!
foreach (Student s in group)
{
    Console.WriteLine(s.Name);
}
```

### 📊 Template implement IEnumerable\<T\>

```
class MyCollection<T> : IEnumerable<T>
{
    private T[] items;

    // 1. Generic GetEnumerator — logic duyệt ở đây
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
            yield return items[i];
    }

    // 2. Non-generic GetEnumerator — chỉ delegate
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
```

---

## 8. IEnumerable vs ICollection vs IList — Hierarchy

```
System.Collections.Generic Hierarchy:

  IEnumerable<T>          ← Duyệt (foreach)
       │
  ICollection<T>          ← Duyệt + Đếm + Add/Remove
       │
  IList<T>                ← Duyệt + Đếm + Add/Remove + Index [i]
       │
  List<T>                 ← Class cụ thể

  ─────────────────────────────────────────────────────
  Mỗi tầng THÊM khả năng:
  IEnumerable<T>:    GetEnumerator()
  ICollection<T>:  + Count, Add, Remove, Contains, Clear
  IList<T>:        + this[index], IndexOf, Insert, RemoveAt
```

### 📊 Bảng so sánh chi tiết

```
┌───────────────────┬──────────────┬────────────────┬──────────────┐
│ Tính năng         │IEnumerable<T>│ ICollection<T> │ IList<T>     │
├───────────────────┼──────────────┼────────────────┼──────────────┤
│ foreach           │ ✅           │ ✅             │ ✅           │
│ Count             │ ❌           │ ✅             │ ✅           │
│ Add/Remove        │ ❌           │ ✅             │ ✅           │
│ Contains          │ ❌           │ ✅             │ ✅           │
│ Index [i]         │ ❌           │ ❌             │ ✅           │
│ IndexOf           │ ❌           │ ❌             │ ✅           │
│ Insert/RemoveAt   │ ❌           │ ❌             │ ✅           │
├───────────────────┼──────────────┼────────────────┼──────────────┤
│ Tính linh hoạt    │ Cao nhất     │ Trung bình     │ Cụ thể nhất │
│ Dùng khi          │ Chỉ cần đọc │ Cần đếm/thêm   │ Cần index    │
└───────────────────┴──────────────┴────────────────┴──────────────┘
```

### 💡 Quy tắc chọn parameter type

```
// ✅ ĐÚNG: Dùng interface HẸP NHẤT có thể
void PrintAll(IEnumerable<int> items)           // Chỉ cần duyệt
void CountItems(ICollection<int> items)         // Cần đếm
void GetThird(IList<int> items)                 // Cần index

// ❌ SAI: Dùng type quá cụ thể
void PrintAll(List<int> items)                  // Bắt buộc phải là List!
```

---

## 9. LINQ Preview — IEnumerable là nền tảng LINQ

> 📌 **Phase 4** sẽ học LINQ chi tiết. Ở đây chỉ preview để thấy vai trò của IEnumerable.

```csharp
// LINQ = chuỗi các phép biến đổi trên IEnumerable<T>
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Where, Select, Take... đều trả về IEnumerable<T>!
IEnumerable<int> result = numbers
    .Where(n => n % 2 == 0)      // Lọc số chẵn → IEnumerable<int>
    .Select(n => n * n)          // Bình phương  → IEnumerable<int>
    .Take(3);                    // Lấy 3 cái    → IEnumerable<int>

// Deferred! Chưa chạy gì cả!
// Chạy khi:
foreach (int x in result)        // BÂY GIỜ mới chạy cả pipeline
{
    Console.Write($"{x} ");      // Output: 4 16 36
}
```

```
LINQ Pipeline — Tất cả dựa trên IEnumerable<T>:

  IEnumerable<int>        IEnumerable<int>        IEnumerable<int>
  ┌──────────┐            ┌──────────┐            ┌──────────┐
  │ 1,2,3,...│ → Where → │ 2,4,6,...│ → Select → │ 4,16,36..│ → ...
  │  10     │   (lọc)    │  8,10   │  (biến đổi) │  64,100  │
  └──────────┘            └──────────┘            └──────────┘
```

---

## 10. So sánh JS: Symbol.iterator, generators, for...of

### 📊 Bảng so sánh C# vs JavaScript

```
┌─────────────────────┬────────────────────────┬───────────────────────┐
│ Khái niệm           │ C#                     │ JavaScript            │
├─────────────────────┼────────────────────────┼───────────────────────┤
│ Giao diện duyệt     │ IEnumerable<T>         │ Symbol.iterator       │
│ Con trỏ duyệt       │ IEnumerator<T>         │ Iterator protocol     │
│                     │ MoveNext() + Current   │ next() → {value,done} │
│ Vòng lặp            │ foreach                │ for...of              │
│ Generator           │ yield return           │ function* + yield     │
│ Dừng generator      │ yield break            │ return                │
│ Lazy evaluation     │ ✅ Deferred execution  │ ✅ Generator lazy     │
│ Chuỗi vô hạn       │ ✅ yield + while(true) │ ✅ function* infinite │
│ Type safety         │ ✅ Generic <T>         │ ❌ any                │
└─────────────────────┴────────────────────────┴───────────────────────┘
```

### 🔄 Code so sánh trực tiếp

```javascript
// ═══ JAVASCRIPT ═══

// Iterator protocol
const range = {
    from: 1,
    to: 5,
    [Symbol.iterator]() {                    // ← Tương đương GetEnumerator()
        let current = this.from;
        const last = this.to;
        return {
            next() {                         // ← Tương đương MoveNext() + Current
                if (current <= last) {
                    return { value: current++, done: false };
                }
                return { done: true };
            }
        };
    }
};

for (const n of range) {                     // ← Tương đương foreach
    console.log(n);                          // 1 2 3 4 5
}

// Generator function
function* fibonacci() {                      // ← function* = iterator method
    let a = 0, b = 1;
    while (true) {
        yield a;                             // ← yield giống yield return
        [a, b] = [b, a + b];
    }
}

const gen = fibonacci();
console.log(gen.next().value);               // 0
console.log(gen.next().value);               // 1
console.log(gen.next().value);               // 1
```

```csharp
// ═══ C# ═══

// Tương đương range object
class Range : IEnumerable<int>
{
    private int from, to;
    public Range(int from, int to) { this.from = from; this.to = to; }

    public IEnumerator<int> GetEnumerator()   // ← Tương đương [Symbol.iterator]()
    {
        for (int i = from; i <= to; i++)
            yield return i;                   // ← yield tương đương
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

foreach (int n in new Range(1, 5))            // ← Tương đương for...of
{
    Console.WriteLine(n);                     // 1 2 3 4 5
}

// Generator method
IEnumerable<long> Fibonacci()                 // ← Tương đương function*
{
    long a = 0, b = 1;
    while (true)
    {
        yield return a;                       // ← yield return
        (a, b) = (b, a + b);                 // Tuple swap giống JS
    }
}
```

---

## 11. Best Practices

### ✅ Nên làm

```
1. ✅ Dùng IEnumerable<T> cho parameter khi chỉ cần duyệt
   void Print(IEnumerable<string> items)    // Linh hoạt!

2. ✅ Dùng yield return cho chuỗi lớn/vô hạn
   IEnumerable<int> Range(int start, int count)  // Lazy!

3. ✅ Nhớ implement BOTH: IEnumerable<T> + IEnumerable
   class MyCol : IEnumerable<T>
   {
       public IEnumerator<T> GetEnumerator() { ... }
       IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
   }

4. ✅ Dùng ToList()/ToArray() khi cần materialized collection
   List<int> list = GetData().ToList();    // Chạy ngay + lưu kết quả

5. ✅ Hiểu deferred execution để tránh bug
```

### ❌ Không nên

```
1. ❌ Duyệt IEnumerable nhiều lần (mỗi lần chạy lại!)
   IEnumerable<int> data = GetExpensiveData();
   int count = data.Count();     // Duyệt lần 1
   int sum = data.Sum();         // Duyệt lần 2! → 2x chi phí!
   // → Fix: var list = data.ToList(); rồi dùng list

2. ❌ Return yield trong try-catch (không cho phép)
   // try { yield return x; } catch { }  ← COMPILE ERROR!

3. ❌ Quên rằng deferred → side effects chạy muộn
   IEnumerable<int> data = GetData();
   // ... thay đổi state ...
   foreach (var x in data)    // ← Chạy với state MỚI, không phải lúc gọi!

4. ❌ Dùng List<T> làm parameter khi chỉ cần IEnumerable<T>
   void Process(List<int> items)   // ❌ Quá cụ thể
   void Process(IEnumerable<int> items)  // ✅ Linh hoạt
```

### 🧠 Quy tắc vàng

```
╔══════════════════════════════════════════════════════╗
║  PARAMETER: Dùng interface HẸP NHẤT                 ║
║  RETURN:    Dùng type CỤ THỂ NHẤT phù hợp           ║
║                                                      ║
║  Chỉ đọc?         → IEnumerable<T>                  ║
║  Cần Count/Add?   → ICollection<T>                  ║
║  Cần index?       → IList<T>                        ║
║  Cần tất cả?      → List<T> (cụ thể)               ║
╚══════════════════════════════════════════════════════╝
```

---

> **Bài tiếp:** Bài 30 — CRUD Console Project (Tổng hợp Phase 3) 🚀

---

*"IEnumerable<T> — một interface nhỏ, nền tảng cho toàn bộ hệ thống LINQ."* 🎯
