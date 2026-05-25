# Bài 36: Grouping & Joins (Gom nhóm và Kết hợp dữ liệu)

Trong bài học này, chúng ta sẽ khám phá các toán tử nâng cao của LINQ dùng để gom nhóm dữ liệu (Grouping) và kết hợp thông tin giữa các danh sách khác nhau (Joins). Đây là những tác vụ cực kỳ hữu ích khi bạn làm báo cáo thống kê dữ liệu.

---

## 1. Gom nhóm dữ liệu bằng `GroupBy`

### 1.1. Khái niệm `GroupBy`
Toán tử `GroupBy` dùng để chia một tập hợp thành các nhóm dựa trên một khóa (Key) xác định.
* **Cú pháp:** `collection.GroupBy(element => element.KeyProperty)`
* **Kiểu trả về:** `IEnumerable<IGrouping<TKey, TSource>>`

> [!NOTE]
> **`IGrouping<TKey, TElement>` là gì?**
> Đây là một kiểu dữ liệu đặc biệt trong C#. Nó hoạt động tương tự như một cặp Key-Value nhưng có đặc điểm:
> * Thuộc tính `.Key` chứa khóa đại diện cho nhóm đó.
> * Bản thân `IGrouping` chính là một danh sách chứa các phần tử thuộc nhóm đó (bạn có thể dùng `foreach` hoặc gọi các hàm LINQ trực tiếp trên nó).

### 1.2. Ví dụ minh họa GroupBy
Giả sử bạn có danh sách Sản phẩm (`List<Product>`), bạn muốn nhóm chúng theo Danh mục (`Category`):
```csharp
var groupByCategory = products.GroupBy(p => p.Category);

foreach (var group in groupByCategory)
{
    Console.WriteLine($"Danh mục: {group.Key}"); // Khóa của nhóm
    foreach (var product in group)
    {
        Console.WriteLine($"  - {product.Name} (${product.Price})");
    }
}
```

---

## 2. Các hàm tổng hợp nhóm (Aggregation Operators)

Khi đã gom nhóm dữ liệu, thông thường chúng ta sẽ muốn làm các phép toán thống kê trên từng nhóm như: Tính tổng giá trị, tìm giá trị trung bình, đếm số lượng...
LINQ cung cấp các toán tử tổng hợp trực tiếp:
* **`Count()`**: Đếm số phần tử.
* **`Sum()`**: Tính tổng.
* **`Average()`**: Tính trung bình cộng.
* **`Min()`** / **`Max()`**: Tìm giá trị nhỏ nhất / lớn nhất.

**Ví dụ:** Tính tổng tiền hàng của từng danh mục sản phẩm:
```csharp
var categoryStats = products
                    .GroupBy(p => p.Category)
                    .Select(g => new {
                        CategoryName = g.Key,
                        ProductCount = g.Count(),
                        TotalPrice = g.Sum(p => p.Price)
                    });
```

---

## 3. Phân biệt `GroupBy` vs `ToLookup`

* **`GroupBy`**: Hoạt động theo cơ chế **Deferred Execution** (Trì hoãn). Nó chỉ chạy gom nhóm khi bạn duyệt qua dữ liệu.
* **`ToLookup`**: Hoạt động theo cơ chế **Eager Execution** (Thực thi ngay). Nó gom nhóm ngay lập tức và lưu kết quả vào RAM dưới dạng một cấu trúc dữ liệu đọc nhanh (Read-only Dictionary-like structure).
  * *Cú pháp:* `collection.ToLookup(p => p.Category)`

---

## 4. Kết hợp danh sách: `Join` & `GroupJoin`

Đôi khi dữ liệu của bạn được lưu tách rời ở 2 danh sách khác nhau (giống như 2 bảng trong Database liên kết bằng Khóa ngoại). LINQ hỗ trợ bạn kết hợp chúng:

### 4.1. `Join` (Inner Join)
Kết hợp hai tập hợp dựa trên các khóa khớp nhau. Những phần tử ở tập hợp 1 không tìm thấy khóa khớp ở tập hợp 2 sẽ bị loại bỏ.
```csharp
var studentWithClass = students.Join(
    classes,
    student => student.ClassId,   // Khóa liên kết của tập hợp 1 (Student)
    clazz => clazz.Id,            // Khóa liên kết của tập hợp 2 (ClassRoom)
    (student, clazz) => new {     // Kết quả đầu ra
        StudentName = student.Name,
        ClassName = clazz.ClassName
    }
);
```

### 4.2. `GroupJoin` (Left Outer Join)
Kết hợp hai tập hợp nhưng gom nhóm kết quả. Với mỗi phần tử ở tập hợp 1, nó sẽ chứa một danh sách con các phần tử khớp khóa ở tập hợp 2 (nếu không khớp, danh sách con sẽ rỗng - không bị loại bỏ phần tử ở tập hợp 1).
```csharp
var classWithStudents = classes.GroupJoin(
    students,
    clazz => clazz.Id,
    student => student.ClassId,
    (clazz, studentGroup) => new {
        ClassName = clazz.ClassName,
        Students = studentGroup // Danh sách học sinh thuộc lớp này
    }
);
```

---

## 5. So sánh với JavaScript

* JavaScript mới đây đã bổ sung `Object.groupBy(array, callback)` (trong ES2024) hoạt động tương tự như `GroupBy` nhưng trả về một Object thuần túy của JS có các Key là tên nhóm.
* Trước ES2024, lập trình viên JS thường phải viết hàm `.reduce()` thủ công rất dài dòng để gom nhóm.
* JS không có các toán tử `Join` hay `GroupJoin` tích hợp sẵn, bắt buộc phải viết hai vòng lặp lồng nhau hoặc dùng Map để tự so khớp khóa.

---

## 6. Checklist đánh giá hiểu bài

1. Kiểu dữ liệu `IGrouping<TKey, TElement>` chứa 2 thông tin quan trọng nào?
2. Sự khác nhau giữa `GroupBy` và `ToLookup` là gì?
3. Khi nào chúng ta dùng `Join` và khi nào dùng `GroupJoin`?
4. Làm thế nào để tính điểm trung bình môn học của từng lớp từ danh sách học sinh?
