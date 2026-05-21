using System;

namespace Lesson15.Exercise5
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public bool Sell(int quantity)
        {
            if (Stock >= quantity)
            {
                Stock -= quantity;
                return true;
            }
            return false;
        }

        public void Restock(int quantity)
        {
            Stock += quantity;
        }

        public void Print()
        {
            Console.WriteLine($"| {Name,-20} | Giá: {Price,10:N0} đ | Tồn kho: {Stock,4} |");
        }
    }
}
