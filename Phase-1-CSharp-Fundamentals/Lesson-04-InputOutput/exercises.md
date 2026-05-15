# ✏️ Lesson 04 — Bài tập: Input/Output

> ⚠️ **QUY TẮC**: Tự làm trước! Tất cả bài tập đều yêu cầu nhận **input từ người dùng**.

---

## Bài 1: Giới thiệu bản thân 👋

### Yêu cầu:

Viết chương trình hỏi người dùng 5 thông tin:
1. Tên
2. Tuổi
3. Nghề nghiệp
4. Sở thích
5. Quê quán

Sau đó in ra **đoạn giới thiệu** hoàn chỉnh dạng:

### Expected Output:

```
=== GIỚI THIỆU BẢN THÂN ===

Nhập tên: Minh
Nhập tuổi: 25
Nghề nghiệp: Lập trình viên
Sở thích: Đọc sách
Quê quán: Hà Nội

━━━━━━━━━━━━━━━━━━━━━━━━━━━
Xin chào! Tôi là Minh, 25 tuổi.
Tôi hiện đang là Lập trình viên.
Sở thích của tôi là Đọc sách.
Tôi đến từ Hà Nội.
━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

###csharp code result
//Bai 1:
            //For storing value
            string userName = "";
            int userAge = 0;
            string occupation = "";
            string hobby = "";
            string homeTown = "";

            //Start-up
            Console.WriteLine("Chào mừng đến với chương trình nhập thông tin người dùng");
            Console.WriteLine("Vui lòng nhập các thông tin sau:");

            //Validating UserName;
            while (true) {
                Console.Write("Tên của bạn là: ");
                string? inputUserName = Console.ReadLine(); 

                if (string.IsNullOrEmpty(inputUserName)) {
                    Console.WriteLine("❌ Tên không được để trống!, Vui lòng kiểm tra và nhập lại");
                }
                else
                {
                    userName = inputUserName.Trim();
                    break;
                }
            }

            //Validating UserAge;
            while (true)
            {
                Console.Write("Nhập tuổi (1-120): ");
                string? inputAge = Console.ReadLine();

                if (int.TryParse(inputAge, out userAge) && userAge >= 1 && userAge <= 120)
                {
                    // TryParse đã gán giá trị vào userAge, không cần gán lại
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Tuổi không hợp lệ! Thử lại.");
                }
            }

            //Validating Occupation;
            while (true)
            {
                Console.Write("Nghề nghiệp của bạn là: ");
                string? inputOccupation = Console.ReadLine();

                if (string.IsNullOrEmpty(inputOccupation))
                {
                    Console.WriteLine("❌ Nghề nghiệp không được để trống!, Vui lòng kiểm tra và nhập lại");
                }
                else
                {
                    occupation = inputOccupation.Trim();
                    break;
                }
            }

            //Validating Hobby;
            while (true)
            {
                Console.Write("Sở thích của bạn là: ");
                string? inputHobby = Console.ReadLine();

                if (string.IsNullOrEmpty(inputHobby))
                {
                    Console.WriteLine("❌ Sở thích không được để trống!, Vui lòng kiểm tra và nhập lại");
                }
                else
                {
                    hobby = inputHobby.Trim();
                    break;
                }
            }

            //Validating HomeTown;
            while (true)
            {
                Console.Write("Quê quán của bạn là: ");
                string? inputHomeTown = Console.ReadLine();

                if (string.IsNullOrEmpty(inputHomeTown))
                {
                    Console.WriteLine("❌ Quê quán không được để trống!, Vui lòng kiểm tra và nhập lại");
                }
                else
                {
                    homeTown = inputHomeTown.Trim();
                    break;
                }
            }

            //Output
            Console.WriteLine();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"Xin chào! Tôi là {userName}, {userAge} tuổi.");
            Console.WriteLine($"Tôi hiện đang là {occupation}.");
            Console.WriteLine($"Sở thích của tôi là {hobby}.");
            Console.WriteLine($"Tôi đến từ {homeTown}.");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━");

### Gợi ý:

- Dùng `Console.Write()` cho prompt
- Dùng `?.Trim()` để xóa khoảng trắng
- Validate tuổi bằng `int.TryParse` (phải là số 1-120)

---

## Bài 2: Máy tính BMI có input 🏋️

### Yêu cầu:

Nâng cấp bài BMI từ Lesson 02 — lần này **nhận input từ người dùng**:

1. Nhập cân nặng (kg) — validate > 0
2. Nhập chiều cao (m) — validate 0.5 - 2.5
3. Tính BMI
4. Phân loại + in màu:
   - `< 18.5` → "Thiếu cân" (vàng)
   - `18.5 - 24.9` → "Bình thường" (xanh)
   - `25 - 29.9` → "Thừa cân" (vàng)
   - `≥ 30` → "Béo phì" (đỏ)
5. **Hỏi lại** nếu nhập sai (dùng vòng lặp)

### Expected Output:

```
=== MÁY TÍNH BMI ===

Cân nặng (kg): abc
❌ Vui lòng nhập số! Thử lại.
Cân nặng (kg): -5
❌ Cân nặng phải lớn hơn 0! Thử lại.
Cân nặng (kg): 70
Chiều cao (m): 1.75

╔════════════════════════╗
║    KẾT QUẢ BMI         ║
╠════════════════════════╣
║  Cân nặng: 70.0 kg    ║
║  Chiều cao: 1.75 m    ║
║  BMI: 22.9             ║
║  → Bình thường ✅      ║
╚════════════════════════╝
```

---

## Bài 3: Đổi tiền tệ — có input 💱

### Yêu cầu:

Viết chương trình đổi tiền tệ tương tác:

1. Hiển thị menu các loại tiền: VNĐ, USD, EUR, JPY
2. User chọn tiền nguồn (1-4)
3. User chọn tiền đích (1-4)
4. User nhập số tiền
5. Tính và hiển thị kết quả
6. Hỏi có muốn đổi tiếp không (y/n)

### Tỷ giá (quy đổi qua VNĐ):

```
1 USD = 25,000 VNĐ
1 EUR = 27,500 VNĐ
1 JPY = 170 VNĐ
```

### Gợi ý:

- Quy đổi qua VNĐ trước, rồi từ VNĐ sang tiền đích
- Dùng `switch` hoặc `if/else` cho lựa chọn
- Validate lựa chọn (1-4) và số tiền (> 0)
- Dùng vòng `while` cho "đổi tiếp"

### Expected Output:

```
=== ĐỔI TIỀN TỆ ===

Chọn tiền nguồn:
  1. VNĐ
  2. USD
  3. EUR
  4. JPY
Lựa chọn: 2

Chọn tiền đích:
  1. VNĐ
  2. USD
  3. EUR
  4. JPY
Lựa chọn: 1

Nhập số tiền: 100

✅ 100.00 USD = 2,500,000 VNĐ

Đổi tiếp? (y/n): n
👋 Tạm biệt!
```

---

## Bài 4: Nhập nhiều giá trị — Tính điểm 📊

### Yêu cầu:

Viết chương trình:

1. Hỏi người dùng có **bao nhiêu môn** học (n)
2. Nhập **tên và điểm** từng môn (validate 0-10)
3. Tính: tổng, trung bình, điểm cao nhất, điểm thấp nhất
4. Xếp loại: Giỏi (≥8), Khá (≥6.5), TB (≥5), Yếu (<5)
5. In bảng điểm đẹp

### Gợi ý:

- Dùng mảng `string[]` cho tên môn, `double[]` cho điểm
- Validate: số môn > 0, điểm 0-10
- Dùng `Math.Max()`, `Math.Min()` hoặc tự tìm
- Format bảng với alignment

### Expected Output:

```
=== BẢNG ĐIỂM HỌC SINH ===

Họ tên: Nguyễn Văn A
Số môn: 4

Nhập điểm từng môn:
  Tên môn 1: Toán
  Điểm Toán: 8.5
  Tên môn 2: Lý
  Điểm Lý: 7.0
  Tên môn 3: Hóa
  Điểm Hóa: 9.0
  Tên môn 4: Anh
  Điểm Anh: 6.5

╔═══════════════════════════════╗
║        BẢNG ĐIỂM             ║
║  Học sinh: Nguyễn Văn A       ║
╠═══════╤══════════╤═══════════╣
║  STT  │ Môn      │   Điểm    ║
╠═══════╪══════════╪═══════════╣
║   1   │ Toán     │      8.50 ║
║   2   │ Lý       │      7.00 ║
║   3   │ Hóa      │      9.00 ║
║   4   │ Anh      │      6.50 ║
╠═══════╧══════════╧═══════════╣
║  Tổng:              31.00    ║
║  Trung bình:         7.75    ║
║  Cao nhất:    9.00 (Hóa)    ║
║  Thấp nhất:   6.50 (Anh)    ║
║  Xếp loại:    Khá           ║
╚═══════════════════════════════╝
```

---

## Bài 5: Mini Game — Đoán số 🎲

### Yêu cầu:

Viết game đoán số:

1. Máy random 1 số từ 1-100
2. User nhập số đoán
3. Máy gợi ý: "Lớn hơn" hoặc "Nhỏ hơn"
4. Đếm số lần đoán
5. Khi đoán đúng: chúc mừng + hiện số lần đoán
6. Đánh giá: ≤5 lần = "Xuất sắc!", ≤10 = "Tốt", >10 = "Cần cải thiện"

### Gợi ý:

- `Random random = new Random();`
- `int secretNumber = random.Next(1, 101);` — random từ 1 đến 100
- Dùng `while` loop
- Validate input (phải là số 1-100)
- Đổi màu khi đoán đúng (xanh)

### Expected Output:

```
🎲 GAME ĐOÁN SỐ (1-100) 🎲

Máy đã chọn 1 số. Hãy đoán!

Lần 1: 50
→ Lớn hơn! ⬆️

Lần 2: 75
→ Nhỏ hơn! ⬇️

Lần 3: 62
→ Lớn hơn! ⬆️

Lần 4: 68
→ Nhỏ hơn! ⬇️

Lần 5: 65
🎉 CHÍNH XÁC! Số bí mật là 65!
Bạn đoán trong 5 lần → Xuất sắc! 🏆

Chơi lại? (y/n):
```

---

## Bài 6: Format bảng — Hóa đơn tiền điện ⚡

### Yêu cầu:

Viết chương trình tính tiền điện hàng tháng:

1. Nhập tên khách hàng
2. Nhập mã khách hàng
3. Nhập chỉ số điện kỳ trước (kWh)
4. Nhập chỉ số điện kỳ này (phải > kỳ trước)
5. Tính tiền điện theo bậc thang:

| Bậc | kWh | Giá (VNĐ/kWh) |
|-----|-----|----------------|
| 1 | 0 — 50 | 1,678 |
| 2 | 51 — 100 | 1,734 |
| 3 | 101 — 200 | 2,014 |
| 4 | 201 — 300 | 2,536 |
| 5 | 301 — 400 | 2,834 |
| 6 | > 400 | 2,927 |

6. VAT: 10%
7. In hóa đơn format đẹp

### Gợi ý:

- Tính điện tiêu thụ = kỳ này - kỳ trước
- Tính tiền từng bậc riêng (tương tự thuế lũy tiến ở Lesson 02)
- Validate: chỉ số kỳ này > kỳ trước
- Dùng format alignment cho bảng

### Expected Output:

```
╔═══════════════════════════════════════════════╗
║          HÓA ĐƠN TIỀN ĐIỆN                   ║
╠═══════════════════════════════════════════════╣
║  Khách hàng:    Nguyễn Văn A                  ║
║  Mã KH:         KH001                         ║
║  Kỳ trước:      1250 kWh                      ║
║  Kỳ này:        1520 kWh                       ║
║  Tiêu thụ:      270 kWh                       ║
╠═══════════════════════════════════════════════╣
║  Bậc 1 (50 kWh × 1,678):         83,900 VNĐ  ║
║  Bậc 2 (50 kWh × 1,734):         86,700 VNĐ  ║
║  Bậc 3 (100 kWh × 2,014):       201,400 VNĐ  ║
║  Bậc 4 (70 kWh × 2,536):        177,520 VNĐ  ║
╠═══════════════════════════════════════════════╣
║  Thành tiền:                     549,520 VNĐ  ║
║  VAT (10%):                       54,952 VNĐ  ║
║  TỔNG CỘNG:                      604,472 VNĐ  ║
╚═══════════════════════════════════════════════╝
```
