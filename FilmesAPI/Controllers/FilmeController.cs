using FilmesAPI.Data.DTO;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("api/filmes")]
public sealed class FilmeController(IFilmeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ReadFilmeDto>>> Listar(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100) return BadRequest("page deve ser >= 1 e pageSize entre 1 e 100.");
        return Ok(await service.ListarPaginadoAsync(page, pageSize, ct));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReadFilmeDto>> Obter(int id, CancellationToken ct)
    {
        var filme = await service.ObterAsync(id, ct);
        return filme is null ? NotFound() : Ok(filme);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ReadFilmeDto>> Criar(CreateFilmeDto request, CancellationToken ct)
    {
        var filme = await service.CriarAsync(request, ct);
        return CreatedAtAction(nameof(Obter), new { id = filme.Id }, filme);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, UpdateFilmeDto request, CancellationToken ct) =>
        await service.AtualizarAsync(id, request, ct) ? NoContent() : NotFound();

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id, CancellationToken ct) =>
        await service.ExcluirAsync(id, ct) ? NoContent() : NotFound();
}