# 📘 Lesson 07 — Loop (Vòng lặp) trong C#

---

## 1. Vòng lặp là gì?

Vòng lặp = **lặp lại** một đoạn code nhiều lần cho đến khi điều kiện không còn đúng.

```
┌──────────────┐
│ Kiểm tra     │◀──────────────┐
│ điều kiện    │               │
└──────┬───────┘               │
       │                       │
  true │     false             │
       ▼       ▼               │
  ┌────────┐  THOÁT            │
  │ Thực   │                   │
  │ hiện   │───────────────────┘
  │ code   │
  └────────┘
```

C# có **4 loại** vòng lặp:

| Loại | Khi nào dùng |
|------|-------------|
| `for` | Biết trước số lần lặp |
| `while` | Lặp cho đến khi điều kiện sai |
| `do...while` | Giống while nhưng chạy **ít nhất 1 lần** |
| `foreach` | Duyệt qua từng phần tử của collection |

---

## 2. Vòng lặp `for`

### Cú pháp:

```csharp
for (khởi_tạo; điều_kiện; cập_nhật)
{
    // Code lặp lại
}
```

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Lần {i}");
}
// Output: Lần 0, Lần 1, Lần 2, Lần 3, Lần 4
```

### Phân tích 3 phần:

```csharp
for (int i = 0;   i < 5;    i++)
//   ↑ Khởi tạo   ↑ Điều kiện  ↑ Cập nhật
//   Chạy 1 lần    Check mỗi    Chạy SAU mỗi
//   đầu tiên      vòng lặp     vòng lặp
```

**Thứ tự thực thi:**
1. `int i = 0` (1 lần duy nhất)
2. `i < 5` → true → chạy body
3. Body xong → `i++` → quay lại bước 2
4. Khi `i < 5` → false → THOÁT

### Lặp ngược:

```csharp
for (int i = 10; i >= 1; i--)
{
    Console.Write($"{i} ");
}
// Output: 10 9 8 7 6 5 4 3 2 1
```

### Bước nhảy khác 1:

```csharp
// Chẵn: 0, 2, 4, 6, 8
for (int i = 0; i <= 8; i += 2)
    Console.Write($"{i} ");

// Bội của 5: 0, 5, 10, 15, 20
for (int i = 0; i <= 20; i += 5)
    Console.Write($"{i} ");
```

### Vòng for lồng nhau (Nested):

```csharp
// Bảng cửu chương
for (int i = 2; i <= 9; i++)
{
    Console.WriteLine($"--- Bảng {i} ---");
    for (int j = 1; j <= 10; j++)
    {
        Console.WriteLine($"  {i} × {j} = {i * j}");
    }
    Console.WriteLine();
}
```

---

## 3. Vòng lặp `while`

### Cú pháp:

```csharp
while (điều_kiện)
{
    // Code lặp lại
    // Phải có cách để điều kiện thành false!
}
```

```csharp
int count = 0;
while (count < 5)
{
    Console.WriteLine($"Count: {count}");
    count++;  // Quan trọng! Không có → vòng lặp vô tận
}
```

### Khi nào dùng `while`?

- Không biết trước số lần lặp
- Lặp cho đến khi user nhập đúng
- Đọc dữ liệu cho đến khi hết

```csharp
// Nhập cho đến khi đúng
int age;
while (true)
{
    Console.Write("Nhập tuổi (1-120): ");
    if (int.TryParse(Console.ReadLine(), out age) && age >= 1 && age <= 120)
        break;
    Console.WriteLine("❌ Không hợp lệ!");
}
```

---

## 4. Vòng lặp `do...while`

```csharp
do
{
    // Code — chạy ÍT NHẤT 1 LẦN
} while (điều_kiện);  // Chú ý dấu ; ở cuối!
```

### Khác `while` ở đâu?

```csharp
// while — kiểm tra TRƯỚC, có thể không chạy lần nào
int x = 10;
while (x < 5)
{
    Console.WriteLine("while");  // KHÔNG chạy!
}

// do...while — chạy TRƯỚC, kiểm tra SAU
int y = 10;
do
{
    Console.WriteLine("do-while");  // Chạy 1 lần!
} while (y < 5);
```

### Ứng dụng phổ biến: Menu

```csharp
string? choice;
do
{
    Console.WriteLine("\n1. Xem   2. Thêm   3. Xóa   0. Thoát");
    Console.Write("Chọn: ");
    choice = Console.ReadLine();

    switch (choice)
    {
        case "1": Console.WriteLine("Đang xem..."); break;
        case "2": Console.WriteLine("Đang thêm..."); break;
        case "3": Console.WriteLine("Đang xóa..."); break;
        case "0": Console.WriteLine("Tạm biệt!"); break;
        default: Console.WriteLine("❌ Không hợp lệ!"); break;
    }
} while (choice != "0");
```

---

## 5. Vòng lặp `foreach`

```csharp
foreach (var item in collection)
{
    // Dùng item
}
```

```csharp
string[] fruits = { "Táo", "Cam", "Xoài", "Dưa" };

foreach (string fruit in fruits)
{
    Console.WriteLine($"🍎 {fruit}");
}
```

### So sánh `for` vs `foreach`:

```csharp
string[] names = { "An", "Bình", "Chi" };

// for — có index
for (int i = 0; i < names.Length; i++)
    Console.WriteLine($"{i + 1}. {names[i]}");

// foreach — gọn hơn, không cần index
foreach (string name in names)
    Console.WriteLine($"• {name}");
```

| | `for` | `foreach` |
|--|-------|-----------|
| Có index | ✅ Có `i` | ❌ Không (phải tự đếm) |
| Sửa collection | ✅ Được | ❌ Không được |
| Đọc gọn | Dài hơn | ✅ Ngắn hơn |
| Dùng khi | Cần index / sửa | Chỉ đọc |

### ⚠️ Không sửa collection trong foreach:

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

// ❌ CRASH! InvalidOperationException
foreach (int n in numbers)
{
    if (n == 2) numbers.Remove(n);  // Không được sửa!
}
```

---

## 6. `break` và `continue`

### `break` — Thoát vòng lặp ngay lập tức:

```csharp
for (int i = 0; i < 100; i++)
{
    if (i == 5) break;  // Dừng khi i = 5
    Console.Write($"{i} ");
}
// Output: 0 1 2 3 4
```

### `continue` — Bỏ qua lần lặp hiện tại, sang lần tiếp:

```csharp
for (int i = 0; i < 10; i++)
{
    if (i % 2 != 0) continue;  // Bỏ qua số lẻ
    Console.Write($"{i} ");
}
// Output: 0 2 4 6 8
```

### Minh họa:

```
for (0 → 9):
  i=0: ✅ In
  i=1: continue (lẻ) → BỎ QUA
  i=2: ✅ In
  i=3: continue (lẻ) → BỎ QUA
  ...
  i=5: break → THOÁT HẲN
```

> 💡 `break` = **thoát vòng lặp**. `continue` = **bỏ qua lần này, lặp tiếp**.

---

## 7. Vòng lặp vô tận — Infinite Loop

```csharp
// Cách 1: while (true)
while (true)
{
    // Phải có break để thoát!
    Console.Write("Nhập 'quit' để thoát: ");
    if (Console.ReadLine() == "quit") break;
}

// Cách 2: for (;;)
for (;;)
{
    // Tương đương while (true)
    break;
}
```

> ⚠️ Nếu quên `break` → chương trình **treo**, phải Ctrl+C để dừng.

---

## 8. So sánh với JavaScript

```javascript
// JS — giống hệt cú pháp
for (let i = 0; i < 5; i++) { ... }
while (condition) { ... }
do { ... } while (condition);
for (const item of array) { ... }  // ≈ foreach
for (const key in object) { ... }  // C# không có tương đương trực tiếp
```

| C# | JavaScript |
|----|-----------|
| `foreach (var x in arr)` | `for (const x of arr)` |
| Không có | `for (const key in obj)` |
| `break` / `continue` | Giống |
| Phải là `bool` trong while | Truthy/falsy cho phép |

---

## 9. Sai lầm phổ biến

### ❌ Quên cập nhật biến trong while:

```csharp
int i = 0;
while (i < 5)
{
    Console.WriteLine(i);
    // Quên i++ → vòng lặp VÔ TẬN!
}
```

### ❌ Off-by-one (lệch 1):

```csharp
// Muốn in 1-10 nhưng:
for (int i = 0; i < 10; i++)  // 0-9 ← thiếu 10!
for (int i = 1; i <= 10; i++) // 1-10 ✅
```

### ❌ Sửa collection trong foreach:

```csharp
foreach (var item in list)
    list.Remove(item);  // ❌ CRASH!
```

### ❌ Break chỉ thoát 1 vòng lặp:

```csharp
for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        if (j == 2) break;  // Chỉ thoát vòng j, vòng i tiếp tục!
    }
}
```

---

## 10. Best Practices

1. **`for`** khi biết trước số lần, **`while`** khi không biết
2. **`foreach`** khi chỉ đọc collection — gọn và an toàn
3. **Luôn đảm bảo** vòng lặp có điểm dừng
4. **Tránh nested loop > 2 cấp** — refactor thành method
5. **Dùng `break` thận trọng** — đôi khi viết điều kiện while rõ ràng hơn
6. **Đặt tên biến loop có ý nghĩa** khi nested: `row`, `col` thay vì `i`, `j`
