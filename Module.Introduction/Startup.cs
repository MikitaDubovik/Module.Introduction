using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Introduction.Infrastructure;
using Module.Introduction.Models;
using Module.Introduction.Services;

namespace Module.Introduction
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = Configuration.GetConnectionString("NorthwindContext");
            LoggerFactory.AddFile("Logs/ConnectionString.txt");
            var logger = LoggerFactory.CreateLogger("Startup");

            logger.LogInformation($"ConnectionString = {connectionString}");
            services.AddDbContext<NorthwindContext>(options => 
                options.UseSqlServer(connectionString));

            services.Configure<ApplicationSettings>(Configuration);

            services.AddScoped<ICategoriesService, CategoriesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
