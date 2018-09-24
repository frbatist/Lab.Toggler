using Lab.Toggler.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Lab.Toggler.Infra.Data
{
    public class TogglerContext : DbContext
    {
        public TogglerContext()
        {

        }

        public TogglerContext(DbContextOptions<TogglerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FeatureMapping());
            base.OnModelCreating(builder);
        }
    }
}
