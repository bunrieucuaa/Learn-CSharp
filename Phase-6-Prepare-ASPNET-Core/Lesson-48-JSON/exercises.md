# Bài tập Thực hành Bài 48: JSON Configuration in Web APIs

Hoàn thành các bài tập cấu hình JSON dưới đây trên dự án test cá nhân của bạn.

---

## Bài tập 1: Phân tích nguyên nhân lỗi map dữ liệu
**Tình huống:**
Bạn có class C# và đoạn code parse JSON sau:
```csharp
public class Car
{
    public string Brand { get; set; }
    public string ModelName { get; set; }
}

string json = "{\"brand\":\"Toyota\",\"modelname\":\"Camry\"}";
Car car = JsonSerializer.Deserialize<Car>(json);
Console.WriteLine(car.ModelName); // In ra chuỗi rỗng / null!
```
* **Bài tập:** Giải thích tại sao thuộc tính `Brand` có thể map được (hoặc không) và tại sao `ModelName` chắc chắn bị `null`? Hãy bổ sung cấu hình `JsonSerializerOptions` để sửa lỗi này.

---

## Bài tập 2: Cấu hình Serialize camelCase cho Danh sách học sinh
**Đề bài:**
Cho class `Student`:
```csharp
public class Student
{
    public string StudentId { get; set; }
    public string FullName { get; set; }
    public double? CurrentGpa { get; set; } // Nullable double
}
```
Hãy viết chương trình C# khởi tạo một danh sách `List<Student>` chứa 2 học sinh, trong đó có một học sinh có `CurrentGpa = null`. 
Cấu hình xuất danh sách này sang chuỗi JSON sao cho:
1. Thuộc tính hiển thị dạng camelCase.
2. Ẩn thuộc tính `currentGpa` ở học sinh bị null.

* **Expected Output JSON:**
  ```json
  [
    {
      "studentId": "S01",
      "fullName": "An Nguyễn",
      "currentGpa": 8.2
    },
    {
      "studentId": "S02",
      "fullName": "Bình Trần"
    }
  ]
  ```

---

## Bài tập 3: Ánh xạ cấu hình API thời tiết (API Mapping)
**Đề bài:**
Bạn đang gọi một API thời tiết bên thứ ba, API trả về JSON cấu trúc sau:
`{"temp_c": 28.5, "humidity_pct": 70, "city_name": "Ha Noi"}`
Hãy viết class `WeatherReport` C# sử dụng các Attribute `[JsonPropertyName]` phù hợp để map trực tiếp dữ liệu trên vào các thuộc tính:
- `Temperature` (kiểu double)
- `Humidity` (kiểu int)
- `City` (kiểu string)

---

## Bài tập 4: So sánh JSON trong C# và JavaScript
**Đề bài:**
Đọc lại phần lý thuyết và trả lời các câu hỏi:
1. Tại sao C# cần truyền kiểu dữ liệu generic (ví dụ: `<UserProfile>`) khi deserialize JSON, còn JavaScript chỉ cần gọi `JSON.parse(string)` là xong?
2. Sự khác biệt này thể hiện đặc tính gì của hai ngôn ngữ?

---

## Bài tập 5: Cấu hình không phân biệt chữ hoa chữ thường (Case Insensitive)
**Đề bài:**
Client gửi lên một chuỗi JSON có cách viết hoa thường lộn xộn do lỗi lập trình:
`{"uSeRnAmE": "admin", "PASSWORD": "123"}`
Hãy viết code C# cấu hình `JsonSerializerOptions` để giải tuần tự hóa chuỗi trên vào class `LoginRequest` chứa 2 thuộc tính `Username` và `Password` một cách trơn tru.
