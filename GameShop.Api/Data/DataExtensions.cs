using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Data;

public static class DataExtensions
{
    public static void IntializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameShopContext>();
        dbContext.Database.Migrate();
    }
}