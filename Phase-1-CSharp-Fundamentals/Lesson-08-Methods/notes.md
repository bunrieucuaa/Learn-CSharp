# 📝 Lesson 08 — Notes: Methods

---

## 🧠 Tóm tắt

### Cú pháp:

```csharp
// Cơ bản
static void PrintHi() { Console.WriteLine("Hi"); }
static int Add(int a, int b) { return a + b; }

// Expression-bodied (1 dòng)
static int Add(int a, int b) => a + b;

// Default params
static void Log(string msg, string level = "INFO") { ... }

// Named params
Log(msg: "error!", level: "ERROR");

// params (tham số không cố định)
static int Sum(params int[] nums) { ... }

// out (trả thêm giá trị)
static bool TryDo(string input, out int result) { ... }

// ref (tham chiếu 2 chiều)
static void Swap(ref int a, ref int b) { ... }

// Overloading (cùng tên, khác tham số)
static double Area(double r);
static double Area(double w, double h);

// Local function (C# 7+)
void Outer() {
    int Helper(int x) => x * 2;
}

// Recursion
static int Fact(int n) => n <= 1 ? 1 : n * Fact(n - 1);
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Quên `return` | Compiler sẽ báo lỗi |
| 2 | Method quá dài (>30 dòng) | Tách nhỏ method |
| 3 | Method làm quá nhiều việc | Single Responsibility |
| 4 | Quá nhiều tham số (>4) | Dùng object hoặc tách method |
| 5 | Default param trước non-default | Default phải ở CUỐI |
| 6 | Đệ quy quên base case | StackOverflowException |
| 7 | Gọi `out` quên keyword `out` | `Method(out var x)` |

---

## 📊 So sánh C# vs JavaScript

| | C# | JavaScript |
|--|-----|-----------|
| Khai báo | `static int Add(int a, int b)` | `function add(a, b)` |
| Kiểu tham số | Bắt buộc khai báo | Không cần |
| Return type | Bắt buộc | Không cần |
| Overloading | ✅ Có | ❌ Không |
| Default params | ✅ `int x = 0` | ✅ `x = 0` |
| Rest params | `params int[]` | `...args` |
| Arrow / Expression | `=> expr` (1 dòng) | `=> expr` |
| `out` / `ref` | ✅ Có | ❌ Không |
| Naming | PascalCase `GetName()` | camelCase `getName()` |

---

## ✅ Checklist

- [ ] Biết khai báo method (void / có return)
- [ ] Biết expression-bodied `=>`
- [ ] Biết default parameters
- [ ] Biết named parameters
- [ ] Biết `params` cho tham số linh hoạt
- [ ] Biết `out` cho trả thêm giá trị
- [ ] Biết `ref` cho tham chiếu
- [ ] Biết method overloading
- [ ] Biết recursion + base case
- [ ] Biết tách code thành methods (Single Responsibility)
- [ ] Main() gọn, chỉ điều phối

---

## 🔗 Liên kết

```
Lesson 07: Loop
    ↓
Lesson 08: Methods ← BẠN Ở ĐÂY
    ↓
Lesson 09: Enum (kiểu liệt kê)
    ↓
Lesson 10: String nâng cao
    ↓
Lesson 11: DateTime
    ↓
Lesson 12: Array nâng cao
```

> 📌 **Milestone!** Sau Lesson 08, bạn đã có đủ công cụ cơ bản để viết bất kỳ console app nào. Các lesson tiếp theo sẽ mở rộng kiến thức về data types và collections.
