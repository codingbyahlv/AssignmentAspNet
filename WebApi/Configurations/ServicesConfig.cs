using Infrastructure_Api.Services;

namespace WebApi.Configurations;

public static class ServicesConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SubscriberService>();
        services.AddScoped<CourseService>();
        services.AddScoped<ContactService>();
    }
}
