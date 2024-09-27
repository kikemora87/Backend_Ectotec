using Maqueta_Backend_EctoTec.Models;
using Microsoft.EntityFrameworkCore;

namespace Maqueta_Backend_EctoTec.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
