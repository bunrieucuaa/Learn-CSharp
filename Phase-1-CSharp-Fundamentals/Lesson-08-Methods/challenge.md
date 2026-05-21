# 🏆 Lesson 08 — Challenge: Methods

---

## Challenge: Hệ thống quản lý ngân hàng mini 🏦

### Bối cảnh:
> "Refactor lại bài ATM Lesson 04 + thêm tính năng mới, tất cả logic phải tách thành methods sạch."

### Yêu cầu:

#### Dữ liệu (mảng, tối đa 10 tài khoản):
- Tên chủ TK
- Số TK (10 chữ số, auto-generate)
- PIN (4 chữ số)
- Số dư (decimal)
- Loại TK ("savings" / "checking")
- Trạng thái (active / locked)

#### Chức năng:

**1. Tạo tài khoản mới**
- Nhập tên, PIN, số tiền gửi ban đầu (tối thiểu 100,000)
- Chọn loại TK
- Tự sinh số TK

**2. Đăng nhập**
- Nhập số TK + PIN
- 3 lần sai → khóa TK

**3. Menu sau đăng nhập:**
- Xem số dư
- Gửi tiền
- Rút tiền (bội 50k, giữ lại 50k)
- Chuyển khoản (tìm TK người nhận, tính phí)
- Xem 10 GD gần nhất
- Đổi PIN
- Đăng xuất

**4. Menu Admin (PIN = "0000"):**
- Xem tất cả tài khoản
- Mở khóa TK bị lock
- Thống kê: tổng tiền, TB số dư, TK nhiều nhất/ít nhất

### Yêu cầu kỹ thuật — TỐI THIỂU 15 METHODS:

```
// ===== Input Methods =====
static string ReadString(string prompt);
static string ReadString(string prompt, int minLen, int maxLen);  // Overload
static int ReadInt(string prompt, int min, int max);
static decimal ReadDecimal(string prompt, decimal min, decimal max);
static bool ReadYesNo(string prompt);

// ===== Account Methods =====
static string GenerateAccountNumber();
static int FindAccount(string accountNumber);            // Return index
static bool ValidatePIN(int accountIndex, string pin);
static bool IsAccountActive(int accountIndex);

// ===== Transaction Methods =====
static bool Deposit(int index, decimal amount);          // Return success
static bool Withdraw(int index, decimal amount);
static bool Transfer(int fromIndex, int toIndex, decimal amount, out decimal fee);
static decimal CalculateTransferFee(decimal amount, bool isSameBank);

// ===== History Methods =====
static void AddHistory(int index, string type, decimal amount);
static void ShowHistory(int index);

// ===== UI Methods =====
static void ShowMainMenu();
static void ShowAccountMenu(int index);
static void ShowAdminMenu();
static void PrintError(string msg);
static void PrintSuccess(string msg);
static void PrintAccountInfo(int index);
static void Pause();

// ===== Admin Methods =====
static void ListAllAccounts();
static void UnlockAccount();
static void ShowStatistics();
```

### Quy tắc clean code:

| Quy tắc | Yêu cầu |
|---------|---------|
| Main() | Tối đa **15 dòng** |
| Mỗi method | Tối đa **20 dòng** |
| Overloading | Ít nhất **2 methods** |
| Default params | Ít nhất **1 method** |
| `out` parameter | Ít nhất **1 method** |
| Expression-bodied | Ít nhất **3 methods** |
| Method có return | Ít nhất **5 methods** |
| Guard clause | Ít nhất **3 methods** |

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Tối thiểu 15 methods | ⭐⭐⭐ Bắt buộc |
| Main() ≤ 15 dòng | ⭐⭐⭐ Bắt buộc |
| Mỗi method ≤ 20 dòng | ⭐⭐⭐ Bắt buộc |
| Tạo TK + Đăng nhập | ⭐⭐⭐ Bắt buộc |
| Gửi/Rút/Chuyển hoạt động | ⭐⭐ Quan trọng |
| Overloading + Default | ⭐⭐ Quan trọng |
| Guard clause | ⭐⭐ Quan trọng |
| Admin menu | ⭐ Bonus |
| Lịch sử GD | ⭐ Bonus |
| Không crash bất kỳ input | ⭐⭐⭐ BẮT BUỘC |
