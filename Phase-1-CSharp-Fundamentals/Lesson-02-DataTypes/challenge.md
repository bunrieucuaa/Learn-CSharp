# 🏆 Lesson 02 — Challenge: Data Types

> ⚠️ **Bài thử thách thực tế!** Mô phỏng task ở công ty. Tự suy nghĩ trước khi xem gợi ý.

---

## Challenge: Hệ thống chấm lương & thuế nhân viên 💼

### Bối cảnh:

Leader giao task:

> "Em viết console app tính bảng lương chi tiết cho 2 nhân viên. Chú ý chọn đúng kiểu dữ liệu, dùng `decimal` cho tiền, xử lý các trường hợp nullable khi nhân viên mới chưa có đủ thông tin."

### Yêu cầu chi tiết:

**Thông tin nhân viên gồm:**

| Trường | Kiểu | Bắt buộc? |
|--------|------|-----------|
| Mã NV | string | ✅ |
| Họ tên | string | ✅ |
| Phòng ban | string | ✅ |
| Chức vụ | string? | ❌ (thử việc chưa có chức vụ) |
| Lương cơ bản | decimal | ✅ |
| Ngày công chuẩn | int | ✅ (const = 26) |
| Ngày công thực tế | int | ✅ |
| Hệ số lương | double? | ❌ (thử việc chưa có) |
| Phụ cấp ăn trưa | decimal | ✅ (const) |
| Phụ cấp xăng xe | decimal? | ❌ |
| Phụ cấp điện thoại | decimal? | ❌ |
| Thưởng KPI | decimal? | ❌ |

**Bảng thuế lũy tiến (đơn giản hóa):**

| Thu nhập chịu thuế | Thuế suất |
|---------------------|-----------|
| ≤ 5,000,000 | 5% |
| 5,000,001 — 10,000,000 | 10% |
| > 10,000,000 | 15% |

**Công thức:**

```
Lương ngày = Lương cơ bản / Ngày công chuẩn
Lương thực lĩnh cơ bản = Lương ngày × Ngày công thực tế
Hệ số = Lương thực lĩnh × (Hệ số lương - 1)    [nếu có hệ số]
Tổng phụ cấp = Phụ cấp ăn + Phụ cấp xăng + Phụ cấp ĐT
Lương Gross = Lương thực lĩnh + Hệ số + Tổng phụ cấp + Thưởng KPI
BHXH = Lương Gross × 8%
BHYT = Lương Gross × 1.5%
BHTN = Lương Gross × 1%
Thu nhập chịu thuế = Lương Gross - BHXH - BHYT - BHTN - 11,000,000 (giảm trừ gia cảnh)
Thuế TNCN = tính theo bảng lũy tiến
Lương Net = Lương Gross - BHXH - BHYT - BHTN - Thuế TNCN
```

**Nhân viên 1 — Chính thức (đầy đủ thông tin):**
- Mã: "NV001", Tên: "Nguyễn Văn A", Phòng: "IT"
- Chức vụ: "Senior Developer"
- Lương CB: 20,000,000, Hệ số: 1.5
- Ngày công thực: 24
- Phụ cấp xăng: 1,000,000, ĐT: 500,000
- Thưởng KPI: 3,000,000

**Nhân viên 2 — Thử việc (thiếu thông tin):**
- Mã: "NV002", Tên: "Trần Thị B", Phòng: "Marketing"
- Chức vụ: null (thử việc)
- Lương CB: 10,000,000, Hệ số: null
- Ngày công thực: 22
- Phụ cấp xăng: null, ĐT: null
- Thưởng KPI: null

### Yêu cầu kỹ thuật:

- ✅ **`decimal`** cho TẤT CẢ số tiền
- ✅ **`const`** cho ngày công chuẩn, phụ cấp ăn, tỷ lệ BH, giảm trừ gia cảnh
- ✅ **`nullable`** (`?`) cho các trường không bắt buộc
- ✅ **`??`** (null-coalescing) để xử lý giá trị mặc định
- ✅ Format tiền tệ đẹp (`N0`)
- ✅ In bảng lương chi tiết cho CẢ 2 nhân viên
- ✅ Xử lý thu nhập chịu thuế **âm** (nếu lương thấp → thuế = 0)

### Expected Output (tương tự):

```
╔══════════════════════════════════════════════════════╗
║              BẢNG LƯƠNG THÁNG 05/2025               ║
╠══════════════════════════════════════════════════════╣

━━━ NHÂN VIÊN 1 ━━━
Mã NV:     NV001
Họ tên:    Nguyễn Văn A
Phòng ban: IT
Chức vụ:   Senior Developer

Lương cơ bản:          20,000,000 VNĐ
Ngày công:             24/26
Lương thực lĩnh CB:    18,461,538 VNĐ
Hệ số lương (×1.5):    9,230,769 VNĐ
Phụ cấp ăn:              730,000 VNĐ
Phụ cấp xăng:          1,000,000 VNĐ
Phụ cấp ĐT:              500,000 VNĐ
Thưởng KPI:             3,000,000 VNĐ
────────────────────────────────────
LƯƠNG GROSS:           32,922,307 VNĐ

Trừ BHXH (8%):         -2,633,785 VNĐ
Trừ BHYT (1.5%):         -493,835 VNĐ
Trừ BHTN (1%):           -329,223 VNĐ
Giảm trừ gia cảnh:   -11,000,000 VNĐ
────────────────────────────────────
Thu nhập chịu thuế:    18,465,464 VNĐ
Thuế TNCN:             -2,019,820 VNĐ
────────────────────────────────────
LƯƠNG NET:             27,445,464 VNĐ

━━━ NHÂN VIÊN 2 ━━━
Mã NV:     NV002
Họ tên:    Trần Thị B
Phòng ban: Marketing
Chức vụ:   Thử việc

...
(tương tự, với các giá trị null hiện "Không có")
...
╚══════════════════════════════════════════════════════╝
```

---

### 💡 Gợi ý (chỉ xem khi bí):

<details>
<summary>Gợi ý 1: Tính thuế lũy tiến</summary>

```csharp
decimal taxableIncome = ...; // Thu nhập chịu thuế
decimal tax = 0m;

if (taxableIncome <= 0)
{
    tax = 0m;
}
else if (taxableIncome <= 5000000m)
{
    tax = taxableIncome * 0.05m;
}
else if (taxableIncome <= 10000000m)
{
    tax = 5000000m * 0.05m + (taxableIncome - 5000000m) * 0.10m;
}
else
{
    tax = 5000000m * 0.05m + 5000000m * 0.10m + (taxableIncome - 10000000m) * 0.15m;
}
```

</details>

<details>
<summary>Gợi ý 2: Xử lý nullable</summary>

```csharp
// Hệ số lương
double? coefficent = null; // hoặc 1.5
decimal coefficentBonus = coefficent.HasValue 
    ? baseSalaryActual * (decimal)(coefficent.Value - 1) 
    : 0m;

// Phụ cấp
decimal totalAllowance = lunchAllowance 
    + (gasAllowance ?? 0m) 
    + (phoneAllowance ?? 0m);
```

</details>

<details>
<summary>Gợi ý 3: Tạo method tái sử dụng</summary>

Vì cần tính cho 2 nhân viên, hãy nghĩ cách **không copy-paste** code. Bạn có thể tạo một `static void PrintPayslip(...)` method nhận tham số. (Sẽ học kỹ hơn ở Lesson 08 — Methods)

</details>

---

### 🎯 Tiêu chí đánh giá:

| Tiêu chí | Mức độ |
|----------|--------|
| Dùng `decimal` cho tiền | ⭐⭐⭐ Bắt buộc |
| Dùng `const` đúng chỗ | ⭐⭐⭐ Bắt buộc |
| Dùng nullable types | ⭐⭐⭐ Bắt buộc |
| Tính thuế lũy tiến đúng | ⭐⭐ Quan trọng |
| Xử lý thu nhập chịu thuế âm | ⭐⭐ Quan trọng |
| Không integer division | ⭐⭐ Quan trọng |
| Format tiền đẹp | ⭐ Bonus |
| Tạo method tái sử dụng | ⭐ Bonus nâng cao |
