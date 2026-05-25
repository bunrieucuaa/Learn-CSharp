# Bài tập Thực hành Bài 42: Layer Architecture Basics

Hãy phân tích lỗi kiến trúc và giải quyết các bài tập dưới đây dựa trên mô hình 3-Layer.

---

## Bài tập 1: Phát hiện lỗi vi phạm phân tầng (Tình huống 1)
**Tình huống:**
Trong một dự án, bạn phát hiện dòng code sau đây nằm trong class `LogRepository` thuộc tầng **Data Access Layer**:
```csharp
public void SaveLog(string logMsg)
{
    // Ghi log vào file
    File.AppendAllText("system.log", logMsg);
    
    // In ra màn hình Console báo thành công
    Console.WriteLine("Đã ghi nhận log thành công vào tệp system.log!"); 
}
```
* **Bài tập:** Đoạn code trên có vi phạm phân tầng không? Tại sao? Làm thế nào để sửa lại cho đúng chuẩn Clean Code?

---

## Bài tập 2: Phát hiện lỗi vi phạm phân tầng (Tình huống 2)
**Tình huống:**
Học viên cũ viết dòng code này trong file UI `ConsoleMenu.cs` để tìm kiếm thông tin nhanh:
```csharp
private void SearchUser()
{
    Console.Write("Nhập ID: ");
    int id = int.Parse(Console.ReadLine());

    // UI gọi trực tiếp file JSON thông qua thư viện để hiển thị kết quả
    string json = File.ReadAllText("users.json");
    var users = JsonSerializer.Deserialize<List<User>>(json);
    var user = users.FirstOrDefault(u => u.Id == id);
    
    Console.WriteLine($"Tên người dùng: {user.Name}");
}
```
* **Bài tập:** Chỉ ra 2 lỗi vi phạm phân tầng nghiêm trọng trong đoạn code trên và vẽ lại sơ đồ gọi hàm đúng chuẩn 3-Layer.

---

## Bài tập 3: Thiết kế cấu trúc thư mục dự án Task Manager
**Đề bài:**
Bạn được giao xây dựng ứng dụng Quản lý công việc (Task Manager) gồm các chức năng: Thêm Task, Đánh dấu hoàn thành Task, Lưu Task xuống file txt.
* **Bài tập:** Hãy liệt kê tên các Class bạn sẽ tạo và phân bổ chúng vào đúng 3 thư mục: `DataAccess`, `Services`, `UI`.

---

## Bài tập 4: Tách logic nghiệp vụ ra khỏi UI
**Đề bài:**
Cho đoạn code UI xử lý đặt mua khóa học:
```csharp
public void BuyCourseUI()
{
    Console.Write("Nhập số tiền bạn có: ");
    decimal balance = decimal.Parse(Console.ReadLine());
    
    // Logic kiểm tra giá khóa học nằm ở UI!
    if (balance < 200)
    {
        Console.WriteLine("Lỗi: Tài khoản của bạn không đủ tiền mua khóa học ($200)!");
        return;
    }
    
    // Gọi tiếp code lưu DB ở đây...
}
```
* **Bài tập:** Hãy viết lại logic trên theo hướng tách biệt:
  1. Tầng Service sẽ có hàm kiểm tra điều kiện tài khoản và ném ra Exception nếu thiếu tiền.
  2. Tầng UI chỉ lo việc nhận dữ liệu từ người dùng, gọi Service, bắt Exception và hiển thị thông báo lỗi lên màn hình Console.

---

## Bài tập 5: Thiết kế sơ đồ luồng dữ liệu khi tạo tài khoản
**Đề bài:**
Vẽ sơ đồ dạng chữ (Text diagram) mô tả chi tiết đường đi của dữ liệu từ lúc người dùng gõ tên đăng ký tài khoản trên Console cho đến khi dữ liệu được ghi vào file `users.json` trên đĩa cứng, đi qua các tầng và các hàm tương ứng của mô hình 3-Layer.
* **Gợi ý format sơ đồ:** `UI (Program.cs) -> [Hàm A] -> Service (UserService.cs) -> [Hàm B] -> ...`
