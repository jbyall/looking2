using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Looking2.Web.DataAccess;
using Looking2.Web.Domain;
using Microsoft.AspNetCore.Identity;
using Looking2.Web.Settings;
using Looking2.Web.ViewModels;

namespace Looking2.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // mongodb identity
            services.AddIdentityWithMongoStores(Configuration.GetConnectionString("Looking2DbConnection")).AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            // App Settings
            services.AddOptions();
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            // Dependency injection. This creates a new instance for each HTTP request.
            // Allows creating controller ctors with params
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();

            // Creates a single instance of this for the entire application
            // Allows appsettings (defined in Startup ctor) to be passed to controller
            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Welcome}/{id?}");
            });
        }

    }
}
