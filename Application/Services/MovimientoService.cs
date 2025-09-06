using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    public class MovimientoService(IMovimientoRepository movimientosRepository) : IMovimientoService
    {
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
                        fechaMovimiento  = p.FechaMovimiento,
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
