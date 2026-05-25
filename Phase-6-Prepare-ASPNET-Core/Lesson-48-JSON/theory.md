# Bài 48: JSON in Web APIs (Dữ liệu JSON trong thế giới Web)

Trong lập trình web hiện đại, đặc biệt là khi kết hợp **ASP.NET Core (Backend)** và **Angular (Frontend)**, hầu hết các cuộc gọi API đều sử dụng **JSON** làm định dạng truyền dữ liệu.

Tuy nhiên, có một rào cản vô hình về quy chuẩn đặt tên giữa C# và JavaScript/TypeScript. Nếu bạn không biết cách cấu hình đồng bộ, hệ thống sẽ gặp lỗi không khớp dữ liệu và trả về các thuộc tính bị `null`. Bài học này sẽ giúp bạn làm chủ kỹ thuật cấu hình JSON nâng cao để tích hợp mượt mà giữa Backend và Frontend.

---

## 1. Vấn đề lệch chuẩn đặt tên (Naming Conventions Conflict)

* **Trong C# (.NET):** Quy chuẩn đặt tên Property bắt buộc là **PascalCase** (viết hoa chữ cái đầu tiên).
  * *Ví dụ:* `public string FirstName { get; set; }`
* **Trong JavaScript / TypeScript (Angular):** Quy chuẩn đặt tên bắt buộc là **camelCase** (viết thường chữ cái đầu tiên).
  * *Ví dụ:* `const firstName = 'Vy';`

### Vấn đề:
Khi Frontend gửi gói tin JSON: `{"firstName": "Vy"}` lên Server C#.
Theo mặc định, thư viện `System.Text.Json` của C# so sánh **phân biệt chữ hoa chữ thường (Case-sensitive)**. Nó sẽ quét tìm thuộc tính `firstName` trong Class C# nhưng không thấy (vì C# đặt tên là `FirstName` viết hoa). Kết quả là thuộc tính `FirstName` trên C# nhận giá trị `null` hoặc rỗng!

---

## 2. Cấu hình JSON nâng cao bằng `JsonSerializerOptions`

Để giải quyết vấn đề trên, .NET cung cấp class **`JsonSerializerOptions`** cho phép chúng ta tùy biến cơ chế đọc/ghi JSON.

### 2.1. Tự động chuyển đổi PascalCase <-> camelCase
Sử dụng thuộc tính `PropertyNamingPolicy`:
```csharp
var options = new JsonSerializerOptions
{
    // Tự động chuyển các property PascalCase của C# thành camelCase khi xuất ra JSON
    // Và tự động nhận diện camelCase từ client gửi lên map vào PascalCase của C#
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string json = JsonSerializer.Serialize(myObject, options);
```

### 2.2. So sánh không phân biệt chữ hoa chữ thường (Case-insensitive)
Nếu API đối tác gửi dữ liệu lộn xộn (lúc viết hoa, lúc viết thường), hãy cấu hình `PropertyNameCaseInsensitive = true`:
```csharp
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
// Lúc này, {"firstname"}, {"FirstName"}, {"firstName"} đều map thành công vào property FirstName!
```

### 2.3. Bỏ qua các thuộc tính có giá trị Null (Ignore Null Values)
Khi truyền dữ liệu qua mạng, để tiết kiệm băng thông, ta nên loại bỏ các trường không có dữ liệu (`null`) khỏi chuỗi JSON.
```csharp
var options = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};
```
*Nếu `Age` bị null, chuỗi JSON xuất ra sẽ không xuất hiện thuộc tính `"Age": null` nữa.*

---

## 3. Ánh xạ thủ công bằng `[JsonPropertyName]` Attribute

Trong trường hợp bạn bắt buộc phải giao tiếp với một hệ thống API cũ có cách đặt tên thuộc tính kỳ lạ (ví dụ: dùng snake_case dạng `first_name` hoặc viết tắt `f_name`), bạn có thể sử dụng thuộc tính **`[JsonPropertyName]`** đặt ngay trên đầu property của C# để chỉ định thủ công:

```csharp
using System.Text.Json.Serialization;

public class Student
{
    // Ánh xạ trường "first_name" trong JSON vào thuộc tính FirstName của C#
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("stu_age")]
    public int Age { get; set; }
}
```

---

## 4. So sánh với JavaScript

* Trong JavaScript, bạn chỉ cần gọi `JSON.stringify(obj)` và `JSON.parse(str)` vì bản thân JS là ngôn ngữ dynamic type, không quan tâm đến kiểu dữ liệu tĩnh.
* Trong C# Web API, quá trình này được tự động hóa bởi framework ASP.NET Core. Khi một request gửi đến Controller, ASP.NET Core tự động sử dụng `System.Text.Json` ngầm định cấu hình sẵn **camelCase** để map dữ liệu vào đối tượng C#, giúp bạn không cần phải viết code cấu hình thủ công cho từng hàm.

---

## 5. Checklist đánh giá hiểu bài

1. Tại sao sự khác nhau về quy chuẩn đặt tên (PascalCase vs camelCase) giữa C# và JS lại có thể gây lỗi dữ liệu khi truyền nhận API?
2. Thuộc tính `PropertyNamingPolicy = JsonNamingPolicy.CamelCase` có vai trò gì?
3. Khi nào bạn nên sử dụng thuộc tính `[JsonPropertyName("tên_tùy_biến")]`?
4. Làm cách nào để cấu hình loại bỏ toàn bộ các trường giá trị `null` khi xuất dữ liệu ra JSON string?
