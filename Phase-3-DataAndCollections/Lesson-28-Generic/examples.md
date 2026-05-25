# 💻 Bài 28: Generic\<T> — Ví dụ Thực Hành

---

## Ví dụ 1: Generic Methods — Swap\<T>, FindMax\<T>, PrintAll\<T> 🔧

> **Mục tiêu**: Viết các generic method cơ bản — dùng chung cho mọi kiểu.

```csharp
using System;
using System.Collections.Generic;

namespace Lesson28_Generic
{
    // ═══════════════════════════════════════════
    // Ví dụ 1: Generic Methods
    // ═══════════════════════════════════════════
    class GenericMethods
    {
        // ---- 1. Swap<T> — Hoán đổi 2 giá trị ----
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        // ---- 2. FindMax<T> — Tìm giá trị lớn nhất ----
        // Constraint: T phải implement IComparable<T>
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        // ---- 3. FindMin<T> — Tìm giá trị nhỏ nhất ----
        public static T FindMin<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        // ---- 4. FindMaxInArray<T> — Tìm max trong mảng ----
        public static T FindMaxInArray<T>(T[] items) where T : IComparable<T>
        {
            if (items.Length == 0)
                throw new ArgumentException("Mảng không được rỗng!");

            T max = items[0];
            for (int i = 1; i < items.Length; i++)
            {
                if (items[i].CompareTo(max) > 0)
                {
                    max = items[i];
                }
            }
            return max;
        }

        // ---- 5. PrintAll<T> — In mảng bất kỳ ----
        public static void PrintAll<T>(T[] items, string label = "")
        {
            string typeName = typeof(T).Name;
            Console.Write($"  [{typeName}]{label} ");
            foreach (T item in items)
            {
                Console.Write($"{item}  ");
            }
            Console.WriteLine();
        }

        // ---- 6. PrintAll<T> cho List<T> (overload) ----
        public static void PrintAll<T>(List<T> items, string label = "")
        {
            string typeName = typeof(T).Name;
            Console.Write($"  [{typeName}]{label} ");
            foreach (T item in items)
            {
                Console.Write($"{item}  ");
            }
            Console.WriteLine();
        }

        // ---- 7. Contains<T> — Kiểm tra phần tử trong mảng ----
        public static bool Contains<T>(T[] items, T target) where T : IEquatable<T>
        {
            foreach (T item in items)
            {
                if (item.Equals(target))
                    return true;
            }
            return false;
        }

        // ---- 8. CountIf<T> — Đếm phần tử thỏa điều kiện ----
        public static int CountIf<T>(T[] items, Func<T, bool> predicate)
        {
            int count = 0;
            foreach (T item in items)
            {
                if (predicate(item))
                    count++;
            }
            return count;
        }
    }

    class Example1_GenericMethods
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 1: GENERIC METHODS ═══\n");

            // --- Swap<T> ---
            Console.WriteLine("🔄 Swap<T>:");
            int x = 10, y = 20;
            Console.WriteLine($"  Trước: x={x}, y={y}");
            GenericMethods.Swap(ref x, ref y);
            Console.WriteLine($"  Sau:   x={x}, y={y}");

            string a = "Hello", b = "World";
            Console.WriteLine($"  Trước: a=\"{a}\", b=\"{b}\"");
            GenericMethods.Swap(ref a, ref b);
            Console.WriteLine($"  Sau:   a=\"{a}\", b=\"{b}\"");

            // --- FindMax<T> ---
            Console.WriteLine("\n📈 FindMax<T>:");
            Console.WriteLine($"  Max(10, 20) = {GenericMethods.FindMax(10, 20)}");
            Console.WriteLine($"  Max(3.14, 2.71) = {GenericMethods.FindMax(3.14, 2.71)}");
            Console.WriteLine($"  Max(\"Zen\", \"Alpha\") = \"{GenericMethods.FindMax("Zen", "Alpha")}\"");

            // --- FindMaxInArray<T> ---
            Console.WriteLine("\n📊 FindMaxInArray<T>:");
            int[] nums = { 5, 3, 8, 1, 9, 2 };
            GenericMethods.PrintAll(nums, " Mảng:");
            Console.WriteLine($"  Max = {GenericMethods.FindMaxInArray(nums)}");

            string[] names = { "Minh", "Lan", "Hùng", "An" };
            GenericMethods.PrintAll(names, " Mảng:");
            Console.WriteLine($"  Max = \"{GenericMethods.FindMaxInArray(names)}\"");

            // --- CountIf<T> ---
            Console.WriteLine("\n🔢 CountIf<T>:");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int evenCount = GenericMethods.CountIf(numbers, n => n % 2 == 0);
            int bigCount = GenericMethods.CountIf(numbers, n => n > 5);
            Console.WriteLine($"  Số chẵn: {evenCount}");
            Console.WriteLine($"  Số > 5: {bigCount}");

            string[] words = { "cat", "elephant", "dog", "butterfly", "ant" };
            int longWords = GenericMethods.CountIf(words, w => w.Length > 3);
            Console.WriteLine($"  Từ dài hơn 3 ký tự: {longWords}");
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 1: GENERIC METHODS ═══

🔄 Swap<T>:
  Trước: x=10, y=20
  Sau:   x=20, y=10
  Trước: a="Hello", b="World"
  Sau:   a="World", b="Hello"

📈 FindMax<T>:
  Max(10, 20) = 20
  Max(3.14, 2.71) = 3.14
  Max("Zen", "Alpha") = "Zen"

📊 FindMaxInArray<T>:
  [Int32] Mảng: 5  3  8  1  9  2
  Max = 9
  [String] Mảng: Minh  Lan  Hùng  An
  Max = "Minh"

🔢 CountIf<T>:
  Số chẵn: 5
  Số > 5: 5
  Từ dài hơn 3 ký tự: 2
```

---

## Ví dụ 2: Generic Classes — Pair\<T1,T2> & Result\<T> 📦

> **Mục tiêu**: Tạo các generic class tiện dụng.

```csharp
using System;
using System.Collections.Generic;

namespace Lesson28_Generic
{
    // ═══════════════════════════════════════════
    // Ví dụ 2: Generic Classes
    // ═══════════════════════════════════════════

    // ---- Pair<T1, T2> — Cặp giá trị ----
    class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }

        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        // Deconstruct — cho phép dùng pattern matching
        public void Deconstruct(out T1 first, out T2 second)
        {
            first = First;
            second = Second;
        }

        public override string ToString()
        {
            return $"({First}, {Second})";
        }
    }

    // ---- Triple<T1, T2, T3> — Bộ ba giá trị ----
    class Triple<T1, T2, T3>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
        public T3 Third { get; set; }

        public Triple(T1 first, T2 second, T3 third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public override string ToString()
        {
            return $"({First}, {Second}, {Third})";
        }
    }

    // ---- Result<T> — Kết quả có thể thành công hoặc lỗi ----
    class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public DateTime Timestamp { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Timestamp = DateTime.Now;
        }

        private Result(string error)
        {
            IsSuccess = false;
            ErrorMessage = error;
            Timestamp = DateTime.Now;
        }

        // Factory methods (Static)
        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string error) => new Result<T>(error);

        // Helper method
        public void Match(Action<T> onSuccess, Action<string> onError)
        {
            if (IsSuccess)
                onSuccess(Value!);
            else
                onError(ErrorMessage!);
        }

        public override string ToString()
        {
            return IsSuccess
                ? $"✅ Success: {Value}"
                : $"❌ Failure: {ErrorMessage}";
        }
    }

    // ---- Optional<T> — Giá trị có thể có hoặc không ----
    class Optional<T>
    {
        private readonly T? _value;
        private readonly bool _hasValue;

        private Optional(T value)
        {
            _value = value;
            _hasValue = true;
        }

        private Optional()
        {
            _hasValue = false;
        }

        public static Optional<T> Some(T value) => new Optional<T>(value);
        public static Optional<T> None() => new Optional<T>();

        public bool HasValue => _hasValue;

        public T GetValueOrDefault(T defaultValue)
        {
            return _hasValue ? _value! : defaultValue;
        }

        public Optional<TResult> Map<TResult>(Func<T, TResult> mapper)
        {
            return _hasValue
                ? Optional<TResult>.Some(mapper(_value!))
                : Optional<TResult>.None();
        }

        public override string ToString()
        {
            return _hasValue ? $"Some({_value})" : "None";
        }
    }

    class Example2_GenericClasses
    {
        // Mô phỏng tìm user
        static Result<string> FindUser(int id)
        {
            if (id == 1) return Result<string>.Success("Minh");
            if (id == 2) return Result<string>.Success("Lan");
            return Result<string>.Failure($"User #{id} không tồn tại!");
        }

        // Mô phỏng chia
        static Result<double> Divide(double a, double b)
        {
            if (b == 0) return Result<double>.Failure("Không thể chia cho 0!");
            return Result<double>.Success(a / b);
        }

        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 2: GENERIC CLASSES ═══\n");

            // --- Pair<T1, T2> ---
            Console.WriteLine("🔗 Pair<T1, T2>:");

            Pair<string, int> student1 = new Pair<string, int>("Minh", 95);
            Pair<string, int> student2 = new Pair<string, int>("Lan", 88);
            Console.WriteLine($"  {student1}");
            Console.WriteLine($"  {student2}");

            // Deconstruct
            var (name, score) = student1;
            Console.WriteLine($"  Deconstruct: name={name}, score={score}");

            // Pair với các kiểu khác nhau
            Pair<string, List<string>> group = new(
                "Nhóm A",
                new List<string> { "Minh", "Lan", "Hùng" }
            );
            Console.WriteLine($"  {group.First}: [{string.Join(", ", group.Second)}]");

            // --- Triple<T1, T2, T3> ---
            Console.WriteLine("\n🔗 Triple<T1, T2, T3>:");
            Triple<string, int, double> record = new("Minh", 25, 8.5);
            Console.WriteLine($"  Student: {record}");
            Console.WriteLine($"  Name={record.First}, Age={record.Second}, GPA={record.Third}");

            // --- Result<T> ---
            Console.WriteLine("\n📋 Result<T>:");

            Result<string> r1 = FindUser(1);
            Result<string> r2 = FindUser(99);
            Console.WriteLine($"  FindUser(1):  {r1}");
            Console.WriteLine($"  FindUser(99): {r2}");

            // Match pattern
            Console.WriteLine("\n  Dùng Match:");
            r1.Match(
                onSuccess: user => Console.WriteLine($"    Tìm thấy: {user}"),
                onError: err => Console.WriteLine($"    Lỗi: {err}")
            );
            r2.Match(
                onSuccess: user => Console.WriteLine($"    Tìm thấy: {user}"),
                onError: err => Console.WriteLine($"    Lỗi: {err}")
            );

            // Divide
            Console.WriteLine("\n  Divide:");
            Result<double> d1 = Divide(10, 3);
            Result<double> d2 = Divide(10, 0);
            Console.WriteLine($"    10/3: {d1}");
            Console.WriteLine($"    10/0: {d2}");

            // --- Optional<T> ---
            Console.WriteLine("\n❓ Optional<T>:");
            Optional<string> some = Optional<string>.Some("Hello");
            Optional<string> none = Optional<string>.None();

            Console.WriteLine($"  some = {some}");
            Console.WriteLine($"  none = {none}");
            Console.WriteLine($"  some.GetValueOrDefault(\"default\") = {some.GetValueOrDefault("default")}");
            Console.WriteLine($"  none.GetValueOrDefault(\"default\") = {none.GetValueOrDefault("default")}");

            // Map
            Optional<int> length = some.Map(s => s.Length);
            Console.WriteLine($"  some.Map(s => s.Length) = {length}");
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 2: GENERIC CLASSES ═══

🔗 Pair<T1, T2>:
  (Minh, 95)
  (Lan, 88)
  Deconstruct: name=Minh, score=95
  Nhóm A: [Minh, Lan, Hùng]

🔗 Triple<T1, T2, T3>:
  Student: (Minh, 25, 8.5)
  Name=Minh, Age=25, GPA=8.5

📋 Result<T>:
  FindUser(1):  ✅ Success: Minh
  FindUser(99): ❌ Failure: User #99 không tồn tại!

  Dùng Match:
    Tìm thấy: Minh
    Lỗi: User #99 không tồn tại!

  Divide:
    10/3: ✅ Success: 3.3333333333333335
    10/0: ❌ Failure: Không thể chia cho 0!

❓ Optional<T>:
  some = Some(Hello)
  none = None
  some.GetValueOrDefault("default") = Hello
  none.GetValueOrDefault("default") = default
  some.Map(s => s.Length) = Some(5)
```

---

## Ví dụ 3: Generic Repository\<T> with CRUD 🗄️

> **Mục tiêu**: Tạo Repository pattern dùng Generic — CRUD cho mọi entity.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson28_Generic
{
    // ═══════════════════════════════════════════
    // Ví dụ 3: Generic Repository<T>
    // ═══════════════════════════════════════════

    // Interface cho entity có Id
    interface IEntity
    {
        int Id { get; set; }
    }

    // Generic Interface cho Repository
    interface IRepository<T> where T : IEntity
    {
        void Add(T item);
        T? GetById(int id);
        List<T> GetAll();
        bool Update(T item);
        bool Delete(int id);
        int Count { get; }
        List<T> Find(Func<T, bool> predicate);
    }

    // ---- Generic Repository Implementation ----
    class Repository<T> : IRepository<T> where T : IEntity
    {
        private List<T> _items = new List<T>();
        private int _nextId = 1;

        public int Count => _items.Count;

        public void Add(T item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            Console.WriteLine($"  ✅ Added [{typeof(T).Name}] Id={item.Id}: {item}");
        }

        public T? GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }

        public bool Update(T item)
        {
            int index = _items.FindIndex(x => x.Id == item.Id);
            if (index == -1)
            {
                Console.WriteLine($"  ❌ Not found: Id={item.Id}");
                return false;
            }
            _items[index] = item;
            Console.WriteLine($"  🔄 Updated [{typeof(T).Name}] Id={item.Id}: {item}");
            return true;
        }

        public bool Delete(int id)
        {
            T? item = GetById(id);
            if (item == null)
            {
                Console.WriteLine($"  ❌ Not found: Id={id}");
                return false;
            }
            _items.Remove(item);
            Console.WriteLine($"  🗑️ Deleted [{typeof(T).Name}] Id={id}");
            return true;
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return _items.Where(predicate).ToList();
        }

        public void ShowAll()
        {
            string typeName = typeof(T).Name;
            Console.WriteLine($"\n  📋 {typeName} Repository ({Count} items):");
            if (Count == 0)
            {
                Console.WriteLine("     (trống)");
                return;
            }
            foreach (T item in _items)
            {
                Console.WriteLine($"     [{item.Id}] {item}");
            }
        }
    }

    // ---- Entities ----
    class Student : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Gpa { get; set; }

        public Student(string name, double gpa)
        {
            Name = name;
            Gpa = gpa;
        }

        public override string ToString()
        {
            return $"{Name} (GPA: {Gpa:F1})";
        }
    }

    class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public override string ToString()
        {
            return $"{Name} - {Price:N0}đ (Kho: {Stock})";
        }
    }

    class Example3_GenericRepository
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 3: GENERIC REPOSITORY<T> ═══\n");

            // === Student Repository ===
            Console.WriteLine("🎓 STUDENT REPOSITORY:\n");

            // ✅ Cùng 1 class Repository<T>, dùng cho Student
            Repository<Student> studentRepo = new Repository<Student>();

            studentRepo.Add(new Student("Minh", 8.5));
            studentRepo.Add(new Student("Lan", 9.2));
            studentRepo.Add(new Student("Hùng", 7.8));
            studentRepo.Add(new Student("Mai", 9.5));

            studentRepo.ShowAll();

            // GetById
            Console.WriteLine($"\n  GetById(2): {studentRepo.GetById(2)}");

            // Find — tìm sinh viên giỏi
            List<Student> honors = studentRepo.Find(s => s.Gpa >= 9.0);
            Console.WriteLine($"\n  Sinh viên giỏi (GPA >= 9.0):");
            foreach (var s in honors)
            {
                Console.WriteLine($"     {s}");
            }

            // Update
            Console.WriteLine();
            Student? minh = studentRepo.GetById(1);
            if (minh != null)
            {
                minh.Gpa = 9.0;
                studentRepo.Update(minh);
            }

            // Delete
            studentRepo.Delete(3);
            studentRepo.ShowAll();

            // === Product Repository ===
            Console.WriteLine("\n\n🛍️ PRODUCT REPOSITORY:\n");

            // ✅ Cùng 1 class Repository<T>, dùng cho Product
            Repository<Product> productRepo = new Repository<Product>();

            productRepo.Add(new Product("iPhone 15", 25_000_000, 50));
            productRepo.Add(new Product("MacBook Pro", 55_000_000, 20));
            productRepo.Add(new Product("AirPods", 5_000_000, 100));
            productRepo.Add(new Product("iPad Air", 18_000_000, 35));

            productRepo.ShowAll();

            // Find — sản phẩm dưới 20 triệu
            Console.WriteLine();
            List<Product> affordable = productRepo.Find(p => p.Price < 20_000_000);
            Console.WriteLine("  Sản phẩm dưới 20 triệu:");
            foreach (var p in affordable)
            {
                Console.WriteLine($"     {p}");
            }

            // ═══ Tóm tắt ═══
            Console.WriteLine("\n  ╔════════════════════════════════════════╗");
            Console.WriteLine("  ║  Cùng 1 class Repository<T>, ta có:   ║");
            Console.WriteLine($"  ║  • StudentRepo: {studentRepo.Count} items              ║");
            Console.WriteLine($"  ║  • ProductRepo: {productRepo.Count} items              ║");
            Console.WriteLine("  ║  → KHÔNG viết lại code! (DRY) ✅      ║");
            Console.WriteLine("  ╚════════════════════════════════════════╝");
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 3: GENERIC REPOSITORY<T> ═══

🎓 STUDENT REPOSITORY:

  ✅ Added [Student] Id=1: Minh (GPA: 8.5)
  ✅ Added [Student] Id=2: Lan (GPA: 9.2)
  ✅ Added [Student] Id=3: Hùng (GPA: 7.8)
  ✅ Added [Student] Id=4: Mai (GPA: 9.5)

  📋 Student Repository (4 items):
     [1] Minh (GPA: 8.5)
     [2] Lan (GPA: 9.2)
     [3] Hùng (GPA: 7.8)
     [4] Mai (GPA: 9.5)

  GetById(2): Lan (GPA: 9.2)

  Sinh viên giỏi (GPA >= 9.0):
     Lan (GPA: 9.2)
     Mai (GPA: 9.5)

  🔄 Updated [Student] Id=1: Minh (GPA: 9.0)
  🗑️ Deleted [Student] Id=3

  📋 Student Repository (3 items):
     [1] Minh (GPA: 9.0)
     [2] Lan (GPA: 9.2)
     [4] Mai (GPA: 9.5)


🛍️ PRODUCT REPOSITORY:

  ✅ Added [Product] Id=1: iPhone 15 - 25,000,000đ (Kho: 50)
  ✅ Added [Product] Id=2: MacBook Pro - 55,000,000đ (Kho: 20)
  ✅ Added [Product] Id=3: AirPods - 5,000,000đ (Kho: 100)
  ✅ Added [Product] Id=4: iPad Air - 18,000,000đ (Kho: 35)

  📋 Product Repository (4 items):
     [1] iPhone 15 - 25,000,000đ (Kho: 50)
     [2] MacBook Pro - 55,000,000đ (Kho: 20)
     [3] AirPods - 5,000,000đ (Kho: 100)
     [4] iPad Air - 18,000,000đ (Kho: 35)

  Sản phẩm dưới 20 triệu:
     AirPods - 5,000,000đ (Kho: 100)
     iPad Air - 18,000,000đ (Kho: 35)

  ╔════════════════════════════════════════╗
  ║  Cùng 1 class Repository<T>, ta có:   ║
  ║  • StudentRepo: 3 items              ║
  ║  • ProductRepo: 4 items              ║
  ║  → KHÔNG viết lại code! (DRY) ✅      ║
  ╚════════════════════════════════════════╝
```

---

## Ví dụ 4: Generic Constraints — IComparable, class, new() 🔒

> **Mục tiêu**: Hiểu sâu cách constraints giới hạn kiểu T.

```csharp
using System;
using System.Collections.Generic;

namespace Lesson28_Generic
{
    // ═══════════════════════════════════════════
    // Ví dụ 4: Constraints trong Generic
    // ═══════════════════════════════════════════

    // ---- 1. where T : IComparable<T> — So sánh được ----
    class SortHelper
    {
        // BubbleSort generic — sort bất kỳ kiểu nào so sánh được
        public static void BubbleSort<T>(T[] items) where T : IComparable<T>
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = 0; j < items.Length - i - 1; j++)
                {
                    if (items[j].CompareTo(items[j + 1]) > 0)
                    {
                        // Swap
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }
    }

    // ---- 2. where T : class — Reference type only ----
    class NullChecker<T> where T : class
    {
        public static bool IsNull(T item)
        {
            return item == null;  // Chỉ reference type mới có thể null
        }

        public static T ThrowIfNull(T item, string paramName)
        {
            if (item == null)
                throw new ArgumentNullException(paramName);
            return item;
        }
    }

    // ---- 3. where T : struct — Value type only ----
    class Range<T> where T : struct, IComparable<T>
    {
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Min phải <= Max!");
            Min = min;
            Max = max;
        }

        public bool Contains(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        public override string ToString()
        {
            return $"[{Min} .. {Max}]";
        }
    }

    // ---- 4. where T : new() — Phải có constructor không tham số ----
    class Factory<T> where T : new()
    {
        public T Create()
        {
            return new T();  // Gọi được new T() nhờ constraint!
        }

        public List<T> CreateMany(int count)
        {
            List<T> items = new List<T>();
            for (int i = 0; i < count; i++)
            {
                items.Add(new T());
            }
            return items;
        }
    }

    // ---- 5. Kết hợp nhiều constraints ----
    class SmartRepository<T> where T : class, IEntity, new()
    {
        private List<T> _items = new List<T>();

        // new() → có thể tạo instance mới
        public T CreateNew()
        {
            T item = new T();
            return item;
        }

        // class → có thể check null
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _items.Add(item);
        }

        // IEntity → có thể access .Id
        public T? FindById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }
    }

    // Sample class for Factory demo
    class Config
    {
        public string Theme { get; set; } = "Default";
        public int FontSize { get; set; } = 14;
        public bool DarkMode { get; set; } = false;

        public override string ToString()
        {
            return $"Theme={Theme}, FontSize={FontSize}, DarkMode={DarkMode}";
        }
    }

    class Example4_Constraints
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 4: GENERIC CONSTRAINTS ═══\n");

            // --- 1. IComparable<T> ---
            Console.WriteLine("📊 Constraint: IComparable<T> (BubbleSort):\n");

            int[] nums = { 64, 25, 12, 22, 11 };
            Console.Write("  Trước: ");
            Console.WriteLine(string.Join(", ", nums));
            SortHelper.BubbleSort(nums);
            Console.Write("  Sau:   ");
            Console.WriteLine(string.Join(", ", nums));

            string[] names = { "Zen", "Alpha", "Minh", "Beta" };
            Console.Write("  Trước: ");
            Console.WriteLine(string.Join(", ", names));
            SortHelper.BubbleSort(names);
            Console.Write("  Sau:   ");
            Console.WriteLine(string.Join(", ", names));

            // --- 2. class (reference type) ---
            Console.WriteLine("\n🔍 Constraint: class (NullChecker):\n");

            string? text = "Hello";
            Console.WriteLine($"  IsNull(\"{text}\") = {NullChecker<string>.IsNull(text!)}");
            text = null;
            Console.WriteLine($"  IsNull(null) = {NullChecker<string>.IsNull(text!)}");

            // Không compile với value type:
            // NullChecker<int>.IsNull(42); // ❌ Compile error!

            // --- 3. struct (value type) ---
            Console.WriteLine("\n📏 Constraint: struct (Range):\n");

            Range<int> ageRange = new Range<int>(18, 65);
            Console.WriteLine($"  Range: {ageRange}");
            Console.WriteLine($"  Contains(25) = {ageRange.Contains(25)}");
            Console.WriteLine($"  Contains(10) = {ageRange.Contains(10)}");
            Console.WriteLine($"  Contains(70) = {ageRange.Contains(70)}");

            Range<double> tempRange = new Range<double>(-10.0, 45.0);
            Console.WriteLine($"\n  Temp Range: {tempRange}");
            Console.WriteLine($"  Contains(36.5) = {tempRange.Contains(36.5)}");

            // Không compile với reference type:
            // Range<string> r = new Range<string>("a", "z"); // ❌ string là class!

            // --- 4. new() ---
            Console.WriteLine("\n🏭 Constraint: new() (Factory):\n");

            Factory<Config> configFactory = new Factory<Config>();
            Config c1 = configFactory.Create();
            Console.WriteLine($"  Config mới: {c1}");

            List<Config> configs = configFactory.CreateMany(3);
            Console.WriteLine($"  Tạo {configs.Count} configs:");
            foreach (var c in configs)
            {
                Console.WriteLine($"     {c}");
            }

            // --- Tóm tắt ---
            Console.WriteLine("\n  ╔══════════════════════════════════════════════╗");
            Console.WriteLine("  ║  CONSTRAINTS SUMMARY:                       ║");
            Console.WriteLine("  ║  where T : struct       → value type only   ║");
            Console.WriteLine("  ║  where T : class        → ref type only     ║");
            Console.WriteLine("  ║  where T : new()        → có constructor()  ║");
            Console.WriteLine("  ║  where T : IComparable  → so sánh được      ║");
            Console.WriteLine("  ║  where T : BaseClass    → kế thừa từ...     ║");
            Console.WriteLine("  ║  Kết hợp: class, IEntity, new()             ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        }
    }
}
```

---

## Ví dụ 5: Tự xây dựng Generic Stack\<T> từ đầu 🔨

> **Mục tiêu**: Hiểu cách generic collection hoạt động bên trong bằng cách tự implement Stack\<T>.

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lesson28_Generic
{
    // ═══════════════════════════════════════════
    // Ví dụ 5: Custom Generic Stack<T>
    // ═══════════════════════════════════════════

    class MyStack<T> : IEnumerable<T>
    {
        private T[] _items;       // Mảng nội bộ
        private int _count;       // Số phần tử hiện tại
        private int _capacity;    // Dung lượng mảng

        private const int DefaultCapacity = 4;

        // Constructor
        public MyStack(int initialCapacity = DefaultCapacity)
        {
            _capacity = initialCapacity;
            _items = new T[_capacity];
            _count = 0;
        }

        // Properties
        public int Count => _count;
        public bool IsEmpty => _count == 0;
        public int Capacity => _capacity;

        // Push — Đẩy lên đỉnh
        public void Push(T item)
        {
            // Nếu mảng đầy → tăng gấp đôi
            if (_count == _capacity)
            {
                Resize(_capacity * 2);
            }

            _items[_count] = item;
            _count++;
        }

        // Pop — Lấy từ đỉnh
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack rỗng!");

            _count--;
            T item = _items[_count];
            _items[_count] = default!;  // Giải phóng reference
            return item;
        }

        // TryPop — Pop an toàn
        public bool TryPop(out T? item)
        {
            if (IsEmpty)
            {
                item = default;
                return false;
            }
            item = Pop();
            return true;
        }

        // Peek — Nhìn đỉnh
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack rỗng!");
            return _items[_count - 1];
        }

        // TryPeek — Peek an toàn
        public bool TryPeek(out T? item)
        {
            if (IsEmpty)
            {
                item = default;
                return false;
            }
            item = _items[_count - 1];
            return true;
        }

        // Contains — Kiểm tra tồn tại
        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i], item))
                    return true;
            }
            return false;
        }

        // Clear — Xóa tất cả
        public void Clear()
        {
            Array.Clear(_items, 0, _count);
            _count = 0;
        }

        // ToArray
        public T[] ToArray()
        {
            T[] result = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                // Đảo ngược (đỉnh ở index 0)
                result[i] = _items[_count - 1 - i];
            }
            return result;
        }

        // Resize mảng nội bộ
        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _count);
            _items = newArray;
            _capacity = newCapacity;
            Console.WriteLine($"     🔄 Resize: {_capacity / 2} → {_capacity}");
        }

        // IEnumerable<T> — cho phép foreach
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Debug — hiển thị trạng thái nội bộ
        public void DebugView()
        {
            Console.Write($"  Stack (Count={_count}, Cap={_capacity}): [");
            for (int i = _count - 1; i >= 0; i--)
            {
                if (i < _count - 1) Console.Write(", ");
                Console.Write(_items[i]);
            }
            Console.WriteLine("]");

            // Hiển thị mảng nội bộ
            Console.Write("  Array nội bộ: [");
            for (int i = 0; i < _capacity; i++)
            {
                if (i > 0) Console.Write(", ");
                string val = i < _count ? _items[i]?.ToString() ?? "null" : "_";
                Console.Write(val);
            }
            Console.WriteLine("]");
        }
    }

    class Example5_CustomStack
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 5: CUSTOM GENERIC STACK<T> ═══\n");

            // Tạo stack với capacity mặc định = 4
            MyStack<string> stack = new MyStack<string>();

            Console.WriteLine("--- Push 6 phần tử (sẽ resize) ---\n");
            string[] items = { "A", "B", "C", "D", "E", "F" };
            foreach (string item in items)
            {
                Console.WriteLine($"  Push(\"{item}\"):");
                stack.Push(item);
                stack.DebugView();
                Console.WriteLine();
            }

            // Pop
            Console.WriteLine("--- Pop 2 phần tử ---\n");
            Console.WriteLine($"  Pop() = \"{stack.Pop()}\"");
            Console.WriteLine($"  Pop() = \"{stack.Pop()}\"");
            stack.DebugView();

            // Peek
            Console.WriteLine($"\n  Peek() = \"{stack.Peek()}\"");
            Console.WriteLine($"  Count = {stack.Count}");

            // Contains
            Console.WriteLine($"\n  Contains(\"A\") = {stack.Contains("A")}");
            Console.WriteLine($"  Contains(\"F\") = {stack.Contains("F")}");

            // foreach (IEnumerable<T>)
            Console.WriteLine("\n--- foreach ---");
            Console.Write("  ");
            foreach (string item in stack)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            // ToArray
            Console.WriteLine("\n--- ToArray ---");
            string[] arr = stack.ToArray();
            Console.WriteLine($"  [{string.Join(", ", arr)}]");

            // === Test với int ===
            Console.WriteLine("\n\n--- MyStack<int> ---\n");
            MyStack<int> intStack = new MyStack<int>(2);

            for (int i = 1; i <= 8; i++)
            {
                intStack.Push(i * 10);
            }
            intStack.DebugView();

            Console.WriteLine($"\n  Pop 3 lần:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"    Pop() = {intStack.Pop()}");
            }
            intStack.DebugView();

            // So sánh với built-in
            Console.WriteLine("\n  ╔══════════════════════════════════════════════╗");
            Console.WriteLine("  ║  MyStack<T> hoạt động GIỐNG Stack<T>!       ║");
            Console.WriteLine("  ║                                             ║");
            Console.WriteLine("  ║  Bên trong built-in Stack<T> cũng dùng:     ║");
            Console.WriteLine("  ║  • Mảng T[] nội bộ                          ║");
            Console.WriteLine("  ║  • Auto-resize khi đầy                      ║");
            Console.WriteLine("  ║  • Count tracking                            ║");
            Console.WriteLine("  ║                                             ║");
            Console.WriteLine("  ║  → Generic cho phép 1 implementation        ║");
            Console.WriteLine("  ║    hoạt động với MỌI kiểu T!               ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        }
    }
}
```

---

## 🔗 Tổng kết các ví dụ

| Ví dụ | Chủ đề | Generic Feature | Điểm chính |
|-------|--------|-----------------|-------------|
| 1 | Generic Methods | `<T>`, type inference | Swap, FindMax, PrintAll |
| 2 | Generic Classes | `<T1, T2>`, factory pattern | Pair, Result, Optional |
| 3 | Generic Repository | Interface + constraints | CRUD cho mọi entity |
| 4 | Constraints | `where T : ...` | struct, class, new(), IComparable |
| 5 | Custom Stack\<T> | Full implementation | Hiểu cách collection hoạt động |

> 📌 **Generic = viết 1 lần, dùng cho tất cả!** Đây là nền tảng của mọi collection trong C#.
