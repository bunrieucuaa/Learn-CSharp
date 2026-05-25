# ✏️ Bài 30: CRUD Console Project — Bài tập mở rộng

> **3 bài tập nâng cấp dự án CRUD — mỗi bài thêm tính năng mới vào hệ thống Student Management.**

---

## Bài 1: Thêm Search & Filter nâng cao 🔍

> **Kỹ năng:** IEnumerable + yield return + business logic

### Yêu cầu

Mở rộng `StudentService` và `ConsoleUI` để hỗ trợ:

```
Tính năng mới trong StudentService:
├── IEnumerable<Student> SearchAdvanced(string? name, int? minAge,
│       int? maxAge, double? minScore, double? maxScore, string? grade)
│   → Kết hợp nhiều điều kiện filter (các tham số null = bỏ qua)
│   → Dùng yield return
│
├── IEnumerable<Student> GetTopN(int n)
│   → Top N sinh viên điểm cao nhất
│
├── IEnumerable<Student> GetBottomN(int n)
│   → N sinh viên điểm thấp nhất
│
├── IEnumerable<Student> GetByAgeRange(int min, int max)
│   → Lọc theo khoảng tuổi
│
├── IEnumerable<IGrouping> GroupByGrade()
│   → Nhóm sinh viên theo xếp loại (dùng Dictionary<string, List<Student>>)
│   → Trả về IEnumerable<KeyValuePair<string, List<Student>>>
│
└── IEnumerable<Student> GetPage(int page, int pageSize)
    → Phân trang: Skip + Take logic
```

### Menu UI mới:

```
╔══════════════════════════════════════╗
║   🔍 TÌM KIẾM NÂNG CAO             ║
╠══════════════════════════════════════╣
║  1. Tìm theo tên                     ║
║  2. Tìm theo khoảng điểm            ║
║  3. Tìm theo khoảng tuổi            ║
║  4. Tìm theo xếp loại               ║
║  5. Tìm kết hợp (nâng cao)          ║
║  6. Top N sinh viên                  ║
║  7. Xem theo nhóm xếp loại          ║
║  8. Xem theo trang (10 SV/trang)    ║
║  0. Quay lại menu chính             ║
╚══════════════════════════════════════╝
```

### Output mong đợi cho "Tìm kết hợp":

```
  ══ TÌM KIẾM KẾT HỢP ══
  Tên chứa (Enter=bỏ qua): Nguyễn
  Tuổi từ (Enter=bỏ qua): 19
  Tuổi đến (Enter=bỏ qua): 21
  Điểm từ (Enter=bỏ qua): 7
  Điểm đến (Enter=bỏ qua):
  Xếp loại (Enter=bỏ qua):

  Kết quả (2 sinh viên):
  1    │ Nguyễn Văn An          │   20 │   8.5 │ ✅ Giỏi
  ...
```

### Output mong đợi cho "Nhóm theo xếp loại":

```
  ══ PHÂN NHÓM THEO XẾP LOẠI ══

  🌟 XUẤT SẮC (2 SV):
    → Phạm Minh Đức (9.5)
    → Trần Thị Bình (9.2)

  ✅ GIỎI (2 SV):
    → Vũ Quang Phúc (8.8)
    → Nguyễn Văn An (8.5)

  👍 KHÁ (2 SV):
    → Ngô Thị Linh (7.5)
    → Lê Hoàng Cường (7.0)
  ...
```

---

## Bài 2: Import / Export (Serialize to String) 📂

> **Kỹ năng:** String processing, IEnumerable, data transformation

### Yêu cầu

Thêm khả năng **import** và **export** dữ liệu dạng CSV (text-based):

```
Tính năng mới:

static class DataExporter:
├── IEnumerable<string> ToCsv(IEnumerable<Student> students)
│   → yield return header + mỗi dòng CSV cho 1 sinh viên
│   → Format: "Id,Name,Age,Score,Email"
│   → Ví dụ: "1,Nguyễn Văn An,20,8.5,an@email.com"
│
├── string ToTable(IEnumerable<Student> students)
│   → Trả về string dạng bảng đẹp (dùng StringBuilder)
│
└── IEnumerable<string> ToJson(IEnumerable<Student> students)
    → yield return dạng JSON thủ công (không dùng thư viện)
    → Ví dụ: {"Id":1,"Name":"Nguyễn Văn An","Age":20,"Score":8.5}

static class DataImporter:
├── IEnumerable<Student> FromCsv(IEnumerable<string> lines)
│   → Parse CSV lines → yield return Student objects
│   → Bỏ qua header line
│   → Bỏ qua dòng lỗi (log warning)
│
└── (int Success, int Failed, string[] Errors) ImportBatch(
        StudentService service, IEnumerable<string> csvLines)
    → Import nhiều sinh viên, đếm success/fail
```

### Menu UI mới:

```
╔══════════════════════════════════════╗
║   📂 IMPORT / EXPORT                 ║
╠══════════════════════════════════════╣
║  1. Export ra CSV (hiển thị)         ║
║  2. Export ra JSON (hiển thị)        ║
║  3. Export ra bảng đẹp               ║
║  4. Import từ CSV (nhập text)        ║
║  0. Quay lại                         ║
╚══════════════════════════════════════╝
```

### Output mong đợi — Export CSV:

```
  ══ EXPORT CSV ══
  Id,Name,Age,Score,Email
  1,Nguyễn Văn An,20,8.5,an@email.com
  2,Trần Thị Bình,19,9.2,binh@email.com
  3,Lê Hoàng Cường,21,7.0,cuong@email.com
  ...
  ── Tổng: 10 dòng ──
```

### Output mong đợi — Import CSV:

```
  ══ IMPORT CSV ══
  Nhập dữ liệu CSV (mỗi dòng 1 sinh viên, gõ 'done' để kết thúc):

  > Trần Văn Test,22,7.5,test@email.com
  > Lê Thị Demo,20,abc,demo@email.com
  > done

  Kết quả import:
  ✅ Thành công: 1
  ❌ Lỗi: 1
  Lỗi chi tiết:
    Dòng 2: "Lê Thị Demo,20,abc,demo@email.com" → Điểm không hợp lệ
```

### Output mong đợi — Export JSON:

```
  ══ EXPORT JSON ══
  [
    {"Id":1,"Name":"Nguyễn Văn An","Age":20,"Score":8.5,"Email":"an@email.com"},
    {"Id":2,"Name":"Trần Thị Bình","Age":19,"Score":9.2,"Email":"binh@email.com"},
    ...
  ]
```

---

## Bài 3: Statistics & Reporting 📊

> **Kỹ năng:** Data aggregation, IEnumerable, formatted output

### Yêu cầu

Thêm module thống kê và báo cáo chi tiết:

```
class ReportService:
├── Constructor(StudentService service)
│
├── void PrintScoreDistribution()
│   → Phân bố điểm (histogram):
│   → [0-1): 0  [1-2): 0  [2-3): 0  [3-4): 1  [4-5): 1
│   → [5-6): 1  [6-7): 1  [7-8): 2  [8-9): 2  [9-10]: 2
│   → Vẽ biểu đồ ASCII ngang
│
├── void PrintAgeReport()
│   → Thống kê theo nhóm tuổi
│   → Trung bình điểm theo tuổi
│
├── void PrintGradeReport()
│   → Tỷ lệ % từng xếp loại
│   → Biểu đồ tròn dạng text
│
├── void PrintComparisonReport(string grade1, string grade2)
│   → So sánh 2 nhóm xếp loại
│   → Điểm TB, số lượng, min, max
│
├── void PrintTopBottomReport(int n)
│   → Top N và Bottom N side-by-side
│
├── IEnumerable<string> GenerateFullReport()
│   → yield return từng dòng báo cáo tổng hợp
│   → Bao gồm tất cả thống kê trên
│
└── void PrintSummaryCard(int studentId)
    → In "thẻ" thông tin 1 sinh viên dạng box
```

### Menu UI mới:

```
╔══════════════════════════════════════╗
║   📊 THỐNG KÊ & BÁO CÁO            ║
╠══════════════════════════════════════╣
║  1. Phân bố điểm (histogram)        ║
║  2. Thống kê theo tuổi              ║
║  3. Thống kê theo xếp loại          ║
║  4. So sánh hai nhóm                 ║
║  5. Top & Bottom N                   ║
║  6. Thẻ sinh viên                    ║
║  7. Báo cáo tổng hợp                ║
║  0. Quay lại                         ║
╚══════════════════════════════════════╝
```

### Output mong đợi — Phân bố điểm:

```
  ══ PHÂN BỐ ĐIỂM ══
  
  [0-1)  : 0  │
  [1-2)  : 0  │
  [2-3)  : 0  │
  [3-4)  : 1  │ █
  [4-5)  : 1  │ █
  [5-6)  : 0  │
  [6-7)  : 1  │ █
  [7-8)  : 2  │ ██
  [8-9)  : 2  │ ██
  [9-10] : 3  │ ███
              └──────────
  Tổng: 10 SV | TB: 7.30
```

### Output mong đợi — Thẻ sinh viên:

```
  ╔════════════════════════════════╗
  ║  🎓 THẺ SINH VIÊN              ║
  ╠════════════════════════════════╣
  ║  ID:        4                  ║
  ║  Họ tên:    Phạm Minh Đức     ║
  ║  Tuổi:      20                 ║
  ║  Email:     duc@email.com      ║
  ║  Điểm:      9.5               ║
  ║  Xếp loại:  🌟 Xuất sắc       ║
  ║  Xếp hạng:  #1/10             ║
  ║  Ngày tạo:  25/05/2024        ║
  ╚════════════════════════════════╝
```

### Output mong đợi — So sánh:

```
  ══ SO SÁNH: Xuất sắc vs Giỏi ══
  
  ┌──────────────┬─────────────┬─────────────┐
  │              │ 🌟 Xuất sắc │ ✅ Giỏi     │
  ├──────────────┼─────────────┼─────────────┤
  │ Số lượng     │     2       │     2       │
  │ Điểm TB      │    9.35     │    8.65     │
  │ Điểm cao nhất│    9.5      │    8.8      │
  │ Điểm thấp nhất│   9.2      │    8.5      │
  │ Tuổi TB      │   19.5      │   19.5      │
  └──────────────┴─────────────┴─────────────┘
```

---

## 📊 Bảng tổng hợp bài tập

| Bài | Chủ đề | Kiến thức chính | Độ khó |
|-----|--------|----------------|--------|
| 1 | Search & Filter | IEnumerable, yield, multi-filter | ⭐⭐⭐ |
| 2 | Import / Export | String processing, CSV parse, yield | ⭐⭐⭐ |
| 3 | Statistics & Report | Data aggregation, formatted output | ⭐⭐⭐⭐ |

---

## 💡 Hướng dẫn thực hiện

```
Bước 1: Chạy được code từ examples.md trước
Bước 2: Chọn 1 bài tập, đọc kỹ yêu cầu
Bước 3: Thêm method mới vào Service (business logic)
Bước 4: Thêm menu mới vào ConsoleUI
Bước 5: Test từng tính năng
Bước 6: Polish output format

💡 Mẹo: Bắt đầu với Bài 1 (Search) → Bài 3 (Stats) → Bài 2 (Import/Export)
Bài 1 đơn giản nhất vì chỉ thêm filter methods.
Bài 2 cần xử lý string phức tạp hơn.
Bài 3 cần tính toán và format output nhiều nhất.
```

---

> **💡 Mẹo:** Mỗi bài tập là một "module" độc lập — bạn có thể làm riêng hoặc kết hợp tất cả vào 1 project! 🚀
