using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using Microsoft.Extensions.Logging;

namespace SampleWebApp
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
            services.AddControllersWithViews();
            AddDbContext(services);
           
        }
        private void AddDbContext(IServiceCollection services)
        {
            string databaseTech = Configuration.GetValue<string>("DatabaseTech");

            switch (databaseTech)
            {
                case "InMemory":
                    AddDbContextInMemory(services);
                    break;
                case "NpgSql":
                    AddDbContextNpgSql(services);
                    break;
                default:
                    throw new ArgumentException($"Configuration value DatabaseTech is invalid: {databaseTech}. Must be InMemory or NpgSql");
            }
        }

        private void AddDbContextInMemory(IServiceCollection services)
        {

            services.AddDbContext<SampleWebAppContext>(options =>
                    options.UseInMemoryDatabase($"SampleWebApp"));
        }

        private void AddDbContextNpgSql(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("SampleWebAppContext");

            services.AddDbContext<SampleWebAppContext>(options =>
                   options.UseNpgsql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            string databaseTech = Configuration.GetValue<string>("DatabaseTech");
            logger.LogInformation("DatabaseTech: {0}", databaseTech);

            string connectionString = Configuration.GetConnectionString("SampleWebAppContext");
            logger.LogInformation("ConnectionString: {0}", connectionString);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
