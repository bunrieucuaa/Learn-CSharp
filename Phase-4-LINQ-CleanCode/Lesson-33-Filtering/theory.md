# Bài 33: Filtering (Lọc dữ liệu trong LINQ)

Trong bài trước, chúng ta đã nắm được khái niệm cơ bản về LINQ và Lambda expression. Bài này sẽ đi sâu vào các toán tử lọc dữ liệu (Filtering Operators) trong LINQ. Đây là nhóm phương thức được sử dụng nhiều nhất để loại bỏ dữ liệu rác và trích xuất thông tin cần thiết.

---

## 1. Các toán tử lọc cơ bản

### 1.1. `Where` (Lọc theo điều kiện logic)
Đây là phương thức phổ biến nhất, tương đương với hàm `.filter()` trong JavaScript.
* **Cú pháp:** `collection.Where(element => condition)`
* **Hoạt động:** Duyệt qua từng phần tử và giữ lại những phần tử mà biểu thức lambda trả về `true`.

### 1.2. `OfType<T>` (Lọc theo kiểu dữ liệu)
Được dùng khi danh sách của bạn chứa nhiều kiểu dữ liệu hỗn hợp (đa hình) và bạn chỉ muốn lấy ra những đối tượng thuộc một class/kiểu cụ thể.
* **Ví dụ:** Lọc ra tất cả các đối tượng kiểu `Dog` từ danh sách `List<Animal>`.
* **Cú pháp:** `animals.OfType<Dog>()`
* **Điểm hay:** Khác với ép kiểu thủ công, `OfType<T>` sẽ tự động bỏ qua các phần tử không đúng kiểu mà không ném ra Exception.

### 1.3. `Distinct` (Lọc trùng lặp)
Loại bỏ các phần tử trùng lặp trong collection.
* **Với Value Type (int, string, double...):** Hoạt động hoàn hảo ngay lập tức.
* **Với Reference Type (Class Objects):** C# mặc định so sánh địa chỉ ô nhớ (Reference). Nếu bạn có 2 object `new Student { Id = 1 }` nằm ở 2 ô nhớ khác nhau, `Distinct` sẽ không loại bỏ được. Để sửa lỗi này, Object đó cần implement interface `IEquatable<T>` hoặc viết một class custom `IEqualityComparer<T>`.

---

## 2. Phân trang dữ liệu với `Skip` & `Take`

Khi xây dựng các ứng dụng thực tế (như trang Web bán hàng), bạn không bao giờ hiển thị cả 1,000,000 sản phẩm cùng lúc. Bạn cần phân trang (Pagination). LINQ hỗ trợ việc này cực kỳ dễ dàng qua bộ đôi:
* **`Take(n)`:** Chỉ lấy ra `n` phần tử đầu tiên của danh sách.
* **`Skip(m)`:** Bỏ qua `m` phần tử đầu tiên và lấy các phần tử còn lại.

### Công thức phân trang tổng quát:
Nếu bạn có số trang hiện tại là `pageNumber` (1-indexed) và số phần tử mỗi trang là `pageSize`:
```csharp
int pageSize = 10;
int pageNumber = 3; // Muốn xem trang 3

var pageData = products
               .Skip((pageNumber - 1) * pageSize) // Bỏ qua 20 phần tử đầu
               .Take(pageSize)                    // Lấy 10 phần tử tiếp theo
               .ToList();
```

---

## 3. Các toán tử lấy 1 phần tử (Element Operators)

Đây là nơi lập trình viên C# mới vào nghề rất dễ nhầm lẫn dẫn đến ứng dụng bị Crash (gặp Runtime Exception). Hãy xem bảng so sánh hành vi dưới đây:

| Hàm | Khi tìm thấy 1 kết quả | Khi có nhiều hơn 1 kết quả | Khi không có kết quả nào |
|---|---|---|---|
| **`First()`** | Trả về kết quả | Trả về kết quả đầu tiên tìm thấy | **Ném ra lỗi** (`InvalidOperationException`) |
| **`FirstOrDefault()`** | Trả về kết quả | Trả về kết quả đầu tiên tìm thấy | Trả về giá trị mặc định (`null` hoặc `0`...) |
| **`Single()`** | Trả về kết quả | **Ném ra lỗi** (Đòi hỏi duy nhất 1) | **Ném ra lỗi** |
| **`SingleOrDefault()`** | Trả về kết quả | **Ném ra lỗi** (Đòi hỏi duy nhất 1) | Trả về giá trị mặc định (`null` hoặc `0`...) |
| **`Last()`** | Trả về kết quả | Trả về kết quả cuối cùng tìm thấy | **Ném ra lỗi** |
| **`LastOrDefault()`** | Trả về kết quả | Trả về kết quả cuối cùng tìm thấy | Trả về giá trị mặc định (`null` hoặc `0`...) |

### Quy tắc chọn hàm:
1. **Dùng `First` / `FirstOrDefault`:** Khi danh sách có thể có nhiều phần tử thỏa mãn, nhưng bạn chỉ quan tâm đến đứa đứng đầu. (Ví dụ: tìm học sinh có điểm cao nhất).
2. **Dùng `Single` / `SingleOrDefault`:** Khi bạn chắc chắn theo nghiệp vụ hệ thống, phần tử đó là **duy nhất** (Ví dụ: Tìm User theo Email hoặc ID). Nếu DB bị lỗi sinh ra 2 User trùng Email, hàm `Single` ném lỗi để báo hiệu dữ liệu DB đang bị lỗi nghiêm trọng.

---

## 4. So sánh với JavaScript

* Hàm `Where` của C# tương đương với `Array.prototype.filter` của JS.
* Hàm `FirstOrDefault` của C# tương đương với `Array.prototype.find` của JS (nếu không thấy trả về `undefined`).
* JS không có hàm tương đương sẵn có cho `OfType`, `Single`, hay `Skip`/`Take` trực tiếp (thường dùng `slice` cho phân trang).

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Lạm dụng `First()` thay vì `FirstOrDefault()`
Nếu bạn viết:
```csharp
var user = users.First(u => u.Email == "notfound@gmail.com");
```
Hệ thống sẽ bị sập (Crash) ngay lập tức nếu không tìm thấy email đó.
*Khắc phục:* Luôn dùng `FirstOrDefault` nếu không chắc chắn dữ liệu có tồn tại hay không, sau đó kiểm tra `null`:
```csharp
var user = users.FirstOrDefault(u => u.Email == "notfound@gmail.com");
if (user != null)
{
    // Xử lý
}
```

### Sai lầm 2: Dùng `Where().FirstOrDefault()` thay vì truyền trực tiếp điều kiện
```csharp
// Tồi (Dài dòng):
var user = users.Where(u => u.Age > 18).FirstOrDefault();

// Tốt (Ngắn gọn):
var user = users.FirstOrDefault(u => u.Age > 18);
```
*Giải thích:* Hầu hết các hàm lấy phần tử như `First`, `FirstOrDefault`, `Single`, `Any`, `Count` đều có overload cho phép truyền trực tiếp lambda làm bộ lọc đầu vào, giúp code ngắn hơn.

---

## 6. Checklist đánh giá hiểu bài

1. Hàm `Where` và `OfType` khác nhau ở điểm nào?
2. Sự khác nhau giữa `First` và `Single` là gì? Trường hợp nào dùng `Single` sẽ an toàn hơn?
3. Viết công thức phân trang lấy dữ liệu của trang thứ 5 với kích thước trang là 20 sản phẩm.
4. Điều gì xảy ra khi dùng `Distinct` trên một danh sách `List<Student>` mà Student là class chưa ghi đè hàm so sánh?
