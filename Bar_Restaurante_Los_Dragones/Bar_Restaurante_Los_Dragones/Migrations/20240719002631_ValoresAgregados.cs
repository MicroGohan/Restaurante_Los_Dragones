using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bar_Restaurante_Los_Dragones.Migrations
{
    /// <inheritdoc />
    public partial class ValoresAgregados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Platos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ListaComida",
                table: "DetallePedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Categoria", "Nombre" },
                values: new object[] { "Comida", "Sopa" });

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Categoria", "Nombre" },
                values: new object[] { "Comida", "Ceviche" });

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Categoria", "Nombre" },
                values: new object[] { "Comida", "Arroz con pollo" });

            migrationBuilder.InsertData(
                table: "Platos",
                columns: new[] { "Id", "Categoria", "Disponible", "ImagenData", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 4, "Comida", true, null, "Pizza", 12.99m },
                    { 5, "Bebida", true, null, "Coca Cola", 1.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Platos");

            migrationBuilder.DropColumn(
                name: "ListaComida",
                table: "DetallePedidos");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "AJI DE GALLINA");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "CEBICHE");

            migrationBuilder.UpdateData(
                table: "Platos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "ARROZ CON POLLO");
        }
    }
}
