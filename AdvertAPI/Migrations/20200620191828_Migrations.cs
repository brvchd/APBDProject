using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Building_pk", x => x.IdBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 250, nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "date", nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Salt = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Client_pk", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    IdCampaign = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    PricePerSquareMeter = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    FromIdBuilding = table.Column<int>(nullable: false),
                    ToIdBuilding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Campaign_pk", x => x.IdCampaign);
                    table.ForeignKey(
                        name: "Campaign_Building_FROM",
                        column: x => x.FromIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Campaign_Client",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Campaign_Building_TO",
                        column: x => x.ToIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    IdCampaign = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Banner_pk", x => x.IdAdvertisement);
                    table.ForeignKey(
                        name: "Banner_Campaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "IdCampaign",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_IdCampaign",
                table: "Banner",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_FromIdBuilding",
                table: "Campaign",
                column: "FromIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdClient",
                table: "Campaign",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_ToIdBuilding",
                table: "Campaign",
                column: "ToIdBuilding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
