# 💻 Bài 27: Stack\<T> & Queue\<T> — Ví dụ Thực Hành

---

## Ví dụ 1: Stack — Undo/Redo Text Editor 📝

> **Mục tiêu**: Dùng 2 Stack để triển khai chức năng Undo/Redo giống các text editor.

```
  Trạng thái ban đầu:
  
  Undo Stack: []          Redo Stack: []
  Text: ""
  
  Gõ "Hello":
  Undo Stack: [""]        Redo Stack: []    ← clear redo
  Text: "Hello"
  
  Gõ " World":
  Undo Stack: ["", "Hello"]   Redo Stack: []
  Text: "Hello World"
  
  Undo:
  Undo Stack: [""]        Redo Stack: ["Hello World"]
  Text: "Hello"
  
  Redo:
  Undo Stack: ["", "Hello"]   Redo Stack: []
  Text: "Hello World"
```

```csharp
using System;
using System.Collections.Generic;

namespace Lesson27_StackQueue
{
    // ═══════════════════════════════════════════
    // Ví dụ 1: Undo/Redo Text Editor
    // ═══════════════════════════════════════════
    class TextEditor
    {
        private string _currentText = "";
        private Stack<string> _undoStack = new Stack<string>();
        private Stack<string> _redoStack = new Stack<string>();

        public string CurrentText => _currentText;

        // Gõ text mới
        public void Type(string newText)
        {
            // Lưu trạng thái hiện tại vào Undo stack
            _undoStack.Push(_currentText);

            // Xóa Redo stack (không thể redo sau khi gõ mới)
            _redoStack.Clear();

            _currentText += newText;
            Console.WriteLine($"  Gõ: \"{newText}\" → Text = \"{_currentText}\"");
        }

        // Undo — hoàn tác
        public void Undo()
        {
            if (_undoStack.Count == 0)
            {
                Console.WriteLine("  ⚠️ Không có gì để Undo!");
                return;
            }

            // Lưu trạng thái hiện tại vào Redo stack
            _redoStack.Push(_currentText);

            // Khôi phục trạng thái trước đó
            _currentText = _undoStack.Pop();
            Console.WriteLine($"  ↩️ Undo → Text = \"{_currentText}\"");
        }

        // Redo — làm lại
        public void Redo()
        {
            if (_redoStack.Count == 0)
            {
                Console.WriteLine("  ⚠️ Không có gì để Redo!");
                return;
            }

            // Lưu trạng thái hiện tại vào Undo stack
            _undoStack.Push(_currentText);

            // Khôi phục trạng thái redo
            _currentText = _redoStack.Pop();
            Console.WriteLine($"  ↪️ Redo → Text = \"{_currentText}\"");
        }

        // Hiển thị trạng thái
        public void ShowStatus()
        {
            Console.WriteLine($"  📄 Text: \"{_currentText}\"");
            Console.WriteLine($"     Undo stack: {_undoStack.Count} bước");
            Console.WriteLine($"     Redo stack: {_redoStack.Count} bước");
            Console.WriteLine();
        }
    }

    class Example1_UndoRedo
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 1: UNDO/REDO TEXT EDITOR ═══\n");

            TextEditor editor = new TextEditor();

            // Gõ text
            editor.Type("Hello");
            editor.ShowStatus();

            editor.Type(" World");
            editor.ShowStatus();

            editor.Type("!!!");
            editor.ShowStatus();

            // Undo 2 lần
            Console.WriteLine("--- Undo 2 lần ---");
            editor.Undo();
            editor.Undo();
            editor.ShowStatus();

            // Redo 1 lần
            Console.WriteLine("--- Redo 1 lần ---");
            editor.Redo();
            editor.ShowStatus();

            // Gõ mới → Redo bị xóa
            Console.WriteLine("--- Gõ mới (Redo sẽ bị xóa) ---");
            editor.Type(" C#");
            editor.ShowStatus();

            // Thử Redo → không có gì
            editor.Redo();
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 1: UNDO/REDO TEXT EDITOR ═══

  Gõ: "Hello" → Text = "Hello"
  📄 Text: "Hello"
     Undo stack: 1 bước
     Redo stack: 0 bước

  Gõ: " World" → Text = "Hello World"
  📄 Text: "Hello World"
     Undo stack: 2 bước
     Redo stack: 0 bước

  Gõ: "!!!" → Text = "Hello World!!!"
  📄 Text: "Hello World!!!"
     Undo stack: 3 bước
     Redo stack: 0 bước

--- Undo 2 lần ---
  ↩️ Undo → Text = "Hello World"
  ↩️ Undo → Text = "Hello"
  📄 Text: "Hello"
     Undo stack: 1 bước
     Redo stack: 2 bước

--- Redo 1 lần ---
  ↪️ Redo → Text = "Hello World"
  📄 Text: "Hello World"
     Undo stack: 2 bước
     Redo stack: 1 bước

--- Gõ mới (Redo sẽ bị xóa) ---
  Gõ: " C#" → Text = "Hello World C#"
  📄 Text: "Hello World C#"
     Undo stack: 3 bước
     Redo stack: 0 bước

  ⚠️ Không có gì để Redo!
```

---

## Ví dụ 2: Stack — Bracket/Parenthesis Validator ✅❌

> **Mục tiêu**: Dùng Stack để kiểm tra chuỗi ngoặc có hợp lệ không.

```
  Hợp lệ:    ({[]})    ✅
  Không:      ({[}])    ❌  — '}' không khớp với '['
  Không:      ((()      ❌  — Thiếu ngoặc đóng
  Không:      ())       ❌  — Thừa ngoặc đóng
```

```csharp
using System;
using System.Collections.Generic;

namespace Lesson27_StackQueue
{
    // ═══════════════════════════════════════════
    // Ví dụ 2: Bracket Validator
    // ═══════════════════════════════════════════
    class BracketValidator
    {
        /// <summary>
        /// Kiểm tra chuỗi ngoặc có hợp lệ không
        /// </summary>
        public static (bool isValid, string message) Validate(string input)
        {
            Stack<char> stack = new Stack<char>();

            // Mapping: ngoặc đóng → ngoặc mở tương ứng
            Dictionary<char, char> pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];

                // Nếu là ngoặc MỞ → Push vào stack
                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                }
                // Nếu là ngoặc ĐÓNG → Pop và kiểm tra
                else if (ch == ')' || ch == ']' || ch == '}')
                {
                    // Stack rỗng → thừa ngoặc đóng
                    if (stack.Count == 0)
                    {
                        return (false, $"Vị trí {i}: Thừa ngoặc đóng '{ch}'");
                    }

                    char top = stack.Pop();

                    // Kiểm tra ngoặc đóng có khớp với ngoặc mở không
                    if (top != pairs[ch])
                    {
                        return (false, 
                            $"Vị trí {i}: '{ch}' không khớp với '{top}'");
                    }
                }
                // Bỏ qua các ký tự khác (chữ, số, khoảng trắng...)
            }

            // Sau khi duyệt xong, stack phải rỗng
            if (stack.Count > 0)
            {
                return (false, 
                    $"Thiếu {stack.Count} ngoặc đóng. Ngoặc mở chưa đóng: '{stack.Peek()}'");
            }

            return (true, "Hợp lệ ✅");
        }

        /// <summary>
        /// Hiển thị quá trình validate từng bước
        /// </summary>
        public static void ValidateWithSteps(string input)
        {
            Console.WriteLine($"  Kiểm tra: \"{input}\"");
            Stack<char> stack = new Stack<char>();
            Dictionary<char, char> pairs = new()
            {
                { ')', '(' }, { ']', '[' }, { '}', '{' }
            };

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];

                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                    Console.WriteLine(
                        $"    Bước {i + 1}: '{ch}' → Push  " +
                        $"| Stack: [{string.Join(", ", stack)}]");
                }
                else if (ch == ')' || ch == ']' || ch == '}')
                {
                    if (stack.Count > 0 && stack.Peek() == pairs[ch])
                    {
                        char popped = stack.Pop();
                        Console.WriteLine(
                            $"    Bước {i + 1}: '{ch}' → Pop '{popped}' ✅ " +
                            $"| Stack: [{string.Join(", ", stack)}]");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"    Bước {i + 1}: '{ch}' → KHÔNG KHỚP ❌");
                        return;
                    }
                }
            }

            string result = stack.Count == 0 ? "HỢP LỆ ✅" : "THIẾU NGOẶC ĐÓNG ❌";
            Console.WriteLine($"    Kết quả: {result}\n");
        }
    }

    class Example2_BracketValidator
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 2: BRACKET VALIDATOR ═══\n");

            // Test các trường hợp
            string[] testCases = {
                "({[]})",           // Hợp lệ
                "(a + b) * [c - d]", // Hợp lệ (bỏ qua ký tự khác)
                "({[}])",           // Không khớp
                "(((",              // Thiếu đóng
                "())",              // Thừa đóng
                "",                 // Rỗng → hợp lệ
                "{[()()]}"          // Hợp lệ phức tạp
            };

            foreach (string test in testCases)
            {
                var (isValid, message) = BracketValidator.Validate(test);
                string icon = isValid ? "✅" : "❌";
                Console.WriteLine($"  {icon} \"{test}\" → {message}");
            }

            // Hiển thị chi tiết
            Console.WriteLine("\n--- Chi tiết từng bước ---\n");
            BracketValidator.ValidateWithSteps("({[]})");
            BracketValidator.ValidateWithSteps("({[}])");
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 2: BRACKET VALIDATOR ═══

  ✅ "({[]})" → Hợp lệ ✅
  ✅ "(a + b) * [c - d]" → Hợp lệ ✅
  ❌ "({[}])" → Vị trí 3: '}' không khớp với '['
  ❌ "(((" → Thiếu 3 ngoặc đóng. Ngoặc mở chưa đóng: '('
  ❌ "())" → Vị trí 2: Thừa ngoặc đóng ')'
  ✅ "" → Hợp lệ ✅
  ✅ "{[()()]}" → Hợp lệ ✅

--- Chi tiết từng bước ---

  Kiểm tra: "({[]})"
    Bước 1: '(' → Push  | Stack: [(]
    Bước 2: '{' → Push  | Stack: [{, (]
    Bước 3: '[' → Push  | Stack: [[, {, (]
    Bước 4: ']' → Pop '[' ✅ | Stack: [{, (]
    Bước 5: '}' → Pop '{' ✅ | Stack: [(]
    Bước 6: ')' → Pop '(' ✅ | Stack: []
    Kết quả: HỢP LỆ ✅

  Kiểm tra: "({[}])"
    Bước 1: '(' → Push  | Stack: [(]
    Bước 2: '{' → Push  | Stack: [{, (]
    Bước 3: '[' → Push  | Stack: [[, {, (]
    Bước 4: '}' → KHÔNG KHỚP ❌
```

---

## Ví dụ 3: Queue — Print Job Manager 🖨️

> **Mục tiêu**: Quản lý hàng đợi in tài liệu — ai gửi trước in trước (FIFO).

```csharp
using System;
using System.Collections.Generic;

namespace Lesson27_StackQueue
{
    // ═══════════════════════════════════════════
    // Ví dụ 3: Print Job Manager
    // ═══════════════════════════════════════════
    class PrintJob
    {
        public string DocumentName { get; set; }
        public int Pages { get; set; }
        public string User { get; set; }
        public DateTime SubmittedAt { get; set; }

        public PrintJob(string docName, int pages, string user)
        {
            DocumentName = docName;
            Pages = pages;
            User = user;
            SubmittedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"\"{DocumentName}\" ({Pages} trang) - bởi {User}";
        }
    }

    class PrintManager
    {
        private Queue<PrintJob> _printQueue = new Queue<PrintJob>();
        private int _totalPrinted = 0;

        // Thêm lệnh in vào hàng đợi
        public void AddJob(string docName, int pages, string user)
        {
            PrintJob job = new PrintJob(docName, pages, user);
            _printQueue.Enqueue(job);
            Console.WriteLine($"  📥 Thêm: {job}");
            Console.WriteLine($"     Vị trí trong hàng đợi: #{_printQueue.Count}");
        }

        // In tài liệu tiếp theo
        public void PrintNext()
        {
            if (_printQueue.TryDequeue(out PrintJob? job))
            {
                _totalPrinted++;
                Console.WriteLine($"  🖨️ Đang in #{_totalPrinted}: {job}");
                Console.WriteLine($"     Còn {_printQueue.Count} tài liệu trong hàng đợi");
            }
            else
            {
                Console.WriteLine("  ✅ Hàng đợi trống — không có gì để in!");
            }
        }

        // Xem tài liệu sắp in (không xóa)
        public void PeekNext()
        {
            if (_printQueue.TryPeek(out PrintJob? job))
            {
                Console.WriteLine($"  👀 Sắp in tiếp: {job}");
            }
            else
            {
                Console.WriteLine("  📭 Hàng đợi trống");
            }
        }

        // In tất cả
        public void PrintAll()
        {
            Console.WriteLine($"\n  🖨️ In tất cả ({_printQueue.Count} tài liệu)...");
            while (_printQueue.Count > 0)
            {
                PrintNext();
            }
            Console.WriteLine("  ✅ Hoàn tất!");
        }

        // Hiển thị hàng đợi
        public void ShowQueue()
        {
            Console.WriteLine($"\n  📋 Hàng đợi hiện tại ({_printQueue.Count} tài liệu):");
            if (_printQueue.Count == 0)
            {
                Console.WriteLine("     (trống)");
                return;
            }

            int pos = 1;
            foreach (PrintJob job in _printQueue)
            {
                Console.WriteLine($"     {pos}. {job}");
                pos++;
            }
            Console.WriteLine();
        }
    }

    class Example3_PrintManager
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 3: PRINT JOB MANAGER ═══\n");

            PrintManager printer = new PrintManager();

            // Thêm các lệnh in
            printer.AddJob("Báo cáo Q1.pdf", 15, "Minh");
            printer.AddJob("Hợp đồng.docx", 5, "Lan");
            printer.AddJob("Slide thuyết trình.pptx", 30, "Hùng");
            printer.AddJob("CV Ứng viên.pdf", 2, "Mai");

            // Xem hàng đợi
            printer.ShowQueue();

            // Xem cái sắp in
            printer.PeekNext();

            // In 2 tài liệu đầu
            Console.WriteLine("\n--- In 2 tài liệu đầu ---");
            printer.PrintNext();
            printer.PrintNext();

            // Thêm tài liệu mới (vào cuối hàng)
            Console.WriteLine("\n--- Thêm tài liệu mới ---");
            printer.AddJob("Hóa đơn.pdf", 1, "Tuấn");

            // Xem lại hàng đợi
            printer.ShowQueue();

            // In hết
            printer.PrintAll();

            // Thử in khi rỗng
            Console.WriteLine();
            printer.PrintNext();
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 3: PRINT JOB MANAGER ═══

  📥 Thêm: "Báo cáo Q1.pdf" (15 trang) - bởi Minh
     Vị trí trong hàng đợi: #1
  📥 Thêm: "Hợp đồng.docx" (5 trang) - bởi Lan
     Vị trí trong hàng đợi: #2
  📥 Thêm: "Slide thuyết trình.pptx" (30 trang) - bởi Hùng
     Vị trí trong hàng đợi: #3
  📥 Thêm: "CV Ứng viên.pdf" (2 trang) - bởi Mai
     Vị trí trong hàng đợi: #4

  📋 Hàng đợi hiện tại (4 tài liệu):
     1. "Báo cáo Q1.pdf" (15 trang) - bởi Minh
     2. "Hợp đồng.docx" (5 trang) - bởi Lan
     3. "Slide thuyết trình.pptx" (30 trang) - bởi Hùng
     4. "CV Ứng viên.pdf" (2 trang) - bởi Mai

  👀 Sắp in tiếp: "Báo cáo Q1.pdf" (15 trang) - bởi Minh

--- In 2 tài liệu đầu ---
  🖨️ Đang in #1: "Báo cáo Q1.pdf" (15 trang) - bởi Minh
     Còn 3 tài liệu trong hàng đợi
  🖨️ Đang in #2: "Hợp đồng.docx" (5 trang) - bởi Lan
     Còn 2 tài liệu trong hàng đợi

--- Thêm tài liệu mới ---
  📥 Thêm: "Hóa đơn.pdf" (1 trang) - bởi Tuấn
     Vị trí trong hàng đợi: #3

  📋 Hàng đợi hiện tại (3 tài liệu):
     1. "Slide thuyết trình.pptx" (30 trang) - bởi Hùng
     2. "CV Ứng viên.pdf" (2 trang) - bởi Mai
     3. "Hóa đơn.pdf" (1 trang) - bởi Tuấn

  🖨️ In tất cả (3 tài liệu)...
  🖨️ Đang in #3: "Slide thuyết trình.pptx" (30 trang) - bởi Hùng
     Còn 2 tài liệu trong hàng đợi
  🖨️ Đang in #4: "CV Ứng viên.pdf" (2 trang) - bởi Mai
     Còn 1 tài liệu trong hàng đợi
  🖨️ Đang in #5: "Hóa đơn.pdf" (1 trang) - bởi Tuấn
     Còn 0 tài liệu trong hàng đợi
  ✅ Hoàn tất!

  ✅ Hàng đợi trống — không có gì để in!
```

---

## Ví dụ 4: Queue — Restaurant Order System 🍜

> **Mục tiêu**: Hệ thống quản lý đơn hàng nhà hàng với Queue + PriorityQueue.

```csharp
using System;
using System.Collections.Generic;

namespace Lesson27_StackQueue
{
    // ═══════════════════════════════════════════
    // Ví dụ 4: Restaurant Order System
    // ═══════════════════════════════════════════
    class FoodOrder
    {
        private static int _nextId = 1;

        public int OrderId { get; }
        public string CustomerName { get; set; }
        public List<string> Items { get; set; }
        public bool IsVip { get; set; }
        public DateTime OrderTime { get; set; }

        public FoodOrder(string customer, List<string> items, bool isVip = false)
        {
            OrderId = _nextId++;
            CustomerName = customer;
            Items = items;
            IsVip = isVip;
            OrderTime = DateTime.Now;
        }

        public override string ToString()
        {
            string vipTag = IsVip ? " ⭐VIP" : "";
            return $"#{OrderId}{vipTag} - {CustomerName}: " +
                   $"{string.Join(", ", Items)}";
        }
    }

    class RestaurantOrderSystem
    {
        // Queue thường — FIFO
        private Queue<FoodOrder> _orderQueue = new Queue<FoodOrder>();

        // PriorityQueue — VIP ưu tiên hơn
        private PriorityQueue<FoodOrder, int> _priorityQueue = new();

        // Đã hoàn thành (dùng Stack để xem đơn gần nhất)
        private Stack<FoodOrder> _completedOrders = new Stack<FoodOrder>();

        // ---- Chế độ Queue thường (FIFO) ----

        public void PlaceOrder(string customer, List<string> items, 
                               bool isVip = false)
        {
            FoodOrder order = new FoodOrder(customer, items, isVip);
            _orderQueue.Enqueue(order);
            Console.WriteLine($"  📝 Đơn mới: {order}");
        }

        public void ProcessNextOrder()
        {
            if (_orderQueue.TryDequeue(out FoodOrder? order))
            {
                Console.WriteLine($"  🍳 Đang nấu: {order}");
                _completedOrders.Push(order); // Lưu vào completed
                Console.WriteLine($"  ✅ Hoàn thành: #{order.OrderId}");
            }
            else
            {
                Console.WriteLine("  📭 Không có đơn hàng nào!");
            }
        }

        // ---- Chế độ Priority Queue (VIP trước) ----

        public void PlaceOrderPriority(string customer, List<string> items,
                                       bool isVip = false)
        {
            FoodOrder order = new FoodOrder(customer, items, isVip);
            // VIP = priority 1 (cao), thường = priority 5 (thấp)
            int priority = isVip ? 1 : 5;
            _priorityQueue.Enqueue(order, priority);
            Console.WriteLine($"  📝 Đơn mới: {order} (priority: {priority})");
        }

        public void ProcessNextPriority()
        {
            if (_priorityQueue.TryDequeue(out FoodOrder? order, out int priority))
            {
                Console.WriteLine($"  🍳 Đang nấu (priority {priority}): {order}");
                _completedOrders.Push(order);
                Console.WriteLine($"  ✅ Hoàn thành: #{order.OrderId}");
            }
            else
            {
                Console.WriteLine("  📭 Không có đơn hàng nào!");
            }
        }

        // Xem đơn hoàn thành gần nhất
        public void ShowLastCompleted()
        {
            if (_completedOrders.TryPeek(out FoodOrder? last))
            {
                Console.WriteLine($"  📦 Đơn hoàn thành gần nhất: {last}");
            }
        }

        // Hiển thị trạng thái
        public void ShowStatus()
        {
            Console.WriteLine($"\n  📊 Trạng thái:");
            Console.WriteLine($"     Đang chờ (Queue): {_orderQueue.Count}");
            Console.WriteLine($"     Đang chờ (Priority): {_priorityQueue.Count}");
            Console.WriteLine($"     Đã hoàn thành: {_completedOrders.Count}\n");
        }
    }

    class Example4_RestaurantOrder
    {
        public static void Run()
        {
            Console.WriteLine("═══ VÍ DỤ 4: RESTAURANT ORDER SYSTEM ═══\n");

            RestaurantOrderSystem restaurant = new RestaurantOrderSystem();

            // === PHẦN 1: Queue thường (FIFO) ===
            Console.WriteLine("🔹 PHẦN 1: Queue thường (FIFO)\n");

            restaurant.PlaceOrder("Minh", 
                new List<string> { "Phở bò", "Trà đá" });
            restaurant.PlaceOrder("Lan", 
                new List<string> { "Bún chả", "Nước cam" });
            restaurant.PlaceOrder("Hùng", 
                new List<string> { "Cơm tấm", "Cà phê" });

            restaurant.ShowStatus();

            Console.WriteLine("--- Xử lý đơn hàng (FIFO) ---");
            restaurant.ProcessNextOrder();  // Minh trước (đến trước)
            restaurant.ProcessNextOrder();  // Lan tiếp theo
            restaurant.ShowLastCompleted();

            // === PHẦN 2: Priority Queue (VIP trước) ===
            Console.WriteLine("\n🔹 PHẦN 2: Priority Queue (VIP ưu tiên)\n");

            restaurant.PlaceOrderPriority("Khách thường 1", 
                new List<string> { "Mì xào" });
            restaurant.PlaceOrderPriority("Khách thường 2", 
                new List<string> { "Cơm rang" });
            restaurant.PlaceOrderPriority("VIP Tuấn", 
                new List<string> { "Bò Wagyu", "Rượu vang" }, isVip: true);
            restaurant.PlaceOrderPriority("Khách thường 3", 
                new List<string> { "Phở gà" });
            restaurant.PlaceOrderPriority("VIP Mai", 
                new List<string> { "Sushi set", "Sake" }, isVip: true);

            Console.WriteLine("\n--- Xử lý đơn hàng (theo Priority) ---");
            restaurant.ProcessNextPriority();  // VIP trước!
            restaurant.ProcessNextPriority();  // VIP tiếp
            restaurant.ProcessNextPriority();  // Rồi mới khách thường
            restaurant.ProcessNextPriority();
            restaurant.ProcessNextPriority();

            restaurant.ShowStatus();
        }
    }
}
```

**Output:**
```
═══ VÍ DỤ 4: RESTAURANT ORDER SYSTEM ═══

🔹 PHẦN 1: Queue thường (FIFO)

  📝 Đơn mới: #1 - Minh: Phở bò, Trà đá
  📝 Đơn mới: #2 - Lan: Bún chả, Nước cam
  📝 Đơn mới: #3 - Hùng: Cơm tấm, Cà phê

  📊 Trạng thái:
     Đang chờ (Queue): 3
     Đang chờ (Priority): 0
     Đã hoàn thành: 0

--- Xử lý đơn hàng (FIFO) ---
  🍳 Đang nấu: #1 - Minh: Phở bò, Trà đá
  ✅ Hoàn thành: #1
  🍳 Đang nấu: #2 - Lan: Bún chả, Nước cam
  ✅ Hoàn thành: #2
  📦 Đơn hoàn thành gần nhất: #2 - Lan: Bún chả, Nước cam

🔹 PHẦN 2: Priority Queue (VIP ưu tiên)

  📝 Đơn mới: #4 - Khách thường 1: Mì xào (priority: 5)
  📝 Đơn mới: #5 - Khách thường 2: Cơm rang (priority: 5)
  📝 Đơn mới: #6 ⭐VIP - VIP Tuấn: Bò Wagyu, Rượu vang (priority: 1)
  📝 Đơn mới: #7 - Khách thường 3: Phở gà (priority: 5)
  📝 Đơn mới: #8 ⭐VIP - VIP Mai: Sushi set, Sake (priority: 1)

--- Xử lý đơn hàng (theo Priority) ---
  🍳 Đang nấu (priority 1): #6 ⭐VIP - VIP Tuấn: Bò Wagyu, Rượu vang
  ✅ Hoàn thành: #6
  🍳 Đang nấu (priority 1): #8 ⭐VIP - VIP Mai: Sushi set, Sake
  ✅ Hoàn thành: #8
  🍳 Đang nấu (priority 5): #4 - Khách thường 1: Mì xào
  ✅ Hoàn thành: #4
  🍳 Đang nấu (priority 5): #5 - Khách thường 2: Cơm rang
  ✅ Hoàn thành: #5
  🍳 Đang nấu (priority 5): #7 - Khách thường 3: Phở gà
  ✅ Hoàn thành: #7
```

---

## 🔗 Tổng kết các ví dụ

| Ví dụ | Cấu trúc | Ứng dụng | Điểm chính |
|-------|-----------|-----------|-------------|
| 1 | Stack × 2 | Undo/Redo | 2 stack phối hợp |
| 2 | Stack | Bracket Matching | Push mở, Pop đóng |
| 3 | Queue | Print Manager | FIFO ordering |
| 4 | Queue + PriorityQueue + Stack | Restaurant | Kết hợp nhiều collection |

> 📌 **Ghi nhớ**: Stack cho "quay lại" (undo, back), Queue cho "xếp hàng" (task, order)!
