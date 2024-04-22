using System.Collections.ObjectModel;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories {
    public interface GenericRepository<T> where T: BaseEntity {
        void Delete(T entity);
        Task DeleteAsync(int id);
        T? Get(int id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Update(T entity);
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

        public List<T> GetAll()
        {
            return this._dbSet.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            this._dbSet.Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await this._dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            this._dbSet.Update(entity);
        }
    }
}