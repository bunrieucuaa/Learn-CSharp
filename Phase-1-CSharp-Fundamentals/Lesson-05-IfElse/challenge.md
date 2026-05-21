# 🏆 Lesson 05 — Challenge: If/Else

> ⚠️ Bài thử thách thực tế!

---

## Challenge: Hệ thống đặt vé xem phim 🎬

### Bối cảnh:
> "Em viết console app cho rạp phim. Khách chọn phim, suất chiếu, loại ghế, nhập thông tin → tính giá vé và in vé."

### Yêu cầu:

#### 1. Chọn phim (hiện danh sách):

| # | Phim | Thể loại | Thời lượng | Rated |
|---|------|---------|-----------|-------|
| 1 | Avengers: Endgame | Action | 182 phút | 13+ |
| 2 | Inside Out 2 | Animation | 100 phút | P (mọi lứa tuổi) |
| 3 | Dune: Part Two | Sci-Fi | 166 phút | 16+ |
| 4 | Deadpool 3 | Action/Comedy | 127 phút | 18+ |

#### 2. Chọn suất chiếu:
- Sáng (9:00-12:00): Giá gốc × 0.8 (giảm 20%)
- Chiều (13:00-17:00): Giá gốc
- Tối (18:00-21:00): Giá gốc × 1.2 (tăng 20%)
- Khuya (22:00+): Giá gốc × 1.5 (tăng 50%) — chỉ phim 18+

#### 3. Chọn loại ghế:

| Loại | Giá gốc |
|------|---------|
| Thường | 75,000 |
| VIP | 120,000 |
| Couple (2 người) | 200,000 |
| Sweetbox (2 người) | 280,000 |

#### 4. Nhập thông tin:
- Tên khách
- Tuổi (kiểm tra rated phim)
- Số lượng vé
- Có thẻ thành viên không? (y/n)
- Mã giảm giá (nullable): "NEWMEMBER" → giảm 15%, "STUDENT" → giảm 10%

#### 5. Tính giá:
- Giá = Giá ghế × Hệ số suất chiếu × Số vé
- Giảm giá thành viên: 5% (không cộng dồn với coupon)
- Giảm giá coupon: theo mã
- **Chọn giảm giá LỚN HƠN** (không cộng dồn)
- Thứ 3 hàng tuần: giảm thêm 30% (cộng dồn)

#### 6. Validation:
- Tuổi < rated → từ chối bán vé
- Suất khuya + phim không phải 18+ → không cho chọn
- Couple/Sweetbox: số vé phải **chẵn**
- Số vé tối đa: 10

### Expected Output:

```
╔═══════════════════════════════════════╗
║         🎬 VÉ XEM PHIM              ║
╠═══════════════════════════════════════╣
║  Phim:     Avengers: Endgame         ║
║  Suất:     Tối (19:00)               ║
║  Ghế:      VIP                       ║
║  Số vé:    2                         ║
╠═══════════════════════════════════════╣
║  Khách:    Nguyễn Văn A              ║
║  Tuổi:     25 ✅                     ║
╠═══════════════════════════════════════╣
║  Giá ghế:         120,000 × 2       ║
║  Hệ số tối:       ×1.2              ║
║  Tạm tính:        288,000 VNĐ       ║
║  Giảm NEWMEMBER:  -43,200 VNĐ (15%) ║
╠═══════════════════════════════════════╣
║  TỔNG:            244,800 VNĐ       ║
╚═══════════════════════════════════════╝
```

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Kiểm tra tuổi theo rated | ⭐⭐⭐ Bắt buộc |
| Tính giá theo suất chiếu | ⭐⭐⭐ Bắt buộc |
| Validation đầy đủ | ⭐⭐⭐ Bắt buộc |
| Giảm giá (chọn cái lớn hơn) | ⭐⭐ Quan trọng |
| Suất khuya chỉ cho phim 18+ | ⭐⭐ Quan trọng |
| Couple/Sweetbox vé chẵn | ⭐ Bonus |
| Thứ 3 giảm thêm 30% | ⭐ Bonus |
