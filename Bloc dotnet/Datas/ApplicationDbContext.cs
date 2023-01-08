using Bloc_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloc_dotnet.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Salarie> Salaries { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
