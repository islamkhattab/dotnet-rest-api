using GameShop.Api.Dtos;
using GameShop.Api.Entities;
using GameShop.Api.Repositories;

namespace GameShop.Api.Endpoints;

public static class GameStoreEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder RegisterGameStoreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                       .WithParameterValidation();

        // GET /games
        group.MapGet("/", (IGamesRepository repository) =>
        {
            return Results.Ok(repository.GetAll().Select(game => game.AsDto()));
        });

        // GET /games/1
        group.MapGet("/{id}", (int id, IGamesRepository repository) =>
        {
            var game = repository.Get(id);

            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto gameDto, IGamesRepository repository) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Price = gameDto.Price,
                Genre = gameDto.Genre,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            game = repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { Id = game.Id }, gameDto);
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGameDto, IGamesRepository repository) =>
        {
            var existingGame = repository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }



            existingGame.Name = updatedGameDto.Name;
            existingGame.Price = updatedGameDto.Price;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            repository.Update(existingGame);

            return Results.NoContent();
        });


        // DELETE /games
        group.MapDelete("/{id}", (int id, IGamesRepository repository) =>
        {
            repository.Delete(id);

            return Results.NoContent();
        });

        return group;
    }
}