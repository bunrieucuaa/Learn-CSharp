# 📘 Lesson 01 — Variables trong C#

---

## 1. Variable là gì? (Nhắc lại nhanh)

Bạn đã biết variable trong JavaScript rồi — nó là **một cái hộp chứa dữ liệu**, có tên, có giá trị.

Trong C# cũng vậy, nhưng có **một điểm khác cực kỳ quan trọng**:

> 🔑 **C# là ngôn ngữ strongly typed (kiểu mạnh).**
> Khi bạn tạo một biến, bạn **phải nói rõ** nó chứa kiểu dữ liệu gì — và **không được đổi kiểu** sau đó.

### So sánh với JavaScript:

```javascript
// JavaScript — weakly typed
let x = 10;      // x là number
x = "hello";     // x bây giờ là string — OK, JS cho phép!
x = true;        // x bây giờ là boolean — vẫn OK!
```

```csharp
// C# — strongly typed
int x = 10;      // x là int (số nguyên)
x = "hello";     // ❌ LỖI COMPILE! Không thể gán string vào biến int
x = true;        // ❌ LỖI COMPILE! Không thể gán bool vào biến int
```

**Vì sao C# làm vậy?**

- Giúp **phát hiện lỗi sớm** (lúc compile, chưa cần chạy)
- Giúp **code an toàn hơn** — bạn biết chắc biến `x` luôn là số
- Giúp **IDE hỗ trợ tốt hơn** — Visual Studio gợi ý chính xác method/property

---

## 2. Cú pháp khai báo biến trong C#

### Công thức:

```
<kiểu_dữ_liệu> <tên_biến> = <giá_trị>;
```

### Ví dụ:

```csharp
int age = 25;              // Số nguyên
double salary = 1500.50;   // Số thực (có dấu phẩy)
string name = "Minh";      // Chuỗi ký tự
bool isActive = true;      // Đúng/Sai
char grade = 'A';          // Một ký tự duy nhất
```

### Giải thích từng phần:

| Phần | Ý nghĩa |
|------|----------|
| `int` | Kiểu dữ liệu — nói cho C# biết biến này chứa gì |
| `age` | Tên biến — bạn tự đặt |
| `=` | Toán tử gán — đặt giá trị vào biến |
| `25` | Giá trị ban đầu |
| `;` | Kết thúc câu lệnh — **bắt buộc trong C#** (JS cũng có nhưng không bắt buộc) |

---

## 3. Các kiểu dữ liệu cơ bản (Primitive Types)

| Kiểu | Mô tả | Ví dụ | Tương đương JS |
|------|--------|-------|----------------|
| `int` | Số nguyên | `42`, `-10`, `0` | `number` (phần nguyên) |
| `double` | Số thực (64-bit) | `3.14`, `-0.5` | `number` (phần thập phân) |
| `float` | Số thực (32-bit) | `3.14f` | `number` |
| `decimal` | Số chính xác cao (tiền tệ) | `99.99m` | không có |
| `string` | Chuỗi ký tự | `"Hello"` | `string` |
| `char` | Một ký tự | `'A'` | không có (JS dùng string) |
| `bool` | Đúng/Sai | `true`, `false` | `boolean` |

### ⚠️ Chú ý quan trọng:

- `float` phải có hậu tố `f`: `float pi = 3.14f;`
- `decimal` phải có hậu tố `m`: `decimal price = 99.99m;`
- `char` dùng **nháy đơn** `'A'`, còn `string` dùng **nháy kép** `"A"`
- Trong JS, tất cả số đều là `number`. Trong C#, bạn phải **chọn đúng kiểu** cho từng mục đích

---

## 4. Khai báo biến KHÔNG gán giá trị ban đầu

```csharp
int count;          // Khai báo nhưng chưa gán
count = 10;         // Gán sau

Console.WriteLine(count);  // ✅ OK — 10
```

Nhưng nếu bạn **dùng biến trước khi gán**:

```csharp
int count;
Console.WriteLine(count);  // ❌ LỖI COMPILE!
// Error: Use of unassigned local variable 'count'
```

> 🔑 **C# bắt buộc biến local phải được gán giá trị trước khi sử dụng.**
> JavaScript thì cho phép (giá trị sẽ là `undefined`).

---

## 5. Từ khóa `var` — Type Inference

C# cũng có `var`, nhưng **khác hoàn toàn** với `var` trong JavaScript!

```csharp
var name = "Minh";    // Compiler tự suy ra: name là string
var age = 25;         // Compiler tự suy ra: age là int
var price = 99.99;    // Compiler tự suy ra: price là double
```

### Cách `var` hoạt động trong C#:

1. Compiler nhìn **giá trị bên phải** dấu `=`
2. Tự suy ra kiểu dữ liệu
3. **Gán kiểu cố định** cho biến — KHÔNG thể đổi kiểu sau đó!

```csharp
var x = 10;       // x là int
x = "hello";      // ❌ LỖI! x đã là int, không thể gán string
```

### So sánh `var` trong C# vs JavaScript:

| | C# `var` | JS `var` / `let` |
|---|----------|------------------|
| Kiểu | Cố định sau khi suy ra | Có thể đổi bất cứ lúc nào |
| Thời điểm xác định kiểu | Compile time | Runtime |
| Bắt buộc gán giá trị | ✅ Có | ❌ Không |
| Scope | Block scope | `var`: function scope, `let`: block scope |

### ⚠️ Khi nào dùng `var`?

- ✅ Khi kiểu dữ liệu **đã rõ ràng** từ giá trị: `var name = "Minh";`
- ❌ Khi kiểu dữ liệu **không rõ ràng**: `var result = GetData();` — người đọc không biết `result` là gì

> 💡 **Best Practice:** Dùng `var` khi kiểu dữ liệu hiển nhiên. Dùng explicit type khi cần rõ ràng.

---

## 6. Hằng số — `const`

Khi bạn muốn một giá trị **không bao giờ thay đổi**:

```csharp
const double PI = 3.14159;
const string APP_NAME = "MyApp";

PI = 3.15;  // ❌ LỖI COMPILE! Không thể thay đổi const
```

### So sánh với JavaScript:

```javascript
// JS
const PI = 3.14159;
PI = 3.15;  // ❌ TypeError — giống C#!
```

**Giống nhau:** Cả hai đều không cho phép gán lại giá trị.

**Khác nhau:**
- JS `const` object vẫn có thể thay đổi property bên trong
- C# `const` phải biết giá trị **lúc compile** (không phải runtime)

---

## 7. Quy tắc đặt tên biến

### ✅ Được phép:

```csharp
int age;              // chữ thường
int myAge;            // camelCase — KHUYẾN KHÍCH cho local variable
string firstName;     // camelCase
int _count;           // bắt đầu bằng underscore
```

### ❌ Không được phép:

```csharp
int 1stPlace;         // ❌ Không bắt đầu bằng số
int my age;           // ❌ Không có khoảng trắng
int class;            // ❌ Không dùng từ khóa C#
int my-name;          // ❌ Không dùng dấu gạch ngang
```

### 📏 Convention trong C#:

| Loại | Convention | Ví dụ |
|------|-----------|-------|
| Local variable | camelCase | `int studentAge;` |
| Constant | PascalCase hoặc UPPER_CASE | `const int MaxRetry = 3;` |
| Method | PascalCase | `void CalculateTotal()` |
| Class | PascalCase | `class StudentManager` |
| Private field | _camelCase | `private int _count;` |

---

## 8. Biến trong Memory — Hiểu bản chất

Khi bạn viết:

```csharp
int age = 25;
```

Chuyện gì xảy ra bên trong máy tính?

```
┌─────────────────────────────────┐
│           STACK MEMORY          │
├─────────────────────────────────┤
│  age  │  25  │ (4 bytes - int)  │
├───────┴──────┴──────────────────┤
│  ...                            │
└─────────────────────────────────┘
```

- `int` chiếm **4 bytes** trong bộ nhớ
- Giá trị `25` được lưu **trực tiếp** trên Stack
- Biến `age` là **tên** để truy cập vào vùng nhớ đó

### Value Type vs Reference Type (giới thiệu nhẹ):

| Value Type | Reference Type |
|-----------|---------------|
| `int`, `double`, `bool`, `char`, `float`, `decimal` | `string`, `object`, `array`, `class` |
| Lưu **giá trị trực tiếp** trên Stack | Lưu **địa chỉ** trên Stack, **giá trị** trên Heap |
| Copy = tạo bản sao độc lập | Copy = copy địa chỉ (cùng trỏ đến 1 vùng nhớ) |

> 💡 Đừng lo nếu chưa hiểu hết phần này. Chúng ta sẽ quay lại ở **Lesson 11 — Memory Basics**.

---

## 9. Sai lầm phổ biến

### ❌ Sai lầm 1: Quên dấu chấm phẩy

```csharp
int age = 25    // ❌ Thiếu ;
```

### ❌ Sai lầm 2: Gán sai kiểu

```csharp
int age = "hai mươi lăm";  // ❌ string không phải int
```

### ❌ Sai lầm 3: Dùng `var` mà không gán giá trị

```csharp
var x;  // ❌ Compiler không biết x là kiểu gì!
```

### ❌ Sai lầm 4: Nhầm `char` và `string`

```csharp
char grade = "A";   // ❌ Phải dùng nháy đơn: 'A'
string name = 'M';  // ❌ Phải dùng nháy kép: "M"
```

### ❌ Sai lầm 5: Quên hậu tố `f` hoặc `m`

```csharp
float pi = 3.14;    // ❌ 3.14 mặc định là double, phải viết 3.14f
decimal price = 99.99;  // ❌ Phải viết 99.99m
```

---

## 10. Best Practices

1. **Đặt tên có ý nghĩa** — `int age` thay vì `int a`
2. **Dùng camelCase** cho local variable — `int studentAge`
3. **Khai báo biến gần nơi sử dụng** — đừng khai báo hết ở đầu method
4. **Dùng `const`** cho giá trị không đổi — `const double PI = 3.14159;`
5. **Dùng kiểu dữ liệu phù hợp** — dùng `decimal` cho tiền, `int` cho đếm
6. **Dùng `var` khi kiểu rõ ràng** — `var name = "Minh";` ✅
7. **Luôn gán giá trị ban đầu** khi có thể — tránh lỗi "unassigned variable"
