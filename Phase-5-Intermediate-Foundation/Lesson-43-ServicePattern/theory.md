# Bài 43: Service Pattern (Mẫu thiết kế Dịch vụ nghiệp vụ)

Trong bài học trước, bạn đã học về mô hình phân tầng 3-Layer. Tầng nằm ở giữa — **Business Logic Layer (BLL)** — chính là nơi chứa linh hồn và trí tuệ của toàn bộ phần mềm. Để thiết kế tầng này một cách chuyên nghiệp và dễ bảo trì, chúng ta sử dụng **Service Pattern (Mẫu thiết kế Dịch vụ)**.

Bài học này sẽ đi sâu vào cách thiết kế các Service độc lập, cách kết hợp chúng với Interface để tăng tính kiểm thử (Testability) và cách phối hợp giữa Service và Repository.

---

## 1. Tại sao cần tách riêng lớp Service?

Một số lập trình viên mới thường đặt câu hỏi: *Tại sao không viết logic kiểm tra (ví dụ: mật khẩu phải mạnh, email không được trùng) ngay trong class Model của đối tượng hoặc viết trực tiếp ở Data Access?*

Lý do là:
* **Tách biệt dữ liệu và hành động:** Class Model (như `User`, `Student`) nên chỉ đóng vai trò chứa dữ liệu thuần túy (gọi là **Anemic Domain Model** - mô hình dữ liệu thiếu máu). Nó không nên chứa các logic nghiệp vụ phức tạp như kết nối Database để kiểm tra email trùng hay gọi API bên thứ ba.
* **Tái sử dụng cao:** Lớp Service hoạt động như một "đầu mối dịch vụ". Một Service đăng ký tài khoản (`UserService`) có thể phục vụ cho cả ứng dụng Console, ứng dụng chạy tự động (Background Worker), ứng dụng di động (Mobile App) hoặc trang Web API mà không cần viết lại logic xác thực.

---

## 2. Thiết kế Service dựa trên Interface (Interface-based Service)

Trong lập trình chuyên nghiệp, một Service **luôn luôn đi kèm với một Interface đại diện** cho nó.

```csharp
// 1. Định nghĩa Interface (Hợp đồng hành động)
public interface IUserService
{
    void RegisterNewUser(string username, string email, string password);
    void ChangePassword(int userId, string oldPassword, string newPassword);
}

// 2. Class Service cụ thể triển khai interface đó
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterNewUser(string username, string email, string password)
    {
        // Thực hiện logic nghiệp vụ...
    }

    public void ChangePassword(int userId, string oldPassword, string newPassword)
    {
        // Thực hiện logic nghiệp vụ...
    }
}
```

### Tại sao bắt buộc phải dùng Interface cho Service?
1. **Dependency Injection:** Dễ dàng đăng ký và tiêm dịch vụ vào tầng UI thông qua Interface (`services.AddTransient<IUserService, UserService>()`).
2. **Hỗ trợ Unit Test (Mocking):** Khi viết Unit Test cho tầng UI (Controller), bạn không muốn chương trình thực sự gửi một Email thật hay thực sự chọc xuống Database thật. Bằng việc sử dụng Interface `IUserService`, bạn có thể tạo ra một lớp giả lập `MockUserService` chỉ trả về kết quả thành công/thất bại giả lập để kiểm tra giao diện hiển thị của UI một cách dễ dàng và nhanh chóng.

---

## 3. Vai trò điều phối của Service đối với Repositories

Lớp Service không tự lưu trữ dữ liệu. Nó đóng vai trò là **Người điều phối (Orchestrator)**. 
Nó nhận yêu cầu, xử lý tính toán nghiệp vụ, và sau đó gọi một hoặc nhiều Repository khác nhau để hoàn thành công việc.

### Ví dụ luồng xử lý Checkout đơn hàng:
```csharp
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEmailService _emailService;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _emailService = emailService;
    }

    public void ProcessCheckout(Order order)
    {
        // 1. Kiểm tra tồn kho (gọi Product Repository)
        foreach (var item in order.Items)
        {
            var product = _productRepository.GetById(item.ProductId);
            if (product.StockQuantity < item.Quantity)
                throw new InvalidOperationException($"Sản phẩm {product.Name} đã hết hàng!");
        }

        // 2. Lưu đơn hàng vào DB (gọi Order Repository)
        _orderRepository.Add(order);

        // 3. Gửi email xác nhận đơn hàng (gọi Email Service)
        _emailService.SendOrderConfirmation(order.CustomerEmail);
    }
}
```
*Bạn thấy đấy, một mình Service điều phối hoạt động phối hợp của 3 dependency khác nhau để hoàn thành nghiệp vụ mua hàng!*

---

## 4. Xử lý ngoại lệ nghiệp vụ (Business Exceptions)

Khi Service phát hiện hành vi vi phạm quy tắc nghiệp vụ (ví dụ: Rút tiền vượt quá số dư tài khoản), nó nên thông báo thế nào cho tầng UI?
* **Cách tồi:** Trả về mã code số (`-1` là thiếu tiền, `-2` là tài khoản khóa). Cách này rất khó nhớ và dễ lỗi.
* **Cách sạch:** Định nghĩa các lớp ngoại lệ tự thiết kế (Custom Exceptions) đại diện cho từng lỗi nghiệp vụ cụ thể.

```csharp
// Định nghĩa Custom Exception
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string msg) : base(msg) { }
}

// Trong Service:
if (account.Balance < withdrawAmount)
{
    throw new InsufficientBalanceException("Số dư tài khoản không đủ để thực hiện giao dịch!");
}
```

---

## 5. Checklist đánh giá hiểu bài

1. Tại sao không nên viết logic kiểm tra dữ liệu trực tiếp trong class Model?
2. Việc sử dụng Interface cho Service đem lại 2 lợi ích cốt lõi nào cho dự án?
3. Nêu vai trò điều phối của Service đối với các Repository.
4. Tại sao sử dụng Custom Exception lại tốt hơn việc trả về các mã code số lỗi (ví dụ: `-1`, `-2`)?
