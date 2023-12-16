using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class FixICollectionInDetributorLev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "5680adf7-2cef-460d-b541-bf4fb449cdf3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "ddf722c3-b4d6-4dbe-817f-d089dafa49d8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "9c6b6136-6796-407a-99c2-f5cb6420b0eb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c9a8dcd-b6a2-42ad-a649-5df58bc32c11", "AQAAAAEAACcQAAAAEF7WgHTkik/n5eoHaGrQSNGQIMhdP2X35vnRsxTblv6XCnoNi/cRg/MDcQG0MU0u7w==", "57fc5939-209e-4463-a8f2-f63995ab4368" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
