# 📝 Lesson 18 — Notes: Access Modifier

---

## 🧠 Tóm tắt

```csharp
// Access Modifiers
public     // 🌍 Tất cả thấy
private    // 🔒 Chỉ trong class (MẶC ĐỊNH)
protected  // 🛡️ Class + con (Inheritance)
internal   // 📦 Cùng project

// Auto Property
public string Name { get; set; }
public int Id { get; private set; }  // Đọc ngoài, ghi trong

// Full Property (validate)
private int _age;
public int Age
{
    get => _age;
    set => _age = value >= 0 ? value : throw new ArgumentException();
}

// Computed Property (readonly)
public string Grade => Score >= 5 ? "Pass" : "Fail";
```

### Quy tắc vàng:
- **Field** → `private` (luôn luôn!)
- **Property** → `public` (giao diện cho bên ngoài)
- **Method nội bộ** → `private`
- **Method giao diện** → `public`

---

## ✅ Checklist

- [ ] Biết `public`, `private`, `protected`, `internal`
- [ ] Biết mặc định là `private`
- [ ] Biết Auto Property: `{ get; set; }`
- [ ] Biết Full Property: `get => _field; set { validate }`
- [ ] Biết `private set`
- [ ] Biết Computed Property: `=>`
- [ ] Biết convention: `_camelCase` private, `PascalCase` property
- [ ] Biết quy tắc: field private, expose qua property

## 🔗 Liên kết

```
Lesson 17: this
    ↓
Lesson 18: Access Modifier ← BẠN Ở ĐÂY
    ↓
Lesson 19+: 4 tính chất OOP (Encapsulation, Inheritance, Polymorphism, Abstraction)
```

> 📌 **Bạn đã sẵn sàng cho 4 tính chất OOP!** Access Modifier + Property chính là nền tảng cho Encapsulation (tính đóng gói).
