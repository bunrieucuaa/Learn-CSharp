# 📚 Bài 27: Stack\<T> & Queue\<T> — Ngăn Xếp & Hàng Đợi

## 🎯 Mục tiêu bài học
- Hiểu khái niệm LIFO (Stack) và FIFO (Queue)
- Sử dụng thành thạo các phương thức của Stack\<T> và Queue\<T>
- Biết khi nào nên dùng Stack, Queue thay vì List
- Làm quen với PriorityQueue\<T, TPriority>

---

## 1. Stack\<T> — Ngăn Xếp (LIFO)

### 📖 Khái niệm: Last In, First Out

Stack hoạt động như một **chồng đĩa** — đĩa đặt vào cuối cùng sẽ được lấy ra đầu tiên.

```
    ┌─────────┐
    │  Đĩa 3  │  ← Push vào cuối cùng, Pop ra đầu tiên
    ├─────────┤
    │  Đĩa 2  │
    ├─────────┤
    │  Đĩa 1  │  ← Push vào đầu tiên, Pop ra cuối cùng
    └─────────┘
       STACK
```

### 🔑 Nguyên tắc LIFO:
- **Last In** = phần tử thêm vào **sau cùng**
- **First Out** = sẽ được lấy ra **đầu tiên**
- Chỉ thao tác ở **đỉnh** (top) của stack

```csharp
using System.Collections.Generic;

Stack<string> dishes = new Stack<string>();

// Push — đặt lên đỉnh
dishes.Push("Đĩa 1");   // [Đĩa 1]
dishes.Push("Đĩa 2");   // [Đĩa 2, Đĩa 1]
dishes.Push("Đĩa 3");   // [Đĩa 3, Đĩa 2, Đĩa 1]

// Pop — lấy từ đỉnh
string top = dishes.Pop();  // "Đĩa 3" — lấy ra và XÓA
Console.WriteLine(top);     // Đĩa 3
// Stack còn: [Đĩa 2, Đĩa 1]
```

---

## 2. Queue\<T> — Hàng Đợi (FIFO)

### 📖 Khái niệm: First In, First Out

Queue hoạt động như **xếp hàng mua vé** — ai đến trước được phục vụ trước.

```
  Enqueue →  ┌───┬───┬───┬───┐  → Dequeue
             │ D │ C │ B │ A │
             └───┴───┴───┴───┘
            Cuối              Đầu
         (thêm vào)       (lấy ra)
```

### 🔑 Nguyên tắc FIFO:
- **First In** = phần tử thêm vào **đầu tiên**
- **First Out** = sẽ được lấy ra **đầu tiên**
- Thêm ở **cuối**, lấy ở **đầu**

```csharp
Queue<string> line = new Queue<string>();

// Enqueue — xếp vào cuối hàng
line.Enqueue("Khách A");   // [Khách A]
line.Enqueue("Khách B");   // [Khách A, Khách B]
line.Enqueue("Khách C");   // [Khách A, Khách B, Khách C]

// Dequeue — phục vụ từ đầu hàng
string first = line.Dequeue();  // "Khách A" — lấy ra và XÓA
Console.WriteLine(first);       // Khách A
// Queue còn: [Khách B, Khách C]
```

---

## 3. Các phương thức của Stack\<T>

| Phương thức     | Mô tả                                        | Throws Exception? |
|-----------------|-----------------------------------------------|--------------------|
| `Push(item)`    | Đẩy phần tử lên đỉnh stack                   | Không              |
| `Pop()`         | Lấy và **xóa** phần tử ở đỉnh                | ✅ Nếu rỗng       |
| `Peek()`        | **Xem** phần tử ở đỉnh (không xóa)           | ✅ Nếu rỗng       |
| `Count`         | Số phần tử trong stack                        | Không              |
| `Contains(item)`| Kiểm tra phần tử có tồn tại không            | Không              |
| `Clear()`       | Xóa toàn bộ stack                             | Không              |
| `TryPop(out T)` | Pop an toàn, trả `false` nếu rỗng            | Không              |
| `TryPeek(out T)`| Peek an toàn, trả `false` nếu rỗng           | Không              |
| `ToArray()`     | Chuyển stack thành mảng                        | Không              |

```csharp
Stack<int> numbers = new Stack<int>();
numbers.Push(10);
numbers.Push(20);
numbers.Push(30);

Console.WriteLine(numbers.Peek());      // 30 — xem nhưng KHÔNG xóa
Console.WriteLine(numbers.Count);       // 3
Console.WriteLine(numbers.Contains(20)); // True

// ⚠️ An toàn hơn với TryPop
if (numbers.TryPop(out int value))
{
    Console.WriteLine($"Popped: {value}");  // Popped: 30
}

// Pop trên stack rỗng → Exception!
Stack<int> empty = new Stack<int>();
// empty.Pop(); // ❌ InvalidOperationException: Stack empty
```

---

## 4. Các phương thức của Queue\<T>

| Phương thức        | Mô tả                                     | Throws Exception? |
|--------------------|--------------------------------------------|--------------------|
| `Enqueue(item)`    | Thêm phần tử vào cuối hàng đợi            | Không              |
| `Dequeue()`        | Lấy và **xóa** phần tử ở đầu             | ✅ Nếu rỗng       |
| `Peek()`           | **Xem** phần tử ở đầu (không xóa)         | ✅ Nếu rỗng       |
| `Count`            | Số phần tử trong queue                     | Không              |
| `Contains(item)`   | Kiểm tra phần tử có tồn tại không         | Không              |
| `Clear()`          | Xóa toàn bộ queue                          | Không              |
| `TryDequeue(out T)`| Dequeue an toàn, trả `false` nếu rỗng     | Không              |
| `TryPeek(out T)`   | Peek an toàn, trả `false` nếu rỗng        | Không              |
| `ToArray()`        | Chuyển queue thành mảng                     | Không              |

```csharp
Queue<string> tasks = new Queue<string>();
tasks.Enqueue("Task A");
tasks.Enqueue("Task B");
tasks.Enqueue("Task C");

Console.WriteLine(tasks.Peek());       // Task A — xem đầu hàng
Console.WriteLine(tasks.Count);        // 3

// ⚠️ An toàn hơn với TryDequeue
if (tasks.TryDequeue(out string? task))
{
    Console.WriteLine($"Processing: {task}");  // Processing: Task A
}
```

---

## 5. So sánh Stack vs Queue — Minh họa trực quan

```
  ╔══════════════════════════════════════════════════════╗
  ║              STACK (LIFO)         QUEUE (FIFO)       ║
  ║                                                      ║
  ║   Push → ┌───┐              Enqueue → ┌───┐         ║
  ║          │ C │                        │ C │          ║
  ║          ├───┤                ┌───┬───┼───┤          ║
  ║          │ B │                │ A │ B │   │          ║
  ║          ├───┤                └───┴───┴───┘          ║
  ║          │ A │                  ↑                    ║
  ║          └───┘              Dequeue (lấy A)          ║
  ║            ↑                                         ║
  ║       Pop (lấy C)                                    ║
  ╚══════════════════════════════════════════════════════╝
```

---

## 6. Use Cases thực tế

### 🔹 Stack — Khi nào dùng?

| Use Case                 | Giải thích                                    |
|--------------------------|-----------------------------------------------|
| **Undo/Redo**            | Ctrl+Z lấy hành động cuối cùng               |
| **Bracket Matching**     | Kiểm tra ngoặc `({[]})` có hợp lệ không      |
| **DFS (tìm kiếm sâu)**  | Duyệt đồ thị theo chiều sâu                  |
| **Call Stack**            | Hệ thống quản lý lời gọi hàm                |
| **Expression Evaluation** | Tính biểu thức `3 + 4 * 2`                  |
| **Reverse chuỗi/mảng**  | Đảo ngược thứ tự các phần tử                 |

### 🔹 Queue — Khi nào dùng?

| Use Case                 | Giải thích                                    |
|--------------------------|-----------------------------------------------|
| **Print Queue**          | In tài liệu theo thứ tự gửi                  |
| **Task Scheduling**      | Xử lý task theo thứ tự đến                   |
| **BFS (tìm kiếm rộng)** | Duyệt đồ thị theo chiều rộng                 |
| **Message Queue**        | Xử lý tin nhắn theo thứ tự nhận              |
| **Buffer**               | Bộ đệm dữ liệu streaming                    |
| **Order Processing**     | Xử lý đơn hàng theo thứ tự                   |

---

## 7. Bracket Matching — Ứng dụng kinh điển của Stack

Kiểm tra chuỗi ngoặc có hợp lệ không: `({[]})`

```
Duyệt: ( { [ ] } )

Bước 1: '(' → Push   Stack: [(]
Bước 2: '{' → Push   Stack: [(, {]
Bước 3: '[' → Push   Stack: [(, {, []
Bước 4: ']' → Pop '[' → Khớp ✅  Stack: [(, {]
Bước 5: '}' → Pop '{' → Khớp ✅  Stack: [(]
Bước 6: ')' → Pop '(' → Khớp ✅  Stack: []

Stack rỗng → HỢP LỆ ✅
```

---

## 8. PriorityQueue\<T, TPriority> (C# 10+)

Hàng đợi **ưu tiên** — phần tử có priority nhỏ nhất được lấy ra trước.

```csharp
// PriorityQueue<phần_tử, độ_ưu_tiên>
PriorityQueue<string, int> emergencyRoom = new();

// Enqueue với priority (số nhỏ = ưu tiên cao)
emergencyRoom.Enqueue("Đau đầu nhẹ", 5);
emergencyRoom.Enqueue("Gãy xương", 2);
emergencyRoom.Enqueue("Ngừng tim", 1);    // ← Ưu tiên cao nhất
emergencyRoom.Enqueue("Cảm cúm", 4);

// Dequeue theo thứ tự ưu tiên
while (emergencyRoom.Count > 0)
{
    string patient = emergencyRoom.Dequeue();
    Console.WriteLine($"Khám: {patient}");
}
// Output:
// Khám: Ngừng tim       (priority 1)
// Khám: Gãy xương       (priority 2)
// Khám: Cảm cúm         (priority 4)
// Khám: Đau đầu nhẹ     (priority 5)
```

> ⚠️ PriorityQueue **KHÔNG** đảm bảo thứ tự FIFO cho các phần tử cùng priority.

---

## 9. So sánh với JavaScript

### JS dùng Array cho cả Stack lẫn Queue:

```javascript
// JS — Stack behavior (push/pop)
let stack = [];
stack.push("A");    // ["A"]
stack.push("B");    // ["A", "B"]
stack.pop();        // "B" — LIFO ✅

// JS — Queue behavior (push/shift)
let queue = [];
queue.push("A");    // ["A"]
queue.push("B");    // ["A", "B"]
queue.shift();      // "A" — FIFO ✅
// ⚠️ shift() rất CHẬM — O(n) vì phải dịch toàn bộ mảng!
```

### C# có Collection chuyên dụng:

```csharp
// C# — Stack chuyên dụng
Stack<string> stack = new Stack<string>();
stack.Push("A");
stack.Pop();    // LIFO — O(1) ✅

// C# — Queue chuyên dụng
Queue<string> queue = new Queue<string>();
queue.Enqueue("A");
queue.Dequeue();  // FIFO — O(1) ✅ (nhanh hơn JS shift!)
```

| Đặc điểm              | JavaScript             | C#                        |
|------------------------|------------------------|---------------------------|
| Stack                  | `Array` (push/pop)     | `Stack<T>` chuyên dụng    |
| Queue                  | `Array` (push/shift)   | `Queue<T>` chuyên dụng    |
| Priority Queue         | Phải tự code           | `PriorityQueue<T,P>`      |
| Performance Queue      | O(n) cho shift         | O(1) cho Dequeue          |
| Type Safety            | Không (any type)       | Có (generic)              |
| Random Access          | Có (`arr[i]`)          | ❌ Không hỗ trợ           |

---

## 10. Stack vs Queue vs List — Chọn cái nào?

```
  ┌──────────────────────────────────────────────────────────┐
  │              Bạn cần gì?                                 │
  │                                                          │
  │  Truy cập theo index? ────────────→ List<T>              │
  │                                                          │
  │  Xử lý theo thứ tự LIFO? ────────→ Stack<T>             │
  │  (cái mới nhất trước)                                    │
  │                                                          │
  │  Xử lý theo thứ tự FIFO? ────────→ Queue<T>             │
  │  (cái cũ nhất trước)                                     │
  │                                                          │
  │  Xử lý theo độ ưu tiên? ─────────→ PriorityQueue<T,P>   │
  │                                                          │
  │  Cần add/remove ở cả 2 đầu? ─────→ LinkedList<T>        │
  └──────────────────────────────────────────────────────────┘
```

### ⚠️ Lưu ý quan trọng:
- Stack và Queue **KHÔNG** hỗ trợ truy cập theo index (`stack[0]` ❌)
- Nếu cần vừa LIFO/FIFO vừa random access → dùng `List<T>`
- Stack/Queue tối ưu hơn List khi bạn chỉ cần thao tác ở đầu/cuối

---

## 💡 Tổng kết

```
  ╔═══════════════════════════════════════════════╗
  ║  Stack<T>  =  LIFO  =  Push / Pop / Peek     ║
  ║  Queue<T>  =  FIFO  =  Enqueue / Dequeue     ║
  ║                                               ║
  ║  → Dùng TryPop/TryDequeue cho an toàn        ║
  ║  → PriorityQueue cho hàng đợi ưu tiên        ║
  ║  → Stack/Queue KHÔNG có index access          ║
  ╚═══════════════════════════════════════════════╝
```

> 📌 **Nhớ**: Stack = chồng đĩa (lấy trên cùng), Queue = xếp hàng (ai đến trước phục vụ trước)!
