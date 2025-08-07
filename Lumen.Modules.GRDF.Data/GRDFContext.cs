using Lumen.Modules.GRDF.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace Lumen.Modules.GRDF.Data {
    public class GRDFContext : DbContext {
        public const string SCHEMA_NAME = "GRDF";

        public GRDFContext(DbContextOptions<GRDFContext> options) : base(options) {
        }

        public DbSet<GRDFPointInTime> GRDF { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema(SCHEMA_NAME);

            var GRDFModelBuilder = modelBuilder.Entity<GRDFPointInTime>();
            GRDFModelBuilder.Property(x => x.Time)
                .HasColumnType("timestamp with time zone");

            GRDFModelBuilder.Property(x => x.Value)
                .HasColumnType("integer");

            GRDFModelBuilder.HasKey(x => x.Time);
        }
    }
}
