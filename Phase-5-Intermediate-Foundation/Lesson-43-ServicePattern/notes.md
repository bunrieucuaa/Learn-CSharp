# Ghi nhớ Bài 43: Service Pattern

## 1. Tóm tắt kiến thức cốt lõi
* **Tầng nghiệp vụ (BLL):** Chứa toàn bộ các quy tắc tính toán và kiểm tra logic nghiệp vụ của phần mềm.
* **Service Pattern:** Mô hình tổ chức code nghiệp vụ thành các lớp Service độc lập đại diện bởi các Interface.
* **Anemic Domain Model:** Các lớp Model dữ liệu thuần túy (chỉ chứa các thuộc tính `{ get; set; }`), không chứa logic xử lý phức tạp.
* **Vai trò điều phối:** Service đóng vai trò trung tâm phối hợp hoạt động của nhiều Repository và dịch vụ khác nhau để giải quyết một quy trình nghiệp vụ phức tạp.
* **Business Exceptions:** Định nghĩa các Custom Exception đặc trưng của nghiệp vụ để ném ra khi có lỗi, giúp tầng UI dễ bắt lỗi sạch sẽ.

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Bỏ qua interface cho Service:** Khai báo trực tiếp class `UserService` và tiêm thẳng class đó vào UI. Điều này vi phạm nguyên lý Dependency Inversion, khiến bạn không thể viết Unit Test độc lập (Mocking) cho tầng UI được.
> * **Mã lỗi ma thuật (Magic Error Codes):** Trả về số `-1` hay `-2` khi xảy ra lỗi nghiệp vụ. Điều này khiến code cực kỳ khó đọc và bảo trì. Hãy luôn ném ra Exception tự giải nghĩa (Custom Exception).

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao Service cần được thiết kế dựa trên Interface chưa?
- [ ] Bạn có phân biệt được vai trò của Service (chứa logic) và Repository (chỉ lo đọc ghi file/DB) không?
- [ ] Bạn có tự tin thiết kế một bộ Custom Exception cho một quy trình nghiệp vụ cụ thể chưa?
