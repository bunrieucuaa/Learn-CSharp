# ✏️ Lesson 09 — Bài tập: Scope & Lifetime

---

## Bài 1: Dự đoán Output 🔮

### Yêu cầu:
Dự đoán output **trên giấy** trước khi chạy:

**Câu A:**
```csharp
int x = 10;
if (x > 5)
{
    int y = 20;
    x += y;
}
Console.WriteLine(x);
// Console.WriteLine(y);  // Dòng này nếu bỏ comment thì sao?
```

**Câu B:**
```csharp
int sum = 0;
for (int i = 1; i <= 5; i++)
{
    int square = i * i;
    sum += square;
}
Console.WriteLine(sum);
// Console.WriteLine(i);       // Sao?
// Console.WriteLine(square);  // Sao?
```

**Câu C:**
```csharp
int a = 5;
Modify(a);
Console.WriteLine(a);

static void Modify(int x) { x = 100; }
```

**Câu D:**
```csharp
int[] arr = { 1, 2, 3 };
Change(arr);
Console.WriteLine(arr[0]);

static void Change(int[] data) { data[0] = 999; }
```

---

## Bài 2: Fix Scope Bugs 🐛

### Yêu cầu:
Code dưới đây có lỗi scope. Tìm và sửa (KHÔNG thay đổi logic):

```csharp
// Bug 1
Console.Write("Nhập điểm: ");
if (double.TryParse(Console.ReadLine(), out double score))
{
    string grade = score >= 5 ? "Đậu" : "Rớt";
}
Console.WriteLine($"Kết quả: {grade}");

// Bug 2
for (int i = 0; i < 5; i++)
{
    string name = $"Item {i}";
}
Console.WriteLine($"Last: {name}");

// Bug 3
int total = 0;
while (total < 100)
{
    int increment = 15;
    total += increment;
}
Console.WriteLine($"Total: {total}, tăng mỗi lần: {increment}");
```

---

## Bài 3: Refactor — Chọn scope đúng ✅

### Yêu cầu:
Code dưới đây hoạt động nhưng scope không tốt. Refactor:

```csharp
// ❌ Quá nhiều biến class-level
static string name = "";
static int age = 0;
static double score1 = 0, score2 = 0, score3 = 0;
static double average = 0;
static string grade = "";
static bool passed = false;

static void Main()
{
    Console.Write("Tên: ");
    name = Console.ReadLine()!;
    Console.Write("Tuổi: ");
    age = int.Parse(Console.ReadLine()!);
    Console.Write("Điểm 1: ");
    score1 = double.Parse(Console.ReadLine()!);
    Console.Write("Điểm 2: ");
    score2 = double.Parse(Console.ReadLine()!);
    Console.Write("Điểm 3: ");
    score3 = double.Parse(Console.ReadLine()!);

    average = (score1 + score2 + score3) / 3;
    grade = average >= 8 ? "Giỏi" : average >= 5 ? "TB" : "Yếu";
    passed = average >= 5;

    Console.WriteLine($"{name}, {age} tuổi, TB: {average:F1}, {grade}, {(passed ? "Đậu" : "Rớt")}");
}
```

**Yêu cầu refactor:**
- Chuyển TẤT CẢ biến class-level thành local (trừ const nếu cần)
- Tách thành methods: `ReadInput()`, `Calculate()`, `PrintResult()`
- Dùng TryParse thay Parse

---

## Bài 4: Value vs Reference — Thí nghiệm 🧪

### Yêu cầu:
Viết chương trình thí nghiệm và in kết quả:

1. Truyền `int` vào method → sửa → kiểm tra
2. Truyền `int` bằng `ref` → sửa → kiểm tra
3. Truyền `int[]` → sửa phần tử → kiểm tra
4. Truyền `int[]` → gán array mới → kiểm tra (array gốc thay đổi không?)
5. Truyền `string` → sửa → kiểm tra

In bảng kết quả so sánh trước/sau cho mỗi thí nghiệm.

### Expected Output:
```
═══ THÍ NGHIỆM VALUE vs REFERENCE ═══

│ TN │ Kiểu           │ Trước │ Sau    │ Đổi? │
├────┼────────────────┼───────┼────────┼──────┤
│  1 │ int (value)    │ 10    │ 10     │ ❌   │
│  2 │ int (ref)      │ 10    │ 999    │ ✅   │
│  3 │ int[] (modify) │ [1]   │ [999]  │ ✅   │
│  4 │ int[] (reassign)│ [1]  │ [1]    │ ❌   │
│  5 │ string         │ "Hi"  │ "Hi"   │ ❌   │
```

---

## Bài 5: Scope trong ứng dụng thực tế 📋

### Yêu cầu:
Viết chương trình quản lý todo list mini. Áp dụng scope đúng cách:

- Class-level: chỉ `const` và mảng data (shared state)
- Method-level: tất cả biến tính toán
- Block-level: biến chỉ dùng trong if/for

**Chức năng:** Thêm, Xem, Đánh dấu hoàn thành, Xóa, Thống kê

**Quy tắc:** KHÔNG được dùng biến class-level cho bất cứ gì ngoài data arrays và const.
