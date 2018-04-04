using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.IO;


namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Identity Provider";
            var logFile = Path.Combine(Directory.GetCurrentDirectory(), "IdentityServerLogs.txt");

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.File(logFile)
                    .WriteTo.Console()
                    .CreateLogger();

            try
            {
                Log.Information($"Starting Identity Provider Server App ... at {DateTime.Now}");
                Log.Information($"Identity Provider Server App started successfully ... at {DateTime.Now}");
                BuildWebHost(args).Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Host terminated unexpectedly at {DateTime.Now} ... server shutting down.");                
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
