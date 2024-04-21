using Infrastructure_Api.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Configurations;

public static class DbContextConfig
{
    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(x=> x.UseSqlServer(configuration.GetConnectionString("LocalApiDb")));
    }
}
