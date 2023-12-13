using System.Linq;
using AcemStudios.ApiRefactor.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcemStudios.ApiRefactor
{
    public class Program
    {
        public static void Main(string[] args)
        {
        

            var app = CreateHostBuilder(args).Build();
    
            app.Run();
            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
