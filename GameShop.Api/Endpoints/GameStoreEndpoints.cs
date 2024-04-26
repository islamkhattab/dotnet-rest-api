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
            return Results.Ok(repository.GetAll());
        });

        // GET /games/1
        group.MapGet("/{id}", (int id, IGamesRepository repository) =>
        {
            var game = repository.Get(id);

            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (Game newGame, IGamesRepository repository) =>
        {
            newGame = repository.Create(newGame);

            return Results.CreatedAtRoute(GetGameEndpointName, new { Id = newGame.Id }, newGame);
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, Game updatedGame, IGamesRepository repository) =>
        {
            var existingGame = repository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            repository.Update(updatedGame);

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