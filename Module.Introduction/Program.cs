using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Module.Introduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            var rootFolder = Directory.GetCurrentDirectory();

            var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/S2.txt");
            var logger = loggerFactory.CreateLogger("Program");
            logger.LogInformation($"Beginning of the programm in - {rootFolder}");

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.Development.json",
                        optional: false,        // File is not optional.
                        reloadOnChange: false);
                })
                .UseStartup<Startup>();
    }
}
