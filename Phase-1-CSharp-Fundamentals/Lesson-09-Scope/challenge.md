# 🏆 Lesson 09 — Challenge: Scope & Lifetime

---

## Challenge: Refactor Game Store 🎮

### Bối cảnh:
> "Code game store dưới đây hoạt động nhưng scope RẤT TỆ. Refactor lại cho clean, áp dụng đúng nguyên tắc scope."

### Code gốc (BAD — refactor lại):

```csharp
static string pn = ""; static decimal pp = 0; static int pq = 0;
static string cn = ""; static decimal cb = 0;
static string[] hn = new string[100]; static decimal[] hp = new decimal[100];
static int[] hq = new int[100]; static int hc = 0;
static decimal tt = 0; static decimal dc = 0; static decimal fn = 0;
static bool mb = false; static string? cp = null;
```

### Yêu cầu refactor:

1. **Đổi tên biến** cho có ý nghĩa
2. **Chuyển scope đúng**: class-level chỉ cho data + const
3. **Tách thành 10+ methods** với tên rõ ràng
4. **Main() ≤ 15 dòng**
5. **Mỗi method ≤ 20 dòng**

### Chức năng Game Store:
- Xem danh sách game (5 game có sẵn)
- Thêm game vào giỏ
- Xem giỏ hàng
- Tính tiền (thành viên -10%, coupon "GAME20" -20%)
- Thanh toán

### 🎯 Tiêu chí:

| Tiêu chí | Mức độ |
|----------|--------|
| Tên biến có ý nghĩa | ⭐⭐⭐ Bắt buộc |
| Scope đúng (minimal) | ⭐⭐⭐ Bắt buộc |
| 10+ methods | ⭐⭐⭐ Bắt buộc |
| Const cho hằng số | ⭐⭐ Quan trọng |
| Không dùng class-level cho biến tạm | ⭐⭐ Quan trọng |
