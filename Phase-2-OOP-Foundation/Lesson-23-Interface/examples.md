# 💻 Bài 23: Interface Nâng Cao — Ví dụ thực hành

> **Tất cả ví dụ đều chạy được. Hãy tạo Console App và copy-paste để thử!**

---

## Ví dụ 1: IComparable\<Student\> — Sort students by score 📊

> **Mục tiêu:** Implement `IComparable<T>` để `Array.Sort()` hoạt động với object tự tạo.

```csharp
using System;

namespace ComparableStudentDemo
{
    // ═══════════════════════════════════════
    // STUDENT — Implement IComparable<Student>
    // ═══════════════════════════════════════
    class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string StudentId { get; set; }

        public Student(string id, string name, double score)
        {
            StudentId = id;
            Name = name;
            Score = score;
        }

        // ═══ IComparable<Student> Implementation ═══
        // CompareTo trả về:
        //   < 0 → this TRƯỚC other (this nhỏ hơn)
        //   = 0 → bằng nhau
        //   > 0 → this SAU other (this lớn hơn)
        public int CompareTo(Student? other)
        {
            if (other == null) return 1;

            // Sắp xếp theo Score GIẢM DẦN (điểm cao lên trước)
            int result = other.Score.CompareTo(Score);

            // Nếu điểm bằng nhau → sắp xếp theo Name A-Z
            if (result == 0)
            {
                result = Name.CompareTo(other.Name);
            }

            return result;
        }

        public override string ToString()
        {
            return $"  {StudentId} | {Name,-15} | Điểm: {Score:F1}";
        }

        // Xếp loại theo điểm
        public string GetGrade()
        {
            return Score switch
            {
                >= 9.0 => "🌟 Xuất sắc",
                >= 8.0 => "✅ Giỏi",
                >= 6.5 => "👍 Khá",
                >= 5.0 => "📝 Trung bình",
                _      => "❌ Yếu"
            };
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   IComparable<Student> DEMO          ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo mảng students
            Student[] students = new Student[]
            {
                new Student("SV001", "Nguyễn Văn An",    8.5),
                new Student("SV002", "Trần Thị Bình",    9.2),
                new Student("SV003", "Lê Hoàng Cường",   7.0),
                new Student("SV004", "Phạm Minh Đức",    9.2),
                new Student("SV005", "Hoàng Thị Em",     6.0),
                new Student("SV006", "Vũ Quang Phúc",    8.5),
                new Student("SV007", "Đặng Thùy Giang",  5.5),
            };

            // === TRƯỚC khi sort ===
            Console.WriteLine("── TRƯỚC KHI SẮP XẾP ──");
            Console.WriteLine("  ID    | Tên             | Điểm");
            Console.WriteLine("  ──────┼─────────────────┼──────");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            // === Sort bằng Array.Sort() — dùng IComparable ===
            Array.Sort(students);  // ← Gọi CompareTo() bên trong!

            Console.WriteLine("\n── SAU KHI SẮP XẾP (Điểm giảm dần, tên A-Z) ──");
            Console.WriteLine("  Hạng | ID    | Tên             | Điểm    | Xếp loại");
            Console.WriteLine("  ─────┼───────┼─────────────────┼─────────┼──────────");
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"  #{i + 1,-4} {students[i]}  | {students[i].GetGrade()}");
            }

            // === So sánh trực tiếp ===
            Console.WriteLine("\n── SO SÁNH TRỰC TIẾP ──");
            Student s1 = students[0];
            Student s2 = students[students.Length - 1];
            int cmp = s1.CompareTo(s2);
            Console.WriteLine($"  {s1.Name} vs {s2.Name}");
            Console.WriteLine($"  CompareTo() = {cmp}");
            Console.WriteLine($"  → {s1.Name} {(cmp < 0 ? "đứng TRƯỚC" : cmp > 0 ? "đứng SAU" : "bằng")} {s2.Name}");

            // === Tìm điểm cao nhất / thấp nhất (đã sort) ===
            Console.WriteLine("\n── THỐNG KÊ ──");
            Console.WriteLine($"  🏆 Điểm cao nhất: {students[0].Name} ({students[0].Score:F1})");
            Console.WriteLine($"  📉 Điểm thấp nhất: {students[students.Length - 1].Name} ({students[students.Length - 1].Score:F1})");

            double avg = 0;
            foreach (Student s in students) avg += s.Score;
            avg /= students.Length;
            Console.WriteLine($"  📊 Điểm trung bình: {avg:F2}");
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   IComparable<Student> DEMO          ║
╚══════════════════════════════════════╝

── SAU KHI SẮP XẾP (Điểm giảm dần, tên A-Z) ──
  Hạng | ID    | Tên             | Điểm    | Xếp loại
  ─────┼───────┼─────────────────┼─────────┼──────────
  #1    SV004 | Phạm Minh Đức    | Điểm: 9.2  | 🌟 Xuất sắc
  #2    SV002 | Trần Thị Bình    | Điểm: 9.2  | 🌟 Xuất sắc
  #3    SV001 | Nguyễn Văn An    | Điểm: 8.5  | ✅ Giỏi
  ...
```

---

## Ví dụ 2: Explicit Interface Implementation — IPrinter + IScanner 🖨️

> **Mục tiêu:** Khi 2 interface có method trùng tên, dùng explicit implementation để phân biệt.

```csharp
using System;

namespace ExplicitInterfaceDemo
{
    // ═══════════════════════════════════════
    // INTERFACES — Cả hai đều có Start() và Stop()
    // ═══════════════════════════════════════
    interface IPrinter
    {
        void Start();
        void Stop();
        void PrintDocument(string doc);
        int PagesRemaining { get; }
    }

    interface IScanner
    {
        void Start();
        void Stop();
        void ScanDocument(string outputPath);
        int Resolution { get; set; }
    }

    interface IFax
    {
        void Start();
        void Stop();
        void SendFax(string number, string document);
    }

    // ═══════════════════════════════════════
    // MULTIFUNCTIONDEVICE — Implement 3 interfaces có method trùng
    // ═══════════════════════════════════════
    class MultiFunctionDevice : IPrinter, IScanner, IFax
    {
        public string DeviceName { get; set; }
        private int pages;
        private int resolution;

        public MultiFunctionDevice(string name)
        {
            DeviceName = name;
            pages = 500;
            resolution = 300;
        }

        // ═══ EXPLICIT IMPLEMENTATION — IPrinter ═══
        int IPrinter.PagesRemaining => pages;

        void IPrinter.Start()
        {
            Console.WriteLine($"  🖨️ [{DeviceName}] Khởi động chế độ IN...");
            Console.WriteLine($"     Giấy còn: {pages} tờ");
        }

        void IPrinter.Stop()
        {
            Console.WriteLine($"  🖨️ [{DeviceName}] Dừng in.");
        }

        void IPrinter.PrintDocument(string doc)
        {
            pages--;
            Console.WriteLine($"  🖨️ [{DeviceName}] Đang in: \"{doc}\" (còn {pages} tờ)");
        }

        // ═══ EXPLICIT IMPLEMENTATION — IScanner ═══
        int IScanner.Resolution
        {
            get => resolution;
            set => resolution = value;
        }

        void IScanner.Start()
        {
            Console.WriteLine($"  📠 [{DeviceName}] Khởi động chế độ QUÉT...");
            Console.WriteLine($"     Độ phân giải: {resolution} DPI");
        }

        void IScanner.Stop()
        {
            Console.WriteLine($"  📠 [{DeviceName}] Dừng quét.");
        }

        void IScanner.ScanDocument(string outputPath)
        {
            Console.WriteLine($"  📠 [{DeviceName}] Đang quét → {outputPath} ({resolution} DPI)");
        }

        // ═══ EXPLICIT IMPLEMENTATION — IFax ═══
        void IFax.Start()
        {
            Console.WriteLine($"  📞 [{DeviceName}] Khởi động chế độ FAX...");
            Console.WriteLine($"     Đang kết nối đường dây...");
        }

        void IFax.Stop()
        {
            Console.WriteLine($"  📞 [{DeviceName}] Ngắt kết nối fax.");
        }

        void IFax.SendFax(string number, string document)
        {
            Console.WriteLine($"  📞 [{DeviceName}] Gửi fax → {number}: \"{document}\"");
        }

        // ═══ PUBLIC METHOD — Không thuộc interface nào ═══
        public void ShowStatus()
        {
            Console.WriteLine($"\n  ℹ️ {DeviceName} — Trạng thái:");
            Console.WriteLine($"     Giấy: {pages} | Độ phân giải: {resolution} DPI");
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   EXPLICIT INTERFACE DEMO            ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            MultiFunctionDevice device = new MultiFunctionDevice("Canon MF645");

            // ❌ KHÔNG THỂ gọi Start() trực tiếp từ device!
            // device.Start();  // Lỗi compile! — Start() thuộc interface nào?

            // ✅ Public method gọi được bình thường
            device.ShowStatus();

            // ═══ Chế độ IN — cast về IPrinter ═══
            Console.WriteLine("\n── CHẾ ĐỘ IN ──");
            IPrinter printer = device;         // Cast về IPrinter
            printer.Start();                    // Gọi IPrinter.Start()
            printer.PrintDocument("Báo cáo Q1.docx");
            printer.PrintDocument("Hóa đơn #1234.pdf");
            printer.Stop();

            // ═══ Chế độ QUÉT — cast về IScanner ═══
            Console.WriteLine("\n── CHẾ ĐỘ QUÉT ──");
            IScanner scanner = device;         // Cast về IScanner
            scanner.Resolution = 600;          // Đổi resolution
            scanner.Start();                    // Gọi IScanner.Start()
            scanner.ScanDocument("scan_001.jpg");
            scanner.Stop();

            // ═══ Chế độ FAX — cast về IFax ═══
            Console.WriteLine("\n── CHẾ ĐỘ FAX ──");
            IFax fax = device;                 // Cast về IFax
            fax.Start();                        // Gọi IFax.Start()
            fax.SendFax("028-1234-5678", "Hợp đồng ABC");
            fax.Stop();

            // ═══ Kiểm tra interface ═══
            Console.WriteLine("\n── KIỂM TRA INTERFACE ──");
            object obj = device;
            Console.WriteLine($"  Is IPrinter?  {obj is IPrinter}");   // true
            Console.WriteLine($"  Is IScanner?  {obj is IScanner}");   // true
            Console.WriteLine($"  Is IFax?      {obj is IFax}");       // true

            // ═══ Cast inline ═══
            Console.WriteLine("\n── CAST INLINE ──");
            ((IPrinter)device).Start();        // IPrinter.Start()
            ((IScanner)device).Start();        // IScanner.Start()
            ((IFax)device).Start();            // IFax.Start()
            // Ba cái Start() — BA hành vi KHÁC NHAU!

            device.ShowStatus();
        }
    }
}
```

**Output mong đợi:**
```
╔══════════════════════════════════════╗
║   EXPLICIT INTERFACE DEMO            ║
╚══════════════════════════════════════╝

── CHẾ ĐỘ IN ──
  🖨️ [Canon MF645] Khởi động chế độ IN...
     Giấy còn: 500 tờ
  🖨️ [Canon MF645] Đang in: "Báo cáo Q1.docx" (còn 499 tờ)
  🖨️ [Canon MF645] Đang in: "Hóa đơn #1234.pdf" (còn 498 tờ)
  🖨️ [Canon MF645] Dừng in.

── CHẾ ĐỘ QUÉT ──
  📠 [Canon MF645] Khởi động chế độ QUÉT...
     Độ phân giải: 600 DPI
  📠 [Canon MF645] Đang quét → scan_001.jpg (600 DPI)
  📠 [Canon MF645] Dừng quét.

── CAST INLINE ──
  🖨️ [Canon MF645] Khởi động chế độ IN...
  📠 [Canon MF645] Khởi động chế độ QUÉT...
  📞 [Canon MF645] Khởi động chế độ FAX...
```

---

## Ví dụ 3: ISP — Interface Segregation Principle 🏭

> **Mục tiêu:** Tách interface lớn thành nhiều interface nhỏ — Robot chỉ implement IWorkable.

```csharp
using System;

namespace InterfaceSegregationDemo
{
    // ═══════════════════════════════════════
    // ❌ VÍ DỤ SAI: Fat Interface
    // (Comment out — chỉ để so sánh)
    // ═══════════════════════════════════════
    /*
    interface IWorker
    {
        void Work();
        void Eat();
        void Sleep();
        void GetPaid();
    }

    // Robot bị ÉP implement method vô nghĩa!
    class BadRobot : IWorker
    {
        public void Work() => Console.WriteLine("Working...");
        public void Eat() => throw new NotSupportedException();   // ❌
        public void Sleep() => throw new NotSupportedException(); // ❌
        public void GetPaid() => throw new NotSupportedException(); // ❌
    }
    */

    // ═══════════════════════════════════════
    // ✅ VÍ DỤ ĐÚNG: Segregated Interfaces
    // ═══════════════════════════════════════

    interface IWorkable
    {
        void Work();
        string GetWorkReport();
    }

    interface IFeedable
    {
        void Eat(string food);
        int EnergyLevel { get; }
    }

    interface ISleepable
    {
        void Sleep(int hours);
        bool IsRested { get; }
    }

    interface IPayable
    {
        decimal Salary { get; }
        void GetPaid();
        string GetPaySlip();
    }

    interface IMaintainable   // Dành cho máy móc
    {
        void Maintain();
        int BatteryLevel { get; }
        bool NeedsMaintenance { get; }
    }

    // ═══════════════════════════════════════
    // HUMAN WORKER — Implement 4 interfaces liên quan đến con người
    // ═══════════════════════════════════════
    class HumanWorker : IWorkable, IFeedable, ISleepable, IPayable
    {
        public string Name { get; set; }
        public int EnergyLevel { get; private set; }
        public bool IsRested { get; private set; }
        public decimal Salary { get; private set; }

        private int hoursWorked;

        public HumanWorker(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
            EnergyLevel = 100;
            IsRested = true;
            hoursWorked = 0;
        }

        // IWorkable
        public void Work()
        {
            if (EnergyLevel <= 0)
            {
                Console.WriteLine($"  😫 {Name}: Quá mệt, không thể làm việc!");
                return;
            }
            hoursWorked++;
            EnergyLevel -= 15;
            Console.WriteLine($"  👨‍💼 {Name}: Đang làm việc... " +
                            $"(⚡ Energy: {EnergyLevel}%)");
        }

        public string GetWorkReport()
            => $"{Name}: Đã làm {hoursWorked} giờ";

        // IFeedable
        public void Eat(string food)
        {
            EnergyLevel = Math.Min(100, EnergyLevel + 30);
            Console.WriteLine($"  🍚 {Name}: Ăn {food} " +
                            $"(⚡ Energy: {EnergyLevel}%)");
        }

        // ISleepable
        public void Sleep(int hours)
        {
            EnergyLevel = Math.Min(100, EnergyLevel + hours * 15);
            IsRested = EnergyLevel >= 70;
            Console.WriteLine($"  😴 {Name}: Ngủ {hours}h " +
                            $"(⚡ Energy: {EnergyLevel}%, Rested: {IsRested})");
        }

        // IPayable
        public void GetPaid()
        {
            Console.WriteLine($"  💰 {Name}: Nhận lương {Salary:N0}đ!");
        }

        public string GetPaySlip()
            => $"Phiếu lương: {Name} — {Salary:N0}đ ({hoursWorked} giờ)";
    }

    // ═══════════════════════════════════════
    // ROBOT WORKER — CHỈ implement IWorkable + IMaintainable
    // → Không bị ép implement Eat, Sleep, GetPaid!
    // ═══════════════════════════════════════
    class RobotWorker : IWorkable, IMaintainable
    {
        public string Model { get; set; }
        public int BatteryLevel { get; private set; }
        public bool NeedsMaintenance => BatteryLevel < 20;

        private int tasksCompleted;

        public RobotWorker(string model)
        {
            Model = model;
            BatteryLevel = 100;
            tasksCompleted = 0;
        }

        // IWorkable
        public void Work()
        {
            if (BatteryLevel <= 0)
            {
                Console.WriteLine($"  🔋 {Model}: Pin hết! Cần sạc.");
                return;
            }
            tasksCompleted++;
            BatteryLevel -= 10;
            Console.WriteLine($"  🤖 {Model}: Đang xử lý task #{tasksCompleted}... " +
                            $"(🔋 Pin: {BatteryLevel}%)");
        }

        public string GetWorkReport()
            => $"{Model}: Hoàn thành {tasksCompleted} tasks";

        // IMaintainable
        public void Maintain()
        {
            BatteryLevel = 100;
            Console.WriteLine($"  🔧 {Model}: Bảo trì + sạc pin hoàn tất! (🔋 100%)");
        }
    }

    // ═══════════════════════════════════════
    // MANAGER — Xử lý workers qua INTERFACE
    // ═══════════════════════════════════════
    class WorkManager
    {
        // Nhận BẤT KỲ IWorkable nào — Human hay Robot đều OK!
        public void AssignWork(IWorkable[] workers, int shifts)
        {
            Console.WriteLine($"\n  📋 Phân công {shifts} ca làm việc:");
            for (int shift = 1; shift <= shifts; shift++)
            {
                Console.WriteLine($"\n  ── Ca {shift} ──");
                foreach (IWorkable worker in workers)
                {
                    worker.Work();
                }
            }
        }

        public void ShowReports(IWorkable[] workers)
        {
            Console.WriteLine("\n  📊 BÁO CÁO CÔNG VIỆC:");
            foreach (IWorkable worker in workers)
            {
                Console.WriteLine($"    {worker.GetWorkReport()}");
            }
        }

        // Chỉ trả lương cho IPayable
        public void PayAll(object[] allWorkers)
        {
            Console.WriteLine("\n  💳 TRẢ LƯƠNG:");
            foreach (object worker in allWorkers)
            {
                if (worker is IPayable payable)
                {
                    payable.GetPaid();
                }
                else
                {
                    Console.WriteLine($"    ℹ️ {worker} — Không nhận lương (máy)");
                }
            }
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   INTERFACE SEGREGATION PRINCIPLE    ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo workers
            HumanWorker alice = new HumanWorker("Alice", 20_000_000);
            HumanWorker bob = new HumanWorker("Bob", 18_000_000);
            RobotWorker r2d2 = new RobotWorker("R2-D2");
            RobotWorker wall_e = new RobotWorker("WALL-E");

            WorkManager manager = new WorkManager();

            // === Tất cả đều IWorkable — làm việc chung! ===
            IWorkable[] allWorkers = { alice, bob, r2d2, wall_e };
            manager.AssignWork(allWorkers, 3);

            // === Chỉ Human mới Eat, Sleep ===
            Console.WriteLine("\n── GIỜ NGHỈ TRƯA ──");
            alice.Eat("Phở bò");
            bob.Eat("Cơm gà");
            // r2d2.Eat(...);  // ❌ Compile error — Robot KHÔNG CÓ Eat()!

            Console.WriteLine("\n── NGHỈ NGƠI ──");
            alice.Sleep(1);
            // wall_e.Sleep(...);  // ❌ Robot KHÔNG CÓ Sleep()!

            // === Robot cần bảo trì ===
            Console.WriteLine("\n── BẢO TRÌ ROBOT ──");
            if (r2d2.NeedsMaintenance) r2d2.Maintain();
            if (wall_e.NeedsMaintenance) wall_e.Maintain();

            // === Trả lương — chỉ IPayable ===
            manager.PayAll(new object[] { alice, bob, r2d2, wall_e });

            // === Báo cáo ===
            manager.ShowReports(allWorkers);

            // === Kiểm tra interface ===
            Console.WriteLine("\n── INTERFACE CHECK ──");
            object[] everyone = { alice, r2d2 };
            foreach (object w in everyone)
            {
                string name = w is HumanWorker h ? h.Name : ((RobotWorker)w).Model;
                Console.Write($"  {name}: ");
                if (w is IWorkable) Console.Write("Workable ");
                if (w is IFeedable) Console.Write("Feedable ");
                if (w is ISleepable) Console.Write("Sleepable ");
                if (w is IPayable) Console.Write("Payable ");
                if (w is IMaintainable) Console.Write("Maintainable ");
                Console.WriteLine();
            }
        }
    }
}
```

**Output mong đợi:**
```
── INTERFACE CHECK ──
  Alice: Workable Feedable Sleepable Payable
  R2-D2: Workable Maintainable
```

---

## Ví dụ 4: IDisposable + using — FileLogger 📁

> **Mục tiêu:** Implement `IDisposable` đúng cách, sử dụng `using` statement để tự động giải phóng.

```csharp
using System;
using System.IO;

namespace DisposableLoggerDemo
{
    // ═══════════════════════════════════════
    // FILELOGGER — Implement IDisposable
    // ═══════════════════════════════════════
    class FileLogger : IDisposable
    {
        private StreamWriter? writer;
        private bool disposed = false;
        private int logCount = 0;

        public string FilePath { get; }
        public string LoggerName { get; }

        public FileLogger(string name, string filePath)
        {
            LoggerName = name;
            FilePath = filePath;
            writer = new StreamWriter(filePath, append: true);
            Console.WriteLine($"  📂 [{LoggerName}] Mở file: {filePath}");
        }

        // ═══ Các method ghi log ═══
        public void Log(string message)
        {
            ThrowIfDisposed();
            logCount++;
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [LOG] {message}";
            writer!.WriteLine(entry);
            writer.Flush();
            Console.WriteLine($"  📝 [{LoggerName}] #{logCount}: {message}");
        }

        public void LogError(string message)
        {
            ThrowIfDisposed();
            logCount++;
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {message}";
            writer!.WriteLine(entry);
            writer.Flush();
            Console.WriteLine($"  ❌ [{LoggerName}] #{logCount} ERROR: {message}");
        }

        public void LogWarning(string message)
        {
            ThrowIfDisposed();
            logCount++;
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARN] {message}";
            writer!.WriteLine(entry);
            writer.Flush();
            Console.WriteLine($"  ⚠️ [{LoggerName}] #{logCount} WARN: {message}");
        }

        // ═══ Helper method ═══
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(
                    LoggerName,
                    "Logger đã bị dispose! Không thể ghi log.");
            }
        }

        // ═══ IDisposable Implementation ═══
        public void Dispose()
        {
            if (!disposed)
            {
                // Ghi dòng cuối trước khi đóng
                writer?.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [SYSTEM] " +
                                $"Logger closed. Total entries: {logCount}");
                writer?.Flush();

                // Đóng và giải phóng StreamWriter
                writer?.Close();
                writer?.Dispose();
                writer = null;

                disposed = true;

                Console.WriteLine($"  🔒 [{LoggerName}] Đã đóng file. " +
                                $"Tổng: {logCount} entries.");
            }
        }
    }

    // ═══════════════════════════════════════
    // DATABASE CONNECTION (giả lập) — Cũng IDisposable
    // ═══════════════════════════════════════
    class DatabaseConnection : IDisposable
    {
        private bool isOpen;
        private bool disposed;
        public string ConnectionString { get; }

        public DatabaseConnection(string connStr)
        {
            ConnectionString = connStr;
            isOpen = true;
            disposed = false;
            Console.WriteLine($"  🔗 DB: Kết nối đến {connStr}");
        }

        public string Query(string sql)
        {
            if (disposed) throw new ObjectDisposedException("DatabaseConnection");
            Console.WriteLine($"  🔍 DB: Executing '{sql}'");
            return $"Result of '{sql}'";
        }

        public void Dispose()
        {
            if (!disposed)
            {
                isOpen = false;
                disposed = true;
                Console.WriteLine($"  🔌 DB: Đã ngắt kết nối.");
            }
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   IDisposable + using DEMO           ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            string logPath = "demo_log.txt";

            // ═══ CÁCH 1: using block — Tự động Dispose() ═══
            Console.WriteLine("── CÁCH 1: using BLOCK ──");
            using (FileLogger logger = new FileLogger("AppLog", logPath))
            {
                logger.Log("Ứng dụng khởi động");
                logger.Log("Đang tải cấu hình...");
                logger.LogWarning("Cấu hình cũ, nên cập nhật");
                logger.Log("Cấu hình đã tải xong");
                logger.LogError("Không tìm thấy plugin XYZ");
                logger.Log("Ứng dụng sẵn sàng");
            }   // ← Dispose() được gọi TỰ ĐỘNG ở đây!

            Console.WriteLine("  ✅ Logger đã tự động được giải phóng!\n");

            // ═══ CÁCH 2: using declaration (C# 8+) ═══
            Console.WriteLine("── CÁCH 2: using DECLARATION ──");
            UseDatabase();

            // ═══ CÁCH 3: Multiple using ═══
            Console.WriteLine("\n── CÁCH 3: MULTIPLE using ──");
            string logPath2 = "demo_log2.txt";
            using (FileLogger mainLog = new FileLogger("Main", logPath2))
            using (DatabaseConnection db = new DatabaseConnection("Server=localhost;DB=shop"))
            {
                mainLog.Log("Bắt đầu truy vấn DB");
                string result = db.Query("SELECT * FROM Products");
                mainLog.Log($"Kết quả: {result}");
                mainLog.Log("Hoàn thành!");
            }   // ← CẢ HAI được Dispose() theo thứ tự ngược!

            Console.WriteLine("\n  ✅ Cả logger và DB đều đã giải phóng!");

            // ═══ Chứng minh Dispose đã gọi ═══
            Console.WriteLine("\n── SAU KHI DISPOSE ──");
            FileLogger oldLogger = new FileLogger("Old", "old.txt");
            oldLogger.Log("Test");
            oldLogger.Dispose();   // Gọi thủ công

            try
            {
                oldLogger.Log("Thử ghi sau dispose");  // ❌ Sẽ throw!
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"  💥 Exception: {ex.Message}");
            }

            // Dọn file demo
            CleanupFiles(logPath, logPath2, "old.txt");
        }

        static void UseDatabase()
        {
            // using declaration — Dispose khi hết scope method
            using DatabaseConnection db = new DatabaseConnection("Server=localhost;DB=users");
            string result = db.Query("SELECT COUNT(*) FROM Users");
            Console.WriteLine($"  📊 Kết quả: {result}");
            // ← Dispose() tự động gọi khi method kết thúc
        }

        static void CleanupFiles(params string[] files)
        {
            foreach (string f in files)
            {
                if (File.Exists(f)) File.Delete(f);
            }
        }
    }
}
```

**Output mong đợi:**
```
── CÁCH 1: using BLOCK ──
  📂 [AppLog] Mở file: demo_log.txt
  📝 [AppLog] #1: Ứng dụng khởi động
  📝 [AppLog] #2: Đang tải cấu hình...
  ⚠️ [AppLog] #3 WARN: Cấu hình cũ, nên cập nhật
  📝 [AppLog] #4: Cấu hình đã tải xong
  ❌ [AppLog] #5 ERROR: Không tìm thấy plugin XYZ
  📝 [AppLog] #6: Ứng dụng sẵn sàng
  🔒 [AppLog] Đã đóng file. Tổng: 6 entries.
  ✅ Logger đã tự động được giải phóng!

── SAU KHI DISPOSE ──
  📂 [Old] Mở file: old.txt
  📝 [Old] #1: Test
  🔒 [Old] Đã đóng file. Tổng: 1 entries.
  💥 Exception: Logger đã bị dispose! Không thể ghi log.
```

---

## Ví dụ 5: Dependency Injection — INotificationService 🔔

> **Mục tiêu:** OrderProcessor nhận `INotificationService` qua constructor — dễ dàng thay đổi implementation mà không sửa code.

```csharp
using System;

namespace DependencyInjectionDemo
{
    // ═══════════════════════════════════════
    // INTERFACES
    // ═══════════════════════════════════════
    interface INotificationService
    {
        string ServiceName { get; }
        void Notify(string recipient, string subject, string message);
        bool IsAvailable();
    }

    interface IPaymentGateway
    {
        string GatewayName { get; }
        bool ProcessPayment(decimal amount, string cardNumber);
        void Refund(decimal amount, string transactionId);
    }

    // ═══════════════════════════════════════
    // NOTIFICATION IMPLEMENTATIONS
    // ═══════════════════════════════════════
    class EmailNotification : INotificationService
    {
        public string ServiceName => "Email Service";

        public void Notify(string recipient, string subject, string message)
        {
            Console.WriteLine($"    📧 Email → {recipient}");
            Console.WriteLine($"       Tiêu đề: {subject}");
            Console.WriteLine($"       Nội dung: {message}");
        }

        public bool IsAvailable() => true;
    }

    class SmsNotification : INotificationService
    {
        public string ServiceName => "SMS Service";

        public void Notify(string recipient, string subject, string message)
        {
            // SMS không có tiêu đề
            Console.WriteLine($"    📱 SMS → {recipient}: [{subject}] {message}");
        }

        public bool IsAvailable() => true;
    }

    class PushNotification : INotificationService
    {
        public string ServiceName => "Push Notification";

        public void Notify(string recipient, string subject, string message)
        {
            Console.WriteLine($"    🔔 Push → {recipient}: {subject} - {message}");
        }

        public bool IsAvailable() => true;
    }

    class SlackNotification : INotificationService
    {
        public string ServiceName => "Slack Service";
        private string channel;

        public SlackNotification(string channel)
        {
            this.channel = channel;
        }

        public void Notify(string recipient, string subject, string message)
        {
            Console.WriteLine($"    💬 Slack #{channel} → @{recipient}: *{subject}* {message}");
        }

        public bool IsAvailable() => true;
    }

    // ═══════════════════════════════════════
    // PAYMENT IMPLEMENTATIONS
    // ═══════════════════════════════════════
    class StripePayment : IPaymentGateway
    {
        public string GatewayName => "Stripe";

        public bool ProcessPayment(decimal amount, string cardNumber)
        {
            string masked = "****" + cardNumber[^4..];
            Console.WriteLine($"    💳 Stripe: Thanh toán {amount:N0}đ từ thẻ {masked}");
            return true;
        }

        public void Refund(decimal amount, string transactionId)
        {
            Console.WriteLine($"    ↩️ Stripe: Hoàn tiền {amount:N0}đ (TX: {transactionId})");
        }
    }

    class MomoPayment : IPaymentGateway
    {
        public string GatewayName => "MoMo";

        public bool ProcessPayment(decimal amount, string phoneNumber)
        {
            Console.WriteLine($"    📲 MoMo: Thanh toán {amount:N0}đ từ SĐT {phoneNumber}");
            return true;
        }

        public void Refund(decimal amount, string transactionId)
        {
            Console.WriteLine($"    ↩️ MoMo: Hoàn tiền {amount:N0}đ (TX: {transactionId})");
        }
    }

    // ═══════════════════════════════════════
    // ORDER — Model
    // ═══════════════════════════════════════
    class Order
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string[] Items { get; set; }
        public decimal TotalAmount { get; set; }

        public Order(string id, string name, string contact,
                     string[] items, decimal total)
        {
            OrderId = id;
            CustomerName = name;
            CustomerContact = contact;
            Items = items;
            TotalAmount = total;
        }
    }

    // ═══════════════════════════════════════
    // ORDER PROCESSOR — Nhận dependencies qua CONSTRUCTOR
    // ═══════════════════════════════════════
    class OrderProcessor
    {
        // Dependencies — chỉ biết INTERFACE, không biết class cụ thể!
        private readonly INotificationService notifier;
        private readonly IPaymentGateway paymentGateway;

        // ═══ Constructor Injection ═══
        public OrderProcessor(INotificationService notification,
                              IPaymentGateway payment)
        {
            notifier = notification;
            paymentGateway = payment;
            Console.WriteLine($"  ✅ OrderProcessor khởi tạo:");
            Console.WriteLine($"     Notification: {notifier.ServiceName}");
            Console.WriteLine($"     Payment: {paymentGateway.GatewayName}");
        }

        public bool ProcessOrder(Order order, string paymentInfo)
        {
            Console.WriteLine($"\n  📦 Xử lý đơn hàng: {order.OrderId}");
            Console.WriteLine($"     Khách: {order.CustomerName}");
            Console.WriteLine($"     Mặt hàng: {string.Join(", ", order.Items)}");
            Console.WriteLine($"     Tổng: {order.TotalAmount:N0}đ");

            // Bước 1: Thanh toán
            Console.WriteLine("\n  [Bước 1] Thanh toán:");
            bool paid = paymentGateway.ProcessPayment(order.TotalAmount, paymentInfo);

            if (!paid)
            {
                Console.WriteLine("  ❌ Thanh toán thất bại!");
                notifier.Notify(order.CustomerContact,
                    "Đơn hàng thất bại",
                    $"Đơn {order.OrderId} không thể thanh toán.");
                return false;
            }

            // Bước 2: Thông báo thành công
            Console.WriteLine("\n  [Bước 2] Thông báo:");
            if (notifier.IsAvailable())
            {
                notifier.Notify(
                    order.CustomerContact,
                    "Đặt hàng thành công!",
                    $"Đơn {order.OrderId} đã xác nhận. " +
                    $"Tổng: {order.TotalAmount:N0}đ");
            }

            Console.WriteLine($"\n  ✅ Đơn hàng {order.OrderId} hoàn tất!");
            return true;
        }
    }

    // ═══════════════════════════════════════
    // MAIN PROGRAM
    // ═══════════════════════════════════════
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   DEPENDENCY INJECTION DEMO          ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // Tạo orders
            Order order1 = new Order("ORD-001", "Nguyễn Văn An",
                "an@email.com",
                new[] { "Laptop", "Mouse" }, 25_000_000);

            Order order2 = new Order("ORD-002", "Trần Thị Bình",
                "0987654321",
                new[] { "Headphone", "Keyboard" }, 3_500_000);

            Order order3 = new Order("ORD-003", "Lê Hoàng Cường",
                "cuong_dev",
                new[] { "Monitor" }, 8_000_000);

            // ═══ KỊCH BẢN 1: Email + Stripe ═══
            Console.WriteLine("═══ KỊCH BẢN 1: Email + Stripe ═══");
            OrderProcessor processor1 = new OrderProcessor(
                new EmailNotification(),    // ← Inject Email
                new StripePayment()          // ← Inject Stripe
            );
            processor1.ProcessOrder(order1, "4111222233334444");

            // ═══ KỊCH BẢN 2: SMS + MoMo ═══
            Console.WriteLine("\n═══ KỊCH BẢN 2: SMS + MoMo ═══");
            OrderProcessor processor2 = new OrderProcessor(
                new SmsNotification(),       // ← Inject SMS
                new MomoPayment()            // ← Inject MoMo
            );
            processor2.ProcessOrder(order2, "0987654321");

            // ═══ KỊCH BẢN 3: Slack + Stripe ═══
            Console.WriteLine("\n═══ KỊCH BẢN 3: Slack + Stripe ═══");
            OrderProcessor processor3 = new OrderProcessor(
                new SlackNotification("orders"),  // ← Inject Slack
                new StripePayment()                // ← Inject Stripe
            );
            processor3.ProcessOrder(order3, "5555666677778888");

            // ═══ SỨC MẠNH CỦA DI ═══
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("💡 Sức mạnh của Dependency Injection:");
            Console.WriteLine("  → OrderProcessor KHÔNG ĐỔI code!");
            Console.WriteLine("  → Chỉ đổi object truyền vào constructor");
            Console.WriteLine("  → Email ↔ SMS ↔ Push ↔ Slack: tùy chọn");
            Console.WriteLine("  → Stripe ↔ MoMo: tùy chọn");
            Console.WriteLine("  → Dễ test: truyền MockNotification vào!");
            Console.WriteLine(new string('═', 50));
        }
    }
}
```

**Output mong đợi:**
```
═══ KỊCH BẢN 1: Email + Stripe ═══
  ✅ OrderProcessor khởi tạo:
     Notification: Email Service
     Payment: Stripe

  📦 Xử lý đơn hàng: ORD-001
     Khách: Nguyễn Văn An
     Mặt hàng: Laptop, Mouse
     Tổng: 25,000,000đ

  [Bước 1] Thanh toán:
    💳 Stripe: Thanh toán 25,000,000đ từ thẻ ****4444

  [Bước 2] Thông báo:
    📧 Email → an@email.com
       Tiêu đề: Đặt hàng thành công!
       Nội dung: Đơn ORD-001 đã xác nhận. Tổng: 25,000,000đ

  ✅ Đơn hàng ORD-001 hoàn tất!

═══ KỊCH BẢN 2: SMS + MoMo ═══
  ...
```

---

> **💡 Tổng kết:** 5 ví dụ trên bao phủ các kỹ thuật interface nâng cao quan trọng nhất. Hãy chạy từng ví dụ và thử **thay đổi** implementation để thấy sức mạnh của interface! 🚀
