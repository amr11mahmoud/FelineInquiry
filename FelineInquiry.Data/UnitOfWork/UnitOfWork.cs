using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Data.DBContext;
using FelineInquiry.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace FelineInquiry.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FelineInquiryDbContext _context;

        public IBaseRepository<User> Users { get; private set; }

        public IBaseRepository<Question> Questions { get; private set; }

        public UnitOfWork(FelineInquiryDbContext context)
        {
            _context = context;

            Users = new BaseRepository<User>(_context);
            Questions = new BaseRepository<Question>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
