using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public sealed class UpdateFilmeDto
{
    [Required, MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;
    [Required, MaxLength(200)]
    public string Diretor { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Genero { get; set; }
    [Range(1, 600)]
    public int Duracao { get; set; }
}