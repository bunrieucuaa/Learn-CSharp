# Bài 44: Repository Pattern (Mẫu thiết kế kho lưu trữ dữ liệu)

Trong bài học cuối cùng của **Phase 5**, chúng ta sẽ nghiên cứu một trong những mẫu thiết kế phổ biến và quan trọng nhất trong phát triển ứng dụng doanh nghiệp: **Repository Pattern (Mẫu thiết kế kho dữ liệu)**.

Mẫu thiết kế này đóng vai trò là chiếc cầu nối giữa tầng Nghiệp vụ (Service) và cơ sở dữ liệu thực tế (Database/File), giúp cô lập hoàn toàn logic nghiệp vụ khỏi các công nghệ lưu trữ dữ liệu cụ thể.

---

## 1. Khái niệm Repository Pattern

### 1.1. Vấn đề khi không dùng Repository
Nếu không sử dụng Repository, tầng Service của bạn sẽ phải viết trực tiếp các câu lệnh truy vấn SQL hoặc các hàm đọc/ghi tệp tin JSON cụ thể:
```csharp
public class StudentService
{
    public void RegisterStudent(Student student)
    {
        // Viết code ghi file JSON trực tiếp ở Service!
        string json = File.ReadAllText("students.json");
        // ...
        File.WriteAllText("students.json", newJson);
    }
}
```
*Nhược điểm:*
* Nếu ngày mai công ty quyết định chuyển đổi từ lưu file JSON sang lưu vào SQL Server Database, bạn sẽ phải vào sửa lại code của toàn bộ các Service trong hệ thống.
* Việc viết code kiểm thử (Unit Test) cho Service trở nên bất khả thi vì nó luôn cố gắng tìm và đọc ghi file thật trên đĩa cứng.

### 1.2. Định nghĩa Repository Pattern
**Repository Pattern** tạo ra một lớp trung gian nằm giữa Service và Data Source. Lớp này cung cấp các phương thức truy xuất dữ liệu giống như một bộ sưu tập (Collection) các đối tượng trong bộ nhớ RAM (như mảng hoặc `List`).

Tầng Service sẽ chỉ tương tác với **Interface đại diện cho Repository**:
```csharp
public interface IStudentRepository
{
    Student GetById(int id);
    List<Student> GetAll();
    void Add(Student student);
    void Update(Student student);
    void Delete(int id);
}
```
Tầng Service hoàn toàn không cần biết dữ liệu được lưu ở đâu và lưu như thế nào. Việc dữ liệu nằm ở SQL, File JSON, hay trên RAM là do Class cụ thể triển khai Interface đó quyết định!

---

## 2. Ưu và nhược điểm của Repository Pattern

### 2.1. Ưu điểm
* **Ghép lỏng hoàn toàn (Decoupling):** Có thể dễ dàng thay đổi công nghệ cơ sở dữ liệu (ví dụ: từ File JSON sang Entity Framework Core / SQL Server) bằng cách tạo class Repository mới hiện thực hóa interface cũ và thay đổi đăng ký trong file cấu hình DI Container. Code của tầng Service và UI giữ nguyên 100%!
* **Dễ dàng kiểm thử (Testability):** Có thể tiêm một `MockRepository` giả lập lưu dữ liệu trên RAM để test nhanh các logic nghiệp vụ của Service mà không cần cài đặt database hay tạo file thật.
* **Tập trung hóa logic truy cập dữ liệu:** Tránh lặp lại mã nguồn đọc/ghi file hoặc câu lệnh SQL ở nhiều nơi (vi phạm DRY).

### 2.2. Nhược điểm
* **Tạo thêm nhiều file code trung gian:** Bạn phải viết thêm Interface và Class tương ứng cho mỗi thực thể dữ liệu.
* **Cực kỳ phức tạp nếu lạm dụng:** Nếu ứng dụng của bạn cực kỳ nhỏ (chỉ có vài chức năng đọc ghi file đơn giản), việc dựng phân tầng và viết Repository có thể là quá đà (Overengineering).

---

## 3. Generic Repository Pattern

Để tránh việc lặp đi lặp lại các phương thức CRUD cơ bản (`Add`, `GetById`, `Delete`...) cho từng thực thể (ví dụ: `IStudentRepository`, `IProductRepository`, `IOrderRepository`), người ta thường thiết kế một **Generic Interface (`IRepository<T>`)** sử dụng Generic đã học ở Bài 28:

```csharp
public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Delete(int id);
}
```
*Sau đó, các Repository chuyên biệt chỉ cần kế thừa lại interface generic này để tái sử dụng toàn bộ code.*

---

## 🏆 TỔNG KẾT PHASE 5 (INTERMEDIATE FOUNDATION)

Chúc mừng bạn đã hoàn thành Phase 5! Đây là chặng đường quan trọng chuẩn bị cho bạn bước vào lập trình web thực thụ:
* **Bài 39 (File Handling):** Biết cách lưu dữ liệu lâu dài vào ổ cứng bằng tệp tin text và JSON Serialization/Deserialization.
* **Bài 40 (Async/Await):** Nắm vững tư duy lập trình bất đồng bộ, giải phóng thread để tăng hiệu năng máy chủ.
* **Bài 41 (Dependency Injection):** Hiểu cách cấu hình, đăng ký dịch vụ (Transient, Scoped, Singleton) để xây dựng ứng dụng ghép lỏng.
* **Bài 42 (Layer Architecture):** Biết cách tổ chức mã nguồn khoa học theo mô hình 3-Layer.
* **Bài 43 (Service Pattern):** Biết cách cô lập và thiết kế các dịch vụ xử lý quy tắc nghiệp vụ độc lập.
* **Bài 44 (Repository Pattern):** Làm chủ kỹ thuật tách biệt hoàn toàn truy cập dữ liệu khỏi nghiệp vụ.

---

## 4. Checklist đánh giá hiểu bài

1. Repository Pattern giải quyết vấn đề gì trong lập trình phần mềm?
2. Hãy giải thích tại sao khi thay đổi công nghệ lưu trữ (từ File sang SQL Server), code của tầng Service không cần thay đổi nếu áp dụng Repository Pattern?
3. Generic Repository là gì? Nó giúp giải quyết vi phạm nguyên lý DRY như thế nào?
4. Kể tên 6 bài học chính bạn đã hoàn thành trong Phase 5 và vai trò của mỗi bài đối với lập trình Web API.
