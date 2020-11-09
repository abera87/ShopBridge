using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Infrastructure.Persistence;
using ShopBridge.Application.Common.Interfaces;

namespace ShopBridge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                            configuration.GetConnectionString("DBConnectionString"),
                            m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                            ));
            
            // use this configuration as derived dbcontext from interface
            service.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDbContext>());
            service.AddScoped<IInventoryService,InventoryService>();

            return service;
        }
    }
}