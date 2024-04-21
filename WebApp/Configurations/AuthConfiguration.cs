using Infrastructure.Context;
using Infrastructure.Entities;

namespace WebApp.Configurations;

public static class AuthConfiguration
{
    public static void RegisterAuthentications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<UserEntity>(x =>
        {
            x.User.RequireUniqueEmail = true;
            x.SignIn.RequireConfirmedAccount = false;
            x.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<AppDbContext>();

        services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/signin";
            x.Cookie.HttpOnly = true;
            x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            x.SlidingExpiration = true;
        });

        services.AddAuthentication()
            .AddFacebook(x =>
            {
                x.AppId = configuration["FacebookAuth:AppId"]!;
                x.AppSecret = configuration["FacebookAuth:AppSecret"]!;
                x.Fields.Add("first_name");
                x.Fields.Add("last_name");
            })
            .AddGoogle(x =>
            {
                x.ClientId = configuration["GoogleAuth:ClientId"]!;
                x.ClientSecret = configuration["GoogleAuth:ClientSecret"]!;
            });
    }
}
