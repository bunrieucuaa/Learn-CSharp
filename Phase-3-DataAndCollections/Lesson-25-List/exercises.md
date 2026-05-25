# Bài 25: List<T> — Bài Tập Thực Hành

---

## Bài 1: Quản Lý Danh Sách Điểm Số ⭐

> 🎯 Luyện tập: Add, Remove, Sort, Find, Count, ForEach

### Yêu cầu:

Viết chương trình quản lý điểm thi của học sinh:

1. Tạo `List<double>` chứa điểm: `7.5, 8.0, 5.5, 9.0, 6.5, 3.0, 10.0, 4.5, 8.5, 7.0`
2. Hiển thị tất cả điểm
3. Thêm điểm mới: `6.0` và `9.5`
4. Tìm và hiển thị:
   - Điểm cao nhất, điểm thấp nhất
   - Điểm trung bình
   - Số học sinh đạt (>= 5.0) và trượt (< 5.0)
   - Tất cả điểm >= 8.0 (giỏi)
5. Sắp xếp điểm tăng dần, hiển thị
6. Xóa tất cả điểm < 4.0 (hủy kết quả)
7. Hiển thị danh sách sau khi xóa

### Gợi ý:

```csharp
List<double> scores = new List<double> { 7.5, 8.0, 5.5, ... };

// Điểm cao nhất — sort rồi lấy cuối, hoặc dùng vòng lặp
scores.Sort();
double max = scores[scores.Count - 1];

// Điểm trung bình
double sum = 0;
scores.ForEach(s => sum += s);
double avg = sum / scores.Count;

// Lọc điểm giỏi
List<double> excellent = scores.FindAll(s => s >= 8.0);
```

### Kết quả mong đợi:
```
📋 Điểm ban đầu: [7.5, 8.0, 5.5, 9.0, 6.5, 3.0, 10.0, 4.5, 8.5, 7.0]
Sau khi thêm: [7.5, 8.0, 5.5, 9.0, 6.5, 3.0, 10.0, 4.5, 8.5, 7.0, 6.0, 9.5]

📊 Thống kê:
  Điểm cao nhất: 10.0
  Điểm thấp nhất: 3.0
  Điểm trung bình: 7.04
  Đạt: 10 | Trượt: 2
  Điểm giỏi (>= 8.0): [8.0, 9.0, 10.0, 8.5, 9.5]

Sắp xếp tăng dần: [3.0, 4.5, 5.5, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0]
Sau khi xóa < 4.0: [4.5, 5.5, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0]
```

---

## Bài 2: Quản Lý Danh Bạ Liên Hệ ⭐⭐

> 🎯 Luyện tập: List<T> với class, CRUD, tìm kiếm, sắp xếp

### Yêu cầu:

1. Tạo class `Contact` với: `Name`, `Phone`, `Email`, `Group` (Gia đình/Bạn bè/Công việc)
2. Tạo `List<Contact>` với ít nhất 6 liên hệ
3. Viết các phương thức:
   - `AddContact()` — thêm liên hệ mới
   - `RemoveContact(string name)` — xóa theo tên
   - `SearchByName(string keyword)` — tìm theo tên (chứa keyword)
   - `SearchByGroup(string group)` — lọc theo nhóm
   - `SortByName()` — sắp xếp A-Z
   - `ShowAll()` — hiển thị dạng bảng
   - `CountByGroup()` — đếm số liên hệ mỗi nhóm

### Gợi ý:

```csharp
class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Group { get; set; }
    // Constructor, ToString()
}

class ContactManager
{
    private List<Contact> _contacts = new List<Contact>();

    public void SearchByName(string keyword)
    {
        List<Contact> results = _contacts.FindAll(c =>
            c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        // Hiển thị kết quả
    }
}
```

### Kết quả mong đợi:
```
📋 DANH BẠ (6 liên hệ):
╔════╦══════════════════╦══════════════╦═══════════════════════╦══════════╗
║ #  ║ Tên              ║ Điện thoại   ║ Email                 ║ Nhóm     ║
╠════╬══════════════════╬══════════════╬═══════════════════════╬══════════╣
║ 1  ║ Nguyễn Văn An    ║ 0901234567   ║ an@email.com          ║ Gia đình ║
║ 2  ║ Trần Thị Bình    ║ 0912345678   ║ binh@email.com        ║ Bạn bè   ║
║ ...                                                                      ║
╚════╩══════════════════╩══════════════╩═══════════════════════╩══════════╝

🔍 Tìm "Nguyễn": 2 kết quả
📊 Thống kê: Gia đình: 2 | Bạn bè: 2 | Công việc: 2
```

---

## Bài 3: Hệ Thống Xếp Hạng (Leaderboard) ⭐⭐

> 🎯 Luyện tập: Sort với Comparison, Insert, FindIndex, ranking

### Yêu cầu:

1. Tạo class `Player` với: `Name`, `Score`, `Level`
2. Tạo class `Leaderboard` quản lý `List<Player>`:
   - `AddPlayer(Player p)` — thêm và tự động sắp xếp theo điểm giảm dần
   - `GetTopN(int n)` — lấy top N người chơi
   - `GetRank(string name)` — lấy thứ hạng (1-based)
   - `UpdateScore(string name, int newScore)` — cập nhật điểm và sắp xếp lại
   - `RemovePlayer(string name)` — xóa người chơi
   - `ShowLeaderboard()` — hiển thị bảng xếp hạng
3. Demo thêm 8 người chơi, hiển thị top 5, cập nhật điểm, xóa

### Gợi ý:

```csharp
class Leaderboard
{
    private List<Player> _players = new List<Player>();

    public void AddPlayer(Player p)
    {
        _players.Add(p);
        _players.Sort((a, b) => b.Score.CompareTo(a.Score));
    }

    public int GetRank(string name)
    {
        int idx = _players.FindIndex(p => p.Name == name);
        return idx >= 0 ? idx + 1 : -1; // 1-based rank
    }
}
```

### Kết quả mong đợi:
```
🏆 BẢNG XẾP HẠNG:
╔══════╦══════════════════╦═════════╦═══════╗
║ Hạng ║ Tên              ║ Điểm    ║ Level ║
╠══════╬══════════════════╬═════════╬═══════╣
║ 🥇 1 ║ ProGamer         ║ 9500    ║ 45    ║
║ 🥈 2 ║ DragonSlayer      ║ 8200    ║ 38    ║
║ 🥉 3 ║ NightHawk         ║ 7800    ║ 35    ║
║   4  ║ SilverFox         ║ 6500    ║ 30    ║
║   5  ║ ThunderBolt       ║ 5900    ║ 28    ║
╚══════╩══════════════════╩═════════╩═══════╝

🔄 Cập nhật điểm SilverFox: 6500 → 9800
🏆 Hạng mới của SilverFox: 1
```

---

## Bài 4: Quản Lý Playlist Nhạc ⭐⭐

> 🎯 Luyện tập: Insert, RemoveAt, GetRange, Reverse, shuffle

### Yêu cầu:

1. Tạo class `Song` với: `Title`, `Artist`, `Duration` (TimeSpan), `Genre`
2. Tạo class `Playlist` quản lý `List<Song>`:
   - `AddSong(Song s)` — thêm vào cuối
   - `InsertAt(int position, Song s)` — chèn tại vị trí
   - `RemoveSong(string title)` — xóa theo tên bài
   - `MoveUp(int index)` — di chuyển bài hát lên 1 vị trí
   - `MoveDown(int index)` — di chuyển bài hát xuống 1 vị trí
   - `Shuffle()` — xáo trộn ngẫu nhiên
   - `GetByGenre(string genre)` — lọc theo thể loại
   - `TotalDuration()` — tổng thời gian playlist
   - `ShowPlaylist()` — hiển thị
3. Demo tạo playlist 8 bài, thực hiện các thao tác

### Gợi ý:

```csharp
class Playlist
{
    private List<Song> _songs = new List<Song>();

    public void MoveUp(int index)
    {
        if (index > 0 && index < _songs.Count)
        {
            Song temp = _songs[index];
            _songs.RemoveAt(index);
            _songs.Insert(index - 1, temp);
        }
    }

    public void Shuffle()
    {
        Random rng = new Random();
        for (int i = _songs.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            Song temp = _songs[i];
            _songs[i] = _songs[j];
            _songs[j] = temp;
        }
    }

    public TimeSpan TotalDuration()
    {
        TimeSpan total = TimeSpan.Zero;
        _songs.ForEach(s => total += s.Duration);
        return total;
    }
}
```

### Kết quả mong đợi:
```
🎵 PLAYLIST: "Nhạc Việt Hay" (8 bài)
══════════════════════════════════════════════════
  1. ♪ Có Chắc Yêu Là Đây    - Sơn Tùng     3:45  [Pop]
  2. ♪ Ánh Nắng Của Anh       - Đức Phúc     4:12  [Ballad]
  3. ♪ Waiting For You        - MONO         3:30  [Pop]
  ...
══════════════════════════════════════════════════
Tổng thời gian: 28:45
Số bài Pop: 4 | Ballad: 2 | Rock: 2
```

---

## Bài 5: Hệ Thống Quản Lý Đơn Hàng ⭐⭐⭐

> 🎯 Luyện tập: List<T> lồng nhau, tính toán, CRUD phức tạp

### Yêu cầu:

1. Tạo các class:
   ```
   OrderItem: ProductName, UnitPrice, Quantity
   Order: Id, CustomerName, Items (List<OrderItem>),
          CreatedDate, Status (Pending/Confirmed/Shipped/Delivered/Cancelled)
   ```
2. Tạo class `OrderManager` quản lý `List<Order>`:
   - `CreateOrder(string customer)` — tạo đơn mới
   - `AddItem(int orderId, OrderItem item)` — thêm sản phẩm vào đơn
   - `RemoveItem(int orderId, string productName)` — xóa sản phẩm
   - `CalculateTotal(int orderId)` — tính tổng đơn hàng
   - `UpdateStatus(int orderId, string status)` — cập nhật trạng thái
   - `GetOrdersByStatus(string status)` — lọc theo trạng thái
   - `GetOrdersByCustomer(string name)` — lọc theo khách hàng
   - `GetRevenueReport()` — báo cáo doanh thu (chỉ tính Delivered)
   - `CancelOrder(int orderId)` — hủy đơn (chỉ khi Pending)
   - `ShowAllOrders()` — hiển thị tất cả đơn
3. Demo tạo 3-4 đơn hàng, thêm sản phẩm, cập nhật trạng thái, xuất báo cáo

### Gợi ý:

```csharp
class Order
{
    private static int _nextId = 1;
    public int Id { get; }
    public string CustomerName { get; set; }
    public List<OrderItem> Items { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; }

    public Order(string customer)
    {
        Id = _nextId++;
        CustomerName = customer;
        Items = new List<OrderItem>();
        Status = "Pending";
        CreatedDate = DateTime.Now;
    }

    public double GetTotal()
    {
        double total = 0;
        Items.ForEach(item => total += item.UnitPrice * item.Quantity);
        return total;
    }
}

class OrderManager
{
    private List<Order> _orders = new List<Order>();

    public void GetRevenueReport()
    {
        List<Order> delivered = _orders.FindAll(o => o.Status == "Delivered");
        double revenue = 0;
        delivered.ForEach(o => revenue += o.GetTotal());
        Console.WriteLine($"Doanh thu: {revenue:N0}đ ({delivered.Count} đơn)");
    }
}
```

### Kết quả mong đợi:
```
📦 ĐƠN HÀNG #1 — Khách: Nguyễn Văn An — Trạng thái: Delivered
  1. iPhone 15        × 1    25,000,000đ
  2. AirPods Pro      × 2    12,000,000đ
  Tổng: 37,000,000đ

📦 ĐƠN HÀNG #2 — Khách: Trần Thị Bình — Trạng thái: Pending
  1. MacBook Pro      × 1    45,000,000đ
  Tổng: 45,000,000đ

📊 BÁO CÁO DOANH THU:
  Đơn đã giao: 2
  Tổng doanh thu: 67,000,000đ
```

---

## 🎯 Bảng Tổng Hợp Kỹ Năng

| Bài | Kỹ năng chính | Độ khó |
|-----|---------------|--------|
| 1 | Add, Sort, Find, FindAll, RemoveAll, ForEach | ⭐ |
| 2 | List + Class, CRUD, tìm kiếm, nhóm | ⭐⭐ |
| 3 | Sort custom, ranking, update + re-sort | ⭐⭐ |
| 4 | Insert, RemoveAt, swap, shuffle, TimeSpan | ⭐⭐ |
| 5 | List lồng nhau, business logic, báo cáo | ⭐⭐⭐ |
