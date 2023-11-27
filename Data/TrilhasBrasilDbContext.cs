using Microsoft.EntityFrameworkCore;
using TrilhasBrasil.API.Models;

namespace TrilhasBrasil.API.Data
{
    public class TrilhasBrasilDbContext : DbContext
    {
        public TrilhasBrasilDbContext(DbContextOptions<TrilhasBrasilDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Dificuldade> Dificuldades { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Trilha> Trilhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dificuldades = new List<Dificuldade>()
            {
                new Dificuldade()
                {
                    Id = Guid.Parse("6d729fe9-54e6-4482-a88e-59d6e6921f00"),
                    Nome = "Fácil"
                },
                new Dificuldade()
                {
                    Id = Guid.Parse("472f64f1-d96e-4a61-ba3b-b7899657ed57"),
                    Nome = "Médio"
                },
                new Dificuldade()
                {
                    Id = Guid.Parse("8d0fb2f1-20b7-4f78-9e28-67acf50a41d7"),
                    Nome = "Difícil"
                },
            };

            modelBuilder.Entity<Dificuldade>().HasData(dificuldades);

            var estados = new List<Estado>()
            {
                new Estado()
                {
                    Id = Guid.Parse("50bb6ee2-9569-4a40-bd85-2ca90a16678b"),
                    Nome = "Rio Grande do Sul",
                    Sigla = "RS",
                    EstadoImagemURl = "https://media.gettyimages.com/id/560616935/pt/foto/portico-of-gramado-rio-grande-do-sul-brazil.jpg?s=2048x2048&w=gi&k=20&c=ExGu5ZKM5lWt0XsPuGQFwAQ2FCgjr7DLdqmSTI9RLRw="
                },
                new Estado()
                {
                    Id = Guid.Parse("72f01c89-0c88-4f78-b184-5735f0ea30d6"),
                    Nome = "Rio Grande do Norte",
                    Sigla = "RN",
                    EstadoImagemURl = null
                },
                new Estado()
                {
                    Id = Guid.Parse("94c09e4b-14b2-453e-9c9c-8e48ec3f9683"),
                    Nome = "Mato Grosso do Sul",
                    Sigla = "MS",
                    EstadoImagemURl = null
                },
            };

            modelBuilder.Entity<Estado>().HasData(estados);
        }
    }
}
