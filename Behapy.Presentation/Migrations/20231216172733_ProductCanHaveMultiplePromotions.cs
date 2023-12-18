using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class ProductCanHaveMultiplePromotions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Promotions_PromotionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PromotionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProductPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPromotions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "9e38ac1d-e061-43ac-8215-6212622c0bd0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "b63b2519-7ab5-48b6-8221-fb880f710804");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "e9235103-8681-404e-b513-9e9c6155ecf2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d99708a8-9fee-4099-8f18-5f8600409067", "AQAAAAEAACcQAAAAEEOq4KJd2VyGsRzKVRYVPYpxqOty9T6aQpBIDVr16Jt/Ah1ei9IZPCaH/ThfKEDLSA==", "db2d45a1-be7b-40f0-9fc2-7a385204326c" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotions_ProductId",
                table: "ProductPromotions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotions_PromotionId",
                table: "ProductPromotions",
                column: "PromotionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPromotions");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Promotions");

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "ca4ff81e-7e89-42ac-b880-34a85b6bb1f4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "b8a51879-bbf5-4fd9-9235-408e18599284");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "0cb5a65d-61aa-4d6c-a84a-b9fcfacc6f67");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54af58a4-6239-417c-b403-459db12e55d9", "AQAAAAEAACcQAAAAEErjO+IjTzPX28/xkYack8zC2lzzMrZLiLOp3FuOWhVPvwqJqJPD7EvwyrjwStwecg==", "d23b1018-08e4-4bb9-8030-5471c1ff2323" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromotionId",
                table: "Products",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Promotions_PromotionId",
                table: "Products",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}
