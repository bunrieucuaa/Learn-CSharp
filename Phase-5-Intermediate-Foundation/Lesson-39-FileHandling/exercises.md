# Bài tập Thực hành Bài 39: File Handling & JSON

Thực hành các bài tập dưới đây bằng cách viết chương trình C# trên IDE cá nhân của bạn để kiểm nghiệm tính bền vững của dữ liệu.

---

## Bài tập 1: Ghi đè và ghi thêm nhật ký hoạt động
**Đề bài:**
1. Hãy viết một chương trình tạo ra tệp tin có tên `counter.txt` chứa một số nguyên duy nhất đại diện cho số lần khởi động chương trình (bắt đầu bằng `1`).
2. Mỗi lần bạn chạy lại chương trình, hãy đọc số hiện tại trong file `counter.txt`, cộng thêm 1 đơn vị, và ghi đè lại kết quả mới vào file.

* **Expected Output:**
  - Lần chạy 1: Nội dung file `1`
  - Lần chạy 2: Nội dung file `2`
  - Lần chạy 3: Nội dung file `3`

---

## Bài tập 2: Tạo thư mục và di chuyển file
**Đề bài:**
Viết chương trình thực hiện chuỗi hành động sau:
1. Kiểm tra thư mục `C#_Backup` trong thư mục chạy hiện tại có tồn tại chưa? Nếu chưa, hãy tạo mới thư mục này.
2. Tạo một file tên là `temp.txt` chứa nội dung bất kỳ ở thư mục hiện tại.
3. Di chuyển file `temp.txt` vào bên trong thư mục `C#_Backup` dưới tên mới là `archive.txt`.

* **Gợi ý:** Sử dụng `Directory.CreateDirectory()`, `File.WriteAllText()` và `File.Move()`.

---

## Bài tập 3: Bộ lọc tệp log chứa từ khóa "ERROR"
**Đề bài:**
Cho một file log giả định tên là `system.log` chứa các dòng văn bản sau (hãy tự tạo file bằng code hoặc viết thủ công):
```text
[09:00] INFO - System starting...
[09:05] WARNING - High CPU usage detected.
[09:10] ERROR - Connection database failed.
[09:12] INFO - User admin logged in.
[09:15] ERROR - Disk space critical.
```
Hãy viết chương trình sử dụng `StreamReader` đọc từng dòng file log này, chỉ lọc ra các dòng chứa từ khóa `"ERROR"` và ghi toàn bộ các dòng lỗi này sang một file mới tên là `errors_only.txt`.

* **Expected Output trong file errors_only.txt:**
  ```text
  [09:10] ERROR - Connection database failed.
  [09:15] ERROR - Disk space critical.
  ```

---

## Bài tập 4: Tuần tự hóa một đối tượng cấu hình (AppConfig)
**Đề bài:**
Cho class `AppConfig` như sau:
```csharp
public class AppConfig
{
    public string AppName { get; set; }
    public string Version { get; set; }
    public int MaxConnections { get; set; }
    public List<string> EnabledFeatures { get; set; }
}
```
Hãy viết chương trình:
1. Khởi tạo một đối tượng `AppConfig` có các thông số cụ thể.
2. Tuần tự hóa đối tượng đó thành chuỗi JSON đẹp đẽ (WriteIndented = true).
3. Lưu chuỗi JSON đó vào tệp tin `config.json`.

* **Expected Output trong config.json:**
  ```json
  {
    "AppName": "MyCSharpApp",
    "Version": "1.0.0",
    "MaxConnections": 100,
    "EnabledFeatures": [
      "Logging",
      "OAuth2",
      "Notifications"
    ]
  }
  ```

---

## Bài tập 5: Tải cấu hình từ JSON file và phục hồi đối tượng
**Đề bài:**
Viết chương trình đọc tệp tin `config.json` được tạo ra ở Bài tập 4:
1. Đọc nội dung tệp tin thành chuỗi.
2. Giải tuần tự hóa chuỗi đó về đối tượng `AppConfig` C#.
3. In các thông số cấu hình ra màn hình Console để chứng minh dữ liệu đã được nạp lại thành công.

* **Expected Output:**
  ```text
  Ứng dụng: MyCSharpApp | Phiên bản: 1.0.0
  Kết nối tối đa: 100
  Các tính năng bật: Logging, OAuth2, Notifications
  ```
