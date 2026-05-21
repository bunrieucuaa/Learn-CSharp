# 📝 Bài 24: Abstract Class Nâng Cao — Tóm tắt & Checklist

---

## 📋 Tóm tắt nhanh

### 1. Template Method Pattern

```
Abstract class định nghĩa SKELETON (bộ khung)
Subclass chỉ fill in các STEPS

  abstract class Processor
  {
      public void Run()          // Template — CỐ ĐỊNH
      {
          Step1();               // abstract
          Step2();               // abstract
          SharedStep();          // concrete — dùng chung
      }
      protected abstract void Step1();
      protected abstract void Step2();
  }
```

### 2. Abstract Properties

```csharp
abstract class Employee
{
    public abstract string Type { get; }        // Chỉ get
    public abstract decimal Salary { get; set; } // Get + Set
}

class FullTime : Employee
{
    public override string Type => "Chính thức";
    public override decimal Salary { get; set; }
}
```

### 3. Protected Abstract vs Public Abstract

```
public abstract    → Bên ngoài THẤY, subclass implement
protected abstract → Chỉ dùng NỘI BỘ (Template Method steps)
```

### 4. Sealed Override

```csharp
class Dog : Animal
{
    public sealed override void MakeSound() { }  // Khóa!
}
class Puppy : Dog
{
    // ❌ Không override được MakeSound()!
}
```

### 5. Abstract Class + Interface

```csharp
abstract class Character : IAttackable, IDefendable
{
    // Shared code + abstract methods + interface implementation
}
```

---

## 🧠 Decision Tree: Abstract Class vs Interface

```
  Cần chia sẻ CODE chung?
      │
  ┌───┴───┐
  CÓ     KHÔNG
  │        │
  ▼        ▼
 Quan     INTERFACE ✅
 hệ
 IS-A?
  │
 ┌┴──┐
 CÓ KHÔNG
 │    │
 ▼    ▼
ABSTRACT   Cần nhiều
CLASS ✅   capabilities?
               │
          ┌────┴────┐
          CÓ      KHÔNG
          │         │
          ▼         ▼
     INTERFACE   Cả hai OK
     (multiple)  (ưu tiên Interface)

  💡 Khi phân vân → dùng Interface
  💡 Kết hợp: abstract class + interface = mạnh nhất
```

---

## 🏗️ Nguyên tắc thiết kế Hierarchy

```
┌────────────────────────────────────────────────┐
│ 1. Depth ≤ 3 tầng                              │
│ 2. Composition > Inheritance khi có thể        │
│ 3. Abstract class nên có CẢ abstract + concrete│
│ 4. Sealed override khi cần khóa behavior       │
│ 5. Protected abstract cho implementation detail│
│ 6. Public abstract cho public API              │
└────────────────────────────────────────────────┘
```

---

## ❌ Anti-patterns cần tránh

| Anti-pattern | Mô tả | Sửa |
|-------------|--------|-----|
| God Class | Abstract class quá nhiều abstract methods | Tách nhỏ + interfaces |
| Deep Inheritance | > 3 tầng kế thừa | Flatten + composition |
| Abstract Abuse | Chỉ có abstract methods | Dùng interface |
| Empty Abstract | Không có abstract member | Dùng class thường |

---

## 🏆 PHASE 2 COMPLETE — Tổng kết 10 bài học

```
╔═══════════════════════════════════════════════════════════╗
║                PHASE 2 — OOP FOUNDATION                   ║
║                    HOÀN THÀNH! 🎉                         ║
╠═══════════════════════════════════════════════════════════╣
║                                                           ║
║  📦 NỀN TẢNG (Bài 15-18)                                ║
║  ┌─────────────────────────────────────────────────────┐  ║
║  │ Bài 15: Class & Object                              │  ║
║  │   → Tạo class, tạo object, fields, methods          │  ║
║  │                                                     │  ║
║  │ Bài 16: Constructor                                 │  ║
║  │   → Default, parameterized, overloading, chaining   │  ║
║  │                                                     │  ║
║  │ Bài 17: Từ khóa this                               │  ║
║  │   → this.field, this(), method chaining, builder     │  ║
║  │                                                     │  ║
║  │ Bài 18: Access Modifier                             │  ║
║  │   → public, private, protected, internal            │  ║
║  └─────────────────────────────────────────────────────┘  ║
║                                                           ║
║  🏛️ 4 TRỤ CỘT OOP (Bài 19-24)                          ║
║  ┌─────────────────────────────────────────────────────┐  ║
║  │ Bài 19: 🔒 Encapsulation                           │  ║
║  │   → Private fields, Properties, get/set validation  │  ║
║  │                                                     │  ║
║  │ Bài 20: 🧬 Inheritance                             │  ║
║  │   → base class, derived class, base(), IS-A         │  ║
║  │                                                     │  ║
║  │ Bài 21: 🎭 Polymorphism                            │  ║
║  │   → virtual/override, runtime behavior, is/as       │  ║
║  │                                                     │  ║
║  │ Bài 22: 🎨 Abstraction                             │  ║
║  │   → abstract class cơ bản, interface cơ bản         │  ║
║  │                                                     │  ║
║  │ Bài 23: 📋 Interface                               │  ║
║  │   → Multiple interfaces, ISP, DI, default methods   │  ║
║  │                                                     │  ║
║  │ Bài 24: 🏗️ Abstract Class Nâng Cao                 │  ║
║  │   → Template Method, sealed override, hierarchy     │  ║
║  │   → Abstract properties, anti-patterns              │  ║
║  │   → Abstract + Interface kết hợp                    │  ║
║  └─────────────────────────────────────────────────────┘  ║
║                                                           ║
║  🔗 4 TRỤ CỘT HOẠT ĐỘNG CÙNG NHAU:                     ║
║  ┌─────────────────────────────────────────────────────┐  ║
║  │ Encapsulation → ẨN dữ liệu (private + properties)  │  ║
║  │ Inheritance   → CHIA SẺ code (base → derived)       │  ║
║  │ Polymorphism  → 1 type → N behaviors (override)     │  ║
║  │ Abstraction   → ẨN phức tạp (abstract + interface) │  ║
║  │                                                     │  ║
║  │ → Kết hợp = Kiến trúc phần mềm chuyên nghiệp! 🚀 │  ║
║  └─────────────────────────────────────────────────────┘  ║
╚═══════════════════════════════════════════════════════════╝
```

---

## ✅ Checklist — Tự đánh giá Bài 24

### Template Method Pattern
- [ ] Hiểu Template Method = skeleton + abstract steps
- [ ] Biết tạo template method (concrete) gọi abstract methods
- [ ] Biết dùng hook methods (virtual — override tùy chọn)
- [ ] Áp dụng cho Report, Pipeline, Game Loop

### Abstract Properties
- [ ] Biết khai báo `abstract string Type { get; }`
- [ ] Override abstract property bằng `=>` hoặc `{ get; set; }`
- [ ] Phân biệt abstract vs virtual vs regular property

### Protected Abstract & Sealed Override
- [ ] Biết khi nào dùng `protected abstract` (implementation detail)
- [ ] Biết khi nào dùng `public abstract` (public API)
- [ ] Hiểu `sealed override` — khóa override tiếp
- [ ] Biết sealed chỉ đi cùng override

### Abstract Class + Interface
- [ ] Biết abstract class implements interface
- [ ] Hiểu lợi ích: shared code + contract
- [ ] Pattern: `abstract class X : IY, IZ { }`

### Design Guidelines
- [ ] Hierarchy depth ≤ 3 tầng
- [ ] Biết tránh God class, deep inheritance
- [ ] Biết khi nào abstract class vs interface (Decision Tree)
- [ ] Ưu tiên Composition over Inheritance

### Bài tập
- [ ] Hoàn thành 5/5 bài tập
- [ ] Hoàn thành Challenge: Game Engine Foundation
- [ ] Code có abstract + interface + sealed kết hợp

---

## 🔑 Từ khóa quan trọng Bài 24

| Từ khóa | Ý nghĩa | Ví dụ |
|---------|---------|-------|
| `abstract` property | Property không có body | `abstract string Type { get; }` |
| `protected abstract` | Abstract chỉ thấy nội bộ | `protected abstract void Step();` |
| `sealed override` | Override + khóa | `sealed override void M() { }` |
| Template Method | Skeleton pattern | Cha define flow, con fill steps |
| Hook Method | Virtual method tùy chọn | `protected virtual void OnDone() { }` |

---

## 🚀 What's Next — Phase 3

```
╔═══════════════════════════════════════════════════════╗
║  PHASE 3 — C# Nâng Cao (Coming up!)                  ║
║                                                       ║
║  Sau khi master OOP Foundation, bạn sẽ học:          ║
║                                                       ║
║  📦 Generics         → List<T>, Dictionary<K,V>      ║
║  📋 Collections      → Array, List, Dictionary       ║
║  🔗 Delegates        → Callback, event handling      ║
║  ⚡ Events           → Publisher/Subscriber          ║
║  🔄 LINQ             → Query data elegantly          ║
║  ⚠️ Exception        → Try/catch, custom exceptions  ║
║  📁 File I/O         → Đọc/ghi file                  ║
║  🧵 Async/Await      → Lập trình bất đồng bộ        ║
║                                                       ║
║  → Phase 3 xây trên nền tảng OOP vững chắc! 🏗️     ║
╚═══════════════════════════════════════════════════════╝
```

---

## 💡 Mẹo ghi nhớ

```
🏗️ ABSTRACT CLASS = Bản thiết kế tòa nhà
   Có sẵn: nền móng, khung sắt (shared code)
   Để trống: nội thất, trang trí (abstract methods)
   Mỗi căn hộ tự hoàn thiện (subclass implement)

🔒 SEALED = Niêm phong
   "Method này đã hoàn thiện, con cháu không được đổi!"

📋 TEMPLATE METHOD = Quy trình sản xuất
   Nhà máy có quy trình cố định: Cắt → Lắp → Kiểm → Đóng gói
   Mỗi dây chuyền cắt/lắp khác nhau, nhưng QUY TRÌNH giống nhau

🤝 ABSTRACT + INTERFACE = Hôn nhân
   Abstract class: Gen di truyền (IS-A)
   Interface: Kỹ năng học được (CAN-DO)
   Kết hợp: Vừa thừa hưởng, vừa học thêm!
```

> **🏆 Chúc mừng! Bạn đã hoàn thành PHASE 2 — OOP Foundation!**
> Từ đây, bạn có đủ kiến thức để thiết kế hệ thống phần mềm theo chuẩn OOP chuyên nghiệp. 🚀

---

*"Mastery = biết MỌI công cụ + biết CHỌN đúng công cụ cho đúng vấn đề."* 🎯
