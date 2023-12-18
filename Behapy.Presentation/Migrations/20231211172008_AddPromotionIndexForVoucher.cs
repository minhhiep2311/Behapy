using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddPromotionIndexForVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Voucher",
                table: "Promotions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                name: "IX_Promotions_Voucher",
                table: "Promotions",
                column: "Voucher",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Promotions_Voucher",
                table: "Promotions");

            migrationBuilder.AlterColumn<string>(
                name: "Voucher",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
