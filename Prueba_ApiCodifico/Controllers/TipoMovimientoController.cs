using Application.services;
using Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones relacionadas con los tipos de movimiento.
    /// Permite consultar los tipos de movimiento registrados en el sistema.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMovimientoController(ITipoMovimientoService tipoMovimientoService) : ControllerBase
    {
        /// <summary>
        /// Obtiene todos los tipos de movimiento registrados.
        /// </summary>
        /// <returns>
        /// Devuelve un listado de tipos de movimiento en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la operación es exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMovimiento>>> GetAll()
        {
            try
            {
                var response = await tipoMovimientoService.GetAll();

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
                var errorResponse = new GeneralResponse<List<TipoMovimiento>>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
