using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ids.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext db)
        {
            this._db = db;
            dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null) { query = query.Where(filter); }

            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked) { query.AsNoTracking(); }
            if (filter != null) { query = query.Where(filter); }

            return await query.FirstOrDefaultAsync() ;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await SaveAsync();

            return entity;
        }
        
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}