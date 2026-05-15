/*
* 📝 Bài tập Lesson 01 — Variables
*/

using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- HỆ THỐNG QUẢN LÝ ĐƠN HÀNG MINI ---");
        Console.WriteLine();

        // 1. Khai báo biến thông tin khách hàng
        string customerName = "Nguyễn Thị C";
        string customerPhone = "0901234567";
        string customerEmail = "[EMAIL_ADDRESS]";
        string customerAddress = "123 Lê Lợi, Q.1, TP.HCM";

        // 2. Khai báo biến thông tin đơn hàng
        string orderId = "ORD-20250511-001";
        string orderDate = DateTime.Now.ToString("dd/MM/yyyy");

        // Thông tin sản phẩm
        string product1Name = "Áo thun";
        int product1Quantity = 2;
        decimal product1Price = 250000m;

        string product2Name = "Quần jean";
        int product2Quantity = 1;
        decimal product2Price = 450000m;

        string product3Name = "Giày sneaker";
        int product3Quantity = 1;
        decimal product3Price = 850000m;

        // 3. Khai báo hằng số và biến tính toán
        const decimal SHIPPING_FEE = 30000m;   // Phí ship cố định
        const decimal DISCOUNT_RATE = 0.05m;  // Giảm giá 5%

        // Tính tạm tính (subtotal)
        decimal subtotal = (product1Price * product1Quantity) + 
                           (product2Price * product2Quantity) + 
                           (product3Price * product3Quantity);

        // Tính giảm giá (nếu có)
        bool isEligibleForDiscount = subtotal > 500000m;
        decimal discountAmount = 0m;
        if (isEligibleForDiscount)
        {
            discountAmount = subtotal * DISCOUNT_RATE;
        }

        // Tính tổng cộng
        decimal total = subtotal - discountAmount + SHIPPING_FEE;

        // 4. Khai báo trạng thái đơn hàng
        string orderStatus = "Đang xử lý";
        bool isPaid = false;
        string paidStatus = isPaid ? "Đã thanh toán" : "Chưa thanh toán";

        // 5. In phiếu xác nhận đơn hàng
        Console.WriteLine("╔═══════════════════════════════════════════════════╗");
        Console.WriteLine("║           PHIẾU XÁC NHẬN ĐƠN HÀNG               ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════╣");

        // In thông tin khách hàng
        Console.WriteLine($"║ Mã đơn hàng:  {orderId,-25} ║");
        Console.WriteLine($"║ Ngày đặt:     {orderDate,-25} ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════╣");
        Console.WriteLine("║ THÔNG TIN KHÁCH HÀNG                             ║");
        Console.WriteLine($"║ Họ tên:       {customerName,-25} ║");
        Console.WriteLine($"║ SĐT:          {customerPhone,-25} ║");
        Console.WriteLine($"║ Email:         {customerEmail,-25} ║");
        Console.WriteLine($"║ Địa chỉ:      {customerAddress,-25} ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════╣");
        Console.WriteLine("║ CHI TIẾT ĐƠN HÀNG                                ║");
        
        // In danh sách sản phẩm
        Console.WriteLine($"║ 1. {product1Name,-15} x{product1Quantity}  {product1Price:N0} VNĐ  ║");
        Console.WriteLine($"║ 2. {product2Name,-15} x{product2Quantity}  {product2Price:N0} VNĐ  ║");
        Console.WriteLine($"║ 3. {product3Name,-15} x{product3Quantity}  {product3Price:N0} VNĐ  ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════╣");

        // In tính toán
        Console.WriteLine($"║ Tạm tính:                  {subtotal:N0} VNĐ ║");
        
        if (isEligibleForDiscount)
        {
            Console.WriteLine($"║ Giảm giá (5%):             -{discountAmount:N0} VNĐ ║");
        }
        
        Console.WriteLine($"║ Phí ship:                     {SHIPPING_FEE:N0} VNĐ ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════╣");
        Console.WriteLine($"║ TỔNG CỘNG:                 {total:N0} VNĐ ║");
        
        Console.WriteLine($"║ Trạng thái:    {orderStatus,-15} ║");
        Console.WriteLine($"║ Thanh toán:    {paidStatus,-15} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════╝");
    }
}
    