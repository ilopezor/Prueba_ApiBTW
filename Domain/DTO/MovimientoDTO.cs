using Domain.Entities;

namespace Domain.DTO
{
    public class MovimientoDTO
    {
        public long idMovimientos { get; set; }
        public long idProducto { get; set; }
        public long idTipoMovimiento { get; set; }
        public int cantidad { get; set; }
        public DateTime fechaMovimiento { get; set; }
        public string comentario { get; set; }
        public string producto { get; set; }
        public string tipoMovimiento { get; set; }

    }
}
