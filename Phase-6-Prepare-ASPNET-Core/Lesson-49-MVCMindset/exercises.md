# Bài tập Thực hành Bài 49: MVC & Parameter Binding

Hãy thực hành các bài tập dưới đây để làm quen với các chú thích liên kết tham số (Parameter Binding Annotations) và kiểu trả về của Controller.

---

## Bài tập 1: Lựa chọn Parameter Binding phù hợp
**Đề bài:**
Hãy lựa chọn Annotation (`[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]`) phù hợp nhất cho các tham số đầu vào của hàm xử lý trong các trường hợp sau:
1. Đọc mã token bảo mật `Authorization` để xác thực người dùng.
2. Nhận thông tin ID sản phẩm từ URL: `/api/products/15`.
3. Lọc danh sách sản phẩm theo khoảng giá: `/api/products?minPrice=100&maxPrice=500`.
4. Nhận đối tượng giỏ hàng phức tạp gồm danh sách 10 món hàng khi người dùng bấm thanh toán.
5. Từ khóa tìm kiếm của người dùng nhập vào ô Search: `/api/books?keyword=csharp`.

---

## Bài tập 2: Thiết kế hàm Cập nhật Sản phẩm (Update Product)
**Đề bài:**
Viết chữ ký (signature) của một phương thức cập nhật thông tin sản phẩm trong Controller có đường dẫn:
`PUT /api/products/{id}`
Hàm này cần nhận 2 tham số:
1. `id` của sản phẩm cần sửa (lấy từ Route).
2. Đối tượng DTO chứa dữ liệu mới `ProductUpdateDto` (lấy từ Body).
* **Bài tập:** Hãy viết code khai báo phương thức này kèm các chú thích annotation thích hợp đứng trước mỗi tham số.

---

## Bài tập 3: Map trạng thái lỗi sang IActionResult phù hợp
**Đề bài:**
Hãy viết các dòng lệnh trả về (`return ...`) tương ứng của ASP.NET Core Controller trong các tình huống sau:
1. Xử lý thành công và trả về một đối tượng `userProfile`.
2. Không tìm thấy thông tin sản phẩm có ID yêu cầu.
3. Người dùng chưa đăng nhập hệ thống.
4. Dữ liệu gửi lên bị sai logic (ví dụ: tuổi là số âm), cần trả về thông báo lỗi `"Tuổi không hợp lệ"`.
5. Tạo mới thành công bản ghi, trả về URL truy xuất bản ghi mới `/api/users/99` kèm chính đối tượng `newUser`.

---

## Bài tập 4: Phân biệt nhiệm vụ của Controller và Service
**Đề bài:**
Giả sử bạn đang viết code cho tính năng "Đăng ký thành viên". Hãy phân bổ các tác vụ sau vào đúng nơi xử lý (**Controller** hoặc **Service**):
1. Đọc chuỗi JSON từ request body map vào class `RegisterDto`.
2. Kiểm tra xem email đăng ký đã tồn tại trong Database chưa.
3. Mã hóa (Hash) mật khẩu của người dùng.
4. Trả về mã trạng thái `201 Created` nếu đăng ký thành công.
5. Gửi email xác nhận đăng ký thành viên.
6. Trả về mã trạng thái `400 Bad Request` nếu email gửi lên bị thiếu ký tự `@`.

---

## Bài tập 5: Phân tích cơ chế binding của kiểu dữ liệu phức tạp (Complex Object)
**Đề bài:**
* **Câu hỏi:** Tại sao các kiểu dữ liệu phức tạp (như class `Student`, class `Order`) mặc định lại được ASP.NET Core liên kết bằng chú thích `[FromBody]` (đọc từ Body JSON), trong khi các kiểu dữ liệu nguyên bản (như `int`, `string`, `bool`) lại mặc định được đọc từ URL (Route hoặc Query)? Giải thích nguyên nhân dựa trên cấu trúc gói tin HTTP.
