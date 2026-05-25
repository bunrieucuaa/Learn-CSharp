# Bài 26: Dictionary<TKey, TValue> — Bộ Sưu Tập Key-Value

## 🎯 Mục tiêu bài học
- Hiểu Dictionary là gì và khi nào sử dụng
- Thành thạo CRUD operations trên Dictionary
- Sử dụng TryGetValue pattern an toàn
- Biết cách duyệt, lọc, nhóm dữ liệu với Dictionary
- So sánh Dictionary với List và JS Object/Map

---

## 1. Dictionary Là Gì?

### 1.1 Khái niệm

```
Dictionary = Bộ sưu tập các CẶP Key-Value (Khóa - Giá trị)

╔══════════════════════════════════════════════════════╗
║  Dictionary giống như một CUỐN TỪ ĐIỂN:              ║
║                                                      ║
║  Từ (Key)     →  Nghĩa (Value)                      ║
║  "apple"      →  "quả táo"                          ║
║  "banana"     →  "quả chuối"                        ║
║  "orange"     →  "quả cam"                          ║
║                                                      ║
║  ✅ Tra cứu CỰC NHANH theo key — O(1)              ║
║  ✅ Key phải UNIQUE (không trùng)                    ║
║  ✅ Value có thể trùng                               ║
╚══════════════════════════════════════════════════════╝
```

### 1.2 So sánh với đời thực

```
📖 Từ điển:     từ → nghĩa
📞 Danh bạ:     tên → số điện thoại
🏫 Điểm danh:   MSSV → tên sinh viên
🏷️ Giá sản phẩm: mã SP → giá tiền
⚙️ Config:      key → value
```

### 1.3 So sánh nhanh với JS

```javascript
// JavaScript — Object (giống Dictionary)
let phonebook = {
    "An": "0901234567",
    "Bình": "0912345678"
};
console.log(phonebook["An"]); // "0901234567"

// JavaScript — Map (giống Dictionary hơn)
let map = new Map();
map.set("An", "0901234567");
console.log(map.get("An"));  // "0901234567"
```

```csharp
// C# — Dictionary<string, string>
Dictionary<string, string> phonebook = new Dictionary<string, string>
{
    { "An", "0901234567" },
    { "Bình", "0912345678" }
};
Console.WriteLine(phonebook["An"]); // "0901234567"
```

---

## 2. Tạo Dictionary

### 2.1 Các cách khởi tạo

```csharp
using System.Collections.Generic;

// Cách 1: Dictionary rỗng
Dictionary<string, int> ages = new Dictionary<string, int>();

// Cách 2: Collection initializer — dùng { }
Dictionary<string, int> scores = new Dictionary<string, int>
{
    { "An", 85 },
    { "Bình", 92 },
    { "Chi", 78 }
};

// Cách 3: Index initializer (C# 6+) — dùng [key] = value
Dictionary<string, string> config = new Dictionary<string, string>
{
    ["host"] = "localhost",
    ["port"] = "3000",
    ["env"] = "development"
};

// Cách 4: Dùng var
var fruits = new Dictionary<string, double>
{
    { "Táo", 25000 },
    { "Cam", 30000 }
};
```

### 2.2 Cấu trúc `<TKey, TValue>`

```
Dictionary<TKey, TValue>

TKey   = Kiểu dữ liệu của KEY (thường là string, int)
TValue = Kiểu dữ liệu của VALUE (bất kỳ kiểu nào)

Dictionary<string, int>      → Key: string, Value: int
Dictionary<int, string>      → Key: int,    Value: string
Dictionary<string, Student>  → Key: string, Value: Student
Dictionary<int, List<string>>→ Key: int,    Value: List<string>
```

```
   Dictionary<string, int>
   ╔═══════════╦═══════╗
   ║  Key      ║ Value ║
   ╠═══════════╬═══════╣
   ║  "An"     ║  85   ║
   ║  "Bình"   ║  92   ║
   ║  "Chi"    ║  78   ║
   ╚═══════════╩═══════╝
       TKey      TValue
```

---

## 3. CRUD Operations

### 3.1 ➕ THÊM phần tử (Create)

```csharp
var dict = new Dictionary<string, int>();

// Cách 1: Add() — ném exception nếu key đã tồn tại
dict.Add("An", 85);
dict.Add("Bình", 92);

// Cách 2: [key] = value — ghi đè nếu key đã tồn tại (AN TOÀN hơn)
dict["Chi"] = 78;
dict["Dũng"] = 88;

// ⚠️ Add với key trùng → ArgumentException!
// dict.Add("An", 90); // ❌ Exception: Key "An" đã tồn tại!

// [key] = value với key trùng → GHI ĐÈ (không lỗi)
dict["An"] = 90;  // ✅ Cập nhật An: 85 → 90
```

```
SO SÁNH 2 CÁCH THÊM:

╔══════════════════╦══════════════╦═══════════════════════╗
║                  ║  Add(k, v)   ║  dict[k] = v          ║
╠══════════════════╬══════════════╬═══════════════════════╣
║ Key chưa có      ║ ✅ Thêm mới  ║ ✅ Thêm mới           ║
║ Key đã có        ║ ❌ Exception ║ ✅ Ghi đè (update)     ║
║ Dùng khi         ║ Muốn biết    ║ Không quan tâm key    ║
║                  ║ key trùng    ║ đã có hay chưa        ║
╚══════════════════╩══════════════╩═══════════════════════╝
```

### 3.2 📖 ĐỌC phần tử (Read)

```csharp
var dict = new Dictionary<string, int>
{
    { "An", 85 }, { "Bình", 92 }, { "Chi", 78 }
};

// Đọc bằng key
int anScore = dict["An"];        // 85

// ⚠️ Key không tồn tại → KeyNotFoundException!
// int x = dict["Xyz"];          // ❌ Exception!

// Kiểm tra trước khi đọc
if (dict.ContainsKey("An"))
{
    Console.WriteLine(dict["An"]);
}

// Count — số cặp key-value
int count = dict.Count;          // 3

// Keys — tất cả các key
foreach (string key in dict.Keys)
    Console.WriteLine(key);      // "An", "Bình", "Chi"

// Values — tất cả các value
foreach (int value in dict.Values)
    Console.WriteLine(value);    // 85, 92, 78
```

### 3.3 ✏️ SỬA phần tử (Update)

```csharp
// Cập nhật value bằng key
dict["An"] = 90;     // An: 85 → 90
dict["Bình"] = 95;   // Bình: 92 → 95

// ⚠️ Không thể đổi key trực tiếp → phải xóa cũ, thêm mới
```

### 3.4 🗑️ XÓA phần tử (Delete)

```csharp
// Remove theo key — trả true/false
bool removed = dict.Remove("Chi");  // true, xóa "Chi"
bool failed = dict.Remove("Xyz");   // false, key không tồn tại

// Clear — xóa toàn bộ
dict.Clear();                       // Count = 0
```

---

## 4. Duyệt Dictionary

### 4.1 foreach với KeyValuePair

```csharp
var scores = new Dictionary<string, int>
{
    { "An", 85 }, { "Bình", 92 }, { "Chi", 78 }
};

// Cách 1: KeyValuePair (đầy đủ)
foreach (KeyValuePair<string, int> pair in scores)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Cách 2: var (ngắn gọn)
foreach (var pair in scores)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Cách 3: Destructuring (C# 7+)
foreach (var (name, score) in scores)
{
    Console.WriteLine($"{name}: {score}");
}

// Cách 4: Chỉ duyệt Keys
foreach (string name in scores.Keys)
{
    Console.WriteLine($"{name}: {scores[name]}");
}
```

### 4.2 So sánh với JS

```javascript
// JavaScript
for (let [key, value] of map) { ... }       // Map
for (let key in obj) { ... }                 // Object
Object.entries(obj).forEach(([k, v]) => {})  // Object
```

```csharp
// C#
foreach (var (key, value) in dict) { ... }   // Dictionary
foreach (var key in dict.Keys) { ... }       // Chỉ keys
```

---

## 5. TryGetValue Pattern — Đọc An Toàn

### 5.1 Vấn đề

```csharp
// ❌ NGUY HIỂM — KeyNotFoundException nếu key không tồn tại
int score = dict["Xyz"]; // 💥 Exception!

// ❌ HAI LẦN TÌM — ContainsKey rồi lại truy cập (tìm 2 lần)
if (dict.ContainsKey("An"))
{
    int s = dict["An"]; // Tìm lại lần 2!
}
```

### 5.2 Giải pháp: TryGetValue

```csharp
// ✅ TryGetValue — TÌM 1 LẦN DUY NHẤT, trả true/false
if (dict.TryGetValue("An", out int score))
{
    Console.WriteLine($"Điểm của An: {score}");
}
else
{
    Console.WriteLine("Không tìm thấy An");
}
```

```
MINH HỌA TryGetValue:

dict.TryGetValue("An", out int score)
          │                    │
          ▼                    ▼
     ┌─────────┐        ┌──────────┐
     │ Tìm key │  true  │ score=85 │
     │  "An"   │───────▶│ (out)    │
     └─────────┘        └──────────┘
          │
     ┌─────────┐
     │ Tìm key │  false → score = 0 (default)
     │  "Xyz"  │
     └─────────┘
```

### 5.3 Pattern phổ biến

```csharp
// Pattern 1: Đếm tần suất (word counter)
var wordCount = new Dictionary<string, int>();
string word = "hello";

if (wordCount.TryGetValue(word, out int count))
    wordCount[word] = count + 1;  // Đã có → tăng
else
    wordCount[word] = 1;          // Chưa có → bắt đầu từ 1

// Pattern 2: GetOrDefault
string value = dict.TryGetValue(key, out var v) ? v : "default";

// Pattern 3: Cache / Memoization
if (!cache.TryGetValue(input, out var result))
{
    result = ExpensiveComputation(input);
    cache[input] = result;
}
```

---

## 6. Dictionary vs List — Khi Nào Dùng Cái Nào?

```
╔══════════════════════╦═══════════════════════╦════════════════════════╗
║  Tiêu chí            ║  List<T>              ║  Dictionary<K,V>       ║
╠══════════════════════╬═══════════════════════╬════════════════════════╣
║  Cấu trúc            ║  Danh sách (ordered)  ║  Cặp key-value         ║
║  Truy cập            ║  Theo index (0,1,2..) ║  Theo key              ║
║  Tìm kiếm            ║  O(n) — chậm         ║  O(1) — CỰC NHANH     ║
║  Thứ tự              ║  Có thứ tự            ║  Không đảm bảo         ║
║  Trùng lặp           ║  Cho phép             ║  Key UNIQUE            ║
║  Dùng khi            ║  Danh sách, duyệt     ║  Tra cứu, ánh xạ      ║
║                      ║  tuần tự, sort        ║  đếm, nhóm            ║
╠══════════════════════╬═══════════════════════╬════════════════════════╣
║  VD: Sinh viên       ║  List<Student>        ║  Dict<string, Student> ║
║  VD: Điểm            ║  List<int>            ║  Dict<string, int>     ║
║  VD: Config          ║  ❌ Không phù hợp     ║  Dict<string, string>  ║
║  VD: Todo            ║  List<Todo>           ║  ❌ Không phù hợp      ║
╚══════════════════════╩═══════════════════════╩════════════════════════╝
```

### Quy tắc chọn

```
❓ Bạn cần TRA CỨU theo key?          → Dictionary
❓ Bạn cần DANH SÁCH có thứ tự?       → List
❓ Bạn cần ĐẾM tần suất?             → Dictionary
❓ Bạn cần NHÓM dữ liệu?             → Dictionary<string, List<T>>
❓ Bạn cần sắp xếp, lọc tuần tự?     → List
❓ Bạn cần ánh xạ 1-1 (map)?         → Dictionary
```

---

## 7. SortedDictionary & SortedList

### 7.1 SortedDictionary — Tự động sắp xếp theo Key

```csharp
var sorted = new SortedDictionary<string, int>
{
    { "Chi", 78 },
    { "An", 85 },
    { "Bình", 92 }
};

// Tự động sắp xếp key A-Z
foreach (var (name, score) in sorted)
    Console.WriteLine($"{name}: {score}");
// An: 85
// Bình: 92
// Chi: 78
```

### 7.2 So sánh 3 loại

```
╔════════════════════════╦══════════════╦════════════╦══════════════╗
║                        ║ Dictionary   ║ Sorted     ║ SortedList   ║
║                        ║              ║ Dictionary ║              ║
╠════════════════════════╬══════════════╬════════════╬══════════════╣
║ Sắp xếp key           ║ ❌ Không     ║ ✅ Có      ║ ✅ Có        ║
║ Lookup speed           ║ O(1) 🏆     ║ O(log n)   ║ O(log n)     ║
║ Insert speed           ║ O(1) 🏆     ║ O(log n)   ║ O(n)         ║
║ Memory                 ║ Nhiều hơn   ║ Trung bình ║ Ít nhất 🏆   ║
║ Dùng khi               ║ Tra cứu     ║ Cần key    ║ Ít thay đổi  ║
║                        ║ nhanh       ║ có thứ tự  ║ + tiết kiệm  ║
╚════════════════════════╩══════════════╩════════════╩══════════════╝
```

> 💡 **90% trường hợp** → dùng `Dictionary<TKey, TValue>` là đủ.

---

## 8. Dictionary<string, List<T>> — Nhóm Dữ Liệu

### 8.1 Ví dụ: Nhóm sinh viên theo ngành

```csharp
var studentsByMajor = new Dictionary<string, List<string>>
{
    { "CNTT", new List<string> { "An", "Chi", "Phong" } },
    { "Kinh tế", new List<string> { "Bình", "Em" } },
    { "Cơ khí", new List<string> { "Dũng" } }
};

// Truy cập nhóm
List<string> itStudents = studentsByMajor["CNTT"];
Console.WriteLine($"Sinh viên CNTT: {string.Join(", ", itStudents)}");

// Thêm sinh viên vào nhóm
if (studentsByMajor.ContainsKey("CNTT"))
    studentsByMajor["CNTT"].Add("Hải");
else
    studentsByMajor["CNTT"] = new List<string> { "Hải" };

// Duyệt tất cả nhóm
foreach (var (major, students) in studentsByMajor)
{
    Console.WriteLine($"\n{major} ({students.Count} SV):");
    students.ForEach(s => Console.WriteLine($"  - {s}"));
}
```

### 8.2 Pattern: Thêm vào nhóm an toàn

```csharp
// Pattern phổ biến: thêm item vào nhóm
void AddToGroup(Dictionary<string, List<string>> groups,
                string groupKey, string item)
{
    if (!groups.ContainsKey(groupKey))
        groups[groupKey] = new List<string>();

    groups[groupKey].Add(item);
}

// Sử dụng:
var groups = new Dictionary<string, List<string>>();
AddToGroup(groups, "CNTT", "An");
AddToGroup(groups, "CNTT", "Bình");
AddToGroup(groups, "Kinh tế", "Chi");
```

```
MINH HỌA Dictionary<string, List<string>>:

╔═════════════╦════════════════════════════╗
║  Key        ║  Value (List<string>)      ║
╠═════════════╬════════════════════════════╣
║  "CNTT"     ║  ["An", "Chi", "Phong"]    ║
║  "Kinh tế"  ║  ["Bình", "Em"]            ║
║  "Cơ khí"   ║  ["Dũng"]                  ║
╚═════════════╩════════════════════════════╝
```

---

## 9. So Sánh: JS Object/Map vs C# Dictionary

```
╔══════════════════════════╦══════════════════════════════╗
║  JavaScript              ║  C# Dictionary               ║
╠══════════════════════════╬══════════════════════════════╣
║  let obj = {}            ║  var dict = new Dictionary   ║
║                          ║    <string, int>();           ║
║  obj.key = value         ║  dict["key"] = value         ║
║  obj["key"] = value      ║  dict["key"] = value         ║
║  delete obj.key          ║  dict.Remove("key")          ║
║  "key" in obj            ║  dict.ContainsKey("key")     ║
║  Object.keys(obj)        ║  dict.Keys                   ║
║  Object.values(obj)      ║  dict.Values                 ║
║  Object.entries(obj)     ║  foreach (var pair in dict)  ║
║  obj.key ?? defaultVal   ║  dict.TryGetValue(key, out)  ║
╠══════════════════════════╬══════════════════════════════╣
║  JS Map                  ║  C# Dictionary               ║
╠══════════════════════════╬══════════════════════════════╣
║  map.set(key, val)       ║  dict[key] = val             ║
║  map.get(key)            ║  dict[key]                   ║
║  map.has(key)            ║  dict.ContainsKey(key)       ║
║  map.delete(key)         ║  dict.Remove(key)            ║
║  map.size                ║  dict.Count                  ║
║  map.clear()             ║  dict.Clear()                ║
║  map.forEach((v,k)=>{})  ║  foreach (var (k,v) in dict) ║
╠══════════════════════════╬══════════════════════════════╣
║  Key: bất kỳ kiểu       ║  Key: bất kỳ kiểu (TKey)    ║
║  Value: bất kỳ kiểu     ║  Value: đúng 1 kiểu (TValue)║
║  Không type-safe         ║  ✅ Type-safe hoàn toàn      ║
╚══════════════════════════╩══════════════════════════════╝
```

---

## 10. Performance — O(1) Lookup

### 10.1 Tại sao Dictionary nhanh?

```
Dictionary dùng HASH TABLE bên trong:

Key "An" → Hash("An") = 42 → slot[42] → Value: 85
Key "Bình" → Hash("Bình") = 17 → slot[17] → Value: 92

╔══════════════════════════════════════════════════╗
║  Hash Table (bên trong Dictionary)               ║
║                                                  ║
║  Slot  0: [rỗng]                                ║
║  Slot  1: [rỗng]                                ║
║  ...                                             ║
║  Slot 17: Key="Bình", Value=92                   ║
║  ...                                             ║
║  Slot 42: Key="An", Value=85                     ║
║  ...                                             ║
║                                                  ║
║  Lookup "An":                                    ║
║  1. Tính hash("An") = 42                         ║
║  2. Nhảy thẳng đến slot[42]                      ║
║  3. Lấy value = 85                               ║
║  → O(1) — KHÔNG cần duyệt qua tất cả!           ║
╚══════════════════════════════════════════════════╝
```

### 10.2 So sánh tốc độ

```
Tìm 1 phần tử trong 1 triệu phần tử:

List<T>.Find()     → O(n)  → duyệt tối đa 1,000,000 phần tử
Dictionary[key]    → O(1)  → nhảy thẳng, ~1 bước

╔════════════╦═══════════════╦═══════════════╗
║  Thao tác  ║  List<T>      ║  Dictionary   ║
╠════════════╬═══════════════╬═══════════════╣
║  Tìm kiếm  ║  O(n) 🐢     ║  O(1) 🚀      ║
║  Thêm      ║  O(1)*        ║  O(1)*        ║
║  Xóa       ║  O(n)         ║  O(1)         ║
║  Duyệt     ║  O(n)         ║  O(n)         ║
╚════════════╩═══════════════╩═══════════════╝
* = Amortized (trung bình)
```

---

## 11. Common Mistakes & Best Practices

### ❌ Lỗi thường gặp

```csharp
// 1. ❌ Truy cập key không tồn tại
int score = dict["Xyz"]; // 💥 KeyNotFoundException!

// ✅ Dùng TryGetValue
if (dict.TryGetValue("Xyz", out int s))
    Console.WriteLine(s);

// 2. ❌ Add key đã tồn tại
dict.Add("An", 85);
dict.Add("An", 90); // 💥 ArgumentException!

// ✅ Dùng [key] = value (ghi đè)
dict["An"] = 90;

// 3. ❌ Thay đổi Dictionary trong foreach
foreach (var pair in dict)
    dict.Remove(pair.Key); // 💥 InvalidOperationException!

// ✅ Copy keys ra trước
foreach (var key in dict.Keys.ToList())
    dict.Remove(key);

// 4. ❌ Dùng mutable object làm key
// Key phải có GetHashCode() và Equals() ổn định
// string, int, enum là OK. Custom class cần override.

// 5. ❌ Giả định Dictionary có thứ tự
// Dictionary KHÔNG đảm bảo thứ tự insertion!
// Nếu cần thứ tự → dùng SortedDictionary hoặc List
```

### ✅ Best Practices

```csharp
// 1. Luôn dùng TryGetValue thay vì ContainsKey + truy cập
// ❌
if (dict.ContainsKey(key))
    var val = dict[key]; // 2 lần lookup

// ✅
if (dict.TryGetValue(key, out var val))
    // Chỉ 1 lần lookup

// 2. Dùng [key] = value cho upsert (thêm hoặc cập nhật)
dict[key] = value; // Thêm nếu chưa có, ghi đè nếu đã có

// 3. Chỉ định capacity nếu biết trước
var dict = new Dictionary<string, int>(1000);

// 4. Key nên dùng kiểu immutable
// ✅ string, int, long, enum, record
// ⚠️ Custom class phải override GetHashCode + Equals

// 5. Dùng StringComparer cho case-insensitive keys
var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
dict["Hello"] = 1;
dict["hello"] = 2;  // Ghi đè — vì "Hello" == "hello"
Console.WriteLine(dict.Count); // 1
```

---

## 📌 Tổng Kết

```
Dictionary<TKey, TValue> = Bộ sưu tập Key-Value cực mạnh 🦸

✅ Tra cứu O(1) — CỰC NHANH theo key
✅ Key UNIQUE — không trùng lặp
✅ CRUD đầy đủ — Add, Remove, [key]=value, Clear
✅ TryGetValue — đọc AN TOÀN, không Exception
✅ Duyệt — foreach KeyValuePair, .Keys, .Values
✅ Nhóm — Dictionary<string, List<T>>
✅ Type-safe — chỉ chứa đúng kiểu TKey, TValue

Bên trong: Hash Table → O(1) lookup
Namespace: System.Collections.Generic
```

> ⏭️ **Bài tiếp theo**: Bài 27 — Các Collection khác & Khi nào dùng gì
