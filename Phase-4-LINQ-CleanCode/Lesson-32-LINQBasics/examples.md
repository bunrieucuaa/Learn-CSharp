# Ví dụ Thực hành Bài 32: LINQ Basics

Dưới đây là các ví dụ minh họa đầy đủ, chạy được bằng .NET 9 sử dụng cú pháp **Top-level statements** và **Collection expressions** (`[...]`). Hãy đọc kỹ comment giải thích từng dòng code.

---

## Ví dụ 1: Loop truyền thống vs LINQ (Query & Method Syntax)

Ví dụ này so sánh cách lọc các số chẵn lớn hơn 5 từ một danh sách số nguyên bằng 3 cách khác nhau.

```csharp
using System;
using System.Collections.Generic;
using System.Linq; // BẮT BUỘC phải import namespace này để dùng LINQ

// 1. Khởi tạo danh sách bằng Collection expression [...] (.NET 8+)
List<int> numbers = [1, 2, 3, 4, 6, 8, 9, 10];

Console.WriteLine("--- CÁCH 1: DÙNG VÒNG LẶP FOREACH TRUYỀN THỐNG ---");
List<int> evenNumbersLoop = new List<int>();
foreach (var n in numbers)
{
    if (n > 5 && n % 2 == 0)
    {
        evenNumbersLoop.Add(n);
    }
}
// In kết quả
Console.WriteLine(string.Join(", ", evenNumbersLoop)); // Output: 6, 8, 10


Console.WriteLine("\n--- CÁCH 2: DÙNG LINQ METHOD SYNTAX (KHUYÊN DÙNG) ---");
// Trả về IEnumerable<int>, lọc trực tiếp bằng Where()
var evenNumbersMethod = numbers.Where(n => n > 5 && n % 2 == 0);
Console.WriteLine(string.Join(", ", evenNumbersMethod)); // Output: 6, 8, 10


Console.WriteLine("\n--- CÁCH 3: DÙNG LINQ QUERY SYNTAX ---");
// Trông giống cú pháp SQL
var evenNumbersQuery = from n in numbers
                        where n > 5 && n % 2 == 0
                        select n;
Console.WriteLine(string.Join(", ", evenNumbersQuery)); // Output: 6, 8, 10
```

---

## Ví dụ 2: Sử dụng Lambda Expression với Objects nâng cao

Lọc danh sách sản phẩm có giá lớn hơn 100 và còn hàng.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Định nghĩa class Product đơn giản
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsInStock { get; set; }
}

// Khởi tạo danh sách sản phẩm
List<Product> products = [
    new Product { Name = "Laptop Dell", Price = 1200, IsInStock = true },
    new Product { Name = "Mouse Logitech", Price = 25, IsInStock = true },
    new Product { Name = "Keyboard Razer", Price = 150, IsInStock = false },
    new Product { Name = "Monitor ASUS", Price = 300, IsInStock = true },
];

// Viết LINQ lọc sản phẩm thoả mãn điều kiện
var filteredProducts = products.Where(p => p.Price > 100 && p.IsInStock);

Console.WriteLine("Sản phẩm giá > 100 và còn hàng:");
foreach (var p in filteredProducts)
{
    Console.WriteLine($"- {p.Name} (${p.Price})");
}
// Output:
// - Laptop Dell ($1200)
// - Monitor ASUS ($300)
```

---

## Ví dụ 3: Minh hoạ Deferred Execution (Trì hoãn thực thi)

Ví dụ này chỉ ra cách LINQ lưu câu query dưới dạng "kế hoạch" và trì hoãn tính toán cho đến khi dữ liệu thực sự được duyệt qua.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<string> names = ["An", "Bình", "Cường"];

Console.WriteLine("1. Bắt đầu khai báo LINQ query...");
// Query tìm các tên bắt đầu bằng ký tự 'A' hoặc 'B'
var query = names.Where(name => 
{
    // Log này sẽ chạy để chứng minh hàm lọc được gọi lúc nào
    Console.WriteLine($"-> Đang lọc phần tử: {name}");
    return name.StartsWith('A') || name.StartsWith('B');
});

Console.WriteLine("2. Đã khai báo xong query. Lúc này chưa có gì chạy cả!");
Console.WriteLine("---------------------------------------------");

Console.WriteLine("3. Thêm một phần tử mới vào danh sách gốc...");
names.Add("Bảo"); // Thêm "Bảo" vào mảng nguồn sau khi đã định nghĩa câu query

Console.WriteLine("4. Bắt đầu dùng foreach để duyệt qua kết quả...");
foreach (var n in query)
{
    Console.WriteLine($"Kết quả tìm được: {n}");
}

// Giải thích Output:
// Nhờ Deferred Execution, khi foreach chạy thì danh sách gốc đã có thêm "Bảo". 
// Nên "Bảo" vẫn được quét qua và lọc thành công!
```

---

## Ví dụ 4: Eager Execution với ToList() và ToArray()

Chuyển câu query thành danh sách thực tế lưu trữ trong bộ nhớ để bảo toàn dữ liệu tại thời điểm truy vấn.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<string> names = ["An", "Bình", "Cường"];

Console.WriteLine("1. Khai báo query và gọi ToList() để ép chạy NGAY LẬP TỨC (Eager)...");
// Gọi ToList() ở cuối câu lệnh
var staticList = names.Where(name => name.StartsWith('A') || name.StartsWith('B')).ToList();

Console.WriteLine("2. Thêm một phần tử mới 'Bảo' vào danh sách gốc...");
names.Add("Bảo");

Console.WriteLine("3. Duyệt qua danh sách đã lọc:");
foreach (var n in staticList)
{
    Console.WriteLine($"Kết quả: {n}");
}

// Giải thích: 
// Vì ToList() đã chạy ở bước 1, nên staticList đã lưu trữ cứng kết quả của ["An", "Bình"] vào RAM.
// Phần tử "Bảo" thêm vào sau đó ở danh sách gốc sẽ KHÔNG ảnh hưởng hay được hiển thị ở staticList.
```
