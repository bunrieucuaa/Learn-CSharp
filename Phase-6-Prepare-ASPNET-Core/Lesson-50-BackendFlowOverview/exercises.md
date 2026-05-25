# Bài tập Thực hành Bài 50: Tổng ôn tập Kiến thức Khóa học

Dưới đây là 5 câu hỏi ôn tập tổng hợp giúp bạn hệ thống lại toàn bộ kiến thức của 6 Phase đã học.

---

## Bài tập 1: Giải thích luồng hoạt động của Middleware và Routing
**Đề bài:**
1. Hãy mô tả vai trò của **Middleware** trong ứng dụng Web API. Dữ liệu khi gửi đến Server sẽ đi qua Middleware trước hay đi vào Controller trước?
2. Chức năng chính của **Routing** là gì?

---

## Bài tập 2: Đối chiếu C# Backend và Angular Frontend
**Đề bài:**
Khi làm việc với dự án Fullstack Angular + ASP.NET Core:
1. Interface `IUserService` và class `UserService` nằm ở phía ứng dụng nào (Client hay Server)?
2. File định nghĩa cấu hình Router URL (ví dụ: `/api/products`) nằm ở phía nào?
3. Thư viện `HttpClient` dùng để gửi request nằm ở phía nào?

---

## Bài tập 3: Bản chất của Async/Await và Luồng xử lý dữ liệu
**Đề bài:**
* **Câu hỏi:** Khi một HTTP Request gửi đến hàm `async Task<IActionResult> GetProductAsync()` và gặp từ khóa `await _repository.GetFromDbAsync()`.
  1. Thread (Luồng) của Web Server đang xử lý request đó sẽ bị khóa cứng (Block) để chờ Database phản hồi hay được giải phóng?
  2. Tại sao cơ chế giải phóng này lại giúp Web Server có thể xử lý đồng thời hàng nghìn người dùng truy cập cùng lúc?

---

## Bài tập 4: Phân biệt vai trò của các tầng trong mô hình 3-Layer
**Đề bài:**
Hãy xác định xem mỗi hành động cụ thể sau đây thuộc về trách nhiệm của tầng nào trong mô hình 3-Layer (Presentation, Business Logic, hay Data Access):
1. Đọc tệp tin JSON lưu trên ổ cứng bằng `StreamReader`.
2. Kiểm tra xem số dư tài khoản của khách hàng có đủ để thực hiện giao dịch hay không.
3. Bắt Exception và in dòng chữ màu đỏ cảnh báo lỗi lên màn hình Console.
4. Đăng ký tài khoản người dùng và tự động sinh mã ID ngẫu nhiên không trùng lặp.
5. Giải tuần tự hóa JSON string nhận về thành đối tượng DTO.

---

## Bài tập 5: Tự đánh giá mức độ sẵn sàng học ASP.NET Core & Angular
**Đề bài:**
Hãy tự rà soát lại danh sách checklist dưới đây và tự đánh giá điểm số (từ 1 đến 10) cho mức độ hiểu bài của mình đối với mỗi chủ đề:
- [ ] Lập trình hướng đối tượng OOP (4 tính chất cốt lõi, Abstract, Interface).
- [ ] Lập trình bất đồng bộ (`Async` / `Await`).
- [ ] Cách thức tiêm và quản lý vòng đời phụ thuộc (`Dependency Injection`).
- [ ] Kiến trúc phân tách 3 lớp (`Presentation - Service - Repository`).
- [ ] Cách thức truyền dữ liệu qua gói tin `HTTP Request` & `Response` (Headers, Body, Parameters).
- [ ] Cách thức serialize và cấu hình định dạng `JSON` trao đổi dữ liệu.
