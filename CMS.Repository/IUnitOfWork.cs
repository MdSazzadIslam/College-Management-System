using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;

        Task<int> CommitAsync();
        void Commit();

        void Rollback();
    }
}
