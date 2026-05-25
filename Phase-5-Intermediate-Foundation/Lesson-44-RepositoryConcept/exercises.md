# Bài tập Thực hành Bài 44: Repository Pattern

Thực hành các bài tập dưới đây bằng cách thiết kế interface và viết các class Repository cụ thể trên IDE của bạn.

---

## Bài tập 1: Khai báo Generic Repository Interface
**Đề bài:**
Viết mã khai báo một Interface Generic có tên `IRepository<T>` (sử dụng Generic type parameter) có 5 phương thức CRUD cơ bản sau:
1. `T GetById(int id);`
2. `IEnumerable<T> GetAll();`
3. `void Add(T entity);`
4. `void Update(T entity);`
5. `void Delete(int id);`

* **Yêu cầu:** Thêm ràng buộc constraint để đảm bảo kiểu `T` bắt buộc phải là một class (`where T : class`).

---

## Bài tập 2: Hiện thực hóa Generic Repository lưu trên RAM
**Đề bài:**
1. Hãy viết một class `InMemoryRepository<T>` triển khai interface generic `IRepository<T>` được định nghĩa ở Bài tập 1.
2. Sử dụng một `List<T>` lưu trong bộ nhớ RAM làm nguồn dữ liệu.
3. Class `T` cần có thuộc tính ID để tìm kiếm (Gợi ý: bạn có thể thiết kế một interface `IEntity` có thuộc tính `int Id { get; set; }` và đặt ràng buộc `where T : class, IEntity` để code repository có thể truy cập trường Id).

---

## Bài tập 3: Cập nhật thông tin trong File Repository
**Đề bài:**
Cho interface `IProductRepository` có hàm `Update(Product product)`.
Hãy mô tả chi tiết các bước xử lý (bằng mã giả hoặc tiếng Việt) khi bạn thực hiện cập nhật giá của một sản phẩm trong tệp tin `products.json` mà vẫn đảm bảo hiệu năng file (không ghi đè mù quáng nếu sản phẩm không thay đổi).

---

## Bài tập 4: Thêm phương thức lọc chuyên biệt cho Repository
**Đề bài:**
Đôi khi interface Generic `IRepository<T>` không đủ để dùng. Ví dụ, đối với `IUserRepository`, bạn cần một phương thức tìm kiếm rất đặc thù: `User GetByEmail(string email)`.
* **Bài tập:** Hãy viết code khai báo interface `IUserRepository` kế thừa từ `IRepository<User>` và bổ sung phương thức `GetByEmail` chuyên biệt nói trên.

---

## Bài tập 5: Ôn tập tổng hợp Phase 5
**Đề bài:**
Hãy trả lời các câu hỏi sau để tổng hợp kiến thức toàn bộ Phase 5:
1. Tại sao nói `using` statement và `IDisposable` là điều kiện bắt buộc khi sử dụng Streams để đọc/ghi tệp tin?
2. Khi viết một Web API với ASP.NET Core, kết nối Database (DbContext) nên được đăng ký dưới dạng nào (Transient, Scoped, hay Singleton)? Tại sao?
3. Tại sao trong mô hình 3-Layer, tầng UI không được phép biết đến sự tồn tại của tầng Data Access?
