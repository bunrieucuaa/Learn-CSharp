# Thách thức Bài 32: Tối ưu hóa hóa đơn của hệ thống CRM cửa hàng

## Ngữ cảnh
Bạn vừa được tuyển vào làm intern tại một công ty SaaS quản lý bán hàng. Quản lý đưa cho bạn một đoạn code "legacy" được viết bởi một cộng tác viên cũ. Đoạn code này dùng để lọc ra những đơn hàng (Orders) có giá trị cao, phục vụ cho chương trình tri ân khách hàng VIP cuối năm.

Đoạn code chạy đúng nhưng gặp vấn đề là:
1. Viết quá dài dòng, sử dụng nhiều biến phụ lồng nhau.
2. Khó bảo trì khi cần thay đổi điều kiện lọc.
3. Không tối ưu hóa hiệu năng bộ nhớ khi xử lý mảng lớn.

## Cấu trúc dữ liệu có sẵn
```csharp
public class Order
{
    public string OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } // "Completed", "Pending", "Cancelled"
    public DateTime CreatedDate { get; set; }
}
```

## Đoạn code Legacy cần refactor
```csharp
public List<Order> GetVipOrdersLegacy(List<Order> allOrders)
{
    List<Order> vipOrders = new List<Order>();
    foreach (var order in allOrders)
    {
        // Điều kiện VIP: Đơn hàng Completed, trị giá từ 500$ trở lên và được tạo trong năm 2026
        if (order.Status == "Completed")
        {
            if (order.TotalAmount >= 500)
            {
                if (order.CreatedDate.Year == 2026)
                {
                    vipOrders.Add(order);
                }
            }
        }
    }
    return vipOrders;
}
```

## Yêu cầu Thách thức
1. **Refactor code:** Hãy viết lại phương thức trên bằng cách sử dụng **LINQ Method Syntax** chỉ trong đúng 1 dòng code logic (không tính khai báo method).
2. **Kiểm tra cơ chế Deferred Execution:**
   - Hãy thiết kế một kịch bản kiểm tra: Gọi hàm lọc đơn hàng, sau đó thêm một đơn hàng VIP mới vào danh sách `allOrders`.
   - Chứng minh rằng câu query LINQ của bạn sẽ tự động bao gồm cả đơn hàng mới đó khi duyệt qua kết quả (nếu không gọi `.ToList()` ngay).
   - Tiếp tục chỉnh sửa kịch bản để kết quả được cố định ngay tại thời điểm gọi hàm bằng cách dùng `.ToList()`.

## Gợi ý các bước
- Bước 1: Hãy sử dụng `.Where(...)` với biểu thức lambda kiểm tra 3 điều kiện đồng thời bằng toán tử logic `&&`.
- Bước 2: Viết một class `Program` chạy thử nghiệm với dữ liệu giả lập (mock data) khoảng 5 đơn hàng, trong đó có đơn thỏa mãn điều kiện và đơn không.
- Bước 3: In kết quả ra màn hình Console để tự chứng minh tính đúng đắn.
