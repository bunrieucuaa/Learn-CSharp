# 🏆 Lesson 14 — Challenge: Exception Handling

---

## Challenge: Hệ thống đặt vé xem phim 🎬 (Robust Edition)

### Bối cảnh:
> "Nâng cấp hệ thống đặt vé phim — TẤT CẢ lỗi phải được xử lý, chương trình KHÔNG BAO GIỜ crash."

### Custom Exceptions:

```csharp
class ShowtimeSoldOutException : Exception
{
    public string MovieTitle { get; }
    public string Showtime { get; }
    public ShowtimeSoldOutException(string movie, string time)
        : base($"Suất {time} phim \"{movie}\" đã hết vé!") { ... }
}

class SeatUnavailableException : Exception
{
    public string Seat { get; }  // Ví dụ: "A5"
    // ...
}

class PaymentFailedException : Exception
{
    public decimal Amount { get; }
    public string Reason { get; }
    // ...
}

class BookingLimitException : Exception
{
    // Mỗi người tối đa 6 vé / suất
    // ...
}
```

### Dữ liệu (mảng):

```csharp
// 5 phim
string[] movies;
string[] genres;       // Action, Comedy, Horror...
int[] durations;       // phút
decimal[] prices;      // Giá vé

// Suất chiếu: mỗi phim có 3 suất (mảng 2D)
string[,] showtimes;    // [phim, suất]: "10:00", "14:00", "19:00"
int[,] seatsAvailable;  // [phim, suất]: số ghế còn lại

// Booking history
string[] bookingIds;    // BK0001, BK0002...
string[] bookingDetails;
int bookingCount = 0;
```

### Chức năng:

#### 1. Xem danh sách phim
```
╔═══════════════════════════════════════════════════╗
║              🎬 CINEMA ROYAL                      ║
╠═══╦══════════════════╦════════╦════════╦══════════╣
║ # ║ Phim             ║ Thể loại ║ Thời lượng ║ Giá     ║
╠═══╬══════════════════╬════════╬════════╬══════════╣
║ 1 ║ Avengers 6       ║ Action ║ 150'   ║ 120,000 ║
║ 2 ║ Inside Out 3     ║ Anim.  ║ 110'   ║  90,000 ║
...
```

#### 2. Đặt vé (TẤT CẢ lỗi phải bắt):
- Chọn phim → `ArgumentOutOfRangeException` nếu sai
- Chọn suất → `ShowtimeSoldOutException` nếu hết vé
- Chọn số lượng → `BookingLimitException` nếu > 6
- Chọn ghế → `SeatUnavailableException` nếu đã đặt
- Thanh toán → `PaymentFailedException` nếu thiếu tiền
- Thành công → in vé, lưu booking

#### 3. Xem lịch sử booking

#### 4. Hủy vé (nếu < 30 phút trước suất)

### Flow xử lý lỗi:

```csharp
try
{
    int movieIdx = SelectMovie();         // Có thể throw ArgumentOutOfRange
    int showIdx = SelectShowtime(movieIdx); // Có thể throw ShowtimeSoldOut
    int qty = SelectQuantity();            // Có thể throw BookingLimit
    string[] seats = SelectSeats(qty);     // Có thể throw SeatUnavailable
    decimal total = CalculateTotal(movieIdx, qty);
    ProcessPayment(total);                 // Có thể throw PaymentFailed
    CreateBooking(movieIdx, showIdx, seats, total);
}
catch (ShowtimeSoldOutException ex)
{
    PrintError(ex.Message);
    Console.WriteLine("💡 Gợi ý suất khác có vé...");
}
catch (SeatUnavailableException ex)
{
    PrintError($"Ghế {ex.Seat} đã có người đặt!");
}
catch (PaymentFailedException ex)
{
    PrintError($"Thanh toán thất bại: {ex.Reason}");
}
catch (BookingLimitException ex)
{
    PrintError(ex.Message);
}
catch (Exception ex)
{
    PrintError($"Lỗi không xác định: {ex.Message}");
}
finally
{
    Console.WriteLine("─── Kết thúc giao dịch ───");
}
```

---

### Yêu cầu kỹ thuật:

- ✅ 4 custom exceptions (có properties riêng)
- ✅ Guard clause + throw trong mỗi method
- ✅ Catch cụ thể (KHÔNG chỉ `catch (Exception)`)
- ✅ `finally` cho cleanup/thông báo
- ✅ `TryParse` cho tất cả user input
- ✅ Tách methods (≥12 methods)
- ✅ Chương trình KHÔNG BAO GIỜ crash

---

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| 4 custom exceptions | ⭐⭐⭐ Bắt buộc |
| Catch cụ thể (≥ 3 loại) | ⭐⭐⭐ Bắt buộc |
| Guard clause + throw | ⭐⭐⭐ Bắt buộc |
| TryParse cho input | ⭐⭐⭐ Bắt buộc |
| Chương trình không crash | ⭐⭐⭐ BẮT BUỘC |
| finally | ⭐⭐ Quan trọng |
| Xem phim + đặt vé | ⭐⭐ Quan trọng |
| Lịch sử booking | ⭐ Bonus |
| Hủy vé | ⭐ Bonus |
| Gợi ý suất khác khi hết vé | ⭐ Bonus |

---

### 🎓 Kết thúc Phase 1!

> 🏆 **Chúc mừng!** Sau challenge này, bạn đã hoàn thành Phase 1 — C# Fundamentals.
> Bạn đã biết: Variables, Types, Operators, I/O, If/Switch, Loop, Methods, Scope, Static, Memory, Arrays, String, Exception Handling.
>
> **Phase 2 — OOP Foundation** sẽ mở ra thế giới mới: Class, Object, Constructor, Inheritance, Polymorphism, Interface...
