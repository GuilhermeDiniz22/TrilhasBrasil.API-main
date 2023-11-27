using System.ComponentModel.DataAnnotations;

namespace TrilhasBrasil.API.Models.Dtos
{
    public class EstadoPostDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Sigla deve conter no mínimo 2 caracteres")]
        [MaxLength(3, ErrorMessage = "Sigla deve conter no máximo 3 caracteres")]
        public string Sigla { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Nome deve conter no mínimo 2 caracteres")]
        [MaxLength(40, ErrorMessage = "Nome deve conter no máximo 40 caracteres")]
        public string Nome { get; set; }

        public string? EstadoImagemURl { get; set; }
    }
}
