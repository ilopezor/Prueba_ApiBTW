using System;
using System.Collections.Generic;

namespace Prueba_ApiBTW.Models;

public partial class Categoria
{
    public long IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Prodcuto> Prodcutos { get; set; } = new List<Prodcuto>();
}
