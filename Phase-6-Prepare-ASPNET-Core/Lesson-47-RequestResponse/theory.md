# Bài 47: HTTP Request & Response (Cấu trúc Gói tin HTTP)

Để làm việc hiệu quả với Web API, bạn không thể coi gói tin mạng như một chiếc hộp đen. Bạn cần hiểu rõ cấu trúc vật lý của một gói tin **HTTP Request** gửi đi và **HTTP Response** nhận về chứa những thông tin gì.

Bài học này sẽ bóc tách chi tiết từng thành phần của Request/Response, giúp bạn phân biệt rõ cách truyền dữ liệu bằng Headers, Query Parameters, Route Parameters và Request Body.

---

## 1. Cấu trúc của một HTTP Request

Một gói tin HTTP Request gửi lên từ Client gồm 3 phần chính:

```text
POST /api/users HTTP/1.1                <-- Request Line (Method, Path, Protocol)
Host: api.site.com                      \
Content-Type: application/json           +-- Headers (Metadata)
Authorization: Bearer my_secret_token   /
                                        <-- Dòng trống (Empty line phân cách)
{"Name": "Vy", "Age": 18}               <-- Request Body (Payload dữ liệu)
```

### 1.1. Request Line (Dòng yêu cầu)
Dòng đầu tiên của gói tin, chứa:
* **Method:** Phương thức HTTP (GET, POST...).
* **Path:** Đường dẫn tài nguyên (URL Path).
* **Protocol Version:** Phiên bản HTTP (thường là HTTP/1.1 hoặc HTTP/2).

### 1.2. HTTP Headers (Tiêu đề - Metadata)
Là các cặp Key-Value chứa thông tin cấu hình và siêu dữ liệu (Metadata) mô tả cho gói tin:
* `Content-Type`: Chỉ rõ định dạng của dữ liệu gửi trong Body (ví dụ: `application/json` nghĩa là dữ liệu gửi lên là JSON, `text/html` là trang web).
* `Authorization`: Chứa token xác thực danh tính người dùng (ví dụ: `Bearer [token]`).
* `User-Agent`: Mô tả loại thiết bị/trình duyệt đang gửi request (Chrome, Safari, hoặc C# HttpClient).

### 1.3. Request Body (Thân yêu cầu)
Nơi chứa dữ liệu thực tế (Payload) gửi lên Server. GET request thường không có Body. POST, PUT, PATCH dùng Body để truyền lên các đối tượng dữ liệu phức tạp (JSON).

---

## 2. Phân biệt 3 cách truyền dữ liệu trong Request

Đây là phần cực kỳ quan trọng khi bạn làm việc với cả Angular (Client) lẫn ASP.NET Core (Server):

### 2.1. Route Parameters (Tham số đường dẫn)
* **Khái niệm:** Dữ liệu nằm trực tiếp trong cấu trúc đường dẫn URL.
* **Định dạng:** `/api/users/5` (ID = 5 là route param).
* **Khi nào dùng:** Dùng để chỉ định cụ thể **Tài nguyên duy nhất** cần truy cập (như ID, Khóa chính).

### 2.2. Query Parameters (Tham số truy vấn)
* **Khái niệm:** Dữ liệu nằm sau dấu chấm hỏi `?` ở cuối URL, các tham số nối nhau bằng dấu `&`.
* **Định dạng:** `/api/users?page=2&size=10` (page = 2 và size = 10 là query params).
* **Khi nào dùng:** Dùng cho các hành động phụ trợ như **Lọc (Filtering), Sắp xếp (Sorting), hoặc Phân trang (Pagination)** trên danh sách.

### 2.3. Request Body (Thân dữ liệu)
* **Khái niệm:** Dữ liệu ẩn bên trong thân gói tin, thường là chuỗi JSON.
* **Khi nào dùng:** Dùng khi cần gửi **đối tượng dữ liệu lớn, phức tạp hoặc thông tin nhạy cảm** lên Server (dùng cho POST, PUT).

---

## 3. Cấu trúc của một HTTP Response

Gói tin phản hồi từ Server gửi về cho Client cũng có cấu trúc 3 phần tương ứng:

```text
HTTP/1.1 200 OK                         <-- Status Line (Protocol, Status Code)
Content-Type: application/json          \
Server: Kestrel                         +-- Headers (Metadata)
Content-Length: 120                     /
                                        <-- Dòng trống
{"Id": 1, "Status": "Success"}          <-- Response Body (Dữ liệu trả về)
```

* **Status Line (Dòng trạng thái):** Chứa phiên bản giao thức và mã Status Code (ví dụ: `200 OK`).
* **Headers:** Cung cấp thông tin về dữ liệu trả về (kiểu file `Content-Type`, độ dài `Content-Length`, thông tin Web Server).
* **Response Body:** Dữ liệu thực tế Server gửi về cho trình duyệt (thường là dữ liệu JSON để Angular parse ra hiển thị lên giao diện).

---

## 4. Checklist đánh giá hiểu bài

1. Vẽ sơ đồ cấu trúc 3 phần của một gói tin HTTP Request.
2. Header `Content-Type` có vai trò gì? Nếu client gửi JSON nhưng không set `Content-Type: application/json` thì chuyện gì xảy ra?
3. Phân biệt sự khác nhau và trường hợp sử dụng của: Route parameters, Query parameters, và Request Body.
4. Đọc URL sau và chỉ ra đâu là Route parameter, đâu là Query parameter:
   `/api/categories/shoes/products?priceMin=50&brand=nike`
