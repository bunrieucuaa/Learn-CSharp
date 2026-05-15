using System;

namespace Lesson_04
{
    class Program
    {
        static void Main(string[] args)
        {
            const string truePassword = "1234";
            bool isLoggin = true;
            int tryAttempts = 3;
            string passWordInput = "";
            Console.ResetColor();

            while (isLoggin)
            {
                //Try
                Console.WriteLine("====Chào mừng đến với ATM====");
                Console.WriteLine("Vui lòng nhập mã PIN (Mã pin bao gồm 4 ký tự số từ 0000 đến 9999)");

                string? inputPassword = Console.ReadLine();

                if (tryAttempts < 4)
                {
                    if (!string.IsNullOrEmpty(inputPassword) && !string.IsNullOrWhiteSpace(inputPassword))
                    {
                        passWordInput = inputPassword.Trim();
                        if (passWordInput == truePassword)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Nhập mã pin thành công !");
                            Console.ResetColor();
                            isLoggin = false;

                            int userChoices;
                            decimal initBalance = 10000000;
                            bool isActive = true;
                            decimal withdrawAmout = 0;
                            decimal depositAmout = 0;

                            while (isActive)
                            {
                                Console.WriteLine("╔══════════════════════════╗");
                                Console.WriteLine("║       🏧 ATM MENU        ║");
                                Console.WriteLine("╠══════════════════════════╣");
                                Console.WriteLine("║  1. Xem số dư            ║");
                                Console.WriteLine("║  2. Rút tiền             ║");
                                Console.WriteLine("║  3. Chuyển khoản         ║");
                                Console.WriteLine("║  4. Lịch sử giao dịch    ║");
                                Console.WriteLine("║  5. Đổi mã PIN           ║");
                                Console.WriteLine("║  0. Thoát                ║");
                                Console.WriteLine("╚══════════════════════════╝");

                                Console.WriteLine("Vui lòng chọn hành động...");
                                string? userChoice = Console.ReadLine();

                                if (int.TryParse(userChoice, out userChoices))
                                {
                                    switch (userChoices)
                                    {
                                        case 1:
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"Số dư hiện tại của bạn là: {initBalance:N0} VNĐ ");
                                            Console.ResetColor();
                                            break;

                                        case 2:
                                            Console.WriteLine("\n=== RÚT TIỀN ===");
                                            Console.WriteLine($"Số dư hiện tại: {initBalance:N0} VNĐ");

                                            while (true)
                                            {
                                                Console.Write("Nhập số tiền rút (bội số 50,000): ");
                                                string? withdrawMoney = Console.ReadLine();

                                                // Validate: phải là số
                                                if (!decimal.TryParse(withdrawMoney, out withdrawAmout))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Vui lòng nhập số hợp lệ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: tối thiểu 50,000
                                                if (withdrawAmout < 50000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền rút tối thiểu là 50,000 VNĐ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: tối đa 5,000,000
                                                if (withdrawAmout > 5000000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền rút tối đa mỗi lần là 5,000,000 VNĐ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: bội số 50,000
                                                if (withdrawAmout % 50000 != 0)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền phải là bội số của 50,000!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: không rút quá số dư
                                                if (withdrawAmout > initBalance)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số dư không đủ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: giữ lại tối thiểu 50,000 trong tài khoản
                                                if (initBalance - withdrawAmout < 50000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"❌ Phải giữ lại tối thiểu 50,000 VNĐ! Bạn chỉ có thể rút tối đa {initBalance - 50000:N0} VNĐ.");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Xác nhận y/n
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.Write($"\nXác nhận rút {withdrawAmout:N0} VNĐ? (y/n): ");
                                                Console.ResetColor();
                                                string? confirm = Console.ReadLine()?.Trim().ToLower();

                                                if (confirm == "y" || confirm == "yes")
                                                {
                                                    initBalance -= withdrawAmout;
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine($"\n✅ Rút tiền thành công!");
                                                    Console.WriteLine($"Số tiền rút:     {withdrawAmout:N0} VNĐ");
                                                    Console.WriteLine($"Số dư còn lại:   {initBalance:N0} VNĐ");
                                                    Console.ResetColor();
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("❌ Đã hủy giao dịch.");
                                                    Console.ResetColor();
                                                }
                                                break; // Thoát vòng while, quay lại menu
                                            }

                                            Console.WriteLine("\nNhấn Enter để quay lại menu...");
                                            Console.ReadLine();
                                            break;

                                        case 3:
                                            decimal account1Balance = 1000000;
                                            string account1 = "ACB123";
                                            string account2 = "BIDV123";
                                            decimal account2Balance = 2000000;
 
                                            Console.WriteLine("\n=== CHUYỂN KHOẢN ===");
                                            Console.WriteLine($"Số dư hiện tại: {initBalance:N0} VNĐ");


                                            while (true)
                                            {
                                                //Validate số tiền chuyển khoản
                                                Console.Write("Nhập số tiền chuyển khoản: ");
                                                string? depositMoney = Console.ReadLine();

                                                // Validate: phải là số
                                                if (!decimal.TryParse(depositMoney, out depositAmout))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Vui lòng nhập số hợp lệ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: tối thiểu 1,000 VNĐ
                                                if (depositAmout < 1000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền chuyển tối thiểu là 1,000 VNĐ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: tối đa 50,000,000
                                                if (depositAmout > 50000000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền chuyển khoản tối đa mỗi lần là 50,000,000 VNĐ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: bội số 1,000 VNĐ
                                                if (depositAmout % 1000 != 0)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số tiền phải là bội số của 1,000!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: không chuyển quá số dư
                                                if (depositAmout > initBalance)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("❌ Số dư không đủ!");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                // Validate: giữ lại tối thiểu 50,000 trong tài khoản
                                                if (initBalance - depositAmout < 50000)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"❌ Phải giữ lại tối thiểu 50,000 VNĐ! Bạn chỉ có thể chuyển tối đa {initBalance - 50000:N0} VNĐ.");
                                                    Console.ResetColor();
                                                    continue;
                                                }

                                                //Chọn ngân hàng (nội bộ hay bên ngoài)
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.Write("Phương thức giao dịch: A: Nội bộ, B: Bên ngoài");
                                                Console.ResetColor();
                                                string? userMethodChoice = Console.ReadLine()?.Trim().ToLower();

                                                if(userMethodChoice == "a" || userMethodChoice == "A")
                                                {
                                                    decimal onSiteFee = 0m;
                                                    Console.WriteLine("Nhập số tài khoản");
                                                    string accountNumber = Console.ReadLine()?.Trim();

                                                    if(string.IsNullOrEmpty(accountNumber) || string.IsNullOrWhiteSpace(accountNumber))
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("Tài khoản không được để trông hoặc có khoản trắng!");
                                                        Console.ResetColor();
                                                    } else
                                                    {
                                                        if (accountNumber.StartsWith("ACB"))
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.Write($"\nXác nhận chuyển tiền {depositAmout:N0} VNĐ? (y/n): ");
                                                            Console.ResetColor();
                                                            string? confirmTranfer = Console.ReadLine()?.Trim().ToLower();

                                                            if (confirmTranfer == "y" || confirmTranfer == "yes")
                                                            {
                                                                account1Balance += depositAmout;
                                                                initBalance -= depositAmout;
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                Console.WriteLine($"\n✅ Chuyển tiền thành công!");
                                                                Console.WriteLine($"Số tiền đã chuyển:     {depositAmout:N0} VNĐ");
                                                                Console.WriteLine($"Số dư còn lại:   {initBalance:N0} VNĐ");
                                                                Console.WriteLine($"Số tiền cho người 1:   {account1Balance:N0} VNĐ");
                                                                Console.ResetColor();
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("❌ Đã hủy giao dịch.");
                                                                Console.ResetColor();
                                                            }
                                                        }
                                                    }


                                                }
                                            }
                                            break;

                                        case 4:
                                            break;

                                        case 5:
                                            break;


                                        default:
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Thoát chương trình");
                                            Console.ResetColor();
                                            isActive = false;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"❌ Sai lựa chọn, vui lòng chọn số trên màn hình!");
                                    Console.ResetColor();
                                }
                            }
                            isActive = false;

                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            --tryAttempts;
                            Console.WriteLine($"❌ Sai mã pin, vui lòng thử lại, còn lại {tryAttempts} lần thử");
                            Console.ResetColor();
                        }
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        --tryAttempts;
                        Console.WriteLine($"❌ Sai mã pin, vui lòng thử lại, còn lại {tryAttempts} lần thử");
                        Console.ResetColor();
                    }
                }

                if (tryAttempts == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nhập sai quá 3 lần, vui lòng chờ thêm giây lát");
                    Console.ResetColor();
                    isLoggin = false;
                    Console.WriteLine("Đóng ứng dụng....");
                }
            }
        }
    }
}


