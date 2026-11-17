using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DawerBook.Migrations
{
    /// <inheritdoc />
    public partial class Eliminando_Relacion_Duplicada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamosId",
                table: "DetallesPrestamo");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPrestamo_PrestamosId",
                table: "DetallesPrestamo");

            migrationBuilder.DropColumn(
                name: "PrestamosId",
                table: "DetallesPrestamo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrestamosId",
                table: "DetallesPrestamo",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPrestamo_PrestamosId",
                table: "DetallesPrestamo",
                column: "PrestamosId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamosId",
                table: "DetallesPrestamo",
                column: "PrestamosId",
                principalTable: "Prestamos",
                principalColumn: "Id");
        }
    }
}
