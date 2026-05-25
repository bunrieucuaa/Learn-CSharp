# Bài 46: API Concept & RESTful Design (Khái niệm API và Thiết kế REST)

Trong thế giới phát triển phần mềm ngày nay, các ứng dụng không hoạt động đơn độc. Ứng dụng Frontend (viết bằng Angular chạy trên trình duyệt của người dùng) cần giao tiếp với ứng dụng Backend (viết bằng ASP.NET Core chạy trên máy chủ đám mây) để lấy dữ liệu.

Cầu nối giao tiếp này được gọi là **API**. Và để thiết kế một hệ thống API chuẩn mực, dễ hiểu và chuyên nghiệp, toàn bộ ngành công nghiệp phần mềm tuân theo một kiến trúc thiết kế chung gọi là **REST**.

---

## 1. API là gì?

**API (Application Programming Interface - Giao diện lập trình ứng dụng)** là một tập hợp các quy định, giao thức và công cụ cho phép các phần mềm/ứng dụng khác nhau giao tiếp và trao đổi dữ liệu với nhau.

*Hãy tưởng tượng API như một người phục vụ bàn ở nhà hàng. Bạn (Client) xem thực đơn và gọi món. Người phục vụ bàn nhận order của bạn, đi vào bếp (Server/Database) yêu cầu đầu bếp nấu ăn, và mang món ăn (Dữ liệu phản hồi) ra bàn cho bạn.*

---

## 2. Kiến trúc REST (Representational State Transfer)

**REST** không phải là một công nghệ hay một ngôn ngữ lập trình. REST là một **Mẫu kiến trúc thiết kế (Architectural Style)** do Roy Fielding đề xuất năm 2000 để xây dựng các dịch vụ web. Một hệ thống API tuân thủ các nguyên tắc của REST được gọi là **RESTful API**.

### 6 Ràng buộc cốt lõi của REST:
1. **Client-Server (Khách - Chủ):** Phân tách rõ ràng giao diện người dùng (Client) và logic lưu trữ dữ liệu (Server).
2. **Stateless (Không trạng thái):** Mỗi request phải chứa đầy đủ thông tin để server hiểu và xử lý, không lưu ngữ cảnh trên server.
3. **Cacheable (Khả năng lưu nháp):** Dữ liệu phản hồi phải tự định nghĩa xem nó có được phép lưu cache ở phía client hay không để giảm tải cho server.
4. **Uniform Interface (Giao diện đồng nhất):** Đây là quy tắc quan trọng nhất. Hệ thống API phải có chung một chuẩn định tuyến URL và định dạng dữ liệu giao tiếp (JSON).
5. **Layered System (Hệ thống phân tầng):** Client không cần biết mình đang kết nối trực tiếp với server hay đi qua các máy chủ trung gian (Proxy, Load Balancer).
6. **Code on Demand (Tùy chọn):** Server có thể gửi mã chạy (như file JS) về cho client thực thi (ít dùng).

---

## 3. Quy tắc vàng Thiết kế URL RESTful API

Quy tắc thiết kế quan trọng nhất của RESTful API là: **URL đại diện cho Tài nguyên (Resources), Phương thức HTTP đại diện cho Hành động (Actions).**

> [!IMPORTANT]
> **Quy tắc đặt tên URL:**
> 1. URL phải sử dụng **Danh từ** số nhiều, tuyệt đối không dùng động từ.
> 2. Cấu trúc đi từ cái chung đến cái riêng, phân tách bởi dấu gạch chéo `/`.
> 3. Không dùng chữ viết hoa trong URL, dùng dấu gạch ngang `-` nếu từ ghép dài (kebab-case).

### So sánh thiết kế Tồi vs thiết kế Chuẩn REST:

| Hành động mong muốn | Thiết kế Tồi (RPC style) | Thiết kế Chuẩn REST |
|---|---|---|
| Lấy danh sách sản phẩm | `GET /api/getAllProducts` | **`GET /api/products`** |
| Xem chi tiết sản phẩm ID 5 | `GET /api/getProductDetail?id=5` | **`GET /api/products/5`** |
| Tạo mới sản phẩm | `POST /api/createProduct` | **`POST /api/products`** (kèm body dữ liệu) |
| Cập nhật sản phẩm ID 5 | `POST /api/updateProduct/5` | **`PUT /api/products/5`** (hoặc `PATCH`) |
| Xóa sản phẩm ID 5 | `GET /api/deleteProduct?id=5` | **`DELETE /api/products/5`** |

*Ý nghĩa:* Bạn thấy đấy, cùng một URL `/api/products/5`, nhưng nếu gửi phương thức **GET** thì là xem, gửi **DELETE** thì là xóa, gửi **PUT** thì là sửa. Cách thiết kế này cực kỳ gọn gàng và khoa học!

---

## 4. REST vs RPC

* **RPC (Remote Procedure Call):** Tập trung vào **Hành động (Action/Verb)**. URL thường chứa động từ như `/api/createUser`, `/api/deleteUser`. Phù hợp cho các hệ thống ra lệnh điều khiển (ví dụ: kích hoạt máy quét `/api/startScan`).
* **REST (Representational State Transfer):** Tập trung vào **Tài nguyên (Noun/Resource)**. Thiết kế xoay quanh thực thể dữ liệu. Phù hợp cho hầu hết các hệ thống quản lý thông tin dữ liệu (CRUD).

---

## 5. So sánh với JavaScript (Express.js)

Trong Node.js, bạn viết định tuyến Express như sau:
```javascript
app.get('/api/users/:id', (req, res) => {
    const userId = req.params.id;
    // ...
});
```
Trong C# ASP.NET Core, cơ chế định tuyến (Routing) hoạt động hoàn toàn tương đồng. Bạn sẽ định nghĩa các Route trên Controller bằng các thuộc tính (Attributes) dạng `[HttpGet("api/users/{id}")]`. Việc hiểu sâu tư duy REST giúp bạn viết định tuyến chuẩn xác trên cả C# lẫn Angular.

---

## 6. Checklist đánh giá hiểu bài

1. API đóng vai trò gì trong mô hình ứng dụng Fullstack Angular + ASP.NET?
2. Hãy nêu 3 quy tắc quan trọng nhất khi đặt tên đường dẫn URL cho RESTful API.
3. Tại sao trong URL chuẩn REST không được phép xuất hiện các động từ như `create`, `delete`, `update`?
4. Đọc URL sau và đoán xem nó làm nhiệm vụ gì: `GET /api/authors/10/books`.
5. Sự khác biệt cơ bản giữa REST và RPC là gì?
