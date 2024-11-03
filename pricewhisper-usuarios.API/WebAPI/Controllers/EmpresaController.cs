using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> Get()
        {
            var empresas = await _empresaService.GetAllAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpresaDto>> GetById(int id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] EmpresaDto empresaDto)
        {
            try
            {
                await _empresaService.AddAsync(empresaDto);
                return CreatedAtAction(nameof(GetById), new { id = empresaDto.EmpresaId }, empresaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update(int id, [FromBody] EmpresaDto empresaDto)
        {
            if (id != empresaDto.EmpresaId)
                return BadRequest("ID da URL não coincide com o ID da empresa.");

            try
            {
                await _empresaService.UpdateAsync(empresaDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _empresaService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}