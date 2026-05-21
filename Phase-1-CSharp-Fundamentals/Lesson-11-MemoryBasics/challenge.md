# 🏆 Lesson 11 — Challenge: Memory Basics

---

## Challenge: Memory Debugger Simulator 🔍

### Bối cảnh:
> "Xây dựng console app MÔ PHỎNG bộ nhớ. User có thể tạo biến, gán giá trị, xem Stack/Heap, và dự đoán kết quả."

### Yêu cầu:

#### Menu:

```
╔═══════════════════════════════════╗
║   🧠 MEMORY DEBUGGER SIMULATOR   ║
╠═══════════════════════════════════╣
║  1. Tạo biến (value type)        ║
║  2. Tạo mảng (reference type)    ║
║  3. Gán biến = biến              ║
║  4. Sửa giá trị                  ║
║  5. Xem Stack                    ║
║  6. Xem Heap                     ║
║  7. So sánh 2 biến               ║
║  8. Gọi method (simulate)        ║
║  9. Quiz nhanh                   ║
║  0. Thoát                        ║
╚═══════════════════════════════════╝
```

#### Chi tiết:

**1. Tạo biến value type:**
- Nhập tên biến + giá trị (int)
- Lưu vào "Stack" (mảng tên + mảng giá trị)

**2. Tạo mảng:**
- Nhập tên + các phần tử
- "Stack" lưu tên + address giả (HEAP_001, HEAP_002...)
- "Heap" lưu data thật

**3. Gán biến = biến:**
- Nếu value type: copy giá trị
- Nếu reference type: copy address (cùng trỏ)
- In thông báo giải thích đang làm gì

**4. Sửa giá trị:**
- Sửa biến hoặc phần tử mảng
- Nếu 2 reference cùng trỏ → hiển thị cảnh báo

**5. Xem Stack:**
```
═══ STACK ═══
┌────────┬───────────┬──────────┐
│ Tên    │ Giá trị   │ Kiểu     │
├────────┼───────────┼──────────┤
│ age    │ 25        │ value    │
│ score  │ 25        │ value    │ (copy từ age)
│ arr1   │ →HEAP_001 │ reference│
│ arr2   │ →HEAP_001 │ reference│ ← cùng trỏ!
└────────┴───────────┴──────────┘
```

**6. Xem Heap:**
```
═══ HEAP ═══
┌───────────┬────────────────┬──────────────┐
│ Address   │ Data           │ Trỏ bởi      │
├───────────┼────────────────┼──────────────┤
│ HEAP_001  │ [1, 2, 3]      │ arr1, arr2   │
│ HEAP_002  │ [10, 20]       │ nums         │
└───────────┴────────────────┴──────────────┘
```

**7. So sánh:** Kiểm tra 2 biến cùng value/reference không

**8. Simulate method call:** Mô phỏng truyền biến vào method (value copy vs reference share)

**9. Quiz:** Cho tình huống → user dự đoán → kiểm tra đúng/sai

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Tạo biến value + reference | ⭐⭐⭐ Bắt buộc |
| Gán và hiểu copy vs share | ⭐⭐⭐ Bắt buộc |
| Xem Stack/Heap visualization | ⭐⭐⭐ Bắt buộc |
| Sửa giá trị + cảnh báo shared | ⭐⭐ Quan trọng |
| So sánh 2 biến | ⭐⭐ Quan trọng |
| Quiz | ⭐ Bonus |
| Method simulation | ⭐ Bonus |
