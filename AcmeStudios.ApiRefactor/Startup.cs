using AcemStudios.ApiRefactor.Data;

using AcmeStudios.ApiRefactor.Contracts;
using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Entities;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcemStudios.ApiRefactor
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
            services.AddDbContext<StudioDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StudioConnection"));
            });

            services.AddCors(options =>
             {
                 options.AddPolicy("AllowMyOrigin",
                 builder => builder.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 );
             });

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IRepository<StudioItem>, StudioRepository<StudioItem>>();
            services.AddScoped<IRepository<StudioItemType>, StudioRepository<StudioItemType>>();
            services.AddScoped<IInterfaceWithDatabase, InterfaceWithDatabase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowMyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireCors("AllowMyOrigin");
            });
        }
    }
}
