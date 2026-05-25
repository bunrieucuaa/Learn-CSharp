# Ví dụ Thực hành Bài 48: JSON Configuration in Web APIs

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức cấu hình nâng cao cho `JsonSerializer` trong .NET.

---

## Ví dụ 1: Cấu hình xuất JSON chuẩn camelCase và ẩn thuộc tính Null

Ví dụ xuất bản thông tin sản phẩm chuẩn camelCase dành cho ứng dụng Angular, tự động ẩn trường `Description` nếu nó bị `null`.

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } // Trường này có thể bị null
}

class Program
{
    static void Main()
    {
        Product product1 = new Product 
        { 
            ProductId = 101, 
            Name = "Bàn phím cơ", 
            Price = 79.9m, 
            Description = "Bàn phím cơ switch Red" 
        };

        Product product2 = new Product 
        { 
            ProductId = 102, 
            Name = "Chuột gaming", 
            Price = 35.0m, 
            Description = null // Mô tả bị null
        };

        // Cấu hình JsonSerializerOptions nâng cao
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, // In thụt lề đẹp mắt
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // PascalCase -> camelCase
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Bỏ qua thuộc tính bị null
        };

        Console.WriteLine("--- PRODUCT 1 JSON OUTPUT (Có mô tả) ---");
        string json1 = JsonSerializer.Serialize(product1, options);
        Console.WriteLine(json1);

        Console.WriteLine("\n--- PRODUCT 2 JSON OUTPUT (Mô tả bị null - tự động ẩn) ---");
        string json2 = JsonSerializer.Serialize(product2, options);
        Console.WriteLine(json2);
    }
}
```

---

## Ví dụ 2: Giải tuần tự hóa JSON camelCase sang Object PascalCase C#

```csharp
using System;
using System.Text.Json;

public class UserProfile
{
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        // Chuỗi JSON dạng camelCase (thường được Angular gửi lên)
        string incomingJson = "{\"username\":\"vy123\",\"emailAddress\":\"vy@gmail.com\",\"age\":18}";

        // Nếu dùng mặc định (không cấu hình), C# sẽ không map được và các thuộc tính sẽ bị null/0!
        UserProfile defaultResult = JsonSerializer.Deserialize<UserProfile>(incomingJson);
        Console.WriteLine("--- SỬ DỤNG MẶC ĐỊNH (KHÔNG CẤU HÌNH) ---");
        Console.WriteLine($"Username:      {defaultResult.Username ?? "NULL"}");
        Console.WriteLine($"EmailAddress:  {defaultResult.EmailAddress ?? "NULL"}");
        Console.WriteLine($"Age:           {defaultResult.Age}");

        // Cấu hình so khớp camelCase
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        UserProfile configuredResult = JsonSerializer.Deserialize<UserProfile>(incomingJson, options);
        Console.WriteLine("\n--- CÓ CẤU HÌNH PropertyNamingPolicy = CamelCase ---");
        Console.WriteLine($"Username:      {configuredResult.Username}");
        Console.WriteLine($"EmailAddress:  {configuredResult.EmailAddress}");
        Console.WriteLine($"Age:           {configuredResult.Age}");
    }
}
```

---

## Ví dụ 3: Sử dụng `[JsonPropertyName]` cho các định dạng tùy biến

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization; // Thư viện chứa Attribute

public class PartnerCustomer
{
    [JsonPropertyName("client_id")] // Ánh xạ snake_case của đối tác vào C#
    public int CustomerId { get; set; }

    [JsonPropertyName("fullName")]  // Ánh xạ camelCase
    public string Name { get; set; }

    [JsonPropertyName("phone_no")]  // Ánh xạ viết tắt
    public string PhoneNumber { get; set; }
}

class Program
{
    static void Main()
    {
        // Chuỗi JSON thô từ hệ thống đối tác gửi sang
        string rawPartnerJson = "{\"client_id\": 777, \"fullName\": \"Lâm Hoàng\", \"phone_no\": \"0909123456\"}";

        // Deserialization bình thường, C# tự động map nhờ các Attribute
        PartnerCustomer customer = JsonSerializer.Deserialize<PartnerCustomer>(rawPartnerJson);

        Console.WriteLine("Dữ liệu đối tác nạp thành công vào class C#:");
        Console.WriteLine($"- ID khách hàng: {customer.CustomerId}");
        Console.WriteLine($"- Tên khách hàng: {customer.Name}");
        Console.WriteLine($"- Số điện thoại:  {customer.PhoneNumber}");
    }
}
```
