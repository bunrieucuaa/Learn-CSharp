# Ví dụ Thực hành Bài 33: Filtering

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức hoạt động của các toán tử lọc và phân trang dữ liệu.

---

## Ví dụ 1: So sánh `Where` và `OfType<T>`

Minh họa cách lấy ra các kiểu dữ liệu cụ thể từ danh sách các đối tượng kế thừa hoặc mảng hỗn hợp.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Tạo cấu trúc kế thừa đơn giản
public abstract class Animal { public string Name { get; set; } }
public class Dog : Animal { public void Bark() => Console.WriteLine($"{Name} sủa: Gâu gâu!"); }
public class Cat : Animal { public void Meow() => Console.WriteLine($"{Name} kêu: Meo meo!"); }

List<Animal> zoo = [
    new Dog { Name = "Cậu Vàng" },
    new Cat { Name = "Mimi" },
    new Dog { Name = "Milu" },
    new Cat { Name = "Kitty" }
];

Console.WriteLine("--- Lọc lấy toàn bộ các con Chó bằng OfType<Dog> ---");
IEnumerable<Dog> dogs = zoo.OfType<Dog>();
foreach (var dog in dogs)
{
    dog.Bark();
}

Console.WriteLine("\n--- Kết hợp lọc Where với con Mèo có tên dài hơn 3 ký tự ---");
var specialCats = zoo.OfType<Cat>().Where(c => c.Name.Length > 3);
foreach (var cat in specialCats)
{
    cat.Meow();
}
```

---

## Ví dụ 2: Phân trang dữ liệu (Pagination)

Sử dụng `Skip` và `Take` để chia nhỏ danh sách hiển thị.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<string> products = [
    "IPhone 15", "Samsung S24", "MacBook Pro", "Dell XPS", "Sony Headphone",
    "iPad Pro", "Apple Watch", "Logitech Mouse", "Razer Keyboard", "Asus Monitor"
];

int pageSize = 3; // Mỗi trang có tối đa 3 sản phẩm

// Viết hàm phụ trợ in dữ liệu trang bất kỳ
void PrintPage(int pageNumber)
{
    Console.WriteLine($"\n--- TRANG {pageNumber} ---");
    var pageData = products
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

    foreach (var item in pageData)
    {
        Console.WriteLine($"- {item}");
    }
}

// Chạy thử phân trang
PrintPage(1); // Lấy phần tử 0, 1, 2
PrintPage(2); // Lấy phần tử 3, 4, 5
PrintPage(3); // Lấy phần tử 6, 7, 8
PrintPage(4); // Trang cuối cùng chỉ còn 1 phần tử (phần tử số 9)
```

---

## Ví dụ 3: So sánh an toàn giữa các hàm Element Operators

Ví dụ chỉ ra hành vi xảy ra lỗi của `First()` / `Single()` và cách xử lý an toàn bằng `OrDefault`.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = [10, 20, 30, 20];

// 1. Tìm số > 15
int firstOver15 = numbers.First(n => n > 15);
Console.WriteLine($"Số đầu tiên > 15: {firstOver15}"); // Output: 20 (bỏ qua 30 và 20 phía sau)

// 2. Tìm số > 100 bằng FirstOrDefault
int? firstOver100 = numbers.FirstOrDefault(n => n > 100);
Console.WriteLine($"Số > 100: {firstOver100}"); // Output: 0 (giá trị mặc định của kiểu int)

// 3. Sử dụng Single()
try
{
    // Lỗi vì có tới 2 số 20 trong danh sách
    int single20 = numbers.Single(n => n == 20); 
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Lỗi Single: {ex.Message}"); // Output: Sequence contains more than one matching element
}

// 4. Tìm kiếm duy nhất an toàn
int single30 = numbers.SingleOrDefault(n => n == 30);
Console.WriteLine($"Số duy nhất bằng 30: {single30}"); // Output: 30
```

---

## Ví dụ 4: Distinct trên Reference Type (Class Objects)

Chỉ ra sự khác biệt khi dùng `Distinct` trên Object thường và Object đã implement `IEquatable<T>`.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Class Student có ghi đè so sánh bằng IEquatable
public class Student : IEquatable<Student>
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Implement IEquatable<Student> để Distinct biết cách so sánh các thuộc tính
    public bool Equals(Student? other)
    {
        if (other is null) return false;
        return this.Id == other.Id && this.Name == other.Name;
    }

    // Luôn ghi đè GetHashCode khi đã ghi đè Equals
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}

List<Student> classroom = [
    new Student { Id = 1, Name = "An" },
    new Student { Id = 2, Name = "Bình" },
    new Student { Id = 1, Name = "An" }, // Trùng lặp nội dung
];

// Tiến hành Distinct
var uniqueStudents = classroom.Distinct();

Console.WriteLine("Danh sách học sinh không trùng lặp:");
foreach (var s in uniqueStudents)
{
    Console.WriteLine($"- ID {s.Id}: {s.Name}");
}
// Output:
// - ID 1: An
// - ID 2: Bình
// (Phần tử trùng lặp thứ 3 đã bị lọc bỏ thành công!)
```
