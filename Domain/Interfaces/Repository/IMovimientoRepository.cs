using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IMovimientoRepository : IRepository<Movimientos>
    {
        Task<List<Movimientos>> GetAllWithIncludes();
    }
}