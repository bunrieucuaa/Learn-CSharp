# Bài 35: Sorting (Sắp xếp dữ liệu trong LINQ)

Sắp xếp dữ liệu (Sorting) là một trong những nhu cầu phổ biến nhất trong lập trình. Trong bài học này, bạn sẽ học cách sử dụng LINQ để sắp xếp một bộ sưu tập theo thứ tự tăng dần, giảm dần hoặc sắp xếp dựa trên nhiều tiêu chí lồng nhau một cách cực kỳ gọn gàng.

---

## 1. Các toán tử sắp xếp cơ bản

### 1.1. `OrderBy` & `OrderByDescending`
Dùng để sắp xếp các phần tử theo một tiêu chí xác định.
* **`OrderBy(keySelector)`:** Sắp xếp tăng dần (Ascending).
  * Số: từ nhỏ đến lớn.
  * Chuỗi: theo bảng chữ cái A-Z.
  * Ngày tháng: từ cũ đến mới nhất.
* **`OrderByDescending(keySelector)`:** Sắp xếp giảm dần (Descending).

**Ví dụ:** Sắp xếp danh sách học sinh theo điểm số giảm dần:
```csharp
var sorted = students.OrderByDescending(s => s.Score);
```

### 1.2. Sắp xếp đa tiêu chí với `ThenBy` & `ThenByDescending`
Đây là điểm mạnh vượt trội của LINQ so với các ngôn ngữ khác. Hãy tưởng tượng bạn muốn:
1. Sắp xếp học sinh theo điểm số giảm dần.
2. Nếu hai học sinh có điểm bằng nhau, sắp xếp họ theo Tên tăng dần từ A-Z.

Nếu bạn dùng `OrderBy` liên tiếp:
```csharp
// SAI LẦM: OrderBy sau sẽ xoá bỏ hoàn toàn kết quả sắp xếp của OrderBy trước
var sorted = students.OrderByDescending(s => s.Score).OrderBy(s => s.Name);
```

Để giải quyết sắp xếp đa tiêu chí lồng nhau, C# cung cấp toán tử **`ThenBy`** và **`ThenByDescending`**:
```csharp
// ĐÚNG:
var sorted = students
             .OrderByDescending(s => s.Score) // Tiêu chí 1: Điểm giảm dần
             .ThenBy(s => s.Name);            // Tiêu chí 2: Tên tăng dần (nếu điểm bằng nhau)
```
*Bạn có thể nối tiếp bao nhiêu `.ThenBy()` tùy ý.*

### 1.3. `Reverse` (Đảo ngược chuỗi)
Đơn giản là đảo ngược vị trí các phần tử trong tập hợp mà không quan tâm đến giá trị của chúng.

---

## 2. Sắp xếp tùy biến bằng `IComparer<T>`

Mặc định, C# biết cách so sánh các kiểu dữ liệu cơ bản. Nhưng nếu bạn muốn sắp xếp theo một logic tùy biến riêng biệt (Ví dụ: sắp xếp danh sách các chuỗi độ ưu tiên "High" > "Medium" > "Low"), bạn có thể tự định nghĩa một Class hiện thực hóa interface `IComparer<T>` rồi truyền vào hàm `OrderBy`.

```csharp
public class StringLengthComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x == null || y == null) return 0;
        return x.Length.CompareTo(y.Length); // So sánh theo độ dài chuỗi
    }
}

// Cách dùng:
var sortedWords = words.OrderBy(w => w, new StringLengthComparer());
```

---

## 3. So sánh với JavaScript: Mutating vs Non-Mutating

Đây là điểm cực kỳ dễ nhầm đối với các bạn chuyển từ JS sang C#.

* **Trong JavaScript:**
  Hàm `.sort()` mặc định là **Mutating** — nó trực tiếp sắp xếp và **làm thay đổi mảng gốc**.
  ```javascript
  const arr = [3, 1, 2];
  arr.sort(); 
  console.log(arr); // Output: [1, 2, 3] -> Mảng gốc đã bị sửa đổi!
  ```

* **Trong C# (LINQ):**
  Hàm `OrderBy` của LINQ là **Non-Mutating** — nó **KHÔNG thay đổi** bộ sưu tập gốc. Nó chỉ tạo ra một bộ sưu tập mới đã được sắp xếp (kiểu `IOrderedEnumerable<T>`).
  ```csharp
  List<int> numbers = [3, 1, 2];
  var sorted = numbers.OrderBy(n => n);
  
  Console.WriteLine(string.Join(", ", numbers)); // Output: 3, 1, 2 -> Mảng gốc giữ nguyên!
  Console.WriteLine(string.Join(", ", sorted));  // Output: 1, 2, 3 -> Mảng đã sắp xếp
  ```

---

## 4. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Viết nhiều `OrderBy` liên tiếp
Như đã giải thích ở mục 1.2, viết nhiều `OrderBy` sẽ làm ghi đè các tiêu chí trước đó, khiến hệ thống tốn tài nguyên chạy sắp xếp nhiều lần vô ích mà kết quả lại sai.
*Khắc phục:* Luôn dùng `ThenBy` / `ThenByDescending` cho các tiêu chí thứ hai trở đi.

### Sai lầm 2: Bỏ qua sắp xếp ổn định (Stable Sort)
LINQ đảm bảo **Stable Sort**. Nghĩa là nếu hai phần tử có khoá so sánh bằng nhau, vị trí ban đầu của chúng trong mảng gốc sẽ được giữ nguyên sau khi sắp xếp. Điều này rất hữu ích trong thiết kế UI.

---

## 5. Checklist đánh giá hiểu bài

1. Hàm `OrderBy` khác `ThenBy` ở điểm nào? Khi nào thì dùng `ThenBy`?
2. Hãy so sánh hành vi thay đổi dữ liệu của hàm `.sort()` trong JS và `OrderBy` trong C# LINQ.
3. Làm thế nào để sắp xếp giảm dần một danh sách số nguyên?
4. Interface nào giúp bạn tự định nghĩa thuật toán so sánh tùy biến cho hàm `OrderBy`?
