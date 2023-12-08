using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System.IO;

namespace AcemStudios.ApiRefactor
{
    public class Startup
    {
        public Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(jsonFile, false, true);

            var config = builder.Build();

            ApiConfigurationSettings = config.GetSection("ApiConfigurationSettings").Get<ApiConfigurationSettings>();
        }

        public IConfiguration Configuration { get; }

        public static ApiConfigurationSettings ApiConfigurationSettings { get; private set; }

        public static readonly string jsonFile = "appsettings.json";

        public void ConfigureServices(IServiceCollection services)
        {
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
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
