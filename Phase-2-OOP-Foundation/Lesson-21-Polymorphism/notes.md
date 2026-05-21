# 📝 Bài 21: Polymorphism — Tóm tắt & Checklist

---

## 📋 Tóm tắt nhanh

### 1. Polymorphism là gì?

```
Poly (nhiều) + Morph (hình dạng)
= 1 method, nhiều cách thực thi khác nhau
```

### 2. Hai loại Polymorphism

| | Compile-time | Runtime |
|---|---|---|
| **Cơ chế** | Method Overloading | Method Overriding |
| **Quyết định** | Lúc biên dịch | Lúc chạy |
| **Từ khóa** | Không cần | `virtual` + `override` |
| **Kế thừa?** | Không | Có |
| **Ví dụ** | `Add(int)`, `Add(double)` | `animal.Speak()` |

### 3. Biến kiểu CHA chứa object CON

```csharp
Animal animal = new Dog();    // ✅ Hợp lệ
animal.Speak();                // Gọi Dog.Speak() (runtime)
// animal.Fetch();             // ❌ Compiler không thấy Fetch()
```

### 4. Mảng đa hình

```csharp
Animal[] animals = { new Dog(), new Cat(), new Bird() };
foreach (Animal a in animals)
    a.Speak();  // Mỗi con kêu khác nhau!
```

### 5. Type checking & Casting

```csharp
// is — kiểm tra kiểu
if (animal is Dog) { }

// as — safe cast (trả null nếu thất bại)
Dog dog = animal as Dog;

// Pattern matching — is + cast + gán biến
if (animal is Dog d) { d.Fetch(); }

// Direct cast — nguy hiểm
Dog dog = (Dog)animal;  // Throws nếu sai kiểu!
```

### 6. Pattern trong switch

```csharp
string result = animal switch
{
    Dog d   => d.Name + " là chó",
    Cat c   => c.Name + " là mèo",
    _       => "Không biết"
};
```

---

## 🔑 Từ khóa quan trọng

| Từ khóa | Ý nghĩa | Ví dụ |
|---------|---------|-------|
| `virtual` | Cho phép override | `public virtual void Speak()` |
| `override` | Ghi đè method cha | `public override void Speak()` |
| `new` | Ẩn method cha (TRÁNH!) | `public new void Speak()` |
| `sealed` | Chặn override tiếp | `public sealed override void Speak()` |
| `is` | Kiểm tra kiểu | `animal is Dog` |
| `as` | Cast an toàn | `animal as Dog` |
| `base` | Gọi method cha | `base.Speak()` |

---

## ⚡ So sánh nhanh: `new` vs `override`

```
override → Polymorphism HOẠT ĐỘNG
  Animal a = new Dog();
  a.Speak();  // → Dog.Speak() ✅

new → Polymorphism KHÔNG hoạt động
  Animal a = new Dog();
  a.Speak();  // → Animal.Speak() ❌ (hiding!)
```

---

## 🧠 Sơ đồ quyết định

```
Cần gọi method trên object mà
không biết chính xác kiểu?
    │
    ▼
Dùng Polymorphism!
    │
    ├── Cùng tên, khác tham số?
    │   └── Method Overloading (compile-time)
    │
    └── Cùng tên, cùng tham số,
        behavior khác nhau ở class con?
        └── Method Overriding (runtime)
            └── virtual ở CHA + override ở CON

Cần kiểm tra kiểu cụ thể?
    │
    ├── Chỉ cần biết true/false → is
    ├── Cần cast an toàn        → as (+ check null)
    └── Cần check + dùng luôn   → pattern matching
```

---

## ❌ Những lỗi cần tránh

1. ❌ **Quên `virtual`** ở base class → `override` ở con sẽ lỗi compile
2. ❌ **Dùng `new` thay `override`** → Mất polymorphism, bug khó tìm
3. ❌ **Direct cast không kiểm tra** → `InvalidCastException` tại runtime
4. ❌ **Lạm dụng `is` kiểm tra kiểu** → Phá vỡ Open/Closed principle
5. ❌ **Quên gọi `base.Method()`** khi cần giữ logic của cha

---

## ✅ Checklist — Tự đánh giá

### Kiến thức cơ bản
- [ ] Hiểu Polymorphism = 1 interface, nhiều implementation
- [ ] Phân biệt compile-time (overloading) vs runtime (overriding)
- [ ] Hiểu `virtual` + `override` hoạt động thế nào
- [ ] Biết biến kiểu CHA có thể chứa object CON

### Kỹ năng thực hành
- [ ] Tạo mảng đa hình `Base[]` chứa nhiều kiểu CON
- [ ] Dùng `is` để kiểm tra kiểu
- [ ] Dùng `as` để safe cast
- [ ] Dùng pattern matching `if (x is Type name)`
- [ ] Viết method overloading đúng cách
- [ ] Gọi `base.Method()` khi cần

### Hiểu sâu
- [ ] Giải thích tại sao `new` (hiding) khác `override`
- [ ] Hiểu Open/Closed Principle
- [ ] Biết khi nào dùng `is/as` vs để polymorphism tự xử lý
- [ ] Viết switch expression với pattern matching

### Bài tập
- [ ] Hoàn thành 5/5 bài tập
- [ ] Hoàn thành Challenge: Zoo Simulation
- [ ] Thử thêm class con mới vào project mà KHÔNG sửa code cũ

---

## 🔗 Liên kết với các bài khác

```
Bài 20: Inheritance     →  Polymorphism XÂY DỰNG trên kế thừa
                            (virtual/override đã học ở Bài 20)

Bài 21: POLYMORPHISM    →  Trái tim của OOP
                            (1 method, nhiều behavior)

Bài 22: Abstraction     →  Sẽ nâng cấp polymorphism lên
                            abstract class + interface
```

---

## 💡 Mẹo ghi nhớ

```
🎭 Polymorphism = Diễn viên trên sân khấu
   - Kịch bản (method signature) giống nhau
   - Mỗi diễn viên (class con) diễn theo cách riêng
   - Đạo diễn (compiler) chỉ cần gọi "Diễn đi!"
   - Ai diễn gì → quyết định lúc biểu diễn (runtime)

🔌 USB Port = Polymorphism
   - 1 cổng (1 interface)
   - Nhiều thiết bị (nhiều implementation)
   - Cắm vào là chạy (runtime dispatch)
```

> **Bài tiếp theo:** Abstraction — abstract class, interface, và cách thiết kế hệ thống linh hoạt! 🚀
