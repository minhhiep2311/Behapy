using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddPhoneAndTotalMoneyToDistributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoney",
                table: "Distributors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "8506ae73-9b64-4457-9201-9e8392464018");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "fd9ba70e-264d-4bf5-811e-64768a0c8d41");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "4e0d7e9f-3b4a-4d46-af06-8882f326e199");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84ad92b4-0228-4bc7-b52e-6aa5451b2566", "AQAAAAEAACcQAAAAEPt4/FrXUnNHxdnLkW1okntxGfMXr8ZOKlFIAHgv1/50sooxGvwJ5PULa2NbQVHbbg==", "a59d5718-9dc6-455e-abfc-5f349e0779f0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "TotalMoney",
                table: "Distributors");

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
    }
}
