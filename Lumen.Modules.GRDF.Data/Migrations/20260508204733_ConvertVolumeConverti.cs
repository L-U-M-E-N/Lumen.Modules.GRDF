using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumen.Modules.GRDF.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertVolumeConverti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "VolumeConverti",
                schema: "GRDF",
                table: "GRDF",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VolumeConverti",
                schema: "GRDF",
                table: "GRDF",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
