# 📝 Lesson 10 — Notes: String

---

## 🧠 Tóm tắt

### String methods quan trọng nhất:

```csharp
// Tìm kiếm
.Contains("x")    .StartsWith("x")    .EndsWith("x")
.IndexOf("x")     .LastIndexOf("x")

// Biến đổi
.ToUpper()   .ToLower()   .Trim()   .TrimStart()   .TrimEnd()
.Replace("old", "new")    .Remove(start, count)
.Insert(index, "text")    .PadLeft(n, '0')

// Cắt & Nối
.Substring(start, length)    text[start..end]    text[^n..]
.Split(',')                  string.Join("-", arr)

// Kiểm tra
string.IsNullOrEmpty(s)      string.IsNullOrWhiteSpace(s)
.Equals(s, StringComparison.OrdinalIgnoreCase)

// Char
char.IsLetter(c)   .IsDigit(c)   .IsUpper(c)   .IsWhiteSpace(c)
```

### StringBuilder:

```csharp
var sb = new StringBuilder();
sb.Append("text");    sb.AppendLine("text");
sb.Insert(i, "t");    sb.Replace("a", "b");
sb.Remove(i, n);      sb.Clear();
string result = sb.ToString();
```

---

## ⚠️ Lỗi dễ quên

| # | Lỗi | Cách tránh |
|---|------|-----------|
| 1 | Quên string immutable | `name = name.ToUpper()` — gán lại! |
| 2 | Nối string trong loop | Dùng `StringBuilder` |
| 3 | So sánh case-sensitive | `StringComparison.OrdinalIgnoreCase` |
| 4 | NullReferenceException | `?.` và `??` cho nullable |
| 5 | `str[0]` trả `char` không phải `string` | `str[0].ToString()` nếu cần string |

---

## ✅ Checklist

- [ ] Biết string immutable — mọi method trả string mới
- [ ] Biết Contains, IndexOf, StartsWith, EndsWith
- [ ] Biết Trim, ToUpper/Lower, Replace
- [ ] Biết Split & Join
- [ ] Biết Range operator `[..]` thay Substring
- [ ] Biết StringBuilder khi nối nhiều chuỗi
- [ ] Biết char methods (IsLetter, IsDigit, IsUpper...)
- [ ] Biết StringComparison cho so sánh case-insensitive
- [ ] Biết string.IsNullOrWhiteSpace
- [ ] Biết verbatim `@""` và raw string `""" """`

---

## 🔗 Liên kết

```
Lesson 12: Arrays
    ↓
Lesson 13: String ← BẠN Ở ĐÂY
    ↓
Lesson 14: Exception Handling
```

> 📌 **Bài tiếp:** Lesson 14 — Exception Handling
