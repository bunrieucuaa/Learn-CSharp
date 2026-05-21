# ✏️ Lesson 10 — Bài tập: String

---

## Bài 1: Đếm & Phân tích chuỗi 📊

### Yêu cầu:
Nhập một câu, phân tích:
- Số ký tự (có/không khoảng trắng)
- Số từ
- Số câu (dấu `.`, `!`, `?`)
- Tần suất mỗi chữ cái (không phân biệt hoa/thường)
- Từ xuất hiện nhiều nhất

### Expected Output:
```
Nhập: Hello World. Hello C#!

Ký tự (có KT): 22
Ký tự (không KT): 19
Số từ: 4
Số câu: 2

Tần suất chữ cái:
  h: 2    e: 1    l: 4    o: 2
  w: 1    r: 1    d: 1    c: 1

Từ nhiều nhất: "hello" (2 lần)
```

---

## Bài 2: Validate & Format dữ liệu ✅

### Yêu cầu:
Viết các method validate:

```csharp
static bool IsValidEmail(string email);     // chứa @ và . sau @
static bool IsValidPhone(string phone);     // 10 số, bắt đầu bằng 0
static bool IsValidPassword(string pass);   // ≥8 ký tự, có hoa+thường+số
static string FormatPhone(string phone);    // "0912345678" → "091-234-5678"
static string FormatName(string name);      // "  nguyễn vĂn a " → "Nguyễn Văn A"
static string MaskEmail(string email);      // "user@mail.com" → "u***@mail.com"
static string MaskCardNumber(string card);  // "1234567890123456" → "****-****-****-3456"
```

Test với nhiều input (đúng/sai).

---

## Bài 3: StringBuilder — Tạo báo cáo 📋

### Yêu cầu:
Dùng `StringBuilder` để tạo báo cáo bán hàng từ data hardcode:

```
═══════════════════════════════════════════
          BÁO CÁO BÁN HÀNG THÁNG 5/2026
═══════════════════════════════════════════

STT │ Sản phẩm         │ SL  │ Đơn giá     │ Thành tiền
────┼───────────────────┼─────┼─────────────┼──────────────
  1 │ iPhone 15 Pro     │   5 │  28,990,000 │ 144,950,000
  2 │ AirPods Pro 2     │  12 │   5,990,000 │  71,880,000
  ...
════╧═══════════════════╧═════╧═════════════╧══════════════
         Tổng doanh thu:               XXX,XXX,XXX VNĐ
         Thuế VAT (10%):                XX,XXX,XXX VNĐ
         Tổng sau thuế:               XXX,XXX,XXX VNĐ
═══════════════════════════════════════════════════════════
```

Phải dùng `StringBuilder`, KHÔNG dùng `Console.WriteLine` trực tiếp (xây toàn bộ báo cáo rồi in 1 lần).

---

## Bài 4: Xử lý text — Tìm và thay thế nâng cao 🔍

### Yêu cầu:
Viết các method:

```csharp
// Đếm số lần xuất hiện của substring
static int CountOccurrences(string text, string word);

// Highlight từ (bọc bằng [])
static string HighlightWord(string text, string word);
// "Hello World Hello" + "Hello" → "[Hello] World [Hello]"

// Censored (che từ cấm)
static string CensorBadWords(string text, string[] badWords);
// "This is bad and ugly" + ["bad","ugly"] → "This is *** and ****"

// Tách CamelCase thành từ
static string SplitCamelCase(string text);
// "helloWorldFooBar" → "hello World Foo Bar"

// Truncate (cắt ngắn + "...")
static string Truncate(string text, int maxLength);
// "Hello World Programming" + 10 → "Hello W..."
```

---

## Bài 5: Mã hóa & Giải mã nâng cao 🔐

### Yêu cầu:
Nâng cấp Caesar cipher:

**A. Vigenère Cipher:** Dùng keyword thay vì 1 số:
```
Plaintext:  HELLO WORLD
Key:        KEYKE YKEYK
Encrypted:  RIJVS UYVJN
```
Mỗi ký tự dịch theo ký tự tương ứng trong key.

**B. Mã hóa Atbash:** Đảo bảng chữ cái:
```
A→Z, B→Y, C→X, ..., Z→A
"HELLO" → "SVOOL"
```

**C. ROT13:** Caesar shift = 13 (mã hóa = giải mã cùng 1 hàm!)

---

## Bài 6: Trò chơi Wordle 🟩🟨⬜

### Yêu cầu:
Mô phỏng game Wordle:
1. Máy chọn 1 từ 5 chữ cái (từ danh sách hardcode)
2. User đoán 6 lần
3. Mỗi lần đoán, hiển thị feedback:
   - 🟩 = đúng vị trí
   - 🟨 = có trong từ nhưng sai vị trí
   - ⬜ = không có trong từ

### Expected Output:
```
🟩🟨⬜ WORDLE — Đoán từ 5 chữ cái! 🟩🟨⬜

Lần 1: HELLO
  H E L L O
  🟨⬜🟩⬜⬜

Lần 2: PLAID
  P L A I D
  ⬜🟩🟨⬜⬜

Lần 3: PLANT
  P L A N T
  🟩🟩🟩🟩🟩

🎉 CHÍNH XÁC! Từ là: PLANT
Bạn đoán trong 3/6 lần!
```

### Gợi ý:
- Dùng `char[]` để track trạng thái từng ký tự
- Dùng `string.Contains(char)` cho 🟨
- So sánh `secretWord[i] == guess[i]` cho 🟩
- `ToUpper()` cho input
