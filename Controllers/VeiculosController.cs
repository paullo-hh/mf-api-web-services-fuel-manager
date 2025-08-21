using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _context.Veiculos.ToListAsync());
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetById(int id)
        {
            // include Consumos if needed
            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return veiculo;
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult> Create(Veiculo veiculo)
        {
            if (veiculo.AnoFabricacao <= 0 || veiculo.AnoModelo <= 0)
            {
                return BadRequest(new {message = "Ano de Fabricação e Ano do Modelo são obrigatórios!"});
            }

            if (string.IsNullOrWhiteSpace(veiculo.Nome) || string.IsNullOrWhiteSpace(veiculo.Modelo) || string.IsNullOrWhiteSpace(veiculo.Placa))
            {
                return BadRequest(new {message = "Nome, Modelo e Placa são obrigatórios!"});
            }
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, veiculo);
            return CreatedAtAction("Create", veiculo);
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public
            async Task<ActionResult> Update(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id) return BadRequest(new { message = "Veículo não encontrado!"});

            if (veiculo.AnoFabricacao <= 0 || veiculo.AnoModelo <= 0)
            {
                return BadRequest(new {message = "Ano de Fabricação e Ano do Modelo são obrigatórios!"});
            }

            if (string.IsNullOrWhiteSpace(veiculo.Nome) || string.IsNullOrWhiteSpace(veiculo.Modelo) || string.IsNullOrWhiteSpace(veiculo.Placa))
            {
                return BadRequest(new {message = "Nome, Modelo e Placa são obrigatórios!"});
            }

            var veiculoExistent = await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if (veiculoExistent == null) return NotFound(new {message = "Veículo não encontrado!"});

            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var veiculo = await _context.Veiculos
                .FindAsync(id);

            if (veiculo == null)
            {
                return NotFound(new { message = "Veículo não encontrado!" });
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
