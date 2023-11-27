using System.ComponentModel.DataAnnotations;

namespace TrilhasBrasil.API.Models.Dtos
{
    public class TrilhaPostDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "O nome deve conter até 25 caracteres.")]
        [MinLength(5, ErrorMessage = "O nome deve conter pelo menos 5 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "A descrição deve conter até 200 caracteres.")]
        [MinLength(20, ErrorMessage = "A descrição deve conter pelo menos 5 caracteres.")]
        public string Descricao { get; set; }

        [Required]
        [Range(0, 50)]
        public double TamanhoKm { get; set; }

        public string? TrilhaImagemUrl { get; set; }

        [Required]
        public Guid DificuldadeId { get; set; }

        [Required]
        public Guid EstadoId { get; set; }
    }
}
