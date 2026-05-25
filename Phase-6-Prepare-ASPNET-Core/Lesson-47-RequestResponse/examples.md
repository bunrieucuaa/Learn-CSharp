# Ví dụ Thực hành Bài 47: HTTP Request & Response

Dưới đây là ví dụ C# chạy được bằng .NET 9 thực hiện bóc tách và phân tích các thành phần vật lý của một gói tin HTTP Request thô.

---

## Ví dụ 1: Bóc tách gói tin HTTP Request thô (Raw HTTP Request Parser)

Ví dụ này mô phỏng công việc của một Web Server ở tầng mạng thấp: Tiếp nhận chuỗi text thô gửi đến và phân tích thành các thuộc tính có cấu trúc.

```csharp
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // Chuỗi văn bản thô mô phỏng một gói tin HTTP POST gửi từ Client
        string rawRequest = 
            "POST /api/products/105?discount=true HTTP/1.1\r\n" +
            "Host: shop.com\r\n" +
            "Content-Type: application/json\r\n" +
            "Authorization: Bearer my_secret_token\r\n" +
            "\r\n" + // Dòng trống phân cách Headers và Body
            "{\"Name\":\"Running Shoes\",\"Price\":99.5}";

        Console.WriteLine("--- RAW HTTP REQUEST RECEIVED ---");
        Console.WriteLine(rawRequest);
        Console.WriteLine("---------------------------------\n");

        // 1. Phân tích dòng đầu tiên (Request Line)
        using (StringReader reader = new StringReader(rawRequest))
        {
            string requestLine = reader.ReadLine();
            string[] requestLineParts = requestLine.Split(' ');
            string method = requestLineParts[0];
            string fullUrl = requestLineParts[1];

            Console.WriteLine("=== BÓC TÁCH REQUEST LINE ===");
            Console.WriteLine($"- Method: {method}");
            Console.WriteLine($"- Full Path: {fullUrl}");

            // Phân tách Route Parameter và Query Parameter từ URL
            string pathOnly = fullUrl;
            string queryString = string.Empty;
            if (fullUrl.Contains('?'))
            {
                string[] urlParts = fullUrl.Split('?');
                pathOnly = urlParts[0];
                queryString = urlParts[1];
            }

            Console.WriteLine($"- Route Path: {pathOnly}");
            if (!string.IsNullOrEmpty(queryString))
            {
                Console.WriteLine($"- Query Parameters String: {queryString}");
            }

            // 2. Phân tích các dòng Headers (Đọc cho đến khi gặp dòng trống)
            Console.WriteLine("\n=== BÓC TÁCH HEADERS ===");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string headerLine;
            while (!string.IsNullOrEmpty(headerLine = reader.ReadLine()))
            {
                int colonIndex = headerLine.IndexOf(':');
                if (colonIndex > 0)
                {
                    string key = headerLine.Substring(0, colonIndex).Trim();
                    string value = headerLine.Substring(colonIndex + 1).Trim();
                    headers[key] = value;
                    Console.WriteLine($"  + Header [{key}] = {value}");
                }
            }

            // 3. Phần còn lại chính là Body
            Console.WriteLine("\n=== BÓC TÁCH REQUEST BODY ===");
            string body = reader.ReadToEnd();
            Console.WriteLine($"- Body Payload: {body}");
        }
    }
}
```

---

## Ví dụ 2: Phân tích cú pháp Query String (Query String Parser)

Hàm tiện ích tách nhỏ chuỗi query `?page=2&size=10` thành một `Dictionary<string, string>` để lập trình viên dễ tra cứu giá trị.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string queryString = "category=shoes&priceMin=50&priceMax=150&inStock=true";
        
        // Chuyển đổi thành Dictionary
        Dictionary<string, string> queryParams = ParseQueryString(queryString);

        Console.WriteLine($"Phân tích Query String: ?{queryString}\n");
        Console.WriteLine($"Tìm kiếm theo Category:  {queryParams.GetValueOrDefault("category")}");
        Console.WriteLine($"Giá tối thiểu (priceMin): {queryParams.GetValueOrDefault("priceMin")} USD");
        Console.WriteLine($"Chỉ lấy hàng có sẵn?      {queryParams.GetValueOrDefault("inStock")}");
    }

    static Dictionary<string, string> ParseQueryString(string query)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(query)) return result;

        // Tách các cặp key-value bằng dấu &
        string[] pairs = query.Split('&');
        foreach (var pair in pairs)
        {
            string[] kv = pair.Split('=');
            if (kv.Length == 2)
            {
                result[kv[0]] = Uri.UnescapeDataString(kv[1]); // Giải mã ký tự đặc biệt URL (UrlDecode)
            }
        }
        return result;
    }
}
```
