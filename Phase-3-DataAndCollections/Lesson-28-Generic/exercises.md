# ✏️ Bài 28: Generic\<T> — Bài Tập Thực Hành

---

## Bài 1: Generic Utility Methods ⭐

> **Mục tiêu**: Viết các generic method tiện ích.

### Yêu cầu:
1. Viết `void PrintArray<T>(T[] items)` — in mảng với format đẹp
2. Viết `T[] ReverseArray<T>(T[] items)` — trả mảng đảo ngược (không thay đổi mảng gốc)
3. Viết `T FindSecondMax<T>(T[] items) where T : IComparable<T>` — tìm giá trị lớn thứ 2
4. Viết `(T min, T max) FindMinMax<T>(T[] items) where T : IComparable<T>` — trả cả min lẫn max
5. Viết `T[] FilterArray<T>(T[] items, Func<T, bool> predicate)` — lọc mảng theo điều kiện
6. Test tất cả với `int[]`, `string[]`, `double[]`

### Gợi ý:
```csharp
// FindSecondMax: sort rồi lấy phần tử thứ 2, hoặc duyệt 2 vòng
// FilterArray: dùng List<T> tạm, sau đó .ToArray()
```

### Input/Output mẫu:
```
PrintArray: [1, 2, 3, 4, 5]
ReverseArray: [5, 4, 3, 2, 1]
FindSecondMax([5, 3, 8, 1, 9]) = 8
FindMinMax([5, 3, 8, 1, 9]) = (1, 9)
FilterArray([1..10], n > 5) = [6, 7, 8, 9, 10]
```

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

class GenericUtility
{
    public static void PrintArray<T>(T[] items)
    {
        Console.Write($"  [{typeof(T).Name}] [");
        for (int i = 0; i < items.Length; i++)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(items[i]);
        }
        Console.WriteLine("]");
    }

    public static T[] ReverseArray<T>(T[] items)
    {
        T[] result = new T[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            result[i] = items[items.Length - 1 - i];
        }
        return result;
    }

    public static T FindSecondMax<T>(T[] items) where T : IComparable<T>
    {
        if (items.Length < 2)
            throw new ArgumentException("Mảng phải có ít nhất 2 phần tử!");

        T max = items[0];
        T secondMax = items[1];

        if (secondMax.CompareTo(max) > 0)
        {
            T temp = max;
            max = secondMax;
            secondMax = temp;
        }

        for (int i = 2; i < items.Length; i++)
        {
            if (items[i].CompareTo(max) > 0)
            {
                secondMax = max;
                max = items[i];
            }
            else if (items[i].CompareTo(secondMax) > 0)
            {
                secondMax = items[i];
            }
        }

        return secondMax;
    }

    public static (T min, T max) FindMinMax<T>(T[] items) where T : IComparable<T>
    {
        if (items.Length == 0)
            throw new ArgumentException("Mảng không được rỗng!");

        T min = items[0];
        T max = items[0];

        for (int i = 1; i < items.Length; i++)
        {
            if (items[i].CompareTo(min) < 0) min = items[i];
            if (items[i].CompareTo(max) > 0) max = items[i];
        }

        return (min, max);
    }

    public static T[] FilterArray<T>(T[] items, Func<T, bool> predicate)
    {
        List<T> result = new List<T>();
        foreach (T item in items)
        {
            if (predicate(item))
                result.Add(item);
        }
        return result.ToArray();
    }
}

class Exercise1
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 1: GENERIC UTILITY METHODS ===\n");

        // int[]
        int[] nums = { 5, 3, 8, 1, 9, 2, 7 };
        Console.WriteLine("Original:");
        GenericUtility.PrintArray(nums);

        Console.WriteLine("Reversed:");
        GenericUtility.PrintArray(GenericUtility.ReverseArray(nums));

        Console.WriteLine($"  SecondMax = {GenericUtility.FindSecondMax(nums)}");

        var (min, max) = GenericUtility.FindMinMax(nums);
        Console.WriteLine($"  MinMax = ({min}, {max})");

        Console.WriteLine("Filtered (> 5):");
        GenericUtility.PrintArray(GenericUtility.FilterArray(nums, n => n > 5));

        // string[]
        Console.WriteLine("\nString array:");
        string[] names = { "Zen", "Alpha", "Minh", "Beta", "Omega" };
        GenericUtility.PrintArray(names);
        Console.WriteLine($"  SecondMax = \"{GenericUtility.FindSecondMax(names)}\"");

        Console.WriteLine("Filtered (Length > 4):");
        GenericUtility.PrintArray(GenericUtility.FilterArray(names, s => s.Length > 4));
    }
}
```

---

## Bài 2: Generic Wrapper Classes ⭐⭐

> **Mục tiêu**: Tạo các generic class wrapper hữu ích.

### Yêu cầu:

1. **`Wrapper<T>`** — bao bọc giá trị với metadata:
   - Property: `Value`, `CreatedAt`, `ModifiedAt`, `ModifyCount`
   - Method: `SetValue(T newValue)` — cập nhật value + tăng modify count + update ModifiedAt
   - Method: `Reset(T defaultValue)` — reset về giá trị mặc định

2. **`LazyValue<T>`** — giá trị chỉ tính khi cần:
   - Constructor nhận `Func<T> factory`
   - Property `Value` — gọi factory lần đầu, cache kết quả
   - Property `IsValueCreated` — đã tạo chưa?

3. **`HistoryValue<T>`** — giá trị có lịch sử thay đổi:
   - Dùng `Stack<T>` lưu lịch sử
   - Method: `Set(T value)`, `Undo()`, `GetHistory()`
   - Property: `Current`, `CanUndo`

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

// ---- Wrapper<T> ----
class Wrapper<T>
{
    public T Value { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime ModifiedAt { get; private set; }
    public int ModifyCount { get; private set; }

    public Wrapper(T initialValue)
    {
        Value = initialValue;
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
        ModifyCount = 0;
    }

    public void SetValue(T newValue)
    {
        Value = newValue;
        ModifiedAt = DateTime.Now;
        ModifyCount++;
    }

    public void Reset(T defaultValue)
    {
        Value = defaultValue;
        ModifiedAt = DateTime.Now;
        ModifyCount = 0;
    }

    public override string ToString()
    {
        return $"Wrapper<{typeof(T).Name}>({Value}) [Modified: {ModifyCount}x]";
    }
}

// ---- LazyValue<T> ----
class LazyValue<T>
{
    private readonly Func<T> _factory;
    private T? _value;
    private bool _isValueCreated;

    public LazyValue(Func<T> factory)
    {
        _factory = factory;
        _isValueCreated = false;
    }

    public T Value
    {
        get
        {
            if (!_isValueCreated)
            {
                Console.WriteLine($"    💤 LazyValue: Tạo giá trị lần đầu...");
                _value = _factory();
                _isValueCreated = true;
            }
            return _value!;
        }
    }

    public bool IsValueCreated => _isValueCreated;
}

// ---- HistoryValue<T> ----
class HistoryValue<T>
{
    private T _current;
    private Stack<T> _history = new Stack<T>();

    public T Current => _current;
    public bool CanUndo => _history.Count > 0;
    public int HistoryCount => _history.Count;

    public HistoryValue(T initialValue)
    {
        _current = initialValue;
    }

    public void Set(T newValue)
    {
        _history.Push(_current);
        _current = newValue;
    }

    public T Undo()
    {
        if (!CanUndo)
            throw new InvalidOperationException("Không có lịch sử để undo!");

        _current = _history.Pop();
        return _current;
    }

    public List<T> GetHistory()
    {
        List<T> result = new List<T>();
        foreach (T item in _history)
        {
            result.Add(item);
        }
        return result;
    }
}

class Exercise2
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 2: GENERIC WRAPPER CLASSES ===\n");

        // --- Wrapper<T> ---
        Console.WriteLine("📦 Wrapper<T>:");
        Wrapper<string> name = new Wrapper<string>("Minh");
        Console.WriteLine($"  {name}");
        name.SetValue("Lan");
        name.SetValue("Hùng");
        Console.WriteLine($"  {name}");

        Wrapper<int> score = new Wrapper<int>(0);
        score.SetValue(85);
        score.SetValue(92);
        score.SetValue(100);
        Console.WriteLine($"  {score}");

        // --- LazyValue<T> ---
        Console.WriteLine("\n💤 LazyValue<T>:");
        LazyValue<List<int>> lazyList = new LazyValue<List<int>>(() =>
        {
            Console.WriteLine("    ⚙️ Đang tạo danh sách lớn...");
            List<int> list = new List<int>();
            for (int i = 0; i < 1000; i++) list.Add(i);
            return list;
        });

        Console.WriteLine($"  IsValueCreated = {lazyList.IsValueCreated}");
        Console.WriteLine($"  Truy cập Value lần 1:");
        Console.WriteLine($"  Count = {lazyList.Value.Count}");
        Console.WriteLine($"  IsValueCreated = {lazyList.IsValueCreated}");
        Console.WriteLine($"  Truy cập Value lần 2 (không tạo lại):");
        Console.WriteLine($"  Count = {lazyList.Value.Count}");

        // --- HistoryValue<T> ---
        Console.WriteLine("\n📜 HistoryValue<T>:");
        HistoryValue<string> theme = new HistoryValue<string>("Light");
        Console.WriteLine($"  Current: {theme.Current}");

        theme.Set("Dark");
        theme.Set("Blue");
        theme.Set("Monokai");
        Console.WriteLine($"  Current: {theme.Current}");
        Console.WriteLine($"  History: [{string.Join(" → ", theme.GetHistory())}]");

        Console.WriteLine($"  Undo → {theme.Undo()}");
        Console.WriteLine($"  Undo → {theme.Undo()}");
        Console.WriteLine($"  Current: {theme.Current}");
    }
}
```

---

## Bài 3: Generic Collection — SortedList\<T> ⭐⭐⭐

> **Mục tiêu**: Tự viết một SortedList\<T> — danh sách luôn được sắp xếp.

### Yêu cầu:
1. Tạo class `SortedList<T> where T : IComparable<T>`
2. Implement:
   - `Add(T item)` — thêm phần tử và duy trì thứ tự sắp xếp
   - `Remove(T item)` — xóa phần tử
   - `Contains(T item)` — kiểm tra tồn tại (dùng Binary Search)
   - `T this[int index]` — indexer
   - `Count` — số phần tử
   - `Min` / `Max` — giá trị nhỏ/lớn nhất (luôn ở đầu/cuối)
   - `ToArray()` — trả mảng
   - `Print()` — in ra

3. Add phải chèn đúng vị trí (Binary Search insertion)
4. Test với `SortedList<int>` và `SortedList<string>`

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

class SortedList<T> where T : IComparable<T>
{
    private List<T> _items = new List<T>();

    public int Count => _items.Count;
    public T Min => _items.Count > 0 ? _items[0] : throw new InvalidOperationException("Empty!");
    public T Max => _items.Count > 0 ? _items[^1] : throw new InvalidOperationException("Empty!");

    // Indexer
    public T this[int index] => _items[index];

    // Thêm phần tử — duy trì thứ tự
    public void Add(T item)
    {
        // Tìm vị trí chèn bằng Binary Search
        int index = FindInsertIndex(item);
        _items.Insert(index, item);
    }

    // Xóa phần tử
    public bool Remove(T item)
    {
        int index = BinarySearch(item);
        if (index >= 0)
        {
            _items.RemoveAt(index);
            return true;
        }
        return false;
    }

    // Tìm kiếm bằng Binary Search
    public bool Contains(T item)
    {
        return BinarySearch(item) >= 0;
    }

    // Trả mảng
    public T[] ToArray() => _items.ToArray();

    // In ra
    public void Print(string label = "")
    {
        Console.Write($"  SortedList<{typeof(T).Name}>{label}: [");
        Console.Write(string.Join(", ", _items));
        Console.WriteLine($"] (Count: {Count})");
    }

    // Binary Search — tìm vị trí chèn
    private int FindInsertIndex(T item)
    {
        int low = 0, high = _items.Count - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            int cmp = item.CompareTo(_items[mid]);
            if (cmp <= 0) high = mid - 1;
            else low = mid + 1;
        }
        return low;
    }

    // Binary Search — tìm phần tử
    private int BinarySearch(T item)
    {
        int low = 0, high = _items.Count - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            int cmp = item.CompareTo(_items[mid]);
            if (cmp == 0) return mid;
            if (cmp < 0) high = mid - 1;
            else low = mid + 1;
        }
        return -1;
    }
}

class Exercise3
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 3: SORTED LIST<T> ===\n");

        // int
        SortedList<int> nums = new SortedList<int>();
        int[] input = { 50, 20, 80, 10, 40, 90, 30, 60, 70 };

        Console.WriteLine("  Thêm: " + string.Join(", ", input));
        foreach (int n in input)
        {
            nums.Add(n);
        }
        nums.Print();
        Console.WriteLine($"  Min={nums.Min}, Max={nums.Max}");
        Console.WriteLine($"  Contains(40)={nums.Contains(40)}, Contains(99)={nums.Contains(99)}");

        nums.Remove(50);
        Console.Write("  Sau khi xóa 50: ");
        nums.Print();

        // string
        Console.WriteLine();
        SortedList<string> names = new SortedList<string>();
        string[] nameList = { "Minh", "An", "Zen", "Bình", "Lan" };
        foreach (string name in nameList)
        {
            names.Add(name);
        }
        names.Print();
        Console.WriteLine($"  Min=\"{names.Min}\", Max=\"{names.Max}\"");
    }
}
```

---

## Bài 4: Generic Event System ⭐⭐⭐

> **Mục tiêu**: Tạo hệ thống sự kiện generic (Publisher/Subscriber pattern).

### Yêu cầu:
1. Tạo `class EventBus<T>` với:
   - `Subscribe(string eventName, Action<T> handler)` — đăng ký listener
   - `Unsubscribe(string eventName, Action<T> handler)` — hủy đăng ký
   - `Publish(string eventName, T data)` — phát sự kiện
   - `GetSubscriberCount(string eventName)` — đếm listeners
2. Dùng `Dictionary<string, List<Action<T>>>` bên trong
3. Test với nhiều loại event data

### ✅ Đáp án tham khảo:

```csharp
using System;
using System.Collections.Generic;

class EventBus<T>
{
    private Dictionary<string, List<Action<T>>> _handlers = new();

    public void Subscribe(string eventName, Action<T> handler)
    {
        if (!_handlers.ContainsKey(eventName))
        {
            _handlers[eventName] = new List<Action<T>>();
        }
        _handlers[eventName].Add(handler);
        Console.WriteLine($"  📡 Subscribed to \"{eventName}\"");
    }

    public void Unsubscribe(string eventName, Action<T> handler)
    {
        if (_handlers.ContainsKey(eventName))
        {
            _handlers[eventName].Remove(handler);
            Console.WriteLine($"  🔇 Unsubscribed from \"{eventName}\"");
        }
    }

    public void Publish(string eventName, T data)
    {
        if (!_handlers.ContainsKey(eventName) || _handlers[eventName].Count == 0)
        {
            Console.WriteLine($"  ⚠️ No subscribers for \"{eventName}\"");
            return;
        }

        Console.WriteLine($"  📢 Publishing \"{eventName}\" to {_handlers[eventName].Count} subscribers:");
        foreach (var handler in _handlers[eventName])
        {
            handler(data);
        }
    }

    public int GetSubscriberCount(string eventName)
    {
        return _handlers.ContainsKey(eventName) ? _handlers[eventName].Count : 0;
    }
}

class Exercise4
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 4: GENERIC EVENT SYSTEM ===\n");

        // EventBus cho string messages
        EventBus<string> messageBus = new EventBus<string>();

        messageBus.Subscribe("chat", msg => Console.WriteLine($"    💬 Chat: {msg}"));
        messageBus.Subscribe("chat", msg => Console.WriteLine($"    📱 Notification: {msg}"));
        messageBus.Subscribe("alert", msg => Console.WriteLine($"    🚨 Alert: {msg}"));

        Console.WriteLine();
        messageBus.Publish("chat", "Xin chào!");
        Console.WriteLine();
        messageBus.Publish("alert", "Hệ thống bảo trì lúc 2h!");

        // EventBus cho int (scores)
        Console.WriteLine("\n--- EventBus<int> cho điểm số ---\n");
        EventBus<int> scoreBus = new EventBus<int>();

        scoreBus.Subscribe("new_score", score =>
        {
            Console.WriteLine($"    📊 Score recorded: {score}");
            if (score >= 90) Console.WriteLine($"    🏆 Excellent!");
        });

        scoreBus.Subscribe("new_score", score =>
        {
            Console.WriteLine($"    📈 Leaderboard updated");
        });

        Console.WriteLine();
        scoreBus.Publish("new_score", 95);
        Console.WriteLine();
        scoreBus.Publish("new_score", 72);
    }
}
```

---

## Bài 5: Generic Converter\<TSource, TDest> ⭐⭐⭐

> **Mục tiêu**: Tạo hệ thống chuyển đổi kiểu generic.

### Yêu cầu:
1. Tạo `interface IConverter<TSource, TDest>` với:
   - `TDest Convert(TSource source)`
   - `TSource ConvertBack(TDest dest)`

2. Implement:
   - `CelsiusToFahrenheitConverter : IConverter<double, double>`
   - `StringToIntConverter : IConverter<string, int>`
   - `StudentToDtoConverter : IConverter<Student, StudentDto>` (DTO = Data Transfer Object)

3. Tạo `class ConverterPipeline<T1, T2, T3>` — chuyển đổi qua 2 bước:
   - Nhận `IConverter<T1, T2>` và `IConverter<T2, T3>`
   - Method `T3 Convert(T1 input)` — convert T1 → T2 → T3

### ✅ Đáp án tham khảo:

```csharp
using System;

interface IConverter<TSource, TDest>
{
    TDest Convert(TSource source);
    TSource ConvertBack(TDest dest);
}

// Celsius ↔ Fahrenheit
class CelsiusToFahrenheitConverter : IConverter<double, double>
{
    public double Convert(double celsius)
    {
        return celsius * 9.0 / 5.0 + 32;
    }

    public double ConvertBack(double fahrenheit)
    {
        return (fahrenheit - 32) * 5.0 / 9.0;
    }
}

// String ↔ Int
class StringToIntConverter : IConverter<string, int>
{
    public int Convert(string source)
    {
        if (int.TryParse(source, out int result))
            return result;
        throw new FormatException($"Cannot convert '{source}' to int");
    }

    public string ConvertBack(int dest)
    {
        return dest.ToString();
    }
}

// Student ↔ StudentDto
class Student2
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public double Gpa { get; set; }
}

class StudentDto
{
    public string FullName { get; set; } = "";
    public string Grade { get; set; } = "";
}

class StudentToDtoConverter : IConverter<Student2, StudentDto>
{
    public StudentDto Convert(Student2 source)
    {
        string grade = source.Gpa switch
        {
            >= 9.0 => "Xuất sắc",
            >= 8.0 => "Giỏi",
            >= 7.0 => "Khá",
            >= 5.0 => "Trung bình",
            _ => "Yếu"
        };

        return new StudentDto
        {
            FullName = $"{source.FirstName} {source.LastName}",
            Grade = grade
        };
    }

    public Student2 ConvertBack(StudentDto dest)
    {
        string[] parts = dest.FullName.Split(' ', 2);
        return new Student2
        {
            FirstName = parts[0],
            LastName = parts.Length > 1 ? parts[1] : "",
        };
    }
}

// Pipeline: T1 → T2 → T3
class ConverterPipeline<T1, T2, T3>
{
    private IConverter<T1, T2> _first;
    private IConverter<T2, T3> _second;

    public ConverterPipeline(IConverter<T1, T2> first, IConverter<T2, T3> second)
    {
        _first = first;
        _second = second;
    }

    public T3 Convert(T1 input)
    {
        T2 intermediate = _first.Convert(input);
        return _second.Convert(intermediate);
    }
}

class Exercise5
{
    static void Main()
    {
        Console.WriteLine("=== BÀI 5: GENERIC CONVERTER ===\n");

        // Temperature
        var tempConverter = new CelsiusToFahrenheitConverter();
        double celsius = 37.5;
        double fahrenheit = tempConverter.Convert(celsius);
        Console.WriteLine($"  🌡️ {celsius}°C = {fahrenheit}°F");
        Console.WriteLine($"  🌡️ {fahrenheit}°F = {tempConverter.ConvertBack(fahrenheit)}°C");

        // String ↔ Int
        Console.WriteLine();
        var strIntConverter = new StringToIntConverter();
        Console.WriteLine($"  \"42\" → {strIntConverter.Convert("42")}");
        Console.WriteLine($"  100 → \"{strIntConverter.ConvertBack(100)}\"");

        // Student → DTO
        Console.WriteLine();
        var studentConverter = new StudentToDtoConverter();
        Student2 student = new Student2
        {
            FirstName = "Nguyễn",
            LastName = "Minh",
            Age = 20,
            Gpa = 8.7
        };
        StudentDto dto = studentConverter.Convert(student);
        Console.WriteLine($"  Student → DTO:");
        Console.WriteLine($"    FullName: {dto.FullName}");
        Console.WriteLine($"    Grade: {dto.Grade}");

        // Pipeline: string → int → double (temperature)
        Console.WriteLine("\n--- Pipeline ---");
        // Demo concept: "100" → 100 (int) → sử dụng int
        var pipeline = new ConverterPipeline<string, int, string>(
            new StringToIntConverter(),
            new IntToHexConverter()
        );
        Console.WriteLine($"  \"255\" → hex → \"{pipeline.Convert("255")}\"");
    }
}

// Bonus converter cho pipeline demo
class IntToHexConverter : IConverter<int, string>
{
    public string Convert(int source) => $"0x{source:X}";
    public int ConvertBack(string dest) => System.Convert.ToInt32(dest, 16);
}
```

---

## 📊 Tổng kết bài tập

| Bài | Chủ đề | Generic Feature | Độ khó |
|-----|--------|-----------------|--------|
| 1 | Utility Methods | Generic methods, constraints | ⭐ |
| 2 | Wrapper Classes | Generic classes, Lazy pattern | ⭐⭐ |
| 3 | SortedList\<T> | Custom collection, Binary Search | ⭐⭐⭐ |
| 4 | Event System | Generic + Dictionary + Action\<T> | ⭐⭐⭐ |
| 5 | Converter System | Multiple type params, Pipeline | ⭐⭐⭐ |

> 💡 **Mẹo**: Khi thấy mình viết cùng logic cho nhiều kiểu → nghĩ đến Generic!
