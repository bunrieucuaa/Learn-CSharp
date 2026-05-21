# 🏆 Lesson 20 — Challenge: Vehicle Management System

---

## 📋 Đề bài: Hệ thống Quản lý Phương tiện Giao thông

Xây dựng hệ thống quản lý phương tiện sử dụng **Inheritance** hoàn chỉnh — base class, constructor chain, virtual/override, protected, upcasting/downcasting.

---

## 🏗️ Kiến trúc Hierarchy

```
                    Vehicle (base)
                   ┌──────────────────────────────┐
                   │ Brand, Model, Year, Price     │
                   │ FuelType, FuelLevel, Mileage  │
                   │ virtual Start(), Stop()        │
                   │ virtual Drive(km)              │
                   │ virtual GetFuelEfficiency()    │
                   │ virtual Maintain()             │
                   └──────┬───────────────────────┘
                          │
            ┌─────────────┼──────────────┐
            ▼             ▼              ▼
       ┌─────────┐  ┌──────────┐  ┌──────────┐
       │   Car   │  │Motorcycle│  │  Truck   │
       │─────────│  │──────────│  │──────────│
       │ Doors   │  │ Type     │  │ Payload  │
       │ Seats   │  │ CC       │  │ Axles    │
       │ Trunk   │  │ HasABS   │  │ Trailer  │
       └────┬────┘  └──────────┘  └──────────┘
            │
       ┌────┴────┐
       ▼         ▼
  ┌────────┐ ┌────────┐
  │ElecCar │ │SUV     │
  │────────│ │────────│
  │Battery │ │4WD     │
  │Range   │ │Ground  │
  └────────┘ └────────┘
```

---

## 📦 Class Vehicle (Base)

### Properties:
| Property | Kiểu | Ghi chú |
|----------|------|---------|
| `VehicleId` | `string` | Auto-gen "VH-001", readonly |
| `Brand` | `string` | Validate không rỗng |
| `Model` | `string` | Validate không rỗng |
| `Year` | `int` | Validate 1900 - current year |
| `Price` | `decimal` | Validate > 0 |
| `FuelType` | `string` | "Xăng", "Dầu", "Điện", "Hybrid" |
| `FuelLevel` | `int` | 0-100 (%), protected set |
| `Mileage` | `double` | km đã chạy, protected set |
| `IsRunning` | `bool` | Đang chạy hay không |
| `Condition` | `string` | "Mới" / "Tốt" / "Cũ" / "Cần sửa" (computed theo mileage) |

### Constructor:
```csharp
public Vehicle(string brand, string model, int year, decimal price, string fuelType)
```

### Virtual methods:
- `Start()` → kiểm tra fuel > 0, in "{Model} khởi động..."
- `Stop()` → in "{Model} tắt máy"
- `Drive(double km)` → tăng mileage, giảm fuel, kiểm tra fuel đủ không
- `Refuel(int percent)` → tăng fuel, max 100
- `GetFuelEfficiency()` → return `double` (km/% fuel) — mỗi loại xe khác nhau
- `Maintain()` → bảo dưỡng, in thông tin
- `GetSpecs()` → return `string` thông tin chi tiết
- `ToString()` → format đẹp

---

## 📦 Class Car (kế thừa Vehicle)

### Thêm properties:
- `int Doors` — 2 hoặc 4
- `int Seats` — 2-8
- `double TrunkCapacity` — dung tích cốp (lít)

### Constructor:
```csharp
public Car(string brand, string model, int year, decimal price,
           int doors, int seats, double trunkCapacity)
    : base(brand, model, year, price, "Xăng")
```

### Override:
- `GetFuelEfficiency()` → 15 km/% fuel
- `Drive(km)` → base + kiểm tra số ghế
- `GetSpecs()` → base + thêm Doors, Seats, Trunk

### Thêm method:
- `void OpenTrunk()` — chỉ khi xe dừng

---

## 📦 Class Motorcycle (kế thừa Vehicle)

### Thêm properties:
- `string MotorcycleType` — "Sport", "Cruiser", "Scooter", "Adventure"
- `int EngineCC` — phân khối (50-2000)
- `bool HasABS`

### Constructor gọi `base(...)`, FuelType = "Xăng"

### Override:
- `GetFuelEfficiency()` → 25 km/% fuel (tiết kiệm hơn car)
- `Drive(km)` → base + in kiểu xe
- `Maintain()` → base + kiểm tra ABS

---

## 📦 Class Truck (kế thừa Vehicle)

### Thêm properties:
- `double MaxPayload` — tải trọng tối đa (tấn)
- `double CurrentPayload` — tải hiện tại
- `int Axles` — số trục (2-6)
- `bool HasTrailer`

### Override:
- `GetFuelEfficiency()` → 8 km/% fuel (tốn nhiên liệu hơn)
  - Nếu CurrentPayload > MaxPayload * 0.8 → efficiency giảm 30%
- `Drive(km)` → base + kiểm tra tải trọng
- `Start()` → base + kiểm tra tải trọng hợp lệ

### Thêm methods:
- `bool LoadCargo(double tons)` — validate không quá tải
- `void UnloadCargo()` — reset payload = 0

---

## 📦 Class ElectricCar (kế thừa Car)

### Thêm properties:
- `double BatteryCapacity` — kWh
- `int BatteryPercent` — 0-100
- `double Range` — computed: BatteryPercent × efficiency

### Constructor:
```csharp
public ElectricCar(string brand, string model, int year, decimal price,
                   int seats, double batteryCapacity)
    : base(brand, model, year, price, 4, seats, trunkCapacity)
```
- FuelType = "Điện"

### Override:
- `GetFuelEfficiency()` → 20 km/% battery
- `Refuel()` → "Sạc pin" thay vì "Đổ xăng"
- `Drive(km)` → dùng battery thay fuel
- `GetSpecs()` → thêm Battery info

---

## 📦 Class SUV (kế thừa Car)

### Thêm properties:
- `bool Has4WD` — dẫn động 4 bánh
- `double GroundClearance` — khoảng sáng gầm (mm)

### Override:
- `GetFuelEfficiency()` → 12 km/% fuel (tốn hơn Car thường)
- `Drive(km)` → nếu 4WD → có thể chạy offroad
- `GetSpecs()` → thêm 4WD + Ground clearance

### Thêm:
- `void ToggleMode4WD()` — bật/tắt 4WD

---

## 📦 Class Fleet (quản lý đội xe)

### Private:
- `List<Vehicle> _vehicles`
- `string _fleetName`

### Public readonly:
- `IReadOnlyList<Vehicle> Vehicles`
- `int VehicleCount`
- `decimal TotalValue` — tổng giá trị
- `double TotalMileage` — tổng km đã chạy

### Methods:
| Method | Mô tả |
|--------|-------|
| `AddVehicle(Vehicle v)` | Thêm xe vào đội |
| `RemoveVehicle(string id)` | Xóa xe |
| `FindById(string id)` | Tìm xe |
| `GetVehiclesByType<T>()` | Lọc theo loại (dùng `is` pattern) |
| `GetVehiclesNeedingFuel(int threshold)` | Xe cần đổ xăng (fuel < threshold%) |
| `GetVehiclesNeedingMaintenance()` | Xe cần bảo dưỡng (Condition == "Cần sửa") |
| `PrintFleetReport()` | In báo cáo toàn đội |
| `DriveAll(double km)` | Cho tất cả xe chạy |
| `RefuelAll()` | Đổ đầy xăng tất cả |

---

## 💻 Main Program demo:

```csharp
class Program
{
    static void Main()
    {
        Console.WriteLine("═══ VEHICLE MANAGEMENT SYSTEM ═══\n");

        var fleet = new Fleet("Đội xe Công ty ABC");

        // Thêm xe đa dạng
        fleet.AddVehicle(new Car("Toyota", "Camry", 2024, 1_200_000_000m, 4, 5, 480));
        fleet.AddVehicle(new Car("Honda", "Civic", 2023, 850_000_000m, 4, 5, 420));
        fleet.AddVehicle(new SUV("Ford", "Everest", 2024, 1_500_000_000m, 7, true, 200));
        fleet.AddVehicle(new ElectricCar("Tesla", "Model 3", 2024, 1_800_000_000m, 5, 75));
        fleet.AddVehicle(new Motorcycle("Honda", "Winner X", 2024, 50_000_000m, "Sport", 150, true));
        fleet.AddVehicle(new Truck("Hino", "500", 2022, 900_000_000m, 8, 4, true));

        // In danh sách
        fleet.PrintFleetReport();

        // Lái xe
        Console.WriteLine("\n═══ LÁI XE ═══");
        var camry = fleet.FindById("VH-001");
        camry?.Start();
        camry?.Drive(50);
        camry?.Drive(30);
        camry?.Stop();

        // Truck load cargo
        Console.WriteLine("\n═══ TRUCK ═══");
        var trucks = fleet.GetVehiclesByType<Truck>();
        foreach (var t in trucks)
        {
            t.LoadCargo(5);
            t.Start();
            t.Drive(100);
            t.Stop();
            t.UnloadCargo();
        }

        // Lọc theo loại
        Console.WriteLine("\n═══ LỌC THEO LOẠI ═══");
        var cars = fleet.GetVehiclesByType<Car>();
        Console.WriteLine($"  Cars (bao gồm SUV, ElecCar): {cars.Count}");

        var motorcycles = fleet.GetVehiclesByType<Motorcycle>();
        Console.WriteLine($"  Motorcycles: {motorcycles.Count}");

        // Xe cần đổ xăng
        var needFuel = fleet.GetVehiclesNeedingFuel(30);
        Console.WriteLine($"\n  Xe cần đổ xăng (< 30%): {needFuel.Count}");

        // Upcasting / Downcasting
        Console.WriteLine("\n═══ UPCASTING / DOWNCASTING ═══");
        foreach (var v in fleet.Vehicles)
        {
            Console.Write($"  {v.Model,-12} → ");
            switch (v)
            {
                case ElectricCar ec:
                    Console.WriteLine($"Electric! Battery: {ec.BatteryPercent}%");
                    break;
                case SUV suv:
                    Console.WriteLine($"SUV! 4WD: {suv.Has4WD}");
                    break;
                case Car car:
                    Console.WriteLine($"Car! Doors: {car.Doors}");
                    break;
                case Motorcycle m:
                    Console.WriteLine($"Moto! {m.EngineCC}cc");
                    break;
                case Truck t:
                    Console.WriteLine($"Truck! Max: {t.MaxPayload}T");
                    break;
            }
        }

        // Báo cáo cuối
        fleet.PrintFleetReport();
    }
}
```

---

## 📊 Output mẫu:

```
═══ VEHICLE MANAGEMENT SYSTEM ═══

╔══════════════════════════════════════════════════════════════════════╗
║                   ĐỘI XE CÔNG TY ABC                              ║
╠════════╦══════════════════╦══════════╦═══════════╦══════╦══════════╣
║ ID     ║ Xe               ║ Loại     ║ Fuel      ║  KM  ║ T.Thái  ║
╠════════╬══════════════════╬══════════╬═══════════╬══════╬══════════╣
║ VH-001 ║ Toyota Camry     ║ Car      ║ ██████ 80%║    0 ║ Mới     ║
║ VH-002 ║ Honda Civic      ║ Car      ║ ██████100%║    0 ║ Mới     ║
║ VH-003 ║ Ford Everest     ║ SUV      ║ ██████100%║    0 ║ Mới     ║
║ VH-004 ║ Tesla Model 3    ║ ElecCar  ║ 🔋  100% ║    0 ║ Mới     ║
║ VH-005 ║ Honda Winner X   ║ Moto     ║ ██████100%║    0 ║ Mới     ║
║ VH-006 ║ Hino 500         ║ Truck    ║ ██████100%║    0 ║ Mới     ║
╠════════╩══════════════════╩══════════╩═══════════╩══════╩══════════╣
║  Tổng xe: 6    Tổng giá trị: 6,300,000,000 VNĐ                   ║
╚══════════════════════════════════════════════════════════════════════╝
```

---

## 🎯 Yêu cầu bắt buộc

### Inheritance:
1. ✅ Đúng hierarchy: Vehicle → Car/Motorcycle/Truck, Car → ElectricCar/SUV
2. ✅ Constructor chain: con gọi `base(...)` đúng cách
3. ✅ `virtual` + `override` cho Drive, Start, GetFuelEfficiency, GetSpecs
4. ✅ `protected` cho FuelLevel, Mileage
5. ✅ `sealed` ít nhất 1 class hoặc method
6. ✅ `base.Method()` — gọi method cha trong override
7. ✅ `override ToString()` cho tất cả class
8. ✅ Upcasting: `Vehicle v = new Car(...)` + downcasting với `is` pattern

### Encapsulation (từ Lesson 19):
1. ✅ Private fields, public properties
2. ✅ Validation trong constructor + setter
3. ✅ Collection encapsulation (IReadOnlyList)

---

## 📊 Tiêu chí chấm điểm

| Tiêu chí | Mô tả | Điểm |
|----------|-------|------|
| Hierarchy đúng | 6 class, đúng quan hệ cha-con | ⭐⭐⭐ |
| Constructor chain | base() đúng ở mọi class | ⭐⭐⭐ |
| virtual/override | Các method override đúng | ⭐⭐⭐ |
| protected fields | FuelLevel, Mileage protected | ⭐⭐ |
| base.Method() | Gọi method cha trong override | ⭐⭐ |
| ToString() override | Mọi class có ToString() đẹp | ⭐⭐ |
| Upcasting/Downcasting | Vehicle[] + is pattern | ⭐⭐⭐ |
| sealed class/method | Ít nhất 1 | ⭐ |
| Fleet manager | Quản lý đội xe hoàn chỉnh | ⭐⭐ |
| Encapsulation | Private fields, validation, IReadOnly | ⭐⭐ |

> 💡 **Gợi ý**: Bắt đầu từ `Vehicle` base class → `Car` → test → rồi mới thêm các class khác.
> ElectricCar và SUV làm cuối cùng (kế thừa từ Car, level 3).
