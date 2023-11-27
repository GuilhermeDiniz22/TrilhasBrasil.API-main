namespace TrilhasBrasil.API.Models.Dtos
{
    public class EstadoDto
    {
        public Guid Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public string? EstadoImagemURl { get; set; }
    }
}
