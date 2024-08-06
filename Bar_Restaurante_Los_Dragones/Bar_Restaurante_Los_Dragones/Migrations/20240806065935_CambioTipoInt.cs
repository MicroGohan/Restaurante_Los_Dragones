using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bar_Restaurante_Los_Dragones.Migrations
{
    /// <inheritdoc />
    public partial class CambioTipoInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalPagar",
                table: "Facturas",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Subtotal",
                table: "Facturas",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "DetallePedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPagar",
                table: "Facturas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Facturas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "DetallePedidos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Precio",
                value: 10.00m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Precio",
                value: 25.00m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Precio",
                value: 8.00m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Precio",
                value: 12.99m);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Precio",
                value: 1.99m);
        }
    }
}
