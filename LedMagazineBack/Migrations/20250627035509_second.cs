using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LedMagazineBack.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RentTimes_Orders_OrderId",
                table: "RentTimes");

            migrationBuilder.DropIndex(
                name: "IX_RentTimes_OrderId",
                table: "RentTimes");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "RentTimes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "RentTimes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CartItemId",
                table: "RentTimes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemId",
                table: "RentTimes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Guests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrganisationName",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CartItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentTimes_CartItemId",
                table: "RentTimes",
                column: "CartItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentTimes_OrderItemId",
                table: "RentTimes",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTimes_CartItems_CartItemId",
                table: "RentTimes",
                column: "CartItemId",
                principalTable: "CartItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTimes_OrderItems_OrderItemId",
                table: "RentTimes",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentTimes_CartItems_CartItemId",
                table: "RentTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_RentTimes_OrderItems_OrderItemId",
                table: "RentTimes");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_RentTimes_CartItemId",
                table: "RentTimes");

            migrationBuilder.DropIndex(
                name: "IX_RentTimes_OrderItemId",
                table: "RentTimes");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "RentTimes");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "RentTimes");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrganisationName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "RentTimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "RentTimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentTimes_OrderId",
                table: "RentTimes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTimes_Orders_OrderId",
                table: "RentTimes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
