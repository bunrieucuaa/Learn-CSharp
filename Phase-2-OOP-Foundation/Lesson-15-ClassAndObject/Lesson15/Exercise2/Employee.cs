using System;

namespace Lesson15.Exercise2
{
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal BaseSalary { get; set; }
        public int WorkDays { get; set; }

        public Employee(string name, string department, decimal baseSalary, int workDays)
        {
            Name = name;
            Department = department;
            BaseSalary = baseSalary;
            WorkDays = workDays;
        }

        public decimal CalculateSalary()
        {
            return BaseSalary * WorkDays / 22m;
        }

        public decimal GetTax()
        {
            decimal salary = CalculateSalary();
            if (salary > 20000000m)
            {
                return salary * 0.15m;
            }
            else if (salary > 10000000m)
            {
                return salary * 0.10m;
            }
            return 0m;
        }

        public decimal GetNetSalary()
        {
            return CalculateSalary() - GetTax();
        }

        public void PrintPayslip()
        {
            Console.WriteLine($"| {Name,-20} | {Department,-15} | {BaseSalary,12:N0} đ | {WorkDays,8} | {CalculateSalary(),12:N0} đ | {GetTax(),10:N0} đ | {GetNetSalary(),12:N0} đ |");
        }
    }
}
