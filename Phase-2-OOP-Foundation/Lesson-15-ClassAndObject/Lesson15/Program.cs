using System;
using System.Text;
using Lesson15.Exercise1;
using Lesson15.Exercise2;
using Lesson15.Exercise3;
using Lesson15.Exercise4;
using Lesson15.Exercise5;

namespace Lesson15
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Thiết lập Console hiển thị tiếng Việt
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("======================================");
                Console.WriteLine("        MENU BÀI TẬP LESSON 15        ");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Bài 1: Class Book");
                Console.WriteLine("2. Bài 2: Class Employee");
                Console.WriteLine("3. Bài 3: Class Rectangle");
                Console.WriteLine("4. Bài 4: Chuyển đổi mảng song song");
                Console.WriteLine("5. Bài 5: Order System");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("======================================");
                Console.Write("Chọn bài tập (0-5): ");

                string choice = Console.ReadLine() ?? "";

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Exercise1Runner.Run();
                        break;
                    case "2":
                        Exercise2Runner.Run();
                        break;
                    case "3":
                        Exercise3Runner.Run();
                        break;
                    case "4":
                        Exercise4Runner.Run();
                        break;
                    case "5":
                        Exercise5Runner.Run();
                        break;
                    case "0":
                        Console.WriteLine("Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}