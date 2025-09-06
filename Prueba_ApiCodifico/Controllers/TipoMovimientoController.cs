using Application.services;
using Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMovimientoController(ITipoMovimientoService tipoMovimientoService) : ControllerBase
    {
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
