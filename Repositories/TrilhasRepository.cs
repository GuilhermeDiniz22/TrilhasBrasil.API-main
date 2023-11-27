using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TrilhasBrasil.API.Data;
using TrilhasBrasil.API.Models;

namespace TrilhasBrasil.API.Repositories
{
    public class TrilhasRepository : ITrilhasRepository
    {
        private readonly TrilhasBrasilDbContext _trilhasBrasilDbContext;

        public TrilhasRepository(TrilhasBrasilDbContext DbContext)
        {
            _trilhasBrasilDbContext = DbContext;
        }

        public async Task<Trilha?> AtualizarTrilhaAsync(Guid id, Trilha trilha)
        {
            var trilhaModel = await _trilhasBrasilDbContext.Trilhas.FindAsync(id);

            if (trilhaModel is null)
            {
                return null;
            };

            trilhaModel.Nome = trilha.Nome;
            trilhaModel.Descricao = trilha.Descricao;
            trilhaModel.TamanhoKm = trilha.TamanhoKm;
            trilhaModel.TrilhaImagemUrl = trilha.TrilhaImagemUrl;
            trilhaModel.DificuldadeId = trilha.DificuldadeId;
            trilhaModel.EstadoId = trilha.EstadoId;

            await _trilhasBrasilDbContext.SaveChangesAsync();

            return trilhaModel;
        }

        public async Task<Trilha> CriarTrilhaAsync(Trilha trilha)
        {
            await _trilhasBrasilDbContext.Trilhas.AddAsync(trilha);

            await _trilhasBrasilDbContext.SaveChangesAsync();

            return trilha;
        }

        public async Task<Trilha?> DeletarTrilhaAsync(Guid id)
        {
            var trilhaModel = await _trilhasBrasilDbContext.Trilhas.FindAsync(id);

            if (trilhaModel is null)
            {
                return null;
            }

            _trilhasBrasilDbContext.Trilhas.Remove(trilhaModel);

            await _trilhasBrasilDbContext.SaveChangesAsync() ;

            return trilhaModel;
        }

        public async Task<Trilha?> GetTrilhaByIdAsync(Guid id)
        {
            return await _trilhasBrasilDbContext.Trilhas.Include(p => p.Dificuldade).Include(p => p.Estado).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Trilha>> GetTrilhasAsync(bool? OrdemAscendente, string? filtro = null, string? filtroConsulta = null, int paginaNumero = 1, int tamanhoPagina = 10)
        {
            var trilhas = _trilhasBrasilDbContext.Trilhas.Include(x => x.Dificuldade).Include(c => c.Estado).AsQueryable();//incluir filtro de consulta

            if (!string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(filtroConsulta))
            {
                //filtro
                if (filtro.Equals("Nome", StringComparison.OrdinalIgnoreCase)) //ignora letras maiusculas ou vice versa
                {
                    trilhas = trilhas.Where(t => t.Nome.Contains(filtroConsulta));
                }

                //ordenação

                if(OrdemAscendente is true) //filtro por ordem ascedente ou descendente
                {
                    trilhas = trilhas.OrderBy(t => t.Nome);
                }
                else
                {
                    trilhas = trilhas.OrderByDescending(t => t.Nome);
                }

            }
            var resultado = (paginaNumero - 1) * tamanhoPagina; //formula de paginação

            return await trilhas.Skip(resultado).Take(tamanhoPagina).ToListAsync(); //ordenado pelo tamanho da trilha e a função take seleciona o numero de resultados vindos do banco
           
        }
    }
}
