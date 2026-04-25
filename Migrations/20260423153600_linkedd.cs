using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplySync.Migrations
{
    /// <inheritdoc />
    public partial class linkedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "warehouseId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_warehouseId",
                table: "User",
                column: "warehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Warehouse_warehouseId",
                table: "User",
                column: "warehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Warehouse_warehouseId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_warehouseId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "warehouseId",
                table: "User");
        }
    }
}
