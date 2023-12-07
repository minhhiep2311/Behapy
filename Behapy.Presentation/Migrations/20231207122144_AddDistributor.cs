using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddDistributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DistributorId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DistributorLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyNeeded = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NextLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributorLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributors_DistributorLevels_DistributorLevelId",
                        column: x => x.DistributorLevelId,
                        principalTable: "DistributorLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DistributorLevels",
                columns: new[] { "Id", "MoneyNeeded", "Name", "NextLevel" },
                values: new object[,]
                {
                    { 1, 1000000m, "A1", 2 },
                    { 2, 5000000m, "B1", 3 },
                    { 3, 10000000m, "C1", null }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DistributorId",
                table: "Orders",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_DistributorLevelId",
                table: "Distributors",
                column: "DistributorLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Distributors_DistributorId",
                table: "Orders",
                column: "DistributorId",
                principalTable: "Distributors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Distributors_DistributorId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "DistributorLevels");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DistributorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "0eaaaafa-c676-4684-a52f-19c9839b5722");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06a07804-2db2-4bb5-a69a-e029aedbb4f3", "AQAAAAEAACcQAAAAENjpQ/WAqsLkuAURHoB8RnwSzKrZFwxkZojEtOUj64u2S8F5LJjLYcVauyeeml4Pbg==", "679013ac-faa8-450e-aef4-ea4c9a257480" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
