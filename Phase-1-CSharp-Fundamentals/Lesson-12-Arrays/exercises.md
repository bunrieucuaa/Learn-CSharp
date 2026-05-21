# ✏️ Lesson 12 — Bài tập: Arrays

---

## Bài 1: Thao tác cơ bản 🔢

### Yêu cầu:
Nhập N số nguyên vào mảng, thực hiện:
- In mảng
- Tìm min, max
- Tính tổng, trung bình
- Đếm số chẵn / lẻ / dương / âm
- Tìm phần tử lớn thứ 2
- Đảo ngược mảng (không dùng `Array.Reverse`)

### Expected Output:
```
Nhập N: 6
Nhập phần tử: 5 -3 8 1 8 -1

Mảng: [5, -3, 8, 1, 8, -1]
Min: -3     Max: 8
Tổng: 18    TB: 3.00
Chẵn: 2     Lẻ: 4
Dương: 4    Âm: 2
Lớn thứ 2: 5
Đảo: [-1, 8, 1, 8, -3, 5]
```

---

## Bài 2: Sắp xếp thủ công 🔄

### Yêu cầu:
Tự viết 2 thuật toán sắp xếp (KHÔNG dùng `Array.Sort`):

**A. Bubble Sort** — hiện quá trình từng bước:
```
Bước 1: [64, 25, 12, 22, 11] → [25, 12, 22, 11, 64]  (1 swap)
Bước 2: [25, 12, 22, 11, 64] → [12, 22, 11, 25, 64]
...
```

**B. Selection Sort** — hiện quá trình chọn min:
```
Bước 1: min=11 (idx 4) → swap [0]↔[4] → [11, 25, 12, 22, 64]
Bước 2: min=12 (idx 2) → swap [1]↔[2] → [11, 12, 25, 22, 64]
...
```

So sánh số bước và số lần swap của 2 thuật toán.

---

## Bài 3: Mảng 2D — Ma trận 📐

### Yêu cầu:
Nhập ma trận M×N, thực hiện:
- In ma trận đẹp (alignment)
- Tính tổng từng hàng, từng cột
- Tìm phần tử lớn nhất + vị trí
- Tính tổng đường chéo chính (nếu vuông)
- Xoay ma trận 90° theo chiều kim đồng hồ

### Expected Output:
```
Ma trận 3×3:
  1   2   3  │ Σ=6
  4   5   6  │ Σ=15
  7   8   9  │ Σ=24
──────────────
  12  15  18   Chéo: 15

Xoay 90°:
  7   4   1
  8   5   2
  9   6   3
```

---

## Bài 4: Loại bỏ trùng lặp ♻️

### Yêu cầu:
Nhập mảng N số, tạo mảng mới **không có phần tử trùng** (KHÔNG dùng thư viện):

```csharp
Input:  [3, 1, 4, 1, 5, 9, 2, 6, 5, 3]
Output: [3, 1, 4, 5, 9, 2, 6]
```

**Bonus:** Đếm số lần xuất hiện của mỗi phần tử:
```
3 → 2 lần
1 → 2 lần
4 → 1 lần
5 → 2 lần
...
```

### Gợi ý:
- Tạo mảng kết quả, duyệt input, kiểm tra từng phần tử đã có trong kết quả chưa
- Dùng method `Contains(arr, count, value)` tự viết

---

## Bài 5: Game Minesweeper mini 💣

### Yêu cầu:
Tạo bảng 5×5, đặt ngẫu nhiên 5 quả bom. User chọn ô:
- Trúng bom → thua
- An toàn → hiện số bom xung quanh (0-8)
- Mở hết ô an toàn → thắng

### Gợi ý:
- `bool[,] mines` — vị trí bom
- `bool[,] revealed` — ô đã mở
- `int CountMines(int row, int col)` — đếm bom 8 ô xung quanh
- Kiểm tra bounds: `row >= 0 && row < 5 && col >= 0 && col < 5`

### Expected Output:
```
  1 2 3 4 5
1 ■ ■ ■ ■ ■
2 ■ ■ ■ ■ ■
3 ■ ■ ■ ■ ■
4 ■ ■ ■ ■ ■
5 ■ ■ ■ ■ ■

Chọn ô (hàng cột): 3 3

  1 2 3 4 5
1 ■ ■ ■ ■ ■
2 ■ ■ 1 ■ ■
3 ■ 1 0 1 ■
4 ■ ■ 1 ■ ■
5 ■ ■ ■ ■ ■
```

---

## Bài 6: Histogram — Biểu đồ tần suất 📊

### Yêu cầu:
Nhập điểm của N học sinh (0-10), vẽ biểu đồ ngang bằng ký tự:

```
Nhập điểm: 8 7 9 5 8 10 7 6 8 7 9 8

Biểu đồ điểm:
 5 │ █
 6 │ █
 7 │ ███
 8 │ ████
 9 │ ██
10 │ █
───┴──────────
     Số lượng
```
