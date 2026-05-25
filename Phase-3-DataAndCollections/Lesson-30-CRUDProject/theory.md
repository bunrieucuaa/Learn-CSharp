# 📘 Bài 30: CRUD Console Project — Tổng hợp Phase 3

> **"Tổng hợp tất cả kiến thức từ Phase 1-3: OOP, Collections, Generics, IEnumerable — trong một dự án CRUD hoàn chỉnh."**

---

## 📋 Mục lục

1. [Mục tiêu dự án](#1-mục-tiêu-dự-án)
2. [Kiến trúc 3 tầng: Model → Repository → UI](#2-kiến-trúc-3-tầng-model--repository--ui)
3. [Sử dụng kiến thức đã học](#3-sử-dụng-kiến-thức-đã-học)
4. [Clean Code principles](#4-clean-code-principles)
5. [Project structure guide](#5-project-structure-guide)

---

## 1. Mục tiêu dự án

> 📌 Xây dựng **Console Application CRUD hoàn chỉnh** — quản lý sinh viên — theo kiến trúc tầng.

### CRUD là gì?

```
╔═══════════════════════════════════════════════════╗
║ C — Create    → Thêm mới dữ liệu                 ║
║ R — Read      → Đọc / Hiển thị dữ liệu            ║
║ U — Update    → Cập nhật dữ liệu                  ║
║ D — Delete    → Xóa dữ liệu                       ║
╚═══════════════════════════════════════════════════╝

4 thao tác CƠ BẢN nhất của mọi ứng dụng quản lý dữ liệu.
Từ app console → web → mobile → tất cả đều xoay quanh CRUD!
```

### Tại sao CRUD Project quan trọng?

```
1. Tổng hợp TẤT CẢ kiến thức đã học
2. Mô phỏng kiến trúc THỰC TẾ (3-layer)
3. Luyện tập OOP trong bối cảnh thực
4. Nền tảng cho Phase 4 (LINQ) và Phase 5 (File, DI)
5. Chuẩn bị mindset cho ASP.NET Core (CRUD web API)
```

---

## 2. Kiến trúc 3 tầng: Model → Repository → UI

### 📊 Tổng quan kiến trúc

```
┌─────────────────────────────────────────────────────────────┐
│                    CONSOLE APPLICATION                       │
│                                                             │
│  ┌─────────────────────────────────────────────────────┐   │
│  │  TẦNG 1: MODEL (Data)                               │   │
│  │  ┌──────────┐ ┌──────────┐                          │   │
│  │  │ Student  │ │ IEntity  │  ← Class + Interface     │   │
│  │  └──────────┘ └──────────┘                          │   │
│  └───────────────────────┬─────────────────────────────┘   │
│                          │ sử dụng                          │
│  ┌───────────────────────▼─────────────────────────────┐   │
│  │  TẦNG 2: REPOSITORY (Data Access)                   │   │
│  │  ┌──────────────┐ ┌────────────────────┐            │   │
│  │  │ IRepository  │ │ GenericRepository  │            │   │
│  │  │    <T>       │ │      <T>           │            │   │
│  │  └──────────────┘ └────────────────────┘            │   │
│  └───────────────────────┬─────────────────────────────┘   │
│                          │ sử dụng                          │
│  ┌───────────────────────▼─────────────────────────────┐   │
│  │  TẦNG 3: SERVICE + UI (Business Logic + Display)    │   │
│  │  ┌────────────────┐ ┌───────────────┐               │   │
│  │  │ StudentService │ │ ConsoleUI     │               │   │
│  │  └────────────────┘ └───────────────┘               │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
│  ┌─────────────────────────────────────────────────────┐   │
│  │  Program.cs — Composition Root (khởi tạo + kết nối) │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

### Trách nhiệm từng tầng

```
┌──────────────┬──────────────────────────────────────────────┐
│ Tầng         │ Trách nhiệm                                  │
├──────────────┼──────────────────────────────────────────────┤
│ Model        │ - Định nghĩa class dữ liệu (Student)         │
│              │ - Validation (kiểm tra dữ liệu hợp lệ)       │
│              │ - IComparable, ToString()                     │
│              │ - KHÔNG biết Repository hay UI                │
├──────────────┼──────────────────────────────────────────────┤
│ Repository   │ - Lưu trữ và truy xuất dữ liệu              │
│              │ - CRUD operations (Add, Get, Update, Delete) │
│              │ - Dùng List<T>, Dictionary cho fast lookup    │
│              │ - KHÔNG biết UI hay business rules            │
├──────────────┼──────────────────────────────────────────────┤
│ Service      │ - Business logic (validate, compute, format) │
│              │ - Dùng Repository để truy cập data           │
│              │ - KHÔNG biết UI                              │
├──────────────┼──────────────────────────────────────────────┤
│ UI (Console) │ - Hiển thị menu, nhận input                  │
│              │ - Gọi Service để xử lý                       │
│              │ - KHÔNG biết cách lưu dữ liệu               │
├──────────────┼──────────────────────────────────────────────┤
│ Program.cs   │ - Tạo instance cho các tầng                  │
│              │ - Kết nối (wiring) các tầng với nhau         │
│              │ - Entry point                                │
└──────────────┴──────────────────────────────────────────────┘
```

### Luồng dữ liệu (Data Flow)

```
User Input → ConsoleUI → StudentService → Repository → List<Student>
                ↑                                           │
                └───────────── Kết quả ←────────────────────┘

Ví dụ "Thêm sinh viên":
1. User nhập: tên, tuổi, điểm
2. ConsoleUI → gọi service.AddStudent(name, age, score)
3. StudentService → validate data → gọi repo.Add(student)
4. Repository → thêm vào List<Student> → trả về true/false
5. Service → trả kết quả cho UI
6. ConsoleUI → hiển thị "Thêm thành công!"
```

---

## 3. Sử dụng kiến thức đã học

```
╔═══════════════════════════════════════════════════════════╗
║  KIẾN THỨC           │  DÙNG Ở ĐÂU                       ║
╠═══════════════════════╪═══════════════════════════════════╣
║  Phase 1:             │                                   ║
║  Variables, Types     │  Properties trong Model           ║
║  Methods              │  Service methods, UI methods      ║
║  Loops, Conditions    │  Menu loop, validation            ║
║  Exception Handling   │  try-catch input, business rules  ║
║  Arrays, Strings      │  Data processing                  ║
╠═══════════════════════╪═══════════════════════════════════╣
║  Phase 2:             │                                   ║
║  Class, Constructor   │  Model classes                    ║
║  Encapsulation        │  Private fields + Public props    ║
║  Inheritance          │  Base entity class                ║
║  Polymorphism         │  Generic repository               ║
║  Interface            │  IRepository<T>, IEntity          ║
║  Abstract Class       │  Có thể dùng cho base repository  ║
╠═══════════════════════╪═══════════════════════════════════╣
║  Phase 3:             │                                   ║
║  List<T>              │  Main storage trong Repository    ║
║  Dictionary<K,V>      │  Fast lookup by ID                ║
║  Generics             │  GenericRepository<T>             ║
║  IEnumerable<T>       │  Query methods, iteration         ║
╚═══════════════════════╧═══════════════════════════════════╝
```

---

## 4. Clean Code principles

### Naming Convention

```
// ✅ PascalCase cho public members
public string StudentName { get; set; }
public void AddStudent(Student student) { }
public int GetTotalCount() { }

// ✅ camelCase cho local variables, parameters
int studentCount = 0;
string inputName = Console.ReadLine();

// ✅ _camelCase cho private fields
private List<Student> _students;
private readonly IRepository<Student> _repository;

// ✅ I-prefix cho interfaces
interface IRepository<T> { }
interface IEntity { }
```

### Single Responsibility

```
❌ SAI: 1 class làm TẤT CẢ
class StudentManager
{
    void AddStudent() { /* nhập input + validate + lưu + in */ }
}

✅ ĐÚNG: Mỗi class 1 trách nhiệm
Model      → Chỉ chứa data + validation
Repository → Chỉ lưu/đọc data
Service    → Chỉ xử lý business logic
UI         → Chỉ nhập/xuất
```

### Separation of Concerns

```
Repository KHÔNG Console.WriteLine()     ← Đúng!
UI KHÔNG truy cập List<Student> trực tiếp ← Đúng!
Service KHÔNG Console.ReadLine()         ← Đúng!
Model KHÔNG biết Repository              ← Đúng!
```

---

## 5. Project structure guide

### File structure

```
StudentManagement/
├── Models/
│   ├── IEntity.cs            → Interface cho entity base
│   └── Student.cs            → Student class
│
├── Repositories/
│   ├── IRepository.cs        → Generic repository interface
│   └── GenericRepository.cs  → Implementation với List<T>
│
├── Services/
│   └── StudentService.cs     → Business logic
│
├── UI/
│   └── ConsoleUI.cs          → Menu + display
│
└── Program.cs                → Entry point + wiring
```

### Workflow phát triển

```
Bước 1: Tạo Model (Student, IEntity)
    ↓
Bước 2: Tạo Repository (IRepository<T>, GenericRepository<T>)
    ↓
Bước 3: Tạo Service (StudentService)
    ↓
Bước 4: Tạo UI (ConsoleUI)
    ↓
Bước 5: Wiring trong Program.cs
    ↓
Bước 6: Test từng chức năng
    ↓
Bước 7: Polish (error handling, UX)
```

---

> **Tiếp theo:** Xem file `examples.md` để thấy code hoàn chỉnh! 🚀

---

*"Good architecture separates concerns. Great architecture makes them easy to change."* 🎯
