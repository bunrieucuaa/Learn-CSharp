# ✏️ Bài 27: Stack\<T> & Queue\<T> — Bài Tập Thực Hành

---

## Bài 1: Đảo ngược chuỗi bằng Stack ⭐

> **Mục tiêu**: Dùng Stack để đảo ngược một chuỗi ký tự.

### Yêu cầu:
1. Viết method `string ReverseString(string input)` sử dụng `Stack<char>`
2. Push từng ký tự vào Stack, sau đó Pop ra → chuỗi đảo ngược
3. Viết thêm method `bool IsPalindrome(string input)` kiểm tra chuỗi đối xứng
4. Test với nhiều chuỗi khác nhau

### Gợi ý:
```csharp
static string ReverseString(string input)
{
    Stack<char> stack = new Stack<char>();
    // Push từng ký tự...
    // Pop từng ký tự thành chuỗi mới...
}
```

### Input/Output mẫu:
```
ReverseString("Hello")     → "olleH"
ReverseString("12345")     → "54321"
IsPalindrome("racecar")    → True
IsPalindrome("hello")      → False
IsPalindrome("madam")      → True
```

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

class Exercise1_ReverseString
{
    static string ReverseString(string input)
    {
        Stack<char> stack = new Stack<char>();

        // Push từng ký tự vào stack
        foreach (char ch in input)
        {
            stack.Push(ch);
        }

        // Pop ra → thứ tự đảo ngược (LIFO)
        char[] reversed = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            reversed[i] = stack.Pop();
        }

        return new string(reversed);
    }

    static bool IsPalindrome(string input)
    {
        // Chuyển thành chữ thường, bỏ khoảng trắng
        string cleaned = input.ToLower().Replace(" ", "");
        return cleaned == ReverseString(cleaned);
    }

    static void Main()
    {
        Console.WriteLine("=== BÀI 1: ĐẢO NGƯỢC CHUỖI ===\n");

        // Test ReverseString
        string[] tests = { "Hello", "12345", "C# is fun", "a" };
        foreach (string s in tests)
        {
            Console.WriteLine($"  \"{s}\" → \"{ReverseString(s)}\"");
        }

        // Test IsPalindrome
        Console.WriteLine("\n--- Kiểm tra Palindrome ---");
        string[] palindromeTests = { "racecar", "hello", "madam", "A Santa at NASA", "level" };
        foreach (string s in palindromeTests)
        {
            Console.WriteLine($"  \"{s}\" → Palindrome: {IsPalindrome(s)}");
        }
    }
}
```

---

## Bài 2: Hệ thống xử lý tin nhắn (Queue) ⭐⭐

> **Mục tiêu**: Xây dựng hệ thống message queue đơn giản.

### Yêu cầu:
1. Tạo class `Message` với: `Sender`, `Content`, `Timestamp`, `Priority` (enum: Low, Normal, High)
2. Tạo class `MessageProcessor` với:
   - `Queue<Message>` để lưu tin nhắn
   - Method `Send(sender, content, priority)` — thêm tin nhắn
   - Method `ProcessNext()` — xử lý tin nhắn tiếp theo
   - Method `ProcessAll()` — xử lý tất cả
   - Method `ShowPending()` — hiển thị tin nhắn đang chờ
   - Property `PendingCount` — số tin nhắn chờ xử lý
3. Tin nhắn được xử lý theo thứ tự FIFO (ai gửi trước xử lý trước)

### Input/Output mẫu:
```
📤 Gửi: [Minh] "Xin chào!" (Normal)
📤 Gửi: [Lan] "Khẩn cấp!!!" (High)
📤 Gửi: [Hùng] "OK nhé" (Low)

Đang chờ: 3 tin nhắn

📨 Xử lý: [Minh] "Xin chào!" - Normal - 08:30:00
📨 Xử lý: [Lan] "Khẩn cấp!!!" - High - 08:30:01
📨 Xử lý: [Hùng] "OK nhé" - Low - 08:30:02

✅ Đã xử lý tất cả!
```

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

enum MessagePriority { Low, Normal, High }

class Message
{
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public MessagePriority Priority { get; set; }

    public Message(string sender, string content, MessagePriority priority)
    {
        Sender = sender;
        Content = content;
        Priority = priority;
        Timestamp = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[{Sender}] \"{Content}\" - {Priority} - {Timestamp:HH:mm:ss}";
    }
}

class MessageProcessor
{
    private Queue<Message> _messageQueue = new Queue<Message>();
    private int _processedCount = 0;

    public int PendingCount => _messageQueue.Count;

    public void Send(string sender, string content, 
                     MessagePriority priority = MessagePriority.Normal)
    {
        Message msg = new Message(sender, content, priority);
        _messageQueue.Enqueue(msg);
        Console.WriteLine($"  📤 Gửi: {msg}");
    }

    public void ProcessNext()
    {
        if (_messageQueue.TryDequeue(out Message? msg))
        {
            _processedCount++;
            Console.WriteLine($"  📨 Xử lý #{_processedCount}: {msg}");
        }
        else
        {
            Console.WriteLine("  📭 Không có tin nhắn nào!");
        }
    }

    public void ProcessAll()
    {
        Console.WriteLine($"\n  🔄 Xử lý tất cả ({_messageQueue.Count} tin nhắn)...");
        while (_messageQueue.Count > 0)
        {
            ProcessNext();
        }
        Console.WriteLine("  ✅ Đã xử lý tất cả!");
    }

    public void ShowPending()
    {
        Console.WriteLine($"\n  📋 Tin nhắn đang chờ ({PendingCount}):");
        if (PendingCount == 0)
        {
            Console.WriteLine("     (trống)");
            return;
        }
        int i = 1;
        foreach (Message msg in _messageQueue)
        {
            Console.WriteLine($"     {i}. {msg}");
            i++;
        }
    }
}

class Exercise2_MessageQueue
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 2: MESSAGE QUEUE ===\n");

        MessageProcessor processor = new MessageProcessor();

        processor.Send("Minh", "Xin chào!");
        processor.Send("Lan", "Khẩn cấp!!!", MessagePriority.High);
        processor.Send("Hùng", "OK nhé", MessagePriority.Low);
        processor.Send("Mai", "Họp lúc 3h", MessagePriority.Normal);

        processor.ShowPending();

        Console.WriteLine("\n--- Xử lý 2 tin đầu ---");
        processor.ProcessNext();
        processor.ProcessNext();

        Console.WriteLine($"\n  Còn {processor.PendingCount} tin nhắn chờ");

        processor.ProcessAll();
    }
}
```

---

## Bài 3: Calculator với Stack (Postfix) ⭐⭐⭐

> **Mục tiêu**: Tính biểu thức dạng Postfix (hậu tố) bằng Stack.

### Kiến thức:
```
Infix (thông thường):  3 + 4 * 2
Postfix (hậu tố):     3 4 2 * +

Cách tính Postfix bằng Stack:
  Đọc 3 → Push 3           Stack: [3]
  Đọc 4 → Push 4           Stack: [3, 4]
  Đọc 2 → Push 2           Stack: [3, 4, 2]
  Đọc * → Pop 2, Pop 4     
         → 4*2=8 → Push 8  Stack: [3, 8]
  Đọc + → Pop 8, Pop 3     
         → 3+8=11 → Push   Stack: [11]
  Kết quả = 11 ✅
```

### Yêu cầu:
1. Viết method `double EvaluatePostfix(string expression)`
2. Hỗ trợ: `+`, `-`, `*`, `/`
3. Các token cách nhau bằng khoảng trắng
4. Xử lý lỗi: chia cho 0, biểu thức không hợp lệ

### Input/Output mẫu:
```
"3 4 +"         → 7
"3 4 2 * +"     → 11
"5 1 2 + 4 * + 3 -"  → 14
"10 0 /"        → Lỗi: Chia cho 0!
```

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

class PostfixCalculator
{
    public static double EvaluatePostfix(string expression)
    {
        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine($"  Biểu thức: \"{expression}\"");

        foreach (string token in tokens)
        {
            // Nếu là số → Push
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
                Console.WriteLine($"    Push {number,-6} | Stack: [{string.Join(", ", stack)}]");
            }
            // Nếu là toán tử → Pop 2 số, tính, Push kết quả
            else if ("+-*/".Contains(token))
            {
                if (stack.Count < 2)
                {
                    throw new InvalidOperationException(
                        "Biểu thức không hợp lệ: thiếu toán hạng!");
                }

                double b = stack.Pop(); // Số thứ 2 (pop trước)
                double a = stack.Pop(); // Số thứ 1

                double result = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b != 0 ? a / b 
                           : throw new DivideByZeroException("Chia cho 0!"),
                    _ => throw new InvalidOperationException($"Toán tử không hợp lệ: {token}")
                };

                stack.Push(result);
                Console.WriteLine(
                    $"    {a} {token} {b} = {result,-4} | Stack: [{string.Join(", ", stack)}]");
            }
            else
            {
                throw new InvalidOperationException($"Token không hợp lệ: '{token}'");
            }
        }

        if (stack.Count != 1)
        {
            throw new InvalidOperationException(
                "Biểu thức không hợp lệ: thừa toán hạng!");
        }

        return stack.Pop();
    }
}

class Exercise3_PostfixCalculator
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 3: POSTFIX CALCULATOR ===\n");

        string[] expressions = {
            "3 4 +",                // 7
            "3 4 2 * +",           // 11
            "5 1 2 + 4 * + 3 -",   // 14
            "15 7 1 1 + - / 3 * 2 1 1 + + -"  // 5
        };

        foreach (string expr in expressions)
        {
            try
            {
                double result = PostfixCalculator.EvaluatePostfix(expr);
                Console.WriteLine($"  ✅ Kết quả = {result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Lỗi: {ex.Message}\n");
            }
        }

        // Test lỗi
        Console.WriteLine("--- Test lỗi ---");
        try
        {
            PostfixCalculator.EvaluatePostfix("10 0 /");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ❌ {ex.Message}\n");
        }
    }
}
```

---

## Bài 4: Hàng đợi vòng tròn (Circular Queue) ⭐⭐⭐

> **Mục tiêu**: Tự implement một Circular Queue với kích thước cố định.

### Yêu cầu:
1. Tạo class `CircularQueue<T>` với kích thước tối đa (maxSize)
2. Implement các method:
   - `Enqueue(item)` — thêm phần tử (trả `false` nếu đầy)
   - `Dequeue()` — lấy phần tử (throw exception nếu rỗng)
   - `TryDequeue(out T)` — lấy an toàn
   - `Peek()` — xem phần tử đầu
   - `IsFull` — kiểm tra đầy
   - `IsEmpty` — kiểm tra rỗng
   - `Count` — số phần tử hiện tại
   - `Display()` — hiển thị trạng thái
3. Dùng mảng nội bộ và 2 con trỏ `front`, `rear`

### Gợi ý cấu trúc:
```
  Circular Queue (maxSize = 5):
  
  ┌───┬───┬───┬───┬───┐
  │ A │ B │ C │   │   │
  └───┴───┴───┴───┴───┘
    ↑           ↑
  front       rear
  
  Enqueue "D":
  ┌───┬───┬───┬───┬───┐
  │ A │ B │ C │ D │   │
  └───┴───┴───┴───┴───┘
    ↑               ↑
  front           rear
  
  Dequeue → "A":
  ┌───┬───┬───┬───┬───┐
  │   │ B │ C │ D │   │
  └───┴───┴───┴───┴───┘
        ↑           ↑
      front       rear
  
  Enqueue "E", "F" (vòng lại):
  ┌───┬───┬───┬───┬───┐
  │ F │ B │ C │ D │ E │
  └───┴───┴───┴───┴───┘
    ↑   ↑
  rear front  ← rear vòng lại đầu mảng!
```

### ✅ Đáp án tham khảo:

```csharp
using System;

class CircularQueue<T>
{
    private T[] _items;
    private int _front = 0;
    private int _rear = -1;
    private int _count = 0;
    private int _maxSize;

    public CircularQueue(int maxSize)
    {
        _maxSize = maxSize;
        _items = new T[maxSize];
    }

    public int Count => _count;
    public bool IsFull => _count == _maxSize;
    public bool IsEmpty => _count == 0;
    public int Capacity => _maxSize;

    public bool Enqueue(T item)
    {
        if (IsFull)
        {
            Console.WriteLine($"  ⚠️ Queue đầy! Không thể thêm.");
            return false;
        }

        // Di chuyển rear (vòng lại nếu cần)
        _rear = (_rear + 1) % _maxSize;
        _items[_rear] = item;
        _count++;
        return true;
    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue rỗng!");

        T item = _items[_front];
        _items[_front] = default!;
        _front = (_front + 1) % _maxSize;
        _count--;
        return item;
    }

    public bool TryDequeue(out T? item)
    {
        if (IsEmpty)
        {
            item = default;
            return false;
        }
        item = Dequeue();
        return true;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue rỗng!");
        return _items[_front];
    }

    public void Display()
    {
        Console.Write("  Queue: [");
        if (!IsEmpty)
        {
            for (int i = 0; i < _count; i++)
            {
                int index = (_front + i) % _maxSize;
                if (i > 0) Console.Write(", ");
                Console.Write(_items[index]);
            }
        }
        Console.WriteLine($"] (Count: {_count}/{_maxSize})");

        // Hiển thị mảng nội bộ
        Console.Write("  Array: [");
        for (int i = 0; i < _maxSize; i++)
        {
            if (i > 0) Console.Write(", ");
            string val = _items[i]?.ToString() ?? "_";
            Console.Write(val);
        }
        Console.WriteLine($"]  front={_front}, rear={_rear}");
    }
}

class Exercise4_CircularQueue
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 4: CIRCULAR QUEUE ===\n");

        CircularQueue<string> queue = new CircularQueue<string>(5);

        // Thêm phần tử
        queue.Enqueue("A");
        queue.Enqueue("B");
        queue.Enqueue("C");
        queue.Display();

        // Dequeue
        Console.WriteLine($"\n  Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"  Dequeue: {queue.Dequeue()}");
        queue.Display();

        // Thêm tiếp (vòng lại)
        Console.WriteLine();
        queue.Enqueue("D");
        queue.Enqueue("E");
        queue.Enqueue("F");
        queue.Enqueue("G");
        queue.Display();

        // Thử thêm khi đầy
        Console.WriteLine();
        queue.Enqueue("H"); // ⚠️ Đầy!

        // Dequeue hết
        Console.WriteLine("\n--- Dequeue tất cả ---");
        while (!queue.IsEmpty)
        {
            Console.WriteLine($"  Dequeue: {queue.Dequeue()}");
        }
        queue.Display();
    }
}
```

---

## Bài 5: Hệ thống quản lý Task với Stack + Queue ⭐⭐⭐

> **Mục tiêu**: Kết hợp Stack và Queue để quản lý tasks.

### Yêu cầu:
1. Tạo class `TaskItem` với: `Name`, `CreatedAt`, `CompletedAt`, `Status` (enum: Pending, InProgress, Completed)
2. Tạo class `TaskManager` với:
   - `Queue<TaskItem>` cho danh sách task chờ xử lý (FIFO)
   - `Stack<TaskItem>` cho danh sách task đã hoàn thành (xem gần nhất)
   - `Stack<TaskItem>` cho Undo (khôi phục task đã complete)
3. Implement các chức năng:
   - `AddTask(name)` — thêm task vào hàng đợi
   - `StartNext()` — bắt đầu task tiếp theo (dequeue → in progress)
   - `Complete(taskItem)` — hoàn thành task → push vào completed stack
   - `UndoComplete()` — hoàn tác task vừa complete → đưa lại queue
   - `ShowStatus()` — hiển thị tất cả
4. Chạy demo với ít nhất 5 tasks

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

enum TaskStatus { Pending, InProgress, Completed }

class TaskItem
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TaskStatus Status { get; set; }

    public TaskItem(string name)
    {
        Id = _nextId++;
        Name = name;
        CreatedAt = DateTime.Now;
        Status = TaskStatus.Pending;
    }

    public override string ToString()
    {
        string status = Status switch
        {
            TaskStatus.Pending => "⏳",
            TaskStatus.InProgress => "🔄",
            TaskStatus.Completed => "✅",
            _ => "❓"
        };
        return $"{status} #{Id} - {Name}";
    }
}

class TaskManager
{
    private Queue<TaskItem> _pendingQueue = new Queue<TaskItem>();
    private Stack<TaskItem> _completedStack = new Stack<TaskItem>();
    private Stack<TaskItem> _undoStack = new Stack<TaskItem>();
    private TaskItem? _currentTask = null;

    public void AddTask(string name)
    {
        TaskItem task = new TaskItem(name);
        _pendingQueue.Enqueue(task);
        Console.WriteLine($"  📥 Thêm task: {task}");
    }

    public void StartNext()
    {
        if (_currentTask != null)
        {
            Console.WriteLine($"  ⚠️ Đang có task: {_currentTask}. Hoàn thành trước!");
            return;
        }

        if (_pendingQueue.TryDequeue(out TaskItem? task))
        {
            task.Status = TaskStatus.InProgress;
            _currentTask = task;
            Console.WriteLine($"  ▶️ Bắt đầu: {task}");
        }
        else
        {
            Console.WriteLine("  📭 Không có task nào trong hàng đợi!");
        }
    }

    public void CompleteCurrent()
    {
        if (_currentTask == null)
        {
            Console.WriteLine("  ⚠️ Không có task đang thực hiện!");
            return;
        }

        _currentTask.Status = TaskStatus.Completed;
        _currentTask.CompletedAt = DateTime.Now;
        _completedStack.Push(_currentTask);
        Console.WriteLine($"  ✅ Hoàn thành: {_currentTask}");
        _currentTask = null;
    }

    public void UndoComplete()
    {
        if (_completedStack.Count == 0)
        {
            Console.WriteLine("  ⚠️ Không có task nào để undo!");
            return;
        }

        TaskItem task = _completedStack.Pop();
        task.Status = TaskStatus.Pending;
        task.CompletedAt = null;

        // Đưa lại vào đầu queue (dùng trick tạo queue mới)
        Queue<TaskItem> newQueue = new Queue<TaskItem>();
        newQueue.Enqueue(task);
        while (_pendingQueue.Count > 0)
        {
            newQueue.Enqueue(_pendingQueue.Dequeue());
        }
        _pendingQueue = newQueue;

        Console.WriteLine($"  ↩️ Undo: {task} → đưa lại hàng đợi");
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n  ╔══════════════════════════════════╗");
        Console.WriteLine("  ║       TASK MANAGER STATUS        ║");
        Console.WriteLine("  ╠══════════════════════════════════╣");

        // Current
        Console.WriteLine($"  ║ Đang làm: {_currentTask?.ToString() ?? "(không có)"}");

        // Pending
        Console.WriteLine($"  ║ Hàng đợi ({_pendingQueue.Count}):");
        foreach (var task in _pendingQueue)
        {
            Console.WriteLine($"  ║   {task}");
        }

        // Completed
        Console.WriteLine($"  ║ Đã xong ({_completedStack.Count}):");
        foreach (var task in _completedStack)
        {
            Console.WriteLine($"  ║   {task}");
        }

        Console.WriteLine("  ╚══════════════════════════════════╝\n");
    }
}

class Exercise5_TaskManager
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 5: TASK MANAGER ===\n");

        TaskManager manager = new TaskManager();

        // Thêm tasks
        manager.AddTask("Thiết kế database");
        manager.AddTask("Code backend API");
        manager.AddTask("Code frontend UI");
        manager.AddTask("Viết unit test");
        manager.AddTask("Deploy lên server");

        manager.ShowStatus();

        // Xử lý tasks
        Console.WriteLine("--- Xử lý tasks ---");
        manager.StartNext();         // Thiết kế database
        manager.CompleteCurrent();   // ✅

        manager.StartNext();         // Code backend API
        manager.CompleteCurrent();   // ✅

        manager.ShowStatus();

        // Undo
        Console.WriteLine("--- Undo task cuối ---");
        manager.UndoComplete();      // Code backend API → lại queue
        manager.ShowStatus();

        // Tiếp tục xử lý
        Console.WriteLine("--- Tiếp tục ---");
        manager.StartNext();
        manager.CompleteCurrent();
        manager.StartNext();
        manager.CompleteCurrent();

        manager.ShowStatus();
    }
}
```

---

## 📊 Tổng kết bài tập

| Bài | Chủ đề | Cấu trúc dữ liệu | Độ khó |
|-----|--------|--------------------|---------| 
| 1 | Đảo chuỗi + Palindrome | Stack | ⭐ |
| 2 | Message Queue | Queue | ⭐⭐ |
| 3 | Postfix Calculator | Stack | ⭐⭐⭐ |
| 4 | Circular Queue | Tự implement | ⭐⭐⭐ |
| 5 | Task Manager | Stack + Queue | ⭐⭐⭐ |

> 💡 **Mẹo**: Khi gặp bài toán, hãy tự hỏi: "Cần xử lý cái nào trước — cái mới nhất (Stack) hay cái cũ nhất (Queue)?"
