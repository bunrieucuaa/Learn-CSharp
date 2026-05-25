# Ghi nhớ Bài 33: Filtering

## 1. Tóm tắt kiến thức cốt lõi
* **Lọc điều kiện:** Dùng `.Where(lambda)`.
* **Lọc kiểu:** Dùng `.OfType<T>()` để lấy đúng kiểu và tự động bỏ qua phần tử sai kiểu mà không lỗi.
* **Lọc trùng:** Dùng `.Distinct()`. Nhớ implement `IEquatable<T>` cho custom class nếu so sánh đối tượng.
* **Phân trang:** Sự kết hợp hoàn hảo giữa `.Skip(m)` và `.Take(n)`.
* **Toán tử lấy 1 phần tử:**
  * `First` vs `FirstOrDefault`: Tìm phần tử đầu tiên. Tránh lỗi ném ra ngoại lệ khi rỗng bằng cách dùng bản `OrDefault`.
  * `Single` vs `SingleOrDefault`: Đòi hỏi dữ liệu chỉ tồn tại **duy nhất 1** phần tử thỏa mãn. Dùng khi check khoá chính (ID/Email).

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lấy giá trị mặc định của Value Type:** `FirstOrDefault()` trên danh sách `int` nếu không tìm thấy sẽ trả về `0`. Nếu bạn muốn phân biệt giữa "không tìm thấy" và "tìm thấy số 0", hãy dùng danh sách kiểu `int?` (nullable int) để nhận giá trị `null` khi không tìm thấy.
> * **Thứ tự Skip và Take:** Luôn thực hiện `.Skip()` trước rồi mới `.Take()`. Nếu đảo ngược lại, bạn sẽ bị giới hạn số lượng phần tử trước rồi mới bỏ qua, gây mất mát hoặc sai lệch dữ liệu.

## 3. Checklist tự đánh giá
- [ ] Bạn đã phân biệt rõ trường hợp nào nên dùng `First` và trường hợp nào nên dùng `Single` chưa?
- [ ] Bạn có biết cách thiết lập `Distinct` hoạt động chính xác trên class tự định nghĩa không?
- [ ] Bạn có tự tin viết code phân trang sản phẩm cho Web API không?
