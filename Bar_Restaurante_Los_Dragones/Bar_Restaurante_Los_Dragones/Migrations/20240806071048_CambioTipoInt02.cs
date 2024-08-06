using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bar_Restaurante_Los_Dragones.Migrations
{
    /// <inheritdoc />
    public partial class CambioTipoInt02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "Platos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "Pedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Precio",
                value: 2300);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Precio",
                value: 5500);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Precio",
                value: 2500);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Precio",
                value: 7500);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Precio",
                value: 1200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Platos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Precio",
                value: 2300m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Precio",
                value: 5500m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Precio",
                value: 2500m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Precio",
                value: 7500m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Precio",
                value: 1200m);
        }
    }
}
