# 🏆 Lesson 10 — Challenge: String

---

## Challenge: Hệ thống xử lý & phân tích Email 📧

### Bối cảnh:
> "Xây dựng console app phân tích hộp thư. Nhập danh sách email, phân tích domain, validate, tạo báo cáo."

### Yêu cầu:

#### 1. Nhập danh sách email (tối đa 20):
- Nhập từng email hoặc paste nhiều email (cách dấu `;`)
- Validate format: `xxx@domain.ext`
- Loại bỏ trùng lặp (case-insensitive)
- Gõ "done" để kết thúc nhập

#### 2. Phân tích & Xử lý:

**a) Thống kê domain:**
```
gmail.com:     8 email (40%)
yahoo.com:     4 email (20%)
company.vn:    6 email (30%)
other:         2 email (10%)
```

**b) Validate & Phân loại:**
- Valid: có @, có domain, có ext (2-4 ký tự)
- Invalid: thiếu @, chứa ký tự đặc biệt, domain sai

**c) Format output:**
- Tên (trước @): capitalize first letter
- Domain: lowercase
- Mask email: `nguyenminh@gmail.com` → `ng*******h@gmail.com`

**d) Tạo username từ email:**
- `nguyen.minh@gmail.com` → `nguyenminh`
- `admin123@company.vn` → `admin123`

**e) Tìm kiếm:**
- Tìm theo tên (chứa keyword)
- Tìm theo domain
- Lọc email invalid

#### 3. Tạo báo cáo (StringBuilder):

```
════════════════════════════════════════════════════
          📧 BÁO CÁO PHÂN TÍCH EMAIL
════════════════════════════════════════════════════

Tổng email nhập: 25
Email hợp lệ:    22
Email không hợp lệ: 3
Email trùng bị loại: 2

─── THỐNG KÊ DOMAIN ───
  gmail.com       ████████████ 12 (55%)
  yahoo.com       ████         4 (18%)
  company.vn      ██████       6 (27%)

─── DANH SÁCH ───
  # │ Email                        │ Username    │ Domain      │ Valid
 ───┼──────────────────────────────┼─────────────┼─────────────┼──────
  1 │ nguyen.minh@gmail.com        │ nguyenminh  │ gmail.com   │ ✅
  2 │ bad-email@@                  │ -           │ -           │ ❌
  ...

─── EMAIL KHÔNG HỢP LỆ ───
  1. "bad-email@@" — Lỗi: Thiếu domain
  2. "test@" — Lỗi: Thiếu extension
  3. "@gmail.com" — Lỗi: Thiếu username

════════════════════════════════════════════════════
```

### Yêu cầu kỹ thuật:

- ✅ `StringBuilder` cho báo cáo
- ✅ `Split`, `Contains`, `IndexOf`, `Substring`/Range
- ✅ `Trim`, `ToLower`, `ToUpper`
- ✅ `string.IsNullOrWhiteSpace`
- ✅ `StringComparison.OrdinalIgnoreCase`
- ✅ Tách thành methods (≥10 methods)
- ✅ Validate input đầy đủ

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Validate email (cơ bản) | ⭐⭐⭐ Bắt buộc |
| Thống kê domain | ⭐⭐⭐ Bắt buộc |
| StringBuilder báo cáo | ⭐⭐⭐ Bắt buộc |
| Tách methods (≥10) | ⭐⭐ Quan trọng |
| Loại trùng lặp | ⭐⭐ Quan trọng |
| Mask email | ⭐ Bonus |
| Biểu đồ thanh (████) | ⭐ Bonus |
| Tìm kiếm | ⭐ Bonus |
