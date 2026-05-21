using System;
using System.Linq;

namespace Lesson15.Exercise4
{
    public static class Exercise4Runner
    {
        public static void Run()
        {
            Console.WriteLine("--- Bài 4: Chuyển đổi mảng song song -> Object ---");

            Student[] students = new Student[]
            {
                new Student("Minh", 8.5, 22),
                new Student("Hùng", 6.0, 25),
                new Student("Lan", 9.2, 20),
                new Student("An", 4.5, 21),
                new Student("Bình", 7.8, 23)
            };

            Console.WriteLine("\nDanh sách sinh viên:");
            PrintStudents(students);

            Console.WriteLine("\nDanh sách sinh viên (sắp xếp theo điểm giảm dần):");
            var sortedStudents = students.OrderByDescending(s => s.Score).ToArray();
            PrintStudents(sortedStudents);

            Console.Write("\nNhập tên sinh viên cần tìm: ");
            string searchName = Console.ReadLine() ?? "";
            var foundStudent = students.FirstOrDefault(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
            
            if (foundStudent != null)
            {
                Console.WriteLine("\nĐã tìm thấy sinh viên:");
                foundStudent.Print();
            }
            else
            {
                Console.WriteLine("\nKhông tìm thấy sinh viên có tên này.");
            }
            Console.WriteLine();
        }

        private static void PrintStudents(Student[] students)
        {
            Console.WriteLine(new string('-', 62));
            foreach (var student in students)
            {
                student.Print();
            }
            Console.WriteLine(new string('-', 62));
        }
    }
}
