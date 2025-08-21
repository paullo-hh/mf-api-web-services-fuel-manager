using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_api_web_services_fuel_manager.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public String Nome { get; set; }

        [Required]
        public String Modelo { get; set; }

        [Required]
        public string Placa { get; set; }
        
        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int AnoModelo { get; set; }

        public ICollection<Consumo> Consumos { get; set; }
    }
}
