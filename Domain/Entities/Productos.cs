namespace Domain.Entities;

public partial class Productos : BaseEntity
{
    public long IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public long IdCategoria { get; set; }
    public decimal PrecioUnitario { get; set; }
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
    public virtual ICollection<Movimientos> Movimientos { get; set; }


}
