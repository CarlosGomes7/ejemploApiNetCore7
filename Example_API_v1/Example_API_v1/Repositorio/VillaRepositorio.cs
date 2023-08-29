using Example_API_v1.Datos;
using Example_API_v1.Models;
using Example_API_v1.Repositorio.IRepositorio;

namespace Example_API_v1.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public VillaRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;            
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
           entidad.FechaDeActualizacion = DateTime.Now;
            _db.villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
