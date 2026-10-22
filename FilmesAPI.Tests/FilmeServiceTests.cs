using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FilmesAPI.Tests;

public class FilmeServiceTests
{
    private static FilmeContext CriarContexto() =>
        new(new DbContextOptionsBuilder<FilmeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    [Fact]
    public async Task CriarEConsultar_DeveRetornarFilme()
    {
        await using var db = CriarContexto();
        var service = new FilmeService(db);
        var criado = await service.CriarAsync(new CreateFilmeDto
        {
            Titulo = "Interestelar", Diretor = "Christopher Nolan", Duracao = 169
        }, CancellationToken.None);

        var consultado = await service.ObterAsync(criado.Id, CancellationToken.None);
        Assert.NotNull(consultado);
        Assert.Equal("Interestelar", consultado.Titulo);
    }

    [Fact]
    public async Task AtualizarInexistente_DeveRetornarFalse()
    {
        await using var db = CriarContexto();
        var service = new FilmeService(db);
        Assert.False(await service.AtualizarAsync(999, new UpdateFilmeDto
        {
            Titulo = "Teste", Diretor = "Teste", Duracao = 100
        }, CancellationToken.None));
    }

    [Fact]
    public async Task Excluir_DeveRemoverFilme()
    {
        await using var db = CriarContexto();
        var service = new FilmeService(db);
        var criado = await service.CriarAsync(new CreateFilmeDto
        {
            Titulo = "Filme", Diretor = "Diretor", Duracao = 100
        }, CancellationToken.None);

        Assert.True(await service.ExcluirAsync(criado.Id, CancellationToken.None));
        Assert.Null(await service.ObterAsync(criado.Id, CancellationToken.None));
    }
}