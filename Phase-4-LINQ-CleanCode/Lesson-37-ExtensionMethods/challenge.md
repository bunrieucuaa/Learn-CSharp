# Thách thức Bài 37: Xây dựng Thư viện Fluent Validation Nhỏ gọn

## Ngữ cảnh
Trong các dự án thực tế, kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation) là nhiệm vụ vô cùng nhàm chán. Viết hàng chục câu lệnh `if-else` lồng nhau để kiểm tra xem Tên có trống không, Tuổi có từ 18-60 không, Email có đúng định dạng không... làm code của chúng ta bị phình to và rất xấu.

Hôm nay, bạn sẽ ứng dụng **Extension Methods** kết hợp mô hình thiết kế **Fluent Interface (Method Chaining)** để xây dựng một bộ thư viện Validation mini, giúp việc viết code validation trở nên "mượt mà" như đọc văn bản tiếng Anh.

## Yêu cầu Thách thức
Thiết kế một bộ Extension Methods cho class `Student` sao cho chúng ta có thể gọi chuỗi xác thực như sau:
```csharp
Student student = new Student { Name = "Vy", Age = 17, Email = "vy_invalid" };

// Viết chuỗi xác thực (Fluent Interface)
bool isValid = student
               .ValidateNameNotEmpty()
               .ValidateAgeBetween(18, 60)
               .ValidateEmailFormat()
               .HasNoErrors();
```

## Các class nền tảng cần định nghĩa
Để làm được việc này, hãy định nghĩa một class chứa kết quả Validation:
```csharp
using System.Collections.Generic;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public class ValidationResult
{
    public Student Target { get; set; }
    public List<string> Errors { get; } = new List<string>();

    public ValidationResult(Student target)
    {
        Target = target;
    }
}
```

## Gợi ý các bước thiết kế Extension Methods
Bạn cần viết các Extension Methods mở rộng cho kiểu `ValidationResult` (và một hàm khởi tạo mở rộng cho kiểu `Student` để bắt đầu chuỗi validate):

1. **Hàm khởi động:** Viết Extension Method `.StartValidation(this Student student)` trả về đối tượng `ValidationResult`.
2. **Hàm validate Tên:** Viết Extension Method `.ValidateNameNotEmpty(this ValidationResult result)`: kiểm tra nếu `result.Target.Name` bị trống thì thêm lỗi `"Tên không được để trống"` vào `result.Errors`. Trả về chính `result` để tiếp tục chuỗi.
3. **Hàm validate Tuổi:** Viết Extension Method `.ValidateAgeBetween(this ValidationResult result, int min, int max)`: kiểm tra nếu `result.Target.Age` nằm ngoài khoảng thì thêm lỗi `"Tuổi phải từ min đến max"`. Trả về chính `result`.
4. **Hàm validate Email:** Viết Extension Method `.ValidateEmailFormat(this ValidationResult result)`: kiểm tra định dạng email của `result.Target.Email`. Trả về chính `result`.
5. **Hàm kiểm tra kết quả cuối:** Viết Extension Method `.HasNoErrors(this ValidationResult result)` trả về `true` nếu danh sách `Errors` trống, ngược lại in ra toàn bộ lỗi và trả về `false`.

Hãy hoàn thành bộ thư viện này và chạy thử nghiệm chương trình. Đoạn code validation của bạn trông sẽ cực kỳ "sạch" và chuyên nghiệp!
