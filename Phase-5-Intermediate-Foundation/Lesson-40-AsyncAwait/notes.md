# Ghi nhớ Bài 40: Async & Await

## 1. Tóm tắt kiến thức cốt lõi
* **Bất đồng bộ (Asynchrony):** Giải phóng luồng (Thread) gọi hàm trong lúc chờ tác vụ I/O (đọc file, gọi API) hoàn thành, giúp nâng cao hiệu năng hệ thống.
* **Task & Task<T>:** Kiểu trả về đại diện cho một tác vụ chạy ngầm. `Task` tương đương `void`, `Task<T>` tương đương kiểu trả về `T`.
* **Async/Await:** Cú pháp viết code bất đồng bộ tự nhiên, tuần tự giống như code đồng bộ. Compiler tự dịch thành State Machine quản lý luồng ngầm.
* **Parallel Tasks:** Dùng `Task.WhenAll` để kích hoạt và đợi nhiều tác vụ chạy song song cùng lúc nhằm tối ưu hóa thời gian.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi Deadlock với .Result và .Wait():**
>   Tuyệt đối tránh sử dụng hai lệnh đồng bộ hóa này trên một đối tượng `Task` trong ứng dụng Web/UI vì nó sẽ khóa chặt Thread của hệ thống và gây ra lỗi nghẽn chết (Deadlock). Hãy luôn sử dụng `await` xuyên suốt tất cả các tầng (Async all the way).
> * **Lỗi Async Void:** Tránh viết `async void` trừ khi viết trình xử lý sự kiện UI (Event Handler). Dùng `async Task` để có thể bắt được ngoại lệ (Exceptions) khi chạy bất đồng bộ.

## 3. Checklist tự đánh giá
- [ ] Bạn đã phân biệt được sự khác nhau giữa chạy song song (Parallel) và bất đồng bộ (Async) chưa?
- [ ] Bạn có nắm được hậu quả của việc lạm dụng `.Result` trong code C# không?
- [ ] Bạn có biết cách kết hợp và chờ đợi nhiều Task chạy song song cùng lúc không?
- [ ] Bạn đã hiểu cơ chế đa luồng Thread Pool của C# khác gì Event Loop của JS chưa?
