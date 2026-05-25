# Ví dụ Thực hành Bài 42: Hiện thực hóa Kiến trúc 3 lớp (3-Layer Architecture)

Dưới đây là một ví dụ hoàn chỉnh về việc chia tách một mã nguồn quản lý sản phẩm (Product Management) thành cấu trúc 3 tầng chuẩn.

---

## 1. Tầng Model (Dữ liệu dùng chung)

File: `Models/Product.cs`
Lớp mô tả thực thể dữ liệu, được sử dụng xuyên suốt bởi tất cả các tầng.

```csharp
namespace ProductApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
```

---

## 2. Tầng Data Access Layer (DAL / Repository)

File: `DataAccess/ProductRepository.cs`
Chỉ lo việc đọc/ghi dữ liệu thô vào file dữ liệu JSON (hoặc giả lập danh sách). Hoàn toàn không chứa logic nghiệp vụ.

```csharp
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProductApp.Models;

namespace ProductApp.DataAccess
{
    public class ProductRepository
    {
        private const string FilePath = "products_db.json";

        public List<Product> GetAll()
        {
            if (!File.Exists(FilePath)) return new List<Product>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void SaveAll(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
```

---

## 3. Tầng Business Logic Layer (BLL / Service)

File: `Services/ProductService.cs`
Xử lý các quy tắc nghiệp vụ: kiểm tra tên không được trống, giá không âm, tính toán giá khuyến mãi bán hàng. Không chứa bất kỳ câu lệnh in ra giao diện nào (`Console`).

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using ProductApp.Models;
using ProductApp.DataAccess;

namespace ProductApp.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        // Nhận Repository qua Constructor (DI)
        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAvailableProducts()
        {
            // Nghiệp vụ: Chỉ lấy sản phẩm còn hàng trong kho (QuantityInStock > 0)
            return _repository.GetAll().Where(p => p.QuantityInStock > 0).ToList();
        }

        public void AddNewProduct(Product product)
        {
            // Ràng buộc nghiệp vụ: Tên không trống, giá bán > 0
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Tên sản phẩm không được trống!");

            if (product.BasePrice <= 0)
                throw new ArgumentOutOfRangeException("Giá sản phẩm phải lớn hơn 0!");

            var all = _repository.GetAll();
            
            // Tự sinh ID tăng dần
            product.Id = all.Count > 0 ? all.Max(p => p.Id) + 1 : 1;

            all.Add(product);
            _repository.SaveAll(all); // Gọi tầng DAL lưu trữ
        }
    }
}
```

---

## 4. Tầng Presentation Layer (UI)

File: `UI/ConsoleUI.cs`
Phụ trách tương tác Console: in chữ, đọc bàn phím của người dùng. Không chứa logic kiểm tra validation hay ghi file trực tiếp.

```csharp
using System;
using ProductApp.Models;
using ProductApp.Services;

namespace ProductApp.UI
{
    public class ConsoleUI
    {
        private readonly ProductService _productService;

        // Nhận Service qua Constructor
        public ConsoleUI(ProductService productService)
        {
            _productService = productService;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== QUẢN LÝ CỬA HÀNG ===");
                Console.WriteLine("1. Xem sản phẩm còn hàng");
                Console.WriteLine("2. Thêm sản phẩm mới");
                Console.WriteLine("3. Thoát");
                Console.Write("Chọn tính năng: ");
                string choice = Console.ReadLine();

                if (choice == "1") ShowProducts();
                else if (choice == "2") AddProduct();
                else if (choice == "3") break;
                else Console.WriteLine("Lựa chọn không hợp lệ!");
            }
        }

        private void ShowProducts()
        {
            var list = _productService.GetAvailableProducts();
            Console.WriteLine("\n--- DANH SÁCH SẢN PHẨM CÒN HÀNG ---");
            foreach (var p in list)
            {
                Console.WriteLine($"- ID {p.Id}: {p.Name} | Giá: ${p.BasePrice} | Kho: {p.QuantityInStock}");
            }
        }

        private void AddProduct()
        {
            try
            {
                Console.Write("Nhập tên sản phẩm: ");
                string name = Console.ReadLine();
                Console.Write("Nhập giá: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Nhập số lượng kho: ");
                int qty = int.Parse(Console.ReadLine());

                var newProd = new Product { Name = name, BasePrice = price, QuantityInStock = qty };
                
                // Gọi Service xử lý nghiệp vụ
                _productService.AddNewProduct(newProd);
                Console.WriteLine("Thêm sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                // Bắt lỗi nghiệp vụ trả lên từ tầng Service để hiển thị cho người dùng
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
    }
}
```

---

## 5. Điểm ráp nối (Composition Root)

File: `Program.cs`
Lắp ráp các tầng lại với nhau và chạy chương trình.

```csharp
using ProductApp.DataAccess;
using ProductApp.Services;
using ProductApp.UI;

class Program
{
    static void Main()
    {
        // 1. Khởi tạo tầng Data Access
        var repository = new ProductRepository();

        // 2. Tiêm Data Access vào tầng Service
        var service = new ProductService(repository);

        // 3. Tiêm Service vào tầng UI
        var ui = new ConsoleUI(service);

        // 4. Chạy ứng dụng
        ui.RunMenu();
    }
}
```
