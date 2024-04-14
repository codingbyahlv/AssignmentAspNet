using Infrastructure_Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Api.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<SubscriberEntity> Subscribers { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
}
