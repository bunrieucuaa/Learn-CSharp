# 🏆 Lesson 19 — Challenge: E-Commerce Product Catalog

---

## 📋 Đề bài: Hệ thống Quản lý Sản phẩm E-Commerce

Xây dựng hệ thống quản lý sản phẩm cho trang thương mại điện tử, áp dụng **encapsulation hoàn chỉnh** — tất cả data được bảo vệ, tất cả business rules được đóng gói bên trong class.

---

## 🏗️ Kiến trúc

```
┌──────────────────────────────────────────────────────────┐
│                    ProductCatalog                         │
│                                                          │
│  🔒 Private:                                             │
│  ├── List<Product> _products                             │
│  ├── Dictionary<string, int> _categoryCount              │
│  ├── int _nextProductId                                  │
│  └── Business logic methods                              │
│                                                          │
│  🌍 Public API:                                          │
│  ├── IReadOnlyList<Product> Products                     │
│  ├── AddProduct() / RemoveProduct()                      │
│  ├── SearchByName() / FilterByCategory()                 │
│  ├── FilterByPriceRange()                                │
│  ├── ApplyDiscount() / RestorePrice()                    │
│  ├── Restock() / Sell()                                  │
│  └── PrintReport()                                       │
│                                                          │
│  ┌────────────────────────────────────────────────┐      │
│  │               Product                          │      │
│  │  🔒 Private: _originalPrice, _discountPercent  │      │
│  │  🌍 Public: Name, Price, Stock, Category...    │      │
│  │  📊 Computed: DiscountedPrice, Status, Rating  │      │
│  └────────────────────────────────────────────────┘      │
└──────────────────────────────────────────────────────────┘
```

---

## 📦 Class Product

### Private fields:
- `_originalPrice` — giá gốc
- `_discountPercent` — % giảm (0-80)
- `_ratings` — `List<int>` danh sách đánh giá (1-5 sao)

### Public properties:
| Property | Kiểu | Ghi chú |
|----------|------|---------|
| `ProductId` | `string` | Readonly, auto-gen "PRD-001" |
| `Name` | `string` | Validate: không rỗng, 2-100 ký tự |
| `Category` | `string` | Validate: không rỗng |
| `Price` | `decimal` | Full property, validate > 0 |
| `Stock` | `int` | Validate >= 0 |
| `CreatedAt` | `DateTime` | Readonly |

### Computed properties:
| Property | Logic |
|----------|-------|
| `DiscountedPrice` | Price × (1 - _discountPercent / 100) |
| `DiscountPercent` | Readonly expose _discountPercent |
| `HasDiscount` | _discountPercent > 0 |
| `IsInStock` | Stock > 0 |
| `StockStatus` | "🔴 Hết hàng" / "🟡 Sắp hết" (≤5) / "🟢 Còn hàng" |
| `AverageRating` | Trung bình _ratings, null nếu chưa có |
| `RatingStars` | "⭐⭐⭐⭐" visual (round) |
| `RatingCount` | Số lượt đánh giá |
| `InventoryValue` | DiscountedPrice × Stock |

### Methods:
- `ApplyDiscount(int percent)` — validate 0-80%
- `RemoveDiscount()` — reset về giá gốc
- `AddRating(int stars)` — validate 1-5
- `Sell(int quantity)` — giảm stock, return bool
- `Restock(int quantity)` — tăng stock
- `PrintDetail()` — in chi tiết sản phẩm

---

## 📦 Class ProductCatalog

### Private:
- `List<Product> _products`
- `int _nextId` — auto-increment ID

### Public readonly:
- `IReadOnlyList<Product> Products`
- `int ProductCount`
- `int TotalStock` — tổng tồn kho
- `decimal TotalInventoryValue` — tổng giá trị tồn kho

### Methods:

| Method | Mô tả |
|--------|-------|
| `AddProduct(name, category, price, stock)` | Validate + tạo Product, return Product |
| `RemoveProduct(productId)` | Chỉ xóa sản phẩm hết hàng |
| `FindById(productId)` | Tìm theo ID |
| `SearchByName(keyword)` | Tìm theo tên (contains, case-insensitive) → IReadOnlyList |
| `FilterByCategory(category)` | Lọc theo danh mục → IReadOnlyList |
| `FilterByPriceRange(min, max)` | Lọc theo khoảng giá → IReadOnlyList |
| `GetInStockProducts()` | Lấy sản phẩm còn hàng |
| `GetTopRated(count)` | Top sản phẩm đánh giá cao nhất |
| `ApplyDiscountToCategory(category, percent)` | Giảm giá theo danh mục |
| `PrintCatalog()` | In toàn bộ catalog dạng bảng |
| `PrintReport()` | In báo cáo tổng hợp |

---

## 🎯 Yêu cầu bắt buộc

### Encapsulation rules:
1. ❌ **KHÔNG** có public field nào
2. ❌ **KHÔNG** expose List trực tiếp (phải dùng IReadOnlyList)
3. ✅ Tất cả property có validation trong setter
4. ✅ Business rules nằm bên trong class (discount %, rating range, stock rules)
5. ✅ Private helper methods cho logic phức tạp
6. ✅ Computed properties cho giá trị tính toán

### Business rules (phải encapsulate):
1. Discount tối đa 80%
2. Rating từ 1-5 sao
3. Không bán khi hết hàng
4. Chỉ xóa sản phẩm hết hàng
5. Không chấp nhận giá âm hoặc stock âm
6. Tên sản phẩm unique trong catalog

---

## 💻 Main Program demo:

```csharp
class Program
{
    static void Main()
    {
        Console.WriteLine("═══ E-COMMERCE PRODUCT CATALOG ═══\n");

        var catalog = new ProductCatalog();

        // ===== THÊM SẢN PHẨM =====
        catalog.AddProduct("iPhone 15 Pro", "Điện thoại", 28_990_000m, 15);
        catalog.AddProduct("Samsung S24", "Điện thoại", 22_990_000m, 20);
        catalog.AddProduct("MacBook Air M3", "Laptop", 32_990_000m, 8);
        catalog.AddProduct("iPad Air", "Tablet", 16_990_000m, 12);
        catalog.AddProduct("AirPods Pro 2", "Phụ kiện", 5_990_000m, 50);
        catalog.AddProduct("Ốp lưng iPhone", "Phụ kiện", 250_000m, 100);
        catalog.AddProduct("Cáp USB-C", "Phụ kiện", 150_000m, 0);  // Hết hàng

        // ===== THÊM ĐÁNH GIÁ =====
        var iphone = catalog.FindById("PRD-001");
        iphone?.AddRating(5);
        iphone?.AddRating(4);
        iphone?.AddRating(5);

        var samsung = catalog.FindById("PRD-002");
        samsung?.AddRating(4);
        samsung?.AddRating(3);

        // ===== GIẢM GIÁ DANH MỤC =====
        catalog.ApplyDiscountToCategory("Phụ kiện", 20);  // Phụ kiện giảm 20%

        // ===== IN CATALOG =====
        catalog.PrintCatalog();

        // ===== TÌM KIẾM =====
        Console.WriteLine("\n═══ TÌM KIẾM 'iphone' ═══");
        var results = catalog.SearchByName("iphone");
        foreach (var p in results) p.PrintDetail();

        // ===== LỌC THEO GIÁ =====
        Console.WriteLine("\n═══ GIÁ 10-20 TRIỆU ═══");
        var filtered = catalog.FilterByPriceRange(10_000_000m, 20_000_000m);
        foreach (var p in filtered) p.PrintDetail();

        // ===== BÁN HÀNG =====
        iphone?.Sell(3);
        Console.WriteLine($"\nSau bán 3 iPhone: Stock = {iphone?.Stock}");

        // ===== BÁO CÁO =====
        catalog.PrintReport();

        // ===== ENCAPSULATION CHECK =====
        // catalog._products.Clear();       // ❌ private!
        // catalog.Products.Add(...);        // ❌ IReadOnlyList!
        // iphone._originalPrice = 0;       // ❌ private!
        // iphone.DiscountedPrice = 100;     // ❌ computed!
    }
}
```

---

## 📊 Output mẫu:

```
═══ E-COMMERCE PRODUCT CATALOG ═══

┌──────────┬───────────────────┬────────────┬──────────────┬──────┬───────────┬────────┐
│ ID       │ Tên               │ Danh mục   │ Giá          │  SL  │ Status    │ Rating │
├──────────┼───────────────────┼────────────┼──────────────┼──────┼───────────┼────────┤
│ PRD-001  │ iPhone 15 Pro     │ Điện thoại │ 28,990,000   │   15 │ 🟢 Còn   │ ⭐⭐⭐⭐⭐│
│ PRD-002  │ Samsung S24       │ Điện thoại │ 22,990,000   │   20 │ 🟢 Còn   │ ⭐⭐⭐⭐ │
│ PRD-003  │ MacBook Air M3    │ Laptop     │ 32,990,000   │    8 │ 🟢 Còn   │  N/A   │
│ PRD-004  │ iPad Air          │ Tablet     │ 16,990,000   │   12 │ 🟢 Còn   │  N/A   │
│ PRD-005  │ AirPods Pro 2     │ Phụ kiện   │  4,792,000 ↓ │   50 │ 🟢 Còn   │  N/A   │
│ PRD-006  │ Ốp lưng iPhone   │ Phụ kiện   │    200,000 ↓ │  100 │ 🟢 Còn   │  N/A   │
│ PRD-007  │ Cáp USB-C        │ Phụ kiện   │    120,000 ↓ │    0 │ 🔴 Hết   │  N/A   │
└──────────┴───────────────────┴────────────┴──────────────┴──────┴───────────┴────────┘

═══ BÁO CÁO TỔNG HỢP ═══
  Tổng sản phẩm    : 7
  Còn hàng         : 6
  Hết hàng         : 1
  Tổng tồn kho     : 205
  Tổng giá trị     : 1,xxx,xxx,xxx VNĐ
  Đang giảm giá    : 3 sản phẩm
```

---

## 📊 Tiêu chí chấm điểm

| Tiêu chí | Mô tả | Điểm |
|----------|-------|------|
| Tất cả field private | Không có public field nào | ⭐⭐⭐ |
| Property validation | Setter có validate đầy đủ | ⭐⭐⭐ |
| Computed properties | Giá trị tự tính chính xác | ⭐⭐ |
| Collection encapsulation | IReadOnlyList, không expose List | ⭐⭐⭐ |
| Private helper methods | Logic nội bộ đóng gói | ⭐⭐ |
| Business rules trong class | Discount, rating, stock rules | ⭐⭐⭐ |
| Code sạch, naming đúng | _camelCase, PascalCase | ⭐ |
| Program chạy đúng | Output đẹp, không lỗi | ⭐⭐ |

> 💡 **Gợi ý**: Bắt đầu với class `Product` trước, test xong rồi mới làm `ProductCatalog`.
