# 📘 Lesson 12 — Arrays (Mảng) trong C#

---

## 1. Array là gì?

Array = **tập hợp các phần tử CÙNG KIỂU**, có kích thước **CỐ ĐỊNH** khi tạo.

```
Index:   0     1     2     3     4
       ┌─────┬─────┬─────┬─────┬─────┐
arr =  │  10 │  20 │  30 │  40 │  50 │
       └─────┴─────┴─────┴─────┴─────┘
Length = 5
```

> 🔑 Index bắt đầu từ **0** (giống JavaScript).

---

## 2. Khai báo & Khởi tạo

```csharp
// Cách 1: Khai báo + khởi tạo riêng
int[] numbers = new int[5];         // 5 phần tử, mặc định = 0
string[] names = new string[3];    // 3 phần tử, mặc định = null

// Cách 2: Khai báo + gán giá trị
int[] scores = new int[] { 8, 9, 7, 6, 10 };
int[] scores2 = { 8, 9, 7, 6, 10 };       // Rút gọn
string[] fruits = { "Táo", "Cam", "Xoài" };

// Cách 3: var
var prices = new int[] { 100, 200, 300 };
var names2 = new[] { "An", "Bình", "Chi" }; // Compiler suy luận kiểu
```

### Giá trị mặc định:

| Kiểu | Default |
|------|---------|
| `int`, `double` | `0` |
| `bool` | `false` |
| `string`, reference types | `null` |
| `char` | `'\0'` |

```csharp
int[] arr = new int[3];
Console.WriteLine(arr[0]);  // 0 (default)
Console.WriteLine(arr[1]);  // 0
```

---

## 3. Truy cập & Sửa phần tử

```csharp
int[] arr = { 10, 20, 30, 40, 50 };

// Đọc
Console.WriteLine(arr[0]);      // 10 (phần tử đầu)
Console.WriteLine(arr[4]);      // 50 (phần tử cuối)
Console.WriteLine(arr[^1]);     // 50 (ký hiệu ^1 = cuối cùng)
Console.WriteLine(arr[^2]);     // 40 (kế cuối)

// Sửa
arr[0] = 100;
arr[^1] = 500;

// Độ dài
Console.WriteLine(arr.Length);   // 5

// ❌ Truy cập ngoài giới hạn
// arr[5] = 99;  // IndexOutOfRangeException!
// arr[-1] = 99; // IndexOutOfRangeException!
```

### So sánh với JavaScript:

```javascript
// JS — array linh hoạt
let arr = [1, 2, 3];
arr.push(4);           // Thêm được! [1,2,3,4]
arr[10] = 99;          // OK! [1,2,3,4,,,,,,,99]
arr.length;            // 11 (sparse array)
```

```csharp
// C# — array CỐ ĐỊNH kích thước
int[] arr = { 1, 2, 3 };
// arr.push(4);         // ❌ Không có push!
// arr[10] = 99;        // ❌ IndexOutOfRangeException!
arr.Length;             // 3 (cố định)
```

| | C# Array | JavaScript Array |
|--|----------|-----------------|
| Kích thước | Cố định | Linh hoạt |
| Kiểu phần tử | Cùng kiểu | Hỗn hợp |
| Index ngoài | Exception! | `undefined` |
| Thêm phần tử | Không được | `push()` |
| Tương đương linh hoạt | `List<T>` (Phase 3) | Mặc định |

---

## 4. Duyệt Array

### 4.1 `for` — Có index:

```csharp
int[] scores = { 8, 9, 7, 10, 6 };

for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine($"[{i}] = {scores[i]}");
}
```

### 4.2 `foreach` — Gọn, chỉ đọc:

```csharp
foreach (int score in scores)
{
    Console.WriteLine(score);
}
// ⚠️ Không có index, không sửa được phần tử
```

### 4.3 Duyệt ngược:

```csharp
for (int i = scores.Length - 1; i >= 0; i--)
{
    Console.Write($"{scores[i]} ");
}
// Output: 6 10 7 9 8
```

### 4.4 Range — Slice array (C# 8+):

```csharp
int[] arr = { 10, 20, 30, 40, 50 };

int[] first3 = arr[..3];      // { 10, 20, 30 }
int[] last2 = arr[^2..];      // { 40, 50 }
int[] middle = arr[1..4];     // { 20, 30, 40 }
```

---

## 5. Các method Array quan trọng

### 5.1 Tìm kiếm:

```csharp
int[] arr = { 30, 10, 50, 20, 40 };

int idx = Array.IndexOf(arr, 50);          // 2 (vị trí đầu tiên)
int idx2 = Array.IndexOf(arr, 99);         // -1 (không tìm thấy)
bool exists = Array.Exists(arr, x => x > 40);  // true
int found = Array.Find(arr, x => x > 25);      // 30 (phần tử đầu match)
int[] all = Array.FindAll(arr, x => x >= 30);   // { 30, 50, 40 }
```

### 5.2 Sắp xếp:

```csharp
int[] arr = { 30, 10, 50, 20, 40 };

Array.Sort(arr);                 // { 10, 20, 30, 40, 50 } — sửa trực tiếp!
Array.Reverse(arr);              // { 50, 40, 30, 20, 10 }

// Sắp xếp không sửa gốc:
int[] sorted = (int[])arr.Clone();
Array.Sort(sorted);
```

### 5.3 Copy:

```csharp
int[] src = { 1, 2, 3, 4, 5 };

// Clone
int[] copy1 = (int[])src.Clone();

// Array.Copy
int[] copy2 = new int[src.Length];
Array.Copy(src, copy2, src.Length);

// CopyTo
int[] copy3 = new int[src.Length];
src.CopyTo(copy3, 0);
```

### 5.4 Khác:

```csharp
int[] arr = { 1, 2, 3, 4, 5 };

Array.Clear(arr, 0, arr.Length);     // Đặt hết về 0
Array.Fill(arr, 42);                 // Đặt hết = 42
Array.Resize(ref arr, 10);          // Tạo array mới kích thước 10, copy data cũ
```

---

## 6. Mảng 2 chiều (Multidimensional)

### 6.1 Rectangular array (ma trận):

```csharp
// 3 hàng × 4 cột
int[,] matrix = new int[3, 4];
matrix[0, 0] = 1;
matrix[2, 3] = 12;

// Khai báo + gán
int[,] grid = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// Truy cập
Console.WriteLine(grid[1, 2]);  // 6 (hàng 1, cột 2)

// Duyệt
int rows = grid.GetLength(0);    // 3
int cols = grid.GetLength(1);    // 3

for (int r = 0; r < rows; r++)
{
    for (int c = 0; c < cols; c++)
    {
        Console.Write($"{grid[r, c],4}");
    }
    Console.WriteLine();
}
```

### 6.2 Jagged array (mảng răng cưa):

```csharp
// Mảng của mảng — mỗi hàng có thể KHÁC chiều dài
int[][] jagged = new int[3][];
jagged[0] = new int[] { 1, 2 };
jagged[1] = new int[] { 3, 4, 5 };
jagged[2] = new int[] { 6 };

// Truy cập
Console.WriteLine(jagged[1][2]);  // 5

// Duyệt
for (int i = 0; i < jagged.Length; i++)
{
    Console.Write($"Row {i}: ");
    for (int j = 0; j < jagged[i].Length; j++)
        Console.Write($"{jagged[i][j]} ");
    Console.WriteLine();
}
```

### So sánh:

| | `int[,]` Rectangular | `int[][]` Jagged |
|--|---------------------|------------------|
| Kích thước hàng | Giống nhau | Khác nhau được |
| Truy cập | `arr[r, c]` | `arr[r][c]` |
| Memory | 1 block liên tục | Nhiều array riêng |
| Dùng khi | Ma trận, bàn cờ | Data không đều |

---

## 7. Thuật toán cơ bản với Array

### 7.1 Tìm min/max:

```csharp
int[] arr = { 30, 10, 50, 20, 40 };

int min = arr[0], max = arr[0];
for (int i = 1; i < arr.Length; i++)
{
    if (arr[i] < min) min = arr[i];
    if (arr[i] > max) max = arr[i];
}
Console.WriteLine($"Min: {min}, Max: {max}");  // 10, 50
```

### 7.2 Tính tổng/trung bình:

```csharp
double sum = 0;
foreach (int x in arr)
    sum += x;
double avg = sum / arr.Length;
```

### 7.3 Đếm phần tử thỏa điều kiện:

```csharp
int countEven = 0;
foreach (int x in arr)
    if (x % 2 == 0) countEven++;
```

### 7.4 Bubble Sort (tự viết):

```csharp
int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

for (int i = 0; i < arr.Length - 1; i++)
{
    for (int j = 0; j < arr.Length - 1 - i; j++)
    {
        if (arr[j] > arr[j + 1])
        {
            // Swap
            int temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;
        }
    }
}
// arr = { 11, 12, 22, 25, 34, 64, 90 }
```

### 7.5 Tìm kiếm tuyến tính:

```csharp
static int LinearSearch(int[] arr, int target)
{
    for (int i = 0; i < arr.Length; i++)
        if (arr[i] == target)
            return i;       // Tìm thấy → trả vị trí
    return -1;              // Không tìm thấy
}
```

---

## 8. Sai lầm phổ biến

### ❌ Index out of range:
```csharp
int[] arr = new int[5];
arr[5] = 10;    // ❌ IndexOutOfRangeException! (max index = 4)
```

### ❌ Quên array cố định kích thước:
```csharp
int[] arr = new int[3];
// arr.Add(4);    // ❌ Không có Add! Array cố định!
// Cần thêm → dùng List<T> (Phase 3)
```

### ❌ Gán array = share reference:
```csharp
int[] a = { 1, 2, 3 };
int[] b = a;
b[0] = 999;         // a[0] cũng = 999!
```

### ❌ Duyệt sai giới hạn:
```csharp
// ❌ <= gây lỗi
for (int i = 0; i <= arr.Length; i++)  // Lỗi tại i = Length!
// ✅
for (int i = 0; i < arr.Length; i++)
```

---

## 9. Best Practices

1. **Dùng `foreach`** khi chỉ đọc — gọn và an toàn
2. **Dùng `for`** khi cần index hoặc sửa phần tử
3. **`.Length`** thay vì hardcode số — tránh lỗi khi resize
4. **Kiểm tra bounds** trước khi truy cập: `if (i >= 0 && i < arr.Length)`
5. **Clone** khi cần copy thật — `(int[])arr.Clone()`
6. **`Array.Sort()`** cho sắp xếp nhanh — không cần tự viết
7. **`List<T>`** khi cần thêm/xóa linh hoạt (sẽ học Phase 3)
8. **Mảng 2D** cho bảng, ma trận — `int[,]` hoặc `int[][]`
