using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmeController : ControllerBase
    {

        private static List<Filme> filmes = new List<Filme>();
        private static int Id = 1;

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filmes.Add(filme);
            filme.Id = Id++;

        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return filmes;
        }

        [HttpGet("{id")]
        public Filme RecuperaFilmePorId(int id)
        {
            foreach (Filme filme in filmes)
            {
                if (filme.Id == id)
                {
                    return filme;
                }
            }
            return null;
        }



    }
}
