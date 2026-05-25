# Thử thách Lớn Tổng hợp Phase 5: Xây dựng Hệ thống Quản lý Sách Phân tầng hoàn chỉnh (Library Management System)

## Ngữ cảnh
Bạn đã đi đến cuối chặng đường **Phase 5**. Để chứng minh bản thân đã hoàn toàn làm chủ các kiến thức lập trình C# nâng cao, bạn được yêu cầu xây dựng một ứng dụng Console Quản lý Kho sách của thư viện trường học. 

Ứng dụng này bắt buộc phải áp dụng **tất cả các kiến thức đã học trong Phase 5**:
1. **Kiến trúc 3-Layer:** Chia tách mã nguồn rõ ràng thành các thư mục/lớp: Models, Repositories, Services, UI.
2. **Repository Pattern:** Truy xuất dữ liệu thông qua Interface `IBookRepository`.
3. **Service Pattern:** Xử lý logic nghiệp vụ và ném ra các Business Exception qua `BookService`.
4. **File Handling & JSON:** Lưu trữ dữ liệu bền vững xuống tệp `books_db.json`.
5. **Async / Await:** Toàn bộ các thao tác đọc ghi file, xử lý nghiệp vụ của Service phải được viết bất đồng bộ (trả về kiểu `Task` hoặc `Task<T>`).
6. **Dependency Injection:** Sử dụng `Microsoft.Extensions.DependencyInjection` để lắp ráp hệ thống trong `Program.cs`.

---

## Cấu trúc dữ liệu Sách (Book Model)
```csharp
public class Book
{
    public string Isbn { get; set; }        // Mã vạch sách (Key duy nhất)
    public string Title { get; set; }
    public string Author { get; set; }
    public int Quantity { get; set; }       // Số lượng sách trong kho
    public string Category { get; set; }    // "IT", "Novel", "Science"
}
```

---

## Giao diện các tầng bắt buộc thiết kế

### 1. Tầng Data Access Layer (Repositories)
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBookRepository
{
    Task<Book> GetByIsbnAsync(string isbn);
    Task<List<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(string isbn);
}
```
*Yêu cầu hiện thực:* Viết class `FileBookRepository` triển khai interface này. Sử dụng `File.WriteAllTextAsync` và `File.ReadAllTextAsync` kết hợp `JsonSerializer` để đọc ghi file `books_db.json` bất đồng bộ.

### 2. Tầng Business Logic Layer (Services)
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBookService
{
    Task<List<Book>> GetBooksByCategoryAsync(string category);
    Task AddBookToInventoryAsync(Book book);
    Task RemoveBookFromInventoryAsync(string isbn);
}
```
*Yêu cầu hiện thực:* Viết class `BookService` nhận `IBookRepository` qua Constructor.
* **Nghiệp vụ thêm sách:** 
  - ISBN không được trống, tiêu đề không được trống.
  - Số lượng sách ban đầu nhập kho phải lớn hơn 0.
  - Kiểm tra xem sách đã tồn tại trong kho chưa (trùng ISBN). Nếu có rồi, cộng dồn số lượng `Quantity` mới vào số lượng cũ (gọi hàm `UpdateAsync`). Nếu chưa có, thêm mới hoàn toàn (gọi hàm `AddAsync`).
* **Nghiệp vụ xóa sách:**
  - Kiểm tra sách có tồn tại không. Nếu không, ném ra ngoại lệ `BookNotFoundException`.
  - Nếu số lượng > 1: Giảm số lượng đi 1 đơn vị.
  - Nếu số lượng = 1: Xóa hoàn toàn sản phẩm khỏi file.

### 3. Tầng Presentation Layer (UI & Program)
* Viết class `ConsoleUI` chứa menu:
  1. Hiển thị toàn bộ sách trong kho.
  2. Thêm sách mới (hoặc nhập thêm số lượng).
  3. Xóa/Giảm số lượng sách theo mã ISBN.
* File `Program.cs` thiết lập DI:
  - Đăng ký `IBookRepository` là `FileBookRepository`.
  - Đăng ký `IBookService` là `BookService`.
  - Đăng ký `ConsoleUI`.
  - Build `ServiceProvider` và khởi chạy `ConsoleUI.RunAsync()`.

Hãy hoàn thành thử thách lớn này để có được một sản phẩm phần mềm mẫu mực, đặt nền tảng kiến trúc vững chắc phục vụ cho lập trình Web API ở Phase tiếp theo!
