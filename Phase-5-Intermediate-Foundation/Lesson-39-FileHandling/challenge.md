# Thách thức Bài 39: Quản lý Danh mục Thiết bị Công ty qua Tệp JSON (JSON Persistence CRUD)

## Ngữ cảnh
Bạn đang xây dựng một module quản lý tài sản công nghệ của công ty. Để đơn giản hóa và không cần cài đặt SQL Database phức tạp, ban giám đốc yêu cầu bạn lưu trữ danh sách thiết bị công nghệ (laptops, monitors, v.v.) vào một tệp tin JSON tĩnh dạng cơ sở dữ liệu nội bộ (`inventory.json`).

Bạn cần viết một chương trình Console hoàn chỉnh cho phép quản trị viên thực hiện các thao tác: **Xem danh sách, Thêm mới thiết bị, Cập nhật trạng thái và Xóa thiết bị**. Mỗi khi có sự thay đổi, dữ liệu phải được lưu cứng xuống file ngay lập tức.

## Cấu trúc dữ liệu Thiết bị (Asset)
```csharp
public class Asset
{
    public string AssetId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }        // "Laptop", "Monitor", "Phone"
    public string AssignedTo { get; set; }  // Tên nhân viên sử dụng
    public bool IsInUse { get; set; }
}
```

## Yêu cầu Thách thức
Hãy xây dựng ứng dụng Console có Menu tương tác đáp ứng các chức năng sau:

1. **Khởi tạo dữ liệu:** Khi chương trình chạy, hãy kiểm tra file `inventory.json` có tồn tại chưa?
   - Nếu chưa: Khởi tạo file này với một danh sách rỗng `[]`.
   - Nếu đã có: Đọc toàn bộ danh sách thiết bị từ file nạp vào chương trình.
2. **Xem danh sách (Read):** Hiển thị toàn bộ thiết bị đang quản lý dưới dạng bảng sạch đẹp.
3. **Thêm thiết bị (Create):** Cho phép nhập các thông tin để thêm thiết bị mới vào danh sách. Tạo một thuộc tính `AssetId` tự động (ví dụ: dùng định dạng `AST-01`, `AST-02`... hoặc dùng `Guid.NewGuid().ToString()`). Lưu thay đổi xuống file.
4. **Cập nhật người sử dụng (Update):** Cho phép nhập `AssetId`, nhập tên nhân viên mới để bàn giao thiết bị. Cập nhật `IsInUse = true` và `AssignedTo = tên nhân viên`. Lưu thay đổi xuống file.
5. **Xóa thiết bị (Delete):** Cho phép nhập `AssetId` để xóa hoàn toàn thiết bị đó ra khỏi danh sách hệ thống. Lưu thay đổi xuống file.

## Ràng buộc & Gợi ý nghiệp vụ
- Tránh ghi đè file bị lỗi: Hãy viết một hàm phụ trách việc lưu dữ liệu riêng biệt:
  ```csharp
  static void SaveInventory(List<Asset> assets)
  {
      string jsonString = JsonSerializer.Serialize(assets, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText("inventory.json", jsonString);
  }
  ```
- Mỗi khi người dùng chọn một tính năng, thực hiện thay đổi trên `List<Asset>` trong bộ nhớ, sau đó gọi ngay hàm `SaveInventory` để lưu bền vững dữ liệu xuống ổ cứng.
- Hãy chạy thử và tự đóng mở Console nhiều lần để kiểm chứng dữ liệu nhập từ phiên chạy trước vẫn được giữ nguyên vẹn ở phiên chạy sau!
