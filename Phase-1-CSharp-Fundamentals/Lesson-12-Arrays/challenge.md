# 🏆 Lesson 12 — Challenge: Arrays

---

## Challenge: Hệ thống quản lý lớp học 🏫

### Bối cảnh:
> "Xây dựng console app quản lý lớp học hoàn chỉnh dùng mảng. Nhiều môn học, nhiều học sinh, có bảng điểm."

### Dữ liệu (mảng, tối đa 30 HS):

```csharp
// Thông tin HS
string[] studentIds;     // Mã HS: HS001, HS002...
string[] studentNames;   // Tên
string[] studentGenders; // "Nam" / "Nữ"

// Bảng điểm: mảng 2D [học sinh, môn]
// 5 môn: Toán, Lý, Hóa, Anh, Văn
double[,] scores;        // scores[i, j] = điểm HS i môn j
double[] averages;       // Điểm TB mỗi HS (tính từ scores)
string[] grades;         // Xếp loại (tính từ averages)
```

### Chức năng:

#### 1. Thêm học sinh
- Nhập tên, giới tính, 5 điểm
- Auto-generate mã: HS001, HS002...
- Tính TB + xếp loại tự động

#### 2. Xem bảng điểm

```
╔═══════════════════════════════════════════════════════════════════╗
║                    BẢNG ĐIỂM LỚP 12A1                           ║
╠═════╦════════════╦═════╦═════╦═════╦═════╦═════╦══════╦═════════╣
║ Mã  ║ Tên        ║Toán ║ Lý  ║ Hóa ║ Anh ║ Văn ║  TB  ║ Loại   ║
╠═════╬════════════╬═════╬═════╬═════╬═════╬═════╬══════╬═════════╣
║HS001║ Nguyễn An  ║ 8.0 ║ 7.5 ║ 9.0 ║ 8.5 ║ 7.0 ║ 8.00 ║ Giỏi  ║
║HS002║ Trần Bình  ║ 5.0 ║ 6.0 ║ 4.5 ║ 7.0 ║ 6.5 ║ 5.80 ║ TB    ║
╚═════╩════════════╩═════╩═════╩═════╩═════╩═════╩══════╩═════════╝
```

#### 3. Sửa điểm
- Chọn HS theo mã → chọn môn → nhập điểm mới
- Tự tính lại TB + xếp loại

#### 4. Xóa học sinh
- Dịch mảng (tất cả mảng song song)

#### 5. Tìm kiếm
- Theo tên (keyword)
- Theo xếp loại
- Theo khoảng điểm TB

#### 6. Sắp xếp (sub-menu)
- Theo tên (A-Z)
- Theo điểm TB (cao → thấp)
- Theo điểm 1 môn cụ thể
- Dùng Bubble Sort tự viết — swap TẤT CẢ mảng song song

#### 7. Thống kê

```
📊 THỐNG KÊ LỚP 12A1
──────────────────────────────
Sĩ số: 25 (Nam: 15, Nữ: 10)

Xếp loại:
  Giỏi: ████████ 8 (32%)
  Khá:  ██████████ 10 (40%)
  TB:   █████ 5 (20%)
  Yếu:  ██ 2 (8%)

Điểm TB theo môn:
  Toán: 7.20  │ Cao nhất: Nguyễn An (9.5)
  Lý:   6.80  │ Cao nhất: Lê Chi (8.5)
  Hóa:  7.50  │ Cao nhất: Phạm Dũng (10.0)
  Anh:  6.50  │ Cao nhất: Trần Bình (9.0)
  Văn:  7.10  │ Cao nhất: Võ Em (8.5)

HS có TB cao nhất: Nguyễn An (8.50)
HS có TB thấp nhất: Hoàng Gì (4.20)
```

---

### Yêu cầu kỹ thuật:

- ✅ Mảng song song + mảng 2D cho bảng điểm
- ✅ Bubble Sort swap tất cả mảng
- ✅ Tính TB tự động khi thêm/sửa
- ✅ Methods tách rõ ràng (≥12 methods)
- ✅ Validate đầy đủ
- ✅ Format bảng đẹp

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Mảng 2D cho bảng điểm | ⭐⭐⭐ Bắt buộc |
| Thêm + Xem bảng điểm | ⭐⭐⭐ Bắt buộc |
| Tính TB + xếp loại tự động | ⭐⭐⭐ Bắt buộc |
| Sắp xếp (ít nhất theo TB) | ⭐⭐ Quan trọng |
| Tìm kiếm | ⭐⭐ Quan trọng |
| Thống kê đầy đủ | ⭐⭐ Quan trọng |
| Sửa + Xóa | ⭐ Bonus |
| Biểu đồ histogram | ⭐ Bonus |
