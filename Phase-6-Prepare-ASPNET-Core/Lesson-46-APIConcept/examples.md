# Ví dụ Thực hành Bài 46: RESTful API Design

Dưới đây là các ví dụ C# mô phỏng thiết kế hệ thống định tuyến (Routing) theo kiến trúc RESTful API.

---

## Ví dụ 1: Mô phỏng hệ thống định tuyến REST API Controller

Ví dụ này mô phỏng cách một Web Server tiếp nhận HTTP Method và phân phối đến các hàm CRUD tương ứng cho tài nguyên **Sản phẩm (Products)**.

```csharp
using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ProductRestController
{
    private readonly List<Product> _db = [
        new Product { Id = 1, Name = "Bàn làm việc" },
        new Product { Id = 2, Name = "Ghế văn phòng" }
    ];

    // GET /api/products
    public void GetProducts()
    {
        Console.WriteLine("-> [200 OK] Đang trả về danh sách toàn bộ sản phẩm.");
        foreach (var p in _db) Console.WriteLine($"   + ID {p.Id}: {p.Name}");
    }

    // GET /api/products/{id}
    public void GetProductById(int id)
    {
        var prod = _db.Find(p => p.Id == id);
        if (prod != null)
        {
            Console.WriteLine($"-> [200 OK] Tìm thấy sản phẩm: ID {prod.Id} - {prod.Name}");
        }
        else
        {
            Console.WriteLine($"-> [404 Not Found] Không tìm thấy sản phẩm có ID = {id}!");
        }
    }

    // POST /api/products
    public void CreateProduct(string name)
    {
        int newId = _db.Count + 1;
        _db.Add(new Product { Id = newId, Name = name });
        Console.WriteLine($"-> [201 Created] Tạo thành công sản phẩm mới '{name}' với ID = {newId}.");
    }

    // DELETE /api/products/{id}
    public void DeleteProduct(int id)
    {
        var prod = _db.Find(p => p.Id == id);
        if (prod != null)
        {
            _db.Remove(prod);
            Console.WriteLine($"-> [200 OK] Đã xóa sản phẩm ID = {id}.");
        }
        else
        {
            Console.WriteLine($"-> [404 Not Found] Lỗi: Không thể xóa sản phẩm không tồn tại!");
        }
    }
}

class Program
{
    static void Main()
    {
        ProductRestController controller = new ProductRestController();

        Console.WriteLine("--- SIMULATE REST API ROUTING CALLS ---");

        // 1. Gọi GET /api/products
        Console.WriteLine("\n[Client gửi: GET /api/products]");
        controller.GetProducts();

        // 2. Gọi POST /api/products
        Console.WriteLine("\n[Client gửi: POST /api/products (Body: 'Đèn để bàn')]");
        controller.CreateProduct("Đèn để bàn");

        // 3. Gọi GET /api/products/3
        Console.WriteLine("\n[Client gửi: GET /api/products/3]");
        controller.GetProductById(3);

        // 4. Gọi DELETE /api/products/1
        Console.WriteLine("\n[Client gửi: DELETE /api/products/1]");
        controller.DeleteProduct(1);

        // 5. Gọi GET /api/products/1 (Kiểm chứng đã xóa)
        Console.WriteLine("\n[Client gửi: GET /api/products/1]");
        controller.GetProductById(1);
    }
}
```

---

## Ví dụ 2: Định tuyến tài nguyên con phụ thuộc (Sub-resource Routing)

Trong REST, nếu một tài nguyên chỉ tồn tại phụ thuộc vào tài nguyên khác (ví dụ: các Đánh giá `Reviews` của một Cuốn sách `Books`), ta dùng URL phân cấp lồng nhau.

* URL lấy toàn bộ review của cuốn sách ID 5: **`GET /api/books/5/reviews`**
* URL thêm review cho cuốn sách ID 5: **`POST /api/books/5/reviews`**

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Mô phỏng Sub-resource Routing:");
        
        // Giả lập cuộc gọi
        RouteHandler("GET", "/api/books/5/reviews");
        RouteHandler("POST", "/api/books/5/reviews");
        RouteHandler("GET", "/api/books/10");
    }

    static void RouteHandler(string method, string url)
    {
        Console.WriteLine($"\nRequest: {method} {url}");

        // Phân tích URL thô
        string[] segments = url.Split('/', StringSplitOptions.RemoveEmptyEntries);

        // Kiểm tra định dạng lồng: api / books / {bookId} / reviews
        if (segments.Length == 4 && segments[0] == "api" && segments[1] == "books" && segments[3] == "reviews")
        {
            string bookId = segments[2];
            if (method == "GET")
            {
                Console.WriteLine($"-> Xử lý: Lấy toàn bộ danh sách Review của Cuốn sách ID = {bookId}.");
            }
            else if (method == "POST")
            {
                Console.WriteLine($"-> Xử lý: Thêm mới 1 Review mới cho Cuốn sách ID = {bookId}.");
            }
        }
        else if (segments.Length == 3 && segments[0] == "api" && segments[1] == "books")
        {
            string bookId = segments[2];
            Console.WriteLine($"-> Xử lý: Lấy chi tiết thông tin cuốn sách ID = {bookId}.");
        }
        else
        {
            Console.WriteLine("-> Lỗi: 404 Route không hợp lệ.");
        }
    }
}
```
