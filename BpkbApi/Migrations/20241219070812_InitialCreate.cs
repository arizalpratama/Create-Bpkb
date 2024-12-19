using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BpkbApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsStorageLocations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsStorageLocations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "MsUsers",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TrBpkbs",
                columns: table => new
                {
                    AgreementNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BpkbNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BpkbDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FakturNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakturDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BpkbDateIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrBpkbs", x => x.AgreementNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsStorageLocations");

            migrationBuilder.DropTable(
                name: "MsUsers");

            migrationBuilder.DropTable(
                name: "TrBpkbs");
        }
    }
}
