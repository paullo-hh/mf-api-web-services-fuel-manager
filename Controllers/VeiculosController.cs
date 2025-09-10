using mf_api_web_services_fuel_manager.DTOs;
using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController(AppDbContext context) : ControllerBase {
        private readonly AppDbContext _context = context;

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<VeiculoDTO>> GetAll() {
            var veiculos = await _context.Veiculos
                .Include(c => c.Consumos)
                .ToListAsync();

            var veiculosDto = veiculos.Select(v => new VeiculoDTO
            { 
                Id = v.Id,
                Nome = v.Nome,
                Consumos = v.Consumos.Select(c => new ConsumoDTO {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Data = c.Data,
                    Valor = c.Valor,
                    Tipo = c.Tipo
                }).ToList()
            }).ToList();

            return Ok(veiculosDto);
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoDTO>> GetById(int id) {
            var veiculo = await _context.Veiculos
                .Include(c => c.Consumos)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null) {
                return NotFound();
            }

            var veiculosDto = new VeiculoDTO {
                Id = veiculo.Id,
                Nome = veiculo.Nome,
                Consumos = veiculo.Consumos.Select(c => new ConsumoDTO {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Data = c.Data,
                    Valor = c.Valor,
                    Tipo = c.Tipo
                }).ToList()
            };

            return Ok(veiculosDto);
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult> Create(Veiculo veiculo) {
            if (veiculo.AnoFabricacao <= 0 || veiculo.AnoModelo <= 0) {
                return BadRequest(new { message = "Ano de Fabricação e Ano do Modelo são obrigatórios!" });
            }

            if (string.IsNullOrWhiteSpace(veiculo.Nome) || string.IsNullOrWhiteSpace(veiculo.Modelo) || string.IsNullOrWhiteSpace(veiculo.Placa)) {
                return BadRequest(new { message = "Nome, Modelo e Placa são obrigatórios!" });
            }
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Create", veiculo);
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public
            async Task<ActionResult> Update(int id, Veiculo veiculo) {
            if (id != veiculo.Id)
                return BadRequest(new { message = "Veículo não encontrado!" });

            if (veiculo.AnoFabricacao <= 0 || veiculo.AnoModelo <= 0) {
                return BadRequest(new { message = "Ano de Fabricação e Ano do Modelo são obrigatórios!" });
            }

            if (string.IsNullOrWhiteSpace(veiculo.Nome) || string.IsNullOrWhiteSpace(veiculo.Modelo) || string.IsNullOrWhiteSpace(veiculo.Placa)) {
                return BadRequest(new { message = "Nome, Modelo e Placa são obrigatórios!" });
            }

            var veiculoExistent = await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if (veiculoExistent == null)
                return NotFound(new { message = "Veículo não encontrado!" });

            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var veiculo = await _context.Veiculos
                .FindAsync(id);

            if (veiculo == null) {
                return NotFound(new { message = "Veículo não encontrado!" });
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
