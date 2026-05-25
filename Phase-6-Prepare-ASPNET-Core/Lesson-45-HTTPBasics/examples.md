# Ví dụ Thực hành Bài 45: HTTP Basics

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức thực hiện các cuộc gọi HTTP Request bằng lớp `HttpClient` của C#.

---

## Ví dụ 1: Gửi HTTP GET Request và kiểm tra Status Code

Ví dụ gọi API thật từ mạng Internet lấy thông tin của một Todo item và in thông tin trạng thái trả về.

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Khởi tạo client dùng using để giải phóng tài nguyên mạng
        using HttpClient client = new HttpClient();

        string url = "https://jsonplaceholder.typicode.com/todos/1";

        Console.WriteLine($"Đang gửi GET Request tới: {url}");
        
        // 1. Gửi request lấy phản hồi HTTP Response
        HttpResponseMessage response = await client.GetAsync(url);

        // 2. Đọc mã trạng thái HTTP Status Code
        HttpStatusCode statusCode = response.StatusCode;
        int statusCodeInt = (int)statusCode;

        Console.WriteLine($"Mã trạng thái trả về: {statusCodeInt} {statusCode}");

        // 3. Kiểm tra xem request có thành công hay không (Status code dạng 2xx)
        if (response.IsSuccessStatusCode)
        {
            // Đọc nội dung Body của phản hồi dưới dạng chuỗi văn bản
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nDữ liệu Body nhận được:");
            Console.WriteLine(responseBody);
        }
        else if (statusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("Lỗi: Không tìm thấy tài nguyên yêu cầu (404 Not Found)!");
        }
        else
        {
            Console.WriteLine("Yêu cầu thất bại!");
        }
    }
}
```

---

## Ví dụ 2: Gửi HTTP POST Request kèm Body JSON

Ví dụ mô phỏng việc tạo mới một bài viết (Post) lên hệ thống API ngầm.

```csharp
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();
        string url = "https://jsonplaceholder.typicode.com/posts";

        // 1. Tạo đối tượng dữ liệu cần gửi
        var newPost = new
        {
            title = "Học C# Web API",
            body = "Nội dung bài viết về HTTP Basics.",
            userId = 1
        };

        // 2. Tuần tự hóa sang chuỗi JSON
        string jsonPayload = JsonSerializer.Serialize(newPost);

        // 3. Đóng gói dữ liệu vào StringContent, chỉ rõ Content-Type là application/json
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        Console.WriteLine($"Đang gửi POST Request tạo bài viết mới tới: {url}...");
        
        // 4. Thực hiện gửi POST Request
        HttpResponseMessage response = await client.PostAsync(url, content);

        Console.WriteLine($"Mã trạng thái phản hồi: {(int)response.StatusCode} {response.StatusCode}");

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nBài viết đã được tạo thành công trên Server mock!");
            Console.WriteLine(result); // Server trả về object kèm theo ID mới được sinh (ID: 101)
        }
    }
}
```

---

## Ví dụ 3: Giả lập trả về các HTTP Status Codes tương ứng với nghiệp vụ

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Mô phỏng phản hồi Status Code từ Server:");
        
        // Giả lập 3 tình huống gọi API
        SimulateApiResponse("admin@site.com", "/admin-dashboard"); // Thành công
        SimulateApiResponse("member@site.com", "/admin-dashboard"); // Bị chặn (403)
        SimulateApiResponse("guest@site.com", "/api/not-found");   // Sai URL (404)
        SimulateApiResponse("", "/api/profile");                  // Chưa đăng nhập (401)
    }

    static void SimulateApiResponse(string email, string path)
    {
        Console.WriteLine($"\nRequest: path='{path}', user='{email}'");

        // 1. Kiểm tra đăng nhập
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Response: 401 Unauthorized (Bạn cần đăng nhập để truy cập).");
            return;
        }

        // 2. Kiểm tra tài nguyên tồn tại
        if (path == "/api/not-found")
        {
            Console.WriteLine("Response: 404 Not Found (Đường dẫn không tồn tại).");
            return;
        }

        // 3. Kiểm tra phân quyền truy cập
        if (path == "/admin-dashboard" && email != "admin@site.com")
        {
            Console.WriteLine("Response: 403 Forbidden (Bạn không có quyền truy cập trang Admin!).");
            return;
        }

        // 4. Hợp lệ thành công
        Console.WriteLine("Response: 200 OK (Thành công!)");
    }
}
```
