# Thách thức Bài 38: Giải cứu hệ thống bán vé rạp chiếu phim (Cinema Ticket System)

## Ngữ cảnh
Một rạp chiếu phim lớn đang gặp sự cố: Hệ thống tính tiền vé tự động bị lỗi và code quá bẩn khiến đội ngũ kỹ thuật hiện tại không thể tìm ra nguyên nhân lỗi ở đâu. Quản lý dự án chuyển giao đoạn code này cho bạn và yêu cầu:
1. **Refactor lại toàn bộ đoạn code bẩn này** để nó trở nên sạch sẽ, tuân thủ Clean Code và sử dụng LINQ.
2. Tìm ra lỗi tiềm ẩn (bug) khiến hệ thống đôi khi tính toán sai giá vé.

## Đoạn code legacy bị lỗi và siêu bẩn
```csharp
using System;
using System.Collections.Generic;

public class Tkt
{
    public string m { get; set; }   // Tên phim (movie)
    public double p { get; set; }   // Giá vé gốc (price)
    public int a { get; set; }      // Tuổi người mua (age)
    public bool s { get; set; }      // Khách hàng có thẻ học sinh/sinh viên không (student status)
    public string t { get; set; }   // Loại phòng chiếu: "Standard", "IMAX", "3D"
}

public class CinemaProcessor
{
    // Hàm tính tổng doanh thu bán vé thực tế cho một nhóm khách hàng
    // Đã áp dụng giảm giá:
    // - Trẻ em dưới 12 tuổi: Giảm 50% giá vé gốc
    // - Học sinh sinh viên (s == true): Giảm 20% giá vé gốc (nếu không phải trẻ em)
    // - Phòng chiếu IMAX: Phụ thu thêm 5$ vào giá vé gốc sau khi tính giảm giá
    // - Phòng chiếu 3D: Phụ thu thêm 3$ vào giá vé gốc sau khi tính giảm giá
    public double CalculateTotalSales(List<Tkt> tkts)
    {
        double tot = 0;
        foreach (var x in tkts)
        {
            double finalP = x.p;

            // Tính giảm giá tuổi
            if (x.a < 12)
            {
                finalP = x.p * 0.5;
            }
            else if (x.s == true)
            {
                // Giảm giá học sinh sinh viên
                finalP = x.p * 0.8;
            }

            // Tính phụ thu phòng chiếu
            if (x.t == "IMAX")
            {
                finalP = finalP + 5;
            }
            if (x.t == "3D")
            {
                finalP = finalP + 3;
            }

            // Tính tổng
            tot = tot + finalP;
        }
        return tot;
    }
}
```

## Yêu cầu Thách thức
Hãy tiến hành refactor class `Tkt` và class `CinemaProcessor` đáp ứng các tiêu chí sau:
1. **Đổi tên sạch sẽ:** Đổi tên class `Tkt` thành `Ticket` và các thuộc tính viết tắt thành tên đầy đủ có nghĩa.
2. **Loại bỏ vòng lặp bẩn:** Dùng LINQ Method Syntax để tính tổng thay vì dùng vòng lặp `foreach` thủ công với biến tích lũy cộng dồn `tot`.
3. **Tách hàm đơn nhiệm:** Tách logic tính toán giá vé cho **từng vé riêng lẻ** thành một hàm riêng biệt (ví dụ: `CalculateTicketPrice(Ticket ticket)`).
4. **Sử dụng Switch Expression:** Dùng switch expression trong C# 8+ để tính toán phụ thu phòng chiếu và tính giá vé thay vì viết các câu lệnh `if` rời rạc.
5. **Tìm và sửa lỗi (Bug):** Hãy phân tích xem logic tính giảm giá trong code cũ đang gặp lỗi gì nếu một học sinh sinh viên 10 tuổi đi mua vé xem phim? (Gợi ý: Trẻ em dưới 12 tuổi là học sinh sinh viên sẽ bị tính thế nào trong code cũ? Họ có được giảm 50% không?).
