using Microsoft.EntityFrameworkCore;
using WDC_F24.infrastructure.Data;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Text;



namespace WDC_F24.infrastructure.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


            //public DbSet<Product> Products { get; set; }

            // You can override OnModelCreating if needed
        }
}
