using BusinessClientsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessClientsManager.Data
{
    public class ClientDBContext : DbContext
    {
        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {
        }

        public DbSet<BusinessClient> BusinessClients { get; set; }
        public DbSet<Postcode> Postcodes { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessClient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => new { e.Name }).IsUnique(true);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => new { e.Address }).IsUnique(true);
                entity.HasOne(e => e.Postcode).WithMany(e => e.BusinessClients).IsRequired(false);
                entity.ToTable("BusinessClient");
            });

            modelBuilder.Entity<Postcode>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(10);
                entity.Property(e => e.City).IsRequired().HasMaxLength(80);
                entity.ToTable("Postcode");
            });
            modelBuilder.Entity<Log>().ToTable("Log");
        }
    }
}
