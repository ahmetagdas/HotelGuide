using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Entity
{
    public class OtelDbContext : DbContext
    {
        public OtelDbContext(DbContextOptions<OtelDbContext> options) : base(options)
        {

        }

        public DbSet<Otel> Oteller { get; set; }
        public DbSet<OtelYetkilisi> OtelYetkilileri { get; set; }
        public DbSet<IletisimBilgisi> IletisimBilgileri { get; set; }
        public DbSet<Rapor> Raporlar { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Replace "Data.Entity" with the correct namespace of your migrations assembly
        //        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=otel;User Id=postgres;Password=postgres", b => b.MigrationsAssembly("OtelService"));
        //    }
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtelYetkilisi>()
                .HasOne(oy => oy.Otel)
                .WithMany(o => o.Yetkililer)
                .HasForeignKey(oy => oy.OtelId);
            modelBuilder.Entity<IletisimBilgisi>()
                .HasOne(ib => ib.Otel)
                .WithMany(o => o.IletisimBilgileri)
                .HasForeignKey(ib => ib.OtelId);
            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.Otel)
                .WithMany(o => o.Raporlar)
                .HasForeignKey(r => r.OtelId);

            // Otel entity'si için HasMany ile diğer entity'lerin referanslarını tanımla.
            modelBuilder.Entity<Otel>()
                .HasMany(o => o.Yetkililer)
                .WithOne(oy => oy.Otel)
                .HasForeignKey(oy => oy.OtelId);

            modelBuilder.Entity<Otel>()
                .HasMany(o => o.IletisimBilgileri)
                .WithOne(ib => ib.Otel)
                .HasForeignKey(ib => ib.OtelId);

            modelBuilder.Entity<Otel>()
                .HasMany(o => o.Raporlar)
                .WithOne(r => r.Otel)
                .HasForeignKey(r => r.OtelId);

        }
    }
}
