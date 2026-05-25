# Ví dụ Thực hành Bài 50: Mô phỏng Luồng đi dữ liệu Web API Pipeline

Dưới đây là một chương trình C# hoàn chỉnh mô phỏng toàn bộ luồng đi của một HTTP Request: từ lúc Client gửi yêu cầu -> đi qua bộ lọc Middleware xác thực -> vào Routing của Controller -> gọi Service xử lý nghiệp vụ -> gọi Repository truy vấn dữ liệu RAM DB -> và trả phản hồi ngược lại.

Hãy đọc kỹ các dòng in log Console để trực quan hóa sơ đồ mạng.

---

## Chương trình mô phỏng toàn bộ Pipeline hệ thống

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// =====================================================================
// 1. TẦNG MODEL & DTO
// =====================================================================
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}

// =====================================================================
// 2. TẦNG DATA ACCESS LAYER (DAL)
// =====================================================================
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
}

public class SqlUserRepository : IUserRepository
{
    private readonly List<User> _db = [
        new User { Id = 1, Name = "Khánh Vy", Role = "Admin" },
        new User { Id = 2, Name = "Lâm Hoàng", Role = "Member" }
    ];

    public async Task<User> GetByIdAsync(int id)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("[6. DAL / Repository] Đang thực thi truy vấn SQL lấy User ID = " + id);
        await Task.Delay(500); // Giả lập chờ kết nối cơ sở dữ liệu
        
        return _db.FirstOrDefault(u => u.Id == id);
    }
}

// =====================================================================
// 3. TẦNG BUSINESS LOGIC LAYER (BLL / Service)
// =====================================================================
public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResponseDto> GetUserProfileAsync(int userId)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[5. BLL / Service] Tiếp nhận yêu cầu. Kiểm tra quyền truy cập...");
        
        // Gọi Repository lấy dữ liệu thô từ DB
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) return null;

        // Nghiệp vụ: Chuyển đổi Entity sang DTO an toàn
        var dto = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name.ToUpper(), // Nghiệp vụ: Viết hoa tên hiển thị
            Role = user.Role
        };

        return dto;
    }
}

// =====================================================================
// 4. TẦNG PRESENTATION LAYER (Controller)
// =====================================================================
public class UsersController
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    // Giả lập xử lý GET /api/users/{id}
    public async Task<string> GetByIdActionAsync(int idFromRoute)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[4. Controller] Routing khớp! Gọi GetByIdAction với ID = {idFromRoute}");

        var userDto = await _userService.GetUserProfileAsync(idFromRoute);
        
        if (userDto == null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[4. Controller] Không tìm thấy! Đóng gói trả về 404 NotFound.");
            return "HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\nUser not found.";
        }

        // Đóng gói JSON trả về 200 OK
        string jsonResponse = $"{{\n  \"id\": {userDto.Id},\n  \"name\": \"{userDto.Name}\",\n  \"role\": \"{userDto.Role}\"\n}}";
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("[4. Controller] Thành công! Đóng gói dữ liệu JSON trả về 200 OK.");
        
        return "HTTP/1.1 200 OK\r\nContent-Type: application/json\r\n\r\n" + jsonResponse;
    }
}

// =====================================================================
// 5. GIẢ LẬP MIDDLEWARE PIPELINE
// =====================================================================
public class WebServerPipeline
{
    private readonly UsersController _controller;

    public WebServerPipeline(UsersController controller)
    {
        _controller = controller;
    }

    public async Task<string> ReceiveRequestAsync(string method, string path, string token)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"\n[2. Web Server] Nhận gói tin mạng: {method} {path}");
        
        // Middleware 1: Ghi log request
        Console.WriteLine($"[3. Middleware 1 - Logger] [{DateTime.Now}] Request: {method} {path}");

        // Middleware 2: Xác thực bảo mật (Token)
        Console.WriteLine("[3. Middleware 2 - Auth] Đang kiểm tra token bảo mật...");
        if (string.IsNullOrEmpty(token) || token != "secret_token_123")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[3. Middleware 2 - Auth] Xác thực thất bại! Từ chối ngay tại cửa ngõ.");
            return "HTTP/1.1 401 Unauthorized\r\n\r\nChưa xác thực danh tính.";
        }
        Console.WriteLine("[3. Middleware 2 - Auth] Xác thực thành công.");

        // Đi qua Middleware hợp lệ -> Tiến hành Routing chuyển vào Controller
        // Giả lập bóc tách ID = 1 từ đường dẫn "/api/users/1"
        int userId = int.Parse(path.Split('/').Last());

        string response = await _controller.GetByIdActionAsync(userId);
        return response;
    }
}

// =====================================================================
// POINT OF ENTRY (MAIN PROGRAM)
// =====================================================================
class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1. Ráp nối hệ thống DI Container bằng tay
        var repo = new SqlUserRepository();
        var service = new UserService(repo);
        var controller = new UsersController(service);
        var server = new WebServerPipeline(controller);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== KHỞI ĐỘNG MOCK WEB SERVER PIPELINE ===");
        Console.ResetColor();

        // -------------------------------------------------------------
        // TÌNH HUỐNG 1: Client gửi yêu cầu hợp lệ lấy User ID = 1
        // -------------------------------------------------------------
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[1. Client Angular] Gửi GET /api/users/1 kèm Token hợp lệ.");
        Console.ResetColor();
        
        string response1 = await server.ReceiveRequestAsync("GET", "/api/users/1", "secret_token_123");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[1. Client Angular] Nhận phản hồi HTTP Response từ Server:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(response1);
        Console.ResetColor();

        Console.WriteLine("\n=================================================================\n");

        // -------------------------------------------------------------
        // TÌNH HUỐNG 2: Client gửi yêu cầu với Token sai
        // -------------------------------------------------------------
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[1. Client Angular] Gửi GET /api/users/1 với Token sai.");
        Console.ResetColor();

        string response2 = await server.ReceiveRequestAsync("GET", "/api/users/1", "wrong_token");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[1. Client Angular] Nhận phản hồi HTTP Response từ Server:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(response2);
        Console.ResetColor();
    }
}
```
