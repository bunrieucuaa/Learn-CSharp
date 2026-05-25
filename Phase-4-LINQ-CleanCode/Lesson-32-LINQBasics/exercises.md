# Bài tập Thực hành Bài 32: LINQ Basics

Dưới đây là 5 bài tập giúp bạn làm quen với cú pháp LINQ và cơ chế hoạt động của nó. 
**Chú ý:** Không được viết lời giải trực tiếp vào các file này. Hãy tự code trên IDE của bạn để kiểm tra kết quả.

---

## Bài tập 1: Lọc số dương
**Đề bài:** 
Cho một danh sách số nguyên: `List<int> numbers = [ -10, 5, -2, 0, 15, -1, 8, -6, 20 ];`
Hãy viết truy vấn LINQ sử dụng **Method Syntax** để lọc ra các số nguyên lớn hơn hoặc bằng 0.

* **Gợi ý:** Sử dụng phương thức `.Where()`.
* **Expected Output:**
  ```text
  5, 0, 15, 8, 20
  ```

---

## Bài tập 2: Lọc chuỗi theo độ dài
**Đề bài:**
Cho một danh sách các từ: `List<string> words = [ "C#", "JavaScript", "Python", "C++", "Go", "Kotlin", "Java" ];`
Hãy dùng **Method Syntax** của LINQ để lọc ra các ngôn ngữ lập trình có độ dài từ 4 ký tự trở lên.

* **Gợi ý:** Sử dụng thuộc tính `.Length` của kiểu `string` trong biểu thức lambda.
* **Expected Output:**
  ```text
  JavaScript, Python, Kotlin, Java
  ```

---

## Bài tập 3: Chuyển đổi cú pháp (Query to Method Syntax)
**Đề bài:**
Cho câu lệnh LINQ sử dụng **Query Syntax** sau đây:
```csharp
List<int> score = [ 45, 78, 92, 50, 64, 88, 30 ];

var highScores = from s in score
                 where s >= 80
                 select s;
```
Hãy viết lại câu lệnh trên bằng **Method Syntax** (dùng toán tử dấu chấm `.Where`).

* **Expected Output khi in ra console:**
  ```text
  92, 88
  ```

---

## Bài tập 4: Phân tích Deferred Execution
**Đề bài:**
Hãy chạy đoạn code sau đây trong IDE của bạn và giải thích tại sao output lại ra như thế:
```csharp
List<string> jobs = [ "Lập trình viên", "Thiết kế đồ hoạ" ];

var result = jobs.Where(j => {
    Console.WriteLine("-> Quét qua công việc: " + j);
    return j.Contains("Lập trình");
});

Console.WriteLine("Bắt đầu in:");
foreach(var item in result)
{
    Console.WriteLine("Tìm thấy: " + item);
}
```
* **Câu hỏi:** Dòng chữ `-> Quét qua công việc...` hiển thị trước hay sau dòng chữ `Bắt đầu in:`? Tại sao?

---

## Bài tập 5: Lọc đối tượng nâng cao
**Đề bài:**
Cho class `User` như sau:
```csharp
public class User
{
    public string Username { get; set; }
    public string Role { get; set; } // "Admin" hoặc "Member"
    public bool IsBanned { get; set; }
}
```
Và danh sách:
```csharp
List<User> users = [
    new User { Username = "alice", Role = "Admin", IsBanned = false },
    new User { Username = "bob", Role = "Member", IsBanned = true },
    new User { Username = "charlie", Role = "Member", IsBanned = false },
    new User { Username = "david", Role = "Admin", IsBanned = true }
];
```
Hãy viết LINQ lọc ra danh sách các `Username` của những tài khoản **Admin** và **không bị ban** (`IsBanned == false`).

* **Gợi ý:** Kết hợp `.Where()` để lọc và `.Select()` để lấy trường `Username`. (Chỉ in ra tên chứ không in toàn bộ Object).
* **Expected Output:**
  ```text
  alice
  ```
