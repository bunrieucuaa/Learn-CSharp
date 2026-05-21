# 📘 Bài 24: Abstract Class Nâng Cao — Kiến Trúc & Design Patterns

> **"Abstract class không chỉ là 'class không thể new' — nó là công cụ thiết kế kiến trúc phần mềm."**

---

## 📋 Mục lục

1. [Nhắc lại: Abstract Class là gì?](#1-nhắc-lại-abstract-class-là-gì)
2. [Template Method Pattern](#2-template-method-pattern)
3. [Abstract Class as Base Layer](#3-abstract-class-as-base-layer)
4. [Kết hợp Abstract Class + Interface](#4-kết-hợp-abstract-class--interface)
5. [Decision Tree: Abstract Class vs Interface](#5-decision-tree-abstract-class-vs-interface)
6. [Abstract Properties](#6-abstract-properties)
7. [Protected Abstract vs Public Abstract](#7-protected-abstract-vs-public-abstract)
8. [Sealed Override — Chặn override tiếp](#8-sealed-override--chặn-override-tiếp)
9. [Thiết kế Class Hierarchies](#9-thiết-kế-class-hierarchies)
10. [Anti-patterns: Những lỗi thiết kế cần tránh](#10-anti-patterns-những-lỗi-thiết-kế-cần-tránh)
11. [Real-world Architecture](#11-real-world-architecture)
12. [Phase 2 Tổng kết — 4 Tính chất OOP](#12-phase-2-tổng-kết--4-tính-chất-oop)
13. [So sánh C# vs JavaScript](#13-so-sánh-c-vs-javascript)
14. [Common Mistakes & Best Practices](#14-common-mistakes--best-practices)

---

## 1. Nhắc lại: Abstract Class là gì?

> 📌 Bài 22 đã giới thiệu cơ bản. Ở đây chỉ nhắc nhanh rồi đi sâu hơn.

```
┌─────────────────────────────────────────────────────────┐
│             ABSTRACT CLASS = BẢN THIẾT KẾ               │
│                                                         │
│   ┌──────────────────────┐  ┌────────────────────────┐  │
│   │   Code đã viết sẵn   │  │  Chỗ trống phải điền   │  │
│   │   (concrete methods) │  │  (abstract methods)     │  │
│   │                      │  │                         │  │
│   │ • Constructor         │  │ • abstract void X();   │  │
│   │ • Fields, Properties │  │ • abstract int Y();    │  │
│   │ • Helper methods     │  │ • abstract string Z(); │  │
│   └──────────────────────┘  └────────────────────────┘  │
│                                                         │
│   ❌ Không thể new trực tiếp                            │
│   ✅ Class con kế thừa + điền vào chỗ trống             │
└─────────────────────────────────────────────────────────┘
```

**Bài 24 sẽ trả lời:** Abstract class dùng **như thế nào** trong kiến trúc thực tế?

---

## 2. Template Method Pattern

### 📖 Định nghĩa

> **Template Method** = Abstract class định nghĩa **skeleton** (bộ khung) của một thuật toán. Các bước cụ thể được để cho subclass tự implement.

```
┌─────────────────────────────────────────────────────┐
│        TEMPLATE METHOD PATTERN                       │
│                                                     │
│   Abstract Class (cha):                             │
│   ┌─────────────────────────────────────┐           │
│   │ public void Execute()  ← TEMPLATE  │           │
│   │ {                                   │           │
│   │     Step1();  ← abstract            │           │
│   │     Step2();  ← abstract            │           │
│   │     Step3();  ← concrete (có sẵn)   │           │
│   │     Step4();  ← abstract            │           │
│   │ }                                   │           │
│   └─────────────────────────────────────┘           │
│                                                     │
│   Subclass A:           Subclass B:                 │
│   Step1() → cách A      Step1() → cách B           │
│   Step2() → cách A      Step2() → cách B           │
│   Step4() → cách A      Step4() → cách B           │
│                                                     │
│   → Cùng 1 skeleton, khác cách thực hiện từng step! │
└─────────────────────────────────────────────────────┘
```

### 🔍 Cú pháp

```csharp
abstract class ReportGenerator
{
    // ═══ TEMPLATE METHOD — gọi các bước theo thứ tự ═══
    public void GenerateReport()    // Không phải abstract!
    {
        PrepareHeader();            // Step 1 — abstract
        BuildBody();                // Step 2 — abstract
        AddFooter();                // Step 3 — abstract
        Console.WriteLine("✅ Report hoàn thành!");  // Step 4 — concrete
    }

    // Các "step" trừu tượng — subclass PHẢI implement
    protected abstract void PrepareHeader();
    protected abstract void BuildBody();
    protected abstract void AddFooter();
}

class HtmlReport : ReportGenerator
{
    protected override void PrepareHeader()
        => Console.WriteLine("<html><head>Report</head>");

    protected override void BuildBody()
        => Console.WriteLine("<body><h1>Nội dung</h1></body>");

    protected override void AddFooter()
        => Console.WriteLine("<footer>© 2025</footer></html>");
}
```

### 🧠 Tại sao dùng Template Method?

```
Ưu điểm:
┌──────────────────────────────────────────────────────┐
│ 1. DRY — Không lặp lại logic điều khiển             │
│ 2. Open/Closed — Thêm loại mới không sửa code cũ    │
│ 3. Consistent — Đảm bảo tất cả report cùng flow    │
│ 4. Inversion of Control — Cha gọi con, không ngược  │
└──────────────────────────────────────────────────────┘

Ví dụ thực tế:
  • Report Generator: HTML, PDF, CSV, Excel
  • Game Loop: Init → Update → Render → Cleanup
  • Data Pipeline: Extract → Transform → Load (ETL)
  • Test Framework: Setup → Test → Teardown
```

---

## 3. Abstract Class as Base Layer

### 📖 Shared logic + Forced implementation

```
┌─────────────────────────────────────────────────┐
│        ABSTRACT CLASS AS BASE LAYER              │
│                                                 │
│   abstract class DataService                     │
│   ┌─────────────────────────────────────┐       │
│   │  SHARED (concrete):                │       │
│   │  • Logging                         │       │
│   │  • Error handling                  │       │
│   │  • Validation                      │       │
│   │  • Connection management           │       │
│   │                                    │       │
│   │  FORCED (abstract):               │       │
│   │  • abstract void SaveData()        │       │
│   │  • abstract Data LoadData()        │       │
│   │  • abstract bool ValidateData()    │       │
│   └─────────────────────────────────────┘       │
│           ▲               ▲                     │
│           │               │                     │
│   ┌───────┴───┐   ┌──────┴────┐                │
│   │ SqlService│   │MongoService│                │
│   │ SaveData()│   │ SaveData() │                │
│   │ LoadData()│   │ LoadData() │                │
│   └───────────┘   └───────────┘                │
└─────────────────────────────────────────────────┘
```

```csharp
abstract class DataService
{
    // ═══ SHARED LOGIC — tất cả subclass dùng chung ═══
    protected void Log(string message)
        => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");

    protected bool ValidateNotEmpty(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Log("❌ Data rỗng!");
            return false;
        }
        return true;
    }

    // Template method kết hợp shared + abstract
    public void Save(string data)
    {
        Log("Bắt đầu lưu...");
        if (ValidateNotEmpty(data))
        {
            SaveData(data);          // Abstract — subclass quyết định
            Log("✅ Lưu thành công!");
        }
    }

    // ═══ FORCED — subclass PHẢI implement ═══
    protected abstract void SaveData(string data);
    public abstract string LoadData(int id);
}
```

---

## 4. Kết hợp Abstract Class + Interface

### 📖 Abstract class implements Interface

> **Pattern mạnh nhất:** Abstract class cung cấp shared code + interface đảm bảo contract.

```
┌─────────────────────────────────────────────────────┐
│  ABSTRACT CLASS + INTERFACE = SỨC MẠNH KẾT HỢP     │
│                                                     │
│   IAttackable     IDefendable     (Interfaces)      │
│       │               │                             │
│       └───────┬───────┘                             │
│               ▼                                     │
│   abstract GameCharacter  ← implements cả 2         │
│   ┌────────────────────────────────┐                │
│   │ Shared: Name, HP, Level        │                │
│   │ Shared: TakeDamage(), Heal()   │                │
│   │ Abstract: Attack(), Defend()   │                │
│   └────────────────────────────────┘                │
│           ▲               ▲                         │
│   ┌───────┴───┐   ┌──────┴────┐                    │
│   │  Warrior  │   │   Mage    │                    │
│   │ Attack:⚔️ │   │ Attack:🔥│                    │
│   │ Defend:🛡️ │   │ Defend:🧊│                    │
│   └───────────┘   └───────────┘                    │
└─────────────────────────────────────────────────────┘
```

```csharp
// Interfaces — định nghĩa contract
interface IAttackable
{
    int AttackPower { get; }
    void Attack(GameCharacter target);
}

interface IDefendable
{
    int Defense { get; }
    int Defend(int incomingDamage);
}

// Abstract class IMPLEMENTS interfaces
abstract class GameCharacter : IAttackable, IDefendable
{
    public string Name { get; set; }
    public int Hp { get; protected set; }

    // Từ IAttackable — để abstract cho subclass
    public abstract int AttackPower { get; }
    public abstract void Attack(GameCharacter target);

    // Từ IDefendable — implement sẵn 1 phần
    public abstract int Defense { get; }
    public virtual int Defend(int incomingDamage)
    {
        int actualDamage = Math.Max(0, incomingDamage - Defense);
        Hp -= actualDamage;
        return actualDamage;
    }

    // Shared logic
    public void Heal(int amount) => Hp += amount;
}
```

### 🎯 Lợi ích

```
┌───────────────────────────────────────────────────┐
│ Abstract class + Interface kết hợp:                │
│                                                   │
│ ✅ Interface: đảm bảo contract rõ ràng            │
│ ✅ Abstract class: chia sẻ code chung             │
│ ✅ Subclass: chỉ cần implement phần khác biệt    │
│ ✅ Polymorphism: dùng interface HOẶC abstract type│
│                                                   │
│ Trong thực tế:                                    │
│   • ASP.NET: ControllerBase implements IDisposable│
│   • EF Core: DbContext implements IDisposable     │
│   • Collections: List<T> : IList<T>, IEnumerable  │
└───────────────────────────────────────────────────┘
```

---

## 5. Decision Tree: Abstract Class vs Interface

### 📊 Bảng so sánh chi tiết (nâng cao)

| Tiêu chí | Abstract Class | Interface |
|-----------|---------------|-----------|
| **Quan hệ** | IS-A (Chó **là** Động vật) | CAN-DO (Chó **có thể** bơi) |
| **Shared code** | ✅ Có concrete methods | ⚠️ Chỉ default methods (C# 8+) |
| **State (fields)** | ✅ Có fields | ❌ Không |
| **Constructor** | ✅ Có | ❌ Không |
| **Đa kế thừa** | ❌ Chỉ 1 class cha | ✅ Nhiều interfaces |
| **Versioning** | ✅ Thêm method dễ | ⚠️ Phá vỡ implementations |
| **Testing** | ⚠️ Khó mock | ✅ Dễ mock |
| **Coupling** | Chặt hơn | Lỏng hơn |

### 🧠 Decision Tree chi tiết

```
                    Cần abstraction?
                         │
              ┌──────────┴──────────┐
              ▼                     ▼
     Có shared code?          Chỉ cần contract?
         │                         │
    ┌────┴────┐                    ▼
    ▼         ▼              INTERFACE ✅
   CÓ       KHÔNG
    │         │
    ▼         ▼
  Có quan   INTERFACE ✅
  hệ IS-A?
    │
  ┌─┴──┐
  ▼    ▼
 CÓ  KHÔNG
  │    │
  ▼    ▼
ABSTRACT   Cần kết hợp
CLASS ✅   nhiều "khả năng"?
              │
         ┌────┴────┐
         ▼         ▼
        CÓ       KHÔNG
         │         │
         ▼         ▼
    INTERFACE   Cả hai đều OK
    (multiple)  (ưu tiên Interface)

  💡 GOLDEN RULE: Khi phân vân → dùng Interface
     Chỉ dùng Abstract Class khi THỰC SỰ cần shared code
```

### 📋 Quy tắc nhanh

```
┌──────────────────────────────────────────────────────┐
│ Dùng ABSTRACT CLASS khi:                              │
│   → Class con cùng "họ" (Animal → Dog, Cat)          │
│   → Cần chia sẻ fields, constructor, code chung       │
│   → Template Method pattern                           │
│   → Cần protected members                             │
│                                                      │
│ Dùng INTERFACE khi:                                   │
│   → Các class KHÔNG cùng "họ" nhưng cùng behavior    │
│   → 1 class cần nhiều capabilities                    │
│   → Dependency Injection / Testing                    │
│   → Định nghĩa contract cho API                      │
│                                                      │
│ KẾT HỢP khi:                                         │
│   → abstract class Vehicle : ITrackable, IFuelable   │
│   → Vừa IS-A, vừa CAN-DO                            │
└──────────────────────────────────────────────────────┘
```

---

## 6. Abstract Properties

### 📖 Cú pháp

```csharp
abstract class Employee
{
    // Abstract property — subclass PHẢI implement
    public abstract string EmployeeType { get; }
    public abstract decimal BaseSalary { get; set; }

    // Abstract read-only property
    public abstract int MaxVacationDays { get; }

    // Concrete property — dùng luôn
    public string Name { get; set; }

    // Concrete method sử dụng abstract property
    public void PrintBadge()
    {
        Console.WriteLine($"[{EmployeeType}] {Name}");
        Console.WriteLine($"Lương cơ bản: {BaseSalary:N0}đ");
        Console.WriteLine($"Ngày phép tối đa: {MaxVacationDays}");
    }
}

class FullTimeEmployee : Employee
{
    // Override abstract properties
    public override string EmployeeType => "Chính thức";
    public override decimal BaseSalary { get; set; }
    public override int MaxVacationDays => 12;

    public FullTimeEmployee(string name, decimal salary)
    {
        Name = name;
        BaseSalary = salary;
    }
}

class Intern : Employee
{
    public override string EmployeeType => "Thực tập";
    public override decimal BaseSalary { get; set; }
    public override int MaxVacationDays => 3;

    public Intern(string name)
    {
        Name = name;
        BaseSalary = 4_000_000;
    }
}
```

### 📊 Abstract property vs Virtual property vs Regular property

```
┌──────────────────┬───────────────────┬──────────────────┐
│ Abstract Property│ Virtual Property  │ Regular Property │
├──────────────────┼───────────────────┼──────────────────┤
│ Không có body    │ Có body sẵn       │ Có body sẵn      │
│ PHẢI override    │ CÓ THỂ override   │ Không override   │
│ Chỉ trong        │ Bất kỳ class     │ Bất kỳ class     │
│ abstract class   │                   │                  │
├──────────────────┼───────────────────┼──────────────────┤
│ abstract string  │ virtual string    │ string Name      │
│ Type { get; }    │ Type => "Base";   │ { get; set; }    │
└──────────────────┴───────────────────┴──────────────────┘
```

---

## 7. Protected Abstract vs Public Abstract

### 📖 Khi nào dùng protected abstract?

```csharp
abstract class OrderProcessor
{
    // PUBLIC abstract — bên ngoài THẤY và GỌI được
    public abstract string GetOrderType();

    // PROTECTED abstract — chỉ bên trong hierarchy THẤY
    // Dùng trong Template Method — bên ngoài KHÔNG gọi trực tiếp
    protected abstract decimal CalculateDiscount(decimal total);
    protected abstract bool ValidateOrder();

    // Template method — PUBLIC, gọi protected abstract bên trong
    public decimal ProcessOrder(decimal total)
    {
        Console.WriteLine($"Xử lý đơn hàng: {GetOrderType()}");

        if (!ValidateOrder())    // protected abstract
        {
            Console.WriteLine("❌ Đơn hàng không hợp lệ!");
            return 0;
        }

        decimal discount = CalculateDiscount(total);  // protected abstract
        decimal finalTotal = total - discount;
        Console.WriteLine($"✅ Tổng: {finalTotal:N0}đ (giảm {discount:N0}đ)");
        return finalTotal;
    }
}
```

### 📊 So sánh

```
┌─────────────────────────────────────────────────────┐
│ public abstract void Method();                       │
│ → Bên ngoài THẤY signature trong API                │
│ → Subclass implement → bên ngoài gọi trực tiếp     │
│ → Dùng khi: method là PART OF PUBLIC API            │
│                                                     │
│ protected abstract void Method();                    │
│ → Bên ngoài KHÔNG THẤY                              │
│ → Chỉ dùng BÊN TRONG class hierarchy               │
│ → Dùng khi: method là IMPLEMENTATION DETAIL          │
│             (thường trong Template Method)            │
└─────────────────────────────────────────────────────┘

Ví dụ:
  abstract class Report
  {
      // public — bên ngoài gọi
      public void Generate() { ... }           // Template

      // protected — chỉ dùng nội bộ
      protected abstract void BuildHeader();   // Step
      protected abstract void BuildBody();     // Step
  }

  Report r = new HtmlReport();
  r.Generate();        // ✅ OK — public
  // r.BuildHeader();  // ❌ Lỗi — protected!
```

---

## 8. Sealed Override — Chặn override tiếp

### 📖 Vấn đề

```
Khi class A → B → C → D (chuỗi kế thừa):
  A: abstract void Process();
  B: override void Process() { ... }     ← implement
  C: override void Process() { ... }     ← override lại!
  D: override void Process() { ... }     ← override lại nữa!

→ Quá nhiều tầng override → khó kiểm soát, dễ bug!
```

### 🔒 Giải pháp: sealed override

```csharp
abstract class Animal
{
    public abstract void MakeSound();
}

class Dog : Animal
{
    // sealed override = override + KHÓA, con của Dog KHÔNG được override tiếp
    public sealed override void MakeSound()
    {
        Console.WriteLine("Gâu gâu!");
    }
}

class GoldenRetriever : Dog
{
    // ❌ LỖI COMPILE! MakeSound() đã bị sealed ở Dog!
    // public override void MakeSound() { }
}
```

### 📊 Minh họa

```
                abstract Animal
                abstract MakeSound()
                       │
              ┌────────┴────────┐
              ▼                 ▼
           Dog                Cat
    sealed override          override
    MakeSound()             MakeSound()
         │                      │
    ┌────┴─────┐          ┌────┴────┐
    ▼          ▼          ▼         ▼
  Golden    Husky     Persian   Siamese
  Retriever                   override
  ❌ Cannot               MakeSound()
  override!                   ✅ OK
```

### 🎯 Khi nào dùng sealed override?

```
┌──────────────────────────────────────────────────────┐
│ Dùng sealed override khi:                             │
│                                                      │
│ 1. Method đã implement CHÍNH XÁC, không nên thay đổi│
│    Ví dụ: Thuật toán đã tối ưu, logic business cố định│
│                                                      │
│ 2. Bảo vệ invariant (tính bất biến)                  │
│    Ví dụ: Validation logic phải luôn giống nhau       │
│                                                      │
│ 3. Security — ngăn subclass bypass logic quan trọng   │
│    Ví dụ: Authentication check                        │
│                                                      │
│ 4. Performance — compiler optimize sealed methods     │
└──────────────────────────────────────────────────────┘
```

---

## 9. Thiết kế Class Hierarchies

### 📖 Guidelines

```
┌──────────────────────────────────────────────────────┐
│        NGUYÊN TẮC THIẾT KẾ CLASS HIERARCHY            │
│                                                      │
│ 1. 🎯 Giữ depth ≤ 3 tầng                            │
│    Abstract → Concrete (tốt nhất)                    │
│    Abstract → Abstract → Concrete (chấp nhận)        │
│    Abstract → Abstract → Abstract → ... (quá sâu! ❌)│
│                                                      │
│ 2. 🧩 Ưu tiên Composition over Inheritance          │
│    Thay vì: class FlyingSwimmingDog : Dog            │
│    Dùng:    class Dog { IFlyable fly; ISwimmable sw; }│
│                                                      │
│ 3. 📐 Single Responsibility                          │
│    Mỗi abstract class chỉ NÊN abstract hóa 1 khía  │
│    cạnh. Quá nhiều abstract methods → tách ra.       │
│                                                      │
│ 4. 🔄 Liskov Substitution Principle                  │
│    Mọi subclass phải thay thế được cho base class    │
│    mà không gây lỗi logic.                           │
│                                                      │
│ 5. 🏗️ Stable base, flexible leaves                   │
│    Abstract class (gốc) ít thay đổi                  │
│    Concrete class (lá) dễ thay đổi / thêm mới       │
└──────────────────────────────────────────────────────┘
```

### 📊 Ví dụ hierarchy tốt vs xấu

```
✅ HIERARCHY TỐT — Nông, rõ ràng:

  abstract Shape                    Level 0
  ├── Circle                        Level 1
  ├── Rectangle                     Level 1
  └── Triangle                      Level 1

  + IDrawable, IResizable (interfaces cho capabilities)


❌ HIERARCHY XẤU — Quá sâu, quá phức tạp:

  abstract LivingThing              Level 0
  └── abstract Animal               Level 1
      └── abstract Mammal           Level 2
          └── abstract Canine       Level 3
              └── abstract DogBreed Level 4
                  └── GoldenRetriever  Level 5  ← quá sâu!

  → Thay đổi LivingThing ảnh hưởng 5 tầng bên dưới!
```

---

## 10. Anti-patterns: Những lỗi thiết kế cần tránh

### ❌ Anti-pattern 1: God Abstract Class

```csharp
// ❌ SAI — abstract class làm quá nhiều việc!
abstract class BaseEntity
{
    public abstract void Save();
    public abstract void Delete();
    public abstract void Validate();
    public abstract void Log();
    public abstract void SendEmail();
    public abstract void GenerateReport();
    public abstract void ExportToPdf();
    public abstract void BackupData();
    // ... 20 abstract methods nữa
}
// → Mọi subclass PHẢI implement TẤT CẢ → rất nặng nề!

// ✅ ĐÚNG — Tách nhỏ bằng interfaces
interface ISaveable { void Save(); void Delete(); }
interface IValidatable { bool Validate(); }
interface IExportable { void ExportToPdf(); }

abstract class BaseEntity : ISaveable, IValidatable
{
    public abstract void Save();
    public abstract void Delete();
    public abstract bool Validate();
    // Chỉ giữ lại phần CỐT LÕI
}
```

### ❌ Anti-pattern 2: Deep Inheritance (> 3 levels)

```csharp
// ❌ SAI — quá nhiều tầng kế thừa
abstract class A { }
abstract class B : A { }
abstract class C : B { }
abstract class D : C { }
class E : D { }    // Level 5 → khó hiểu, khó debug!

// ✅ ĐÚNG — Flatter hierarchy + interfaces
abstract class Base { }
class ConcreteA : Base, IFeatureX, IFeatureY { }
class ConcreteB : Base, IFeatureY, IFeatureZ { }
```

### ❌ Anti-pattern 3: Abstract Class Abuse

```csharp
// ❌ SAI — Abstract class không có abstract method
abstract class Config
{
    public string ConnectionString { get; set; }
    public int Timeout { get; set; }
    public void Save() { /* ... */ }
    // Không có gì abstract → Tại sao lại abstract?
}
// → Đây chỉ là class thường, không cần abstract!

// ❌ SAI — Abstract class chỉ có abstract methods
abstract class ICanFly   // Đặt tên giống interface
{
    public abstract void Fly();
    public abstract int Altitude { get; }
    // Toàn abstract, không shared code → Dùng interface!
}

// ✅ ĐÚNG — Dùng interface nếu chỉ cần contract
interface ICanFly
{
    void Fly();
    int Altitude { get; }
}
```

---

## 11. Real-world Architecture

### 🏗️ ASP.NET Controller Base Class

```csharp
// Trong ASP.NET MVC — ControllerBase là abstract class!
// Microsoft thiết kế:

abstract class ControllerBase : IDisposable  // abstract + interface
{
    // Shared (concrete):
    public HttpContext HttpContext { get; }
    public ModelStateDictionary ModelState { get; }
    public ViewResult View() { /* shared logic */ }
    public JsonResult Json(object data) { /* shared */ }
    public RedirectResult Redirect(string url) { /* shared */ }

    // Bạn kế thừa và thêm actions:
}

class ProductController : ControllerBase
{
    public ViewResult Index() => View();       // Dùng shared View()
    public JsonResult GetAll() => Json(products); // Dùng shared Json()
}
```

### 🏗️ Entity Framework DbContext

```
DbContext (abstract-like base class)
┌──────────────────────────────────────┐
│ Shared:                              │
│ • SaveChanges()                      │
│ • Connection management              │
│ • Change tracking                    │
│ • Migration support                  │
│                                      │
│ Bạn override:                        │
│ • OnModelCreating() ← virtual       │
│ • DbSet<T> properties               │
└──────────────────────────────────────┘
        ▲
        │
┌───────┴──────────────────────────────┐
│ class AppDbContext : DbContext        │
│ {                                    │
│     DbSet<Product> Products { get; } │
│     DbSet<Order> Orders { get; }     │
│                                      │
│     override OnModelCreating() { }   │
│ }                                    │
└──────────────────────────────────────┘
```

---

## 12. Phase 2 Tổng kết — 4 Tính chất OOP

### 🏆 Nhìn lại hành trình Phase 2

```
┌═══════════════════════════════════════════════════════┐
║              PHASE 2 — OOP FOUNDATION                 ║
║                                                       ║
║  Bài 15-16-17-18: NỀN TẢNG                          ║
║  ┌─────────────────────────────────────────┐          ║
║  │ Class & Object → Constructor → this →   │          ║
║  │ Access Modifier                         │          ║
║  │ → Biết TẠO objects, quản lý state      │          ║
║  └─────────────────────────────────────────┘          ║
║                                                       ║
║  Bài 19-22-23-24: 4 TRỤ CỘT OOP                    ║
║  ┌─────────────────────────────────────────┐          ║
║  │ 🔒 Encapsulation  │ 🧬 Inheritance      │          ║
║  │ (Bài 19)          │ (Bài 20)            │          ║
║  │ Đóng gói dữ liệu │ Kế thừa & tái sử   │          ║
║  │ private + get/set │ dụng code           │          ║
║  ├────────────────────┼─────────────────────┤          ║
║  │ 🎭 Polymorphism   │ 🎨 Abstraction      │          ║
║  │ (Bài 21)          │ (Bài 22-23-24)      │          ║
║  │ 1 type, N behavior│ Ẩn phức tạp,        │          ║
║  │ virtual/override  │ abstract + interface│          ║
║  └─────────────────────────────────────────┘          ║
║                                                       ║
║  → 4 trụ cột PHỐI HỢP = Kiến trúc phần mềm vững!   ║
╚═══════════════════════════════════════════════════════╝
```

### 🔗 4 tính chất hoạt động CÙNG NHAU

```csharp
// Encapsulation: private fields + public interface
abstract class BankAccount    // Abstraction: abstract class
{
    private decimal balance;                    // Encapsulation
    public string Owner { get; private set; }   // Encapsulation

    protected BankAccount(string owner, decimal initial)
    {
        Owner = owner;
        balance = initial;
    }

    public decimal GetBalance() => balance;     // Encapsulation

    // Abstraction: abstract method
    public abstract decimal GetInterestRate();

    // Polymorphism: virtual method
    public virtual void Deposit(decimal amount)
    {
        balance += amount;
    }
}

// Inheritance: kế thừa từ BankAccount
class SavingsAccount : BankAccount    // Inheritance
{
    public SavingsAccount(string owner, decimal initial)
        : base(owner, initial) { }

    public override decimal GetInterestRate() => 0.05m;   // Polymorphism

    public override void Deposit(decimal amount)          // Polymorphism
    {
        base.Deposit(amount);
        base.Deposit(amount * GetInterestRate());  // Thêm lãi
    }
}
```

---

## 13. So sánh C# vs JavaScript

### 📊 Template Method

```csharp
// ═══ C# — Template Method rõ ràng ═══
abstract class DataProcessor
{
    public void Process()               // Template
    {
        var data = ReadData();          // abstract
        var result = TransformData(data); // abstract
        SaveResult(result);             // concrete
    }

    protected abstract string ReadData();
    protected abstract string TransformData(string data);

    protected void SaveResult(string result)
        => Console.WriteLine($"Saved: {result}");
}

class CsvProcessor : DataProcessor
{
    protected override string ReadData() => "csv_data";
    protected override string TransformData(string data)
        => data.ToUpper();
}
```

```javascript
// ═══ JavaScript — Simulate Template Method ═══
class DataProcessor {
    process() {                         // "Template"
        const data = this.readData();   // No enforcement!
        const result = this.transformData(data);
        this.saveResult(result);
    }

    readData() {
        throw new Error("Must implement readData()!");
        // ❌ Chỉ báo lỗi RUNTIME, không phải compile-time
    }

    transformData(data) {
        throw new Error("Must implement!");
    }

    saveResult(result) {
        console.log(`Saved: ${result}`);
    }
}

class CsvProcessor extends DataProcessor {
    readData() { return "csv_data"; }
    transformData(data) { return data.toUpperCase(); }
}
```

### 📊 Sealed override

```csharp
// ═══ C# — sealed override ═══
class Dog : Animal
{
    public sealed override void MakeSound()  // Khóa!
    {
        Console.WriteLine("Gâu!");
    }
}

class Puppy : Dog
{
    // ❌ Lỗi compile — MakeSound đã sealed!
    // public override void MakeSound() { }
}
```

```javascript
// ═══ JavaScript — KHÔNG CÓ sealed! ═══
class Dog extends Animal {
    makeSound() {
        console.log("Gâu!");
    }
}

class Puppy extends Dog {
    makeSound() {              // ✅ JS cho override thoải mái
        console.log("Ẳng!");   // Không có cách nào chặn!
    }
}
// → JS KHÔNG có cơ chế chặn override
```

### 📊 Bảng so sánh tổng hợp

| Tính năng | C# | JavaScript |
|-----------|-----|-----------|
| Abstract class | ✅ `abstract class` | ❌ Simulate (throw Error) |
| Abstract property | ✅ `abstract string X { get; }` | ❌ Không có |
| Sealed override | ✅ `sealed override` | ❌ Không có |
| Protected abstract | ✅ Có | ❌ `#private` chỉ có ES2022 |
| Template Method | ✅ Compile-time safe | ⚠️ Runtime only |
| Multi-level abstract | ✅ abstract → abstract → concrete | ⚠️ Simulate |
| Enforcement | Compile-time | Runtime (throw) |

---

## 14. Common Mistakes & Best Practices

### ❌ Mistake 1: Abstract class không có abstract member

```csharp
// ❌ Vô nghĩa — abstract class nhưng không abstract gì?
abstract class Config
{
    public string Name { get; set; }
    public void Save() { /* ... */ }
    // → Nên là class thường!
}

// ✅ Có abstract member → có lý do abstract
abstract class Config
{
    public string Name { get; set; }
    public abstract string GetConfigPath();  // Subclass phải define
    public void Save() { /* dùng GetConfigPath() */ }
}
```

### ❌ Mistake 2: Quên gọi base constructor

```csharp
abstract class Vehicle
{
    public string Brand { get; set; }
    protected Vehicle(string brand) { Brand = brand; }
}

class Car : Vehicle
{
    // ❌ Quên gọi base constructor → Lỗi compile!
    // public Car() { }

    // ✅ Phải gọi base()
    public Car(string brand) : base(brand) { }
}
```

### ❌ Mistake 3: Sealed sai chỗ

```csharp
// ❌ Sealed trên method chưa override → vô nghĩa!
class Dog
{
    public sealed void Bark() { }  // ❌ Lỗi! Chỉ sealed + override
}

// ✅ Sealed phải đi với override
class Dog : Animal
{
    public sealed override void MakeSound() { }  // ✅ OK
}
```

### ✅ Best Practices tổng hợp

```
┌──────────────────────────────────────────────────────┐
│            BEST PRACTICES — ABSTRACT CLASS             │
│                                                      │
│ 1. 🎯 Abstract class nên có CẢ abstract + concrete  │
│    (Nếu chỉ abstract → dùng interface)              │
│    (Nếu chỉ concrete → dùng class thường)           │
│                                                      │
│ 2. 🛡️ Dùng protected abstract cho implementation     │
│    details, public abstract cho public API           │
│                                                      │
│ 3. 📐 Hierarchy depth ≤ 3 tầng                       │
│                                                      │
│ 4. 🔒 Dùng sealed override khi subclass KHÔNG ĐƯỢC  │
│    thay đổi behavior (security, invariant)           │
│                                                      │
│ 5. 🧩 Kết hợp abstract class + interface             │
│    abstract class Vehicle : ITrackable, IFuelable    │
│                                                      │
│ 6. 📋 Abstract property cho metadata khác biệt       │
│    abstract string Type { get; }                     │
│                                                      │
│ 7. 🔄 Template Method cho consistent workflow        │
│    Cha define skeleton, con fill in steps            │
│                                                      │
│ 8. ⚖️ Composition > Inheritance khi có thể            │
└──────────────────────────────────────────────────────┘
```

---

*"Abstract class là bản thiết kế kiến trúc — vừa đủ cứng để giữ cấu trúc, vừa đủ mềm để mở rộng."* 🏗️
