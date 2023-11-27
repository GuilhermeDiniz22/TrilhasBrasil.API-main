using TrilhasBrasil.API.Models;

namespace TrilhasBrasil.API.Repositories
{
    public interface ITrilhasRepository
    {
        Task<List<Trilha>> GetTrilhasAsync(bool? OrdemAscendente, string? filtro = null, string? filtroConsulta = null, int paginaNumero = 1, int tamanhoPagina = 10);

        Task<Trilha?> GetTrilhaByIdAsync(Guid id);

        Task<Trilha> CriarTrilhaAsync(Trilha trilha);

        Task<Trilha?> AtualizarTrilhaAsync(Guid id, Trilha trilha);

        Task<Trilha?> DeletarTrilhaAsync(Guid id);
    }
}
