using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Define las operaciones básicas de acceso a datos para una entidad.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad.</typeparam>
    public interface IProductsRepository : IRepository<Productos>
    {
        Task<List<Productos>> GetAllWithIncludes();
    }
}
