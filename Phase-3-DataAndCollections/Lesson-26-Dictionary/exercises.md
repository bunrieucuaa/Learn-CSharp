# Bài 26: Dictionary<TKey, TValue> — Bài Tập Thực Hành

---

## Bài 1: Đếm Tần Suất Ký Tự ⭐

> 🎯 Luyện tập: TryGetValue, đếm tần suất, duyệt Dictionary

### Yêu cầu:

Viết chương trình nhận một chuỗi văn bản và:

1. Đếm số lần xuất hiện của MỖI KÝ TỰ (bỏ qua khoảng trắng, không phân biệt hoa/thường)
2. Hiển thị bảng tần suất có sắp xếp giảm dần theo số lần
3. Tìm ký tự xuất hiện nhiều nhất, ít nhất
4. Đếm số nguyên âm và phụ âm
5. Hiển thị biểu đồ ngang bằng ký tự █

### Gợi ý:

```csharp
string text = "Hello World! Dictionary la cong cu manh me";
Dictionary<char, int> charCount = new Dictionary<char, int>();

foreach (char c in text.ToLower())
{
    if (c == ' ') continue; // Bỏ khoảng trắng

    if (charCount.TryGetValue(c, out int count))
        charCount[c] = count + 1;
    else
        charCount[c] = 1;
}

// Chuyển sang List để sort
List<KeyValuePair<char, int>> sorted =
    new List<KeyValuePair<char, int>>(charCount);
sorted.Sort((a, b) => b.Value.CompareTo(a.Value));
```

### Kết quả mong đợi:
```
📝 Văn bản: "Hello World! Dictionary la cong cu manh me"

📊 TẦN SUẤT KÝ TỰ (giảm dần):
  'a': 4  ████
  'l': 3  ███
  'o': 3  ███
  'c': 3  ███
  'n': 3  ███
  ...

📈 Thống kê:
  Nhiều nhất: 'a' (4 lần)
  Ít nhất: 'w' (1 lần)
  Nguyên âm: 12 | Phụ âm: 20
  Ký tự đặc biệt: 1
```

---

## Bài 2: Quản Lý Cấu Hình (Config Manager) ⭐⭐

> 🎯 Luyện tập: CRUD Dictionary, TryGetValue, ContainsKey

### Yêu cầu:

Viết class `ConfigManager` quản lý cấu hình ứng dụng:

1. Lưu cấu hình dưới dạng `Dictionary<string, string>`
2. Các phương thức:
   - `Set(string key, string value)` — thêm/cập nhật cấu hình
   - `Get(string key)` — lấy giá trị (trả null nếu không có)
   - `GetOrDefault(string key, string defaultValue)` — lấy hoặc trả default
   - `Remove(string key)` — xóa cấu hình
   - `HasKey(string key)` — kiểm tra key tồn tại
   - `GetAllByPrefix(string prefix)` — lấy tất cả config bắt đầu bằng prefix
   - `ShowAll()` — hiển thị tất cả dạng bảng
   - `Export()` — xuất dạng "key=value" (mỗi dòng 1 cặp)
   - `GetInt(string key)` — lấy giá trị dạng int
   - `GetBool(string key)` — lấy giá trị dạng bool
3. Key không phân biệt hoa/thường (dùng `StringComparer.OrdinalIgnoreCase`)

### Gợi ý:

```csharp
class ConfigManager
{
    private Dictionary<string, string> _config;

    public ConfigManager()
    {
        _config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    public string GetOrDefault(string key, string defaultValue)
    {
        return _config.TryGetValue(key, out string value) ? value : defaultValue;
    }

    public int GetInt(string key)
    {
        if (_config.TryGetValue(key, out string value) && int.TryParse(value, out int result))
            return result;
        return 0;
    }

    public Dictionary<string, string> GetAllByPrefix(string prefix)
    {
        var result = new Dictionary<string, string>();
        foreach (var (k, v) in _config)
        {
            if (k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                result[k] = v;
        }
        return result;
    }
}
```

### Kết quả mong đợi:
```
⚙️ CONFIG MANAGER

Set: app.name = "MyApp"
Set: app.version = "1.0.0"
Set: db.host = "localhost"
Set: db.port = "3306"
Set: db.name = "mydb"
Set: debug = "true"

📋 TẤT CẢ CẤU HÌNH:
╔══════════════════╦═══════════════════╗
║ Key              ║ Value             ║
╠══════════════════╬═══════════════════╣
║ app.name         ║ MyApp             ║
║ app.version      ║ 1.0.0             ║
║ db.host          ║ localhost         ║
║ db.port          ║ 3306              ║
║ db.name          ║ mydb              ║
║ debug            ║ true              ║
╚══════════════════╩═══════════════════╝

🔍 Lọc prefix "db.":
  db.host = localhost
  db.port = 3306
  db.name = mydb

📤 Export:
app.name=MyApp
app.version=1.0.0
db.host=localhost
...
```

---

## Bài 3: Hệ Thống Bỏ Phiếu (Voting System) ⭐⭐

> 🎯 Luyện tập: Đếm tần suất, sắp xếp, thống kê

### Yêu cầu:

1. Tạo class `VotingSystem` với:
   - `Dictionary<string, int>` — lưu ứng cử viên và số phiếu
   - `List<string>` — lưu lịch sử bỏ phiếu (ai đã bầu cho ai)
2. Các phương thức:
   - `AddCandidate(string name)` — đăng ký ứng cử viên
   - `Vote(string voter, string candidate)` — bỏ phiếu (mỗi người chỉ được bầu 1 lần)
   - `GetResults()` — hiển thị kết quả (sắp xếp theo phiếu giảm dần)
   - `GetWinner()` — tìm người thắng
   - `GetVotePercentage()` — tính % phiếu mỗi ứng cử viên
   - `ShowChart()` — biểu đồ thanh ngang
   - `IsElectionValid(int minVotes)` — kiểm tra cuộc bầu cử hợp lệ
3. Kiểm tra: không cho bầu trùng, ứng cử viên phải tồn tại

### Gợi ý:

```csharp
class VotingSystem
{
    private Dictionary<string, int> _candidates = new Dictionary<string, int>();
    private Dictionary<string, string> _voterHistory = new Dictionary<string, string>();
    // Key: tên cử tri, Value: đã bầu cho ai

    public bool Vote(string voter, string candidate)
    {
        // Kiểm tra đã bầu chưa
        if (_voterHistory.ContainsKey(voter))
        {
            Console.WriteLine($"❌ {voter} đã bầu rồi!");
            return false;
        }

        // Kiểm tra ứng cử viên tồn tại
        if (!_candidates.ContainsKey(candidate))
        {
            Console.WriteLine($"❌ Ứng cử viên \"{candidate}\" không tồn tại!");
            return false;
        }

        _candidates[candidate]++;
        _voterHistory[voter] = candidate;
        return true;
    }
}
```

### Kết quả mong đợi:
```
🗳️ KẾT QUẢ BẦU CỬ:

╔══════════════════╦═══════╦════════╦══════════════════════╗
║ Ứng cử viên     ║ Phiếu ║ Tỉ lệ  ║ Biểu đồ             ║
╠══════════════════╬═══════╬════════╬══════════════════════╣
║ Nguyễn Văn An   ║   8   ║ 40.0%  ║ ████████             ║
║ Trần Thị Bình   ║   7   ║ 35.0%  ║ ███████              ║
║ Lê Hoàng Chi    ║   5   ║ 25.0%  ║ █████                ║
╚══════════════════╩═══════╩════════╩══════════════════════╝

🏆 Người thắng: Nguyễn Văn An (8 phiếu, 40.0%)
📊 Tổng phiếu: 20/30 cử tri đã bầu
```

---

## Bài 4: Quản Lý Từ Viết Tắt (Acronym Manager) ⭐⭐

> 🎯 Luyện tập: CRUD, tìm kiếm, Dictionary<string, List<string>>

### Yêu cầu:

Viết chương trình quản lý từ viết tắt (acronym) thường gặp trong lập trình:

1. Tạo `Dictionary<string, string>` lưu từ viết tắt → nghĩa đầy đủ
2. Các phương thức:
   - `AddAcronym(string abbr, string fullForm)` — thêm từ viết tắt
   - `Lookup(string abbr)` — tra cứu nghĩa
   - `ReverseLookup(string keyword)` — tìm viết tắt từ nghĩa (chứa keyword)
   - `GetByFirstLetter(char letter)` — lọc theo chữ cái đầu
   - `ShowAll()` — hiển thị theo thứ tự A-Z
   - `Quiz()` — trò chơi đoán nghĩa (hiển thị viết tắt, người chơi đoán)
3. Khởi tạo sẵn 15+ từ viết tắt IT phổ biến:
   - API, CPU, GUI, HTML, HTTP, IDE, JSON, OOP, RAM, REST, SDK, SQL, URL, UI, UX, CSS, DOM, DNS, FTP, SSH

### Gợi ý:

```csharp
class AcronymManager
{
    private Dictionary<string, string> _acronyms =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public AcronymManager()
    {
        // Khởi tạo sẵn
        _acronyms["API"]  = "Application Programming Interface";
        _acronyms["CPU"]  = "Central Processing Unit";
        _acronyms["OOP"]  = "Object-Oriented Programming";
        // ... thêm 15+
    }

    public void ReverseLookup(string keyword)
    {
        Console.WriteLine($"🔍 Tìm viết tắt chứa \"{keyword}\":");
        foreach (var (abbr, full) in _acronyms)
        {
            if (full.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                Console.WriteLine($"  {abbr} = {full}");
        }
    }
}
```

### Kết quả mong đợi:
```
📖 TỪ ĐIỂN VIẾT TẮT IT:
╔═══════╦════════════════════════════════════════════╗
║ Viết  ║ Nghĩa đầy đủ                              ║
║ tắt   ║                                            ║
╠═══════╬════════════════════════════════════════════╣
║ API   ║ Application Programming Interface          ║
║ CPU   ║ Central Processing Unit                    ║
║ CSS   ║ Cascading Style Sheets                     ║
║ ...                                                 ║
╚═══════╩════════════════════════════════════════════╝

🔍 Tra cứu "OOP": Object-Oriented Programming
🔍 Tìm ngược "Programming": API, OOP

🎮 QUIZ: HTML = ???
  Đáp án: HyperText Markup Language ✅
```

---

## Bài 5: Hệ Thống Quản Lý Khách Sạn (Hotel Booking) ⭐⭐⭐

> 🎯 Luyện tập: Dictionary kết hợp List, CRUD phức tạp, business logic

### Yêu cầu:

1. Tạo các class:
   ```
   Room: RoomNumber (string), Type ("Standard"/"Deluxe"/"Suite"),
         PricePerNight (double), IsAvailable (bool)
   Booking: Id, GuestName, RoomNumber, CheckIn, CheckOut, TotalPrice
   ```
2. Tạo class `Hotel` quản lý:
   - `Dictionary<string, Room>` — Key: số phòng, Value: Room
   - `Dictionary<int, Booking>` — Key: booking ID, Value: Booking
   - `Dictionary<string, List<Booking>>` — Key: tên khách, Value: lịch sử đặt phòng
3. Các phương thức:
   - `AddRoom(Room room)` — thêm phòng
   - `GetAvailableRooms()` — phòng trống
   - `GetAvailableByType(string type)` — phòng trống theo loại
   - `BookRoom(string guest, string roomNumber, DateTime checkIn, DateTime checkOut)` — đặt phòng
   - `CancelBooking(int bookingId)` — hủy đặt
   - `CheckIn(int bookingId)` — nhận phòng
   - `CheckOut(int bookingId)` — trả phòng
   - `GetGuestHistory(string guest)` — lịch sử đặt phòng của khách
   - `GetRevenueReport()` — báo cáo doanh thu
   - `GetOccupancyRate()` — tỉ lệ phòng đang sử dụng
   - `ShowAllRooms()` — hiển thị tất cả phòng
   - `ShowAllBookings()` — hiển thị tất cả booking

### Gợi ý:

```csharp
class Hotel
{
    private Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
    private Dictionary<int, Booking> _bookings = new Dictionary<int, Booking>();
    private Dictionary<string, List<Booking>> _guestHistory =
        new Dictionary<string, List<Booking>>(StringComparer.OrdinalIgnoreCase);
    private int _nextBookingId = 1;

    public int BookRoom(string guest, string roomNumber,
                        DateTime checkIn, DateTime checkOut)
    {
        if (!_rooms.TryGetValue(roomNumber, out Room room))
        {
            Console.WriteLine($"❌ Phòng {roomNumber} không tồn tại!");
            return -1;
        }

        if (!room.IsAvailable)
        {
            Console.WriteLine($"❌ Phòng {roomNumber} không trống!");
            return -1;
        }

        int nights = (checkOut - checkIn).Days;
        double total = room.PricePerNight * nights;

        Booking booking = new Booking
        {
            Id = _nextBookingId++,
            GuestName = guest,
            RoomNumber = roomNumber,
            CheckIn = checkIn,
            CheckOut = checkOut,
            TotalPrice = total
        };

        _bookings[booking.Id] = booking;
        room.IsAvailable = false;

        // Thêm vào lịch sử khách
        if (!_guestHistory.ContainsKey(guest))
            _guestHistory[guest] = new List<Booking>();
        _guestHistory[guest].Add(booking);

        Console.WriteLine($"✅ Đặt phòng thành công! Booking #{booking.Id}");
        Console.WriteLine($"   Khách: {guest} | Phòng: {roomNumber}");
        Console.WriteLine($"   {checkIn:dd/MM} → {checkOut:dd/MM} ({nights} đêm) = {total:N0}đ");

        return booking.Id;
    }
}
```

### Kết quả mong đợi:
```
🏨 KHÁCH SẠN PARADISE

📋 TẤT CẢ PHÒNG:
╔═══════╦══════════╦═══════════════╦═══════════╗
║ Phòng ║ Loại     ║ Giá/đêm       ║ Tình trạng║
╠═══════╬══════════╬═══════════════╬═══════════╣
║ 101   ║ Standard ║   500,000đ    ║ 🟢 Trống  ║
║ 102   ║ Standard ║   500,000đ    ║ 🔴 Đã đặt ║
║ 201   ║ Deluxe   ║   800,000đ    ║ 🟢 Trống  ║
║ 301   ║ Suite    ║ 1,500,000đ    ║ 🟢 Trống  ║
╚═══════╩══════════╩═══════════════╩═══════════╝

Tỉ lệ sử dụng: 25% (1/4 phòng)

📊 DOANH THU: 3,500,000đ (3 booking)
```

---

## 🎯 Bảng Tổng Hợp Kỹ Năng

| Bài | Kỹ năng chính | Độ khó |
|-----|---------------|--------|
| 1 | TryGetValue, đếm tần suất, sắp xếp KVP | ⭐ |
| 2 | CRUD, case-insensitive, prefix filter, type conversion | ⭐⭐ |
| 3 | Đếm phiếu, kiểm tra trùng, thống kê % | ⭐⭐ |
| 4 | Reverse lookup, Quiz, SortedDictionary | ⭐⭐ |
| 5 | Nhiều Dictionary, kết hợp List, business logic | ⭐⭐⭐ |
