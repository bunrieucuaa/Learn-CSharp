# Ghi nhớ Bài 47: HTTP Request & Response

## 1. Tóm tắt kiến thức cốt lõi
* **HTTP Request Structure:** 
  1. Request Line: Phương thức, đường dẫn và phiên bản giao thức.
  2. Headers: Siêu dữ liệu mô tả (như `Content-Type`, `Authorization`).
  3. Body: Nội dung payload dữ liệu thực tế (thường là JSON).
* **HTTP Response Structure:** 
  1. Status Line: Phiên bản giao thức và mã Status Code (ví dụ: `200 OK`).
  2. Headers: Metadata của phản hồi.
  3. Body: Dữ liệu Server trả về cho Client.
* **3 cách truyền dữ liệu chính:**
  * **Route Parameter:** Xác định tài nguyên cụ thể thông qua cấu trúc đường dẫn `/api/users/{id}`.
  * **Query Parameter:** Lọc, sắp xếp, phân trang danh sách tài nguyên `/api/users?page=2`.
  * **Request Body:** Truyền tải đối tượng dữ liệu lớn, phức tạp hoặc nhạy cảm.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi định dạng Content-Type:** Khi gửi dữ liệu JSON bằng HttpClient hoặc Postman, nếu bạn quên thiết lập Header `Content-Type: application/json`, hầu hết các Web API (bao gồm cả ASP.NET Core) sẽ từ chối nhận dữ liệu và ném ra lỗi `415 Unsupported Media Type` hoặc bỏ qua dữ liệu khiến các tham số nhận được ở Controller bị `null`.
> * **Giới hạn độ dài URL:** Route và Query parameters được truyền trực tiếp trên URL. URL có giới hạn độ dài vật lý tùy theo trình duyệt và web server (thường khoảng 2000 ký tự). Do đó, tuyệt đối không dùng URL để truyền tải dữ liệu có dung lượng lớn (như file, ảnh) hoặc thông tin nhạy cảm. Hãy chuyển sang dùng **Request Body**.

## 3. Checklist tự đánh giá
- [ ] Bạn đã phân biệt được cấu trúc vật lý của Request và Response chưa?
- [ ] Bạn có chỉ ra được đâu là Route Parameter và đâu là Query Parameter trên một URL bất kỳ không?
- [ ] Bạn đã giải thích được tầm quan trọng của Header `Content-Type` chưa?
- [ ] Bạn có biết cách tự viết thuật toán phân tích (parse) chuỗi Query String thô chưa?
