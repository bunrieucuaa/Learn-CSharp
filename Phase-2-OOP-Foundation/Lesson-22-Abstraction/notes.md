# 📝 Bài 22: Abstraction — Tóm tắt & Checklist

---

## 📋 Tóm tắt nhanh

### 1. Abstraction là gì?

```
Ẩn chi tiết phức tạp → Chỉ hiện interface cần thiết
Người dùng biết LÀM GÌ (What), không cần biết LÀM THẾ NÀO (How)
```

### 2. Hai công cụ chính

```
  Abstract Class               Interface
  ─────────────               ─────────
  abstract class Shape { }    interface IShape { }
  Có code + abstract methods  Chỉ có signatures
  Có fields, constructor      Không fields, constructor
  Kế thừa ĐƠN (1 class)      Implement NHIỀU
  Quan hệ IS-A                Quan hệ CAN-DO
```

### 3. Abstract Class — Cú pháp

```csharp
abstract class Animal               // Không thể new
{
    public string Name { get; set; } // Có properties
    protected Animal(string name) { } // Có constructor

    public abstract void Speak();    // Abstract — PHẢI override
    public void Sleep() { }          // Concrete — dùng luôn
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }
    public override void Speak() => Console.WriteLine("Gâu!");
}
```

### 4. Interface — Cú pháp

```csharp
interface IPlayable                  // Bắt đầu bằng I
{
    void Play();                     // Chỉ signature
    void Stop();
    bool IsPlaying { get; }
}

class Music : IPlayable             // Implement tất cả
{
    public bool IsPlaying { get; private set; }
    public void Play() { IsPlaying = true; }
    public void Stop() { IsPlaying = false; }
}
```

### 5. Multiple Interfaces

```csharp
class Game : IPlayable, ISaveable, IDisposable
{
    // Implement TẤT CẢ methods từ 3 interfaces
}
```

---

## 📊 Bảng so sánh: Abstract Class vs Interface

| Đặc điểm | Abstract Class | Interface |
|-----------|---------------|-----------|
| **Từ khóa** | `abstract class` | `interface` |
| **Tạo object?** | ❌ Không | ❌ Không |
| **Concrete methods?** | ✅ Có | ✅ Default (C# 8+) |
| **Abstract methods?** | ✅ Có | ✅ (mặc định) |
| **Fields?** | ✅ Có | ❌ Không |
| **Properties?** | ✅ Có | ✅ Có (auto) |
| **Constructor?** | ✅ Có | ❌ Không |
| **Access modifiers?** | ✅ Có | Mặc định public |
| **Đa kế thừa?** | ❌ Chỉ 1 | ✅ Nhiều |
| **Naming** | Tự do | Bắt đầu bằng `I` |
| **Quan hệ** | IS-A | CAN-DO |
| **Dùng khi** | Chia sẻ code chung | Định nghĩa contract |

---

## 🧠 Sơ đồ quyết định

```
Cần abstraction?
    │
    ├── Các class con CHIA SẺ CODE chung?
    │   ├── CÓ → Abstract Class
    │   └── KHÔNG → Interface
    │
    ├── Cần fields hoặc constructor?
    │   ├── CÓ → Abstract Class
    │   └── KHÔNG → Interface
    │
    ├── Class cần NHIỀU "khả năng"?
    │   ├── CÓ → Interface (multiple)
    │   └── KHÔNG → Cả hai đều OK
    │
    └── Có thể KẾT HỢP cả hai!
        class Tesla : Vehicle, IChargeable, IGpsEnabled
        (1 abstract class + nhiều interfaces)
```

---

## 🔑 Từ khóa quan trọng

| Từ khóa | Ý nghĩa | Ví dụ |
|---------|---------|-------|
| `abstract class` | Class trừu tượng | `abstract class Shape` |
| `abstract` (method) | Method không có body | `public abstract void Draw();` |
| `interface` | Hợp đồng/contract | `interface IPlayable` |
| `override` | Implement abstract method | `public override void Draw()` |
| `virtual` | Method có thể override | `public virtual void Sleep()` |
| `:` | Kế thừa/implement | `class Dog : Animal, IPlayable` |

---

## 📋 Interface Naming — Quy ước

```
interface I + Tính từ/Danh từ

Ví dụ:
  IDisposable    → Có thể giải phóng
  IComparable    → Có thể so sánh
  IEnumerable    → Có thể duyệt
  IPlayable      → Có thể phát
  ISaveable      → Có thể lưu
  IRepository    → Là kho dữ liệu
```

---

## ⚡ Patterns quan trọng

### Pattern 1: Template Method (Abstract Class)

```csharp
abstract class Report
{
    // Template method — gọi abstract methods
    public void Generate()
    {
        GatherData();    // Abstract
        ProcessData();   // Abstract
        PrintReport();   // Concrete
    }

    protected abstract void GatherData();
    protected abstract void ProcessData();

    private void PrintReport() { /* code chung */ }
}
```

### Pattern 2: Dependency Injection (Interface)

```csharp
class Service
{
    private IDatabase db;  // Phụ thuộc vào interface

    public Service(IDatabase database)  // Inject qua constructor
    {
        db = database;
    }
}
// Dễ dàng đổi: new Service(new SqlDb()) → new Service(new MongoDb())
```

### Pattern 3: Kết hợp Abstract + Interface

```csharp
abstract class Vehicle : ITrackable    // IS-A + CAN-DO
{
    // Shared code + abstract methods + interface methods
}
```

---

## ❌ Những lỗi cần tránh

1. ❌ **Tạo object từ abstract class** → Lỗi compile
2. ❌ **Quên implement tất cả abstract methods** → Lỗi compile
3. ❌ **Interface quá lớn** → Tách nhỏ (Interface Segregation)
4. ❌ **Dùng abstract class cho "khả năng"** → Dùng interface
5. ❌ **Truy cập default method qua kiểu class** → Phải qua kiểu interface
6. ❌ **Quên chữ I khi đặt tên interface** → IPlayable, không phải Playable

---

## ✅ Checklist — Tự đánh giá

### Abstract Class
- [ ] Hiểu abstract class = không thể new, có thể có code chung
- [ ] Biết tạo abstract method (không có body)
- [ ] Biết class con PHẢI override tất cả abstract methods
- [ ] Phân biệt abstract method vs virtual method
- [ ] Biết abstract class có thể có constructor, fields

### Interface
- [ ] Hiểu interface = contract/hợp đồng
- [ ] Biết implement interface (dùng `:`)
- [ ] Biết 1 class có thể implement NHIỀU interfaces
- [ ] Đặt tên interface đúng convention (bắt đầu bằng I)
- [ ] Biết dùng interface làm kiểu biến (polymorphism)
- [ ] Hiểu default interface methods (C# 8+)

### Kết hợp
- [ ] Phân biệt khi nào dùng abstract class vs interface
- [ ] Biết kết hợp abstract class + interfaces
- [ ] Hiểu Dependency Inversion (phụ thuộc vào abstraction)
- [ ] Dùng `is` kiểm tra interface
- [ ] Pattern matching với interface types

### Bài tập
- [ ] Hoàn thành 5/5 bài tập
- [ ] Hoàn thành Challenge: E-Learning Platform
- [ ] Thử thêm interface mới mà không sửa code cũ

---

## 🔗 Liên kết với các bài khác

```
Bài 19: Encapsulation  → Ẩn dữ liệu (private)
Bài 20: Inheritance     → Kế thừa code (IS-A)
Bài 21: Polymorphism    → 1 interface, nhiều behavior
Bài 22: ABSTRACTION     → Ẩn phức tạp, hiện contract
                            Abstract class + Interface

  ┌──────────────────────────────────────────┐
  │ Encapsulation: ẨN dữ liệu              │
  │ Inheritance:   CHIA SẺ code             │
  │ Polymorphism:  1 interface → N behavior │
  │ Abstraction:   ẨN phức tạp → contract   │
  │                                          │
  │ → 4 trụ cột OOP hoàn thành! 🎉        │
  └──────────────────────────────────────────┘
```

---

## 💡 Mẹo ghi nhớ

```
🚗 Abstract Class = Bản thiết kế xe
   - Có sẵn: khung xe, bánh xe (code chung)
   - Để trống: động cơ, nội thất (abstract)
   - Mỗi hãng tự hoàn thiện

📋 Interface = Bằng lái xe
   - Chỉ ghi: "Biết lái, biết đỗ, biết rẽ"
   - Không ghi: "Dùng xe gì, lái kiểu gì"
   - Ai có bằng → được lái (contract)

🏗️ Kết hợp = Xây nhà
   - Abstract class: Bản vẽ kiến trúc (cấu trúc chung)
   - Interface: Tiêu chuẩn xây dựng (phải đáp ứng)
   - Concrete class: Ngôi nhà thực tế (hoàn chỉnh)
```

> **Chúc mừng!** Bạn đã hoàn thành 4 trụ cột OOP! 🏆
> Từ đây, bạn có đủ kiến thức để thiết kế hệ thống phần mềm theo chuẩn OOP. 🚀

---

*"Program to an interface, not an implementation." — Gang of Four* 🎯
