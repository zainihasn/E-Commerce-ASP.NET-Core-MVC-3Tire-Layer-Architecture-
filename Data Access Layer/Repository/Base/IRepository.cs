using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Base
{
    
        public interface IRepository<T> where T : class
        {


            Task<T> FindByIdAsync(int Id);
            Task<IEnumerable<T>> FindAllAsync();
            Task<IEnumerable<T>> FindAllAsync(
              Expression<Func<T, bool>> predicate = null,
              Expression<Func<T, object>> orderBy = null,
               bool ascending = true,
               int? take = null);
            Task<T> SelectOneAsync(Expression<Func<T, bool>> match);     
            Task AddOneAsync(T entity);     
            Task EditOneAsync(T entity);        
            Task DeleteOneAsync(T entity);  
            Task AddListAsync(IEnumerable<T> entities);
            
      
    }
    
}
