using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddRoleEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "af841e03-566c-49c4-b7e2-048b4e98d62d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "b5909a08-ea63-4c34-bfff-8570b8b1f534");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dacb0904-8ed9-4728-af4e-cecf7b4c29e3", "0eaaaafa-c676-4684-a52f-19c9839b5722", "Employee", "EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06a07804-2db2-4bb5-a69a-e029aedbb4f3", "AQAAAAEAACcQAAAAENjpQ/WAqsLkuAURHoB8RnwSzKrZFwxkZojEtOUj64u2S8F5LJjLYcVauyeeml4Pbg==", "679013ac-faa8-450e-aef4-ea4c9a257480" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "a2bcfe94-b346-44ea-9e51-597e107157dc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "4d59f571-3b4e-4a78-9588-aaa2ebc30453");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac74f5ed-3db8-4771-b14c-71ae8a8c581c", "AQAAAAEAACcQAAAAECdMX7y4seQkwA88utD+qyat5KDJ1RBMHwRyHOcjIMAIjjvO0ewyw+EhnQB9vQ+Q+g==", "390ff56b-d037-49d5-86fd-38a2ccc009ea" });
        }
    }
}
