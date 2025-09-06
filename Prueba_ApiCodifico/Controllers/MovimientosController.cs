using Application.services;
using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController(IMovimientoService movimientosService) : ControllerBase
    {
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
