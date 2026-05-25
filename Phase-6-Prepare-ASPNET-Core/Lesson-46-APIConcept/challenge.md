# Thách thức Bài 46: Thiết kế Hệ thống REST API cho Ứng dụng Gọi xe Công nghệ (Ride-Hailing API)

## Ngữ cảnh
Bạn được bổ nhiệm làm kiến trúc sư trưởng (Lead Architect) thiết kế hệ thống API cho một startup gọi xe công nghệ thế hệ mới (tương tự Grab/Uber). 

Hệ thống có 3 đối tượng tài nguyên chính cần quản lý:
1. **Khách hàng (`riders`)**
2. **Tài xế (`drivers`)**
3. **Chuyến đi (`trips`)**

Một chuyến đi (`trips`) luôn liên kết với một Khách hàng và một Tài xế cụ thể.

## Yêu cầu Thách thức
Hãy thiết kế một bản tài liệu đặc tả API chuẩn RESTful chi tiết cho các hành động dưới đây. Tài liệu phải chỉ rõ **HTTP Method** và cấu trúc **URL Route** tương ứng cho mỗi hành động.

### Danh sách hành động cần thiết kế:
1. **Quản lý Tài xế:**
   - Xem toàn bộ danh sách tài xế đang hoạt động.
   - Đăng ký hồ sơ tài xế mới.
   - Xem chi tiết hồ sơ tài xế có mã ID `DRV-888`.
   - Cập nhật riêng trạng thái hoạt động (ví dụ: Đổi sang rảnh/bận) của tài xế `DRV-888`.
2. **Quản lý Khách hàng:**
   - Tạo mới tài khoản khách hàng.
   - Xóa tài khoản khách hàng `RID-111` ra khỏi hệ thống ( do vi phạm chính sách).
3. **Quản lý Chuyến đi (Quan hệ lồng ghép):**
   - Đặt một chuyến đi mới (tạo mới Trip).
   - Xem chi tiết thông tin chuyến đi có mã ID `TRIP-999`.
   - Khách hàng `RID-111` muốn xem toàn bộ lịch sử các chuyến đi của mình.
   - Hủy chuyến đi `TRIP-999` (Xóa/Cập nhật trạng thái hủy).
   - Lấy danh sách các đánh giá (Reviews) của chuyến đi `TRIP-999` (Sub-resource).

## Định dạng trình bày báo cáo (Ví dụ mẫu)
Hãy viết câu trả lời của bạn dưới dạng bảng Markdown sạch đẹp như sau:
```markdown
| Nghiệp vụ | HTTP Method | URL Route | Giải thích ngắn |
|---|---|---|---|
| Đăng ký tài xế | POST | /api/drivers | Tạo mới thực thể tài xế |
| ... | ... | ... | ... |
```
*(Hãy tự suy nghĩ cẩn thận và điền đầy đủ các nghiệp vụ được yêu cầu ở trên vào bảng. Đảm bảo không dùng động từ trong URL và cấu trúc phân cấp tài nguyên con hợp lý).*
