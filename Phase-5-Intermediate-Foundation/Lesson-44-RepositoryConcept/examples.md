# Ví dụ Thực hành Bài 44: Repository Pattern

Dưới đây là ví dụ hoàn chỉnh về thiết kế Repository: Khai báo interface `IStudentRepository` và triển khai 2 phiên bản lưu trữ khác nhau (trên RAM và trên File JSON) để chứng minh sức mạnh của ghép lỏng thông qua DI Container.

---

## Ví dụ 1: Khai báo Model và Interface Repository

File: `Models/Student.cs` & `Repositories/IStudentRepository.cs`

```csharp
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Gpa { get; set; }
    }
}

namespace SchoolApp.Repositories
{
    using SchoolApp.Models;

    // Định nghĩa Interface (hợp đồng truy cập dữ liệu)
    public interface IStudentRepository
    {
        Student GetById(int id);
        List<Student> GetAll();
        void Add(Student student);
        void Delete(int id);
    }
}
```

---

## Ví dụ 2: Viết hai phiên bản Repository triển khai Interface

### Phiên bản A: Lưu trữ trên RAM (InMemory Repository - Phục vụ Test nhanh)
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolApp.Models;
using SchoolApp.Repositories;

namespace SchoolApp.Repositories.Impl
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new List<Student>();

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> GetAll() => _students;

        public void Add(Student student)
        {
            student.Id = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
            _students.Add(student);
            Console.WriteLine($"[RAM DB] Đã thêm sinh viên: {student.Name}");
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _students.Remove(student);
                Console.WriteLine($"[RAM DB] Đã xóa sinh viên ID {id}");
            }
        }
    }
}
```

### Phiên bản B: Lưu trữ trên tệp JSON (File-based Repository - Phục vụ lưu trữ bền vững)
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SchoolApp.Models;
using SchoolApp.Repositories;

namespace SchoolApp.Repositories.Impl
{
    public class FileStudentRepository : IStudentRepository
    {
        private const string DbPath = "students_database.json";

        private List<Student> LoadFromFile()
        {
            if (!File.Exists(DbPath)) return new List<Student>();
            string json = File.ReadAllText(DbPath);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }

        private void SaveToFile(List<Student> list)
        {
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DbPath, json);
        }

        public Student GetById(int id)
        {
            return LoadFromFile().FirstOrDefault(s => s.Id == id);
        }

        public List<Student> GetAll() => LoadFromFile();

        public void Add(Student student)
        {
            var list = LoadFromFile();
            student.Id = list.Count > 0 ? list.Max(s => s.Id) + 1 : 1;
            list.Add(student);
            SaveToFile(list);
            Console.WriteLine($"[JSON FILE DB] Đã ghi nhận sinh viên: {student.Name}");
        }

        public void Delete(int id)
        {
            var list = LoadFromFile();
            var target = list.FirstOrDefault(s => s.Id == id);
            if (target != null)
            {
                list.Remove(target);
                SaveToFile(list);
                Console.WriteLine($"[JSON FILE DB] Đã xóa sinh viên ID {id}");
            }
        }
    }
}
```

---

## Ví dụ 3: Lớp Service nhận Repository qua Interface (Loose Coupling)

File: `Services/StudentService.cs`

```csharp
using System;
using System.Collections.Generic;
using SchoolApp.Models;
using SchoolApp.Repositories;

namespace SchoolApp.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        // Bơm IStudentRepository (Interface) vào qua Constructor
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public void EnrollStudent(string name, double gpa)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên sinh viên không hợp lệ!");

            var student = new Student { Name = name, Gpa = gpa };
            _repository.Add(student);
        }

        public List<Student> GetHonorRoll()
        {
            // Nghiệp vụ: Lọc học sinh giỏi (GPA >= 8.0)
            return _repository.GetAll().FindAll(s => s.Gpa >= 8.0);
        }
    }
}
```

---

## Ví dụ 4: Ráp nối và hoán đổi Repository bằng DI Container

File: `Program.cs`

```csharp
using System;
using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Repositories;
using SchoolApp.Repositories.Impl;
using SchoolApp.Services;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();

        // =====================================================================
        // ĐĂNG KÝ CẤU HÌNH DỊCH VỤ
        // =====================================================================
        
        // Cách 1: Sử dụng lưu trên RAM (InMemory)
        services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();

        // Cách 2: Chỉ cần đổi dòng code dưới đây để chuyển sang lưu tệp tin JSON!
        // services.AddSingleton<IStudentRepository, FileStudentRepository>();

        services.AddTransient<StudentService>();

        var provider = services.BuildServiceProvider();

        // =====================================================================
        // CHẠY THỬ NGHIỆM HỆ THỐNG
        // =====================================================================
        var studentService = provider.GetRequiredService<StudentService>();

        Console.WriteLine("Đang đăng ký sinh viên mới...");
        studentService.EnrollStudent("Nguyễn Văn An", 8.5);
        studentService.EnrollStudent("Trần Thị Bình", 7.2);
        studentService.EnrollStudent("Phạm Hồng Hải", 9.0);

        var honors = studentService.GetHonorRoll();
        Console.WriteLine("\nDanh sách sinh viên xuất sắc (GPA >= 8.0):");
        foreach (var s in honors)
        {
            Console.WriteLine($"- ID {s.Id}: {s.Name} (GPA: {s.Gpa})");
        }
    }
}
```
*(Hãy chạy thử chương trình, sau đó tắt comment Cách 1 và bật comment Cách 2 ở Program.cs để thấy sự chuyển đổi công nghệ database vi diệu chỉ với đúng 1 dòng code thay đổi ở Composition Root).*
