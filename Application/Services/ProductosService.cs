using Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Application.services
{
    public class ProductosService(IProductsRepository productosRepository) : IProductosService
    {
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

        public async Task<GeneralResponse<Productos>> UpdateProduct(ProductoDTO producto)
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

                var existingProducto = await productosRepository.GetByIdAsync(producto.IdProducto);
                if (existingProducto == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Producto no encontrado.";
                    return response;
                }

                existingProducto.NombreProducto = producto.NombreProducto;
                existingProducto.Cantidad = producto.Cantidad;
                existingProducto.Estado = producto.Estado;
                existingProducto.IdCategoria = producto.IdCategoria;
                existingProducto.PrecioUnitario = producto.PrecioUnitario;

                await productosRepository.UpdateAsync(existingProducto);

                response.ObjectResponse = existingProducto;
                response.OperationSuccess = true;
                response.SuccessMessage = "Producto actualizado correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al actualizar producto: {ex.Message}";
                return response;
            }
        }

        public async Task<GeneralResponse<Productos>> DeleteProduct(long idProducto)
        {
            var response = new GeneralResponse<Productos>();
            try
            {
                var producto = await productosRepository.GetByIdAsync(idProducto);
                if (producto == null)
                {
                    response.OperationSuccess = false;
                    response.ErrorMessage = "Producto no encontrado.";
                    return response;
                }

                producto.Estado = false;
                await productosRepository.UpdateAsync(producto);

                response.ObjectResponse = producto;
                response.OperationSuccess = true;
                response.SuccessMessage = "Estado del producto actualizado correctamente.";
                return response;
            }
            catch (Exception ex)
            {
                response.OperationSuccess = false;
                response.ErrorMessage = $"Error al actualizar estado del producto: {ex.Message}";
                return response;
            }
        }
    }
}
