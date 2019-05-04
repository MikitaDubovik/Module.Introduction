using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Introduction.Contexts;
using Module.Introduction.Models;

[assembly: HostingStartup(typeof(Module.Introduction.Areas.Identity.IdentityHostingStartup))]
namespace Module.Introduction.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}