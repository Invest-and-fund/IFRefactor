using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Interfaces;
using AcmeStudios.ApiRefactor.Profiles;
using AcmeStudios.ApiRefactor.Repositories;
using AcmeStudios.ApiRefactor.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AcmeStudios.ApiRefactor;

public class Startup
{
    private IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowMyOrigin",
                builder => builder.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        services.AddScoped<StudioItemServices>();
        services.AddScoped<IRepository<StudioItem>, StudioItemRepository>();
        services.AddScoped<IRepository<StudioItemType>, StudioItemTypeRepository>();

        services.AddControllers();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Acme Studıo Api", Version = "v1" });
        });
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddDatabaseContext(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,EFStudioDbContext dbContext)
    {
        try
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                dbContext.Database.EnsureCreated(); 
            }
            else
            {
                dbContext.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while ensuring database creation/migration: {ex.Message}");
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Acme Studıo Api");
            options.RoutePrefix = "swagger"; 
        });
        app.UseAuthorization();
        app.UseCors("AllowMyOrigin");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}