using System.ComponentModel.DataAnnotations;

namespace GameShop.Api.Dtos;

public record GameDto(
    int Id,
    [Required, MaxLength(50)] string Name,
    [Required, MaxLength(20)] string Genre,
    [Required, Range(0, 100)] decimal Price,
    DateOnly ReleaseDate,
    string ImageUri
);

public record CreateGameDto(
    [Required, MaxLength(50)] string Name,
    [Required, MaxLength(20)] string Genre,
    [Required, Range(0, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Url] string ImageUri
);

public record UpdateGameDto(
    [Required, MaxLength(50)] string Name,
    [Required, MaxLength(20)] string Genre,
    [Required, Range(0, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Url] string ImageUri
);