# Ghi nhớ Bài 46: API Concept & RESTful Design

## 1. Tóm tắt kiến thức cốt lõi
* **API:** Cầu nối trung gian cho phép các phần mềm trao đổi dữ liệu.
* **REST:** Mẫu kiến trúc thiết kế dịch vụ web dựa trên tài nguyên.
* **Uniform Interface:** Ràng buộc giao diện đồng nhất của REST.
* **Quy tắc thiết kế URL REST:**
  * Chỉ dùng **Danh từ số nhiều** đại diện cho tài nguyên (ví dụ: `/api/books`, `/api/users`).
  * Tuyệt đối không dùng động từ.
  * Sử dụng chữ thường, ngăn cách bằng dấu `-` (kebab-case).
  * HTTP Method đại diện cho hành động CRUD tương ứng trên tài nguyên đó.
* **Sub-resource Routing:** Thiết kế URL lồng nhau đại diện cho tài nguyên phụ thuộc (ví dụ: `/api/users/1/orders`).

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi nhét động từ vào URL:** Một lỗi kinh điển là cố viết `/api/createUser` hay `/api/updateBook/5`. Hãy nhớ rằng trong REST, URL chỉ đại diện cho tài nguyên, hành động đã được đại diện bởi phương thức HTTP (`POST`, `PUT`). Viết động từ vào URL là bạn đang chuyển sang kiến trúc RPC truyền thống chứ không còn là REST chuẩn mực nữa.
> * **Sử dụng sai số nhiều/số ít:** Viết `/api/user` (số ít) thay vì `/api/users`. Hãy luôn sử dụng danh từ số nhiều để đại diện cho một "tập hợp" tài nguyên trong hệ thống.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao REST API lại ưa chuộng sử dụng danh từ số nhiều trong URL chưa?
- [ ] Bạn có tự thiết kế được cấu trúc URL lồng nhau cho các quan hệ cha-con chưa?
- [ ] Bạn phân biệt được sự khác nhau giữa RPC (tập trung hành động) và REST (tập trung tài nguyên) chưa?
- [ ] Bạn có biết cách ánh xạ các Method GET/POST/PUT/DELETE sang các hành động CRUD tương ứng không?
