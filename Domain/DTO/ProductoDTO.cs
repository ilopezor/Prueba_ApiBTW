using Domain.Entities;

namespace Domain.DTO
{
    public class ProductoDTO
    {
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdCategoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public virtual CategoriaDTO? IdCategoriaNavigation { get; set; } = null!;

    }
}
