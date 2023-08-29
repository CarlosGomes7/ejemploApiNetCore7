using Example_API_v1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Example_API_v1.Datos
{
    public class ApplicationDbContext: DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Villa> villas { get; set; }


        //este codigo es para poder tener valores ya definidos cuando se actualice la base de dato con el comnado update-database
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Villa>().HasData(
        //    new Villa() {
        //        Id = 1,
        //        Nombre= "Cupira",                
        //        Ocupantes = 2,
        //        MetrosCuadrados = 400,
        //        Detalles = "detalle",
        //        Tarifa = 100,
        //        FechaDeCreacion = DateTime.Now,
        //        ImagenUrl = "",
        //        Amenidad = "",
        //        FechaDeActualizacion = DateTime.Now
        //    }    
        //    );
        //}


    }
}
