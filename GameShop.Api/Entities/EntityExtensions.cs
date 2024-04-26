using GameShop.Api.Dtos;

namespace GameShop.Api.Entities;

public static class EntityExtensions
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri);
    }

    public static CreateGameDto AsCreateDto(this Game game)
    {
        return new CreateGameDto(
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri);
    }

    public static UpdateGameDto AsUpdateDto(this Game game)
    {
        return new UpdateGameDto(
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri);
    }

}