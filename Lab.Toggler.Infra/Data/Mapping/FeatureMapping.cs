using Lab.Toggler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.Toggler.Infra.Data.Mapping
{
    internal class FeatureMapping : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(d => d.Name).IsUnicode(false).HasMaxLength(40);
            builder.HasIndex(d => d.Name).IsUnique(true);
        }
    }
}