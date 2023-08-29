using System.Linq.Expressions;

namespace Example_API_v1.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Create(T entity);

        Task<List<T>> GetAll(Expression<Func<T,bool>>? filtro = null);

        Task<T> Get(Expression<Func<T, bool>>? filtro = null, bool tracked = true);

        Task delete(T entidad);

        Task Grabar();

    }
}
