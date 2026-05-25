# Thách thức Bài 47: Bộ phân tích và Xác thực HTTP Request thô (HTTP Request Validator)

## Ngữ cảnh
Để bảo mật hệ thống API, mọi request gửi lên Server trước khi được chuyển đến tầng Service xử lý bắt buộc phải đi qua một bộ lọc bảo mật để kiểm tra tính hợp lệ (xác thực token và định dạng dữ liệu).

Hôm nay, bạn sẽ đóng vai trò lập trình viên hệ thống mạng viết một class **`RequestValidator`** để phân tích một chuỗi gói tin HTTP Request thô gửi lên và đưa ra quyết định: Cho phép đi tiếp (200 OK) hay từ chối (400 Bad Request / 401 Unauthorized).

## Cấu trúc dữ liệu phản hồi
```csharp
public class ValidationResult
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ExtractedToken { get; set; }
    public string ExtractedBody { get; set; }
}
```

## Yêu cầu Thách thức
Hãy xây dựng phương thức xác thực:
```csharp
public static ValidationResult ValidateRequest(string rawRequest)
```

**Các luật xác thực cần áp dụng:**
1. **Kiểm tra Header Content-Type:** Request gửi lên phải có header `Content-Type` và giá trị của nó bắt buộc phải là `application/json`. (Nếu không khớp hoặc thiếu, trả về code `400 Bad Request` kèm thông báo `"Chỉ chấp nhận dữ liệu JSON"`).
2. **Kiểm tra Header Authorization (Xác thực):** Request bắt buộc phải chứa header `Authorization` với định dạng `Bearer [token]`.
   - Nếu thiếu header này: Trả về code `401 Unauthorized` kèm thông báo `"Yêu cầu đăng nhập"`.
   - Nếu có header nhưng giá trị token bị rỗng hoặc không bắt đầu bằng `"Bearer "`: Trả về code `401 Unauthorized` kèm thông báo `"Token không hợp lệ"`.
3. **Kiểm tra Body:** Nếu phương thức là `POST` hoặc `PUT`, gói tin bắt buộc phải có phần Request Body. (Nếu rỗng, trả về code `400 Bad Request` kèm thông báo `"Thiếu body dữ liệu"`).
4. **Hợp lệ:** Nếu vượt qua toàn bộ các kiểm tra trên, trả về code `200 OK`, điền thông tin token đã bóc tách được và chuỗi body đã trích xuất vào đối tượng `ValidationResult`.

## Kịch bản kiểm thử (Test Cases)
Hãy viết hàm trên và chạy thử với 3 chuỗi request thô mô phỏng các tình huống:
- **Request A (Lỗi thiếu token):**
  ```text
  POST /api/users HTTP/1.1
  Content-Type: application/json

  {"Name":"Vy"}
  ```
- **Request B (Lỗi sai Content-Type):**
  ```text
  POST /api/users HTTP/1.1
  Content-Type: text/plain
  Authorization: Bearer secret_123

  {"Name":"Vy"}
  ```
- **Request C (Hợp lệ thành công):**
  ```text
  POST /api/users HTTP/1.1
  Content-Type: application/json
  Authorization: Bearer token_super_secure

  {"Name":"Vy"}
  ```
In kết quả chi tiết của từng lượt validation ra màn hình Console để tự chứng minh bộ lọc của bạn hoạt động chính xác tuyệt đối.
