using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab.Toggler.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    Version = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureId = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFeature_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_Name_Version",
                table: "Application",
                columns: new[] { "Name", "Version" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Version] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeature_FeatureId",
                table: "ApplicationFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeature_ApplicationId_FeatureId",
                table: "ApplicationFeature",
                columns: new[] { "ApplicationId", "FeatureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_Name",
                table: "Feature",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationFeature");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Feature");
        }
    }
}
