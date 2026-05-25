# Ví dụ Thực hành Bài 37: Extension Methods

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách viết và sử dụng các phương thức mở rộng.

---

## Ví dụ 1: Viết hàm mở rộng định dạng tiền tệ cho kiểu `double`

```csharp
using System;

// 1. Tạo một class static để chứa các hàm mở rộng
public static class NumberExtensions
{
    // 2. Tạo một method static có tham số đầu tiên sử dụng từ khóa 'this'
    public static string ToVnd(this double amount)
    {
        return $"{amount:N0} đ";
    }
}

// Lớp Program kiểm thử
class Program
{
    static void Main()
    {
        double priceOfLaptop = 25000000;
        double priceOfMouse = 450000.5;

        // Gọi hàm mở rộng trực tiếp như thể nó là phương thức của kiểu double
        string formattedLaptop = priceOfLaptop.ToVnd();
        string formattedMouse = priceOfMouse.ToVnd();

        Console.WriteLine($"Giá Laptop: {formattedLaptop}"); // Output: Giá Laptop: 25.000.000 đ
        Console.WriteLine($"Giá Chuột:  {formattedMouse}");  // Output: Giá Chuột:  450.001 đ (tự động làm tròn số chẵn lẻ)
    }
}
```

---

## Ví dụ 2: Hàm mở rộng xác thực chuỗi (String Validation Helper)

```csharp
using System;
using System.Text.RegularExpressions;

public static class StringValidationExtensions
{
    // Kiểm tra định dạng Email hợp lệ
    public static bool IsValidEmail(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(value, pattern);
    }

    // Đếm số lượng từ trong chuỗi
    public static int WordCount(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0;

        // Tách chuỗi theo các khoảng trắng
        string[] words = value.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}

class Program
{
    static void Main()
    {
        string email1 = "test@gmail.com";
        string email2 = "invalid-email-format";

        Console.WriteLine($"'{email1}' có hợp lệ? {email1.IsValidEmail()}"); // Output: True
        Console.WriteLine($"'{email2}' có hợp lệ? {email2.IsValidEmail()}"); // Output: False

        string bio = "Tôi đang học lập trình C# căn bản.";
        Console.WriteLine($"Số từ của bio: {bio.WordCount()}"); // Output: 8
    }
}
```

---

## Ví dụ 3: Viết Extension Method cho interface `IEnumerable<T>`

Tự viết một hàm lọc nâng cao `.GetItemsPerPage()` mở rộng cho mọi bộ sưu tập `IEnumerable<T>`.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    // Hàm phân trang tùy chọn mở rộng cho mọi danh sách IEnumerable<T>
    public static IEnumerable<T> GetItemsPerPage<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}

class Program
{
    static void Main()
    {
        List<string> employees = ["An", "Bình", "Cường", "Dũng", "Giang", "Hải", "Hương"];

        // Gọi hàm phân trang mở rộng tự thiết kế
        var page2 = employees.GetItemsPerPage(pageNumber: 2, pageSize: 3);

        Console.WriteLine("Nhân viên thuộc Trang 2:");
        foreach (var emp in page2)
        {
            Console.WriteLine($"- {emp}");
        }
        // Output:
        // - Dũng
        // - Giang
        // - Hải
    }
}
```

---

## Ví dụ 4: Chứng minh độ ưu tiên của Instance Method đối với Extension Method

```csharp
using System;

public class Sample
{
    // Hàm gốc của Class (Instance Method)
    public void Display()
    {
        Console.WriteLine("Hàm gốc của Class được gọi.");
    }
}

public static class SampleExtensions
{
    // Hàm mở rộng trùng tên với hàm gốc
    public static void Display(this Sample sample)
    {
        Console.WriteLine("Hàm mở rộng (Extension Method) được gọi.");
    }
}

class Program
{
    static void Main()
    {
        Sample s = new Sample();
        
        // Gọi Display()
        s.Display();
        
        // Output:
        // "Hàm gốc của Class được gọi."
        // Giải thích: C# Compiler luôn ưu tiên tìm hàm thành viên thực tế của Class trước. 
        // Chỉ khi không tìm thấy, nó mới quét tìm hàm mở rộng Extension Method.
    }
}
```
