using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace mf_api_web_services_fuel_manager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(AppDbContext context) : ControllerBase {
        public readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult> GetAll() {
            var usuarios = await _context.Usuarios.ToListAsync();

            if (usuarios == null)
                return NotFound(new { message = "Nenhum usuário cadastrado!" });

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario) {
            var result = new { message = "Usuário cadastrado com sucesso!" };

            if (string.IsNullOrWhiteSpace(usuario.Nome)) {
                return BadRequest(new { message = "O campo 'Nome' é obrigatório!" });
            }

            if (string.IsNullOrWhiteSpace(usuario.Password)) {
                return BadRequest(new { message = "O campo 'Senha' é obrigatório!" });
            }

            if (usuario.Perfil.Equals(null)) {
                return BadRequest(new { message = "O campo 'Perfil' é obrigatório!" });
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = usuario.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado!" });

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Usuario usuario) {
            if (id != usuario.Id) return NotFound(new { message = "Usuário não identificado!" });

            var _usuarioExistente = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (_usuarioExistente == null)
                return NotFound(new { message = "Usuário não encontrado!" });

            if (string.IsNullOrWhiteSpace(_usuarioExistente.Nome)) {
                return BadRequest(new { message = "O campo 'Nome' é obrigatório!" });
            }

            if (string.IsNullOrWhiteSpace(_usuarioExistente.Password)) {
                return BadRequest(new { message = "O campo 'Senha' é obrigatório!" });
            }

            if (_usuarioExistente.Perfil.Equals(null)) {
                return BadRequest(new { message = "O campo 'Perfil' é obrigatório!" });
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var _usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (_usuarioExistente == null)
                return NotFound(new { message = "Usuário não encontrado!" });

            _context.Usuarios.Remove(_usuarioExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
