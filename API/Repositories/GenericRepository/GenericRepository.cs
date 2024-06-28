using System.Linq.Expressions;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Repositories {
    public interface GenericRepository<T> where T: BaseEntity {
        void Delete(T entity);
        Task DeleteAsync(int id);
        T? Get(int id);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        Task<T?> GetAsync(int id);
        Task<T?> GetAsync(int id, string include);
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
    }

    public class GenericRepositoryImplementation<T> : GenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;

        public GenericRepositoryImplementation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            T? entity = await this.GetAsync(id);
            if (entity != null) {
                this.Delete(entity);
            }
        }

        public T? Get(int id)
        {
            return this._dbSet.Find(id);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public Task<T?> GetAsync(int id, string include)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            return this._dbSet.Add(entity).Entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            EntityEntry<T> entryEntity = await this._dbSet.AddAsync(entity);
            return entryEntity.Entity;
        }

        public T Update(T entity)
        {
            EntityEntry<T> updatedEntity = this._dbSet.Update(entity);
            // _dbSet.Entry(entity).State = EntityState.Modified;
            //_dbSet.Attach(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}