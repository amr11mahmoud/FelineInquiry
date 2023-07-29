using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Test.Services
{
    public class UserTestDataRepository:IBaseRepository<User>
    {
        private List<User> _users;

        public UserTestDataRepository()
        {
            Thread.Sleep(3000);

            _users = new()
            {
                new User("amr", "mahmoud", "amr@gmail.com", Guid.Parse("cbf6db3b-c4ee-46aa-9457-5fa8aefef33a"), true),
                new User("ahmed", "ahmed", "ahmed@gmail.com", Guid.Parse("d6e0e4b7-9365-4332-9b29-bb7bf09664a6")),
                new User("may", "may", "may@gmail.com", Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"))
            };
        }

        public User Add(User entity)
        {
           return entity;
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public User? Find(Expression<Func<User, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindAsync(Expression<Func<User, bool>> match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public User? GetById(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(GetById(id));
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
