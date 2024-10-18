using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }

        // Một chi tiết đơn hàng thuộc về một đơn hàng
        public Order? Order { get; set; }

        // Một chi tiết đơn hàng chứa một sản phẩm
        public Product? Product { get; set; }
    }
}
