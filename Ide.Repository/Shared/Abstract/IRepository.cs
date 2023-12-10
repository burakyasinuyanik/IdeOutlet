using Ide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Repository.Shared.Abstract
{
    public interface IRepository<T> where T : BaseModel
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Guid guid);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllDeleted();
        IQueryable<T> GetAllDelFalseAndTrue();
        IQueryable<T> GetAllDeleted(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T GetByGuid(Guid id);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}
