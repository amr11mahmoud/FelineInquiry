using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        //Get
        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        //Find
        Task<T?> FindAsync(Expression<Func<T,bool>> match);
        T? Find(Expression<Func<T,bool>> match);

        //Add
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        //Update
        T Update(T entity);

        //Delete
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
