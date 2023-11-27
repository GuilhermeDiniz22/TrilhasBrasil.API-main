using System.ComponentModel.DataAnnotations;

namespace TrilhasBrasil.API.Models.Dtos
{
    public class UsuarioPostDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}