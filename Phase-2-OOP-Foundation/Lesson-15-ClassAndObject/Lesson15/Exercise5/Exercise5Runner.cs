using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson15.Exercise5
{
    public static class Exercise5Runner
    {
        public static void Run()
        {
            Console.WriteLine("--- Bài 5: 2 Class tương tác - Order System ---");

            Product[] products = new Product[]
            {
                new Product("Laptop Dell", 15000000m, 10),
                new Product("Chuột Logitech", 500000m, 50),
                new Product("Bàn phím cơ", 1200000m, 20),
                new Product("Màn hình LG", 4500000m, 15),
                new Product("Tai nghe Sony", 2000000m, 30)
            };

            Console.WriteLine("Danh sách sản phẩm hiện có:");
            foreach (var p in products)
            {
                p.Print();
            }

            Console.WriteLine("\nKhách hàng mua hàng...");
            
            List<OrderItem> orderItems = new List<OrderItem>();
            
            // Khách mua 2 Laptop
            if (products[0].Sell(2))
            {
                orderItems.Add(new OrderItem(products[0], 2));
            }

            // Khách mua 1 Chuột
            if (products[1].Sell(1))
            {
                orderItems.Add(new OrderItem(products[1], 1));
            }

            // Khách mua 3 Bàn phím cơ
            if (products[2].Sell(3))
            {
                orderItems.Add(new OrderItem(products[2], 3));
            }

            Console.WriteLine("\n================== HÓA ĐƠN ==================");
            decimal totalOrder = 0;
            foreach (var item in orderItems)
            {
                item.Print();
                totalOrder += item.GetSubtotal();
            }
            Console.WriteLine(new string('-', 71));
            Console.WriteLine($"TỔNG CỘNG: {totalOrder,56:N0} đ");
            Console.WriteLine("=============================================");

            Console.WriteLine("\nKho hàng sau khi bán:");
            foreach (var p in products)
            {
                p.Print();
            }
            Console.WriteLine();
        }
    }
}
