namespace mf_api_web_services_fuel_manager.DTOs {
    public class VeiculoDTO : LinksHATEOAS {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<ConsumoDTO> Consumos { get; set; }
    }
}
