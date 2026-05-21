# 📝 Lesson 15 — Notes: Class & Object

---

## 🧠 Tóm tắt

```csharp
// Định nghĩa class
class Student
{
    public string Name = "";     // Field
    public double Score = 0;

    public string GetGrade() => Score >= 5 ? "Đậu" : "Rớt";  // Method
}

// Tạo object
Student s = new Student();  // new → tạo trên Heap
s.Name = "Minh";
s.GetGrade();               // Gọi method trên object
```

### Quy tắc:
- Class = reference type → `new` tạo trên Heap
- Copy object = chia sẻ reference (giống array)
- Field = data (danh từ), Method = behavior (động từ)
- Mảng object `Student[]` thay mảng song song

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Quên `new` → NullRef | Luôn `new` trước khi dùng |
| 2 | Copy object = share | Cần copy riêng thì tạo `new` + gán fields |
| 3 | Mảng object quên new từng item | `arr[i] = new Student()` |
| 4 | Field public quá nhiều | Sẽ học `private` + Property (Lesson 18) |

---

## ✅ Checklist

- [ ] Biết class = bản thiết kế, object = instance
- [ ] Biết field (data) vs method (behavior)
- [ ] Biết `new` tạo object trên Heap
- [ ] Biết copy object = chia sẻ reference
- [ ] Biết mảng object `Student[]`
- [ ] Biết PascalCase cho class/method, camelCase cho biến

---

## 🔗 Liên kết

```
Phase 1 ✅ (Lesson 01-14)
    ↓
Lesson 15: Class & Object ← BẠN Ở ĐÂY
    ↓
Lesson 16: Constructor
    ↓
Lesson 17: this
    ↓
Lesson 18: Access Modifier
    ↓
Lesson 19+: 4 tính chất OOP
```
