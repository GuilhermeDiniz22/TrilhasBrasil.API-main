namespace TrilhasBrasil.API.Models.Dtos
{
    public class TrilhaDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double TamanhoKm { get; set; }

        public string? TrilhaImagemUrl { get; set; }

        public EstadoDto Estado { get; set; }

        public DificuldadeDto Dificuldade { get; set; }
    }
}
