using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services;

public sealed class FilmeService(FilmeContext db) : IFilmeService
{
    private static ReadFilmeDto Map(Filme f) => new()
    {
        Id = f.Id, Titulo = f.Titulo, Diretor = f.Diretor,
        Genero = f.Genero, Duracao = f.Duracao
    };

    public async Task<IReadOnlyList<ReadFilmeDto>> ListarAsync(CancellationToken ct) =>
        (await db.Filmes.AsNoTracking().OrderBy(f => f.Id).ToListAsync(ct)).Select(Map).ToList();

    public async Task<ReadFilmeDto?> ObterAsync(int id, CancellationToken ct)
    {
        var filme = await db.Filmes.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id, ct);
        return filme is null ? null : Map(filme);
    }

    public async Task<ReadFilmeDto> CriarAsync(CreateFilmeDto request, CancellationToken ct)
    {
        var filme = new Filme
        {
            Titulo = request.Titulo, Diretor = request.Diretor,
            Genero = request.Genero, Duracao = request.Duracao
        };
        db.Filmes.Add(filme);
        await db.SaveChangesAsync(ct);
        return Map(filme);
    }

    public async Task<bool> AtualizarAsync(int id, UpdateFilmeDto request, CancellationToken ct)
    {
        var filme = await db.Filmes.FirstOrDefaultAsync(f => f.Id == id, ct);
        if (filme is null) return false;
        filme.Titulo = request.Titulo;
        filme.Diretor = request.Diretor;
        filme.Genero = request.Genero;
        filme.Duracao = request.Duracao;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> ExcluirAsync(int id, CancellationToken ct)
    {
        var filme = await db.Filmes.FirstOrDefaultAsync(f => f.Id == id, ct);
        if (filme is null) return false;
        db.Filmes.Remove(filme);
        await db.SaveChangesAsync(ct);
        return true;
    }
}