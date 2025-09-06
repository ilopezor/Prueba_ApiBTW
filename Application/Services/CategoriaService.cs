using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    /// <summary>
    /// Servicio para la gestión de categorías, implementando la lógica de negocio.
    /// </summary>
    public class CategoriaService(IRepository<Categoria> categoriaRepository) : ICategoriaService
    {
        /// <summary>
        /// Guarda una nueva categoría en la base de datos.
        /// </summary>
        public async Task<GeneralResponse<Categoria>> SaveData(CategoriaDTO categoria)
        {
            var response = new GeneralResponse<Categoria>();
            try
            {
                if (categoria == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Datos de categoría inválidos.";
                    return response;
                }
                var exists = (await categoriaRepository.GetAllAsync())
                    .Any(c => c.NombreCategoria == categoria.NombreCategoria && c.Estado);

                Categoria categoriaEntity = new()
                {
                    NombreCategoria = categoria.NombreCategoria,
                    Estado = true,
                };

                var savedCategoria = await categoriaRepository.AddAsync(categoriaEntity);
                response.ObjectResponse = savedCategoria;
                response.OperationSuccess = true;
                response.SuccessMessage = "Categoría guardada correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al guardar la categoría: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Obtiene todas las categorías activas.
        /// </summary>
        public async Task<GeneralResponse<List<Categoria>>> GetAll()
        {
            var response = new GeneralResponse<List<Categoria>>();
            try
            {
                List<Categoria> categorias = (await categoriaRepository.GetAllAsync())
                .Where(c => c.Estado)
                .ToList();

                response.ObjectResponse = categorias;
                response.OperationSuccess = true;
                response.SuccessMessage = "Categorias obtenidas con exito.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Categoria no encontrada: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        public async Task<GeneralResponse<Categoria>> UpdateData(CategoriaDTO categoria)
        {
            var response = new GeneralResponse<Categoria>();
            try
            {
                if (categoria == null || categoria.IdCategoria <= 0)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Datos de categoría inválidos.";
                    return response;
                }

                var categoriaEntity = await categoriaRepository.GetByIdAsync(categoria.IdCategoria);
                if (categoriaEntity == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Categoría no encontrada.";
                    return response;
                }

                categoriaEntity.NombreCategoria = categoria.NombreCategoria;
                await categoriaRepository.UpdateAsync(categoriaEntity);

                response.ObjectResponse = categoriaEntity;
                response.OperationSuccess = true;
                response.SuccessMessage = "Categoría actualizada correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al actualizar la categoría: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica de una categoría cambiando su estado.
        /// </summary>
        public async Task<GeneralResponse<Categoria>> DeleteData(long id)
        {
            var response = new GeneralResponse<Categoria>();
            try
            {
                var categoria = await categoriaRepository.GetByIdAsync(id);
                if (categoria == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Categoría no encontrada.";
                    return response;
                }

                categoria.Estado = false;

                await categoriaRepository.UpdateAsync(categoria);
                response.ObjectResponse = categoria;
                response.OperationSuccess = true;
                response.SuccessMessage = "Categoría eliminada correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al eliminar la categoría: {ex.Message}";
                return response;
            }
        }
    }
}