# Bài tập Thực hành Bài 47: HTTP Request & Response

Hoàn thành các bài tập dưới đây để làm quen với cấu trúc gói tin HTTP.

---

## Bài tập 1: Chỉ ra các thành phần của Request
**Đề bài:**
Cho gói tin HTTP Request sau:
```text
GET /api/v1/blogs/125?mode=read HTTP/1.1
Host: blogsite.com
User-Agent: Mozilla/5.0
Accept: application/json
```
Hãy chỉ ra:
1. HTTP Method của request là gì?
2. Path của tài nguyên cần truy xuất là gì?
3. Giá trị của Header `User-Agent` là gì?
4. Đâu là Route Parameter, đâu là Query Parameter? Giá trị của chúng là bao nhiêu?
5. Request này có chứa Body không? Tại sao?

---

## Bài tập 2: Thiết kế cấu trúc Request tạo mới Sản phẩm
**Đề bài:**
Bạn muốn tạo mới một sản phẩm có tên `"Bàn phím cơ"`, giá `$80`. 
Hãy tự viết (vẽ dạng text) cấu trúc của gói tin HTTP Request gửi lên Server. Yêu cầu:
- Sử dụng phương thức phù hợp.
- Đặt URL đúng chuẩn RESTful API.
- Thiết lập Header chỉ rõ kiểu dữ liệu gửi đi là JSON.
- Đặt nội dung dữ liệu JSON của sản phẩm vào đúng vị trí trong gói tin.

---

## Bài tập 3: Đọc và bóc tách Header "Authorization"
**Đề bài:**
Khi làm việc với xác thực, Client thường gửi một token trong Header `Authorization` dưới dạng:
`Authorization: Bearer xyz123_my_secret_token`
Hãy viết một hàm trong C# có nguyên mẫu:
`public static string ExtractToken(string authorizationHeaderValue)`
Hàm này nhận vào giá trị của header và cắt bỏ chữ `"Bearer "` ở đầu để lấy ra chuỗi token thực tế phía sau. Nếu giá trị không bắt đầu bằng `"Bearer "`, trả về chuỗi rỗng.

* **Expected Output:**
  - `ExtractToken("Bearer token_key")` -> `"token_key"`
  - `ExtractToken("Basic admin:123")` -> `""`

---

## Bài tập 4: Phân tích kiểu dữ liệu truyền (Content-Type)
**Đề bài:**
Giải thích sự khác nhau giữa hai giá trị Header `Content-Type` sau đây của HTTP Request:
1. `Content-Type: application/json`
2. `Content-Type: text/plain`
* **Câu hỏi:** Nếu client gửi chuỗi `"{\"Name\":\"Vy\"}"` nhưng set `Content-Type: text/plain`, Server sẽ xử lý chuỗi này như thế nào? Nó có tự động chuyển đổi thành đối tượng C# được không?

---

## Bài tập 5: Thiết kế Query String lọc đa điều kiện
**Đề bài:**
Bạn đang thiết kế trang lọc tìm kiếm ô tô. Người dùng muốn:
- Lọc ô tô có hãng (`brand`) là `toyota`.
- Lọc xe có màu (`color`) là `red`.
- Sắp xếp (`sortBy`) theo giá tiền (`price`).
- Xem dữ liệu ở trang thứ `3` (`page`).
Hãy viết đường dẫn URL đầy đủ (bắt đầu bằng `/api/cars...`) chứa toàn bộ các Query Parameters đại diện cho bộ lọc trên.
