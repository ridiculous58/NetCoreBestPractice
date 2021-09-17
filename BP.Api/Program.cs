using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
{
    public class Program
    {
        private static IConfiguration Configuration
        {
            get
            {
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false) //  appsettings.Development yok ise o zaman appsetting.json oku
                    .AddJsonFile($"appsettings.{env}.json", optional: true) // git bak uygulamada appsettings.Development var mý  var ise bunu oku appsetting.json goz ardý et
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                //.WriteTo.Debug(Serilog.Events.LogEventLevel.Information)
                //.WriteTo.File("Logs.txt")
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() 
            // 1. Sistem üzerindeki butun loglarý sadece serilog gondermek uzere loglama iþlemi yapabiliyoruz
            // 2. Serilog u .net core un kendi loglama mekanizmasýnda kullanabiliyoruz .net core'un kendi loglari bile serilog aracýlýgý ile kayýt edilir.
            //.ConfigureLogging(conf =>
            //{
            //    conf.ClearProviders(); // eðer daha onceden loggin provider eklenmiþ ise onlarý siler
            //    conf.SetMinimumLevel(LogLevel.Debug);
            //    conf.AddConsole();

            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
