using Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITipoMovimientoService
    {
        Task<GeneralResponse<List<TipoMovimiento>>> GetAll();
    }
}