# Bài 37: Extension Methods (Phương thức mở rộng)

Trong các bài học trước, bạn đã sử dụng rất nhiều hàm LINQ như `.Where()`, `.Select()`, `.OrderBy()`. Bạn có bao giờ tự hỏi: *Tại sao class `List<T>` hay mảng `T[]` không hề định nghĩa các hàm này trong mã nguồn của chúng, nhưng chúng ta vẫn có thể gọi chúng bằng toán tử dấu chấm `.` một cách dễ dàng?*

Câu trả lời nằm ở **Extension Methods (Phương thức mở rộng)**. Đây là một tính năng cực kỳ mạnh mẽ của C# cho phép bạn "bổ sung" các method mới vào các Class/Interface có sẵn mà không cần thay đổi mã nguồn gốc của Class đó và không cần dùng đến kế thừa (Inheritance).

---

## 1. Tại sao cần Extension Methods?

Hãy tưởng tượng bạn đang sử dụng class `string` của hệ thống .NET. Bạn muốn có một hàm chuyển đổi một chuỗi tiền tệ thành định dạng tiền Việt Nam Đồng (ví dụ: `"500000"` -> `"500.000 đ"`).

Bình thường, bạn sẽ phải viết một class Helper:
```csharp
public static class StringHelper
{
    public static string ToVnd(string value)
    {
        if (decimal.TryParse(value, out decimal amount))
        {
            return $"{amount:N0} đ";
        }
        return value;
    }
}

// Cách gọi:
string price = "500000";
string vndPrice = StringHelper.ToVnd(price); // Cú pháp gọi hàm lồng nhau thông thường
```

Cách gọi trên hoạt động tốt, nhưng không được tự nhiên và không viết chuỗi phương thức (fluent interface / method chaining) được. Bạn muốn gọi trực tiếp như thế này:
```csharp
string vndPrice = price.ToVnd(); // Nhìn như ToVnd là hàm có sẵn của class string!
```
Vì class `string` là class hệ thống (và là class `sealed` - không thể kế thừa), bạn không thể sửa code của nó. Đây chính là lúc **Extension Methods** tỏa sáng.

---

## 2. Cách tạo một Extension Method

Để tạo một Extension Method, bạn cần tuân thủ **3 quy tắc vàng** sau đây:

1. Phương thức mở rộng bắt buộc phải được định nghĩa bên trong một **`static class` (lớp tĩnh)**.
2. Phương thức mở rộng bắt buộc phải là một **`static method` (phương thức tĩnh)**.
3. Tham số đầu tiên của phương thức đại diện cho kiểu dữ liệu mà bạn muốn mở rộng, và bắt buộc phải có từ khóa **`this`** đứng trước.

### Hiện thực hóa ví dụ `.ToVnd()`:
```csharp
// Quy tắc 1: Class tĩnh
public static class StringExtensions
{
    // Quy tắc 2: Method tĩnh
    // Quy tắc 3: Có từ khóa 'this' đứng trước kiểu dữ liệu cần mở rộng (string)
    public static string ToVnd(this string value)
    {
        if (decimal.TryParse(value, out decimal amount))
        {
            return $"{amount:N0} đ";
        }
        return value;
    }
}
```

### Cách sử dụng:
Bây giờ, chỉ cần import đúng `namespace` chứa class `StringExtensions` là bạn có thể gọi trực tiếp:
```csharp
string price = "1000000";
string result = price.ToVnd(); // Output: 1.000.000 đ
```

> [!TIP]
> **Bản chất thực thi:**
> Extension method thực chất chỉ là cú pháp "đánh lừa thị giác" (Syntactic Sugar). Khi bạn biên dịch, Compiler sẽ tự động dịch chuyển lệnh `price.ToVnd()` về lại thành lệnh gọi tĩnh truyền thống `StringExtensions.ToVnd(price)`. 
> Do đó, Extension Method hoàn toàn **không làm suy giảm hiệu năng** của ứng dụng!

---

## 3. LINQ hoạt động dưới dạng Extension Methods như thế nào?

Bây giờ bạn đã hiểu bản chất!
Toàn bộ các hàm của LINQ (`Where`, `Select`, `OrderBy`, `GroupBy`...) thực chất chỉ là các **Extension Methods** được định nghĩa trong class tĩnh `System.Linq.Enumerable`.
Chúng mở rộng cho interface **`IEnumerable<T>`**.

Vì `List<T>`, `Dictionary<TKey, TValue>`, `Array` (`T[]`) đều implement interface `IEnumerable<T>`, nên tất cả chúng đều tự động thừa hưởng toàn bộ các phương thức của LINQ khi bạn `using System.Linq;`.

---

## 4. So sánh với JavaScript: Ghi đè Prototype vs Extension Method

* **Trong JavaScript:**
  Để thêm tính năng vào mảng có sẵn, lập trình viên JS thường ghi đè thuộc tính `prototype` của đối tượng toàn cục `Array`:
  ```javascript
  Array.prototype.last = function() {
      return this[this.length - 1];
  };
  ```
  *Mối nguy hiểm:* Cách này gọi là **Monkey Patching** (Vá khỉ). Nó cực kỳ nguy hiểm vì làm ô nhiễm (pollute) prototype toàn cục. Nếu có 2 thư viện bên thứ ba cùng định nghĩa hàm `Array.prototype.last` với logic khác nhau, chúng sẽ đè lên nhau gây ra những bug không thể debug nổi.

* **Trong C#:**
  Extension Methods cực kỳ an toàn. Bạn chỉ có thể dùng hàm mở rộng nếu bạn chủ động nhập namespace chứa nó bằng lệnh `using`. Nếu không `using`, hàm mở rộng đó hoàn toàn ẩn đi và không gây ảnh hưởng gì tới các thành phần khác. Nó cũng được kiểm tra kiểu dữ liệu cực kỳ nghiêm ngặt tại thời điểm Compile (Type-safe).

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Lạm dụng viết Extension Method vô tội vạ
Hàm mở rộng chỉ nên dùng cho các tiện ích dùng chung (Utility methods) mang tính tổng quát (ví dụ: Xử lý chuỗi, định dạng ngày tháng, kiểm tra null/rỗng...). Tránh viết các logic mang nặng tính nghiệp vụ cụ thể của một dự án vào Extension Method vì nó làm mất tính hướng đối tượng và khó kiểm thử.

### Sai lầm 2: Tạo trùng tên với hàm gốc (Instance Method)
Nếu Class gốc đã có sẵn một hàm `public void Save()`, và bạn viết một Extension Method cũng tên là `.Save()`, C# sẽ luôn luôn **ưu tiên gọi hàm gốc của class** và **bỏ qua hoàn toàn** Extension Method của bạn mà không báo lỗi.

---

## 6. Checklist đánh giá hiểu bài

1. Nêu 3 quy tắc bắt buộc khi khai báo một Extension Method trong C#.
2. Extension Method thực chất hoạt động như thế nào dưới sự biên dịch của Compiler? Nó có làm chậm chương trình không?
3. Tại sao khi gõ code LINQ ta phải viết `using System.Linq;` ở đầu file?
4. Sự khác biệt về mặt an toàn giữa Extension Method trong C# và ghi đè Prototype trong JS là gì?
