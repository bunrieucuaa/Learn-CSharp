# Ví dụ Thực hành Bài 43: Thiết kế Dịch vụ Nghiệp vụ (Service Pattern)

Dưới đây là ví dụ hoàn chỉnh về thiết kế Service xử lý đăng ký tài khoản người dùng, sử dụng các Exception tự chế để xử lý lỗi nghiệp vụ sạch sẽ.

---

## Ví dụ 1: Định nghĩa các Ngoại lệ Nghiệp vụ (Custom Exceptions)

Định nghĩa các lỗi đặc trưng để tầng UI có thể bắt lỗi chính xác và thân thiện.

```csharp
using System;

namespace UserApp.Exceptions
{
    // Lỗi cơ sở cho các lỗi nghiệp vụ của ứng dụng
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }

    // Lỗi khi email đăng ký đã bị trùng trong DB
    public class DuplicateEmailException : BusinessException
    {
        public DuplicateEmailException(string email) 
            : base($"Email '{email}' đã tồn tại trên hệ thống!") { }
    }

    // Lỗi khi mật khẩu quá yếu không đủ ký tự
    public class WeakPasswordException : BusinessException
    {
        public WeakPasswordException(string detail) 
            : base($"Mật khẩu không an toàn: {detail}") { }
    }
}
```

---

## Ví dụ 2: Khai báo Interface và Lớp Service điều phối

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using UserApp.Exceptions;

namespace UserApp.Services
{
    // Đối tượng Model thuần túy chứa thông tin
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }

    // Giả định Interface của Data Access Layer (Repository)
    public interface IUserRepository
    {
        User GetByEmail(string email);
        void Save(User user);
        List<User> GetAll();
    }

    // 1. Định nghĩa Interface cho tầng Service (BLL)
    public interface IUserService
    {
        void RegisterUser(string email, string rawPassword);
    }

    // 2. Triển khai Service thực thi logic nghiệp vụ và điều phối
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Tiêm Repository vào Service qua Constructor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string email, string rawPassword)
        {
            // Nghiệp vụ 1: Validate Email cơ bản
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new BusinessException("Định dạng email không hợp lệ!");
            }

            // Nghiệp vụ 2: Kiểm tra mật khẩu mạnh (tối thiểu 6 ký tự)
            if (string.IsNullOrWhiteSpace(rawPassword) || rawPassword.Length < 6)
            {
                throw new WeakPasswordException("Mật khẩu phải chứa ít nhất 6 ký tự.");
            }

            // Nghiệp vụ 3: Kiểm tra email trùng lặp (gọi Repository kiểm tra DB)
            var existingUser = _userRepository.GetByEmail(email);
            if (existingUser != null)
            {
                throw new DuplicateEmailException(email);
            }

            // Giả lập mã hóa mật khẩu đơn giản trước khi lưu (Nghiệp vụ an toàn)
            string hashedPassword = "HASHED_" + rawPassword;

            var newUser = new User
            {
                Email = email,
                HashedPassword = hashedPassword
            };

            // Gọi Repository thực hiện lưu xuống DB
            _userRepository.Save(newUser);
        }
    }
}
```

---

## Ví dụ 3: Viết Mock Repository và ráp nối kiểm thử chạy chương trình

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using UserApp.Services;
using UserApp.Exceptions;

namespace UserApp
{
    // Giả lập Repository lưu dữ liệu trên RAM để test thử nhanh
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void Save(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            Console.WriteLine($"[MockDB] Đã lưu thành công User ID {user.Id} vào RAM.");
        }

        public List<User> GetAll() => _users;
    }

    class Program
    {
        static void Main()
        {
            // 1. Khởi tạo Mock DB
            IUserRepository repo = new MockUserRepository();

            // 2. Khởi tạo Service và tiêm DB
            IUserService userService = new UserService(repo);

            Console.WriteLine("--- LẦN 1: Đăng ký email hợp lệ ---");
            try
            {
                userService.RegisterUser("vy@gmail.com", "secure123");
                Console.WriteLine("Đăng ký hoàn tất thành công!");
            }
            catch (BusinessException ex)
            {
                Console.WriteLine($"Thất bại: {ex.Message}");
            }

            Console.WriteLine("\n--- LẦN 2: Cố tình đăng ký trùng email ---");
            try
            {
                userService.RegisterUser("vy@gmail.com", "anotherPass"); // Cố tình trùng email
            }
            catch (BusinessException ex)
            {
                // Catch bắt được DuplicateEmailException
                Console.WriteLine($"Thất bại: {ex.Message}"); 
            }

            Console.WriteLine("\n--- LẦN 3: Cố tình nhập mật khẩu ngắn ---");
            try
            {
                userService.RegisterUser("lam@gmail.com", "123"); // Mật khẩu quá ngắn
            }
            catch (BusinessException ex)
            {
                // Catch bắt được WeakPasswordException
                Console.WriteLine($"Thất bại: {ex.Message}");
            }
        }
    }
}
```
