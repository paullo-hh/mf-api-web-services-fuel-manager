using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Models {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<VeiculoUsuario>()
                .HasKey(c => new { c.VeiculoId, c.UsuarioId });

            modelBuilder.Entity<VeiculoUsuario>()
                .HasOne(c => c.Veiculo).WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.VeiculoId);

            modelBuilder.Entity<VeiculoUsuario>()
                .HasOne(c => c.Usuario).WithMany(c => c.Veiculos)
                .HasForeignKey(c => c.UsuarioId);
        }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Consumo> Consumos { get; set; }
        
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<VeiculoUsuario> VeiculosUsuarios { get; set; }
    }
}
