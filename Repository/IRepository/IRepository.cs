using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnet_ids.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> ? filter = null ); 
        Task<T?> GetAsync(Expression<Func<T,bool>>?  filter = null , bool tracked = true , string? includeNavigations = null); 
        Task CreateAsync(T entity); 
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();
    }
}