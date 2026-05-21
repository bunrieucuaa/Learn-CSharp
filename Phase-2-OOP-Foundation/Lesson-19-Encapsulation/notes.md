# 📝 Lesson 19 — Notes: Encapsulation (Tính đóng gói)

---

## 🧠 Tóm tắt

```
Encapsulation = Đóng gói DATA + BEHAVIOR vào class
              + Ẩn implementation details
              + Chỉ expose PUBLIC API
```

### 3 cấp độ Encapsulation:

```csharp
// Level 1: Field Encapsulation — private field + property
private decimal _salary;
public decimal Salary
{
    get => _salary;
    set => _salary = value >= 0 ? value : throw new ArgumentException();
}

// Level 2: Method Encapsulation — private helper methods
public bool ProcessOrder(Order order)
{
    if (!ValidateOrder(order)) return false;   // private
    return ChargePayment(CalculateTotal());     // private
}

// Level 3: Class Encapsulation — internal class
internal class DatabaseHelper { /* chỉ trong project */ }
```

### Property patterns:

```csharp
// init — gán lúc tạo, sau đó readonly
public string Username { get; init; }

// required — BẮT BUỘC gán khi tạo
public required string Email { get; set; }

// Immutable — readonly, "thay đổi" bằng tạo mới
public decimal Amount { get; }
public Money Add(decimal v) => new Money(Amount + v, Currency);
```

### Encapsulate collection:

```csharp
private readonly List<string> _items = new();
public IReadOnlyList<string> Items => _items.AsReadOnly();  // ✅ Readonly
// KHÔNG: public List<string> Items { get; set; }            // ❌ Sửa được!
```

### So sánh C# vs JS:

| | C# | JavaScript |
|--|----|-----------| 
| Private | `private` keyword | `#` prefix |
| Property | `get; set;` built-in | `get/set` methods |
| init-only | `{ get; init; }` | ❌ |
| required | `required` modifier | ❌ |
| Enforce | **Compile time** | Runtime |

### Quy tắc vàng:

```
1. Field → luôn PRIVATE
2. Property → validate trong setter
3. Collection → expose IReadOnly
4. Logic phức tạp → private helper methods
5. Immutable khi có thể
6. API tối giản — chỉ expose cái CẦN THIẾT
```

---

## ✅ Checklist

- [ ] Hiểu Encapsulation = bundling + information hiding
- [ ] Biết 3 levels: field, method, class encapsulation
- [ ] Biết validation property (full property + setter check)
- [ ] Biết computed property (readonly, tự tính)
- [ ] Biết `init` accessor — gán lúc tạo, readonly sau đó
- [ ] Biết `required` modifier — bắt buộc gán
- [ ] Biết Immutable pattern — readonly, tạo mới khi "thay đổi"
- [ ] Biết encapsulate collection (IReadOnlyList)
- [ ] Hiểu Information Hiding — ẩn HOW, expose WHAT
- [ ] Hiểu TẠI SAO encapsulation (maintainability, flexibility)
- [ ] Biết sự khác biệt giữa "chỉ private" vs encapsulation thật sự
- [ ] Biết best practices (API tối giản, validate, naming)

---

## 🔗 Liên kết

```
Lesson 18: Access Modifier & Property (tools)
    ↓
Lesson 19: Encapsulation ← BẠN Ở ĐÂY (trụ cột OOP #1)
    ↓
Lesson 20: Inheritance (trụ cột OOP #2)
    ↓
Lesson 21: Polymorphism (trụ cột OOP #3)
    ↓
Lesson 22: Abstraction (trụ cột OOP #4)
```

> 📌 **Lesson 18 dạy TOOLS** (private, property, get/set).
> **Lesson 19 dạy DESIGN** — TẠI SAO dùng, CÁCH dùng đúng, patterns.
> Bạn đã master trụ cột #1! Tiếp theo: **Inheritance** — kế thừa class!
