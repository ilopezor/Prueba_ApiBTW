using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación genérica de la interfaz IOrderRepository para el acceso a datos.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad.</typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// El contexto de la base de datos.
        /// </summary>
        protected readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor de la clase Repository.
        /// </summary>
        /// <param name="context">El contexto de la base de datos a utilizar.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        /// <inheritdoc />
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<T> AddAsync(T entity)
        {
            var newEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
