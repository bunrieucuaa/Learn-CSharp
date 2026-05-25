# Ghi nhớ Bài 39: File Handling & JSON

## 1. Tóm tắt kiến thức cốt lõi
* **Lưu trữ bền vững (Persistence):** Lưu dữ liệu từ RAM xuống ổ đĩa cứng dưới dạng file để bảo toàn dữ liệu khi chương trình tắt.
* **Lớp tĩnh Helper:** `File` và `Directory` cung cấp các tính năng thao tác nhanh chóng, đơn giản.
* **Streams (Luồng dữ liệu):** Sử dụng `StreamReader` và `StreamWriter` để đọc/ghi file lớn theo cơ chế tải từng phần (chunk) để tiết kiệm RAM.
* **Giải phóng tài nguyên:** Bắt buộc dùng `using` statement hoặc `using` declaration cho các đối tượng stream để tự động đóng file, tránh khoá tệp tin.
* **JSON:** Định dạng trao đổi dữ liệu nhẹ. Tuần tự hóa (`Serialize`) chuyển Object thành chuỗi JSON. Giải tuần tự hóa (`Deserialize`) chuyển chuỗi JSON thành đối tượng C# tương ứng.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi khóa file (File Locking):** Nếu bạn mở một file bằng StreamReader nhưng không bọc trong khối `using`, hệ điều hành sẽ giữ khóa file đó. Khi dòng lệnh tiếp theo cố gắng ghi đè vào file đó, chương trình sẽ báo lỗi `IOException: The process cannot access the file because it is being used by another process`.
> * **Đường dẫn tương đối vs Tuyệt đối:** Khi viết `"demo.txt"`, file sẽ được lưu tại thư mục chứa file thực thi của ứng dụng (thư mục `bin/Debug/net9.0`). Hãy cẩn thận khi xác định đường dẫn lưu trữ.

## 3. Checklist tự đánh giá
- [ ] Bạn đã hiểu tại sao Streams lại giúp tối ưu hóa RAM khi xử lý tệp tin khổng lồ chưa?
- [ ] Bạn đã giải thích được cơ chế hoạt động của từ khóa `using` chưa?
- [ ] Bạn có viết đúng thuộc tính `{ get; set; }` cho class khi muốn serialize sang JSON không?
- [ ] Bạn đã so sánh được sự tương đồng giữa hàm xử lý JSON của JS và C# chưa?
