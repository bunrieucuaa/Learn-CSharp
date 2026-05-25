# Ví dụ Thực hành Bài 35: Sorting

Dưới đây là các ví dụ C# chạy được bằng .NET 9 chứng minh cách thức hoạt động của các toán tử sắp xếp dữ liệu.

---

## Ví dụ 1: Sắp xếp cơ bản tăng dần và giảm dần

Sắp xếp danh sách tên học viên và điểm số cơ bản.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<string> names = ["Cường", "An", "Dũng", "Bình"];

// Sắp xếp tăng dần theo bảng chữ cái
var sortedNames = names.OrderBy(name => name);
Console.WriteLine("Tăng dần A-Z: " + string.Join(", ", sortedNames));
// Output: An, Bình, Cường, Dũng

// Sắp xếp giảm dần theo bảng chữ cái
var sortedNamesDesc = names.OrderByDescending(name => name);
Console.WriteLine("Giảm dần Z-A: " + string.Join(", ", sortedNamesDesc));
// Output: Dũng, Cường, Bình, An
```

---

## Ví dụ 2: Sắp xếp nhiều tiêu chí (Multi-criteria Sorting)

Sắp xếp danh sách nhân viên theo Phòng ban tăng dần, nếu trùng phòng ban thì sắp xếp theo Lương giảm dần.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

List<Employee> employees = [
    new Employee { Name = "An", Department = "IT", Salary = 1500 },
    new Employee { Name = "Bình", Department = "HR", Salary = 1000 },
    new Employee { Name = "Cường", Department = "IT", Salary = 2000 },
    new Employee { Name = "Dũng", Department = "HR", Salary = 1200 },
    new Employee { Name = "Giang", Department = "IT", Salary = 1500 }
];

// Tiến hành sắp xếp đa tiêu chí
var sortedEmployees = employees
                      .OrderBy(e => e.Department)          // Tiêu chí 1: Phòng ban A-Z
                      .ThenByDescending(e => e.Salary)    // Tiêu chí 2: Lương giảm dần
                      .ThenBy(e => e.Name);               // Tiêu chí 3: Tên A-Z (nếu lương bằng nhau)

Console.WriteLine("Danh sách nhân viên sau sắp xếp:");
foreach (var e in sortedEmployees)
{
    Console.WriteLine($"- Phòng: {e.Department} | Lương: ${e.Salary} | Tên: {e.Name}");
}
// Output kỳ vọng:
// - Phòng: HR | Lương: $1200 | Tên: Dũng
// - Phòng: HR | Lương: $1000 | Tên: Bình
// - Phòng: IT | Lương: $2000 | Tên: Cường
// - Phòng: IT | Lương: $1500 | Tên: An       (cùng lương, An đứng trước Giang)
// - Phòng: IT | Lương: $1500 | Tên: Giang
```

---

## Ví dụ 3: Sắp xếp tùy biến bằng Custom Comparer

Sắp xếp công việc (Task) theo độ ưu tiên: "High" > "Medium" > "Low".

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Job
{
    public string Title { get; set; }
    public string Priority { get; set; } // "High", "Medium", "Low"
}

// Khai báo bộ so sánh tùy biến
public class PriorityComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        // Gán trọng số điểm cho mỗi độ ưu tiên
        int GetWeight(string? priority) => priority switch
        {
            "High" => 3,
            "Medium" => 2,
            "Low" => 1,
            _ => 0
        };

        // So sánh giảm dần (trọng số cao hơn xếp trước)
        return GetWeight(y).CompareTo(GetWeight(x));
    }
}

List<Job> jobs = [
    new Job { Title = "Fix bug critical", Priority = "High" },
    new Job { Title = "Write documentation", Priority = "Low" },
    new Job { Title = "Refactor code helper", Priority = "Medium" },
    new Job { Title = "Update landing page", Priority = "High" }
];

// Sắp xếp dùng PriorityComparer
var sortedJobs = jobs.OrderBy(j => j.Priority, new PriorityComparer());

Console.WriteLine("Danh sách công việc ưu tiên:");
foreach (var j in sortedJobs)
{
    Console.WriteLine($"- [{j.Priority}] {j.Title}");
}
// Output:
// - [High] Fix bug critical
// - [High] Update landing page
// - [Medium] Refactor code helper
// - [Low] Write documentation
```

---

## Ví dụ 4: Chứng minh LINQ không làm thay đổi mảng gốc (Non-mutating)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<int> originalList = [5, 3, 8, 1];

Console.WriteLine("Mảng gốc trước khi sắp xếp: " + string.Join(", ", originalList));

// Gọi OrderBy
var sortedList = originalList.OrderBy(x => x).ToList();

Console.WriteLine("Mảng mới sau khi OrderBy:  " + string.Join(", ", sortedList));
Console.WriteLine("Mảng gốc sau khi sắp xếp:  " + string.Join(", ", originalList));

// Kết quả Console:
// Mảng gốc trước khi sắp xếp: 5, 3, 8, 1
// Mảng mới sau khi OrderBy:  1, 3, 5, 8
// Mảng gốc sau khi sắp xếp:  5, 3, 8, 1 (Vẫn giữ nguyên vị trí ban đầu!)
```
