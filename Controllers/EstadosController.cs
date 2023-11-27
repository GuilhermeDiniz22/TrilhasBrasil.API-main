using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public EstadosController(TrilhasBrasilDbContext DbContext, IEstadoRepository estadoRepository, IMapper mapper)
        {
            _trilhasBrasilDbContext = DbContext;
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEstados()
        {
            var estados = await _estadoRepository.GetEstadosAsync();

            _mapper.Map<List<EstadoDto>>(estados);// auto mapping de uma lista de estados(model) para o estadoDto
            
            return Ok(estados);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEstadoById(Guid id)
        {
            var estadoModel = await _estadoRepository.GetEstadoByIdAsync(id);// pode ser usando FirstOrDefault

            if (estadoModel is null)
            {
                return NotFound("Estado não encontrado.");
            }

            _mapper.Map<Estado, EstadoDto>(estadoModel);


            return Ok(estadoModel);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CriarEstado(EstadoPostDto estado)
        {
            if(ModelState.IsValid)
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

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizarEstado(Guid id, EstadoPutDto estado)
        {
            var estadoModel = _mapper.Map<Estado>(estado);

            estadoModel = await _estadoRepository.AtualizarEstadoAsync(id, estadoModel);

            if(estadoModel is null)
            {
                return NotFound("Estado não encontrado.");
            }

           
            var estadoDto = _mapper.Map<EstadoDto>(estadoModel); //converte o estadoModel que recebe o model estado para o estadoDto

            return Ok(estadoDto);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletarEstado(Guid id)
        {
            var estado = await _estadoRepository.DeletarEstadoAsync(id);

            if(estado is null)
            {
                return NotFound("Estado não encontrado.");
            }

            return Ok();
        }
    }
    
}

