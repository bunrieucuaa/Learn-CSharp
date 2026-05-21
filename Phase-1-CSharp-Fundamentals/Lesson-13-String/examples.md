# 💻 Lesson 10 — Ví dụ thực hành: String

---

## Ví dụ 1: String Methods phổ biến nhất

```csharp
using System;

class Program
{
    static void Main()
    {
        string text = "  Hello World, Hello C# Programming!  ";
        Console.WriteLine($"Original: \"{text}\"");
        Console.WriteLine($"Length: {text.Length}");

        // Trim
        string trimmed = text.Trim();
        Console.WriteLine($"\nTrim: \"{trimmed}\"");

        // Case
        Console.WriteLine($"Upper: {trimmed.ToUpper()}");
        Console.WriteLine($"Lower: {trimmed.ToLower()}");

        // Search
        Console.WriteLine($"\nContains 'World': {trimmed.Contains("World")}");
        Console.WriteLine($"StartsWith 'Hello': {trimmed.StartsWith("Hello")}");
        Console.WriteLine($"IndexOf 'Hello': {trimmed.IndexOf("Hello")}");
        Console.WriteLine($"LastIndexOf 'Hello': {trimmed.LastIndexOf("Hello")}");

        // Replace
        Console.WriteLine($"\nReplace 'Hello' → 'Hi': {trimmed.Replace("Hello", "Hi")}");

        // Substring / Range
        Console.WriteLine($"Substring(0,5): {trimmed.Substring(0, 5)}");
        Console.WriteLine($"Range [..5]: {trimmed[..5]}");
        Console.WriteLine($"Range [^14..]: {trimmed[^14..]}");

        // Split
        string[] words = trimmed.Split(' ');
        Console.WriteLine($"\nSplit by space ({words.Length} words):");
        for (int i = 0; i < words.Length; i++)
            Console.WriteLine($"  [{i}] \"{words[i]}\"");

        // Join
        Console.WriteLine($"\nJoin with '-': {string.Join("-", words)}");
    }
}
```

---

## Ví dụ 2: Xử lý dữ liệu CSV

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== XỬ LÝ DỮ LIỆU CSV ===\n");

        // Dữ liệu CSV
        string csvData = @"Tên,Tuổi,Điểm,Thành phố
Nguyễn Minh,22,8.5,Hà Nội
Trần Hùng,25,7.0,HCM
Lê Lan,20,9.2,Đà Nẵng
Phạm An,23,6.5,Hải Phòng
Võ Bình,21,8.0,Cần Thơ";

        // Tách thành dòng
        string[] lines = csvData.Split('\n');

        // Header
        string[] headers = lines[0].Split(',');
        Console.WriteLine($"{headers[0],-15} {headers[1],5} {headers[2],6} {headers[3],-12}");
        Console.WriteLine(new string('─', 42));

        // Data rows
        double totalScore = 0;
        double maxScore = 0;
        string topStudent = "";

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Trim().Split(',');
            if (fields.Length < 4) continue;

            string name = fields[0];
            string age = fields[1];
            double.TryParse(fields[2], out double score);
            string city = fields[3];

            Console.WriteLine($"{name,-15} {age,5} {score,6:F1} {city,-12}");

            totalScore += score;
            if (score > maxScore)
            {
                maxScore = score;
                topStudent = name;
            }
        }

        int studentCount = lines.Length - 1;
        Console.WriteLine(new string('─', 42));
        Console.WriteLine($"Trung bình: {totalScore / studentCount:F2}");
        Console.WriteLine($"Cao nhất: {topStudent} ({maxScore:F1})");
    }
}
```

---

## Ví dụ 3: StringBuilder — Tạo HTML

```csharp
using System;
using System.Text;

class Program
{
    static void Main()
    {
        string[] products = { "iPhone 15", "AirPods Pro", "MacBook Air" };
        decimal[] prices = { 25990000m, 5990000m, 32990000m };

        // ===== So sánh: string vs StringBuilder =====

        // Cách 1: String (OK cho ít phần tử)
        string html1 = "<ul>\n";
        for (int i = 0; i < products.Length; i++)
            html1 += $"  <li>{products[i]} - {prices[i]:N0} VNĐ</li>\n";
        html1 += "</ul>";

        // Cách 2: StringBuilder (tốt hơn)
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<table>");
        sb.AppendLine("  <tr><th>Sản phẩm</th><th>Giá</th></tr>");

        for (int i = 0; i < products.Length; i++)
        {
            sb.AppendLine($"  <tr>");
            sb.AppendLine($"    <td>{products[i]}</td>");
            sb.AppendLine($"    <td>{prices[i]:N0} VNĐ</td>");
            sb.AppendLine($"  </tr>");
        }

        sb.AppendLine("</table>");

        Console.WriteLine("--- HTML (string) ---");
        Console.WriteLine(html1);

        Console.WriteLine("--- HTML (StringBuilder) ---");
        Console.WriteLine(sb.ToString());

        // ===== Benchmark đơn giản =====
        Console.WriteLine("--- Benchmark ---");

        var sw1 = System.Diagnostics.Stopwatch.StartNew();
        string s = "";
        for (int i = 0; i < 50000; i++) s += "x";
        sw1.Stop();

        var sw2 = System.Diagnostics.Stopwatch.StartNew();
        StringBuilder sb2 = new StringBuilder();
        for (int i = 0; i < 50000; i++) sb2.Append("x");
        string s2 = sb2.ToString();
        sw2.Stop();

        Console.WriteLine($"String concat: {sw1.ElapsedMilliseconds}ms");
        Console.WriteLine($"StringBuilder: {sw2.ElapsedMilliseconds}ms");
        Console.WriteLine($"→ StringBuilder nhanh hơn ~{sw1.ElapsedMilliseconds / Math.Max(sw2.ElapsedMilliseconds, 1)}x!");
    }
}
```

---

## Ví dụ 4: Xử lý text — Đếm, tìm, thay thế

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhập đoạn text: ");
        string? text = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(text))
        { Console.WriteLine("❌ Text rỗng!"); return; }

        Console.WriteLine($"\n📊 PHÂN TÍCH TEXT");
        Console.WriteLine(new string('═', 35));
        Console.WriteLine($"  Độ dài: {text.Length} ký tự");
        Console.WriteLine($"  Số từ:  {CountWords(text)}");
        Console.WriteLine($"  Chữ cái: {CountByType(text, char.IsLetter)}");
        Console.WriteLine($"  Chữ số:  {CountByType(text, char.IsDigit)}");
        Console.WriteLine($"  Khoảng trắng: {CountByType(text, char.IsWhiteSpace)}");
        Console.WriteLine($"  Nguyên âm: {CountVowels(text)}");
        Console.WriteLine($"  Phụ âm:    {CountConsonants(text)}");

        // Từ dài nhất
        string longest = FindLongestWord(text);
        Console.WriteLine($"  Từ dài nhất: \"{longest}\" ({longest.Length} ký tự)");

        // Đảo ngược từng từ
        Console.WriteLine($"  Đảo từ: \"{ReverseWords(text)}\"");
    }

    static int CountWords(string text)
        => text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

    static int CountByType(string text, Func<char, bool> check)
    {
        int count = 0;
        foreach (char c in text)
            if (check(c)) count++;
        return count;
    }

    static int CountVowels(string text)
    {
        int count = 0;
        foreach (char c in text.ToLower())
            if ("aeiou".Contains(c)) count++;
        return count;
    }

    static int CountConsonants(string text)
    {
        int count = 0;
        foreach (char c in text.ToLower())
            if (char.IsLetter(c) && !"aeiou".Contains(c)) count++;
        return count;
    }

    static string FindLongestWord(string text)
    {
        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string longest = "";
        foreach (string w in words)
            if (w.Length > longest.Length) longest = w;
        return longest;
    }

    static string ReverseWords(string text)
    {
        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            char[] chars = words[i].ToCharArray();
            Array.Reverse(chars);
            words[i] = new string(chars);
        }
        return string.Join(" ", words);
    }
}
```

---

## Ví dụ 5: Ứng dụng tổng hợp — Mã hóa Caesar

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║    🔐 MÃ HÓA CAESAR         ║");
        Console.WriteLine("╚══════════════════════════════╝\n");

        Console.Write("Nhập tin nhắn: ");
        string? message = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(message)) return;

        Console.Write("Khóa dịch chuyển (1-25): ");
        if (!int.TryParse(Console.ReadLine(), out int key) || key < 1 || key > 25)
        { Console.WriteLine("❌ Khóa không hợp lệ!"); return; }

        string encrypted = CaesarEncrypt(message, key);
        string decrypted = CaesarDecrypt(encrypted, key);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n📝 Gốc:     \"{message}\"");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"🔒 Mã hóa:  \"{encrypted}\"");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"🔓 Giải mã: \"{decrypted}\"");
        Console.ResetColor();

        // Brute force — thử tất cả key
        Console.Write("\nHiện tất cả key? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
        {
            Console.WriteLine($"\n--- Brute Force \"{encrypted}\" ---");
            for (int k = 1; k <= 25; k++)
            {
                string attempt = CaesarDecrypt(encrypted, k);
                string marker = k == key ? " ← ĐÚNG!" : "";
                Console.WriteLine($"  Key {k,2}: {attempt}{marker}");
            }
        }
    }

    static string CaesarEncrypt(string text, int shift)
    {
        char[] result = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (char.IsLetter(c))
            {
                char baseChar = char.IsUpper(c) ? 'A' : 'a';
                result[i] = (char)((c - baseChar + shift) % 26 + baseChar);
            }
            else
            {
                result[i] = c;  // Giữ nguyên ký tự không phải chữ cái
            }
        }

        return new string(result);
    }

    static string CaesarDecrypt(string text, int shift)
        => CaesarEncrypt(text, 26 - shift);  // Giải mã = dịch ngược
}
```

### 📝 Giải thích Caesar:

| Ký tự | Shift = 3 | Kết quả |
|-------|-----------|---------|
| A (65) | +3 | D (68) |
| Z (90) | +3 | C (67) — wrap around! |
| a (97) | +3 | d (100) |

```
Original:  A B C D E F ... X Y Z
Encrypted: D E F G H I ... A B C   (shift = 3)
```
