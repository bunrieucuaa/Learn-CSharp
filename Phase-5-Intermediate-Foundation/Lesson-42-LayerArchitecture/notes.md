# Ghi nhớ Bài 42: Layer Architecture Basics

## 1. Tóm tắt kiến thức cốt lõi
* **Tư duy phân tách trách nhiệm (SoC):** Không viết gộp toàn bộ logic vào một file. Chia nhỏ code thành các khu vực chuyên trách riêng biệt.
* **Mô hình 3-Layer:**
  1. **Presentation (UI):** Tương tác với người dùng, thu thập dữ liệu input, in kết quả output. Không chứa logic nghiệp vụ hay truy xuất file/DB.
  2. **Business Logic (Service):** Xử lý quy tắc nghiệp vụ cốt lõi, kiểm tra ràng buộc logic. Không tương tác trực tiếp với giao diện Console hay DB.
  3. **Data Access (Repository):** Chỉ phụ trách đọc, ghi dữ liệu vào cơ sở dữ liệu hoặc tệp tin. Không quan tâm logic nghiệp vụ được tính thế nào.
* **Quy tắc luồng phụ thuộc:** Phụ thuộc một chiều từ trên xuống dưới: `UI -> Service -> Data Access`.

## 2. Các lỗi hay quên (Gotchas)
> [!CAUTION]
> * **Lỗi ô nhiễm UI trong Service:** Viết code Console (`Console.ReadLine()`, `Console.WriteLine()`) hoặc code giao diện trong Service. Điều này khiến Service bị ghép cứng vào Console App, không thể tái sử dụng được nếu sau này dự án chuyển sang Web API (nơi giao diện được dựng bằng Angular/HTML và dữ liệu được truyền nhận qua JSON).
> * **Lỗi gọi nhảy tầng:** Cho tầng UI gọi thẳng xuống tầng Data Access để lấy file hiển thị nhằm viết code nhanh hơn. Điều này vi phạm nguyên tắc luồng phụ thuộc, làm mất đi tầng kiểm tra logic nghiệp vụ ở giữa.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được tại sao viết code phân tầng lại giúp chia việc làm việc nhóm (Teamwork) dễ dàng hơn chưa?
- [ ] Bạn có biết cách xử lý lỗi khi một Service cần in thông báo lỗi lên màn hình Console mà không vi phạm nguyên lý phân tầng không? (Gợi ý: Ném Exception).
- [ ] Bạn có tự tin vẽ lại sơ đồ cấu trúc thư mục của một dự án C# chuẩn 3-Layer không?
