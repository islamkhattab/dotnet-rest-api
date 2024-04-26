using GameShop.Api.Entities;

namespace GameShop.Api.Endpoints;

public static class GameStoreEndpoints
{
    private const string GetGameEndpointName = "GetGame";
    public static List<Game> games = new()
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
    public static RouteGroupBuilder RegisterGameStoreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        });

        // GET /games
        group.MapGet("/{Id}", (int Id) =>
        {
            var game = games.Find(x => x.Id == Id);

            return game != null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (Game newGame) =>
        {
            newGame.Id = games.Max(game => game.Id) + 1;

            games.Add(newGame);

            return Results.CreatedAtRoute(GetGameEndpointName, new { Id = newGame.Id }, newGame);
        });

        group.MapPut("/", (Game game) =>
        {
            int index = games.FindIndex(x => x.Id == game.Id);

            if (index < 0)
            {
                return Results.NotFound();
            }

            games[index] = game;

            return Results.NoContent();
        });

        group.MapDelete("/{Id}", (int Id) =>
        {
            games.RemoveAll(x => x.Id == Id);

            return Results.NoContent();
        });

        return group;
    }
}