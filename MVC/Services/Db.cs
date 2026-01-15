using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Services
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }
        public DbSet<MVC.Models.Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
