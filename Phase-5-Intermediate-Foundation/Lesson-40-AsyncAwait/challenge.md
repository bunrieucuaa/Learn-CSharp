# Thách thức Bài 40: Mô phỏng Bếp nhà hàng thông minh (Restaurant Kitchen Simulator)

## Ngữ cảnh
Một chuỗi nhà hàng thức ăn nhanh muốn tự động hóa hệ thống quản lý bếp. Khi khách hàng đặt các Combo đồ ăn gồm nhiều món (ví dụ: Burger, Khoai tây chiên, Nước ngọt), hệ thống bếp phải phân phối các món này cho các khu vực máy móc chuẩn bị riêng biệt.

Để khách hàng nhận được đồ ăn nhanh nhất và nóng hổi nhất, các món ăn phải được chuẩn bị **song song (bất đồng bộ)**. 
Ví dụ: Trong lúc bếp đang rán Burger (mất 4 giây), khu vực chiên khoai vẫn có thể chiên khoai (mất 2.5 giây) và máy rót nước vẫn rót nước ngọt (mất 1 giây). Tổng thời gian hoàn thành đơn hàng chỉ bằng thời gian của món lâu nhất (4 giây) chứ không được phép làm tuần tự (tốn tới 7.5 giây).

## Yêu cầu Thách thức
Hãy xây dựng chương trình Console giả lập hệ thống bếp bất đồng bộ này.

1. **Định nghĩa các hàm nấu ăn bất đồng bộ:**
   - `async Task<string> PrepareBurgerAsync()`: In ra `"Bắt đầu rán Burger..."`, chờ bất đồng bộ 4 giây (`Task.Delay(4000)`), sau đó trả về chuỗi `"Burger bò phô mai"`.
   - `async Task<string> PrepareFriesAsync()`: In ra `"Bắt đầu chiên khoai tây..."`, chờ bất đồng bộ 2.5 giây (`Task.Delay(2500)`), trả về `"Khoai tây chiên giòn"`.
   - `async Task<string> PrepareDrinkAsync()`: In ra `"Bắt đầu rót nước..."`, chờ bất đồng bộ 1 giây (`Task.Delay(1000)`), trả về `"Ly Pepsi lạnh"`.

2. **Hàm xử lý Đơn hàng:**
   Viết phương thức `async Task ServeComboOrderAsync()`:
   - Sử dụng đồng hồ bấm giờ `Stopwatch` để theo dõi thời gian thực thi.
   - Kích hoạt cả 3 hàm nấu ăn trên **cùng một lúc** (song song).
   - Chờ cả 3 món hoàn thành bằng `Task.WhenAll`.
   - In ra thông báo giao hàng cho khách chứa đầy đủ tên 3 món ăn kèm tổng thời gian chuẩn bị thực tế của bếp.

3. **Yêu cầu nâng cao (Xử lý sự cố):**
   - Giả lập trường hợp máy rót nước bị hỏng (ném ra ngoại lệ `InvalidOperationException("Hết Pepsi!")`).
   - Hãy thiết kế chương trình sao cho nếu máy rót nước bị lỗi, bếp vẫn tiếp tục chuẩn bị xong Burger và Khoai tây, hệ thống bắt được lỗi của máy nước ngọt để báo cho quản lý nhưng không làm sập toàn bộ chương trình của bếp.
