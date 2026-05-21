using System;
using System.Globalization;

namespace Lesson16
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("123");

            Student hocSinhA = new Student("An", 12, 26.76);

            hocSinhA.LogStudent();

            hocSinhA.SetName("Binh");

            hocSinhA.LogStudent();

        }
    }

    class Student
    {
        public string Name;
        public int Age;
        public double Score;

        // Constructor
        public Student(string Name, int age, double score)
        {
            this.Name = Name;
            Age = age;
            Score = score;
        }

        public void LogStudent()
        {
            Console.WriteLine($"Tên: {Name}, tuổi: {Age}, Score: {Score}");
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}

