using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    /// <summary>
    /// Service class that manages business logic related to Movimientos.
    /// Handles retrieving and creating movement records with validations
    /// and mapping to DTOs.
    /// </summary>
    public class MovimientoService(IMovimientoRepository movimientosRepository) : IMovimientoService
    {
        /// <summary>
        /// Retrieves all movements including related entities (Producto and TipoMovimiento).
        /// </summary>
        /// <returns>
        /// A <see cref="GeneralResponse{T}"/> containing a list of <see cref="MovimientoDTO"/>.
        /// </returns>
        public async Task<GeneralResponse<List<MovimientoDTO>>> GetAll()
        {
            var response = new GeneralResponse<List<MovimientoDTO>>();
            try
            {
                List<Movimientos> movimientos = (await movimientosRepository.GetAllWithIncludes()).ToList();

                if (movimientos == null || !movimientos.Any())
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "No se encontraron movimientos para el cliente especificado.";
                    return response;
                }

                List<MovimientoDTO> movimientoDto = movimientos.Select(p =>
                    new MovimientoDTO()
                    {
                        idMovimientos = p.IdMovimientos,
                        cantidad = p.Cantidad,
                        comentario = p.Comentario,
                        fechaMovimiento = p.FechaMovimiento,
                        idProducto = p.IdProducto,
                        idTipoMovimiento = p.IdTipoMovimiento,
                        producto = p.IdProductoNavigation != null ? p.IdProductoNavigation.NombreProducto : string.Empty,
                        tipoMovimiento = p.IdTipoMovimientoNavigation != null ? p.IdTipoMovimientoNavigation.Descripcion : string.Empty
                    }
                ).ToList();

                response.ObjectResponse = movimientoDto;
                response.OperationSuccess = true;
                response.SuccessMessage = "Movimientos obtenidos correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"No se encontraron movimientos: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Creates a new movement record in the system based on the provided DTO.
        /// </summary>
        /// <param name="order">The <see cref="MovimientoDTO"/> with movement data.</param>
        /// <returns>
        /// A <see cref="GeneralResponse{T}"/> containing the created <see cref="Movimientos"/> entity.
        /// </returns>
        public async Task<GeneralResponse<Movimientos>> CreateOrder(MovimientoDTO order)
        {
            var response = new GeneralResponse<Movimientos>();
            try
            {
                if (order == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Datos de movimiento inválidos.";
                    return response;
                }

                var movimiento = new Movimientos
                {
                    Cantidad = order.cantidad,
                    Comentario = order.comentario,
                    FechaCreacion = DateTime.UtcNow,
                    IdProducto = order.idProducto,
                    IdTipoMovimiento = order.idTipoMovimiento,
                    FechaMovimiento = order.fechaMovimiento
                };

                var savedMovimiento = await movimientosRepository.AddAsync(movimiento);

                response.ObjectResponse = savedMovimiento;
                response.OperationSuccess = true;
                response.SuccessMessage = "Movimiento creado correctamente.";
                return response;

            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al crear el movimiento: {ex.Message}";
                return response;
            }
        }
    }
}
