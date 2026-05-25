# 🏆 Bài 30: FINAL CHALLENGE — Library Management System 📚

> **Dự án cuối Phase 3:** Xây dựng hệ thống Quản lý Thư viện hoàn chỉnh — tổng hợp TẤT CẢ kiến thức từ Phase 1 đến Phase 3!

---

## 📋 Mô tả dự án

Bạn đang xây dựng **Library Management System** — hệ thống quản lý thư viện với đầy đủ chức năng: quản lý sách, quản lý thành viên, mượn/trả sách, tìm kiếm, theo dõi quá hạn, thống kê.

---

## 🏗️ Kiến trúc

```
┌────────────────────────────────────────────────────────────────┐
│                  LIBRARY MANAGEMENT SYSTEM                     │
│                                                                │
│  ┌──────────────── MODELS ────────────────┐                   │
│  │  IEntity                               │                   │
│  │  ├── Book (Id, Title, Author, ISBN,    │                   │
│  │  │         Genre, Year, Copies, Avail) │                   │
│  │  ├── Member (Id, Name, Email, Phone,   │                   │
│  │  │          JoinDate, MemberType)      │                   │
│  │  └── BorrowRecord (Id, BookId,         │                   │
│  │                     MemberId, BorrowDate,                  │
│  │                     DueDate, ReturnDate,                   │
│  │                     Status)            │                   │
│  └────────────────────────────────────────┘                   │
│                        │                                       │
│  ┌──────────── REPOSITORIES ──────────────┐                   │
│  │  IRepository<T>                        │                   │
│  │  ├── GenericRepository<T>              │                   │
│  │  │   (List<T> + Dictionary for lookup) │                   │
│  │  ├── BookRepository : GenericRepo<Book>│                   │
│  │  │   + SearchByTitle, SearchByAuthor,  │                   │
│  │  │   + GetByGenre, GetAvailable        │                   │
│  │  └── BorrowRepository                  │                   │
│  │      + GetByMember, GetOverdue         │                   │
│  └────────────────────────────────────────┘                   │
│                        │                                       │
│  ┌──────────── SERVICES ──────────────────┐                   │
│  │  BookService    → CRUD sách            │                   │
│  │  MemberService  → CRUD thành viên      │                   │
│  │  BorrowService  → Mượn/trả/gia hạn    │                   │
│  │  ReportService  → Thống kê, báo cáo   │                   │
│  └────────────────────────────────────────┘                   │
│                        │                                       │
│  ┌──────────── UI ────────────────────────┐                   │
│  │  ConsoleUI → Menu chính + Sub-menus    │                   │
│  └────────────────────────────────────────┘                   │
│                                                                │
│  Program.cs → Composition Root                                 │
└────────────────────────────────────────────────────────────────┘
```

---

## 📝 Yêu cầu chi tiết

### Phần 1: Models

```csharp
// ═══ Base Entity ═══
interface IEntity
{
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
}

// ═══ BOOK ═══
class Book : IEntity, IComparable<Book>
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Title { get; set; }          // Tiêu đề
    public string Author { get; set; }         // Tác giả
    public string ISBN { get; set; }           // Mã ISBN (unique)
    public string Genre { get; set; }          // "Fiction", "Science", "History",
                                               // "Technology", "Literature"
    public int PublishYear { get; set; }        // Năm xuất bản
    public int TotalCopies { get; set; }        // Tổng số bản
    public int AvailableCopies { get; set; }    // Số bản còn trống

    // Computed
    public bool IsAvailable => AvailableCopies > 0;
    public int BorrowedCopies => TotalCopies - AvailableCopies;

    // IComparable — Sort theo Title A-Z
    public int CompareTo(Book? other) { ... }

    // Validation trong properties
    // ToString() hiển thị đẹp
}

// ═══ MEMBER ═══
class Member : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime JoinDate { get; set; }
    public string MemberType { get; set; }     // "Student", "Teacher", "Regular"

    // Computed
    public int MaxBooksAllowed => MemberType switch
    {
        "Teacher" => 10,
        "Student" => 5,
        _         => 3
    };

    public int BorrowDays => MemberType switch
    {
        "Teacher" => 30,
        "Student" => 14,
        _         => 7
    };
}

// ═══ BORROW RECORD ═══
class BorrowRecord : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }   // null = chưa trả

    // Status
    public string Status => ReturnDate.HasValue ? "Returned"
        : DateTime.Now > DueDate ? "Overdue"
        : "Borrowed";

    public bool IsOverdue => !ReturnDate.HasValue && DateTime.Now > DueDate;
    public int DaysOverdue => IsOverdue
        ? (DateTime.Now - DueDate).Days : 0;
    public decimal Fine => DaysOverdue * 5000m;   // 5,000đ/ngày quá hạn
}
```

### Phần 2: Repositories

```csharp
// Generic Repository — tái sử dụng từ examples.md
class GenericRepository<T> : IRepository<T> where T : IEntity
{
    // Add, GetById, GetAll, Update, Delete, Find
    // Dùng List<T> + Dictionary<int, T>
}

// Book Repository — thêm methods đặc biệt
class BookRepository : GenericRepository<Book>
{
    private Dictionary<string, Book> _isbnIndex;   // Lookup bằng ISBN

    public Book? GetByISBN(string isbn) { ... }

    public IEnumerable<Book> SearchByTitle(string keyword)
    {
        // yield return — tìm theo title chứa keyword
    }

    public IEnumerable<Book> SearchByAuthor(string keyword)
    {
        // yield return
    }

    public IEnumerable<Book> GetByGenre(string genre)
    {
        // yield return
    }

    public IEnumerable<Book> GetAvailable()
    {
        // yield return — chỉ sách còn bản trống
    }
}
```

### Phần 3: Services

```csharp
// ═══ BOOK SERVICE ═══
class BookService
{
    private readonly BookRepository _bookRepo;

    // CRUD
    public (bool, string) AddBook(string title, string author, string isbn,
        string genre, int year, int copies) { ... }
    public Book? GetBook(int id) { ... }
    public IEnumerable<Book> GetAllBooks() { ... }
    public (bool, string) UpdateBook(int id, ...) { ... }
    public (bool, string) DeleteBook(int id) { ... }

    // Search
    public IEnumerable<Book> SearchBooks(string keyword) { ... }
    public IEnumerable<Book> GetByGenre(string genre) { ... }
    public IEnumerable<Book> GetAvailableBooks() { ... }
}

// ═══ MEMBER SERVICE ═══
class MemberService
{
    private readonly GenericRepository<Member> _memberRepo;
    // CRUD + Search tương tự BookService
}

// ═══ BORROW SERVICE ═══  (QUAN TRỌNG NHẤT!)
class BorrowService
{
    private readonly GenericRepository<BorrowRecord> _borrowRepo;
    private readonly BookRepository _bookRepo;
    private readonly GenericRepository<Member> _memberRepo;

    // Mượn sách
    public (bool, string) BorrowBook(int memberId, int bookId)
    {
        // 1. Kiểm tra member tồn tại
        // 2. Kiểm tra book tồn tại + còn bản trống
        // 3. Kiểm tra member chưa vượt quá giới hạn mượn
        // 4. Kiểm tra member không có sách quá hạn
        // 5. Tạo BorrowRecord
        // 6. Giảm AvailableCopies của book
        // 7. Trả kết quả
    }

    // Trả sách
    public (bool, string) ReturnBook(int borrowId)
    {
        // 1. Tìm BorrowRecord
        // 2. Cập nhật ReturnDate
        // 3. Tăng AvailableCopies
        // 4. Tính phạt nếu quá hạn
    }

    // Gia hạn
    public (bool, string) ExtendBorrow(int borrowId, int extraDays)
    {
        // Gia hạn DueDate — chỉ khi chưa quá hạn
    }

    // Query
    public IEnumerable<BorrowRecord> GetMemberBorrows(int memberId) { ... }
    public IEnumerable<BorrowRecord> GetOverdueRecords() { ... }
    public IEnumerable<BorrowRecord> GetActiveBorrows() { ... }

    // Kiểm tra
    public int GetMemberBorrowCount(int memberId) { ... }
    public bool HasOverdueBooks(int memberId) { ... }
}

// ═══ REPORT SERVICE ═══
class ReportService
{
    // Thống kê sách
    public void PrintBookStats()
    {
        // Tổng sách, theo thể loại, sách phổ biến nhất
    }

    // Thống kê mượn/trả
    public void PrintBorrowStats()
    {
        // Tổng lượt mượn, đang mượn, quá hạn, tổng phạt
    }

    // Thống kê thành viên
    public void PrintMemberStats()
    {
        // Active members, top members mượn nhiều nhất
    }

    // Sách quá hạn
    public void PrintOverdueReport()
    {
        // Danh sách chi tiết: ai mượn, sách gì, quá bao lâu, phạt
    }
}
```

### Phần 4: Console UI

```csharp
class ConsoleUI
{
    // ═══ MAIN MENU ═══
    // ╔══════════════════════════════════════╗
    // ║   📚 LIBRARY MANAGEMENT SYSTEM      ║
    // ╠══════════════════════════════════════╣
    // ║  1. 📖 Quản lý Sách                  ║
    // ║  2. 👤 Quản lý Thành viên            ║
    // ║  3. 📋 Mượn / Trả sách              ║
    // ║  4. 🔍 Tìm kiếm                     ║
    // ║  5. 📊 Thống kê & Báo cáo           ║
    // ║  0. 🚪 Thoát                         ║
    // ╚══════════════════════════════════════╝

    // ═══ SUB-MENU: Quản lý Sách ═══
    // 1. Thêm sách
    // 2. Xem tất cả sách
    // 3. Tìm sách
    // 4. Cập nhật sách
    // 5. Xóa sách
    // 0. Quay lại

    // ═══ SUB-MENU: Mượn/Trả ═══
    // 1. Mượn sách
    // 2. Trả sách
    // 3. Gia hạn
    // 4. Xem sách đang mượn (của 1 thành viên)
    // 5. Xem tất cả sách quá hạn
    // 0. Quay lại
}
```

---

## 🎮 Luồng hoạt động mẫu

### Mượn sách:

```
  ══ MƯỢN SÁCH ══
  Nhập ID thành viên: 1
  → Thành viên: Nguyễn Văn An (Student, đang mượn 2/5 sách)

  Nhập ID sách: 3
  → Sách: "Clean Code" — Robert C. Martin (Còn 2/3 bản)

  Xác nhận mượn? (y/n): y

  ✅ Đã mượn thành công!
  ┌──────────────────────────────────────┐
  │  📋 PHIẾU MƯỢN                       │
  │  Mã phiếu:    BR-015                 │
  │  Thành viên:   Nguyễn Văn An         │
  │  Sách:         Clean Code             │
  │  Ngày mượn:    25/05/2024            │
  │  Hạn trả:      08/06/2024 (14 ngày)  │
  │  Sách còn lại: 1/3 bản              │
  └──────────────────────────────────────┘
```

### Trả sách:

```
  ══ TRẢ SÁCH ══
  Nhập ID phiếu mượn: 12

  📋 Thông tin phiếu mượn BR-012:
  Sách: "Head First Design Patterns"
  Mượn: 10/05/2024 → Hạn: 24/05/2024
  ⚠️  QUÁ HẠN 1 NGÀY!

  Trả sách và chấp nhận phạt 5,000đ? (y/n): y

  ✅ Đã trả sách!
  ⚠️  Phạt quá hạn: 5,000đ (1 ngày × 5,000đ/ngày)
```

### Báo cáo quá hạn:

```
  ══ BÁO CÁO SÁCH QUÁ HẠN ══

  ⚠️ Có 3 phiếu mượn quá hạn:

  ┌──────┬─────────────────────┬─────────────────────┬──────────┬──────────┐
  │ Phiếu│ Thành viên          │ Sách                │ Quá hạn  │ Phạt     │
  ├──────┼─────────────────────┼─────────────────────┼──────────┼──────────┤
  │ BR-05│ Trần Thị Bình       │ Algorithms          │ 5 ngày   │ 25,000đ  │
  │ BR-08│ Lê Hoàng Cường      │ Design Patterns     │ 3 ngày   │ 15,000đ  │
  │ BR-12│ Phạm Minh Đức       │ Clean Code          │ 1 ngày   │  5,000đ  │
  └──────┴─────────────────────┴─────────────────────┴──────────┴──────────┘
  Tổng phạt: 45,000đ
```

---

## ✅ Seed Data mẫu

```csharp
static void SeedData(BookService bookSvc, MemberService memberSvc,
                     BorrowService borrowSvc)
{
    // ═══ SÁCH ═══
    bookSvc.AddBook("Clean Code", "Robert C. Martin", "978-0132350884",
        "Technology", 2008, 3);
    bookSvc.AddBook("Design Patterns", "Gang of Four", "978-0201633610",
        "Technology", 1994, 2);
    bookSvc.AddBook("Dune", "Frank Herbert", "978-0441172719",
        "Fiction", 1965, 4);
    bookSvc.AddBook("Sapiens", "Yuval Noah Harari", "978-0062316097",
        "History", 2011, 3);
    bookSvc.AddBook("The Pragmatic Programmer", "Hunt & Thomas", "978-0135957059",
        "Technology", 2019, 2);
    bookSvc.AddBook("1984", "George Orwell", "978-0451524935",
        "Literature", 1949, 5);
    bookSvc.AddBook("Cosmos", "Carl Sagan", "978-0345539434",
        "Science", 1980, 2);
    bookSvc.AddBook("Algorithms", "Robert Sedgewick", "978-0321573513",
        "Technology", 2011, 2);

    // ═══ THÀNH VIÊN ═══
    memberSvc.AddMember("Nguyễn Văn An", "an@email.com", "0901234567", "Student");
    memberSvc.AddMember("Trần Thị Bình", "binh@email.com", "0912345678", "Student");
    memberSvc.AddMember("Lê Hoàng Cường", "cuong@email.com", "0923456789", "Teacher");
    memberSvc.AddMember("Phạm Minh Đức", "duc@email.com", "0934567890", "Regular");

    // ═══ MƯỢN SÁCH ═══ (một số đã quá hạn để test)
    borrowSvc.BorrowBook(1, 1);   // An mượn Clean Code
    borrowSvc.BorrowBook(1, 3);   // An mượn Dune
    borrowSvc.BorrowBook(2, 8);   // Bình mượn Algorithms (sẽ set quá hạn)
    borrowSvc.BorrowBook(3, 2);   // Cường mượn Design Patterns
}
```

---

## 🎯 Tiêu chí đánh giá

| Tiêu chí | Điểm | Mô tả |
|-----------|-------|--------|
| **Models** (3 classes) | 15% | Book, Member, BorrowRecord — encapsulated, validated |
| **Generic Repository** | 15% | IRepository\<T\>, GenericRepository\<T\> — reusable |
| **CRUD Operations** | 15% | Add, Read, Update, Delete cho Books + Members |
| **Borrow/Return** | 20% | Business logic: mượn, trả, gia hạn, check giới hạn |
| **Search & Filter** | 10% | IEnumerable + yield return cho queries |
| **Statistics** | 10% | Overdue tracking, thống kê, báo cáo |
| **Architecture** | 10% | 3-layer separation, DI qua constructor |
| **UX & Error Handling** | 5% | Menu đẹp, input validation, error messages |

---

## 💡 Must-use checklist

```
✅ Kiến thức bắt buộc sử dụng:

□ Generic Repository      → GenericRepository<T> reusable cho Book, Member, BorrowRecord
□ List<T>                 → Lưu trữ chính trong repository
□ Dictionary<K,V>         → Fast lookup: ID→Entity, ISBN→Book
□ IEnumerable<T>          → Return type cho query methods
□ yield return            → Lazy filtering trong Search, GetOverdue...
□ IComparable<T>          → Sort sách theo Title
□ Interface               → IEntity, IRepository<T>
□ Encapsulation           → Private fields + validated properties
□ Constructor DI          → Service nhận Repository qua constructor
□ Exception Handling      → Validate input, business rules
□ Pattern Matching        → switch expressions cho Grade, Status
□ Tuples                  → (bool Success, string Message) returns
```

---

## 💡 Bonus (tùy chọn)

1. **Reservation System:** Đặt trước sách khi hết → Queue\<Member\> cho mỗi sách
2. **History Log:** Stack\<string\> ghi lại lịch sử thao tác
3. **Member Rating:** Tính điểm uy tín dựa trên lịch sử trả sách đúng hạn
4. **Book Recommendations:** Gợi ý sách cùng thể loại/tác giả
5. **Multi-language Search:** Tìm kiếm không phân biệt dấu tiếng Việt

---

## 📅 Timeline gợi ý

```
Ngày 1 (2h): Models + Generic Repository
Ngày 2 (2h): Book Service + Member Service + UI cơ bản
Ngày 3 (2h): Borrow Service (mượn/trả) + UI
Ngày 4 (1h): Report Service + Statistics
Ngày 5 (1h): Polish + Seed data + Test
```

> **Mục tiêu:** Hoàn thành trong 6-8 giờ (chia 4-5 ngày). Đây là dự án CUỐI CÙNG của Phase 3! 🚀

---

*"The best way to learn is to build something real."* 🎯
