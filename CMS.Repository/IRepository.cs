using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Query();

        /// <summary>
        /// Find entity by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(long id);
        Task<T> GetAsync(long id);
        void Insert(T entity);
        Task InsertAsync(T entity);
        Task<T> InsertAndReturnAsync(T entity);
        T InsertAndReturn(T entity);
        void Update(T entity);
        void UpdateRange(List<T> entities);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        void AddRange(List<T> entities);
        [Obsolete("Single repository pattern merged with unit of work. Instade of this use _uow.Commit()")]
        void SaveChanges();
        [Obsolete("Single repository pattern merged with unit of work. Instade of this use _uow.CommitAsync()")]
        Task SaveChangesAsync();

        /// <summary>
        /// </summary>
        /// <param name="match"></param>
        /// <returns>returns Single or Default of Entity</returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// </summary>
        /// <param name="match"></param>
        /// <returns>returns Single or Default of Entity</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        bool Exist(Expression<Func<T, bool>> predicate);

        int Count();
        Task<int> CountAsync();

        void RemoveRange(List<T> entities);

        DbEntityEntry<T> GetTable(T entity);
        Task<List<T>> SQL(string query);
   }
}
