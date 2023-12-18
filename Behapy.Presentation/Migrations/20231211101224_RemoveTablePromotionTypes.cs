using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class RemoveTablePromotionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_PromotionTypes_TypeId",
                table: "Promotions");

            migrationBuilder.DropTable(
                name: "PromotionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_TypeId",
                table: "Promotions");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Promotions",
                newName: "Type");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "798eaba3-a171-4c5c-9790-0bb994ebb27d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "25b8691b-7fae-4c24-bdd5-bd107010707d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "e10343e8-80d7-41dc-b933-2a9174452f6d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11e61e66-c259-44ce-a0c7-b76009ce597c", "AQAAAAEAACcQAAAAEIXucpn5XxrqqiXcSR9r0ZN1NXmG31aoo1Yb0VZJQilLtCmRA64fI6tpQe4j55T5OA==", "718e92cd-c3eb-4e87-8b6b-d789e2da4b17" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Promotions",
                newName: "TypeId");

            migrationBuilder.CreateTable(
                name: "PromotionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "d441fd5d-c0ed-4172-aa14-aa20357a90d1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "b7e8dbef-058f-4617-96e2-33a2a9f78eed");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "dbc06c4b-849b-4fcb-888c-696bd199d405");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "015cafa4-f121-469c-8202-794ce5b757a4", "AQAAAAEAACcQAAAAEIqvz4jC4kIEJfn1p8JiVnZkR89lCc+UaXYO3BrAuKAWKt5RxgO2VvRIrVxdYIN5TA==", "02627f2f-4dc5-4559-8212-37539fe8bf5e" });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_TypeId",
                table: "Promotions",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_PromotionTypes_TypeId",
                table: "Promotions",
                column: "TypeId",
                principalTable: "PromotionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
