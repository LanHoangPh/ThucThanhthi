using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers {  get; set; } 
        public DbSet<Product> Products {  get; set; } 
        public DbSet<Order> Orders {  get; set; } 
        public DbSet<OrderDetail> OrderDetails {  get; set; } 
        public DbSet<Payment> Payments {  get; set; } 
    }
}
