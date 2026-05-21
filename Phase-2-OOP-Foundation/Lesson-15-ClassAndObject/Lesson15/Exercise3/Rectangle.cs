using System;

namespace Lesson15.Exercise3
{
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area()
        {
            return Width * Height;
        }

        public double Perimeter()
        {
            return 2 * (Width + Height);
        }

        public bool IsSquare()
        {
            return Width == Height;
        }

        public void Scale(double factor)
        {
            Width *= factor;
            Height *= factor;
        }

        public void Print()
        {
            Console.WriteLine($"[Rectangle] Rộng: {Width}, Cao: {Height}, Chu vi: {Perimeter()}, Diện tích: {Area()}, Hình vuông: {(IsSquare() ? "Có" : "Không")}");
            
            // Vẽ bằng dấu *
            int w = (int)Math.Round(Width);
            int h = (int)Math.Round(Height);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
