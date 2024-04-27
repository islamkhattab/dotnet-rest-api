using GameShop.Api.Entities;

namespace GameShop.Api.Repositories;

public interface IGamesRepository
{
    public IEnumerable<Game> GetAll();
    public Game? Get(int id);
    public Game Create(Game game);
    public void Update(Game game);
    public void Delete(int id);
}