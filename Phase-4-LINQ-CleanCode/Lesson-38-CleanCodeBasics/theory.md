# Bài 38: Clean Code Basics (Quy tắc viết Code Sạch)

Chúc mừng bạn đã đi đến bài học cuối cùng của **Phase 4**. Đến thời điểm này, bạn đã nắm vững cách sử dụng các toán tử LINQ để truy vấn và biến đổi dữ liệu. Nhưng viết code chạy được mới chỉ là điều kiện cần. Điều kiện đủ để trở thành một lập trình viên chuyên nghiệp là viết **Code Sạch (Clean Code)** — dễ đọc, dễ hiểu và dễ bảo trì.

Hôm nay, chúng ta sẽ học các nguyên lý Clean Code nền tảng nhất và cách áp dụng LINQ để "dọn dẹp" những dòng code rác.

---

## 1. Quy tắc đặt tên sạch (Clean Naming)

Đặt tên là công việc khó nhất trong lập trình. Một cái tên tồi sẽ biến dự án thành một mê cung.

### 1.1. Đặt tên có ý nghĩa và thể hiện mục đích
* **Tồi:** `int d; // Số ngày` hoặc `var list = GetVal();`
* **Sạch:** `int daysSinceLastActive;` hoặc `var activeEmployees = GetActiveEmployees();`

### 1.2. Tránh viết tắt vô nghĩa
* **Tồi:** `var s = new Stud();` hoặc `void ProcOrd(Order o);`
* **Sạch:** `var student = new Student();` hoặc `void ProcessOrder(Order order);`

### 1.3. Tuân thủ chuẩn C# (Naming Conventions)
* **PascalCase** (Viết hoa chữ cái đầu mỗi từ): Dùng cho Class, Struct, Method, Property, Namespace.
  * *Ví dụ:* `class OrderProcessor`, `public decimal TotalAmount { get; }`, `void SaveDatabase()`
* **camelCase** (Viết thường từ đầu tiên, viết hoa các từ sau): Dùng cho Variable, Parameter.
  * *Ví dụ:* `int remainingTickets = 5;`, `void SendEmail(string emailAddress)`
* **camelCase có gạch dưới trước `_`**: Dùng cho `private read-only` fields trong Class.
  * *Ví dụ:* `private readonly ILogger _logger;`

---

## 2. Quy tắc viết Phương thức sạch (Clean Methods)

### 2.1. Nhỏ gọn (Small)
Một phương thức tốt nhất nên nằm gọn trong vòng 10-20 dòng code. Nếu phương thức dài tới 100 dòng, chắc chắn nó đang làm quá nhiều việc và cần được chia nhỏ.

### 2.2. Đơn nhiệm (Single Responsibility Principle - SRP)
Một hàm **chỉ nên làm duy nhất một việc** và làm thật tốt việc đó.
* **Tồi:** Hàm `ProcessOrder` vừa tính tiền, vừa cập nhật kho, vừa gửi email xác nhận. (Hàm này có tới 3 lý do để bị thay đổi khi nghiệp vụ thay đổi).
* **Sạch:** Hàm `ProcessOrder` chỉ điều phối cuộc gọi sang 3 class/hàm riêng biệt: `_paymentService.Charge()`, `_inventoryService.Deduct()`, `_emailService.SendReceipt()`.

### 2.3. Hạn chế tham số đầu vào (Parameter Limit)
Một hàm có quá 3 tham số sẽ cực kỳ khó kiểm thử và dễ gọi sai thứ tự tham số.
* Nếu hàm cần nhận nhiều dữ liệu đầu vào, hãy gom chúng lại thành một Object chuyên dụng (Parameter Object).

---

## 3. Các nguyên lý thiết kế kinh điển

* **DRY (Don't Repeat Yourself - Đừng lặp lại chính mình):** 
  Nếu bạn viết một đoạn code tính thuế VAT ở 3 nơi khác nhau trong dự án, hãy đưa nó vào một hàm dùng chung. Khi luật thuế thay đổi, bạn chỉ cần sửa ở đúng 1 nơi.
* **KISS (Keep It Simple, Stupid - Hãy giữ mọi thứ đơn giản):** 
  Đừng cố gắng phô diễn kỹ thuật cá nhân bằng những đoạn code "hack não", viết tắt lắt léo. Code chạy thông minh nhất là code mà một intern mới vào công ty đọc cũng hiểu ngay.
* **YAGNI (You Aren't Gonna Need It - Bạn sẽ chưa cần đến nó đâu):** 
  Đừng viết trước các tính năng "dự phòng cho tương lai" khi khách hàng chưa yêu cầu. Viết code dư thừa vừa tốn công bảo trì vừa làm phình hệ thống vô ích.

---

## 4. Quy tắc viết Comment (Chú thích)

> [!IMPORTANT]
> **"Code tốt nhất là code tự giải thích (Self-documenting code)."**
> Đừng dùng comment để giải thích cho đoạn code viết dở. Hãy refactor (viết lại) đoạn code đó cho đến khi nó tự rõ ràng.

* **Comment tồi (Giải thích CÁI GÌ - What):**
  ```csharp
  // Kiểm tra nếu tuổi lớn hơn 18
  if (age > 18) { ... }
  ```
  *(Thừa thãi, vì code đã thể hiện rõ `age > 18` là gì).*

* **Comment sạch (Giải thích TẠI SAO - Why):**
  ```csharp
  // Bắt buộc phải ngủ 500ms vì API bên thứ ba giới hạn rate limit 2 req/giây
  Thread.Sleep(500);
  ```
  *(Hữu ích, vì người đọc code sau này sẽ hiểu lý do logic của dòng code đó mà không tự ý xóa đi).*

---

## 5. Dọn dẹp "Code thối" (Code Smells) bằng LINQ

Một trong những "mùi thối" kinh điển nhất trong code legacy là các cấu trúc **Nested If-Else & Loops** (Vòng lặp và rẽ nhánh lồng nhau quá nhiều cấp - tạo thành hình mũi tên thụt lề).

Sử dụng LINQ kết hợp Extension Methods sẽ giúp bạn đập phẳng các vòng lặp này thành một chuỗi các toán tử lọc và map dữ liệu tuyến tính (Linear), giúp code cực kỳ dễ đọc.

---

## 6. Tổng kết Phase 4 (LINQ & Clean Code)

Phase 4 đã trang bị cho bạn:
1. Tư duy lập trình khai báo (Declarative Programming) bằng LINQ: nói cho máy tính biết "Bạn muốn lấy dữ liệu gì" thay vì bắt nó chạy vòng lặp thủ công "Lấy thế nào".
2. Các kỹ năng lọc (`Where`), biến đổi (`Select`, `SelectMany`), sắp xếp (`OrderBy`, `ThenBy`), gom nhóm (`GroupBy`, `Join`).
3. Cách thiết kế Extension Methods để viết code Fluent tự nhiên.
4. Tư duy thẩm mỹ viết mã sạch theo chuẩn quốc tế.

---

## 7. Checklist tự đánh giá

1. Cú pháp PascalCase và camelCase khác nhau thế nào? Nêu đối tượng áp dụng của mỗi loại trong C#.
2. Nguyên lý DRY và KISS khuyên chúng ta điều gì khi lập trình?
3. Tại sao không nên viết comment giải thích hoạt động của một dòng code if-else thông thường?
4. Phương pháp nào hiệu quả nhất để loại bỏ các vòng lặp `foreach` lồng nhau phức tạp?
