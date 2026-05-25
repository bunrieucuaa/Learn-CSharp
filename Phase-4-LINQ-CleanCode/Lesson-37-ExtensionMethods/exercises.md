# Bài tập Thực hành Bài 37: Extension Methods

Hãy hoàn thành các bài tập dưới đây bằng cách tự viết các static class/method mở rộng tương ứng trên IDE của bạn.

---

## Bài tập 1: Hàm mở rộng viết hoa chữ cái đầu (Capitalize)
**Đề bài:**
Hãy viết một Extension Method tên là `Capitalize()` mở rộng cho kiểu `string` để biến đổi một chuỗi thường thành chuỗi viết hoa chữ cái đầu tiên (ví dụ: `"csharp"` -> `"Csharp"`, `"hello world"` -> `"Hello world"`).

* **Gợi ý:** Sử dụng `char.ToUpper(value[0])` kết hợp với `value.Substring(1)`. Nhớ xử lý trường hợp chuỗi rỗng hoặc null.
* **Expected Output:**
  ```text
  Chuỗi ban đầu: "programming"
  Sau khi Capitalize: "Programming"
  ```

---

## Bài tập 2: Xác định ngày cuối tuần (IsWeekend)
**Đề bài:**
Viết một Extension Method tên là `IsWeekend()` mở rộng cho kiểu dữ liệu `DateTime` của C# để trả về `true` nếu ngày đó rơi vào Thứ Bảy (Saturday) hoặc Chủ Nhật (Sunday), ngược lại trả về `false`.

* **Gợi ý:** Sử dụng thuộc tính `.DayOfWeek` của struct `DateTime`.
* **Expected Output khi kiểm thử:**
  ```text
  Ngày 25/05/2026 (Thứ Hai): IsWeekend? False
  Ngày 24/05/2026 (Chủ Nhật): IsWeekend? True
  ```

---

## Bài tập 3: Hàm tính phần trăm giảm giá cho giá gốc
**Đề bài:**
Viết một Extension Method tên là `DiscountPercent(this decimal price, int percent)` mở rộng cho kiểu `decimal` để tính số tiền phải trả sau khi giảm giá `percent`%.
Ví dụ: Giá gốc `100`$, giảm `20`%, số tiền còn lại phải trả là `80`$.

* **Expected Output:**
  ```text
  Giá gốc: $200 | Giảm: 15% | Số tiền thanh toán: $170
  ```

---

## Bài tập 4: Lọc danh sách chuỗi theo ký tự đặc biệt
**Đề bài:**
Hãy viết một Extension Method cho `IEnumerable<string>` với chữ ký:
`public static IEnumerable<string> ContainingKeyword(this IEnumerable<string> source, string keyword)`
Trả về các phần tử chuỗi có chứa từ khóa `keyword` (không phân biệt chữ hoa chữ thường).

* **Expected Output khi lọc danh sách ["An", "Bình", "Cường"] với keyword "n":**
  ```text
  An, Bình
  ```

---

## Bài tập 5: Chuyển đổi định dạng giờ thân thiện
**Đề bài:**
Viết một Extension Method tên là `ToTimeAgo()` mở rộng cho kiểu `DateTime` để trả về một chuỗi so sánh thời gian thân thiện so với thời gian hiện tại (`DateTime.Now`):
- Nếu khoảng cách thời gian nhỏ hơn 60 giây: Trả về `"Vừa xong"`.
- Nếu nhỏ hơn 60 phút: Trả về `"x phút trước"`.
- Nếu nhỏ hơn 24 giờ: Trả về `"x giờ trước"`.
- Nếu lớn hơn 24 giờ: Trả về `"x ngày trước"`.

* **Gợi ý:** Tính toán `TimeSpan diff = DateTime.Now - value;` sau đó dùng `.TotalSeconds`, `.TotalMinutes`, v.v.
* **Expected Output:**
  ```text
  Vừa xong
  15 phút trước
  3 giờ trước
  ```
