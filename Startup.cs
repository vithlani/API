using CourseCRUD.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseCRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy("_Allowed", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            // services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("_Allowed"));
            });
            services.AddDbContext<CourseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("_Allowed");
            app.UseAuthorization();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal class CorsAuthorizationFilterFactory : IFilterMetadata
    {
        private string v;

        public CorsAuthorizationFilterFactory(string v)
        {
            this.v = v;
        }
    }
}
