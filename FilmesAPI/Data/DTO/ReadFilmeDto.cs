namespace FilmesAPI.Data.DTO;

public sealed class ReadFilmeDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string? Genero { get; set; }
    public int Duracao { get; set; }
}