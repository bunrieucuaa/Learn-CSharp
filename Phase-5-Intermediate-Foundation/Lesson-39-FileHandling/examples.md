# Ví dụ Thực hành Bài 39: File Handling & JSON

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức đọc/ghi tệp tin và chuyển đổi định dạng dữ liệu JSON.

---

## Ví dụ 1: Đọc và ghi tệp tin cơ bản bằng lớp tĩnh `File`

Thao tác nhanh ghi đè và đọc toàn bộ nội dung một tệp tin văn bản đơn giản.

```csharp
using System;
using System.IO;

string filePath = "demo.txt";
string contentToWrite = "Chào mừng bạn đến với Phase 5!\nHọc C# thật thú vị.";

// 1. Ghi toàn bộ nội dung vào file (Tự tạo file nếu chưa có, ghi đè nếu đã có)
File.WriteAllText(filePath, contentToWrite);
Console.WriteLine("Đã ghi file thành công.");

// 2. Kiểm tra sự tồn tại của file trước khi đọc để tránh lỗi sập app
if (File.Exists(filePath))
{
    // Đọc toàn bộ nội dung file thành một chuỗi duy nhất
    string readContent = File.ReadAllText(filePath);
    Console.WriteLine("\nNội dung file đọc được:");
    Console.WriteLine(readContent);
}
else
{
    Console.WriteLine("File không tồn tại!");
}
```

---

## Ví dụ 2: Sử dụng `StreamWriter` và `StreamReader` giải phóng tự động

Ví dụ mô phỏng việc ghi nhật ký log hoạt động và đọc từng dòng file để tối ưu RAM.

```csharp
using System;
using System.IO;

string logFile = "app.log";

// 1. Sử dụng StreamWriter để ghi thêm dữ liệu vào cuối file (Append mode: true)
// Cú pháp 'using var' giúp giải phóng file ngay khi kết thúc phương thức
using (StreamWriter writer = new StreamWriter(logFile, append: true))
{
    writer.WriteLine($"[{DateTime.Now}] - Hệ thống khởi động.");
    writer.WriteLine($"[{DateTime.Now}] - Kết nối Database thành công.");
} // writer.Dispose() được gọi tự động ở đây, file được đóng.

Console.WriteLine("Đã ghi thêm log thành công.");

// 2. Sử dụng StreamReader để đọc file từng dòng một
if (File.Exists(logFile))
{
    Console.WriteLine("\nNội dung log hệ thống:");
    using StreamReader reader = new StreamReader(logFile);
    string line;
    
    // Đọc liên tục cho đến hết tệp tin (ReadLine trả về null khi hết file)
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine($"-> Dòng log: {line}");
    }
}
```

---

## Ví dụ 3: Tuần tự hóa đối tượng thành JSON và lưu xuống file (Serialization)

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json; // Namespace chứa thư viện JSON

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Skills { get; set; }
}

class Program
{
    static void Main()
    {
        List<Student> students = [
            new Student { Name = "Vy", Age = 18, Skills = ["HTML", "C#"] },
            new Student { Name = "Lâm", Age = 20, Skills = ["SQL", "C#", "CSS"] }
        ];

        // Thiết lập tùy chọn định dạng JSON in thụt lề cho đẹp
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };

        // Chuyển đối tượng sang chuỗi JSON string
        string jsonString = JsonSerializer.Serialize(students, options);
        
        Console.WriteLine("Chuỗi JSON thu được:");
        Console.WriteLine(jsonString);

        // Lưu chuỗi JSON này xuống file
        File.WriteAllText("students.json", jsonString);
        Console.WriteLine("\nĐã lưu danh sách học sinh vào file students.json.");
    }
}
```

---

## Ví dụ 4: Đọc file JSON và giải tuần tự hóa thành Object C# (Deserialization)

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Skills { get; set; }
}

class Program
{
    static void Main()
    {
        string filePath = "students.json";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Hãy chạy Ví dụ 3 để tạo file JSON trước!");
            return;
        }

        // 1. Đọc nội dung chuỗi JSON từ file
        string jsonString = File.ReadAllText(filePath);

        // 2. Giải tuần tự hóa chuỗi JSON về lại List<Student> có kiểu dữ liệu an toàn
        // Cần truyền đúng kiểu dữ liệu đích vào dấu ngoặc nhọn <T>
        List<Student> loadedStudents = JsonSerializer.Deserialize<List<Student>>(jsonString);

        Console.WriteLine("Dữ liệu học sinh tải lên từ file:");
        foreach (var s in loadedStudents)
        {
            Console.WriteLine($"- Tên: {s.Name} | Tuổi: {s.Age} | Kỹ năng: {string.Join(", ", s.Skills)}");
        }
    }
}
```
