# 📘 Lesson 20 — Inheritance (Tính kế thừa) trong C#

---

## 1. Inheritance là gì?

**Inheritance** (Tính kế thừa) = Class con **kế thừa** data + behavior từ class cha, tránh viết lại code trùng lặp (**DRY** — Don't Repeat Yourself).

```
╔═══════════════════════════════════════════════════════════════╗
║                     INHERITANCE                               ║
║                                                               ║
║   ┌───────────────────────┐                                   ║
║   │    Animal (CHA)       │  ← BASE class / Parent class      ║
║   │  ─────────────────    │                                   ║
║   │  string Name          │                                   ║
║   │  int Age              │                                   ║
║   │  void Eat()           │                                   ║
║   │  void Sleep()         │                                   ║
║   └──────────┬────────────┘                                   ║
║              │ kế thừa                                        ║
║     ┌────────┼────────┐                                       ║
║     ▼        ▼        ▼                                       ║
║   ┌───────┐ ┌──────┐ ┌───────┐                                ║
║   │  Dog  │ │ Cat  │ │ Bird  │  ← DERIVED class / Child class  ║
║   │ ───── │ │ ──── │ │ ───── │                                ║
║   │ Bark()│ │Meow()│ │ Fly() │  ← Thêm behavior riêng        ║
║   └───────┘ └──────┘ └───────┘                                ║
║                                                               ║
║   Dog CÓ: Name, Age, Eat(), Sleep() (từ Animal)              ║
║         + Bark() (riêng Dog)                                  ║
╚═══════════════════════════════════════════════════════════════╝
```

### IS-A Relationship:

```
Dog    IS-A  Animal    ✅ (Dog là một Animal)
Cat    IS-A  Animal    ✅
Bird   IS-A  Animal    ✅
Animal IS-A  Dog       ❌ (Không phải mọi Animal đều là Dog)
```

> 🔑 Inheritance mô hình hóa quan hệ **"IS-A"** (là một).

---

## 2. Cú pháp: `class Child : Parent`

```csharp
// ===== CLASS CHA (Base class) =====
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Eat() => Console.WriteLine($"{Name} đang ăn...");
    public void Sleep() => Console.WriteLine($"{Name} đang ngủ...");
}

// ===== CLASS CON (Derived class) — kế thừa bằng dấu : =====
class Dog : Animal
{
    public string Breed { get; set; }   // Thêm property riêng

    public void Bark() => Console.WriteLine($"{Name} sủa: Gâu gâu!"); // Thêm method riêng
}

class Cat : Animal
{
    public bool IsIndoor { get; set; }

    public void Meow() => Console.WriteLine($"{Name} kêu: Meo meo!");
}
```

```csharp
// Sử dụng:
Dog dog = new Dog();
dog.Name = "Buddy";     // ✅ Kế thừa từ Animal
dog.Age = 3;            // ✅ Kế thừa từ Animal
dog.Breed = "Corgi";    // ✅ Property riêng Dog

dog.Eat();     // ✅ "Buddy đang ăn..."    (kế thừa)
dog.Sleep();   // ✅ "Buddy đang ngủ..."   (kế thừa)
dog.Bark();    // ✅ "Buddy sủa: Gâu gâu!" (riêng Dog)

Cat cat = new Cat();
cat.Name = "Mimi";
cat.Meow();    // ✅ "Mimi kêu: Meo meo!"
// cat.Bark(); // ❌ LỖI! Cat không có Bark()
```

---

## 3. `base` keyword — Gọi constructor cha

### 3.1 Constructor chain: con GỌI constructor cha qua `base()`

```csharp
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Constructor cha
    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine($"  [Animal] Constructor: {name}, {age} tuổi");
    }
}

class Dog : Animal
{
    public string Breed { get; set; }

    // Constructor con → GỌI constructor cha qua base()
    public Dog(string name, int age, string breed) : base(name, age)
    {
        Breed = breed;
        Console.WriteLine($"  [Dog] Constructor: giống {breed}");
    }
}

class GoldenRetriever : Dog
{
    public bool IsTrained { get; set; }

    public GoldenRetriever(string name, int age, bool trained)
        : base(name, age, "Golden Retriever")
    {
        IsTrained = trained;
        Console.WriteLine($"  [Golden] Constructor: trained={trained}");
    }
}
```

```csharp
var goldie = new GoldenRetriever("Max", 2, true);
```

```
Output (thứ tự constructor):
  [Animal] Constructor: Max, 2 tuổi        ← CHA chạy TRƯỚC
  [Dog] Constructor: giống Golden Retriever ← Con chạy sau
  [Golden] Constructor: trained=True        ← Cháu chạy cuối
```

```
Thứ tự constructor:
  Animal → Dog → GoldenRetriever
  (Từ cha → con → cháu)
```

### 3.2 Nếu cha có constructor mặc định:

```csharp
class Animal
{
    public string Name { get; set; } = "Unknown";

    public Animal() { }                // Constructor mặc định
    public Animal(string name) { Name = name; }
}

class Dog : Animal
{
    public string Breed { get; set; }

    // Nếu KHÔNG viết : base() → tự động gọi base() (constructor mặc định)
    public Dog(string breed)
    {
        Breed = breed;
        // → Tự gọi Animal() → Name = "Unknown"
    }

    // Chỉ định gọi constructor nào:
    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
        // → Gọi Animal(name)
    }
}
```

### 3.3 `base` — Gọi method của cha:

```csharp
class Animal
{
    public string Name { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"Tôi là {Name}");
    }
}

class Dog : Animal
{
    public string Breed { get; set; }

    public new void Introduce()
    {
        base.Introduce();  // Gọi method cha TRƯỚC
        Console.WriteLine($"Giống: {Breed}");  // Thêm thông tin riêng
    }
}
```

---

## 4. Method Overriding: `virtual` + `override`

### Vấn đề: Con muốn THAY ĐỔI behavior của cha

```csharp
class Animal
{
    public string Name { get; set; }

    // virtual = cho phép con OVERRIDE (ghi đè)
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} phát ra âm thanh...");
    }

    public virtual string GetInfo()
    {
        return $"{Name} (Animal)";
    }
}

class Dog : Animal
{
    // override = GHI ĐÈ method của cha
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} sủa: Gâu gâu! 🐕");
    }

    public override string GetInfo()
    {
        return $"{Name} (Dog)";
    }
}

class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} kêu: Meo meo! 🐱");
    }

    public override string GetInfo()
    {
        return $"{Name} (Cat)";
    }
}

class Fish : Animal
{
    // KHÔNG override MakeSound → dùng của cha
    public override string GetInfo()
    {
        return $"{Name} (Fish)";
    }
}
```

```csharp
Animal[] animals = {
    new Dog { Name = "Buddy" },
    new Cat { Name = "Mimi" },
    new Fish { Name = "Nemo" }
};

foreach (var a in animals)
{
    a.MakeSound();
}
```

```
Output:
  Buddy sủa: Gâu gâu! 🐕        ← Dog override
  Mimi kêu: Meo meo! 🐱          ← Cat override
  Nemo phát ra âm thanh...        ← Fish dùng của cha
```

```
virtual + override:
┌─────────────┐
│   Animal    │  virtual MakeSound() { "âm thanh..." }
└──────┬──────┘
       │
  ┌────┼────┐
  ▼    ▼    ▼
┌────┐┌────┐┌─────┐
│Dog ││Cat ││Fish │
│────││────││─────│
│Gâu!││Meo!││(kế  │  ← Fish KHÔNG override → dùng của Animal
└────┘└────┘│thừa)│
            └─────┘
```

---

## 5. `sealed` — Chặn kế thừa / chặn override

### 5.1 `sealed class` — Không cho class nào kế thừa:

```csharp
sealed class FinalAnimal
{
    public string Name { get; set; }
}

// class SuperAnimal : FinalAnimal { }  // ❌ LỖI! sealed class
```

> `string` trong C# là `sealed class` — không ai kế thừa được!

### 5.2 `sealed override` — Cho phép override nhưng DỪNG ở đây:

```csharp
class Animal
{
    public virtual void MakeSound() { }
}

class Dog : Animal
{
    public sealed override void MakeSound()  // Dừng override ở Dog
    {
        Console.WriteLine("Gâu!");
    }
}

class Puppy : Dog
{
    // public override void MakeSound() { }  // ❌ LỖI! sealed ở Dog
}
```

---

## 6. `protected` — Access modifier cho Inheritance

```csharp
class Animal
{
    public string Name { get; set; }       // 🌍 Tất cả thấy
    protected int Energy { get; set; }     // 🛡️ Chỉ class này + con thấy
    private string _dna = "ATCG";          // 🔒 Chỉ class này thấy
}

class Dog : Animal
{
    public void Play()
    {
        Energy -= 10;    // ✅ protected → Dog thấy được
        Console.WriteLine($"{Name} chơi! Energy: {Energy}");

        // Console.WriteLine(_dna);  // ❌ LỖI! private → chỉ Animal thấy
    }
}
```

```csharp
var dog = new Dog { Name = "Buddy", Energy = 100 };
dog.Play();              // ✅ "Buddy chơi! Energy: 90"
Console.WriteLine(dog.Name);    // ✅ public
// Console.WriteLine(dog.Energy); // ❌ LỖI! protected — bên ngoài không thấy
```

```
Access Modifier trong Inheritance:
┌────────────────────────────────────────────────┐
│ Animal                                          │
│  public Name        ← Tất cả thấy              │
│  protected Energy   ← Chỉ Animal + con thấy    │
│  private _dna       ← Chỉ Animal thấy          │
└──────────┬─────────────────────────────────────┘
           │ kế thừa
           ▼
┌────────────────────────────────────────────────┐
│ Dog : Animal                                    │
│  ✅ Name (public)                               │
│  ✅ Energy (protected) ← Con thấy!              │
│  ❌ _dna (private)     ← KHÔNG thấy!            │
└────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────┐
│ Bên ngoài (Main)                                │
│  ✅ Name (public)                               │
│  ❌ Energy (protected) ← KHÔNG thấy!            │
│  ❌ _dna (private)     ← KHÔNG thấy!            │
└────────────────────────────────────────────────┘
```

---

## 7. Constructor Chain — Thứ tự khởi tạo

```csharp
class Animal
{
    public string Name { get; set; }
    protected int Energy { get; set; }

    public Animal(string name, int energy = 100)
    {
        Name = name;
        Energy = energy;
        Console.WriteLine($"  1️⃣ Animal({name}, energy={energy})");
    }
}

class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, string breed) : base(name, 120)
    {
        Breed = breed;
        Console.WriteLine($"  2️⃣ Dog(breed={breed})");
    }
}

class Puppy : Dog
{
    public bool IsVaccinated { get; set; }

    public Puppy(string name, string breed, bool vaccinated)
        : base(name, breed)
    {
        IsVaccinated = vaccinated;
        Console.WriteLine($"  3️⃣ Puppy(vaccinated={vaccinated})");
    }
}
```

```csharp
var puppy = new Puppy("Lucky", "Poodle", true);
```

```
Output:
  1️⃣ Animal(Lucky, energy=120)    ← Ông chạy trước
  2️⃣ Dog(breed=Poodle)            ← Cha chạy sau
  3️⃣ Puppy(vaccinated=True)       ← Con chạy cuối

Chain: Puppy → Dog → Animal (gọi ngược lên)
Chạy: Animal → Dog → Puppy (chạy từ trên xuống)
```

---

## 8. Object class — Tổ tiên của mọi class

```csharp
// MỌI class trong C# đều kế thừa từ Object (ngầm)
class Animal { }

// Tương đương:
class Animal : Object { }
```

```
Object (System.Object)
  ├── ToString()      ← Override để hiển thị đẹp
  ├── Equals()        ← So sánh
  ├── GetHashCode()   ← Hash
  └── GetType()       ← Lấy kiểu
       │
       ├── Animal
       │     ├── Dog
       │     └── Cat
       ├── String
       ├── Int32
       └── ... (mọi class khác)
```

### Override `ToString()`:

```csharp
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Override ToString() của Object
    public override string ToString()
    {
        return $"{Name} ({Age} tuổi)";
    }
}

class Dog : Animal
{
    public string Breed { get; set; }

    // Override tiếp
    public override string ToString()
    {
        return $"🐕 {Name} - {Breed} ({Age} tuổi)";
    }
}
```

```csharp
Dog dog = new Dog { Name = "Buddy", Age = 3, Breed = "Corgi" };

Console.WriteLine(dog);               // 🐕 Buddy - Corgi (3 tuổi)
Console.WriteLine(dog.ToString());     // 🐕 Buddy - Corgi (3 tuổi)
Console.WriteLine($"Pet: {dog}");      // Pet: 🐕 Buddy - Corgi (3 tuổi)
// Console.WriteLine tự gọi ToString()!
```

---

## 9. Single Inheritance — C# chỉ kế thừa 1 class

```csharp
class Animal { }
class Pet { }

// ❌ C# KHÔNG cho phép đa kế thừa class:
// class Dog : Animal, Pet { }  // LỖI!

// ✅ Giải pháp: kế thừa 1 class + implement nhiều interface (học sau)
// class Dog : Animal, IPlayable, ITrainable { }
```

```
C# vs Java:
  C#:   Single inheritance (1 class) + Multiple interfaces
  Java: Single inheritance (1 class) + Multiple interfaces
  C++:  Multiple inheritance (nhiều class)  ← Phức tạp, diamond problem!
```

---

## 10. Memory — Child object chứa cả fields cha

```csharp
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Dog : Animal
{
    public string Breed { get; set; }
}

Dog buddy = new Dog { Name = "Buddy", Age = 3, Breed = "Corgi" };
```

```
MEMORY LAYOUT:

Stack                    Heap
┌──────────┐            ┌──────────────────────────┐
│ buddy ───────────────→│  [Dog object]            │
│          │            │  ┌─────────────────────┐ │
└──────────┘            │  │ ANIMAL PART (cha)   │ │
                        │  │  Name = "Buddy"     │ │
                        │  │  Age  = 3           │ │
                        │  ├─────────────────────┤ │
                        │  │ DOG PART (con)      │ │
                        │  │  Breed = "Corgi"    │ │
                        │  └─────────────────────┘ │
                        └──────────────────────────┘

Dog object = Animal fields + Dog fields
→ Con CHỨA tất cả data của cha!
```

---

## 11. Upcasting / Downcasting cơ bản

### Upcasting — Con → Cha (luôn an toàn, tự động):

```csharp
Dog dog = new Dog { Name = "Buddy", Breed = "Corgi" };
Animal animal = dog;           // ✅ Upcasting — Dog IS-A Animal

animal.Name;      // ✅ "Buddy"
// animal.Breed;  // ❌ LỖI! Animal không biết Breed
// animal vẫn LÀ Dog object, nhưng nhìn qua "kính" Animal
```

### Downcasting — Cha → Con (cần cast, có thể thất bại):

```csharp
Animal animal = new Dog { Name = "Buddy", Breed = "Corgi" };

// Cách 1: Direct cast — exception nếu sai kiểu
Dog dog1 = (Dog)animal;           // ✅ vì animal thật sự là Dog
// Cat cat = (Cat)animal;         // 💥 InvalidCastException! Không phải Cat

// Cách 2: as — trả về null nếu sai kiểu (an toàn hơn)
Dog? dog2 = animal as Dog;        // ✅ dog2 = Dog object
Cat? cat = animal as Cat;         // ✅ cat = null (không crash)

// Cách 3: is — kiểm tra kiểu trước
if (animal is Dog d)
{
    Console.WriteLine(d.Breed);   // ✅ An toàn nhất!
}
```

```
Upcasting / Downcasting:

                Animal (cha)
                  ▲  │
    Upcasting     │  │  Downcasting
    (tự động)     │  │  (cần cast)
    (luôn OK)     │  ▼  (có thể fail)
                Dog (con)

Memory: Dog object KHÔNG THAY ĐỔI!
Chỉ thay đổi CÁCH NHÌN (kiểu biến).
```

---

## 12. So sánh C# vs JavaScript

```javascript
// JavaScript — Inheritance với extends
class Animal {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    makeSound() {
        console.log(`${this.name} makes a sound`);
    }
}

class Dog extends Animal {
    constructor(name, age, breed) {
        super(name, age);  // Gọi constructor cha
        this.breed = breed;
    }

    makeSound() {  // Override (không cần virtual/override keyword)
        console.log(`${this.name} barks!`);
    }
}
```

```csharp
// C# — Inheritance với :
class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public virtual void MakeSound()  // Phải đánh dấu virtual
    {
        Console.WriteLine($"{Name} makes a sound");
    }
}

class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, int age, string breed)
        : base(name, age)  // Gọi constructor cha
    {
        Breed = breed;
    }

    public override void MakeSound()  // Phải đánh dấu override
    {
        Console.WriteLine($"{Name} barks!");
    }
}
```

| | C# | JavaScript |
|--|----|-----------| 
| Kế thừa | `class Dog : Animal` | `class Dog extends Animal` |
| Gọi cha constructor | `: base(args)` | `super(args)` |
| Gọi cha method | `base.Method()` | `super.method()` |
| Override | `virtual` + `override` (bắt buộc) | Tự động (không keyword) |
| Sealed | `sealed class` / `sealed override` | ❌ Không có |
| Protected | `protected` keyword | ❌ Không có (ES2022 có proposal) |
| Multiple inherit | ❌ Chỉ 1 class | ❌ Chỉ 1 class |
| Check type | `is`, `as`, cast | `instanceof` |

---

## 13. Khi nào dùng / KHÔNG dùng Inheritance

### ✅ Dùng khi:

| Tình huống | Ví dụ |
|-----------|-------|
| Quan hệ IS-A rõ ràng | Dog IS-A Animal |
| Chia sẻ code chung | Tất cả Employee có Name, Salary |
| Override behavior | Dog.MakeSound() khác Cat.MakeSound() |
| Hierarchy tự nhiên | Shape → Circle, Rectangle, Triangle |

### ❌ KHÔNG dùng khi:

| Tình huống | Giải pháp |
|-----------|-----------|
| Chỉ muốn dùng lại code (HAS-A) | Composition: `Car HAS-A Engine` |
| Hierarchy quá sâu (> 3 levels) | Phẳng hóa, dùng Interface |
| Override quá nhiều methods | Design có vấn đề |
| Chỉ có 1 class con | Không cần inheritance |

```
✅ IS-A (dùng Inheritance):
    Dog IS-A Animal → class Dog : Animal

❌ HAS-A (dùng Composition):
    Car HAS-A Engine → class Car { Engine engine; }
    Student HAS-A Address → class Student { Address address; }
```

---

## 14. Tổng kết

```
┌─────────────────────────────────────────────────────────┐
│                    INHERITANCE                           │
│                                                         │
│   Syntax:    class Child : Parent                       │
│   Base:      : base(args) — gọi constructor cha        │
│   Virtual:   virtual method — cho phép override         │
│   Override:  override method — ghi đè behavior          │
│   Sealed:    sealed class/method — chặn kế thừa         │
│   Protected: protected — cha + con thấy                 │
│   Object:    mọi class kế thừa Object                   │
│   Memory:    Child chứa tất cả fields của Parent        │
│   Cast:      Upcasting (↑ auto) / Downcasting (↓ cast) │
│   Rule:      Single inheritance — chỉ 1 class cha      │
│   Khi dùng:  IS-A relationship                          │
│                                                         │
│   Constructor chain: Parent → Child → Grandchild        │
└─────────────────────────────────────────────────────────┘
```
