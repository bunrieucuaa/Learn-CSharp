# Ví dụ Thực hành Bài 38: Refactoring Code bẩn sang Code sạch dùng LINQ

Dưới đây là một ví dụ thực tế về việc tiến hành refactor một đoạn code legacy "bẩn" (code smell) thành một phiên bản code sạch sẽ, dễ bảo trì bằng cách kết hợp đặt tên chuẩn, chia nhỏ phương thức và dùng LINQ.

---

## 1. Đoạn code "Bẩn" ban đầu (Dirty Code)

Hãy phân tích hàm lọc danh sách khách hàng để gửi mã khuyến mãi sinh nhật.

```csharp
using System;
using System.Collections.Generic;

public class Cust
{
    public string N { get; set; }     // Tên khách hàng (Nghèo nàn)
    public int A { get; set; }     // Tuổi (Nghèo nàn)
    public string E { get; set; }     // Email
    public bool Act { get; set; }   // Trạng thái hoạt động
    public string City { get; set; } // Thành phố
    public DateTime Bd { get; set; } // Ngày sinh
}

public class Processor
{
    // Hàm xử lý lấy danh sách email khách hàng thỏa mãn gửi khuyến mãi
    public List<string> Process(List<Cust> lst)
    {
        List<string> res = new List<string>();
        foreach (var c in lst)
        {
            // Kiểm tra khách hàng hoạt động và hôm nay là sinh nhật
            if (c.Act == true)
            {
                if (c.Bd.Month == DateTime.Today.Month && c.Bd.Day == DateTime.Today.Day)
                {
                    // Lọc khách hàng sống ở Hà Nội hoặc HCM
                    if (c.City == "HN" || c.City == "HCM")
                    {
                        // Khách hàng phải từ 18 tuổi trở lên mới được tham gia chương trình rượu/bia
                        if (c.A >= 18)
                        {
                            res.Add(c.E);
                        }
                    }
                }
            }
        }
        return res;
    }
}
```

### Các điểm bẩn (Code Smells) phát hiện:
1. **Đặt tên biến nghèo nàn:** Tên thuộc tính viết tắt vô nghĩa (`N`, `A`, `E`, `Act`, `Bd`), tên biến danh sách viết tắt (`lst`, `res`).
2. **Cấu trúc mũi tên thụt lề (Nested If):** 4 tầng `if` lồng nhau trong vòng lặp `foreach` cực kỳ khó đọc.
3. **Magic Strings:** Các giá trị `"HN"`, `"HCM"` bị viết cứng (hardcoded) trong logic.
4. **Viết tắt so sánh Boolean:** So sánh thừa thãi `c.Act == true` thay vì chỉ dùng `c.Act`.

---

## 2. Phiên bản Code Sạch sau khi Refactor (Clean Code)

Chúng ta tiến hành:
1. Đổi tên Class, thuộc tính và biến theo chuẩn PascalCase/camelCase.
2. Dùng LINQ Method Syntax để đập phẳng toàn bộ cấu trúc vòng lặp và câu lệnh rẽ nhánh lồng nhau.
3. Tách các điều kiện kiểm tra phức tạp (như kiểm tra sinh nhật) thành một hàm phụ bổ trợ (Helper method) tự giải nghĩa.
4. Gom các thành phố mục tiêu thành một tập hợp riêng biệt.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Customer
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
}

public class BirthdayPromotionService
{
    // Gom nhóm hằng số thành phố mục tiêu để dễ quản lý
    private static readonly HashSet<string> TargetCities = ["HN", "HCM"];
    private const int MinimumRequiredAge = 18;

    public List<string> GetTargetEmailsForPromotion(List<Customer> customers)
    {
        if (customers == null) return [];

        return customers
            .Where(customer => IsEligibleForPromotion(customer))
            .Select(customer => customer.Email)
            .ToList();
    }

    // Hàm phụ trách kiểm tra điều kiện (SRP)
    // Tên hàm thể hiện rõ mục đích, thay thế hoàn toàn cho các comment giải thích
    private bool IsEligibleForPromotion(Customer customer)
    {
        return customer.IsActive 
               && IsBirthdayToday(customer.BirthDate)
               && TargetCities.Contains(customer.City)
               && customer.Age >= MinimumRequiredAge;
    }

    private bool IsBirthdayToday(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        return birthDate.Month == today.Month && birthDate.Day == today.Day;
    }
}
```

### So sánh kết quả:
* **Tính đọc hiểu:** Hàm `GetTargetEmailsForPromotion` mới đọc lên giống như một câu tiếng Anh: *"Lọc danh sách khách hàng thỏa mãn điều kiện khuyến mãi, sau đó lấy ra email của họ và chuyển thành danh sách"*.
* **Tính bảo trì:** Nếu sau này tuổi tối thiểu thay đổi thành 21, hoặc có thêm thành phố "DaNang" được nhận khuyến mãi, bạn chỉ cần sửa đổi hằng số ở đầu trang thay vì chui sâu vào giữa hàng chục dòng lồng nhau để sửa.
