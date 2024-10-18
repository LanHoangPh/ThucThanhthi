using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
