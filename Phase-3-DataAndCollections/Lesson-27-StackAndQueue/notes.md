# 📝 Bài 27: Stack\<T> & Queue\<T> — Ghi Chú & Tóm Tắt

---

## 🔑 Tóm tắt nhanh

### Stack\<T> — LIFO (Last In, First Out)
```
  Chồng đĩa: đĩa cuối cùng đặt lên → lấy ra đầu tiên

  Push("A") → Push("B") → Push("C")
  
  ┌───┐
  │ C │ ← Pop() = "C"
  ├───┤
  │ B │ ← Peek() nhìn nhưng KHÔNG lấy
  ├───┤
  │ A │
  └───┘
```

### Queue\<T> — FIFO (First In, First Out)
```
  Xếp hàng: ai đến trước → phục vụ trước

  Enqueue("A") → Enqueue("B") → Enqueue("C")
  
  ┌───┬───┬───┐
  │ A │ B │ C │
  └───┴───┴───┘
    ↑           ↑
  Dequeue()   Enqueue()
  = "A"       thêm vào đây
```

---

## 📋 Cheat Sheet

### Stack\<T> Methods

```csharp
Stack<T> stack = new Stack<T>();

// Thêm / Lấy
stack.Push(item);              // Đẩy lên đỉnh
T item = stack.Pop();          // Lấy + xóa từ đỉnh ⚠️ throw nếu rỗng
T top = stack.Peek();          // Nhìn đỉnh (không xóa) ⚠️ throw nếu rỗng

// An toàn (KHUYÊN DÙNG)
bool ok = stack.TryPop(out T val);    // false nếu rỗng
bool ok = stack.TryPeek(out T val);   // false nếu rỗng

// Khác
int count = stack.Count;                // Số phần tử
bool has = stack.Contains(item);        // Kiểm tra tồn tại
stack.Clear();                          // Xóa tất cả
T[] arr = stack.ToArray();              // → mảng (đỉnh ở index 0)
```

### Queue\<T> Methods

```csharp
Queue<T> queue = new Queue<T>();

// Thêm / Lấy
queue.Enqueue(item);              // Thêm cuối hàng
T item = queue.Dequeue();         // Lấy + xóa đầu hàng ⚠️ throw nếu rỗng
T front = queue.Peek();           // Nhìn đầu hàng (không xóa)

// An toàn (KHUYÊN DÙNG)
bool ok = queue.TryDequeue(out T val);   // false nếu rỗng
bool ok = queue.TryPeek(out T val);      // false nếu rỗng

// Khác
int count = queue.Count;
bool has = queue.Contains(item);
queue.Clear();
T[] arr = queue.ToArray();
```

### PriorityQueue\<T, TPriority> (C# 10+)

```csharp
PriorityQueue<string, int> pq = new();

pq.Enqueue("item", priority);             // Số nhỏ = ưu tiên cao
string item = pq.Dequeue();               // Lấy ưu tiên cao nhất
pq.TryDequeue(out string item, out int p); // An toàn
```

---

## 🔄 So sánh nhanh

| Tiêu chí | Stack\<T> | Queue\<T> | List\<T> |
|----------|-----------|-----------|----------|
| Nguyên tắc | LIFO | FIFO | Random Access |
| Thêm | `Push()` O(1) | `Enqueue()` O(1) | `Add()` O(1) |
| Lấy | `Pop()` O(1) | `Dequeue()` O(1) | `RemoveAt()` O(n) |
| Xem | `Peek()` O(1) | `Peek()` O(1) | `list[i]` O(1) |
| Index | ❌ | ❌ | ✅ |
| Use case | Undo, DFS | Task queue, BFS | Dữ liệu chung |

---

## ⚡ So sánh JS vs C#

```javascript
// JavaScript — dùng Array cho mọi thứ
let stack = [];
stack.push("A");        // Push
stack.pop();            // Pop — O(1)

let queue = [];
queue.push("A");        // Enqueue
queue.shift();          // Dequeue — ⚠️ O(n) CHẬM!
```

```csharp
// C# — Collection chuyên dụng
Stack<string> stack = new();
stack.Push("A");        // Push — O(1)
stack.Pop();            // Pop — O(1)

Queue<string> queue = new();
queue.Enqueue("A");     // Enqueue — O(1)
queue.Dequeue();        // Dequeue — O(1) ✅ NHANH
```

> 💡 C# Queue.Dequeue() nhanh hơn JS Array.shift() vì dùng circular buffer nội bộ!

---

## ⚠️ Lỗi thường gặp

### 1. Pop/Dequeue trên collection rỗng
```csharp
// ❌ SAI — throw InvalidOperationException
Stack<int> stack = new();
int x = stack.Pop(); // 💥 Exception!

// ✅ ĐÚNG — dùng Try methods
if (stack.TryPop(out int value))
{
    Console.WriteLine(value);
}
```

### 2. Truy cập index
```csharp
// ❌ SAI — Stack/Queue KHÔNG có indexer
Stack<int> stack = new();
stack.Push(1);
// int x = stack[0]; // 💥 Compile error!

// ✅ ĐÚNG — chuyển sang Array nếu cần index
int[] arr = stack.ToArray();
int x = arr[0]; // OK
```

### 3. Quên Clear Forward stack khi Visit mới (Undo/Redo)
```csharp
// ❌ SAI — Forward stack vẫn giữ data cũ
void Visit(string url)
{
    _backStack.Push(_current);
    _current = new Page(url);
    // Quên _forwardStack.Clear(); ← BUG!
}

// ✅ ĐÚNG
void Visit(string url)
{
    _backStack.Push(_current);
    _forwardStack.Clear(); // ← QUAN TRỌNG!
    _current = new Page(url);
}
```

### 4. Duyệt foreach không thay đổi collection
```csharp
// ❌ SAI — Cannot modify collection during foreach
foreach (var item in queue)
{
    queue.Dequeue(); // 💥 Exception!
}

// ✅ ĐÚNG — dùng while
while (queue.Count > 0)
{
    var item = queue.Dequeue(); // OK
}
```

---

## 🎯 Khi nào dùng gì?

```
  Câu hỏi                          → Dùng
  ─────────────────────────────────────────────
  "Cái mới nhất trước"             → Stack<T>
  "Ai đến trước phục vụ trước"     → Queue<T>
  "Ưu tiên ai cao nhất"            → PriorityQueue<T,P>
  "Cần truy cập theo vị trí"       → List<T>
  "Cần thêm/xóa ở cả 2 đầu"      → LinkedList<T>
  "Cần tìm kiếm theo key"          → Dictionary<K,V>
```

---

## ✅ Checklist kiến thức

| # | Nội dung | Hiểu? |
|---|---------|-------|
| 1 | Stack = LIFO (Last In First Out) | ⬜ |
| 2 | Queue = FIFO (First In First Out) | ⬜ |
| 3 | Push / Pop / Peek (Stack) | ⬜ |
| 4 | Enqueue / Dequeue / Peek (Queue) | ⬜ |
| 5 | TryPop / TryDequeue (an toàn) | ⬜ |
| 6 | PriorityQueue\<T, TPriority> | ⬜ |
| 7 | Use case: Undo/Redo (Stack) | ⬜ |
| 8 | Use case: Bracket Matching (Stack) | ⬜ |
| 9 | Use case: Task Queue (Queue) | ⬜ |
| 10 | Khác biệt JS vs C# | ⬜ |
| 11 | Stack/Queue KHÔNG có index access | ⬜ |
| 12 | Biết chọn Stack vs Queue vs List | ⬜ |

> 🎯 Đạt **10/12** = Sẵn sàng cho bài tiếp theo!

---

## 📚 Bài tiếp theo

**Bài 28: Generic\<T>** — Viết code một lần, dùng cho MỌI kiểu dữ liệu!
- Generic method, generic class, generic interface
- Constraints: `where T : class`, `where T : new()`
- Tự xây dựng collection generic
