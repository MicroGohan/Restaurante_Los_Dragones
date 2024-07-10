using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bar_Restaurante_Los_Dragones.Migrations
{
    /// <inheritdoc />
    public partial class Factura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtotal = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<int>(type: "int", nullable: true),
                    Iva = table.Column<int>(type: "int", nullable: false),
                    TotalPagar = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoPago = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Facturas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PedidoId",
                table: "Facturas",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas");
        }
    }
}
