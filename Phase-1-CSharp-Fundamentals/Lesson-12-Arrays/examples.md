# 💻 Lesson 12 — Ví dụ thực hành: Arrays

---

## Ví dụ 1: CRUD với Array

```csharp
using System;

class Program
{
    static string[] names = new string[20];
    static double[] scores = new double[20];
    static int count = 0;

    static void Main()
    {
        string? choice;
        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═════════════════════════════╗");
            Console.WriteLine($"║  📋 QUẢN LÝ ĐIỂM ({count,2}/20)   ║");
            Console.WriteLine($"╠═════════════════════════════╣");
            Console.WriteLine($"║  1. Thêm    4. Xóa          ║");
            Console.WriteLine($"║  2. Xem     5. Sắp xếp      ║");
            Console.WriteLine($"║  3. Sửa     6. Tìm kiếm     ║");
            Console.WriteLine($"║  0. Thoát                    ║");
            Console.WriteLine($"╚═════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Chọn: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Add(); break;
                case "2": List(); break;
                case "3": Edit(); break;
                case "4": Delete(); break;
                case "5": Sort(); break;
                case "6": Search(); break;
            }

            if (choice != "0") { Console.Write("\nEnter..."); Console.ReadLine(); }
        } while (choice != "0");
    }

    static void Add()
    {
        if (count >= 20) { Console.WriteLine("❌ Đầy!"); return; }
        Console.Write("Tên: ");
        string? name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name)) return;

        Console.Write("Điểm (0-10): ");
        if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10) return;

        names[count] = name;
        scores[count] = score;
        count++;
        Console.WriteLine($"✅ Đã thêm {name}!");
    }

    static void List()
    {
        if (count == 0) { Console.WriteLine("📭 Trống!"); return; }
        Console.WriteLine($"\n{"#",3} {"Tên",-15} {"Điểm",6} {"Loại",-8}");
        Console.WriteLine(new string('─', 36));
        for (int i = 0; i < count; i++)
        {
            string grade = scores[i] >= 8 ? "Giỏi" : scores[i] >= 5 ? "TB" : "Yếu";
            Console.WriteLine($"{i + 1,3} {names[i],-15} {scores[i],6:F1} {grade,-8}");
        }
    }

    static void Edit()
    {
        List();
        Console.Write("Sửa # (1-{0}): ", count);
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > count) return;
        idx--;

        Console.Write($"Tên mới ({names[idx]}): ");
        string? newName = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(newName)) names[idx] = newName;

        Console.Write($"Điểm mới ({scores[idx]:F1}): ");
        if (double.TryParse(Console.ReadLine(), out double newScore) && newScore >= 0 && newScore <= 10)
            scores[idx] = newScore;

        Console.WriteLine("✅ Đã sửa!");
    }

    static void Delete()
    {
        List();
        Console.Write("Xóa # (1-{0}): ", count);
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > count) return;
        idx--;

        // Dịch mảng sang trái
        for (int i = idx; i < count - 1; i++)
        {
            names[i] = names[i + 1];
            scores[i] = scores[i + 1];
        }
        count--;
        Console.WriteLine("✅ Đã xóa!");
    }

    static void Sort()
    {
        if (count < 2) return;
        // Bubble Sort theo điểm giảm dần
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - 1 - i; j++)
            {
                if (scores[j] < scores[j + 1])
                {
                    // Swap cả tên và điểm
                    (scores[j], scores[j + 1]) = (scores[j + 1], scores[j]);
                    (names[j], names[j + 1]) = (names[j + 1], names[j]);
                }
            }
        }
        Console.WriteLine("✅ Đã sắp xếp theo điểm giảm dần!");
        List();
    }

    static void Search()
    {
        Console.Write("Tìm tên: ");
        string? keyword = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(keyword)) return;

        bool found = false;
        for (int i = 0; i < count; i++)
        {
            if (names[i].Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  → [{i + 1}] {names[i]} — {scores[i]:F1}");
                found = true;
            }
        }
        if (!found) Console.WriteLine("❌ Không tìm thấy!");
    }
}
```

---

## Ví dụ 2: Mảng 2 chiều — Bàn cờ & Tic-Tac-Toe

```csharp
using System;

class Program
{
    static void Main()
    {
        // ── Tic-Tac-Toe Board ──
        char[,] board = {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' }
        };

        char currentPlayer = 'X';
        int moves = 0;

        while (moves < 9)
        {
            Console.Clear();
            PrintBoard(board);

            Console.Write($"\nLượt {currentPlayer} — Nhập vị trí (1-9): ");
            if (!int.TryParse(Console.ReadLine(), out int pos) || pos < 1 || pos > 9) continue;

            int row = (pos - 1) / 3;
            int col = (pos - 1) % 3;

            if (board[row, col] != ' ')
            {
                Console.WriteLine("❌ Ô đã đánh!"); Console.ReadKey();
                continue;
            }

            board[row, col] = currentPlayer;
            moves++;

            if (CheckWin(board, currentPlayer))
            {
                Console.Clear();
                PrintBoard(board);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n🎉 {currentPlayer} THẮNG!");
                Console.ResetColor();
                return;
            }

            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }

        Console.Clear();
        PrintBoard(board);
        Console.WriteLine("\n🤝 HÒA!");
    }

    static void PrintBoard(char[,] board)
    {
        Console.WriteLine("     1   2   3");
        Console.WriteLine("   ┌───┬───┬───┐");
        for (int r = 0; r < 3; r++)
        {
            Console.Write($"   │");
            for (int c = 0; c < 3; c++)
            {
                char ch = board[r, c];
                if (ch == 'X') Console.ForegroundColor = ConsoleColor.Red;
                else if (ch == 'O') Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($" {ch} ");
                Console.ResetColor();
                Console.Write("│");
            }
            Console.WriteLine($"  {r * 3 + 1}-{r * 3 + 3}");
            if (r < 2) Console.WriteLine("   ├───┼───┼───┤");
        }
        Console.WriteLine("   └───┴───┴───┘");
    }

    static bool CheckWin(char[,] b, char p)
    {
        // Hàng ngang
        for (int r = 0; r < 3; r++)
            if (b[r, 0] == p && b[r, 1] == p && b[r, 2] == p) return true;
        // Cột dọc
        for (int c = 0; c < 3; c++)
            if (b[0, c] == p && b[1, c] == p && b[2, c] == p) return true;
        // Chéo
        if (b[0, 0] == p && b[1, 1] == p && b[2, 2] == p) return true;
        if (b[0, 2] == p && b[1, 1] == p && b[2, 0] == p) return true;
        return false;
    }
}
```

---

## Ví dụ 3: Thuật toán — Sắp xếp & Tìm kiếm

```csharp
using System;

class Program
{
    static void Main()
    {
        int[] data = { 64, 25, 12, 22, 11, 90, 34, 45 };

        Console.Write("Gốc:          ");
        Print(data);

        // ── Bubble Sort ──
        int[] bubbled = (int[])data.Clone();
        BubbleSort(bubbled);
        Console.Write("Bubble Sort:  ");
        Print(bubbled);

        // ── Selection Sort ──
        int[] selected = (int[])data.Clone();
        SelectionSort(selected);
        Console.Write("Selection Sort:");
        Print(selected);

        // ── Array.Sort (built-in) ──
        int[] builtin = (int[])data.Clone();
        Array.Sort(builtin);
        Console.Write("Array.Sort:   ");
        Print(builtin);

        // ── Linear Search ──
        Console.Write("\nTìm số: ");
        if (int.TryParse(Console.ReadLine(), out int target))
        {
            int idx = LinearSearch(data, target);
            Console.WriteLine(idx >= 0
                ? $"✅ Tìm thấy {target} tại index {idx}"
                : $"❌ Không tìm thấy {target}");

            // Binary Search (cần sorted array)
            int bIdx = Array.BinarySearch(builtin, target);
            Console.WriteLine(bIdx >= 0
                ? $"✅ Binary Search: index {bIdx} (trong sorted array)"
                : $"❌ Binary Search: không tìm thấy");
        }
    }

    static void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
            for (int j = 0; j < arr.Length - 1 - i; j++)
                if (arr[j] > arr[j + 1])
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
    }

    static void SelectionSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < arr.Length; j++)
                if (arr[j] < arr[minIdx])
                    minIdx = j;
            if (minIdx != i)
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
        }
    }

    static int LinearSearch(int[] arr, int target)
    {
        for (int i = 0; i < arr.Length; i++)
            if (arr[i] == target) return i;
        return -1;
    }

    static void Print(int[] arr)
    {
        Console.Write("[");
        for (int i = 0; i < arr.Length; i++)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(arr[i]);
        }
        Console.WriteLine("]");
    }
}
```

---

## Ví dụ 4: Mảng song song — Quản lý sản phẩm

```csharp
using System;

class Program
{
    const int MAX = 50;
    static string[] productNames = new string[MAX];
    static decimal[] productPrices = new decimal[MAX];
    static int[] productStocks = new int[MAX];
    static int productCount = 0;

    static void Main()
    {
        // Seed data
        AddProduct("iPhone 15", 25990000m, 10);
        AddProduct("AirPods Pro", 5990000m, 25);
        AddProduct("MacBook Air", 32990000m, 5);
        AddProduct("iPad Air", 16990000m, 15);
        AddProduct("Apple Watch", 11990000m, 20);

        ShowAll();
        ShowStats();
    }

    static void AddProduct(string name, decimal price, int stock)
    {
        if (productCount >= MAX) return;
        productNames[productCount] = name;
        productPrices[productCount] = price;
        productStocks[productCount] = stock;
        productCount++;
    }

    static void ShowAll()
    {
        Console.WriteLine($"\n{"#",3} {"Sản phẩm",-15} {"Giá",14} {"Tồn",5} {"Giá trị tồn",16}");
        Console.WriteLine(new string('═', 56));
        for (int i = 0; i < productCount; i++)
        {
            decimal value = productPrices[i] * productStocks[i];
            Console.WriteLine(
                $"{i + 1,3} {productNames[i],-15} {productPrices[i],14:N0} {productStocks[i],5} {value,16:N0}");
        }
    }

    static void ShowStats()
    {
        decimal totalValue = 0;
        decimal maxPrice = 0;
        string? mostExpensive = null;
        int lowStockCount = 0;

        for (int i = 0; i < productCount; i++)
        {
            totalValue += productPrices[i] * productStocks[i];
            if (productPrices[i] > maxPrice)
            {
                maxPrice = productPrices[i];
                mostExpensive = productNames[i];
            }
            if (productStocks[i] <= 10) lowStockCount++;
        }

        Console.WriteLine($"\n📊 Thống kê:");
        Console.WriteLine($"  Tổng giá trị kho: {totalValue:N0} VNĐ");
        Console.WriteLine($"  Đắt nhất: {mostExpensive} ({maxPrice:N0})");
        Console.WriteLine($"  Sắp hết ({lowStockCount} SP): tồn ≤ 10");
    }
}
```
