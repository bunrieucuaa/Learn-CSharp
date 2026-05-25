# Bài 25: List<T> — Ghi Chú & Tóm Tắt

---

## 📝 Tóm Tắt Nhanh

### List<T> là gì?

```
List<T> = Mảng ĐỘNG trong C#
- Tự mở rộng khi thêm phần tử (không cần khai báo kích thước)
- Có sẵn các phương thức CRUD, tìm kiếm, sắp xếp
- Type-safe: chỉ chứa đúng 1 kiểu dữ liệu T
- Namespace: System.Collections.Generic
```

---

## 📋 Cheat Sheet — Tất Cả Phương Thức Quan Trọng

### Khởi tạo

```csharp
List<int> a = new List<int>();                  // Rỗng
List<int> b = new List<int> { 1, 2, 3 };       // Có dữ liệu
List<int> c = new List<int>(100);               // Chỉ định capacity
List<int> d = new List<int>(existingArray);     // Từ array
var e = new List<string> { "a", "b" };          // Dùng var
```

### ➕ Thêm (Create)

```
╔═══════════════════════════════╦══════════════════════════════════════╗
║  Phương thức                  ║  Mô tả                              ║
╠═══════════════════════════════╬══════════════════════════════════════╣
║  list.Add(item)               ║  Thêm 1 phần tử vào CUỐI           ║
║  list.AddRange(collection)    ║  Thêm NHIỀU phần tử vào cuối       ║
║  list.Insert(index, item)     ║  Chèn tại vị trí index             ║
║  list.InsertRange(index, col) ║  Chèn nhiều phần tử tại index      ║
╚═══════════════════════════════╩══════════════════════════════════════╝
```

### 📖 Đọc (Read)

```
╔═══════════════════════════════╦══════════════════════════════════════╗
║  Phương thức / Thuộc tính     ║  Mô tả                              ║
╠═══════════════════════════════╬══════════════════════════════════════╣
║  list[index]                  ║  Truy cập phần tử tại index         ║
║  list[^1]                     ║  Phần tử cuối cùng (index from end) ║
║  list.Count                   ║  Số phần tử hiện tại                ║
║  list.Capacity                ║  Dung lượng array bên trong         ║
║  list.Contains(item)          ║  Kiểm tra có chứa item không        ║
║  list.IndexOf(item)           ║  Vị trí đầu tiên (-1 nếu không có) ║
║  list.LastIndexOf(item)       ║  Vị trí cuối cùng                   ║
╚═══════════════════════════════╩══════════════════════════════════════╝
```

### 🔍 Tìm kiếm nâng cao (với Predicate)

```
╔═══════════════════════════════╦══════════════════════════════════════╗
║  Phương thức                  ║  Mô tả                    ║ Trả về  ║
╠═══════════════════════════════╬════════════════════════════╬════════╣
║  list.Find(predicate)         ║  Phần tử ĐẦU TIÊN khớp   ║ T      ║
║  list.FindLast(predicate)     ║  Phần tử CUỐI CÙNG khớp   ║ T      ║
║  list.FindAll(predicate)      ║  TẤT CẢ phần tử khớp      ║ List<T>║
║  list.FindIndex(predicate)    ║  INDEX đầu tiên khớp       ║ int    ║
║  list.FindLastIndex(pred)     ║  INDEX cuối cùng khớp      ║ int    ║
║  list.Exists(predicate)       ║  Có ÍT NHẤT 1 phần tử?    ║ bool   ║
║  list.TrueForAll(predicate)   ║  TẤT CẢ thỏa điều kiện?   ║ bool   ║
╚═══════════════════════════════╩════════════════════════════╩════════╝

Predicate = lambda expression: item => điều_kiện
Ví dụ: n => n > 10, s => s.Contains("abc")
```

### ✏️ Sửa (Update)

```csharp
list[index] = newValue;    // Gán trực tiếp
```

### 🗑️ Xóa (Delete)

```
╔═══════════════════════════════╦══════════════════════════════════════╗
║  Phương thức                  ║  Mô tả                              ║
╠═══════════════════════════════╬══════════════════════════════════════╣
║  list.Remove(item)            ║  Xóa phần tử ĐẦU TIÊN khớp         ║
║  list.RemoveAt(index)         ║  Xóa tại vị trí index               ║
║  list.RemoveRange(index, cnt) ║  Xóa nhiều phần tử liên tiếp        ║
║  list.RemoveAll(predicate)    ║  Xóa TẤT CẢ thỏa điều kiện         ║
║  list.Clear()                 ║  Xóa TOÀN BỘ                        ║
╚═══════════════════════════════╩══════════════════════════════════════╝
```

### 🔄 Sắp xếp & Chuyển đổi

```
╔═══════════════════════════════╦══════════════════════════════════════╗
║  Phương thức                  ║  Mô tả                              ║
╠═══════════════════════════════╬══════════════════════════════════════╣
║  list.Sort()                  ║  Sắp xếp tăng dần (mặc định)       ║
║  list.Sort((a,b) => ...)      ║  Sắp xếp tùy chỉnh                 ║
║  list.Reverse()               ║  Đảo ngược thứ tự                   ║
║  list.ToArray()               ║  Chuyển thành array                  ║
║  list.GetRange(index, count)  ║  Lấy 1 phần (sub-list)             ║
║  list.ConvertAll(converter)   ║  Chuyển đổi kiểu (map)             ║
║  list.ForEach(action)         ║  Thực hiện action cho mỗi phần tử  ║
║  list.CopyTo(array)           ║  Copy vào array                     ║
║  list.TrimExcess()            ║  Thu gọn Capacity = Count           ║
╚═══════════════════════════════╩══════════════════════════════════════╝
```

---

## 🔄 JS → C# Quick Map

```
╔════════════════════════╦════════════════════════════╗
║  JavaScript            ║  C# List<T>                ║
╠════════════════════════╬════════════════════════════╣
║  let arr = []          ║  var list = new List<T>()  ║
║  [1, 2, 3]             ║  new List<int>{1, 2, 3}   ║
║  arr.push(x)           ║  list.Add(x)               ║
║  arr.pop()             ║  list.RemoveAt(Count-1)    ║
║  arr.unshift(x)        ║  list.Insert(0, x)         ║
║  arr.shift()           ║  list.RemoveAt(0)          ║
║  arr.splice(i,1)       ║  list.RemoveAt(i)          ║
║  arr.splice(i,0,x)     ║  list.Insert(i, x)         ║
║  arr.length            ║  list.Count                ║
║  arr.indexOf(x)        ║  list.IndexOf(x)           ║
║  arr.includes(x)       ║  list.Contains(x)          ║
║  arr.find(fn)          ║  list.Find(fn)             ║
║  arr.filter(fn)        ║  list.FindAll(fn)          ║
║  arr.findIndex(fn)     ║  list.FindIndex(fn)        ║
║  arr.some(fn)          ║  list.Exists(fn)           ║
║  arr.every(fn)         ║  list.TrueForAll(fn)       ║
║  arr.forEach(fn)       ║  list.ForEach(fn)          ║
║  arr.map(fn)           ║  list.ConvertAll(fn)       ║
║  arr.sort(fn)          ║  list.Sort(fn)             ║
║  arr.reverse()         ║  list.Reverse()            ║
║  arr.concat(arr2)      ║  list.AddRange(list2)      ║
║  arr.slice(a,b)        ║  list.GetRange(a, b-a)     ║
║  [...arr]              ║  new List<T>(list)         ║
╚════════════════════════╩════════════════════════════╝
```

---

## ⚠️ Lỗi Thường Gặp

### 1. Xóa phần tử trong foreach

```csharp
// ❌ SAI — InvalidOperationException
foreach (var item in list)
    if (item < 0) list.Remove(item);

// ✅ ĐÚNG — RemoveAll
list.RemoveAll(item => item < 0);

// ✅ ĐÚNG — Duyệt ngược
for (int i = list.Count - 1; i >= 0; i--)
    if (list[i] < 0) list.RemoveAt(i);
```

### 2. Truy cập index ngoài phạm vi

```csharp
// ❌ SAI
var item = list[list.Count]; // Count = 5 → index 5 không tồn tại!

// ✅ ĐÚNG
var item = list[list.Count - 1]; // Index cuối = Count - 1
```

### 3. Nhầm Count vs Capacity

```csharp
// Count = số phần tử THỰC TẾ
// Capacity = kích thước array BÊN TRONG (>= Count)
// Luôn dùng Count, hiếm khi cần Capacity
```

### 4. Quên kiểm tra null/rỗng

```csharp
// ✅ Kiểm tra trước khi thao tác
if (list != null && list.Count > 0)
{
    var first = list[0];
}
```

### 5. Find trả về default khi không tìm thấy

```csharp
int result = list.Find(x => x > 1000);
// Nếu không tìm thấy → result = 0 (default int) — KHÔNG phải lỗi!

// Với reference type:
Student s = students.Find(s => s.Id == 999);
// Nếu không tìm thấy → s = null — CẨN THẬN NullReferenceException!
```

---

## 🧠 Memory — Cách List Hoạt Động

```
List<T> = Wrapper quanh array nội bộ T[]

╔══════════════════════════════════════════╗
║  List<int> list = new List<int>();       ║
║                                          ║
║  Bước 1: Count=0, Capacity=0            ║
║  [rỗng]                                 ║
║                                          ║
║  Bước 2: Add(1) → Capacity=4            ║
║  [1, _, _, _]                            ║
║                                          ║
║  Bước 3: Add(2), Add(3), Add(4)         ║
║  [1, 2, 3, 4]  ← ĐẦY!                  ║
║                                          ║
║  Bước 4: Add(5) → RESIZE! Capacity=8    ║
║  [1, 2, 3, 4, 5, _, _, _]               ║
║  (tạo array mới gấp đôi, copy sang)     ║
║                                          ║
║  ⚡ Resize tốn O(n) — nên chỉ định      ║
║     Capacity nếu biết trước số lượng     ║
╚══════════════════════════════════════════╝
```

---

## ✅ Checklist Kiến Thức

Đánh dấu ✅ khi đã nắm vững:

- [ ] Hiểu hạn chế của array và lý do cần List<T>
- [ ] Tạo List: `new List<T>()`, collection initializer, từ array
- [ ] Hiểu Generic `<T>` cơ bản (placeholder cho kiểu)
- [ ] **Thêm**: Add, AddRange, Insert, InsertRange
- [ ] **Đọc**: [index], Count, Contains, IndexOf
- [ ] **Sửa**: list[index] = newValue
- [ ] **Xóa**: Remove, RemoveAt, RemoveAll, Clear
- [ ] **Tìm kiếm**: Find, FindAll, FindIndex, Exists, TrueForAll
- [ ] **Sắp xếp**: Sort(), Sort(comparison), Reverse()
- [ ] **Chuyển đổi**: ToArray(), ConvertAll(), GetRange(), ForEach()
- [ ] Hiểu Capacity vs Count (cơ chế resize gấp đôi)
- [ ] Biết cách xóa phần tử an toàn (RemoveAll hoặc duyệt ngược)
- [ ] So sánh được JS Array với C# List<T>
- [ ] Dùng List<T> với class tự tạo (List<Student>, ...)
- [ ] Hoàn thành ít nhất 3 bài tập
- [ ] Hoàn thành challenge Shopping Cart

---

## 📚 Đọc Thêm

- [Microsoft Docs — List<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)
- Bài 26: Dictionary<TKey, TValue> — Bộ sưu tập Key-Value
- Bài 28: Generics — Tìm hiểu sâu về `<T>`
