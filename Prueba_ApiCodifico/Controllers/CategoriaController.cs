using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Prueba_ApiBTW.Controllers
{
    /// <summary>
    /// Controlador para la gestión de categorías.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController(ICategoriaService categoriaService) : ControllerBase
    {
        /// <summary>
        /// Obtiene todas las categorías.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Categoria>>>> GetAll()
        {
            try
            {
                var response = await categoriaService.GetAll();

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
                var errorResponse = new GeneralResponse<IEnumerable<Categoria>>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Actualiza el estado de una categoría (eliminación lógica).
        /// </summary>
        [HttpPut("Status")]
        public async Task<ActionResult<GeneralResponse<Categoria>>> UpdateStatus([FromBody] long id)
        {
            try
            {
                var response = await categoriaService.DeleteData(id);

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
                var errorResponse = new GeneralResponse<Categoria>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Categoria>>> Create(CategoriaDTO categoria)
        {
            try
            {
                var response = await categoriaService.SaveData(categoria);

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
                var errorResponse = new GeneralResponse<Categoria>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<GeneralResponse<Categoria>>> Update(CategoriaDTO categoria)
        {
            try
            {
                var response = await categoriaService.UpdateData(categoria);

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
                var errorResponse = new GeneralResponse<Categoria>
                {
                    OperationSuccess = false,
                    ErrorMessage = $"Error inesperado: {ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}