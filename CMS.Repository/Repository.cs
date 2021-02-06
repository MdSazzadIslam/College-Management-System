using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppContext _context;
        public Repository(AppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add list of entity to database
        /// after using this method use _context.SaveChanges();
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return  await _context.Set<T>().CountAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            var exist = _context.Set<T>().Where(predicate);
            return exist.Any();
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public T Get(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Create();
            _context.Set<T>().Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            //_context.Set<T>().Attach(entity);
            //_context.Entry(entity).State = EntityState.Added;
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public void SaveChanges()
        {
             _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void RemoveRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<T> InsertAndReturnAsync(T entity)
        {
            _context.Set<T>().Add(entity);
             await _context.SaveChangesAsync();
            return entity;
        }

        public T InsertAndReturn(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void LoadReference(T entity,params Expression<Func<T, object>>[] refferences)
        {
            if (refferences.Any())
            {
                foreach (var item in refferences)
                {
                    _context.Entry(entity).Reference(item).Load();
                }
            }
        }

        public DbEntityEntry<T> GetTable(T entity)
        {
            return _context.Entry(entity);

        }

        public  Task<List<T>> SQL(string query)
        {
            
            return  _context.Database.SqlQuery<T>(query).ToListAsync();
        }

        #region .net Core feature
        public IQueryable<T> GetAllWithInclude(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            dbQuery = dbQuery
                .AsNoTracking()
                .Where(filter);

            return dbQuery;
        }

        public T GetSingleInclude(Func<T, bool> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(filter); //Apply where clause
            return item;
        }
        #endregion
    }
}
