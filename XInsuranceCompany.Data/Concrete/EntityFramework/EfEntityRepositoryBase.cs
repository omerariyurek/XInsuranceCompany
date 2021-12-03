using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XInsuranceCompany.Core.DataAccess;
using XInsuranceCompany.Core.Entities.Common;
using XInsuranceCompany.Data.Contexts;

namespace XInsuranceCompany.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity>
            : IEntityRepository<TEntity>
            where TEntity : class, IBaseEntity
    {
        protected readonly DbContext _context;

        public EfEntityRepositoryBase(XInsuranceCompanyDbContext context)
        {
            _context = context;
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = null)
        {
            using var context =  _context;
            var query = context.Set<TEntity>().AsQueryable();

            if (string.IsNullOrEmpty(includeProperties))
                return query.FirstOrDefault(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(filter);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = _context;
            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }

        public void Add(TEntity entity)
        {
            using var context = _context;
            context.Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using var context = _context;
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = _context;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            using var context = _context;
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            await using var context = _context;
            return expression == null ? await context.Set<TEntity>().ToListAsync() :
                await context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            using var context = _context;
            var query = context.Set<TEntity>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            await using var context = _context;
            var query = context.Set<TEntity>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string includeProperties = null)
        {
            await using var context = _context;
            var query = context.Set<TEntity>().AsQueryable();

            if (string.IsNullOrEmpty(includeProperties))
                return await query.FirstOrDefaultAsync(expression);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public IQueryable<TEntity> Query()
        {
            using var context = _context;
            return context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await using var context = _context;
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
