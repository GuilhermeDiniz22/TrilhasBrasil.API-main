using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrilhasBrasil.API.Data;
using TrilhasBrasil.API.Models;
using TrilhasBrasil.API.Models.Dtos;
using TrilhasBrasil.API.Repositories;

namespace TrilhasBrasil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrilhasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrilhasRepository _trilhasRepository;
        private readonly ILogger logger;

        public TrilhasController(IMapper mapper, ITrilhasRepository trilhasRepository, ILogger logger)
        {
            
            _mapper = mapper;
            _trilhasRepository = trilhasRepository;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CriarTrilha(TrilhaPostDto trilha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trilhaNova = _mapper.Map<Trilha>(trilha);

                    await _trilhasRepository.CriarTrilhaAsync(trilhaNova);

                    _mapper.Map<TrilhaDto>(trilhaNova);

                    return Ok(trilhaNova);
                }

                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
           
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetTrilhas(string? filtro, string? filtroConsulta, bool? OrdemAscendente, int paginaNumero = 1, int tamanhoPagina = 10)
        {
            var trilhas = await _trilhasRepository.GetTrilhasAsync(OrdemAscendente, filtro, filtroConsulta, paginaNumero, tamanhoPagina);

            var retorno = _mapper.Map<List<TrilhaDto>>(trilhas);

            return Ok(retorno);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetTrilhaByIdAsync(Guid id)
        {
            try
            {
                var trilha = await _trilhasRepository.GetTrilhaByIdAsync(id);

                if (trilha is null)
                {
                    return NotFound("Trilha não encontrada.");
                }

                var retorno = _mapper.Map<TrilhaDto>(trilha);

                return Ok(retorno);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
           
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTrilha(Guid id, TrilhaPutDto trilha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trilhaModel = _mapper.Map<Trilha>(trilha);

                    trilhaModel = await _trilhasRepository.AtualizarTrilhaAsync(id, trilhaModel);

                    if (trilhaModel is null)
                    {
                        return NotFound("Trilha não encontrada.");
                    }

                    var retorno = _mapper.Map<TrilhaDto>(trilhaModel);

                    return Ok(retorno);
                }

                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
           
           

        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrilha(Guid id)
        {
            try
            {
                var trilha = await _trilhasRepository.DeletarTrilhaAsync(id);

                if (trilha is null)
                {
                    return NotFound("Trilha não encontrada.");
                }

                var retorno = _mapper.Map<TrilhaDto>(trilha);

                return Ok(retorno);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
            
        }
    }
}
