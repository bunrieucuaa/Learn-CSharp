# 🏆 Bài 21: Challenge — Zoo Simulation 🦁

> **Dự án lớn:** Xây dựng mô phỏng Sở thú với hành vi đa hình cho các loài động vật!

---

## 📋 Mô tả dự án

Bạn là lập trình viên cho một Sở thú ảo. Hệ thống cần quản lý nhiều loài động vật, mỗi loài có hành vi riêng (ăn, kêu, di chuyển, biểu diễn). Sở thú tổ chức **show biểu diễn** hàng ngày, và bạn cần code hệ thống điều phối bằng **Polymorphism**.

---

## 🏗️ Kiến trúc

```
                    Animal (base class)
                   /       |        \
              Mammal      Bird      Reptile
             /    \       /   \        \
          Lion  Elephant Eagle Parrot  Snake
```

---

## 📝 Yêu cầu chi tiết

### Phần 1: Base class `Animal`

```csharp
class Animal
{
    // Properties
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }        // kg
    public int Hunger { get; set; }           // 0-100 (0=no đủ, 100=rất đói)
    public int Energy { get; set; }           // 0-100
    public int Happiness { get; set; }        // 0-100

    // Virtual methods — class con override
    public virtual void MakeSound() { }       // Kêu
    public virtual void Eat(string food) { }  // Ăn
    public virtual void Move() { }            // Di chuyển
    public virtual void Perform() { }         // Biểu diễn
    public virtual void Sleep() { }           // Ngủ
    public virtual string GetDescription() { return ""; }

    // Concrete methods
    public void ShowStatus() { }              // In trạng thái
    public bool IsHungry() => Hunger > 60;
    public bool IsTired() => Energy < 30;
    public bool IsHappy() => Happiness > 50;
}
```

### Phần 2: Intermediate classes

**`Mammal` (kế thừa Animal):**
- Thêm: `FurColor` (string)
- Override `Sleep()`: "Cuộn mình ngủ" + Energy += 30

**`Bird` (kế thừa Animal):**
- Thêm: `WingSpan` (double, mét), `CanFly` (bool)
- Override `Move()`: nếu CanFly → "Bay lượn" ngược lại "Đi bộ"
- Override `Sleep()`: "Đậu trên cành ngủ" + Energy += 25

**`Reptile` (kế thừa Animal):**
- Thêm: `IsVenomous` (bool)
- Override `Sleep()`: "Nằm phơi nắng nghỉ ngơi" + Energy += 20

### Phần 3: Concrete animal classes

**`Lion` (kế thừa Mammal):**
- `MakeSound()`: "GRRR! ROAR!" + in hoa tên
- `Eat("thịt")`: Hunger -= 40, "Xé thịt ăn ngon lành!"
- `Eat(other)`: "Sư tử chỉ ăn thịt!" Hunger không đổi
- `Move()`: "Sải bước oai phong"
- `Perform()`: "Nhảy qua vòng lửa!" Energy -= 30, Happiness += 10

**`Elephant` (kế thừa Mammal):**
- `MakeSound()`: "PAOO! PAOO!"
- `Eat("cỏ"/"trái cây")`: Hunger -= 30, mô tả ăn
- `Move()`: "Đi chậm rãi, nặng nề"
- `Perform()`: "Đứng trên 2 chân sau!" Energy -= 20, Happiness += 15
- Thêm method riêng: `SprayWater()` — phun nước

**`Eagle` (kế thừa Bird, CanFly = true):**
- `MakeSound()`: "SCREECH!"
- `Eat("cá"/"thịt")`: Hunger -= 35
- `Perform()`: "Bay cao rồi lao xuống bắt mồi giả!" Energy -= 25

**`Parrot` (kế thừa Bird, CanFly = true):**
- `MakeSound()`: "Xin chào! Polly muốn bánh!"
- `Eat("hạt"/"trái cây")`: Hunger -= 25
- `Perform()`: "Nhắc lại lời khán giả!" Energy -= 10, Happiness += 20
- Thêm method riêng: `Mimic(string phrase)` — nhắc lại câu nói

**`Snake` (kế thừa Reptile):**
- `MakeSound()`: "Ssssss..."
- `Eat("chuột")`: Hunger -= 50, "Nuốt chửng!"
- `Move()`: "Trườn bò uốn lượn"
- `Perform()`: "Uốn mình theo tiếng sáo!" Energy -= 15

### Phần 4: Class `Zoo`

```csharp
class Zoo
{
    private string ZooName;
    private Animal[] Animals;     // Mảng đa hình!
    private int AnimalCount;
    private int MaxCapacity;

    // Methods
    public void AddAnimal(Animal animal) { }
    public void FeedAll() { }              // Cho tất cả ăn
    public void MorningRoutine() { }       // Buổi sáng: thức dậy, ăn, di chuyển
    public void AfternoonShow() { }        // Buổi chiều: biểu diễn!
    public void EveningRoutine() { }       // Buổi tối: ăn, ngủ
    public void ShowAllStatus() { }        // Hiển thị trạng thái tất cả
    public void FindHungryAnimals() { }    // Tìm con vật đói (dùng is/as)
    public void CountByType() { }          // Đếm theo loại (Mammal, Bird, Reptile)
    public Animal FindHappiestAnimal() { } // Tìm con vật hạnh phúc nhất
}
```

### Phần 5: Class `ZooShow` — Show biểu diễn

```csharp
class ZooShow
{
    private string ShowName;
    private Animal[] Performers;       // Mảng đa hình!
    private int PerformerCount;

    public void AddPerformer(Animal animal) { }
    public void StartShow() { }        // Chạy show — mỗi con vật biểu diễn
    public void InteractWithAudience() { }  // Parrot nhắc lại, Lion gầm...
    public void ShowReport() { }       // Báo cáo: năng lượng, hạnh phúc
}
```

---

## 🎮 Main Program — Mô phỏng 1 ngày

```csharp
static void Main()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    // 1. Tạo sở thú
    Zoo zoo = new Zoo("Sở Thú Hạnh Phúc", 20);

    // 2. Thêm động vật (đa hình!)
    zoo.AddAnimal(new Lion("Simba", 5, 180));
    zoo.AddAnimal(new Elephant("Dumbo", 10, 4000));
    zoo.AddAnimal(new Eagle("Freedom", 3, 5));
    zoo.AddAnimal(new Parrot("Polly", 2, 0.5));
    zoo.AddAnimal(new Snake("Nagini", 4, 15));
    zoo.AddAnimal(new Lion("Nala", 4, 150));

    // 3. Buổi sáng
    Console.WriteLine("☀️ === BUỔI SÁNG ===");
    zoo.MorningRoutine();
    zoo.ShowAllStatus();

    // 4. Buổi chiều — Show biểu diễn!
    Console.WriteLine("🎪 === BUỔI CHIỀU — SHOW TIME! ===");
    ZooShow show = new ZooShow("Show Kỳ Diệu");
    // Thêm performers...
    show.StartShow();

    // 5. Kiểm tra sau show
    zoo.FindHungryAnimals();
    Console.WriteLine($"🏆 Con vật hạnh phúc nhất: {zoo.FindHappiestAnimal().Name}");

    // 6. Buổi tối
    Console.WriteLine("🌙 === BUỔI TỐI ===");
    zoo.EveningRoutine();

    // 7. Thống kê cuối ngày
    Console.WriteLine("📊 === THỐNG KÊ ===");
    zoo.CountByType();
    zoo.ShowAllStatus();

    // 8. Pattern matching bonus
    Console.WriteLine("🔍 === THÔNG TIN CHI TIẾT ===");
    // Dùng pattern matching để hiển thị thông tin riêng:
    // - Mammal: FurColor
    // - Bird: WingSpan, CanFly
    // - Reptile: IsVenomous
}
```

---

## ✅ Output mong đợi (tham khảo)

```
╔════════════════════════════════════════╗
║   🦁 SỞ THÚ HẠNH PHÚC — MÔ PHỎNG    ║
╚════════════════════════════════════════╝

☀️ === BUỔI SÁNG ===
  🌅 Thức dậy!
  Lion Simba: GRRR! ROAR!
  Elephant Dumbo: PAOO! PAOO!
  Eagle Freedom: SCREECH!
  Parrot Polly: Xin chào! Polly muốn bánh!
  Snake Nagini: Ssssss...

  🍖 Cho ăn sáng...
  Simba xé thịt ăn ngon lành! (Hunger: 60→20)
  Dumbo ăn cỏ tươi (Hunger: 50→20)
  ...

  ═══ TRẠNG THÁI ═══
  Simba    [████████████████░░░░] Hunger:20 Energy:80 Happy:60
  Dumbo    [██████████████░░░░░░] Hunger:20 Energy:70 Happy:55
  ...

🎪 === BUỔI CHIỀU — SHOW TIME! ===
  🎬 Show "Show Kỳ Diệu" bắt đầu!

  🦁 Simba: Nhảy qua vòng lửa!
  👏 Khán giả vỗ tay!

  🐘 Dumbo: Đứng trên 2 chân sau!
  👏 Khán giả vỗ tay!

  🦅 Freedom: Bay cao rồi lao xuống bắt mồi giả!
  👏 Khán giả vỗ tay!

  🦜 Polly: Nhắc lại lời khán giả!
  Polly nói: "Sở thú vui quá!"
  👏 Khán giả vỗ tay!

  🐍 Nagini: Uốn mình theo tiếng sáo!
  👏 Khán giả vỗ tay!

  🎬 Show kết thúc!

📊 === THỐNG KÊ ===
  🐾 Mammals: 3 (Lion: 2, Elephant: 1)
  🐦 Birds:   2 (Eagle: 1, Parrot: 1)
  🦎 Reptiles: 1 (Snake: 1)
  📈 Tổng: 6 động vật

🔍 === THÔNG TIN CHI TIẾT ===
  Simba (Lion) — Lông màu vàng, Tuổi: 5
  Dumbo (Elephant) — Lông màu xám, Tuổi: 10
  Freedom (Eagle) — Sải cánh: 2.1m, Bay được: Có
  Polly (Parrot) — Sải cánh: 0.3m, Bay được: Có
  Nagini (Snake) — Có nọc độc: Có
```

---

## 🎯 Tiêu chí đánh giá

| Tiêu chí | Điểm | Mô tả |
|-----------|-------|--------|
| Kế thừa đúng | 20% | Hierarchy 3 tầng hoạt động đúng |
| Polymorphism | 30% | virtual/override đúng, mảng đa hình |
| Type checking | 15% | Dùng is/as/pattern matching chính xác |
| Zoo logic | 20% | Morning/Afternoon/Evening routine chạy đúng |
| Code quality | 15% | Clean code, có comment, naming đúng |

---

## 💡 Bonus (tùy chọn)

1. **Thêm loài mới** (Penguin, Tiger, Crocodile) — xem code có cần sửa không?
2. **Random events:** Đôi khi con vật không chịu biểu diễn (Energy < 20)
3. **Feeding schedule:** Mỗi loại ăn đồ khác nhau, cho ăn sai → Happiness giảm
4. **Show rating:** Tính điểm show dựa trên Happiness + Energy của performers

> **Mục tiêu:** Hoàn thành trong 60-90 phút. Dùng tất cả kiến thức từ Bài 15-21! 🚀
