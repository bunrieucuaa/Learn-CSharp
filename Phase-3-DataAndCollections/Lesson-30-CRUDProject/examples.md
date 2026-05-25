# 💻 Bài 30: CRUD Console Project — Ví dụ hoàn chỉnh

> **Student Management System — Code hoàn chỉnh, chạy được ngay!**
> **Copy toàn bộ code dưới đây vào 1 file Program.cs trong Console App.**

---

## 🏗️ Kiến trúc tổng quan

```
Program.cs (all-in-one cho đơn giản)
├── IEntity              → Interface base cho entities
├── Student              → Model class (encapsulated, IComparable)
├── IRepository<T>       → Generic repository interface
├── GenericRepository<T> → Implementation với List<T> + Dictionary
├── StudentService       → Business logic layer
├── ConsoleUI            → Menu + display + input
└── Program              → Composition root
```

---

## Code hoàn chỉnh — Student Management System 🎓

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentManagement
{
    // ╔══════════════════════════════════════════════════════════╗
    // ║  TẦNG 1: MODEL — Định nghĩa dữ liệu                   ║
    // ╚══════════════════════════════════════════════════════════╝

    // ═══ Interface base cho mọi entity ═══
    interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }

    // ═══ STUDENT MODEL ═══
    class Student : IEntity, IComparable<Student>
    {
        // ── Private fields (encapsulation) ──
        private string _name = string.Empty;
        private int _age;
        private double _score;

        // ── IEntity ──
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        // ── Properties with validation ──
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên không được để trống!");
                _name = value.Trim();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 16 || value > 60)
                    throw new ArgumentOutOfRangeException(
                        nameof(Age), "Tuổi phải từ 16 đến 60!");
                _age = value;
            }
        }

        public double Score
        {
            get => _score;
            set
            {
                if (value < 0.0 || value > 10.0)
                    throw new ArgumentOutOfRangeException(
                        nameof(Score), "Điểm phải từ 0.0 đến 10.0!");
                _score = value;
            }
        }

        public string Email { get; set; } = string.Empty;

        // ── Constructor ──
        public Student() { CreatedAt = DateTime.Now; }

        public Student(int id, string name, int age, double score, string email = "")
        {
            Id = id;
            Name = name;
            Age = age;
            Score = score;
            Email = email;
            CreatedAt = DateTime.Now;
        }

        // ── Computed properties ──
        public string Grade => Score switch
        {
            >= 9.0 => "Xuất sắc",
            >= 8.0 => "Giỏi",
            >= 6.5 => "Khá",
            >= 5.0 => "Trung bình",
            >= 3.5 => "Yếu",
            _      => "Kém"
        };

        public string GradeEmoji => Score switch
        {
            >= 9.0 => "🌟",
            >= 8.0 => "✅",
            >= 6.5 => "👍",
            >= 5.0 => "📝",
            >= 3.5 => "⚠️",
            _      => "❌"
        };

        // ── IComparable — sort theo điểm giảm dần ──
        public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            int result = other.Score.CompareTo(Score);
            if (result == 0)
                result = Name.CompareTo(other.Name);
            return result;
        }

        // ── Display ──
        public override string ToString()
        {
            return $"  {Id,-4} │ {Name,-22} │ {Age,4} │ " +
                   $"{Score,5:F1} │ {GradeEmoji} {Grade,-10} │ {Email}";
        }

        public string ToShortString()
            => $"[{Id}] {Name} — {Score:F1} ({Grade})";
    }

    // ╔══════════════════════════════════════════════════════════╗
    // ║  TẦNG 2: REPOSITORY — Lưu trữ và truy xuất dữ liệu    ║
    // ╚══════════════════════════════════════════════════════════╝

    // ═══ Generic Repository Interface ═══
    interface IRepository<T> where T : IEntity
    {
        // CRUD
        bool Add(T entity);
        T? GetById(int id);
        IEnumerable<T> GetAll();
        bool Update(T entity);
        bool Delete(int id);

        // Query
        int Count { get; }
        bool Exists(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }

    // ═══ Generic Repository Implementation ═══
    class GenericRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> _items = new List<T>();
        private readonly Dictionary<int, T> _indexById = new Dictionary<int, T>();
        private int _nextId = 1;

        public int Count => _items.Count;

        // ── CREATE ──
        public bool Add(T entity)
        {
            if (entity == null) return false;

            entity.Id = _nextId++;
            entity.CreatedAt = DateTime.Now;

            _items.Add(entity);
            _indexById[entity.Id] = entity;
            return true;
        }

        // ── READ ──
        public T? GetById(int id)
        {
            if (_indexById.TryGetValue(id, out T? entity))
                return entity;
            return default;
        }

        public IEnumerable<T> GetAll()
        {
            foreach (T item in _items)
            {
                yield return item;      // Deferred execution!
            }
        }

        // ── UPDATE ──
        public bool Update(T entity)
        {
            if (entity == null || !_indexById.ContainsKey(entity.Id))
                return false;

            // Tìm vị trí trong list
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == entity.Id)
                {
                    _items[i] = entity;
                    _indexById[entity.Id] = entity;
                    return true;
                }
            }
            return false;
        }

        // ── DELETE ──
        public bool Delete(int id)
        {
            if (!_indexById.ContainsKey(id))
                return false;

            T entity = _indexById[id];
            _items.Remove(entity);
            _indexById.Remove(id);
            return true;
        }

        // ── QUERY ──
        public bool Exists(int id)
        {
            return _indexById.ContainsKey(id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            foreach (T item in _items)
            {
                if (predicate(item))
                    yield return item;    // Lazy filtering!
            }
        }
    }

    // ╔══════════════════════════════════════════════════════════╗
    // ║  TẦNG 3a: SERVICE — Business logic                     ║
    // ╚══════════════════════════════════════════════════════════╝

    class StudentService
    {
        private readonly IRepository<Student> _repository;

        // Constructor Injection — nhận repository qua constructor!
        public StudentService(IRepository<Student> repository)
        {
            _repository = repository;
        }

        // ── CRUD Operations ──

        public (bool Success, string Message) AddStudent(
            string name, int age, double score, string email)
        {
            try
            {
                Student student = new Student(0, name, age, score, email);
                bool result = _repository.Add(student);

                return result
                    ? (true, $"✅ Đã thêm: {student.ToShortString()}")
                    : (false, "❌ Không thể thêm sinh viên!");
            }
            catch (ArgumentException ex)
            {
                return (false, $"❌ Lỗi: {ex.Message}");
            }
        }

        public Student? GetStudent(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public (bool Success, string Message) UpdateStudent(
            int id, string name, int age, double score, string email)
        {
            Student? existing = _repository.GetById(id);
            if (existing == null)
                return (false, $"❌ Không tìm thấy sinh viên ID={id}!");

            try
            {
                existing.Name = name;
                existing.Age = age;
                existing.Score = score;
                existing.Email = email;

                bool result = _repository.Update(existing);
                return result
                    ? (true, $"✅ Đã cập nhật: {existing.ToShortString()}")
                    : (false, "❌ Không thể cập nhật!");
            }
            catch (ArgumentException ex)
            {
                return (false, $"❌ Lỗi: {ex.Message}");
            }
        }

        public (bool Success, string Message) DeleteStudent(int id)
        {
            Student? student = _repository.GetById(id);
            if (student == null)
                return (false, $"❌ Không tìm thấy sinh viên ID={id}!");

            string name = student.Name;
            bool result = _repository.Delete(id);
            return result
                ? (true, $"✅ Đã xóa sinh viên: {name}")
                : (false, "❌ Không thể xóa!");
        }

        // ── Query Operations ──

        public IEnumerable<Student> SearchByName(string keyword)
        {
            string lower = keyword.ToLower();
            return _repository.Find(s =>
                s.Name.ToLower().Contains(lower));
        }

        public IEnumerable<Student> GetByGrade(string grade)
        {
            return _repository.Find(s => s.Grade == grade);
        }

        public IEnumerable<Student> GetSorted()
        {
            List<Student> sorted = new List<Student>(_repository.GetAll());
            sorted.Sort();   // Dùng IComparable — giảm dần theo điểm
            return sorted;
        }

        public int TotalCount => _repository.Count;

        // ── Statistics ──

        public (double Avg, double Max, double Min, int PassCount, int FailCount)
            GetStatistics()
        {
            double sum = 0, max = double.MinValue, min = double.MaxValue;
            int count = 0, passCount = 0, failCount = 0;

            foreach (Student s in _repository.GetAll())
            {
                sum += s.Score;
                if (s.Score > max) max = s.Score;
                if (s.Score < min) min = s.Score;
                if (s.Score >= 5.0) passCount++;
                else failCount++;
                count++;
            }

            if (count == 0)
                return (0, 0, 0, 0, 0);

            return (sum / count, max, min, passCount, failCount);
        }
    }

    // ╔══════════════════════════════════════════════════════════╗
    // ║  TẦNG 3b: UI — Console Input/Output                    ║
    // ╚══════════════════════════════════════════════════════════╝

    class ConsoleUI
    {
        private readonly StudentService _service;

        public ConsoleUI(StudentService service)
        {
            _service = service;
        }

        // ══════════════════════════════
        // MAIN MENU LOOP
        // ══════════════════════════════
        public void Run()
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = ReadInput("Chọn chức năng");

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": ShowAllStudents(); break;
                    case "3": SearchStudent(); break;
                    case "4": UpdateStudent(); break;
                    case "5": DeleteStudent(); break;
                    case "6": ShowSorted(); break;
                    case "7": ShowStatistics(); break;
                    case "0":
                        running = false;
                        Console.WriteLine("\n  👋 Tạm biệt!\n");
                        break;
                    default:
                        PrintError("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        // ══════════════════════════════
        // MENU DISPLAY
        // ══════════════════════════════
        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   📚 QUẢN LÝ SINH VIÊN              ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. ➕ Thêm sinh viên                ║");
            Console.WriteLine("║  2. 📋 Xem tất cả                   ║");
            Console.WriteLine("║  3. 🔍 Tìm kiếm theo tên            ║");
            Console.WriteLine("║  4. ✏️  Cập nhật thông tin            ║");
            Console.WriteLine("║  5. 🗑️  Xóa sinh viên                ║");
            Console.WriteLine("║  6. 📊 Xem sắp xếp theo điểm        ║");
            Console.WriteLine("║  7. 📈 Thống kê                      ║");
            Console.WriteLine("║  0. 🚪 Thoát                         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"  Tổng: {_service.TotalCount} sinh viên");
        }

        // ══════════════════════════════
        // 1. ADD STUDENT
        // ══════════════════════════════
        private void AddStudent()
        {
            PrintHeader("THÊM SINH VIÊN MỚI");

            string name = ReadInput("Họ tên");
            int age = ReadInt("Tuổi", 16, 60);
            double score = ReadDouble("Điểm (0-10)", 0, 10);
            string email = ReadInput("Email (bỏ trống nếu không có)");

            var (success, message) = _service.AddStudent(name, age, score, email);
            if (success)
                PrintSuccess(message);
            else
                PrintError(message);
        }

        // ══════════════════════════════
        // 2. SHOW ALL STUDENTS
        // ══════════════════════════════
        private void ShowAllStudents()
        {
            PrintHeader("DANH SÁCH SINH VIÊN");

            if (_service.TotalCount == 0)
            {
                PrintWarning("Chưa có sinh viên nào!");
                return;
            }

            PrintTableHeader();
            foreach (Student s in _service.GetAllStudents())
            {
                Console.WriteLine(s);
            }
            PrintTableFooter(_service.TotalCount);
        }

        // ══════════════════════════════
        // 3. SEARCH STUDENT
        // ══════════════════════════════
        private void SearchStudent()
        {
            PrintHeader("TÌM KIẾM SINH VIÊN");

            string keyword = ReadInput("Nhập tên cần tìm");
            IEnumerable<Student> results = _service.SearchByName(keyword);

            int count = 0;
            PrintTableHeader();
            foreach (Student s in results)
            {
                Console.WriteLine(s);
                count++;
            }

            if (count == 0)
                PrintWarning($"Không tìm thấy sinh viên nào có tên chứa \"{keyword}\"");
            else
                PrintTableFooter(count);
        }

        // ══════════════════════════════
        // 4. UPDATE STUDENT
        // ══════════════════════════════
        private void UpdateStudent()
        {
            PrintHeader("CẬP NHẬT SINH VIÊN");

            int id = ReadInt("Nhập ID sinh viên", 1, int.MaxValue);
            Student? student = _service.GetStudent(id);

            if (student == null)
            {
                PrintError($"Không tìm thấy sinh viên ID={id}!");
                return;
            }

            Console.WriteLine($"\n  Thông tin hiện tại:");
            Console.WriteLine($"  {student.ToShortString()}");
            Console.WriteLine($"  Tuổi: {student.Age}, Email: {student.Email}");
            Console.WriteLine($"  (Nhấn Enter để giữ nguyên)\n");

            string nameInput = ReadInputOptional($"Họ tên [{student.Name}]");
            string name = string.IsNullOrEmpty(nameInput) ? student.Name : nameInput;

            string ageInput = ReadInputOptional($"Tuổi [{student.Age}]");
            int age = string.IsNullOrEmpty(ageInput) ? student.Age : int.Parse(ageInput);

            string scoreInput = ReadInputOptional($"Điểm [{student.Score:F1}]");
            double score = string.IsNullOrEmpty(scoreInput)
                ? student.Score : double.Parse(scoreInput);

            string emailInput = ReadInputOptional($"Email [{student.Email}]");
            string email = string.IsNullOrEmpty(emailInput) ? student.Email : emailInput;

            var (success, message) = _service.UpdateStudent(id, name, age, score, email);
            if (success)
                PrintSuccess(message);
            else
                PrintError(message);
        }

        // ══════════════════════════════
        // 5. DELETE STUDENT
        // ══════════════════════════════
        private void DeleteStudent()
        {
            PrintHeader("XÓA SINH VIÊN");

            int id = ReadInt("Nhập ID sinh viên cần xóa", 1, int.MaxValue);
            Student? student = _service.GetStudent(id);

            if (student == null)
            {
                PrintError($"Không tìm thấy sinh viên ID={id}!");
                return;
            }

            Console.WriteLine($"\n  ⚠️  Bạn sắp xóa: {student.ToShortString()}");
            string confirm = ReadInput("Xác nhận xóa? (y/n)");

            if (confirm.ToLower() == "y")
            {
                var (success, message) = _service.DeleteStudent(id);
                if (success)
                    PrintSuccess(message);
                else
                    PrintError(message);
            }
            else
            {
                Console.WriteLine("  ↩️  Đã hủy xóa.");
            }
        }

        // ══════════════════════════════
        // 6. SHOW SORTED
        // ══════════════════════════════
        private void ShowSorted()
        {
            PrintHeader("BẢNG XẾP HẠNG (Điểm giảm dần)");

            if (_service.TotalCount == 0)
            {
                PrintWarning("Chưa có sinh viên nào!");
                return;
            }

            PrintTableHeader();
            int rank = 1;
            foreach (Student s in _service.GetSorted())
            {
                Console.Write($"  #{rank,-3}");
                Console.WriteLine(s);
                rank++;
            }
            PrintTableFooter(_service.TotalCount);
        }

        // ══════════════════════════════
        // 7. SHOW STATISTICS
        // ══════════════════════════════
        private void ShowStatistics()
        {
            PrintHeader("THỐNG KÊ");

            if (_service.TotalCount == 0)
            {
                PrintWarning("Chưa có dữ liệu để thống kê!");
                return;
            }

            var (avg, max, min, pass, fail) = _service.GetStatistics();

            Console.WriteLine($"  📊 Tổng số sinh viên:    {_service.TotalCount}");
            Console.WriteLine($"  📊 Điểm trung bình:      {avg:F2}");
            Console.WriteLine($"  📊 Điểm cao nhất:        {max:F1}");
            Console.WriteLine($"  📊 Điểm thấp nhất:       {min:F1}");
            Console.WriteLine($"  ✅ Đạt (>= 5.0):         {pass} ({pass * 100.0 / _service.TotalCount:F1}%)");
            Console.WriteLine($"  ❌ Không đạt (< 5.0):    {fail} ({fail * 100.0 / _service.TotalCount:F1}%)");

            // Phân bố xếp loại
            Console.WriteLine("\n  ── PHÂN BỐ XẾP LOẠI ──");
            string[] grades = { "Xuất sắc", "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
            string[] emojis = { "🌟", "✅", "👍", "📝", "⚠️", "❌" };
            for (int i = 0; i < grades.Length; i++)
            {
                int count = 0;
                foreach (Student s in _service.GetByGrade(grades[i]))
                    count++;

                string bar = new string('█', count * 2);
                Console.WriteLine($"  {emojis[i]} {grades[i],-12}: {count,3} │ {bar}");
            }
        }

        // ══════════════════════════════
        // HELPER METHODS — Input
        // ══════════════════════════════
        private string ReadInput(string prompt)
        {
            Console.Write($"  {prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private string ReadInputOptional(string prompt)
        {
            Console.Write($"  {prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write($"  {prompt}: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;
                PrintError($"Vui lòng nhập số từ {min} đến {max}!");
            }
        }

        private double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write($"  {prompt}: ");
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double value) && value >= min && value <= max)
                    return value;
                PrintError($"Vui lòng nhập số từ {min} đến {max}!");
            }
        }

        // ══════════════════════════════
        // HELPER METHODS — Display
        // ══════════════════════════════
        private void PrintHeader(string title)
        {
            Console.WriteLine($"\n  ══ {title} ══");
        }

        private void PrintTableHeader()
        {
            Console.WriteLine("  ─────┬────────────────────────┬──────┬───────┬─────────────┬───────────────");
            Console.WriteLine("  ID   │ Họ tên                 │ Tuổi │ Điểm  │ Xếp loại    │ Email");
            Console.WriteLine("  ─────┼────────────────────────┼──────┼───────┼─────────────┼───────────────");
        }

        private void PrintTableFooter(int count)
        {
            Console.WriteLine("  ─────┴────────────────────────┴──────┴───────┴─────────────┴───────────────");
            Console.WriteLine($"  Tổng: {count} sinh viên");
        }

        private void PrintSuccess(string message)
        {
            Console.WriteLine($"\n  {message}");
        }

        private void PrintError(string message)
        {
            Console.WriteLine($"\n  {message}");
        }

        private void PrintWarning(string message)
        {
            Console.WriteLine($"\n  ⚠️  {message}");
        }
    }

    // ╔══════════════════════════════════════════════════════════╗
    // ║  PROGRAM.CS — Composition Root                         ║
    // ╚══════════════════════════════════════════════════════════╝

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ═══ Composition Root — Tạo và kết nối các tầng ═══

            // 1. Tạo Repository (tầng data)
            IRepository<Student> repository = new GenericRepository<Student>();

            // 2. Tạo Service (tầng business) — inject repository
            StudentService service = new StudentService(repository);

            // 3. Tạo UI (tầng presentation) — inject service
            ConsoleUI ui = new ConsoleUI(service);

            // ═══ Seed data — Dữ liệu mẫu ═══
            SeedData(service);

            // ═══ Chạy ứng dụng ═══
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   🎓 HỆ THỐNG QUẢN LÝ SINH VIÊN    ║");
            Console.WriteLine("║   Student Management System v1.0    ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            ui.Run();
        }

        // ═══ Dữ liệu mẫu để test ═══
        static void SeedData(StudentService service)
        {
            service.AddStudent("Nguyễn Văn An",     20, 8.5,  "an@email.com");
            service.AddStudent("Trần Thị Bình",     19, 9.2,  "binh@email.com");
            service.AddStudent("Lê Hoàng Cường",    21, 7.0,  "cuong@email.com");
            service.AddStudent("Phạm Minh Đức",     20, 9.5,  "duc@email.com");
            service.AddStudent("Hoàng Thị Em",      22, 6.0,  "em@email.com");
            service.AddStudent("Vũ Quang Phúc",     19, 8.8,  "phuc@email.com");
            service.AddStudent("Đặng Thùy Giang",   20, 4.5,  "giang@email.com");
            service.AddStudent("Mai Thanh Hải",      21, 9.0,  "hai@email.com");
            service.AddStudent("Bùi Minh Khang",    19, 3.0,  "khang@email.com");
            service.AddStudent("Ngô Thị Linh",      20, 7.5,  "linh@email.com");
        }
    }
}
```

---

## ▶️ Output mong đợi khi chạy

```
╔══════════════════════════════════════╗
║   🎓 HỆ THỐNG QUẢN LÝ SINH VIÊN    ║
║   Student Management System v1.0    ║
╚══════════════════════════════════════╝

╔══════════════════════════════════════╗
║   📚 QUẢN LÝ SINH VIÊN              ║
╠══════════════════════════════════════╣
║  1. ➕ Thêm sinh viên                ║
║  2. 📋 Xem tất cả                   ║
║  3. 🔍 Tìm kiếm theo tên            ║
║  4. ✏️  Cập nhật thông tin            ║
║  5. 🗑️  Xóa sinh viên                ║
║  6. 📊 Xem sắp xếp theo điểm        ║
║  7. 📈 Thống kê                      ║
║  0. 🚪 Thoát                         ║
╚══════════════════════════════════════╝
  Tổng: 10 sinh viên

  Chọn chức năng: 2

  ══ DANH SÁCH SINH VIÊN ══
  ─────┬────────────────────────┬──────┬───────┬─────────────┬───────────────
  ID   │ Họ tên                 │ Tuổi │ Điểm  │ Xếp loại    │ Email
  ─────┼────────────────────────┼──────┼───────┼─────────────┼───────────────
  1    │ Nguyễn Văn An          │   20 │   8.5 │ ✅ Giỏi      │ an@email.com
  2    │ Trần Thị Bình          │   19 │   9.2 │ 🌟 Xuất sắc  │ binh@email.com
  3    │ Lê Hoàng Cường         │   21 │   7.0 │ 👍 Khá       │ cuong@email.com
  4    │ Phạm Minh Đức          │   20 │   9.5 │ 🌟 Xuất sắc  │ duc@email.com
  ...
  ─────┴────────────────────────┴──────┴───────┴─────────────┴───────────────
  Tổng: 10 sinh viên

  Chọn chức năng: 7

  ══ THỐNG KÊ ══
  📊 Tổng số sinh viên:    10
  📊 Điểm trung bình:      7.30
  📊 Điểm cao nhất:        9.5
  📊 Điểm thấp nhất:       3.0
  ✅ Đạt (>= 5.0):         8 (80.0%)
  ❌ Không đạt (< 5.0):    2 (20.0%)

  ── PHÂN BỐ XẾP LOẠI ──
  🌟 Xuất sắc    :   2 │ ████
  ✅ Giỏi        :   2 │ ████
  👍 Khá         :   2 │ ████
  📝 Trung bình  :   2 │ ████
  ⚠️ Yếu         :   1 │ ██
  ❌ Kém         :   1 │ ██
```

---

## 📊 Phân tích kiến trúc

```
Kiến thức đã dùng trong project:

┌───────────────────────┬────────────────────────────────────────┐
│ Kiến thức             │ Áp dụng ở đâu                          │
├───────────────────────┼────────────────────────────────────────┤
│ Class, Constructor    │ Student, GenericRepository, Service    │
│ Properties + Validate │ Student.Name, .Age, .Score             │
│ Interface             │ IEntity, IRepository<T>                │
│ Generics              │ GenericRepository<T>, IRepository<T>   │
│ IComparable<T>        │ Student — sort theo điểm               │
│ IEnumerable<T>        │ GetAll(), Find() dùng yield return     │
│ List<T>               │ _items trong Repository                │
│ Dictionary<K,V>       │ _indexById cho fast lookup             │
│ Encapsulation         │ Private fields + public properties     │
│ Exception Handling    │ try-catch trong Service + validation   │
│ Tuple return          │ (bool Success, string Message)         │
│ Pattern matching      │ switch expression cho Grade            │
│ DI (cơ bản)          │ Service nhận Repository qua constructor │
└───────────────────────┴────────────────────────────────────────┘
```

---

> **💡 Mẹo:** Chạy thử chương trình và test từng chức năng. Thử nhập sai dữ liệu để xem error handling hoạt động! 🚀
