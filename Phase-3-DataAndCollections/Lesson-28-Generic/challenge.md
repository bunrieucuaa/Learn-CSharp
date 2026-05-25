# 🏆 Bài 28: Generic\<T> — Thử Thách

---

## 📊 Generic Data Table — GenericTable\<T>

> **Mục tiêu**: Xây dựng một bảng dữ liệu generic với khả năng định nghĩa cột, sắp xếp, lọc, và hiển thị đẹp.

---

### 📋 Mô tả dự án

Bạn sẽ tạo một **Generic Data Table** — giống Excel/DataTable nhưng type-safe nhờ Generic. Bảng này có thể chứa bất kỳ loại dữ liệu nào (Student, Product, Employee...) và cung cấp các chức năng phân tích dữ liệu.

---

### 🏗️ Cấu trúc classes

```
  ┌──────────────────────────────────────────────────────┐
  │                GenericTable<T>                        │
  │                                                      │
  │  ┌──────────────────────────────────────────────┐    │
  │  │  ColumnDefinition<T>                         │    │
  │  │  - Name : string                             │    │
  │  │  - Selector : Func<T, object>                │    │
  │  │  - Width : int                               │    │
  │  │  - Alignment : Align (Left/Right/Center)     │    │
  │  └──────────────────────────────────────────────┘    │
  │                                                      │
  │  - List<T> _rows                                     │
  │  - List<ColumnDefinition<T>> _columns                │
  │                                                      │
  │  + AddColumn(name, selector, width, alignment)       │
  │  + AddRow(T item) / AddRows(IEnumerable<T>)          │
  │  + RemoveRow(Func<T, bool> predicate)                │
  │                                                      │
  │  + Sort(Func<T, IComparable> keySelector, desc?)     │
  │  + Filter(Func<T, bool> predicate) : GenericTable<T> │
  │  + GroupBy<TKey>(Func<T, TKey> keySelector)           │
  │                                                      │
  │  + Display()                                          │
  │  + DisplayFormatted() — bảng đẹp với border          │
  │                                                      │
  │  + Aggregate<TResult>(column, Func<...> aggregator)  │
  │  + Sum(Func<T, double> selector) : double            │
  │  + Average(Func<T, double> selector) : double        │
  │  + Min/Max<TKey>(Func<T, TKey> selector)              │
  │  + Count(Func<T, bool>? predicate) : int              │
  │                                                      │
  │  + ExportToCsv(filePath)                              │
  │  + ForEach(Action<T> action)                          │
  │  + Select<TResult>(Func<T, TResult>) : GenericTable  │
  └──────────────────────────────────────────────────────┘
```

---

### 📐 Yêu cầu chi tiết

#### 1. Enum Align:
```csharp
enum Align { Left, Right, Center }
```

#### 2. Class ColumnDefinition\<T>:
```csharp
class ColumnDefinition<T>
{
    string Name;
    Func<T, object> Selector;    // Lấy giá trị từ T
    int Width;                    // Chiều rộng cột
    Align Alignment;              // Canh lề
}
```

#### 3. Class GenericTable\<T>:

**Thêm dữ liệu:**
- `AddColumn(name, selector, width?, alignment?)` — thêm cột
- `AddRow(item)` — thêm 1 dòng
- `AddRows(items)` — thêm nhiều dòng
- `RemoveRow(predicate)` — xóa dòng theo điều kiện

**Hiển thị:**
- `Display()` — hiển thị bảng dạng đơn giản
- `DisplayFormatted()` — hiển thị bảng với border đẹp:
```
┌──────────┬─────┬──────┐
│ Tên      │ Tuổi│ GPA  │
├──────────┼─────┼──────┤
│ Minh     │   25│ 8.50 │
│ Lan      │   22│ 9.20 │
└──────────┴─────┴──────┘
```

**Sắp xếp & Lọc:**
- `Sort(keySelector, descending?)` — sắp xếp theo cột
- `Filter(predicate)` — lọc dòng, trả GenericTable mới
- `Take(n)` — lấy n dòng đầu
- `Skip(n)` — bỏ qua n dòng đầu

**Thống kê:**
- `Sum(selector)` — tổng
- `Average(selector)` — trung bình
- `Min(selector)` / `Max(selector)` — min/max
- `Count(predicate?)` — đếm (có thể có điều kiện)

---

### 🎯 Tính năng bắt buộc:

1. ✅ GenericTable\<T> hoạt động với bất kỳ class T nào
2. ✅ Định nghĩa cột linh hoạt (tên, selector, width, alignment)
3. ✅ Hiển thị bảng formatted với border
4. ✅ Sort ascending/descending
5. ✅ Filter trả về table mới
6. ✅ Aggregate: Sum, Average, Min, Max, Count
7. ✅ Demo với ít nhất 2 kiểu T khác nhau

### 🌟 Tính năng nâng cao (bonus):
- 📊 GroupBy — nhóm dữ liệu theo cột
- 📄 ExportToCsv — xuất ra file CSV
- 🔗 Select/Transform — tạo table mới từ mapping
- 📱 Pagination — hiển thị từng trang

---

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Lesson28_Challenge
{
    // ═══════════════════════════════════════════
    // Enums & Helper Classes
    // ═══════════════════════════════════════════

    enum Align { Left, Right, Center }

    class ColumnDefinition<T>
    {
        public string Name { get; set; }
        public Func<T, object> Selector { get; set; }
        public int Width { get; set; }
        public Align Alignment { get; set; }

        public ColumnDefinition(string name, Func<T, object> selector, 
                               int width = 15, Align alignment = Align.Left)
        {
            Name = name;
            Selector = selector;
            Width = width;
            Alignment = alignment;
        }

        // Format giá trị theo alignment
        public string FormatValue(object value)
        {
            string text = value?.ToString() ?? "";
            if (text.Length > Width)
                text = text.Substring(0, Width - 2) + "..";

            return Alignment switch
            {
                Align.Left => text.PadRight(Width),
                Align.Right => text.PadLeft(Width),
                Align.Center => PadCenter(text, Width),
                _ => text.PadRight(Width)
            };
        }

        private string PadCenter(string text, int width)
        {
            int padding = width - text.Length;
            int padLeft = padding / 2;
            int padRight = padding - padLeft;
            return new string(' ', padLeft) + text + new string(' ', padRight);
        }
    }

    // ═══════════════════════════════════════════
    // GenericTable<T>
    // ═══════════════════════════════════════════

    class GenericTable<T>
    {
        private List<T> _rows = new List<T>();
        private List<ColumnDefinition<T>> _columns = new List<ColumnDefinition<T>>();
        private string _tableName;

        public int RowCount => _rows.Count;
        public int ColumnCount => _columns.Count;

        public GenericTable(string tableName = "Table")
        {
            _tableName = tableName;
        }

        // ---- Column Management ----

        public GenericTable<T> AddColumn(string name, Func<T, object> selector,
                                         int width = 15, Align alignment = Align.Left)
        {
            _columns.Add(new ColumnDefinition<T>(name, selector, width, alignment));
            return this; // Fluent API
        }

        // ---- Row Management ----

        public GenericTable<T> AddRow(T item)
        {
            _rows.Add(item);
            return this;
        }

        public GenericTable<T> AddRows(IEnumerable<T> items)
        {
            _rows.AddRange(items);
            return this;
        }

        public int RemoveRow(Func<T, bool> predicate)
        {
            return _rows.RemoveAll(item => predicate(item));
        }

        // ---- Display ----

        public void Display()
        {
            if (_columns.Count == 0)
            {
                Console.WriteLine("  ⚠️ Chưa định nghĩa cột nào!");
                return;
            }

            Console.WriteLine($"\n  📊 {_tableName} ({RowCount} rows)\n");

            // Header
            Console.Write("  ");
            foreach (var col in _columns)
            {
                Console.Write(col.FormatValue(col.Name) + " ");
            }
            Console.WriteLine();

            // Separator
            Console.Write("  ");
            foreach (var col in _columns)
            {
                Console.Write(new string('-', col.Width) + " ");
            }
            Console.WriteLine();

            // Rows
            foreach (T row in _rows)
            {
                Console.Write("  ");
                foreach (var col in _columns)
                {
                    object value = col.Selector(row);
                    Console.Write(col.FormatValue(value) + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayFormatted()
        {
            if (_columns.Count == 0)
            {
                Console.WriteLine("  ⚠️ Chưa định nghĩa cột nào!");
                return;
            }

            Console.WriteLine($"\n  📊 {_tableName} ({RowCount} rows)\n");

            // Build border strings
            string topBorder = "  ┌" + string.Join("┬", 
                _columns.Select(c => new string('─', c.Width + 2))) + "┐";
            string midBorder = "  ├" + string.Join("┼", 
                _columns.Select(c => new string('─', c.Width + 2))) + "┤";
            string botBorder = "  └" + string.Join("┴", 
                _columns.Select(c => new string('─', c.Width + 2))) + "┘";

            // Top border
            Console.WriteLine(topBorder);

            // Header
            Console.Write("  │");
            foreach (var col in _columns)
            {
                Console.Write($" {col.FormatValue(col.Name)} │");
            }
            Console.WriteLine();

            // Middle border
            Console.WriteLine(midBorder);

            // Rows
            foreach (T row in _rows)
            {
                Console.Write("  │");
                foreach (var col in _columns)
                {
                    object value = col.Selector(row);
                    Console.Write($" {col.FormatValue(value)} │");
                }
                Console.WriteLine();
            }

            // Bottom border
            Console.WriteLine(botBorder);
        }

        // ---- Sort ----

        public GenericTable<T> Sort<TKey>(Func<T, TKey> keySelector, 
                                          bool descending = false)
            where TKey : IComparable<TKey>
        {
            if (descending)
                _rows = _rows.OrderByDescending(keySelector).ToList();
            else
                _rows = _rows.OrderBy(keySelector).ToList();

            return this;
        }

        // ---- Filter ----

        public GenericTable<T> Filter(Func<T, bool> predicate)
        {
            GenericTable<T> filtered = new GenericTable<T>($"{_tableName} (filtered)");

            // Copy columns
            foreach (var col in _columns)
            {
                filtered.AddColumn(col.Name, col.Selector, col.Width, col.Alignment);
            }

            // Filter rows
            filtered.AddRows(_rows.Where(predicate));
            return filtered;
        }

        // ---- Take / Skip ----

        public GenericTable<T> Take(int count)
        {
            GenericTable<T> result = new GenericTable<T>($"{_tableName} (top {count})");
            foreach (var col in _columns)
                result.AddColumn(col.Name, col.Selector, col.Width, col.Alignment);
            result.AddRows(_rows.Take(count));
            return result;
        }

        public GenericTable<T> Skip(int count)
        {
            GenericTable<T> result = new GenericTable<T>($"{_tableName} (skip {count})");
            foreach (var col in _columns)
                result.AddColumn(col.Name, col.Selector, col.Width, col.Alignment);
            result.AddRows(_rows.Skip(count));
            return result;
        }

        // ---- Aggregate Functions ----

        public double Sum(Func<T, double> selector)
        {
            double sum = 0;
            foreach (T item in _rows)
                sum += selector(item);
            return sum;
        }

        public double Average(Func<T, double> selector)
        {
            if (RowCount == 0) return 0;
            return Sum(selector) / RowCount;
        }

        public TKey Min<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            if (RowCount == 0)
                throw new InvalidOperationException("Table rỗng!");
            return _rows.Min(selector)!;
        }

        public TKey Max<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            if (RowCount == 0)
                throw new InvalidOperationException("Table rỗng!");
            return _rows.Max(selector)!;
        }

        public int Count(Func<T, bool>? predicate = null)
        {
            if (predicate == null) return RowCount;
            return _rows.Count(predicate);
        }

        // ---- GroupBy ----

        public Dictionary<TKey, List<T>> GroupBy<TKey>(Func<T, TKey> keySelector)
            where TKey : notnull
        {
            Dictionary<TKey, List<T>> groups = new();
            foreach (T item in _rows)
            {
                TKey key = keySelector(item);
                if (!groups.ContainsKey(key))
                    groups[key] = new List<T>();
                groups[key].Add(item);
            }
            return groups;
        }

        // ---- ForEach ----

        public void ForEach(Action<T> action)
        {
            foreach (T item in _rows)
                action(item);
        }

        // ---- Export CSV ----

        public void ExportToCsv(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Header
            sb.AppendLine(string.Join(",", _columns.Select(c => $"\"{c.Name}\"")));

            // Rows
            foreach (T row in _rows)
            {
                var values = _columns.Select(c =>
                {
                    object val = c.Selector(row);
                    return $"\"{val}\"";
                });
                sb.AppendLine(string.Join(",", values));
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"  💾 Exported to {filePath} ({RowCount} rows)");
        }

        // ---- ShowStats ----

        public void ShowStats(string columnName, Func<T, double> selector)
        {
            Console.WriteLine($"\n  📈 Thống kê cột \"{columnName}\":");
            Console.WriteLine($"     Count:   {RowCount}");
            Console.WriteLine($"     Sum:     {Sum(selector):F2}");
            Console.WriteLine($"     Average: {Average(selector):F2}");
            Console.WriteLine($"     Min:     {Min(selector):F2}");
            Console.WriteLine($"     Max:     {Max(selector):F2}");
        }
    }

    // ═══════════════════════════════════════════
    // Sample Data Classes
    // ═══════════════════════════════════════════

    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Department { get; set; } = "";
        public decimal Salary { get; set; }
        public int YearsExp { get; set; }

        public Employee(int id, string name, string dept, decimal salary, int years)
        {
            Id = id; Name = name; Department = dept;
            Salary = salary; YearsExp = years;
        }
    }

    class BookInfo
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int Pages { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }

        public BookInfo(string title, string author, int pages, double rating, int year)
        {
            Title = title; Author = author; Pages = pages;
            Rating = rating; Year = year;
        }
    }

    // ═══════════════════════════════════════════
    // Main Program
    // ═══════════════════════════════════════════

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║     📊 GENERIC DATA TABLE CHALLENGE      ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝\n");

            DemoEmployeeTable();
            DemoBookTable();
        }

        static void DemoEmployeeTable()
        {
            Console.WriteLine("═══ DEMO 1: Employee Table ═══");

            // Tạo table
            var table = new GenericTable<Employee>("Nhân viên");

            // Định nghĩa cột
            table
                .AddColumn("ID", e => e.Id, 4, Align.Right)
                .AddColumn("Tên", e => e.Name, 18, Align.Left)
                .AddColumn("Phòng ban", e => e.Department, 12, Align.Left)
                .AddColumn("Lương", e => $"{e.Salary:N0}đ", 15, Align.Right)
                .AddColumn("Kinh nghiệm", e => $"{e.YearsExp} năm", 12, Align.Center);

            // Thêm dữ liệu
            table.AddRows(new List<Employee>
            {
                new(1, "Nguyễn Minh", "Engineering", 25_000_000, 5),
                new(2, "Trần Lan", "Design", 18_000_000, 3),
                new(3, "Lê Hùng", "Engineering", 30_000_000, 7),
                new(4, "Phạm Mai", "Marketing", 20_000_000, 4),
                new(5, "Hoàng Tuấn", "Engineering", 35_000_000, 10),
                new(6, "Đỗ Linh", "Design", 22_000_000, 5),
                new(7, "Vũ Anh", "Marketing", 15_000_000, 2),
                new(8, "Bùi Trang", "HR", 19_000_000, 3),
            });

            // Hiển thị đẹp
            table.DisplayFormatted();

            // Thống kê lương
            table.ShowStats("Lương", e => (double)e.Salary);

            // Sort theo lương giảm dần
            Console.WriteLine("\n--- Sort theo lương (giảm dần) ---");
            table.Sort<decimal>(e => e.Salary, descending: true);
            table.DisplayFormatted();

            // Filter — chỉ Engineering
            Console.WriteLine("--- Filter: Phòng Engineering ---");
            var engineeringTable = table.Filter(e => e.Department == "Engineering");
            engineeringTable.DisplayFormatted();
            engineeringTable.ShowStats("Lương Engineering", e => (double)e.Salary);

            // Top 3 lương cao nhất
            Console.WriteLine("--- Top 3 lương cao nhất ---");
            table.Sort<decimal>(e => e.Salary, descending: true)
                 .Take(3)
                 .DisplayFormatted();

            // GroupBy Department
            Console.WriteLine("\n--- GroupBy Department ---\n");
            var groups = table.GroupBy(e => e.Department);
            foreach (var group in groups)
            {
                double avgSalary = 0;
                foreach (var emp in group.Value)
                    avgSalary += (double)emp.Salary;
                avgSalary /= group.Value.Count;

                Console.WriteLine($"  📁 {group.Key}: {group.Value.Count} người, " +
                                  $"Lương TB: {avgSalary:N0}đ");
            }

            // Count với điều kiện
            Console.WriteLine($"\n  📊 Nhân viên > 5 năm KN: " +
                              $"{table.Count(e => e.YearsExp > 5)}");
            Console.WriteLine($"  📊 Nhân viên lương > 20tr: " +
                              $"{table.Count(e => e.Salary > 20_000_000)}");
        }

        static void DemoBookTable()
        {
            Console.WriteLine("\n\n═══ DEMO 2: Book Table ═══");

            var bookTable = new GenericTable<BookInfo>("Sách");

            bookTable
                .AddColumn("Tiêu đề", b => b.Title, 28, Align.Left)
                .AddColumn("Tác giả", b => b.Author, 20, Align.Left)
                .AddColumn("Trang", b => b.Pages, 6, Align.Right)
                .AddColumn("Rating", b => $"⭐{b.Rating:F1}", 8, Align.Center)
                .AddColumn("Năm", b => b.Year, 5, Align.Right);

            bookTable.AddRows(new List<BookInfo>
            {
                new("Clean Code", "Robert C. Martin", 464, 4.7, 2008),
                new("Design Patterns", "Gang of Four", 395, 4.2, 1994),
                new("The Pragmatic Programmer", "Hunt & Thomas", 352, 4.6, 1999),
                new("C# in Depth", "Jon Skeet", 528, 4.8, 2019),
                new("Head First Design Patterns", "Eric Freeman", 694, 4.5, 2004),
                new("Refactoring", "Martin Fowler", 448, 4.6, 2018),
            });

            bookTable.DisplayFormatted();

            // Sort theo rating
            Console.WriteLine("--- Sort theo Rating (cao nhất) ---");
            bookTable.Sort(b => b.Rating, descending: true);
            bookTable.DisplayFormatted();

            // Filter — sách dày > 400 trang
            Console.WriteLine("--- Sách > 400 trang ---");
            bookTable.Filter(b => b.Pages > 400).DisplayFormatted();

            // Stats
            bookTable.ShowStats("Số trang", b => b.Pages);
            bookTable.ShowStats("Rating", b => b.Rating);

            // Tóm tắt
            Console.WriteLine("\n  ╔══════════════════════════════════════════════╗");
            Console.WriteLine("  ║  GenericTable<T> — 1 class, NHIỀU kiểu!     ║");
            Console.WriteLine("  ║                                             ║");
            Console.WriteLine("  ║  GenericTable<Employee>  — bảng nhân viên   ║");
            Console.WriteLine("  ║  GenericTable<BookInfo>  — bảng sách        ║");
            Console.WriteLine("  ║  GenericTable<Product>   — bảng sản phẩm    ║");
            Console.WriteLine("  ║  GenericTable<AnyClass>  — bảng bất kỳ!     ║");
            Console.WriteLine("  ║                                             ║");
            Console.WriteLine("  ║  → Đây chính là sức mạnh của Generic! 🎯    ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        }
    }
}
```

---

## 📊 Checklist hoàn thành

| # | Yêu cầu | Hoàn thành |
|---|---------|------------|
| 1 | GenericTable\<T> class | ⬜ |
| 2 | ColumnDefinition\<T> với Selector | ⬜ |
| 3 | AddRow / AddRows | ⬜ |
| 4 | DisplayFormatted (bảng có border) | ⬜ |
| 5 | Sort ascending/descending | ⬜ |
| 6 | Filter trả GenericTable mới | ⬜ |
| 7 | Sum, Average, Min, Max, Count | ⬜ |
| 8 | Demo với ≥ 2 kiểu T khác nhau | ⬜ |
| 9 | GroupBy (bonus) | ⬜ |
| 10 | ExportToCsv (bonus) | ⬜ |
| 11 | Take / Skip (bonus) | ⬜ |
| 12 | Fluent API (method chaining) | ⬜ |

> 🏆 **Hoàn thành 8/12** = Xuất sắc! Bạn đã hiểu sâu Generic!
