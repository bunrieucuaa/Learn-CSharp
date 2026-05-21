# ✏️ Lesson 15 — Bài tập: Class & Object

---

## Bài 1: Class Book 📚

### Yêu cầu:
Tạo class `Book` với:
- Fields: `Title`, `Author`, `Price`, `Pages`, `Year`
- Methods: `GetAge()` (năm hiện tại - năm xuất bản), `IsExpensive()` (> 200,000), `Print()`

Tạo 5 objects, in danh sách đẹp, tìm sách đắt nhất, sách cũ nhất.

---

## Bài 2: Class Employee 👔

### Yêu cầu:
Tạo class `Employee`:
- Fields: `Name`, `Department`, `BaseSalary`, `WorkDays`
- Methods: `CalculateSalary()` (lương = base × workDays/22), `GetTax()` (>10tr: 10%, >20tr: 15%), `GetNetSalary()`, `PrintPayslip()`

Tạo mảng 5 NV, in bảng lương, thống kê: tổng lương, TB, phòng ban nhiều NV nhất.

---

## Bài 3: Class Rectangle 📐

### Yêu cầu:
Tạo class `Rectangle`:
- Fields: `Width`, `Height`
- Methods: `Area()`, `Perimeter()`, `IsSquare()`, `Scale(factor)`, `Print()` (vẽ bằng ký tự `*`)

Tạo 3 hình, in thông tin, scale ×2, so sánh diện tích.

---

## Bài 4: Chuyển đổi — Mảng song song → Object 🔄

### Yêu cầu:
Refactor code Phase 1 dưới đây sang dùng class:

```csharp
// ❌ CŨ — mảng song song
string[] names = { "Minh", "Hùng", "Lan" };
double[] scores = { 8.5, 6.0, 9.2 };
int[] ages = { 22, 25, 20 };
```

Tạo class `Student` → mảng `Student[]` → thêm methods: `GetGrade()`, `IsPass()`, `Print()`.
Thêm: sắp xếp theo điểm, tìm kiếm theo tên.

---

## Bài 5: 2 Class tương tác — Order System 🛒

### Yêu cầu:
Tạo 2 class:

```csharp
class Product { Name, Price, Stock, Sell(), Restock(), Print() }
class OrderItem { Product, Quantity, GetSubtotal(), Print() }
```

Tạo mảng sản phẩm (5 SP), tạo đơn hàng (mảng OrderItem), tính tổng, in hóa đơn đẹp.
