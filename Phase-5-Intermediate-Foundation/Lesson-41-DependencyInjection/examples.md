# Ví dụ Thực hành Bài 41: Dependency Injection

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức hoạt động của Dependency Injection và các loại thời gian sống (Lifetimes) của dịch vụ.

---

## Ví dụ 1: Trước và Sau khi áp dụng Dependency Injection

Ví dụ mô phỏng hệ thống gửi tin nhắn thông báo.

### Cách viết cũ (Tight Coupling - Không dùng DI)
```csharp
public class EmailSender
{
    public void Send(string msg) => Console.WriteLine($"Email: {msg}");
}

public class OrderService
{
    private EmailSender _sender;

    public OrderService()
    {
        // Khởi tạo trực tiếp bằng từ khóa new -> Bị ghép cứng!
        _sender = new EmailSender(); 
    }

    public void Checkout()
    {
        Console.WriteLine("Đang xử lý đơn hàng...");
        _sender.Send("Đơn hàng của bạn đã được thanh toán.");
    }
}
```

### Cách viết mới (Loose Coupling - Áp dụng DI qua Constructor)
```csharp
using System;

// 1. Tạo Interface đại diện cho hành động gửi tin nhắn
public interface IMessageSender
{
    void Send(string message);
}

// 2. Hiện thực hóa cụ thể bằng Class gửi Email
public class EmailSender : IMessageSender
{
    public void Send(string message) => Console.WriteLine($"Email: {message}");
}

// 3. Hiện thực hóa bằng Class gửi SMS (dễ dàng hoán đổi)
public class SmsSender : IMessageSender
{
    public void Send(string message) => Console.WriteLine($"SMS: {message}");
}

// 4. OrderService nhận phụ thuộc thông qua Interface và Constructor
public class OrderService
{
    private readonly IMessageSender _sender;

    public OrderService(IMessageSender sender)
    {
        _sender = sender; // Phụ thuộc được bơm từ ngoài vào
    }

    public void Checkout()
    {
        Console.WriteLine("Đang xử lý đơn hàng...");
        _sender.Send("Đơn hàng của bạn đã thanh toán.");
    }
}
```

---

## Ví dụ 2: Cấu hình và giải quyết Dependency tự động bằng DI Container

Sử dụng thư viện chính thức của Microsoft `.NET DI Container`.
*(Trong dự án Console, bạn cần cài đặt package: `dotnet add package Microsoft.Extensions.DependencyInjection`)*.

```csharp
using System;
using Microsoft.Extensions.DependencyInjection; // Bắt buộc import thư viện này

class Program
{
    static void Main()
    {
        // 1. Khởi tạo kho chứa cấu hình dịch vụ (ServiceCollection)
        var services = new ServiceCollection();

        // 2. Đăng ký các dịch vụ vào Container
        services.AddTransient<IMessageSender, EmailSender>(); // Đăng ký IMessageSender ánh xạ sang EmailSender
        services.AddTransient<OrderService>();                // Đăng ký chính class OrderService

        // 3. Xây dựng bộ máy giải quyết phụ thuộc (ServiceProvider)
        var serviceProvider = services.BuildServiceProvider();

        // 4. Lấy dịch vụ ra sử dụng
        // DI Container sẽ tự động phát hiện OrderService cần IMessageSender. 
        // Nó sẽ tự khởi tạo EmailSender, sau đó tự khởi tạo OrderService và nhét EmailSender vào!
        var orderService = serviceProvider.GetRequiredService<OrderService>();

        orderService.Checkout();
        // Output:
        // Đang xử lý đơn hàng...
        // Email: Đơn hàng của bạn đã thanh toán.
    }
}
```

---

## Ví dụ 3: Minh hoạ vòng đời dịch vụ (Transient vs Scoped vs Singleton)

Ví dụ tạo ra một mã định danh ngẫu nhiên (Guid) cho mỗi dịch vụ để kiểm tra xem khi nào một đối tượng mới được tạo ra.

```csharp
using System;
using Microsoft.Extensions.DependencyInjection;

// Định nghĩa các interface đại diện cho 3 vòng đời
public interface ITransientService { Guid Id { get; } }
public interface IScopedService { Guid Id { get; } }
public interface ISingletonService { Guid Id { get; } }

// Class cụ thể triển khai sinh mã ID ngẫu nhiên khi khởi tạo
public class LifecycleService : ITransientService, IScopedService, ISingletonService
{
    public Guid Id { get; } = Guid.NewGuid(); // Mỗi đối tượng mới sẽ có 1 GUID riêng biệt
}

// Dịch vụ phụ thuộc sử dụng cả 3 loại lifecycle để kiểm chứng
public class ClientService
{
    public ITransientService Transient { get; }
    public IScopedService Scoped { get; }
    public ISingletonService Singleton { get; }

    public ClientService(ITransientService transient, IScopedService scoped, ISingletonService singleton)
    {
        Transient = transient;
        Scoped = scoped;
        Singleton = singleton;
    }
}

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();

        // Đăng ký 3 loại thời gian sống
        services.AddTransient<ITransientService, LifecycleService>();
        services.AddScoped<IScopedService, LifecycleService>();
        services.AddSingleton<ISingletonService, LifecycleService>();

        services.AddTransient<ClientService>();

        var provider = services.BuildServiceProvider();

        // --- KHỞI TẠO REQUEST LẦN 1 (Phạm vi 1) ---
        using (var scope1 = provider.CreateScope())
        {
            var p = scope1.ServiceProvider;
            var clientA = p.GetRequiredService<ClientService>();
            var clientB = p.GetRequiredService<ClientService>();

            Console.WriteLine("--- SCOPE 1: Lấy hai đối tượng liên tiếp ---");
            Console.WriteLine($"Transient A: {clientA.Transient.Id}");
            Console.WriteLine($"Transient B: {clientB.Transient.Id} (Mỗi lần lấy là Khác nhau)");
            Console.WriteLine();
            Console.WriteLine($"Scoped A:    {clientA.Scoped.Id}");
            Console.WriteLine($"Scoped B:    {clientB.Scoped.Id} (Trong cùng 1 Scope là Giống nhau)");
            Console.WriteLine();
            Console.WriteLine($"Singleton A: {clientA.Singleton.Id}");
            Console.WriteLine($"Singleton B: {clientB.Singleton.Id} (Giống nhau)");
        }

        Console.WriteLine("\n----------------------------------------------------\n");

        // --- KHỞI TẠO REQUEST LẦN 2 (Phạm vi 2) ---
        using (var scope2 = provider.CreateScope())
        {
            var p = scope2.ServiceProvider;
            var clientC = p.GetRequiredService<ClientService>();

            Console.WriteLine("--- SCOPE 2: Lấy đối tượng ở Scope mới ---");
            Console.WriteLine($"Transient C: {clientC.Transient.Id} (Tạo mới hoàn toàn)");
            Console.WriteLine($"Scoped C:    {clientC.Scoped.Id} (Thay đổi vì đã chuyển sang Scope 2)");
            Console.WriteLine($"Singleton C: {clientC.Singleton.Id} (Vẫn GIỮ NGUYÊN không đổi từ đầu chương trình)");
        }
    }
}
```
