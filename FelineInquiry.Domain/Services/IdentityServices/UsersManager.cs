using AutoMapper;
using FelineInquiry.Core.DTOs.Users;
using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Domain.Services.Abstract;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Domain.Services.IdentityServices
{
    // example of how to set properties for request completion log
    //_diagnosticContext.Set("User:", user.ToString());

    public class UsersManager : IUserManager<User>
    {
        private readonly IBaseRepository<User> _baseRepository;
        private readonly UserManager<User> _identityUserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly IdentityErrorDescriber _identityErrorDescriber;
        private readonly IConfiguration _configuration;

        public UsersManager(IBaseRepository<User> baseRepository, 
            IdentityErrorDescriber identityErrorDescriber,
            UserManager<User> identityUserManager,
            SignInManager<User> signInManager,
            IMapper mapper, 
            IConfiguration configuration,
            IDiagnosticContext diagnosticContext)
        {
            _baseRepository = baseRepository;
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _diagnosticContext = diagnosticContext;
            _identityErrorDescriber = identityErrorDescriber;
            _configuration = configuration;
        }

        public async Task<(IdentityResult? result, User? user)> CreateUserAsync(RegisterDto input)
        {
            User user = _mapper.Map<User>(input);

            IdentityResult result = await _identityUserManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                return (result, null);
            }

            var userClaims = new List<Claim>()
            {
                new Claim("FirstName", input.FirstName),
                new Claim("LastName", input.LastName),
                new Claim("Email", input.Email),
            };
            
            result = await _identityUserManager.AddClaimsAsync(user, userClaims);

            if (!result.Succeeded)
            {
                return (result, null);
            }

            return (null, user);
        }

        public async Task<(IdentityResult? result, User? user, UserToken? tokens)> LoginUserAsync(LoginDto input)
        {
            User? user = await _identityUserManager.FindByEmailAsync(input.UserNameOrEmail);

            if (user == null)
            {
                user = await _identityUserManager.FindByNameAsync(input.UserNameOrEmail);

                if (user == null)
                {
                    return (null, null, null);
                }
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (!signInResult.Succeeded)
            {
                return (IdentityResult.Failed(_identityErrorDescriber.PasswordMismatch()), null, null);
            }

            UserToken tokens = new UserToken
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = GenerateRefreshToken(user),
                IsEmailConfirmed = user.EmailConfirmed
            };

            return (null, user, tokens);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            User? user = await FindUserByIdAsync(id);

            if (user == null)
            {
                return IdentityResult.Failed(_identityErrorDescriber.InvalidToken());
            }

            IdentityResult result = await _identityUserManager.DeleteAsync(user);

            return result;
        }

        public async Task<User?> FindUserByIdAsync(string id)
        {
            User? user = await _identityUserManager.FindByIdAsync(id.ToString());

            return user;
        }

        public async Task<(IdentityResult? result,User? user)> UpdateUserAsync(UpdateUserDto input, string userId)
        {
            User? user = await _identityUserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (IdentityResult.Failed(_identityErrorDescriber.InvalidToken()), null);
            }

            if (user.Email!= input.Email)
            {
                return (IdentityResult.Failed(_identityErrorDescriber.InvalidEmail(input.Email)), null);
            }

            user.FirstName = input.FirstName; 
            user.LastName = input.LastName;
            user.UserName = input.UserName;
            user.PhoneNumber = input.PhoneNumber;

            IdentityResult identityResult = await _identityUserManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                return (identityResult, null);
            }

            return (null, user);
        }

        private JWTToken GenerateAccessToken(User user)
        {
            byte[]? securityKey = Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]);
            var credientials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddDays(7);

            var claims = new List<Claim> {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString().ToUpper()),
                new Claim(JwtClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                expires: expiration,
                signingCredentials: credientials,
                claims: claims
            ));

            return new JWTToken { Token = token, Expiration = expiration };
        }

        private JWTToken GenerateRefreshToken(User user)
        {
            byte[]? securityKey = Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:RefreshSecurityKey"]);
            var credientials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha384);

            var expiration = DateTime.UtcNow.AddDays(30);

            var claims = new List<Claim> {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString().ToUpper()),
                new Claim(JwtClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                expires: expiration,
                signingCredentials: credientials,
                claims: claims
            ));

            // TODO Store in Database

            return new JWTToken { Token = token, Expiration = expiration};
        }
    }
}
