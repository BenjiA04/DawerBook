using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DawerBook.Migrations
{
    /// <inheritdoc />
    public partial class Corrigiendo_Relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Libros_LibroId",
                table: "DetallesPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamoId",
                table: "DetallesPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Devuelto",
                table: "DetallesPrestamo",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

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
                name: "FK_DetallesPrestamo_Libros_LibroId",
                table: "DetallesPrestamo",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamoId",
                table: "DetallesPrestamo",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamosId",
                table: "DetallesPrestamo",
                column: "PrestamosId",
                principalTable: "Prestamos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Libros_LibroId",
                table: "DetallesPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamoId",
                table: "DetallesPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamosId",
                table: "DetallesPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPrestamo_PrestamosId",
                table: "DetallesPrestamo");

            migrationBuilder.DropColumn(
                name: "PrestamosId",
                table: "DetallesPrestamo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Devuelto",
                table: "DetallesPrestamo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPrestamo_Libros_LibroId",
                table: "DetallesPrestamo",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPrestamo_Prestamos_PrestamoId",
                table: "DetallesPrestamo",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
