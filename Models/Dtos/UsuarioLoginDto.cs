using System.ComponentModel.DataAnnotations;

namespace TrilhasBrasil.API.Models.Dtos
{
    public class UsuarioLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
