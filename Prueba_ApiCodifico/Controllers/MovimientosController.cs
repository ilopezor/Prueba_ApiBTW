using Application.services;
using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    /// <summary>
    /// Controlador que gestiona los movimientos de productos en el sistema.
    /// Proporciona endpoints para consultar y registrar movimientos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController(IMovimientoService movimientosService) : ControllerBase
    {
        /// <summary>
        /// Obtiene todos los movimientos registrados.
        /// </summary>
        /// <returns>
        /// Devuelve un listado de movimientos en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la operación es exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoDTO>>> GetAll()
        {
            try
            {
                var response = await movimientosService.GetAll();

                if (response.OperationSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new GeneralResponse<List<MovimientoDTO>>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Registra un nuevo movimiento en el sistema.
        /// </summary>
        /// <param name="movimiento">Objeto con la información del movimiento a registrar.</param>
        /// <returns>
        /// Devuelve el resultado de la operación en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la creación fue exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Productos>>> SaveProduct([FromBody] MovimientoDTO movimiento)
        {
            try
            {
                var response = await movimientosService.CreateOrder(movimiento);

                if (response.OperationSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new GeneralResponse<Productos>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
