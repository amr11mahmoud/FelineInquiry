using FelineInquiry.Application.Filters;
using FelineInquiry.Application.Interfaces.Abstract;
using FelineInquiry.Application.Services;
using FelineInquiry.Core.Interfaces;
using FelineInquiry.Data.UnitOfWork;
using FelineInquiry.Root;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace FelineInquiry.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // built-in logging 
            // Specify HTTPLogging Proprties

            services.AddHttpLogging(options =>
            {
                // Customize logging fields as needed
                options.LoggingFields =
                Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
                Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });


            services.AddControllers(options =>
            {
                // global filter to handel exceptions
                options.Filters.Add<GlobalExceptionFilter>();
                options.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson() // this replace the default built-in JSON input-output formatters
            .AddXmlDataContractSerializerFormatters();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(C =>
            {
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                C.IncludeXmlComments(xmlCommentsFullPath);

                C.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "FelineInquiry Public API v1.0", Version = "v1" });

                C.AddSecurityDefinition("FelineInquiryApiAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Input Valid Access token to access this API"
                });

                C.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "FelineInquiryApiAuth"
                            } 
                        }, new List<string>()
                    }
                });
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            // Application Services
            services.AddTransient(typeof(IBaseController), typeof(BaseController));

            CompositionRoot.injectDependencies(services, Configuration);

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            //Enable Serilog diagnostics, A compeletion of request log to use in any service acorss the application
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            // HTTPLogging Middleware
            app.UseHttpLogging();

            //app.Logger.LogDebug("HELLO IM DEBUG");
            //app.Logger.LogInformation("HELLO IM INFO");
            //app.Logger.LogWarning("HELLO IM WARNING");
            //app.Logger.LogError("HELLO IM ERROR");
            //app.Logger.LogCritical("HELLO IM CRITICAL");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FelineInquiry Public API V1");
            });

            app.MapControllers();
        }
    }
}
