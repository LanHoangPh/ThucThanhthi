using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Payment
    {
        [Key] 
        public int PaymentId { get; set; }
        public string? PaymentMethod { get; set; } // WArnings CS8616 nghĩa là gì.
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Một thanh toán chỉ thuộc về một đơn hàng
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
