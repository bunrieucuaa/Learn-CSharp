using System;

namespace Lesson15.Exercise4
{
    public class Student
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public int Age { get; set; }

        public Student(string name, double score, int age)
        {
            Name = name;
            Score = score;
            Age = age;
        }

        public string GetGrade()
        {
            if (Score >= 9.0) return "Xuất sắc";
            if (Score >= 8.0) return "Giỏi";
            if (Score >= 6.5) return "Khá";
            if (Score >= 5.0) return "Trung bình";
            return "Yếu";
        }

        public bool IsPass()
        {
            return Score >= 5.0;
        }

        public void Print()
        {
            Console.WriteLine($"| {Name,-15} | {Age,4} tuổi | Điểm: {Score,4:F1} | {GetGrade(),-10} | {(IsPass() ? "Đậu" : "Rớt"),-4} |");
        }
    }
}
