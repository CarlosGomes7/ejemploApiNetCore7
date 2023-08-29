using Example_API_v1.Datos;
using Example_API_v1.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Example_API_v1.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();                
        }



        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Grabar();
        }

        public async Task delete(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }

        public async Task<T> Get(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();

        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }
    }

}
