# Bài tập Thực hành Bài 45: HTTP Basics

Hãy suy nghĩ và hoàn thành các câu hỏi trắc nghiệm tư duy và bài tập thực hành dưới đây.

---

## Bài tập 1: Lựa chọn HTTP Method phù hợp
**Đề bài:**
Hãy lựa chọn phương thức HTTP (GET, POST, PUT, PATCH, DELETE) phù hợp nhất cho các hành động sau từ Client gửi lên Web API:
1. Người dùng nhấn nút "Xem thông tin chi tiết phòng khách sạn".
2. Người dùng nhấn nút "Hủy phòng đã đặt" (Xóa lịch sử).
3. Người dùng cập nhật riêng số điện thoại của mình trong hồ sơ cá nhân.
4. Người dùng gửi bình luận mới dưới bài viết.
5. Người dùng thay thế toàn bộ địa chỉ giao hàng bằng địa chỉ mới.

---

## Bài tập 2: Phân tích mã trạng thái phản hồi (Status Code)
**Đề bài:**
Hãy xác định mã trạng thái HTTP Status Code (ví dụ: 200, 201, 400, 401, 403, 404, 500) phù hợp nhất cho các trường hợp xử lý sau của Server:
1. Server xử lý thành công yêu cầu GET lấy thông tin sản phẩm.
2. Server tạo mới thành công tài khoản người dùng và lưu vào DB.
3. Người dùng cố gắng đăng nhập nhưng nhập sai mật khẩu.
4. Người dùng là khách vãng lai cố tình gọi API xóa tài khoản của Admin.
5. Server đang kết nối với Database thì Database bị sập điện đột ngột.
6. Người dùng gõ sai đường dẫn API `/api/profile-details-wrong`.

---

## Bài tập 3: Cấu trúc của một URL
**Đề bài:**
Cho URL sau: `https://api.shop.com:8080/api/v1/products?category=shoes&brand=nike#rating`
Hãy bóc tách và liệt kê tên tương ứng của các thành phần trong URL trên:
- Protocol (Giao thức): ?
- Host (Tên miền): ?
- Port (Cổng): ?
- Path (Đường dẫn): ?
- Query String (Chuỗi truy vấn lọc): ?
- Anchor (Mỏ neo): ?

---

## Bài tập 4: Gửi HTTP GET lấy danh sách bài viết
**Đề bài:**
Hãy tự viết một chương trình Console C# sử dụng `HttpClient` để gọi một GET Request tới URL:
`https://jsonplaceholder.typicode.com/posts`
1. Đọc và in ra Status Code của phản hồi.
2. Nếu thành công, hãy đọc nội dung Body, chuyển đổi (Deserialize) từ chuỗi JSON thô thành mảng các Object C# và in ra màn hình tiêu đề (`title`) của 3 bài viết đầu tiên.

---

## Bài tập 5: Viết code kiểm tra mã lỗi Client Error
**Đề bài:**
Viết một phương thức kiểm tra an toàn trong C# nhận vào mã trạng thái nguyên `int statusCode`:
`public static bool IsClientError(int statusCode)`
Trả về `true` nếu mã trạng thái đó nằm trong nhóm lỗi của Client (Client Error), ngược lại trả về `false`.

* **Expected Output:**
  - `IsClientError(404)` -> `true`
  - `IsClientError(200)` -> `false`
  - `IsClientError(500)` -> `false`
