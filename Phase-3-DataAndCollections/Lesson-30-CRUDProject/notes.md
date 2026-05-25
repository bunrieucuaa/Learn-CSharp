# 📝 Bài 30: CRUD Console Project — Tóm tắt & Phase 3 Recap

---

## 📋 Tóm tắt Project

### Kiến trúc 3 tầng

```
Model → Repository → Service → UI → Program.cs

Model:      Định nghĩa data + validation
Repository: CRUD operations + storage (List<T> + Dictionary)
Service:    Business logic + query
UI:         Input/Output + menu
Program:    Composition root (tạo + kết nối)
```

### CRUD Pattern

```csharp
// Mỗi entity đều có 4 thao tác cơ bản:
interface IRepository<T>
{
    bool Add(T entity);                    // C — Create
    T? GetById(int id);                    // R — Read
    IEnumerable<T> GetAll();               // R — Read all
    bool Update(T entity);                 // U — Update
    bool Delete(int id);                   // D — Delete
}
```

### Generic Repository — Tái sử dụng

```csharp
// 1 implementation, NHIỀU entity types:
GenericRepository<Student>  repo1 = new();
GenericRepository<Book>     repo2 = new();
GenericRepository<Product>  repo3 = new();
// Cùng code Add/Get/Update/Delete cho tất cả!
```

### Dependency Injection cơ bản

```csharp
// Service NHẬN repository qua constructor (không tự tạo)
class StudentService
{
    private readonly IRepository<Student> _repo;

    public StudentService(IRepository<Student> repo)  // ← DI
    {
        _repo = repo;
    }
}

// Program.cs — wiring
IRepository<Student> repo = new GenericRepository<Student>();
StudentService service = new StudentService(repo);
ConsoleUI ui = new ConsoleUI(service);
```

---

## 📊 PHASE 3 — Tổng kết hoàn chỉnh

### Các bài đã học

```
Phase 3: Data & Collections
├── Bài 25: List<T>          → Dynamic array, CRUD operations
├── Bài 26: Dictionary<K,V>  → Key-value pairs, fast lookup
├── Bài 27: Stack & Queue    → LIFO, FIFO data structures
├── Bài 28: Generics         → Type-safe reusable code
├── Bài 29: IEnumerable<T>   → Iterator pattern, yield, deferred
└── Bài 30: CRUD Project     → Tổng hợp tất cả
```

### Bảng kiến thức tích lũy Phase 1→3

```
┌───────────────────────────────────────────────────────────────┐
│  PHASE 1: C# Fundamentals (Bài 1-14)                        │
│  ✅ Variables, Types, Operators                               │
│  ✅ If/Else, Switch, Loops                                   │
│  ✅ Methods, Scope, Static                                    │
│  ✅ Memory, Arrays, Strings                                   │
│  ✅ Exception Handling                                        │
├───────────────────────────────────────────────────────────────┤
│  PHASE 2: OOP Foundation (Bài 15-24)                         │
│  ✅ Class, Object, Constructor, this                          │
│  ✅ Access Modifiers, Encapsulation                           │
│  ✅ Inheritance, Polymorphism                                 │
│  ✅ Abstraction, Interface, Abstract Class                    │
├───────────────────────────────────────────────────────────────┤
│  PHASE 3: Data & Collections (Bài 25-30)                     │
│  ✅ List<T>          — Dynamic array                         │
│  ✅ Dictionary<K,V>  — Hash table                            │
│  ✅ Stack<T>, Queue<T> — LIFO, FIFO                         │
│  ✅ Generics<T>      — Type parameters                       │
│  ✅ IEnumerable<T>   — Iterator pattern                      │
│  ✅ CRUD Project     — Real-world application                │
└───────────────────────────────────────────────────────────────┘
```

---

## 🔑 Collection cheat sheet

```
Cần gì?                              → Dùng:
──────────────────────────────────    ────────────────
Danh sách có thứ tự, CRUD             List<T>
Tra cứu nhanh theo key                Dictionary<K,V>
Undo/Redo, xử lý lồng nhau           Stack<T>
Hàng đợi, xử lý tuần tự              Queue<T>
Duyệt lazy, pipeline                  IEnumerable<T> + yield
Tái sử dụng cho nhiều types           Generic<T>
```

---

## ✅ Checklist — Phase 3 hoàn chỉnh

### List\<T\> (Bài 25)
- [ ] Tạo List, Add, Remove, Insert, Contains, IndexOf
- [ ] Sort, Reverse, Find, FindAll
- [ ] Duyệt bằng foreach và for

### Dictionary\<K,V\> (Bài 26)
- [ ] Add, ContainsKey, TryGetValue
- [ ] Duyệt KeyValuePair
- [ ] Dùng cho fast lookup (O(1))

### Stack & Queue (Bài 27)
- [ ] Stack: Push, Pop, Peek (LIFO)
- [ ] Queue: Enqueue, Dequeue, Peek (FIFO)
- [ ] Hiểu use case cho mỗi loại

### Generics (Bài 28)
- [ ] Viết generic class và method
- [ ] Hiểu type constraint: where T : class
- [ ] Dùng generic cho repository pattern

### IEnumerable\<T\> (Bài 29)
- [ ] Hiểu foreach → IEnumerator
- [ ] Dùng yield return tạo iterator
- [ ] Hiểu deferred execution
- [ ] Implement IEnumerable cho class riêng

### CRUD Project (Bài 30)
- [ ] Hiểu kiến trúc 3 tầng
- [ ] Viết Generic Repository
- [ ] Implement CRUD hoàn chỉnh
- [ ] Tách Model / Repository / Service / UI
- [ ] Dùng DI cơ bản (constructor injection)

### Challenge
- [ ] Hoàn thành Library Management System
- [ ] Sử dụng đủ: List, Dictionary, Generics, IEnumerable, OOP, Interfaces

---

## 🔮 Phase 4: LINQ & Clean Code (Sắp tới!)

```
Bạn đã sẵn sàng cho LINQ!

Phase 4 sẽ dạy:
├── LINQ Basics     → Query syntax, method syntax
├── Filtering       → Where, OfType
├── Projection      → Select, SelectMany
├── Sorting         → OrderBy, ThenBy
├── Grouping        → GroupBy, ToLookup
├── Extension Methods → Mở rộng type có sẵn
└── Clean Code      → Naming, SOLID review

LINQ hoạt động HOÀN TOÀN trên IEnumerable<T>!
→ Kiến thức Bài 29 là NỀN TẢNG cho LINQ!

Ví dụ preview:
// Thay vì code dài:
List<Student> result = new List<Student>();
foreach (Student s in students)
    if (s.Score >= 8.0)
        result.Add(s);
result.Sort((a, b) => b.Score.CompareTo(a.Score));

// LINQ — 1 dòng:
var result = students
    .Where(s => s.Score >= 8.0)
    .OrderByDescending(s => s.Score);
```

---

## 💡 Mẹo ghi nhớ Phase 3

```
📦 List<T> = Hộp đồ chơi
   - Bỏ vào, lấy ra, sắp xếp, tìm kiếm
   - Có thứ tự, có index

📖 Dictionary = Danh bạ điện thoại
   - Biết tên → tìm SĐT ngay lập tức
   - Key unique, Value bất kỳ

📚 Stack = Chồng sách
   - Đặt lên trên, lấy từ trên → LIFO
   - Undo/Redo

🎫 Queue = Hàng đợi mua vé
   - Vào cuối hàng, ra đầu hàng → FIFO
   - Task processing

🔧 Generic = Khuôn đúc
   - 1 khuôn, đổ nhiều loại nguyên liệu
   - Repository<Student>, Repository<Book>...

🎣 IEnumerable = Cần câu
   - Câu từng con, không vớt cả ao
   - Lazy, tiết kiệm, pipeline

🏗️ CRUD Project = Nhà hoàn chỉnh
   - Model = vật liệu
   - Repository = nền móng
   - Service = cấu trúc
   - UI = nội thất
```

---

> **Chúc mừng hoàn thành Phase 3! 🎉 Sẵn sàng cho Phase 4 — LINQ! 🚀**

---

*"Data structures + algorithms + architecture = real software."* 🎯
