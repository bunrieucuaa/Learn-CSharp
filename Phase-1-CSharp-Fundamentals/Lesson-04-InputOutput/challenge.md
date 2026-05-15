# 🏆 Lesson 04 — Challenge: Input/Output

> ⚠️ **Bài thử thách thực tế!** Tự suy nghĩ trước khi xem gợi ý.

---

## Challenge: ATM Simulator 🏧

### Bối cảnh:

Team lead giao task:

> "Em viết console app mô phỏng máy ATM đơn giản. Cần có menu, xử lý input, validate dữ liệu, và quản lý số dư tài khoản."

### Yêu cầu chức năng:

#### 1. Đăng nhập:
- Nhập mã PIN (4 chữ số)
- PIN đúng: `1234` (hardcode)
- Cho phép nhập sai tối đa **3 lần**
- Sai 3 lần → khóa tài khoản, kết thúc chương trình

#### 2. Menu chính (sau khi đăng nhập thành công):

```
╔══════════════════════════╗
║      🏧 ATM MENU        ║
╠══════════════════════════╣
║  1. Xem số dư            ║
║  2. Rút tiền              ║
║  3. Gửi tiền              ║
║  4. Chuyển khoản           ║
║  5. Lịch sử giao dịch     ║
║  6. Đổi mã PIN            ║
║  0. Thoát                  ║
╚══════════════════════════╝
```

#### 3. Chi tiết từng chức năng:

**Xem số dư:**
- Hiển thị số dư hiện tại (ban đầu: 10,000,000 VNĐ)

**Rút tiền:**
- Nhập số tiền muốn rút
- Validate:
  - Phải là **bội số của 50,000** (ATM chỉ có tờ 50k)
  - Tối thiểu: 50,000
  - Tối đa mỗi lần: 5,000,000
  - Không được rút quá số dư
  - Phải giữ lại tối thiểu 50,000 trong tài khoản
- Hỏi xác nhận (y/n) trước khi rút

**Gửi tiền:**
- Nhập số tiền muốn gửi
- Validate: > 0, bội số của 10,000
- Cập nhật số dư

**Chuyển khoản:**
- Nhập số tài khoản người nhận (10 chữ số)
- Nhập tên người nhận
- Nhập số tiền chuyển
- Phí chuyển khoản: 
  - Cùng ngân hàng (tài khoản bắt đầu bằng "100"): miễn phí
  - Khác ngân hàng: 1% (tối thiểu 11,000 VNĐ)
- Hiện tổng tiền bị trừ (tiền chuyển + phí)
- Xác nhận (y/n)

**Lịch sử giao dịch:**
- Lưu tối đa **10 giao dịch gần nhất**
- Mỗi giao dịch gồm: STT, Loại (Rút/Gửi/Chuyển), Số tiền, Số dư sau GD
- In dạng bảng

**Đổi mã PIN:**
- Nhập PIN cũ (phải đúng)
- Nhập PIN mới (4 chữ số, khác PIN cũ)
- Nhập lại PIN mới (phải khớp)

### Yêu cầu kỹ thuật:

- ✅ `decimal` cho tiền
- ✅ `TryParse` cho MỌI input số
- ✅ Validate đầy đủ (null, rỗng, phạm vi, format)
- ✅ Vòng lặp `while` cho menu và retry
- ✅ Màu sắc: xanh = thành công, đỏ = lỗi, vàng = cảnh báo
- ✅ Format tiền `N0`
- ✅ `Console.Clear()` khi chuyển màn hình
- ✅ Mảng string để lưu lịch sử giao dịch

### Expected Output mẫu (rút tiền):

```
=== RÚT TIỀN ===

Số dư hiện tại: 10,000,000 VNĐ
Nhập số tiền rút: 100000
❌ Số tiền phải là bội số của 50,000!

Nhập số tiền rút: 2000000

Xác nhận rút 2,000,000 VNĐ? (y/n): y

✅ Rút tiền thành công!
Số tiền rút:     2,000,000 VNĐ
Số dư còn lại:   8,000,000 VNĐ

Nhấn Enter để quay lại menu...
```

---

### 💡 Gợi ý (chỉ xem khi bí):

<details>
<summary>Gợi ý 1: Cấu trúc chính</summary>

```csharp
// Biến toàn cục (khai báo ở đầu Main)
decimal balance = 10000000m;
string pin = "1234";
string[] history = new string[10];  // Mảng lưu lịch sử
int historyCount = 0;               // Số giao dịch đã lưu

// Đăng nhập
int attempts = 0;
while (attempts < 3)
{
    Console.Write("Nhập PIN: ");
    string? inputPin = Console.ReadLine();
    if (inputPin == pin) break;
    attempts++;
    Console.WriteLine($"❌ Sai PIN! Còn {3 - attempts} lần thử.");
}
if (attempts >= 3) { /* Khóa tài khoản */ return; }

// Menu chính
while (true)
{
    // Hiện menu
    // switch (choice) { case "1": ... }
}
```

</details>

<details>
<summary>Gợi ý 2: Kiểm tra bội số</summary>

```csharp
// Kiểm tra bội số của 50,000
if (amount % 50000 != 0)
{
    Console.WriteLine("❌ Số tiền phải là bội số của 50,000!");
}
```

</details>

<details>
<summary>Gợi ý 3: Lưu lịch sử giao dịch</summary>

```csharp
// Thêm giao dịch vào mảng
void AddHistory(string type, decimal amount)
{
    if (historyCount < 10)
    {
        history[historyCount] = $"{historyCount + 1}|{type}|{amount}|{balance}";
        historyCount++;
    }
    else
    {
        // Dịch mảng lên 1, bỏ giao dịch cũ nhất
        for (int i = 0; i < 9; i++)
            history[i] = history[i + 1];
        history[9] = $"{historyCount + 1}|{type}|{amount}|{balance}";
        historyCount++;
    }
}
```

</details>

<details>
<summary>Gợi ý 4: Kiểm tra tài khoản cùng ngân hàng</summary>

```csharp
string accountNumber = "1001234567";
bool isSameBank = accountNumber.StartsWith("100");
decimal fee = isSameBank ? 0m : Math.Max(amount * 0.01m, 11000m);
```

</details>

---

### 🎯 Tiêu chí đánh giá:

| Tiêu chí | Mức độ |
|----------|--------|
| Đăng nhập + giới hạn 3 lần | ⭐⭐⭐ Bắt buộc |
| Menu loop đúng | ⭐⭐⭐ Bắt buộc |
| Rút tiền + validate đầy đủ | ⭐⭐⭐ Bắt buộc |
| TryParse cho mọi input | ⭐⭐⭐ Bắt buộc |
| Gửi tiền hoạt động | ⭐⭐ Quan trọng |
| Chuyển khoản + tính phí | ⭐⭐ Quan trọng |
| Lịch sử giao dịch | ⭐⭐ Quan trọng |
| Đổi mã PIN | ⭐ Bonus |
| Màu sắc, format đẹp | ⭐ Bonus |
| Không crash với bất kỳ input nào | ⭐⭐⭐ BẮT BUỘC |
