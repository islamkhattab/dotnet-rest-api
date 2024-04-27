using System.Reflection;
using GameShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Data;

public class GameShopContext(DbContextOptions<GameShopContext> options) : DbContext(options)
{
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Game> Games => Set<Game>();
}