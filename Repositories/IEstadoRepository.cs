using TrilhasBrasil.API.Models;

namespace TrilhasBrasil.API.Repositories
{
    public interface IEstadoRepository
    {
        Task<List<Estado>> GetEstadosAsync();

        Task<Estado?> GetEstadoByIdAsync(Guid id);

        Task<Estado> CriarEstadoAsync(Estado estado);

        Task<Estado?> AtualizarEstadoAsync(Guid id, Estado estado);

        Task<Estado?> DeletarEstadoAsync(Guid id);
    }
}
