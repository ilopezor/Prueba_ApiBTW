using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    /// <summary>
    /// Servicio que implementa la lógica de negocio para la gestión de productos.
    /// Proporciona operaciones para consultar, crear, actualizar y desactivar productos.
    /// </summary>
    public class ProductosService(IProductsRepository productosRepository) : IProductosService
    {
        /// <summary>
        /// Obtiene todos los productos activos del sistema.
        /// </summary>
        /// <returns>
        /// Devuelve un objeto <see cref="GeneralResponse{T}"/> con la lista de productos activos.  
        /// Incluye información de su categoría correspondiente.  
        /// Si ocurre un error, <c>OperationSuccess</c> será <c>false</c>.
        /// </returns>
        public async Task<GeneralResponse<List<ProductoDTO>>> GetAll()
        {
            var response = new GeneralResponse<List<ProductoDTO>>();
            try
            {
                var productos = (await productosRepository.GetAllWithIncludes())
                    .Where(c => c.Estado)
                    .ToList();

                var result = productos.Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Cantidad = p.Cantidad,
                    Estado = p.Estado,
                    PrecioUnitario = p.PrecioUnitario,
                    IdCategoria = p.IdCategoria,
                    IdCategoriaNavigation = new CategoriaDTO
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria
                    }
                }).ToList();

                response.ObjectResponse = result;
                response.OperationSuccess = true;
                response.SuccessMessage = "Productos obtenidos correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al obtener productos: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Registra un nuevo producto en el sistema.
        /// </summary>
        /// <param name="producto">Objeto <see cref="ProductoDTO"/> con la información del producto a guardar.</param>
        /// <returns>
        /// Devuelve un objeto <see cref="GeneralResponse{T}"/> con el producto guardado.  
        /// Si ya existe un producto con el mismo nombre, devuelve error controlado.
        /// </returns>
        public async Task<GeneralResponse<Productos>> SaveProduct(ProductoDTO producto)
        {
            var response = new GeneralResponse<Productos>();
            try
            {
                if (producto == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Datos de producto inválidos.";
                    return response;
                }

                var exists = (await productosRepository.GetAllAsync())
                    .Any(p => p.NombreProducto == producto.NombreProducto && p.Estado);

                if (exists)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "El producto ya existe.";
                    return response;
                }

                var newProducto = new Productos
                {
                    NombreProducto = producto.NombreProducto,
                    Cantidad = producto.Cantidad,
                    Estado = producto.Estado,
                    FechaCreacion = DateTime.UtcNow,
                    IdCategoria = producto.IdCategoria,
                    PrecioUnitario = producto.PrecioUnitario
                };

                var savedProducto = await productosRepository.AddAsync(newProducto);
                response.ObjectResponse = savedProducto;
                response.OperationSuccess = true;
                response.SuccessMessage = "Producto guardado correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al guardar producto: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Actualiza la información de un producto existente.
        /// </summary>
        /// <param name="producto">Objeto <see cref="ProductoDTO"/> con los datos actualizados del producto.</param>
        /// <returns>
        /// Devuelve un objeto <see cref="GeneralResponse{T}"/> con el producto actualizado.  
        /// Si el producto no existe, devuelve error controlado.
        /// </returns>
        public async Task<GeneralResponse<Productos>> UpdateProduct(ProductoDTO producto)
        {
            var response = new GeneralResponse<Productos>
