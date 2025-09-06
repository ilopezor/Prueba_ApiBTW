using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.AppDbContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductsRepository : Repository<Productos>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Productos>> GetAllWithIncludes()
        {
            return await _context.Productos
            .Include(p => p.IdCategoriaNavigation) 
            .Where(p => p.Estado)                 
            .ToListAsync();
        }
    }
}
