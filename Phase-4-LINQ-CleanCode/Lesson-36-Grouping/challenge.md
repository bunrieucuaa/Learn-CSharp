# Thách thức Bài 36: Module Thống kê Báo cáo Tài chính Tháng của Doanh nghiệp

## Ngữ cảnh
Kế toán trưởng của doanh nghiệp yêu cầu bạn xây dựng một module báo cáo tài chính cuối tháng. Hệ thống cần tổng hợp thông tin giao dịch mua sắm của các bộ phận trong doanh nghiệp.

Dữ liệu đầu vào gồm 2 danh sách riêng biệt:
1. Danh sách nhân viên trong công ty (`Employees`).
2. Danh sách các giao dịch thanh toán mua thiết bị được ghi nhận (`Transactions`).

Mỗi giao dịch sẽ ghi nhận `EmployeeId` của người thực hiện thanh toán đó.

## Cấu trúc dữ liệu
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; } // "IT", "HR", "Marketing"
}

public class Transaction
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public string ItemName { get; set; }
}
```

## Dữ liệu mock-up đầu vào
```csharp
List<Employee> employees = [
    new Employee { Id = 1, Name = "An", Department = "IT" },
    new Employee { Id = 2, Name = "Bình", Department = "HR" },
    new Employee { Id = 3, Name = "Cường", Department = "IT" },
    new Employee { Id = 4, Name = "Dũng", Department = "Marketing" },
    new Employee { Id = 5, Name = "Giang", Department = "Marketing" }
];

List<Transaction> transactions = [
    new Transaction { Id = 101, EmployeeId = 1, Amount = 1500, ItemName = "Server Rent" },
    new Transaction { Id = 102, EmployeeId = 3, Amount = 800, ItemName = "Monitor LG" },
    new Transaction { Id = 103, EmployeeId = 2, Amount = 120, ItemName = "Paper A4" },
    new Transaction { Id = 104, EmployeeId = 4, Amount = 300, ItemName = "Facebook Ads" },
    new Transaction { Id = 105, EmployeeId = 1, Amount = 500, ItemName = "RAM Crucial" },
    new Transaction { Id = 106, EmployeeId = 5, Amount = 450, ItemName = "Google Ads" }
];
```

## Yêu cầu Thách thức
Hãy viết một câu truy vấn LINQ phức hợp thực hiện đồng thời các bước sau:
1. **Kết hợp dữ liệu (Join):** Ghép nối bảng `Transactions` với bảng `Employees` để lấy ra tên và phòng ban của nhân viên thực hiện giao dịch đó.
2. **Gom nhóm (Group):** Nhóm các giao dịch đã được kết hợp ở trên theo **Tên phòng ban (`Department`)**.
3. **Thống kê (Aggregation & Projection):** Trích xuất báo cáo của từng phòng ban chứa các thông tin:
   - Tên phòng ban (`Department`).
   - Tổng số giao dịch mua sắm của phòng đó.
   - Tổng số tiền phòng đó đã chi tiêu.
   - Danh sách tên các thiết bị/dịch vụ đã mua (`ItemNames`), viết cách nhau bởi dấu phẩy.

## Expected Output
Khi in kết quả ra màn hình Console, thông tin phải hiển thị sạch đẹp dạng bảng:
```text
BÁO CÁO CHI TIÊU THEO PHÒNG BAN:
---------------------------------------------
Phòng ban: IT
- Số giao dịch: 3
- Tổng chi tiêu: $2800
- Các mặt hàng đã mua: Server Rent, Monitor LG, RAM Crucial
---------------------------------------------
Phòng ban: HR
- Số giao dịch: 1
- Tổng chi tiêu: $120
- Các mặt hàng đã mua: Paper A4
---------------------------------------------
Phòng ban: Marketing
- Số giao dịch: 2
- Tổng chi tiêu: $750
- Các mặt hàng đã mua: Facebook Ads, Google Ads
---------------------------------------------
```
*Gợi ý:* Để gộp danh sách tên các mặt hàng thành chuỗi ngăn cách bởi dấu phẩy, hãy sử dụng `string.Join(", ", group.Select(...))`.
