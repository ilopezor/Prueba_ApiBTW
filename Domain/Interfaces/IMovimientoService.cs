using Common;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovimientoService
    {
        Task<GeneralResponse<List<MovimientoDTO>>> GetAll();
        Task<GeneralResponse<Movimientos>> CreateOrder(MovimientoDTO movimiento);
    }
}