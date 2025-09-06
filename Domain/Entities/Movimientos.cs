namespace Domain.Entities;

public partial class Movimientos : BaseEntity
{
    public long IdMovimientos { get; set; }
    public int Cantidad { get; set; }
    public string? Comentario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public long IdProducto { get; set; }
    public long IdTipoMovimiento { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public virtual Productos IdProductoNavigation { get; set; }
    public virtual TipoMovimiento IdTipoMovimientoNavigation { get; set; }
}
