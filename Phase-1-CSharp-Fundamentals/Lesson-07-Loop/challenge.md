# 🏆 Lesson 07 — Challenge: Loop

---

## Challenge: Hệ thống quản lý thư viện sách 📚

### Bối cảnh:
> "Xây dựng console app quản lý thư viện mini. Dùng mảng cố định để lưu sách, có CRUD + tìm kiếm + thống kê."

### Yêu cầu:

#### Dữ liệu sách (mảng cố định, tối đa 50 cuốn):
- Mã sách (auto-increment: BK001, BK002...)
- Tên sách
- Tác giả
- Thể loại (Văn học / CNTT / Kinh tế / Khoa học)
- Giá (decimal)
- Số lượng tồn (int)

#### Menu chức năng:

```
╔══════════════════════════════════╗
║     📚 QUẢN LÝ THƯ VIỆN        ║
╠══════════════════════════════════╣
║  1. Thêm sách mới               ║
║  2. Xem danh sách tất cả        ║
║  3. Tìm kiếm sách               ║
║  4. Sửa thông tin sách           ║
║  5. Xóa sách                    ║
║  6. Thống kê                    ║
║  7. Sắp xếp                    ║
║  0. Thoát                       ║
╚══════════════════════════════════╝
```

#### Chi tiết:

**1. Thêm sách:**
- Nhập tên, tác giả, thể loại (switch menu), giá, số lượng
- Mã tự sinh: BK001, BK002...
- Validate tất cả input

**2. Xem danh sách:**
- In bảng đẹp với alignment
- Phân trang: 10 cuốn/trang, nhấn Enter xem trang tiếp

**3. Tìm kiếm (sub-menu):**
- Tìm theo tên (chứa keyword)
- Tìm theo tác giả
- Tìm theo thể loại
- Tìm theo giá (từ X đến Y)
- Dùng `foreach` hoặc `for` + `string.Contains()`

**4. Sửa sách:**
- Nhập mã sách → tìm → hiện info hiện tại → sửa từng field
- Enter để giữ giá trị cũ

**5. Xóa sách:**
- Nhập mã → xác nhận → xóa (dịch mảng)

**6. Thống kê:**
- Tổng số sách, tổng giá trị kho
- Sách đắt nhất / rẻ nhất
- Số sách theo từng thể loại
- Sách sắp hết (tồn ≤ 3)
- Dùng `for` loop để tính

**7. Sắp xếp (Bonus):**
- Theo tên (A-Z)
- Theo giá (thấp → cao)
- Dùng thuật toán **Bubble Sort** (vòng lặp lồng nhau)

### Yêu cầu kỹ thuật:

- ✅ Dùng mảng song song: `string[] titles`, `string[] authors`, `decimal[] prices`...
- ✅ `for` loop cho CRUD, thống kê, sắp xếp
- ✅ `while/do-while` cho menu + validation
- ✅ `foreach` khi chỉ đọc
- ✅ `break` / `continue` hợp lý
- ✅ Nested loop cho tìm kiếm, sắp xếp
- ✅ Format bảng đẹp

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Menu loop hoạt động | ⭐⭐⭐ Bắt buộc |
| Thêm + Xem danh sách | ⭐⭐⭐ Bắt buộc |
| Tìm kiếm (ít nhất theo tên) | ⭐⭐⭐ Bắt buộc |
| Validate đầy đủ | ⭐⭐ Quan trọng |
| Xóa + dịch mảng | ⭐⭐ Quan trọng |
| Thống kê | ⭐⭐ Quan trọng |
| Sửa sách | ⭐ Bonus |
| Sắp xếp Bubble Sort | ⭐ Bonus |
| Phân trang | ⭐ Bonus |
