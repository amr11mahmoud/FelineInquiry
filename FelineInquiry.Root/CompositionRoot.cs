using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Roles;
using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Data.DBContext;
using FelineInquiry.Data.Repositories;
using FelineInquiry.Domain.Services.Abstract;
using FelineInquiry.Domain.Services.IdentityServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FelineInquiry.Core.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FelineInquiry.Domain.Services.Questions;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Data.UnitOfWork;

namespace FelineInquiry.Root
{
    public class CompositionRoot
    {
        public CompositionRoot()
        {
            
        }

        public static void injectDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<FelineInquiryDbContext>(
                opts => opts.UseSqlServer(Configuration.GetConnectionString("Defualt"),
                b => b.MigrationsAssembly(typeof(FelineInquiryDbContext).Assembly.FullName)));

            // Enable Identity
            // Application and domain level
            services.AddIdentity<User, Role>( options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<FelineInquiryDbContext>()
                .AddDefaultTokenProviders()
                // Repository (data access) level
                .AddUserStore<UserStore<User, Role, FelineInquiryDbContext, Guid>>()
                .AddRoleStore<RoleStore<Role, FelineInquiryDbContext, Guid>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                byte[]? securityKey = Encoding.ASCII.GetBytes(Configuration["Authentication:JwtBearer:SecurityKey"]);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:JwtBearer:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidAudience = Configuration["Authentication:JwtBearer:Audience"]
                };

            }).AddTwitter(options =>
            {
                IConfigurationSection twitterAuthSection = Configuration.GetSection("Authentication:Twitter");
                options.ConsumerKey = twitterAuthSection["ConsumerKey"];
                options.ConsumerSecret = twitterAuthSection["ConsumerSecret"];
            });

            services.AddAutoMapper(typeof(MapperConfig));

            // Core and Data Services
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            // Domain Services
            services.AddTransient(typeof(IUserManager<User>), typeof(UsersManager));
            services.AddTransient(typeof(IQuestionsManager<Question>), typeof(QuestionsManager));

        }
    }
}