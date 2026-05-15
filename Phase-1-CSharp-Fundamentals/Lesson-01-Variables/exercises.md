# ✏️ Lesson 01 — Bài tập: Variables

> ⚠️ **QUY TẮC**: Tự làm trước, chỉ xem gợi ý khi thật sự bí. Không có đáp án sẵn — hãy tự code và chạy thử!

---

## Bài 1: Thẻ sinh viên 🎓

### Yêu cầu:

Tạo chương trình in ra **thẻ sinh viên** với các thông tin sau:

- Họ tên (string)
- Mã sinh viên (string)
- Tuổi (int)
- GPA (double)
- Đang theo học (bool)
- Khoa (string)

### Gợi ý:

- Khai báo 6 biến với kiểu dữ liệu phù hợp
- Dùng `Console.WriteLine` và **string interpolation** (`$"..."`) để in
- Trang trí output cho đẹp (dùng `===` hoặc `---`)

### Expected Output (tương tự):

```
=============================
    THẺ SINH VIÊN
=============================
Họ tên:     Nguyễn Văn A
MSSV:       SV2024001
Tuổi:       20
GPA:        3.65
Đang học:   True
Khoa:       Công nghệ thông tin
=============================
```

---

## Bài 2: Đổi nhiệt độ 🌡️

### Yêu cầu:

Viết chương trình chuyển đổi nhiệt độ từ **Celsius sang Fahrenheit**.

### Công thức:

```
°F = (°C × 9/5) + 32
```

### Gợi ý:

- Tạo biến `celsius` kiểu `double`, gán giá trị bất kỳ (ví dụ: 36.5)
- Tính `fahrenheit` theo công thức
- In kết quả với 2 chữ số thập phân (dùng `:F2`)

### Expected Output:

```
Nhiệt độ: 36.50°C = 97.70°F
```

### Thử thêm:

- Thử với `celsius = 0` → kết quả phải là `32.00°F`
- Thử với `celsius = 100` → kết quả phải là `212.00°F`

---

## Bài 3: Máy tính lương 💰

### Yêu cầu:

Tính lương nhân viên với các thông tin:

- Lương cơ bản (decimal)
- Số ngày công (int)
- Phụ cấp (decimal)
- Thuế thu nhập cá nhân: 10%
- Bảo hiểm: 8%

### Cần tính:

1. Lương gross = (Lương cơ bản / 26) × Số ngày công + Phụ cấp
2. Thuế = Lương gross × 10%
3. Bảo hiểm = Lương gross × 8%
4. Lương net = Lương gross - Thuế - Bảo hiểm

### Gợi ý:

- Dùng `const decimal` cho thuế suất và tỷ lệ bảo hiểm
- Dùng `decimal` cho tất cả số tiền
- Dùng format `N0` để hiển thị số tiền có dấu phẩy

### Expected Output (tương tự):

```
=== BẢNG LƯƠNG THÁNG 5/2025 ===
Nhân viên:    Trần Văn B
Lương CB:     12,000,000 VNĐ
Ngày công:    24 ngày
Phụ cấp:      2,000,000 VNĐ

Lương Gross:  13,076,923 VNĐ
Thuế (10%):    1,307,692 VNĐ
BH (8%):       1,046,154 VNĐ
---------------------------------
Lương Net:    10,723,077 VNĐ
```

---

## Bài 4: Kiểm tra hiểu `var` 🔍

### Yêu cầu:

Trả lời các câu hỏi sau bằng cách **viết code và chạy thử**:

**Câu A:** Đoạn code sau có chạy được không? Vì sao?

```csharp
var x = 10;
x = 20;
x = 30;
```

**Câu B:** Đoạn code sau có chạy được không? Vì sao?

```csharp
var x = 10;
x = "hello";
```

**Câu C:** Đoạn code sau có chạy được không? Vì sao?

```csharp
var x;
x = 10;
```

**Câu D:** Đoạn code sau, `result` là kiểu gì?

```csharp
var result = 10 / 3;
Console.WriteLine(result);
```

**Câu E:** Đoạn code sau, `result` là kiểu gì? Output là bao nhiêu?

```csharp
var result = 10.0 / 3;
Console.WriteLine(result);
```

### Gợi ý:

- Dùng `.GetType().Name` để kiểm tra kiểu
- Chú ý sự khác biệt giữa phép chia số nguyên và số thực

---

## Bài 5: Đổi tiền tệ 💱

### Yêu cầu:

Viết chương trình quy đổi tiền tệ:

- Cho số tiền VNĐ ban đầu
- Quy đổi sang: USD, EUR, JPY
- Dùng tỷ giá cố định (const)

### Gợi ý:

- Tỷ giá tham khảo:
  - 1 USD = 25,000 VNĐ
  - 1 EUR = 27,500 VNĐ
  - 1 JPY = 170 VNĐ
- Dùng `const decimal` cho tỷ giá
- In kết quả với 2 chữ số thập phân

### Expected Output:

```
=== ĐỔI TIỀN TỆ ===
Số tiền: 10,000,000 VNĐ

→ USD: 400.00
→ EUR: 363.64
→ JPY: 58,823.53
```

---

## Bài 6: Swap 3 biến 🔄

### Yêu cầu:

Cho 3 biến `a = 1`, `b = 2`, `c = 3`.

Viết code để xoay vòng giá trị: `a → b → c → a`

Sau khi xoay: `a = 3`, `b = 1`, `c = 2`

### Gợi ý:

- Cần bao nhiêu biến tạm (`temp`)?
- Thứ tự gán rất quan trọng — nếu sai sẽ mất giá trị!

### Expected Output:

```
Trước: a = 1, b = 2, c = 3
Sau:   a = 3, b = 1, c = 2
```

### Bonus:

- Thử làm bằng Tuple: `(a, b, c) = (?, ?, ?)`
