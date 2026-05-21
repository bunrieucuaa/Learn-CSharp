# 🏆 Bài 22: Challenge — E-Learning Platform 🎓

> **Dự án lớn:** Xây dựng nền tảng học trực tuyến với abstract classes và interfaces!

---

## 📋 Mô tả dự án

Bạn là developer cho một nền tảng E-Learning. Hệ thống cần quản lý các **khóa học**, **nội dung học**, **người dùng**, và **chứng chỉ**. Mỗi loại có hành vi khác nhau nhưng tuân theo các contract chung (interfaces).

---

## 🏗️ Kiến trúc

```
  Interfaces:
  ┌─────────────────────────────────────────────────────┐
  │ IProgressTrackable   IGradable    ICertificatable   │
  │ IPlayable           IDownloadable ISearchable       │
  └─────────────────────────────────────────────────────┘

  Abstract Classes + Concrete Classes:
  ┌──────────────────────────────────────────────────────┐
  │ abstract User                                        │
  │ ├── Student (IProgressTrackable)                     │
  │ ├── Instructor                                       │
  │ └── Admin                                            │
  │                                                      │
  │ abstract Content (IPlayable)                         │
  │ ├── VideoLesson (IDownloadable)                      │
  │ ├── TextLesson                                       │
  │ ├── Quiz (IGradable)                                 │
  │ └── Assignment (IGradable, IDownloadable)            │
  │                                                      │
  │ Course (ICertificatable, ISearchable)                │
  │ Certificate                                           │
  │ LearningPlatform                                     │
  └──────────────────────────────────────────────────────┘
```

---

## 📝 Yêu cầu chi tiết

### Phần 1: Interfaces

```csharp
interface IProgressTrackable
{
    double GetProgressPercent();       // 0-100%
    int GetCompletedLessons();
    int GetTotalLessons();
    void UpdateProgress(int lessonId);
}

interface IGradable
{
    int MaxScore { get; }
    int Score { get; }
    double GetGradePercent();          // Score/MaxScore * 100
    string GetGradeLetter();           // A, B, C, D, F
    bool IsPassed();                   // >= 60%
}

interface ICertificatable
{
    bool IsEligibleForCertificate();
    string GenerateCertificate(string studentName);
}

interface IPlayable
{
    bool IsCompleted { get; }
    void Start();
    void Complete();
    int GetDurationMinutes();
}

interface IDownloadable
{
    string FileName { get; }
    double FileSizeMb { get; }
    void Download();
    string GetDownloadUrl();
}

interface ISearchable
{
    string[] GetKeywords();
    bool MatchesSearch(string query);
}
```

### Phần 2: Abstract class `User`

```csharp
abstract class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime JoinDate { get; set; }

    protected User(int id, string name, string email) { ... }

    public abstract string GetRole();
    public abstract void ShowDashboard();

    // Concrete
    public void ShowProfile() { ... }
}
```

**Concrete Users:**

- **Student** : User, IProgressTrackable
  - Thêm: `EnrolledCourses` (Course[]), `CompletedLessons` (int[])
  - `ShowDashboard()`: Hiển thị courses đang học, progress, điểm
  - Implement IProgressTrackable

- **Instructor** : User
  - Thêm: `Courses` (Course[]) — các khóa học đang dạy
  - `ShowDashboard()`: Hiển thị courses, số student, ratings

- **Admin** : User
  - `ShowDashboard()`: Hiển thị tổng users, courses, revenue

### Phần 3: Abstract class `Content`

```csharp
abstract class Content : IPlayable
{
    public int ContentId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int OrderInCourse { get; set; }
    public bool IsCompleted { get; set; }

    protected Content(int id, string title, string desc) { ... }

    public abstract void Start();
    public abstract void Complete();
    public abstract int GetDurationMinutes();
    public abstract string GetContentType();

    // Concrete
    public void ShowContentInfo() { ... }
}
```

**Concrete Content:**

| Class | Đặc điểm | Interfaces thêm |
|-------|----------|-----------------|
| VideoLesson | VideoUrl, Resolution, DurationMin | IDownloadable |
| TextLesson | HtmlContent, ReadTimeMinutes | — |
| Quiz | Questions[], MaxScore, Score | IGradable |
| Assignment | Instructions, Deadline, FileSubmission | IGradable, IDownloadable |

### Phần 4: Class `Course`

```csharp
class Course : ICertificatable, ISearchable
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public string InstructorName { get; set; }
    
    private Content[] contents;
    private int contentCount;
    private string[] keywords;

    public void AddContent(Content content) { }
    public Content[] GetAllContent() { }
    public int GetTotalDuration() { }

    // ICertificatable
    public bool IsEligibleForCertificate() { }
    public string GenerateCertificate(string studentName) { }

    // ISearchable
    public string[] GetKeywords() { }
    public bool MatchesSearch(string query) { }
}
```

### Phần 5: Class `LearningPlatform`

```csharp
class LearningPlatform
{
    private string PlatformName;
    private User[] Users;
    private Course[] Courses;

    public void AddUser(User user) { }
    public void AddCourse(Course course) { }

    // Polymorphism — xử lý tất cả users
    public void ShowAllDashboards() { }

    // Interface-based operations
    public void TrackAllProgress() { }          // IProgressTrackable
    public void GradeAllAssignments() { }       // IGradable
    public void GenerateCertificates() { }      // ICertificatable
    public Course[] SearchCourses(string q) { } // ISearchable

    // Thống kê
    public void ShowPlatformStats() { }
}
```

---

## 🎮 Main Program — Mô phỏng

```csharp
static void Main()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    // 1. Tạo platform
    LearningPlatform platform = new LearningPlatform("LearnHub", 50, 20);

    // 2. Tạo Instructors
    Instructor teacher1 = new Instructor(1, "Nguyễn Văn A", "teacher@mail.com");
    Instructor teacher2 = new Instructor(2, "Trần Thị B", "teacher2@mail.com");
    platform.AddUser(teacher1);
    platform.AddUser(teacher2);

    // 3. Tạo Courses với Content
    Course csharpCourse = new Course(1, "C# Từ Zero", "Programming", 500000,
                                     "Nguyễn Văn A");
    csharpCourse.AddContent(new VideoLesson(1, "Giới thiệu C#",
        "Video giới thiệu", "video1.mp4", 15, 1080));
    csharpCourse.AddContent(new TextLesson(2, "Biến và kiểu dữ liệu",
        "Bài viết chi tiết", "<p>Nội dung...</p>", 10));
    csharpCourse.AddContent(new Quiz(3, "Quiz: Biến",
        "Kiểm tra kiến thức", 10, 100));
    csharpCourse.AddContent(new Assignment(4, "Bài tập: Calculator",
        "Viết máy tính", "assignment1.pdf", 2.5));
    platform.AddCourse(csharpCourse);

    // 4. Tạo Students
    Student student1 = new Student(3, "Lê Văn C", "student@mail.com");
    Student student2 = new Student(4, "Phạm Thị D", "student2@mail.com");
    platform.AddUser(student1);
    platform.AddUser(student2);

    // 5. Student học bài
    Console.WriteLine("═══ HỌC BÀI ═══");
    // Student hoàn thành video, text, quiz...

    // 6. Hiển thị dashboard cho tất cả users
    Console.WriteLine("═══ DASHBOARDS ═══");
    platform.ShowAllDashboards();  // Polymorphism!

    // 7. Track progress
    Console.WriteLine("═══ PROGRESS ═══");
    platform.TrackAllProgress();

    // 8. Grade assignments
    Console.WriteLine("═══ CHẤM ĐIỂM ═══");
    platform.GradeAllAssignments();

    // 9. Search courses
    Console.WriteLine("═══ TÌM KIẾM ═══");
    Course[] results = platform.SearchCourses("C#");

    // 10. Generate certificates
    Console.WriteLine("═══ CHỨNG CHỈ ═══");
    platform.GenerateCertificates();

    // 11. Thống kê
    Console.WriteLine("═══ THỐNG KÊ PLATFORM ═══");
    platform.ShowPlatformStats();
}
```

---

## ✅ Output mong đợi (tham khảo)

```
╔══════════════════════════════════════════╗
║   🎓 LEARNHUB — E-LEARNING PLATFORM     ║
╚══════════════════════════════════════════╝

═══ DASHBOARDS ═══

📋 INSTRUCTOR Dashboard — Nguyễn Văn A
  📚 Khóa học đang dạy: 1
  👥 Tổng học viên: 2
  ⭐ Đánh giá: 4.5/5

📋 STUDENT Dashboard — Lê Văn C
  📚 Khóa đang học: 1
  📊 Tiến độ: C# Từ Zero [████████░░] 75%
  🏆 Chứng chỉ: 0

═══ PROGRESS ═══
  Lê Văn C → C# Từ Zero: 75% (3/4 bài hoàn thành)
  Phạm Thị D → C# Từ Zero: 50% (2/4 bài hoàn thành)

═══ CHẤM ĐIỂM ═══
  Quiz "Quiz: Biến": Lê Văn C — 85/100 (B) ✅ Đạt
  Assignment "Bài tập: Calculator": Lê Văn C — 90/100 (A) ✅ Đạt

═══ TÌM KIẾM "C#" ═══
  ✅ C# Từ Zero (500,000đ) — 4 bài học, 40 phút

═══ CHỨNG CHỈ ═══
  🎓 Chứng chỉ cho Lê Văn C
     Khóa: C# Từ Zero
     Hoàn thành: 20/05/2026
     Điểm TB: 87.5%

═══ THỐNG KÊ PLATFORM ═══
  👥 Tổng users: 4 (2 Instructors, 2 Students)
  📚 Tổng courses: 1
  📝 Tổng content: 4 (1 Video, 1 Text, 1 Quiz, 1 Assignment)
  💰 Doanh thu: 1,000,000đ
```

---

## 🎯 Tiêu chí đánh giá

| Tiêu chí | Điểm | Mô tả |
|-----------|-------|--------|
| Abstract classes | 20% | User & Content abstract đúng cách |
| Interfaces | 25% | Implement đúng 6 interfaces |
| Polymorphism | 20% | Mảng đa hình, pattern matching |
| Logic hoạt động | 20% | Course, progress, grading chạy đúng |
| Code quality | 15% | Clean, có comment, naming convention |

---

## 💡 Bonus (tùy chọn)

1. **Thêm loại Content:** LiveSession, PodcastLesson
2. **Rating system:** Students rate courses (interface IRatable)
3. **Discussion forum:** Students comment bài học
4. **Enrollment system:** Track enrollment date, completion date
5. **Export:** Implement IExportable cho Certificate (export ra text)

> **Mục tiêu:** Hoàn thành trong 90-120 phút. Đây là bài tổng hợp lớn nhất — dùng TẤT CẢ kiến thức OOP từ Bài 15-22! 🚀
