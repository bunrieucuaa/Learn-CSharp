# Thách thức Bài 45: Thiết kế Trình phân luồng yêu cầu HTTP (HTTP Router Handler)

## Ngữ cảnh
Trước khi học các framework Web mạnh mẽ tự động hóa mọi thứ (như ASP.NET Core), bạn cần tự tay viết một bộ máy phân phối yêu cầu (**Router Handler**) thô sơ. Điều này giúp bạn hiểu bản chất cách Web Server tiếp nhận một gói tin HTTP Request, bóc tách phương thức (Method) và đường dẫn (Path) để điều phối sang hàm xử lý tương ứng và trả về mã phản hồi (Status Code) phù hợp.

Nhiệm vụ của bạn là hiện thực hóa bộ Router Handler này bằng C#.

## Cấu trúc dữ liệu yêu cầu (Request Class)
```csharp
public class MockRequest
{
    public string Method { get; set; } // "GET", "POST", "PUT", "DELETE"
    public string Path { get; set; }   // ví dụ: "/api/products", "/api/products/1"
    public string Body { get; set; }   // Nội dung JSON payload gửi lên (nếu có)
}

public class MockResponse
{
    public int StatusCode { get; set; }
    public string Body { get; set; }
}
```

## Yêu cầu Thách thức
Hãy xây dựng class `Router` chứa phương thức:
```csharp
public static MockResponse HandleRequest(MockRequest request)
```

**Quy tắc điều phối của Router (Routing Rules):**
1. **GET `/api/products`:** Trả về danh sách sản phẩm giả lập dưới dạng chuỗi và mã `200 OK`.
2. **POST `/api/products`:** 
   - Kiểm tra thuộc tính `Body` của request. Nếu `Body` bị trống hoặc null, trả về mã lỗi `400 Bad Request` kèm thông báo `"Body dữ liệu không được trống"`.
   - Nếu hợp lệ, giả lập lưu thành công và trả về mã `201 Created` kèm thông báo `"Tạo mới thành công sản phẩm: [tên body]"`.
3. **DELETE `/api/products/1`:** Giả lập xóa thành công sản phẩm có ID = 1, trả về mã `200 OK` (hoặc `204 No Content`).
4. **DELETE `/api/products/99`:** Giả lập sản phẩm ID = 99 không tồn tại, trả về mã `404 Not Found`.
5. **Mọi URL khác:** Trả về mã `404 Not Found` kèm thông báo `"Đường dẫn không tồn tại"`.
6. **Mọi phương thức không hợp lệ** (ví dụ gửi PATCH đến `/api/products`): Trả về mã lỗi `405 Method Not Allowed`.

Hãy viết đầy đủ code xử lý Router và dựng kịch bản kiểm thử trong hàm `Main` gửi liên tục 5 request giả lập khác nhau để kiểm chứng mã trạng thái trả về đúng như kỳ vọng.
