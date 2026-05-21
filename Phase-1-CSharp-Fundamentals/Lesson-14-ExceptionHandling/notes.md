# 📝 Lesson 14 — Notes: Exception Handling

---

## 🧠 Tóm tắt

### Cú pháp:

```csharp
// try-catch-finally
try
{
    // code có thể lỗi
}
catch (FormatException ex)      // Cụ thể trước
{
    Console.WriteLine(ex.Message);
}
catch (Exception ex)             // Chung sau (hoặc bỏ)
{
    Console.WriteLine(ex.Message);
}
finally
{
    // LUÔN chạy (cleanup)
}

// throw
throw new ArgumentException("Lỗi!", nameof(param));

// throw lại
catch (Exception ex) { throw; }  // Giữ stack trace

// TryParse
if (int.TryParse(input, out int val)) { ... }
else { ... }

// Custom Exception
class MyException : Exception
{
    public MyException(string msg) : base(msg) { }
}
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Nuốt exception `catch { }` | Ít nhất log message |
| 2 | Catch `Exception` quá rộng | Catch cụ thể trước |
| 3 | `throw ex` mất stack trace | Dùng `throw` (không `ex`) |
| 4 | Dùng exception cho validation | Dùng `TryParse` + if |
| 5 | Quên `finally` cho cleanup | File, DB → luôn dùng finally |
| 6 | Không catch → app crash | Wrap entry point bằng try-catch |

---

## 📊 Khi nào dùng gì?

| Tình huống | Dùng |
|-----------|------|
| Input user (nhập sai) | `TryParse` + validation |
| File không tồn tại | `try-catch` |
| Network lỗi | `try-catch` |
| Method nhận tham số sai | `throw` (guard clause) |
| Logic lỗi nghiêm trọng | Custom exception |
| Cleanup (đóng file/DB) | `finally` |

---

## ✅ Checklist

- [ ] Biết `try-catch-finally` cú pháp
- [ ] Biết catch cụ thể trước, `Exception` cuối
- [ ] Biết `TryParse` thay `Parse` cho input
- [ ] Biết `throw new XxxException("msg")`
- [ ] Biết `throw` vs `throw ex` (stack trace)
- [ ] Biết guard clause + throw
- [ ] Biết tạo custom exception
- [ ] Biết exception properties (Message, StackTrace, InnerException)
- [ ] Biết khi nào exception vs validation
- [ ] Biết `finally` cho cleanup

---

## 🔗 Liên kết

```
Lesson 13: String
    ↓
Lesson 14: Exception Handling ← BẠN Ở ĐÂY
    ↓
════════════════════════════
  🏆 HOÀN THÀNH PHASE 1!
════════════════════════════
    ↓
Phase 2: OOP Foundation
  → Class/Object
  → Constructor
  → Encapsulation
  → Inheritance
  → Polymorphism
  → Interface
```

> 🎓 **Phase 1 Complete!** Bạn đã nắm đủ nền tảng C# để bắt đầu OOP. Mọi thứ từ Phase 1 sẽ được dùng lại trong Phase 2+.
