using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// POC NOTES: Manually added uses
using Serilog;
using NewRelic.LogEnrichers.Serilog;

namespace LoggingDataLayerWEBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // POC NOTES:
            // Configuring Serilog to use NewRelic Enrichers
            // Configuring Serilog to use json flat file by NewRelic
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostContext, LoggerConfiguration) =>
                {
                    LoggerConfiguration
                        .Enrich.WithThreadName()
                        .Enrich.WithThreadId()
                        .Enrich.WithNewRelicLogsInContext()
                        .WriteTo.File(
                            formatter: new NewRelicFormatter(),
                            path: @"C:\logs\LoggingDataLayerWEBAPI.log.json");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
