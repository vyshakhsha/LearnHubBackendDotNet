using Microsoft.EntityFrameworkCore;
using LearnHubBackendDotNet.Models;

namespace LearnHubBackendDotNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TokenBlacklist> TokenBlacklists { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Cart> CartItems { get; set; }

    }
}