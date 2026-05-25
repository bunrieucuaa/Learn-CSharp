# Ví dụ Thực hành Bài 36: Grouping & Joins

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức gom nhóm và kết nối dữ liệu.

---

## Ví dụ 1: Gom nhóm sản phẩm bằng `GroupBy`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

List<Product> products = [
    new Product { Name = "IPhone 15", Category = "Mobile", Price = 1000 },
    new Product { Name = "MacBook Pro", Category = "Laptop", Price = 2000 },
    new Product { Name = "Samsung S24", Category = "Mobile", Price = 900 },
    new Product { Name = "Dell XPS", Category = "Laptop", Price = 1500 },
    new Product { Name = "Sony Headphone", Category = "Accessory", Price = 200 }
];

// Nhóm sản phẩm theo Category
var groupedProducts = products.GroupBy(p => p.Category);

Console.WriteLine("--- KẾT QUẢ GOM NHÓM ---");
foreach (IGrouping<string, Product> group in groupedProducts)
{
    // group.Key chính là giá trị Category đại diện cho nhóm
    Console.WriteLine($"Danh mục: {group.Key.ToUpper()}");
    
    // Bản thân biến group là một danh sách các Product trong nhóm đó
    foreach (var p in group)
    {
        Console.WriteLine($"  + {p.Name} (${p.Price})");
    }
}
```

---

## Ví dụ 2: Thống kê báo cáo doanh số với Aggregations

Tính toán tổng tiền, trung bình giá, số lượng sản phẩm trong từng nhóm danh mục.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<Product> products = [
    new Product { Name = "IPhone 15", Category = "Mobile", Price = 1000 },
    new Product { Name = "MacBook Pro", Category = "Laptop", Price = 2000 },
    new Product { Name = "Samsung S24", Category = "Mobile", Price = 900 },
    new Product { Name = "Dell XPS", Category = "Laptop", Price = 1500 },
    new Product { Name = "Sony Headphone", Category = "Accessory", Price = 200 }
];

// Nhóm và tính toán thống kê ngay
var categoryReports = products
                      .GroupBy(p => p.Category)
                      .Select(g => new
                      {
                          CategoryName = g.Key,
                          Count = g.Count(),
                          TotalValue = g.Sum(p => p.Price),
                          AveragePrice = g.Average(p => p.Price),
                          MaxPrice = g.Max(p => p.Price)
                      });

Console.WriteLine("Báo cáo thống kê theo ngành hàng:");
foreach (var report in categoryReports)
{
    Console.WriteLine($"- Ngành hàng: {report.CategoryName}");
    Console.WriteLine($"  + Số lượng sản phẩm: {report.Count}");
    Console.WriteLine($"  + Tổng giá trị tồn: ${report.TotalValue}");
    Console.WriteLine($"  + Giá trung bình:    ${report.AveragePrice:F1}");
    Console.WriteLine($"  + Sản phẩm đắt nhất: ${report.MaxPrice}");
    Console.WriteLine();
}
```

---

## Ví dụ 3: Kết hợp dữ liệu bằng `Join` (Inner Join)

Kết hợp danh sách Học sinh và danh sách Lớp học dựa trên `ClassId`.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class ClassRoom
{
    public int Id { get; set; }
    public string ClassName { get; set; }
}

public class Student
{
    public string Name { get; set; }
    public int ClassId { get; set; }
}

List<ClassRoom> classes = [
    new ClassRoom { Id = 1, ClassName = "Lớp C# Cơ Bản" },
    new ClassRoom { Id = 2, ClassName = "Lớp Web Frontend" },
    new ClassRoom { Id = 3, ClassName = "Lớp Cloud DevOps" } // Lớp này chưa có học sinh nào
];

List<Student> students = [
    new Student { Name = "Lâm", ClassId = 1 },
    new Student { Name = "Vy", ClassId = 2 },
    new Student { Name = "Hải", ClassId = 1 },
    new Student { Name = "Hương", ClassId = 99 } // ClassId 99 không tồn tại
];

// Tiến hành Inner Join
var innerJoinQuery = students.Join(
    classes,
    student => student.ClassId,   // Khóa liên kết của students
    classRoom => classRoom.Id,    // Khóa liên kết của classes
    (student, classRoom) => new
    {
        StudentName = student.Name,
        ClassName = classRoom.ClassName
    }
);

Console.WriteLine("Kết quả ghép nối Inner Join:");
foreach (var item in innerJoinQuery)
{
    Console.WriteLine($"- Học sinh {item.StudentName} học tại {item.ClassName}");
}
// Chú ý: "Hương" (ClassId 99) và Lớp "Cloud DevOps" (Id 3) bị biến mất vì không khớp khóa liên kết.
```

---

## Ví dụ 4: Ghép nối phân cấp với `GroupJoin` (Left Outer Join)

Giữ lại toàn bộ lớp học, kể cả lớp không có học viên nào.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

var leftOuterJoinQuery = classes.GroupJoin(
    students,
    classRoom => classRoom.Id,    // Khóa liên kết của classes
    student => student.ClassId,   // Khóa liên kết của students
    (classRoom, studentGroup) => new
    {
        ClassName = classRoom.ClassName,
        StudentCount = studentGroup.Count(),
        StudentNames = studentGroup.Select(s => s.Name)
    }
);

Console.WriteLine("Kết quả ghép nối GroupJoin (Left Join):");
foreach (var item in leftOuterJoinQuery)
{
    Console.WriteLine($"- Lớp: {item.ClassName} ({item.StudentCount} học viên)");
    if (item.StudentCount > 0)
    {
        Console.WriteLine($"  + Học viên: {string.Join(", ", item.StudentNames)}");
    }
    else
    {
        Console.WriteLine("  + (Trống)");
    }
}
// Chú ý: Lớp "Cloud DevOps" vẫn được giữ lại trên danh sách hiển thị với số học viên là 0.
```
