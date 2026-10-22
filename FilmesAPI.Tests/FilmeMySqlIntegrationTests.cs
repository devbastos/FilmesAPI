using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MySql;
using Xunit;

namespace FilmesAPI.Tests;

public sealed class FilmeMySqlIntegrationTests : IAsyncLifetime
{
    private readonly MySqlContainer _mysql = new MySqlBuilder().WithImage("mysql:8.4")
        .WithDatabase("filmes_integration")
        .WithUsername("filmes")
        .WithPassword("filmes_test_only")
        .Build();

    public Task InitializeAsync() => _mysql.StartAsync();
    public Task DisposeAsync() => _mysql.DisposeAsync().AsTask();

    [Fact]
    public async Task Crud_DevePersistirDadosNoMySql()
    {
        var options = new DbContextOptionsBuilder<FilmeContext>()
            .UseMySql(_mysql.GetConnectionString(), ServerVersion.AutoDetect(_mysql.GetConnectionString()))
            .Options;

        await using var db = new FilmeContext(options);
        await db.Database.MigrateAsync();
        var service = new FilmeService(db);

        var criado = await service.CriarAsync(new CreateFilmeDto
        {
            Titulo = "Interestelar", Diretor = "Christopher Nolan", Duracao = 169
        }, CancellationToken.None);

        Assert.True(criado.Id > 0);
        Assert.Equal("Interestelar", (await service.ObterAsync(criado.Id, CancellationToken.None))?.Titulo);

        Assert.True(await service.AtualizarAsync(criado.Id, new UpdateFilmeDto
        {
            Titulo = "Interestelar - atualizado", Diretor = "Christopher Nolan", Duracao = 169
        }, CancellationToken.None));

        await using (var segundoContexto = new FilmeContext(options))
        {
            var novoService = new FilmeService(segundoContexto);
            Assert.Equal("Interestelar - atualizado",
                (await novoService.ObterAsync(criado.Id, CancellationToken.None))?.Titulo);
            Assert.True(await novoService.ExcluirAsync(criado.Id, CancellationToken.None));
            Assert.Null(await novoService.ObterAsync(criado.Id, CancellationToken.None));
        }
    }
}
