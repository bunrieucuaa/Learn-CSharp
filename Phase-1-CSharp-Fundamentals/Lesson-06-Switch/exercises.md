# ✏️ Lesson 06 — Bài tập: Switch

> ⚠️ Tự làm trước! Thử dùng cả switch truyền thống lẫn switch expression.

---

## Bài 1: Số ngày trong tháng 📅

### Yêu cầu:
Nhập tháng (1-12) và năm → in ra số ngày của tháng đó.

### Gợi ý:
- Tháng 2: 28 ngày (hoặc 29 nếu năm nhuận)
- Tháng 4, 6, 9, 11: 30 ngày
- Còn lại: 31 ngày
- Dùng multiple cases: `case 4: case 6: case 9: case 11:`

### Expected Output:
```
Nhập tháng: 2
Nhập năm: 2024
→ Tháng 2/2024 có 29 ngày (năm nhuận)
```

---

## Bài 2: Máy dịch — Switch Expression 🌐

### Yêu cầu:
Nhập 1 từ tiếng Việt, dịch sang 3 ngôn ngữ (Anh, Pháp, Nhật).

Từ vựng (hardcode khoảng 10 từ):
- xin chào, tạm biệt, cảm ơn, xin lỗi, yêu, ...

Dùng **switch expression** cho gọn.

### Expected Output:
```
Nhập từ tiếng Việt: xin chào

🇬🇧 English: Hello
🇫🇷 French: Bonjour
🇯🇵 Japanese: こんにちは

Dịch từ khác? (y/n):
```

---

## Bài 3: Tử vi 12 con giáp 🐲

### Yêu cầu:
Nhập năm sinh → in ra con giáp và dự đoán vui.

**Công thức:** `(năm - 4) % 12` → index con giáp

| Index | Con giáp |
|-------|----------|
| 0 | Tý (Chuột) |
| 1 | Sửu (Trâu) |
| 2 | Dần (Hổ) |
| 3 | Mão (Mèo) |
| ... | ... |
| 11 | Hợi (Lợn) |

Dùng switch expression cho con giáp + nhận xét.

### Expected Output:
```
Nhập năm sinh: 2000
🐲 Con giáp: Thìn (Rồng)
📝 Tính cách: Mạnh mẽ, quyết đoán, đầy tham vọng!
```

---

## Bài 4: Hệ thống menu quản lý — Switch + Loop 📋

### Yêu cầu:
Tạo menu quản lý học sinh (chưa cần lưu data, chỉ hiện thông báo):

```
=== QUẢN LÝ HỌC SINH ===
1. Thêm học sinh
2. Xem danh sách
3. Tìm kiếm
4. Sửa thông tin
5. Xóa học sinh
6. Thống kê
0. Thoát
```

Mỗi lựa chọn → in thông báo "Chức năng X đang phát triển..."
Dùng `while` + `switch` để lặp menu.

Riêng chức năng 6 (Thống kê) → hiện sub-menu:

```
--- THỐNG KÊ ---
a. Theo giới tính
b. Theo điểm
c. Theo lớp
d. Quay lại
```

### Gợi ý:
- Switch string cho sub-menu (`"a"`, `"b"`, ...)
- `Console.Clear()` khi chuyển màn hình

---

## Bài 5: Tuple Pattern — Tính phí bưu phẩm 📦

### Yêu cầu:
Tính phí gửi bưu phẩm dựa trên **2 yếu tố**: loại hàng + vùng giao.

| | Nội thành | Ngoại thành | Tỉnh khác |
|---|----------|------------|-----------|
| Thư/tài liệu | 15,000 | 25,000 | 35,000 |
| Hàng nhỏ (<2kg) | 25,000 | 40,000 | 55,000 |
| Hàng lớn (2-10kg) | 45,000 | 65,000 | 90,000 |
| Hàng đặc biệt | 80,000 | 120,000 | 180,000 |

Dùng **tuple pattern** `(loại, vùng) switch { ... }` để tính giá.

### Expected Output:
```
=== TÍNH PHÍ BƯU PHẨM ===

Loại hàng (1-Thư, 2-Nhỏ, 3-Lớn, 4-Đặc biệt): 2
Vùng giao (1-Nội thành, 2-Ngoại thành, 3-Tỉnh khác): 3

📦 Hàng nhỏ → Tỉnh khác
💰 Phí: 55,000 VNĐ
```

---

## Bài 6: Pattern Matching — Phân loại BMI nâng cao 🏋️

### Yêu cầu:
Nhập cân nặng, chiều cao, và giới tính. Tính BMI và phân loại theo **giới tính**.

Dùng switch expression + relational + when guard:

```csharp
string category = (bmi, gender) switch
{
    (< 18.5, _) => "Thiếu cân",
    (< 25, "male") => "Bình thường (nam)",
    (< 23, "female") => "Bình thường (nữ)",
    // ...
};
```

Thêm khuyến nghị cụ thể cho từng nhóm.

### Expected Output:
```
Cân nặng: 70
Chiều cao: 1.75
Giới tính (male/female): male

BMI: 22.9
Phân loại: Bình thường (nam) ✅
Khuyến nghị: Duy trì chế độ tập luyện hiện tại.
```
