using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController( IProductosService productosService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ProductoDTO>>>> GetAll()
        {
            try
            {
                var response = await productosService.GetAll();

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
                var errorResponse = new GeneralResponse<IEnumerable<ProductoDTO>>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Productos>>> Save([FromBody] ProductoDTO producto)
        {
            try
            {
                var response = await productosService.SaveProduct(producto);

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

        [HttpPut]
        public async Task<ActionResult<GeneralResponse<Productos>>> Update([FromBody] ProductoDTO producto)
        {
            try
            {
                var response = await productosService.UpdateProduct(producto);

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

        [HttpPut("Status")]
        public async Task<ActionResult<GeneralResponse<Productos>>> UpdateStatus([FromBody] long id)
        {
            try
            {
                var response = await productosService.DeleteProduct(id);

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
