using FelineInquiry.Core.DTOs.Users;
using FelineInquiry.Core.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Domain.Services.Abstract
{
    public interface IUserManager<T> : IBaseManager<T> where T : class
    {
        Task<(IdentityResult? result, User? user)> CreateUserAsync(RegisterDto input);
        Task<(IdentityResult? result, User? user, UserToken? tokens)> LoginUserAsync(LoginDto input);
        Task<(IdentityResult? result, User? user)> UpdateUserAsync(UpdateUserDto input, string userId);
        Task<User?> FindUserByIdAsync(string id);
        Task<IdentityResult> DeleteUserAsync(string id);
    }
}
