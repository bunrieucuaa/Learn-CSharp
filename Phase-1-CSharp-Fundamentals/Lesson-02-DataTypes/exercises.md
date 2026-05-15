# ✏️ Lesson 02 — Bài tập: Data Types

> ⚠️ **QUY TẮC**: Tự làm trước, chỉ xem gợi ý khi thật sự bí. Không có đáp án sẵn!

---

## Bài 1: Thám tử kiểu dữ liệu 🔍

### Yêu cầu:

Viết chương trình nhận các giá trị sau và **in ra kiểu dữ liệu thực tế** của chúng:

```csharp
var a = 42;
var b = 3.14;
var c = 3.14f;
var d = 3.14m;
var e = true;
var f = 'X';
var g = "Hello";
var h = 100L;
```

### Gợi ý:

- Dùng `.GetType().Name` để lấy tên kiểu
- In theo format: `a = 42 → kiểu: Int32`

### Expected Output:

```
a = 42      → kiểu: Int32
b = 3.14    → kiểu: Double
c = 3.14    → kiểu: Single    ← float tên thật là Single
d = 3.14    → kiểu: Decimal
e = True    → kiểu: Boolean
f = X       → kiểu: Char
g = Hello   → kiểu: String
h = 100     → kiểu: Int64     ← long tên thật là Int64
```

---

## Bài 2: Máy tính BMI 🏋️

### Yêu cầu:

Viết chương trình tính chỉ số BMI (Body Mass Index):

```
BMI = Cân nặng (kg) / (Chiều cao (m))²
```

**Phân loại:**
- BMI < 18.5 → "Thiếu cân"
- 18.5 ≤ BMI < 25 → "Bình thường"
- 25 ≤ BMI < 30 → "Thừa cân"
- BMI ≥ 30 → "Béo phì"

### Gợi ý:

- Dùng `double` cho cân nặng và chiều cao
- Dùng `Math.Pow(height, 2)` hoặc `height * height` để bình phương
- Format BMI với 2 chữ số thập phân `:F2`
- Dùng `if/else` để phân loại

### Expected Output:

```
=== MÁY TÍNH BMI ===
Cân nặng: 70.0 kg
Chiều cao: 1.75 m
BMI: 22.86
Phân loại: Bình thường ✅
```

---

## Bài 3: Ép kiểu thực hành 🔄

### Yêu cầu:

Dự đoán output của từng đoạn code, **sau đó mới chạy thử** để kiểm chứng:

**Câu A:**
```csharp
double x = 7.0 / 2;
Console.WriteLine(x);
```

**Câu B:**
```csharp
int x = 7 / 2;
Console.WriteLine(x);
```

**Câu C:**
```csharp
double x = 7 / 2;
Console.WriteLine(x);
```

**Câu D:**
```csharp
int x = (int)7.9;
Console.WriteLine(x);
```

**Câu E:**
```csharp
byte a = 200;
byte b = 100;
// Dòng dưới có chạy được không?
byte c = (byte)(a + b);
Console.WriteLine(c);
```

**Câu F:**
```csharp
string s = "123abc";
bool ok = int.TryParse(s, out int val);
Console.WriteLine($"ok = {ok}, val = {val}");
```

### Gợi ý:

- Viết dự đoán ra giấy/comment trước
- Chú ý: `int / int = int` (cắt thập phân)
- Chú ý: `(int)7.9` cắt hay làm tròn?
- Chú ý: `200 + 100 = 300` → byte max = 255 → overflow!

---

## Bài 4: Chuyển đổi nhiệt độ nâng cao 🌡️

### Yêu cầu:

Viết chương trình chuyển đổi giữa **3 đơn vị** nhiệt độ: Celsius, Fahrenheit, Kelvin.

**Công thức:**
```
°F = (°C × 9/5) + 32
K  = °C + 273.15
```

**Yêu cầu kỹ thuật:**
1. Nhập nhiệt độ Celsius (hardcode, chưa cần input)
2. Tính ra Fahrenheit và Kelvin
3. In kết quả với 2 chữ số thập phân
4. **Quan trọng:** Phép `9/5` phải cho kết quả `1.8`, **không phải `1`**!

### Gợi ý:

- ⚠️ `9 / 5 = 1` (integer division!) → phải viết `9.0 / 5` hoặc ép kiểu
- Dùng `const` cho 273.15 và 32

### Expected Output:

```
=== CHUYỂN ĐỔI NHIỆT ĐỘ ===
Celsius:    37.50°C
Fahrenheit: 99.50°F
Kelvin:     310.65 K
```

---

## Bài 5: Hệ thống nhập điểm học sinh 📊

### Yêu cầu:

Nhập điểm 5 môn của học sinh (hardcode) và tính:
1. Tổng điểm
2. Trung bình (phải có thập phân!)
3. Điểm cao nhất
4. Điểm thấp nhất
5. Xếp loại (Giỏi ≥ 8.0, Khá ≥ 6.5, TB ≥ 5.0, Yếu < 5.0)

### Gợi ý:

- Dùng `int` cho điểm từng môn (0-10)
- Tính trung bình: **cẩn thận integer division!**
- Dùng `Math.Max()` và `Math.Min()` cho điểm cao/thấp nhất
- Hoặc tự so sánh bằng `if`

### Expected Output:

```
=== BẢNG ĐIỂM HỌC SINH ===
Họ tên: Lê Văn C

Toán:    8
Lý:      7
Hóa:     9
Anh:     6
Văn:     8

Tổng điểm:   38
Trung bình:  7.60
Cao nhất:    9 (Hóa)
Thấp nhất:   6 (Anh)
Xếp loại:    Khá
```

---

## Bài 6: Nullable — Phiếu đăng ký khám bệnh 🏥

### Yêu cầu:

Tạo phiếu đăng ký khám bệnh với một số thông tin **có thể chưa điền**:

**Bắt buộc điền:**
- Họ tên (string)
- Năm sinh (int)
- Số CMND/CCCD (string)

**Không bắt buộc (nullable):**
- Số BHYT (string?) — có thể chưa có
- Tiền sử bệnh (string?) — có thể không biết
- Cân nặng (double?) — có thể không cân
- Huyết áp (int?) — có thể chưa đo
- Người thân liên hệ (string?) — có thể không khai

### Gợi ý:

- Dùng `??` (null-coalescing) để hiển thị "Chưa cung cấp" khi null
- Dùng `.HasValue` cho nullable value types
- Tạo 2 bệnh nhân: 1 đầy đủ thông tin, 1 thiếu nhiều thông tin

### Expected Output:

```
=== PHIẾU ĐĂNG KÝ KHÁM BỆNH ===

Bệnh nhân 1:
  Họ tên:      Nguyễn Văn A
  Năm sinh:    1990
  CCCD:        012345678901
  Số BHYT:     HS1234567890
  Tiền sử:     Viêm dạ dày
  Cân nặng:    68.50 kg
  Huyết áp:    120 mmHg
  Người thân:  Nguyễn Thị B (0912345678)

Bệnh nhân 2:
  Họ tên:      Trần Văn C
  Năm sinh:    2000
  CCCD:        098765432109
  Số BHYT:     Chưa cung cấp
  Tiền sử:     Không rõ
  Cân nặng:    Chưa cân
  Huyết áp:    Chưa đo
  Người thân:  Chưa khai
```
