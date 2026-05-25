# Bài 34: Projection (Phép chiếu dữ liệu trong LINQ)

Ở các bài học trước, bạn đã biết cách giữ lại các phần tử mong muốn (Filtering). Bài học này sẽ giới thiệu cho bạn **Projection (Phép chiếu)** — kỹ thuật biến đổi dữ liệu từ dạng này sang dạng khác hoặc trích xuất một số thuộc tính nhất định của phần tử.

---

## 1. Khái niệm Projection

Trong SQL, khi bạn viết `SELECT Name, Age FROM Students`, bạn đang thực hiện một phép chiếu (chỉ lấy ra 2 cột trong số hàng chục cột của bảng).
Trong LINQ, phép chiếu giúp chúng ta:
* Trích xuất một thuộc tính đơn lẻ (ví dụ: chuyển `List<Student>` thành `List<string>` chứa toàn bộ Tên).
* Chuyển đổi một đối tượng lớn sang một đối tượng nhỏ hơn (gọi là DTO - Data Transfer Object) để gửi về client nhằm tăng tính bảo mật và tiết kiệm băng thông.
* Tạo ra các đối tượng có cấu trúc hoàn toàn mới tại thời điểm chạy (Anonymous Types).

---

## 2. Các toán tử Projection phổ biến

### 2.1. `Select` (Biến đổi từng phần tử)
Đây là toán tử cơ bản nhất, tương đương với hàm `.map()` trong JavaScript.
* **Cú pháp:** `collection.Select(element => transformation)`
* **Hoạt động:** Áp dụng biểu thức lambda lên từng phần tử và trả về một bộ sưu tập mới chứa các kết quả biến đổi.

**Ví dụ:** Chuyển mảng số nguyên thành mảng chuỗi chữ viết:
```csharp
List<int> numbers = [1, 2, 3];
var strings = numbers.Select(n => $"Số: {n}"); // Trả về IEnumerable<string>
```

### 2.2. `SelectMany` (Biến đổi và Làm phẳng - Flattening)
Khi phần tử trong danh sách gốc của bạn lại chứa một danh sách con, và bạn muốn gộp tất cả các danh sách con đó thành một danh sách phẳng duy nhất, `SelectMany` chính là giải pháp.
* Nó tương đương với `.flatMap()` trong JavaScript.
* **Ví dụ:** Bạn có danh sách các Lớp học (`List<ClassRoom>`), mỗi lớp học chứa một danh sách các Học sinh (`List<Student>`). Bạn muốn lấy ra một danh sách phẳng chứa tất cả Học sinh của toàn trường (`List<Student>`).

**Minh họa:**
```
[Lớp A: {Hùng, Lan}] -> SelectMany -> [Hùng, Lan, Tuấn, Vy]
[Lớp B: {Tuấn, Vy}]
```

### 2.3. `Zip` (Ghép đôi song song)
Kết hợp hai hoặc ba tập hợp có cùng độ dài theo từng vị trí tương ứng (phần tử thứ 1 đi với phần tử thứ 1, thứ 2 đi với thứ 2...).
```csharp
List<string> names = ["An", "Bình"];
List<int> ages = [20, 22];

// Ghép đôi tên và tuổi thành chuỗi giới thiệu
var info = names.Zip(ages, (name, age) => $"{name} ({age} tuổi)");
// Kết quả: ["An (20 tuổi)", "Bình (22 tuổi)"]
```

---

## 3. Kiểu dữ liệu vô danh (Anonymous Types)

Trong quá trình chiếu dữ liệu, đôi khi bạn cần gộp một vài thuộc tính từ Object cũ thành một cấu trúc tạm thời mà không muốn tốn công định nghĩa một class mới. C# hỗ trợ **Anonymous Type** bằng từ khóa `new { ... }`.

```csharp
var studentBrief = students.Select(s => new {
    FullName = s.FirstName + " " + s.LastName,
    IsAdult = s.Age >= 18
});
```
* **Đặc điểm:**
  * Kiểu dữ liệu này không có tên gọi cụ thể trong code. Trình biên dịch sẽ tự sinh tên ngầm.
  * Các thuộc tính của Anonymous Type là **chỉ đọc (Read-only)**, bạn không thể thay đổi giá trị của chúng sau khi khởi tạo.
  * Thích hợp dùng làm dữ liệu trung gian trong một phương thức. Không nên trả Anonymous Type ra khỏi phương thức (bởi vì kiểu trả về của phương thức sẽ phải để là `object` hoặc `dynamic`, làm mất tính an toàn kiểu dữ liệu).

---

## 4. So sánh với JavaScript

| Toán tử C# | Hàm JS tương đương | Mô tả |
|---|---|---|
| `.Select(x => ...)` | `.map(x => ...)` | Biến đổi phần tử 1-1 |
| `.SelectMany(x => ...)` | `.flatMap(x => ...)` | Biến đổi 1-n và làm phẳng |
| Anonymous Type `new { A = 1 }` | Object Literal `{ A: 1 }` | JS object linh hoạt hơn nhưng không có type safety tĩnh như C#. |

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm: Sử dụng `Select` thay vì `SelectMany` khi muốn làm phẳng dữ liệu
```csharp
// Tồi: Trả về một tập hợp lồng nhau IEnumerable<List<Student>>
var listGroup = classes.Select(c => c.Students);

// Tốt: Trả về một tập hợp phẳng IEnumerable<Student>
var flatList = classes.SelectMany(c => c.Students);
```
*Khắc phục:* Nếu thấy kiểu dữ liệu trả về bị lồng kiểu dạng `IEnumerable<List<T>>` hoặc `IEnumerable<IEnumerable<T>>`, hãy lập tức nghĩ đến việc thay thế `Select` bằng `SelectMany`.

---

## 6. Checklist đánh giá hiểu bài

1. Phép chiếu (Projection) là gì?
2. Sự khác biệt lớn nhất giữa `Select` và `SelectMany` là gì?
3. Khi nào chúng ta nên sử dụng Anonymous Types? Tại sao không nên trả Anonymous Types ra ngoài phương thức (hàm public)?
4. Làm cách nào để gộp hai mảng song song có cùng độ dài thành một mảng đối tượng mới?
