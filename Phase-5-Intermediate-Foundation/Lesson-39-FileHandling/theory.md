# Bài 39: File Handling & JSON (Xử lý Tệp tin và Dữ liệu JSON)

Cho đến thời điểm hiện tại, mọi dữ liệu bạn khai báo trong chương trình (như các biến, các danh sách `List<T>`, `Dictionary`) đều được lưu trữ tạm thời trên **RAM**. Khi bạn tắt chương trình hoặc tắt máy tính, toàn bộ dữ liệu này sẽ biến mất hoàn toàn.

Để xây dựng một ứng dụng thực tế, bạn cần lưu trữ dữ liệu bền vững (Persistence) xuống **ổ đĩa cứng** dưới dạng các tệp tin (Files) để có thể đọc lại chúng vào lần chạy sau. Trong bài này, chúng ta sẽ học cách tương tác với hệ thống tệp tin và lưu trữ dữ liệu phổ biến nhất hiện nay — **JSON**.

---

## 1. Thao tác File & Directory cơ bản

C# cung cấp hai cách tiếp cận để làm việc với tệp tin và thư mục thông qua namespace `System.IO`:

### 1.1. Các lớp tĩnh helper (`File`, `Directory`)
Phù hợp cho các thao tác nhanh, đơn giản, chỉ cần truyền đường dẫn tệp tin làm đối số.
* `File.WriteAllText(path, content)`: Ghi chuỗi văn bản vào file (tự động tạo file nếu chưa có, ghi đè nếu đã có).
* `File.ReadAllText(path)`: Đọc toàn bộ nội dung file thành một chuỗi duy nhất.
* `File.Exists(path)`: Kiểm tra file có tồn tại hay không.
* `Directory.CreateDirectory(path)`: Tạo thư mục mới.
* `Directory.Exists(path)`: Kiểm tra thư mục có tồn tại hay không.

### 1.2. Các lớp hướng đối tượng (`FileInfo`, `DirectoryInfo`)
Phù hợp khi bạn cần truy vấn nhiều thuộc tính của file/thư mục (như dung lượng, ngày tạo, quyền truy cập) hoặc thực hiện nhiều thao tác liên tiếp trên cùng một đối tượng.
```csharp
FileInfo fileInfo = new FileInfo(@"C:\data\report.txt");
if (fileInfo.Exists)
{
    long size = fileInfo.Length; // Lấy kích thước file (bytes)
    DateTime created = fileInfo.CreationTime; // Ngày tạo
}
```

---

## 2. Làm việc với Streams (Luồng dữ liệu)

Khi xử lý tệp tin lớn (ví dụ: file Log hệ thống nặng 2GB), việc gọi `File.ReadAllText()` sẽ cực kỳ nguy hiểm. Nó sẽ cố gắng nạp toàn bộ 2GB dữ liệu từ ổ cứng vào RAM cùng một lúc, dễ dẫn đến lỗi **Out Of Memory Exception** và làm treo máy.

Để giải quyết vấn đề này, chúng ta sử dụng **Streams (Luồng dữ liệu)**. Thay vì tải toàn bộ file, Stream mở một đường ống kết nối và đọc/ghi dữ liệu theo từng khối nhỏ (Chunk-by-chunk) hoặc từng dòng một.

### 2.1. `StreamWriter` & `StreamReader`
* `StreamWriter`: Dùng để ghi dữ liệu vào luồng.
* `StreamReader`: Dùng để đọc dữ liệu từ luồng.

```csharp
// Đọc file từng dòng một để tiết kiệm RAM
using (StreamReader reader = new StreamReader("large_log.txt"))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        // Xử lý từng dòng dữ liệu ở đây, RAM chỉ tốn dung lượng cho 1 dòng duy nhất!
        Console.WriteLine(line);
    }
}
```

### 2.2. Câu lệnh `using` và giải phóng tài nguyên
Tệp tin là một tài nguyên hệ thống (System Resource) được quản lý bởi Hệ điều hành. Khi chương trình của bạn mở một file, hệ điều hành sẽ khóa file đó lại. Nếu chương trình quên không đóng file (`.Close()`), các phần mềm khác (hoặc chính chương trình của bạn ở luồng sau) sẽ không thể truy cập được file đó nữa.

C# cung cấp cú pháp **`using` statement** (dựa trên interface `IDisposable` đã học ở Bài 23) để đảm bảo file sẽ luôn được đóng và giải phóng tài nguyên tự động, ngay cả khi có lỗi xảy ra ở giữa quá trình xử lý.

Có hai cách viết `using`:
```csharp
// Cách 1: Sử dụng khối lệnh ngoặc nhọn (Classic)
using (var writer = new StreamWriter("file.txt"))
{
    writer.WriteLine("Hello");
} // writer tự động giải phóng ở dấu ngoặc nhọn đóng

// Cách 2: Sử dụng using declaration (C# 8.0+) - Gọn hơn
using var writer = new StreamWriter("file.txt");
writer.WriteLine("Hello");
// writer tự động giải phóng khi chạy hết phương thức chứa nó
```

---

## 3. Lưu trữ dữ liệu với định dạng JSON

**JSON (JavaScript Object Notation)** là định dạng trao đổi dữ liệu nhẹ, dễ đọc viết đối với con người và dễ phân tích đối với máy tính. Đây là định dạng chuẩn mực trong lập trình Web/API.

Trong .NET, thư viện hiệu năng cao tích hợp sẵn là **`System.Text.Json`**.

### 3.1. Serialization (Tuần tự hóa)
Là quá trình chuyển đổi một Object trong bộ nhớ C# thành một chuỗi JSON (dạng string) để lưu xuống file hoặc gửi qua mạng.
```csharp
var student = new Student { Name = "Vy", Age = 18 };
string jsonString = JsonSerializer.Serialize(student);
// Kết quả: {"Name":"Vy","Age":18}
```

*Muốn in chuỗi JSON đẹp đẽ thụt lề (Pretty Print) để dễ đọc:*
```csharp
var options = new JsonSerializerOptions { WriteIndented = true };
string prettyJson = JsonSerializer.Serialize(student, options);
```

### 3.2. Deserialization (Giải tuần tự hóa)
Là quá trình đọc chuỗi JSON từ file và chuyển ngược lại thành Object C# có kiểu dữ liệu rõ ràng.
```csharp
string jsonString = "{\"Name\":\"Vy\",\"Age\":18}";
Student student = JsonSerializer.Deserialize<Student>(jsonString);
Console.WriteLine(student.Name); // Output: Vy
```

---

## 4. So sánh với JavaScript

Vì bạn có nền tảng JS/Node.js, hãy đối chiếu để ghi nhớ nhanh hơn:

| Thao tác | Trong Node.js (JavaScript) | Trong C# (.NET) |
|---|---|---|
| Đọc toàn bộ file | `fs.readFileSync(path, 'utf8')` | `File.ReadAllText(path)` |
| Ghi toàn bộ file | `fs.writeFileSync(path, text)` | `File.WriteAllText(path, text)` |
| Chuyển Object -> String | `JSON.stringify(obj)` | `JsonSerializer.Serialize(obj)` |
| Chuyển String -> Object | `JSON.parse(jsonString)` | `JsonSerializer.Deserialize<T>(jsonString)` |

> [!NOTE]
> **Điểm khác biệt:**
> Trong JS, `JSON.parse()` trả về kiểu `any` linh hoạt không định hình.
> Trong C#, `JsonSerializer.Deserialize<T>()` bắt buộc bạn phải truyền kiểu Generic `<T>` để đảm bảo sau khi chuyển đổi, đối tượng nhận được sẽ có kiểu dữ liệu an toàn (Type-safe).

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Quên giải phóng Stream
Mở tệp tin bằng `new StreamReader()` nhưng không bọc trong khối `using`. Hậu quả: File bị khóa vô thời hạn, chương trình phát sinh lỗi "File is being used by another process" vào các lần gọi sau.
*Khắc phục:* Luôn sử dụng `using` khi khởi tạo các lớp Stream hoặc Connection.

### Sai lầm 2: Thuộc tính Class không có `{ get; set; }` khi dùng JSON
Thư viện `System.Text.Json` mặc định chỉ Serialize/Deserialize các thuộc tính **Public** và phải có đủ phương thức `get` và `set`.
```csharp
// SAI: Không thể Serialize trường private hoặc field không có property
public class User { public string name; } 

// ĐÚNG:
public class User { public string Name { get; set; } }
```

---

## 6. Checklist đánh giá hiểu bài

1. RAM và Ổ đĩa cứng khác nhau thế nào về khả năng lưu trữ dữ liệu?
2. Tại sao gọi `File.ReadAllText` trên file dung lượng cực lớn lại có thể gây crash ứng dụng? Phương án thay thế là gì?
3. Từ khóa `using` trong xử lý file có nhiệm vụ gì? Hoạt động dựa trên interface nào đã học?
4. Khái niệm Serialization và Deserialization là gì?
5. Tại sao các trường cần Serialize trong C# class bắt buộc phải viết dạng Property có `get; set;`?
