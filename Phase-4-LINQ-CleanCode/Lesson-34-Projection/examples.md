# Ví dụ Thực hành Bài 34: Projection

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức hoạt động của phép chiếu dữ liệu.

---

## Ví dụ 1: Map đối tượng bằng `Select` (Entity -> DTO)

Ví dụ thực tế mô phỏng việc ẩn mật khẩu và thông tin nhạy cảm của User trước khi gửi dữ liệu ra ngoài.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Lớp dữ liệu lưu trong Database (chứa PasswordHash nhạy cảm)
public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // Nhạy cảm, không được lộ ra ngoài
}

// Lớp dữ liệu gửi về Client (an toàn)
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

List<UserEntity> dbUsers = [
    new UserEntity { Id = 1, Username = "admin", Email = "admin@site.com", PasswordHash = "sha256_hash_1" },
    new UserEntity { Id = 2, Username = "member1", Email = "m1@site.com", PasswordHash = "sha256_hash_2" }
];

// Chiếu (Project) danh sách UserEntity sang UserDto
List<UserDto> safeUsers = dbUsers
                          .Select(u => new UserDto 
                          {
                              Id = u.Id,
                              Username = u.Username,
                              Email = u.Email
                          })
                          .ToList();

Console.WriteLine("Danh sách người dùng an toàn gửi về client:");
foreach (var u in safeUsers)
{
    Console.WriteLine($"- ID {u.Id}: {u.Username} ({u.Email})");
}
```

---

## Ví dụ 2: Làm phẳng cấu trúc lồng nhau với `SelectMany`

Ví dụ lấy toàn bộ danh sách kỹ năng của tất cả các Lập trình viên trong công ty mà không bị trùng lặp.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Developer
{
    public string Name { get; set; }
    public List<string> Skills { get; set; } // Danh sách kỹ năng của mỗi người
}

List<Developer> team = [
    new Developer { Name = "Bảo", Skills = ["C#", "SQL", "HTML"] },
    new Developer { Name = "Vy", Skills = ["JavaScript", "React", "HTML"] },
    new Developer { Name = "Hải", Skills = ["C#", "Docker", "AWS"] }
];

// Dùng SelectMany để gộp toàn bộ Skills lại thành 1 mảng phẳng duy nhất
// Kết hợp Distinct() để loại bỏ các kỹ năng trùng lặp (ví dụ "C#", "HTML")
var allSkills = team
                .SelectMany(dev => dev.Skills)
                .Distinct();

Console.WriteLine("Toàn bộ kỹ năng của team (đã lọc trùng):");
Console.WriteLine(string.Join(", ", allSkills));
// Output: C#, SQL, HTML, JavaScript, React, Docker, AWS
```

---

## Ví dụ 3: Ghép đôi bộ sưu tập với `Zip`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<string> codes = ["PROMO10", "PROMO20", "PROMO50"];
List<int> discounts = [10, 20, 50];

// Ghép mã giảm giá với số phần trăm tương ứng
var couponBook = codes.Zip(discounts, (code, discount) => new 
{
    CouponCode = code,
    Percent = discount
});

Console.WriteLine("Danh sách mã giảm giá:");
foreach (var coupon in couponBook)
{
    Console.WriteLine($"- Mã: {coupon.CouponCode} giảm {coupon.Percent}%");
}
```

---

## Ví dụ 4: Khởi tạo và sử dụng Anonymous Types

Sử dụng kiểu dữ liệu vô danh khi cần tính toán dữ liệu trung gian và in ra ngay trong phương thức.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double MathScore { get; set; }
    public double PhysicsScore { get; set; }
}

List<Student> students = [
    new Student { FirstName = "Nguyễn Văn", LastName = "An", MathScore = 8.5, PhysicsScore = 9.0 },
    new Student { FirstName = "Trần Thị", LastName = "Bình", MathScore = 6.0, PhysicsScore = 7.5 }
];

// Tạo Anonymous Type chứa họ tên đầy đủ và điểm trung bình
var report = students.Select(s => new
{
    FullName = $"{s.FirstName} {s.LastName}",
    AverageScore = (s.MathScore + s.PhysicsScore) / 2
});

Console.WriteLine("Báo cáo điểm trung bình:");
foreach (var item in report)
{
    // compiler tự nhận diện thuộc tính FullName và AverageScore từ kiểu vô danh
    Console.WriteLine($"- Học sinh: {item.FullName} | ĐTB: {item.AverageScore:F1}");
}
```
