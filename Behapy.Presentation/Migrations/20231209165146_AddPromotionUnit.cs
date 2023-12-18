using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddPromotionUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "Promotions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "0ad2b1b4-e8c3-468e-af56-6539885bfb58");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "dd9e1e4f-6d9f-40d0-8ca0-4e30cafcc6b2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "82b27626-8e55-4bac-a5b1-5cfeb5323ec7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3a93a9a-a335-4838-9dc0-69892b194643", "AQAAAAEAACcQAAAAEPBhAHLDvdRprWs5/qmCNZVFMdZgttXMoFOFbaQS4vEUWGZOITR7GxXODQd5LTNFag==", "e66804cc-224a-4d93-be59-50459be06628" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "d54a26a1-3973-44cc-b9b8-a414d3206360");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "9f0fe06e-660a-47b7-a3ca-6172986771e8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "652894ea-91ea-4f1e-82f9-8c9c06ad83fa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d22c6588-9b05-40e7-897e-f8315692c5e3", "AQAAAAEAACcQAAAAEAAlZdoaLf+E47HfA7JqZeLj1RqI/h00UYRspYwI/YGG2MpvD20zYu/+dY1aQauIkg==", "0b1ebb32-0402-4a06-b78d-23e70c4f2808" });
        }
    }
}
