using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mf_api_web_services_fuel_manager.Models {
    [Table("Usuarios")]
    public class Usuario {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [JsonIgnore] 
        public string Password { get; set; }

        [Required]
        public Perfil Perfil { get; set; }

        public ICollection<VeiculoUsuario> Veiculos { get; set; }
    }

    public enum Perfil {
        [Display(Name = "Administrador")]
        Administrador,

        [Display(Name = "Usuario")]
        Usuario
    }
}
