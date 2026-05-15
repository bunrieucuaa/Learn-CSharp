# 🏆 Lesson 03 — Challenge: Operators

> ⚠️ **Bài thử thách thực tế!** Tự suy nghĩ trước khi xem gợi ý.

---

## Challenge: Hệ thống tính cước taxi 🚕

### Bối cảnh:

Bạn được giao task tại công ty gọi xe:

> "Em viết console app tính cước phí taxi cho khách dựa trên quãng đường, thời gian, loại xe, và các phụ thu. Cần xử lý nhiều điều kiện kết hợp."

### Quy tắc tính cước:

#### Bảng giá theo loại xe:

| Loại xe | Mở cửa (0.5km đầu) | Giá/km (tiếp theo) |
|---------|---------------------|---------------------|
| Xe 4 chỗ | 12,000 | 13,500 |
| Xe 7 chỗ | 14,000 | 15,800 |
| Xe Premium | 20,000 | 22,000 |

#### Giá theo km lũy tiến (áp dụng sau 0.5km đầu):

| Khoảng cách | Hệ số |
|-------------|-------|
| 0.5 — 30 km | ×1.0 (giá gốc) |
| 30 — 50 km | ×0.85 (giảm 15%) |
| > 50 km | ×0.75 (giảm 25%) |

#### Phụ thu:

| Điều kiện | Phụ thu |
|-----------|---------|
| Ban đêm (22h — 5h) | +30% tổng cước |
| Giờ cao điểm (7h-9h, 17h-19h) | +20% tổng cước |
| Chờ đợi (phút) | 1,000 VNĐ/phút |
| Qua cầu/hầm | Cộng phí cầu hầm (nếu có) |

#### Giảm giá:

| Điều kiện | Giảm |
|-----------|------|
| Thành viên app | -5% |
| Coupon (nếu có) | -10% |
| Quãng đường > 100km | -thêm 5% |
| **Lưu ý:** Tối đa chỉ giảm 20% |

### Input (hardcode — tạo 2 chuyến khác nhau):

**Chuyến 1:**
- Loại xe: 4 chỗ
- Khoảng cách: 15.5 km
- Giờ đón: 18 (giờ cao điểm)
- Thời gian chờ: 5 phút
- Qua cầu: không
- Thành viên: có
- Coupon: "GIAM10"

**Chuyến 2:**
- Loại xe: Premium
- Khoảng cách: 45.0 km
- Giờ đón: 23 (ban đêm)
- Thời gian chờ: 0 phút
- Phí cầu: 20,000
- Thành viên: không
- Coupon: null

### Yêu cầu kỹ thuật:

- ✅ Dùng `decimal` cho tiền, `double` cho khoảng cách
- ✅ Dùng `const` cho bảng giá, tỷ lệ phụ thu
- ✅ Dùng `bool` cho điều kiện (ban đêm, giờ cao điểm, thành viên)
- ✅ Dùng `string?` cho coupon (nullable)
- ✅ Dùng `&&`, `||` cho logic phức tạp
- ✅ Dùng `? :` (ternary) cho phụ thu
- ✅ Dùng `??` cho nullable
- ✅ Dùng `Math.Min()` để giới hạn giảm giá tối đa 20%
- ✅ Tính cước lũy tiến đúng (chia thành từng khoảng km)

### Expected Output (tương tự):

```
╔════════════════════════════════════════════════════╗
║              HÓA ĐƠN CƯỚC TAXI                    ║
╠════════════════════════════════════════════════════╣

━━━ CHUYẾN 1 ━━━
Loại xe:         4 chỗ
Khoảng cách:     15.50 km
Giờ đón:         18:00 (Giờ cao điểm)
Thời gian chờ:   5 phút

Chi tiết cước:
  Mở cửa (0.5km):              12,000 VNĐ
  15.0 km × 13,500:           202,500 VNĐ
  ─────────────────────────────────────
  Cước cơ bản:                214,500 VNĐ
  Phụ thu cao điểm (+20%):     42,900 VNĐ
  Phí chờ (5 phút):             5,000 VNĐ
  ─────────────────────────────────────
  Tổng trước giảm:            262,400 VNĐ
  Giảm thành viên (-5%):      -13,120 VNĐ
  Giảm coupon GIAM10 (-10%):  -26,240 VNĐ
  (Tổng giảm: 15% — trong giới hạn 20%)
  ─────────────────────────────────────
  TỔNG CƯỚC:                  223,040 VNĐ

━━━ CHUYẾN 2 ━━━
...

╚════════════════════════════════════════════════════╝
```

---

### 💡 Gợi ý (chỉ xem khi bí):

<details>
<summary>Gợi ý 1: Tính cước lũy tiến</summary>

```csharp
double distance = 45.0;
double firstKm = 0.5;  // Mở cửa
double remaining = distance - firstKm;  // 44.5 km

// Chia thành các khoảng
double tier1Km = Math.Min(remaining, 29.5);        // 0.5 → 30km: tối đa 29.5
double tier2Km = Math.Min(Math.Max(remaining - 29.5, 0), 20);  // 30 → 50km: tối đa 20
double tier3Km = Math.Max(remaining - 49.5, 0);     // > 50km

decimal fare = openFee
    + (decimal)tier1Km * pricePerKm * 1.0m
    + (decimal)tier2Km * pricePerKm * 0.85m
    + (decimal)tier3Km * pricePerKm * 0.75m;
```

</details>

<details>
<summary>Gợi ý 2: Kiểm tra giờ cao điểm / ban đêm</summary>

```csharp
int hour = 18;
bool isNight = hour >= 22 || hour < 5;
bool isPeakHour = (hour >= 7 && hour < 9) || (hour >= 17 && hour < 19);
// Lưu ý: ban đêm và cao điểm KHÔNG trùng nhau
```

</details>

<details>
<summary>Gợi ý 3: Giới hạn giảm giá tối đa</summary>

```csharp
decimal totalDiscountRate = 0m;
if (isMember) totalDiscountRate += 0.05m;
if (hasCoupon) totalDiscountRate += 0.10m;
if (distance > 100) totalDiscountRate += 0.05m;

// Giới hạn tối đa 20%
totalDiscountRate = Math.Min(totalDiscountRate, 0.20m);
decimal discountAmount = totalBeforeDiscount * totalDiscountRate;
```

</details>

---

### 🎯 Tiêu chí đánh giá:

| Tiêu chí | Mức độ |
|----------|--------|
| Tính cước lũy tiến đúng | ⭐⭐⭐ Bắt buộc |
| Xử lý phụ thu (đêm/cao điểm) | ⭐⭐⭐ Bắt buộc |
| Giới hạn giảm giá tối đa 20% | ⭐⭐⭐ Bắt buộc |
| Dùng đúng operators | ⭐⭐ Quan trọng |
| Xử lý nullable (coupon) | ⭐⭐ Quan trọng |
| Dùng const cho hằng số | ⭐⭐ Quan trọng |
| Output đẹp, format tiền | ⭐ Bonus |
| Tính cho 2 chuyến | ⭐ Bonus |
