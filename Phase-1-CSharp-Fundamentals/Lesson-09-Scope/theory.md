# 📘 Lesson 09 — Scope & Lifetime (Phạm vi & Vòng đời biến) trong C#

---

## 1. Scope là gì?

**Scope** = phạm vi mà một biến **có thể được truy cập**. Ngoài scope → biến không tồn tại.

```csharp
{
    int x = 10;        // x bắt đầu tồn tại
    Console.WriteLine(x);  // ✅ OK — trong scope
}
// Console.WriteLine(x);  // ❌ LỖI! x không còn tồn tại
```

> 🔑 **Quy tắc vàng:** Biến chỉ sống trong cặp `{}` chứa nó.

---

## 2. Các loại Scope trong C#

```
┌─────────────────────────────────────────────┐
│ Class Scope (toàn class)                     │
│  ┌──────────────────────────────────────┐   │
│  │ Method Scope                         │   │
│  │  ┌────────────────────────────┐      │   │
│  │  │ Block Scope (if/for/...)   │      │   │
│  │  │  ┌─────────────────┐      │      │   │
│  │  │  │ Nested Block     │      │      │   │
│  │  │  └─────────────────┘      │      │   │
│  │  └────────────────────────────┘      │   │
│  └──────────────────────────────────────┘   │
└─────────────────────────────────────────────┘
```

### 2.1 Class Scope (Fields)

```csharp
class Program
{
    // Class-level — truy cập được từ MỌI method trong class
    static int counter = 0;
    static string appName = "MyApp";

    static void Main()
    {
        counter++;                    // ✅ OK
        Console.WriteLine(appName);   // ✅ OK
        DoSomething();
    }

    static void DoSomething()
    {
        counter++;                    // ✅ OK — cùng class
        Console.WriteLine(appName);   // ✅ OK
    }
}
```

### 2.2 Method Scope (Local Variables)

```csharp
static void Main()
{
    int age = 25;              // Method scope — chỉ trong Main()
    string name = "Minh";

    Console.WriteLine(age);    // ✅ OK
    DoSomething();
}

static void DoSomething()
{
    // Console.WriteLine(age);  // ❌ LỖI! age thuộc Main(), không thấy ở đây
    int age = 30;               // ✅ OK — đây là biến KHÁC, cùng tên nhưng khác scope
}
```

### 2.3 Block Scope (if / for / while / ...)

```csharp
static void Main()
{
    int x = 10;

    if (x > 5)
    {
        int y = 20;            // Block scope — chỉ trong if
        Console.WriteLine(x);  // ✅ OK — x từ scope cha
        Console.WriteLine(y);  // ✅ OK — y trong scope hiện tại
    }

    Console.WriteLine(x);      // ✅ OK
    // Console.WriteLine(y);   // ❌ LỖI! y đã chết khi ra khỏi if {}
}
```

### 2.4 Loop Scope

```csharp
for (int i = 0; i < 5; i++)
{
    int temp = i * 2;           // temp tạo + hủy MỖI lần lặp
    Console.WriteLine(temp);    // ✅ OK
}
// Console.WriteLine(i);       // ❌ LỖI! i chết khi ra khỏi for
// Console.WriteLine(temp);    // ❌ LỖI! temp cũng chết

// Nếu cần i sau vòng lặp → khai báo trước
int j;
for (j = 0; j < 5; j++) { }
Console.WriteLine(j);          // ✅ OK — j = 5
```

---

## 3. Shadowing — Che tên biến

```csharp
class Program
{
    static int x = 100;        // Class-level

    static void Main()
    {
        Console.WriteLine(x);  // 100 — class-level

        int x = 50;            // ⚠️ Local x che (shadow) class-level x
        Console.WriteLine(x);  // 50 — local x

        if (true)
        {
            // int x = 25;     // ❌ LỖI! C# KHÔNG cho shadow trong cùng method
                               // Khác JavaScript (let cho phép shadow trong block)
        }
    }
}
```

### So sánh với JavaScript:

```javascript
// JS — cho phép shadow trong block
let x = 10;
if (true) {
    let x = 20;    // ✅ OK — shadow x cha
    console.log(x); // 20
}
console.log(x);     // 10
```

```csharp
// C# — KHÔNG cho shadow trong cùng method scope
int x = 10;
if (true)
{
    // int x = 20;  // ❌ LỖI COMPILE!
    x = 20;         // ✅ Gán lại (cùng biến x)
}
Console.WriteLine(x); // 20
```

> 🔑 **C# nghiêm ngặt hơn JS**: không cho shadow biến local trong block con. Điều này tránh bug.

---

## 4. Lifetime — Vòng đời biến

| Loại biến | Tạo khi | Hủy khi |
|-----------|---------|---------|
| Class field (`static`) | Class load | Chương trình kết thúc |
| Local variable | Vào scope (block) | Ra khỏi scope |
| Loop variable (`int i`) | Bắt đầu vòng lặp | Kết thúc vòng lặp |
| Parameter | Method được gọi | Method kết thúc |

```csharp
static void Example()
{
    // age sống từ đây...
    int age = 25;

    for (int i = 0; i < 3; i++)  // i sống trong for
    {
        int temp = i * 10;        // temp sống trong 1 iteration
    }                              // temp chết, i chết

    // age vẫn sống ở đây
    Console.WriteLine(age);
}                                  // age chết ở đây
```

---

## 5. Value Types vs Reference Types — Scope & Memory

### Value Types (Stack):

```csharp
static void Main()
{
    int a = 10;
    Modify(a);
    Console.WriteLine(a);  // 10 — KHÔNG thay đổi! (truyền bản copy)
}

static void Modify(int x)
{
    x = 999;  // Chỉ sửa bản copy, không ảnh hưởng a
}
```

### Reference Types (Heap):

```csharp
static void Main()
{
    int[] arr = { 1, 2, 3 };
    Modify(arr);
    Console.WriteLine(arr[0]);  // 999 — ĐÃ thay đổi! (truyền tham chiếu)
}

static void Modify(int[] arr)
{
    arr[0] = 999;  // Sửa data thật vì arr là reference
}
```

### Minh họa Memory:

```
STACK                          HEAP
┌────────────┐                ┌──────────┐
│ a = 10     │                │ [1, 2, 3]│
│ arr ───────┼───────────────▶│          │
│            │                └──────────┘
│ (Method)   │
│ x = 10     │ ← copy riêng
│ arr ───────┼───────────────▶ (cùng object!)
└────────────┘
```

> 🔑 **Value type**: truyền vào method = copy → sửa không ảnh hưởng gốc.
> **Reference type**: truyền vào method = tham chiếu → sửa ảnh hưởng gốc.

---

## 6. `const` vs `readonly` vs `static`

```csharp
class Config
{
    // const — hằng số compile-time, PHẢI gán lúc khai báo
    const double PI = 3.14159;
    const string APP_NAME = "MyApp";
    // const int x;  // ❌ LỖI — phải gán giá trị ngay

    // static — thuộc class, không thuộc instance
    static int counter = 0;

    // readonly — chỉ gán 1 lần (lúc khai báo hoặc constructor)
    readonly int maxSize = 100;
    // Sẽ học kỹ hơn ở OOP
}
```

| | `const` | `readonly` | `static` |
|--|---------|-----------|----------|
| Gán khi | Khai báo (compile-time) | Khai báo hoặc constructor | Bất kỳ lúc nào |
| Thay đổi | ❌ Không bao giờ | ❌ Sau constructor | ✅ Được |
| Scope | Class-level hoặc local | Class-level | Class-level |
| Dùng khi | Giá trị cố định vĩnh viễn | Giá trị cố định sau khởi tạo | Chia sẻ giữa instances |

---

## 7. Closures — Method bắt biến ngoài (C# 7+)

```csharp
static void Main()
{
    int multiplier = 3;

    // Local function "bắt" biến multiplier từ scope cha
    int Multiply(int x) => x * multiplier;

    Console.WriteLine(Multiply(5));   // 15
    Console.WriteLine(Multiply(10));  // 30

    multiplier = 5;
    Console.WriteLine(Multiply(10));  // 50 — cập nhật theo multiplier!
}
```

> 💡 Giống closure trong JavaScript! Local function có thể truy cập biến của scope cha.

---

## 8. Sai lầm phổ biến

### ❌ Khai báo biến trong block rồi dùng ngoài:

```csharp
if (condition)
{
    string result = "OK";
}
// Console.WriteLine(result);  // ❌ LỖI!

// ✅ Khai báo trước block
string result = "";
if (condition)
{
    result = "OK";
}
Console.WriteLine(result);  // ✅
```

### ❌ Dùng biến vòng lặp sau khi loop kết thúc:

```csharp
for (int i = 0; i < 10; i++) { }
// Console.WriteLine(i);  // ❌ LỖI! i đã chết

// ✅ Khai báo trước
int i;
for (i = 0; i < 10; i++) { }
Console.WriteLine(i);  // ✅ 10
```

### ❌ Quên rằng value type truyền bằng copy:

```csharp
int score = 100;
Reduce(score);
Console.WriteLine(score);  // Vẫn 100! Không giảm!

static void Reduce(int s) { s -= 10; }  // Chỉ sửa bản copy
```

### ❌ Class field quá nhiều (global state):

```csharp
// ❌ Quá nhiều biến class-level → khó theo dõi, dễ bug
static int a, b, c, d, e, f, g;

// ✅ Chỉ dùng class-level khi THẬT SỰ cần chia sẻ giữa methods
// Ưu tiên local variable + parameter
```

---

## 9. Best Practices

1. **Khai báo biến ở scope NHỎ NHẤT có thể** — dễ đọc, ít side effects
2. **Tránh class-level nếu local đủ** — giảm global state
3. **Dùng `const`** cho giá trị không bao giờ đổi
4. **Khai báo biến gần nơi dùng** — không khai báo tất cả ở đầu method
5. **Hiểu value vs reference** khi truyền vào method
6. **Dùng `ref`/`out`** nếu cần method sửa value type bên ngoài
7. **Tên biến scope nhỏ có thể ngắn** (`i`, `j`) — scope lớn phải mô tả rõ (`studentCount`)
