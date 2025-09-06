using System;
using System.Collections.Generic;

namespace Prueba_ApiBTW.Models;

public partial class TipoMovimiento
{
    public long IdTipoMovimiento { get; set; }

    public byte[] Descripcion { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
