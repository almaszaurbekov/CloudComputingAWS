using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Context
{
    public class RdsContext : IdentityDbContext<User>
    {
        public RdsContext(DbContextOptions<RdsContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
