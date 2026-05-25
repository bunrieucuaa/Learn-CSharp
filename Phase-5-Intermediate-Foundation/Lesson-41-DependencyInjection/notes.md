# Ghi nhớ Bài 41: Dependency Injection

## 1. Tóm tắt kiến thức cốt lõi
* **Tight Coupling:** Lỗi thiết kế các Class khởi tạo trực tiếp phụ thuộc bằng `new`, gây khó sửa đổi và không thể viết unit test.
* **Loose Coupling:** Giải pháp nhận đối tượng phụ thuộc qua Constructor dưới dạng Interface.
* **DIP (Dependency Inversion Principle):** Mô-đun cấp cao không phụ thuộc mô-đun cấp thấp, cả hai phụ thuộc vào sự trừu tượng (Interface).
* **DI Lifetimes:**
  * **Transient:** Mỗi lần yêu cầu tạo ra 1 instance mới.
  * **Scoped:** Tạo 1 instance duy nhất trong phạm vi 1 Request (đặc trưng Web).
  * **Singleton:** Tạo duy nhất 1 instance và dùng chung suốt vòng đời ứng dụng.
* **Constructor Injection:** Tiêm dependency trực tiếp qua phương thức khởi tạo của Class.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi Captive Dependency:** Đăng ký một Scoped service bên trong một Singleton service. Lỗi này làm rò rỉ kết nối Database và bộ nhớ. .NET sẽ chặn lỗi này khi chạy app.
> * **Quên đăng ký dịch vụ:** Gây ra lỗi `InvalidOperationException: Unable to resolve service for type '...'`. Hãy đảm bảo tất cả các interface và class liên quan đều được đăng ký đầy đủ trong `ServiceCollection`.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao viết code ghép lỏng (Loose Coupling) lại dễ viết Unit Test hơn chưa?
- [ ] Bạn phân biệt được rõ Transient, Scoped và Singleton chưa?
- [ ] Bạn có biết cách sửa lỗi thiếu đăng ký dịch vụ trong DI Container không?
- [ ] Bạn có liên hệ được hệ thống DI này giống với hệ thống DI của Angular không?
