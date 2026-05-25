# Bài tập Thực hành Bài 41: Dependency Injection

Thực hành các bài tập dưới đây bằng cách viết code cấu hình và sử dụng DI Container trong dự án test của bạn.

---

## Bài tập 1: Tiêm ILogger vào UserService
**Đề bài:**
1. Hãy định nghĩa một interface `ILogger` chứa phương thức `void Log(string message)`.
2. Tạo class `ConsoleLogger` triển khai `ILogger` để in log ra màn hình Console.
3. Tạo class `UserService` nhận `ILogger` qua Constructor và sử dụng nó để ghi nhận hành động khi tạo User mới.

* **Yêu cầu:** Đăng ký các dịch vụ vào DI Container và khởi tạo `UserService` từ Container để chạy thử.

---

## Bài tập 2: Hoán đổi dịch vụ thanh toán
**Đề bài:**
Cho interface:
```csharp
public interface IPaymentGateway
{
    void ProcessPayment(decimal amount);
}
```
Hãy viết 2 class triển khai interface này:
1. `PaypalPayment` (in ra màn hình: `"Thanh toán $x qua PayPal"`).
2. `StripePayment` (in ra màn hình: `"Thanh toán $x qua Stripe"`).

Viết một class `CheckoutManager` nhận `IPaymentGateway` qua Constructor.
* **Bài tập:** Hãy cấu hình DI Container sao cho khi ta thay đổi cấu hình đăng ký từ `PaypalPayment` sang `StripePayment`, chương trình tự động hoán đổi cổng thanh toán mà không cần sửa code của class `CheckoutManager`.

---

## Bài tập 3: Đăng ký dịch vụ dạng Singleton
**Đề bài:**
Hãy tạo một class `ConfigurationSettings` lưu trữ cấu hình hệ thống (ví dụ: `AppName = "MyApp"`, `MaxUsers = 100`).
* **Câu hỏi:** Trong 3 loại Lifetimes (Transient, Scoped, Singleton), loại nào thích hợp nhất để đăng ký `ConfigurationSettings` vào DI Container? Hãy viết mã cấu hình đăng ký đó.

---

## Bài tập 4: Phân tích lỗi DI Container khi thiếu đăng ký
**Đề bài:**
Hãy thử chạy đoạn code sau trong IDE của bạn:
```csharp
var services = new ServiceCollection();
services.AddTransient<OrderService>(); // Đăng ký OrderService nhưng quên đăng ký IMessageSender!
var provider = services.BuildServiceProvider();

var order = provider.GetRequiredService<OrderService>(); // Gọi hàm lấy dịch vụ
```
* **Bài tập:** Chạy thử chương trình, đọc nội dung ngoại lệ `InvalidOperationException` được ném ra và giải thích tại sao lỗi đó xảy ra. Làm thế nào để sửa lỗi?

---

## Bài tập 5: Thực hành Scoped Lifetime trong Web API giả lập
**Đề bài:**
Tạo một class `UserContext` lưu trữ thông tin User đang đăng nhập hiện tại (`CurrentUserId`).
- Đăng ký `UserContext` dạng **Scoped**.
- Tạo một Scope bằng lệnh `provider.CreateScope()`. Trong scope đó, lấy ra 2 instance của `UserContext` và thay đổi `CurrentUserId` ở instance thứ nhất. 
- Kiểm tra xem instance thứ hai có tự động thay đổi theo hay không.
- Tạo tiếp một scope thứ hai và kiểm tra xem `CurrentUserId` có bị ảnh hưởng từ scope thứ nhất hay không.
