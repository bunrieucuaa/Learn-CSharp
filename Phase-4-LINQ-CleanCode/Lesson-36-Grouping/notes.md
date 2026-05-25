# Ghi nhớ Bài 36: Grouping & Joins

## 1. Tóm tắt kiến thức cốt lõi
* **GroupBy:** Chia bộ sưu tập thành các nhóm dạng `IGrouping<TKey, TElement>`. Mỗi nhóm có thuộc tính `.Key` và bản thân nhóm có thể dùng để lặp/truy vấn.
* **ToLookup:** Khác với GroupBy hoạt động trì hoãn (Deferred), ToLookup thực thi gom nhóm tức thì (Eager) và lưu kết quả dạng Read-only vào RAM.
* **Join:** Inner Join 2 bộ sưu tập dựa trên so khớp khóa. Bỏ qua các phần tử không khớp ở cả 2 bảng.
* **GroupJoin:** Left Join 2 bộ sưu tập. Giữ nguyên toàn bộ phần tử bảng 1, phần tử bảng 2 khớp khóa sẽ được nhét vào một danh sách con.
* **Hàm Aggregation:** Thường dùng kèm sau khi GroupBy để tính toán thống kê nhóm (`Count`, `Sum`, `Average`, `Min`, `Max`).

## 2. Các lỗi hay quên (Gotchas)
> [!IMPORTANT]
> * **Lỗi ném Exception khi Key bị null:** Hãy đảm bảo thuộc tính khóa bạn dùng để `GroupBy` hoặc `Join` không bị `null`. Nếu khoá bị null, chương trình có thể phát sinh lỗi `NullReferenceException` khi truy cập hoặc xử lý so sánh.
> * **Hành vi của GroupJoin khi không khớp khóa:** Danh sách kết quả con nhận được sẽ là một tập hợp rỗng (Empty collection) chứ không phải là `null`. Bạn có thể gọi `.Count()` hoặc các hàm LINQ trên tập con đó an toàn mà không sợ bị sập app.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được kiểu dữ liệu `IGrouping` hoạt động như thế nào chưa?
- [ ] Bạn có biết cách tính tổng và trung bình cộng trực tiếp trên kết quả của một nhóm không?
- [ ] Bạn phân biệt được khi nào dùng `Join` (Inner Join) và khi nào dùng `GroupJoin` (Left Outer Join) chưa?
- [ ] Bạn có biết toán tử gom nhóm tương ứng của JS là gì không?
