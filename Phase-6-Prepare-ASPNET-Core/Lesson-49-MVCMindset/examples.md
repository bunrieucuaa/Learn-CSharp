# Ví dụ Thực hành Bài 49: MVC & Parameter Binding

Dưới đây là các ví dụ C# chạy được bằng .NET 9 giả lập cấu trúc của các lớp Controller trong ASP.NET Core và cơ chế Parameter Binding.

---

## Ví dụ 1: Mô phỏng Parameter Binding trong Web API Controller

Ví dụ này định nghĩa các DTO và giả lập cách ASP.NET Core tự động trích xuất dữ liệu từ các vùng khác nhau của gói tin HTTP Request để gán vào tham số.

```csharp
using System;

// 1. Đối tượng DTO (Data Transfer Object) đại diện cho dữ liệu gửi lên trong Body
public class StudentCreateDto
{
    public string Name { get; set; }
    public double Gpa { get; set; }
}

// Giả lập interface IActionResult của ASP.NET Core
public interface IActionResult
{
    int StatusCode { get; }
    object Value { get; }
}

// Khai báo các Response class cụ thể
public class OkResult : IActionResult { public int StatusCode => 200; public object Value { get; } public OkResult(object val) => Value = val; }
public class CreatedResult : IActionResult { public int StatusCode => 201; public object Value { get; } public CreatedResult(object val) => Value = val; }
public class BadRequestResult : IActionResult { public int StatusCode => 400; public object Value { get; } public BadRequestResult(string err) => Value = err; }
public class NotFoundResult : IActionResult { public int StatusCode => 404; public object Value => "Not Found"; }

// 2. Class Controller mô phỏng
public class StudentsController
{
    // Giả lập GET /api/students/{id} (Lấy ID từ Route)
    public IActionResult GetById(int idFromRoute)
    {
        Console.WriteLine($"-> [Controller] Nhận được ID từ Route: {idFromRoute}");
        if (idFromRoute <= 0) return new NotFoundResult();

        var mockStudent = new { Id = idFromRoute, Name = "Văn Lâm", Gpa = 8.5 };
        return new OkResult(mockStudent);
    }

    // Giả lập GET /api/students?page=2&size=10 (Lấy page, size từ Query)
    public IActionResult GetPagedList(int pageFromQuery, int sizeFromQuery)
    {
        Console.WriteLine($"-> [Controller] Lọc danh sách: Trang = {pageFromQuery}, Kích thước = {sizeFromQuery}");
        var mockList = new[] { "Student A", "Student B" };
        return new OkResult(mockList);
    }

    // Giả lập POST /api/students (Lấy object từ Body)
    public IActionResult Create(StudentCreateDto dtoFromBody)
    {
        Console.WriteLine($"-> [Controller] Đang tạo học sinh từ Body JSON: {dtoFromBody.Name} (GPA: {dtoFromBody.Gpa})");
        
        if (string.IsNullOrEmpty(dtoFromBody.Name))
            return new BadRequestResult("Tên học sinh không được rỗng!");

        var createdStudent = new { Id = 999, Name = dtoFromBody.Name, Gpa = dtoFromBody.Gpa };
        return new CreatedResult(createdStudent);
    }
}
```

---

## Ví dụ 2: Lắp ráp chạy thử nghiệm điều phối Parameter Binding

```csharp
using System;

class Program
{
    static void Main()
    {
        StudentsController controller = new StudentsController();

        Console.WriteLine("--- MÔ PHỎNG LUỒNG ĐIỀU PHỐI CONTROLLER ---");

        // 1. Tình huống 1: Lấy chi tiết học sinh ID = 5
        Console.WriteLine("\nTình huống 1: Gọi hàm GetById với Route param = 5");
        IActionResult result1 = controller.GetById(idFromRoute: 5);
        PrintResult(result1);

        // 2. Tình huống 2: Phân trang danh sách (Trang 3, size 20)
        Console.WriteLine("\nTình huống 2: Gọi hàm GetPagedList với Query params: page=3, size=20");
        IActionResult result2 = controller.GetPagedList(pageFromQuery: 3, sizeFromQuery: 20);
        PrintResult(result2);

        // 3. Tình huống 3: Tạo mới học sinh hợp lệ
        Console.WriteLine("\nTình huống 3: Gọi hàm Create với Body JSON hợp lệ");
        var validDto = new StudentCreateDto { Name = "Khánh Vy", Gpa = 9.2 };
        IActionResult result3 = controller.Create(dtoFromBody: validDto);
        PrintResult(result3);

        // 4. Tình huống 4: Tạo mới học sinh bị lỗi validation tên trống
        Console.WriteLine("\nTình huống 4: Gọi hàm Create với Body bị thiếu tên");
        var invalidDto = new StudentCreateDto { Name = "", Gpa = 0 };
        IActionResult result4 = controller.Create(dtoFromBody: invalidDto);
        PrintResult(result4);
    }

    static void PrintResult(IActionResult result)
    {
        Console.WriteLine($"[HTTP RESPONSE] Status Code: {result.StatusCode}");
        if (result.Value != null)
        {
            // Nếu là đối tượng ẩn danh, in thông tin
            Console.WriteLine($"                Content:     {result.Value}");
        }
    }
}
```
