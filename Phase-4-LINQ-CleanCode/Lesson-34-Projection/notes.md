# Ghi nhớ Bài 34: Projection

## 1. Tóm tắt kiến thức cốt lõi
* **Select (1-1):** Map từng phần tử ban đầu thành một phần tử mới. Tương tự như `.map()` của JS.
* **SelectMany (1-N):** Làm phẳng các danh sách lồng nhau thành một danh sách phẳng duy nhất. Tương tự như `.flatMap()` của JS.
* **Zip:** Kết hợp song song các tập hợp có độ dài tương thích.
* **Anonymous Type `new { ... }`:** Khởi tạo nhanh đối tượng lưu trữ tạm thời mà không cần định nghĩa Class mới. Trình biên dịch tự gán thuộc tính chỉ đọc (Read-only).

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Lạm dụng Anonymous Types:** Anonymous Type là kiểu dữ liệu không tên, chỉ nên dùng cục bộ trong phương thức để xử lý logic trung gian. Nếu bạn viết một phương thức `public object GetStudentData()` trả về một Anonymous Type, phía gọi hàm sẽ cực kỳ khó truy cập các thuộc tính của nó (phải dùng dynamic hoặc reflection). Trong trường hợp đó, hãy định nghĩa một Class DTO rõ ràng.
> * **Zip lệch độ dài:** Nếu ghép đôi hai danh sách có độ dài không bằng nhau bằng `Zip`, kết quả trả về sẽ tự động bị giới hạn theo độ dài của danh sách ngắn nhất.

## 3. Checklist tự đánh giá
- [ ] Bạn đã phân biệt được khi nào dùng `Select` và khi nào dùng `SelectMany` chưa?
- [ ] Bạn có nắm được cú pháp viết Anonymous Type không?
- [ ] Bạn có biết tại sao các thuộc tính trong Anonymous Type lại không thể gán lại giá trị không?
- [ ] Bạn đã giải thích được sự tương đồng giữa `Select` C# và `map` JS chưa?
