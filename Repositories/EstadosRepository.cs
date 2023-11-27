using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TrilhasBrasil.API.Data;
using TrilhasBrasil.API.Models;

namespace TrilhasBrasil.API.Repositories
{
    public class EstadosRepository : IEstadoRepository
    {
        private readonly TrilhasBrasilDbContext _trilhasBrasilDbContext;
        public EstadosRepository(TrilhasBrasilDbContext DbContext) // injeção do contexto do banco de dados
        {
            _trilhasBrasilDbContext = DbContext;
        }

        public async Task<Estado?> AtualizarEstadoAsync(Guid id, Estado estado)
        {
            var estadoModel = await _trilhasBrasilDbContext.Estados.FindAsync(id);

            if (estadoModel is null)
            {
                return null;
            }

            estadoModel.Sigla = estado.Sigla;
            estadoModel.Nome = estado.Nome;
            estadoModel.EstadoImagemURl = estado.EstadoImagemURl;

            await _trilhasBrasilDbContext.SaveChangesAsync();

            return estadoModel;
        }

        public async Task<Estado> CriarEstadoAsync(Estado estado)
        {
            await _trilhasBrasilDbContext.AddAsync(estado);

            await _trilhasBrasilDbContext.SaveChangesAsync();

            return estado;
        }

        public async Task<Estado?> DeletarEstadoAsync(Guid id)
        {
            var estado = await _trilhasBrasilDbContext.Estados.FindAsync(id);

            if(estado is null)
            {
                return null;
            }

             _trilhasBrasilDbContext.Remove(estado);
            await _trilhasBrasilDbContext.SaveChangesAsync();

            return estado;
        }

        public async Task<Estado?> GetEstadoByIdAsync(Guid id)
        {
           return await _trilhasBrasilDbContext.Estados.FindAsync(id);
           
        }

        public async Task<List<Estado>> GetEstadosAsync()
        {
           return await _trilhasBrasilDbContext.Estados.ToListAsync();
        }
    }
}
