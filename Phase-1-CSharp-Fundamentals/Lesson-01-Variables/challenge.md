# 🏆 Lesson 01 — Challenge: Variables

> ⚠️ **Đây là bài thử thách!** Mô phỏng bài toán thực tế ở công ty.
> Tự suy nghĩ và code trước khi hỏi gợi ý.

---

## Challenge: Hệ thống quản lý đơn hàng mini 🛒

### Bối cảnh:

Bạn vừa vào intern ở một công ty thương mại điện tử. Leader giao cho bạn task đầu tiên:

> "Em viết một chương trình console đơn giản để in ra **phiếu xác nhận đơn hàng** cho khách hàng. Chưa cần database hay input, cứ hardcode dữ liệu trước."

### Yêu cầu chi tiết:

Tạo chương trình in ra phiếu đơn hàng gồm:

**1. Thông tin khách hàng:**
- Tên khách hàng
- Số điện thoại
- Địa chỉ giao hàng
- Email

**2. Thông tin đơn hàng:**
- Mã đơn hàng (ví dụ: "ORD-20250511-001")
- Ngày đặt hàng
- 3 sản phẩm, mỗi sản phẩm gồm: tên, đơn giá, số lượng

**3. Tính toán:**
- Tạm tính (subtotal) = tổng tiền 3 sản phẩm
- Phí ship (cố định, dùng const)
- Giảm giá: nếu subtotal > 500,000 thì giảm 5% trên subtotal (dùng biến bool để đánh dấu)
- Tổng cộng = Tạm tính - Giảm giá + Phí ship

**4. Trạng thái:**
- Trạng thái đơn hàng (string): "Đang xử lý"
- Đã thanh toán (bool): false

### Yêu cầu kỹ thuật:

- ✅ Dùng **đúng kiểu dữ liệu** cho từng biến (int, string, decimal, bool, const...)
- ✅ Dùng **string interpolation** (`$"..."`)
- ✅ Dùng **const** cho các giá trị cố định
- ✅ Format tiền tệ có dấu phẩy (`N0`)
- ✅ Đặt tên biến theo **camelCase**, có ý nghĩa rõ ràng
- ✅ Comment code giải thích

### Expected Output (tương tự):

```
╔═══════════════════════════════════════════════════╗
║           PHIẾU XÁC NHẬN ĐƠN HÀNG               ║
╠═══════════════════════════════════════════════════╣
║ Mã đơn hàng:  ORD-20250511-001                   ║
║ Ngày đặt:     11/05/2025                          ║
╠═══════════════════════════════════════════════════╣
║ THÔNG TIN KHÁCH HÀNG                             ║
║ Họ tên:       Nguyễn Thị C                        ║
║ SĐT:          0901234567                          ║
║ Email:         nguyenc@email.com                   ║
║ Địa chỉ:      123 Lê Lợi, Q.1, TP.HCM            ║
╠═══════════════════════════════════════════════════╣
║ CHI TIẾT ĐƠN HÀNG                                ║
║ 1. Áo thun       x2          250,000 VNĐ         ║
║ 2. Quần jean     x1          450,000 VNĐ         ║
║ 3. Giày sneaker  x1          850,000 VNĐ         ║
╠═══════════════════════════════════════════════════╣
║ Tạm tính:                  1,800,000 VNĐ         ║
║ Giảm giá (5%):               -90,000 VNĐ         ║
║ Phí ship:                     30,000 VNĐ         ║
╠═══════════════════════════════════════════════════╣
║ TỔNG CỘNG:                 1,740,000 VNĐ         ║
║ Trạng thái:    Đang xử lý                        ║
║ Thanh toán:    Chưa thanh toán                    ║
╚═══════════════════════════════════════════════════╝
```

---

### 💡 Gợi ý (chỉ xem khi bí):

<details>
<summary>Gợi ý 1: Biến nào cần khai báo?</summary>

Bạn sẽ cần khoảng 15-20 biến:
- String: tên, sđt, email, địa chỉ, mã đơn, ngày, trạng thái, tên sản phẩm
- Int: số lượng
- Decimal: đơn giá, subtotal, discount, ship, total
- Bool: đã thanh toán, có đủ điều kiện giảm giá
- Const: phí ship, tỷ lệ giảm giá

</details>

<details>
<summary>Gợi ý 2: Tính giảm giá thế nào?</summary>

```csharp
bool isEligibleForDiscount = subtotal > 500000m;
decimal discountAmount = isEligibleForDiscount ? subtotal * DISCOUNT_RATE : 0m;
```

Chưa học toán tử `? :` (ternary)? Bạn có thể dùng if/else:

```csharp
decimal discountAmount = 0m;
if (subtotal > 500000m)
{
    discountAmount = subtotal * DISCOUNT_RATE;
}
```

</details>

<details>
<summary>Gợi ý 3: In bool thành text thế nào?</summary>

```csharp
bool isPaid = false;
string paidText = isPaid ? "Đã thanh toán" : "Chưa thanh toán";
Console.WriteLine($"Thanh toán: {paidText}");
```

</details>

---

### 🎯 Tiêu chí đánh giá:

| Tiêu chí | Mức độ |
|----------|--------|
| Chọn đúng kiểu dữ liệu | ⭐⭐⭐ Quan trọng |
| Dùng const cho giá trị cố định | ⭐⭐ Quan trọng |
| Đặt tên biến rõ ràng, camelCase | ⭐⭐ Quan trọng |
| String interpolation | ⭐⭐ Quan trọng |
| Format số tiền đẹp | ⭐ Bonus |
| Output trình bày đẹp | ⭐ Bonus |
| Comment code đầy đủ | ⭐ Bonus |
