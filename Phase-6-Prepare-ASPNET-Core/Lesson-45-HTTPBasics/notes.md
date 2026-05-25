# Ghi nhớ Bài 45: HTTP Basics

## 1. Tóm tắt kiến thức cốt lõi
* **HTTP:** Giao thức mạng theo mô hình Client-Server. Trình duyệt/Client gửi Request, Server trả về Response.
* **Stateless:** Mỗi request là độc lập, Server không tự ghi nhớ lịch sử trạng thái của client.
* **HTTP Methods:** GET (đọc), POST (tạo mới), PUT (thay thế), PATCH (sửa một phần), DELETE (xóa).
* **HTTP Status Codes:** 
  * 2xx: Thành công (`200 OK`, `201 Created`).
  * 4xx: Lỗi do Client (`400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`).
  * 5xx: Lỗi do Server sập (`500 Internal Server Error`).
* **URL:** Địa chỉ định vị tài nguyên trên mạng.

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Lầm tưởng GET an toàn tuyệt đối:** GET gửi dữ liệu thông qua URL (Query String), do đó dữ liệu hiển thị trực tiếp trên thanh địa chỉ trình duyệt và bị lưu lại trong lịch sử duyệt web. Tuyệt đối không dùng phương thức GET để gửi các thông tin nhạy cảm như Mật khẩu hay Mã pin thẻ ngân hàng. Sử dụng **POST** để đóng gói thông tin nhạy cảm ẩn bên trong Request Body.
> * **Nhầm lẫn giữa 401 và 403:** 
>   - `401 Unauthorized`: Hệ thống chưa biết bạn là ai (chưa đăng nhập).
>   - `403 Forbidden`: Hệ thống biết bạn là ai (đã đăng nhập), nhưng bạn không đủ đặc quyền để thực hiện hành động đó.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao HTTP lại là giao thức Stateless chưa?
- [ ] Bạn có chọn đúng mã Status Code tương ứng khi Server gặp lỗi Exception chưa?
- [ ] Bạn đã nắm được cách bóc tách một URL thành các thành phần nhỏ chưa?
- [ ] Bạn có biết cách viết C# sử dụng `HttpClient` gửi request lấy dữ liệu web chưa?
