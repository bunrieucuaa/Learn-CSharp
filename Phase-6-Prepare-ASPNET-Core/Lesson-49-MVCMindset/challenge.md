# Thách thức Bài 49: Xây dựng Mock Controller xử lý Mượn Sách (Mock Library Controller)

## Ngữ cảnh
Để chuẩn bị cho việc viết code Web API thật bằng ASP.NET Core ở Phase sau, bạn được yêu cầu xây dựng một lớp **`BookController`** giả lập. Lớp này mô phỏng chính xác cách thức tiếp nhận tham số (Parameter Binding) và điều phối trả về mã trạng thái mạng (IActionResult) cho nghiệp vụ mượn sách.

## Cấu trúc DTO đầu vào
```csharp
public class BorrowBookDto
{
    public string MemberId { get; set; }
    public string Isbn { get; set; }
}
```

## Các interface dịch vụ hỗ trợ (Tầng BLL)
```csharp
using System;

public interface IBookService
{
    // Trả về true nếu mượn thành công. 
    // Ném InvalidOperationException nếu sách đã hết hoặc member bị khoá.
    // Ném ArgumentException nếu không tìm thấy MemberId hoặc Isbn.
    bool BorrowBook(string memberId, string isbn);
}
```

## Yêu cầu Thách thức
Hãy xây dựng class `BookController` chứa phương thức hành động (Action method) giả lập:
```csharp
public IActionResult BorrowBookAction(BorrowBookDto dto)
```

**Quy tắc xử lý và trả về mã mạng (IActionResult):**
1. **Kiểm tra null:** Nếu `dto` truyền vào bị null, trả về `400 Bad Request` kèm thông báo `"Dữ liệu yêu cầu trống"`.
2. **Gọi dịch vụ nghiệp vụ:** Bọc lời gọi `_bookService.BorrowBook(dto.MemberId, dto.Isbn)` trong khối `try-catch`.
3. **Ánh xạ Exception sang Status Code:**
   - Nếu bắt được `ArgumentException` (không tìm thấy thực thể): Trả về `404 Not Found` kèm thông báo lỗi của exception.
   - Nếu bắt được `InvalidOperationException` (vi phạm luật nghiệp vụ: hết sách, khóa thẻ): Trả về `400 Bad Request` kèm thông báo lỗi của exception.
   - Nếu xảy ra lỗi bất ngờ khác (ví dụ sập DB): Trả về `500 Internal Server Error`.
   - Nếu thành công (`true`): Trả về `200 OK` kèm thông báo `"Đã đăng ký mượn sách thành công!"`.

Hãy viết code của `BookController` hoàn chỉnh, tự viết một Mock class triển khai `IBookService` giả lập các tình huống ném lỗi nói trên, và viết kịch bản test thử nghiệm trong hàm `Main` in ra Console để chứng minh bộ điều phối Controller của bạn ánh xạ Exception sang mã trạng thái HTTP chính xác tuyệt đối.
