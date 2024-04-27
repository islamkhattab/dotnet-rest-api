using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Data;

public static class DataExtensions
{
    public static async Task IntializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameShopContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var GameShopDBConnectionString = configuration.GetConnectionString("GameShopDB");

        services.AddSqlServer<GameShopContext>($"{GameShopDBConnectionString}")
                .AddScoped<IGamesRepository, EFGamesRepository>(); ;

        return services;
    }
}