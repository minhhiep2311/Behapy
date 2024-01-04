using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behapy.Presentation.Migrations
{
    public partial class AddImportReductionToDistributorLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextLevel",
                table: "DistributorLevels",
                newName: "NextLevelId");

            migrationBuilder.AddColumn<decimal>(
                name: "ImportReduction",
                table: "DistributorLevels",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImportReduction",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImportReduction",
                value: 20000m);

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImportReduction",
                value: 30000m);

            migrationBuilder.UpdateData(
                table: "DistributorLevels",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImportReduction",
                value: 40000m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "77036dfc-8228-432b-9d3f-93813c764786");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "b3134b04-8b6e-4336-bdf2-b39e5825228d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "5fc49acf-fa3b-4042-a8fd-2f85ed62e514");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1126fd4f-a61e-48ad-ab59-ac3a71a8893f", "AQAAAAEAACcQAAAAEP8CtE13mTv2Gv4XM+SUJHNZQSiz7V/gqpmShSxs6iBdU/aUi8jd6Vzdp22TER6aQw==", "2992399a-ec3a-4d33-85fa-7e54799a316a" });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorLevels_NextLevelId",
                table: "DistributorLevels",
                column: "NextLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorLevels_DistributorLevels_NextLevelId",
                table: "DistributorLevels",
                column: "NextLevelId",
                principalTable: "DistributorLevels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorLevels_DistributorLevels_NextLevelId",
                table: "DistributorLevels");

            migrationBuilder.DropIndex(
                name: "IX_DistributorLevels_NextLevelId",
                table: "DistributorLevels");

            migrationBuilder.DropColumn(
                name: "ImportReduction",
                table: "DistributorLevels");

            migrationBuilder.RenameColumn(
                name: "NextLevelId",
                table: "DistributorLevels",
                newName: "NextLevel");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e18-c46f-4e76-8e77-69430f54d796",
                column: "ConcurrencyStamp",
                value: "6934e92c-e746-4b49-aac8-c3b3975a40bc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "08db1e1a-7953-4790-8ebe-272e34a8fe18",
                column: "ConcurrencyStamp",
                value: "43b7b685-49b4-4193-a6a3-5d5fe1b1367b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dacb0904-8ed9-4728-af4e-cecf7b4c29e3",
                column: "ConcurrencyStamp",
                value: "cb07ec56-46eb-47bb-91dd-959fd9601cfe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "145aba11-057c-4fec-8ad5-4af354dd6a93", "AQAAAAEAACcQAAAAEBq2GZ7t2mSxfbqPB8qeJ6z1+6qQkYzLGzA+fo998LqXnjsn26JuR4tnPZrWS7Idhw==", "29002a16-145d-4c59-9e0b-49b9cf2e28e6" });
        }
    }
}
