using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// POC NOTES: Manually added uses
using CorrelationId;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using LoggingDataLayerWEBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoggingDataLayerWEBAPI
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

            services.AddDbContext<LoggingPOCContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // POC NOTES: Enabling CorrelationId library with common defaults
            services.AddDefaultCorrelationId(options =>
            {
                options.AddToLoggingScope = true;
            });

            // POC NOTES: Configure HTTP service calls to apply Correlation Id in the header
            services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName)
                .AddCorrelationIdForwarding();

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoggingDataLayerWEBAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingDataLayerWEBAPI"));
            }

            app.UseRouting();

            app.UseAuthorization();

            // POC NOTES:
            // Enabling Correlation middleware
            // Needs to be registered before any other middleware you want access to Correlation ID
            app.UseCorrelationId();

            // POC NOTES:
            // Enabling Exception Handling middleware
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
