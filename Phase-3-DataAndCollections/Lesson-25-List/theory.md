# Bài 25: List<T> — Danh Sách Động Trong C#

## 🎯 Mục tiêu bài học
- Hiểu hạn chế của array và tại sao cần List<T>
- Thành thạo CRUD operations trên List<T>
- Sử dụng các phương thức tìm kiếm, sắp xếp, lọc
- Hiểu cơ chế hoạt động bên trong của List<T>
- So sánh List<T> với Array và JS Array

---

## 1. Vấn Đề Của Array — Tại Sao Cần List<T>?

### 1.1 Array cố định kích thước

```csharp
// Khai báo array 3 phần tử
string[] fruits = new string[3];
fruits[0] = "Táo";
fruits[1] = "Cam";
fruits[2] = "Chuối";

// ❌ Muốn thêm phần tử thứ 4? KHÔNG THỂ!
// fruits[3] = "Xoài"; // IndexOutOfRangeException!
```

### 1.2 Những hạn chế của Array

```
╔══════════════════════════════════════════════════════╗
║            CÁC HẠN CHẾ CỦA ARRAY                   ║
╠══════════════════════════════════════════════════════╣
║ ❌ Kích thước cố định — không thêm/xóa phần tử     ║
║ ❌ Không có Add(), Remove() — phải tự xử lý         ║
║ ❌ Xóa phần tử → phải tạo array mới, copy lại      ║
║ ❌ Chèn giữa → phải dịch tất cả phần tử phía sau   ║
║ ❌ Không biết "đang dùng bao nhiêu" vs "kích thước" ║
╚══════════════════════════════════════════════════════╝
```

### 1.3 Cách "mở rộng" array thủ công (rất phiền!)

```csharp
// Muốn thêm 1 phần tử vào array? Phải làm THỦ CÔNG:
string[] old = { "Táo", "Cam", "Chuối" };
string[] newArr = new string[old.Length + 1];
Array.Copy(old, newArr, old.Length);
newArr[newArr.Length - 1] = "Xoài";
// 😫 Quá phức tạp cho việc đơn giản!
```

> 💡 **List<T> giải quyết TẤT CẢ các vấn đề trên!**

---

## 2. List<T> — Mảng Động, Tự Mở Rộng

### 2.1 List<T> là gì?

```
╔═══════════════════════════════════════════════════╗
║  List<T> = Dynamic Array (Mảng động)              ║
║                                                   ║
║  ✅ Tự động mở rộng khi thêm phần tử             ║
║  ✅ Tự động co lại khi xóa phần tử               ║
║  ✅ Có sẵn Add, Remove, Find, Sort...            ║
║  ✅ Truy cập bằng index như array                 ║
║  ✅ Đếm số phần tử hiện tại (Count)              ║
╚═══════════════════════════════════════════════════╝
```

### 2.2 Namespace cần using

```csharp
using System.Collections.Generic; // ⬅️ BẮT BUỘC cho List<T>
```

> 📝 Từ .NET 6+ với `ImplicitUsings`, namespace này đã được include tự động.

---

## 3. Generic Type `<T>` — Giới Thiệu Nhanh

```
T = Type Parameter (Tham số kiểu)

List<int>       → Danh sách chứa int
List<string>    → Danh sách chứa string
List<Student>   → Danh sách chứa Student
List<bool>      → Danh sách chứa bool

T giống như "chỗ trống" để bạn điền kiểu dữ liệu vào.
Bài 28 sẽ học chi tiết về Generics.
```

```
  List<T>  ←── T là "placeholder" cho kiểu dữ liệu
    │
    ├── List<int>      → [1, 2, 3, 4, 5]
    ├── List<string>   → ["Táo", "Cam", "Chuối"]
    └── List<Student>  → [student1, student2, ...]
```

---

## 4. Tạo List<T>

### 4.1 Các cách khởi tạo

```csharp
// Cách 1: List rỗng
List<int> numbers = new List<int>();

// Cách 2: Collection Initializer (giống array)
List<int> scores = new List<int> { 90, 85, 78, 95 };

// Cách 3: Chỉ định capacity ban đầu
List<string> names = new List<string>(100); // Dự trù 100 phần tử

// Cách 4: Tạo từ array có sẵn
int[] arr = { 1, 2, 3 };
List<int> fromArray = new List<int>(arr);

// Cách 5: Dùng var (C# tự suy kiểu)
var cities = new List<string> { "Hà Nội", "TP.HCM", "Đà Nẵng" };
```

### 4.2 So sánh với JS

```javascript
// JavaScript — Array linh hoạt sẵn
let numbers = [1, 2, 3];
numbers.push(4);     // OK
numbers.push("abc"); // OK — JS cho phép mixed types! 😱
```

```csharp
// C# — List<T> kiểm tra kiểu nghiêm ngặt
List<int> numbers = new List<int> { 1, 2, 3 };
numbers.Add(4);      // ✅ OK
// numbers.Add("abc"); // ❌ Compile error — chỉ nhận int!
```

---

## 5. CRUD Operations — Thêm, Đọc, Sửa, Xóa

### 5.1 ➕ THÊM phần tử (Create)

```csharp
List<string> fruits = new List<string>();

// Add — thêm 1 phần tử vào cuối
fruits.Add("Táo");           // ["Táo"]
fruits.Add("Cam");           // ["Táo", "Cam"]

// AddRange — thêm nhiều phần tử cùng lúc
fruits.AddRange(new[] { "Chuối", "Xoài" });
                              // ["Táo", "Cam", "Chuối", "Xoài"]

// Insert — chèn tại vị trí index
fruits.Insert(1, "Dưa hấu"); // ["Táo", "Dưa hấu", "Cam", "Chuối", "Xoài"]

// InsertRange — chèn nhiều phần tử tại vị trí
fruits.InsertRange(0, new[] { "Nho", "Lê" });
                              // ["Nho", "Lê", "Táo", "Dưa hấu", ...]
```

### 5.2 📖 ĐỌC phần tử (Read)

```csharp
List<string> fruits = new List<string> { "Táo", "Cam", "Chuối", "Xoài" };

// Truy cập bằng index (giống array)
string first = fruits[0];    // "Táo"
string last = fruits[^1];    // "Xoài" (index from end)

// Count — số phần tử hiện tại
int count = fruits.Count;    // 4

// Contains — kiểm tra có phần tử không
bool hasCam = fruits.Contains("Cam"); // true

// IndexOf — tìm vị trí (trả -1 nếu không có)
int idx = fruits.IndexOf("Chuối");    // 2

// Duyệt toàn bộ
foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}
```

### 5.3 ✏️ SỬA phần tử (Update)

```csharp
List<string> fruits = new List<string> { "Táo", "Cam", "Chuối" };

// Gán trực tiếp qua index
fruits[1] = "Bưởi";         // ["Táo", "Bưởi", "Chuối"]
```

### 5.4 🗑️ XÓA phần tử (Delete)

```csharp
List<string> fruits = new List<string>
    { "Táo", "Cam", "Chuối", "Xoài", "Cam" };

// Remove — xóa phần tử ĐẦU TIÊN khớp (trả true/false)
fruits.Remove("Cam");       // Xóa "Cam" đầu tiên → true

// RemoveAt — xóa tại vị trí index
fruits.RemoveAt(0);         // Xóa phần tử index 0

// RemoveAll — xóa TẤT CẢ phần tử thỏa điều kiện
fruits.RemoveAll(f => f.StartsWith("C"));  // Xóa tất cả bắt đầu bằng "C"

// Clear — xóa TOÀN BỘ
fruits.Clear();             // List rỗng, Count = 0
```

```
MINH HỌA REMOVE:

Trước:  ["Táo", "Cam", "Chuối", "Xoài", "Cam"]
                  ↑
Remove("Cam")  ───┘  (xóa cái ĐẦU TIÊN tìm thấy)

Sau:    ["Táo", "Chuối", "Xoài", "Cam"]
```

---

## 6. Các Phương Thức Tìm Kiếm Nâng Cao

### 6.1 Find, FindAll, FindIndex, FindLast

```csharp
List<int> numbers = new List<int> { 3, 7, 12, 5, 18, 9, 25 };

// Find — tìm phần tử ĐẦU TIÊN thỏa điều kiện
int firstEven = numbers.Find(n => n % 2 == 0);      // 12

// FindLast — tìm phần tử CUỐI CÙNG thỏa điều kiện
int lastEven = numbers.FindLast(n => n % 2 == 0);   // 18

// FindAll — tìm TẤT CẢ phần tử thỏa điều kiện → trả List<T>
List<int> bigNumbers = numbers.FindAll(n => n > 10); // [12, 18, 25]

// FindIndex — tìm INDEX đầu tiên thỏa điều kiện
int idx = numbers.FindIndex(n => n > 10);            // 2

// Exists — kiểm tra CÓ ÍT NHẤT 1 phần tử thỏa điều kiện
bool hasNegative = numbers.Exists(n => n < 0);       // false

// TrueForAll — kiểm tra TẤT CẢ phần tử thỏa điều kiện
bool allPositive = numbers.TrueForAll(n => n > 0);   // true
```

### 6.2 So sánh với JS

```
╔══════════════════════╦══════════════════════════════╗
║  JavaScript          ║  C# List<T>                  ║
╠══════════════════════╬══════════════════════════════╣
║  arr.find(fn)        ║  list.Find(fn)               ║
║  arr.filter(fn)      ║  list.FindAll(fn)            ║
║  arr.findIndex(fn)   ║  list.FindIndex(fn)          ║
║  arr.some(fn)        ║  list.Exists(fn)             ║
║  arr.every(fn)       ║  list.TrueForAll(fn)         ║
║  arr.includes(val)   ║  list.Contains(val)          ║
║  arr.indexOf(val)    ║  list.IndexOf(val)           ║
╚══════════════════════╩══════════════════════════════╝
```

---

## 7. Sắp Xếp và Đảo Ngược

### 7.1 Sort() — Sắp xếp

```csharp
List<int> numbers = new List<int> { 5, 2, 8, 1, 9 };

// Sort mặc định (tăng dần)
numbers.Sort();              // [1, 2, 5, 8, 9]

// Sort giảm dần — dùng Comparison delegate
numbers.Sort((a, b) => b.CompareTo(a));  // [9, 8, 5, 2, 1]

// Sort string theo độ dài
List<string> words = new List<string> { "chuối", "táo", "dưa hấu" };
words.Sort((a, b) => a.Length.CompareTo(b.Length));
// ["táo", "chuối", "dưa hấu"]
```

### 7.2 Reverse() — Đảo ngược

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
numbers.Reverse();           // [5, 4, 3, 2, 1]
```

### 7.3 Sort object theo thuộc tính

```csharp
List<Student> students = new List<Student> { ... };

// Sort theo điểm giảm dần
students.Sort((a, b) => b.Score.CompareTo(a.Score));

// Sort theo tên A-Z
students.Sort((a, b) => a.Name.CompareTo(b.Name));
```

---

## 8. Chuyển Đổi và Tiện Ích

### 8.1 Chuyển đổi qua lại

```csharp
// List → Array
List<int> list = new List<int> { 1, 2, 3 };
int[] array = list.ToArray();

// Array → List
int[] arr = { 4, 5, 6 };
List<int> fromArr = arr.ToList();  // cần using System.Linq

// List → List (copy)
List<int> copy = new List<int>(list);

// GetRange — lấy 1 phần của List
List<int> sub = list.GetRange(0, 2);  // [1, 2]
```

### 8.2 ForEach — duyệt với Action

```csharp
List<string> names = new List<string> { "An", "Bình", "Chi" };

// ForEach — thực hiện action cho mỗi phần tử
names.ForEach(name => Console.WriteLine($"Xin chào {name}!"));

// Tương đương:
foreach (string name in names)
{
    Console.WriteLine($"Xin chào {name}!");
}
```

### 8.3 ConvertAll — chuyển đổi kiểu

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4 };

// Chuyển int → string
List<string> strings = numbers.ConvertAll(n => n.ToString());
// ["1", "2", "3", "4"]

// Chuyển int → double (nhân đôi)
List<double> doubles = numbers.ConvertAll(n => n * 2.0);
// [2.0, 4.0, 6.0, 8.0]
```

---

## 9. List<T> vs Array — Bảng So Sánh Chi Tiết

```
╔════════════════════╦══════════════════╦═══════════════════════╗
║  Tiêu chí          ║  Array (T[])     ║  List<T>              ║
╠════════════════════╬══════════════════╬═══════════════════════╣
║  Kích thước        ║  Cố định         ║  Động (tự mở rộng)   ║
║  Thêm phần tử      ║  ❌ Không có     ║  ✅ Add, Insert       ║
║  Xóa phần tử       ║  ❌ Không có     ║  ✅ Remove, RemoveAt  ║
║  Tìm kiếm          ║  Array.Find()    ║  list.Find()          ║
║  Sắp xếp           ║  Array.Sort()    ║  list.Sort()          ║
║  Count / Length     ║  .Length         ║  .Count               ║
║  Performance       ║  Nhanh hơn       ║  Chậm hơn chút       ║
║  Bộ nhớ            ║  Tiết kiệm hơn  ║  Dư capacity          ║
║  Tiện lợi          ║  ⭐⭐           ║  ⭐⭐⭐⭐⭐           ║
║  Dùng khi          ║  Size cố định    ║  Size thay đổi        ║
╚════════════════════╩══════════════════╩═══════════════════════╝
```

> 🏆 **Kết luận**: Trong 90% trường hợp thực tế, hãy dùng `List<T>`.
> Chỉ dùng array khi biết chắc kích thước cố định và cần hiệu suất tối đa.

---

## 10. Memory — List<T> Hoạt Động Bên Trong

### 10.1 Capacity vs Count

```
List<T> bên trong dùng một ARRAY ẩn.
Khi array đầy → tạo array MỚI gấp đôi → copy dữ liệu sang.

Capacity = kích thước array bên trong (dung lượng tối đa)
Count    = số phần tử THỰC TẾ đang chứa
```

```
MINH HỌA RESIZE:

Ban đầu: Capacity=4, Count=0
┌───┬───┬───┬───┐
│   │   │   │   │     Count = 0
└───┴───┴───┴───┘

Add(1), Add(2), Add(3), Add(4):
┌───┬───┬───┬───┐
│ 1 │ 2 │ 3 │ 4 │     Count = 4, Capacity = 4 (ĐẦY!)
└───┴───┴───┴───┘

Add(5) → RESIZE! Tạo array mới Capacity = 8:
┌───┬───┬───┬───┬───┬───┬───┬───┐
│ 1 │ 2 │ 3 │ 4 │ 5 │   │   │   │  Count = 5, Capacity = 8
└───┴───┴───┴───┴───┴───┴───┴───┘
                          ↑ ↑ ↑
                        Dư 3 slot
```

### 10.2 Xem Capacity và Count

```csharp
List<int> list = new List<int>();
Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
// Count: 0, Capacity: 0

list.Add(1);
Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
// Count: 1, Capacity: 4  (mặc định khởi tạo 4)

for (int i = 2; i <= 5; i++) list.Add(i);
Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
// Count: 5, Capacity: 8  (đã resize gấp đôi)
```

### 10.3 Tối ưu Capacity

```csharp
// Nếu biết trước số phần tử → chỉ định capacity
List<int> list = new List<int>(1000); // Tránh resize nhiều lần

// Thu gọn capacity về = Count
list.TrimExcess();
```

---

## 11. So Sánh JS Array vs C# List<T>

```
╔══════════════════════════════╦══════════════════════════════╗
║  JavaScript Array            ║  C# List<T>                  ║
╠══════════════════════════════╬══════════════════════════════╣
║  let arr = [1, 2, 3]        ║  var list = new List<int>    ║
║                              ║      { 1, 2, 3 };           ║
║  arr.push(4)                 ║  list.Add(4)                 ║
║  arr.pop()                   ║  list.RemoveAt(Count - 1)    ║
║  arr.unshift(0)              ║  list.Insert(0, val)         ║
║  arr.splice(i, 1)            ║  list.RemoveAt(i)            ║
║  arr.length                  ║  list.Count                  ║
║  arr.sort()                  ║  list.Sort()                 ║
║  arr.reverse()               ║  list.Reverse()              ║
║  arr.filter(fn)              ║  list.FindAll(fn)            ║
║  arr.map(fn)                 ║  list.ConvertAll(fn)         ║
║  arr.forEach(fn)             ║  list.ForEach(fn)            ║
║  arr.concat(arr2)            ║  list.AddRange(list2)        ║
║  arr.slice(a, b)             ║  list.GetRange(a, count)     ║
║  [...arr]                    ║  new List<T>(list)           ║
╠══════════════════════════════╬══════════════════════════════╣
║  Mixed types: [1, "hi", {}] ║  ❌ Chỉ 1 kiểu duy nhất     ║
║  Không type-safe             ║  ✅ Type-safe hoàn toàn      ║
╚══════════════════════════════╩══════════════════════════════╝
```

---

## 12. Best Practices

### ✅ Nên làm

```csharp
// 1. Dùng var khi kiểu rõ ràng
var students = new List<Student>();

// 2. Chỉ định capacity nếu biết trước số lượng
var bigList = new List<int>(10000);

// 3. Dùng collection initializer khi có dữ liệu ban đầu
var colors = new List<string> { "Đỏ", "Xanh", "Vàng" };

// 4. Kiểm tra Count trước khi truy cập index
if (list.Count > 0)
{
    var first = list[0];
}

// 5. Dùng RemoveAll thay vì loop + Remove
list.RemoveAll(x => x < 0); // Xóa tất cả số âm
```

### ❌ Không nên

```csharp
// 1. ❌ Không xóa phần tử trong foreach
foreach (var item in list)
{
    list.Remove(item); // ❌ InvalidOperationException!
}

// 2. ❌ Không truy cập index ngoài phạm vi
var item = list[list.Count]; // ❌ ArgumentOutOfRangeException!

// 3. ❌ Không dùng List khi kích thước cố định và hiệu suất quan trọng
// → Dùng array thay thế

// 4. ❌ Đừng quên kiểm tra null
List<int> list = null;
// list.Add(1); // ❌ NullReferenceException!
```

### ⚠️ Xóa phần tử an toàn trong vòng lặp

```csharp
// Cách đúng 1: Duyệt ngược
for (int i = list.Count - 1; i >= 0; i--)
{
    if (list[i] < 0) list.RemoveAt(i);
}

// Cách đúng 2: Dùng RemoveAll (đơn giản nhất!)
list.RemoveAll(x => x < 0);
```

---

## 📌 Tổng Kết

```
List<T> = Array + Superpowers 🦸

✅ Kích thước ĐỘNG — tự mở rộng/co lại
✅ CRUD đầy đủ — Add, Remove, Insert, Clear
✅ Tìm kiếm mạnh — Find, FindAll, Exists, Contains
✅ Sắp xếp dễ — Sort(), Sort(comparison)
✅ Type-safe — chỉ chứa đúng kiểu T
✅ Chuyển đổi — ToArray(), ConvertAll(), GetRange()

Bên trong: Array ẩn + tự resize gấp đôi khi đầy
Namespace: System.Collections.Generic
```

> ⏭️ **Bài tiếp theo**: Dictionary<TKey, TValue> — Bộ sưu tập Key-Value
