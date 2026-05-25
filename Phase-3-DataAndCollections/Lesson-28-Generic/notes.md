# 📝 Bài 28: Generic\<T> — Ghi Chú & Tóm Tắt

---

## 🔑 Tóm tắt nhanh

### Generic = Viết 1 lần, dùng cho MỌI kiểu

```
  ╔═══════════════════════════════════════════════╗
  ║  KHÔNG CÓ GENERIC:                           ║
  ║  SwapInt()   → cho int                        ║
  ║  SwapString() → cho string                    ║
  ║  SwapDouble() → cho double                    ║
  ║  → 3 method, cùng 1 logic! 😱                ║
  ║                                               ║
  ║  CÓ GENERIC:                                  ║
  ║  Swap<T>()   → cho TẤT CẢ!                   ║
  ║  → 1 method, type safe! ✅                    ║
  ╚═══════════════════════════════════════════════╝
```

---

## 📋 Cheat Sheet

### Generic Method

```csharp
// Khai báo
ReturnType MethodName<T>(T param) { }
ReturnType MethodName<T>(T param) where T : constraint { }

// Nhiều type parameters
ReturnType MethodName<T1, T2>(T1 a, T2 b) { }

// Sử dụng
Swap<int>(ref x, ref y);     // Chỉ định T
Swap(ref x, ref y);           // Compiler tự suy luận
```

### Generic Class

```csharp
// Khai báo
class ClassName<T>
{
    private T _value;
    public T Value { get; set; }
    public ClassName(T value) { _value = value; }
}

// Nhiều type parameters
class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }
}

// Sử dụng
var box = new ClassName<int>(42);
var pair = new Pair<string, int>("Minh", 25);
```

### Generic Interface

```csharp
// Khai báo
interface IRepository<T> where T : IEntity
{
    void Add(T item);
    T? GetById(int id);
    List<T> GetAll();
}

// Implement
class StudentRepo : IRepository<Student>
{
    public void Add(Student item) { }
    public Student? GetById(int id) { return null; }
    public List<Student> GetAll() { return new(); }
}
```

### Constraints

```csharp
where T : struct            // Value type (int, double, struct)
where T : class             // Reference type (string, class)
where T : new()             // Có constructor không tham số
where T : IComparable<T>    // Implement interface
where T : BaseClass         // Kế thừa class
where T : notnull           // Không được null

// Kết hợp (thứ tự quan trọng!)
where T : class, IEntity, new()
//        ↑       ↑        ↑
//     đầu tiên  giữa   cuối cùng
```

---

## 🔄 So sánh nhanh

| Đặc điểm | Dùng `object` | Dùng Generic\<T> |
|----------|:-------------:|:-----------------:|
| Type Safety | ❌ Runtime error | ✅ Compile error |
| Performance | ❌ Boxing/Unboxing | ✅ Không boxing |
| Code Reuse | ⚠️ Có nhưng unsafe | ✅ Safe & reusable |
| IntelliSense | ❌ Không có | ✅ Đầy đủ |
| Cast thủ công | ✅ Cần cast | ❌ Không cần |

---

## ⚡ So sánh JS vs C#

```javascript
// JavaScript — không có generics
function findMax(a, b) {
    return a > b ? a : b;     // Không kiểm tra kiểu!
}
findMax(1, "hello");          // Hợp lệ?! 😱

// TypeScript — có generics (giống C#)
function findMax<T extends Comparable>(a: T, b: T): T {
    return a > b ? a : b;
}
```

```csharp
// C# — generic với constraints
T FindMax<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}
// FindMax(1, "hello"); // ❌ Compile error! Type safe!
```

> 💡 C# generic được **giữ lại ở runtime** (reified) → nhanh hơn TypeScript (erased at compile)!

---

## ⚠️ Lỗi thường gặp

### 1. Quên constraint khi cần so sánh
```csharp
// ❌ SAI — T không có CompareTo
T FindMax<T>(T a, T b)
{
    return a.CompareTo(b) >= 0 ? a : b; // 💥 Compile error
}

// ✅ ĐÚNG — thêm constraint
T FindMax<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b; // ✅ OK
}
```

### 2. Gọi new T() mà không có constraint
```csharp
// ❌ SAI
T Create<T>()
{
    return new T(); // 💥 Compile error — T có thể không có constructor!
}

// ✅ ĐÚNG
T Create<T>() where T : new()
{
    return new T(); // ✅ OK — đảm bảo T có constructor()
}
```

### 3. So sánh T với null mà không có constraint
```csharp
// ❌ SAI — T có thể là value type (int không thể null)
void Process<T>(T item)
{
    if (item == null) return; // ⚠️ Warning
}

// ✅ ĐÚNG — thêm class constraint
void Process<T>(T item) where T : class
{
    if (item == null) return; // ✅ OK — T là reference type
}
```

### 4. Thứ tự constraints sai
```csharp
// ❌ SAI — new() phải ở cuối, class/struct phải ở đầu
where T : new(), IEntity, class  // 💥 Compile error

// ✅ ĐÚNG
where T : class, IEntity, new()  // ✅ class → interface → new()
```

### 5. Dùng generic không cần thiết
```csharp
// ❌ Overkill — nếu chỉ dùng cho int
class IntCalculator<T>
{
    public T Add(T a, T b) { ... } // Cần constraint gì?
}

// ✅ Đơn giản hơn — dùng int trực tiếp
class IntCalculator
{
    public int Add(int a, int b) => a + b;
}
```

---

## 🎯 Quy tắc đặt tên Type Parameter

| Tên | Ý nghĩa | Ví dụ |
|-----|---------|-------|
| `T` | Type chung | `List<T>`, `Stack<T>` |
| `TKey` | Kiểu key | `Dictionary<TKey, TValue>` |
| `TValue` | Kiểu value | `Dictionary<TKey, TValue>` |
| `TResult` | Kiểu kết quả | `Func<T, TResult>` |
| `TSource` | Kiểu nguồn | `Select<TSource, TResult>` |
| `TEntity` | Kiểu entity | `Repository<TEntity>` |
| `TInput` | Kiểu input | `IConverter<TInput, TOutput>` |
| `TOutput` | Kiểu output | `IConverter<TInput, TOutput>` |

> 📌 Luôn bắt đầu bằng **T viết hoa** — convention của C#.

---

## 🗺️ Bản đồ kiến thức

```
  Generic<T>
  │
  ├── Generic Method
  │   ├── Swap<T>(ref T, ref T)
  │   ├── FindMax<T>(T, T)
  │   └── PrintAll<T>(T[])
  │
  ├── Generic Class
  │   ├── Box<T>
  │   ├── Pair<T1, T2>
  │   ├── Result<T>
  │   └── Repository<T>
  │
  ├── Generic Interface
  │   ├── IRepository<T>
  │   ├── IConverter<TIn, TOut>
  │   └── IComparer<T>
  │
  ├── Constraints
  │   ├── where T : struct
  │   ├── where T : class
  │   ├── where T : new()
  │   ├── where T : IComparable<T>
  │   └── where T : BaseClass
  │
  ├── Built-in Generics
  │   ├── List<T>, Dictionary<K,V>
  │   ├── Stack<T>, Queue<T>
  │   ├── Func<T>, Action<T>
  │   └── Nullable<T> (int?)
  │
  └── Advanced
      ├── Covariance (out T)
      └── Contravariance (in T)
```

---

## ✅ Checklist kiến thức

| # | Nội dung | Hiểu? |
|---|---------|-------|
| 1 | Vấn đề: code lặp cho mỗi kiểu | ⬜ |
| 2 | Generic method: `<T>` syntax | ⬜ |
| 3 | Generic class: `class Box<T>` | ⬜ |
| 4 | Generic interface: `IRepo<T>` | ⬜ |
| 5 | Nhiều type params: `<T1, T2>` | ⬜ |
| 6 | Constraint: `where T : struct` | ⬜ |
| 7 | Constraint: `where T : class` | ⬜ |
| 8 | Constraint: `where T : new()` | ⬜ |
| 9 | Constraint: `where T : IComparable<T>` | ⬜ |
| 10 | Kết hợp nhiều constraints | ⬜ |
| 11 | Type inference (tự suy luận T) | ⬜ |
| 12 | Generic vs object (type safety, performance) | ⬜ |
| 13 | Convention đặt tên: T, TKey, TValue | ⬜ |
| 14 | Covariance/Contravariance (cơ bản) | ⬜ |
| 15 | Tự viết generic collection | ⬜ |

> 🎯 Đạt **12/15** = Sẵn sàng cho bài tiếp theo!

---

## 📚 Bài tiếp theo

**Bài 29** — Tiếp tục khám phá Collections nâng cao:
- IEnumerable\<T> & IEnumerator\<T>
- yield return
- LINQ cơ bản
- Extension methods
