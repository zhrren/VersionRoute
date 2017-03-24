using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Core;
using NLog.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;

namespace Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("settings/appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"settings/appsettings.{env.EnvironmentName.ToLower()}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<Settings>(Configuration.GetSection("settings"));

            // Add framework services.
            services.AddMvc();

            services.AddCors();

            services.AddTransient<IVersionSettingsService, VersionSettingsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            var nlogPath = Path.Combine(env.ContentRootPath, $"settings/nlog.{env.EnvironmentName.ToLower()}.config");
            if (File.Exists(nlogPath))
                loggerFactory.ConfigureNLog($"settings/nlog.{env.EnvironmentName.ToLower()}.config");
            else
                loggerFactory.ConfigureNLog("settings/nlog.config");

            app.UseMvc();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
