using GameShop.Api.Data;
using GameShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories;

public class EFGamesRepository(GameShopContext dbContext) : IGamesRepository
{
    public IEnumerable<Game> GetAll() => dbContext.Games.AsNoTracking().ToList();

    public Game? Get(int id) => dbContext.Games.Find(id);

    public Game Create(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
        return game;
    }

    public void Update(Game game)
    {
        dbContext.Update(game);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.Games.Where(game => game.Id == id)
                       .ExecuteDelete();
    }
}