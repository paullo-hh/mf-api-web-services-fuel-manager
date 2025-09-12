using mf_api_web_services_fuel_manager.DTOs;
using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace mf_api_web_services_fuel_manager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController(AppDbContext context) : ControllerBase {
        private readonly AppDbContext _context = context;

        public TipoCombustivel _tipoCombustivel;
        public Veiculo _veiculo;

        // GET: api/Consumos
        [HttpGet]
        public async Task<ActionResult> GetAll() {
            var consumos = await _context.Consumos.ToListAsync();

            if (consumos == null || consumos.IsNullOrEmpty()) {
                return NotFound(new { message = "Nenhum consumo encontrado!" });
            }

            var consumosDto = consumos.Select(c => new ConsumoDetalhadoDTO {
                Id = c.Id,
                Descricao = c.Descricao,
                Data = c.Data,
                Valor = c.Valor,
                Tipo = c.Tipo,
                VeiculoId = c.VeiculoId
            });

            return Ok(consumosDto);
        }

        // GET: api/Consumos/4
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoDTO>> GetById(int id) {
            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consumo == null) {
                return NotFound(
                    new { message = "Consumo não encontrado!" }
                    );
            }

            var consumoDto = new ConsumoDetalhadoDTO {
                Id = consumo.Id,
                Descricao = consumo.Descricao,
                Data = consumo.Data,
                Valor = consumo.Valor,
                Tipo = consumo.Tipo,
                VeiculoId = consumo.VeiculoId
            };

            return Ok(consumoDto);
        }

        // POST: api/Consumos
        [HttpPost]
        public async Task<ActionResult<Consumo>> Create(Consumo consumo) {
            var result = new { message = "Consumo cadastrado com sucesso!" };

            if (string.IsNullOrWhiteSpace(consumo.Descricao)) {
                return BadRequest(new { message = "O campo 'Descrição' é obrigatório!" });
            }

            if (consumo.Data > DateTime.UtcNow) {
                return BadRequest(new { message = "O campo 'Data' não pode ser maior de a data atual!" });
            }

            if (consumo.Valor <= 0) {
                return BadRequest(new { message = "O campo 'Valor' não pode ser menor ou igual a zero(0)!" });
            }


            if (consumo.Tipo.Equals(TipoCombustivel.Nenhum)) {
                return BadRequest(new { message = "O campo 'Tipo' não deve ser igual a 'Nenhum'!" });
            }

            var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == consumo.VeiculoId);

            if (veiculo == null) {
                return NotFound(new { message = "Veículo não encontrado!" });
            }

            consumo.Veiculo = veiculo;

            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = consumo.Id }, result);
        }

        // PUT: api/Consumos/4
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Consumo consumo) {
            if (id != consumo.Id) {
                return BadRequest(new { message = "Consumo não identificado!" });
            }

            var _consumoExistente = await _context.Consumos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (_consumoExistente == null) {
                return NotFound(new { message = "Consumo não encontrado!" });
            }

            if (string.IsNullOrWhiteSpace(_consumoExistente.Descricao)) {
                return BadRequest(new { message = "O campo 'Descrição' é obrigatório!" });
            }

            if (_consumoExistente.Data > DateTime.UtcNow) {
                return BadRequest(new { message = "O campo 'Data' não pode ser maior que a data atual!" });
            }

            if (_consumoExistente.Valor <= 0) {
                return BadRequest(new { message = "O campo 'Valor' não pode ser menor ou igual a zero(0)!" });
            }


            if (_consumoExistente.Tipo.Equals(TipoCombustivel.Nenhum)) {
                return BadRequest(new { message = "O campo 'Tipo' não deve ser igual a 'Nenhum'!" });
            }

            var _veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == consumo.VeiculoId);

            if (_veiculo == null) {
                return NotFound(new { message = "Veículo não encontrado!" });                
            }

            _consumoExistente.Veiculo = _veiculo;
            _context.Consumos.Update(consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Consumos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var _consumo = await _context.Consumos.FindAsync(id);

            if (_consumo == null) {
                return NotFound(new { message = "Consumo não encontrado!" });
            }

            _context.Consumos.Remove(_consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
