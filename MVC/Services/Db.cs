using Microsoft.EntityFrameworkCore;

namespace MVC.Services
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }
        public DbSet<MVC.Models.Product> Products
        {
            get; set;
        }
    }
}
