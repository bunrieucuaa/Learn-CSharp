# Thử thách Cuối cùng Khóa học: Thiết kế Kiến trúc Hệ thống cho Tính năng Đăng nhập (Authentication Flow Architecture)

## Ngữ cảnh
Bạn đã đi đến điểm cuối của chặng đường 50 bài học C# và Kiến trúc phần mềm. Để khép lại khóa học một cách ấn tượng nhất, bạn được giao thử thách thiết kế kiến trúc toàn bộ luồng đi dữ liệu cho một tính năng kinh điển nhất của mọi hệ thống web: **Đăng nhập và Xác thực tài khoản (Authentication & Authorization Flow)**.

Đây là cơ hội để bạn xâu chuỗi toàn bộ kiến thức: Client-side (Angular), HTTP Request/Response, Middleware, Controller, Service, Repository, Database.

---

## Nghiệp vụ yêu cầu thiết kế
Khách hàng mở trình duyệt (Angular App):
1. Nhập thông tin: Email và Mật khẩu.
2. Bấm nút **"Đăng nhập"**.
3. Hệ thống kiểm tra: Nếu thông tin đúng, trả về mã Token bảo mật dạng chuỗi (JWT Token) để Angular lưu lại và hiển thị thông báo đăng nhập thành công. Nếu thông tin sai, trả về mã lỗi thích hợp.

---

## Nhiệm vụ Thách thức của bạn
Hãy vẽ sơ đồ thiết kế kiến trúc và mô tả chi tiết đường đi của dữ liệu ở từng bước (bằng văn bản mô tả cụ thể từ Bước 1 đến khi hoàn thành). 

Tài liệu thiết kế của bạn phải làm rõ các điểm sau:

### 1. Phía Client (Angular)
- Client sẽ đóng gói dữ liệu đăng nhập bằng phương thức HTTP nào? URL gửi đến Route nào là chuẩn REST?
- Gói tin Request gửi đi sẽ được đặt thông tin Email/Password ở đâu (Header, Query string, hay Body)?
- Định dạng dữ liệu gửi đi nên là gì (JSON)? Hãy viết mẫu chuỗi JSON gửi đi (camelCase).

### 2. Phía Web Server & Middleware Pipeline
- Request chạm cổng Web Server.
- Bộ lọc Middleware Logger làm nhiệm vụ gì?
- Middleware Cors có vai trò gì (giới thiệu sơ lược: kiểm soát chặn truy cập chéo tên miền)?

### 3. Phía Controller
- Annotation Parameter Binding nào được dùng trước tham số DTO của hàm đăng nhập trong Controller?
- Controller kiểm tra validation thô bằng cách nào?
- Khi Service trả về kết quả thành công hoặc lỗi, Controller sẽ đóng gói dữ liệu và trả về các mã HTTP Status Code nào tương ứng?

### 4. Phía Service
- Dịch vụ đăng nhập `AuthService` sẽ làm nhiệm vụ gì?
- Nó cần gọi Repository nào để kiểm tra thông tin người dùng?
- Nó thực hiện mã hóa mật khẩu và sinh mã token như thế nào (giải thích ngắn gọn tư duy nghiệp vụ)?

### 5. Phía Repository & Database
- Repository truy vấn DB bằng cách nào? Nó trả dữ liệu gì về cho Service?

---

## Định dạng bài báo cáo thiết kế:
Hãy trình bày bản đặc tả thiết kế hệ thống này một cách chi tiết dưới dạng văn bản Markdown có cấu trúc các tiêu đề rõ ràng. Thiết kế này sẽ là tài liệu cực kỳ giá trị để bạn lưu giữ và đối chiếu khi bắt đầu xây dựng dự án Web API thực tế ở giai đoạn tiếp theo.
