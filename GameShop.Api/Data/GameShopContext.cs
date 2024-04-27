using System.Reflection;
using GameShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Data;

public class GameShopContext(DbContextOptions<GameShopContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}