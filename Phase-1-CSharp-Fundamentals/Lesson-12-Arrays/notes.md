# 📝 Lesson 12 — Notes: Arrays

---

## 🧠 Tóm tắt

### Khai báo:

```csharp
int[] arr = new int[5];              // 5 phần tử, default = 0
int[] arr = { 1, 2, 3, 4, 5 };      // Gán luôn giá trị
int[,] matrix = new int[3, 4];      // 2D: 3 hàng × 4 cột
int[][] jagged = new int[3][];      // Jagged: mỗi hàng khác dài
```

### Truy cập:

```csharp
arr[0]        // Phần tử đầu
arr[^1]       // Phần tử cuối
arr[1..3]     // Slice: index 1 đến 2
arr.Length    // Số phần tử
```

### Methods quan trọng:

```csharp
Array.Sort(arr);                    // Sắp xếp
Array.Reverse(arr);                 // Đảo ngược
Array.IndexOf(arr, value);          // Tìm vị trí
Array.Find(arr, x => x > 5);       // Tìm phần tử
Array.FindAll(arr, x => x > 5);    // Tìm tất cả
Array.Exists(arr, x => x > 5);     // Kiểm tra tồn tại
Array.Copy(src, dest, length);     // Copy
(int[])arr.Clone();                // Clone
Array.Fill(arr, value);            // Gán hết
Array.Clear(arr, 0, length);      // Xóa hết
Array.Resize(ref arr, newSize);    // Đổi kích thước
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | `IndexOutOfRangeException` | `i < arr.Length` (không `<=`) |
| 2 | Gán array = share reference | `.Clone()` nếu cần copy |
| 3 | Array không thêm/xóa được | Dùng `List<T>` (Phase 3) |
| 4 | Off-by-one trong loop | Kiểm tra boundary: 0 → Length-1 |
| 5 | Quên swap parallel arrays | Swap TẤT CẢ mảng song song |

---

## ✅ Checklist

- [ ] Biết khai báo array 1D: `new int[n]` và `{ }`
- [ ] Biết truy cập: `arr[i]`, `arr[^1]`, `arr[a..b]`
- [ ] Biết duyệt: `for` (có index) và `foreach` (chỉ đọc)
- [ ] Biết `Array.Sort`, `Array.Reverse`, `Array.IndexOf`
- [ ] Biết clone: `(int[])arr.Clone()`
- [ ] Biết mảng 2D: `int[,]` và `int[][]`
- [ ] Biết tự viết Bubble Sort
- [ ] Biết tự viết Linear Search
- [ ] Biết xóa phần tử = dịch mảng sang trái
- [ ] Biết mảng song song (parallel arrays)

---

## 🔗 Liên kết

```
Lesson 11: Memory Basics
    ↓
Lesson 12: Arrays ← BẠN Ở ĐÂY
    ↓
Lesson 13: String
    ↓
Lesson 14: Exception Handling
```

> 📌 **Bài tiếp:** Lesson 13 — String (xử lý chuỗi nâng cao, StringBuilder, Regex)

> 💡 **Phase 3 Preview:** Sau khi học OOP, bạn sẽ thay Array bằng `List<T>` — linh hoạt hơn (thêm/xóa tự do). Array là nền tảng để hiểu List.
