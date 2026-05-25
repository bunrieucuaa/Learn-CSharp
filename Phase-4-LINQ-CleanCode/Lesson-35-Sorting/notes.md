# Ghi nhớ Bài 35: Sorting

## 1. Tóm tắt kiến thức cốt lõi
* **Sắp xếp cơ bản:** `.OrderBy(x => x.Prop)` (Tăng dần) và `.OrderByDescending(x => x.Prop)` (Giảm dần).
* **Sắp xếp đa tiêu chí:** Luôn luôn dùng `.ThenBy()` hoặc `.ThenByDescending()` cho các tiêu chí phụ tiếp theo.
* **Custom Sort:** Sử dụng interface `IComparer<T>` truyền vào tham số thứ hai của `OrderBy` để tự định nghĩa logic so sánh.
* **Non-mutating:** LINQ không chỉnh sửa danh sách ban đầu mà tạo ra một tập hợp mới, ngược lại với `.sort()` của JS sửa đổi trực tiếp trên mảng nguồn.
* **Stable Sort:** Đảm bảo thứ tự tương đối của các phần tử có khoá bằng nhau được giữ nguyên.

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Lỗi viết liên tiếp nhiều `OrderBy`:**
>   `list.OrderBy(a => a.Age).OrderBy(a => a.Name)`
>   Đây là lỗi kinh điển. Đoạn code này sẽ sắp xếp theo Age, sau đó sắp xếp lại toàn bộ theo Name và vứt bỏ hoàn toàn kết quả sắp xếp của Age.
>   *Sửa đúng:* `list.OrderBy(a => a.Age).ThenBy(a => a.Name)`
> * **Hiệu năng của sorting:** Sắp xếp là một tác vụ tốn tài nguyên O(N log N). Hạn chế sắp xếp quá nhiều lần hoặc sắp xếp trên các tập dữ liệu cực lớn khi chưa qua bộ lọc `.Where()`.

## 3. Checklist tự đánh giá
- [ ] Bạn đã hiểu tại sao không được dùng hai hàm `OrderBy` liên tiếp chưa?
- [ ] Bạn có phân biệt được hành vi sắp xếp của JS (Mutating) và C# LINQ (Non-mutating) không?
- [ ] Bạn có biết cách viết một class Custom Comparer để sắp xếp chuỗi theo độ dài tăng dần không?
