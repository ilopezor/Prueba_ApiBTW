namespace Domain.Entities;

public partial class Categoria: BaseEntity
{
    public long IdCategoria { get; set; }
    public string NombreCategoria { get; set; }
    public bool Estado { get; set; }

    public virtual ICollection<Productos> Productos { get; set; }
}
