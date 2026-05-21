# 📘 Lesson 15 — Class & Object (Lớp và Đối tượng) trong C#

---

## 1. Vấn đề: Tại sao cần Class?

### Từ Phase 1, bạn đã dùng mảng song song:

```csharp
// ❌ Mảng song song — khó quản lý, dễ lệch index
string[] names = new string[50];
double[] scores = new double[50];
int[] ages = new int[50];
bool[] isActive = new bool[50];
int count = 0;

// Thêm 1 sinh viên = thêm vào 4 mảng riêng!
names[count] = "Minh";
scores[count] = 8.5;
ages[count] = 22;
isActive[count] = true;
count++;
```

**Vấn đề:**
- 4 mảng riêng → phải đồng bộ index → dễ bug
- Thêm thuộc tính mới → thêm 1 mảng nữa
- Xóa 1 phần tử → phải dịch 4 mảng
- Không rõ ràng: `names[3]` liên quan gì `scores[3]`?

### Giải pháp: Gom lại thành 1 "đối tượng":

```csharp
// ✅ Class — gom TẤT CẢ thuộc tính liên quan vào 1 chỗ
class Student
{
    public string Name;
    public double Score;
    public int Age;
    public bool IsActive;
}

Student s = new Student();
s.Name = "Minh";
s.Score = 8.5;
s.Age = 22;
s.IsActive = true;
```

> 🔑 **Class = bản thiết kế**. **Object = sản phẩm tạo từ bản thiết kế**.

---

## 2. Class là gì? Object là gì?

### Analogy:

| Concept | Ví dụ đời thực |
|---------|---------------|
| **Class** (bản thiết kế) | Bản vẽ nhà — mô tả: 3 phòng, 2 tầng, 1 garage |
| **Object** (instance) | Ngôi nhà THẬT được xây — mỗi nhà có địa chỉ riêng, màu sơn riêng |
| 1 class → nhiều objects | 1 bản vẽ → xây 100 ngôi nhà, mỗi nhà khác nhau |

```
Class "Student" (bản thiết kế)
┌─────────────────┐
│ Fields:          │
│  - Name          │
│  - Score         │
│  - Age           │
│                  │
│ Methods:         │
│  - GetGrade()    │
│  - PrintInfo()   │
│  - IsPass()      │
└─────────────────┘
        │
        │  new Student()
        ▼
┌──────────────┐  ┌──────────────┐  ┌──────────────┐
│ Object s1    │  │ Object s2    │  │ Object s3    │
│ Name="Minh"  │  │ Name="Hùng"  │  │ Name="Lan"   │
│ Score=8.5    │  │ Score=6.0    │  │ Score=9.2    │
│ Age=22       │  │ Age=25       │  │ Age=20       │
└──────────────┘  └──────────────┘  └──────────────┘
  Mỗi object có DATA RIÊNG, nhưng CÙNG cấu trúc
```

---

## 3. Cú pháp Class cơ bản

```csharp
// ── Định nghĩa Class ──
class Student
{
    // Fields (thuộc tính — data)
    public string Name = "";
    public int Age = 0;
    public double Score = 0;

    // Methods (hành vi — logic)
    public string GetGrade()
    {
        return Score switch
        {
            >= 8 => "Giỏi",
            >= 6.5 => "Khá",
            >= 5 => "TB",
            _ => "Yếu"
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Name}, {Age} tuổi, {Score:F1} → {GetGrade()}");
    }

    public bool IsPass() => Score >= 5;
}
```

```csharp
// ── Tạo Object (instance) ──
Student s1 = new Student();   // Tạo object mới trên Heap
s1.Name = "Minh";
s1.Age = 22;
s1.Score = 8.5;

Student s2 = new Student();
s2.Name = "Hùng";
s2.Age = 25;
s2.Score = 6.0;

// Gọi methods
s1.PrintInfo();  // "Minh, 22 tuổi, 8.5 → Giỏi"
s2.PrintInfo();  // "Hùng, 25 tuổi, 6.0 → TB"

Console.WriteLine(s1.IsPass());  // True
Console.WriteLine(s2.IsPass());  // True
```

---

## 4. Memory — Object nằm ở đâu?

```csharp
Student s1 = new Student();
s1.Name = "Minh";
s1.Score = 8.5;
```

```
STACK                          HEAP
┌──────────────┐              ┌────────────────────┐
│ s1 = 0x100   ┼─────────────▶│ Student object     │ 0x100
│              │              │   Name = "Minh"    │
│              │              │   Age = 0           │
│              │              │   Score = 8.5       │
│              │              │   GetGrade()        │
│              │              │   PrintInfo()       │
└──────────────┘              └────────────────────┘
  Stack chứa                    Heap chứa
  REFERENCE (địa chỉ)          DATA THẬT (object)
```

> 🔑 Class là **reference type** → `new` tạo object trên Heap, biến trên Stack chỉ lưu địa chỉ.

### Copy object = chia sẻ reference (giống array!):

```csharp
Student s1 = new Student();
s1.Name = "Minh";

Student s2 = s1;      // s2 trỏ CÙNG object s1
s2.Name = "Hùng";     // Sửa qua s2

Console.WriteLine(s1.Name);  // "Hùng" ← ĐÃ ĐỔI!
```

---

## 5. Field vs Method — Data vs Behavior

```csharp
class Product
{
    // ═══ FIELDS (data — danh từ) ═══
    public string Name = "";
    public decimal Price = 0;
    public int Stock = 0;

    // ═══ METHODS (behavior — động từ) ═══
    public decimal GetTotal(int quantity) => Price * quantity;
    public bool IsInStock() => Stock > 0;

    public bool Sell(int quantity)
    {
        if (quantity <= 0 || quantity > Stock) return false;
        Stock -= quantity;
        return true;
    }

    public void Restock(int quantity)
    {
        if (quantity > 0) Stock += quantity;
    }

    public void PrintInfo()
    {
        string status = IsInStock() ? $"Còn {Stock}" : "Hết hàng";
        Console.WriteLine($"{Name} — {Price:N0} VNĐ [{status}]");
    }
}
```

> 🔑 **Field = danh từ** (tên, giá, tồn kho). **Method = động từ** (bán, nhập, hiển thị).

---

## 6. Instance vs Static — Trong Context Class

```csharp
class Counter
{
    // STATIC — thuộc CLASS (chia sẻ)
    public static int TotalCreated = 0;

    // INSTANCE — thuộc MỖI object (riêng biệt)
    public string Label = "";
    public int Value = 0;

    // INSTANCE method — gọi trên object: obj.Increment()
    public void Increment() => Value++;

    // STATIC method — gọi trên class: Counter.GetTotal()
    public static int GetTotal() => TotalCreated;
}

// Dùng:
Counter c1 = new Counter();
c1.Label = "A";
c1.Increment();              // Instance — trên c1

Counter c2 = new Counter();
c2.Label = "B";
c2.Increment();
c2.Increment();

Console.WriteLine(c1.Value);  // 1 — riêng c1
Console.WriteLine(c2.Value);  // 2 — riêng c2
Console.WriteLine(Counter.TotalCreated);  // Static — chung class
```

---

## 7. So sánh với JavaScript

```javascript
// JS — class (ES6+)
class Student {
    constructor(name, score) {
        this.name = name;     // Không khai báo kiểu
        this.score = score;
    }

    getGrade() {
        if (this.score >= 8) return "Giỏi";
        if (this.score >= 5) return "TB";
        return "Yếu";
    }
}

let s = new Student("Minh", 8.5);
s.getGrade();  // "Giỏi"
```

```csharp
// C# — class
class Student
{
    public string Name = "";       // Phải khai báo kiểu
    public double Score = 0;

    public string GetGrade()       // return type bắt buộc
    {
        return Score >= 8 ? "Giỏi" : Score >= 5 ? "TB" : "Yếu";
    }
}

Student s = new Student();  // Phải new riêng
s.Name = "Minh";            // Gán riêng
s.Score = 8.5;
s.GetGrade();               // "Giỏi"
```

| | C# | JavaScript |
|--|-----|-----------|
| Khai báo field | Bắt buộc kiểu: `public string Name` | Không cần: `this.name` |
| Default value | Tường minh: `= ""` | `undefined` |
| Access modifier | Có: `public`, `private`... | Có: `#` (private), mặc định public |
| Return type | Bắt buộc: `string GetGrade()` | Không cần |
| Constructor | Sẽ học Lesson 16 | `constructor()` |
| `new` | **Bắt buộc** | Bắt buộc |
| Naming | PascalCase: `GetGrade()` | camelCase: `getGrade()` |

---

## 8. Mảng Object

```csharp
// Mảng chứa objects — thay thế mảng song song!
Student[] students = new Student[30];
int count = 0;

// Thêm
students[count] = new Student();
students[count].Name = "Minh";
students[count].Score = 8.5;
count++;

// Duyệt
for (int i = 0; i < count; i++)
{
    students[i].PrintInfo();
}
```

> 🔑 **1 mảng object** thay thế **N mảng song song** → gọn, rõ, không lệch index!

---

## 9. Sai lầm phổ biến

### ❌ Quên `new`:
```csharp
Student s;           // Chỉ khai báo reference (null!)
// s.Name = "Minh";  // ❌ NullReferenceException!

Student s = new Student();  // ✅ Phải new
s.Name = "Minh";
```

### ❌ Nhầm class = value type:
```csharp
Student a = new Student();
a.Name = "Minh";
Student b = a;        // ⚠️ Cùng trỏ 1 object!
b.Name = "Hùng";
Console.WriteLine(a.Name);  // "Hùng" — không phải "Minh"!
```

### ❌ Mảng object quên new từng phần tử:
```csharp
Student[] arr = new Student[5];   // 5 slot, nhưng mỗi slot = null!
// arr[0].Name = "Minh";          // ❌ NullReferenceException!

arr[0] = new Student();            // ✅ Phải new từng object
arr[0].Name = "Minh";
```

---

## 10. Best Practices

1. **1 class = 1 khái niệm** — Student, Product, Order (không gom lung tung)
2. **Field = data**, **Method = behavior** — phân biệt rõ
3. **PascalCase** cho tên class và method: `Student`, `GetGrade()`
4. **camelCase** cho tham số và biến local: `studentName`, `totalScore`
5. **Mỗi object nên tự mô tả được** — có `PrintInfo()` hoặc `ToString()`
6. **Tránh field public** (sẽ học `private` + Property ở Lesson 18)
7. **Dùng `new`** khi tạo object — nhớ reference type!
