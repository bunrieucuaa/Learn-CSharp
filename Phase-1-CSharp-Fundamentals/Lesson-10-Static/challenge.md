# 🏆 Lesson 10 — Challenge: Static

---

## Challenge: Hệ thống quản lý quán cà phê ☕

### Bối cảnh:
> "Xây dựng console app cho quán café. Tất cả code phải tổ chức bằng static classes đúng cách."

### Kiến trúc BẮT BUỘC (5 static classes + 1 Program):

```
static class CafeConfig     → Hằng số (tên quán, thuế, giờ mở/đóng...)
static class CafeMenu       → Menu đồ uống (data + methods)
static class CafeOrder      → Quản lý đơn hàng (tạo, xem, hủy)
static class CafeStats      → Thống kê (doanh thu, best seller...)
static class CafeUI          → Tất cả hiển thị + input
class Program                → Main() ≤ 10 dòng
```

### Chi tiết:

#### `CafeConfig`:
```csharp
static class CafeConfig
{
    public const string CAFE_NAME = "☕ Café 42";
    public const decimal TAX_RATE = 0.08m;
    public const decimal MEMBER_DISCOUNT = 0.10m;
    public const string COUPON_CODE = "CAFE20";
    public const decimal COUPON_DISCOUNT = 0.20m;
    public const int MAX_ORDERS = 50;
    public const int OPEN_HOUR = 7;
    public const int CLOSE_HOUR = 22;
}
```

#### `CafeMenu` (static constructor seed data):
| # | Đồ uống | S | M | L |
|---|---------|---|---|---|
| 1 | Cà phê đen | 25k | 30k | 35k |
| 2 | Cà phê sữa | 29k | 35k | 42k |
| 3 | Bạc xỉu | 32k | 39k | 45k |
| 4 | Trà sen | 35k | 42k | 49k |
| 5 | Trà đào | 39k | 45k | 55k |
| 6 | Matcha latte | 45k | 55k | 65k |

#### `CafeOrder`:
- `CreateOrder()` → nhập món, size, số lượng, tên KH
- Auto-generate mã đơn: `CF0001`, `CF0002`...
- Tính tiền (giá × qty + tax - discount)
- Lưu vào mảng history
- `ShowOrder(string orderId)` → xem chi tiết 1 đơn
- `CancelOrder(string orderId)` → hủy đơn (chỉ nếu chưa 5 phút)

#### `CafeStats`:
- Tổng doanh thu hôm nay
- Số đơn hàng
- Đồ uống bán chạy nhất
- Size phổ biến nhất
- Doanh thu trung bình / đơn
- Thời điểm cao điểm (nếu bonus)

#### `CafeUI`:
- Menu chính: Đặt món | Xem đơn | Thống kê | Thoát
- Tất cả Console.Write/Read nằm ở đây
- Format bảng đẹp, có icon/color

### Menu:

```
╔═══════════════════════════════════╗
║        ☕ Café 42                 ║
╠═══════════════════════════════════╣
║  1. 🛒 Đặt món                   ║
║  2. 📋 Xem đơn hàng              ║
║  3. 📊 Thống kê                  ║
║  4. 📜 Lịch sử đơn hàng          ║
║  0. 🚪 Đóng quán                 ║
╚═══════════════════════════════════╝
```

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| 5 static classes đúng vai trò | ⭐⭐⭐ Bắt buộc |
| Main() ≤ 10 dòng | ⭐⭐⭐ Bắt buộc |
| Static constructor seed data | ⭐⭐⭐ Bắt buộc |
| Config chỉ chứa const | ⭐⭐⭐ Bắt buộc |
| UI tách riêng (không tính toán) | ⭐⭐ Quan trọng |
| Auto-generate order ID | ⭐⭐ Quan trọng |
| Thống kê (doanh thu, best seller) | ⭐⭐ Quan trọng |
| Hủy đơn | ⭐ Bonus |
| Coupon code | ⭐ Bonus |
| Member discount | ⭐ Bonus |
