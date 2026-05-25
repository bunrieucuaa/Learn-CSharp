# Thách thức Bài 34: Chuẩn hóa Dữ liệu JSON Nhập khẩu từ Hệ thống Khác

## Ngữ cảnh
Công ty bạn đang tiến hành tích hợp dữ liệu khách hàng từ một đối tác ngoại bang. Phía đối tác cung cấp dữ liệu thô dưới dạng một cấu trúc phức tạp: Mỗi khách hàng (Customer) có thể có nhiều chi nhánh (Branches), và mỗi chi nhánh lại quản lý một danh sách các liên hệ (Contacts).

Phòng Marketing yêu cầu bạn trích xuất danh sách tất cả các Email của các liên hệ thuộc toàn bộ các chi nhánh, để gửi chiến dịch email chúc mừng năm mới. Dữ liệu đầu ra phải được làm phẳng và loại bỏ tất cả email không hợp lệ (không chứa ký tự `@` hoặc bị trùng).

## Cấu trúc dữ liệu Đối tác cung cấp
```csharp
public class PartnerContact
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}

public class PartnerBranch
{
    public string BranchName { get; set; }
    public List<PartnerContact> Contacts { get; set; }
}

public class PartnerCustomer
{
    public string CompanyName { get; set; }
    public List<PartnerBranch> Branches { get; set; }
}
```

## Dữ liệu mock-up đầu vào
```csharp
List<PartnerCustomer> rawPartners = [
    new PartnerCustomer {
        CompanyName = "TechCorp",
        Branches = [
            new PartnerBranch {
                BranchName = "HN Branch",
                Contacts = [
                    new PartnerContact { Name = "An", Email = "an@techcorp.com", IsActive = true },
                    new PartnerContact { Name = "Bảo", Email = "bad_email_format", IsActive = true } // Lỗi email
                ]
            },
            new PartnerBranch {
                BranchName = "HCM Branch",
                Contacts = [
                    new PartnerContact { Name = "Cường", Email = "cuong@techcorp.com", IsActive = false }, // Không hoạt động
                    new PartnerContact { Name = "An", Email = "an@techcorp.com", IsActive = true } // Trùng lặp
                ]
            }
        ]
    },
    new PartnerCustomer {
        CompanyName = "SoftInc",
        Branches = [
            new PartnerBranch {
                BranchName = "Da Nang Branch",
                Contacts = [
                    new PartnerContact { Name = "Dung", Email = "dung@softinc.com", IsActive = true }
                ]
            }
        ]
    }
];
```

## Yêu cầu Thách thức
Hãy viết một câu truy vấn LINQ duy nhất (sử dụng Method Syntax chain) xử lý danh sách `rawPartners` trên để trả về một `List<string>` chứa danh sách các email của khách hàng thỏa mãn:
1. Liên hệ đó phải đang hoạt động (`IsActive == true`).
2. Email phải hợp lệ (chứa ký tự `'@'`).
3. Danh sách email đầu ra phải được làm phẳng (không lồng nhau) và không được trùng lặp (Distinct).

## Expected Output
Hàm in ra màn hình Console danh sách các email kết quả:
```text
an@techcorp.com
dung@softinc.com
```
*(Hãy tự giải thích tại sao email của Bảo và Cường bị loại bỏ).*
