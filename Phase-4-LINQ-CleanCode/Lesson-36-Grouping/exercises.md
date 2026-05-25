# Bài tập Thực hành Bài 36: Grouping & Joins

Hoàn thành các bài tập dưới đây bằng cách viết code trực tiếp trên IDE cá nhân của bạn để kiểm tra tính đúng đắn.

---

## Bài tập 1: Gom nhóm số nguyên chẵn/lẻ
**Đề bài:**
Cho danh sách số nguyên: `List<int> numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];`
Hãy viết LINQ để nhóm các số này thành hai nhóm: nhóm Số Chẵn và nhóm Số Lẻ. 

* **Gợi ý:** Dùng `.GroupBy(n => n % 2 == 0)`.
* **Expected Output:**
  ```text
  - Nhóm Lẻ (Key = False): 1, 3, 5, 7, 9
  - Nhóm Chẵn (Key = True): 2, 4, 6, 8, 10
  ```

---

## Bài tập 2: Thống kê số lượng sách theo thể loại
**Đề bài:**
Cho class `Book`:
```csharp
public class Book
{
    public string Title { get; set; }
    public string Genre { get; set; } // "IT", "Novel", "Science"
}
```
Và danh sách sách:
```csharp
List<Book> library = [
    new Book { Title = "C# in Depth", Genre = "IT" },
    new Book { Title = "Dune", Genre = "Novel" },
    new Book { Title = "Clean Code", Genre = "IT" },
    new Book { Title = "A Brief History of Time", Genre = "Science" },
    new Book { Title = "Sherlock Holmes", Genre = "Novel" }
];
```
Hãy viết LINQ nhóm sách theo Thể loại (`Genre`) và in ra số lượng sách của từng thể loại đó.

* **Expected Output:**
  ```text
  Thể loại IT có 2 cuốn.
  Thể loại Novel có 2 cuốn.
  Thể loại Science có 1 cuốn.
  ```

---

## Bài tập 3: Tìm doanh thu trung bình của khách hàng
**Đề bài:**
Cho danh sách hóa đơn mua sắm của các khách hàng:
```csharp
public class Invoice
{
    public string CustomerName { get; set; }
    public decimal Amount { get; set; }
}

List<Invoice> sales = [
    new Invoice { CustomerName = "An", Amount = 150 },
    new Invoice { CustomerName = "Bình", Amount = 300 },
    new Invoice { CustomerName = "An", Amount = 50 },
    new Invoice { CustomerName = "Bình", Amount = 100 },
    new Invoice { CustomerName = "Cường", Amount = 200 }
];
```
Hãy viết LINQ thống kê xem mỗi khách hàng đã chi tiêu tổng cộng bao nhiêu tiền và trung bình bao nhiêu tiền cho mỗi hóa đơn.

* **Expected Output:**
  ```text
  Khách hàng An: Tổng chi tiêu = $200 | Trung bình = $100/đơn
  Khách hàng Bình: Tổng chi tiêu = $400 | Trung bình = $200/đơn
  Khách hàng Cường: Tổng chi tiêu = $200 | Trung bình = $200/đơn
  ```

---

## Bài tập 4: Ghép nối đơn hàng và khách hàng (Join)
**Đề bài:**
Cho 2 danh sách:
```csharp
public class Customer { public int Id { get; set; } public string Name { get; set; } }
public class Order { public int OrderId { get; set; } public int CustomerId { get; set; } public string ItemName { get; set; } }

List<Customer> customers = [
    new Customer { Id = 1, Name = "Nam" },
    new Customer { Id = 2, Name = "Vân" }
];

List<Order> orders = [
    new Order { OrderId = 101, CustomerId = 1, ItemName = "Bàn làm việc" },
    new Order { OrderId = 102, CustomerId = 2, ItemName = "Ghế xoay" },
    new Order { OrderId = 103, CustomerId = 1, ItemName = "Đèn Led" }
];
```
Hãy viết LINQ sử dụng `Join` để in ra danh sách đơn hàng kèm tên khách hàng tương ứng.

* **Expected Output:**
  ```text
  Đơn 101: Bàn làm việc - Khách hàng: Nam
  Đơn 102: Ghế xoay - Khách hàng: Vân
  Đơn 103: Đèn Led - Khách hàng: Nam
  ```

---

## Bài tập 5: Thống kê giao dịch lỗi của người dùng
**Đề bài:**
Cho danh sách giao dịch tài chính:
```csharp
public class Transaction
{
    public string Username { get; set; }
    public decimal Amount { get; set; }
    public bool IsSuccess { get; set; }
}

List<Transaction> transactions = [
    new Transaction { Username = "alice", Amount = 50, IsSuccess = true },
    new Transaction { Username = "bob", Amount = 120, IsSuccess = false },
    new Transaction { Username = "alice", Amount = 100, IsSuccess = false },
    new Transaction { Username = "charlie", Amount = 200, IsSuccess = true },
    new Transaction { Username = "bob", Amount = 300, IsSuccess = false }
];
```
Hãy viết LINQ tìm ra những tài khoản nào đang có số giao dịch bị **thất bại** (`IsSuccess == false`) từ 2 lần trở lên.

* **Gợi ý:** Nhóm theo `Username`, sau đó lọc danh sách nhóm bằng cách kiểm tra số lượng các phần tử có `IsSuccess == false` trong nhóm đó bằng toán tử `.Where(g => g.Count(t => !t.IsSuccess) >= 2)`.
* **Expected Output:**
  ```text
  bob (có 2 giao dịch thất bại)
  ```
