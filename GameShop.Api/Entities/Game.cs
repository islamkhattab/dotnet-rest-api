using System.ComponentModel.DataAnnotations;

namespace GameShop.Api.Entities;

public record class Game
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    [MaxLength(20)]
    public required string Genre { get; set; }
    [Range(0,100)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public required string ImageUri { get; set; }
}
