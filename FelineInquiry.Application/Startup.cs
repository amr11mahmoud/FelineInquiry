using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            services.AddControllers();
            //services.AddDbContext<MangementSystemDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("Defualt")));
            services.AddSwaggerGen(C =>
            {
                C.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "FelineInquiry Public API", Version = "v1" });
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FelineInquiry Public API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
