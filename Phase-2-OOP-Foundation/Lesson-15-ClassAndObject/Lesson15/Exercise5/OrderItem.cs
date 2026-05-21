using System;

namespace Lesson15.Exercise5
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetSubtotal()
        {
            return Product.Price * Quantity;
        }

        public void Print()
        {
            Console.WriteLine($"| {Product.Name,-20} | SL: {Quantity,3} | Đơn giá: {Product.Price,10:N0} đ | Thành tiền: {GetSubtotal(),12:N0} đ |");
        }
    }
}
