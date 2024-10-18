using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        // Một sản phẩm có thể có nhiều chi tiết đơn hàng
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
