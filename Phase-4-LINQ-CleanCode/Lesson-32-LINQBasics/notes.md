# Ghi nhớ Bài 32: LINQ Basics

## 1. Tóm tắt kiến thức cốt lõi
* **LINQ (Language Integrated Query):** Giúp viết các câu lệnh truy vấn dữ liệu đồng nhất trên Object, SQL DB, XML ngay trong code C#.
* **Lambda Expression:** Cú pháp `x => x.Property` giúp truyền điều kiện lọc một cách ngắn gọn (tương tự Arrow function của JS).
* **2 Cú pháp:**
  * **Query Syntax:** Dễ đọc với người dùng SQL (`from... where... select`).
  * **Method Syntax (Khuyên dùng):** Dùng phương thức mở rộng nối tiếp nhau, hỗ trợ 100% các hàm của LINQ.
* **Deferred Execution (Trì hoãn thực thi):** 
  * Bản chất LINQ là lưu kế hoạch truy vấn chứ không chạy lọc ngay.
  * Chỉ thực sự thực thi khi duyệt `foreach` hoặc gọi các hàm kích hoạt như `.ToList()`, `.ToArray()`, `.Count()`.

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Null Reference Exception:** Trước khi lọc LINQ trên danh sách, hãy đảm bảo danh sách đó không bị `null`.
> * **Hiệu năng IQueryable vs IEnumerable:** Khi làm việc với database (Entity Framework), lọc trước khi gọi `.ToList()` sẽ dịch thành SQL và chạy dưới DB. Gọi `.ToList()` trước sẽ lôi toàn bộ bảng vào RAM rồi mới lọc trên RAM (cực kỳ tốn RAM và chậm).

## 3. Checklist tự đánh giá
- [ ] Bạn có phân biệt được Method Syntax và Query Syntax không?
- [ ] Bạn đã giải thích được cơ chế hoạt động của Deferred Execution chưa?
- [ ] Bạn có biết cách ép câu lệnh LINQ chạy ngay lập tức không?
- [ ] Bạn đã nắm được sự giống nhau giữa Lambda trong C# và Arrow Function trong JS chưa?
