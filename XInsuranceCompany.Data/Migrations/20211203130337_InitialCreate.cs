using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XInsuranceCompany.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarInsuranceOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LicenseSerialCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LicenseSerialNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInsuranceOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarInsuranceOfferDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarInsuranceOfferId = table.Column<int>(type: "int", nullable: false),
                    InsuranceCompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InsuranceCompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OfferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInsuranceOfferDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarInsuranceOfferDetails_CarInsuranceOffers_CarInsuranceOfferId",
                        column: x => x.CarInsuranceOfferId,
                        principalTable: "CarInsuranceOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarInsuranceOfferDetails_CarInsuranceOfferId",
                table: "CarInsuranceOfferDetails",
                column: "CarInsuranceOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarInsuranceOfferDetails");

            migrationBuilder.DropTable(
                name: "CarInsuranceOffers");
        }
    }
}
