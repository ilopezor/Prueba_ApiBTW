using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.AppDbContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovimientoRepository : Repository<Movimientos>, IMovimientoRepository
    {
        public MovimientoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Movimientos>> GetAllWithIncludes()
        {
            return await _context.Movimientos
            .Include(p => p.IdTipoMovimientoNavigation)
            .Include(p => p.IdProductoNavigation)
            .ToListAsync();
        }
    }
}
