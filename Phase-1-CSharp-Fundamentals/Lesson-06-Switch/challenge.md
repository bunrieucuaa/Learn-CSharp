# 🏆 Lesson 06 — Challenge: Switch

> ⚠️ Bài thử thách tổng hợp If/Else + Switch!

---

## Challenge: Hệ thống đặt đồ ăn online 🍕

### Bối cảnh:
> "Xây dựng console app đặt đồ ăn. Khách chọn nhà hàng, chọn món, chọn size, áp dụng khuyến mãi, chọn phương thức thanh toán."

### Yêu cầu:

#### 1. Chọn nhà hàng (switch):

| # | Nhà hàng | Loại |
|---|---------|------|
| 1 | Pizza Hut | Pizza |
| 2 | KFC | Gà rán |
| 3 | Phở 24 | Việt Nam |
| 4 | Starbucks | Đồ uống |

#### 2. Menu mỗi nhà hàng (switch theo nhà hàng + món):

**Pizza Hut:** Hawaiian (S/M/L: 99k/149k/199k), Pepperoni (109k/159k/209k), Seafood (129k/179k/249k)

**KFC:** Gà rán 1pc (29k), 2pc (55k), Combo 1 (79k), Combo 2 (109k)

**Phở 24:** Phở bò (55k), Phở gà (50k), Bún bò (60k), Cơm (45k)

**Starbucks:** Americano (55k/65k/75k), Latte (65k/75k/85k), Frappuccino (75k/85k/95k)

*(Size S/M/L cho Pizza & Starbucks)*

#### 3. Tính phí giao hàng (switch expression + relational):

| Khoảng cách | Phí |
|-------------|-----|
| ≤ 3 km | Miễn phí (đơn ≥ 100k), 15,000 (đơn < 100k) |
| 3 - 5 km | 15,000 |
| 5 - 10 km | 25,000 |
| > 10 km | 35,000 + 3,000/km thêm |

#### 4. Phương thức thanh toán (switch):
- Tiền mặt: không phụ thu
- Thẻ ngân hàng: -5% (khuyến mãi)
- Ví MoMo: -10,000 cố định
- ZaloPay: -5,000 cố định

#### 5. Khuyến mãi (tuple pattern):
- (đơn ≥ 200k, khách mới) → giảm 15%
- (đơn ≥ 200k, khách cũ) → giảm 10%
- (đơn ≥ 100k, bất kỳ) → giảm 5%
- (đơn < 100k) → không giảm
- **Tối đa giảm 50,000 VNĐ**

#### 6. Thời gian giao hàng ước tính (switch expression):
- ≤ 3km → 15-20 phút
- ≤ 5km → 20-30 phút
- ≤ 10km → 30-45 phút
- \> 10km → 45-60 phút

### Yêu cầu kỹ thuật:

- ✅ Switch truyền thống cho menu lựa chọn
- ✅ Switch expression cho tính phí, khuyến mãi, thời gian
- ✅ Tuple pattern cho khuyến mãi
- ✅ Relational pattern cho phí giao hàng
- ✅ `when` guard cho điều kiện phụ
- ✅ Validate đầy đủ
- ✅ In hóa đơn đẹp

### Expected Output:

```
╔═══════════════════════════════════════════╗
║        🍕 ĐẶT ĐỒ ĂN ONLINE             ║
╠═══════════════════════════════════════════╣

Chọn nhà hàng (1-4): 1
→ Pizza Hut

Chọn món:
  1. Hawaiian    2. Pepperoni    3. Seafood
Món: 2
Size (S/M/L): L
Số lượng: 2

Khoảng cách (km): 4.5
Khách mới? (y/n): y
Thanh toán (1-Tiền mặt, 2-Thẻ, 3-MoMo, 4-ZaloPay): 3

╠═══════════════════════════════════════════╣
║  🧾 HÓA ĐƠN                             ║
║  Nhà hàng:   Pizza Hut                    ║
║  Món:        Pepperoni (L) × 2            ║
║  Giá món:              418,000 VNĐ       ║
║  Phí giao hàng:         15,000 VNĐ       ║
║  Giảm khách mới (15%):  -50,000 VNĐ      ║ ← cap 50k
║  Giảm MoMo:             -10,000 VNĐ      ║
║  ─────────────────────────────────────    ║
║  TỔNG:                  373,000 VNĐ      ║
║  Giao hàng: 20-30 phút 🚗               ║
╚═══════════════════════════════════════════╝
```

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Switch menu + món | ⭐⭐⭐ Bắt buộc |
| Switch expression cho phí/giảm/thời gian | ⭐⭐⭐ Bắt buộc |
| Tuple pattern cho khuyến mãi | ⭐⭐ Quan trọng |
| Relational pattern | ⭐⭐ Quan trọng |
| Validate đầy đủ | ⭐⭐ Quan trọng |
| Cap giảm giá 50k | ⭐ Bonus |
| Nhiều nhà hàng hoạt động | ⭐ Bonus |
