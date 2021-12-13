using Microsoft.EntityFrameworkCore;

namespace ExtensionsWebApi.Database
{
    public class UsersDbContext : DbContext //DbContext kütüphanesinden kalıtım alır.
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        //Database set işlemi -->
        public DbSet<User> Users { get; set; }
    }
}