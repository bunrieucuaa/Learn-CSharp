using System;
using System.Linq;

namespace Lesson15.Exercise1
{
    public static class Exercise1Runner
    {
        public static void Run()
        {
            Console.WriteLine("--- Bài 1: Class Book ---");

            Book[] books = new Book[]
            {
                new Book("Dế Mèn Phiêu Lưu Ký", "Tô Hoài", 45000m, 150, 1941),
                new Book("Đắc Nhân Tâm", "Dale Carnegie", 80000m, 320, 1936),
                new Book("Clean Code", "Robert C. Martin", 350000m, 464, 2008),
                new Book("Harry Potter", "J.K. Rowling", 250000m, 500, 1997),
                new Book("Nhà Giả Kim", "Paulo Coelho", 79000m, 225, 1988)
            };

            Console.WriteLine(new string('-', 106));
            Console.WriteLine($"| {"Tên sách",-25} | {"Tác giả",-20} | {"Giá",10} | {"Số trang",11} | {"Xuất bản",-17} | {"Phân loại",-10} |");
            Console.WriteLine(new string('-', 106));

            foreach (var book in books)
            {
                book.Print();
            }
            Console.WriteLine(new string('-', 106));

            var mostExpensiveBook = books.OrderByDescending(b => b.Price).First();
            var oldestBook = books.OrderBy(b => b.Year).First();

            Console.WriteLine($"\n> Sách đắt nhất: {mostExpensiveBook.Title} ({mostExpensiveBook.Price:N0} đ)");
            Console.WriteLine($"> Sách cũ nhất: {oldestBook.Title} (Xuất bản năm {oldestBook.Year})");
            Console.WriteLine();
        }
    }
}
