using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lumen.Modules.GRDF.Data {
    public class GRDFDbContextFactory : IDesignTimeDbContextFactory<GRDFContext> {
        public GRDFContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<GRDFContext>();
            optionsBuilder.UseNpgsql();

            return new GRDFContext(optionsBuilder.Options);
        }
    }
}
