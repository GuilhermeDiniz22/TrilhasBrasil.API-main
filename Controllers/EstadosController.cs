using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrilhasBrasil.API.Data;
using TrilhasBrasil.API.Models;
using TrilhasBrasil.API.Models.Dtos;
using TrilhasBrasil.API.Repositories;

namespace TrilhasBrasil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly TrilhasBrasilDbContext _trilhasBrasilDbContext;
        private readonly IEstadoRepository _estadoRepository;
        private readonly ILogger<EstadosController> logger;
        private readonly IMapper _mapper;

        public EstadosController(TrilhasBrasilDbContext DbContext, IEstadoRepository estadoRepository, IMapper mapper, ILogger<EstadosController> logger)
        {
            _trilhasBrasilDbContext = DbContext;
            _estadoRepository = estadoRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "User")] //role para autorização 
        public async Task<IActionResult> GetEstados()
        {
            try
            {
                var estados = await _estadoRepository.GetEstadosAsync();

                _mapper.Map<List<EstadoDto>>(estados);// auto mapping de uma lista de estados(model) para o estadoDto

                logger.LogInformation($"Get Todos os Estados foi ativado: {JsonSerializer.Serialize(estados)}");

                return Ok(estados);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);

                throw;
            }
           
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEstadoById(Guid id)
        {
            try
            {
                var estadoModel = await _estadoRepository.GetEstadoByIdAsync(id);// pode ser usando FirstOrDefault

                if (estadoModel is null)
                {
                    return NotFound("Estado não encontrado.");
                }

                _mapper.Map<Estado, EstadoDto>(estadoModel);


                return Ok(estadoModel);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CriarEstado(EstadoPostDto estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoModel = _mapper.Map<Estado>(estado);

                    estadoModel = await _estadoRepository.CriarEstadoAsync(estadoModel);

                    var estadoDto = _mapper.Map<EstadoDto>(estadoModel);

                    return CreatedAtAction(nameof(CriarEstado), new { id = estadoModel.Id }, estadoModel);
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

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizarEstado(Guid id, EstadoPutDto estado)
        {
            try
            {
                var estadoModel = _mapper.Map<Estado>(estado);

                estadoModel = await _estadoRepository.AtualizarEstadoAsync(id, estadoModel);

                if (estadoModel is null)
                {
                    return NotFound("Estado não encontrado.");
                }


                var estadoDto = _mapper.Map<EstadoDto>(estadoModel); //converte o estadoModel que recebe o model estado para o estadoDto

                return Ok(estadoDto);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
            
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletarEstado(Guid id)
        {
            try
            {
                var estado = await _estadoRepository.DeletarEstadoAsync(id);

                if (estado is null)
                {
                    return NotFound("Estado não encontrado.");
                }

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
           
        }
    }
    
}

