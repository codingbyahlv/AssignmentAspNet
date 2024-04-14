using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalAppDb")));
builder.Services.AddScoped<AddressManager>();

//local login
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});

//external login


var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
