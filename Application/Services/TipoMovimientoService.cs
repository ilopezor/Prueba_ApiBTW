using Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    /// <summary>
    /// Servicio que implementa la lógica de negocio para la gestión de los tipos de movimiento.
    /// Se encarga de consultar la información desde el repositorio correspondiente.
    /// </summary>
    public class TipoMovimientoService(IRepository<TipoMovimiento> tipoMovimientoRepository) : ITipoMovimientoService
    {
        /// <summary>
        /// Obtiene todos los tipos de movimiento registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Devuelve un objeto <see cref="GeneralResponse{T}"/> con la lista de tipos de movimiento.  
        /// La propiedad <c>OperationSuccess</c> será <c>true</c> si la operación fue exitosa.  
        /// En caso de error, <c>OperationSuccess</c> será <c>false</c> y se incluirá un mensaje en <c>ErrorMessage</c>.
        /// </returns>
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
