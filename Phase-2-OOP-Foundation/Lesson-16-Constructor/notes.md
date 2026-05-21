# 📝 Lesson 16 — Notes: Constructor

---

## 🧠 Tóm tắt

```csharp
class X
{
    public X() { }                          // Default
    public X(int a) { }                     // Parameterized
    public X(int a, int b) : this(a) { }    // Chaining
}
var obj = new X(1, 2);                     // Gọi constructor
var obj2 = new X { Field = value };        // Object Initializer
```

### Quy tắc:
- Cùng tên class, không return type
- Chạy tự động khi `new`
- Viết constructor → mất default constructor
- Chaining `this(...)` tránh lặp code
- Validate trong constructor → object luôn hợp lệ

---

## ✅ Checklist

- [ ] Biết constructor cú pháp (cùng tên class, không return)
- [ ] Biết default constructor biến mất khi viết constructor riêng
- [ ] Biết overloading (nhiều constructor)
- [ ] Biết chaining `this(...)`
- [ ] Biết validate + throw trong constructor
- [ ] Biết Object Initializer `new X { A = 1 }`
- [ ] Biết kết hợp static field + constructor (auto ID)

---

## 🔗 Liên kết

```
Lesson 15: Class & Object
    ↓
Lesson 16: Constructor ← BẠN Ở ĐÂY
    ↓
Lesson 17: this
```
