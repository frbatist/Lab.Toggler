using Lab.Toggler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.Toggler.Infra.Data.Mapping
{
    internal class ApplicationMapping : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.Property(d => d.Name).IsUnicode(false).HasMaxLength(80);
            builder.Property(d => d.Version).IsUnicode(false).HasMaxLength(20);

            builder.HasIndex(d => new { d.Name, d.Version }).IsUnique(true);            
        }
    }
}