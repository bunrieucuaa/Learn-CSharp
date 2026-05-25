# Bài tập Thực hành Bài 43: Service Pattern

Hoàn thành các bài tập dưới đây bằng cách định nghĩa các interface và class Service tương ứng trên IDE của bạn.

---

## Bài tập 1: Dịch vụ Đăng ký Khóa học (CourseRegistrationService)
**Đề bài:**
1. Tạo interface `ICourseRegistrationService` chứa phương thức:
   `void RegisterStudentToCourse(int studentId, int courseId);`
2. Tạo class `CourseRegistrationService` triển khai interface đó.
3. Service cần tiêm 3 interface: `IStudentRepository`, `ICourseRepository`, và `IRegistrationRepository`.
4. Viết logic kiểm tra:
   - Nếu sinh viên không tồn tại: ném ra ngoại lệ `StudentNotFoundException`.
   - Nếu khóa học không tồn tại: ném ra ngoại lệ `CourseNotFoundException`.
   - Nếu sinh viên đã đăng ký khóa học này rồi: ném ra ngoại lệ `DuplicateRegistrationException`.
   - Nếu hợp lệ: Gọi `IRegistrationRepository` lưu bản ghi đăng ký mới.

---

## Bài tập 2: Tách biệt tính năng gửi thông báo tự động
**Đề bài:**
Trong class `UserService`, bạn có phương thức `RegisterUser`. Sau khi đăng ký thành công, hệ thống cần gửi một email chúc mừng đến người dùng.
* **Bài tập:** Để tuân thủ Single Responsibility (SRP), bạn không được viết code kết nối SMTP server gửi mail trực tiếp ở trong `UserService`. Hãy thiết kế một interface `IEmailService` có hàm `SendWelcomeEmail(string email)` và tiêm nó vào `UserService` để gọi sau khi lưu thông tin thành công.

---

## Bài tập 3: Dịch vụ Quản lý Thư viện (LibraryService)
**Đề bài:**
Thiết kế `LibraryService` thực thi logic cho mượn sách.
- Phương thức: `void BorrowBook(int memberId, string isbn);`
- Luật nghiệp vụ:
  - Một thành viên không được mượn quá 3 cuốn sách cùng một lúc. (Nếu vi phạm, ném ra ngoại lệ `BorrowLimitExceededException`).
  - Cuốn sách có mã `isbn` yêu cầu phải còn bản sao rảnh trong kho thư viện. (Nếu hết, ném ra ngoại lệ `BookOutOfStockException`).
  - Nếu thỏa mãn: Tạo bản ghi mượn sách mới và giảm số lượng sách trong kho đi 1.

---

## Bài tập 4: Định nghĩa bộ Custom Exception cho Giỏ hàng (Cart)
**Đề bài:**
Hãy tự viết mã khai báo 3 class Custom Exception kế thừa từ `Exception` dùng cho hệ thống Giỏ hàng bán hàng trực tuyến:
1. `EmptyCartException` (lỗi khi thanh toán một giỏ hàng trống không có sản phẩm).
2. `InvalidQuantityException` (lỗi khi người dùng cập nhật số lượng sản phẩm bằng số âm hoặc số 0).
3. `ProductPriceChangedException` (lỗi cảnh báo khi giá sản phẩm bị thay đổi ở hệ thống admin trong lúc người dùng đang thanh toán).

---

## Bài tập 5: Phân tích Rich vs Anemic Domain Model
**Đề bài:**
Đọc lại phần lý thuyết mục 1 và trả lời câu hỏi:
- Sự khác nhau giữa Anemic Domain Model (Mô hình thiếu máu) và Rich Domain Model (Mô hình giàu có) là gì?
- Tại sao hầu hết các ứng dụng web backend sử dụng Layered Architecture (như ASP.NET Core) kết hợp Service Pattern lại ưa chuộng sử dụng Anemic Domain Model (các class Model chỉ chứa `{ get; set; }` dữ liệu)?
