﻿// <auto-generated />
using Lab.Toggler.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab.Toggler.Infra.Migrations
{
    [DbContext(typeof(TogglerContext))]
    partial class TogglerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab.Toggler.Domain.Entities.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(80)
                        .IsUnicode(false);

                    b.Property<string>("Version")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name", "Version")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [Version] IS NOT NULL");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Lab.Toggler.Domain.Entities.ApplicationFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationId");

                    b.Property<int>("FeatureId");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ApplicationId", "FeatureId")
                        .IsUnique();

                    b.ToTable("ApplicationFeature");
                });

            modelBuilder.Entity("Lab.Toggler.Domain.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Feature");
                });

            modelBuilder.Entity("Lab.Toggler.Domain.Entities.ApplicationFeature", b =>
                {
                    b.HasOne("Lab.Toggler.Domain.Entities.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab.Toggler.Domain.Entities.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
