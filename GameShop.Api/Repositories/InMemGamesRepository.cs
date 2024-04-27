using GameShop.Api.Entities;

namespace GameShop.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateOnly(1991, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 9, 27),
            ImageUri = "https://placehold.co/100"
        }
    };

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(x => x.Id == id));
    }

    public async Task<Game> CreateAsync(Game game)
    {
        game.Id = games.Max(x => x.Id) + 1;
        games.Add(game);
        return await Task.FromResult(game);
    }

    public async Task UpdateAsync(Game game)
    {
        var index = games.FindIndex(x => x.Id == game.Id);

        games[index] = game;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        games.RemoveAll(x => x.Id == id);
        await Task.CompletedTask;
    }
}