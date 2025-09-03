using Microsoft.EntityFrameworkCore;
using ECommerceApi.Models; // User sınıfı burada olacak

namespace ECommerceApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
