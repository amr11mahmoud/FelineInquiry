using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Core.Models.Global;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FelineInquiry.Application.Interfaces.Abstract
{
    public interface IBaseController
    {
        ObjectResult MapErrorResultToValidHttpErrorResponse(ResultMessage result);
        Task<(ResultMessage result, User user)> GetCurrentUserAsync(ClaimsPrincipal claims);
    }
}
