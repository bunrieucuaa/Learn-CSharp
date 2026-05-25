# Bài 41: Dependency Injection (Tiêm phụ thuộc trong .NET)

Khi dự án phần mềm lớn dần lên, số lượng các Class tăng từ hàng chục lên hàng trăm. Nếu các Class này phụ thuộc chặt chẽ vào nhau (gọi là **Tight Coupling**), việc thay đổi code ở một Class có thể làm sập dây chuyền hàng chục Class khác. Code trở nên cực kỳ "cứng nhắc" và không thể viết Unit Test được.

Để giải quyết vấn đề này, các kiến trúc sư phần mềm sử dụng một nguyên lý thiết kế kinh điển: **Dependency Inversion Principle (DIP)** thông qua kỹ thuật **Dependency Injection (DI)**. Đây là xương sống kiến trúc của mọi ứng dụng ASP.NET Core và Angular hiện đại.

---

## 1. Tight Coupling vs Loose Coupling

### 1.1. Ghép cứng (Tight Coupling) - Vấn đề
Hãy xem đoạn code sau:
```csharp
public class Car
{
    private Engine _engine;

    public Car()
    {
        // Car tự khởi tạo Engine bằng từ khóa new!
        // Nghĩa là Car bị ghép cứng với class Engine cụ thể này.
        _engine = new Engine(); 
    }
}
```
*Vấn đề:* 
* Nếu sau này bạn muốn đổi sang động cơ điện `ElectricEngine` hoặc động cơ phản lực, bạn bắt buộc phải sửa code của class `Car`.
* Bạn không thể viết Unit Test độc lập cho class `Car` vì nó luôn kéo theo class `Engine` thật chạy cùng (không thể Mocking dữ liệu được).

### 1.2. Ghép lỏng (Loose Coupling) - Giải pháp
Thay vì tự khởi tạo phụ thuộc bằng từ khóa `new`, Class sẽ yêu cầu các phụ thuộc thông qua **Interface** và nhận chúng từ bên ngoài truyền vào thông qua Constructor:
```csharp
public interface IEngine { void Start(); }

public class Car
{
    private readonly IEngine _engine;

    // Phụ thuộc (IEngine) được "tiêm" (inject) từ bên ngoài vào qua Constructor
    public Car(IEngine engine)
    {
        _engine = engine; 
    }
}
```
Bây giờ, class `Car` hoàn toàn "lỏng lẻo". Nó không quan tâm động cơ chạy bên trong là gì, miễn là động cơ đó tuân thủ hợp đồng `IEngine`. Bạn có thể truyền `GasolineEngine`, `ElectricEngine`, hoặc thậm chí là một `MockEngine` giả lập khi viết Unit Test.

---

## 2. Các khái niệm cốt lõi: IoC & DI Container

* **Inversion of Control (IoC - Đảo ngược điều khiển):** Là một nguyên lý thiết kế. Thay vì Class chủ động điều khiển và khởi tạo các đối tượng phụ thuộc, quyền điều khiển đó được giao lại cho một thực thể bên ngoài (Hệ thống hoặc Framework).
* **Dependency Injection (DI):** Là một kỹ thuật cụ thể để thực hiện nguyên lý IoC. Phổ biến nhất là **Constructor Injection** (tiêm phụ thuộc qua Constructor).
* **DI Container (IoC Container):** Là một bộ thư viện chuyên nghiệp giúp bạn tự động quản lý vòng đời, đăng ký và tự động tiêm (resolve) các đối tượng phụ thuộc. Bạn chỉ cần cấu hình luật đăng ký ở đầu chương trình, container sẽ tự động "lắp ráp" toàn bộ hệ thống cho bạn.

---

## 3. Vòng đời của Service trong .NET (Service Lifetimes)

Khi bạn đăng ký một Class vào hệ thống DI Container của .NET, bạn phải quyết định xem đối tượng đó sẽ tồn tại trong bao lâu. .NET hỗ trợ 3 loại thời gian sống:

### 3.1. Transient (Tạm thời)
* **Đăng ký:** `services.AddTransient<IEngine, Engine>();`
* **Cơ chế:** Mỗi khi có một class yêu cầu `IEngine`, DI Container sẽ tạo ra một **đối tượng `Engine` hoàn toàn mới**.
* **Phù hợp:** Cho các dịch vụ nhẹ, không lưu trạng thái (Stateless), thực hiện các tác vụ nhanh gọn.

### 3.2. Scoped (Theo phạm vi yêu cầu)
* **Đăng ký:** `services.AddScoped<IEngine, Engine>();`
* **Cơ chế:** DI Container tạo ra **duy nhất 1 đối tượng** trong phạm vi một vòng đời Request (ví dụ: một yêu cầu HTTP gửi đến trang Web). Đối tượng đó sẽ được dùng chung bởi tất cả các Class cần đến nó trong suốt quá trình xử lý request đó. Request kết thúc, đối tượng bị hủy.
* **Phù hợp:** Dùng nhiều nhất trong lập trình Web, đặc biệt là các kết nối Database (DbContext) để đảm bảo các service xử lý chung 1 request dùng chung 1 kết nối.

### 3.3. Singleton (Duy nhất toàn cục)
* **Đăng ký:** `services.AddSingleton<IEngine, Engine>();`
* **Cơ chế:** DI Container tạo ra **duy nhất 1 đối tượng duy nhất** trong lần yêu cầu đầu tiên. Đối tượng này sẽ sống mãi trong bộ nhớ RAM cho đến khi chương trình tắt hoàn toàn. Mọi class ở mọi request đều dùng chung đúng đối tượng này.
* **Phù hợp:** Cấu hình hệ thống, dịch vụ ghi Log, bộ nhớ đệm (Caching).

---

## 4. So sánh với JavaScript & Angular

Vì mục tiêu của bạn là trở thành Fullstack Developer với **Angular**, bạn sẽ thấy hệ thống DI trong .NET và Angular cực kỳ giống nhau:

* **Điểm giống:** Cả hai đều dùng Constructor Injection để tiêm phụ thuộc thông qua kiểu dữ liệu rõ ràng.
* **Angular DI:** Trong Angular, bạn dùng `@Injectable({ providedIn: 'root' })` để đăng ký một Singleton Service.
* **.NET DI:** Trong C#, bạn đăng ký trong file cấu hình khởi tạo của ứng dụng (`Program.cs`) sử dụng `builder.Services.Add...`.
* Học sâu DI trong C# giúp bạn ngay lập tức hiểu hệ thống Service Injection của Angular mà không mất thời gian bỡ ngỡ!

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Captive Dependency (Bẫy giữ phụ thuộc)
> [!CAUTION]
> Lỗi này xảy ra khi bạn tiêm một dịch vụ có thời gian sống ngắn vào một dịch vụ có thời gian sống dài hơn.
> *Ví dụ:* Tiêm một **Scoped** Service (ví dụ DB Connection) vào một **Singleton** Service (sống mãi mãi). 
> *Hậu quả:* Scoped Service đó sẽ bị giữ chặt bởi Singleton và sống mãi mãi, dẫn đến rò rỉ bộ nhớ (Memory Leak) hoặc khóa kết nối database. .NET sẽ ném ra Exception cảnh báo lỗi này ngay khi khởi động chương trình ở chế độ Development.

### Sai lầm 2: Lạm dụng Singleton vô tội vạ
Singleton dùng chung một ô nhớ cho toàn bộ ứng dụng. Nếu bạn lưu trữ dữ liệu tạm thời của một người dùng cụ thể trong Singleton, dữ liệu đó sẽ bị nhìn thấy và ghi đè bởi những người dùng khác, gây lỗi bảo mật nghiêm trọng.

---

## 6. Checklist đánh giá hiểu bài

1. Ghép cứng (Tight Coupling) gây ra những khó khăn gì khi phát triển dự án lớn?
2. Phân biệt Inversion of Control (IoC) và Dependency Injection (DI).
3. Trình bày sự khác biệt về vòng đời của 3 loại dịch vụ: Transient, Scoped, và Singleton.
4. Lỗi Captive Dependency là gì? Tại sao .NET lại cấm lỗi này?
5. Điểm tương đồng giữa Dependency Injection trong .NET và Angular Service là gì?
