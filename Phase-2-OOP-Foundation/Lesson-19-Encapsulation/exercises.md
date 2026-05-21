# ✏️ Lesson 19 — Bài tập: Encapsulation

---

## Bài 1: Refactor — Public Fields → Encapsulation ⭐

### Yêu cầu:

Cho class với public fields (CHƯA có encapsulation). Refactor sang encapsulation hoàn chỉnh.

```csharp
// ❌ BEFORE — Refactor class này:
class Product
{
    public string Name;
    public decimal Price;
    public int Stock;
    public string Category;
}
```

### Sau khi refactor phải có:

1. Private fields + public properties với validation:
   - `Name`: không rỗng, tối đa 50 ký tự, auto trim
   - `Price`: >= 0, tối đa 999,999,999
   - `Stock`: >= 0
   - `Category`: không rỗng, default = "Chung"
2. Computed properties:
   - `InventoryValue` = Price × Stock
   - `StockStatus` → "Hết hàng" / "Sắp hết" (≤ 5) / "Còn hàng"
   - `IsAvailable` → true nếu Stock > 0
3. Constructor với validation
4. Method `Print()` hiển thị thông tin

### Test cases:

```csharp
var p1 = new Product("iPhone 15", 25990000m, 10, "Phone");   // ✅
var p2 = new Product("AirPods", 5990000m, 3);                // ✅ Category = "Chung"

p1.Print();
// → iPhone 15  25,990,000  10  🟢 Còn hàng  Value: 259,900,000

p1.Price = -100;    // ❌ Exception!
p1.Name = "";       // ❌ Exception!
// p1.InventoryValue = 0;  // ❌ Compile error! (computed)
```

---

## Bài 2: Class Password với Encapsulated Hashing ⭐⭐

### Yêu cầu:

Tạo class `PasswordManager` encapsulate logic quản lý mật khẩu:

### Private (ẩn bên trong):
- `string _hashedPassword` — lưu hash, không lưu plain text
- `int _failedAttempts` — đếm số lần sai
- `DateTime? _lockedUntil` — thời gian khóa
- `List<DateTime> _loginHistory` — lịch sử đăng nhập
- Private method `Hash(string)` — hash đơn giản (reverse + thêm salt)
- Private method `RecordLogin()` — ghi lịch sử

### Public API:
- `bool IsLocked { get; }` — computed: kiểm tra còn bị khóa không
- `int FailedAttempts { get; }` — readonly
- `IReadOnlyList<DateTime> LoginHistory` — readonly history
- Constructor `PasswordManager(string password)` — hash và lưu
- `bool Verify(string password)` — kiểm tra password
  - Đúng → reset failed, record login, return true
  - Sai → tăng failed, nếu ≥ 3 → khóa 30 giây
- `bool ChangePassword(string oldPassword, string newPassword)` — đổi mật khẩu

### Test:

```csharp
var pm = new PasswordManager("abc123");
pm.Verify("abc123");     // ✅ true
pm.Verify("wrong");      // ❌ false, failed = 1
pm.Verify("wrong");      // ❌ failed = 2
pm.Verify("wrong");      // ❌ failed = 3 → KHÓA 30s!
pm.Verify("abc123");     // ❌ false (đang bị khóa!)
// Chờ 30s...
pm.Verify("abc123");     // ✅ true
```

---

## Bài 3: Encapsulated Collection — Library Book Manager ⭐⭐

### Yêu cầu:

Tạo class `Library` quản lý sách với collection được encapsulate:

### Class `Book`:
- `string Title { get; }` — readonly
- `string Author { get; }` — readonly
- `bool IsAvailable { get; private set; }`
- `string BorrowedBy { get; private set; }`
- Methods: `Borrow(string memberName)`, `Return()`

### Class `Library`:
- **Private**: `List<Book> _books`, `int _maxBooks = 100`
- **Public readonly**: `IReadOnlyList<Book> Books`
- **Computed**: `AvailableCount`, `BorrowedCount`, `IsFull`
- **Methods**:
  - `AddBook(string title, string author)` — validate + thêm
  - `RemoveBook(string title)` — chỉ xóa sách đang available
  - `BorrowBook(string title, string member)` — tìm + cho mượn
  - `ReturnBook(string title)` — tìm + trả
  - `SearchByAuthor(string author)` → `IReadOnlyList<Book>`
  - `PrintCatalog()` — hiển thị tất cả sách

### Test:

```csharp
var lib = new Library();
lib.AddBook("Doraemon", "Fujiko");
lib.AddBook("Conan", "Gosho");

lib.BorrowBook("Doraemon", "Minh");
lib.PrintCatalog();
// Doraemon - Fujiko - 📕 Mượn bởi: Minh
// Conan - Gosho - 📗 Có sẵn

// lib.Books.Clear();           // ❌ IReadOnlyList!
// lib.Books.Add(new Book());   // ❌ IReadOnlyList!
```

---

## Bài 4: Immutable Money Class ⭐⭐⭐

### Yêu cầu:

Tạo class `Money` hoàn toàn immutable (không thể thay đổi sau khi tạo):

### Properties (tất cả readonly):
- `decimal Amount { get; }` 
- `string Currency { get; }` — "VND", "USD", "EUR"

### Computed:
- `bool IsZero` → Amount == 0
- `bool IsPositive` → Amount > 0

### Methods (trả về object MỚI, không sửa object hiện tại):
- `Money Add(Money other)` — cộng (phải cùng currency)
- `Money Subtract(Money other)` — trừ (phải cùng currency)
- `Money Multiply(decimal factor)` — nhân
- `Money ConvertTo(string targetCurrency)` — đổi tiền (USD↔VND simple rate)
- `override string ToString()` — format đẹp

### Quy tắc:
- Amount >= 0 (không cho phép âm)
- Chỉ cộng/trừ cùng currency
- ConvertTo dùng tỷ giá cố định: 1 USD = 25,000 VND

### Test:

```csharp
var price = new Money(500000m, "VND");
var shipping = new Money(30000m, "VND");

var total = price.Add(shipping);
Console.WriteLine(price);     // 500,000 VND  (KHÔNG ĐỔI!)
Console.WriteLine(total);     // 530,000 VND  (object mới)

var usd = total.ConvertTo("USD");
Console.WriteLine(usd);       // 21.20 USD

// price.Amount = 0;    // ❌ Compile error! Readonly
```

---

## Bài 5: Student Grade Manager — Full Encapsulation ⭐⭐⭐

### Yêu cầu:

Tạo hệ thống quản lý điểm sinh viên với encapsulation hoàn chỉnh:

### Class `Subject`:
- `string Name { get; }` — readonly
- `double Score { get; private set; }` — validate 0-10
- `double Weight { get; }` — hệ số (1-5), readonly
- Computed: `Grade` (A/B/C/D/F), `IsPass` (>= 5)

### Class `StudentRecord`:
- **Private**: `List<Subject> _subjects`, `string _notes`
- **Public**: `string StudentId { get; }`, `string Name { get; }`
- **Computed**:
  - `double GPA` — trung bình có trọng số: Σ(Score × Weight) / Σ(Weight)
  - `string Classification` — Xuất sắc(≥9) / Giỏi(≥8) / Khá(≥7) / TB(≥5) / Yếu
  - `int SubjectCount`, `int PassedCount`, `int FailedCount`
  - `bool IsEligibleForScholarship` — GPA >= 8.0 && không có môn nào fail
  - `IReadOnlyList<Subject> Subjects`
- **Methods**:
  - `AddSubject(string name, double score, double weight)`
  - `UpdateScore(string subjectName, double newScore)`
  - `RemoveSubject(string subjectName)`
  - `PrintTranscript()` — in bảng điểm đẹp

### Test:

```csharp
var student = new StudentRecord("SV001", "Nguyễn Minh");
student.AddSubject("Toán", 8.5, 3);
student.AddSubject("Lý", 7.0, 2);
student.AddSubject("Anh", 9.0, 2);

student.PrintTranscript();
// ┌─────────────────────────────────────┐
// │  BẢNG ĐIỂM — Nguyễn Minh (SV001)   │
// ├──────────┬──────┬─────┬─────┬───────┤
// │ Môn      │ Điểm │ Hệ số│ XL  │ K/Q  │
// ├──────────┼──────┼─────┼─────┼───────┤
// │ Toán     │  8.5 │  3  │  A  │  ✅   │
// │ Lý       │  7.0 │  2  │  B  │  ✅   │
// │ Anh      │  9.0 │  2  │  A  │  ✅   │
// ├──────────┴──────┴─────┴─────┴───────┤
// │ GPA: 8.21 — Giỏi                    │
// │ Học bổng: ✅ Đủ điều kiện           │
// └─────────────────────────────────────┘

Console.WriteLine(student.IsEligibleForScholarship);  // true
// student.Subjects.Clear();  // ❌ IReadOnlyList!
```

---

## 📊 Bảng tổng hợp

| Bài | Chủ đề | Kỹ năng | Độ khó |
|-----|--------|---------|--------|
| 1 | Refactor public → encapsulated | Validation property, computed | ⭐ |
| 2 | Password Manager | Private logic, lockout rules | ⭐⭐ |
| 3 | Library Manager | Collection encapsulation, IReadOnly | ⭐⭐ |
| 4 | Immutable Money | Immutable pattern, readonly | ⭐⭐⭐ |
| 5 | Grade Manager | Full encapsulation, business rules | ⭐⭐⭐ |
