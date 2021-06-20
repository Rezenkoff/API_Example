using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Autodoc.CatalogAdmin.API
{
    public class Program
    {
        public static void Main (string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost (string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var apiHostingUrl = config.GetSection("AppSettings:ApiHostingUrl");

            var host = CreateWebHostBuilder(args)                    
                    .UseUrls(apiHostingUrl?.Value)
                    .Build();

            return host;
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}


