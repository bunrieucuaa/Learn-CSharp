# Thách thức Bài 41: Thiết kế Công cụ Gửi Báo cáo Đa định dạng (Fluent Report Engine)

## Ngữ cảnh
Bộ phận vận hành của doanh nghiệp cần một công cụ tạo và gửi báo cáo doanh thu cuối ngày. Báo cáo này có thể được xuất ra dưới nhiều định dạng khác nhau (PDF, HTML, Excel) và được gửi qua nhiều kênh khác nhau (Email, Telegram).

Để dự án dễ mở rộng (ví dụ: tháng sau muốn thêm định dạng Word và kênh SMS), bạn được yêu cầu thiết kế hệ thống này tuân thủ tuyệt đối nguyên lý **Dependency Inversion** và sử dụng **DI Container** để lắp ráp các bộ phận.

## Cấu trúc các Interface thiết kế
Bạn cần định nghĩa các interface sau:
```csharp
// 1. Giao diện xuất định dạng báo cáo
public interface IReportFormatter
{
    string Format(string data);
}

// 2. Giao diện kênh gửi báo cáo
public interface IReportSender
{
    void Send(string formattedReport);
}
```

## Các Class cụ thể triển khai
Hãy tạo các class hiện thực hóa:
- **Formatters:**
  - `PdfReportFormatter`: trả về `"=== PDF REPORT ===\n" + data`.
  - `HtmlReportFormatter`: trả về `"<html><body>" + data + "</body></html>"`.
- **Senders:**
  - `EmailReportSender`: in ra `"Gửi email báo cáo: [nội dung]"`.
  - `TelegramReportSender`: in ra `"Gửi tin nhắn Telegram báo cáo: [nội dung]"`.

## Công cụ điều phối chính (ReportEngine)
Viết class `ReportEngine` nhận cả 2 dependency qua Constructor:
```csharp
public class ReportEngine
{
    private readonly IReportFormatter _formatter;
    private readonly IReportSender _sender;

    public ReportEngine(IReportFormatter formatter, IReportSender sender)
    {
        _formatter = formatter;
        _sender = sender;
    }

    public void GenerateAndSend(string data)
    {
        string formatted = _formatter.Format(data);
        _sender.Send(formatted);
    }
}
```

## Yêu cầu Thách thức
1. **Lập cấu hình linh hoạt:** Viết chương trình cấu hình DI Container. Hãy đăng ký sử dụng `PdfReportFormatter` và `TelegramReportSender`. Giải quyết `ReportEngine` từ Container và chạy thử.
2. **Kiểm chứng Loose Coupling:** Thay đổi cấu hình đăng ký sang `HtmlReportFormatter` và `EmailReportSender`. Kiểm chứng rằng bạn **không cần chỉnh sửa một dòng code nào** của class `ReportEngine` mà hành vi của nó vẫn thay đổi chính xác.
3. **Cài đặt Lifetime an toàn:** Phân tích xem các class Formatter và Sender ở trên có trạng thái nội bộ không? Chúng nên được đăng ký dưới dạng nào (Transient, Scoped, hay Singleton) để tiết kiệm tài nguyên bộ nhớ tối đa?
