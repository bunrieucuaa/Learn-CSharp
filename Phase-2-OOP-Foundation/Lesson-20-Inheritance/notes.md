# 📝 Lesson 20 — Notes: Inheritance (Tính kế thừa)

---

## 🧠 Tóm tắt

```
Inheritance = Class con KẾ THỪA data + behavior từ class cha
            → DRY (Don't Repeat Yourself)
            → IS-A relationship
```

### Cú pháp cơ bản:

```csharp
// Kế thừa
class Dog : Animal { }

// Constructor chain — con gọi constructor cha
class Dog : Animal
{
    public Dog(string name, string breed)
        : base(name)          // Gọi Animal(name)
    {
        Breed = breed;
    }
}

// Virtual + Override — ghi đè behavior
class Animal
{
    public virtual void MakeSound() => Console.WriteLine("...");
}
class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("Gâu!");
}

// Gọi method cha
public override void MakeSound()
{
    base.MakeSound();    // Gọi của cha trước
    // thêm logic con
}

// Sealed — chặn kế thừa/override
sealed class FinalClass { }
public sealed override void Method() { }

// Protected — cha + con thấy
protected int Energy { get; set; }

// ToString() override
public override string ToString() => $"{Name} ({Age} tuổi)";
```

### Memory layout:

```
Stack                    Heap
┌──────────┐            ┌──────────────────────┐
│ dog ─────────────────→│ [Dog object]         │
│          │            │  ANIMAL: Name, Age   │ ← Fields cha
└──────────┘            │  DOG:    Breed       │ ← Fields con
                        └──────────────────────┘
```

### Upcasting / Downcasting:

```csharp
Dog dog = new Dog("Buddy", "Corgi");
Animal animal = dog;              // ✅ Upcasting (tự động)

if (animal is Dog d)              // ✅ Downcasting (kiểm tra + cast)
    Console.WriteLine(d.Breed);
```

### So sánh C# vs JS:

| | C# | JavaScript |
|--|----|-----------| 
| Kế thừa | `class Dog : Animal` | `class Dog extends Animal` |
| Constructor cha | `: base(args)` | `super(args)` |
| Method cha | `base.Method()` | `super.method()` |
| Override | `virtual` + `override` | Tự động |
| Sealed | `sealed` | ❌ |
| Protected | `protected` | ❌ |

### Khi nào dùng:

```
✅ IS-A: Dog IS-A Animal           → Inheritance
❌ HAS-A: Car HAS-A Engine         → Composition (chứa object)
✅ Chia sẻ code chung              → Inheritance
❌ Chỉ 1 class con                 → Không cần inheritance
❌ Hierarchy > 3 levels             → Xem xét Interface
```

---

## ✅ Checklist

- [ ] Biết cú pháp `class Child : Parent`
- [ ] Biết `base(args)` — gọi constructor cha
- [ ] Biết `base.Method()` — gọi method cha
- [ ] Biết `virtual` (cha) + `override` (con)
- [ ] Biết `sealed class` — chặn kế thừa
- [ ] Biết `sealed override` — chặn override tiếp
- [ ] Biết `protected` — cha + con thấy, ngoài không thấy
- [ ] Biết constructor chain: Parent → Child (cha chạy trước)
- [ ] Biết `Object` là tổ tiên mọi class
- [ ] Biết override `ToString()`
- [ ] Biết single inheritance (C# chỉ 1 class cha)
- [ ] Biết memory layout: child chứa fields cha
- [ ] Biết Upcasting (tự động) vs Downcasting (`is`, `as`, cast)
- [ ] Biết khi nào dùng (IS-A) vs không dùng (HAS-A)

---

## 🔗 Liên kết

```
Lesson 19: Encapsulation (trụ cột OOP #1)
    ↓
Lesson 20: Inheritance ← BẠN Ở ĐÂY (trụ cột OOP #2)
    ↓
Lesson 21: Polymorphism (trụ cột OOP #3)
    ↓
Lesson 22: Abstraction (trụ cột OOP #4)
    ↓
Lesson 23: Interface
```

> 📌 **Inheritance** mở đường cho **Polymorphism** (Lesson 21).
> Khi bạn dùng `virtual/override`, bạn đã thấy "preview" của Polymorphism rồi!
> `Animal[] animals = { new Dog(), new Cat() }; animals[0].MakeSound();` ← Đây chính là Polymorphism!
