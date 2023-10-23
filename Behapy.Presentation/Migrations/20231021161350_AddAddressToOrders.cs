using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddAddressToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "6a011bf3-017b-46eb-ad78-437ee4aca410");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "3f0d5700-79dc-460e-8626-886ad186de98");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6e30251-525f-411e-907e-6e744374fd30", "AQAAAAEAACcQAAAAEIGWWAER0K6V4H6GyzLC6FoHz97MDkFEDB7r0Sxwpa9d14/M02+K+HUAJbu1LFhxyw==", "869069ba-80bd-4acb-b1d7-4124f4cece58" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "67696138-a9c6-43fd-9458-a47dc9dec2e1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "bc86a40b-756b-4ec9-8207-77d6a8f0f93d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30ebdb35-c7d4-4616-afc7-1cf5b7aaf2e5", "AQAAAAEAACcQAAAAEO0uUP4Thdtw+Shxyuq7gzUt025agKBxYU5FxTV+uBIj61fYZLltJvx0TmLW03vjAQ==", "cccdc6f2-3774-4a4a-85da-6e87c9de1353" });
        }
    }
}
