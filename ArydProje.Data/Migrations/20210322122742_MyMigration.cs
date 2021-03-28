using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArydProje.Data.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherNo = table.Column<int>(type: "int", nullable: false),
                    SpecialCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialCode = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    MaterialDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_OrderHeader_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderHeader",
                columns: new[] { "Id", "Date", "ProjectCode", "SpecialCode", "TotalAmount", "VoucherNo" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 15, 27, 41, 859, DateTimeKind.Local).AddTicks(9070), "PRJ-MVC", new Guid("8d5360fc-fbc7-4093-a048-076d8b718d4a"), 220m, 1001 });

            migrationBuilder.InsertData(
                table: "OrderHeader",
                columns: new[] { "Id", "Date", "ProjectCode", "SpecialCode", "TotalAmount", "VoucherNo" },
                values: new object[] { 2, new DateTime(2021, 3, 22, 15, 27, 41, 861, DateTimeKind.Local).AddTicks(2897), "PRJ-MVC", new Guid("d6a4d546-722c-4785-8241-5999de7b9225"), 220m, 1002 });

            migrationBuilder.InsertData(
                table: "OrderHeader",
                columns: new[] { "Id", "Date", "ProjectCode", "SpecialCode", "TotalAmount", "VoucherNo" },
                values: new object[] { 3, new DateTime(2021, 3, 22, 15, 27, 41, 861, DateTimeKind.Local).AddTicks(2980), "PRJ-MVC", new Guid("8135f597-722f-455a-8b94-b5a3fad4c753"), 220m, 1003 });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "Id", "MaterialCode", "MaterialDescription", "OrderHeaderId", "Quantity", "TaxAmount", "TaxRate", "TotalAmount", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "test", "test", 1, 10m, 10m, 10m, 110m, 10m },
                    { 2, "test", "test", 1, 10m, 10m, 10m, 110m, 10m },
                    { 3, "test", "test", 2, 10m, 10m, 10m, 110m, 10m },
                    { 4, "test", "test", 2, 10m, 10m, 10m, 110m, 10m },
                    { 5, "test", "test", 3, 10m, 10m, 10m, 110m, 10m },
                    { 6, "test", "test", 3, 10m, 10m, 10m, 110m, 10m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderHeaderId",
                table: "OrderLines",
                column: "OrderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "OrderHeader");
        }
    }
}
