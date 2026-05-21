# ✏️ Lesson 16 — Bài tập: Constructor

---

## Bài 1: Constructor cơ bản + Overloading
Tạo class `Movie` với 3 constructor: đầy đủ (title, genre, duration, rating), không rating (default 0), chỉ title. Dùng chaining `this(...)`. Tạo 5 phim, in bảng.

## Bài 2: Validation trong Constructor
Tạo class `CreditCard` — validate: number 16 số, expiry > hôm nay, CVV 3 số, holder không rỗng. Throw exception nếu sai. Test đúng + sai.

## Bài 3: Static + Constructor — Auto ID
Tạo class `Patient` — auto-generate ID (BN001...), tính tuổi từ năm sinh. Tạo 5 bệnh nhân, in danh sách, tìm lớn tuổi nhất.

## Bài 4: Constructor + Methods
Tạo class `Circle` (bán kính, validate > 0) với methods: `Area()`, `Circumference()`, `Scale(factor)`, `IsLargerThan(Circle other)`. So sánh 3 hình tròn.

## Bài 5: Object Initializer vs Constructor
Tạo class `Config` (10 fields optional) dùng Object Initializer VÀ class `Connection` (3 fields bắt buộc) dùng Constructor. Giải thích khi nào dùng cái nào.

---

# 🏆 Challenge: Hệ thống tuyển sinh 🎓

Tạo class `Applicant` (auto ID, tên, năm sinh, 3 điểm thi, validate) + class `AdmissionResult` (applicant, tổng điểm, đậu/rớt, nguyện vọng). Constructor validate tất cả. Nhập 5 thí sinh, tính tổng, xếp hạng, in kết quả, thống kê.

| Tiêu chí | Mức độ |
|----------|--------|
| Constructor + validation | ⭐⭐⭐ |
| Auto ID + chaining | ⭐⭐⭐ |
| Xếp hạng + thống kê | ⭐⭐ |
