# 📘 Lesson 11 — Memory Basics (Nền tảng bộ nhớ) trong C#

---

## 1. Tại sao cần hiểu Memory?

Bạn đã dùng biến, mảng, string — nhưng chúng **nằm ở đâu** trong bộ nhớ? Hiểu memory giúp:

- Tránh bug khó hiểu (sửa array ảnh hưởng chỗ khác)
- Viết code hiệu quả (không tạo thừa object)
- Hiểu sâu OOP ở Phase 2
- Đọc hiểu lỗi `NullReferenceException`, `StackOverflowException`

---

## 2. Hai vùng nhớ chính: Stack và Heap

```
╔══════════════════════════════════════════════════╗
║                    MEMORY                         ║
╠═════════════════════╦════════════════════════════╣
║       STACK         ║          HEAP               ║
║  (ngăn xếp)         ║    (vùng nhớ động)          ║
║                     ║                             ║
║  • Nhỏ, nhanh       ║  • Lớn, chậm hơn           ║
║  • Tự dọn dẹp       ║  • GC dọn dẹp              ║
║  • LIFO (vào sau    ║  • Không có thứ tự          ║
║    ra trước)        ║                             ║
║                     ║                             ║
║  Chứa:              ║  Chứa:                      ║
║  • Value types      ║  • Reference types           ║
║  • Biến local       ║  • Objects (new)             ║
║  • Parameters       ║  • Arrays                   ║
║  • Return address   ║  • Strings                  ║
╚═════════════════════╩════════════════════════════╝
```

### Analogy:

| | Stack | Heap |
|--|-------|------|
| Giống như | **Chồng đĩa** — đặt lên trên, lấy từ trên xuống | **Nhà kho** — đặt bất kỳ đâu, cần address để tìm |
| Tốc độ | ⚡ Rất nhanh | 🐢 Chậm hơn |
| Kích thước | ~1-4 MB (nhỏ) | GB (lớn) |
| Quản lý | Tự động (vào/ra scope) | Garbage Collector |
| Lưu gì | Giá trị nhỏ, cố định | Object lớn, kích thước thay đổi |

---

## 3. Value Types — Nằm trên Stack

### Value types lưu **giá trị trực tiếp** trên Stack:

```csharp
int age = 25;
double salary = 15000000.50;
bool isActive = true;
char grade = 'A';
```

```
STACK
┌──────────────┐
│ grade = 'A'  │ ← top
│ isActive = 1 │
│ salary = 15M │
│ age = 25     │ ← bottom
└──────────────┘
```

### Danh sách Value Types:

| Kiểu | Kích thước | Mô tả |
|------|-----------|-------|
| `bool` | 1 byte | true/false |
| `byte` | 1 byte | 0 → 255 |
| `char` | 2 bytes | Ký tự Unicode |
| `int` | 4 bytes | Số nguyên |
| `long` | 8 bytes | Số nguyên lớn |
| `float` | 4 bytes | Số thực |
| `double` | 8 bytes | Số thực chính xác hơn |
| `decimal` | 16 bytes | Số thực cho tiền tệ |
| `struct` | Tùy | Kiểu tùy chỉnh (value) |
| `enum` | 4 bytes | Kiểu liệt kê |

### Copy = tạo bản sao ĐỘC LẬP:

```csharp
int a = 10;
int b = a;    // Copy giá trị → b = 10
b = 999;      // Sửa b

Console.WriteLine(a);  // 10 ← KHÔNG đổi!
Console.WriteLine(b);  // 999
```

```
STACK (sau b = 999)
┌──────────────┐
│ b = 999      │ ← bản copy riêng
│ a = 10       │ ← giữ nguyên
└──────────────┘
```

---

## 4. Reference Types — Nằm trên Heap

### Reference types lưu **địa chỉ (reference)** trên Stack, **data thật** trên Heap:

```csharp
int[] arr = { 1, 2, 3 };
string name = "Minh";
```

```
STACK                          HEAP
┌──────────────┐              ┌───────────────┐
│ name = 0x200 ┼─────────────▶│ "Minh"        │ 0x200
│ arr  = 0x100 ┼─────────────▶│ [1, 2, 3]     │ 0x100
└──────────────┘              └───────────────┘
     ↑ địa chỉ                    ↑ data thật
```

### Danh sách Reference Types:

| Kiểu | Ví dụ |
|------|-------|
| `string` | `"Hello"` |
| `array` | `int[]`, `string[]` |
| `class` | Tất cả class (sẽ học Phase 2) |
| `object` | Base type |
| `dynamic` | Kiểu động |
| `delegate` | Con trỏ hàm |

### Copy = chia sẻ CÙNG data:

```csharp
int[] a = { 1, 2, 3 };
int[] b = a;       // Copy REFERENCE (địa chỉ), KHÔNG copy data!
b[0] = 999;        // Sửa qua b

Console.WriteLine(a[0]);  // 999 ← ĐÃ ĐỔI! Vì a và b trỏ cùng data
Console.WriteLine(b[0]);  // 999
```

```
STACK                          HEAP
┌──────────────┐              ┌───────────────┐
│ b = 0x100    ┼──────┐      │               │
│ a = 0x100    ┼──────┼─────▶│ [999, 2, 3]   │ 0x100
└──────────────┘      │      │               │
                      └──────┘ Cả a và b trỏ
                               cùng 1 chỗ!
```

> 🔑 **Đây là khác biệt quan trọng nhất!**
> - Value type copy → **2 bản riêng**
> - Reference type copy → **2 tên cùng trỏ 1 data**

---

## 5. Truyền vào Method — Tổng kết

### 5.1 Value Type → Copy:

```csharp
static void Change(int x)
{
    x = 999;  // Chỉ sửa bản copy
}

int num = 10;
Change(num);
Console.WriteLine(num);  // 10 — KHÔNG đổi
```

### 5.2 Reference Type → Chia sẻ:

```csharp
static void Change(int[] arr)
{
    arr[0] = 999;  // Sửa data thật trên Heap
}

int[] nums = { 1, 2, 3 };
Change(nums);
Console.WriteLine(nums[0]);  // 999 — ĐÃ đổi!
```

### 5.3 Reference Type — Gán lại (reassign):

```csharp
static void Replace(int[] arr)
{
    arr = new int[] { 100, 200, 300 };  // Tạo array MỚI trên Heap
    // arr giờ trỏ sang data mới, nhưng biến gốc vẫn trỏ chỗ cũ!
}

int[] nums = { 1, 2, 3 };
Replace(nums);
Console.WriteLine(nums[0]);  // 1 — KHÔNG đổi!
// Vì: chỉ copy reference bị gán lại, reference gốc không đổi
```

```
STACK                          HEAP
Trước Replace:
│ arr = 0x100  ┼──────────────▶│ [1, 2, 3]       │ 0x100

Trong Replace:
│ arr = 0x200  ┼──────────────▶│ [100, 200, 300]  │ 0x200 (mới)
│ nums = 0x100 ┼──────────────▶│ [1, 2, 3]       │ 0x100 (gốc, không đổi)

Sau Replace:
│ nums = 0x100 ┼──────────────▶│ [1, 2, 3]       │ 0x100 ✅
                               │ [100, 200, 300]  │ 0x200 → GC dọn
```

### 5.4 Dùng `ref` để sửa cả reference:

```csharp
static void Replace(ref int[] arr)
{
    arr = new int[] { 100, 200, 300 };
}

int[] nums = { 1, 2, 3 };
Replace(ref nums);
Console.WriteLine(nums[0]);  // 100 — ĐÃ đổi! ref cho phép thay reference gốc
```

---

## 6. `string` — Reference Type đặc biệt (Immutable)

```csharp
string a = "Hello";
string b = a;       // Copy reference
b = "World";        // Tạo string MỚI, b trỏ sang đó

Console.WriteLine(a);  // "Hello" — KHÔNG đổi!
Console.WriteLine(b);  // "World"
```

```
STACK                          HEAP
┌──────────────┐              ┌───────────┐
│ b = 0x200    ┼─────────────▶│ "World"   │ 0x200 (mới)
│ a = 0x100    ┼─────────────▶│ "Hello"   │ 0x100 (gốc)
└──────────────┘              └───────────┘
```

> 🔑 `string` là **reference type** nhưng **immutable** (bất biến). Mọi thao tác tạo string MỚI → giống hành vi value type.

---

## 7. Null — "Không trỏ đến đâu cả"

```csharp
int[] arr = null;        // Reference không trỏ đâu cả
// arr[0] = 1;           // ❌ NullReferenceException!

string? name = null;
// int len = name.Length; // ❌ NullReferenceException!
int len = name?.Length ?? 0;  // ✅ An toàn — 0 nếu null

// Value type KHÔNG THỂ null (trừ khi dùng ?)
// int x = null;          // ❌ LỖI COMPILE!
int? x = null;            // ✅ Nullable value type
```

```
STACK                          HEAP
┌──────────────┐
│ arr = null   │ ──── ✖ ──── không trỏ đâu cả
│ name = null  │ ──── ✖ ──── không trỏ đâu cả
└──────────────┘
```

---

## 8. Garbage Collection (GC) — Dọn rác tự động

```csharp
void CreateData()
{
    int[] big = new int[1000000];  // 1 triệu phần tử trên Heap
    // ... dùng big ...
}  // big ra khỏi scope → không ai trỏ tới nữa

// GC sẽ TỰ ĐỘNG dọn dẹp data trên Heap khi cần
// Bạn KHÔNG cần free/delete như C/C++!
```

### GC hoạt động thế nào?

```
1. Chương trình chạy, tạo objects trên Heap
2. Khi Heap gần đầy, GC kích hoạt
3. GC tìm objects KHÔNG CÒN AI trỏ tới
4. GC xóa chúng, giải phóng bộ nhớ
5. Chương trình tiếp tục
```

> 💡 **C# tự quản lý bộ nhớ** qua GC. Khác C/C++ phải `free()` thủ công. Giống JavaScript cũng có GC.

---

## 9. Boxing & Unboxing

```csharp
// Boxing — value type → object (Heap)
int number = 42;
object boxed = number;  // int (Stack) → object (Heap)

// Unboxing — object → value type (Stack)
int unboxed = (int)boxed;  // object (Heap) → int (Stack)
```

```
Boxing:
STACK          HEAP
│ number = 42 │
│ boxed ──────┼──▶ │ object: 42 │   ← copy lên Heap

Unboxing:
│ unboxed = 42│ ← copy xuống Stack
```

> ⚠️ Boxing/Unboxing **tốn performance**. Tránh dùng `object` khi biết kiểu cụ thể.

---

## 10. So sánh với JavaScript

| | C# | JavaScript |
|--|-----|-----------|
| Value types | `int`, `double`, `bool`, `struct` | `number`, `boolean` (primitives) |
| Reference types | `class`, `array`, `string` | `object`, `array`, `function` |
| Null | `null` (reference), `Nullable<T>` | `null`, `undefined` |
| GC | ✅ Automatic | ✅ Automatic |
| Memory control | Nhiều hơn (struct, ref, span) | Ít (engine quản lý) |
| Stack Overflow | `StackOverflowException` | `Maximum call stack size exceeded` |

---

## 11. Sai lầm phổ biến

### ❌ Nghĩ array copy = copy data:
```csharp
int[] a = { 1, 2, 3 };
int[] b = a;        // ❌ Cùng trỏ 1 data!
b[0] = 999;         // a[0] cũng = 999!

// ✅ Copy thật:
int[] b = (int[])a.Clone();
// hoặc
int[] b = new int[a.Length];
Array.Copy(a, b, a.Length);
```

### ❌ NullReferenceException:
```csharp
string? s = null;
Console.WriteLine(s.Length);  // ❌ CRASH!
// ✅ Console.WriteLine(s?.Length ?? 0);
```

### ❌ StackOverflowException (đệ quy vô tận):
```csharp
void Loop() { Loop(); }  // ❌ Stack đầy!
```

### ❌ Nhầm string thay đổi được:
```csharp
string s = "hello";
s.ToUpper();           // ❌ Kết quả bị bỏ!
s = s.ToUpper();       // ✅ Gán lại
```

---

## 12. Best Practices

1. **Biết value vs reference** → tránh bug chia sẻ data
2. **Clone array** khi cần copy thật: `(int[])arr.Clone()`
3. **Null check** trước khi truy cập reference: `?.` và `??`
4. **Tránh boxing** — dùng kiểu cụ thể, không dùng `object`
5. **Tránh tạo object thừa** trong vòng lặp
6. **String immutable** — gán lại sau khi transform
7. **Đệ quy có base case** → tránh StackOverflow
