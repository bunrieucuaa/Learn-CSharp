# Thách thức Bài 43: Thiết kế Dịch vụ Ngân hàng cốt lõi (Core Banking Service)

## Ngữ cảnh
Bạn được giao trách nhiệm phát triển module dịch vụ quản lý tài khoản ngân hàng cốt lõi (Core Banking System) cho một ngân hàng số. Đây là phần mềm đòi hỏi sự an toàn và kiểm tra bảo mật dữ liệu ở mức độ tuyệt đối. 

Bạn cần xây dựng một **Dịch vụ Ngân hàng (`BankService`)** chịu trách nhiệm xử lý các giao dịch nạp tiền, rút tiền và chuyển khoản giữa các tài khoản khách hàng, đảm bảo ghi lại nhật ký giao dịch (Auditing) cho mỗi hành động thành công.

## Cấu trúc dữ liệu có sẵn
```csharp
using System;
using System.Collections.Generic;

public class Account
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; set; }
    public bool IsLocked { get; set; }
}

public class AuditLog
{
    public int Id { get; set; }
    public string Action { get; set; } // "DEPOSIT", "WITHDRAW", "TRANSFER"
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
}
```

## Các Interface cần tương tác (Tầng Data Access)
```csharp
public interface IAccountRepository
{
    Account GetByNumber(string accountNumber);
    void Update(Account account);
}

public interface IAuditLogRepository
{
    void SaveLog(AuditLog log);
}
```

## Yêu cầu Thách thức
Hãy xây dựng interface `IBankService` và class triển khai `BankService` (nhận 2 repository trên qua Constructor Injection) hiện thực hóa các quy tắc nghiệp vụ sau:

1. **Nạp tiền (`void Deposit(string accNumber, decimal amount)`):**
   - Số tiền nạp phải lớn hơn 0 (ngược lại ném `ArgumentException`).
   - Tài khoản phải tồn tại (ngược lại ném `AccountNotFoundException`).
   - Tài khoản không được đang bị khóa (`IsLocked == false`, ngược lại ném `AccountLockedException`).
   - Tăng số dư tài khoản, gọi repository lưu cập nhật, đồng thời lưu 1 bản ghi `AuditLog` ghi nhận chi tiết số tiền nạp.
2. **Rút tiền (`void Withdraw(string accNumber, decimal amount)`):**
   - Kiểm tra số tiền hợp lệ (> 0).
   - Kiểm tra tài khoản tồn tại và không bị khóa.
   - Số dư tài khoản phải lớn hơn hoặc bằng số tiền rút (ngược lại ném `InsufficientFundsException`).
   - Giảm số dư, lưu cập nhật DB, ghi nhận `AuditLog`.
3. **Chuyển khoản (`void Transfer(string fromAccNumber, string toAccNumber, decimal amount)`):**
   - Kiểm tra số tiền hợp lệ (> 0).
   - Kiểm tra cả 2 tài khoản gửi và nhận đều phải tồn tại và không bị khóa.
   - Tài khoản gửi phải đủ số dư thực hiện giao dịch.
   - Trừ tiền tài khoản gửi, cộng tiền tài khoản nhận.
   - Lưu cập nhật cả 2 tài khoản, ghi nhận `AuditLog` chi tiết: `"Chuyển $amount từ acc A sang acc B"`.

Hãy viết đầy đủ code của Service, các Custom Exception liên quan, và thiết kế kịch bản test thử nghiệm trong `Program.cs` bằng cách tạo Mock data cho 2 tài khoản ngân hàng để tự chứng minh tính bảo mật và đúng đắn của logic nghiệp vụ.
