using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
}
