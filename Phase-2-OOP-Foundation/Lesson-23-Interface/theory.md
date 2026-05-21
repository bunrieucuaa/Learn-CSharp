# 📘 Bài 23: Interface Nâng Cao — Patterns & Built-in Interfaces

> **"Interface nhỏ, sức mạnh lớn — thiết kế tốt bắt đầu từ contract đúng."**

---

## 📋 Mục lục

1. [Nhắc lại: Interface = Hợp đồng](#1-nhắc-lại-interface--hợp-đồng)
2. [Explicit Interface Implementation](#2-explicit-interface-implementation)
3. [Interface Segregation Principle (ISP)](#3-interface-segregation-principle-isp)
4. [Built-in Interfaces quan trọng](#4-built-in-interfaces-quan-trọng)
5. [Interface as Dependency — DI Pattern](#5-interface-as-dependency--di-pattern)
6. [Interface + Generic: IRepository\<T\>](#6-interface--generic-irepositoryt)
7. [Interface Inheritance](#7-interface-inheritance)
8. [Static Abstract Members (C# 11+)](#8-static-abstract-members-c-11)
9. [Marker Interface Pattern](#9-marker-interface-pattern)
10. [So sánh C# vs Java vs JavaScript](#10-so-sánh-c-vs-java-vs-javascript)
11. [Common Mistakes & Best Practices](#11-common-mistakes--best-practices)

---

## 1. Nhắc lại: Interface = Hợp đồng

> 📌 Ở **Bài 22 (Abstraction)**, ta đã học:
> - Interface là **contract** — chỉ khai báo signature
> - Đặt tên bắt đầu bằng `I` (IPlayable, IDisposable...)
> - 1 class implement **nhiều** interfaces
> - Default interface methods (C# 8+)

```
Bài 22 đã biết:                  Bài 23 sẽ học:
─────────────────                ───────────────────
✅ Syntax cơ bản                 🔸 Explicit Implementation
✅ Implement interface           🔸 ISP — tách interface nhỏ
✅ Multiple interfaces           🔸 Built-in: IComparable, IDisposable...
✅ Default methods               🔸 Dependency Injection pattern
✅ Abstract class vs Interface   🔸 Generic interface: IRepository<T>
✅ Naming convention (I-)        🔸 Interface kế thừa interface
                                 🔸 Static abstract members
                                 🔸 Marker interface
```

---

## 2. Explicit Interface Implementation

### 📖 Vấn đề: Hai interface có method trùng tên

```csharp
interface IPrinter
{
    void Start();   // Bắt đầu in
    void Stop();
}

interface IScanner
{
    void Start();   // Bắt đầu quét
    void Stop();
}

// MultiFunctionDevice implement CẢ HAI — nhưng Start() làm gì?
class MultiFunctionDevice : IPrinter, IScanner
{
    // ❌ Nếu dùng implicit — CHỈ CÓ 1 Start() cho cả hai!
    public void Start()
    {
        Console.WriteLine("???");  // In hay Quét?
    }

    public void Stop()
    {
        Console.WriteLine("???");
    }
}
```

### ✅ Giải pháp: Explicit Interface Implementation

```csharp
class MultiFunctionDevice : IPrinter, IScanner
{
    // Explicit — thuộc về IPrinter
    void IPrinter.Start()
    {
        Console.WriteLine("🖨️ Bắt đầu IN...");
    }

    void IPrinter.Stop()
    {
        Console.WriteLine("🖨️ Dừng in.");
    }

    // Explicit — thuộc về IScanner
    void IScanner.Start()
    {
        Console.WriteLine("📠 Bắt đầu QUÉT...");
    }

    void IScanner.Stop()
    {
        Console.WriteLine("📠 Dừng quét.");
    }
}
```

### ⚙️ Cách gọi Explicit members

```csharp
MultiFunctionDevice device = new MultiFunctionDevice();

// ❌ Không thể gọi trực tiếp từ biến kiểu class!
// device.Start();  // Lỗi compile!

// ✅ PHẢI ép kiểu về interface
IPrinter printer = device;
printer.Start();   // "🖨️ Bắt đầu IN..."

IScanner scanner = device;
scanner.Start();   // "📠 Bắt đầu QUÉT..."

// Hoặc cast inline:
((IPrinter)device).Start();
((IScanner)device).Start();
```

### 📊 Implicit vs Explicit

```
┌───────────────────┬─────────────────────┬─────────────────────┐
│                   │ Implicit            │ Explicit            │
├───────────────────┼─────────────────────┼─────────────────────┤
│ Cú pháp          │ public void Start() │ void IFoo.Start()   │
│ Access modifier   │ public              │ KHÔNG ghi (private  │
│                   │                     │ to interface)        │
│ Gọi từ class type │ ✅ OK              │ ❌ Không được       │
│ Gọi từ interface │ ✅ OK              │ ✅ OK               │
│ Dùng khi         │ Bình thường         │ 2 interface trùng   │
│                   │                     │ tên method          │
└───────────────────┴─────────────────────┴─────────────────────┘
```

### 💡 Khi nào dùng Explicit?

```
1. Hai interface có method TRÙNG TÊN → Bắt buộc explicit
2. Muốn ẨN method khỏi API chính → Chỉ lộ qua interface
3. Implement interface để "tuân thủ" nhưng KHÔNG muốn user dùng trực tiếp
```

---

## 3. Interface Segregation Principle (ISP)

### 📖 Nguyên tắc

> **"Không nên ÉP class implement những method mà nó KHÔNG CẦN."**
> — Chữ **I** trong SOLID

### ❌ Vi phạm ISP: Interface quá lớn (Fat Interface)

```csharp
// ❌ Interface "béo" — có method không phải ai cũng cần
interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
    void GetPaid();
}

// Robot chỉ làm việc — KHÔNG ăn, KHÔNG ngủ, KHÔNG nhận lương!
class Robot : IWorker
{
    public void Work() => Console.WriteLine("🤖 Đang làm việc...");

    // ❌ Phải implement nhưng KHÔNG CÓ Ý NGHĨA!
    public void Eat() => throw new NotSupportedException("Robot không ăn!");
    public void Sleep() => throw new NotSupportedException("Robot không ngủ!");
    public void GetPaid() => throw new NotSupportedException("Robot không nhận lương!");
}
```

### ✅ Tuân thủ ISP: Tách interface nhỏ

```csharp
// ✅ Tách thành nhiều interface nhỏ, mỗi cái 1 responsibility
interface IWorkable
{
    void Work();
}

interface IFeedable
{
    void Eat();
}

interface ISleepable
{
    void Sleep();
}

interface IPayable
{
    void GetPaid();
}

// Human implement TẤT CẢ
class HumanWorker : IWorkable, IFeedable, ISleepable, IPayable
{
    public void Work()    => Console.WriteLine("👨‍💼 Đang làm việc...");
    public void Eat()     => Console.WriteLine("🍚 Đang ăn trưa...");
    public void Sleep()   => Console.WriteLine("😴 Đang ngủ...");
    public void GetPaid() => Console.WriteLine("💰 Nhận lương!");
}

// Robot CHỈ implement IWorkable — không bị ép method thừa!
class Robot : IWorkable
{
    public void Work() => Console.WriteLine("🤖 Đang làm việc 24/7...");
}
```

### 📊 Minh họa ISP

```
❌ Fat Interface:                  ✅ Segregated Interfaces:

   IWorker                           IWorkable  IFeedable  ISleepable  IPayable
   ┌───────────┐                     ┌────────┐ ┌────────┐ ┌────────┐ ┌────────┐
   │ Work()    │                     │ Work() │ │ Eat()  │ │ Sleep()│ │GetPaid│
   │ Eat()     │                     └────┬───┘ └────┬───┘ └───┬────┘ └───┬────┘
   │ Sleep()   │                          │         │         │         │
   │ GetPaid() │                     Human: ──┬──────┼─────────┼─────────┘
   └─────┬─────┘                              │     │         │
         │                              Robot: ┘    ✗         ✗
   Human: ✅ OK
   Robot: ❌ Phải implement
          method vô nghĩa!
```

---

## 4. Built-in Interfaces quan trọng

### 4.1 `IComparable<T>` — Để sort object

> Khi bạn dùng `Array.Sort()` với object tự tạo, .NET cần biết **so sánh thế nào**.

```csharp
class Student : IComparable<Student>
{
    public string Name { get; set; }
    public double Score { get; set; }

    // CompareTo trả về:
    //   < 0  → this ĐỨNG TRƯỚC other
    //   = 0  → bằng nhau
    //   > 0  → this ĐỨNG SAU other
    public int CompareTo(Student? other)
    {
        if (other == null) return 1;
        return Score.CompareTo(other.Score);  // Sắp xếp tăng dần theo điểm
    }
}

// Bây giờ Array.Sort() hoạt động!
Student[] students = { ... };
Array.Sort(students);   // ✅ Sắp xếp theo Score tăng dần
```

```
CompareTo() trả về:        Ý nghĩa:
─────────────────────       ──────────
  Số âm (< 0)              this < other → this đứng trước
  0                         this == other → giữ nguyên
  Số dương (> 0)            this > other → this đứng sau
```

### 4.2 `IEquatable<T>` — So sánh bằng

```csharp
class Product : IEquatable<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Hai Product "bằng nhau" khi cùng Id
    public bool Equals(Product? other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }

    // NÊN override cả GetHashCode
    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj) => Equals(obj as Product);
}

Product p1 = new Product { Id = 1, Name = "Laptop" };
Product p2 = new Product { Id = 1, Name = "Laptop Dell" };
Console.WriteLine(p1.Equals(p2));  // true — cùng Id
```

### 4.3 `ICloneable` — Clone object

```csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Clone trả về bản sao
    public object Clone()
    {
        return new Person { Name = Name, Age = Age };  // Deep copy
    }
}

Person original = new Person { Name = "An", Age = 25 };
Person copy = (Person)original.Clone();
copy.Name = "Bình";
// original.Name vẫn là "An" → independent copy!
```

> ⚠️ `ICloneable` trả về `object` — cần cast. Trong code hiện đại, nhiều dev tự tạo `ICloneable<T>` generic.

### 4.4 `IDisposable` — Giải phóng tài nguyên + `using`

> Đây là interface **QUAN TRỌNG NHẤT** cần biết!

```csharp
class FileLogger : IDisposable
{
    private StreamWriter writer;
    private bool disposed = false;

    public FileLogger(string filePath)
    {
        writer = new StreamWriter(filePath, append: true);
    }

    public void Log(string message)
    {
        if (disposed) throw new ObjectDisposedException(nameof(FileLogger));
        writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    // Dispose pattern — giải phóng tài nguyên
    public void Dispose()
    {
        if (!disposed)
        {
            writer?.Close();
            writer?.Dispose();
            disposed = true;
            Console.WriteLine("📁 FileLogger đã được giải phóng.");
        }
    }
}
```

### ⚡ `using` statement — Tự động gọi Dispose()

```csharp
// Cách 1: using block (C# truyền thống)
using (FileLogger logger = new FileLogger("app.log"))
{
    logger.Log("Bắt đầu");
    logger.Log("Đang xử lý...");
    logger.Log("Hoàn thành");
}   // ← Dispose() được gọi TỰ ĐỘNG ở đây!

// Cách 2: using declaration (C# 8+) — ngắn gọn hơn
using FileLogger logger2 = new FileLogger("app.log");
logger2.Log("Hello");
// Dispose() gọi khi biến ra khỏi scope
```

```
Không dùng using:                   Dùng using:
──────────────────                   ──────────────
FileLogger log = new FileLogger();   using (var log = new FileLogger())
log.Log("Hi");                       {
// ❌ QUÊN gọi Dispose()!               log.Log("Hi");
// → Memory leak!                   }  // ← Auto Dispose() ✅
// → File bị lock!
```

### 4.5 `IEnumerable<T>` — Duyệt collection (Preview)

> Sẽ học kỹ ở **Phase 3 (Collections & LINQ)**. Giới thiệu sơ ở đây.

```csharp
using System.Collections;
using System.Collections.Generic;

class StudentGroup : IEnumerable<Student>
{
    private Student[] students;

    // Cho phép dùng foreach với StudentGroup!
    public IEnumerator<Student> GetEnumerator()
    {
        foreach (Student s in students)
            yield return s;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Sử dụng:
StudentGroup group = new StudentGroup();
foreach (Student s in group)     // ✅ foreach hoạt động nhờ IEnumerable!
{
    Console.WriteLine(s.Name);
}
```

### 📊 Bảng tổng hợp Built-in Interfaces

```
┌──────────────────┬─────────────────────────────────┬───────────────────┐
│ Interface        │ Mục đích                        │ Method chính      │
├──────────────────┼─────────────────────────────────┼───────────────────┤
│ IComparable<T>   │ So sánh, sắp xếp objects        │ CompareTo(T)      │
│ IEquatable<T>    │ So sánh bằng nhau               │ Equals(T)         │
│ ICloneable       │ Clone/copy object               │ Clone()           │
│ IDisposable      │ Giải phóng tài nguyên           │ Dispose()         │
│ IEnumerable<T>   │ Duyệt bằng foreach              │ GetEnumerator()   │
│ IFormattable     │ Custom ToString format           │ ToString(fmt,fp)  │
│ IConvertible     │ Convert giữa các kiểu           │ ToInt32(), etc.   │
└──────────────────┴─────────────────────────────────┴───────────────────┘
```

---

## 5. Interface as Dependency — DI Pattern

### 📖 Dependency Injection (DI) cơ bản

> Bài 22 đã preview Dependency Inversion. Bây giờ ta đi sâu hơn vào **pattern thực tế**.

```
❌ KHÔNG có DI:                    ✅ CÓ DI:
──────────────                     ──────────
class OrderProcessor               class OrderProcessor
{                                   {
  // Hard-coded dependency            private INotificationService svc;
  EmailService svc = new();
                                      // Inject qua constructor
  void Process(Order o)               public OrderProcessor(INotificationService s)
  {                                   {
    svc.Send(o.Email, "OK");            svc = s;
  }                                   }
}
                                      void Process(Order o)
// ❌ Muốn đổi sang SMS?               {
// → Phải SỬA OrderProcessor!            svc.Notify(o.Email, "OK");
                                      }
                                    }

                                    // ✅ Đổi sang SMS? Chỉ đổi lúc khởi tạo!
                                    new OrderProcessor(new SmsService());
```

### 🔧 Code mẫu DI

```csharp
// 1. Định nghĩa contract
interface INotificationService
{
    void Notify(string recipient, string message);
}

// 2. Nhiều implementation
class EmailService : INotificationService
{
    public void Notify(string recipient, string message)
        => Console.WriteLine($"📧 Email → {recipient}: {message}");
}

class SmsService : INotificationService
{
    public void Notify(string recipient, string message)
        => Console.WriteLine($"📱 SMS → {recipient}: {message}");
}

class PushService : INotificationService
{
    public void Notify(string recipient, string message)
        => Console.WriteLine($"🔔 Push → {recipient}: {message}");
}

// 3. Class "consumer" nhận interface qua CONSTRUCTOR
class OrderProcessor
{
    private readonly INotificationService notifier;

    public OrderProcessor(INotificationService service)   // ← DI!
    {
        notifier = service;
    }

    public void ProcessOrder(string orderId, string customer)
    {
        Console.WriteLine($"📦 Xử lý đơn hàng {orderId}...");
        notifier.Notify(customer, $"Đơn hàng {orderId} đã xác nhận!");
    }
}

// 4. Sử dụng — đổi implementation KHÔNG sửa OrderProcessor!
OrderProcessor op1 = new OrderProcessor(new EmailService());
OrderProcessor op2 = new OrderProcessor(new SmsService());
```

### 💡 3 kiểu DI

```
1. Constructor Injection (phổ biến nhất):
   public OrderProcessor(INotificationService svc) { }

2. Property Injection:
   public INotificationService Notifier { get; set; }

3. Method Injection:
   public void Process(INotificationService svc) { }
```

---

## 6. Interface + Generic: IRepository\<T\>

> Kết hợp interface với Generic (sẽ học kỹ Phase 3) tạo ra pattern cực mạnh.

```csharp
// Generic interface — T là kiểu dữ liệu bất kỳ
interface IRepository<T>
{
    void Add(T entity);
    T GetById(int id);
    T[] GetAll();
    void Update(T entity);
    void Delete(int id);
    int Count { get; }
}

// Implementation cho Student
class StudentRepository : IRepository<Student>
{
    private Student[] students = new Student[100];
    private int count = 0;
    public int Count => count;

    public void Add(Student entity) { students[count++] = entity; }
    public Student GetById(int id) { /* tìm theo id */ }
    public Student[] GetAll() { /* trả về tất cả */ }
    public void Update(Student entity) { /* cập nhật */ }
    public void Delete(int id) { /* xóa */ }
}

// Implementation cho Product — CÙNG interface, KHÁC entity!
class ProductRepository : IRepository<Product>
{
    public void Add(Product entity) { /* ... */ }
    // ...
}
```

```
IRepository<T> — Một contract, nhiều kiểu dữ liệu:

   IRepository<T>
       │
       ├── StudentRepository  : IRepository<Student>
       ├── ProductRepository   : IRepository<Product>
       ├── OrderRepository     : IRepository<Order>
       └── UserRepository      : IRepository<User>

   Tất cả đều có: Add, GetById, GetAll, Update, Delete
   Nhưng mỗi cái làm việc với kiểu dữ liệu KHÁC NHAU!
```

---

## 7. Interface Inheritance

### 📖 Interface có thể kế thừa interface khác

```csharp
interface IEntity
{
    int Id { get; set; }
}

interface IAuditable : IEntity                // Kế thừa IEntity
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}

interface ISoftDeletable : IEntity            // Cũng kế thừa IEntity
{
    bool IsDeleted { get; set; }
    void SoftDelete();
}

// IFullEntity kế thừa CẢ HAI — "kim cương" không vấn đề với interface!
interface IFullEntity : IAuditable, ISoftDeletable
{
    string CreatedBy { get; set; }
}
```

### 📊 Minh họa cây kế thừa

```
        IEntity
       /       \
  IAuditable   ISoftDeletable
       \       /
     IFullEntity          ← Đa kế thừa interface: OK!
         │
     class Product : IFullEntity
     {
         // Phải implement TẤT CẢ members từ cả 3 interfaces:
         // IEntity: Id
         // IAuditable: CreatedAt, UpdatedAt
         // ISoftDeletable: IsDeleted, SoftDelete()
         // IFullEntity: CreatedBy
     }
```

> ⚠️ **Lưu ý:** Đa kế thừa interface KHÔNG có "Diamond Problem" như class, vì interface chỉ có **signature** — không có **state** (fields).

---

## 8. Static Abstract Members (C# 11+)

### 📖 Tính năng mới: Interface có thể yêu cầu static members

```csharp
// C# 11+ — Interface với static abstract member
interface IParseable<TSelf> where TSelf : IParseable<TSelf>
{
    static abstract TSelf Parse(string text);
    static abstract bool TryParse(string text, out TSelf result);
}

class Temperature : IParseable<Temperature>
{
    public double Value { get; set; }
    public string Unit { get; set; }

    public static Temperature Parse(string text)
    {
        // "36.5°C" → Temperature object
        double value = double.Parse(text.TrimEnd('°', 'C', 'F'));
        string unit = text.Contains('F') ? "F" : "C";
        return new Temperature { Value = value, Unit = unit };
    }

    public static bool TryParse(string text, out Temperature result)
    {
        try { result = Parse(text); return true; }
        catch { result = null!; return false; }
    }
}
```

> 💡 Đây là tính năng **nâng cao** — chủ yếu dùng trong library design. Biết sự tồn tại là đủ ở giai đoạn này.

---

## 9. Marker Interface Pattern

### 📖 Interface rỗng — chỉ để "đánh dấu"

```csharp
// Marker interface — không có method nào!
interface ISerializable { }
interface IAuditable { }
interface ICacheable { }

class Order : ISerializable, ICacheable
{
    public int Id { get; set; }
    public decimal Total { get; set; }
}

class Log : ISerializable
{
    public string Message { get; set; }
}

// Kiểm tra marker:
void SaveToCache(object obj)
{
    if (obj is ICacheable)
    {
        Console.WriteLine("✅ Object này có thể cache!");
        // Logic cache...
    }
    else
    {
        Console.WriteLine("❌ Object này KHÔNG cacheable.");
    }
}
```

### 💡 Marker Interface vs Attribute

```
Marker Interface:                  Attribute (hiện đại hơn):
─────────────────                  ──────────────────────────
interface ICacheable { }           [Cacheable]
class Order : ICacheable { }      class Order { }

✅ Kiểm tra runtime bằng `is`     ✅ Kiểm tra bằng Reflection
✅ Dùng làm generic constraint    ✅ Linh hoạt hơn (có parameters)
❌ Hạn chế inheritance             ✅ Không ảnh hưởng hierarchy

→ Trong C# hiện đại, Attribute thường được ưa chuộng hơn.
→ Nhưng Marker Interface vẫn dùng khi cần generic constraint.
```

---

## 10. So sánh C# vs Java vs JavaScript

### 📊 Bảng so sánh

| Tính năng | C# | Java | JavaScript/TS |
|-----------|-----|------|---------------|
| Từ khóa | `interface` | `interface` | `interface` (TS only) |
| Prefix I | ✅ Quy ước IFoo | ❌ Không | ❌ Không |
| Multiple implement | ✅ Có | ✅ Có | ✅ TS `implements` |
| Default methods | ✅ C# 8+ | ✅ Java 8+ | N/A |
| Static abstract | ✅ C# 11+ | ❌ Không | N/A |
| Properties | ✅ Có | ❌ Dùng getter/setter | ✅ TS có |
| Explicit impl | ✅ Có | ❌ Không | ❌ Không |
| Runtime tồn tại | ✅ Có | ✅ Có | ❌ Bị xóa khi compile |

### 🔄 JS/TS so sánh trực tiếp

```typescript
// TypeScript — interface
interface ILogger {
    log(message: string): void;
    level: string;
}

class ConsoleLogger implements ILogger {
    level = "info";
    log(message: string): void {
        console.log(`[${this.level}] ${message}`);
    }
}

// ⚠️ TS interface BỊ XÓA khi compile sang JS!
// → Runtime không biết interface là gì
// → Không thể dùng `instanceof` với interface
```

```csharp
// C# — interface tồn tại ở RUNTIME
interface ILogger
{
    void Log(string message);
    string Level { get; }
}

class ConsoleLogger : ILogger
{
    public string Level => "Info";
    public void Log(string message)
        => Console.WriteLine($"[{Level}] {message}");
}

// ✅ Runtime check hoạt động!
object obj = new ConsoleLogger();
if (obj is ILogger logger)
{
    logger.Log("Hello!");  // ✅ OK
}
```

---

## 11. Common Mistakes & Best Practices

### ❌ Sai lầm thường gặp

```
1. ❌ Interface quá lớn (God Interface)
   → Giải pháp: Tách nhỏ theo ISP

2. ❌ Implement interface rồi throw NotSupportedException
   → Giải pháp: Class đó KHÔNG NÊN implement interface đó

3. ❌ Quên gọi Dispose() cho IDisposable objects
   → Giải pháp: LUÔN dùng `using` statement

4. ❌ Explicit implementation nhưng không hiểu cách gọi
   → Giải pháp: Phải cast về interface type

5. ❌ Implement IComparable nhưng quên override Equals/GetHashCode
   → Giải pháp: Implement cả 3 cho nhất quán

6. ❌ Dùng interface ở MỌI NƠI (over-abstraction)
   → Giải pháp: Chỉ dùng khi có >1 implementation hoặc cần testability
```

### ✅ Best Practices

```
1. ✅ Interface NHỎ, tập trung (1-5 methods)
2. ✅ Đặt tên rõ ràng: I + Tính từ/Danh từ (IComparable, IRepository)
3. ✅ Prefer interface over abstract class khi không cần shared state
4. ✅ LUÔN dùng using cho IDisposable
5. ✅ Dùng DI khi class phụ thuộc vào service khác
6. ✅ Implement IComparable<T> generic thay vì IComparable non-generic
7. ✅ Dùng interface cho public API, implementation detail giữ internal
```

### 🧠 Nguyên tắc thiết kế interface

```
┌─────────────────────────────────────────────────────────┐
│ "Depend on abstractions, not concretions."              │
│ "Clients should not be forced to depend on              │
│  interfaces they do not use."                           │
│                                                         │
│ → Interface phải NHẸ, ĐÚNG MỤC ĐÍCH, DỄ THAY ĐỔI     │
│ → Nếu thêm method mới — TẠO interface mới, đừng sửa   │
│   interface cũ (Open/Closed Principle)                  │
└─────────────────────────────────────────────────────────┘
```

---

> **Bài tiếp theo:** Bài 24 — Abstract Class chuyên sâu (Template Method, Abstract + Interface kết hợp) 🚀

---

*"The interface is the most important thing in software design." — Martin Fowler* 🎯
