# 🏆 Bài 27: Stack\<T> & Queue\<T> — Thử Thách

---

## 🌐 Browser History & Download Manager

> **Mục tiêu**: Xây dựng trình duyệt web đơn giản với chức năng điều hướng (Stack) và quản lý tải xuống (Queue).

---

### 📋 Mô tả dự án

Bạn sẽ tạo một **trình duyệt web console** với 2 chức năng chính:

1. **Browser Navigation** (dùng Stack):
   - Đi tới trang mới (Visit)
   - Quay lại (Back) — lấy từ Back Stack
   - Đi tiếp (Forward) — lấy từ Forward Stack
   - Xem lịch sử duyệt web

2. **Download Manager** (dùng Queue):
   - Thêm file vào hàng đợi tải
   - Tải file tiếp theo (FIFO)
   - Xem hàng đợi tải
   - Thống kê đã tải

---

### 🏗️ Cấu trúc classes

```
  ┌───────────────────────────────────────────────┐
  │                   Browser                      │
  │                                                │
  │  ┌──────────────────────────────────────────┐  │
  │  │          NavigationManager               │  │
  │  │  - Stack<Page> backStack                 │  │
  │  │  - Stack<Page> forwardStack              │  │
  │  │  - Page currentPage                      │  │
  │  │  + Visit(url)                            │  │
  │  │  + Back() : Page                         │  │
  │  │  + Forward() : Page                      │  │
  │  │  + CanGoBack : bool                      │  │
  │  │  + CanGoForward : bool                   │  │
  │  │  + GetHistory() : List<Page>             │  │
  │  └──────────────────────────────────────────┘  │
  │                                                │
  │  ┌──────────────────────────────────────────┐  │
  │  │          DownloadManager                 │  │
  │  │  - Queue<DownloadItem> downloadQueue     │  │
  │  │  - Stack<DownloadItem> completedStack    │  │
  │  │  - DownloadItem? currentDownload         │  │
  │  │  + AddDownload(url, fileSize)            │  │
  │  │  + ProcessNext()                         │  │
  │  │  + ProcessAll()                          │  │
  │  │  + CancelCurrent()                       │  │
  │  │  + ShowQueue()                           │  │
  │  │  + GetStats() : DownloadStats            │  │
  │  └──────────────────────────────────────────┘  │
  └───────────────────────────────────────────────┘
```

---

### 📐 Yêu cầu chi tiết

#### Class `Page`:
```
- Url (string)
- Title (string)
- VisitedAt (DateTime)
- ToString() → "[Title] url - HH:mm:ss"
```

#### Class `DownloadItem`:
```
- FileName (string)
- Url (string)
- FileSizeMB (double)
- Status (enum: Queued, Downloading, Completed, Cancelled)
- StartedAt (DateTime?)
- CompletedAt (DateTime?)
```

#### Class `NavigationManager`:
```
- Visit(url, title) 
    → Push current vào backStack
    → Clear forwardStack
    → Set new page as current

- Back()
    → Push current vào forwardStack
    → Pop backStack → set as current
    
- Forward()
    → Push current vào backStack
    → Pop forwardStack → set as current

- GetHistory() → tất cả pages đã visit (back + current + forward)
```

#### Class `DownloadManager`:
```
- AddDownload(fileName, url, sizeMB)
    → Enqueue vào downloadQueue
    
- ProcessNext()
    → Dequeue → set as currentDownload
    → Simulate download (hiển thị progress)
    → Push vào completedStack
    
- CancelCurrent()
    → Hủy download đang chạy
    
- GetStats() → { TotalDownloaded, TotalSizeMB, AverageSize }
```

#### Class `Browser`:
```
- NavigationManager nav
- DownloadManager downloads
- Chạy menu console cho user tương tác
```

---

### 🎯 Tính năng bắt buộc:

1. ✅ Điều hướng Back/Forward đúng logic (2 Stack)
2. ✅ Visit trang mới → xóa Forward stack
3. ✅ Download queue xử lý FIFO
4. ✅ Hiển thị trạng thái đầy đủ
5. ✅ Xử lý edge cases (back khi không có trang, queue rỗng)
6. ✅ Menu console interactive

### 🌟 Tính năng nâng cao (bonus):
- 📌 Bookmark pages (lưu vào List)
- 📊 Thống kê: tổng dung lượng đã tải, trang truy cập nhiều nhất
- 🔄 Download retry (nếu fail → enqueue lại)
- ⏱️ Simulate download time dựa trên file size

---

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson27_Challenge
{
    // ═══════════════════════════════════════════
    // Models
    // ═══════════════════════════════════════════

    class Page
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime VisitedAt { get; set; }

        public Page(string url, string title)
        {
            Url = url;
            Title = title;
            VisitedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Title}] {Url} - {VisitedAt:HH:mm:ss}";
        }
    }

    enum DownloadStatus { Queued, Downloading, Completed, Cancelled }

    class DownloadItem
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public double FileSizeMB { get; set; }
        public DownloadStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public DownloadItem(string fileName, string url, double sizeMB)
        {
            Id = _nextId++;
            FileName = fileName;
            Url = url;
            FileSizeMB = sizeMB;
            Status = DownloadStatus.Queued;
        }

        public override string ToString()
        {
            string statusIcon = Status switch
            {
                DownloadStatus.Queued => "⏳",
                DownloadStatus.Downloading => "⬇️",
                DownloadStatus.Completed => "✅",
                DownloadStatus.Cancelled => "❌",
                _ => "❓"
            };
            return $"{statusIcon} #{Id} {FileName} ({FileSizeMB:F1}MB) - {Status}";
        }
    }

    class DownloadStats
    {
        public int TotalDownloaded { get; set; }
        public double TotalSizeMB { get; set; }
        public double AverageSizeMB { get; set; }
        public int TotalCancelled { get; set; }
        public int PendingCount { get; set; }
    }

    // ═══════════════════════════════════════════
    // Navigation Manager (2 Stacks)
    // ═══════════════════════════════════════════

    class NavigationManager
    {
        private Stack<Page> _backStack = new Stack<Page>();
        private Stack<Page> _forwardStack = new Stack<Page>();
        private Page? _currentPage = null;
        private List<Page> _fullHistory = new List<Page>();

        public Page? CurrentPage => _currentPage;
        public bool CanGoBack => _backStack.Count > 0;
        public bool CanGoForward => _forwardStack.Count > 0;

        public void Visit(string url, string title)
        {
            // Lưu trang hiện tại vào Back stack
            if (_currentPage != null)
            {
                _backStack.Push(_currentPage);
            }

            // Xóa Forward stack (không thể forward sau khi visit mới)
            _forwardStack.Clear();

            // Set trang mới
            _currentPage = new Page(url, title);
            _fullHistory.Add(_currentPage);

            Console.WriteLine($"  🌐 Truy cập: {_currentPage}");
        }

        public Page? Back()
        {
            if (!CanGoBack)
            {
                Console.WriteLine("  ⚠️ Không thể quay lại — đã ở trang đầu!");
                return null;
            }

            // Push current → Forward stack
            _forwardStack.Push(_currentPage!);

            // Pop Back stack → current
            _currentPage = _backStack.Pop();
            Console.WriteLine($"  ⬅️ Back: {_currentPage}");
            return _currentPage;
        }

        public Page? Forward()
        {
            if (!CanGoForward)
            {
                Console.WriteLine("  ⚠️ Không thể đi tiếp — đã ở trang cuối!");
                return null;
            }

            // Push current → Back stack
            _backStack.Push(_currentPage!);

            // Pop Forward stack → current
            _currentPage = _forwardStack.Pop();
            Console.WriteLine($"  ➡️ Forward: {_currentPage}");
            return _currentPage;
        }

        public void ShowStatus()
        {
            Console.WriteLine("\n  ┌── NAVIGATION STATUS ──────────────────┐");
            Console.WriteLine($"  │ Current: {_currentPage?.ToString() ?? "(trống)"}");
            Console.WriteLine($"  │ Back stack:    {_backStack.Count} trang");
            Console.WriteLine($"  │ Forward stack: {_forwardStack.Count} trang");
            Console.WriteLine("  └─────────────────────────────────────────┘");

            // Hiển thị chi tiết
            if (_backStack.Count > 0)
            {
                Console.WriteLine("  ⬅️ Back stack (mới nhất trước):");
                foreach (var page in _backStack)
                {
                    Console.WriteLine($"     • {page}");
                }
            }

            if (_forwardStack.Count > 0)
            {
                Console.WriteLine("  ➡️ Forward stack (mới nhất trước):");
                foreach (var page in _forwardStack)
                {
                    Console.WriteLine($"     • {page}");
                }
            }
        }

        public void ShowFullHistory()
        {
            Console.WriteLine($"\n  📜 Lịch sử duyệt web ({_fullHistory.Count} trang):");
            for (int i = 0; i < _fullHistory.Count; i++)
            {
                string marker = _fullHistory[i] == _currentPage ? " ← HIỆN TẠI" : "";
                Console.WriteLine($"     {i + 1}. {_fullHistory[i]}{marker}");
            }
        }
    }

    // ═══════════════════════════════════════════
    // Download Manager (Queue + Stack)
    // ═══════════════════════════════════════════

    class DownloadManager
    {
        private Queue<DownloadItem> _downloadQueue = new Queue<DownloadItem>();
        private Stack<DownloadItem> _completedStack = new Stack<DownloadItem>();
        private List<DownloadItem> _cancelledList = new List<DownloadItem>();
        private DownloadItem? _currentDownload = null;

        public int PendingCount => _downloadQueue.Count;

        public void AddDownload(string fileName, string url, double sizeMB)
        {
            DownloadItem item = new DownloadItem(fileName, url, sizeMB);
            _downloadQueue.Enqueue(item);
            Console.WriteLine($"  📥 Thêm download: {item}");
            Console.WriteLine($"     Vị trí hàng đợi: #{_downloadQueue.Count}");
        }

        public void ProcessNext()
        {
            if (_currentDownload != null)
            {
                Console.WriteLine($"  ⚠️ Đang tải: {_currentDownload.FileName}. " +
                                  $"Chờ hoàn thành!");
                return;
            }

            if (!_downloadQueue.TryDequeue(out DownloadItem? item))
            {
                Console.WriteLine("  📭 Hàng đợi tải trống!");
                return;
            }

            _currentDownload = item;
            item.Status = DownloadStatus.Downloading;
            item.StartedAt = DateTime.Now;

            // Simulate download progress
            Console.WriteLine($"  ⬇️ Bắt đầu tải: {item.FileName} ({item.FileSizeMB:F1}MB)");
            SimulateProgress(item.FileSizeMB);

            // Complete
            item.Status = DownloadStatus.Completed;
            item.CompletedAt = DateTime.Now;
            _completedStack.Push(item);
            _currentDownload = null;
            Console.WriteLine($"  ✅ Hoàn thành: {item.FileName}");
        }

        public void ProcessAll()
        {
            int count = _downloadQueue.Count;
            if (count == 0)
            {
                Console.WriteLine("  📭 Không có file nào trong hàng đợi!");
                return;
            }

            Console.WriteLine($"\n  🔄 Tải tất cả ({count} files)...\n");
            while (_downloadQueue.Count > 0)
            {
                ProcessNext();
                Console.WriteLine();
            }
            Console.WriteLine("  🎉 Đã tải xong tất cả!");
        }

        public void CancelCurrent()
        {
            if (_currentDownload == null)
            {
                Console.WriteLine("  ⚠️ Không có download đang chạy!");
                return;
            }

            _currentDownload.Status = DownloadStatus.Cancelled;
            _cancelledList.Add(_currentDownload);
            Console.WriteLine($"  ❌ Đã hủy: {_currentDownload.FileName}");
            _currentDownload = null;
        }

        public void ShowQueue()
        {
            Console.WriteLine($"\n  📋 Hàng đợi tải ({_downloadQueue.Count} files):");
            if (_downloadQueue.Count == 0)
            {
                Console.WriteLine("     (trống)");
                return;
            }
            int pos = 1;
            foreach (var item in _downloadQueue)
            {
                Console.WriteLine($"     {pos}. {item}");
                pos++;
            }
        }

        public void ShowCompleted()
        {
            Console.WriteLine($"\n  ✅ Đã tải ({_completedStack.Count} files, gần nhất trước):");
            if (_completedStack.Count == 0)
            {
                Console.WriteLine("     (chưa có)");
                return;
            }
            foreach (var item in _completedStack)
            {
                Console.WriteLine($"     • {item}");
            }
        }

        public DownloadStats GetStats()
        {
            double totalSize = 0;
            foreach (var item in _completedStack)
            {
                totalSize += item.FileSizeMB;
            }

            return new DownloadStats
            {
                TotalDownloaded = _completedStack.Count,
                TotalSizeMB = totalSize,
                AverageSizeMB = _completedStack.Count > 0 
                    ? totalSize / _completedStack.Count : 0,
                TotalCancelled = _cancelledList.Count,
                PendingCount = _downloadQueue.Count
            };
        }

        public void ShowStats()
        {
            DownloadStats stats = GetStats();
            Console.WriteLine("\n  📊 THỐNG KÊ DOWNLOAD:");
            Console.WriteLine($"     Đã tải:   {stats.TotalDownloaded} files");
            Console.WriteLine($"     Tổng:     {stats.TotalSizeMB:F1} MB");
            Console.WriteLine($"     TB:       {stats.AverageSizeMB:F1} MB/file");
            Console.WriteLine($"     Đã hủy:  {stats.TotalCancelled} files");
            Console.WriteLine($"     Đang chờ: {stats.PendingCount} files");
        }

        private void SimulateProgress(double sizeMB)
        {
            int steps = 5;
            for (int i = 1; i <= steps; i++)
            {
                int percent = i * 100 / steps;
                double downloaded = sizeMB * i / steps;
                string bar = new string('█', i * 4) + new string('░', (steps - i) * 4);
                Console.Write($"\r     [{bar}] {percent}% ({downloaded:F1}/{sizeMB:F1} MB)");
            }
            Console.WriteLine();
        }
    }

    // ═══════════════════════════════════════════
    // Browser (Main Controller)
    // ═══════════════════════════════════════════

    class Browser
    {
        private NavigationManager _nav = new NavigationManager();
        private DownloadManager _downloads = new DownloadManager();

        public void Run()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║       🌐 SIMPLE BROWSER v1.0             ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝\n");

            // Demo Navigation
            DemoNavigation();

            Console.WriteLine("\n" + new string('═', 50) + "\n");

            // Demo Downloads
            DemoDownloads();

            Console.WriteLine("\n" + new string('═', 50) + "\n");

            // Demo Interactive Menu
            ShowMenu();
        }

        private void DemoNavigation()
        {
            Console.WriteLine("🔹 DEMO: NAVIGATION (Back/Forward)\n");

            _nav.Visit("https://google.com", "Google");
            _nav.Visit("https://github.com", "GitHub");
            _nav.Visit("https://stackoverflow.com", "StackOverflow");
            _nav.Visit("https://learn.microsoft.com", "Microsoft Learn");

            _nav.ShowStatus();

            // Back 2 lần
            Console.WriteLine("\n--- Nhấn Back 2 lần ---");
            _nav.Back();    // → StackOverflow
            _nav.Back();    // → GitHub

            _nav.ShowStatus();

            // Forward 1 lần
            Console.WriteLine("\n--- Nhấn Forward 1 lần ---");
            _nav.Forward(); // → StackOverflow

            _nav.ShowStatus();

            // Visit trang mới → xóa Forward
            Console.WriteLine("\n--- Visit trang mới (Forward bị xóa) ---");
            _nav.Visit("https://youtube.com", "YouTube");

            _nav.ShowStatus();

            // Thử Forward → không có
            Console.WriteLine();
            _nav.Forward(); // ⚠️

            // Xem lịch sử
            _nav.ShowFullHistory();
        }

        private void DemoDownloads()
        {
            Console.WriteLine("🔹 DEMO: DOWNLOAD MANAGER\n");

            _downloads.AddDownload("document.pdf", 
                "https://example.com/doc.pdf", 2.5);
            _downloads.AddDownload("image.png", 
                "https://example.com/img.png", 1.2);
            _downloads.AddDownload("video.mp4", 
                "https://example.com/video.mp4", 150.0);
            _downloads.AddDownload("music.mp3", 
                "https://example.com/music.mp3", 4.8);
            _downloads.AddDownload("archive.zip", 
                "https://example.com/archive.zip", 25.3);

            _downloads.ShowQueue();

            // Tải 2 file đầu
            Console.WriteLine("\n--- Tải 2 file đầu ---");
            _downloads.ProcessNext();
            _downloads.ProcessNext();

            _downloads.ShowCompleted();
            _downloads.ShowQueue();

            // Tải tất cả còn lại
            _downloads.ProcessAll();

            // Thống kê
            _downloads.ShowStats();
        }

        private void ShowMenu()
        {
            Console.WriteLine("🔹 INTERACTIVE MENU\n");
            Console.WriteLine("  ┌─────────────────────────────────────┐");
            Console.WriteLine("  │  1. Visit trang mới                 │");
            Console.WriteLine("  │  2. Back (⬅️)                       │");
            Console.WriteLine("  │  3. Forward (➡️)                    │");
            Console.WriteLine("  │  4. Xem trạng thái navigation      │");
            Console.WriteLine("  │  5. Xem lịch sử                    │");
            Console.WriteLine("  │  6. Thêm download                  │");
            Console.WriteLine("  │  7. Tải file tiếp theo             │");
            Console.WriteLine("  │  8. Tải tất cả                     │");
            Console.WriteLine("  │  9. Xem thống kê download          │");
            Console.WriteLine("  │  0. Thoát                          │");
            Console.WriteLine("  └─────────────────────────────────────┘");
            Console.WriteLine("\n  💡 Trong thực tế, bạn sẽ dùng Console.ReadLine()");
            Console.WriteLine("     để đọc input và switch/case để xử lý menu.");
            Console.WriteLine("     Hãy tự implement phần interactive này!\n");
        }
    }

    // ═══════════════════════════════════════════
    // Main Program
    // ═══════════════════════════════════════════

    class Program
    {
        static void Main(string[] args)
        {
            Browser browser = new Browser();
            browser.Run();
        }
    }
}
```

---

## 📊 Checklist hoàn thành

| # | Yêu cầu | Hoàn thành |
|---|---------|------------|
| 1 | NavigationManager với 2 Stack | ⬜ |
| 2 | Visit → clear Forward stack | ⬜ |
| 3 | Back/Forward đúng logic | ⬜ |
| 4 | DownloadManager với Queue | ⬜ |
| 5 | Download FIFO ordering | ⬜ |
| 6 | Completed stack (xem gần nhất) | ⬜ |
| 7 | Xử lý edge cases | ⬜ |
| 8 | Hiển thị trạng thái đầy đủ | ⬜ |
| 9 | Statistics | ⬜ |
| 10 | Interactive menu (bonus) | ⬜ |

> 🏆 **Hoàn thành 8/10** = Xuất sắc! Bạn đã thành thạo Stack & Queue!
