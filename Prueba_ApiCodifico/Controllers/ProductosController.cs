using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones relacionadas con los productos.
    /// Permite consultar, crear, actualizar y cambiar el estado de los productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController(IProductosService productosService) : ControllerBase
    {
        /// <summary>
        /// Obtiene todos los productos registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Devuelve un listado de productos en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la operación es exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
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

        /// <summary>
        /// Registra un nuevo producto en el sistema.
        /// </summary>
        /// <param name="producto">Objeto con la información del producto a registrar.</param>
        /// <returns>
        /// Devuelve el resultado de la operación en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la creación fue exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
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

        /// <summary>
        /// Actualiza la información de un producto existente.
        /// </summary>
        /// <param name="producto">Objeto con los datos actualizados del producto.</param>
        /// <returns>
        /// Devuelve el resultado de la operación en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si la actualización fue exitosa, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
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

        /// <summary>
        /// Cambia el estado de un producto (activo/inactivo).
        /// </summary>
        /// <param name="id">Identificador único del producto.</param>
        /// <returns>
        /// Devuelve el resultado de la operación en un objeto <see cref="GeneralResponse{T}"/>.  
        /// Responde con código 200 si el cambio fue exitoso, 400 si hay error controlado,  
        /// o 500 en caso de error inesperado.
        /// </returns>
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
