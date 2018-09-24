using Lab.Toggler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.Toggler.Infra.Data.Mapping
{
    public class ApplicationFeatureMapping : IEntityTypeConfiguration<ApplicationFeature>
    {
        public void Configure(EntityTypeBuilder<ApplicationFeature> builder)
        {
            builder.HasIndex(d => new { d.Application, d.Feature }).IsUnique();
        }
    }
}
