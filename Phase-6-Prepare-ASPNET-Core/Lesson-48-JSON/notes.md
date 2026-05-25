# Ghi nhớ Bài 48: JSON in Web APIs

## 1. Tóm tắt kiến thức cốt lõi
* **Lệch chuẩn đặt tên:** C# dùng PascalCase, JS/Angular dùng camelCase. Cần cấu hình đồng bộ để tránh bị lỗi map thuộc tính bị `null`.
* **Cấu hình JsonSerializerOptions:**
  * `PropertyNamingPolicy = JsonNamingPolicy.CamelCase`: Tự động chuyển đổi định dạng tên giữa PascalCase và camelCase.
  * `PropertyNameCaseInsensitive = true`: Cho phép so khớp không phân biệt chữ hoa chữ thường.
  * `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull`: Loại bỏ các thuộc tính có giá trị null để tối ưu băng thông mạng.
* **Attribute `[JsonPropertyName("tên_tùy_biến")]`:** Ánh xạ thủ công một thuộc tính JSON cụ thể vào thuộc tính C# (rất hữu ích khi đối tác dùng snake_case).

## 2. Các lỗi hay quên (Gotchas)
> [!WARNING]
> * **Lỗi tham chiếu vòng (Circular Reference):** Lỗi này xảy ra khi bạn cố gắng Serialize một đối tượng có quan hệ lặp vòng tròn (ví dụ: Class `ClassRoom` chứa danh sách `List<Student>`, và Class `Student` lại chứa thuộc tính tham chiếu ngược về `ClassRoom` của nó). Thư viện JSON sẽ bị lặp vô tận và ném ra lỗi `JsonException`.
>   *Cách xử lý:* Thêm cấu hình `ReferenceHandler = ReferenceHandler.IgnoreCycles` vào `JsonSerializerOptions` để tự động ngắt lặp.

## 3. Checklist tự đánh giá
- [ ] Bạn đã giải thích được lý do tại sao thuộc tính C# đôi khi nhận giá trị null khi đọc dữ liệu gửi lên từ client chưa?
- [ ] Bạn có cấu hình được `JsonSerializerOptions` để tự động convert PascalCase sang camelCase không?
- [ ] Bạn đã biết cách ẩn các trường null trong chuỗi JSON xuất ra chưa?
- [ ] Bạn có nắm được cách dùng `[JsonPropertyName]` để map dữ liệu snake_case của đối tác không?
