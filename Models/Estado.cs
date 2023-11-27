namespace TrilhasBrasil.API.Models
{
    public class Estado
    {
        public Guid Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public string? EstadoImagemURl { get; set; }
    }
}
