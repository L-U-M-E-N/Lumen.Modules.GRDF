using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumen.Modules.GRDF.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GRDF");

            migrationBuilder.CreateTable(
                name: "GRDF",
                schema: "GRDF",
                columns: table => new
                {
                    JourneeGaziere = table.Column<DateOnly>(type: "date", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IndexDebut = table.Column<int>(type: "integer", nullable: false),
                    IndexFin = table.Column<int>(type: "integer", nullable: false),
                    VolumeBrutConsomme = table.Column<float>(type: "real", nullable: false),
                    EnergieConsomme = table.Column<float>(type: "real", nullable: false),
                    VolumeConverti = table.Column<int>(type: "integer", nullable: false),
                    OutsideTemperature = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRDF", x => x.JourneeGaziere);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRDF",
                schema: "GRDF");
        }
    }
}
