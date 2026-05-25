# 📝 Bài 29: IEnumerable<T> & Iteration — Tóm tắt & Checklist

---

## 📋 Tóm tắt nhanh

### 1. IEnumerable\<T\> = Giao diện cho foreach

```csharp
// Bất kỳ class nào implement IEnumerable<T> đều dùng được foreach
public interface IEnumerable<T>
{
    IEnumerator<T> GetEnumerator();   // Trả về "con trỏ duyệt"
}
```

### 2. foreach = IEnumerator dưới nền

```csharp
// Bạn viết:                    // Compiler tạo ra:
foreach (var x in collection)   var e = collection.GetEnumerator();
{                               try {
    Process(x);                   while (e.MoveNext())
}                                   Process(e.Current);
                                } finally { e.Dispose(); }
```

### 3. yield return — Tạo iterator đơn giản

```csharp
IEnumerable<int> EvenNumbers(int max)
{
    for (int i = 0; i <= max; i++)
        if (i % 2 == 0)
            yield return i;   // Trả về + tạm dừng
}

// yield break → Dừng iterator hoàn toàn
```

### 4. Deferred Execution

```
IEnumerable<int> data = GetData();   // ← CHƯA chạy!
foreach (int x in data)              // ← BÂY GIỜ mới chạy!
```

---

## 📊 Interface Hierarchy

```
IEnumerable<T>     → Chỉ duyệt (foreach)
    │
ICollection<T>     → + Count, Add, Remove, Contains
    │
IList<T>           → + Index [i], IndexOf, Insert
    │
List<T>            → Class cụ thể
```

### Quy tắc chọn parameter type

| Cần gì? | Dùng |
|---------|------|
| Chỉ đọc/duyệt | `IEnumerable<T>` |
| Cần Count/Add/Remove | `ICollection<T>` |
| Cần truy cập theo index | `IList<T>` |
| Cần đầy đủ tính năng | `List<T>` |

---

## 🔑 Cú pháp quan trọng

```
yield return value;           → Trả value, TẠM DỪNG iterator
yield break;                  → KẾT THÚC iterator
IEnumerable<T>               → Return type cho iterator method
GetEnumerator()              → Method chính của IEnumerable<T>
MoveNext() + Current          → 2 thao tác chính của IEnumerator<T>
```

---

## 📊 Deferred vs Immediate

```
┌─────────────────┬──────────────────────┬──────────────────────┐
│                 │ Deferred (yield)     │ Immediate (List)     │
├─────────────────┼──────────────────────┼──────────────────────┤
│ Khi nào chạy    │ Khi foreach          │ Khi gọi method       │
│ Bộ nhớ          │ 1 phần tử/lần       │ Toàn bộ              │
│ Vô hạn          │ ✅ OK               │ ❌ Tràn              │
│ Duyệt lại       │ Chạy lại method     │ Dùng lại kết quả     │
└─────────────────┴──────────────────────┴──────────────────────┘
```

---

## 🔄 C# vs JavaScript

```
C#                              JavaScript
─────────────                   ──────────
IEnumerable<T>                  [Symbol.iterator]
IEnumerator<T>                  { next() → {value, done} }
foreach                         for...of
yield return                    yield (trong function*)
yield break                     return (trong function*)
```

---

## ✅ Checklist — Tự đánh giá

### IEnumerable\<T\> cơ bản
- [ ] Hiểu IEnumerable\<T\> chỉ có 1 method: GetEnumerator()
- [ ] Hiểu IEnumerator\<T\>: MoveNext(), Current
- [ ] Biết foreach được compiler chuyển thành IEnumerator

### yield return / yield break
- [ ] Viết method trả về IEnumerable\<T\> với yield return
- [ ] Dùng yield break để dừng iterator sớm
- [ ] Hiểu yield tạo "lazy" iterator — không chạy ngay

### Deferred Execution
- [ ] Hiểu deferred: method chỉ chạy khi duyệt (foreach)
- [ ] Biết cạm bẫy: duyệt IEnumerable nhiều lần = chạy nhiều lần
- [ ] Biết dùng ToList() khi cần materialized collection

### Custom Collection
- [ ] Tự implement IEnumerable\<T\> cho class riêng
- [ ] Implement cả IEnumerable (non-generic) — delegate cho generic
- [ ] Viết filter methods bằng yield return

### Interface Hierarchy
- [ ] Phân biệt IEnumerable vs ICollection vs IList
- [ ] Chọn đúng interface type cho parameter method
- [ ] Biết LINQ dựa trên IEnumerable\<T\>

### Bài tập
- [ ] Hoàn thành 5/5 bài tập
- [ ] Hoàn thành Challenge: Data Pipeline
- [ ] Thử chain nhiều bước pipeline + deferred execution

---

## 🔗 Liên kết bài học

```
Bài 28: Generic<T>           → Generic constraint, method
Bài 29: IEnumerable<T>       → Duyệt collection, yield, deferred
Bài 30: CRUD Project          → Tổng hợp Phase 3

  Bài 28 (Generic)  →  Bài 29 (IEnumerable)  →  Bài 30 (Project)
  Type-safe            Iterator pattern          Áp dụng thực tế
  Reusable code        Lazy evaluation           CRUD hoàn chỉnh
```

---

## 💡 Mẹo ghi nhớ

```
🎣 IEnumerable = Cần câu cá
   - Không bắt TẤT CẢ cá lên bờ 1 lúc (List = lưới)
   - Câu TỪNG CON một khi cần (yield return)
   - Dừng câu bất cứ lúc nào (yield break / break)
   - Muốn câu lại? Thả câu lại từ đầu (duyệt lại IEnumerable)

🏭 yield return = Dây chuyền sản xuất
   - Mỗi lần nhấn nút → ra 1 sản phẩm (MoveNext)
   - Máy TẠM DỪNG giữa chừng, không chạy liên tục
   - Tiết kiệm điện (bộ nhớ) hơn sản xuất tất cả rồi để kho

🔗 Pipeline = Ống nước nối tiếp
   - Nước (data) chảy qua từng ống (filter)
   - Mỗi ống xử lý 1 việc (Where, Select, Take...)
   - Nước chỉ chảy khi mở vòi (foreach) — không mở = không chảy
```

---

> **Bài tiếp:** Bài 30 — CRUD Console Project (Tổng hợp Phase 3) 🚀

---

*"IEnumerable<T> — simple interface, infinite possibilities."* 🎯
