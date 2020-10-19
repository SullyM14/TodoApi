using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class SpartansContext : DbContext
    {
        public SpartansContext(DbContextOptions<SpartansContext> options)
            : base(options)
        {
        }

        public DbSet<Spartans> Spartans { get; set; }
    }
}
