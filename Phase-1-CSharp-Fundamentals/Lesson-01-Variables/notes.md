# 📝 Lesson 01 — Notes: Variables

---

## 🧠 Tóm tắt kiến thức

### 1. Khai báo biến trong C#:

```csharp
<kiểu> <tên> = <giá_trị>;

int age = 25;
string name = "Minh";
```

### 2. C# là strongly typed:
- Mỗi biến có **1 kiểu duy nhất**, không thể đổi
- Khác JS — JS cho phép đổi kiểu tự do

### 3. Các kiểu dữ liệu cơ bản:

```
int       → số nguyên           → 42
double    → số thực 64-bit      → 3.14
float     → số thực 32-bit      → 3.14f  (cần hậu tố f)
decimal   → số chính xác cao    → 99.99m (cần hậu tố m)
string    → chuỗi ký tự         → "Hello"
char      → 1 ký tự             → 'A'    (nháy đơn!)
bool      → true/false          → true
```

### 4. `var` — Type Inference:
- Compiler tự suy kiểu từ giá trị
- Kiểu **vẫn cố định** — không thể đổi sau đó
- **Bắt buộc** gán giá trị khi dùng `var`

### 5. `const` — Hằng số:
- Giá trị **không thể thay đổi** sau khi khai báo
- Phải biết giá trị **lúc compile** (không phải runtime)

### 6. String Interpolation:
```csharp
$"Tên: {name}, Tuổi: {age}"
// Tương tự JS: `Tên: ${name}, Tuổi: ${age}`
```

### 7. Naming Convention:
- Local variable: `camelCase` → `studentAge`
- Constant: `PascalCase` hoặc `UPPER_CASE` → `MaxRetry`
- Private field: `_camelCase` → `_count`

---

## ⚠️ Những lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Quên `;` cuối dòng | C# **bắt buộc** dấu `;` — khác JS |
| 2 | Gán sai kiểu: `int x = "abc"` | Kiểm tra kiểu trước khi gán |
| 3 | Dùng `var` không gán giá trị | `var x;` ❌ — phải có `= giá_trị` |
| 4 | Nhầm `char` (`'A'`) và `string` (`"A"`) | Nháy đơn = char, nháy kép = string |
| 5 | Quên hậu tố `f` hoặc `m` | `float x = 3.14f;` `decimal x = 99.99m;` |
| 6 | Dùng biến chưa gán giá trị | C# không cho dùng biến local chưa gán |
| 7 | Nghĩ `var` giống JS (đổi kiểu được) | `var` trong C# = kiểu cố định! |

---

## 💡 Mindset quan trọng

### 1. "C# nghiêm khắc = C# an toàn"
- C# bắt lỗi **lúc compile** → bạn sửa lỗi **trước khi chạy**
- JS cho qua lỗi → lỗi phát sinh **lúc runtime** → khó debug hơn

### 2. "Chọn kiểu dữ liệu là một quyết định thiết kế"
- `int` vs `long` → phạm vi số
- `double` vs `decimal` → độ chính xác
- Chọn sai kiểu = bug tiềm ẩn

### 3. "Tên biến là documentation"
- `int a` → người đọc không hiểu
- `int studentAge` → tự giải thích ý nghĩa
- Code tốt = code tự giải thích

### 4. "Dùng `const` cho mọi thứ không đổi"
- Thuế suất, phí cố định, tỷ lệ → `const`
- Tránh **magic number** — số xuất hiện không rõ ý nghĩa

---

## ✅ Checklist "Đã hiểu chưa?"

Hãy tự trả lời các câu hỏi sau. Nếu trả lời được hết → bạn đã hiểu bài!

- [✅] Tôi biết khai báo biến với kiểu dữ liệu rõ ràng (`int`, `string`, `bool`...)
- [✅] Tôi hiểu sự khác biệt giữa C# (strongly typed) và JS (weakly typed)
- [✅] Tôi biết khi nào dùng `int`, `double`, `float`, `decimal`
- [✅] Tôi biết `char` dùng nháy đơn, `string` dùng nháy kép
- [x] Tôi biết `float` cần hậu tố `f`, `decimal` cần hậu tố `m`
- [✅] Tôi hiểu `var` trong C# khác `var`/`let` trong JS thế nào
- [✅] Tôi biết dùng `const` cho giá trị không đổi
- [✅] Tôi biết dùng string interpolation `$"...{biến}..."`
- [✅] Tôi biết quy tắc đặt tên biến (camelCase cho local variable)
- [✅] Tôi hiểu tại sao C# bắt buộc gán giá trị trước khi dùng biến local

---

## 🔗 Liên kết kiến thức

```
Lesson 01: Variables ← BẠN Ở ĐÂY
    ↓
Lesson 02: Data Types (đi sâu hơn vào kiểu dữ liệu)
    ↓
Lesson 03: Operators (phép tính trên các kiểu dữ liệu)
    ↓
Lesson 09: Scope (biến sống ở đâu, chết khi nào?)
    ↓
Lesson 11: Memory Basics (Stack vs Heap, Value vs Reference)
```

> 📌 **Bài tiếp theo:** Lesson 02 — Data Types: Đi sâu vào các kiểu dữ liệu, ép kiểu (casting), overflow, và cách C# quản lý kiểu dữ liệu.
