using System.Linq.Expressions;
using DataAccessLayer.Repository.Base;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repository
{

    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public MainRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); 
        }

        
        public async Task<T> FindByIdAsync(int id) => await _dbSet.FindAsync(id);         
        public async Task<IEnumerable<T>> FindAllAsync() => await _dbSet.ToListAsync();
        public async Task<T> SelectOneAsync(Expression<Func<T, bool>> match) => await _dbSet.FirstOrDefaultAsync(match);


        public async Task<IEnumerable<T>> FindAllAsync(
              Expression<Func<T, bool>> predicate = null,
              Expression<Func<T, object>> orderBy = null,
              bool ascending = true,
              int? take = null)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }





        public async Task AddOneAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

       
        public async Task EditOneAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteOneAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        
        public async Task AddListAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

      

    }
}
