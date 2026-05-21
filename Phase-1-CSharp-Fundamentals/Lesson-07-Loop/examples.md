# 💻 Lesson 07 — Ví dụ thực hành: Loop

---

## Ví dụ 1: Bảng cửu chương

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhập bảng cửu chương (2-9): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 2 || n > 9)
        { Console.WriteLine("❌ Không hợp lệ!"); return; }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n╔═══ BẢNG CỬU CHƯƠNG {n} ═══╗");
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"║   {n} × {i,2} = {n * i,3}        ║");
        }
        Console.WriteLine("╚═══════════════════════╝");
        Console.ResetColor();
    }
}
```

---

## Ví dụ 2: Các pattern với vòng lặp lồng nhau

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhập số hàng (1-10): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 10) return;

        // Pattern 1: Tam giác phải
        Console.WriteLine("\n--- Tam giác phải ---");
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= i; j++)
                Console.Write("★ ");
            Console.WriteLine();
        }

        // Pattern 2: Tam giác ngược
        Console.WriteLine("\n--- Tam giác ngược ---");
        for (int i = n; i >= 1; i--)
        {
            for (int j = 1; j <= i; j++)
                Console.Write("★ ");
            Console.WriteLine();
        }

        // Pattern 3: Kim tự tháp
        Console.WriteLine("\n--- Kim tự tháp ---");
        for (int i = 1; i <= n; i++)
        {
            // In khoảng trắng
            for (int s = 0; s < n - i; s++)
                Console.Write(" ");
            // In sao
            for (int j = 0; j < 2 * i - 1; j++)
                Console.Write("★");
            Console.WriteLine();
        }

        // Pattern 4: Bàn cờ
        Console.WriteLine("\n--- Bàn cờ ---");
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                Console.Write((row + col) % 2 == 0 ? "⬛" : "⬜");
            }
            Console.WriteLine();
        }
    }
}
```

### 📝 Output (n = 5):

```
--- Tam giác phải ---
★
★ ★
★ ★ ★
★ ★ ★ ★
★ ★ ★ ★ ★

--- Kim tự tháp ---
    ★
   ★★★
  ★★★★★
 ★★★★★★★
★★★★★★★★★

--- Bàn cờ ---
⬛⬜⬛⬜⬛
⬜⬛⬜⬛⬜
⬛⬜⬛⬜⬛
⬜⬛⬜⬛⬜
⬛⬜⬛⬜⬛
```

---

## Ví dụ 3: While — Game đoán số

```csharp
using System;

class Program
{
    static void Main()
    {
        Random rng = new Random();
        bool playAgain = true;

        while (playAgain)
        {
            int secret = rng.Next(1, 101);
            int attempts = 0;
            int maxAttempts = 7;
            bool won = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║    🎲 GAME ĐOÁN SỐ 1-100    ║");
            Console.WriteLine($"║    Bạn có {maxAttempts} lần đoán!       ║");
            Console.WriteLine("╚══════════════════════════════╝\n");
            Console.ResetColor();

            while (attempts < maxAttempts && !won)
            {
                Console.Write($"Lần {attempts + 1}/{maxAttempts}: ");
                if (!int.TryParse(Console.ReadLine(), out int guess) || guess < 1 || guess > 100)
                {
                    Console.WriteLine("❌ Nhập số 1-100!");
                    continue;  // Không tính lần này
                }

                attempts++;

                if (guess == secret)
                {
                    won = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n🎉 CHÍNH XÁC! Số bí mật là {secret}!");
                    Console.WriteLine($"Bạn đoán trong {attempts} lần!");
                    string rank = attempts switch
                    {
                        1 => "🏆 THẦN THÁNH!",
                        <= 3 => "⭐ Xuất sắc!",
                        <= 5 => "👍 Tốt lắm!",
                        _ => "📝 Ổn!"
                    };
                    Console.WriteLine(rank);
                    Console.ResetColor();
                }
                else
                {
                    string hint = guess < secret ? "⬆️ Lớn hơn!" : "⬇️ Nhỏ hơn!";
                    int diff = Math.Abs(guess - secret);
                    string proximity = diff switch
                    {
                        <= 5 => "🔥 Rất gần!",
                        <= 15 => "🌡️ Gần!",
                        <= 30 => "❄️ Còn xa!",
                        _ => "🧊 Rất xa!"
                    };
                    Console.WriteLine($"  {hint} {proximity}");
                }
            }

            if (!won)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n💀 Hết lượt! Số bí mật là {secret}.");
                Console.ResetColor();
            }

            Console.Write("\nChơi lại? (y/n): ");
            playAgain = Console.ReadLine()?.Trim().ToLower() == "y";
        }

        Console.WriteLine("👋 Tạm biệt!");
    }
}
```

---

## Ví dụ 4: Do-While — Nhập và tính toán liên tục

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== MÁY TÍNH LIÊN TỤC ===");
        Console.WriteLine("Nhập phép tính (VD: 5+3) hoặc 'q' để thoát\n");

        decimal running = 0;
        bool firstTime = true;

        do
        {
            if (!firstTime)
                Console.WriteLine($"  [Kết quả trước: {running:N2}]");

            Console.Write(">>> ");
            string? input = Console.ReadLine()?.Trim();

            if (input == null || input.ToLower() == "q") break;

            // Tìm operator
            char op = ' ';
            int opIndex = -1;
            for (int i = 1; i < input.Length; i++)  // Bắt đầu từ 1 (số âm)
            {
                if ("+-*/".Contains(input[i]))
                {
                    op = input[i];
                    opIndex = i;
                    break;
                }
            }

            if (opIndex == -1)
            {
                Console.WriteLine("  ❌ Nhập dạng: 5+3, 10*2, ...");
                continue;
            }

            if (decimal.TryParse(input[..opIndex], out decimal a) &&
                decimal.TryParse(input[(opIndex + 1)..], out decimal b))
            {
                running = op switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' when b != 0 => a / b,
                    '/' => 0,
                    _ => 0
                };

                if (op == '/' && b == 0)
                    Console.WriteLine("  ❌ Chia cho 0!");
                else
                    Console.WriteLine($"  = {running:N4}");

                firstTime = false;
            }
            else
            {
                Console.WriteLine("  ❌ Số không hợp lệ!");
            }
        } while (true);

        Console.WriteLine($"\nKết quả cuối: {running:N2}. Tạm biệt!");
    }
}
```

---

## Ví dụ 5: Foreach + Tổng hợp — Quản lý danh sách

```csharp
using System;

class Program
{
    static void Main()
    {
        string[] students = new string[20];
        double[] scores = new double[20];
        int count = 0;

        string? choice;
        do
        {
            Console.WriteLine($"\n=== QUẢN LÝ ĐIỂM ({count} học sinh) ===");
            Console.WriteLine("1. Thêm   2. Xem   3. Thống kê   0. Thoát");
            Console.Write("Chọn: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (count >= 20) { Console.WriteLine("❌ Đầy!"); break; }
                    Console.Write("Tên: ");
                    string? name = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(name)) break;

                    Console.Write("Điểm (0-10): ");
                    if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10) break;

                    students[count] = name;
                    scores[count] = score;
                    count++;
                    Console.WriteLine($"✅ Đã thêm {name}!");
                    break;

                case "2":
                    if (count == 0) { Console.WriteLine("📭 Chưa có ai!"); break; }
                    Console.WriteLine($"\n{"STT",4} {"Tên",-15} {"Điểm",6} {"Loại",-10}");
                    Console.WriteLine(new string('─', 40));
                    for (int i = 0; i < count; i++)
                    {
                        string grade = scores[i] switch
                        {
                            >= 8 => "Giỏi",
                            >= 6.5 => "Khá",
                            >= 5 => "TB",
                            _ => "Yếu"
                        };
                        Console.WriteLine($"{i + 1,4} {students[i],-15} {scores[i],6:F1} {grade,-10}");
                    }
                    break;

                case "3":
                    if (count == 0) { Console.WriteLine("📭 Chưa có ai!"); break; }
                    double sum = 0, max = scores[0], min = scores[0];
                    int gioi = 0, kha = 0, tb = 0, yeu = 0;

                    for (int i = 0; i < count; i++)
                    {
                        sum += scores[i];
                        if (scores[i] > max) max = scores[i];
                        if (scores[i] < min) min = scores[i];

                        if (scores[i] >= 8) gioi++;
                        else if (scores[i] >= 6.5) kha++;
                        else if (scores[i] >= 5) tb++;
                        else yeu++;
                    }

                    Console.WriteLine($"\n📊 Thống kê ({count} học sinh):");
                    Console.WriteLine($"  TB:  {sum / count:F2}");
                    Console.WriteLine($"  Max: {max:F1}  |  Min: {min:F1}");
                    Console.WriteLine($"  Giỏi: {gioi} | Khá: {kha} | TB: {tb} | Yếu: {yeu}");
                    break;

                case "0":
                    Console.WriteLine("👋 Tạm biệt!");
                    break;
            }
        } while (choice != "0");
    }
}
```

### 📝 Bài học:

- `do-while` cho menu loop
- `for` cho duyệt mảng theo index (cần sửa/đọc)
- `switch expression` cho xếp loại
- Kết hợp tất cả kiến thức Lesson 01-07
