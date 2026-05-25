# Bài 25: List<T> — Thử Thách

---

## 🏆 Shopping Cart System — Hệ Thống Giỏ Hàng

> Xây dựng một hệ thống giỏ hàng mua sắm hoàn chỉnh sử dụng `List<Product>` và `List<CartItem>`

---

### 📋 Mô Tả Dự Án

Bạn là lập trình viên xây dựng module giỏ hàng cho ứng dụng thương mại điện tử.
Hệ thống bao gồm: **Kho hàng** (danh sách sản phẩm) và **Giỏ hàng** (sản phẩm khách chọn mua).

---

### 🏗️ Kiến Trúc Hệ Thống

```
╔══════════════════════════════════════════════════════════╗
║                  SHOPPING CART SYSTEM                     ║
╠══════════════════════════════════════════════════════════╣
║                                                          ║
║  ┌─────────────┐    ┌──────────────┐    ┌────────────┐  ║
║  │   Product    │    │   CartItem   │    │  Category  │  ║
║  │  (Sản phẩm) │───▶│ (Item trong  │    │  (Phân     │  ║
║  │             │    │   giỏ hàng)  │    │   loại)    │  ║
║  └─────────────┘    └──────────────┘    └────────────┘  ║
║         │                   │                            ║
║         ▼                   ▼                            ║
║  ┌─────────────┐    ┌──────────────┐                    ║
║  │    Store     │    │ ShoppingCart │                    ║
║  │  (Cửa hàng) │    │ (Giỏ hàng)  │                    ║
║  │ List<Product>│    │List<CartItem>│                    ║
║  └─────────────┘    └──────────────┘                    ║
║         │                   │                            ║
║         └────────┬──────────┘                            ║
║                  ▼                                       ║
║         ┌────────────────┐                               ║
║         │  ShoppingApp   │                               ║
║         │ (Điều phối)    │                               ║
║         └────────────────┘                               ║
╚══════════════════════════════════════════════════════════╝
```

---

### 📝 Yêu Cầu Chi Tiết

#### 1. Class `Product`

```csharp
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }          // Giá gốc
    public string Category { get; set; }       // "Điện thoại", "Laptop", "Phụ kiện"
    public int StockQuantity { get; set; }     // Số lượng tồn kho
    public double DiscountPercent { get; set; } // % giảm giá (0-100)

    // Tính giá sau giảm
    public double GetFinalPrice() { ... }

    // ToString hiển thị đẹp
    public override string ToString() { ... }
}
```

#### 2. Class `CartItem`

```csharp
class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    // Tổng tiền = FinalPrice × Quantity
    public double GetSubtotal() { ... }

    public override string ToString() { ... }
}
```

#### 3. Class `Store` — Quản lý kho hàng

```csharp
class Store
{
    private List<Product> _products = new List<Product>();

    // Khởi tạo với 10+ sản phẩm mẫu (nhiều category)
    public Store() { InitializeProducts(); }

    public void ShowAllProducts()           // Hiển thị tất cả SP
    public void ShowByCategory(string cat)  // Lọc theo danh mục
    public Product FindById(int id)         // Tìm SP theo ID
    public List<Product> Search(string kw)  // Tìm theo tên
    public List<Product> GetOnSale()        // SP đang giảm giá
    public void SortByPrice(bool ascending) // Sắp xếp theo giá
    public void SortByName()                // Sắp xếp theo tên
    public bool ReduceStock(int id, int qty) // Giảm tồn kho khi mua
    public void RestoreStock(int id, int qty) // Phục hồi khi hủy
}
```

#### 4. Class `ShoppingCart` — Giỏ hàng

```csharp
class ShoppingCart
{
    private List<CartItem> _items = new List<CartItem>();

    public void AddItem(Product p, int qty)    // Thêm SP (nếu đã có → tăng qty)
    public void RemoveItem(int productId)      // Xóa SP khỏi giỏ
    public void UpdateQuantity(int productId, int newQty) // Cập nhật SL
    public void IncreaseQuantity(int productId) // +1
    public void DecreaseQuantity(int productId) // -1 (xóa nếu = 0)

    public double GetSubtotal()     // Tạm tính (chưa giảm)
    public double GetDiscount()     // Tổng giảm giá
    public double GetTotal()        // Tổng thanh toán

    public int GetItemCount()       // Số loại SP trong giỏ
    public int GetTotalQuantity()   // Tổng số lượng tất cả SP

    public void ShowCart()          // Hiển thị giỏ hàng đẹp
    public void Clear()             // Xóa toàn bộ giỏ

    public List<CartItem> GetItemsByCategory(string cat) // Lọc theo loại
    public void SortByPrice()       // Sắp xếp item theo giá
    public void SortByName()        // Sắp xếp item theo tên
}
```

#### 5. Class `ShoppingApp` — Chương trình chính

Điều phối `Store` và `ShoppingCart`, demo đầy đủ luồng mua hàng.

---

### 🎮 Demo Flow

```
╔══════════════════════════════════════════╗
║         🛒 SHOPPING CART DEMO            ║
╚══════════════════════════════════════════╝

═══ 📦 CỬA HÀNG — TẤT CẢ SẢN PHẨM ═══
╔════╦════════════════════╦═══════════════╦════════╦══════╦═══════════╗
║ ID ║ Tên sản phẩm       ║ Giá gốc       ║ Giảm % ║ Kho  ║ Giá bán   ║
╠════╬════════════════════╬═══════════════╬════════╬══════╬═══════════╣
║  1 ║ iPhone 15 Pro      ║ 30,000,000đ   ║  10%   ║  15  ║ 27,000,000║
║  2 ║ Samsung Galaxy S24 ║ 22,000,000đ   ║   5%   ║  20  ║ 20,900,000║
║  3 ║ MacBook Air M3     ║ 32,000,000đ   ║   0%   ║   8  ║ 32,000,000║
║  4 ║ AirPods Pro 2      ║  6,500,000đ   ║  15%   ║  30  ║  5,525,000║
║ ...                                                                   ║
╚════╩════════════════════╩═══════════════╩════════╩══════╩═══════════╝

═══ 🔍 LỌC SẢN PHẨM ═══
📱 Điện thoại: 3 sản phẩm
💻 Laptop: 3 sản phẩm
🎧 Phụ kiện: 4 sản phẩm

🏷️ Đang giảm giá: 5 sản phẩm

═══ 🛒 THÊM VÀO GIỎ HÀNG ═══
✅ Đã thêm iPhone 15 Pro × 1
✅ Đã thêm AirPods Pro 2 × 2
✅ Đã thêm MacBook Air M3 × 1
✅ Đã thêm iPhone 15 Pro × 1 (cập nhật: 2 cái)

═══ 🛒 GIỎ HÀNG CỦA BẠN ═══
╔════╦════════════════════╦═══════════╦═════╦═══════════════╗
║ #  ║ Sản phẩm           ║ Đơn giá   ║ SL  ║ Thành tiền    ║
╠════╬════════════════════╬═══════════╬═════╬═══════════════╣
║  1 ║ iPhone 15 Pro      ║ 27,000,000║  2  ║  54,000,000đ  ║
║  2 ║ AirPods Pro 2      ║  5,525,000║  2  ║  11,050,000đ  ║
║  3 ║ MacBook Air M3     ║ 32,000,000║  1  ║  32,000,000đ  ║
╠════╩════════════════════╩═══════════╩═════╩═══════════════╣
║  Số loại sản phẩm:  3                                     ║
║  Tổng số lượng:      5                                     ║
║  ─────────────────────────────────────────────────────     ║
║  Tạm tính (giá gốc):                      104,500,000đ   ║
║  Giảm giá:                                  -7,450,000đ   ║
║  ═══════════════════════════════════════════════════════   ║
║  💰 TỔNG THANH TOÁN:                       97,050,000đ   ║
╚═══════════════════════════════════════════════════════════╝

═══ ✏️ CẬP NHẬT GIỎ HÀNG ═══
🔄 Cập nhật AirPods Pro 2: 2 → 3
🗑️ Đã xóa MacBook Air M3 khỏi giỏ

═══ 🛒 GIỎ HÀNG SAU CẬP NHẬT ═══
...
💰 TỔNG THANH TOÁN: 70,575,000đ

═══ 📊 THỐNG KÊ ═══
Tổng SP trong giỏ: 2 loại, 5 cái
Tiết kiệm được: 10,425,000đ nhờ giảm giá!
```

---

### ⚡ Yêu Cầu Kỹ Thuật

| # | Yêu cầu | Chi tiết |
|---|---------|---------|
| 1 | **Kiểm tra tồn kho** | Không cho thêm quá số lượng trong kho |
| 2 | **Cộng dồn** | Thêm SP đã có → tăng quantity, không tạo item mới |
| 3 | **Giảm kho** | Khi thêm vào giỏ → giảm StockQuantity |
| 4 | **Phục hồi kho** | Khi xóa khỏi giỏ → hoàn lại StockQuantity |
| 5 | **Tính giá đúng** | Tổng = Σ(FinalPrice × Quantity) |
| 6 | **Validation** | Quantity > 0, ProductId hợp lệ |
| 7 | **Hiển thị đẹp** | Bảng có viền, số tiền format đúng |

---

### 🌟 Bonus (Nâng cao)

1. **Coupon System**: Thêm mã giảm giá cho toàn giỏ hàng (%, cố định, freeship)
2. **Wishlist**: `List<Product>` cho sản phẩm yêu thích, chuyển từ wishlist → cart
3. **Order History**: Khi "thanh toán" → lưu đơn vào `List<Order>`
4. **Recommend**: Gợi ý SP cùng category với SP trong giỏ

---

### 💡 Gợi Ý Triển Khai

```csharp
class ShoppingCart
{
    private List<CartItem> _items = new List<CartItem>();

    public void AddItem(Product product, int quantity)
    {
        // Kiểm tra tồn kho
        if (product.StockQuantity < quantity)
        {
            Console.WriteLine($"❌ Chỉ còn {product.StockQuantity} sản phẩm!");
            return;
        }

        // Kiểm tra đã có trong giỏ chưa
        CartItem existing = _items.Find(i => i.Product.Id == product.Id);

        if (existing != null)
        {
            // Đã có → tăng số lượng
            existing.Quantity += quantity;
            Console.WriteLine($"🔄 Cập nhật {product.Name}: {existing.Quantity} cái");
        }
        else
        {
            // Chưa có → thêm mới
            _items.Add(new CartItem { Product = product, Quantity = quantity });
            Console.WriteLine($"✅ Đã thêm {product.Name} × {quantity}");
        }

        // Giảm tồn kho
        product.StockQuantity -= quantity;
    }

    public void ShowCart()
    {
        Console.WriteLine("\n🛒 GIỎ HÀNG CỦA BẠN:");
        Console.WriteLine(new string('═', 60));

        if (_items.Count == 0)
        {
            Console.WriteLine("  (Giỏ hàng trống)");
            return;
        }

        int index = 1;
        _items.ForEach(item =>
        {
            Console.WriteLine($"  {index}. {item}");
            index++;
        });

        Console.WriteLine(new string('─', 60));
        Console.WriteLine($"  Tạm tính:      {GetSubtotal(),15:N0}đ");
        Console.WriteLine($"  Giảm giá:      {GetDiscount() * -1,15:N0}đ");
        Console.WriteLine(new string('═', 60));
        Console.WriteLine($"  💰 TỔNG:       {GetTotal(),15:N0}đ");
    }
}
```

---

### ✅ Checklist Hoàn Thành

- [ ] Class Product với GetFinalPrice()
- [ ] Class CartItem với GetSubtotal()
- [ ] Store: khởi tạo 10+ sản phẩm, tìm kiếm, lọc, sắp xếp
- [ ] ShoppingCart: Add/Remove/Update/Show
- [ ] Kiểm tra tồn kho khi thêm
- [ ] Cộng dồn khi thêm SP đã có
- [ ] Giảm/phục hồi tồn kho
- [ ] Tính tổng đúng (có giảm giá)
- [ ] Hiển thị bảng đẹp
- [ ] Demo flow hoàn chỉnh
- [ ] (Bonus) Coupon system
- [ ] (Bonus) Wishlist
