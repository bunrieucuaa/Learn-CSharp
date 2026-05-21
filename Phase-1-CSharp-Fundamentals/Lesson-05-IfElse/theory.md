# 📘 Lesson 05 — If/Else (Cấu trúc rẽ nhánh) trong C#

---

## 1. Tại sao cần If/Else?

Chương trình không phải lúc nào cũng chạy thẳng từ trên xuống dưới. Đôi khi cần **quyết định**:

```
              ┌──── true ───▶ Làm A
 Điều kiện? ──┤
              └──── false ──▶ Làm B
```

> 🔑 If/Else giúp chương trình **rẽ nhánh** — chọn hành động dựa trên điều kiện.

### So sánh với JavaScript:

Cú pháp **gần như giống hệt** JS! Điểm khác duy nhất: **điều kiện trong C# PHẢI là `bool`**.

```javascript
// JS — cho phép truthy/falsy
if (0) { ... }         // OK (false)
if ("") { ... }        // OK (false)
if ("hello") { ... }   // OK (true)
```

```csharp
// C# — CHỈ chấp nhận bool
if (0) { ... }         // ❌ LỖI COMPILE! int không phải bool
if ("") { ... }        // ❌ LỖI COMPILE! string không phải bool
if (age > 18) { ... }  // ✅ OK — so sánh trả về bool
if (isActive) { ... }  // ✅ OK — isActive là bool
```

---

## 2. Cú pháp cơ bản

### 2.1 `if` đơn giản

```csharp
if (condition)
{
    // Code chạy khi condition = true
}
```

```csharp
int age = 20;

if (age >= 18)
{
    Console.WriteLine("Bạn đủ tuổi bầu cử!");
}
// Nếu age < 18 → không in gì cả
```

### 2.2 `if...else`

```csharp
if (condition)
{
    // Code khi true
}
else
{
    // Code khi false
}
```

```csharp
int score = 45;

if (score >= 50)
{
    Console.WriteLine("✅ Đậu!");
}
else
{
    Console.WriteLine("❌ Rớt!");
}
```

### 2.3 `if...else if...else` (nhiều nhánh)

```csharp
int score = 75;

if (score >= 90)
{
    Console.WriteLine("Xuất sắc");
}
else if (score >= 80)
{
    Console.WriteLine("Giỏi");
}
else if (score >= 65)
{
    Console.WriteLine("Khá");       // ← Chạy cái này
}
else if (score >= 50)
{
    Console.WriteLine("Trung bình");
}
else
{
    Console.WriteLine("Yếu");
}
```

### ⚠️ Thứ tự quan trọng!

```csharp
// ❌ SAI — luôn vào nhánh đầu tiên
if (score >= 50)       // 75 >= 50 → true → vào đây!
    Console.WriteLine("Trung bình");
else if (score >= 65)  // Không bao giờ tới
    Console.WriteLine("Khá");
else if (score >= 80)  // Không bao giờ tới
    Console.WriteLine("Giỏi");

// ✅ ĐÚNG — kiểm tra từ CAO xuống THẤP
if (score >= 80)
    Console.WriteLine("Giỏi");
else if (score >= 65)
    Console.WriteLine("Khá");
else if (score >= 50)
    Console.WriteLine("Trung bình");
else
    Console.WriteLine("Yếu");
```

> 🔑 **Khi dùng `else if`, kiểm tra điều kiện từ NGHIÊM NGẶT nhất trước.**

---

## 3. Ngoặc nhọn `{}` — Bắt buộc hay không?

```csharp
// Nếu chỉ có 1 dòng → có thể bỏ {}
if (age >= 18)
    Console.WriteLine("Đủ tuổi");

// Nếu có 2+ dòng → BẮT BUỘC có {}
if (age >= 18)
{
    Console.WriteLine("Đủ tuổi");
    Console.WriteLine("Được bầu cử");
}
```

### ⚠️ Bẫy kinh điển khi bỏ `{}`:

```csharp
// BUG — dòng thứ 2 LUÔN chạy, không phụ thuộc if!
if (age >= 18)
    Console.WriteLine("Đủ tuổi");
    Console.WriteLine("Được bầu cử");  // ← LUÔN chạy! Không thuộc if!

// Tương đương:
if (age >= 18)
{
    Console.WriteLine("Đủ tuổi");
}
Console.WriteLine("Được bầu cử");  // Nằm NGOÀI if
```

> 💡 **Best Practice:** **LUÔN dùng `{}`** — ngay cả khi chỉ 1 dòng. Tránh bug, dễ maintain.

---

## 4. Nested If — If lồng nhau

```csharp
int age = 25;
bool hasLicense = true;

if (age >= 18)
{
    if (hasLicense)
    {
        Console.WriteLine("✅ Được lái xe!");
    }
    else
    {
        Console.WriteLine("⚠️ Đủ tuổi nhưng chưa có bằng lái!");
    }
}
else
{
    Console.WriteLine("❌ Chưa đủ tuổi lái xe!");
}
```

### Viết gọn hơn bằng `&&`:

```csharp
// Tương đương nested if ở trên (phần "được lái xe")
if (age >= 18 && hasLicense)
{
    Console.WriteLine("✅ Được lái xe!");
}
```

> 💡 **Best Practice:** Nested if tối đa **2-3 cấp**. Sâu hơn → refactor bằng `&&`, `||`, hoặc tách method.

---

## 5. Ternary Operator — Rút gọn If/Else

```csharp
// If/Else đơn giản
string status;
if (score >= 50)
    status = "Đậu";
else
    status = "Rớt";

// Ternary — 1 dòng
string status = score >= 50 ? "Đậu" : "Rớt";
```

### Khi nào dùng ternary?

- ✅ Gán giá trị dựa trên 1 điều kiện đơn giản
- ❌ Logic phức tạp, nhiều nhánh, có side effects

```csharp
// ✅ OK
int max = (a > b) ? a : b;
string display = name ?? "Không tên";

// ❌ KHÔNG nên — quá phức tạp
string grade = s >= 9 ? "Giỏi" : s >= 7 ? "Khá" : s >= 5 ? "TB" : "Yếu";
```

---

## 6. Pattern Matching trong If (C# 7+)

### 6.1 `is` pattern:

```csharp
object value = 42;

if (value is int number)
{
    Console.WriteLine($"Đây là số nguyên: {number}");
}
else if (value is string text)
{
    Console.WriteLine($"Đây là chuỗi: {text}");
}
```

### 6.2 `is not`:

```csharp
string? name = GetName();

if (name is not null)
{
    Console.WriteLine(name.Length);  // An toàn!
}

// Tương đương:
if (name != null) { ... }
```

### 6.3 Relational patterns (C# 9+):

```csharp
int age = 25;

if (age is >= 18 and < 65)
{
    Console.WriteLine("Trong độ tuổi lao động");
}

if (age is < 13 or > 65)
{
    Console.WriteLine("Miễn vé");
}

// Tương đương:
if (age >= 18 && age < 65) { ... }
if (age < 13 || age > 65) { ... }
```

> 💡 Pattern matching là tính năng **mạnh và hiện đại** của C#. JavaScript không có tính năng tương đương!

---

## 7. Guard Clause — Kỹ thuật tránh nested if

```csharp
// ❌ Nested if sâu — khó đọc
void ProcessOrder(string? name, int quantity, decimal price)
{
    if (name != null)
    {
        if (quantity > 0)
        {
            if (price > 0)
            {
                // Logic chính ở đây
                decimal total = quantity * price;
                Console.WriteLine($"Đơn hàng: {name}, Tổng: {total:N0}");
            }
            else
            {
                Console.WriteLine("Giá phải > 0");
            }
        }
        else
        {
            Console.WriteLine("Số lượng phải > 0");
        }
    }
    else
    {
        Console.WriteLine("Tên không được null");
    }
}

// ✅ Guard Clause — đảo điều kiện, return sớm
void ProcessOrder(string? name, int quantity, decimal price)
{
    if (name == null)
    {
        Console.WriteLine("Tên không được null");
        return;
    }
    if (quantity <= 0)
    {
        Console.WriteLine("Số lượng phải > 0");
        return;
    }
    if (price <= 0)
    {
        Console.WriteLine("Giá phải > 0");
        return;
    }

    // Logic chính — không nested!
    decimal total = quantity * price;
    Console.WriteLine($"Đơn hàng: {name}, Tổng: {total:N0}");
}
```

> 🔑 **Guard Clause** = kiểm tra điều kiện sai → `return` sớm → logic chính không bị lồng sâu. Đây là **kỹ thuật clean code quan trọng** dùng rất nhiều trong thực tế.

---

## 8. Sai lầm phổ biến

### ❌ Sai lầm 1: Dùng `=` thay vì `==`

```csharp
// C# bảo vệ bạn — LỖI COMPILE!
if (x = 5) { }   // ❌ Gán, không phải so sánh
if (x == 5) { }  // ✅ So sánh
```

### ❌ Sai lầm 2: Thứ tự else if sai

```csharp
// ❌ score = 95 nhưng vào nhánh "TB" vì 95 >= 50 là true
if (score >= 50) Console.WriteLine("TB");
else if (score >= 80) Console.WriteLine("Giỏi");  // Không bao giờ tới!
```

### ❌ Sai lầm 3: Quên `{}` với nhiều dòng

```csharp
if (condition)
    DoA();
    DoB();  // ← BUG! Luôn chạy, không thuộc if
```

### ❌ Sai lầm 4: So sánh string không chuẩn hóa

```csharp
string input = "  Yes  ";
if (input == "Yes")  // ❌ false! Vì có khoảng trắng

if (input.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))  // ✅
```

### ❌ Sai lầm 5: Nested if quá sâu

```csharp
// ❌ 4-5 cấp if lồng nhau → dùng guard clause hoặc tách method
```

---

## 9. Best Practices

1. **Luôn dùng `{}`** — kể cả 1 dòng
2. **Kiểm tra từ nghiêm ngặt nhất** trong else if
3. **Guard clause** thay vì nested if sâu
4. **Ternary cho gán giá trị đơn giản** — không dùng cho logic phức tạp
5. **`string.Equals(..., StringComparison.OrdinalIgnoreCase)`** khi so sánh string từ input
6. **Tách điều kiện phức tạp** thành biến có tên ý nghĩa:

```csharp
// ❌ Khó đọc
if (age >= 18 && hasID && (isMember || hasCoupon) && balance >= price)

// ✅ Dễ đọc
bool isAdult = age >= 18;
bool isVerified = hasID;
bool hasDiscount = isMember || hasCoupon;
bool canAfford = balance >= price;

if (isAdult && isVerified && hasDiscount && canAfford)
```
