namespace TrilhasBrasil.API.Models.Dtos
{
    public class TrilhaPutDto
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double TamanhoKm { get; set; }

        public string? TrilhaImagemUrl { get; set; }

        public Guid DificuldadeId { get; set; }

        public Guid EstadoId { get; set; }
    }
}
