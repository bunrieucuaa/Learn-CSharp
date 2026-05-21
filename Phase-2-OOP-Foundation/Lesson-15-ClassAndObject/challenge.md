# 🏆 Lesson 15 — Challenge: Class & Object

---

## Challenge: Hệ thống quản lý cửa hàng 🏪

### Yêu cầu:
Refactor lại bài quản lý sản phẩm Phase 1 (dùng mảng song song) thành OOP.

### Classes cần tạo:

```csharp
class Product
{
    public string Id;         // SP001, SP002...
    public string Name;
    public decimal Price;
    public int Stock;
    public string Category;

    public bool Sell(int qty);
    public void Restock(int qty);
    public decimal GetValue();  // Price × Stock
    public void Print(int index);
}
```

### Chức năng (dùng `Product[]`):
1. Thêm sản phẩm (auto ID)
2. Xem danh sách (bảng đẹp)
3. Bán hàng (chọn SP, nhập SL, giảm stock)
4. Nhập hàng (chọn SP, nhập SL, tăng stock)
5. Tìm kiếm (theo tên / category)
6. Sắp xếp (theo giá / tồn kho / giá trị)
7. Thống kê (tổng giá trị kho, SP sắp hết, doanh thu ước tính)

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Class Product với fields + methods | ⭐⭐⭐ Bắt buộc |
| Mảng `Product[]` thay song song | ⭐⭐⭐ Bắt buộc |
| CRUD (thêm, xem, sửa) | ⭐⭐⭐ Bắt buộc |
| Methods trên object (Sell, Print...) | ⭐⭐ Quan trọng |
| Sắp xếp + Tìm kiếm | ⭐⭐ Quan trọng |
| Thống kê | ⭐ Bonus |
