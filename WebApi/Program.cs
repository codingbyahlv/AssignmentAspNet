using Infrastructure_Api.Context;
using Infrastructure_Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalApiDb")));
builder.Services.AddScoped<SubscriberService>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
