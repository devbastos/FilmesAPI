using FilmesAPI.Data.DTO;

namespace FilmesAPI.Services;

public interface IFilmeService
{
    Task<IReadOnlyList<ReadFilmeDto>> ListarAsync(CancellationToken ct);
    Task<PagedResult<ReadFilmeDto>> ListarPaginadoAsync(int page, int pageSize, CancellationToken ct);
    Task<ReadFilmeDto?> ObterAsync(int id, CancellationToken ct);
    Task<ReadFilmeDto> CriarAsync(CreateFilmeDto request, CancellationToken ct);
    Task<bool> AtualizarAsync(int id, UpdateFilmeDto request, CancellationToken ct);
    Task<bool> ExcluirAsync(int id, CancellationToken ct);
}