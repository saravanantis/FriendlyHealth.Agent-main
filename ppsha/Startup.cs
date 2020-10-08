using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ppsha.Models;
using ppsha.Services;

namespace ppsha
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(4);//You can set Time   
            });
            var myConstants = Configuration.GetSection("MySettings");
            services.Configure<MySettings>(myConstants);
            var appSettings = myConstants.Get<MySettings>();
            var baseUrl = Environment.GetEnvironmentVariable(appSettings.APIConnectionString);

            services.AddSingleton(new AppSettings(baseUrl));
            services.AddMvc();
            services.AddSingleton<PPSHAService>();
            services.AddSingleton<IHostedService, PPSHAService>(serviceProvider => serviceProvider.GetService<PPSHAService>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("PPSHA BackGround Service Log Started");

            var myConstants = Configuration.GetSection("MySettings");
            var appSettings = myConstants.Get<MySettings>();
            var baseUrl = Environment.GetEnvironmentVariable(appSettings.APIConnectionString);
            MySettings mySettings = new MySettings();
            mySettings = appSettings;
            mySettings.BaseURL = baseUrl;
            GlobalStatic._MySettings = mySettings;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Textract Worker is Running...!");
            });
        }
    }
}
