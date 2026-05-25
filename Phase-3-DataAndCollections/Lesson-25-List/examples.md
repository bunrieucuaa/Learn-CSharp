# Bài 25: List<T> — Ví Dụ Minh Họa

---

## Ví dụ 1: List CRUD Cơ Bản — Thêm / Xóa / Tìm / Sắp Xếp

> 🎯 Thực hành tất cả các thao tác cơ bản trên List<int>

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VÍ DỤ 1: LIST CRUD CƠ BẢN ===\n");

        // ========== 1. TẠO LIST ==========
        List<int> numbers = new List<int> { 10, 25, 7, 42, 15 };
        Console.WriteLine("📋 Danh sách ban đầu:");
        PrintList(numbers);

        // ========== 2. THÊM PHẦN TỬ ==========
        Console.WriteLine("\n--- THÊM ---");

        // Add — thêm vào cuối
        numbers.Add(30);
        Console.WriteLine("Sau Add(30):");
        PrintList(numbers);

        // AddRange — thêm nhiều phần tử
        numbers.AddRange(new[] { 5, 50, 20 });
        Console.WriteLine("Sau AddRange({5, 50, 20}):");
        PrintList(numbers);

        // Insert — chèn tại vị trí
        numbers.Insert(0, 99);
        Console.WriteLine("Sau Insert(0, 99) — chèn 99 vào đầu:");
        PrintList(numbers);

        // ========== 3. TÌM KIẾM ==========
        Console.WriteLine("\n--- TÌM KIẾM ---");

        // Contains
        Console.WriteLine($"Chứa 42? {numbers.Contains(42)}");       // True
        Console.WriteLine($"Chứa 100? {numbers.Contains(100)}");     // False

        // IndexOf
        int idx = numbers.IndexOf(42);
        Console.WriteLine($"Vị trí của 42: index {idx}");

        // Find — tìm phần tử đầu tiên > 40
        int found = numbers.Find(n => n > 40);
        Console.WriteLine($"Số đầu tiên > 40: {found}");

        // FindAll — tìm tất cả số chẵn
        List<int> evens = numbers.FindAll(n => n % 2 == 0);
        Console.Write("Tất cả số chẵn: ");
        PrintList(evens);

        // Count
        Console.WriteLine($"Tổng số phần tử: {numbers.Count}");

        // ========== 4. SẮP XẾP ==========
        Console.WriteLine("\n--- SẮP XẾP ---");

        // Sort tăng dần
        numbers.Sort();
        Console.WriteLine("Sau Sort() — tăng dần:");
        PrintList(numbers);

        // Sort giảm dần
        numbers.Sort((a, b) => b.CompareTo(a));
        Console.WriteLine("Sau Sort giảm dần:");
        PrintList(numbers);

        // Reverse
        numbers.Reverse();
        Console.WriteLine("Sau Reverse():");
        PrintList(numbers);

        // ========== 5. XÓA ==========
        Console.WriteLine("\n--- XÓA ---");

        // Remove — xóa giá trị đầu tiên khớp
        numbers.Remove(99);
        Console.WriteLine("Sau Remove(99):");
        PrintList(numbers);

        // RemoveAt — xóa theo index
        numbers.RemoveAt(0);
        Console.WriteLine($"Sau RemoveAt(0) — xóa phần tử đầu:");
        PrintList(numbers);

        // RemoveAll — xóa tất cả số < 10
        int removed = numbers.RemoveAll(n => n < 10);
        Console.WriteLine($"Sau RemoveAll(< 10) — đã xóa {removed} phần tử:");
        PrintList(numbers);

        // ========== 6. CAPACITY vs COUNT ==========
        Console.WriteLine("\n--- CAPACITY vs COUNT ---");
        Console.WriteLine($"Count: {numbers.Count}");
        Console.WriteLine($"Capacity: {numbers.Capacity}");
        numbers.TrimExcess();
        Console.WriteLine($"Sau TrimExcess() — Capacity: {numbers.Capacity}");
    }

    static void PrintList(List<int> list)
    {
        Console.WriteLine($"  [{string.Join(", ", list)}]");
    }
}
```

**Kết quả mong đợi:**
```
=== VÍ DỤ 1: LIST CRUD CƠ BẢN ===

📋 Danh sách ban đầu:
  [10, 25, 7, 42, 15]

--- THÊM ---
Sau Add(30):
  [10, 25, 7, 42, 15, 30]
Sau AddRange({5, 50, 20}):
  [10, 25, 7, 42, 15, 30, 5, 50, 20]
Sau Insert(0, 99) — chèn 99 vào đầu:
  [99, 10, 25, 7, 42, 15, 30, 5, 50, 20]

--- TÌM KIẾM ---
Chứa 42? True
Chứa 100? False
Vị trí của 42: index 4
Số đầu tiên > 40: 99
Tất cả số chẵn:
  [10, 42, 30, 50, 20]
Tổng số phần tử: 10

--- SẮP XẾP ---
Sau Sort() — tăng dần:
  [5, 7, 10, 15, 20, 25, 30, 42, 50, 99]
Sau Sort giảm dần:
  [99, 50, 42, 30, 25, 20, 15, 10, 7, 5]
Sau Reverse():
  [5, 7, 10, 15, 20, 25, 30, 42, 50, 99]

--- XÓA ---
Sau Remove(99):
  [5, 7, 10, 15, 20, 25, 30, 42, 50]
Sau RemoveAt(0) — xóa phần tử đầu:
  [7, 10, 15, 20, 25, 30, 42, 50]
Sau RemoveAll(< 10) — đã xóa 1 phần tử:
  [10, 15, 20, 25, 30, 42, 50]

--- CAPACITY vs COUNT ---
Count: 7
Capacity: 16
Sau TrimExcess() — Capacity: 7
```

---

## Ví dụ 2: List\<Student\> — Quản Lý Sinh Viên Với Class Objects

> 🎯 Kết hợp List với OOP — thêm, tìm, sắp xếp, lọc đối tượng Student

```csharp
using System;
using System.Collections.Generic;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Gpa { get; set; }
    public string Major { get; set; }

    public Student(int id, string name, double gpa, string major)
    {
        Id = id;
        Name = name;
        Gpa = gpa;
        Major = major;
    }

    public override string ToString()
    {
        return $"  [{Id}] {Name,-15} GPA: {Gpa:F1}  Ngành: {Major}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VÍ DỤ 2: QUẢN LÝ SINH VIÊN ===\n");

        // ========== 1. TẠO DANH SÁCH SINH VIÊN ==========
        List<Student> students = new List<Student>
        {
            new Student(1, "Nguyễn Văn An",    8.5, "CNTT"),
            new Student(2, "Trần Thị Bình",    9.2, "Kinh tế"),
            new Student(3, "Lê Hoàng Chi",     7.0, "CNTT"),
            new Student(4, "Phạm Minh Dũng",   6.5, "Cơ khí"),
            new Student(5, "Hoàng Thị Em",     8.8, "Kinh tế"),
            new Student(6, "Vũ Đức Phong",     9.5, "CNTT")
        };

        Console.WriteLine("📋 Danh sách sinh viên:");
        PrintStudents(students);

        // ========== 2. THÊM SINH VIÊN MỚI ==========
        students.Add(new Student(7, "Đỗ Thị Giang", 7.8, "Y khoa"));
        Console.WriteLine("\n✅ Đã thêm Đỗ Thị Giang");

        // ========== 3. TÌM KIẾM ==========
        Console.WriteLine("\n--- TÌM KIẾM ---");

        // Tìm sinh viên theo ID
        Student found = students.Find(s => s.Id == 3);
        Console.WriteLine($"Sinh viên ID=3: {found?.Name ?? "Không tìm thấy"}");

        // Tìm sinh viên GPA cao nhất
        Student topStudent = students.Find(s =>
            s.Gpa == students.ConvertAll(st => st.Gpa).FindAll(g => true)
                .ConvertAll(g => g).TrueForAll(g => true) ? s.Gpa : 0
        );

        // Cách đơn giản hơn: sort rồi lấy đầu tiên
        List<Student> sorted = new List<Student>(students);
        sorted.Sort((a, b) => b.Gpa.CompareTo(a.Gpa));
        Console.WriteLine($"Sinh viên GPA cao nhất: {sorted[0].Name} ({sorted[0].Gpa})");

        // Tìm tất cả sinh viên ngành CNTT
        List<Student> itStudents = students.FindAll(s => s.Major == "CNTT");
        Console.WriteLine($"\nSinh viên ngành CNTT ({itStudents.Count} người):");
        PrintStudents(itStudents);

        // Tìm sinh viên xuất sắc (GPA >= 8.5)
        List<Student> excellent = students.FindAll(s => s.Gpa >= 8.5);
        Console.WriteLine($"\nSinh viên xuất sắc - GPA >= 8.5 ({excellent.Count} người):");
        PrintStudents(excellent);

        // Kiểm tra có sinh viên nào ngành Y khoa không?
        bool hasDoctor = students.Exists(s => s.Major == "Y khoa");
        Console.WriteLine($"\nCó sinh viên Y khoa? {hasDoctor}");

        // ========== 4. SẮP XẾP ==========
        Console.WriteLine("\n--- SẮP XẾP ---");

        // Sắp xếp theo GPA giảm dần
        students.Sort((a, b) => b.Gpa.CompareTo(a.Gpa));
        Console.WriteLine("Sắp xếp theo GPA (giảm dần):");
        PrintStudents(students);

        // Sắp xếp theo tên A-Z
        students.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
        Console.WriteLine("\nSắp xếp theo tên (A-Z):");
        PrintStudents(students);

        // ========== 5. XÓA ==========
        Console.WriteLine("\n--- XÓA ---");

        // Xóa sinh viên có GPA < 7.0
        int removed = students.RemoveAll(s => s.Gpa < 7.0);
        Console.WriteLine($"Đã xóa {removed} sinh viên có GPA < 7.0");
        Console.WriteLine("Danh sách còn lại:");
        PrintStudents(students);

        // ========== 6. THỐNG KÊ ==========
        Console.WriteLine("\n--- THỐNG KÊ ---");
        double totalGpa = 0;
        students.ForEach(s => totalGpa += s.Gpa);
        double avgGpa = totalGpa / students.Count;
        Console.WriteLine($"Tổng số SV: {students.Count}");
        Console.WriteLine($"GPA trung bình: {avgGpa:F2}");
        Console.WriteLine($"Tất cả đạt >= 7.0? {students.TrueForAll(s => s.Gpa >= 7.0)}");
    }

    static void PrintStudents(List<Student> students)
    {
        students.ForEach(s => Console.WriteLine(s));
    }
}
```

**Kết quả mong đợi:**
```
=== VÍ DỤ 2: QUẢN LÝ SINH VIÊN ===

📋 Danh sách sinh viên:
  [1] Nguyễn Văn An    GPA: 8.5  Ngành: CNTT
  [2] Trần Thị Bình    GPA: 9.2  Ngành: Kinh tế
  [3] Lê Hoàng Chi     GPA: 7.0  Ngành: CNTT
  [4] Phạm Minh Dũng   GPA: 6.5  Ngành: Cơ khí
  [5] Hoàng Thị Em     GPA: 8.8  Ngành: Kinh tế
  [6] Vũ Đức Phong     GPA: 9.5  Ngành: CNTT

✅ Đã thêm Đỗ Thị Giang

--- TÌM KIẾM ---
Sinh viên ID=3: Lê Hoàng Chi
Sinh viên GPA cao nhất: Vũ Đức Phong (9.5)

Sinh viên ngành CNTT (3 người):
  [1] Nguyễn Văn An    GPA: 8.5  Ngành: CNTT
  [3] Lê Hoàng Chi     GPA: 7.0  Ngành: CNTT
  [6] Vũ Đức Phong     GPA: 9.5  Ngành: CNTT

Sinh viên xuất sắc - GPA >= 8.5 (4 người):
  [1] Nguyễn Văn An    GPA: 8.5  Ngành: CNTT
  [2] Trần Thị Bình    GPA: 9.2  Ngành: Kinh tế
  [5] Hoàng Thị Em     GPA: 8.8  Ngành: Kinh tế
  [6] Vũ Đức Phong     GPA: 9.5  Ngành: CNTT

Có sinh viên Y khoa? True

--- SẮP XẾP ---
Sắp xếp theo GPA (giảm dần):
  [6] Vũ Đức Phong     GPA: 9.5  Ngành: CNTT
  [2] Trần Thị Bình    GPA: 9.2  Ngành: Kinh tế
  [5] Hoàng Thị Em     GPA: 8.8  Ngành: Kinh tế
  [1] Nguyễn Văn An    GPA: 8.5  Ngành: CNTT
  [7] Đỗ Thị Giang     GPA: 7.8  Ngành: Y khoa
  [3] Lê Hoàng Chi     GPA: 7.0  Ngành: CNTT
  [4] Phạm Minh Dũng   GPA: 6.5  Ngành: Cơ khí

Sắp xếp theo tên (A-Z):
  ...

--- XÓA ---
Đã xóa 1 sinh viên có GPA < 7.0
Danh sách còn lại:
  ...

--- THỐNG KÊ ---
Tổng số SV: 6
GPA trung bình: 8.47
Tất cả đạt >= 7.0? True
```

---

## Ví dụ 3: List Methods Nâng Cao — Find, FindAll, RemoveAll, Exists, ForEach

> 🎯 Tập trung vào các phương thức dùng lambda expression (Predicate, Action)

```csharp
using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, string category, int stock)
    {
        Name = name;
        Price = price;
        Category = category;
        Stock = stock;
    }

    public override string ToString()
    {
        string stockStatus = Stock > 0 ? $"Còn {Stock}" : "HẾT HÀNG";
        return $"  {Name,-20} {Price,12:N0}đ  [{Category}]  {stockStatus}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VÍ DỤ 3: LIST METHODS NÂNG CAO ===\n");

        List<Product> products = new List<Product>
        {
            new Product("iPhone 15",       25_000_000, "Điện thoại",  10),
            new Product("Samsung Galaxy",  20_000_000, "Điện thoại",  0),
            new Product("MacBook Pro",     45_000_000, "Laptop",      5),
            new Product("Dell XPS 13",     30_000_000, "Laptop",      8),
            new Product("AirPods Pro",      6_000_000, "Phụ kiện",   20),
            new Product("Bàn phím cơ",      2_500_000, "Phụ kiện",    0),
            new Product("iPad Air",        18_000_000, "Tablet",      3),
            new Product("Chuột Logitech",   1_500_000, "Phụ kiện",   15),
            new Product("Lenovo ThinkPad",  28_000_000, "Laptop",     0)
        };

        Console.WriteLine("📋 TẤT CẢ SẢN PHẨM:");
        products.ForEach(p => Console.WriteLine(p));

        // ========== FIND — Tìm 1 phần tử ==========
        Console.WriteLine("\n🔍 FIND — Tìm phần tử đầu tiên:");

        Product cheapest = products.Find(p => p.Price < 2_000_000);
        Console.WriteLine($"Sản phẩm < 2 triệu (đầu tiên): {cheapest?.Name}");

        Product expensiveLaptop = products.Find(p =>
            p.Category == "Laptop" && p.Price > 40_000_000);
        Console.WriteLine($"Laptop > 40 triệu: {expensiveLaptop?.Name}");

        // ========== FINDLAST — Tìm từ cuối ==========
        Product lastPhone = products.FindLast(p => p.Category == "Điện thoại");
        Console.WriteLine($"Điện thoại cuối cùng trong list: {lastPhone?.Name}");

        // ========== FINDALL — Tìm tất cả ==========
        Console.WriteLine("\n📦 FINDALL — Lọc nhiều phần tử:");

        List<Product> accessories = products.FindAll(p => p.Category == "Phụ kiện");
        Console.WriteLine($"\nPhụ kiện ({accessories.Count} sản phẩm):");
        accessories.ForEach(p => Console.WriteLine(p));

        List<Product> inStock = products.FindAll(p => p.Stock > 0);
        Console.WriteLine($"\nCòn hàng ({inStock.Count} sản phẩm):");
        inStock.ForEach(p => Console.WriteLine(p));

        List<Product> expensive = products.FindAll(p => p.Price >= 20_000_000);
        Console.WriteLine($"\nSản phẩm >= 20 triệu ({expensive.Count}):");
        expensive.ForEach(p => Console.WriteLine(p));

        // ========== FINDINDEX — Tìm vị trí ==========
        Console.WriteLine("\n📍 FINDINDEX:");

        int idxTablet = products.FindIndex(p => p.Category == "Tablet");
        Console.WriteLine($"Vị trí Tablet đầu tiên: index {idxTablet}");

        int idxExpensive = products.FindIndex(p => p.Price > 40_000_000);
        Console.WriteLine($"Vị trí SP > 40 triệu đầu tiên: index {idxExpensive}");

        // ========== EXISTS — Kiểm tra tồn tại ==========
        Console.WriteLine("\n❓ EXISTS — Kiểm tra:");

        bool hasOutOfStock = products.Exists(p => p.Stock == 0);
        Console.WriteLine($"Có sản phẩm hết hàng? {hasOutOfStock}");

        bool hasGaming = products.Exists(p => p.Category == "Gaming");
        Console.WriteLine($"Có sản phẩm Gaming? {hasGaming}");

        bool hasCheap = products.Exists(p => p.Price < 1_000_000);
        Console.WriteLine($"Có SP dưới 1 triệu? {hasCheap}");

        // ========== TRUEFORALL — Kiểm tra tất cả ==========
        Console.WriteLine("\n✅ TRUEFORALL:");

        bool allHavePrice = products.TrueForAll(p => p.Price > 0);
        Console.WriteLine($"Tất cả đều có giá > 0? {allHavePrice}");

        bool allInStock = products.TrueForAll(p => p.Stock > 0);
        Console.WriteLine($"Tất cả đều còn hàng? {allInStock}");

        // ========== REMOVEALL — Xóa hàng loạt ==========
        Console.WriteLine("\n🗑️ REMOVEALL — Xóa sản phẩm hết hàng:");

        int removedCount = products.RemoveAll(p => p.Stock == 0);
        Console.WriteLine($"Đã xóa {removedCount} sản phẩm hết hàng");
        Console.WriteLine($"Còn lại {products.Count} sản phẩm:");
        products.ForEach(p => Console.WriteLine(p));

        // ========== CONVERTALL — Chuyển đổi ==========
        Console.WriteLine("\n🔄 CONVERTALL — Lấy danh sách tên:");

        List<string> productNames = products.ConvertAll(p => p.Name);
        Console.WriteLine("Tên sản phẩm:");
        productNames.ForEach(name => Console.WriteLine($"  - {name}"));

        List<string> priceLabels = products.ConvertAll(p =>
            $"{p.Name}: {p.Price:N0}đ");
        Console.WriteLine("\nBảng giá:");
        priceLabels.ForEach(label => Console.WriteLine($"  {label}"));
    }
}
```

---

## Ví dụ 4: TodoList App Hoàn Chỉnh — Add / Remove / Toggle / Filter / Stats

> 🎯 Ứng dụng thực tế kết hợp tất cả kiến thức List<T>

```csharp
using System;
using System.Collections.Generic;

class TodoItem
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; }
    public string Priority { get; set; } // "Cao", "Trung bình", "Thấp"

    public TodoItem(string title, string priority = "Trung bình")
    {
        Id = _nextId++;
        Title = title;
        Priority = priority;
        IsCompleted = false;
        CreatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "✅" : "⬜";
        string priorityIcon = Priority switch
        {
            "Cao" => "🔴",
            "Trung bình" => "🟡",
            "Thấp" => "🟢",
            _ => "⚪"
        };
        return $"  {status} [{Id}] {Title,-30} {priorityIcon} {Priority,-12} ({CreatedAt:dd/MM/yyyy})";
    }
}

class TodoApp
{
    private List<TodoItem> _todos = new List<TodoItem>();

    // ➕ Thêm công việc
    public void AddTodo(string title, string priority = "Trung bình")
    {
        _todos.Add(new TodoItem(title, priority));
        Console.WriteLine($"✅ Đã thêm: \"{title}\"");
    }

    // 🗑️ Xóa công việc theo ID
    public void RemoveTodo(int id)
    {
        TodoItem todo = _todos.Find(t => t.Id == id);
        if (todo != null)
        {
            _todos.Remove(todo);
            Console.WriteLine($"🗑️ Đã xóa: \"{todo.Title}\"");
        }
        else
        {
            Console.WriteLine($"❌ Không tìm thấy todo ID={id}");
        }
    }

    // 🔄 Toggle trạng thái hoàn thành
    public void ToggleTodo(int id)
    {
        TodoItem todo = _todos.Find(t => t.Id == id);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
            string status = todo.IsCompleted ? "hoàn thành" : "chưa hoàn thành";
            Console.WriteLine($"🔄 \"{todo.Title}\" → {status}");
        }
        else
        {
            Console.WriteLine($"❌ Không tìm thấy todo ID={id}");
        }
    }

    // 📋 Hiển thị tất cả
    public void ShowAll()
    {
        Console.WriteLine($"\n📋 TẤT CẢ CÔNG VIỆC ({_todos.Count}):");
        Console.WriteLine(new string('─', 70));
        if (_todos.Count == 0)
        {
            Console.WriteLine("  (Trống — chưa có công việc nào)");
        }
        else
        {
            _todos.ForEach(t => Console.WriteLine(t));
        }
        Console.WriteLine(new string('─', 70));
    }

    // 🔍 Lọc theo trạng thái
    public void ShowByStatus(bool completed)
    {
        List<TodoItem> filtered = _todos.FindAll(t => t.IsCompleted == completed);
        string label = completed ? "ĐÃ HOÀN THÀNH" : "CHƯA HOÀN THÀNH";
        Console.WriteLine($"\n📋 {label} ({filtered.Count}):");
        if (filtered.Count == 0)
            Console.WriteLine("  (Không có)");
        else
            filtered.ForEach(t => Console.WriteLine(t));
    }

    // 🔍 Lọc theo độ ưu tiên
    public void ShowByPriority(string priority)
    {
        List<TodoItem> filtered = _todos.FindAll(t => t.Priority == priority);
        Console.WriteLine($"\n📋 ĐỘ ƯU TIÊN: {priority} ({filtered.Count}):");
        if (filtered.Count == 0)
            Console.WriteLine("  (Không có)");
        else
            filtered.ForEach(t => Console.WriteLine(t));
    }

    // 🔍 Tìm kiếm theo từ khóa
    public void Search(string keyword)
    {
        List<TodoItem> results = _todos.FindAll(t =>
            t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\n🔍 Kết quả tìm \"{keyword}\" ({results.Count}):");
        if (results.Count == 0)
            Console.WriteLine("  Không tìm thấy.");
        else
            results.ForEach(t => Console.WriteLine(t));
    }

    // 📊 Thống kê
    public void ShowStats()
    {
        int total = _todos.Count;
        int completed = _todos.FindAll(t => t.IsCompleted).Count;
        int pending = total - completed;
        int highPriority = _todos.FindAll(t => t.Priority == "Cao" && !t.IsCompleted).Count;

        Console.WriteLine("\n📊 THỐNG KÊ:");
        Console.WriteLine($"  Tổng công việc:        {total}");
        Console.WriteLine($"  Đã hoàn thành:         {completed}");
        Console.WriteLine($"  Chưa hoàn thành:       {pending}");
        Console.WriteLine($"  Ưu tiên cao (chưa xong): {highPriority}");

        if (total > 0)
        {
            double percent = (double)completed / total * 100;
            Console.WriteLine($"  Tiến độ:               {percent:F1}%");

            // Progress bar
            int barLength = 20;
            int filled = (int)(percent / 100 * barLength);
            string bar = new string('█', filled) + new string('░', barLength - filled);
            Console.WriteLine($"  [{bar}]");
        }
    }

    // 🗑️ Xóa tất cả đã hoàn thành
    public void ClearCompleted()
    {
        int removed = _todos.RemoveAll(t => t.IsCompleted);
        Console.WriteLine($"🗑️ Đã xóa {removed} công việc đã hoàn thành");
    }

    // 📊 Sắp xếp theo độ ưu tiên
    public void SortByPriority()
    {
        Dictionary<string, int> priorityOrder = new Dictionary<string, int>
        {
            { "Cao", 1 }, { "Trung bình", 2 }, { "Thấp", 3 }
        };

        _todos.Sort((a, b) =>
        {
            int pa = priorityOrder.ContainsKey(a.Priority) ? priorityOrder[a.Priority] : 99;
            int pb = priorityOrder.ContainsKey(b.Priority) ? priorityOrder[b.Priority] : 99;
            return pa.CompareTo(pb);
        });
        Console.WriteLine("📊 Đã sắp xếp theo độ ưu tiên (Cao → Thấp)");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║     📝 TODO LIST APPLICATION     ║");
        Console.WriteLine("╚══════════════════════════════════╝\n");

        TodoApp app = new TodoApp();

        // ========== THÊM CÔNG VIỆC ==========
        Console.WriteLine("--- THÊM CÔNG VIỆC ---");
        app.AddTodo("Học C# List<T>", "Cao");
        app.AddTodo("Làm bài tập OOP", "Cao");
        app.AddTodo("Đọc sách Design Patterns", "Trung bình");
        app.AddTodo("Xem video tutorial", "Thấp");
        app.AddTodo("Viết blog về C#", "Trung bình");
        app.AddTodo("Ôn thi cuối kỳ", "Cao");
        app.AddTodo("Dọn phòng", "Thấp");

        app.ShowAll();

        // ========== TOGGLE ==========
        Console.WriteLine("\n--- TOGGLE TRẠNG THÁI ---");
        app.ToggleTodo(1);  // Hoàn thành "Học C# List<T>"
        app.ToggleTodo(4);  // Hoàn thành "Xem video tutorial"
        app.ToggleTodo(7);  // Hoàn thành "Dọn phòng"

        app.ShowAll();

        // ========== LỌC ==========
        app.ShowByStatus(true);   // Đã hoàn thành
        app.ShowByStatus(false);  // Chưa hoàn thành
        app.ShowByPriority("Cao");

        // ========== TÌM KIẾM ==========
        app.Search("C#");
        app.Search("bài tập");

        // ========== SẮP XẾP ==========
        Console.WriteLine("\n--- SẮP XẾP ---");
        app.SortByPriority();
        app.ShowAll();

        // ========== THỐNG KÊ ==========
        app.ShowStats();

        // ========== XÓA ==========
        Console.WriteLine("\n--- DỌN DẸP ---");
        app.ClearCompleted();
        app.ShowAll();
        app.ShowStats();
    }
}
```

**Kết quả mong đợi:**
```
╔══════════════════════════════════╗
║     📝 TODO LIST APPLICATION     ║
╚══════════════════════════════════╝

--- THÊM CÔNG VIỆC ---
✅ Đã thêm: "Học C# List<T>"
✅ Đã thêm: "Làm bài tập OOP"
✅ Đã thêm: "Đọc sách Design Patterns"
✅ Đã thêm: "Xem video tutorial"
✅ Đã thêm: "Viết blog về C#"
✅ Đã thêm: "Ôn thi cuối kỳ"
✅ Đã thêm: "Dọn phòng"

📋 TẤT CẢ CÔNG VIỆC (7):
──────────────────────────────────────────────────────────────────────
  ⬜ [1] Học C# List<T>                  🔴 Cao          (25/05/2026)
  ⬜ [2] Làm bài tập OOP                🔴 Cao          (25/05/2026)
  ⬜ [3] Đọc sách Design Patterns       🟡 Trung bình   (25/05/2026)
  ⬜ [4] Xem video tutorial             🟢 Thấp         (25/05/2026)
  ⬜ [5] Viết blog về C#                🟡 Trung bình   (25/05/2026)
  ⬜ [6] Ôn thi cuối kỳ                 🔴 Cao          (25/05/2026)
  ⬜ [7] Dọn phòng                      🟢 Thấp         (25/05/2026)
──────────────────────────────────────────────────────────────────────
...

📊 THỐNG KÊ:
  Tổng công việc:        7
  Đã hoàn thành:         3
  Chưa hoàn thành:       4
  Ưu tiên cao (chưa xong): 2
  Tiến độ:               42.9%
  [████████░░░░░░░░░░░░]
```
