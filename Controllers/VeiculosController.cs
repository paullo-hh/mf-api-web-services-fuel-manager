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
            //return await _context.Veiculos.Include(v => v.Consumos).ToListAsync();
            return Ok(await _context.Veiculos.ToListAsync());
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.Include(v => v.Consumos).FirstOrDefaultAsync(v => v.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return veiculo;
        }
    }
}
