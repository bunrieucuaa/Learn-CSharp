# Thách thức Bài 42: Tái cấu trúc Hệ thống Đăng ký Sự kiện (Event Registration System)

## Ngữ cảnh
Một lập trình viên thử việc (intern) trước khi rời công ty đã viết một ứng dụng Console để quản lý đăng ký vé tham dự sự kiện. Vì thiếu kinh nghiệm, toàn bộ mã nguồn của dự án (từ đọc ghi file JSON, kiểm tra giới hạn tuổi, đếm số lượng vé còn lại, cho đến in giao diện màu sắc) đều được viết gom chung vào đúng một file duy nhất: `Program.cs`.

Quản lý dự án đánh giá dự án này không thể bảo trì được và yêu cầu bạn tiến hành **Refactoring (Tái cấu trúc)** lại toàn bộ dự án này theo kiến trúc 3-Layer sạch sẽ để kịp bàn giao cho đối tác.

## Đoạn code Spaghetti ban đầu cần cứu hộ
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string dbPath = "registrations.json";
        int maxCapacity = 3; // Sự kiện chỉ cho phép tối đa 3 người tham gia

        while (true)
        {
            Console.WriteLine("\n=== ĐĂNG KÝ VÉ SỰ KIỆN ===");
            Console.WriteLine("1. Xem danh sách đăng ký");
            Console.WriteLine("2. Đăng ký vé mới");
            Console.WriteLine("3. Thoát");
            Console.Write("Chọn: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                if (!File.Exists(dbPath))
                {
                    Console.WriteLine("Chưa có ai đăng ký.");
                    continue;
                }
                string json = File.ReadAllText(dbPath);
                var list = JsonSerializer.Deserialize<List<string>>(json);
                Console.WriteLine("\nDanh sách người tham dự:");
                foreach (var name in list)
                {
                    Console.WriteLine($"- {name}");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Nhập tên của bạn: ");
                string name = Console.ReadLine();
                Console.Write("Nhập tuổi của bạn: ");
                int age = int.Parse(Console.ReadLine());

                // Logic 1: Kiểm tra độ tuổi (Sự kiện chỉ dành cho người >= 16 tuổi)
                if (age < 16)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lỗi: Bạn phải từ 16 tuổi trở lên mới được tham gia sự kiện!");
                    Console.ResetColor();
                    continue;
                }

                // Đọc file để kiểm tra số lượng hiện tại
                List<string> list = new List<string>();
                if (File.Exists(dbPath))
                {
                    string json = File.ReadAllText(dbPath);
                    list = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }

                // Logic 2: Kiểm tra quá tải
                if (list.Count >= maxCapacity)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lỗi: Sự kiện đã hết vé (Đầy chỗ)!");
                    Console.ResetColor();
                    continue;
                }

                list.Add(name);
                string newJson = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dbPath, newJson);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Đăng ký vé thành công!");
                Console.ResetColor();
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }
}
```

## Yêu cầu Thách thức
Hãy tiến hành chia nhỏ dự án trên thành 3 lớp sạch sẽ bằng cách tạo ra các file và class tương ứng:

1. **Tạo lớp Model:** Định nghĩa class `Participant` chứa thông tin `Name` và `Age` thay vì lưu chuỗi tên `string` đơn giản trong mảng JSON.
2. **Tạo lớp Data Access (`ParticipantRepository`):** 
   - Đảm nhiệm việc đọc và ghi danh sách `List<Participant>` xuống file `registrations.json`.
3. **Tạo lớp Service (`RegistrationService`):**
   - Chứa logic kiểm tra tuổi học viên (`age >= 16`).
   - Chứa logic kiểm tra giới hạn sức chứa sự kiện (`Count >= 3`).
   - Ném ra các Exception phù hợp khi vi phạm điều kiện nghiệp vụ.
4. **Tạo lớp UI (`ConsoleUI`):**
   - Lo việc vẽ menu, đọc input bàn phím.
   - Gọi `RegistrationService` để đăng ký.
   - Bắt Exception ném ra từ Service để hiển thị màu chữ đỏ báo lỗi hoặc chữ xanh báo thành công.
5. **Đắp ráp ở `Program.cs`:** Sử dụng Constructor Injection để ráp nối Dal -> Service -> UI và chạy thử nghiệm.
