using System;
using System.Linq;

namespace Lesson15.Exercise3
{
    public static class Exercise3Runner
    {
        public static void Run()
        {
            Console.WriteLine("--- Bài 3: Class Rectangle ---");

            Rectangle[] rectangles = new Rectangle[]
            {
                new Rectangle(3, 4),
                new Rectangle(5, 5),
                new Rectangle(10, 2)
            };

            Console.WriteLine("Danh sách hình chữ nhật ban đầu:");
            for (int i = 0; i < rectangles.Length; i++)
            {
                Console.WriteLine($"\nHình {i + 1}:");
                rectangles[i].Print();
            }

            Console.WriteLine("\nScale hình thứ nhất x2:");
            rectangles[0].Scale(2);
            rectangles[0].Print();

            var largestRectangle = rectangles.OrderByDescending(r => r.Area()).First();
            Console.WriteLine($"\n> Hình có diện tích lớn nhất là hình có Rộng: {largestRectangle.Width}, Cao: {largestRectangle.Height} (Diện tích: {largestRectangle.Area()})");
            Console.WriteLine();
        }
    }
}
