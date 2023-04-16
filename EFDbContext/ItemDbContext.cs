using ItemRazorV1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemRazorV1.EFDbContext
{
    public class ItemDbContext : DbContext
    {
        // Et alternativ til at override OnConfiguring( ) er at oprette en "ConnectionStrings" i appSettings.json.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ItemDB; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }



    }
}
