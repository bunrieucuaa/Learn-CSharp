# Bài 32: LINQ Basics (Cơ bản về LINQ)

Chào mừng bạn đến với **Phase 4: LINQ & Clean Code**. Trong bài này, chúng ta sẽ làm quen với một trong những công nghệ mạnh mẽ và thú vị nhất của C# — **LINQ (Language Integrated Query)**. 

Nếu bạn đã từng mệt mỏi với việc viết những vòng lặp `foreach` lồng nhau chỉ để lọc, tìm kiếm hoặc sắp xếp dữ liệu, thì LINQ chính là giải pháp cứu cánh giúp code của bạn ngắn hơn, dễ đọc hơn và chuyên nghiệp hơn rất nhiều.

---

## 1. LINQ là gì? Tại sao nó ra đời?

### Khái niệm
**LINQ (Language Integrated Query)** (đọc là /lɪŋk/) là một tập hợp các tính năng được tích hợp trực tiếp vào ngôn ngữ C# kể từ phiên bản C# 3.0. Nó cung cấp một cú pháp truy vấn nhất quán để truy cập và xử lý dữ liệu từ nhiều nguồn khác nhau như:
* **Objects trong bộ nhớ** (Arrays, Lists, Dictionaries... - gọi là LINQ to Objects).
* **Cơ sở dữ liệu** (SQL Server, MySQL thông qua Entity Framework - gọi là LINQ to Entities).
* **XML** (LINQ to XML).

### Vấn đề khi không dùng LINQ
Hãy tưởng tượng bạn có một danh sách các học viên (`List<Student>`) và bạn muốn:
1. Lọc ra những học viên có tuổi >= 18.
2. Sắp xếp họ theo tên từ A-Z.
3. Chỉ lấy ra tên của họ để hiển thị.

Nếu không dùng LINQ (cách viết cổ điển dùng vòng lặp):
```csharp
// Mảng dữ liệu ban đầu
List<Student> students = GetStudents();

// Bước 1: Lọc
List<Student> filtered = new List<Student>();
foreach (var s in students)
{
    if (s.Age >= 18)
    {
        filtered.Add(s);
    }
}

// Bước 2: Sắp xếp (sử dụng thuật toán thủ công hoặc viết IComparable)
filtered.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

// Bước 3: Lấy ra tên (Projection)
List<string> names = new List<string>();
foreach (var s in filtered)
{
    names.Add(s.Name);
}
```
*Nhược điểm:* Code rất dài dòng, khai báo nhiều biến phụ (`filtered`, `names`), khó đọc và dễ xảy ra lỗi logic khi thuật toán phức tạp lên.

### Giải pháp với LINQ
Với LINQ, cả 3 bước trên chỉ tốn **đúng 1 dòng code**:
```csharp
var names = students
            .Where(s => s.Age >= 18)
            .OrderBy(s => s.Name)
            .Select(s => s.Name)
            .ToList();
```

---

## 2. So sánh với JavaScript (JS)

Vì bạn có nền tảng JavaScript, bạn sẽ thấy LINQ cực kỳ quen thuộc. Các phương thức của LINQ hoạt động rất giống với các hàm xử lý mảng (Array methods) của ES6:

| Tính năng / Hàm | Trong JavaScript | Trong C# (LINQ Method Syntax) |
|---|---|---|
| Lọc phần tử | `array.filter(x => ...)` | `collection.Where(x => ...)` |
| Biến đổi kiểu | `array.map(x => ...)` | `collection.Select(x => ...)` |
| Sắp xếp | `array.sort((a, b) => ...)` | `collection.OrderBy(x => ...)` |
| Lấy một phần tử | `array.find(x => ...)` | `collection.FirstOrDefault(x => ...)` |
| Kiểm tra tất cả | `array.every(x => ...)` | `collection.All(x => ...)` |
| Kiểm tra có ít nhất 1 | `array.some(x => ...)` | `collection.Any(x => ...)` |

> [!NOTE]
> **Điểm khác biệt cốt lõi:**
> Hàm của JS thực thi **ngay lập tức** (Eager Execution) và trả về một mảng mới.
> Còn LINQ trong C# mặc định hoạt động theo cơ chế **Trì hoãn thực thi** (Deferred Execution). Chúng ta sẽ tìm hiểu chi tiết ở mục 5.

---

## 3. Lambda Expressions (Biểu thức Lambda)

Để dùng được LINQ, bạn phải hiểu **Lambda Expression**. 
Thực chất, Lambda Expression trong C# chính là **Arrow Function** trong JavaScript.

* Trong JS: `(x) => x.age > 18`
* Trong C#: `(x) => x.Age > 18` hoặc rút gọn `x => x.Age > 18`

Cú pháp:
```
(danh_sách_tham_số) => biểu_thức_hoặc_thân_hàm
```

* **Trái dấu `=>`:** Danh sách tham số đầu vào. Nếu chỉ có 1 tham số, không cần ngoặc đơn.
* **Phải dấu `=>`:** Biểu thức logic hoặc hành động trả về kết quả.

Ví dụ:
```csharp
// Không có tham số
() => Console.WriteLine("Hello");

// 1 tham số
x => x * x;

// Nhiều tham số
(x, y) => x + y;

// Thân hàm có nhiều dòng lệnh (cần từ khóa return)
(x, y) => {
    int sum = x + y;
    return sum * 2;
};
```

---

## 4. Hai cú pháp viết LINQ

Trong C#, bạn có hai cách để viết truy vấn LINQ:

### Cách 1: Query Syntax (Cú pháp truy vấn)
Trông giống như ngôn ngữ truy vấn SQL.
```csharp
var query = from s in students
            where s.Age >= 18
            orderby s.Name
            select s.Name;
```
* **Đặc điểm:** Dễ đọc với những người quen viết SQL, rất trực quan khi thực hiện các phép kết hợp bảng phức tạp (Join, Group).

### Cách 2: Method Syntax (Cú pháp phương thức - Khuyên dùng)
Sử dụng các phương thức mở rộng (Extension Methods) nối tiếp nhau (Method Chaining).
```csharp
var query = students
            .Where(s => s.Age >= 18)
            .OrderBy(s => s.Name)
            .Select(s => s.Name);
```
* **Đặc điểm:** Rất quen thuộc với lập trình viên JS/TS. Dễ viết validation, debug và hỗ trợ đầy đủ 100% các toán tử của LINQ (trong khi Query Syntax bị giới hạn một số hàm).

---

## 5. Bản chất LINQ: Deferred Execution (Trì hoãn thực thi)

Đây là phần **quan trọng nhất** cần hiểu về bản chất của LINQ.

> [!IMPORTANT]
> **Deferred Execution** nghĩa là: Khi bạn viết một câu lệnh LINQ, câu lệnh đó **chưa hề chạy** và chưa hề lọc dữ liệu. Nó chỉ lưu lại "cách thức" truy vấn (như một bản kế hoạch). Truy vấn chỉ thực sự chạy khi bạn bắt đầu duyệt qua kết quả (dùng `foreach`) hoặc ép nó trả về kết quả ngay lập tức (dùng `.ToList()`, `.ToArray()`, `.Count()`).

### Minh họa cơ chế:
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Bước 1: Khai báo LINQ query. 
// Chưa có phép tính toán nào diễn ra ở đây!
var query = numbers.Where(n => {
    Console.WriteLine($"Đang lọc số: {n}");
    return n % 2 == 0;
});

Console.WriteLine("Đã khai báo xong câu query.");

// Bước 2: Thực sự chạy query bằng cách lặp
foreach (var num in query)
{
    Console.WriteLine($"Kết quả lọc được: {num}");
}
```

**Output thực tế của đoạn code trên:**
```text
Đã khai báo xong câu query.
Đang lọc số: 1
Đang lọc số: 2
Kết quả lọc được: 2
Đang lọc số: 3
Đang lọc số: 4
Kết quả lọc được: 4
Đang lọc số: 5
```
Bạn thấy đấy, dòng chữ `"Đã khai báo xong câu query"` in ra trước cả khi quá trình lọc diễn ra! Đây chính là Deferred Execution.

### Eager Execution (Thực thi tức thì)
Nếu bạn muốn query chạy ngay lập tức để lấy kết quả và lưu vào bộ nhớ, hãy gọi các hàm chuyển đổi như `.ToList()`, `.ToArray()`, `.ToDictionary()` hoặc các hàm tổng hợp như `.Count()`, `.Sum()`.

```csharp
// Query này chạy NGAY LẬP TỨC và trả về một List<int> thực tế trong Heap
var activeList = numbers.Where(n => n % 2 == 0).ToList();
```

---

## 6. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Gọi `.ToList()` quá sớm
```csharp
// Tồi: Gọi ToList() sớm làm tải toàn bộ dữ liệu vào RAM rồi mới lọc
var result = database.Students.ToList().Where(s => s.Age > 18);

// Tốt: Lọc trước trên Database, chỉ lấy những dòng đạt yêu cầu về RAM
var result = database.Students.Where(s => s.Age > 18).ToList();
```
*Giải thích:* Khi làm việc với Database (Entity Framework), nếu gọi `.ToList()`, C# sẽ tải toàn bộ bảng từ DB vào RAM. Lọc sau `.ToList()` là lọc trên RAM. Còn lọc trước `.ToList()`, EF sẽ dịch câu lệnh thành SQL lọc trực tiếp dưới Database trước khi tải về.

### Sai lầm 2: Chạy lặp đi lặp lại một câu Query Deferred
```csharp
var query = students.Where(s => s.IsActive);

// Mỗi lần bạn gọi foreach hoặc Count(), LINQ lại chạy lại bộ lọc từ đầu!
int count = query.Count(); // Chạy lọc lần 1
foreach(var item in query) // Chạy lọc lần 2
{
    // ...
}
```
*Khắc phục:* Nếu muốn tái sử dụng kết quả lọc nhiều lần, hãy lưu nó lại bằng `.ToList()`.

---

## 7. Checklist đánh giá hiểu bài

1. LINQ là viết tắt của từ gì? Nó dùng để làm gì?
2. Sự khác nhau lớn nhất giữa LINQ và các hàm xử lý mảng (map, filter) trong JS là gì?
3. Trì hoãn thực thi (Deferred Execution) là gì? Tại sao nó lại giúp tối ưu hóa hiệu năng?
4. Khi nào một câu lệnh LINQ thực sự chạy? Làm sao để bắt nó chạy ngay lập tức?
