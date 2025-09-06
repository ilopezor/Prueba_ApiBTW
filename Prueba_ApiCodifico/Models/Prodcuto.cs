using System;
using System.Collections.Generic;

namespace Prueba_ApiBTW.Models;

public partial class Prodcuto
{
    public long IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public int Catidad { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public long IdCategoria { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
