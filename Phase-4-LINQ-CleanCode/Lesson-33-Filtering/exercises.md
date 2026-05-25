# Bài tập Thực hành Bài 33: Filtering

Hãy hoàn thành 5 bài tập dưới đây bằng cách viết code trực tiếp trên dự án cá nhân hoặc môi trường Console test của bạn.

---

## Bài tập 1: Lọc số chẵn bỏ qua các số đầu tiên
**Đề bài:**
Cho danh sách số nguyên: `List<int> numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];`
Hãy viết LINQ để bỏ qua 3 số đầu tiên của danh sách, sau đó lọc ra những số chẵn còn lại.

* **Gợi ý:** Dùng kết hợp `.Skip()` và `.Where()`.
* **Expected Output:**
  ```text
  4, 6, 8, 10
  ```

---

## Bài tập 2: Lọc danh sách tệp tin
**Đề bài:**
Cho danh sách tên file trong một thư mục:
`List<string> filenames = [ "image.png", "document.pdf", "script.js", "logo.png", "index.html", "avatar.png" ];`
Hãy viết LINQ lọc ra các file có đuôi mở rộng là `.png`.

* **Gợi ý:** Sử dụng hàm `.EndsWith()` của kiểu `string` trong biểu thức lambda lọc.
* **Expected Output:**
  ```text
  image.png, logo.png, avatar.png
  ```

---

## Bài tập 3: Thiết kế hàm phân trang
**Đề bài:**
Cho danh sách bài viết dưới dạng chuỗi:
`List<string> posts = [ "Post 1", "Post 2", "Post 3", "Post 4", "Post 5", "Post 6", "Post 7", "Post 8" ];`
Viết một hàm có nguyên mẫu:
`List<string> GetPage(List<string> source, int pageIndex, int pageSize)`
Trả về danh sách bài viết thuộc trang tương ứng. Hãy chạy thử hàm này để lấy dữ liệu trang 2, mỗi trang 3 phần tử.

* **Gợi ý:** Dùng công thức `.Skip((pageIndex - 1) * pageSize).Take(pageSize)`.
* **Expected Output khi chạy GetPage(posts, 2, 3):**
  ```text
  Post 4, Post 5, Post 6
  ```

---

## Bài tập 4: Tìm sinh viên theo MSSV (Mã số sinh viên)
**Đề bài:**
Cho class `Student`:
```csharp
public class Student
{
    public string StudentId { get; set; }
    public string Name { get; set; }
}
```
Và danh sách:
```csharp
List<Student> students = [
    new Student { StudentId = "S01", Name = "Lâm" },
    new Student { StudentId = "S02", Name = "Hoàng" },
    new Student { StudentId = "S03", Name = "Hải" }
];
```
Viết câu truy vấn LINQ để tìm kiếm sinh viên có `StudentId` là `"S02"`. Yêu cầu:
1. Sử dụng toán tử lọc đảm bảo tính **duy nhất** của MSSV.
2. Đảm bảo an toàn không ném ra lỗi nếu MSSV cần tìm không tồn tại (trả về `null`).

* **Gợi ý:** Sử dụng `.SingleOrDefault()`.
* **Expected Output:**
  ```text
  StudentId: S02, Name: Hoàng
  ```

---

## Bài tập 5: Lọc đối tượng không xác định
**Đề bài:**
Cho một mảng các đối tượng chứa nhiều kiểu dữ liệu hỗn hợp (kiểu `object`):
`object[] rawData = [ 10, "Hello", 20.5, "World", 30, true, "C#" ];`
Hãy viết LINQ lọc ra toàn bộ các chuỗi ký tự (kiểu `string`) trong mảng và in chúng dưới dạng viết hoa (UPPERCASE).

* **Gợi ý:** Sử dụng `.OfType<string>()` để tách lọc kiểu dữ liệu trước, sau đó dùng `.Select()` để biến đổi sang chữ in hoa.
* **Expected Output:**
  ```text
  HELLO, WORLD, C#
  ```
