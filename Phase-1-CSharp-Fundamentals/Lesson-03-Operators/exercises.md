# ✏️ Lesson 03 — Bài tập: Operators

> ⚠️ **QUY TẮC**: Tự làm trước, chỉ xem gợi ý khi thật sự bí. Không có đáp án sẵn!

---

## Bài 1: Dự đoán Output 🔮

### Yêu cầu:

Dự đoán output của mỗi đoạn code **TRÊN GIẤY** trước, sau đó mới chạy thử kiểm chứng.

**Câu A:**
```csharp
int a = 10, b = 3;
Console.WriteLine(a / b); 3.333
Console.WriteLine(a % b); 1
Console.WriteLine((double)a / b); 3.3333333333333335
```

**Câu B:**
```csharp
int x = 5;
Console.WriteLine(x++);
Console.WriteLine(x);
Console.WriteLine(++x);
Console.WriteLine(x);
```

**Câu C:**
```csharp
int a = 10;
a += 5;
a *= 2;
a -= 8;
a /= 3;
a %= 4;
Console.WriteLine(a);
```

**Câu D:**
```csharp
bool result = (5 > 3) && (10 < 20) || (7 == 8);
Console.WriteLine(result);
```

**Câu E:**
```csharp
string? name = null;
int? age = 25;
Console.WriteLine(name ?? "Unknown");
Console.WriteLine(name?.Length ?? -1);
Console.WriteLine(age ?? 0);
Console.WriteLine(age.HasValue ? $"{age} tuổi" : "N/A");
```

### Gợi ý:

- Câu C: Tính từng bước một, viết ra giá trị `a` sau mỗi dòng
- Câu D: Nhớ thứ tự ưu tiên: `>`, `<`, `==` → `&&` → `||`

---

## Bài 2: Tách số thành chữ số 🔢

### Yêu cầu:

Cho một số nguyên **4 chữ số** (ví dụ: 2025). Viết chương trình:

1. Tách ra từng chữ số (hàng nghìn, trăm, chục, đơn vị)
2. Tính tổng các chữ số
3. Tạo số đảo ngược (2025 → 5202)
4. Kiểm tra tổng chữ số chẵn hay lẻ

### Gợi ý:

- Dùng `/` và `%` để tách chữ số
- Hàng đơn vị: `n % 10`
- Hàng chục: `(n / 10) % 10`
- Hàng trăm: `(n / 100) % 10`
- Hàng nghìn: `n / 1000`
- Số đảo: `đvị*1000 + chục*100 + trăm*10 + nghìn`

### Expected Output:

```
=== TÁCH SỐ 2025 ===
Hàng nghìn: 2
Hàng trăm:  0
Hàng chục:  2
Hàng đơn vị: 5
Tổng chữ số: 9
Số đảo ngược: 5202
Tổng chữ số là: Lẻ
```

---

## Bài 3: Chuyển đổi thời gian ⏰

### Yêu cầu:

Viết chương trình chuyển đổi:

**Phần A:** Cho tổng số giây → chuyển thành `ngày giờ phút giây`

Ví dụ: `90061 giây = 1 ngày, 1 giờ, 1 phút, 1 giây`

**Phần B:** Cho giờ, phút, giây → chuyển thành tổng số giây

### Gợi ý:

- 1 ngày = 86400 giây, 1 giờ = 3600 giây, 1 phút = 60 giây
- Dùng `/` để lấy phần nguyên, `%` để lấy phần dư
- Tách từ lớn → nhỏ: ngày trước, rồi giờ, phút, giây

### Expected Output:

```
=== CHUYỂN ĐỔI THỜI GIAN ===

Phần A: 90061 giây =
→ 1 ngày, 1 giờ, 1 phút, 1 giây

Phần B: 2 giờ, 30 phút, 45 giây =
→ 9045 giây
```

---

## Bài 4: Hệ thống xét tuyển đại học 🎓

### Yêu cầu:

Viết chương trình xét tuyển với các điều kiện:

**Input (hardcode):**
- Điểm 3 môn: Toán, Lý, Hóa (mỗi môn 0-10)
- Khu vực ưu tiên (1, 2, 3 hoặc null = không ưu tiên)
- Đối tượng ưu tiên (true/false)

**Điểm ưu tiên:**
- Khu vực 1: +0.75 điểm
- Khu vực 2: +0.50 điểm
- Khu vực 3: +0.25 điểm
- Null: +0 điểm
- Đối tượng ưu tiên: +1.0 điểm

**Điều kiện trúng tuyển:**
- Tổng điểm (sau ưu tiên) >= 22 **VÀ** không có môn nào dưới 5
- **HOẶC** tổng >= 27 (dù có môn dưới 5)

**Cần hiển thị:**
- Điểm từng môn
- Điểm ưu tiên
- Tổng điểm
- Kết quả: Trúng tuyển / Không trúng tuyển
- Lý do trúng/không trúng

### Gợi ý:

- Dùng `int?` cho khu vực ưu tiên (có thể null)
- Dùng `??` để xử lý khu vực null
- Dùng `&&`, `||` cho điều kiện phức tạp
- Dùng ternary cho hiển thị kết quả

### Expected Output:

```
=== KẾT QUẢ XÉT TUYỂN ===

Thí sinh: Nguyễn Văn A
Toán: 8.0 | Lý: 7.5 | Hóa: 9.0
Khu vực: 1 (+0.75)
Đối tượng ưu tiên: Có (+1.00)

Tổng điểm gốc:    24.50
Điểm ưu tiên:      1.75
Tổng điểm cuối:   26.25

✅ KẾT QUẢ: TRÚNG TUYỂN
Lý do: Tổng >= 22 và không có môn dưới 5
```

---

## Bài 5: Máy tính tiền siêu thị 🛒

### Yêu cầu:

Viết chương trình tính tiền thanh toán tại siêu thị:

**Sản phẩm (hardcode 4 sản phẩm):**

| Sản phẩm | Đơn giá | Số lượng |
|----------|---------|----------|
| Sữa tươi | 32,000 | 3 |
| Mì gói (lốc) | 65,000 | 2 |
| Trứng (vỉ) | 38,000 | 1 |
| Nước ngọt | 12,000 | 5 |

**Chính sách giảm giá:**
- Hóa đơn >= 500,000: giảm 10%
- Hóa đơn >= 300,000: giảm 5%
- Hóa đơn < 300,000: không giảm
- Thành viên (bool): giảm **thêm** 3%
- Coupon (string?): nếu có coupon "SALE50" → giảm thêm 50,000 cố định

**Thanh toán:**
- Khách đưa X đồng
- Tính tiền thừa
- Nếu đưa không đủ → thông báo thiếu bao nhiêu

### Gợi ý:

- Dùng `decimal` cho tiền
- Dùng `const` cho các ngưỡng giảm giá
- Dùng ternary cho điều kiện giảm giá
- Dùng `??` cho coupon
- So sánh `>`, `>=` để xét ngưỡng

### Expected Output (tương tự):

```
🛒 SIÊU THỊ ABC - HÓA ĐƠN THANH TOÁN

1. Sữa tươi      x3     96,000 VNĐ
2. Mì gói (lốc)  x2    130,000 VNĐ
3. Trứng (vỉ)    x1     38,000 VNĐ
4. Nước ngọt     x5     60,000 VNĐ
─────────────────────────────────
Tạm tính:              324,000 VNĐ
Giảm HD (5%):           -16,200 VNĐ
Giảm thành viên (3%):    -9,720 VNĐ
Coupon SALE50:           -50,000 VNĐ
─────────────────────────────────
TỔNG:                   248,080 VNĐ

Khách đưa:              300,000 VNĐ
Tiền thừa:               51,920 VNĐ
```

---

## Bài 6: FizzBuzz — Bài kinh điển 💪

### Yêu cầu:

In các số từ 1 đến 30, nhưng:

- Nếu chia hết cho **3** → in `"Fizz"`
- Nếu chia hết cho **5** → in `"Buzz"`
- Nếu chia hết cho **cả 3 và 5** → in `"FizzBuzz"`
- Còn lại → in số bình thường

### Gợi ý:

- Dùng `%` để kiểm tra chia hết
- Kiểm tra `%15` trước (hoặc `%3 && %5`)
- Thứ tự kiểm tra quan trọng!

### Expected Output:

```
1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz,
11, Fizz, 13, 14, FizzBuzz, 16, 17, Fizz, 19, Buzz,
Fizz, 22, 23, Fizz, Buzz, 26, Fizz, 28, 29, FizzBuzz
```

> 💡 FizzBuzz là câu hỏi phỏng vấn kinh điển! Nó test khả năng dùng `%`, `if/else`, và tư duy logic.
