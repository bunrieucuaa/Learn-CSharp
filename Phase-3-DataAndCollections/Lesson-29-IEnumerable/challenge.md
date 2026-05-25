# 🏆 Bài 29: Challenge — Data Pipeline 🔄

> **Dự án lớn:** Xây dựng Data Pipeline hoàn chỉnh dùng chuỗi yield-based transformations: **Filter → Transform → Paginate**

---

## 📋 Mô tả dự án

Bạn đang xây dựng một **Data Processing Pipeline** cho hệ thống quản lý sản phẩm. Pipeline nhận data thô, qua nhiều bước xử lý (filter, transform, sort, paginate), và xuất kết quả. Mọi thứ đều dùng `IEnumerable<T>` + `yield return` — **deferred execution** từ đầu đến cuối!

---

## 🏗️ Kiến trúc

```
  ┌─────────────────────────────────────────────────────────────┐
  │                     DATA PIPELINE                           │
  │                                                             │
  │  Source Data           Pipeline Steps          Output        │
  │  ┌──────────┐    ┌─────┐ ┌─────┐ ┌─────┐    ┌──────────┐  │
  │  │ Product  │ →  │Where│→│Select│→│Order│ →  │ Display  │  │
  │  │   List   │    │     │ │     │ │By   │    │ Paginate │  │
  │  │ (100+)   │    │yield│ │yield│ │yield│    │ Format   │  │
  │  └──────────┘    └─────┘ └─────┘ └─────┘    └──────────┘  │
  │                                                             │
  │  Tất cả đều IEnumerable<T> — Deferred Execution!           │
  └─────────────────────────────────────────────────────────────┘
```

---

## 📝 Yêu cầu chi tiết

### Phần 1: Model

```csharp
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }        // "Electronics", "Books", "Clothing", "Food"
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public double Rating { get; set; }          // 1.0 - 5.0
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }

    public override string ToString()
        => $"[{Id:D3}] {Name,-25} | {Category,-12} | " +
           $"{Price,10:N0}đ | Stock:{Stock,4} | ★{Rating:F1}";
}
```

### Phần 2: DataGenerator — Tạo dữ liệu mẫu

```csharp
static class DataGenerator
{
    // Tạo 100+ sản phẩm giả lập bằng yield return!
    public static IEnumerable<Product> GenerateProducts(int count)
    {
        string[] categories = { "Electronics", "Books", "Clothing", "Food" };
        string[] adjectives = { "Super", "Pro", "Ultra", "Mega", "Basic" };
        string[] nouns = { "Phone", "Book", "Shirt", "Snack", "Laptop",
                          "Novel", "Jacket", "Candy", "Tablet", "Guide" };
        Random rng = new Random(42);    // Seed cố định để kết quả nhất quán

        for (int i = 1; i <= count; i++)
        {
            yield return new Product
            {
                Id = i,
                Name = $"{adjectives[rng.Next(adjectives.Length)]} " +
                       $"{nouns[rng.Next(nouns.Length)]} {i}",
                Category = categories[rng.Next(categories.Length)],
                Price = rng.Next(10, 5000) * 1000m,
                Stock = rng.Next(0, 200),
                Rating = Math.Round(rng.NextDouble() * 4 + 1, 1),
                CreatedDate = DateTime.Now.AddDays(-rng.Next(1, 365)),
                IsActive = rng.Next(100) > 15   // 85% active
            };
        }
    }
}
```

### Phần 3: Pipeline — Các bước xử lý (TẤT CẢ dùng yield!)

```csharp
static class Pipeline
{
    // ═══ FILTER — Lọc dữ liệu ═══

    // Lọc theo category
    public static IEnumerable<Product> WhereCategory(
        IEnumerable<Product> source, string category)
    {
        foreach (Product p in source)
            if (p.Category == category)
                yield return p;
    }

    // Lọc theo khoảng giá
    public static IEnumerable<Product> WherePriceRange(
        IEnumerable<Product> source, decimal min, decimal max)
    { /* yield return */ }

    // Lọc chỉ active
    public static IEnumerable<Product> WhereActive(
        IEnumerable<Product> source)
    { /* yield return */ }

    // Lọc theo rating tối thiểu
    public static IEnumerable<Product> WhereMinRating(
        IEnumerable<Product> source, double minRating)
    { /* yield return */ }

    // Lọc còn hàng (stock > 0)
    public static IEnumerable<Product> WhereInStock(
        IEnumerable<Product> source)
    { /* yield return */ }

    // ═══ TRANSFORM — Biến đổi ═══

    // Giảm giá theo phần trăm (trả về Product mới với Price mới)
    public static IEnumerable<Product> ApplyDiscount(
        IEnumerable<Product> source, decimal discountPercent)
    { /* yield return new Product { ...copy, Price = adjusted } */ }

    // Chỉ lấy các trường cần thiết (projection)
    public static IEnumerable<string> SelectSummary(
        IEnumerable<Product> source)
    {
        foreach (Product p in source)
            yield return $"{p.Name} — {p.Price:N0}đ (★{p.Rating:F1})";
    }

    // ═══ SORT — Sắp xếp (phải materialize!) ═══

    // Sort theo giá (cần tạo List tạm để sort)
    public static IEnumerable<Product> OrderByPrice(
        IEnumerable<Product> source, bool ascending = true)
    {
        List<Product> list = new List<Product>(source);
        list.Sort((a, b) => ascending
            ? a.Price.CompareTo(b.Price)
            : b.Price.CompareTo(a.Price));
        foreach (Product p in list)
            yield return p;
    }

    // Sort theo rating
    public static IEnumerable<Product> OrderByRating(
        IEnumerable<Product> source, bool ascending = false)
    { /* similar to OrderByPrice */ }

    // ═══ PAGINATE — Phân trang ═══

    public static IEnumerable<Product> Skip(
        IEnumerable<Product> source, int count)
    {
        int skipped = 0;
        foreach (Product p in source)
        {
            if (skipped < count) { skipped++; continue; }
            yield return p;
        }
    }

    public static IEnumerable<Product> Take(
        IEnumerable<Product> source, int count)
    {
        int taken = 0;
        foreach (Product p in source)
        {
            if (taken >= count) yield break;
            yield return p;
            taken++;
        }
    }

    // Lấy trang N (1-indexed), mỗi trang pageSize phần tử
    public static IEnumerable<Product> Page(
        IEnumerable<Product> source, int pageNumber, int pageSize)
    {
        return Take(Skip(source, (pageNumber - 1) * pageSize), pageSize);
    }

    // ═══ AGGREGATE — Thống kê (materialize) ═══

    public static int Count(IEnumerable<Product> source)
    { /* đếm */ }

    public static decimal AveragePrice(IEnumerable<Product> source)
    { /* tính trung bình giá */ }

    public static decimal TotalValue(IEnumerable<Product> source)
    { /* tổng giá trị = sum(Price * Stock) */ }
}
```

### Phần 4: PipelineDisplay — Hiển thị kết quả

```csharp
static class PipelineDisplay
{
    // In bảng sản phẩm với header
    public static void PrintTable(string title, IEnumerable<Product> products)
    {
        Console.WriteLine($"\n═══ {title} ═══");
        Console.WriteLine("  ID  | Tên                       | Loại         | " +
                         "      Giá | Stock | Rating");
        Console.WriteLine("  " + new string('─', 80));

        int count = 0;
        foreach (Product p in products)
        {
            Console.WriteLine($"  {p}");
            count++;
        }
        Console.WriteLine($"  ── Tổng: {count} sản phẩm ──");
    }

    // In kết quả thống kê
    public static void PrintStats(string title, IEnumerable<Product> products)
    {
        // Phải ToList() vì duyệt nhiều lần!
        List<Product> list = new List<Product>(products);
        Console.WriteLine($"\n  📊 {title}:");
        Console.WriteLine($"     Số lượng: {list.Count}");
        // ... thêm stats
    }
}
```

---

## 🎮 Main Program — Demo Pipelines

```csharp
static void Main()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   DATA PIPELINE — Product System     ║");
    Console.WriteLine("╚══════════════════════════════════════╝\n");

    // === Tạo dữ liệu ===
    IEnumerable<Product> allProducts = DataGenerator.GenerateProducts(100);

    // ═══════════════════════════════════════
    // PIPELINE 1: Electronics, giá 1-3 triệu, rating >= 4.0
    // ═══════════════════════════════════════
    IEnumerable<Product> pipeline1 = Pipeline.WhereCategory(allProducts, "Electronics");
    pipeline1 = Pipeline.WherePriceRange(pipeline1, 1_000_000, 3_000_000);
    pipeline1 = Pipeline.WhereMinRating(pipeline1, 4.0);
    pipeline1 = Pipeline.WhereActive(pipeline1);
    pipeline1 = Pipeline.OrderByPrice(pipeline1);

    PipelineDisplay.PrintTable("Electronics, 1-3tr, ★4.0+, Active", pipeline1);

    // ═══════════════════════════════════════
    // PIPELINE 2: Sản phẩm giảm giá 20%
    // ═══════════════════════════════════════
    IEnumerable<Product> pipeline2 = Pipeline.WhereCategory(allProducts, "Books");
    pipeline2 = Pipeline.WhereInStock(pipeline2);
    pipeline2 = Pipeline.ApplyDiscount(pipeline2, 20);
    pipeline2 = Pipeline.OrderByRating(pipeline2);
    pipeline2 = Pipeline.Take(pipeline2, 5);

    PipelineDisplay.PrintTable("Top 5 Books (giảm 20%, còn hàng)", pipeline2);

    // ═══════════════════════════════════════
    // PIPELINE 3: Phân trang — Trang 2, mỗi trang 10
    // ═══════════════════════════════════════
    IEnumerable<Product> allActive = Pipeline.WhereActive(allProducts);
    allActive = Pipeline.OrderByPrice(allActive, ascending: false);
    IEnumerable<Product> page2 = Pipeline.Page(allActive, 2, 10);

    PipelineDisplay.PrintTable("Trang 2 (Active, giá giảm dần)", page2);

    // ═══════════════════════════════════════
    // PIPELINE 4: Thống kê theo category
    // ═══════════════════════════════════════
    Console.WriteLine("\n═══ THỐNG KÊ THEO CATEGORY ═══");
    string[] categories = { "Electronics", "Books", "Clothing", "Food" };
    foreach (string cat in categories)
    {
        IEnumerable<Product> catProducts =
            Pipeline.WhereCategory(allProducts, cat);
        PipelineDisplay.PrintStats(cat, catProducts);
    }

    // ═══════════════════════════════════════
    // PIPELINE 5: Chuỗi SelectSummary (string output)
    // ═══════════════════════════════════════
    Console.WriteLine("\n═══ SUMMARY VIEW — Top 5 đắt nhất ═══");
    IEnumerable<Product> top5 = Pipeline.OrderByPrice(
        Pipeline.WhereActive(allProducts), ascending: false);
    top5 = Pipeline.Take(top5, 5);
    IEnumerable<string> summaries = Pipeline.SelectSummary(top5);

    int rank = 1;
    foreach (string summary in summaries)
    {
        Console.WriteLine($"  #{rank++}: {summary}");
    }
}
```

---

## ✅ Output mong đợi (tham khảo)

```
╔══════════════════════════════════════╗
║   DATA PIPELINE — Product System     ║
╚══════════════════════════════════════╝

═══ Electronics, 1-3tr, ★4.0+, Active ═══
  ID  | Tên                       | Loại         |       Giá | Stock | Rating
  ────────────────────────────────────────────────────────────────────────────────
  [012] Super Phone 12             | Electronics  |  1,200,000đ | Stock:  45 | ★4.2
  [034] Pro Laptop 34              | Electronics  |  2,100,000đ | Stock: 123 | ★4.7
  [078] Ultra Tablet 78            | Electronics  |  2,800,000đ | Stock:  67 | ★4.5
  ── Tổng: 3 sản phẩm ──

═══ Top 5 Books (giảm 20%, còn hàng) ═══
  ...

═══ Trang 2 (Active, giá giảm dần) ═══
  ...

═══ THỐNG KÊ THEO CATEGORY ═══
  📊 Electronics:
     Số lượng: 28
     Giá trung bình: 2,450,000đ
     Tổng giá trị kho: 156,800,000đ

═══ SUMMARY VIEW — Top 5 đắt nhất ═══
  #1: Mega Laptop 89 — 4,900,000đ (★3.8)
  #2: Ultra Phone 45 — 4,750,000đ (★4.2)
  #3: Pro Tablet 67 — 4,600,000đ (★4.5)
  ...
```

---

## 🎯 Tiêu chí đánh giá

| Tiêu chí | Điểm | Mô tả |
|-----------|-------|--------|
| yield return đúng | 25% | Tất cả filter/transform dùng yield, không tạo List trung gian |
| Pipeline chaining | 25% | Chain nhiều bước liền mạch, deferred execution |
| Paginate đúng | 15% | Skip + Take + Page hoạt động chính xác |
| DataGenerator | 10% | Tạo dữ liệu mẫu bằng yield, đa dạng |
| Display + Stats | 15% | Hiển thị bảng đẹp, thống kê chính xác |
| Code quality | 10% | Clean code, naming, separation of concerns |

---

## 💡 Bonus (tùy chọn)

1. **Pipeline Builder pattern:**
   ```csharp
   // Fluent API:
   var result = new PipelineBuilder<Product>(allProducts)
       .Where(p => p.Category == "Electronics")
       .Where(p => p.Price > 1_000_000)
       .OrderBy(p => p.Rating)
       .Page(1, 10)
       .Execute();
   ```

2. **Performance Counter:** Thêm đếm số phần tử được xử lý tại mỗi bước → chứng minh lazy evaluation tiết kiệm.

3. **Export Pipeline:** `IEnumerable<string> ToCsv(IEnumerable<Product>)` — xuất ra CSV bằng yield.

4. **Reverse Iterator:** `IEnumerable<T> Reverse(IEnumerable<T>)` — đảo ngược chuỗi.

5. **Distinct:** `IEnumerable<string> DistinctCategories(IEnumerable<Product>)` — dùng HashSet + yield.

> **Mục tiêu:** Hoàn thành trong 60-90 phút. Đây là bài tổng hợp toàn bộ kiến thức IEnumerable! 🚀
