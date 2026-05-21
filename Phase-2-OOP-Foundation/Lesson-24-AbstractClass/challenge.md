# 🏆 Bài 24: Challenge — Game Engine Foundation 🎮

> **Xây dựng nền tảng Game Engine sử dụng abstract class hierarchy + interfaces.
> Kết hợp TẤT CẢ kiến thức Phase 2: Class, Constructor, Access Modifiers, Encapsulation, Inheritance, Polymorphism, Abstraction, Interface, Abstract Class nâng cao.**

---

## 🎯 Mục tiêu

Tạo hệ thống **Game Engine** với:
- Abstract class hierarchy cho GameObjects
- Interfaces cho capabilities (IMovable, ICollidable, IRenderable)
- Game Loop pattern (Template Method)
- Collision detection
- Scoring system

---

## 📐 Kiến trúc

```
═══════════════════════════════════════════════════════════
                     GAME ENGINE ARCHITECTURE
═══════════════════════════════════════════════════════════

Interfaces (Capabilities):
┌────────────┐  ┌──────────────┐  ┌──────────────┐
│ IMovable   │  │ ICollidable  │  │ IRenderable  │
│ Move()     │  │ GetBounds()  │  │ Render()     │
│ X, Y       │  │ CollidesWith │  │ RenderChar   │
│ Speed      │  │ OnCollision()│  │ Color        │
└────────────┘  └──────────────┘  └──────────────┘

Abstract Class Hierarchy:
┌──────────────────────────────────────────────────────┐
│ abstract GameObject : IRenderable                     │
│ ┌──────────────────────────────────────┐              │
│ │ Shared: Id, Name, IsActive          │              │
│ │ Abstract: Update(), GetObjectType() │              │
│ │ Concrete: Activate(), Deactivate()  │              │
│ │ Template: ProcessFrame()            │              │
│ └──────────────────────────────────────┘              │
│          ▲                ▲              ▲            │
│          │                │              │            │
│ ┌────────┴──────┐ ┌──────┴─────┐ ┌──────┴──────┐    │
│ │abstract       │ │ Collectible│ │ Obstacle    │    │
│ │Character      │ │ (concrete) │ │ (concrete)  │    │
│ │:IMovable      │ │            │ │:ICollidable │    │
│ │:ICollidable   │ │            │ │             │    │
│ ├───────────────┤ └────────────┘ └─────────────┘    │
│ │Abstract:      │                                    │
│ │ Attack()      │                                    │
│ │ GetClassName()│                                    │
│ │Concrete:      │                                    │
│ │ TakeDamage()  │                                    │
│ │ Move()        │                                    │
│ ├───────┬───────┤                                    │
│ │       │       │                                    │
│ ▼       ▼       ▼                                    │
│Player  Enemy   NPC                                   │
└──────────────────────────────────────────────────────┘

GameLoop:
┌──────────────────────────────────────────┐
│ class GameLoop                            │
│ ├── AddGameObject(GameObject obj)        │
│ ├── Run(int frames)                      │
│ │   └── Mỗi frame:                      │
│ │       1. ProcessInput()                │
│ │       2. UpdateAll()                   │
│ │       3. CheckCollisions()             │
│ │       4. RenderAll()                   │
│ │       5. UpdateScore()                 │
│ ├── GetScore()                           │
│ └── ShowGameState()                      │
└──────────────────────────────────────────┘
```

---

## 📋 Yêu cầu chi tiết

### 1. Interfaces

```csharp
interface IMovable
{
    double X { get; }
    double Y { get; }
    double Speed { get; }
    void Move(double dx, double dy);
    void MoveTo(double x, double y);
}

interface ICollidable
{
    double Width { get; }
    double Height { get; }
    bool CollidesWith(ICollidable other);
    void OnCollision(ICollidable other);
}

interface IRenderable
{
    char RenderChar { get; }
    string Color { get; }
    void Render();
}
```

### 2. Abstract GameObject

```csharp
abstract class GameObject : IRenderable
{
    // Properties: Id (auto-increment), Name, IsActive
    // Abstract: Update(), GetObjectType()
    // Abstract property: RenderChar, Color (từ IRenderable)
    // Concrete: Activate(), Deactivate(), Render()
    //
    // Template Method:
    // public void ProcessFrame()
    // {
    //     if (!IsActive) return;
    //     Update();         // abstract
    //     Render();         // concrete
    // }
}
```

### 3. Abstract Character : GameObject, IMovable, ICollidable

```csharp
abstract class Character : GameObject, IMovable, ICollidable
{
    // Thêm: Hp, MaxHp, AttackPower, Defense, IsAlive
    // Abstract: Attack(Character target), GetClassName()
    // Concrete: TakeDamage(int damage), Heal(int amount)
    // IMovable: Move(), MoveTo() — concrete shared
    // ICollidable: CollidesWith(), OnCollision() — concrete shared
    // sealed override GetObjectType() => "Character"
}
```

### 4. Concrete Characters

**Player:**
- RenderChar = '@', Color = "Green"
- Có Score, Lives
- Attack(): physical damage
- Khi HP = 0 → mất 1 Lives, hồi full HP
- GetClassName() => "Player"

**Enemy:**
- RenderChar = '!', Color = "Red"  
- Có EnemyType (Zombie/Skeleton/Ghost), ScoreValue
- Attack(): damage theo EnemyType
- Khi OnCollision với Player → tự động attack
- GetClassName() => tên EnemyType

**NPC:**
- RenderChar = '?', Color = "Yellow"
- Có Dialogue, QuestName
- Attack(): NPC không attack (in "NPC không chiến đấu")
- Khi OnCollision với Player → hiển thị Dialogue
- GetClassName() => "NPC"

### 5. Collectible : GameObject

```
Collectible (concrete, KHÔNG phải Character):
- RenderChar = '*', Color = "Cyan"
- PointValue: điểm nhận được
- CollectibleType: Health/Coin/PowerUp
- Khi "collect" → tăng score hoặc heal player
```

### 6. Obstacle : GameObject, ICollidable

```
Obstacle (concrete):
- RenderChar = '#', Color = "Gray"
- DamageOnContact: sát thương khi chạm
- IsDestructible: có phá được không
- OnCollision: gây damage cho Character chạm phải
```

### 7. GameLoop

```csharp
class GameLoop
{
    private GameObject[] objects;  // Max 50
    private int objectCount;
    private int score;
    private int frame;

    public void AddGameObject(GameObject obj) { ... }

    public void Run(int totalFrames)
    {
        for (frame = 1; frame <= totalFrames; frame++)
        {
            Console.WriteLine($"\n══ Frame {frame}/{totalFrames} ══");

            // 1. Update all active objects
            UpdateAll();

            // 2. Move characters (simulate random movement)
            MoveCharacters();

            // 3. Check collisions giữa tất cả ICollidable
            CheckCollisions();

            // 4. Render all
            RenderAll();

            // 5. Show status
            ShowStatus();

            // 6. Remove dead enemies, collected items
            CleanUp();
        }

        ShowFinalScore();
    }

    private void CheckCollisions()
    {
        // So sánh từng cặp ICollidable
        // Nếu CollidesWith() == true → gọi OnCollision()
    }
}
```

---

## 🎮 Main Program

```csharp
static void Main()
{
    GameLoop game = new GameLoop();

    // Tạo Player
    Player player = new Player("Hero", 100, 20);

    // Tạo Enemies
    Enemy zombie = new Enemy("Zombie_1", "Zombie", 50, 10, 100);
    Enemy skeleton = new Enemy("Skeleton_1", "Skeleton", 40, 15, 150);
    Enemy ghost = new Enemy("Ghost_1", "Ghost", 30, 20, 200);

    // Tạo NPCs
    NPC merchant = new NPC("Merchant", "Chào! Mua đồ không?", "Mua sắm");
    NPC guide = new NPC("Guide", "Đi về phía Đông!", "Hướng dẫn");

    // Tạo Collectibles
    Collectible healthPack = new Collectible("Health Pack", "Health", 0);
    Collectible coin = new Collectible("Gold Coin", "Coin", 50);
    Collectible powerUp = new Collectible("Power Up", "PowerUp", 0);

    // Tạo Obstacles
    Obstacle wall = new Obstacle("Stone Wall", 0, false);
    Obstacle trap = new Obstacle("Spike Trap", 15, true);

    // Thêm vào game
    game.AddGameObject(player);
    game.AddGameObject(zombie);
    game.AddGameObject(skeleton);
    game.AddGameObject(ghost);
    game.AddGameObject(merchant);
    game.AddGameObject(guide);
    game.AddGameObject(healthPack);
    game.AddGameObject(coin);
    game.AddGameObject(powerUp);
    game.AddGameObject(wall);
    game.AddGameObject(trap);

    // Chạy 5 frames
    game.Run(5);
}
```

---

## 📊 Output mong đợi (sample)

```
╔══════════════════════════════════════════╗
║         🎮 GAME ENGINE FOUNDATION        ║
╚══════════════════════════════════════════╝

══ Frame 1/5 ══
  [Update] Hero moved to (2, 3)
  [Update] Zombie_1 moved to (5, 1)
  [Update] Skeleton_1 moved to (3, 4)

  [Collision] Hero ↔ Zombie_1!
    💥 Hero attacks Zombie_1 for 20 damage! HP: 30/50
    💥 Zombie_1 attacks Hero for 10 damage! HP: 90/100

  [Render]
    @ Hero [HP: 90/100] (2,3)
    ! Zombie_1 [HP: 30/50] (5,1)
    ! Skeleton_1 [HP: 40/40] (3,4)
    ? Merchant (0,0)
    * Gold Coin
    # Stone Wall

  [Status] Score: 0 | Lives: 3

══ Frame 2/5 ══
  ...
  [Collision] Hero ↔ Gold Coin!
    🪙 Hero nhận 50 điểm!

  [Collision] Hero ↔ Spike Trap!
    💥 Hero nhận 15 damage từ Spike Trap! HP: 75/100

  [Status] Score: 50 | Lives: 3

══ Frame 5/5 ══
  ...

═══ KẾT QUẢ ═══
  🏆 Score: 350
  ❤️ Lives: 2
  💀 Enemies defeated: 2/3
  ⭐ Items collected: 3/3
  📊 Frames played: 5
```

---

## ✅ Checklist hoàn thành

- [ ] 3 interfaces: IMovable, ICollidable, IRenderable
- [ ] abstract GameObject với Template Method ProcessFrame()
- [ ] abstract Character kế thừa GameObject + implement IMovable, ICollidable
- [ ] sealed override GetObjectType() ở Character
- [ ] 3 concrete characters: Player, Enemy, NPC
- [ ] Collectible (concrete, không phải Character)
- [ ] Obstacle : GameObject, ICollidable
- [ ] GameLoop với Run(), CheckCollisions(), RenderAll()
- [ ] Collision detection hoạt động
- [ ] Scoring system
- [ ] Polymorphism: `GameObject[]` chứa tất cả loại
- [ ] Interface checking: `is IMovable`, `is ICollidable`
- [ ] Code chạy được, output rõ ràng

---

## 💡 Gợi ý Collision Detection đơn giản

```csharp
// Bounding box collision — 2 hình chữ nhật overlap?
public bool CollidesWith(ICollidable other)
{
    if (this is IMovable m1 && other is IMovable m2)
    {
        // Đơn giản: cùng vị trí hoặc khoảng cách < 2
        double dx = m1.X - m2.X;
        double dy = m1.Y - m2.Y;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        return distance < 2.0;
    }
    return false;
}
```

---

## 🔑 Patterns sử dụng trong Challenge

| Pattern | Ở đâu |
|---------|--------|
| Template Method | `GameObject.ProcessFrame()`, `GameLoop.Run()` |
| Abstract + Interface | `Character : GameObject, IMovable, ICollidable` |
| Sealed Override | `Character.GetObjectType()` |
| Abstract Properties | `RenderChar`, `Color`, `GetClassName()` |
| Multi-level Abstract | `GameObject → Character → Player/Enemy` |
| Polymorphism | `GameObject[]` chứa tất cả, interface checking |
| Encapsulation | HP, Score chỉ thay đổi qua methods |
| Protected Abstract | `abstract void Attack(Character target)` |

---

*"Game engine = playground hoàn hảo để thực hành OOP. Mọi entity đều là object, mọi behavior đều là method."* 🎮
