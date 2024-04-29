using GameShop.Api.Data;
using GameShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories;

public class EFGamesRepository(GameShopContext dbContext, ILogger<EFGamesRepository> logger) : IGamesRepository
{
    public async Task<IEnumerable<Game>> GetAllAsync() => await dbContext.Games.AsNoTracking().ToListAsync();

    public async Task<Game?> GetAsync(int id) => await dbContext.Games.FindAsync(id);
    
    public async Task<Game> CreateAsync(Game game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Create game '{Name}' with price: {Price} and id: {Id}", game.Name, game.Price, game.Id);

        return game;
    }

    public async Task UpdateAsync(Game game)
    {
        dbContext.Update(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Games.Where(game => game.Id == id)
                             .ExecuteDeleteAsync();
    }
}