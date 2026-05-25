# Bài tập Thực hành Bài 35: Sorting

Áp dụng các hàm `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending` để hoàn thành các bài tập dưới đây.

---

## Bài tập 1: Sắp xếp số nguyên giảm dần
**Đề bài:**
Cho danh sách số nguyên: `List<int> numbers = [ 15, 3, 9, 1, 12, 6, 8 ];`
Hãy viết truy vấn LINQ để sắp xếp danh sách trên theo thứ tự giảm dần (từ lớn đến nhỏ).

* **Expected Output:**
  ```text
  15, 12, 9, 8, 6, 3, 1
  ```

---

## Bài tập 2: Sắp xếp chuỗi theo độ dài
**Đề bài:**
Cho danh sách chuỗi: `List<string> words = [ "C#", "JavaScript", "Go", "Python", "Rust", "Java" ];`
Hãy viết LINQ sắp xếp các chuỗi trên theo độ dài của chuỗi từ ngắn nhất đến dài nhất. Nếu độ dài bằng nhau, sắp xếp theo bảng chữ cái A-Z.

* **Gợi ý:** Dùng `.OrderBy(w => w.Length).ThenBy(w => w)`.
* **Expected Output:**
  ```text
  C#, Go, Java, Rust, Python, JavaScript
  ```

---

## Bài tập 3: Sắp xếp sản phẩm theo giá và tên
**Đề bài:**
Cho class `Product`:
```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```
Và danh sách:
```csharp
List<Product> products = [
    new Product { Name = "Bàn phím", Price = 150 },
    new Product { Name = "Chuột không dây", Price = 50 },
    new Product { Name = "Chuột gaming", Price = 50 },
    new Product { Name = "Tai nghe", Price = 120 }
];
```
Hãy viết LINQ sắp xếp danh sách sản phẩm theo Giá tăng dần, nếu giá bằng nhau thì sắp xếp theo Tên giảm dần (Z-A).

* **Expected Output:**
  ```text
  - Chuột không dây ($50)
  - Chuột gaming ($50)
  - Tai nghe ($120)
  - Bàn phím ($150)
  ```
  *(Lưu ý: "Chuột không dây" có chữ "k" đứng sau chữ "g" của "Chuột gaming", nên khi Z-A thì "không dây" đứng trước).*

---

## Bài tập 4: Sắp xếp danh sách ngày tháng
**Đề bài:**
Cho danh sách ngày tháng:
```csharp
List<DateTime> dates = [
    new DateTime(2026, 5, 20),
    new DateTime(2025, 12, 25),
    new DateTime(2026, 1, 1),
    new DateTime(2026, 5, 10)
];
```
Hãy viết LINQ sắp xếp danh sách ngày tháng trên từ mới nhất đến cũ nhất.

* **Gợi ý:** Dùng `.OrderByDescending()`.
* **Expected Output:**
  ```text
  20/05/2026, 10/05/2026, 01/01/2026, 25/12/2025
  ```

---

## Bài tập 5: Đảo ngược kết quả lọc
**Đề bài:**
Cho danh sách: `List<int> numbers = [ 1, 4, 3, 8, 5, 10, 7 ];`
Hãy viết câu LINQ thực hiện:
1. Lọc ra các số lẻ.
2. Sắp xếp các số lẻ này tăng dần.
3. Đảo ngược vị trí của kết quả sắp xếp đó.

* **Expected Output:**
  ```text
  7, 5, 3, 1
  ```
