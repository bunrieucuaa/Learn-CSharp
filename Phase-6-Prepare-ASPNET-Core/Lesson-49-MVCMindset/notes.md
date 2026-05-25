# Ghi nhớ Bài 49: MVC & Parameter Binding

## 1. Tóm tắt kiến thức cốt lõi
* **MVC Pattern:** 
  * Model: Chứa dữ liệu (Model/DTO) và logic nghiệp vụ (Service/Repository).
  * View: Client (Angular App) nhận và hiển thị dữ liệu JSON.
  * Controller: Bộ điều phối, tiếp nhận request và trả về response.
* **Parameter Binding:**
  * `[FromRoute]`: Lấy dữ liệu ID từ đường dẫn URL.
  * `[FromQuery]`: Lấy các tham số lọc, phân trang sau dấu `?`.
  * `[FromBody]`: Parse dữ liệu JSON phức tạp gửi trong Body.
  * `[FromHeader]`: Đọc siêu dữ liệu (token) gửi ở Header.
* **IActionResult:** Kiểu trả về giúp dễ dàng đóng gói dữ liệu kèm Status Code tương ứng (`Ok`, `Created`, `BadRequest`, `NotFound`).
* **Skinny Controller:** Nguyên tắc giữ Controller mỏng nhẹ, chỉ làm nhiệm vụ điều phối và validation thô. Logic nghiệp vụ sâu bắt buộc đẩy xuống Service.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi Binding sai kiểu dữ liệu:** Ví dụ bạn khai báo `[FromRoute] int id` nhưng Client lại gửi lên URL `/api/products/abc` (truyền chuỗi "abc" thay vì số nguyên). ASP.NET Core sẽ không thể bind dữ liệu và tự động trả về lỗi `400 Bad Request` trước khi chạy vào dòng code đầu tiên của hàm xử lý.
> * **Lập trình logic nghiệp vụ trong Controller:** Viết hàng chục dòng code `if-else` kiểm tra database, tính toán tiền nong ngay trong Controller. Điều này vi phạm nguyên tắc SoC, khiến code không thể viết unit test và không thể tái sử dụng.

## 3. Checklist tự đánh giá
- [ ] Bạn đã phân biệt được vai trò của Model, View, và Controller trong Web API chưa?
- [ ] Bạn có chỉ ra được nguồn gốc lấy dữ liệu của 4 chú thích Parameter Binding chưa?
- [ ] Bạn có nắm được nguyên tắc thiết kế "Skinny Controller, Fat Service" chưa?
- [ ] Bạn có biết cách ánh xạ các kết quả xử lý nghiệp vụ sang kiểu trả về `IActionResult` thích hợp không?
