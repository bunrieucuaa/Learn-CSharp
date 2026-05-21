# 📝 Bài 23: Interface Nâng Cao — Tóm tắt & Checklist

---

## 📋 Tóm tắt nhanh

### 1. Explicit Interface Implementation

```csharp
// Khi 2 interface có method TRÙNG TÊN:
class Device : IPrinter, IScanner
{
    void IPrinter.Start() => Console.WriteLine("In...");   // Explicit
    void IScanner.Start() => Console.WriteLine("Quét...");  // Explicit
}

// Gọi: PHẢI cast về interface!
((IPrinter)device).Start();   // "In..."
((IScanner)device).Start();   // "Quét..."
```

### 2. ISP — Interface Segregation Principle

```
❌ Fat Interface:             ✅ Tách nhỏ:
interface IWorker             interface IWorkable  { Work(); }
{                             interface IFeedable  { Eat(); }
    Work();                   interface ISleepable { Sleep(); }
    Eat();
    Sleep();                  Robot : IWorkable ← CHỈ implement cái cần!
}
Robot : IWorker ← Bị ÉP implement Eat(), Sleep()!
```

### 3. Dependency Injection (DI)

```csharp
class OrderProcessor
{
    private readonly INotificationService svc;  // Interface!

    public OrderProcessor(INotificationService s) { svc = s; }  // Inject!

    public void Process() => svc.Notify("...");
}

// Đổi implementation KHÔNG sửa code:
new OrderProcessor(new EmailService());
new OrderProcessor(new SmsService());
```

---

## 📊 Built-in Interfaces quan trọng

| Interface | Method chính | Mục đích | Ví dụ sử dụng |
|-----------|-------------|---------|----------------|
| `IComparable<T>` | `CompareTo(T)` | Sort objects | `Array.Sort(students)` |
| `IEquatable<T>` | `Equals(T)` | So sánh bằng | `p1.Equals(p2)` |
| `ICloneable` | `Clone()` | Copy object | `(Person)p.Clone()` |
| `IDisposable` | `Dispose()` | Giải phóng resource | `using (var f = ...)` |
| `IEnumerable<T>` | `GetEnumerator()` | Duyệt foreach | `foreach (var x in col)` |

---

## 🔑 Cú pháp quan trọng

```
Explicit:           void IFoo.Method() { }     // Không ghi public!
using block:        using (var x = new Foo()) { ... }
using declaration:  using var x = new Foo();    // C# 8+
CompareTo:          < 0 (trước) | 0 (bằng) | > 0 (sau)
DI Constructor:     public Svc(IFoo foo) { this.foo = foo; }
Generic interface:  interface IRepo<T> { void Add(T item); }
Interface inherit:  interface IChild : IParent { }
```

---

## 📊 Khi nào dùng gì?

```
┌─────────────────────────────────────────────────┐
│ Tình huống                 → Dùng               │
├─────────────────────────────────────────────────┤
│ 2 interface trùng method   → Explicit impl      │
│ Interface quá lớn          → Tách nhỏ (ISP)     │
│ Cần sort Array.Sort()      → IComparable<T>     │
│ Dùng file/DB/network       → IDisposable+using  │
│ Class phụ thuộc service    → DI qua constructor  │
│ Cùng CRUD cho nhiều entity → IRepository<T>      │
│ Interface mở rộng          → Interface inherit   │
│ Đánh dấu class            → Marker interface    │
└─────────────────────────────────────────────────┘
```

---

## ✅ Checklist — Tự đánh giá

### Explicit Implementation
- [ ] Hiểu vấn đề: 2 interface có method trùng tên
- [ ] Biết cú pháp `void IFoo.Method()` (không ghi public)
- [ ] Biết phải cast về interface type để gọi explicit member

### ISP
- [ ] Hiểu nguyên tắc: không ép class implement method không cần
- [ ] Biết tách interface lớn thành nhiều interface nhỏ
- [ ] Biết dùng `is` để check interface tại runtime

### Built-in Interfaces
- [ ] Implement `IComparable<T>` — dùng với `Array.Sort()`
- [ ] Implement `IEquatable<T>` — override `Equals` + `GetHashCode`
- [ ] Implement `IDisposable` — pattern Dispose đúng cách
- [ ] Dùng `using` statement/declaration tự động Dispose
- [ ] Biết `IEnumerable<T>` cho phép `foreach`

### DI Pattern
- [ ] Hiểu Constructor Injection: nhận interface qua constructor
- [ ] Biết đổi implementation mà không sửa consumer class
- [ ] Biết 3 kiểu DI: Constructor, Property, Method

### Nâng cao
- [ ] Interface kế thừa interface
- [ ] Generic interface: `IRepository<T>`
- [ ] Biết Marker Interface pattern
- [ ] Biết Static abstract members (C# 11+) tồn tại

### Bài tập
- [ ] Hoàn thành 5/5 bài tập
- [ ] Hoàn thành Challenge: Plugin System
- [ ] Thử thêm plugin mới mà KHÔNG sửa PluginManager

---

## 🔗 Liên kết bài học

```
Bài 22: Abstraction      → Interface cơ bản, Abstract class vs Interface
Bài 23: INTERFACE NÂNG CAO → Explicit, ISP, Built-in, DI, Generics
Bài 24: Abstract Class   → Template Method, Advanced patterns

  Bài 22 (cơ bản)  →  Bài 23 (nâng cao)  →  Phase 3 (Collections, LINQ)
  Syntax, basics       Patterns, DI           IEnumerable, Generics sâu
```

---

## 💡 Mẹo ghi nhớ

```
🔌 Interface = Ổ cắm điện
   - Ổ cắm 2 chân (IChargeable) → sạc phone, laptop
   - Ổ cắm 3 chân (IGrounded) → máy giặt, tủ lạnh
   - 1 thiết bị có thể có nhiều đầu cắm (multiple interfaces)
   - Đổi ổ cắm (DI) → đổi nguồn điện, thiết bị vẫn chạy!

🏭 ISP = Đừng bắt cá phải leo cây
   - Cá: ISwimmable ✅
   - Khỉ: IClimbable ✅
   - Đừng tạo IAnimal với cả Swim() và Climb()!

💉 DI = Đổi pin cho đồng hồ
   - Đồng hồ (OrderProcessor) không CẦN BIẾT pin gì
   - Duracell hay Energizer (EmailService hay SmsService)
   - Chỉ cần đúng kích cỡ (đúng interface)!
```

> **Bài tiếp:** Bài 24 — Abstract Class chuyên sâu 🚀

---

*"Program to an interface, not an implementation." — Gang of Four* 🎯
