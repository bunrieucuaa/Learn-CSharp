# Thách thức Bài 35: Thuật toán Sắp xếp Bảng hàng không (Flight Schedule)

## Ngữ cảnh
Bạn đang phát triển hệ thống hiển thị bảng chuyến bay tại một sân bay quốc tế. Màn hình bảng điện tử cần hiển thị thông tin chuyến bay được sắp xếp thông minh để hành khách dễ theo dõi.

Thông tin mỗi chuyến bay gồm:
* Mã chuyến bay (`FlightNumber`)
* Hãng hàng không (`Airline`)
* Trạng thái chuyến bay (`Status`): "Delayed" (Trễ giờ), "Boarding" (Đang lên máy bay), "Scheduled" (Theo kế hoạch)
* Thời gian cất cánh (`DepartureTime`)

## Quy tắc sắp xếp hiển thị của Sân bay:
1. Các chuyến bay đang **"Boarding"** luôn là khẩn cấp nhất, bắt buộc phải hiển thị lên hàng đầu bảng.
2. Tiếp theo là các chuyến bay **"Scheduled"**.
3. Cuối cùng mới là các chuyến bay bị **"Delayed"** (vì họ sẽ phải đợi lâu hơn).
4. Đối với các chuyến bay có **cùng trạng thái**, ưu tiên hiển thị chuyến bay có **thời gian cất cánh sớm hơn** (cũ hơn về mốc thời gian).
5. Nếu trùng cả trạng thái lẫn thời gian cất cánh, sắp xếp theo tên hãng **Airline** từ A-Z.

## Cấu trúc dữ liệu
```csharp
public class Flight
{
    public string FlightNumber { get; set; }
    public string Airline { get; set; }
    public string Status { get; set; } // "Boarding", "Scheduled", "Delayed"
    public DateTime DepartureTime { get; set; }
}
```

## Dữ liệu mock-up đầu vào
```csharp
DateTime baseTime = DateTime.Today;

List<Flight> airportBoard = [
    new Flight { FlightNumber = "VN102", Airline = "Vietnam Airlines", Status = "Delayed", DepartureTime = baseTime.AddHours(10) },
    new Flight { FlightNumber = "VJ203", Airline = "VietJet Air", Status = "Boarding", DepartureTime = baseTime.AddHours(9) },
    new Flight { FlightNumber = "QH501", Airline = "Bamboo Airways", Status = "Scheduled", DepartureTime = baseTime.AddHours(9).AddMinutes(30) },
    new Flight { FlightNumber = "VN105", Airline = "Vietnam Airlines", Status = "Boarding", DepartureTime = baseTime.AddHours(8) },
    new Flight { FlightNumber = "VJ205", Airline = "VietJet Air", Status = "Scheduled", DepartureTime = baseTime.AddHours(9).AddMinutes(30) }
];
```

## Yêu cầu Thách thức
Hãy xây dựng câu truy vấn LINQ sử dụng Method Syntax để sắp xếp danh sách `airportBoard` tuân thủ chính xác 5 quy tắc sắp xếp trên.

* **Gợi ý cực kỳ quan trọng:** Để xếp hạng trạng thái ("Boarding" > "Scheduled" > "Delayed"), bạn có thể tự viết một Custom Comparer cho trường `Status` hoặc ánh xạ thuộc tính này sang trọng số điểm số (ví dụ: dùng biểu thức chuyển đổi switch-expression trong hàm so sánh hoặc trong hàm `OrderBy`).
* **Expected Output:**
  ```text
  - VJ203 (VietJet Air) | Status: Boarding | Time: 09:00 AM
  - VN105 (Vietnam Airlines) | Status: Boarding | Time: 08:00 AM
  - QH501 (Bamboo Airways) | Status: Scheduled | Time: 09:30 AM
  - VJ205 (VietJet Air) | Status: Scheduled | Time: 09:30 AM
  - VN102 (Vietnam Airlines) | Status: Delayed | Time: 10:00 AM
  ```
  *(Chú thích: Chuyến bay VJ203 và VN105 đều là Boarding, nhưng tại sao VJ203 lại xếp trước? Chú ý kỹ quy tắc 1: Boarding phải lên đầu, nhưng tại sao VN105 DepartureTime lúc 8h (sớm hơn 9h) lại có thể xếp sau hoặc xếp trước? Hãy lập trình cẩn thận phần so sánh này).*
  *Đính chính:* Nếu sắp xếp theo thời gian sớm hơn lên trước thì VN105 (8h) phải xếp trước VJ203 (9h). Hãy viết code để output của bạn đảm bảo logic này!
