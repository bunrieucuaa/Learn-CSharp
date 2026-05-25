# Bài 45: HTTP Basics (Cơ bản về Giao thức HTTP)

Chào mừng bạn đến với **Phase 6: Prepare for ASP.NET Core**. Đây là chặng đường cuối cùng của chúng ta, nơi chúng ta sẽ chuẩn bị hành trang tư duy mạng internet để chuyển dịch từ các ứng dụng Console chạy offline sang lập trình Web Backend trực tuyến.

Kiến thức đầu tiên và quan trọng nhất bạn cần nắm vững là **HTTP** — giao thức truyền dữ liệu nền tảng làm nên mạng lưới World Wide Web (WWW).

---

## 1. Giao thức HTTP là gì?

### Khái niệm
**HTTP (Hypertext Transfer Protocol - Giao thức truyền siêu văn bản)** là một giao thức mạng chuẩn cho phép truyền tải các tài nguyên như file HTML, ảnh, video, dữ liệu JSON qua internet. 

HTTP hoạt động theo mô hình **Client-Server (Khách - Chủ)**:
* **Client (Trình duyệt, Angular App, Mobile App):** Gửi yêu cầu (**Request**) lên Server.
* **Server (ASP.NET Core Web API, Node.js Server):** Tiếp nhận yêu cầu, xử lý và trả về kết quả (**Response**).

```text
+--------+      HTTP Request (GET /api/books)      +--------+
| Client | --------------------------------------> | Server |
| (Web)  | <-------------------------------------- | (Web)  |
+--------+      HTTP Response (JSON data)          +--------+
```

### Tính chất Stateless (Không trạng thái)
> [!IMPORTANT]
> HTTP là giao thức **Stateless**. Nghĩa là mỗi cặp Request-Response hoàn toàn độc lập và không liên quan gì đến nhau. Server mặc định không nhớ Client vừa làm gì ở request trước đó.
> *Để hệ thống nhớ được người dùng đã đăng nhập hay chưa, chúng ta phải sử dụng thêm các kỹ thuật như **Cookies, Sessions, hoặc Token (JWT)**.*

---

## 2. HTTP Methods (Các phương thức HTTP)

Khi gửi Request, Client phải chỉ ra **Hành động** mong muốn thực hiện thông qua các HTTP Methods. 5 phương thức kinh điển của kiến trúc RESTful gồm:

| Method | Ý nghĩa nghiệp vụ | Ví dụ thực tế |
|---|---|---|
| **GET** | Truy vấn / Đọc dữ liệu (Không làm thay đổi dữ liệu trên Server) | Lấy danh sách sản phẩm, đọc bài viết. |
| **POST** | Tạo mới dữ liệu | Đăng ký tài khoản, đặt mua hàng, tạo bài viết mới. |
| **PUT** | Cập nhật toàn bộ dữ liệu (Thay thế bản ghi cũ) | Cập nhật lại toàn bộ thông tin cá nhân của User. |
| **PATCH** | Cập nhật một phần dữ liệu (Chỉ sửa một vài trường) | Đổi mật khẩu, cập nhật riêng số lượng hàng trong kho. |
| **DELETE** | Xóa dữ liệu | Xóa một bài viết, hủy đơn hàng. |

---

## 3. HTTP Status Codes (Mã trạng thái phản hồi)

Mỗi Response trả về từ Server luôn đi kèm một con số 3 chữ số đại diện cho kết quả xử lý. Chúng được chia làm 5 nhóm chính:

* **1xx (Informational):** Đang xử lý yêu cầu.
* **2xx (Success - Thành công):** Yêu cầu được xử lý ngon lành.
  * `200 OK`: Thành công (thường dùng cho GET, PUT, PATCH).
  * `201 Created`: Tạo mới thành công (thường dùng cho POST).
* **3xx (Redirection - Chuyển hướng):** Client cần chuyển hướng sang URL khác để lấy dữ liệu.
* **4xx (Client Error - Lỗi từ phía Client):** Client gửi yêu cầu sai điều kiện hoặc sai cú pháp.
  * `400 Bad Request`: Yêu cầu lỗi (ví dụ: gửi thiếu thông tin bắt buộc, sai định dạng JSON).
  * `401 Unauthorized`: Chưa đăng nhập / Chưa xác thực danh tính.
  * `403 Forbidden`: Đã đăng nhập nhưng không có quyền truy cập (ví dụ: Member cố vào trang Admin).
  * `404 Not Found`: Không tìm thấy tài nguyên (đường dẫn sai hoặc ID sản phẩm không tồn tại).
* **5xx (Server Error - Lỗi từ phía Server):** Code của Server bị crash hoặc cơ sở dữ liệu bị hỏng.
  * `500 Internal Server Error`: Lỗi hệ thống ngầm của Server (lập trình viên viết code bị lỗi Exception mà không bắt lỗi).

---

## 4. URL và URI là gì?

* **URI (Uniform Resource Identifier):** Là một chuỗi ký tự dùng để định danh duy nhất một tài nguyên.
* **URL (Uniform Resource Locator):** Là một dạng cụ thể của URI, không chỉ định danh mà còn chỉ ra cách thức định vị (đường dẫn) để tìm ra tài nguyên đó trên mạng.

### Cấu trúc của một URL:
```text
https://www.site.com:443/api/v1/products?category=laptop#spec
\___/   \__________/ \_/ \_____________/ \_____________/ \__/
  |          |        |         |               |          |
Protocol   Host     Port      Path         Query String  Anchor
```

---

## 5. So sánh với JavaScript (Client-side)

Vì bạn có nền tảng JavaScript, bạn chắc chắn đã từng viết code gửi HTTP Request bằng hàm `fetch()` của trình duyệt hoặc thư viện `axios`:
```javascript
// JS gửi HTTP GET
fetch('https://api.site.com/products')
  .then(res => res.json())
  .then(data => console.log(data));
```
Trong C# Web API, Server của bạn sẽ là nơi hứng (tiếp nhận) các request này. Tuy nhiên, đôi khi Server C# của bạn cũng đóng vai trò là một "Client" đi gọi API của một Server bên thứ ba (ví dụ: gọi API ngân hàng để check thanh toán). C# cung cấp class **`HttpClient`** để thực hiện việc này một cách bất đồng bộ.

---

## 6. Checklist đánh giá hiểu bài

1. Tính chất Stateless của giao thức HTTP nghĩa là gì? 
2. Nêu sự khác biệt về mục đích sử dụng giữa 4 phương thức: GET, POST, PUT, và DELETE.
3. Khi người dùng nhập sai mật khẩu đăng nhập, Server nên trả về mã Status Code nào trong các mã: 400, 401, 403, hay 404? Giải thích tại sao.
4. Mã trạng thái `500 Internal Server Error` thường xuất hiện khi nào? Làm thế nào để hạn chế nó?
