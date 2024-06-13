using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Emit;

namespace DataBaseTransportCompany
{
    // Точка доступа к базе данных
    public class TransportCompanyContext : DbContext
    {
        public DbSet<StampTransport> Stamps { get; set; }
        public DbSet<ModelTransport> Models { get; set; }
        public DbSet<TypeTransport> Types { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        public DbSet<TransportVehicleDriver> TransportVehicleDrivers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverCategory> DriverCategories { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Добавляем составной первичный ключ для таблиц DriverCategory и TransportVehicleDriver
            modelBuilder.Entity<DriverCategory>()
                .HasKey(dc => new { dc.driverId, dc.categoryId });
            modelBuilder.Entity<TransportVehicleDriver>()
                .HasKey(tvd => new { tvd.transportVehicleId, tvd.driverId });

            // Добавлляем ограничение уникальности
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.title)
                .IsUnique();
            modelBuilder.Entity<TypeTransport>()
                .HasIndex(t => t.title)
                .IsUnique();
            modelBuilder.Entity<StampTransport>()
                .HasIndex(s => s.title)
                .IsUnique();
            modelBuilder.Entity<ModelTransport>()
                .HasIndex(m => m.title)
                .IsUnique();
            modelBuilder.Entity<TransportVehicle>()
                .HasIndex(tv => new { tv.number, tv.codeRegion, tv.engineNumber })
                .IsUnique();
            modelBuilder.Entity<Driver>()
                .HasIndex(d => new { d.driverLicenseNumber, d.phone })
                .IsUnique();
        }

        public TransportCompanyContext() { }

        public TransportCompanyContext(DbContextOptions<TransportCompanyContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Получаем строку подключения из файла конфигурации App.config
                optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["ConnectionString"]);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}