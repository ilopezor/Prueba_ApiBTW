namespace Domain.Entities;

public partial class TipoMovimiento : BaseEntity
{
    public long IdTipoMovimiento { get; set; }
    public string Descripcion { get; set; }
    public virtual ICollection<Movimientos> Movimientos { get; set; }



}
