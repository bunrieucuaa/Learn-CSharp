# Bài 26: Dictionary<TKey, TValue> — Ví Dụ Minh Họa

---

## Ví dụ 1: Word Counter — Đếm Từ Trong Chuỗi

> 🎯 Ứng dụng kinh điển của Dictionary: đếm tần suất xuất hiện

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VÍ DỤ 1: WORD COUNTER ===\n");

        string text = "con mèo con đang đuổi con chuột con chuột chạy " +
                       "nhanh con mèo chạy nhanh hơn con mèo bắt được con chuột";

        Console.WriteLine($"📝 Văn bản: \"{text}\"\n");

        // ========== BƯỚC 1: Tách từ ==========
        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"Tổng số từ: {words.Length}\n");

        // ========== BƯỚC 2: Đếm tần suất ==========
        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        foreach (string word in words)
        {
            string lower = word.ToLower();

            if (wordCount.TryGetValue(lower, out int count))
            {
                wordCount[lower] = count + 1; // Đã có → tăng đếm
            }
            else
            {
                wordCount[lower] = 1;          // Chưa có → bắt đầu từ 1
            }
        }

        // ========== BƯỚC 3: Hiển thị kết quả ==========
        Console.WriteLine("📊 TẦN SUẤT XUẤT HIỆN:");
        Console.WriteLine("╔══════════════════╦═══════╦══════════════════╗");
        Console.WriteLine("║  Từ              ║ Số lần ║ Biểu đồ         ║");
        Console.WriteLine("╠══════════════════╬═══════╬══════════════════╣");

        foreach (var (word, count) in wordCount)
        {
            string bar = new string('█', count);
            Console.WriteLine($"║  {word,-16} ║   {count,-4}║ {bar,-17}║");
        }

        Console.WriteLine("╚══════════════════╩═══════╩══════════════════╝");

        // ========== BƯỚC 4: Thống kê ==========
        Console.WriteLine($"\n📊 THỐNG KÊ:");
        Console.WriteLine($"  Tổng số từ: {words.Length}");
        Console.WriteLine($"  Từ unique: {wordCount.Count}");

        // Tìm từ xuất hiện nhiều nhất
        string mostCommon = "";
        int maxCount = 0;
        foreach (var (word, count) in wordCount)
        {
            if (count > maxCount)
            {
                maxCount = count;
                mostCommon = word;
            }
        }
        Console.WriteLine($"  Từ nhiều nhất: \"{mostCommon}\" ({maxCount} lần)");

        // Từ chỉ xuất hiện 1 lần
        Console.Write("  Từ xuất hiện 1 lần: ");
        List<string> unique = new List<string>();
        foreach (var (word, count) in wordCount)
        {
            if (count == 1) unique.Add(word);
        }
        Console.WriteLine(string.Join(", ", unique));

        // ========== BƯỚC 5: Đếm ký tự (bonus) ==========
        Console.WriteLine("\n📊 TẦN SUẤT KÝ TỰ:");
        Dictionary<char, int> charCount = new Dictionary<char, int>();

        foreach (char c in text.Replace(" ", "").ToLower())
        {
            if (charCount.TryGetValue(c, out int cc))
                charCount[c] = cc + 1;
            else
                charCount[c] = 1;
        }

        // Sắp xếp theo tần suất giảm dần
        List<KeyValuePair<char, int>> charList =
            new List<KeyValuePair<char, int>>(charCount);
        charList.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var pair in charList)
        {
            string bar = new string('▓', pair.Value);
            Console.WriteLine($"  '{pair.Key}': {pair.Value,3} | {bar}");
        }
    }
}
```

**Kết quả mong đợi:**
```
=== VÍ DỤ 1: WORD COUNTER ===

📝 Văn bản: "con mèo con đang đuổi con chuột con chuột chạy ..."

Tổng số từ: 18

📊 TẦN SUẤT XUẤT HIỆN:
╔══════════════════╦═══════╦══════════════════╗
║  Từ              ║ Số lần ║ Biểu đồ         ║
╠══════════════════╬═══════╬══════════════════╣
║  con             ║   6   ║ ██████           ║
║  mèo             ║   3   ║ ███              ║
║  chuột           ║   3   ║ ███              ║
║  chạy            ║   2   ║ ██               ║
║  nhanh           ║   2   ║ ██               ║
║  đang            ║   1   ║ █                ║
║  đuổi            ║   1   ║ █                ║
║  hơn             ║   1   ║ █                ║
║  bắt             ║   1   ║ █                ║
║  được            ║   1   ║ █                ║
╚══════════════════╩═══════╩══════════════════╝

📊 THỐNG KÊ:
  Tổng số từ: 18
  Từ unique: 10
  Từ nhiều nhất: "con" (6 lần)
  Từ xuất hiện 1 lần: đang, đuổi, hơn, bắt, được
```

---

## Ví dụ 2: Phone Book — Danh Bạ Điện Thoại

> 🎯 CRUD đầy đủ trên Dictionary + TryGetValue pattern

```csharp
using System;
using System.Collections.Generic;

class PhoneBook
{
    private Dictionary<string, string> _contacts =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // ➕ Thêm liên hệ
    public void AddContact(string name, string phone)
    {
        if (_contacts.ContainsKey(name))
        {
            Console.WriteLine($"⚠️ \"{name}\" đã tồn tại (SĐT: {_contacts[name]})");
            Console.Write($"   Bạn muốn cập nhật không? (y/n): ");
            // Auto-yes for demo
            _contacts[name] = phone;
            Console.WriteLine($"   ✅ Đã cập nhật: {name} → {phone}");
        }
        else
        {
            _contacts[name] = phone;
            Console.WriteLine($"✅ Đã thêm: {name} → {phone}");
        }
    }

    // 🔍 Tìm số điện thoại theo tên
    public void LookupByName(string name)
    {
        if (_contacts.TryGetValue(name, out string phone))
        {
            Console.WriteLine($"📞 {name}: {phone}");
        }
        else
        {
            Console.WriteLine($"❌ Không tìm thấy \"{name}\" trong danh bạ");
        }
    }

    // 🔍 Tìm tên theo số điện thoại (reverse lookup)
    public void LookupByPhone(string phone)
    {
        Console.Write($"🔍 Tìm số {phone}: ");
        foreach (var (name, p) in _contacts)
        {
            if (p == phone)
            {
                Console.WriteLine($"→ {name}");
                return;
            }
        }
        Console.WriteLine("Không tìm thấy");
    }

    // 🔍 Tìm kiếm theo từ khóa
    public void Search(string keyword)
    {
        List<KeyValuePair<string, string>> results =
            new List<KeyValuePair<string, string>>();

        foreach (var pair in _contacts)
        {
            if (pair.Key.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                pair.Value.Contains(keyword))
            {
                results.Add(pair);
            }
        }

        Console.WriteLine($"\n🔍 Tìm \"{keyword}\": {results.Count} kết quả");
        foreach (var (name, phone) in results)
        {
            Console.WriteLine($"  📞 {name}: {phone}");
        }
    }

    // 🗑️ Xóa liên hệ
    public void DeleteContact(string name)
    {
        if (_contacts.Remove(name))
            Console.WriteLine($"🗑️ Đã xóa \"{name}\"");
        else
            Console.WriteLine($"❌ Không tìm thấy \"{name}\"");
    }

    // 📋 Hiển thị tất cả
    public void ShowAll()
    {
        Console.WriteLine($"\n📱 DANH BẠ ({_contacts.Count} liên hệ):");
        Console.WriteLine("╔════╦══════════════════════╦════════════════╗");
        Console.WriteLine("║ #  ║ Tên                  ║ Số điện thoại  ║");
        Console.WriteLine("╠════╬══════════════════════╬════════════════╣");

        int i = 1;
        foreach (var (name, phone) in _contacts)
        {
            Console.WriteLine($"║ {i,-3}║ {name,-21}║ {phone,-15}║");
            i++;
        }

        Console.WriteLine("╚════╩══════════════════════╩════════════════╝");
    }

    // 📊 Thống kê
    public void ShowStats()
    {
        Console.WriteLine($"\n📊 THỐNG KÊ:");
        Console.WriteLine($"  Tổng liên hệ: {_contacts.Count}");

        // Đếm theo đầu số
        Dictionary<string, int> prefixCount = new Dictionary<string, int>();
        foreach (var phone in _contacts.Values)
        {
            string prefix = phone.Length >= 3 ? phone.Substring(0, 3) : phone;
            if (prefixCount.TryGetValue(prefix, out int count))
                prefixCount[prefix] = count + 1;
            else
                prefixCount[prefix] = 1;
        }

        Console.WriteLine("  Theo đầu số:");
        foreach (var (prefix, count) in prefixCount)
        {
            Console.WriteLine($"    {prefix}xxx: {count} số");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine("║    📱 PHONE BOOK APP          ║");
        Console.WriteLine("╚═══════════════════════════════╝\n");

        PhoneBook pb = new PhoneBook();

        // ========== THÊM LIÊN HỆ ==========
        Console.WriteLine("--- THÊM LIÊN HỆ ---");
        pb.AddContact("Nguyễn Văn An",    "0901234567");
        pb.AddContact("Trần Thị Bình",    "0912345678");
        pb.AddContact("Lê Hoàng Chi",     "0987654321");
        pb.AddContact("Phạm Minh Dũng",   "0909876543");
        pb.AddContact("Hoàng Thị Em",     "0934567890");
        pb.AddContact("Vũ Đức Phong",     "0978901234");

        pb.ShowAll();

        // ========== TÌM KIẾM ==========
        Console.WriteLine("\n--- TÌM KIẾM ---");
        pb.LookupByName("Lê Hoàng Chi");
        pb.LookupByName("Nguyễn Văn Xyz");
        pb.LookupByPhone("0912345678");

        // ========== TÌM THEO TỪ KHÓA ==========
        pb.Search("Nguyễn");
        pb.Search("090");

        // ========== CẬP NHẬT ==========
        Console.WriteLine("\n--- CẬP NHẬT ---");
        pb.AddContact("Nguyễn Văn An", "0901111111"); // Cập nhật SĐT

        // ========== XÓA ==========
        Console.WriteLine("\n--- XÓA ---");
        pb.DeleteContact("Phạm Minh Dũng");
        pb.DeleteContact("Người Lạ");

        pb.ShowAll();
        pb.ShowStats();
    }
}
```

**Kết quả mong đợi:**
```
╔═══════════════════════════════╗
║    📱 PHONE BOOK APP          ║
╚═══════════════════════════════╝

--- THÊM LIÊN HỆ ---
✅ Đã thêm: Nguyễn Văn An → 0901234567
✅ Đã thêm: Trần Thị Bình → 0912345678
✅ Đã thêm: Lê Hoàng Chi → 0987654321
✅ Đã thêm: Phạm Minh Dũng → 0909876543
✅ Đã thêm: Hoàng Thị Em → 0934567890
✅ Đã thêm: Vũ Đức Phong → 0978901234

📱 DANH BẠ (6 liên hệ):
╔════╦══════════════════════╦════════════════╗
║ #  ║ Tên                  ║ Số điện thoại  ║
╠════╬══════════════════════╬════════════════╣
║ 1  ║ Nguyễn Văn An        ║ 0901234567     ║
║ 2  ║ Trần Thị Bình        ║ 0912345678     ║
║ ...                                         ║
╚════╩══════════════════════╩════════════════╝

--- TÌM KIẾM ---
📞 Lê Hoàng Chi: 0987654321
❌ Không tìm thấy "Nguyễn Văn Xyz" trong danh bạ
🔍 Tìm số 0912345678: → Trần Thị Bình
```

---

## Ví dụ 3: Student Grades — Dictionary<string, List\<double\>>

> 🎯 Dictionary với Value là List — nhóm dữ liệu và tính toán

```csharp
using System;
using System.Collections.Generic;

class GradeBook
{
    // Key: Tên môn học, Value: Danh sách điểm
    private Dictionary<string, List<double>> _grades =
        new Dictionary<string, List<double>>();

    // Thêm điểm cho môn học
    public void AddGrade(string subject, double grade)
    {
        if (!_grades.ContainsKey(subject))
        {
            _grades[subject] = new List<double>();
        }
        _grades[subject].Add(grade);
        Console.WriteLine($"  ✅ {subject}: +{grade}");
    }

    // Thêm nhiều điểm cùng lúc
    public void AddGrades(string subject, params double[] grades)
    {
        foreach (double g in grades)
        {
            AddGrade(subject, g);
        }
    }

    // Tính trung bình 1 môn
    public double GetAverage(string subject)
    {
        if (_grades.TryGetValue(subject, out List<double> grades) && grades.Count > 0)
        {
            double sum = 0;
            grades.ForEach(g => sum += g);
            return sum / grades.Count;
        }
        return 0;
    }

    // Tìm điểm cao nhất của 1 môn
    public double GetHighest(string subject)
    {
        if (_grades.TryGetValue(subject, out List<double> grades) && grades.Count > 0)
        {
            List<double> sorted = new List<double>(grades);
            sorted.Sort();
            return sorted[sorted.Count - 1];
        }
        return 0;
    }

    // Tìm điểm thấp nhất của 1 môn
    public double GetLowest(string subject)
    {
        if (_grades.TryGetValue(subject, out List<double> grades) && grades.Count > 0)
        {
            List<double> sorted = new List<double>(grades);
            sorted.Sort();
            return sorted[0];
        }
        return 0;
    }

    // Hiển thị bảng điểm
    public void ShowGradeBook()
    {
        Console.WriteLine("\n📚 BẢNG ĐIỂM:");
        Console.WriteLine("╔══════════════════╦══════════════════════╦═══════╦═══════╦═══════╗");
        Console.WriteLine("║ Môn học          ║ Các điểm             ║  TB   ║  Max  ║  Min  ║");
        Console.WriteLine("╠══════════════════╬══════════════════════╬═══════╬═══════╬═══════╣");

        double totalAvg = 0;
        int subjectCount = 0;

        foreach (var (subject, grades) in _grades)
        {
            string gradesStr = string.Join(", ", grades);
            double avg = GetAverage(subject);
            double max = GetHighest(subject);
            double min = GetLowest(subject);
            totalAvg += avg;
            subjectCount++;

            Console.WriteLine(
                $"║ {subject,-17}║ {gradesStr,-21}║ {avg,5:F1} ║ {max,5:F1} ║ {min,5:F1} ║");
        }

        Console.WriteLine("╠══════════════════╬══════════════════════╬═══════╬═══════╬═══════╣");
        double gpa = subjectCount > 0 ? totalAvg / subjectCount : 0;
        Console.WriteLine(
            $"║ {"GPA TỔNG",-17}║ {"",- 21}║ {gpa,5:F1} ║       ║       ║");
        Console.WriteLine("╚══════════════════╩══════════════════════╩═══════╩═══════╩═══════╝");
    }

    // Xếp loại
    public void ShowRanking()
    {
        Console.WriteLine("\n🏆 XẾP LOẠI THEO MÔN:");

        // Chuyển thành list để sort
        List<KeyValuePair<string, double>> avgList =
            new List<KeyValuePair<string, double>>();

        foreach (var subject in _grades.Keys)
        {
            avgList.Add(new KeyValuePair<string, double>(subject, GetAverage(subject)));
        }

        // Sắp xếp theo TB giảm dần
        avgList.Sort((a, b) => b.Value.CompareTo(a.Value));

        int rank = 1;
        foreach (var (subject, avg) in avgList)
        {
            string medal = rank switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => "  "
            };

            string grade = avg switch
            {
                >= 9.0 => "Xuất sắc",
                >= 8.0 => "Giỏi",
                >= 6.5 => "Khá",
                >= 5.0 => "Trung bình",
                _ => "Yếu"
            };

            Console.WriteLine($"  {medal} {rank}. {subject,-15} TB: {avg:F1}  ({grade})");
            rank++;
        }
    }

    // Tìm môn cần cải thiện
    public void ShowWeakSubjects()
    {
        Console.WriteLine("\n⚠️ MÔN CẦN CẢI THIỆN (TB < 7.0):");
        bool found = false;

        foreach (var (subject, grades) in _grades)
        {
            double avg = GetAverage(subject);
            if (avg < 7.0)
            {
                Console.WriteLine($"  📛 {subject}: TB = {avg:F1}");
                // Gợi ý cần bao nhiêu điểm để đạt 7.0
                double needed = 7.0 * (grades.Count + 1) - (avg * grades.Count);
                if (needed <= 10)
                    Console.WriteLine($"     → Cần đạt {needed:F1} trong bài tiếp để TB = 7.0");
                else
                    Console.WriteLine($"     → Cần nhiều hơn 1 bài để cải thiện");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("  ✅ Tất cả các môn đều >= 7.0!");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine("║    📚 STUDENT GRADE BOOK          ║");
        Console.WriteLine("╚═══════════════════════════════════╝\n");

        GradeBook book = new GradeBook();

        // Nhập điểm
        Console.WriteLine("--- NHẬP ĐIỂM ---");
        book.AddGrades("Toán",      8.0, 7.5, 9.0, 8.5);
        Console.WriteLine();
        book.AddGrades("Lý",        6.5, 7.0, 6.0, 7.5);
        Console.WriteLine();
        book.AddGrades("Hóa",       9.0, 8.5, 9.5, 9.0);
        Console.WriteLine();
        book.AddGrades("Văn",       5.5, 6.0, 5.0, 6.5);
        Console.WriteLine();
        book.AddGrades("Anh văn",   8.0, 8.5, 7.5, 9.0);

        // Hiển thị
        book.ShowGradeBook();
        book.ShowRanking();
        book.ShowWeakSubjects();
    }
}
```

**Kết quả mong đợi:**
```
📚 BẢNG ĐIỂM:
╔══════════════════╦══════════════════════╦═══════╦═══════╦═══════╗
║ Môn học          ║ Các điểm             ║  TB   ║  Max  ║  Min  ║
╠══════════════════╬══════════════════════╬═══════╬═══════╬═══════╣
║ Toán             ║ 8, 7.5, 9, 8.5      ║   8.3 ║   9.0 ║   7.5 ║
║ Lý               ║ 6.5, 7, 6, 7.5      ║   6.8 ║   7.5 ║   6.0 ║
║ Hóa              ║ 9, 8.5, 9.5, 9      ║   9.0 ║   9.5 ║   8.5 ║
║ Văn              ║ 5.5, 6, 5, 6.5      ║   5.8 ║   6.5 ║   5.0 ║
║ Anh văn          ║ 8, 8.5, 7.5, 9      ║   8.3 ║   9.0 ║   7.5 ║
╠══════════════════╬══════════════════════╬═══════╬═══════╬═══════╣
║ GPA TỔNG         ║                      ║   7.6 ║       ║       ║
╚══════════════════╩══════════════════════╩═══════╩═══════╩═══════╝

🏆 XẾP LOẠI THEO MÔN:
  🥇 1. Hóa             TB: 9.0  (Xuất sắc)
  🥈 2. Toán            TB: 8.3  (Giỏi)
  🥉 3. Anh văn         TB: 8.3  (Giỏi)
    4. Lý              TB: 6.8  (Khá)
    5. Văn             TB: 5.8  (Trung bình)

⚠️ MÔN CẦN CẢI THIỆN (TB < 7.0):
  📛 Lý: TB = 6.8
     → Cần đạt 7.8 trong bài tiếp để TB = 7.0
  📛 Văn: TB = 5.8
     → Cần đạt 11.8 trong bài tiếp để TB = 7.0
     → Cần nhiều hơn 1 bài để cải thiện
```

---

## Ví dụ 4: Inventory System — Tra Cứu Sản Phẩm Theo ID

> 🎯 Dictionary trong ứng dụng thực tế: tra cứu nhanh O(1), quản lý tồn kho

```csharp
using System;
using System.Collections.Generic;

class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }

    public Product(string id, string name, double price, int stock, string category)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
        Category = category;
    }

    public override string ToString()
    {
        string stockLabel = Stock > 0 ? $"Còn {Stock}" : "❌ HẾT";
        return $"  [{Id}] {Name,-20} {Price,12:N0}đ  Kho: {stockLabel,-10} ({Category})";
    }
}

class InventorySystem
{
    // Key: Product ID, Value: Product object
    private Dictionary<string, Product> _inventory =
        new Dictionary<string, Product>(StringComparer.OrdinalIgnoreCase);

    // Nhóm theo category: Key: Category, Value: List<Product ID>
    private Dictionary<string, List<string>> _categories =
        new Dictionary<string, List<string>>();

    // ➕ Thêm sản phẩm
    public void AddProduct(Product product)
    {
        if (_inventory.ContainsKey(product.Id))
        {
            Console.WriteLine($"⚠️ Sản phẩm {product.Id} đã tồn tại!");
            return;
        }

        _inventory[product.Id] = product;

        // Thêm vào nhóm category
        if (!_categories.ContainsKey(product.Category))
            _categories[product.Category] = new List<string>();
        _categories[product.Category].Add(product.Id);

        Console.WriteLine($"✅ Đã thêm: {product.Name} (ID: {product.Id})");
    }

    // 🔍 Tra cứu theo ID — O(1)!
    public Product GetById(string id)
    {
        if (_inventory.TryGetValue(id, out Product product))
            return product;
        return null;
    }

    // 📦 Nhập hàng
    public void Restock(string id, int quantity)
    {
        if (_inventory.TryGetValue(id, out Product product))
        {
            product.Stock += quantity;
            Console.WriteLine($"📦 Nhập thêm {quantity} × {product.Name} → Kho: {product.Stock}");
        }
        else
        {
            Console.WriteLine($"❌ Không tìm thấy sản phẩm ID: {id}");
        }
    }

    // 🛒 Bán hàng (giảm kho)
    public bool Sell(string id, int quantity)
    {
        if (_inventory.TryGetValue(id, out Product product))
        {
            if (product.Stock >= quantity)
            {
                product.Stock -= quantity;
                double total = product.Price * quantity;
                Console.WriteLine($"🛒 Bán {quantity} × {product.Name} = {total:N0}đ (Kho: {product.Stock})");
                return true;
            }
            else
            {
                Console.WriteLine($"❌ Không đủ hàng! {product.Name} chỉ còn {product.Stock}");
                return false;
            }
        }
        Console.WriteLine($"❌ Sản phẩm ID: {id} không tồn tại");
        return false;
    }

    // 📋 Hiển thị tất cả
    public void ShowAll()
    {
        Console.WriteLine($"\n📋 KHO HÀNG ({_inventory.Count} sản phẩm):");
        Console.WriteLine(new string('═', 70));

        foreach (var product in _inventory.Values)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine(new string('═', 70));
    }

    // 📋 Hiển thị theo nhóm
    public void ShowByCategory()
    {
        Console.WriteLine("\n📋 SẢN PHẨM THEO DANH MỤC:");

        foreach (var (category, productIds) in _categories)
        {
            Console.WriteLine($"\n  📁 {category} ({productIds.Count} sản phẩm):");
            foreach (string id in productIds)
            {
                if (_inventory.TryGetValue(id, out Product p))
                {
                    Console.WriteLine($"    {p}");
                }
            }
        }
    }

    // ⚠️ Sản phẩm sắp hết hàng
    public void ShowLowStock(int threshold = 5)
    {
        Console.WriteLine($"\n⚠️ SẢN PHẨM SẮP HẾT (Kho <= {threshold}):");
        bool found = false;

        foreach (var product in _inventory.Values)
        {
            if (product.Stock <= threshold)
            {
                Console.WriteLine($"  🔴 {product}");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("  ✅ Tất cả sản phẩm đều đủ hàng!");
    }

    // 📊 Báo cáo giá trị kho
    public void ShowInventoryValue()
    {
        Console.WriteLine("\n📊 GIÁ TRỊ KHO HÀNG:");
        double totalValue = 0;
        int totalItems = 0;

        Console.WriteLine("╔══════════════════════╦═══════╦═══════════════╗");
        Console.WriteLine("║ Sản phẩm             ║  Kho  ║  Giá trị      ║");
        Console.WriteLine("╠══════════════════════╬═══════╬═══════════════╣");

        foreach (var product in _inventory.Values)
        {
            double value = product.Price * product.Stock;
            totalValue += value;
            totalItems += product.Stock;
            Console.WriteLine(
                $"║ {product.Name,-21}║ {product.Stock,5} ║ {value,12:N0}đ ║");
        }

        Console.WriteLine("╠══════════════════════╬═══════╬═══════════════╣");
        Console.WriteLine(
            $"║ {"TỔNG",-21}║ {totalItems,5} ║ {totalValue,12:N0}đ ║");
        Console.WriteLine("╚══════════════════════╩═══════╩═══════════════╝");
    }

    // 🔍 Tìm kiếm theo tên
    public void SearchByName(string keyword)
    {
        Console.WriteLine($"\n🔍 Tìm \"{keyword}\":");
        bool found = false;

        foreach (var product in _inventory.Values)
        {
            if (product.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(product);
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("  Không tìm thấy sản phẩm nào.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("╔═══════════════════════════════════════╗");
        Console.WriteLine("║    📦 INVENTORY MANAGEMENT SYSTEM     ║");
        Console.WriteLine("╚═══════════════════════════════════════╝\n");

        InventorySystem inv = new InventorySystem();

        // ========== THÊM SẢN PHẨM ==========
        Console.WriteLine("--- THÊM SẢN PHẨM ---");
        inv.AddProduct(new Product("PH001", "iPhone 15 Pro",     30_000_000, 15, "Điện thoại"));
        inv.AddProduct(new Product("PH002", "Samsung Galaxy S24", 22_000_000, 20, "Điện thoại"));
        inv.AddProduct(new Product("LT001", "MacBook Air M3",    32_000_000,  3, "Laptop"));
        inv.AddProduct(new Product("LT002", "Dell XPS 15",       28_000_000,  8, "Laptop"));
        inv.AddProduct(new Product("AC001", "AirPods Pro 2",      6_500_000, 25, "Phụ kiện"));
        inv.AddProduct(new Product("AC002", "Magic Mouse",        2_500_000,  2, "Phụ kiện"));
        inv.AddProduct(new Product("TB001", "iPad Air",          18_000_000, 10, "Tablet"));

        // ========== HIỂN THỊ ==========
        inv.ShowAll();
        inv.ShowByCategory();

        // ========== TRA CỨU ==========
        Console.WriteLine("\n--- TRA CỨU NHANH (O(1)) ---");
        Product p = inv.GetById("LT001");
        if (p != null)
            Console.WriteLine($"Tìm thấy: {p.Name} — {p.Price:N0}đ");

        inv.SearchByName("Air");

        // ========== BÁN HÀNG ==========
        Console.WriteLine("\n--- BÁN HÀNG ---");
        inv.Sell("PH001", 2);
        inv.Sell("AC002", 1);
        inv.Sell("AC002", 5);  // Không đủ hàng

        // ========== NHẬP HÀNG ==========
        Console.WriteLine("\n--- NHẬP HÀNG ---");
        inv.Restock("AC002", 20);
        inv.Restock("LT001", 5);

        // ========== BÁO CÁO ==========
        inv.ShowLowStock(5);
        inv.ShowInventoryValue();
    }
}
```

**Kết quả mong đợi:**
```
╔═══════════════════════════════════════╗
║    📦 INVENTORY MANAGEMENT SYSTEM     ║
╚═══════════════════════════════════════╝

--- THÊM SẢN PHẨM ---
✅ Đã thêm: iPhone 15 Pro (ID: PH001)
✅ Đã thêm: Samsung Galaxy S24 (ID: PH002)
...

📋 SẢN PHẨM THEO DANH MỤC:

  📁 Điện thoại (2 sản phẩm):
    [PH001] iPhone 15 Pro        30,000,000đ  Kho: Còn 15   (Điện thoại)
    [PH002] Samsung Galaxy S24   22,000,000đ  Kho: Còn 20   (Điện thoại)

  📁 Laptop (2 sản phẩm):
    ...

--- BÁN HÀNG ---
🛒 Bán 2 × iPhone 15 Pro = 60,000,000đ (Kho: 13)
🛒 Bán 1 × Magic Mouse = 2,500,000đ (Kho: 1)
❌ Không đủ hàng! Magic Mouse chỉ còn 1

📊 GIÁ TRỊ KHO HÀNG:
╔══════════════════════╦═══════╦═══════════════╗
║ Sản phẩm             ║  Kho  ║  Giá trị      ║
╠══════════════════════╬═══════╬═══════════════╣
║ iPhone 15 Pro        ║    13 ║  390,000,000đ ║
║ ...                                           ║
╠══════════════════════╬═══════╬═══════════════╣
║ TỔNG                 ║    99 ║ 1,234,500,000đ║
╚══════════════════════╩═══════╩═══════════════╝
```
