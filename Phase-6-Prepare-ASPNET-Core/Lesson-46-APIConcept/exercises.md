# Bài tập Thực hành Bài 46: RESTful API Design

Hãy thực hành thiết kế các cấu trúc định tuyến API theo chuẩn RESTful thông qua các bài tập dưới đây.

---

## Bài tập 1: Thiết kế API Quản lý Thư viện (Library)
**Đề bài:**
Hãy thiết kế danh sách các URL và Method tương ứng cho hệ thống API của một thư viện sách đáp ứng các chức năng sau (đảm bảo đúng chuẩn danh từ số nhiều và các Method GET/POST/PUT/DELETE):
1. Lấy danh sách toàn bộ các cuốn sách.
2. Tìm kiếm sách theo mã ISBN (ví dụ: `ISBN999`).
3. Nhập kho một cuốn sách mới.
4. Cập nhật thông tin chi tiết của một cuốn sách theo mã ISBN.
5. Xóa hoàn toàn một cuốn sách ra khỏi thư viện.

---

## Bài tập 2: Thiết kế API Quản lý Đơn hàng (E-Commerce)
**Đề bài:**
Hãy thiết kế cấu trúc URL RESTful API cho đối tượng Đơn hàng (`orders`):
1. Lấy lịch sử mua hàng của khách hàng (lấy toàn bộ đơn).
2. Xem chi tiết đơn hàng số `1024`.
3. Đặt mua đơn hàng mới.
4. Hủy đơn hàng số `1024`.
5. Cập nhật riêng địa chỉ giao hàng của đơn hàng số `1024` (Sử dụng phương thức sửa một phần).

---

## Bài tập 3: Thiết kế API cho Quan hệ Phụ thuộc (Sub-resource)
**Đề bài:**
Giả sử bạn có tài nguyên Lớp học (`classes`) và mỗi lớp học có nhiều Học sinh (`students`). Hãy thiết kế URL RESTful API cho các tác vụ sau:
1. Lấy danh sách học sinh thuộc lớp học có ID = `5`.
2. Thêm một học sinh mới vào lớp học có ID = `5`.
3. Xóa học sinh có ID = `12` ra khỏi lớp học có ID = `5`.

---

## Bài tập 4: Phát hiện lỗi thiết kế REST API
**Đề bài:**
Dưới đây là thiết kế API của một dự án cũ do một cộng tác viên viết. Hãy chỉ ra các điểm vi phạm nguyên tắc thiết kế RESTful và viết lại các URL đó cho chuẩn mực:
1. `GET /api/get-all-users`
2. `POST /api/users/delete?id=5`
3. `POST /api/update-user-password/5`
4. `GET /api/createNewCategory`

---

## Bài tập 5: Phân tích 6 ràng buộc của REST
**Đề bài:**
Hãy giải thích ngắn gọn bằng ngôn ngữ dễ hiểu:
1. Ràng buộc **Stateless** (Không trạng thái) của REST yêu cầu Client và Server phải làm gì?
2. Tại sao việc tuân thủ ràng buộc **Uniform Interface** (Giao diện đồng nhất) lại giúp lập trình viên viết ứng dụng Client (Angular) và Backend (ASP.NET Core) có thể làm việc độc lập song song với nhau?
