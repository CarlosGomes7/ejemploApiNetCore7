using Example_API_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace Example_API_v1.Datos
{
    public class ApplicationDbContext: DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Villa> villas { get; set; }

    }
}
