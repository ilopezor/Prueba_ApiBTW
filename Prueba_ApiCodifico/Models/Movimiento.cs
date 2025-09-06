using System;
using System.Collections.Generic;

namespace Prueba_ApiBTW.Models;

public partial class Movimiento
{
    public long IdMovimientos { get; set; }

    public long IdProducto { get; set; }

    public long IdTipoMovimiento { get; set; }

    public int Cantidad { get; set; }

    public DateOnly FechaMovimiento { get; set; }

    public byte[] Comentario { get; set; } = null!;

    public DateOnly FechaCreacion { get; set; }

    public virtual Prodcuto IdProductoNavigation { get; set; } = null!;

    public virtual TipoMovimiento IdTipoMovimientoNavigation { get; set; } = null!;
}
