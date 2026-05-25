# Bài tập Thực hành Bài 34: Projection

Hãy thực hành các bài tập dưới đây bằng cách áp dụng phương thức `Select`, `SelectMany`, `Zip` và cấu trúc Anonymous Type.

---

## Bài tập 1: Bình phương danh sách số nguyên
**Đề bài:**
Cho danh sách: `List<int> numbers = [ 1, 2, 3, 4, 5 ];`
Hãy viết LINQ để tạo ra một danh sách mới chứa bình phương của các số trên.

* **Gợi ý:** Dùng `.Select()`.
* **Expected Output:**
  ```text
  1, 4, 9, 16, 25
  ```

---

## Bài tập 2: Rút trích thông tin sản phẩm (DTO)
**Đề bài:**
Cho class `Product`:
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
```
Và danh sách:
```csharp
List<Product> products = [
    new Product { Id = 101, Name = "Mouse", Price = 30, Category = "Gear" },
    new Product { Id = 102, Name = "Keyboard", Price = 80, Category = "Gear" },
    new Product { Id = 103, Name = "Monitor", Price = 250, Category = "Screen" }
];
```
Hãy viết LINQ chiếu danh sách trên thành một danh sách đối tượng vô danh (Anonymous Type) chỉ chứa 2 trường: `ProductName` (là giá trị Name) và `DisplayPrice` (định dạng dạng chuỗi có ký hiệu đô la đứng trước, ví dụ: `"$30"`).

* **Expected Output khi in:**
  ```text
  - Mouse: $30
  - Keyboard: $80
  - Monitor: $250
  ```

---

## Bài tập 3: Làm phẳng đơn hàng
**Đề bài:**
Cho class `OrderItem` và `Order`:
```csharp
public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public List<OrderItem> Items { get; set; }
}
```
Và danh sách đơn hàng:
```csharp
List<Order> orders = [
    new Order { 
        OrderId = 1, 
        Items = [ 
            new OrderItem { ProductName = "Bánh mì", Quantity = 2 },
            new OrderItem { ProductName = "Sữa tươi", Quantity = 1 } 
        ]
    },
    new Order { 
        OrderId = 2, 
        Items = [ 
            new OrderItem { ProductName = "Cà phê", Quantity = 3 } 
        ]
    }
];
```
Hãy viết LINQ trả về danh sách phẳng gồm toàn bộ tên các sản phẩm đã được mua trong tất cả các đơn hàng trên (không cần lấy số lượng, bỏ trùng nếu có).

* **Gợi ý:** Sử dụng `.SelectMany()` để làm phẳng các phần tử `Items`, sau đó chọn trường tên và dùng `Distinct()`.
* **Expected Output:**
  ```text
  Bánh mì, Sữa tươi, Cà phê
  ```

---

## Bài tập 4: Ghép danh sách sinh viên và lớp học
**Đề bài:**
Cho hai danh sách song song:
`List<string> students = [ "Vy", "Lâm", "Hải" ];`
`List<string> classNames = [ "Lớp A", "Lớp B", "Lớp C" ];`
Hãy dùng toán tử `Zip` để ghép hai danh sách trên thành một danh sách đối tượng chứa hai thuộc tính tương ứng: `StudentName` và `ClassName`.

* **Expected Output:**
  ```text
  Vy học ở Lớp A
  Lâm học ở Lớp B
  Hải học ở Lớp C
  ```

---

## Bài tập 5: Phép chiếu lọc kết hợp
**Đề bài:**
Cho danh sách điểm số: `List<int> scores = [ 45, 65, 80, 95, 30, 75 ];`
Hãy viết LINQ thực hiện:
1. Lọc ra các điểm số >= 50.
2. Chiếu điểm số đó thành trạng thái học tập dạng chuỗi: Nếu điểm >= 80 ghi `"Giỏi"`, ngược lại ghi `"Đạt"`.

* **Gợi ý:** Gọi `.Where()` trước rồi gọi `.Select()` kết hợp toán tử điều kiện 3 ngôi `score >= 80 ? "Giỏi" : "Đạt"`.
* **Expected Output:**
  ```text
  Đạt, Giỏi, Giỏi, Đạt
  ```
