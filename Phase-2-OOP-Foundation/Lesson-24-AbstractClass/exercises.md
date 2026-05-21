# ✏️ Bài 24: Abstract Class Nâng Cao — Bài tập

> **Làm từng bài theo thứ tự. Mỗi bài xây dựng trên kiến thức trước đó.**

---

## Bài 1: Template Method — Document Converter 📄

> **Mục tiêu:** Thực hành Template Method pattern.

### Yêu cầu:

Tạo hệ thống chuyển đổi tài liệu:

```
abstract class DocumentConverter
├── Template method: Convert()
│   → OpenSource()      (abstract)
│   → ReadContent()     (abstract)
│   → TransformContent() (abstract)
│   → SaveOutput()      (abstract)
│   → LogResult()       (concrete — shared)
│
├── WordToPdfConverter
├── ExcelToCsvConverter
└── HtmlToMarkdownConverter
```

**Chi tiết:**
1. `abstract class DocumentConverter`:
   - Properties: `SourceFile`, `OutputFile`, `ConvertedAt`
   - Template method `Convert()` gọi 4 abstract steps + 1 concrete step
   - Concrete helper: `Log(string message)` — in với timestamp
   - Abstract: `GetConverterName()` trả về tên converter

2. 3 subclasses — mỗi cái implement 4 abstract steps khác nhau (simulate bằng Console.WriteLine)

3. Trong `Main()`:
   - Tạo mảng `DocumentConverter[]` chứa 3 converter
   - Gọi `Convert()` cho tất cả
   - In thống kê: tên converter, thời gian convert

**Gợi ý:**
```csharp
abstract class DocumentConverter
{
    public string SourceFile { get; set; }
    public string OutputFile { get; set; }

    public void Convert()
    {
        // Template method — gọi abstract steps
    }

    protected abstract void OpenSource();
    // ... thêm abstract methods
}
```

---

## Bài 2: Abstract Properties — Vehicle Hierarchy 🚗

> **Mục tiêu:** Thực hành abstract properties + sealed override.

### Yêu cầu:

```
abstract class Vehicle
├── abstract string VehicleType { get; }
├── abstract int MaxSpeed { get; }
├── abstract decimal FuelCostPerKm { get; }
├── abstract string FuelType { get; }
├── sealed override string ToString()     ← sealed!
├── concrete: CalculateTripCost(int km)
│
├── Car (VehicleType="Ô tô", MaxSpeed=200, FuelType="Xăng")
├── Motorcycle (VehicleType="Xe máy", MaxSpeed=150, FuelType="Xăng")
├── ElectricCar (VehicleType="Xe điện", MaxSpeed=180, FuelType="Điện")
└── Bicycle (VehicleType="Xe đạp", MaxSpeed=40, FuelType="Không")
```

**Chi tiết:**
1. `abstract class Vehicle`:
   - Concrete: `Brand`, `Model`, `Year`
   - 4 abstract properties (xem trên)
   - Concrete method: `CalculateTripCost(int km)` = km * FuelCostPerKm
   - `sealed override ToString()` — format: `"[VehicleType] Brand Model (Year)"`
   - Concrete: `ShowSpecs()` — in tất cả thông tin

2. 4 subclasses — mỗi loại có abstract properties khác nhau

3. Trong `Main()`:
   - Tạo mảng `Vehicle[]` chứa 4+ xe
   - In specs tất cả
   - Tính chi phí chuyến đi 100km cho mỗi xe
   - Tìm xe rẻ nhất, xe nhanh nhất

---

## Bài 3: Abstract Class + Interface — Notification System 📬

> **Mục tiêu:** Kết hợp abstract class + multiple interfaces.

### Yêu cầu:

```
Interfaces:
├── ISendable { void Send(); bool IsSent { get; } }
├── IRetryable { int MaxRetries { get; } void Retry(); }
└── ILoggable { void LogHistory(); }

abstract class Notification : ISendable, ILoggable
├── abstract string GetRecipient()
├── abstract string FormatMessage()
├── concrete: Send()    ← template method
├── concrete: LogHistory()
│
├── EmailNotification : Notification, IRetryable
├── SmsNotification : Notification
└── PushNotification : Notification, IRetryable
```

**Chi tiết:**
1. `abstract class Notification`:
   - Properties: `Title`, `Message`, `CreatedAt`, `IsSent`
   - Abstract: `GetRecipient()`, `FormatMessage()`
   - Concrete `Send()`: gọi FormatMessage(), đánh dấu IsSent = true
   - Concrete `LogHistory()`: in lịch sử gửi

2. `EmailNotification`: có `EmailAddress`, `CC`, override FormatMessage() thêm email header
3. `SmsNotification`: có `PhoneNumber`, giới hạn 160 ký tự
4. `PushNotification`: có `DeviceToken`, `AppName`

5. Trong `Main()`:
   - Tạo mảng `Notification[]`
   - Gửi tất cả
   - Retry những cái implement `IRetryable`
   - In lịch sử

---

## Bài 4: Multi-level Abstraction — Payment System 💳

> **Mục tiêu:** abstract → abstract → concrete + protected abstract.

### Yêu cầu:

```
abstract class PaymentProcessor          Level 0
├── Template: ProcessPayment()
├── protected abstract: ValidatePayment()
├── protected abstract: ExecutePayment()
├── concrete: LogTransaction()
│
├── abstract class OnlinePayment         Level 1
│   ├── sealed override ValidatePayment()  ← sealed!
│   ├── protected abstract: ConnectGateway()
│   │
│   ├── CreditCardPayment               Level 2
│   └── EWalletPayment                  Level 2
│
└── abstract class OfflinePayment        Level 1
    ├── sealed override ValidatePayment()  ← sealed!
    │
    ├── CashPayment                      Level 2
    └── BankTransferPayment              Level 2
```

**Chi tiết:**
1. `PaymentProcessor`: Template method `ProcessPayment(decimal amount)`
2. `OnlinePayment`: sealed validate (kiểm tra internet + gateway), abstract `ConnectGateway()`
3. `OfflinePayment`: sealed validate (kiểm tra số tiền mặt)
4. 4 concrete classes implement phần còn lại

5. Trong `Main()`:
   - Tạo mảng `PaymentProcessor[]`
   - ProcessPayment cho các amount khác nhau
   - In thống kê tổng tiền đã xử lý

---

## Bài 5: Complete Architecture — Library System 📚

> **Mục tiêu:** Tổng hợp tất cả patterns đã học.

### Yêu cầu:

Thiết kế hệ thống quản lý thư viện:

```
Interfaces:
├── IBorrowable { bool Borrow(string memberId); void Return(); }
├── ISearchable { bool MatchesQuery(string query); }
├── IReservable { bool Reserve(string memberId); }

abstract class LibraryItem : ISearchable
├── abstract string ItemType { get; }
├── abstract string GetDetails()
├── concrete: Display()
│
├── Book : LibraryItem, IBorrowable, IReservable
├── Magazine : LibraryItem, IBorrowable
├── DVD : LibraryItem, IBorrowable, IReservable
└── ReferenceBook : LibraryItem    ← KHÔNG IBorrowable (không mượn được!)

class Library
├── AddItem(LibraryItem item)
├── SearchItems(string query) → dùng ISearchable
├── BorrowItem(int id, string memberId) → kiểm tra IBorrowable
├── ShowAll()
```

**Chi tiết:**
1. Mỗi `LibraryItem` có: `Id`, `Title`, `Year`, `IsAvailable`
2. `Book`: Author, ISBN, Genre
3. `Magazine`: Issue, Publisher
4. `DVD`: Director, Duration
5. `ReferenceBook`: Section, Edition — chỉ đọc tại chỗ

6. Trong `Main()`:
   - Tạo Library với 8+ items
   - Search bằng keyword
   - Borrow/Return items
   - Thử borrow ReferenceBook → fail (không IBorrowable)
   - Reserve items
   - In catalog

---

## 📊 Bảng tổng kết bài tập

| Bài | Chủ đề | Pattern | Độ khó |
|-----|--------|---------|--------|
| 1 | Template Method | Skeleton + abstract steps | ⭐⭐ |
| 2 | Abstract Properties + Sealed | Properties + sealed override | ⭐⭐ |
| 3 | Abstract + Interface | Multiple interfaces + abstract class | ⭐⭐⭐ |
| 4 | Multi-level + Protected | abstract → abstract → concrete | ⭐⭐⭐ |
| 5 | Complete Architecture | Tất cả patterns kết hợp | ⭐⭐⭐⭐ |

---

> **💡 Mẹo:** Vẽ class diagram trước khi code! Xác định rõ đâu là abstract class, đâu là interface, đâu là concrete class.

*"Thực hành thiết kế class hierarchy là cách nhanh nhất để thành thạo OOP."* 🏗️
