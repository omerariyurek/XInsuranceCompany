using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XInsuranceCompany.Core.Entities.Common;

namespace XInsuranceCompany.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IBaseEntity
    {
        T Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        Task<T> AddAsync(T entity);

        void Delete(T entity);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, string includeProperties = null);
        IQueryable<T> Query();
    }
}
