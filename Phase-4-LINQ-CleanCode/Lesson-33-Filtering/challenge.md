# Thách thức Bài 33: Bộ lọc Tìm kiếm Nâng cao cho Hệ thống Log hệ thống

## Ngữ cảnh
Hệ thống Gateway của công ty bạn ghi nhận lại toàn bộ nhật ký (Logs) truy cập của người dùng. Mỗi ngày hệ thống sinh ra hàng chục nghìn log. Hệ thống Admin Dashboard cần một chức năng tìm kiếm và phân trang log thông minh theo các điều kiện lọc linh hoạt từ Client gửi lên.

Nhiệm vụ của bạn là hiện thực hóa hàm tìm kiếm log này bằng các toán tử lọc của LINQ.

## Cấu trúc dữ liệu Log
```csharp
public class LogEntry
{
    public int Id { get; set; }
    public string Level { get; set; }     // "INFO", "WARNING", "ERROR"
    public string Message { get; set; }   // Nội dung log
    public string IpAddress { get; set; } // IP gửi yêu cầu
    public DateTime Timestamp { get; set; }
}
```

## Các điều kiện lọc cần đáp ứng
Bạn được yêu cầu viết một hàm lọc log có chữ ký (signature) như sau:
```csharp
public static List<LogEntry> FilterLogs(
    List<LogEntry> allLogs, 
    string? level, 
    string? searchKeyword, 
    int pageIndex, 
    int pageSize
)
```

**Quy tắc nghiệp vụ của hàm:**
1. **Lọc theo Level:** Nếu `level` truyền vào khác `null`, lọc theo đúng level đó. Nếu `level` là `null`, lấy tất cả các level.
2. **Lọc theo Keyword:** Nếu `searchKeyword` truyền vào khác `null` hoặc không rỗng, lọc ra các log mà thuộc tính `Message` hoặc `IpAddress` chứa keyword đó (không phân biệt chữ hoa chữ thường).
3. **Phân trang:** Trả về đúng số lượng log cho trang `pageIndex` (1-indexed) với kích thước `pageSize`.
4. **Hiệu năng:** Đảm bảo toàn bộ quá trình lọc (Level + Keyword) được thực hiện **trước** khi phân trang (`Skip` và `Take`), để tránh phân trang sai lệch dữ liệu.

## Dữ liệu giả lập (Mock Data) test thử
Hãy viết hàm trên và chạy thử với danh sách mock data sau:
```csharp
List<LogEntry> mockLogs = [
    new LogEntry { Id = 1, Level = "INFO", Message = "User login successful", IpAddress = "192.168.1.1", Timestamp = DateTime.Now },
    new LogEntry { Id = 2, Level = "WARNING", Message = "Disk usage at 85%", IpAddress = "127.0.0.1", Timestamp = DateTime.Now },
    new LogEntry { Id = 3, Level = "ERROR", Message = "Database connection timed out", IpAddress = "10.0.0.5", Timestamp = DateTime.Now },
    new LogEntry { Id = 4, Level = "INFO", Message = "File downloaded: report.pdf", IpAddress = "192.168.1.1", Timestamp = DateTime.Now },
    new LogEntry { Id = 5, Level = "ERROR", Message = "Unauthorized access attempt", IpAddress = "192.168.1.10", Timestamp = DateTime.Now }
];
```

**Kịch bản kiểm thử:**
- Test 1: Lọc level `"ERROR"`, keyword `null`, trang 1, kích thước 10. (Kỳ vọng: Lấy log ID 3 và 5).
- Test 2: Lọc level `null`, keyword `"192.168"`, trang 1, kích thước 2. (Kỳ vọng: Lấy log ID 1 và 4).
- Test 3: Lọc level `null`, keyword `"Database"`, trang 2, kích thước 1. (Kỳ vọng: Danh sách rỗng vì kết quả lọc chỉ có 1 phần tử nằm ở trang 1).
