using Lab.Toggler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.Toggler.Infra.Data.Mapping
{
    public class ApplicationFeatureMapping : IEntityTypeConfiguration<ApplicationFeature>
    {
        public void Configure(EntityTypeBuilder<ApplicationFeature> builder)
        {
            builder.HasOne(d => d.Application).WithMany().HasForeignKey(d => d.ApplicationId);
            builder.HasOne(d => d.Feature).WithMany().HasForeignKey(d => d.FeatureId);
            builder.HasIndex(d => new { d.ApplicationId, d.FeatureId }).IsUnique();
        }
    }
}
