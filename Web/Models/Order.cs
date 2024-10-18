using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }

        // Một đơn hàng thuộc về một khách hàng
        public Customer? Customer { get; set; }

        // Một đơn hàng có thể có nhiều chi tiết
        public ICollection<OrderDetail>? OrderDetails { get; set; }

        // Mối quan hệ với thanh toán
        public Payment? Payment { get; set; }
    }
}
