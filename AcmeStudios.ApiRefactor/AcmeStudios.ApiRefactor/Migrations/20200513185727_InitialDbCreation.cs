using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcemStudios.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    HitPoints = table.Column<int>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    RpgClass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudioItemTypes",
                columns: table => new
                {
                    StudioItemTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItemTypes", x => x.StudioItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StudioItems",
                columns: table => new
                {
                    StudioItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acquired = table.Column<DateTime>(nullable: false),
                    Sold = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SoldFor = table.Column<decimal>(nullable: true),
                    Eurorack = table.Column<bool>(nullable: false),
                    StudioItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItems", x => x.StudioItemId);
                    table.ForeignKey(
                        name: "FK_StudioItems_StudioItemTypes_StudioItemTypeId",
                        column: x => x.StudioItemTypeId,
                        principalTable: "StudioItemTypes",
                        principalColumn: "StudioItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudioItemImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileTitle = table.Column<string>(nullable: true),
                    FileData = table.Column<byte[]>(nullable: true),
                    StudioItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudioItemImages_StudioItems_StudioItemId",
                        column: x => x.StudioItemId,
                        principalTable: "StudioItems",
                        principalColumn: "StudioItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[,]
                {
                    { 1, "Synthesiser" },
                    { 2, "Drum Machine" },
                    { 3, "Effect" },
                    { 4, "Sequencer" },
                    { 5, "Mixer" },
                    { 6, "Oscillator" },
                    { 7, "Utility" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudioItemImages_StudioItemId",
                table: "StudioItemImages",
                column: "StudioItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StudioItems_StudioItemTypeId",
                table: "StudioItems",
                column: "StudioItemTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "StudioItemImages");

            migrationBuilder.DropTable(
                name: "StudioItems");

            migrationBuilder.DropTable(
                name: "StudioItemTypes");
        }
    }
}
