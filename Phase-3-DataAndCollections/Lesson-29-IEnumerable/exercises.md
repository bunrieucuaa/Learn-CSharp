# ✏️ Bài 29: IEnumerable<T> & Iteration — Bài tập

> **Hoàn thành 5 bài tập dưới đây. Mỗi bài tập trung vào 1 khía cạnh của IEnumerable và yield.**

---

## Bài 1: Custom Iterator — NumberSequence 🔢

> **Kỹ năng:** Dùng `yield return` tạo các chuỗi số khác nhau

### Yêu cầu

Tạo static class `NumberSequence` với các method trả về `IEnumerable<int>`:

```
NumberSequence:
├── IEnumerable<int> OddNumbers(int max)          → Số lẻ: 1, 3, 5, 7...
├── IEnumerable<int> Multiples(int n, int count)   → Bội số: n, 2n, 3n...
├── IEnumerable<int> Primes(int max)               → Số nguyên tố <= max
├── IEnumerable<int> Triangular()                  → Tam giác: 1, 3, 6, 10, 15...
│                                                    (mỗi số = tổng từ 1 đến n)
└── IEnumerable<int> Collatz(int start)            → Collatz: nếu chẵn n/2,
                                                     nếu lẻ 3n+1, dừng khi = 1
```

### Chương trình Main:

```csharp
// 1. In 10 số lẻ đầu tiên
// 2. In bội số của 7 (10 bội)
// 3. In số nguyên tố <= 50
// 4. In 8 số tam giác đầu tiên
// 5. In chuỗi Collatz bắt đầu từ 27
```

### Output mong đợi:

```
── SỐ LẺ (10 số) ──
  1 3 5 7 9 11 13 15 17 19

── BỘI SỐ CỦA 7 (10 bội) ──
  7 14 21 28 35 42 49 56 63 70

── SỐ NGUYÊN TỐ <= 50 ──
  2 3 5 7 11 13 17 19 23 29 31 37 41 43 47

── SỐ TAM GIÁC (8 số) ──
  1 3 6 10 15 21 28 36

── COLLATZ (start=27) ──
  27 82 41 124 62 31 94 47 142 71 ... 4 2 1
  Tổng: ??? bước
```

---

## Bài 2: IEnumerable cho class — PlayList 🎵

> **Kỹ năng:** Implement `IEnumerable<T>` cho class riêng + yield return filters

### Yêu cầu

```
class Song:
├── string Title
├── string Artist
├── int DurationSeconds
├── string Genre        → "Pop", "Rock", "Jazz", "Hip-Hop"...
└── int PlayCount
    ToString(): "🎵 Title — Artist (M:SS)"

class Playlist : IEnumerable<Song>
├── string Name
├── void Add(Song song)
├── void Remove(string title)
├── int Count
├── int TotalDuration                    → Tổng thời lượng (seconds)
│
├── IEnumerable<Song> GetByGenre(string genre)   → Lọc theo thể loại
├── IEnumerable<Song> GetLongerThan(int secs)    → Lọc theo thời lượng
├── IEnumerable<Song> GetTopPlayed(int n)        → Top n bài nghe nhiều
├── IEnumerable<Song> Shuffle()                  → Trộn ngẫu nhiên (yield)
│
├── IEnumerator<Song> GetEnumerator()            → foreach
└── IEnumerator IEnumerable.GetEnumerator()
```

### Chương trình Main:

```csharp
// 1. Tạo Playlist "My Mix" với 8-10 bài hát
// 2. Duyệt bằng foreach — in toàn bộ playlist
// 3. Lọc theo genre "Pop"
// 4. Lọc bài > 4 phút
// 5. Top 3 bài nghe nhiều nhất
// 6. Shuffle và in 5 bài ngẫu nhiên
// 7. In tổng thời lượng playlist (MM:SS)
```

---

## Bài 3: yield return — String Utilities 📝

> **Kỹ năng:** Dùng `yield return` + `yield break` xử lý chuỗi

### Yêu cầu

Tạo static class `StringIterator` với các method:

```
StringIterator:
├── IEnumerable<char> Vowels(string text)
│   → Trả về các nguyên âm trong chuỗi (a, e, i, o, u, A, E, I, O, U)
│
├── IEnumerable<string> Words(string text)
│   → Tách chuỗi thành từng từ (yield từng từ)
│
├── IEnumerable<string> Lines(string text)
│   → Tách chuỗi thành từng dòng (yield từng dòng)
│
├── IEnumerable<string> Chunks(string text, int size)
│   → Chia chuỗi thành các đoạn có size ký tự
│   → "HelloWorld" → size=3 → "Hel", "loW", "orl", "d"
│
├── IEnumerable<char> UniqueChars(string text)
│   → Trả về các ký tự KHÔNG trùng lặp (giữ thứ tự xuất hiện)
│
└── IEnumerable<(int Index, char Char)> FindAll(string text, char target)
    → Trả về tất cả vị trí của ký tự target (dùng tuple)
```

### Chương trình Main:

```csharp
string text = "Xin chào các bạn! Hôm nay chúng ta học C#.";

// 1. In các nguyên âm
// 2. In từng từ
// 3. Chia thành chunks 5 ký tự
// 4. In ký tự unique
// 5. Tìm tất cả vị trí của ký tự 'c'

string multiline = "Dòng 1\nDòng 2\nDòng 3\nDòng 4";
// 6. In từng dòng
```

### Output mong đợi:

```
── NGUYÊN ÂM ──
  i, à, o, á, ạ, ô, a, ú, a, ọ

── TỪNG TỪ ──
  [Xin] [chào] [các] [bạn!] [Hôm] [nay] ...

── CHUNKS (5 ký tự) ──
  "Xin c" | "hào c" | "ác bạ" | "n! Hô" | ...

── TÌM 'c' ──
  Vị trí 7: 'c'
  Vị trí 11: 'c'
  ...
```

---

## Bài 4: Deferred Execution — Logger Pipeline 📋

> **Kỹ năng:** Hiểu deferred execution qua pipeline xử lý log entries

### Yêu cầu

```
class LogEntry:
├── DateTime Timestamp
├── string Level        → "DEBUG", "INFO", "WARN", "ERROR"
├── string Message
├── string Source       → "App", "Database", "Network", "Auth"
└── ToString(): "[Timestamp] [LEVEL] [Source] Message"

static class LogPipeline:
├── IEnumerable<LogEntry> FilterByLevel(IEnumerable<LogEntry> logs, string level)
│   → yield return chỉ log có Level == level
│
├── IEnumerable<LogEntry> FilterBySource(IEnumerable<LogEntry> logs, string source)
│   → yield return chỉ log có Source == source
│
├── IEnumerable<LogEntry> FilterByDateRange(IEnumerable<LogEntry> logs,
│       DateTime from, DateTime to)
│   → yield return log trong khoảng thời gian
│
├── IEnumerable<string> FormatForDisplay(IEnumerable<LogEntry> logs)
│   → yield return formatted string cho mỗi log
│
├── IEnumerable<LogEntry> Take(IEnumerable<LogEntry> logs, int count)
│   → yield return N phần tử đầu, yield break khi đủ
│
└── IEnumerable<LogEntry> Skip(IEnumerable<LogEntry> logs, int count)
    → Bỏ qua N phần tử đầu, yield return phần còn lại
```

### Chương trình Main:

```csharp
// 1. Tạo List<LogEntry> với 20-30 log entries giả lập
// 2. Pipeline 1: FilterByLevel("ERROR") → FormatForDisplay → foreach
// 3. Pipeline 2: FilterBySource("Database") → Take(5) → foreach
// 4. Pipeline 3: FilterByLevel("WARN") → FilterBySource("Network") → foreach
// 5. Chứng minh deferred: In message khi tạo pipeline vs khi duyệt
// 6. So sánh: dùng List filter (eager) vs yield filter (lazy)
```

### Output mong đợi:

```
═══ PIPELINE 1: Tất cả ERROR logs ═══
  [2024-01-15 10:23:45] [ERROR] [Database] Connection timeout
  [2024-01-15 10:25:12] [ERROR] [Network] Socket reset
  ...

═══ PIPELINE 3: WARN + Network ═══
  [2024-01-15 10:20:01] [WARN] [Network] High latency detected
  ...

═══ DEFERRED PROOF ═══
  Tạo pipeline... (chưa có output từ filter)
  Bắt đầu duyệt...
  [FilterByLevel] Kiểm tra log #1... PASS
  → [2024-01-15 ...] ERROR ...
  [FilterByLevel] Kiểm tra log #2... SKIP
  ...
```

---

## Bài 5: Custom IEnumerable — Matrix\<T\> 🔲

> **Kỹ năng:** Implement IEnumerable cho cấu trúc 2D — ma trận

### Yêu cầu

```
class Matrix<T> : IEnumerable<T>
├── Constructor(int rows, int cols)
├── T this[int row, int col] { get; set; }  → Indexer 2D
├── int Rows, int Cols
│
├── IEnumerable<T> GetRow(int row)          → yield từng phần tử của hàng
├── IEnumerable<T> GetColumn(int col)       → yield từng phần tử của cột
├── IEnumerable<T> GetDiagonal()            → yield đường chéo chính
├── IEnumerable<(int Row, int Col, T Value)> GetAllWithIndex()
│   → yield (row, col, value) cho mỗi phần tử
│
├── IEnumerator<T> GetEnumerator()          → Duyệt hàng-theo-hàng
└── IEnumerator IEnumerable.GetEnumerator()
    
Bonus:
├── IEnumerable<T> Flatten()                → Duyệt 2D → 1D
└── void Fill(IEnumerable<T> source)        → Điền data từ IEnumerable vào matrix
```

### Chương trình Main:

```csharp
// 1. Tạo Matrix<int> 3x4 và fill dữ liệu
Matrix<int> matrix = new Matrix<int>(3, 4);
// Fill: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12

// 2. In ma trận dạng bảng
//    1   2   3   4
//    5   6   7   8
//    9  10  11  12

// 3. foreach duyệt toàn bộ (hàng-theo-hàng)
foreach (int x in matrix) Console.Write($"{x} ");
// Output: 1 2 3 4 5 6 7 8 9 10 11 12

// 4. Duyệt hàng 1
// 5. Duyệt cột 2
// 6. Duyệt đường chéo
// 7. GetAllWithIndex → in (row, col, value)

// 8. Matrix<string> 2x3 — chứng minh generic
Matrix<string> names = new Matrix<string>(2, 3);
```

---

## 📊 Bảng tổng hợp bài tập

| Bài | Chủ đề | Kỹ thuật chính | Độ khó |
|-----|--------|---------------|--------|
| 1 | Number sequences | yield return, yield break | ⭐⭐ |
| 2 | Playlist collection | IEnumerable\<T\> implement | ⭐⭐⭐ |
| 3 | String utilities | yield + chuỗi xử lý | ⭐⭐ |
| 4 | Logger pipeline | Deferred execution, chaining | ⭐⭐⭐⭐ |
| 5 | Matrix\<T\> | IEnumerable + Generic + 2D | ⭐⭐⭐⭐ |

---

> **💡 Mẹo:** Bắt đầu từ Bài 1 (yield cơ bản), rồi Bài 3 (cũng yield), sau đó Bài 2 (implement IEnumerable), cuối cùng Bài 4-5 (nâng cao). 🚀
