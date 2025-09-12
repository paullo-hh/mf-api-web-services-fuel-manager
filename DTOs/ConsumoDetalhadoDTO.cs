using mf_api_web_services_fuel_manager.Models;

namespace mf_api_web_services_fuel_manager.DTOs {
    public class ConsumoDetalhadoDTO : LinksHATEOAS {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public TipoCombustivel Tipo { get; set; }
        public int VeiculoId { get; set; }
    }
}
