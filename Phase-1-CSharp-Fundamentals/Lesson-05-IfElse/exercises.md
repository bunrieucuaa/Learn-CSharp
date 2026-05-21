# ✏️ Lesson 05 — Bài tập: If/Else

> ⚠️ Tự làm trước! Nhận input từ người dùng, validate đầy đủ.

---

## Bài 1: Năm nhuận 📅

### Yêu cầu:
Nhập một năm, kiểm tra có phải **năm nhuận** không.

**Quy tắc năm nhuận:**
- Chia hết cho 4 **VÀ** không chia hết cho 100
- **HOẶC** chia hết cho 400

### Gợi ý:
- `year % 4 == 0 && year % 100 != 0` hoặc `year % 400 == 0`
- Validate: năm > 0

### Expected Output:
```
Nhập năm: 2024
✅ 2024 là năm nhuận!

Nhập năm: 1900
❌ 1900 KHÔNG phải năm nhuận.

Nhập năm: 2000
✅ 2000 là năm nhuận!
```

---

## Bài 2: Phân loại tam giác 📐

### Yêu cầu:
Nhập 3 cạnh, kiểm tra:
1. Có tạo thành tam giác không? (tổng 2 cạnh > cạnh còn lại)
2. Nếu có → phân loại: Đều, Cân, Vuông, Thường

### Gợi ý:
- Đều: `a == b && b == c`
- Cân: 2 cạnh bằng nhau
- Vuông: `a² + b² = c²` (cần kiểm tra cả 3 trường hợp)
- Dùng `Math.Pow()` hoặc `a*a + b*b == c*c`

### Expected Output:
```
Nhập cạnh a: 3
Nhập cạnh b: 4
Nhập cạnh c: 5
✅ Tam giác VUÔNG
```

---

## Bài 3: Máy bán hàng tự động 🥤

### Yêu cầu:
Mô phỏng máy bán hàng:
1. Hiện menu sản phẩm (5 sản phẩm, giá khác nhau)
2. User chọn sản phẩm
3. User nhập số tiền bỏ vào
4. Kiểm tra:
   - Đủ tiền → trả hàng + tiền thừa
   - Thiếu tiền → thông báo thiếu bao nhiêu
   - Chọn sai → thông báo lỗi

### Expected Output:
```
🥤 MÁY BÁN HÀNG TỰ ĐỘNG

1. Coca Cola    - 10,000 VNĐ
2. Pepsi        - 10,000 VNĐ
3. Nước suối    -  5,000 VNĐ
4. Trà xanh     - 12,000 VNĐ
5. Cà phê lon   - 15,000 VNĐ

Chọn sản phẩm (1-5): 4
Bỏ tiền vào: 20000

✅ Đã mua: Trà xanh
💰 Tiền thừa: 8,000 VNĐ
```

---

## Bài 4: Tính cước điện thoại 📱

### Yêu cầu:
Nhập số phút gọi, tính cước theo bảng:

| Phút | Giá |
|------|-----|
| 0 - 50 | 500 VNĐ/phút |
| 51 - 150 | 300 VNĐ/phút |
| > 150 | 200 VNĐ/phút |

**Lưu ý:** Tính lũy tiến (50 phút đầu × 500, 100 phút tiếp × 300, phần còn lại × 200)

### Gợi ý:
- Giống cách tính thuế lũy tiến
- Validate: số phút >= 0

### Expected Output:
```
Nhập số phút gọi: 200

=== CHI TIẾT CƯỚC ===
50 phút đầu × 500:    25,000 VNĐ
100 phút tiếp × 300:  30,000 VNĐ
50 phút còn lại × 200: 10,000 VNĐ
────────────────────────────────
TỔNG CƯỚC:             65,000 VNĐ
```

---

## Bài 5: Đăng nhập với điều kiện phức tạp 🔐

### Yêu cầu:
Mô phỏng hệ thống đăng nhập với các điều kiện:

1. Nhập username và password
2. Kiểm tra:
   - Username = "admin", Password = "1234" → Đăng nhập admin
   - Username = "user", Password = "abcd" → Đăng nhập user
   - Username đúng, password sai → "Sai mật khẩu"
   - Username sai → "Tài khoản không tồn tại"
3. Tối đa **3 lần** thử
4. Sau khi đăng nhập admin: hiện quyền đặc biệt
5. Sau khi đăng nhập user: hiện quyền giới hạn

### Gợi ý:
- So sánh string không phân biệt hoa/thường cho username
- Password phải phân biệt hoa/thường
- Dùng guard clause hoặc nested if

---

## Bài 6: Tính tiền gửi xe 🅿️

### Yêu cầu:
Nhập thời gian gửi xe (giờ vào, phút vào, giờ ra, phút ra). Tính tiền gửi:

| Loại xe | Giá/lượt (≤2h) | Mỗi giờ thêm | Qua đêm (22h-6h) |
|---------|----------------|---------------|-------------------|
| Xe máy | 5,000 | 2,000 | 10,000 (cố định) |
| Ô tô | 20,000 | 5,000 | 40,000 (cố định) |
| Xe đạp | 3,000 | 0 (miễn phí) | 5,000 (cố định) |

### Gợi ý:
- Tính tổng phút = (giờ ra - giờ vào) × 60 + (phút ra - phút vào)
- Chuyển sang giờ: tổng phút / 60 (làm tròn lên)
- Kiểm tra qua đêm: giờ vào >= 22 hoặc giờ ra <= 6
- Dùng `Math.Ceiling()` để làm tròn lên

### Expected Output:
```
=== TÍNH TIỀN GỬI XE ===

Loại xe (1-Xe máy, 2-Ô tô, 3-Xe đạp): 1
Giờ vào (HH MM): 08 30
Giờ ra (HH MM): 12 45

Thời gian gửi: 4 giờ 15 phút ≈ 5 giờ
Giá 2 giờ đầu:         5,000 VNĐ
3 giờ thêm × 2,000:    6,000 VNĐ
────────────────────────────
TỔNG:                  11,000 VNĐ
```
