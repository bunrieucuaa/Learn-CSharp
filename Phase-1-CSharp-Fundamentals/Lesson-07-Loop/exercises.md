# ✏️ Lesson 07 — Bài tập: Loop

---

## Bài 1: Dãy số Fibonacci 🔢

### Yêu cầu:
Nhập N, in ra N số Fibonacci đầu tiên: `0, 1, 1, 2, 3, 5, 8, 13, ...`

Quy tắc: `F(n) = F(n-1) + F(n-2)`

### Expected Output:
```
Nhập N: 10
Fibonacci: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34
```

---

## Bài 2: Số nguyên tố 🔍

### Yêu cầu:
**Phần A:** Nhập N, kiểm tra N có phải số nguyên tố không.

**Phần B:** In tất cả số nguyên tố từ 2 đến N.

### Gợi ý:
- Số nguyên tố: chỉ chia hết cho 1 và chính nó
- Kiểm tra: duyệt từ 2 đến √N, nếu chia hết cho bất kỳ số nào → không phải
- Dùng `Math.Sqrt(n)` hoặc `i * i <= n`

### Expected Output:
```
Nhập N: 30
Số nguyên tố từ 2 đến 30:
2  3  5  7  11  13  17  19  23  29
Tổng: 10 số
```

---

## Bài 3: Vẽ hình bằng vòng lặp ✨

### Yêu cầu:
Nhập N, vẽ các hình sau:

**A. Hình chữ nhật rỗng (N×N):**
```
★ ★ ★ ★ ★
★         ★
★         ★
★         ★
★ ★ ★ ★ ★
```

**B. Kim cương:**
```
    ★
   ★★★
  ★★★★★
 ★★★★★★★
★★★★★★★★★
 ★★★★★★★
  ★★★★★
   ★★★
    ★
```

**C. Chữ X:**
```
★       ★
 ★     ★
  ★   ★
   ★ ★
    ★
   ★ ★
  ★   ★
 ★     ★
★       ★
```

### Gợi ý:
- Hình chữ nhật rỗng: in ★ khi `i==0 || i==n-1 || j==0 || j==n-1`
- Kim cương: nửa trên + nửa dưới
- Chữ X: in ★ khi `j==i || j==n-1-i`

---

## Bài 4: Tính lãi kép 📈

### Yêu cầu:
Nhập số tiền gốc, lãi suất/năm, số năm. In bảng tăng trưởng theo từng năm.

**Công thức:** `Số dư = Gốc × (1 + lãi suất)^năm`

### Expected Output:
```
Tiền gốc: 10,000,000 VNĐ
Lãi suất: 8%/năm
Số năm: 5

Năm │ Số dư đầu năm  │ Lãi          │ Số dư cuối năm
────┼─────────────────┼──────────────┼────────────────
  1 │    10,000,000   │    800,000   │    10,800,000
  2 │    10,800,000   │    864,000   │    11,664,000
  3 │    11,664,000   │    933,120   │    12,597,120
  4 │    12,597,120   │  1,007,770   │    13,604,890
  5 │    13,604,890   │  1,088,391   │    14,693,281

Tổng lãi: 4,693,281 VNĐ (+46.93%)
```

---

## Bài 5: Game Hangman đơn giản 🎮

### Yêu cầu:
Máy chọn 1 từ ngẫu nhiên từ danh sách (hardcode 10 từ). User đoán từng chữ cái:
- Đoán đúng → hiện chữ cái
- Đoán sai → mất 1 mạng (tối đa 6 mạng)
- Thắng khi đoán hết từ, thua khi hết mạng

### Gợi ý:
- Dùng `char[]` để lưu trạng thái hiện tại (ban đầu toàn `_`)
- Dùng `string.Contains(char)` để kiểm tra
- Dùng `while` loop

### Expected Output:
```
🎮 HANGMAN — Đoán từ!
Từ: _ _ _ _ _    ❤️❤️❤️❤️❤️❤️

Đoán chữ cái: a
✅ Đúng! _ A _ _ _

Đoán chữ cái: e
❌ Sai! Còn 5 mạng   ❤️❤️❤️❤️❤️

Đoán chữ cái: p
✅ Đúng! _ A P _ _

...

🎉 THẮNG! Từ là: HAPPY
Bạn đoán trong 8 lần, còn 3 mạng!
```

---

## Bài 6: In lịch tháng 📅

### Yêu cầu:
Nhập tháng và năm, in lịch tháng đó.

### Gợi ý:
- Dùng `new DateTime(year, month, 1).DayOfWeek` để biết thứ mấy ngày đầu tháng
- Dùng `DateTime.DaysInMonth(year, month)` để biết số ngày
- Dùng 2 vòng lặp: 1 cho khoảng trắng đầu, 1 cho các ngày
- Xuống dòng sau mỗi Chủ nhật

### Expected Output:
```
       Tháng 5 / 2025
  CN   T2   T3   T4   T5   T6   T7
                        1    2    3
   4    5    6    7    8    9   10
  11   12   13   14   15   16   17
  18   19   20   21   22   23   24
  25   26   27   28   29   30   31
```
