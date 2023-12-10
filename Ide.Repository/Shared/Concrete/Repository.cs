using Ide.Data;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Repository.Shared.Concrete
{
    public class Repository<T>:IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context=context;
            _dbSet=_context.Set<T>();
        }
        public void Add(T entity)
        {
           _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted=true;
            entity.IsActive = false;
            entity.DateModified=DateTime.Now;
            entity.DateDeleted = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void Delete(Guid guid)
        {
            
            Delete(_dbSet.FirstOrDefault(t => t.Guid == guid));
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach(T entiti in entities)
            {
                Delete(entiti);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(t => t.IsDeleted == false);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<T> GetAllDeleted()
        {
            return _dbSet.Where(t => t.IsDeleted == true);

        }

        public IQueryable<T> GetAllDeleted(Expression<Func<T, bool>> predicate)
        {
            return GetAllDeleted().Where(predicate);
        }

        public IQueryable<T> GetAllDelFalseAndTrue()
        {
            return _dbSet.AsQueryable();
        }

        public T GetByGuid(Guid id)
        {
            return _dbSet.FirstOrDefault(t => t.Guid == id);
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id);

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            entity.DateModified = DateTime.Now;
            _dbSet.Update(entity);
        }
    }
}
