using Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    public class TipoMovimientoService(IRepository<TipoMovimiento> tipoMovimientoRepository) : ITipoMovimientoService
    {

        public async Task<GeneralResponse<List<TipoMovimiento>>> GetAll()
        {
            var response = new GeneralResponse<List<TipoMovimiento>>();
            try
            {
                List<TipoMovimiento> tipoMovimientos = (await tipoMovimientoRepository.GetAllAsync()).ToList();

                response.ObjectResponse = tipoMovimientos;
                response.OperationSuccess = true;
                response.SuccessMessage = "Tipos de movimiento obtenidos correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"No se encontraron tipos de movimiento: {ex.Message}";
                return response;
            }
        }
    }
}
