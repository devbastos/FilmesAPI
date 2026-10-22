using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/filmes")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ReadFilmeDto>> AdicionaFilme(
            [FromBody] CreateFilmeDto filmeDto, CancellationToken cancellationToken)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            await _context.Filmes.AddAsync(filme, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var resposta = _mapper.Map<ReadFilmeDto>(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, resposta);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFilmeDto>>> RecuperarFilmes(
            CancellationToken cancellationToken)
        {
            var filmes = await _context.Filmes.AsNoTracking().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ReadFilmeDto>>(filmes));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReadFilmeDto>> RecuperaFilmePorId(
            int id, CancellationToken cancellationToken)
        {
            var filme = await _context.Filmes.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (filme == null)
                return NotFound();

            return Ok(_mapper.Map<ReadFilmeDto>(filme));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizaFilme(
            int id, [FromBody] UpdateFilmeDto filmeDto, CancellationToken cancellationToken)
        {
            var filme = await _context.Filmes
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (filme == null)
                return NotFound();

            _mapper.Map(filmeDto, filme);
            await _context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletaFilme(int id, CancellationToken cancellationToken)
        {
            var filme = await _context.Filmes
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (filme == null)
                return NotFound();

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
