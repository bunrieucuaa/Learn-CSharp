# Bài tập Thực hành Bài 38: Clean Code Basics

Hãy phân tích lỗi thiết kế và tiến hành refactor các đoạn code bẩn dưới đây trên môi trường dự án test của bạn.

---

## Bài tập 1: Sửa lỗi đặt tên và viết tắt
**Đề bài:**
Hãy viết lại đoạn code sau cho chuẩn hóa quy tắc đặt tên của C# (PascalCase, camelCase) và đặt tên biến có ý nghĩa:
```csharp
public class manager_user
{
    public string usr_name;
    public string pass_word;
    
    public void do_login(string u, string p)
    {
        if (usr_name == u && pass_word == p)
        {
            Console.WriteLine("ok");
        }
    }
}
```

---

## Bài tập 2: Đập phẳng Nested If dùng LINQ
**Đề bài:**
Cho đoạn code lọc danh sách số nguyên:
```csharp
List<int> data = [ 1, -5, 10, -20, 15, 30, 45, 0 ];
List<int> validData = new List<int>();

foreach (var x in data)
{
    if (x > 0)
    {
        if (x % 5 == 0)
        {
            if (x != 30)
            {
                validData.Add(x);
            }
        }
    }
}
```
Hãy viết lại đoạn code trên bằng cách sử dụng duy nhất một dòng lệnh LINQ Method Syntax.

---

## Bài tập 3: Tách hàm đơn nhiệm (SRP)
**Đề bài:**
Phân tích xem hàm dưới đây đang vi phạm lỗi thiết kế gì và tiến hành chia nhỏ nó thành các phương thức đơn nhiệm sạch sẽ:
```csharp
public void HandleStudentData(Student student)
{
    // 1. In thông tin
    Console.WriteLine($"Tên học sinh: {student.Name}");
    
    // 2. Tính điểm trung bình môn
    double gpa = (student.Math + student.English + student.Science) / 3;
    
    // 3. Ghi log lưu tệp tin
    System.IO.File.WriteAllText("log.txt", $"Học sinh {student.Name} đạt GPA {gpa}");
}
```

---

## Bài tập 4: Loại bỏ Boolean thừa thãi
**Đề bài:**
Hãy rút gọn đoạn code rườm rà dưới đây sao cho ngắn gọn và tự nhiên nhất:
```csharp
public bool CheckAdult(User user)
{
    if (user.Age >= 18 == true)
    {
        return true;
    }
    else
    {
        return false;
    }
}
```
* **Gợi ý:** Chỉ cần trả về trực tiếp biểu thức so sánh logic.

---

## Bài tập 5: Áp dụng YAGNI và comment thừa
**Đề bài:**
Hãy dọn dẹp các dòng comment thừa và phần code "dự phòng tương lai" không cần thiết trong đoạn code sau:
```csharp
public class Calculator
{
    // Hàm này dùng để cộng hai số nguyên a và b lại với nhau
    public int Add(int a, int b)
    {
        // Trả về kết quả cộng
        return a + b;
    }

    // TODO: Trong tương lai có thể khách hàng sẽ muốn cộng 3 số, 4 số, hoặc cộng chuỗi
    // Nên tôi viết sẵn hàm này dự phòng dù hiện tại dự án không dùng đến:
    public string AddStringFuture(string a, string b, string c)
    {
        return a + b + c;
    }
}
```
