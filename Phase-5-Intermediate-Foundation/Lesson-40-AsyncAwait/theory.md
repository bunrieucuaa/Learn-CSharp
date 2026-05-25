# Bài 40: Async & Await (Lập trình Bất đồng bộ trong C#)

Trong thế giới hiện đại, các ứng dụng Web hoặc App di động đòi hỏi tốc độ phản hồi cực kỳ cao. Nếu người dùng nhấn nút "Tải báo cáo" và ứng dụng bị đơ (freeze) mất 10 giây để chờ tải dữ liệu, trải nghiệm người dùng sẽ cực kỳ tồi tệ.

Để giải quyết vấn đề nghẽn hệ thống và tối ưu hóa hiệu năng máy chủ, lập trình viên sử dụng kỹ thuật **Lập trình Bất đồng bộ (Asynchronous Programming)**. Trong C#, điều này được thực hiện thông qua bộ đôi từ khóa kinh điển: `async` và `await`.

---

## 1. Concurrency vs Parallelism vs Asynchrony

Hãy phân biệt rõ 3 khái niệm rất dễ nhầm lẫn này:
* **Concurrency (Đồng thời):** Nhiều tác vụ đang được xử lý trong cùng một khoảng thời gian (nhưng có thể chuyển đổi xen kẽ nhau trên cùng 1 CPU, tạo cảm giác chạy cùng lúc).
* **Parallelism (Song song):** Nhiều tác vụ thực sự chạy **cùng một thời điểm vật lý** trên các nhân (Cores) CPU khác nhau.
* **Asynchrony (Bất đồng bộ - Không đồng bộ):** Tác vụ được kích hoạt và chạy ngầm (thường là các tác vụ I/O như đọc file, gọi API, truy vấn Database). Trong lúc tác vụ đó đang chạy, **Thread (Luồng) gọi nó sẽ được giải phóng** để làm việc khác, không bị ngồi im chờ đợi.

### Ví dụ đời thực: Gọi món ở nhà hàng
* **Đồng bộ (Synchronous):** Bạn gọi món, nhân viên thu ngân đi vào bếp nấu ăn, bạn đứng chờ ở quầy thu ngân. Cho đến khi món ăn chín, thu ngân mang ra đưa bạn rồi mới phục vụ người khách tiếp theo. (Tất cả mọi người đều bị nghẽn!).
* **Bất đồng bộ (Asynchronous):** Bạn gọi món, thu ngân đưa bạn một thiết bị báo rung (Task) rồi tiếp tục phục vụ người khách tiếp theo. Đầu bếp trong bếp sẽ nấu ăn. Khi đồ ăn chín, thiết bị báo rung rung lên (Await), bạn quay lại nhận đồ ăn.

---

## 2. Mô hình lập trình bất đồng bộ Task-based (TAP)

Trong C#, bất đồng bộ xoay quanh lớp **`Task`** (đại diện cho một công việc chạy ngầm sẽ hoàn thành trong tương lai).

### 2.1. Cú pháp cơ bản
* Để khai báo một phương thức bất đồng bộ, thêm từ khóa **`async`** trước kiểu trả về của hàm.
* Để đợi một phương thức bất đồng bộ hoàn thành, sử dụng từ khóa **`await`** trước lời gọi hàm đó.

```csharp
// Phương thức trả về Task (tương đương void đồng bộ)
public async Task SaveDataAsync(string data)
{
    // Giả lập ghi file bất đồng bộ
    await File.WriteAllTextAsync("data.txt", data);
}

// Phương thức trả về Task<T> (tương đương trả về kiểu T đồng bộ)
public async Task<string> FetchDataFromApiAsync()
{
    using var client = new HttpClient();
    // Chờ tải dữ liệu từ internet bất đồng bộ
    string result = await client.GetStringAsync("https://api.site.com/data");
    return result;
}
```

### 2.2. Quy tắc đặt tên phương thức
Theo Best Practice của C#, mọi phương thức bất đồng bộ bắt buộc phải có hậu tố **`Async`** ở cuối tên phương thức (ví dụ: `ReadAsync`, `FetchDataAsync`) để lập trình viên khác biết rằng hàm này cần sử dụng `await`.

---

## 3. Bản chất hoạt động của Async/Await dưới Compiler

Khi bạn viết từ khóa `async`, C# Compiler không chạy phép thuật nào cả. Thực chất, nó sẽ tự động biên dịch toàn bộ cấu trúc mã nguồn của phương thức đó thành một **State Machine (Máy trạng thái)** phức tạp.

Khi gặp từ khóa `await`:
1. Trình biên dịch lưu trạng thái hiện tại của hàm.
2. Giải phóng Thread hiện tại về lại Thread Pool để CPU dùng làm việc khác (ví dụ: phản hồi request của user khác trên web).
3. Khi tác vụ ngầm chạy xong, State Machine sẽ chọn một Thread trống khác từ Thread Pool để nạp lại trạng thái cũ và tiếp tục chạy các dòng code bên dưới từ khóa `await`.

---

## 4. So sánh với JavaScript: Promises vs Tasks

Vì bạn có nền tảng JavaScript, bạn sẽ thấy `async/await` cực kỳ thân thuộc. Tuy nhiên, mô hình thực thi bên dưới của hai ngôn ngữ hoàn toàn khác nhau:

| Tiêu chí | Trong JavaScript | Trong C# (.NET) |
|---|---|---|
| Đại diện tác vụ ngầm | `Promise` | `Task` hoặc `Task<T>` |
| Cơ chế luồng | **Đơn luồng (Single-threaded):** Sử dụng cơ chế Event Loop để chuyển đổi các callback. | **Đa luồng (Multi-threaded):** Sử dụng hệ thống Thread Pool quản lý nhiều Thread chạy song song trên nhiều nhân CPU. |
| Hàm chạy song song | `Promise.all([p1, p2])` | `Task.WhenAll(t1, t2)` |
| Bắt lỗi | `try/catch` | `try/catch` (C# tự động unwrap các exception bên trong Task) |

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Đồng bộ hóa bất đồng bộ (Sync-over-Async)
```csharp
// SAI LẦM: Sử dụng .Result hoặc .Wait() để lấy dữ liệu từ Task
string data = FetchDataFromApiAsync().Result;
```
*Hậu quả cực kỳ nghiêm trọng:* Việc gọi trực tiếp `.Result` hoặc `.Wait()` sẽ khóa cứng (Block) Thread hiện tại để ép nó đợi bằng được Task chạy xong. Trong ứng dụng Web (ASP.NET), điều này rất dễ dẫn đến lỗi **Deadlock (Khoá chết)** — ứng dụng bị treo cứng và sập hoàn toàn.
*Khắc phục:* Luôn dùng `await` xuyên suốt (Async all the way).

### Sai lầm 2: Sử dụng `async void`
```csharp
// SAI LẦM:
public async void DoWorkAsync() { ... }
```
*Lưu ý:* Chỉ sử dụng `async void` cho các trình xử lý sự kiện (Event Handlers) trong giao diện (như WinForms, WPF). Trong tất cả các trường hợp khác, nếu hàm bất đồng bộ không trả về giá trị, bắt buộc kiểu trả về phải là **`Task`** (thay vì `void`).
*Lý do:* Nếu dùng `async void`, phía gọi hàm sẽ không thể sử dụng `await` và không thể bắt được Exception nếu hàm xảy ra lỗi, khiến ứng dụng sập đột ngột mà không ghi nhận được log lỗi.

---

## 6. Checklist đánh giá hiểu bài

1. Bất đồng bộ (Asynchrony) khác với chạy song song (Parallelism) ở điểm nào?
2. Tại sao gọi `.Result` hoặc `.Wait()` lại là một anti-pattern cực kỳ nguy hiểm trong C#?
3. Hậu tố nào cần thêm vào tên của một phương thức bất đồng bộ theo tiêu chuẩn C#?
4. Từ khóa `await` giải phóng Thread như thế nào?
5. Sự khác biệt cơ bản giữa cơ chế chạy bất đồng bộ của JS (Event Loop) và C# (Thread Pool) là gì?
