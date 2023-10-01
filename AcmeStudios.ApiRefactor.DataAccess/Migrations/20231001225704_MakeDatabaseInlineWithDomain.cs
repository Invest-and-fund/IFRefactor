using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeStudios.ApiRefactor.DataAccess.Migrations
{
    public partial class MakeDatabaseInlineWithDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "StudioItemImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RpgClass = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudioItemImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudioItemId = table.Column<int>(type: "int", nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudioItemImages_StudioItems_StudioItemId",
                        column: x => x.StudioItemId,
                        principalTable: "StudioItems",
                        principalColumn: "StudioItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudioItemImages_StudioItemId",
                table: "StudioItemImages",
                column: "StudioItemId");
        }
    }
}
