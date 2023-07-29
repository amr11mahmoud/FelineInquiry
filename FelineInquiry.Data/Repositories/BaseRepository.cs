using FelineInquiry.Core.Interfaces;
using FelineInquiry.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected FelineInquiryDbContext _felineInquiryDbContext;

        public BaseRepository(FelineInquiryDbContext felineInquiryDbContext)
        {
            _felineInquiryDbContext = felineInquiryDbContext;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> match)
        {
            T entity = await _felineInquiryDbContext.Set<T>().SingleOrDefaultAsync(match);
            return entity;
        }

        public T? Find(Expression<Func<T, bool>> match)
        {
            T entity = _felineInquiryDbContext.Set<T>().SingleOrDefault(match);
            return entity;
        }

        public T Add(T entity)
        {
            _felineInquiryDbContext.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _felineInquiryDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _felineInquiryDbContext.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _felineInquiryDbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public void Delete(T entity)
        {
            _felineInquiryDbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _felineInquiryDbContext.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return _felineInquiryDbContext.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _felineInquiryDbContext.Set<T>().ToListAsync();
        }

        public T GetById(Guid id)
        {
            return _felineInquiryDbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _felineInquiryDbContext.Set<T>().FindAsync(id);
        }

        public T Update(T entity)
        {
            _felineInquiryDbContext.Update(entity);
            return entity;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
