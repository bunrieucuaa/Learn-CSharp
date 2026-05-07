using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BE_2024
{
    class Program
    {

        public enum LoaiPhepTinh
        {
            Cong,
            Tru
        }

        public static int CongTruHaiSo(LoaiPhepTinh loaiPhepTinh, int soA, int soB)
        {
            switch (loaiPhepTinh)
            {
                case LoaiPhepTinh.Cong:
                    return soA + soB;
                case LoaiPhepTinh.Tru:
                    return soA - soB;
                default:
                    throw new Exception("Không hợp lệ");
            }
        }

        public static double PhuongTrinhBacNhat(double soA, double soB)
        {
            if (soA == 0)
            {
                if (soB == 0)
                {
                    Console.WriteLine("Phương trình vô số nghiệm");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Phương trình vô nghiệm");
                    return 0;
                }
            }

            return -soB / soA;
        }

        public static void PhuongTrinhBacHai(double soA, double soB, double soC)
        {
            double delta = (soB * soB) - (4 * soA * soC);

            if (soA == 0)
            {
                Console.WriteLine("Đây không phải phương trình bậc 2");
                return;
            } else
            {
                if (delta < 0)
                {
                    Console.WriteLine("Phương trình vô nghiệm");
                }
                else if (delta == 0)
                {
                    Console.WriteLine("Phương trình có nghiệm kép");

                    double nghiem = -soB / (2 * soA);

                    Console.WriteLine($"Nghiệm kép x = {nghiem}");
                }
                else
                {
                    Console.WriteLine("Phương trình có 2 nghiệm phân biệt");

                    double nghiemSo1 = (-soB + Math.Sqrt(delta)) / (2 * soA);
                    double nghiemSo2 = (-soB - Math.Sqrt(delta)) / (2 * soA);

                    Console.WriteLine($"Nghiệm số 1: {nghiemSo1}");
                    Console.WriteLine($"Nghiệm số 2: {nghiemSo2}");
                }
            }
        }


        // Hàm nhập số nguyên với validation
        public static int NhapSoNguyen(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("❌ Vui lòng nhập số nguyên hợp lệ!");
                }
            }
        }

        // Hàm nhập số thực với validation
        public static double NhapSoThuc(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("❌ Vui lòng nhập số thực hợp lệ (ví dụ: 3.14 hoặc -5)!");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Hỗ trợ tiếng Việt

            bool isRunning = true;

            do
            {
                Console.WriteLine("Chào mừng đến với chương trình tính toán phương trình bậc 1 và 2");
                Console.WriteLine("Vui lòng chọn chương trình tính toán");
                Console.WriteLine("================================================================");
                Console.WriteLine("1: Phương trình bậc 1");
                Console.WriteLine("2: Phương trình bậc 2");
                Console.WriteLine("3: Thoát chương trình tính toán");

                // Get choice với validation
                int userChoice = NhapSoNguyen("Nhập lựa chọn của bạn (1-3): ");

                if (userChoice < 1 || userChoice > 3)
                {
                    Console.WriteLine("❌ Vui lòng chọn số từ 1 đến 3\n");
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("\n📐 Chương trình tính toán phương trình bậc nhất dạng ax + b = 0");
                        Console.WriteLine("================================");

                        double soA = NhapSoThuc("Nhập số a: ");
                        double soB = NhapSoThuc("Nhập số b: ");

                        var ketQua = PhuongTrinhBacNhat(soA, soB);
                        Console.WriteLine($"✅ Kết quả của phép tính là: {ketQua}\n");

                        break;
                    case 2:
                        Console.WriteLine("\n📐 Chương trình tính toán phương trình bậc 2 dạng ax² + bx + c = 0");
                        Console.WriteLine("================================");

                        double soA1 = NhapSoThuc("Nhập số a: ");
                        double soB1 = NhapSoThuc("Nhập số b: ");
                        double soC1 = NhapSoThuc("Nhập số c: ");

                        PhuongTrinhBacHai(soA1, soB1, soC1);
                        Console.WriteLine();

                        break;
                    case 3:
                        Console.WriteLine("👋 Good bye, see u next time");
                        Console.WriteLine("==========================================");
                        isRunning = false;
                        break;
                }
            }
            while (isRunning);
        }
    }
}
