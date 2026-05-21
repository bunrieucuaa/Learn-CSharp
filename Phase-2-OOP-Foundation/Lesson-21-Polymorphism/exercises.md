# ✏️ Bài 21: Polymorphism — Bài tập thực hành

> **Hoàn thành 5 bài tập để nắm vững Tính Đa Hình!**
> Mỗi bài xây dựng trên kiến thức từ bài trước.

---

## Bài 1: Vehicle Polymorphism — Phương tiện giao thông 🚗

### 📋 Yêu cầu

Tạo hệ thống quản lý phương tiện giao thông với đa hình.

**Class hierarchy:**
```
        Vehicle (base)
       /       |       \
    Car     Motorcycle  Bicycle
```

**Base class `Vehicle`:**
- Properties: `Brand` (string), `Speed` (int), `FuelLevel` (int, 0-100)
- Constructor nhận brand và speed
- Virtual methods:
  - `StartEngine()` — in thông báo khởi động
  - `Accelerate()` — tăng tốc, giảm fuel
  - `GetInfo()` — trả về string mô tả

**Class `Car`:**
- Thêm property: `NumberOfDoors` (int)
- Override `StartEngine()`: "Brrrum! Xe hơi [Brand] khởi động"
- Override `Accelerate()`: Speed += 20, FuelLevel -= 10
- Override `GetInfo()`: bao gồm số cửa

**Class `Motorcycle`:**
- Thêm property: `HasSidecar` (bool)
- Override `StartEngine()`: "Vroooom! Xe máy [Brand] nổ máy"
- Override `Accelerate()`: Speed += 30, FuelLevel -= 8
- Override `GetInfo()`: ghi chú có sidecar hay không

**Class `Bicycle`:**
- Thêm property: `GearCount` (int)
- Override `StartEngine()`: "Không có động cơ, đạp thôi!"
- Override `Accelerate()`: Speed += 10, FuelLevel không đổi
- Override `GetInfo()`: ghi số gear

**Trong `Main()`:**
1. Tạo mảng `Vehicle[]` với ít nhất 5 phương tiện
2. Dùng foreach gọi `StartEngine()` và `Accelerate()` cho tất cả
3. In `GetInfo()` của tất cả
4. Dùng `is` đếm số lượng từng loại
5. Dùng pattern matching tìm xe có Speed > 50

### 💡 Gợi ý

```csharp
// Khung code bắt đầu:
class Vehicle
{
    public string Brand { get; set; }
    public int Speed { get; set; }
    public int FuelLevel { get; set; }

    public Vehicle(string brand, int speed)
    {
        // TODO
    }

    public virtual void StartEngine() { /* TODO */ }
    public virtual void Accelerate() { /* TODO */ }
    public virtual string GetInfo() { /* TODO */ return ""; }
}
```

### ✅ Output mong đợi (tham khảo)

```
═══ KHỞI ĐỘNG ═══
Brrrum! Xe hơi Toyota khởi động
Vroooom! Xe máy Honda nổ máy
Không có động cơ, đạp thôi!
...

═══ THỐNG KÊ ═══
🚗 Ô tô: 2
🏍️ Xe máy: 2
🚲 Xe đạp: 1
```

---

## Bài 2: Employee Salary — Tính lương đa hình 💰

### 📋 Yêu cầu

Hệ thống tính lương cho nhiều loại nhân viên.

**Class hierarchy:**
```
        Employee (base)
       /       |        \
  FullTime   PartTime   Freelancer
```

**Base class `Employee`:**
- Properties: `Name`, `EmployeeId`, `BaseSalary` (decimal)
- Virtual `CalculateSalary()` → trả về decimal
- Virtual `GetEmployeeType()` → trả về string
- Concrete `PrintPaySlip()` → in phiếu lương (dùng CalculateSalary())

**Class `FullTimeEmployee`:**
- Thêm: `Bonus` (decimal), `TaxRate` (double, ví dụ 0.1 = 10%)
- `CalculateSalary()` = (BaseSalary + Bonus) * (1 - TaxRate)

**Class `PartTimeEmployee`:**
- Thêm: `HoursWorked` (int), `HourlyRate` (decimal)
- `CalculateSalary()` = HoursWorked * HourlyRate

**Class `Freelancer`:**
- Thêm: `ProjectCount` (int), `RatePerProject` (decimal)
- `CalculateSalary()` = ProjectCount * RatePerProject

**Trong `Main()`:**
1. Tạo mảng `Employee[]` với 6+ nhân viên (mỗi loại 2+)
2. In phiếu lương cho tất cả bằng 1 vòng lặp
3. Tính tổng lương cần trả
4. Tìm nhân viên có lương cao nhất
5. Hiển thị thống kê: trung bình lương theo loại

### 💡 Gợi ý

```csharp
// Tìm nhân viên lương cao nhất:
Employee highest = employees[0];
foreach (Employee emp in employees)
{
    if (emp.CalculateSalary() > highest.CalculateSalary())
        highest = emp;
}
```

---

## Bài 3: Media Player — Trình phát media 🎵

### 📋 Yêu cầu

Xây dựng trình phát media hỗ trợ nhiều loại file.

**Class hierarchy:**
```
        MediaFile (base)
       /       |       \
   AudioFile VideoFile ImageFile
```

**Base class `MediaFile`:**
- Properties: `FileName`, `FileSize` (double, MB), `Duration` (int, giây), `IsPlaying`
- Virtual methods:
  - `Play()` — bắt đầu phát
  - `Stop()` — dừng phát
  - `GetDetails()` — trả về string chi tiết
- Concrete: `FormatDuration()` — chuyển giây → "mm:ss"

**Class `AudioFile`:**
- Thêm: `Artist`, `Album`, `Bitrate` (int, kbps)
- Override `Play()`: "[♫] Đang phát: Artist - FileName"
- Override `GetDetails()`: hiển thị artist, album, bitrate

**Class `VideoFile`:**
- Thêm: `Resolution` (string, "1920x1080"), `FrameRate` (int)
- Override `Play()`: "[▶] Đang phát video: FileName (Resolution)"
- Override `GetDetails()`: hiển thị resolution, framerate

**Class `ImageFile`:**
- Thêm: `Width`, `Height` (int, pixels)
- Duration = 0 (ảnh không có duration)
- Override `Play()`: "[🖼] Đang hiển thị: FileName"
- Override `GetDetails()`: hiển thị kích thước

**Tạo class `Playlist`:**
- Chứa mảng `MediaFile[]`
- Method `PlayAll()` — phát tất cả (polymorphism!)
- Method `ShowPlaylist()` — hiển thị danh sách
- Method `GetTotalDuration()` — tổng thời lượng
- Method `FindByType<T>()` — tìm file theo kiểu (dùng `is`)

**Trong `Main()`:**
1. Tạo playlist với 6+ media files
2. Hiển thị danh sách
3. Phát tất cả
4. In tổng thời lượng
5. Đếm số file theo từng loại

### ✅ Output mong đợi (tham khảo)

```
═══ PLAYLIST ═══
1. [Audio] Chúng ta của hiện tại - Sơn Tùng (04:23)
2. [Video] Avengers.mp4 - 1920x1080 (02:15:00)
3. [Image] sunset.jpg - 3840x2160
...

═══ PHÁT TẤT CẢ ═══
[♫] Đang phát: Sơn Tùng - Chúng ta của hiện tại
[▶] Đang phát video: Avengers.mp4 (1920x1080)
[🖼] Đang hiển thị: sunset.jpg
```

---

## Bài 4: Banking System — Tài khoản ngân hàng 🏦

### 📋 Yêu cầu

Hệ thống tài khoản ngân hàng với nhiều loại tài khoản.

**Class hierarchy:**
```
        BankAccount (base)
       /          |          \
  SavingsAccount CheckingAccount LoanAccount
```

**Base class `BankAccount`:**
- Properties: `AccountNumber`, `OwnerName`, `Balance` (decimal)
- Virtual methods:
  - `Deposit(decimal amount)` — nạp tiền
  - `Withdraw(decimal amount)` → bool — rút tiền (true=thành công)
  - `CalculateInterest()` → decimal — tính lãi
  - `GetAccountInfo()` → string

**Class `SavingsAccount`:**
- Thêm: `InterestRate` (double, ví dụ 0.065 = 6.5%/năm)
- `Withdraw()`: không được rút quá 80% số dư, phí rút 5,000đ
- `CalculateInterest()`: Balance * InterestRate / 12

**Class `CheckingAccount`:**
- Thêm: `OverdraftLimit` (decimal — được rút âm đến giới hạn)
- `Withdraw()`: cho phép rút âm (đến OverdraftLimit), phí overdraft 50,000đ
- `CalculateInterest()`: 0 (không có lãi)

**Class `LoanAccount`:**
- Balance ở đây là SỐ NỢ (số dương = nợ)
- Thêm: `LoanInterestRate` (double)
- `Deposit()` = trả nợ (giảm Balance)
- `Withdraw()` = vay thêm (tăng Balance, không quá giới hạn)
- `CalculateInterest()`: Balance * LoanInterestRate / 12

**Trong `Main()`:**
1. Tạo mảng `BankAccount[]` với 5+ tài khoản
2. Thực hiện các giao dịch: nạp/rút tiền
3. Tính lãi hàng tháng cho tất cả
4. In bảng tổng kết tất cả tài khoản
5. Dùng pattern matching xử lý logic riêng cho từng loại

### 💡 Gợi ý

```csharp
// Xử lý đa hình + pattern matching:
foreach (BankAccount account in accounts)
{
    decimal interest = account.CalculateInterest();
    Console.WriteLine($"{account.AccountNumber}: Lãi = {interest:N0}đ");

    if (account is LoanAccount loan)
    {
        Console.WriteLine($"  ⚠️ Nợ còn: {loan.Balance:N0}đ");
    }
}
```

---

## Bài 5: Polymorphism + Overloading tổng hợp — Shipping System 📦

### 📋 Yêu cầu

Hệ thống vận chuyển kết hợp CÙNG LÚC runtime và compile-time polymorphism.

**Class hierarchy (Runtime Polymorphism):**
```
        Package (base)
       /       |        \
  StandardPkg ExpressPkg FragilePkg
```

**Base class `Package`:**
- Properties: `TrackingId`, `Weight` (double, kg), `Destination`, `SenderName`
- Virtual `CalculateShippingCost()` → decimal
- Virtual `EstimateDeliveryDays()` → int
- Virtual `GetPackageLabel()` → string

**Class `StandardPackage`:**
- Cost = Weight * 15,000đ/kg
- Delivery = 5-7 ngày
- Label: "📦 Standard"

**Class `ExpressPackage`:**
- Thêm: `IsNextDay` (bool)
- Cost = Weight * 35,000đ/kg (next day: * 50,000đ)
- Delivery = IsNextDay ? 1 : 2 ngày
- Label: "🚀 Express" hoặc "⚡ Next Day"

**Class `FragilePackage`:**
- Thêm: `InsuranceValue` (decimal)
- Cost = Weight * 25,000đ/kg + InsuranceValue * 0.02
- Delivery = 4-5 ngày
- Label: "🔒 Fragile + Insured"

**Class `ShippingCalculator` (Compile-time Polymorphism):**
```csharp
class ShippingCalculator
{
    // Overload 1: Tính cho 1 package
    public decimal Calculate(Package package) { ... }

    // Overload 2: Tính cho mảng packages
    public decimal Calculate(Package[] packages) { ... }

    // Overload 3: Tính cho package + discount code
    public decimal Calculate(Package package, string discountCode) { ... }

    // Overload 4: Tính nhanh theo weight + distance
    public decimal Calculate(double weight, int distanceKm) { ... }
}
```

**Trong `Main()`:**
1. Tạo mảng `Package[]` với 6+ gói hàng (mỗi loại 2+)
2. Dùng `ShippingCalculator` tính phí (thử tất cả overloads)
3. In bảng chi tiết: tracking, loại, weight, cost, delivery time
4. Tìm gói hàng đắt nhất
5. Tổng phí vận chuyển
6. Dùng pattern matching hiển thị thông tin riêng

### ✅ Output mong đợi (tham khảo)

```
╔══════════════════════════════════════════════════════════╗
║              BẢNG PHÍ VẬN CHUYỂN                        ║
╠══════════╦══════════╦═══════╦════════════╦══════════════╣
║ Tracking ║ Loại     ║ Kg    ║ Phí ship   ║ Giao trong  ║
╠══════════╬══════════╬═══════╬════════════╬══════════════╣
║ PKG001   ║ Standard ║ 2.5   ║ 37,500đ   ║ 5-7 ngày    ║
║ PKG002   ║ Express  ║ 1.0   ║ 35,000đ   ║ 2 ngày      ║
║ PKG003   ║ Fragile  ║ 3.0   ║ 95,000đ   ║ 4-5 ngày    ║
╚══════════╩══════════╩═══════╩════════════╩══════════════╝
Tổng phí: 167,500đ
```

---

## 📊 Bảng đánh giá

| Bài | Độ khó | Concepts | Thời gian ước tính |
|-----|--------|----------|-------------------|
| 1 | ⭐⭐ | Mảng đa hình, is keyword | 25 phút |
| 2 | ⭐⭐ | virtual/override, tính toán | 30 phút |
| 3 | ⭐⭐⭐ | Polymorphism + Collection | 35 phút |
| 4 | ⭐⭐⭐ | Pattern matching, logic phức tạp | 40 phút |
| 5 | ⭐⭐⭐⭐ | Runtime + Compile-time tổng hợp | 45 phút |

> **Tip:** Làm tuần tự từ Bài 1 → Bài 5. Nếu stuck, xem lại phần theory và examples! 💪
