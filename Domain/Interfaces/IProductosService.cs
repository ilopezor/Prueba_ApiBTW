using Common;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductosService
    {
        Task<GeneralResponse<List<ProductoDTO>>> GetAll();
        Task<GeneralResponse<Productos>> SaveProduct(ProductoDTO producto);
        Task<GeneralResponse<Productos>> DeleteProduct(long idProducto);
        Task<GeneralResponse<Productos>> UpdateProduct(ProductoDTO producto);
    }
}