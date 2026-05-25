# Ghi nhớ Bài 37: Extension Methods

## 1. Tóm tắt kiến thức cốt lõi
* **Mục đích:** Bổ sung phương thức mới cho Class/Interface có sẵn mà không cần thay đổi source code gốc hay kế thừa.
* **3 điều kiện khai báo bắt buộc:**
  1. Class chứa method phải là `static class`.
  2. Method phải là `static method`.
  3. Tham số đầu tiên phải có từ khóa `this` đứng trước kiểu dữ liệu cần mở rộng.
* **Bản chất:** Chỉ là Syntactic Sugar (cú pháp viết tắt). Khi chạy, trình biên dịch tự động dịch thành lời gọi static truyền thống (`HelperClass.Method(object)`), không làm chậm ứng dụng.
* **LINQ:** Toàn bộ các toán tử LINQ thực chất là Extension Methods mở rộng cho interface `IEnumerable<T>`.

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Namespace:** Nếu bạn viết Extension Method trong một namespace khác với nơi sử dụng, bạn bắt buộc phải import namespace đó bằng câu lệnh `using` thì IDE mới nhận diện được phương thức mở rộng.
> * **Ưu tiên Instance Method:** Nếu hàm thành viên của class trùng tên và chữ ký (signature) với hàm mở rộng, C# sẽ luôn ưu tiên gọi hàm thành viên của class. Hàm mở rộng sẽ bị bỏ qua và không thể gọi được.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao Extension Method không làm suy giảm hiệu năng phần mềm chưa?
- [ ] Bạn có nắm được lý do tại sao LINQ lại hoạt động được trên cả `List<T>`, `T[]` và `Dictionary` không?
- [ ] Bạn có phân biệt được sự an toàn của Extension Method trong C# so với Ghi đè prototype trong JS không?
