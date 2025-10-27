using Microsoft.EntityFrameworkCore;
using BancomatApp.Models;

namespace BancomatApp.Data
{
    public class BancomatDbContext : DbContext
    {
        public BancomatDbContext(DbContextOptions<BancomatDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContoCorrente> ContiCorrenti { get; set; }
        public DbSet<Movimento> Movimenti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione ContoCorrente
            modelBuilder.Entity<ContoCorrente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Saldo).HasPrecision(18, 2);
                entity.Property(e => e.NumeroContoCorrente).IsRequired();
                entity.Property(e => e.CodicePin).IsRequired();
            });

            // Configurazione Movimento
            modelBuilder.Entity<Movimento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Importo).HasPrecision(18, 2);
                entity.HasOne(e => e.ContoCorrente)
                      .WithMany(c => c.Movimenti)
                      .HasForeignKey(e => e.ContoCorrenteId);
            });

            // Dati di seed per testing
            modelBuilder.Entity<ContoCorrente>().HasData(
                new ContoCorrente
                {
                    Id = 1,
                    NumeroContoCorrente = "IT0000012345",
                    CodicePin = "1234",
                    Saldo = 1000.00m
                },
                new ContoCorrente
                {
                    Id = 2,
                    NumeroContoCorrente = "IT0000067890",
                    CodicePin = "5678",
                    Saldo = 500.00m
                }
            );
        }
    }
}
