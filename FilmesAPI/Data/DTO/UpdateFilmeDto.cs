using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O campo titulo é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo diretor é obrigatorio")]
        public string Diretor { get; set; }

        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve estar entre 1 e 600 minutos")]

        public int Duracao { get; set; }
    }
}
