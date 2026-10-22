using FilmesAPI.Data.DTO;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("api/filmes")]
public sealed class FilmeController(IFilmeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReadFilmeDto>>> Listar(CancellationToken ct) =>
        Ok(await service.ListarAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReadFilmeDto>> Obter(int id, CancellationToken ct)
    {
        var filme = await service.ObterAsync(id, ct);
        return filme is null ? NotFound() : Ok(filme);
    }

    [HttpPost]
    public async Task<ActionResult<ReadFilmeDto>> Criar(CreateFilmeDto request, CancellationToken ct)
    {
        var filme = await service.CriarAsync(request, ct);
        return CreatedAtAction(nameof(Obter), new { id = filme.Id }, filme);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, UpdateFilmeDto request, CancellationToken ct) =>
        await service.AtualizarAsync(id, request, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id, CancellationToken ct) =>
        await service.ExcluirAsync(id, ct) ? NoContent() : NotFound();
}