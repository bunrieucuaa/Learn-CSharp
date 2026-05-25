# 💻 Bài 29: IEnumerable<T> & Iteration — Ví dụ thực hành

> **Tất cả ví dụ đều chạy được. Hãy tạo Console App và copy-paste để thử!**

---

## Ví dụ 1: foreach giải mã — IEnumerator thủ công 🔍

> **Mục tiêu:** Hiểu cách foreach THỰC SỰ hoạt động bên trong — bằng cách dùng IEnumerator thủ công.

```csharp
using System;
using System.Collections.Generic;

namespace ForEachDecodedDemo
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   FOREACH GIẢI MÃ — IEnumerator     ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            List<string> fruits = new List<string>
            {
                "🍎 Táo", "🍌 Chuối", "🍊 Cam", "🍇 Nho", "🥭 Xoài"
            };

            // ═══════════════════════════════════════
            // CÁCH 1: foreach (cách thông thường)
            // ═══════════════════════════════════════
            Console.WriteLine("── CÁCH 1: foreach (đơn giản) ──");
            foreach (string fruit in fruits)
            {
                Console.WriteLine($"  {fruit}");
            }

            // ═══════════════════════════════════════
            // CÁCH 2: IEnumerator thủ công (cách compiler làm)
            // ═══════════════════════════════════════
            Console.WriteLine("\n── CÁCH 2: IEnumerator thủ công ──");

            // Bước 1: Lấy enumerator từ collection
            IEnumerator<string> enumerator = fruits.GetEnumerator();
            Console.WriteLine("  📌 Đã lấy IEnumerator<string>");

            try
            {
                int step = 0;

                // Bước 2: Lặp bằng MoveNext()
                while (enumerator.MoveNext())
                {
                    step++;
                    // Bước 3: Đọc giá trị qua Current
                    string current = enumerator.Current;
                    Console.WriteLine($"  Bước {step}: MoveNext()=true → Current=\"{current}\"");
                }

                step++;
                Console.WriteLine($"  Bước {step}: MoveNext()=false → KẾT THÚC!");
            }
            finally
            {
                // Bước 4: Giải phóng enumerator
                enumerator.Dispose();
                Console.WriteLine("  📌 Đã gọi Dispose()");
            }

            // ═══════════════════════════════════════
            // CÁCH 3: So sánh với for loop
            // ═══════════════════════════════════════
            Console.WriteLine("\n── CÁCH 3: for loop (quen thuộc) ──");
            for (int i = 0; i < fruits.Count; i++)
            {
                Console.WriteLine($"  [{i}] {fruits[i]}");
            }

            // ═══════════════════════════════════════
            // DEMO: Array cũng có IEnumerator!
            // ═══════════════════════════════════════
            Console.WriteLine("\n── DEMO: Array IEnumerator ──");
            int[] numbers = { 10, 20, 30 };

            IEnumerator<int> numEnum = ((IEnumerable<int>)numbers).GetEnumerator();
            Console.Write("  ");
            while (numEnum.MoveNext())
            {
                Console.Write($"{numEnum.Current} → ");
            }
            Console.WriteLine("end");
            numEnum.Dispose();

            // ═══════════════════════════════════════
            // DEMO: Dictionary enumerator → KeyValuePair
            // ═══════════════════════════════════════
            Console.WriteLine("\n── DEMO: Dictionary IEnumerator ──");
            Dictionary<string, int> scores = new Dictionary<string, int>
            {
                ["An"] = 85,
                ["Bình"] = 92,
                ["Cường"] = 78
            };

            // Mỗi phần tử là KeyValuePair<string, int>
            IEnumerator<KeyValuePair<string, int>> dictEnum = scores.GetEnumerator();
            while (dictEnum.MoveNext())
            {
                KeyValuePair<string, int> kv = dictEnum.Current;
                Console.WriteLine($"  Key=\"{kv.Key}\", Value={kv.Value}");
            }
            dictEnum.Dispose();

            // ═══════════════════════════════════════
            // SO SÁNH TRỰC QUAN
            // ═══════════════════════════════════════
            Console.WriteLine("\n── KẾT LUẬN ──");
            Console.WriteLine("  foreach (var x in collection)");
            Console.WriteLine("       ↓ compiler chuyển thành ↓");
            Console.WriteLine("  var e = collection.GetEnumerator();");
            Console.WriteLine("  try {");
            Console.WriteLine("    while (e.MoveNext())");
            Console.WriteLine("      var x = e.Current; // xử lý");
            Console.WriteLine("  } finally { e.Dispose(); }");
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   FOREACH GIẢI MÃ — IEnumerator     ║
╚══════════════════════════════════════╝

── CÁCH 2: IEnumerator thủ công ──
  📌 Đã lấy IEnumerator<string>
  Bước 1: MoveNext()=true → Current="🍎 Táo"
  Bước 2: MoveNext()=true → Current="🍌 Chuối"
  Bước 3: MoveNext()=true → Current="🍊 Cam"
  Bước 4: MoveNext()=true → Current="🍇 Nho"
  Bước 5: MoveNext()=true → Current="🥭 Xoài"
  Bước 6: MoveNext()=false → KẾT THÚC!
  📌 Đã gọi Dispose()
```

---

## Ví dụ 2: yield return — Fibonacci Generator & Range Generator ♾️

> **Mục tiêu:** Dùng `yield return` tạo iterator lazy — bao gồm chuỗi vô hạn Fibonacci và Range tùy chỉnh.

```csharp
using System;
using System.Collections.Generic;

namespace YieldReturnDemo
{
    class Program
    {
        // ═══════════════════════════════════════
        // FIBONACCI GENERATOR — Chuỗi vô hạn!
        // ═══════════════════════════════════════
        static IEnumerable<long> Fibonacci()
        {
            long a = 0, b = 1;

            while (true)           // Vô hạn — nhưng lazy nên OK!
            {
                yield return a;    // Trả về giá trị, tạm dừng
                long temp = a;
                a = b;
                b = temp + b;
            }
        }

        // ═══════════════════════════════════════
        // RANGE GENERATOR — Tạo dãy số tùy chỉnh
        // ═══════════════════════════════════════
        static IEnumerable<int> Range(int start, int count, int step = 1)
        {
            for (int i = 0; i < count; i++)
            {
                yield return start + (i * step);
            }
        }

        // ═══════════════════════════════════════
        // EVEN NUMBERS — Lọc số chẵn
        // ═══════════════════════════════════════
        static IEnumerable<int> EvenNumbers(int max)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    yield return i;
                }
            }
        }

        // ═══════════════════════════════════════
        // POWER OF TWO — Lũy thừa 2
        // ═══════════════════════════════════════
        static IEnumerable<long> PowersOfTwo()
        {
            long value = 1;
            while (true)
            {
                yield return value;
                value *= 2;
            }
        }

        // ═══════════════════════════════════════
        // TAKE — Lấy N phần tử đầu tiên
        // ═══════════════════════════════════════
        static IEnumerable<T> Take<T>(IEnumerable<T> source, int count)
        {
            int taken = 0;
            foreach (T item in source)
            {
                if (taken >= count)
                    yield break;           // Dừng iterator!

                yield return item;
                taken++;
            }
        }

        // ═══════════════════════════════════════
        // POSITIVE ONLY — yield break khi gặp số âm
        // ═══════════════════════════════════════
        static IEnumerable<int> TakeWhilePositive(int[] data)
        {
            foreach (int n in data)
            {
                if (n < 0)
                    yield break;          // Gặp số âm → DỪNG!

                yield return n;
            }
        }

        // ═══════════════════════════════════════
        // MAIN
        // ═══════════════════════════════════════
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   YIELD RETURN — Generators          ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // === Fibonacci ===
            Console.WriteLine("── FIBONACCI (15 số đầu) ──");
            Console.Write("  ");
            int count = 0;
            foreach (long fib in Fibonacci())
            {
                if (count++ >= 15) break;
                Console.Write($"{fib} ");
            }
            Console.WriteLine();

            // === Fibonacci dùng Take helper ===
            Console.WriteLine("\n── FIBONACCI dùng Take() ──");
            Console.Write("  ");
            foreach (long fib in Take(Fibonacci(), 10))
            {
                Console.Write($"{fib} ");
            }
            Console.WriteLine();

            // === Range Generator ===
            Console.WriteLine("\n── RANGE GENERATOR ──");
            Console.Write("  Range(1, 5):        ");
            foreach (int n in Range(1, 5))
                Console.Write($"{n} ");
            Console.WriteLine();

            Console.Write("  Range(0, 6, 3):     ");
            foreach (int n in Range(0, 6, 3))
                Console.Write($"{n} ");
            Console.WriteLine();

            Console.Write("  Range(10, 5, -2):   ");
            foreach (int n in Range(10, 5, -2))
                Console.Write($"{n} ");
            Console.WriteLine();

            // === Even Numbers ===
            Console.WriteLine("\n── EVEN NUMBERS (0-20) ──");
            Console.Write("  ");
            foreach (int n in EvenNumbers(20))
                Console.Write($"{n} ");
            Console.WriteLine();

            // === Powers of Two ===
            Console.WriteLine("\n── POWERS OF TWO (12 số) ──");
            Console.Write("  ");
            foreach (long p in Take(PowersOfTwo(), 12))
                Console.Write($"{p} ");
            Console.WriteLine();

            // === yield break ===
            Console.WriteLine("\n── YIELD BREAK — TakeWhilePositive ──");
            int[] data = { 5, 12, 8, 3, -1, 7, 9 };
            Console.Write("  Data:   ");
            foreach (int n in data)
                Console.Write($"{n} ");
            Console.WriteLine();

            Console.Write("  Result: ");
            foreach (int n in TakeWhilePositive(data))
                Console.Write($"{n} ");
            Console.WriteLine("  ← Dừng khi gặp -1");

            // === Kết hợp nhiều generators ===
            Console.WriteLine("\n── KẾT HỢP GENERATORS ──");
            Console.Write("  Fibonacci chẵn (5 số): ");
            int found = 0;
            foreach (long fib in Fibonacci())
            {
                if (fib > 0 && fib % 2 == 0)
                {
                    Console.Write($"{fib} ");
                    found++;
                    if (found >= 5) break;
                }
            }
            Console.WriteLine();
        }
    }
}
```

**Output mong đợi:**
```
── FIBONACCI (15 số đầu) ──
  0 1 1 2 3 5 8 13 21 34 55 89 144 233 377

── RANGE GENERATOR ──
  Range(1, 5):        1 2 3 4 5
  Range(0, 6, 3):     0 3 6 9 12 15
  Range(10, 5, -2):   10 8 6 4 2

── YIELD BREAK — TakeWhilePositive ──
  Data:   5 12 8 3 -1 7 9
  Result: 5 12 8 3   ← Dừng khi gặp -1
```

---

## Ví dụ 3: Custom Collection — StudentGroup implement IEnumerable\<T\> 🎓

> **Mục tiêu:** Tự tạo collection class implement IEnumerable\<T\> để hỗ trợ foreach.

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollectionDemo
{
    // ═══════════════════════════════════════
    // STUDENT MODEL
    // ═══════════════════════════════════════
    class Student : IComparable<Student>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }

        public Student(string id, string name, double score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            return other.Score.CompareTo(Score);  // Giảm dần
        }

        public override string ToString()
            => $"{Id} | {Name,-18} | {Score:F1}";

        public string GetGrade() => Score switch
        {
            >= 9.0 => "🌟 Xuất sắc",
            >= 8.0 => "✅ Giỏi",
            >= 6.5 => "👍 Khá",
            >= 5.0 => "📝 TB",
            _ => "❌ Yếu"
        };
    }

    // ═══════════════════════════════════════
    // STUDENT GROUP — Custom Collection
    // Implement IEnumerable<Student>
    // ═══════════════════════════════════════
    class StudentGroup : IEnumerable<Student>
    {
        private List<Student> students = new List<Student>();
        public string GroupName { get; set; }

        public StudentGroup(string name)
        {
            GroupName = name;
        }

        // ═══ CRUD operations ═══
        public void Add(Student student)
        {
            students.Add(student);
        }

        public bool Remove(string id)
        {
            Student? found = null;
            foreach (Student s in students)
            {
                if (s.Id == id) { found = s; break; }
            }
            if (found != null) return students.Remove(found);
            return false;
        }

        public Student? FindById(string id)
        {
            foreach (Student s in students)
            {
                if (s.Id == id) return s;
            }
            return null;
        }

        public int Count => students.Count;

        // ═══ Iterator methods dùng yield ═══

        // Lọc sinh viên xuất sắc (>= 9.0)
        public IEnumerable<Student> GetExcellent()
        {
            foreach (Student s in students)
            {
                if (s.Score >= 9.0)
                    yield return s;
            }
        }

        // Lọc theo khoảng điểm
        public IEnumerable<Student> GetByScoreRange(double min, double max)
        {
            foreach (Student s in students)
            {
                if (s.Score >= min && s.Score <= max)
                    yield return s;
            }
        }

        // Top N sinh viên (đã sort giảm dần)
        public IEnumerable<Student> TopN(int n)
        {
            // Tạo copy và sort
            List<Student> sorted = new List<Student>(students);
            sorted.Sort();   // Dùng IComparable

            int count = 0;
            foreach (Student s in sorted)
            {
                if (count >= n) yield break;
                yield return s;
                count++;
            }
        }

        // ═══ IEnumerable<Student> Implementation ═══
        public IEnumerator<Student> GetEnumerator()
        {
            // Dùng yield return — đơn giản và linh hoạt
            foreach (Student s in students)
            {
                yield return s;
            }
        }

        // ═══ IEnumerable (non-generic) — BẮT BUỘC ═══
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();   // Delegate cho generic version
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   CUSTOM COLLECTION — StudentGroup  ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo group và thêm sinh viên
            StudentGroup group = new StudentGroup("Lớp 12A1");

            group.Add(new Student("SV001", "Nguyễn Văn An", 8.5));
            group.Add(new Student("SV002", "Trần Thị Bình", 9.2));
            group.Add(new Student("SV003", "Lê Hoàng Cường", 7.0));
            group.Add(new Student("SV004", "Phạm Minh Đức", 9.5));
            group.Add(new Student("SV005", "Hoàng Thị Em", 6.0));
            group.Add(new Student("SV006", "Vũ Quang Phúc", 8.8));
            group.Add(new Student("SV007", "Đặng Thùy Giang", 5.5));
            group.Add(new Student("SV008", "Mai Thanh Hải", 9.0));

            // === foreach hoạt động nhờ IEnumerable! ===
            Console.WriteLine($"── DANH SÁCH {group.GroupName} ({group.Count} SV) ──");
            Console.WriteLine("  ID    | Tên                | Điểm  | Xếp loại");
            Console.WriteLine("  ──────┼────────────────────┼───────┼──────────");
            foreach (Student s in group)     // ← foreach hoạt động!
            {
                Console.WriteLine($"  {s} | {s.GetGrade()}");
            }

            // === Sinh viên xuất sắc (yield return) ===
            Console.WriteLine("\n── SINH VIÊN XUẤT SẮC (>= 9.0) ──");
            foreach (Student s in group.GetExcellent())
            {
                Console.WriteLine($"  🌟 {s.Name}: {s.Score:F1}");
            }

            // === Lọc theo khoảng điểm ===
            Console.WriteLine("\n── ĐIỂM TỪ 7.0 ĐẾN 8.9 ──");
            foreach (Student s in group.GetByScoreRange(7.0, 8.9))
            {
                Console.WriteLine($"  👍 {s.Name}: {s.Score:F1}");
            }

            // === Top 3 ===
            Console.WriteLine("\n── TOP 3 SINH VIÊN ──");
            int rank = 1;
            foreach (Student s in group.TopN(3))
            {
                Console.WriteLine($"  #{rank++} {s.Name}: {s.Score:F1}");
            }

            // === Tìm kiếm ===
            Console.WriteLine("\n── TÌM KIẾM ──");
            Student? found = group.FindById("SV004");
            Console.WriteLine(found != null
                ? $"  ✅ Tìm thấy: {found.Name} ({found.Score:F1})"
                : "  ❌ Không tìm thấy");

            // === Xóa và duyệt lại ===
            Console.WriteLine("\n── SAU KHI XÓA SV007 ──");
            group.Remove("SV007");
            Console.WriteLine($"  Còn {group.Count} sinh viên");
            foreach (Student s in group)
            {
                Console.Write($"  {s.Name}");
            }
            Console.WriteLine();
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   CUSTOM COLLECTION — StudentGroup  ║
╚══════════════════════════════════════╝

── DANH SÁCH Lớp 12A1 (8 SV) ──
  SV001 | Nguyễn Văn An      | 8.5   | ✅ Giỏi
  SV002 | Trần Thị Bình      | 9.2   | 🌟 Xuất sắc
  ...

── SINH VIÊN XUẤT SẮC (>= 9.0) ──
  🌟 Trần Thị Bình: 9.2
  🌟 Phạm Minh Đức: 9.5
  🌟 Mai Thanh Hải: 9.0

── TOP 3 SINH VIÊN ──
  #1 Phạm Minh Đức: 9.5
  #2 Trần Thị Bình: 9.2
  #3 Mai Thanh Hải: 9.0
```

---

## Ví dụ 4: Deferred Execution — Lazy vs Eager ⏱️

> **Mục tiêu:** Chứng minh rằng yield return tạo deferred execution — code chỉ chạy KHI duyệt.

```csharp
using System;
using System.Collections.Generic;

namespace DeferredExecutionDemo
{
    class Program
    {
        // ═══════════════════════════════════════
        // EAGER — Chạy NGAY khi gọi method
        // ═══════════════════════════════════════
        static List<int> GetNumbersEager(int count)
        {
            Console.WriteLine("  [EAGER] ⚡ Bắt đầu tạo TẤT CẢ...");
            List<int> result = new List<int>();

            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"  [EAGER] Tạo số {i}");
                result.Add(i * 10);
            }

            Console.WriteLine($"  [EAGER] ✅ Xong! Tạo {count} số trong bộ nhớ.");
            return result;
        }

        // ═══════════════════════════════════════
        // LAZY — Chỉ chạy khi foreach DUYỆT
        // ═══════════════════════════════════════
        static IEnumerable<int> GetNumbersLazy(int count)
        {
            Console.WriteLine("  [LAZY] ⏳ Bắt đầu (nhưng chỉ khi duyệt)...");

            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"  [LAZY] Tạo số {i}");
                yield return i * 10;    // TẠM DỪNG ở đây!
            }

            Console.WriteLine($"  [LAZY] ✅ Hoàn tất tất cả.");
        }

        // ═══════════════════════════════════════
        // DEMO: Deferred chỉ chạy khi duyệt
        // ═══════════════════════════════════════
        static IEnumerable<string> GetWithTimestamp()
        {
            yield return $"Item 1 (lúc {DateTime.Now:HH:mm:ss.fff})";
            System.Threading.Thread.Sleep(500);
            yield return $"Item 2 (lúc {DateTime.Now:HH:mm:ss.fff})";
            System.Threading.Thread.Sleep(500);
            yield return $"Item 3 (lúc {DateTime.Now:HH:mm:ss.fff})";
        }

        // ═══════════════════════════════════════
        // DEMO: Side effect với deferred
        // ═══════════════════════════════════════
        static IEnumerable<int> MultiplyBy(IEnumerable<int> source, int factor)
        {
            Console.WriteLine($"  [PIPE] Nhân với {factor}...");
            foreach (int item in source)
            {
                Console.WriteLine($"  [PIPE] {item} × {factor} = {item * factor}");
                yield return item * factor;
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   DEFERRED vs EAGER EXECUTION       ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // ═══ PHẦN 1: Eager — Tạo tất cả ngay lập tức ═══
            Console.WriteLine("═══ PHẦN 1: EAGER (List<T>) ═══");
            Console.WriteLine("Gọi GetNumbersEager(5)...");
            List<int> eager = GetNumbersEager(5);
            Console.WriteLine($"Đã nhận List có {eager.Count} phần tử.\n");

            Console.WriteLine("Bắt đầu duyệt eager:");
            foreach (int n in eager)
            {
                Console.WriteLine($"  → Nhận: {n}");
            }

            // ═══ PHẦN 2: Lazy — Chỉ tạo khi duyệt ═══
            Console.WriteLine("\n═══ PHẦN 2: LAZY (yield return) ═══");
            Console.WriteLine("Gọi GetNumbersLazy(5)...");
            IEnumerable<int> lazy = GetNumbersLazy(5);
            Console.WriteLine("Đã gọi xong — NHƯNG CHƯA CÓ GÌ CHẠY!\n");

            Console.WriteLine("Bắt đầu duyệt lazy:");
            foreach (int n in lazy)
            {
                Console.WriteLine($"  → Nhận: {n}");
            }

            // ═══ PHẦN 3: Chỉ lấy 2 phần tử — lazy tiết kiệm! ═══
            Console.WriteLine("\n═══ PHẦN 3: LAZY — Chỉ lấy 2 phần tử ═══");
            int count = 0;
            foreach (int n in GetNumbersLazy(1000))  // Source có 1000 phần tử
            {
                Console.WriteLine($"  → Nhận: {n}");
                count++;
                if (count >= 2) break;    // CHỈ lấy 2 → KHÔNG tạo 998 phần tử còn lại!
            }
            Console.WriteLine("  ← Chỉ tạo 2/1000 phần tử! Tiết kiệm!");

            // ═══ PHẦN 4: Timestamp — chứng minh lazy ═══
            Console.WriteLine("\n═══ PHẦN 4: TIMESTAMP — Mỗi phần tử tạo ở thời điểm khác ═══");
            IEnumerable<string> items = GetWithTimestamp();
            Console.WriteLine($"Gọi method lúc: {DateTime.Now:HH:mm:ss.fff}");
            Console.WriteLine("Đợi 1 giây trước khi duyệt...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Bắt đầu duyệt:");
            foreach (string item in items)
            {
                Console.WriteLine($"  {item}");
            }

            // ═══ PHẦN 5: Pipeline — Deferred qua nhiều tầng ═══
            Console.WriteLine("\n═══ PHẦN 5: PIPELINE — Nhiều tầng deferred ═══");
            int[] source = { 1, 2, 3 };

            // Tạo pipeline — CHƯA chạy gì!
            Console.WriteLine("Tạo pipeline...");
            IEnumerable<int> step1 = MultiplyBy(source, 10);
            IEnumerable<int> step2 = MultiplyBy(step1, 2);
            Console.WriteLine("Pipeline đã tạo — CHƯA chạy!\n");

            // Duyệt → BÂY GIỜ mới chạy cả pipeline
            Console.WriteLine("Duyệt kết quả:");
            foreach (int result in step2)
            {
                Console.WriteLine($"  ★ Kết quả: {result}");
            }

            // ═══ PHẦN 6: Cạm bẫy — Duyệt nhiều lần ═══
            Console.WriteLine("\n═══ PHẦN 6: CẠM BẪY — Duyệt IEnumerable nhiều lần ═══");
            IEnumerable<int> data = GetNumbersLazy(3);

            Console.WriteLine("Duyệt lần 1:");
            foreach (int n in data) Console.Write($"  {n}");
            Console.WriteLine();

            Console.WriteLine("Duyệt lần 2 (chạy LẠI method!):");
            foreach (int n in data) Console.Write($"  {n}");
            Console.WriteLine();
            Console.WriteLine("  ⚠️ Mỗi lần duyệt đều gọi lại GetNumbersLazy()!");
        }
    }
}
```

**Output mong đợi:**
```
═══ PHẦN 2: LAZY (yield return) ═══
Gọi GetNumbersLazy(5)...
Đã gọi xong — NHƯNG CHƯA CÓ GÌ CHẠY!

Bắt đầu duyệt lazy:
  [LAZY] ⏳ Bắt đầu (nhưng chỉ khi duyệt)...
  [LAZY] Tạo số 1
  → Nhận: 10
  [LAZY] Tạo số 2
  → Nhận: 20
  [LAZY] Tạo số 3
  → Nhận: 30
  ...

═══ PHẦN 3: LAZY — Chỉ lấy 2 phần tử ═══
  [LAZY] ⏳ Bắt đầu (nhưng chỉ khi duyệt)...
  [LAZY] Tạo số 1
  → Nhận: 10
  [LAZY] Tạo số 2
  → Nhận: 20
  ← Chỉ tạo 2/1000 phần tử! Tiết kiệm!
```

---

## 📊 Bảng tổng hợp ví dụ

| Ví dụ | Chủ đề | Kỹ thuật chính | Điểm nhấn |
|-------|--------|---------------|-----------|
| 1 | foreach giải mã | IEnumerator thủ công | MoveNext + Current |
| 2 | yield return | Fibonacci, Range generator | Lazy, yield break |
| 3 | Custom collection | StudentGroup : IEnumerable | Class dùng được foreach |
| 4 | Deferred execution | Eager vs Lazy | Pipeline, cạm bẫy |

---

> **💡 Mẹo:** Chạy Ví dụ 4 và quan sát thứ tự output — đó là cách tốt nhất để hiểu deferred execution! 🚀
