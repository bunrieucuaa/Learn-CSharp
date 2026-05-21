# ✏️ Lesson 20 — Bài tập: Inheritance (Tính kế thừa)

---

## Bài 1: Animal Hierarchy cơ bản ⭐

### Yêu cầu:

Tạo hierarchy đơn giản:

```
Animal (base)
├── Dog
├── Cat
└── Rabbit
```

### Class Animal (base):
- Properties: `Name`, `Age`, `Color`
- Constructor `Animal(name, age, color)`
- `virtual void MakeSound()` → "..."
- `virtual void Move()` → "{Name} di chuyển"
- `override string ToString()` → "{Name} ({Type}, {Age} tuổi)"

### Class Dog:
- Thêm: `string Breed`
- Constructor gọi `base(name, age, color)`
- Override `MakeSound()` → "Gâu gâu!"
- Override `Move()` → "{Name} chạy nhanh!"
- Thêm: `void Fetch()` → "{Name} nhặt bóng!"

### Class Cat:
- Thêm: `bool IsIndoor`
- Override `MakeSound()` → "Meo meo!"
- Override `Move()` → "{Name} nhảy lên cao!"
- Thêm: `void Scratch()` → "{Name} cào cào!"

### Class Rabbit:
- Thêm: `double EarLength` (cm)
- Override `MakeSound()` → "(im lặng)"
- Override `Move()` → "{Name} nhảy nhảy!"

### Test:

```csharp
Animal[] animals = {
    new Dog("Buddy", 3, "Nâu", "Corgi"),
    new Cat("Mimi", 2, "Trắng", true),
    new Rabbit("Bunny", 1, "Xám", 8.5)
};

foreach (var a in animals)
{
    Console.WriteLine(a);          // ToString()
    a.MakeSound();                 // Override khác nhau
    a.Move();                      // Override khác nhau
    Console.WriteLine();
}

// Dog-specific
if (animals[0] is Dog dog) dog.Fetch();
```

---

## Bài 2: Device Hierarchy — Constructor Chain ⭐⭐

### Yêu cầu:

Tạo hierarchy thiết bị điện tử:

```
Device (base)
├── Phone
├── Laptop
└── Tablet
```

### Class Device (base):
- Properties: `Brand`, `Model`, `Price`, `int BatteryPercent`
- Constructor `Device(brand, model, price)` — battery = 100
- `virtual void TurnOn()` → "{Model} đang bật..."
- `virtual void TurnOff()` → "{Model} đang tắt..."
- `void Charge()` → Battery = 100
- `virtual void UseBattery(int percent)` — giảm battery, min 0
- `virtual string GetSpecs()` → trả về thông tin
- `override string ToString()`

### Class Phone (kế thừa Device):
- Thêm: `string PhoneNumber`, `int StorageGB`
- Constructor: `Phone(brand, model, price, phoneNumber, storage)` gọi `base(...)`
- `void Call(string number)` — tốn 5% battery
- `void TakePhoto()` — tốn 3% battery
- Override `GetSpecs()` — thêm SĐT + Storage

### Class Laptop:
- Thêm: `int RamGB`, `string Cpu`
- `void Code(string language)` — tốn 10% battery
- `void RunGame(string game)` — tốn 20% battery
- Override `GetSpecs()` — thêm RAM + CPU

### Class Tablet:
- Thêm: `double ScreenSize` (inch), `bool HasStylus`
- `void Draw()` — tốn 8% battery, chỉ khi HasStylus
- Override `GetSpecs()`

### Test:

```csharp
var phone = new Phone("Samsung", "Galaxy S24", 22990000m, "0912345678", 256);
var laptop = new Laptop("MacBook", "Air M3", 32990000m, 16, "M3");
var tablet = new Tablet("iPad", "Air", 16990000m, 11.0, true);

phone.Call("0909090909");
phone.TakePhoto();
Console.WriteLine($"Phone battery: {phone.BatteryPercent}%");

laptop.Code("C#");
laptop.RunGame("Cyberpunk");
Console.WriteLine($"Laptop battery: {laptop.BatteryPercent}%");
```

---

## Bài 3: BankAccount Hierarchy — protected + base() ⭐⭐

### Yêu cầu:

```
BankAccount (base)
├── SavingsAccount (tiết kiệm — có lãi suất)
├── CheckingAccount (thanh toán — có phí GD)
└── StudentAccount (sinh viên — giới hạn rút)
```

### Class BankAccount (base):
- Protected: `decimal _balance`
- Properties: `AccountNumber` (auto-gen), `Owner`, `Balance` (readonly)
- Constructor: `BankAccount(owner, initialBalance)`
- `virtual bool Deposit(decimal amount)` — validate > 0
- `virtual bool Withdraw(decimal amount)` — validate > 0, <= balance
- `virtual void PrintStatement()` — in sao kê
- `override string ToString()`

### Class SavingsAccount:
- Thêm: `decimal InterestRate` (% năm, ví dụ 0.05 = 5%)
- `void ApplyInterest()` — cộng lãi vào balance
- Override `Withdraw()` — yêu cầu balance sau rút >= 1,000,000 (duy trì tối thiểu)

### Class CheckingAccount:
- Thêm: `decimal TransactionFee` (phí mỗi GD, ví dụ 10,000)
- Override `Withdraw()` — trừ thêm phí giao dịch
- Override `Deposit()` — miễn phí
- Thêm: `int TransactionCount`

### Class StudentAccount:
- Thêm: `decimal DailyLimit` (giới hạn rút/ngày, ví dụ 2,000,000)
- Private: `decimal _withdrawnToday`, `DateTime _lastWithdrawDate`
- Override `Withdraw()` — kiểm tra giới hạn rút hàng ngày
- Override `PrintStatement()` — thêm thông tin limit

### Test:

```csharp
var savings = new SavingsAccount("Minh", 50_000_000m, 0.06m);
var checking = new CheckingAccount("Lan", 20_000_000m, 11_000m);
var student = new StudentAccount("Hùng", 5_000_000m, 2_000_000m);

savings.ApplyInterest();  // +5% = +2,500,000
savings.Withdraw(52_000_000m);  // ❌ Balance sau rút < 1,000,000!

checking.Withdraw(1_000_000m);  // Rút 1tr + phí 11k
student.Withdraw(1_500_000m);   // ✅ Trong limit
student.Withdraw(1_000_000m);   // ❌ Vượt limit 2tr/ngày!
```

---

## Bài 4: Shape Hierarchy — Sealed + GetType() ⭐⭐⭐

### Yêu cầu:

```
Shape (base)
├── Circle (sealed — không cho kế thừa tiếp)
├── Rectangle
│   └── Square (kế thừa Rectangle)
└── Triangle
```

### Class Shape:
- `string Color { get; set; }`
- `virtual double Area()`, `virtual double Perimeter()`
- `virtual void Draw()` — in ASCII đơn giản
- `override string ToString()` — "{ShapeType}: S={Area}, C={Perimeter}"

### Class Circle (sealed):
- `double Radius`
- Override Area, Perimeter, Draw, ToString

### Class Rectangle:
- `double Width`, `double Height`
- `bool IsSquare` computed
- Override Area, Perimeter, Draw

### Class Square (kế thừa Rectangle):
- Constructor `Square(double side)` : `base(side, side)`
- **Sealed override** `Draw()` — không cho kế thừa tiếp Draw

### Class Triangle:
- 3 cạnh + validate tam giác
- Heron's formula cho Area

### Test:

```csharp
Shape[] shapes = {
    new Circle(5),
    new Rectangle(8, 4),
    new Square(6),
    new Triangle(3, 4, 5)
};

foreach (var s in shapes)
{
    Console.WriteLine(s);
    Console.WriteLine($"  Type: {s.GetType().Name}");
    s.Draw();
}

// Square IS-A Rectangle?
Rectangle rect = new Square(5);  // ✅ Upcasting
Console.WriteLine(rect.IsSquare);  // true
```

---

## Bài 5: School System — Upcasting / Downcasting ⭐⭐⭐

### Yêu cầu:

```
Person (base)
├── Student
├── Teacher
└── Staff
```

### Class Person:
- `string Name`, `int Age`, `string Email`
- `virtual string GetRole()` → "Person"
- `virtual void Introduce()` → "Tôi là {Name}, {Age} tuổi"
- `override string ToString()`

### Class Student (kế thừa Person):
- Thêm: `string StudentId`, `string Major`, `double GPA`
- Override `GetRole()` → "Sinh viên"
- Override `Introduce()` → base + thêm StudentId + Major

### Class Teacher:
- Thêm: `string Subject`, `int YearsExperience`
- Override `GetRole()` → "Giảng viên"
- `void Teach()` → "{Name} đang giảng {Subject}"

### Class Staff:
- Thêm: `string Position`, `string Office`
- Override `GetRole()` → "Nhân viên"

### Yêu cầu trong Main:

1. Tạo mảng `Person[]` chứa cả Student, Teacher, Staff
2. Duyệt mảng:
   - Gọi `Introduce()` cho tất cả (virtual → đúng kiểu)
   - Dùng `is` pattern matching để xử lý riêng:
     - Student → in GPA
     - Teacher → gọi Teach()
     - Staff → in Office
3. Đếm số lượng mỗi loại
4. Tìm Student có GPA cao nhất (dùng downcasting)

### Test:

```csharp
Person[] people = {
    new Student("Minh", 20, "minh@edu.vn", "SV001", "CNTT", 8.5),
    new Teacher("Hoa", 35, "hoa@edu.vn", "Toán", 10),
    new Student("Lan", 21, "lan@edu.vn", "SV002", "Kinh tế", 9.0),
    new Staff("Hùng", 40, "hung@edu.vn", "Kế toán", "P.201"),
    new Teacher("Nam", 45, "nam@edu.vn", "Lý", 15),
};

foreach (var p in people)
{
    p.Introduce();  // Mỗi loại giới thiệu khác nhau

    if (p is Student s)
        Console.WriteLine($"  → GPA: {s.GPA}");
    else if (p is Teacher t)
        t.Teach();
}

// Tìm Student GPA cao nhất
Student? topStudent = null;
foreach (var p in people)
{
    if (p is Student s && (topStudent == null || s.GPA > topStudent.GPA))
        topStudent = s;
}
Console.WriteLine($"\nTop student: {topStudent?.Name} — GPA: {topStudent?.GPA}");
```

---

## 📊 Bảng tổng hợp

| Bài | Chủ đề | Kỹ năng | Độ khó |
|-----|--------|---------|--------|
| 1 | Animal Hierarchy | virtual/override, ToString | ⭐ |
| 2 | Device Hierarchy | Constructor chain, base() | ⭐⭐ |
| 3 | BankAccount Types | protected, override business logic | ⭐⭐ |
| 4 | Shape + Square | sealed, GetType(), Square:Rectangle | ⭐⭐⭐ |
| 5 | School System | Upcasting, downcasting, is pattern | ⭐⭐⭐ |
