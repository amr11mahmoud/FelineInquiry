using FelineInquiry.Application.Interfaces.Abstract;
using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Core.Models.Global;
using FelineInquiry.Core.Models.Global.Errors;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FelineInquiry.Application.Services
{
    public class BaseController : IBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(ResultMessage result, User user)> GetCurrentUserAsync(ClaimsPrincipal claims)
        {
            ResultMessage result = new ResultMessage
            {
                Success = true,
            };

            string subId = claims.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value;

            if (subId == null)
            {
                result.Success = false;
                result.Error = new InvalidSubjectIdError();

                return (result, null);
            }

            Guid userId;
            User user;

            if (Guid.TryParse(subId, out userId))
            {
                user = await _unitOfWork.Users.GetByIdAsync(userId);

                if (user == null)
                {
                    result.Success = false;
                    result.Error = new UserNotFoundErrorResult();

                    return (result, null);
                }
            }
            else
            {
                result.Success = false;
                result.Error = new InvalidSubjectIdError();

                return (result, null);
            }

            return(result, user);
        }

        public ObjectResult? MapErrorResultToValidHttpErrorResponse(ResultMessage result)
        {
            switch (result.Error.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(
                        new
                        {
                            Error = result.Error.Title,
                            ErrorDescription = result.Error.Description,
                            IsError = true
                        });

                case System.Net.HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(
                        new
                        {
                            Error = result.Error.Title,
                            ErrorDescription = result.Error.Description,
                            IsError = true
                        });

                case System.Net.HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(
                        new
                        {
                            Error = result.Error.Title,
                            ErrorDescription = result.Error.Description,
                            IsError = true
                        });

                case System.Net.HttpStatusCode.Forbidden:
                    return new UnauthorizedObjectResult(
                        new
                        {
                            Error = result.Error.Title,
                            ErrorDescription = result.Error.Description,
                            IsError = true
                        });

                default:
                    return new BadRequestObjectResult(
                        new
                        {
                            Error = result.Error.Title,
                            ErrorDescription = result.Error.Description,
                            IsError = true
                        });
            }
        }
    }
}
