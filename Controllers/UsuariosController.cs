using mf_api_web_services_fuel_manager.DTOs;
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
        public async Task<ActionResult> Create(UsuarioDTO usuarioDto) { 
            var result = new { message = "Usuário cadastrado com sucesso!" };


            if (string.IsNullOrWhiteSpace(usuarioDto.Nome)) {
                return BadRequest(new { message = "O campo 'Nome' é obrigatório!" });
            }

            if (string.IsNullOrWhiteSpace(usuarioDto.Password)) {
                return BadRequest(new { message = "O campo 'Senha' é obrigatório!" });
            }

            if (usuarioDto.Perfil.Equals(null)) {
                return BadRequest(new { message = "O campo 'Perfil' é obrigatório!" });
            }

            Usuario _usuario = new Usuario() {
                Nome = usuarioDto.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password),
                Perfil = usuarioDto.Perfil
            };

            _context.Usuarios.Add(_usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = _usuario.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado!" });

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDTO usuarioDto) {
            if (id != usuarioDto.Id) return BadRequest(new { message = "Usuário não identificado com o ID informado!" });

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

            _usuarioExistente.Nome = usuarioDto.Nome;
            _usuarioExistente.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password);
            _usuarioExistente.Perfil = usuarioDto.Perfil;
            
            _context.Usuarios.Update(_usuarioExistente);
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
