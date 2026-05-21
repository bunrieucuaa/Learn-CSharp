# ✏️ Lesson 08 — Bài tập: Methods

---

## Bài 1: Thư viện Math helper 🔢

### Yêu cầu:
Tạo các method tiện ích toán học:

```csharp
static int Max(int a, int b);
static int Min(int a, int b);
static int Clamp(int value, int min, int max);  // Giới hạn trong khoảng
static bool IsPrime(int n);
static int GCD(int a, int b);           // Ước chung lớn nhất
static int LCM(int a, int b);           // Bội chung nhỏ nhất
static long Factorial(int n);
static double Power(double base_, int exp);
```

Test tất cả method trong Main() với nhiều input khác nhau.

### Gợi ý:
- `Clamp`: nếu value < min → min, nếu value > max → max, còn lại → value
- `GCD`: dùng thuật toán Euclid (đệ quy): `GCD(a, b) = GCD(b, a % b)`, base: `GCD(a, 0) = a`
- `LCM(a, b) = (a * b) / GCD(a, b)`

---

## Bài 2: Tạo thư viện Input reusable 📥

### Yêu cầu:
Tạo các method đọc input dùng lại được:

```csharp
static string ReadString(string prompt);
static string ReadString(string prompt, int minLen, int maxLen);
static int ReadInt(string prompt);
static int ReadInt(string prompt, int min, int max);
static double ReadDouble(string prompt, double min, double max);
static bool ReadYesNo(string prompt);
static string ReadChoice(string prompt, string[] validOptions);
```

Dùng **overloading** cho `ReadString` và `ReadInt`.

Test bằng chương trình nhập thông tin nhân viên:
- Tên (3-50 ký tự)
- Tuổi (18-65)
- Lương (1,000,000 - 100,000,000)
- Có hợp đồng? (y/n)
- Phòng ban (HR/IT/Sales/Marketing)

---

## Bài 3: String Utilities 🔤

### Yêu cầu:
Tạo các method xử lý chuỗi (KHÔNG dùng method có sẵn — tự viết logic):

```csharp
static int CountVowels(string text);       // Đếm nguyên âm
static int CountWords(string text);        // Đếm số từ
static string ReverseString(string text);  // Đảo chuỗi
static bool IsPalindrome(string text);     // Kiểm tra palindrome
static string ToCamelCase(string text);    // "hello world" → "helloWorld"
static string ToPascalCase(string text);   // "hello world" → "HelloWorld"
static string CensorWord(string text, string word); // Che từ bằng ***
```

### Expected Output:
```
CountVowels("Hello World") = 3
CountWords("  Hello   World  ") = 2
ReverseString("Hello") = "olleH"
IsPalindrome("madam") = True
ToCamelCase("hello world foo") = "helloWorldFoo"
CensorWord("I love you", "love") = "I **** you"
```

---

## Bài 4: Tái cấu trúc — Refactor bài cũ 🔧

### Yêu cầu:
Chọn **1 challenge từ Lesson 03-06** (taxi, ATM, đặt vé, đặt đồ ăn) và **refactor** thành methods:

**Quy tắc:**
- Main() tối đa 20 dòng
- Mỗi method tối đa 15-20 dòng
- Phải có tối thiểu 8 methods
- Mỗi method chỉ làm 1 việc
- Dùng ít nhất 1 method có `return`
- Dùng ít nhất 1 method có default params
- Dùng ít nhất 1 overloaded method

### Cấu trúc gợi ý:
```
Main()
 ├── ShowMenu()
 ├── ReadXxxInput()     ← Nhóm Input
 ├── CalculateXxx()     ← Nhóm Logic
 ├── ValidateXxx()      ← Nhóm Validation
 ├── PrintResult()      ← Nhóm Output
 └── PrintError()
```

---

## Bài 5: Recursive challenges 🔄

### Yêu cầu:
Viết các method đệ quy:

```csharp
// A. Tổng các chữ số
static int SumDigits(int n);
// SumDigits(1234) = 1+2+3+4 = 10

// B. Chuyển sang nhị phân
static string ToBinary(int n);
// ToBinary(10) = "1010"

// C. Tính tổ hợp C(n, k)
static long Combination(int n, int k);
// C(5,2) = 10

// D. Tower of Hanoi (in các bước)
static void Hanoi(int n, char from, char to, char aux);
// Hanoi(3, 'A', 'C', 'B')

// E. Kiểm tra xâu con
static bool IsSubstring(string text, string sub, int index = 0);
```

---

## Bài 6: Mini Calculator App (tổng hợp) 🧮

### Yêu cầu:
Viết ứng dụng máy tính khoa học mini sử dụng methods:

**Chức năng:**
1. Phép tính cơ bản (+, -, ×, ÷)
2. Phép tính nâng cao (lũy thừa, căn bậc 2, giai thừa)
3. Chuyển đổi đơn vị (km↔mile, kg↔pound, °C↔°F)
4. Lịch sử 10 phép tính gần nhất

**Yêu cầu kỹ thuật:**
- Mỗi phép tính = 1 method riêng
- Method `PrintHistory()` hiển thị lịch sử
- Method `AddToHistory(string entry)` lưu vào mảng
- Dùng `params` cho method tính trung bình
- Dùng overloading cho method Convert (nhiều loại đơn vị)

### Cấu trúc:
```csharp
// Input
static double ReadNumber(string prompt);
static char ReadOperator();

// Basic operations
static double Add(double a, double b);
static double Subtract(double a, double b);
// ...

// Advanced
static long Factorial(int n);
static double Sqrt(double n);
static double Power(double b, int e);

// Conversion (overloading)
static double Convert(double km, string unit);  // km → mile
static double Convert(double value, string from, string to);

// History
static void AddToHistory(string entry);
static void PrintHistory();

// UI
static void ShowMenu();
```
