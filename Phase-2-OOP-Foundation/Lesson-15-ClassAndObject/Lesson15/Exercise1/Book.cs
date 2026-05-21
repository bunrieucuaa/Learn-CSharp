using System;

namespace Lesson15.Exercise1
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, decimal price, int pages, int year)
        {
            Title = title;
            Author = author;
            Price = price;
            Pages = pages;
            Year = year;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - Year;
        }

        public bool IsExpensive()
        {
            return Price > 200000m;
        }

        public void Print()
        {
            Console.WriteLine($"| {Title,-25} | {Author,-20} | {Price,10:N0} đ | {Pages,5} trang | NXB: {Year,4} ({GetAge()} năm) | {(IsExpensive() ? "Đắt" : "Bình dân")} |");
        }
    }
}
