using mf_api_web_services_fuel_manager.Models;
using System.ComponentModel.DataAnnotations;

namespace mf_api_web_services_fuel_manager.DTOs {
    public class UsuarioDTO {
        public int? Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Perfil Perfil { get; set; }
    }
}
