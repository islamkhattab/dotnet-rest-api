using GameShop.Api.Authorization;
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
        group.MapGet("/", async (IGamesRepository repository) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(game => game.AsDto()));
        });

        // GET /games/1
        group.MapGet("/{id}", async (int id, IGamesRepository repository) =>
        {
            var game = await repository.GetAsync(id);

            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        })
        .WithName(GetGameEndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        // POST /games
        group.MapPost("/", async (CreateGameDto gameDto, IGamesRepository repository) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Price = gameDto.Price,
                Genre = gameDto.Genre,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            game = await repository.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { Id = game.Id }, gameDto);
        })
        .RequireAuthorization(Policies.WriteAccess);

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGameDto, IGamesRepository repository) =>
        {
            var existingGame = await repository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }



            existingGame.Name = updatedGameDto.Name;
            existingGame.Price = updatedGameDto.Price;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            await repository.UpdateAsync(existingGame);

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);


        // DELETE /games
        group.MapDelete("/{id}", async (int id, IGamesRepository repository) =>
        {
            await repository.DeleteAsync(id);

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        return group;
    }
}