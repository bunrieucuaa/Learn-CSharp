# ✏️ Bài 23: Interface Nâng Cao — Bài tập

> **Hoàn thành 5 bài tập dưới đây. Mỗi bài tập trung vào 1 kỹ thuật interface nâng cao.**

---

## Bài 1: IComparable — Sắp xếp sản phẩm 🛍️

> **Kỹ năng:** Implement `IComparable<T>` + `Array.Sort()`

### Yêu cầu

Tạo class `Product` implement `IComparable<Product>`:

```
Product:
├── Properties: Id, Name, Price (decimal), Rating (double 1-5)
├── IComparable<Product>: Sắp xếp theo Price TĂNG DẦN
│   Nếu Price bằng → sắp theo Rating GIẢM DẦN
└── ToString(): "[ID] Name — Price đ (★ Rating)"
```

### Chương trình Main:

```csharp
// 1. Tạo mảng 6 sản phẩm (có 2 cặp trùng giá)
// 2. In danh sách TRƯỚC khi sort
// 3. Array.Sort() — sort theo Price tăng, Rating giảm
// 4. In danh sách SAU khi sort
// 5. Tìm sản phẩm rẻ nhất và đắt nhất
```

### Output mong đợi:

```
── SAU KHI SẮP XẾP ──
  #1  [P04] Chuột Logitech — 350,000đ (★ 4.5)
  #2  [P02] Bàn phím Rapoo  — 500,000đ (★ 4.8)
  #3  [P06] Bàn phím Razer  — 500,000đ (★ 4.2)
  ...
```

---

## Bài 2: Explicit Interface — IVehicle + IBoat 🚗⛵

> **Kỹ năng:** Explicit Interface Implementation khi 2 interface trùng method

### Yêu cầu

```
interface IVehicle:
├── void Start()        → "🚗 Khởi động động cơ xe..."
├── void Stop()         → "🚗 Tắt máy xe."
├── int Speed { get; }
└── void Accelerate(int amount)

interface IBoat:
├── void Start()        → "⛵ Hạ thủy, khởi động..."
├── void Stop()         → "⛵ Neo thuyền."
├── int Speed { get; }
└── void Accelerate(int amount)

class Hovercraft : IVehicle, IBoat
├── Explicit implementation cho Start(), Stop(), Speed, Accelerate
├── string Name, bool IsOnWater
├── void SwitchMode(bool water) → chuyển chế độ
└── void ShowStatus()
```

### Chương trình Main:

```csharp
Hovercraft craft = new Hovercraft("HV-001");

// Chế độ đường bộ
IVehicle vehicle = craft;
vehicle.Start();
vehicle.Accelerate(60);

// Chuyển chế độ thủy
craft.SwitchMode(true);
IBoat boat = craft;
boat.Start();
boat.Accelerate(30);
```

---

## Bài 3: ISP — Hệ thống nhân viên 👔

> **Kỹ năng:** Interface Segregation Principle — tách interface phù hợp

### Yêu cầu

Tách `IEmployee` lớn thành các interface nhỏ:

```
interface IIdentifiable     → int Id, string Name
interface IWorkable         → void Work(), int HoursWorked
interface ISalaried         → decimal BaseSalary, decimal CalculatePay()
interface IBenefitable      → string[] GetBenefits()
interface IManageable       → void AssignTask(string task), string[] GetTasks()

Các class:
┌────────────────────┬─────────────────────────────────────┐
│ FullTimeEmployee   │ IIdentifiable + IWorkable +          │
│                    │ ISalaried + IBenefitable              │
├────────────────────┼─────────────────────────────────────┤
│ Contractor         │ IIdentifiable + IWorkable + ISalaried│
│                    │ (KHÔNG có Benefits!)                  │
├────────────────────┼─────────────────────────────────────┤
│ Intern             │ IIdentifiable + IWorkable             │
│                    │ (KHÔNG lương, KHÔNG benefits!)        │
├────────────────────┼─────────────────────────────────────┤
│ Manager            │ IIdentifiable + IWorkable +          │
│                    │ ISalaried + IBenefitable + IManageable│
└────────────────────┴─────────────────────────────────────┘
```

### Chương trình Main:

```csharp
// 1. Tạo 1 FullTime, 1 Contractor, 1 Intern, 1 Manager
// 2. Cho tất cả Work() qua mảng IWorkable[]
// 3. Trả lương chỉ cho ISalaried (dùng is check)
// 4. Hiển thị benefits chỉ cho IBenefitable
// 5. Manager assign tasks
```

---

## Bài 4: IDisposable — TemporaryFile 📄

> **Kỹ năng:** Implement `IDisposable` + dùng `using` đúng cách

### Yêu cầu

```
class TemporaryFile : IDisposable
├── Constructor: tạo file tạm với prefix (vd: "temp_abc123.txt")
├── string FilePath { get; }        → đường dẫn file tạm
├── void Write(string content)      → ghi vào file
├── string Read()                   → đọc toàn bộ nội dung
├── long Size                       → kích thước file (bytes)
├── Dispose()                       → XÓA file tạm khỏi ổ đĩa!
└── Kiểm tra disposed trước mọi operation

class TempFileManager
├── void ProcessData(string[] data) → dùng using với TemporaryFile
│   - Tạo file tạm, ghi data, xử lý, file tự xóa khi xong
└── void MergeFiles(string[] contents, string outputPath)
    - Tạo nhiều file tạm, merge vào 1 file, tất cả tạm tự xóa
```

### Chương trình Main:

```csharp
// 1. Dùng using block — file tự xóa khi ra khỏi block
using (TemporaryFile temp = new TemporaryFile("report"))
{
    temp.Write("Dữ liệu báo cáo...");
    Console.WriteLine(temp.Read());
    Console.WriteLine($"Size: {temp.Size} bytes");
}
// File đã bị XÓA tự động!

// 2. Dùng using declaration
using TemporaryFile temp2 = new TemporaryFile("cache");
temp2.Write("Cache data...");
// File xóa khi hết scope method

// 3. Thử truy cập sau dispose → bắt exception
```

---

## Bài 5: Dependency Injection — Logger System 📋

> **Kỹ năng:** DI pattern — inject interface qua constructor

### Yêu cầu

```
interface ILogger
├── void Info(string message)
├── void Warning(string message)
├── void Error(string message)
└── string LoggerType { get; }

Implementations:
├── ConsoleLogger    → Ghi ra Console với màu (nếu được)
├── FileLogger       → Ghi ra file (implement IDisposable)
└── MemoryLogger     → Ghi vào List<string>, có GetAllLogs()

class UserService
├── Constructor(ILogger logger)     → DI qua constructor!
├── void Register(string username)  → logger.Info("User registered")
├── void Login(string username)     → logger.Info hoặc Warning
├── void Delete(string username)    → logger.Warning + Error handling
└── void ShowActivity()

class Application
├── Constructor(ILogger logger, UserService userService)
├── void Run()  → Chạy các operations, log mọi thứ
└── void Shutdown()
```

### Chương trình Main:

```csharp
// Kịch bản 1: Dùng ConsoleLogger
ILogger consoleLog = new ConsoleLogger();
UserService service1 = new UserService(consoleLog);
service1.Register("alice");
service1.Login("alice");

// Kịch bản 2: Dùng FileLogger + using
using FileLogger fileLog = new FileLogger("app.log");
UserService service2 = new UserService(fileLog);
service2.Register("bob");
service2.Login("bob");

// Kịch bản 3: Dùng MemoryLogger — xem lại toàn bộ logs
MemoryLogger memLog = new MemoryLogger();
UserService service3 = new UserService(memLog);
service3.Register("charlie");
service3.Login("charlie");
service3.Delete("charlie");

// In toàn bộ logs từ memory
foreach (string log in memLog.GetAllLogs())
{
    Console.WriteLine(log);
}

// ✅ Cùng UserService — chỉ đổi Logger!
```

---

## 📊 Bảng tổng hợp bài tập

| Bài | Chủ đề | Interface chính | Độ khó |
|-----|--------|----------------|--------|
| 1 | IComparable — Sort | `IComparable<T>` | ⭐⭐ |
| 2 | Explicit Implementation | `IVehicle`, `IBoat` | ⭐⭐⭐ |
| 3 | ISP — Tách interface | 5 small interfaces | ⭐⭐⭐ |
| 4 | IDisposable + using | `IDisposable` | ⭐⭐⭐ |
| 5 | Dependency Injection | `ILogger` | ⭐⭐⭐⭐ |

---

> **💡 Mẹo:** Bắt đầu từ Bài 1 (đơn giản nhất), sau đó làm Bài 4, rồi Bài 2-3, cuối cùng Bài 5 (tổng hợp nhất). 🚀
