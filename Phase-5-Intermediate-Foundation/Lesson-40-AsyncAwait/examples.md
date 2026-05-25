# Ví dụ Thực hành Bài 40: Async & Await

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức lập trình bất đồng bộ.

---

## Ví dụ 1: Mô phỏng nghẽn luồng đồng bộ vs Bất đồng bộ

Ví dụ này so sánh sự khác nhau giữa việc dùng `Thread.Sleep` (Đồng bộ - nghẽn luồng) và `Task.Delay` (Bất đồng bộ - giải phóng luồng).

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("--- CHẠY THỬ ĐỒNG BỘ (BLOCKING) ---");
        RunSynchronousWork();
        Console.WriteLine("Công việc đồng bộ hoàn thành.");

        Console.WriteLine("\n--- CHẠY THỬ BẤT ĐỒNG BỘ (NON-BLOCKING) ---");
        // Gọi hàm bất đồng bộ nhưng không await ngay để chứng minh luồng chính không bị nghẽn
        Task asyncTask = RunAsynchronousWorkAsync();
        
        Console.WriteLine("Luồng chính vẫn chạy tiếp dòng này trong khi việc ngầm đang xử lý...");
        
        // Bây giờ mới await để chờ việc ngầm hoàn thành trước khi tắt chương trình
        await asyncTask;
        Console.WriteLine("Chương trình kết thúc.");
    }

    static void RunSynchronousWork()
    {
        Console.WriteLine("Bắt đầu làm việc đồng bộ (Chờ 2 giây)...");
        Thread.Sleep(2000); // Khóa cứng Thread hiện tại
        Console.WriteLine("Hoàn thành việc đồng bộ.");
    }

    static async Task RunAsynchronousWorkAsync()
    {
        Console.WriteLine("Bắt đầu làm việc bất đồng bộ (Chờ 2 giây)...");
        // Sử dụng Task.Delay thay vì Thread.Sleep để giải phóng Thread tạm thời
        await Task.Delay(2000); 
        Console.WriteLine("Hoàn thành việc bất đồng bộ.");
    }
}
```

---

## Ví dụ 2: Lấy dữ liệu bất đồng bộ với `HttpClient`

Ví dụ thực tế mô phỏng gọi API lấy thông tin thời tiết.

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Bắt đầu gọi API lấy dữ liệu...");
        
        try
        {
            string url = "https://jsonplaceholder.typicode.com/todos/1";
            string result = await FetchDataFromInternetAsync(url);
            
            Console.WriteLine("\nDữ liệu API trả về thành công:");
            Console.WriteLine(result);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Lỗi mạng: {ex.Message}");
        }
    }

    // Hàm bất đồng bộ trả về chuỗi JSON lấy từ Internet
    static async Task<string> FetchDataFromInternetAsync(string url)
    {
        // Khởi tạo client dùng using để tự giải phóng kết nối mạng
        using HttpClient client = new HttpClient();
        
        // Gọi bất đồng bộ và await lấy kết quả chuỗi
        string json = await client.GetStringAsync(url);
        return json;
    }
}
```

---

## Ví dụ 3: Gọi nhiều tác vụ song song với `Task.WhenAll`

Ví dụ giả lập tải 3 hình ảnh cùng một lúc từ internet và chờ toàn bộ chúng hoàn thành.

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        Console.WriteLine("Bắt đầu tải 3 hình ảnh song song...");

        // Khởi động 3 task tải độc lập mà không dùng await trực tiếp
        Task<string> downloadImage1 = DownloadImageAsync("Avatar", 3000); // Tải mất 3 giây
        Task<string> downloadImage2 = DownloadImageAsync("Banner", 1500); // Tải mất 1.5 giây
        Task<string> downloadImage3 = DownloadImageAsync("Logo", 1000);   // Tải mất 1 giây

        // Dùng Task.WhenAll để chờ toàn bộ 3 task hoàn thành đồng thời
        // Thời gian chờ tổng cộng sẽ bằng thời gian của Task lâu nhất (3 giây) thay vì cộng dồn (5.5 giây)
        string[] results = await Task.WhenAll(downloadImage1, downloadImage2, downloadImage3);

        watch.Stop();
        Console.WriteLine("\nKết quả tải ảnh:");
        foreach (var img in results)
        {
            Console.WriteLine($"- {img}");
        }
        
        Console.WriteLine($"\nTổng thời gian tải song song: {watch.ElapsedMilliseconds} ms");
    }

    static async Task<string> DownloadImageAsync(string imageName, int delayMs)
    {
        Console.WriteLine($"-> Khởi động tải ảnh: {imageName}...");
        await Task.Delay(delayMs); // Giả lập thời gian tải mạng
        return $"{imageName}.png (Kích thước: {delayMs}px)";
    }
}
```

---

## Ví dụ 4: Xử lý ngoại lệ trong các phương thức bất đồng bộ

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Chương trình kiểm tra giao dịch tài chính bất đồng bộ...");

        try
        {
            // Gọi hàm và chờ đợi
            await ProcessPaymentAsync(-50); // Truyền số tiền âm để cố ý tạo lỗi
        }
        catch (ArgumentException ex)
        {
            // C# tự unwrap exception từ Task ra và ném vào khối catch này
            Console.WriteLine($"Bắt lỗi thành công: {ex.Message}");
        }
    }

    static async Task ProcessPaymentAsync(decimal amount)
    {
        Console.WriteLine($"Đang kết nối cổng thanh toán để trừ tiền: ${amount}...");
        await Task.Delay(1000); // Giả lập chờ kết nối ngân hàng

        if (amount <= 0)
        {
            // Ném exception bình thường, C# sẽ tự đóng gói lỗi vào Task trả về
            throw new ArgumentException("Số tiền thanh toán phải lớn hơn 0!");
        }

        Console.WriteLine("Thanh toán thành công!");
    }
}
```
