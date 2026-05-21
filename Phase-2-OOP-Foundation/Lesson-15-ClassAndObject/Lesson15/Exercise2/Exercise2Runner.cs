using System;
using System.Linq;

namespace Lesson15.Exercise2
{
    public static class Exercise2Runner
    {
        public static void Run()
        {
            Console.WriteLine("--- Bài 2: Class Employee ---");

            Employee[] employees = new Employee[]
            {
                new Employee("Nguyen Van A", "IT", 25000000m, 22),
                new Employee("Tran Thi B", "HR", 12000000m, 20),
                new Employee("Le Van C", "IT", 18000000m, 24),
                new Employee("Pham Thi D", "Marketing", 15000000m, 21),
                new Employee("Hoang Van E", "IT", 30000000m, 22)
            };

            Console.WriteLine(new string('-', 112));
            Console.WriteLine($"| {"Họ và tên",-20} | {"Phòng ban",-15} | {"Lương cơ bản",15} | {"Ngày làm",8} | {"Tổng lương",15} | {"Thuế",13} | {"Thực lãnh",15} |");
            Console.WriteLine(new string('-', 112));

            foreach (var emp in employees)
            {
                emp.PrintPayslip();
            }
            Console.WriteLine(new string('-', 112));

            decimal totalNetSalary = employees.Sum(e => e.GetNetSalary());
            decimal averageNetSalary = employees.Average(e => e.GetNetSalary());

            var mostPopulatedDepartment = employees
                .GroupBy(e => e.Department)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;

            Console.WriteLine($"\n> Tổng lương thực lãnh phải trả: {totalNetSalary:N0} đ");
            Console.WriteLine($"> Lương trung bình: {averageNetSalary:N0} đ");
            Console.WriteLine($"> Phòng ban đông nhân viên nhất: {mostPopulatedDepartment}");
            Console.WriteLine();
        }
    }
}
