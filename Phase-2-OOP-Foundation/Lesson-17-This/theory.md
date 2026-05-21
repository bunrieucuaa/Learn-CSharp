# 📘 Lesson 17 — `this` Keyword trong C#

---

## 1. `this` là gì?

`this` = **tham chiếu đến OBJECT HIỆN TẠI** (instance đang gọi method).

```csharp
class Student
{
    public string Name;

    public void SetName(string name)
    {
        this.Name = name;   // this.Name = field, name = parameter
        // Nếu không dùng this: Name = name; cũng OK khi tên khác nhau
    }
}

Student s = new Student();
s.SetName("Minh");
// Lúc này, "this" bên trong SetName() chính là object "s"
```

---

## 2. Khi nào PHẢI dùng `this`?

### 2.1 Phân biệt field và parameter cùng tên:

```csharp
class Product
{
    public string name;   // field
    public decimal price; // field

    public Product(string name, decimal price)
    {
        // name = name;      // ❌ BUG! Tự gán cho chính nó (parameter)!
        this.name = name;    // ✅ this.name = field, name = parameter
        this.price = price;
    }
}
```

> 🔑 Khi parameter và field **trùng tên** → PHẢI dùng `this` để phân biệt.

### 2.2 Constructor Chaining:

```csharp
class Employee
{
    public string Name;
    public decimal Salary;

    public Employee(string name, decimal salary)
    {
        Name = name; Salary = salary;
    }

    public Employee(string name) : this(name, 5000000m) { }
    //                              ↑ this() gọi constructor khác
    public Employee() : this("Unknown") { }
}
```

### 2.3 Trả về chính object (Method Chaining / Fluent API):

```csharp
class QueryBuilder
{
    private string table = "";
    private string condition = "";
    private string orderBy = "";

    public QueryBuilder From(string table)
    {
        this.table = table;
        return this;    // Trả về chính object → gọi tiếp method khác
    }

    public QueryBuilder Where(string condition)
    {
        this.condition = condition;
        return this;
    }

    public QueryBuilder OrderBy(string field)
    {
        this.orderBy = field;
        return this;
    }

    public string Build()
    {
        string sql = $"SELECT * FROM {table}";
        if (!string.IsNullOrEmpty(condition)) sql += $" WHERE {condition}";
        if (!string.IsNullOrEmpty(orderBy)) sql += $" ORDER BY {orderBy}";
        return sql;
    }
}

// Fluent API — gọi liên tiếp nhờ return this:
string query = new QueryBuilder()
    .From("Students")
    .Where("Score > 5")
    .OrderBy("Name")
    .Build();
// "SELECT * FROM Students WHERE Score > 5 ORDER BY Name"
```

### 2.4 Truyền object hiện tại cho method khác:

```csharp
class Player
{
    public string Name;
    public int Score;

    public Player(string name, int score)
    {
        Name = name; Score = score;
    }

    public void CompareWith(Player other)
    {
        if (this.Score > other.Score)
            Console.WriteLine($"{this.Name} thắng!");
        else if (this.Score < other.Score)
            Console.WriteLine($"{other.Name} thắng!");
        else
            Console.WriteLine("Hòa!");
    }

    public void RegisterTo(Tournament tournament)
    {
        tournament.AddPlayer(this);  // Truyền chính mình
    }
}
```

---

## 3. Khi nào KHÔNG cần `this`?

```csharp
class Student
{
    public string Name;
    public int Age;

    // Tên KHÔNG trùng → không cần this
    public void SetInfo(string studentName, int studentAge)
    {
        Name = studentName;   // OK — rõ ràng Name là field
        Age = studentAge;     // OK — rõ ràng Age là field
    }

    // Methods không dùng parameter trùng tên field
    public string GetGrade() => Score >= 5 ? "Đậu" : "Rớt";  // Không cần this
}
```

> 💡 **Convention C#**: Nhiều dev dùng `this` trong constructor (vì hay trùng tên), bỏ `this` ở method khác (cho gọn).

---

## 4. So sánh với JavaScript

```javascript
// JS — this phức tạp hơn C#!
class Student {
    constructor(name) {
        this.name = name;  // Giống C#
    }
    greet() {
        console.log(`Hi ${this.name}`);
    }
}

// ⚠️ JS: this thay đổi theo cách gọi!
const s = new Student("Minh");
s.greet();              // "Hi Minh" ✅
const fn = s.greet;
fn();                   // "Hi undefined" ❌ — this bị mất!
```

```csharp
// C# — this LUÔN là instance hiện tại, không bao giờ thay đổi!
Student s = new Student("Minh");
s.Greet();              // Luôn OK
```

| | C# | JavaScript |
|--|-----|-----------|
| `this` trỏ đến | Instance hiện tại (cố định) | Tùy context (thay đổi!) |
| Trong constructor | Bắt buộc khi trùng tên | Luôn dùng `this.x` |
| Arrow function | Không có (chỉ class method) | `this` từ scope cha |
| Method chaining | `return this` | `return this` |

> 🔑 **C# đơn giản hơn JS**: `this` luôn là object hiện tại, không bao giờ thay đổi context.

---

## 5. Best Practices

1. **Dùng `this`** khi field và parameter trùng tên (constructor)
2. **Bỏ `this`** khi không cần — code gọn hơn
3. **`return this`** cho Fluent API / Method Chaining
4. **`this(...)` constructor chaining** tránh lặp code
5. **Convention**: trong team, thống nhất dùng/không dùng `this` ở methods
