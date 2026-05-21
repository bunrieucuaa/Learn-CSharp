# 📘 Lesson 06 — Switch Case trong C#

---

## 1. Switch là gì? Khi nào dùng?

`switch` dùng để so sánh **một giá trị** với **nhiều trường hợp cụ thể**. Nó thay thế chuỗi `if...else if` khi bạn đang so sánh bằng (`==`).

### Khi nào dùng Switch thay If/Else?

| Dùng `if/else` | Dùng `switch` |
|----------------|---------------|
| So sánh **phạm vi** (`>`, `<`, `>=`) | So sánh **giá trị cụ thể** (`==`) |
| Điều kiện **phức tạp** (`&&`, `\|\|`) | Nhiều case của **cùng 1 biến** |
| Ít nhánh (2-3) | Nhiều nhánh (4+) |

```csharp
// ❌ If/else dài khi so sánh giá trị cụ thể
if (day == 1) Console.WriteLine("Thứ 2");
else if (day == 2) Console.WriteLine("Thứ 3");
else if (day == 3) Console.WriteLine("Thứ 4");
// ... quá dài!

// ✅ Switch — gọn và rõ ràng hơn
switch (day)
{
    case 1: Console.WriteLine("Thứ 2"); break;
    case 2: Console.WriteLine("Thứ 3"); break;
    case 3: Console.WriteLine("Thứ 4"); break;
}
```

---

## 2. Cú pháp Switch truyền thống

```csharp
switch (variable)
{
    case value1:
        // Code khi variable == value1
        break;
    case value2:
        // Code khi variable == value2
        break;
    case value3:
        // Code khi variable == value3
        break;
    default:
        // Code khi không match case nào
        break;
}
```

### Ví dụ:

```csharp
Console.Write("Nhập ngày (1-7): ");
int.TryParse(Console.ReadLine(), out int day);

switch (day)
{
    case 1:
        Console.WriteLine("Thứ Hai");
        break;
    case 2:
        Console.WriteLine("Thứ Ba");
        break;
    case 3:
        Console.WriteLine("Thứ Tư");
        break;
    case 4:
        Console.WriteLine("Thứ Năm");
        break;
    case 5:
        Console.WriteLine("Thứ Sáu");
        break;
    case 6:
        Console.WriteLine("Thứ Bảy");
        break;
    case 7:
        Console.WriteLine("Chủ Nhật");
        break;
    default:
        Console.WriteLine("❌ Ngày không hợp lệ!");
        break;
}
```

### ⚠️ Bắt buộc có `break`!

```csharp
// C# BẮT BUỘC có break (hoặc return/goto/throw) ở cuối mỗi case
// Khác JavaScript — JS cho phép "fall through" (không có break → chạy tiếp case sau)

switch (x)
{
    case 1:
        DoSomething();
        // ❌ LỖI COMPILE! Thiếu break
    case 2:
        DoOther();
        break;
}
```

### So sánh với JavaScript:

```javascript
// JS — fall through nếu quên break
switch (x) {
    case 1:
        console.log("A");
        // Quên break → chạy tiếp case 2! (fall through)
    case 2:
        console.log("B");
        break;
}
// Input x=1 → Output: "A" "B" (chạy cả 2!)
```

```csharp
// C# — KHÔNG cho fall through (trừ case rỗng)
switch (x)
{
    case 1:
        Console.WriteLine("A");
        break;  // BẮT BUỘC!
    case 2:
        Console.WriteLine("B");
        break;
}
```

> 🔑 **C# an toàn hơn JS**: buộc bạn viết `break`, tránh bug fall-through.

---

## 3. Multiple Cases — Gộp nhiều case

```csharp
// Nhiều case cùng xử lý giống nhau
switch (day)
{
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        Console.WriteLine("Ngày làm việc");
        break;
    case 6:
    case 7:
        Console.WriteLine("Cuối tuần");
        break;
    default:
        Console.WriteLine("Không hợp lệ");
        break;
}
```

> 💡 Case rỗng (không có code) được phép **fall through** — đây là cách duy nhất C# cho phép.

---

## 4. Switch với `string`

```csharp
Console.Write("Chọn màu (red/green/blue): ");
string? color = Console.ReadLine()?.Trim().ToLower();

switch (color)
{
    case "red":
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("🔴 Đỏ!");
        break;
    case "green":
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🟢 Xanh lá!");
        break;
    case "blue":
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("🔵 Xanh dương!");
        break;
    case null:
    case "":
        Console.WriteLine("❌ Không nhập gì!");
        break;
    default:
        Console.WriteLine($"❌ Màu \"{color}\" không hỗ trợ!");
        break;
}
Console.ResetColor();
```

> 💡 Luôn `Trim().ToLower()` input trước khi switch — tránh lỗi khoảng trắng và hoa/thường.

---

## 5. Switch Expression (C# 8+) ⭐

Cú pháp **hiện đại và gọn hơn** — trả về giá trị trực tiếp:

```csharp
// Switch truyền thống — dài
string dayName;
switch (day)
{
    case 1: dayName = "Thứ 2"; break;
    case 2: dayName = "Thứ 3"; break;
    case 3: dayName = "Thứ 4"; break;
    default: dayName = "N/A"; break;
}

// Switch Expression — gọn ⭐
string dayName = day switch
{
    1 => "Thứ 2",
    2 => "Thứ 3",
    3 => "Thứ 4",
    _ => "N/A"    // _ = default
};
```

### Cú pháp:

```csharp
var result = variable switch
{
    pattern1 => value1,
    pattern2 => value2,
    _ => defaultValue       // _ bắt buộc (hoặc cover hết case)
};                          // Chú ý: có dấu ; ở cuối!
```

### So sánh:

| | Switch truyền thống | Switch Expression |
|--|---------------------|-------------------|
| Cú pháp | `switch (x) { case: ... break; }` | `x switch { pattern => value }` |
| Trả giá trị | Không (gán trong case) | Có (trả trực tiếp) |
| `default` | `default:` | `_` |
| Kết thúc case | `break` | `,` (dấu phẩy) |
| Dùng khi | Logic phức tạp trong case | Gán giá trị dựa trên pattern |

---

## 6. Pattern Matching trong Switch (C# 9+) ⭐

### 6.1 Relational Patterns — So sánh phạm vi:

```csharp
string category = score switch
{
    >= 9.0 => "Xuất sắc",
    >= 8.0 => "Giỏi",
    >= 6.5 => "Khá",
    >= 5.0 => "Trung bình",
    >= 0 => "Yếu",
    _ => "Không hợp lệ"
};
```

> 🔑 Đây là tính năng **cực mạnh** — giờ switch CÓ THỂ thay thế chuỗi `if...else if` cho phạm vi!

### 6.2 `when` Guard — Thêm điều kiện:

```csharp
switch (score)
{
    case >= 9.0:
        Console.WriteLine("Xuất sắc");
        break;
    case >= 5.0 when isPassed:  // Thêm điều kiện phụ
        Console.WriteLine("Đậu");
        break;
    case >= 5.0:
        Console.WriteLine("Đậu nhưng chưa xác nhận");
        break;
    default:
        Console.WriteLine("Rớt");
        break;
}

// Trong switch expression:
string result = score switch
{
    >= 9.0 => "Xuất sắc",
    >= 5.0 when hasBonus => "Đậu + Thưởng",
    >= 5.0 => "Đậu",
    _ => "Rớt"
};
```

### 6.3 Type Pattern — Kiểm tra kiểu:

```csharp
object data = 42;

string description = data switch
{
    int n when n > 0 => $"Số nguyên dương: {n}",
    int n => $"Số nguyên: {n}",
    double d => $"Số thực: {d}",
    string s => $"Chuỗi: {s}",
    null => "Null!",
    _ => $"Kiểu khác: {data.GetType().Name}"
};

Console.WriteLine(description);
```

### 6.4 Tuple Pattern — So sánh nhiều giá trị:

```csharp
// So sánh 2 giá trị cùng lúc
string result = (role, action) switch
{
    ("admin", "delete") => "✅ Admin được phép xóa",
    ("admin", _) => "✅ Admin được phép làm mọi thứ",
    ("user", "read") => "✅ User được phép đọc",
    ("user", "write") => "✅ User được phép viết",
    ("user", "delete") => "❌ User KHÔNG được xóa",
    ("guest", "read") => "✅ Guest chỉ được đọc",
    ("guest", _) => "❌ Guest không có quyền này",
    _ => "❌ Role không xác định"
};
```

> 💡 **Tuple pattern** cực mạnh cho bảng phân quyền, state machine, game logic!

### 6.5 Property Pattern:

```csharp
// Kiểm tra property của object (sẽ dùng nhiều khi học OOP)
var greeting = dateTime switch
{
    { Hour: < 12 } => "Chào buổi sáng!",
    { Hour: < 18 } => "Chào buổi chiều!",
    _ => "Chào buổi tối!"
};
```

---

## 7. `goto case` — Nhảy sang case khác

```csharp
switch (level)
{
    case 3:
        Console.WriteLine("Level 3 bonus!");
        goto case 2;  // Nhảy sang case 2
    case 2:
        Console.WriteLine("Level 2 bonus!");
        goto case 1;
    case 1:
        Console.WriteLine("Level 1 bonus!");
        break;
    default:
        Console.WriteLine("No bonus");
        break;
}

// Input level = 3:
// Output: "Level 3 bonus!" → "Level 2 bonus!" → "Level 1 bonus!"
```

> ⚠️ `goto case` ít dùng. Biết là có, nhưng **tránh dùng** vì code khó đọc.

---

## 8. Sai lầm phổ biến

### ❌ Sai lầm 1: Quên `break`

```csharp
switch (x)
{
    case 1:
        DoA();
        // ❌ LỖI COMPILE! C# bắt buộc break
    case 2:
        DoB();
        break;
}
```

### ❌ Sai lầm 2: Dùng switch cho phạm vi (truyền thống)

```csharp
// ❌ Switch truyền thống KHÔNG so sánh phạm vi được
switch (score)
{
    case > 90:  // ❌ LỖI trong switch truyền thống!
        break;
}

// ✅ Dùng switch EXPRESSION với pattern matching
string grade = score switch
{
    > 90 => "A",   // ✅ OK trong switch expression!
    > 80 => "B",
    _ => "C"
};
```

### ❌ Sai lầm 3: Quên `_` trong switch expression

```csharp
// ❌ LỖI nếu không cover hết case
string result = day switch
{
    1 => "Mon",
    2 => "Tue"
    // Thiếu _ hoặc các case còn lại!
};
```

### ❌ Sai lầm 4: Không normalize string trước khi switch

```csharp
// ❌ User nhập "RED" → không match "red"
switch (input)
{
    case "red": ...
}

// ✅
switch (input?.Trim().ToLower())
{
    case "red": ...
}
```

---

## 9. Best Practices

1. **Switch expression** cho gán giá trị — gọn hơn truyền thống
2. **Luôn có `default` / `_`** — xử lý trường hợp không mong đợi
3. **`Trim().ToLower()`** string trước khi switch
4. **Pattern matching** cho phạm vi — thay thế if/else if
5. **Tuple pattern** cho logic phụ thuộc nhiều biến
6. **Tránh `goto case`** — code khó đọc
7. **Khi case > 10**: nghĩ đến dùng **Dictionary** hoặc **Enum** thay thế
