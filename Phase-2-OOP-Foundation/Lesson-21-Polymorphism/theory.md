# 📘 Bài 21: Polymorphism — Tính Đa Hình

> **"Một giao diện, nhiều hình dạng — đó là sức mạnh thực sự của OOP."**

---

## 📋 Mục lục

1. [Polymorphism là gì?](#1-polymorphism-là-gì)
2. [Runtime Polymorphism (Method Overriding)](#2-runtime-polymorphism-method-overriding)
3. [Compile-time Polymorphism (Method Overloading)](#3-compile-time-polymorphism-method-overloading)
4. [Biến kiểu CHA chứa object CON](#4-biến-kiểu-cha-chứa-object-con)
5. [Polymorphism với mảng](#5-polymorphism-với-mảng)
6. [Từ khóa `is` — Type Checking](#6-từ-khóa-is--type-checking)
7. [Từ khóa `as` — Safe Casting](#7-từ-khóa-as--safe-casting)
8. [Pattern Matching](#8-pattern-matching)
9. [Tại sao cần Polymorphism?](#9-tại-sao-cần-polymorphism)
10. [Real-world Analogy: Cổng USB](#10-real-world-analogy-cổng-usb)
11. [So sánh C# vs JavaScript](#11-so-sánh-c-vs-javascript)
12. [Sai lầm thường gặp & Best Practices](#12-sai-lầm-thường-gặp--best-practices)

---

## 1. Polymorphism là gì?

### 📖 Định nghĩa

**Polymorphism** (Tính đa hình) xuất phát từ tiếng Hy Lạp:
- **Poly** = nhiều
- **Morph** = hình dạng

> 💡 **Polymorphism = 1 method/interface, nhiều cách thực thi (implementation) khác nhau.**

### 🎯 Ý tưởng cốt lõi

```
┌─────────────────────────────────────────────────────┐
│              POLYMORPHISM                            │
│                                                     │
│   Cùng 1 lời gọi method  →  Kết quả KHÁC NHAU     │
│   tùy thuộc vào OBJECT thực sự đang chạy           │
│                                                     │
│   animal.Speak();                                   │
│     ├── Dog  → "Gâu gâu!"                          │
│     ├── Cat  → "Meo meo!"                          │
│     └── Bird → "Chíp chíp!"                        │
└─────────────────────────────────────────────────────┘
```

### 🧩 Hai loại Polymorphism

```
            Polymorphism
           /            \
          /              \
   Compile-time        Runtime
   (Static)            (Dynamic)
      │                   │
   Method             Method
   Overloading         Overriding
   (cùng tên,         (virtual/override
    khác tham số)       ở class con)
```

| Đặc điểm | Compile-time | Runtime |
|-----------|-------------|---------|
| Tên gọi khác | Static Polymorphism | Dynamic Polymorphism |
| Cơ chế | Method Overloading | Method Overriding |
| Quyết định lúc | Biên dịch (compile) | Chạy (runtime) |
| Từ khóa | Không cần | `virtual` / `override` |
| Liên quan đến kế thừa? | ❌ Không | ✅ Có |

---

## 2. Runtime Polymorphism (Method Overriding)

> 📌 Bạn đã học `virtual/override` ở Bài 20 (Inheritance). Bây giờ ta nâng cao lên!

### 🔄 Nhắc lại cơ bản

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("...");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Gâu gâu!");
    }
}
```

### ⚡ Điểm MỚI: Runtime quyết định method nào được gọi

```csharp
Animal animal = new Dog();  // Biến kiểu Animal, object thực sự là Dog
animal.Speak();              // → "Gâu gâu!" (KHÔNG PHẢI "...")
```

**Tại sao?** Vì C# dùng **virtual dispatch** — tại thời điểm chạy (runtime), CLR kiểm tra **object thực sự** là gì, rồi gọi method tương ứng.

### 🧠 Bộ nhớ minh họa

```
    Stack                         Heap
┌──────────────┐           ┌──────────────────┐
│ animal       │ ────────► │  [Dog object]     │
│ (kiểu Animal)│           │  Type: Dog        │
└──────────────┘           │  Speak() → Dog's  │
                           │    version        │
                           └──────────────────┘

Compiler thấy: animal là Animal → cho phép gọi Speak()
Runtime thấy:  object thực sự là Dog → gọi Dog.Speak()
```

### 🔗 Chuỗi override qua nhiều tầng

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Gâu gâu!");
}

class Puppy : Dog
{
    public override void Speak() => Console.WriteLine("Ẳng ẳng!");
}

// Runtime polymorphism hoạt động qua nhiều tầng:
Animal a1 = new Dog();    // → "Gâu gâu!"
Animal a2 = new Puppy();  // → "Ẳng ẳng!"
Dog    d1 = new Puppy();  // → "Ẳng ẳng!"
```

---

## 3. Compile-time Polymorphism (Method Overloading)

> 📌 Bạn đã biết overloading từ các bài trước. Giờ hệ thống lại!

### 📋 Method Overloading = Cùng tên, khác signature

```csharp
class Calculator
{
    // Overload 1: hai số int
    public int Add(int a, int b) => a + b;

    // Overload 2: ba số int
    public int Add(int a, int b, int c) => a + b + c;

    // Overload 3: hai số double
    public double Add(double a, double b) => a + b;

    // Overload 4: string (nối chuỗi)
    public string Add(string a, string b) => a + b;
}
```

### ⚙️ Compiler quyết định dựa trên:

```
Method được chọn tại COMPILE TIME dựa trên:
┌─────────────────────────────────────┐
│ 1. Số lượng tham số                 │
│ 2. Kiểu dữ liệu của tham số       │
│ 3. Thứ tự của tham số              │
│                                     │
│ ❌ KHÔNG dựa trên kiểu trả về     │
│    (return type không tính!)        │
└─────────────────────────────────────┘
```

### ⚠️ Lưu ý quan trọng

```csharp
// ❌ SAI — chỉ khác return type KHÔNG phải overloading
public int Calculate(int a) => a * 2;
public double Calculate(int a) => a * 2.0;  // Lỗi compile!

// ✅ ĐÚNG — khác kiểu tham số
public int Calculate(int a) => a * 2;
public double Calculate(double a) => a * 2.0;
```

---

## 4. Biến kiểu CHA chứa object CON

> 🎯 **Đây là trái tim của Polymorphism!**

### 📖 Nguyên tắc

```
┌──────────────────────────────────────────────────┐
│  Biến kiểu CHA có thể tham chiếu đến object CON │
│                                                  │
│  Animal animal = new Dog();     ✅ Hợp lệ       │
│  Animal animal = new Cat();     ✅ Hợp lệ       │
│  Dog dog = new Animal();        ❌ Không hợp lệ  │
│                                                  │
│  Lý do: Dog IS-A Animal (Bài 20)                │
│         Nhưng Animal KHÔNG IS-A Dog              │
└──────────────────────────────────────────────────┘
```

### 🔍 Biến kiểu CHA chỉ thấy member của CHA

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Gâu gâu!");
    public void Fetch() => Console.WriteLine("Bắt bóng!"); // Riêng của Dog
}

Animal animal = new Dog();
animal.Speak();   // ✅ OK — Speak() có trong Animal
// animal.Fetch(); // ❌ Lỗi — Fetch() KHÔNG có trong Animal
```

```
  Compiler nhìn thấy:        Runtime thực thi:
┌─────────────────┐        ┌─────────────────┐
│ Animal          │        │ Dog             │
│ ├── Speak() ────┼────────┤ ├── Speak()     │
│                 │        │ ├── Fetch()     │
│ Fetch() ← ❌   │        │                 │
│ KHÔNG THẤY      │        │                 │
└─────────────────┘        └─────────────────┘
```

### 💡 Tại sao hữu ích?

```csharp
// Không cần Polymorphism — code cứng, lặp lại:
void MakeDogSpeak(Dog dog) => dog.Speak();
void MakeCatSpeak(Cat cat) => cat.Speak();
void MakeBirdSpeak(Bird bird) => bird.Speak();

// Với Polymorphism — 1 method cho TẤT CẢ:
void MakeAnimalSpeak(Animal animal) => animal.Speak(); // 🎉
```

---

## 5. Polymorphism với mảng

### 🎯 Mảng kiểu CHA chứa nhiều loại CON

```csharp
Animal[] animals = new Animal[]
{
    new Dog(),
    new Cat(),
    new Bird(),
    new Dog(),
    new Cat()
};

// Một vòng lặp — xử lý TẤT CẢ
foreach (Animal animal in animals)
{
    animal.Speak();  // Mỗi con vật kêu theo cách riêng!
}
```

**Output:**
```
Gâu gâu!
Meo meo!
Chíp chíp!
Gâu gâu!
Meo meo!
```

### 📊 Minh họa bộ nhớ

```
  animals (Animal[])
┌─────┬─────┬─────┬─────┬─────┐
│ [0] │ [1] │ [2] │ [3] │ [4] │      Heap
├─────┼─────┼─────┼─────┼─────┤
│  ●──┼──●──┼──●──┼──●──┼──●  │
└──┼──┴──┼──┴──┼──┴──┼──┴──┼──┘
   │     │     │     │     │
   ▼     ▼     ▼     ▼     ▼
 [Dog] [Cat] [Bird] [Dog] [Cat]

Cùng 1 mảng Animal[] nhưng chứa nhiều LOẠI object khác nhau!
```

### 🔄 Thêm loại mới — KHÔNG sửa code cũ

```csharp
// Thêm class mới...
class Fish : Animal
{
    public override void Speak() => Console.WriteLine("Blub blub!");
}

// ...và thêm vào mảng — code forEach KHÔNG CẦN SỬA!
Animal[] animals = new Animal[]
{
    new Dog(), new Cat(), new Bird(), new Fish()  // Thêm Fish
};

foreach (Animal animal in animals)
{
    animal.Speak();  // Tự động gọi Fish.Speak() cho Fish! 🎉
}
```

---

## 6. Từ khóa `is` — Type Checking

### 📖 Cú pháp

```csharp
if (variable is Type)
{
    // variable thuộc kiểu Type (hoặc kiểu con của Type)
}
```

### 🔍 Ví dụ

```csharp
Animal animal = new Dog();

Console.WriteLine(animal is Animal);  // True — Dog IS-A Animal
Console.WriteLine(animal is Dog);     // True — object thực sự là Dog
Console.WriteLine(animal is Cat);     // False — Dog KHÔNG phải Cat
```

### 📋 Ứng dụng thực tế

```csharp
Animal[] animals = { new Dog(), new Cat(), new Bird() };

foreach (Animal animal in animals)
{
    if (animal is Dog)
    {
        Console.WriteLine("Tìm thấy một chú chó!");
    }
    else if (animal is Cat)
    {
        Console.WriteLine("Tìm thấy một chú mèo!");
    }
}
```

### ⚠️ Lưu ý: `is` kiểm tra cả kế thừa

```
            Animal
           /      \
         Dog      Cat
         /
       Puppy

Puppy puppy = new Puppy();
puppy is Puppy  → True
puppy is Dog    → True   ← Vì Puppy kế thừa Dog
puppy is Animal → True   ← Vì Puppy kế thừa Animal (gián tiếp)
puppy is Cat    → False
```

---

## 7. Từ khóa `as` — Safe Casting

### 📖 Vấn đề với Direct Casting

```csharp
Animal animal = new Dog();

// Direct cast — nguy hiểm!
Dog dog = (Dog)animal;        // ✅ OK — animal thực sự là Dog
Cat cat = (Cat)animal;        // ❌ CRASH! InvalidCastException!
```

### ✅ `as` — Casting an toàn

```csharp
Animal animal = new Dog();

Dog dog = animal as Dog;      // ✅ dog = Dog object
Cat cat = animal as Cat;      // ✅ cat = null (KHÔNG crash!)

// Luôn kiểm tra null sau khi dùng as
if (dog != null)
{
    dog.Fetch();  // An toàn!
}

if (cat != null)
{
    cat.Purr();   // Không chạy vì cat == null
}
```

### 📊 So sánh Direct Cast vs `as`

```
┌──────────────────────────────────────────────────┐
│         Direct Cast          │     as keyword     │
│         (Type)variable       │   variable as Type │
├──────────────────────────────┼────────────────────┤
│ Thành công → trả về object  │ Thành công → object│
│ Thất bại → EXCEPTION! 💥    │ Thất bại → null    │
│ Dùng khi chắc chắn đúng    │ Dùng khi không chắc│
│ Nhanh hơn 1 chút           │ An toàn hơn        │
└──────────────────────────────┴────────────────────┘
```

### ⚠️ `as` chỉ dùng với reference types

```csharp
// ❌ Không dùng as với value types (int, struct,...)
// int number = animal as int;  // Lỗi compile!

// ✅ Dùng is cho value types
if (obj is int number)
{
    Console.WriteLine(number);
}
```

---

## 8. Pattern Matching

> 🆕 **C# 7+** — Kết hợp `is` + khai báo biến trong 1 bước!

### 📖 Cú pháp cơ bản

```csharp
// Cách cũ (2 bước):
if (animal is Dog)
{
    Dog dog = (Dog)animal;    // Phải cast thêm lần nữa
    dog.Fetch();
}

// Cách mới — Pattern Matching (1 bước):
if (animal is Dog dog)        // Kiểm tra + cast + gán biến
{
    dog.Fetch();              // Dùng luôn!
}
```

### 🎯 Ví dụ thực tế

```csharp
void DescribeAnimal(Animal animal)
{
    if (animal is Dog dog)
    {
        Console.WriteLine($"Chó: {dog.Name}, biết bắt bóng!");
        dog.Fetch();
    }
    else if (animal is Cat cat)
    {
        Console.WriteLine($"Mèo: {cat.Name}, biết kêu purr!");
        cat.Purr();
    }
    else if (animal is Bird bird)
    {
        Console.WriteLine($"Chim: {bird.Name}, biết bay!");
        bird.Fly();
    }
    else
    {
        Console.WriteLine($"Động vật: {animal.Name}");
    }
}
```

### 🔄 Switch Expression với Pattern Matching (C# 8+)

```csharp
string GetSound(Animal animal) => animal switch
{
    Dog dog   => $"{dog.Name} kêu: Gâu gâu!",
    Cat cat   => $"{cat.Name} kêu: Meo meo!",
    Bird bird => $"{bird.Name} kêu: Chíp chíp!",
    _         => "Không biết kêu gì"
};
```

### 📊 Các loại Pattern phổ biến

| Pattern | Ví dụ | Mô tả |
|---------|-------|-------|
| Type pattern | `x is Dog dog` | Kiểm tra kiểu + gán biến |
| Constant pattern | `x is null` | Kiểm tra giá trị cụ thể |
| Property pattern | `x is Dog { Age: > 5 }` | Kiểm tra kiểu + property |
| Discard pattern | `_` | Khớp với mọi thứ |

---

## 9. Tại sao cần Polymorphism?

### 🏗️ Open/Closed Principle (OCP)

> **"Open for extension, Closed for modification"**
> — Mở để mở rộng, đóng để sửa đổi

```
❌ KHÔNG CÓ Polymorphism:
┌────────────────────────────────────────────┐
│ void ProcessPayment(string type, ...)      │
│ {                                          │
│     if (type == "cash") { ... }            │
│     else if (type == "card") { ... }       │
│     else if (type == "ewallet") { ... }    │
│     // Thêm loại mới → SỬA code cũ! 😱   │
│     else if (type == "crypto") { ... }     │
│ }                                          │
└────────────────────────────────────────────┘

✅ CÓ Polymorphism:
┌────────────────────────────────────────────┐
│ void ProcessPayment(Payment payment)       │
│ {                                          │
│     payment.Process();  // Tự động đúng!   │
│     // Thêm loại mới → KHÔNG sửa gì! 🎉  │
│ }                                          │
└────────────────────────────────────────────┘
```

### 📋 Lợi ích cụ thể

```
┌─────────────────────────────────────────────────┐
│ 1. 🔌 Extensibility (Mở rộng dễ)               │
│    → Thêm class con mới, không sửa code cũ     │
│                                                 │
│ 2. 🧹 Clean Code (Code sạch)                   │
│    → Loại bỏ if/else dài dằng dặc              │
│                                                 │
│ 3. 🧪 Testability (Dễ test)                    │
│    → Test từng class con độc lập                │
│                                                 │
│ 4. 🔄 Flexibility (Linh hoạt)                  │
│    → Thay đổi behavior tại runtime             │
│                                                 │
│ 5. 👥 Team Work (Làm việc nhóm)                │
│    → Mỗi người code 1 class con, không xung đột│
└─────────────────────────────────────────────────┘
```

---

## 10. Real-world Analogy: Cổng USB

```
🔌 Cổng USB = Polymorphism

  Laptop
┌─────────────────────┐
│                     │
│   ┌───┐             │     1 cổng USB (interface)
│   │USB│◄────────────┼──── nhưng cắm được NHIỀU thiết bị
│   └───┘             │
│                     │
└─────────────────────┘
      │
      ├── 🖱️ Chuột      → Di chuyển con trỏ
      ├── ⌨️ Bàn phím   → Gõ chữ
      ├── 📱 Điện thoại → Sạc pin
      ├── 💾 USB Drive  → Lưu trữ dữ liệu
      └── 🎮 Tay cầm   → Chơi game

Laptop KHÔNG CẦN BIẾT thiết bị gì được cắm vào.
Nó chỉ cần biết: "Thiết bị này hỗ trợ USB protocol."
→ Mỗi thiết bị tự xử lý hành vi của riêng mình!
```

```
Tương tự trong code:

  IUsbDevice device = new Mouse();     // Cắm chuột
  device.Connect();                     // → "Mouse connected"

  IUsbDevice device = new Keyboard();  // Cắm bàn phím
  device.Connect();                     // → "Keyboard connected"

  // Laptop (code gọi) KHÔNG ĐỔI,
  // chỉ thay thiết bị (object) là xong!
```

---

## 11. So sánh C# vs JavaScript

### 📊 Runtime Polymorphism

```csharp
// ═══ C# — Cần virtual/override, có type safety ═══
class Animal
{
    public virtual void Speak()         // virtual ở CHA
    {
        Console.WriteLine("...");
    }
}

class Dog : Animal
{
    public override void Speak()        // override ở CON
    {
        Console.WriteLine("Gâu gâu!");
    }
}

Animal animal = new Dog();
animal.Speak();  // "Gâu gâu!" — Runtime Polymorphism
```

```javascript
// ═══ JavaScript — Duck typing, không cần từ khóa đặc biệt ═══
class Animal {
    speak() {
        console.log("...");
    }
}

class Dog extends Animal {
    speak() {                           // Tự động override
        console.log("Gâu gâu!");
    }
}

let animal = new Dog();
animal.speak();  // "Gâu gâu!"

// JS còn cho phép duck typing:
let obj = { speak: () => console.log("Quack!") };
obj.speak();  // Không cần kế thừa!
```

### 📊 Type Checking

```csharp
// ═══ C# ═══
if (animal is Dog dog)           // Pattern matching — an toàn
{
    dog.Fetch();
}

Animal a = new Dog();
Dog d = a as Dog;                // Safe cast
```

```javascript
// ═══ JavaScript ═══
if (animal instanceof Dog) {     // Chỉ check class chain
    animal.fetch();              // Không cần cast (dynamic typing)
}

// typeof chỉ cho primitives
typeof "hello"  // "string"
typeof 42       // "number"
```

### 📊 Bảng so sánh tổng quan

| Tính năng | C# | JavaScript |
|-----------|-----|-----------|
| Override method | `virtual` + `override` | Tự động |
| Overloading | ✅ Hỗ trợ | ❌ Không có (dùng default params) |
| Type checking | `is`, `as`, pattern matching | `instanceof`, `typeof` |
| Safe casting | `as` (trả null) | Không cần (duck typing) |
| Type safety | ✅ Compile-time | ❌ Runtime only |
| Duck typing | ❌ Không | ✅ Có |

---

## 12. Sai lầm thường gặp & Best Practices

### ❌ Sai lầm #1: Quên `virtual` ở class cha

```csharp
class Animal
{
    public void Speak() { }  // ❌ Thiếu virtual!
}

class Dog : Animal
{
    public override void Speak() { }  // ❌ Lỗi compile!
}
```

### ❌ Sai lầm #2: Dùng `new` thay vì `override`

```csharp
class Dog : Animal
{
    public new void Speak()  // ⚠️ HIDING, không phải overriding!
    {
        Console.WriteLine("Gâu gâu!");
    }
}

Animal animal = new Dog();
animal.Speak();  // → "..." ← GỌI CỦA ANIMAL, không phải Dog!
                 // Vì new = hiding, KHÔNG PHẢI polymorphism!
```

### ❌ Sai lầm #3: Direct cast không kiểm tra

```csharp
Animal animal = new Cat();
Dog dog = (Dog)animal;  // 💥 InvalidCastException tại runtime!

// ✅ Fix:
if (animal is Dog dog)
{
    dog.Fetch();
}
```

### ❌ Sai lầm #4: Lạm dụng `is` kiểm tra kiểu

```csharp
// ❌ Anti-pattern — phá vỡ mục đích polymorphism!
foreach (Animal animal in animals)
{
    if (animal is Dog) DoSomethingForDog();
    else if (animal is Cat) DoSomethingForCat();
    else if (animal is Bird) DoSomethingForBird();
    // Thêm loại mới → phải sửa code! 😱
}

// ✅ Đúng cách — dùng polymorphism!
foreach (Animal animal in animals)
{
    animal.DoSomething();  // Mỗi class con tự xử lý! 🎉
}
```

### ✅ Best Practices

```
┌──────────────────────────────────────────────────────┐
│ 1. Luôn dùng virtual/override cho runtime polymorphism│
│                                                      │
│ 2. Ưu tiên polymorphism hơn if/else type checking    │
│                                                      │
│ 3. Dùng pattern matching (is Type var) thay vì       │
│    is + direct cast                                  │
│                                                      │
│ 4. Tránh dùng "new" keyword cho method hiding        │
│    (trừ khi có lý do rất rõ ràng)                    │
│                                                      │
│ 5. Method ở base class nên là virtual nếu class con  │
│    CÓ THỂ cần thay đổi behavior                     │
│                                                      │
│ 6. Dùng sealed override nếu muốn chặn override tiếp │
│    class Puppy : Dog                                 │
│    {                                                 │
│        public sealed override void Speak() { }       │
│        // Các class con của Puppy KHÔNG override được│
│    }                                                 │
└──────────────────────────────────────────────────────┘
```

---

## 🗺️ Tổng kết hành trình OOP

```
Bài 15-18: Class, Constructor, this, Property
                    │
Bài 19: Encapsulation (đóng gói dữ liệu)
                    │
Bài 20: Inheritance (kế thừa, virtual/override)
                    │
             ┌──────┴──────┐
             ▼             ▼
    Bài 21: POLYMORPHISM   Bài 22: Abstraction
    (1 interface,          (sẽ học tiếp)
     nhiều hình dạng)
```

> **Bài tiếp theo:** Abstraction — Tính trừu tượng, Abstract Class & Interface 🚀

---

*"Polymorphism không chỉ là feature — nó là cách tư duy. Nghĩ về HÀNH VI, không phải KIỂU DỮ LIỆU."* 🎯
