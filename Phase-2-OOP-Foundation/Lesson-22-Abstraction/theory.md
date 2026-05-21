# 📘 Bài 22: Abstraction — Tính Trừu Tượng

> **"Ẩn đi sự phức tạp, chỉ lộ ra những gì cần thiết — đó là nghệ thuật thiết kế phần mềm."**

---

## 📋 Mục lục

1. [Abstraction là gì?](#1-abstraction-là-gì)
2. [Abstract Class](#2-abstract-class)
3. [Abstract Method](#3-abstract-method)
4. [Interface](#4-interface)
5. [Abstract Class vs Interface](#5-abstract-class-vs-interface)
6. [Multiple Interfaces](#6-multiple-interfaces)
7. [Interface Naming Convention](#7-interface-naming-convention)
8. [Default Interface Methods (C# 8+)](#8-default-interface-methods-c-8)
9. [Dependency Inversion Preview](#9-dependency-inversion-preview)
10. [Real-world Analogy: Vô-lăng ô tô](#10-real-world-analogy-vô-lăng-ô-tô)
11. [So sánh C# vs JavaScript](#11-so-sánh-c-vs-javascript)
12. [Sai lầm thường gặp & Best Practices](#12-sai-lầm-thường-gặp--best-practices)

---

## 1. Abstraction là gì?

### 📖 Định nghĩa

**Abstraction** (Tính trừu tượng) = **Ẩn chi tiết triển khai phức tạp**, chỉ hiển thị **giao diện cần thiết** cho người dùng.

> 💡 Người dùng chỉ cần biết **LÀM GÌ** (What), không cần biết **LÀM THẾ NÀO** (How).

### 🎯 Ý tưởng cốt lõi

```
┌─────────────────────────────────────────────────────┐
│              ABSTRACTION                            │
│                                                     │
│   Người dùng nhìn thấy:    │  Bên trong ẩn đi:    │
│   ┌──────────────────┐     │  ┌──────────────────┐ │
│   │ ▶ Play()         │     │  │ DecodeAudio()    │ │
│   │ ⏸ Pause()        │     │  │ BufferStream()   │ │
│   │ ⏹ Stop()         │     │  │ SyncOutput()     │ │
│   │ 🔊 SetVolume()   │     │  │ ManageMemory()   │ │
│   └──────────────────┘     │  │ HandleErrors()   │ │
│   (Interface đơn giản)     │  │ CompressData()   │ │
│                            │  └──────────────────┘ │
│                            │  (Implementation      │
│                            │   phức tạp)           │
└─────────────────────────────────────────────────────┘
```

### 🧩 Hai công cụ chính

```
        Abstraction trong C#
       /                      \
  Abstract Class              Interface
  (class trừu tượng)         (giao diện/hợp đồng)
      │                          │
  Có thể chứa:               Chỉ chứa:
  - Abstract methods          - Method signatures
  - Concrete methods          - Properties
  - Fields, properties        - Default methods (C# 8+)
  - Constructor               - KHÔNG có fields
      │                          │
  Kế thừa ĐƠN                Implement NHIỀU
  (1 class chỉ kế thừa       (1 class implement
   1 abstract class)           nhiều interfaces)
```

---

## 2. Abstract Class

### 📖 Cú pháp

```csharp
abstract class Shape      // Từ khóa abstract trước class
{
    // ✅ Có thể có fields
    protected string color;

    // ✅ Có thể có constructor
    public Shape(string color)
    {
        this.color = color;
    }

    // ✅ Có thể có concrete methods (có body)
    public void PrintColor()
    {
        Console.WriteLine($"Màu: {color}");
    }

    // ✅ Có thể có abstract methods (KHÔNG có body)
    public abstract double GetArea();
    public abstract void Draw();
}
```

### ⚠️ Quy tắc quan trọng

```
┌─────────────────────────────────────────────────────┐
│ ABSTRACT CLASS — Quy tắc:                           │
│                                                     │
│ 1. ❌ KHÔNG THỂ tạo object trực tiếp               │
│    Shape shape = new Shape();  // Lỗi compile!     │
│                                                     │
│ 2. ✅ CÓ THỂ dùng làm kiểu biến (Polymorphism)    │
│    Shape shape = new Circle(5, "Đỏ");  // OK!     │
│                                                     │
│ 3. ✅ CÓ THỂ chứa cả abstract & concrete methods  │
│                                                     │
│ 4. ✅ CÓ THỂ có constructor, fields, properties    │
│                                                     │
│ 5. Class con PHẢI override TẤT CẢ abstract methods │
│    (hoặc class con cũng phải là abstract)           │
└─────────────────────────────────────────────────────┘
```

### 🔍 Ví dụ

```csharp
abstract class Shape
{
    public string Color { get; set; }

    public Shape(string color)
    {
        Color = color;
    }

    // Abstract — class con PHẢI implement
    public abstract double GetArea();

    // Concrete — class con được dùng luôn
    public void Describe()
    {
        Console.WriteLine($"Hình {Color}, diện tích: {GetArea():F2}");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius, string color) : base(color)
    {
        Radius = radius;
    }

    // BẮT BUỘC override abstract method
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

// Sử dụng:
// Shape s = new Shape("Red");    // ❌ Lỗi! Không new abstract class
Shape s = new Circle(5, "Đỏ");   // ✅ OK! New class con
s.Describe();                      // "Hình Đỏ, diện tích: 78.54"
```

### 🧠 Bộ nhớ

```
    Stack                          Heap
┌──────────────┐            ┌──────────────────┐
│ s            │ ──────────►│ [Circle object]   │
│ (kiểu Shape)│            │ Type: Circle      │
└──────────────┘            │ Color: "Đỏ"      │
                            │ Radius: 5         │
  Compiler: thấy Shape      │ GetArea() → πr²  │
  Runtime:  biết là Circle   │ Describe() → kế  │
                            │   thừa từ Shape   │
                            └──────────────────┘
```

---

## 3. Abstract Method

### 📖 Định nghĩa

```csharp
// Abstract method = method KHÔNG CÓ BODY
// Chỉ khai báo signature, class con PHẢI implement

abstract class Animal
{
    // Abstract — chỉ có signature, kết thúc bằng ;
    public abstract void MakeSound();
    public abstract string GetFood();

    // So sánh với virtual — CÓ body, có thể override hoặc không
    public virtual void Sleep()
    {
        Console.WriteLine("Zzz...");   // Có default behavior
    }
}
```

### 📊 So sánh: abstract vs virtual

```
┌─────────────────┬────────────────────┬────────────────────┐
│                 │ abstract method    │ virtual method     │
├─────────────────┼────────────────────┼────────────────────┤
│ Body            │ ❌ KHÔNG có       │ ✅ CÓ body         │
│ Override        │ BẮT BUỘC          │ TÙY CHỌN          │
│ Ở đâu          │ Chỉ trong         │ Bất kỳ class nào   │
│                 │ abstract class     │                    │
│ Default behavior│ Không có          │ Có (body của cha)  │
│ Từ khóa ở CON  │ override           │ override           │
└─────────────────┴────────────────────┴────────────────────┘
```

### 🔍 Abstract class nhiều tầng

```csharp
abstract class Animal
{
    public abstract void MakeSound();     // Abstract
    public abstract string GetType();     // Abstract
}

// Cách 1: Implement đầy đủ → class bình thường
class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("Gâu!");
    public override string GetType() => "Chó";
}

// Cách 2: Implement 1 phần → vẫn abstract
abstract class Bird : Animal
{
    public override string GetType() => "Chim";
    // MakeSound() vẫn abstract → Bird vẫn phải là abstract!
}

class Parrot : Bird
{
    public override void MakeSound() => Console.WriteLine("Xin chào!");
    // GetType() đã được Bird implement → không cần override
}
```

```
  Animal (abstract)
  ├── MakeSound() [abstract]
  ├── GetType()   [abstract]
  │
  ├── Dog (concrete)
  │   ├── MakeSound() ✅ override
  │   └── GetType()   ✅ override
  │
  └── Bird (abstract) ← vẫn abstract vì chưa implement hết
      ├── GetType()   ✅ override
      ├── MakeSound() [vẫn abstract]
      │
      └── Parrot (concrete)
          └── MakeSound() ✅ override
```

---

## 4. Interface

### 📖 Định nghĩa

> **Interface** = Hợp đồng (contract) — định nghĩa **method signatures** mà class PHẢI implement.

```csharp
interface IPlayable          // Bắt đầu bằng chữ I
{
    void Play();             // Chỉ signature, KHÔNG có body
    void Pause();
    void Stop();
    bool IsPlaying { get; }  // Property cũng được
}
```

### ⚙️ Implement interface

```csharp
class MusicPlayer : IPlayable     // Dùng : giống kế thừa
{
    public bool IsPlaying { get; private set; }

    // PHẢI implement TẤT CẢ methods trong interface
    public void Play()
    {
        IsPlaying = true;
        Console.WriteLine("🎵 Đang phát nhạc...");
    }

    public void Pause()
    {
        IsPlaying = false;
        Console.WriteLine("⏸ Tạm dừng");
    }

    public void Stop()
    {
        IsPlaying = false;
        Console.WriteLine("⏹ Dừng phát");
    }
}
```

### 📊 Interface vs Class thường

```
┌───────────────────┬─────────────────────────────────┐
│ Interface         │ Class                           │
├───────────────────┼─────────────────────────────────┤
│ Chỉ có signature │ Có cả signature + body          │
│ Không có fields  │ Có fields                        │
│ Không constructor│ Có constructor                    │
│ Implement NHIỀU  │ Kế thừa CHỈ 1                   │
│ Dùng chữ I đầu  │ Không có quy ước đặc biệt       │
│ Không có access  │ Có access modifiers              │
│ modifier (mặc    │ (public, private,...)            │
│ định public)     │                                   │
└───────────────────┴─────────────────────────────────┘
```

### ⚠️ Quy tắc interface

```
┌─────────────────────────────────────────────────────┐
│ INTERFACE — Quy tắc:                                │
│                                                     │
│ 1. ❌ KHÔNG THỂ tạo object từ interface            │
│    IPlayable p = new IPlayable();  // Lỗi!        │
│                                                     │
│ 2. ✅ CÓ THỂ dùng làm kiểu biến (Polymorphism)   │
│    IPlayable p = new MusicPlayer();  // OK!        │
│                                                     │
│ 3. Members mặc định là public (không ghi modifier) │
│                                                     │
│ 4. Class implement PHẢI define TẤT CẢ members     │
│                                                     │
│ 5. Interface có thể kế thừa interface khác         │
│    interface IAdvancedPlayable : IPlayable { }     │
└─────────────────────────────────────────────────────┘
```

---

## 5. Abstract Class vs Interface

### 📊 Bảng so sánh chi tiết

| Đặc điểm | Abstract Class | Interface |
|-----------|---------------|-----------|
| Từ khóa | `abstract class` | `interface` |
| Tạo object? | ❌ Không | ❌ Không |
| Concrete methods? | ✅ Có | ✅ Default methods (C# 8+) |
| Abstract methods? | ✅ Có | ✅ Tất cả method mặc định |
| Fields? | ✅ Có | ❌ Không |
| Constructor? | ✅ Có | ❌ Không |
| Access modifiers? | ✅ Có | Mặc định public |
| Đa kế thừa? | ❌ Chỉ 1 | ✅ Nhiều |
| Naming | Không quy ước | Bắt đầu bằng `I` |

### 🧠 Khi nào dùng cái nào?

```
┌─────────────────────────────────────────────────────┐
│           KHI NÀO DÙNG ABSTRACT CLASS?              │
│                                                     │
│ ✅ Khi các class con CHIA SẺ CODE chung             │
│ ✅ Khi cần fields, constructor                      │
│ ✅ Khi có quan hệ IS-A rõ ràng                     │
│ ✅ Khi muốn cung cấp default behavior              │
│                                                     │
│ Ví dụ: Animal → Dog, Cat                           │
│ (Dog IS-A Animal, chia sẻ Name, Age,...)            │
└─────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────┐
│           KHI NÀO DÙNG INTERFACE?                   │
│                                                     │
│ ✅ Khi chỉ cần định nghĩa CONTRACT                 │
│ ✅ Khi class KHÔNG liên quan nhau cần chung behavior│
│ ✅ Khi 1 class cần nhiều "khả năng" (capabilities) │
│ ✅ Khi muốn tách biệt implementation               │
│                                                     │
│ Ví dụ: IDisposable — File, Database, Network       │
│ (không cùng họ, nhưng đều cần Dispose())            │
└─────────────────────────────────────────────────────┘
```

### 🎯 Sơ đồ quyết định

```
Cần abstraction?
    │
    ├── Các class con có CHIA SẺ CODE chung?
    │   │
    │   ├── CÓ → Dùng ABSTRACT CLASS
    │   │        (cung cấp shared implementation)
    │   │
    │   └── KHÔNG → Dùng INTERFACE
    │              (chỉ cần contract)
    │
    └── Class con cần NHIỀU "khả năng"?
        │
        ├── CÓ → Dùng INTERFACE (multiple)
        │
        └── KHÔNG → Cân nhắc cả hai
                    (ưu tiên interface)
```

### 📋 Ví dụ thực tế

```csharp
// Abstract class — chia sẻ code chung
abstract class Vehicle
{
    public string Brand { get; set; }
    public int Speed { get; set; }

    protected Vehicle(string brand) { Brand = brand; }

    public void ShowInfo()  // Concrete — dùng chung
    {
        Console.WriteLine($"{Brand}, Speed: {Speed}");
    }

    public abstract void StartEngine();  // Abstract — mỗi xe khác nhau
}

// Interface — "khả năng" không liên quan đến Vehicle hierarchy
interface IChargeable
{
    int BatteryLevel { get; }
    void Charge();
}

interface IGpsEnabled
{
    string GetLocation();
    void Navigate(string destination);
}

// Kết hợp: Tesla IS-A Vehicle + CÓ KHẢ NĂNG Chargeable + GpsEnabled
class Tesla : Vehicle, IChargeable, IGpsEnabled
{
    public int BatteryLevel { get; private set; }

    public Tesla() : base("Tesla") { BatteryLevel = 100; }

    public override void StartEngine()
    {
        Console.WriteLine("🔋 Khởi động êm ái (điện)");
    }

    public void Charge()
    {
        BatteryLevel = 100;
        Console.WriteLine("⚡ Đang sạc...");
    }

    public string GetLocation() => "21.0285° N, 105.8542° E";
    public void Navigate(string destination)
    {
        Console.WriteLine($"🗺 Đang dẫn đường đến {destination}");
    }
}
```

---

## 6. Multiple Interfaces

### 📖 C# cho phép implement NHIỀU interface

```csharp
interface IReadable
{
    string Read();
}

interface IWritable
{
    void Write(string data);
}

interface IDeletable
{
    void Delete();
}

// 1 class implement NHIỀU interfaces!
class Document : IReadable, IWritable, IDeletable
{
    private string content = "";

    public string Read()
    {
        return content;
    }

    public void Write(string data)
    {
        content = data;
    }

    public void Delete()
    {
        content = "";
    }
}
```

### 🎯 Polymorphism với nhiều interface

```csharp
Document doc = new Document();
doc.Write("Hello World!");

// Dùng bất kỳ interface nào làm kiểu biến
IReadable readable = doc;
Console.WriteLine(readable.Read());  // "Hello World!"

IWritable writable = doc;
writable.Write("Bye!");

IDeletable deletable = doc;
deletable.Delete();

// Kiểm tra interface
if (doc is IReadable r)
{
    Console.WriteLine(r.Read());
}
```

### 📊 Minh họa

```
         IReadable     IWritable     IDeletable
            │              │              │
            │    ┌─────────┼──────────────┘
            │    │         │
            ▼    ▼         ▼
        ┌──────────────────────┐
        │      Document        │
        │                      │
        │ Read()      ← IReadable
        │ Write()     ← IWritable
        │ Delete()    ← IDeletable
        │                      │
        │ + own methods...     │
        └──────────────────────┘

So sánh với class: C# KHÔNG cho đa kế thừa class
    class A : B, C  // ❌ Lỗi nếu B, C là class!
    class A : B, IC // ✅ OK! B là class, IC là interface
```

### 🔄 Interface kế thừa interface

```csharp
interface IEntity
{
    int Id { get; set; }
}

interface IAuditable : IEntity   // Kế thừa IEntity
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}

interface ISoftDeletable : IEntity  // Cũng kế thừa IEntity
{
    bool IsDeleted { get; set; }
    void SoftDelete();
}

// Implement IAuditable → phải implement cả IEntity members
class Product : IAuditable, ISoftDeletable
{
    public int Id { get; set; }                  // Từ IEntity
    public DateTime CreatedAt { get; set; }      // Từ IAuditable
    public DateTime UpdatedAt { get; set; }      // Từ IAuditable
    public bool IsDeleted { get; set; }          // Từ ISoftDeletable

    public void SoftDelete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.Now;
    }
}
```

---

## 7. Interface Naming Convention

### 📖 Quy ước đặt tên

```
Interface LUÔN bắt đầu bằng chữ I (viết hoa)

  I + TínhTừ/DanhTừ mô tả khả năng
  │
  ├── IDisposable    → Có thể dispose (giải phóng tài nguyên)
  ├── IComparable    → Có thể so sánh
  ├── IEnumerable    → Có thể duyệt (enumerate)
  ├── ISerializable  → Có thể serialize
  ├── ICloneable     → Có thể clone (sao chép)
  ├── IPlayable      → Có thể phát
  ├── ISaveable      → Có thể lưu
  └── IRepository    → Là repository (kho dữ liệu)
```

### 📋 Interfaces có sẵn trong .NET

| Interface | Namespace | Mô tả |
|-----------|-----------|--------|
| `IDisposable` | System | Giải phóng tài nguyên |
| `IComparable` | System | So sánh 2 objects |
| `ICloneable` | System | Sao chép object |
| `IEnumerable` | System.Collections | Duyệt collection |
| `IList` | System.Collections | Danh sách |
| `IDictionary` | System.Collections | Dictionary |
| `IEquatable<T>` | System | So sánh bằng nhau |

### 🎯 Tại sao bắt đầu bằng I?

```
// Khi đọc code, lập tức biết đây là interface:
class FileManager : IDisposable, IComparable
//                  ^            ^
//                  Interface!   Interface!

// Nếu không có I, sẽ nhầm với class:
class FileManager : Disposable, Comparable  // Kế thừa class?
```

---

## 8. Default Interface Methods (C# 8+)

### 📖 Tính năng mới: interface có thể có body!

```csharp
interface ILogger
{
    // Method truyền thống — class phải implement
    void Log(string message);

    // Default method — có body sẵn, class KHÔNG BẮT BUỘC implement
    void LogError(string message)
    {
        Log($"[ERROR] {message}");  // Gọi method abstract
    }

    void LogWarning(string message)
    {
        Log($"[WARNING] {message}");
    }

    void LogInfo(string message)
    {
        Log($"[INFO] {message}");
    }
}

class ConsoleLogger : ILogger
{
    // Chỉ CẦN implement Log() — các method khác đã có default!
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}
```

### ⚠️ Lưu ý quan trọng

```csharp
ConsoleLogger logger = new ConsoleLogger();
logger.Log("Test");          // ✅ OK

// logger.LogError("Test");  // ❌ Lỗi! Default method chỉ truy cập qua interface!

ILogger ilogger = logger;
ilogger.LogError("Test");    // ✅ OK — truy cập qua interface type!
```

```
┌─────────────────────────────────────────────────────┐
│ DEFAULT INTERFACE METHODS — Lưu ý:                  │
│                                                     │
│ 1. Chỉ truy cập được qua kiểu interface            │
│    (KHÔNG qua kiểu class cụ thể)                   │
│                                                     │
│ 2. Hữu ích khi thêm method mới vào interface       │
│    mà KHÔNG phá vỡ các class đã implement          │
│                                                     │
│ 3. KHÔNG nên lạm dụng — interface vẫn nên          │
│    chủ yếu là contract                              │
└─────────────────────────────────────────────────────┘
```

---

## 9. Dependency Inversion Preview

### 📖 Nguyên tắc

> **"Depend on abstractions, not on concretions."**
> (Phụ thuộc vào trừu tượng, không phụ thuộc vào cụ thể)

### ❌ Không có Abstraction

```csharp
// OrderService PHỤ THUỘC TRỰC TIẾP vào SqlDatabase
class OrderService
{
    private SqlDatabase db = new SqlDatabase();  // ❌ Hard-coded!

    public void SaveOrder(string order)
    {
        db.Save(order);  // Chỉ hoạt động với SQL!
    }
}

// Muốn đổi sang MongoDB? → PHẢI SỬA OrderService! 😱
```

### ✅ Có Abstraction (Interface)

```csharp
// Định nghĩa contract
interface IDatabase
{
    void Save(string data);
    string Load(int id);
}

// Implementation 1
class SqlDatabase : IDatabase
{
    public void Save(string data) => Console.WriteLine("SQL: Saved!");
    public string Load(int id) => $"SQL data #{id}";
}

// Implementation 2
class MongoDatabase : IDatabase
{
    public void Save(string data) => Console.WriteLine("Mongo: Saved!");
    public string Load(int id) => $"Mongo data #{id}";
}

// OrderService phụ thuộc vào INTERFACE, không phải class cụ thể
class OrderService
{
    private IDatabase db;  // ✅ Phụ thuộc vào abstraction!

    public OrderService(IDatabase database)  // Inject qua constructor
    {
        db = database;
    }

    public void SaveOrder(string order)
    {
        db.Save(order);  // Hoạt động với BẤT KỲ IDatabase nào!
    }
}

// Sử dụng:
OrderService service1 = new OrderService(new SqlDatabase());
OrderService service2 = new OrderService(new MongoDatabase());
// Đổi database → KHÔNG sửa OrderService! 🎉
```

### 📊 Minh họa

```
❌ Coupling chặt:
┌──────────────┐         ┌──────────────┐
│ OrderService │────────►│ SqlDatabase  │
└──────────────┘         └──────────────┘
  Muốn đổi DB → sửa OrderService!

✅ Coupling lỏng (qua Interface):
┌──────────────┐         ┌──────────────┐
│ OrderService │────────►│ IDatabase    │ ← Interface
└──────────────┘         └──────┬───────┘
                                │
                    ┌───────────┼───────────┐
                    ▼           ▼           ▼
              ┌──────────┐ ┌──────────┐ ┌──────────┐
              │  SQL DB  │ │ MongoDB  │ │ InMemory │
              └──────────┘ └──────────┘ └──────────┘
  Thêm/đổi DB → KHÔNG sửa OrderService!
```

---

## 10. Real-world Analogy: Vô-lăng ô tô

```
🚗 VÔ-LĂNG Ô TÔ = Abstraction

  Người lái xe chỉ cần biết:
  ┌────────────────────────┐
  │ 🔄 Xoay trái → rẽ trái │
  │ 🔄 Xoay phải → rẽ phải │
  │ 🏎️ Nhấn ga → tăng tốc  │
  │ 🛑 Nhấn phanh → dừng   │
  └────────────────────────┘

  KHÔNG CẦN BIẾT bên trong:
  ┌────────────────────────────────────┐
  │ Hệ thống trợ lực lái thủy lực    │
  │ Hộp số tự động 8 cấp             │
  │ Hệ thống ABS chống bó cứng phanh │
  │ Bơm nhiên liệu điện tử           │
  │ ECU điều khiển động cơ            │
  │ Turbo tăng áp biến thiên          │
  └────────────────────────────────────┘

  → Cùng 1 "interface" (vô-lăng, ga, phanh)
  → Lái được Toyota, BMW, Tesla, Vinfast...
  → KHÔNG CẦN HỌC LẠI mỗi khi đổi xe!
```

```csharp
// Tương tự trong code:
interface ICar
{
    void TurnLeft();
    void TurnRight();
    void Accelerate();
    void Brake();
}

// Toyota, BMW, Tesla đều implement ICar
// Người lái (code gọi) dùng ICar → không cần biết xe gì!
void Drive(ICar car)
{
    car.Accelerate();
    car.TurnLeft();
    car.Brake();
}
```

---

## 11. So sánh C# vs JavaScript

### 📊 Abstract Class

```csharp
// ═══ C# — Abstract class đầy đủ ═══
abstract class Animal
{
    public string Name { get; set; }

    protected Animal(string name) { Name = name; }

    public abstract void MakeSound();    // Bắt buộc override

    public void Sleep()                  // Concrete method
    {
        Console.WriteLine($"{Name} đang ngủ...");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }
    public override void MakeSound() => Console.WriteLine("Gâu!");
}

// Animal a = new Animal("test");  // ❌ Lỗi compile!
```

```javascript
// ═══ JavaScript — Không có abstract class thực sự ═══
class Animal {
    constructor(name) {
        this.name = name;
        if (new.target === Animal) {
            throw new Error("Cannot instantiate abstract class!");
        }
    }

    makeSound() {
        throw new Error("Must implement makeSound()!");
    }

    sleep() {
        console.log(`${this.name} đang ngủ...`);
    }
}

class Dog extends Animal {
    makeSound() { console.log("Gâu!"); }
}

// new Animal("test");  // Throw error ở runtime (không phải compile)
```

### 📊 Interface

```csharp
// ═══ C# — Interface thực sự ═══
interface IPlayable
{
    void Play();
    void Stop();
}

interface ISaveable
{
    void Save();
}

class Game : IPlayable, ISaveable   // Multiple interfaces!
{
    public void Play() { }
    public void Stop() { }
    public void Save() { }
}
```

```javascript
// ═══ JavaScript — KHÔNG CÓ interface! ═══
// Phải dùng convention hoặc TypeScript

// Cách 1: Duck typing (không cần khai báo)
class Game {
    play() { }
    stop() { }
    save() { }
}

// Cách 2: TypeScript interfaces
// interface IPlayable {
//     play(): void;
//     stop(): void;
// }
```

### 📊 Bảng so sánh

| Tính năng | C# | JavaScript |
|-----------|-----|-----------|
| Abstract class | ✅ `abstract class` | ❌ Simulate bằng throw Error |
| Abstract method | ✅ `abstract void M()` | ❌ Simulate |
| Interface | ✅ `interface IName` | ❌ Không có (TypeScript có) |
| Multiple interfaces | ✅ Hỗ trợ | N/A |
| Enforce ở | Compile-time | Runtime (throw error) |
| Type safety | ✅ Mạnh | ❌ Duck typing |
| Default methods | ✅ C# 8+ | N/A |

---

## 12. Sai lầm thường gặp & Best Practices

### ❌ Sai lầm #1: Cố tạo object từ abstract class

```csharp
abstract class Shape { }

// Shape s = new Shape();  // ❌ Lỗi compile!
// Abstract class KHÔNG THỂ instantiate!

Shape s = new Circle();    // ✅ Dùng class con!
```

### ❌ Sai lầm #2: Quên implement abstract method

```csharp
abstract class Animal
{
    public abstract void MakeSound();
    public abstract string GetFood();
}

class Dog : Animal
{
    public override void MakeSound() { }
    // ❌ Quên GetFood() → Lỗi compile!
    // "Dog does not implement inherited abstract member GetFood()"
}
```

### ❌ Sai lầm #3: Interface quá lớn

```csharp
// ❌ Interface quá to — vi phạm Interface Segregation Principle
interface IAnimal
{
    void Eat();
    void Sleep();
    void Fly();        // Cá không bay được!
    void Swim();       // Chim không bơi giỏi!
    void MakeSound();
    void Hunt();       // Thỏ không săn mồi!
}

// ✅ Tách thành nhiều interface nhỏ
interface IFeedable { void Eat(); }
interface ISleepable { void Sleep(); }
interface IFlyable { void Fly(); }
interface ISwimmable { void Swim(); }
```

### ❌ Sai lầm #4: Dùng abstract class khi nên dùng interface

```csharp
// ❌ Sai — Logger và Database không liên quan → không nên dùng abstract class
abstract class Disposable
{
    public abstract void Dispose();
}
class Logger : Disposable { ... }
class Database : Disposable { ... }
// → Logger không thể kế thừa class nào khác nữa!

// ✅ Đúng — dùng interface cho "khả năng"
interface IDisposable
{
    void Dispose();
}
class Logger : BaseLogger, IDisposable { ... }     // Vừa kế thừa, vừa implement
class Database : BaseDatabase, IDisposable { ... }
```

### ❌ Sai lầm #5: Truy cập default method qua kiểu class

```csharp
interface IGreetable
{
    void Greet();
    void SayBye() { Console.WriteLine("Bye!"); }  // Default method
}

class Person : IGreetable
{
    public void Greet() => Console.WriteLine("Hi!");
}

Person p = new Person();
// p.SayBye();        // ❌ Lỗi! Default method không thấy qua kiểu class

IGreetable g = p;
g.SayBye();           // ✅ OK — qua kiểu interface
```

### ✅ Best Practices

```
┌──────────────────────────────────────────────────────┐
│ 1. Ưu tiên INTERFACE hơn abstract class              │
│    (trừ khi cần chia sẻ code chung)                 │
│                                                      │
│ 2. Interface nên NHỎ và CHUYÊN BIỆT                │
│    (Interface Segregation Principle)                 │
│                                                      │
│ 3. Đặt tên interface bắt đầu bằng I                │
│                                                      │
│ 4. Abstract class cho quan hệ IS-A                  │
│    Interface cho "capabilities" (CAN-DO)            │
│                                                      │
│ 5. Program to an INTERFACE, not an implementation   │
│    IDatabase db = new SqlDb();  // ✅               │
│    SqlDb db = new SqlDb();      // ❌ (nếu có thể) │
│                                                      │
│ 6. Kết hợp abstract class + interfaces              │
│    class Tesla : Vehicle, IChargeable, IGpsEnabled  │
└──────────────────────────────────────────────────────┘
```

---

## 🗺️ Tổng kết 4 trụ cột OOP

```
┌─────────────────────────────────────────────────────┐
│           4 TRỤ CỘT CỦA OOP                        │
│                                                     │
│  ┌──────────────┐  ┌──────────────┐                │
│  │ Encapsulation│  │ Inheritance  │                │
│  │ (Bài 19)     │  │ (Bài 20)     │                │
│  │ Đóng gói     │  │ Kế thừa      │                │
│  │ dữ liệu     │  │ code & type  │                │
│  └──────┬───────┘  └──────┬───────┘                │
│         │                 │                         │
│  ┌──────┴───────┐  ┌──────┴───────┐                │
│  │ Polymorphism │  │ ABSTRACTION  │  ← BÀI NÀY    │
│  │ (Bài 21)     │  │ (Bài 22)     │                │
│  │ Đa hình      │  │ Trừu tượng   │                │
│  │ 1 interface  │  │ Ẩn phức tạp  │                │
│  │ nhiều impl   │  │ hiện đơn giản│                │
│  └──────────────┘  └──────────────┘                │
│                                                     │
│  Tất cả kết hợp → Code linh hoạt, dễ bảo trì! 🎉 │
└─────────────────────────────────────────────────────┘
```

> **Chúc mừng!** Bạn đã hoàn thành 4 trụ cột OOP! Từ đây, bạn có thể thiết kế hệ thống phần mềm chuyên nghiệp. 🚀

---

*"Abstraction là skill quan trọng nhất của developer — biết ẩn cái gì, hiện cái gì."* 🎯
