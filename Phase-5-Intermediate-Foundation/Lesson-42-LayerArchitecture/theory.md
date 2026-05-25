# Bài 42: Layer Architecture Basics (Kiến trúc phân tầng cơ bản)

Khi làm việc trong môi trường doanh nghiệp thực tế, bạn sẽ không bao giờ được viết toàn bộ code ứng dụng vào một tệp duy nhất (như `Program.cs`). Một ứng dụng lớn có hàng trăm nghìn dòng code nếu không được sắp xếp theo một cấu trúc khoa học sẽ nhanh chóng trở thành **Spaghetti Code** (mớ code hỗn độn, rối ren như đĩa mì Ý).

Để chia tách trách nhiệm và giúp code dễ quản lý, người ta chia dự án thành các **Tầng (Layers)**. Trong bài này, chúng ta sẽ làm quen với kiến trúc phân tầng kinh điển nhất: **3-Layer Architecture (Kiến trúc 3 lớp)**.

---

## 1. Bản chất của Kiến trúc phân tầng (Layered Architecture)

Nguyên lý cốt lõi của phân tầng là **Separation of Concerns (Phân tách trách nhiệm)**: Mỗi tầng chỉ giải quyết một nhóm vấn đề cụ thể của hệ thống và chỉ giao tiếp với tầng nằm ngay dưới nó.

Mô hình 3-Layer truyền thống gồm các tầng xếp chồng lên nhau:

```text
+---------------------------------------------------+
|         Presentation Layer (Tầng hiển thị - UI)    |
+---------------------------------------------------+
                          |
                          v
+---------------------------------------------------+
|     Business Logic Layer (Tầng nghiệp vụ - Service)|
+---------------------------------------------------+
                          |
                          v
+---------------------------------------------------+
|     Data Access Layer (Tầng truy cập dữ liệu - DB)|
+---------------------------------------------------+
```

---

## 2. Nhiệm vụ chi tiết của từng tầng

### 2.1. Presentation Layer (Tầng hiển thị - UI)
* **Nhiệm vụ:** Tương tác trực tiếp với người dùng cuối hoặc hệ thống bên ngoài.
* **Thành phần:** Console UI, Web Controllers (API), views HTML/Angular.
* **Hoạt động:** Nhận dữ liệu đầu vào từ người dùng (ví dụ: gõ bàn phím, click chuột, gửi JSON request), hiển thị dữ liệu ra màn hình. Tầng này **không được chứa logic nghiệp vụ** hay **logic truy vấn file/DB trực tiếp**. Nó chỉ làm nhiệm vụ trung chuyển: nhận input -> chuyển xuống Service xử lý -> lấy kết quả in ra.

### 2.2. Business Logic Layer (BLL - Tầng nghiệp vụ / Service)
* **Nhiệm vụ:** Là "bộ não" của ứng dụng, chịu trách nhiệm xử lý toàn bộ các quy tắc nghiệp vụ (Business Rules).
* **Thành phần:** Services (như `OrderService`, `UserService`).
* **Hoạt động:** Kiểm tra tính hợp lệ của dữ liệu (Validation), tính toán số tiền, thuế, xử lý logic thanh toán. Nó nhận yêu cầu từ UI, thực hiện tính toán, kiểm tra và gọi tầng Data Access để lưu/lấy dữ liệu. Tầng này **hoàn toàn không được biết giao diện hiển thị là gì** (không có lệnh `Console.WriteLine` hay `Alert`).

### 2.3. Data Access Layer (DAL - Tầng dữ liệu / Repository)
* **Nhiệm vụ:** Chịu trách nhiệm giao tiếp trực tiếp với nguồn lưu trữ dữ liệu thực tế.
* **Thành phần:** Repositories, Database Context (EF Core), tệp tin lưu trữ (JSON, XML).
* **Hoạt động:** Thực hiện các câu lệnh SQL, đọc/ghi tệp tin trên đĩa cứng để thực hiện các thao tác CRUD (Create, Read, Update, Delete) thô sơ. Tầng này **không quan tâm đến logic nghiệp vụ** (ví dụ: nó chỉ lưu trữ số tiền, còn số tiền đó được tính thế nào là việc của Service).

---

## 3. Quy tắc vàng của luồng phụ thuộc (Dependency Flow)

> [!IMPORTANT]
> **Chiều phụ thuộc chỉ được phép đi từ TRÊN xuống DƯỚI:**
> * Tầng UI phụ thuộc vào (biết đến) tầng Service.
> * Tầng Service phụ thuộc vào (biết đến) tầng Data Access.
> * Tầng Data Access không được biết đến bất kỳ tầng nào phía trên nó.
> * UI **không bao giờ** được phép nhảy cóc gọi trực tiếp xuống Data Access.

Nếu bạn vi phạm quy tắc này (ví dụ: từ Service gọi ngược lên UI để in chữ đỏ báo lỗi), bạn đang phá hỏng cấu trúc phân tầng. Ứng dụng của bạn sẽ bị ghép chặt (tight coupling) và không thể tái sử dụng hoặc chạy trên nền tảng khác (ví dụ: chuyển từ ứng dụng Console sang ứng dụng Web).

---

## 4. Cấu trúc thư mục dự án chuẩn

Trong C# .NET, cấu trúc thư mục của một dự án phân tầng sạch sẽ thường có dạng:

```text
Solution/
│
├── Models/              <-- Chứa các Class Entity dữ liệu (dùng chung cho các tầng)
│   └── Student.cs
│
├── DataAccess/          <-- Tầng Data Access Layer
│   └── StudentRepository.cs
│
├── Services/            <-- Tầng Business Logic Layer
│   └── StudentService.cs
│
└── UI/                  <-- Tầng Presentation Layer
    ├── ConsoleUI.cs
    └── Program.cs
```

---

## 5. Sai lầm phổ biến & Best Practice

### Sai lầm 1: Viết mã giao diện (UI Code) vào trong Service
```csharp
// SAI LẦM: Viết Console.ReadLine() trong Service
public class UserService
{
    public void CreateUser()
    {
        Console.Write("Nhập tên: "); // VI PHẠM: Service không được tương tác Console!
        string name = Console.ReadLine();
        // ...
    }
}
```
*Khắc phục:* UI phải là nơi nhập dữ liệu, sau đó truyền dữ liệu đó làm đối số vào hàm của Service: `_userService.CreateUser(name)`.

### Sai lầm 2: UI truy cập trực tiếp file/DB bỏ qua Service
Nhiều lập trình viên lười biếng thường viết lệnh đọc file JSON trực tiếp ở file `Program.cs` để hiển thị, thay vì viết hàm đọc ở Repository rồi gọi qua Service. Điều này làm mất tính kiểm soát logic nghiệp vụ.

---

## 6. Checklist đánh giá hiểu bài

1. Tại sao cấu trúc phân tầng (Layered Architecture) lại giúp bảo trì dự án lớn dễ dàng hơn?
2. Hãy mô tả ngắn gọn nhiệm vụ chính của 3 tầng: Presentation, Business Logic, và Data Access.
3. Chiều phụ thuộc (Dependency Flow) hợp lệ giữa các tầng được quy định như thế nào?
4. Điều gì xảy ra nếu bạn viết lệnh `Console.WriteLine` bên trong một class thuộc tầng Data Access?
