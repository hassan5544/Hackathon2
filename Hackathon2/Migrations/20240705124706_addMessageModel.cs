using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon2.Migrations
{
    /// <inheritdoc />
    public partial class addMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DsmConsentrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DsmParticle = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AirQualityLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SensorValue = table.Column<int>(type: "int", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
