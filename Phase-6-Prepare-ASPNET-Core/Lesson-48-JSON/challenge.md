# Thách thức Bài 48: Bộ chuẩn hóa và Đổi dạng Dữ liệu JSON (JSON Normalizer Engine)

## Ngữ cảnh
Công ty bạn đang tiến hành hợp nhất dữ liệu từ nhiều nguồn API khác nhau của các đối tác cũ. Mỗi đối tác gửi dữ liệu khách hàng về hệ thống dưới các định dạng đặt tên rất "kỳ quái" (lộn xộn viết hoa thường, snake_case, viết tắt).

Trong khi đó, ứng dụng Frontend Angular của công ty bạn đòi hỏi dữ liệu JSON trả về phải tuân thủ nghiêm ngặt chuẩn **camelCase** sạch sẽ, đồng thời không chứa các trường dữ liệu `null` để tối ưu dung lượng tải mạng.

Nhiệm vụ của bạn là viết một module **JSON Normalizer** bằng C# để tiếp nhận dữ liệu JSON thô của đối tác, chuẩn hóa nó sang Object C# an toàn, và xuất bản ra chuỗi JSON sạch đạt chuẩn của công ty.

## Cấu trúc dữ liệu chuẩn của Công ty (C# Models)
```csharp
public class CompanyUser
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; } // Có thể bị null
}
```

## Dữ liệu JSON thô từ 2 đối tác khác nhau gửi sang

- **JSON từ Đối tác A (Dùng snake_case):**
  ```json
  {
    "user_id": 1001,
    "full_name": "Nguyễn Văn Lâm",
    "email_address": "lam@partnera.com",
    "phone_number": null
  }
  ```

- **JSON từ Đối tác B (Đặt tên viết tắt, lộn xộn hoa thường):**
  ```json
  {
    "uId": 2002,
    "NAME": "Trần Thị Vy",
    "EMAIL": "vy@partnerb.com",
    "phone_number": "0987654321"
  }
  ```

## Yêu cầu Thách thức
Hãy xây dựng chương trình C# thực hiện:
1. **Thiết kế ánh xạ cho Đối tác A:** Sử dụng `[JsonPropertyName]` hoặc cấu hình thích hợp để parse JSON của Đối tác A thành đối tượng `CompanyUser` C#.
2. **Thiết kế ánh xạ cho Đối tác B:** Viết một class DTO riêng biệt hoặc dùng cấu hình thông minh để parse JSON của Đối tác B thành đối tượng `CompanyUser` C#.
3. **Chuẩn hóa đầu ra (Normalizer):** Viết một hàm `static string NormalizeToCompanyJson(CompanyUser user)`:
   - Xuất ra chuỗi JSON định dạng chuẩn **camelCase** cho Angular (ví dụ: `userId`, `fullName`).
   - Ẩn hoàn toàn các thuộc tính có giá trị `null` (như `phoneNumber` của Đối tác A).
   - In thụt lề đẹp mắt (WriteIndented = true).

## Expected Output JSON của Đối tác A sau khi chuẩn hóa:
```json
{
  "userId": 1001,
  "fullName": "Nguyễn Văn Lâm",
  "emailAddress": "lam@partnera.com"
}
```
*(Hãy tự chạy chương trình và in kết quả chuẩn hóa của cả 2 đối tác ra Console để kiểm chứng).*
