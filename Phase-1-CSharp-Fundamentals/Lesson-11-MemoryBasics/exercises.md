# ✏️ Lesson 11 — Bài tập: Memory Basics

---

## Bài 1: Dự đoán Output — Memory 🔮

### Yêu cầu:
Dự đoán output **trên giấy**, vẽ Stack/Heap, rồi chạy kiểm tra:

**Câu A:**
```csharp
int x = 5;
int y = x;
y = 10;
Console.WriteLine($"x={x}, y={y}");
```

**Câu B:**
```csharp
int[] a = { 1, 2, 3 };
int[] b = a;
b[1] = 99;
Console.WriteLine($"a[1]={a[1]}, b[1]={b[1]}");
```

**Câu C:**
```csharp
string s1 = "Hello";
string s2 = s1;
s2 += " World";
Console.WriteLine($"s1=\"{s1}\", s2=\"{s2}\"");
```

**Câu D:**
```csharp
int[] arr = { 10, 20, 30 };
ModifyFirst(arr);
Console.WriteLine(arr[0]);

static void ModifyFirst(int[] data) { data[0] = 999; }
```

**Câu E:**
```csharp
int val = 100;
TryChange(val);
Console.WriteLine(val);

static void TryChange(int x) { x = 0; }
```

**Với mỗi câu:** Vẽ sơ đồ Stack/Heap tại mỗi bước.

---

## Bài 2: Clone vs Reference — Thí nghiệm 🧪

### Yêu cầu:
Viết chương trình chứng minh sự khác biệt giữa 3 cách copy array:

```csharp
int[] original = { 10, 20, 30, 40, 50 };

// Cách 1: Gán trực tiếp (reference copy)
int[] copy1 = original;

// Cách 2: Clone
int[] copy2 = (int[])original.Clone();

// Cách 3: Array.Copy
int[] copy3 = new int[original.Length];
Array.Copy(original, copy3, original.Length);
```

Sau đó sửa `copy1[0] = 999`, `copy2[1] = 888`, `copy3[2] = 777`.

In bảng so sánh tất cả array. Giải thích tại sao `original` bị ảnh hưởng bởi `copy1` nhưng không bởi `copy2`, `copy3`.

### Expected Output:
```
│ Index │ Original │ Copy1 │ Copy2 │ Copy3 │
├───────┼──────────┼───────┼───────┼───────┤
│   0   │   999    │  999  │  10   │  10   │ ← copy1 ảnh hưởng original!
│   1   │    20    │   20  │  888  │  20   │
│   2   │    30    │   30  │  30   │  777  │
│   3   │    40    │   40  │  40   │  40   │
│   4   │    50    │   50  │  50   │  50   │
```

---

## Bài 3: Null Safety Practice 🛡️

### Yêu cầu:
Viết chương trình nhập danh sách tên (tối đa 10). Một số ô có thể là `null` (user nhấn Enter bỏ qua).

Xử lý tất cả trường hợp null an toàn:

```csharp
static string SafeUpperCase(string? input);      // null → "(trống)"
static int SafeLength(string? input);              // null → 0
static string SafeTrim(string? input);             // null → ""
static bool IsValid(string? input);                // null/empty/whitespace → false
static string SafeSubstring(string? input, int start, int length);
```

Test: nhập 5 tên (để 2 cái trống), in bảng với độ dài, uppercase, valid.

---

## Bài 4: Method Parameter Lab 🔬

### Yêu cầu:
Viết 6 method minh họa tất cả cách truyền tham số:

```csharp
// 1. Value type - bình thường
static void Test1(int x);

// 2. Value type - ref
static void Test2(ref int x);

// 3. Value type - out
static void Test3(out int x);

// 4. Reference type - modify
static void Test4(int[] arr);

// 5. Reference type - reassign
static void Test5(int[] arr);

// 6. Reference type - ref + reassign
static void Test6(ref int[] arr);
```

Với mỗi method: in giá trị TRƯỚC và SAU khi gọi.
In bảng tổng kết 6 thí nghiệm.

---

## Bài 5: Memory Quiz Game 🎮

### Yêu cầu:
Tạo quiz 10 câu hỏi trắc nghiệm về memory:

Ví dụ câu hỏi:
```
Câu 1: int nằm ở đâu trong bộ nhớ?
  A. Stack    B. Heap    C. Cả hai    D. Không biết

Câu 2: int[] arr = {1,2,3}; int[] b = arr; b[0]=9; arr[0] = ?
  A. 1        B. 9       C. 0          D. Lỗi

Câu 3: string s = "Hi"; s.ToUpper(); Console.Write(s); Output?
  A. HI       B. Hi      C. Lỗi        D. null
```

Tự tạo 10 câu, mỗi câu 4 đáp án, có giải thích sau khi user trả lời.
Tính điểm cuối cùng.
