namespace TrilhasBrasil.API.Models
{
    public class Trilha
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double TamanhoKm { get; set; }

        public string? TrilhaImagemUrl { get; set; }

        public Guid DificuldadeId { get; set; }

        public Guid EstadoId { get; set; }

        public Dificuldade Dificuldade { get; set; } // navegação entre entidades

        public Estado Estado { get; set;}
    }
}
