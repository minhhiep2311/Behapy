using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class RemovePositionFromDistributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Distributors");

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MoneyNeeded", "Name" },
                values: new object[] { 0m, "B4" });

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MoneyNeeded", "Name" },
                values: new object[] { 1000000m, "B3" });

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MoneyNeeded", "Name", "NextLevel" },
                values: new object[] { 30000000m, "B2", 4 });

            migrationBuilder.InsertData(
                table: "DistributorLevels",
                columns: new[] { "Id", "MoneyNeeded", "Name", "NextLevel" },
                values: new object[] { 4, 100000000m, "B1", null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "d900c88c-e7a0-442d-a1b9-7ef98ed34635");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "65ca94bb-42c4-480c-bf96-78168f8a6b59");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "606f6458-ee14-4389-95da-0c8e0ba5b3f8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f73fd75d-2f2d-4cff-9b4e-61cbced27fd1", "AQAAAAEAACcQAAAAEHkkQOxkOe0OVxwGOR0zhIBnBttZ5jtr+7oT7h2lsSUm1FC/ItRTW3vGi+v3XL5Ohg==", "350e7f32-4a54-47fb-a75a-ed16408c5c07" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MoneyNeeded", "Name" },
                values: new object[] { 1000000m, "A1" });

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MoneyNeeded", "Name" },
                values: new object[] { 5000000m, "B1" });

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MoneyNeeded", "Name", "NextLevel" },
                values: new object[] { 10000000m, "C1", null });

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
        }
    }
}
