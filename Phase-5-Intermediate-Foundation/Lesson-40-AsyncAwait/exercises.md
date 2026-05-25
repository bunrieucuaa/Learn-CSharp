# Bài tập Thực hành Bài 40: Async & Await

Hoàn thành các bài tập dưới đây bằng cách viết các phương thức bất đồng bộ sử dụng cú pháp `async` / `await` trong dự án test của bạn.

---

## Bài tập 1: Hàm đếm ngược bất đồng bộ
**Đề bài:**
Hãy viết một phương thức bất đồng bộ có chữ ký: `static async Task CountdownAsync(int seconds)`
Hàm này nhận vào số giây, in ra màn hình đếm ngược từng giây một và dừng lại 1 giây giữa mỗi lần in.

* **Gợi ý:** Sử dụng vòng lặp `for` hoặc `while` kết hợp với `await Task.Delay(1000)`.
* **Expected Output khi gọi CountdownAsync(3):**
  ```text
  3...
  2...
  1...
  Hết giờ!
  ```

---

## Bài tập 2: Chuyển đổi hàm đọc file đồng bộ sang bất đồng bộ
**Đề bài:**
Cho đoạn code đọc file đồng bộ sau:
```csharp
public string ReadConfig(string path)
{
    if (File.Exists(path))
    {
        return File.ReadAllText(path);
    }
    return string.Empty;
}
```
Hãy viết lại phương thức trên dưới dạng bất đồng bộ: thay đổi kiểu trả về và sử dụng các phiên bản bất đồng bộ của thư viện file C#.

* **Gợi ý:** Dùng `File.WriteAllTextAsync()` hoặc `File.ReadAllTextAsync()`. Cập nhật kiểu trả về thành `Task<string>`.

---

## Bài tập 3: Tải dữ liệu từ 2 API song song
**Đề bài:**
Hãy viết một hàm bất đồng bộ có nhiệm vụ lấy thông tin người dùng từ 2 URL API giả định sau (có thể dùng API thật của jsonplaceholder):
- API 1: `https://jsonplaceholder.typicode.com/users/1`
- API 2: `https://jsonplaceholder.typicode.com/users/2`

Yêu cầu: Tải thông tin từ 2 URL này **đồng thời** (song song) để tối ưu thời gian phản hồi thay vì tải tuần tự từng cái một.

* **Gợi ý:** Khởi động hai Task `client.GetStringAsync()`, sau đó chờ đợi cả hai bằng `Task.WhenAll`.

---

## Bài tập 4: Xử lý lỗi Timeout bất đồng bộ
**Đề bài:**
Giả lập một tác vụ mạng chập chờn có thể xảy ra lỗi. Viết một hàm `async Task ConnectDatabaseAsync()`. Hàm này giả lập chờ kết nối 1.5 giây.
- Sử dụng hàm tạo số ngẫu nhiên `Random`. 50% cơ hội ném ra ngoại lệ `TimeoutException("Kết nối cơ sở dữ liệu thất bại do quá giờ!")`.
- Viết khối lệnh gọi trong hàm `Main` thực hiện try-catch để bắt lỗi này và in log cảnh báo phù hợp.

---

## Bài tập 5: Bất đồng bộ với kiểu trả về có giá trị (Task<T>)
**Đề bài:**
Hãy viết một hàm bất đồng bộ: `static async Task<int> CalculateSumAsync(int a, int b)`
Hàm này nhận vào 2 số nguyên, giả lập quá trình tính toán phức tạp bằng cách chờ 1 giây (`Task.Delay`), sau đó trả về tổng của `a + b`.
Hãy gọi hàm này trong `Main`, nhận giá trị và in kết quả ra màn hình.

* **Expected Output:**
  ```text
  Đang tính toán...
  Kết quả tính được: 15
  ```
