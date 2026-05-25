# 📚 Bài 28: Generic\<T> — Viết Code Một Lần, Dùng Cho Mọi Kiểu

## 🎯 Mục tiêu bài học
- Hiểu vấn đề mà Generic giải quyết (code trùng lặp)
- Viết generic method, generic class, generic interface
- Sử dụng constraints để giới hạn kiểu T
- Phân biệt generic tự viết vs built-in
- Hiểu cơ bản về Covariance/Contravariance

---

## 1. Vấn đề: Code lặp cho mỗi kiểu dữ liệu

### ❌ Không có Generic — phải viết LẶP LẠI:

```csharp
// Muốn swap 2 số int
void SwapInt(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

// Muốn swap 2 string → viết LẠI y hệt!
void SwapString(ref string a, ref string b)
{
    string temp = a;
    a = b;
    b = temp;
}

// Muốn swap 2 double → LẠI viết nữa!
void SwapDouble(ref double a, ref double b)
{
    double temp = a;
    a = b;
    b = temp;
}

// 😱 Cùng logic, chỉ khác KIỂU! → Lặp 3 lần!
```

### ❌ Dùng `object` — mất type safety:

```csharp
void SwapObject(ref object a, ref object b)
{
    object temp = a;
    a = b;
    b = temp;
}

// Vấn đề:
object x = 10;
object y = "hello";
SwapObject(ref x, ref y);  // Swap int với string?! → Không lỗi compile! 😱
// Boxing/Unboxing → chậm
```

```
  ┌──────────────────────────────────────────────┐
  │  VẤN ĐỀ KHI KHÔNG CÓ GENERIC:              │
  │                                              │
  │  1. Code lặp lại (DRY violation)             │
  │  2. Hoặc dùng object → mất type safety       │
  │  3. Boxing/Unboxing → giảm performance       │
  │  4. Phải cast thủ công → dễ lỗi runtime      │
  └──────────────────────────────────────────────┘
```

---

## 2. Generic = Viết 1 lần, dùng cho MỌI kiểu

### ✅ Giải pháp — Generic method:

```csharp
// T là "type parameter" — placeholder cho kiểu thực tế
void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

// Sử dụng:
int x = 1, y = 2;
Swap<int>(ref x, ref y);           // T = int
Swap(ref x, ref y);                // Compiler tự suy luận T = int

string a = "Hello", b = "World";
Swap<string>(ref a, ref b);        // T = string
Swap(ref a, ref b);                // Compiler tự suy luận T = string

double m = 1.5, n = 2.5;
Swap(ref m, ref n);                // T = double (suy luận)
```

```
  ╔═══════════════════════════════════════════════════╗
  ║  GENERIC = TEMPLATE (khuôn mẫu)                  ║
  ║                                                   ║
  ║  Swap<T>(ref T a, ref T b)                        ║
  ║       ↓                                           ║
  ║  T = int    → Swap<int>(ref int a, ref int b)     ║
  ║  T = string → Swap<string>(ref string a, ...)     ║
  ║  T = double → Swap<double>(ref double a, ...)     ║
  ║  T = Student → Swap<Student>(ref Student a, ...)  ║
  ║                                                   ║
  ║  → VIẾT 1 LẦN, dùng cho TẤT CẢ kiểu!            ║
  ╚═══════════════════════════════════════════════════╝
```

---

## 3. Generic Method

### Cú pháp:

```csharp
// Khai báo generic method
ReturnType MethodName<T>(T parameter)
{
    // T được dùng như một kiểu bình thường
}

// Nhiều type parameters
ReturnType MethodName<T1, T2>(T1 param1, T2 param2)
{
    // T1, T2 là các kiểu khác nhau
}
```

### Ví dụ thực tế:

```csharp
class Utility
{
    // Generic method — in bất kỳ kiểu nào
    public static void Print<T>(T item)
    {
        Console.WriteLine($"[{typeof(T).Name}] {item}");
    }

    // Generic method — tìm max
    public static T FindMax<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) >= 0 ? a : b;
    }

    // Generic method — in mảng bất kỳ
    public static void PrintAll<T>(T[] items)
    {
        Console.Write($"[{typeof(T).Name}] ");
        foreach (T item in items)
        {
            Console.Write($"{item}  ");
        }
        Console.WriteLine();
    }
}

// Sử dụng:
Utility.Print(42);              // [Int32] 42
Utility.Print("Hello");         // [String] Hello
Utility.Print(3.14);            // [Double] 3.14

int max = Utility.FindMax(10, 20);       // 20
string maxStr = Utility.FindMax("Z", "A"); // "Z"

int[] nums = { 1, 2, 3, 4, 5 };
Utility.PrintAll(nums);   // [Int32] 1  2  3  4  5

string[] names = { "Minh", "Lan", "Hùng" };
Utility.PrintAll(names);  // [String] Minh  Lan  Hùng
```

---

## 4. Generic Class

### Cú pháp:

```csharp
class ClassName<T>
{
    private T _value;

    public ClassName(T value)
    {
        _value = value;
    }

    public T GetValue() => _value;
}
```

### Ví dụ 1: Pair\<T1, T2> — Cặp giá trị:

```csharp
class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }

    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}

// Sử dụng:
Pair<string, int> nameAge = new Pair<string, int>("Minh", 25);
Console.WriteLine(nameAge);  // (Minh, 25)

Pair<int, int> coordinates = new Pair<int, int>(10, 20);
Console.WriteLine(coordinates);  // (10, 20)

Pair<string, List<string>> dict = new("Fruits", new List<string> { "Apple", "Banana" });
Console.WriteLine(dict.First);  // Fruits
```

### Ví dụ 2: Result\<T> — Kết quả có thể lỗi:

```csharp
class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }

    // Constructor thành công
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    // Constructor thất bại
    private Result(string error)
    {
        IsSuccess = false;
        ErrorMessage = error;
    }

    // Factory methods
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(string error) => new Result<T>(error);
}

// Sử dụng:
Result<int> r1 = Result<int>.Success(42);
Result<int> r2 = Result<int>.Failure("Không tìm thấy!");

if (r1.IsSuccess)
    Console.WriteLine($"Giá trị: {r1.Value}");  // Giá trị: 42

Result<string> r3 = Result<string>.Success("Hello");
Result<List<int>> r4 = Result<List<int>>.Failure("Database error");
```

```
  Memory Layout — Generic vs Non-Generic:
  
  ┌────────────────────────────────────────────┐
  │  NON-GENERIC (object)                      │
  │                                            │
  │  Box<object> box = new Box<object>(42);    │
  │  Stack: [ref] → Heap: [object → 42]       │
  │                    ↑ Boxing! (int→object)   │
  │                    ↑ Chậm + allocate heap   │
  ├────────────────────────────────────────────┤
  │  GENERIC                                   │
  │                                            │
  │  Box<int> box = new Box<int>(42);          │
  │  Stack: [ref] → Heap: [int: 42]           │
  │                    ↑ Không boxing!          │
  │                    ↑ Nhanh + type safe      │
  └────────────────────────────────────────────┘
```

---

## 5. Generic Interface

```csharp
// Khai báo generic interface
interface IRepository<T>
{
    void Add(T item);
    T? GetById(int id);
    List<T> GetAll();
    bool Update(T item);
    bool Delete(int id);
}

// Implement cho kiểu cụ thể
class StudentRepository : IRepository<Student>
{
    private List<Student> _students = new();

    public void Add(Student item) { _students.Add(item); }
    public Student? GetById(int id) { return _students.FirstOrDefault(s => s.Id == id); }
    public List<Student> GetAll() { return _students; }
    public bool Update(Student item) { /* ... */ return true; }
    public bool Delete(int id) { /* ... */ return true; }
}

// Implement cho kiểu khác — CÙNG interface!
class ProductRepository : IRepository<Product>
{
    private List<Product> _products = new();

    public void Add(Product item) { _products.Add(item); }
    public Product? GetById(int id) { return _products.FirstOrDefault(p => p.Id == id); }
    public List<Product> GetAll() { return _products; }
    public bool Update(Product item) { /* ... */ return true; }
    public bool Delete(int id) { /* ... */ return true; }
}
```

```
  ┌──────────────────────────────────────┐
  │      IRepository<T> (Interface)      │
  │  + Add(T)                            │
  │  + GetById(int) : T                  │
  │  + GetAll() : List<T>               │
  │  + Update(T) : bool                  │
  │  + Delete(int) : bool                │
  └──────────┬──────────┬────────────────┘
             │          │
    ┌────────┴──┐  ┌────┴────────────┐
    │ Student   │  │ Product         │
    │ Repository│  │ Repository      │
    │ T=Student │  │ T=Product       │
    └───────────┘  └─────────────────┘
```

---

## 6. Constraints — Giới hạn kiểu T

Mặc định, `T` có thể là **BẤT KỲ** kiểu nào. Constraints cho phép bạn **giới hạn** những kiểu được phép.

### Bảng các Constraints:

| Constraint | Ý nghĩa | Ví dụ |
|-----------|---------|-------|
| `where T : struct` | T phải là value type (int, double, bool, struct...) | `class Nullable<T> where T : struct` |
| `where T : class` | T phải là reference type (string, class, interface...) | `class Cache<T> where T : class` |
| `where T : new()` | T phải có constructor không tham số | `class Factory<T> where T : new()` |
| `where T : BaseClass` | T phải kế thừa từ BaseClass | `class List<T> where T : Animal` |
| `where T : IInterface` | T phải implement IInterface | `where T : IComparable<T>` |
| `where T : notnull` | T không được null | `class Dict<T> where T : notnull` |
| `where T : U` | T phải kế thừa từ U (type parameter khác) | `class Derived<T, U> where T : U` |

### Ví dụ thực tế:

```csharp
// 1. where T : IComparable<T> — T phải so sánh được
T FindMax<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}

FindMax(10, 20);        // ✅ int implements IComparable<int>
FindMax("abc", "xyz");  // ✅ string implements IComparable<string>
// FindMax(new object(), new object()); // ❌ object không implement IComparable

// 2. where T : class — T phải là reference type
void ProcessRef<T>(T item) where T : class
{
    if (item == null) return;   // Có thể check null
    Console.WriteLine(item);
}

ProcessRef("hello");    // ✅ string là reference type
// ProcessRef(42);      // ❌ int là value type!

// 3. where T : new() — T phải có constructor không tham số
T CreateInstance<T>() where T : new()
{
    return new T();   // Có thể gọi new T()!
}

// 4. Kết hợp nhiều constraints
class Repository<T> where T : class, IEntity, new()
{
    // T phải là: reference type + implement IEntity + có constructor()
    public T Create()
    {
        T entity = new T();  // new() cho phép điều này
        return entity;
    }
}

// 5. Constraints cho nhiều type parameters
class Mapper<TSource, TDest> 
    where TSource : class 
    where TDest : class, new()
{
    public TDest Map(TSource source)
    {
        TDest dest = new TDest();
        // Map properties...
        return dest;
    }
}
```

### Thứ tự Constraints (bắt buộc):
```
where T : [class/struct]     ← phải đầu tiên (nếu có)
           [base class]      ← tiếp theo
           [interfaces]      ← tiếp theo
           [new()]           ← phải cuối cùng
           
// Ví dụ:
where T : class, Animal, IComparable<T>, new()
//        ↑      ↑        ↑                ↑
//     struct/  base     interfaces       new()
//     class   class                     (cuối)
//    (đầu)
```

---

## 7. Multiple Type Parameters

```csharp
// Dictionary dùng 2 type parameters
Dictionary<string, int> ages = new();   // TKey=string, TValue=int

// Tự định nghĩa
class KeyValueStore<TKey, TValue>
{
    private Dictionary<TKey, TValue> _store = new() 
        where TKey : notnull;

    public void Set(TKey key, TValue value)
    {
        _store[key] = value;
    }

    public TValue? Get(TKey key)
    {
        return _store.TryGetValue(key, out TValue? value) ? value : default;
    }
}

// Sử dụng:
KeyValueStore<string, int> config = new();
config.Set("MaxRetry", 3);
config.Set("Timeout", 30);

KeyValueStore<int, string> lookup = new();
lookup.Set(1, "Minh");
lookup.Set(2, "Lan");
```

### Convention đặt tên Type Parameter:

| Tên | Quy ước | Ví dụ |
|-----|---------|-------|
| `T` | Type chung (mặc định) | `List<T>`, `Stack<T>` |
| `TKey` | Kiểu của key | `Dictionary<TKey, TValue>` |
| `TValue` | Kiểu của value | `Dictionary<TKey, TValue>` |
| `TResult` | Kiểu kết quả | `Func<T, TResult>` |
| `TSource` | Kiểu nguồn | `IEnumerable<TSource>` |
| `TEntity` | Kiểu entity | `Repository<TEntity>` |

> 💡 Luôn bắt đầu bằng `T` (viết hoa) — đó là convention của C#.

---

## 8. Generic tự viết vs Built-in

### Built-in Generics (đã dùng):

```csharp
// Tất cả đều là generic!
List<int> numbers = new();              // Generic collection
Dictionary<string, int> map = new();    // 2 type params
Stack<string> stack = new();            // LIFO
Queue<int> queue = new();               // FIFO
HashSet<string> set = new();            // Unique items

// Nullable<T> — value type có thể null
int? x = null;  // Thực ra là Nullable<int>

// Func<T, TResult> — delegate
Func<int, string> toString = n => n.ToString();

// Task<T> — async
Task<int> task = Task.FromResult(42);
```

### Khi nào tự viết Generic?

```
  ┌─────────────────────────────────────────────┐
  │  TỰ VIẾT GENERIC khi:                      │
  │                                             │
  │  ✅ Logic giống nhau cho nhiều kiểu          │
  │  ✅ Muốn code reusable & type-safe          │
  │  ✅ Pattern Repository, Factory, Cache...    │
  │  ✅ Wrapper types: Result<T>, Option<T>      │
  │                                             │
  │  DÙNG BUILT-IN khi:                          │
  │                                             │
  │  ✅ Collection → List<T>, Dictionary<K,V>    │
  │  ✅ Stack/Queue → Stack<T>, Queue<T>         │
  │  ✅ Nullable → int?, string?                 │
  │  ✅ Delegate → Func<T>, Action<T>            │
  └─────────────────────────────────────────────┘
```

---

## 9. Covariance & Contravariance (Giới thiệu)

> ⚠️ Đây là khái niệm **nâng cao**. Chỉ cần hiểu cơ bản ở thời điểm này.

### Vấn đề:

```csharp
class Animal { }
class Dog : Animal { }

// Dog KẾ THỪA Animal, vậy List<Dog> có phải List<Animal> không?
List<Dog> dogs = new List<Dog>();
// List<Animal> animals = dogs;  // ❌ COMPILE ERROR! Tại sao?

// Vì nếu được phép:
// animals.Add(new Cat());  // 💥 Thêm Cat vào list Dog?!
```

### Covariance (`out` keyword) — Chỉ ĐỌC:

```csharp
// IEnumerable<out T> — T chỉ xuất hiện ở OUTPUT (return)
IEnumerable<Dog> dogs = new List<Dog>();
IEnumerable<Animal> animals = dogs;  // ✅ OK! Covariant

// Vì IEnumerable chỉ ĐỌC, không thể Add → an toàn
foreach (Animal a in animals) { }  // OK — Dog IS Animal
```

### Contravariance (`in` keyword) — Chỉ VIẾT:

```csharp
// Action<in T> — T chỉ xuất hiện ở INPUT (parameter)
Action<Animal> animalAction = a => Console.WriteLine(a);
Action<Dog> dogAction = animalAction;  // ✅ OK! Contravariant

// Vì method nhận Animal cũng nhận được Dog (Dog IS Animal)
dogAction(new Dog());  // OK — Dog IS Animal
```

### Tóm tắt:

```
  ┌────────────────────────────────────────────┐
  │  Covariance (out)    │  Contravariance (in) │
  │  T chỉ ở OUTPUT      │  T chỉ ở INPUT       │
  │  Dog → Animal ✅     │  Animal → Dog ✅     │
  │  IEnumerable<out T>  │  Action<in T>         │
  │  IReadOnlyList<out T>│  IComparer<in T>      │
  └────────────────────────────────────────────┘
  
  💡 Nhớ: out = ra (con → cha), in = vào (cha → con)
```

---

## 10. So sánh JS vs C# Generic

### JavaScript — KHÔNG CÓ Generic (trừ TypeScript):

```javascript
// JS — function nhận bất kỳ kiểu
function swap(a, b) {
    return [b, a];  // Không type check!
}

swap(1, 2);           // OK
swap("a", 42);        // OK — nhưng có ý nghĩa gì?! 😅
swap(null, undefined); // OK — 😱

// JS — "generic" class (thực ra là any)
class Box {
    constructor(value) {
        this.value = value;  // value có thể là bất kỳ gì
    }
}

const box1 = new Box(42);
const box2 = new Box("hello");
box1.value = "oops"; // Đổi kiểu?! Không lỗi! 😱
```

### TypeScript — CÓ Generic (giống C#):

```typescript
// TypeScript — có generics
function swap<T>(a: T, b: T): [T, T] {
    return [b, a];
}

swap<number>(1, 2);     // OK
swap<string>("a", "b"); // OK
// swap<number>(1, "b"); // ❌ Compile error!

// TypeScript generic class
class Box<T> {
    value: T;
    constructor(value: T) {
        this.value = value;
    }
}

const box = new Box<number>(42);
// box.value = "oops"; // ❌ Error!
```

### C# — Generic đầy đủ nhất:

```csharp
// C# — generic với constraints
T FindMax<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}

// C# — generic class
class Box<T>
{
    public T Value { get; set; }
    public Box(T value) => Value = value;
}

Box<int> box = new Box<int>(42);
// box.Value = "oops"; // ❌ Compile error — type safe!
```

| Đặc điểm | JavaScript | TypeScript | C# |
|----------|-----------|-----------|-----|
| Generic syntax | ❌ Không có | `<T>` | `<T>` |
| Type checking | Runtime | Compile time | Compile time |
| Constraints | ❌ | `extends` | `where T :` |
| Covariance | N/A | Có (limited) | `out` keyword |
| Performance | N/A | Erased at runtime | Specialized at runtime ✅ |

> 💡 C# generics được **giữ lại** ở runtime (reified), TypeScript generics bị **xóa** sau compile (erased). C# generic nhanh hơn vì compiler tạo code chuyên biệt cho mỗi kiểu!

---

## 11. Best Practices

### ✅ Nên làm:

```csharp
// 1. Dùng constraints để method/class an toàn hơn
T FindMax<T>(T a, T b) where T : IComparable<T>  // ✅ Rõ ràng

// 2. Đặt tên type parameter có ý nghĩa
class Repository<TEntity> where TEntity : class   // ✅ TEntity
class Mapper<TSource, TDest>                       // ✅ TSource, TDest

// 3. Dùng generic thay vì object
class TypeSafe<T> { public T Value { get; set; } }  // ✅
// class NotSafe { public object Value { get; set; } }  // ❌

// 4. Tận dụng built-in generics
List<Student> students = new();      // ✅ Dùng built-in
// ArrayList students = new();       // ❌ Non-generic, dùng object
```

### ❌ Không nên:

```csharp
// 1. Đừng dùng generic khi chỉ có 1 kiểu
class IntCalculator<T>  // ❌ Nếu chỉ dùng cho int, không cần generic
class IntCalculator     // ✅

// 2. Đừng quá nhiều type parameters
class Monster<T1, T2, T3, T4, T5>  // ❌ Quá phức tạp!
// Nên tách thành nhiều class nhỏ hơn

// 3. Đừng quên constraints khi cần
T FindMax<T>(T a, T b)  // ❌ T không so sánh được!
{
    return a > b ? a : b;  // 💥 Compile error — T không có operator >
}
```

---

## 💡 Tổng kết

```
  ╔══════════════════════════════════════════════════╗
  ║  GENERIC = Viết 1 lần, dùng cho MỌI kiểu       ║
  ║                                                  ║
  ║  Generic Method:  T DoSomething<T>(T input)      ║
  ║  Generic Class:   class Box<T> { T Value; }      ║
  ║  Generic Interface: interface IRepo<T> { ... }   ║
  ║                                                  ║
  ║  Constraints:  where T : class/struct/new()/...  ║
  ║  Convention:   T, TKey, TValue, TResult          ║
  ║                                                  ║
  ║  ✅ Type safe (compile-time check)               ║
  ║  ✅ Performance (no boxing)                       ║
  ║  ✅ Code reuse (DRY)                              ║
  ╚══════════════════════════════════════════════════╝
```

> 📌 **Nhớ**: Generic giống như "khuôn bánh" — bạn tạo 1 khuôn, rồi đổ bất kỳ nguyên liệu nào (int, string, Student...) vào để tạo ra sản phẩm cùng hình dáng nhưng khác chất liệu!
