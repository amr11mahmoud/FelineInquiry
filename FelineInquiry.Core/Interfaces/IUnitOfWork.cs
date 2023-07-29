using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<Question> Questions { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
