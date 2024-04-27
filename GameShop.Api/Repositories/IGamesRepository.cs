using GameShop.Api.Entities;

namespace GameShop.Api.Repositories;

public interface IGamesRepository
{
    public Task<IEnumerable<Game>> GetAllAsync();
    public Task<Game?> GetAsync(int id);
    public Task<Game> CreateAsync(Game game);
    public Task UpdateAsync(Game game);
    public Task DeleteAsync(int id);
}