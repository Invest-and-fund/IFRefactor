using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeStudios.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "StudioItemTypeId",
                keyValue: 7);

            migrationBuilder.RenameColumn(
                name: "StudioItemTypeId",
                table: "StudioItemTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StudioItemId",
                table: "StudioItems",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 1L, "Synthesiser" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 2L, "Drum Machine" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 3L, "Effect" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 4L, "Sequencer" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 5L, "Mixer" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 6L, "Oscillator" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "Id", "Value" },
                values: new object[] { 7L, "Utility" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "StudioItemTypes",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudioItemTypes",
                newName: "StudioItemTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudioItems",
                newName: "StudioItemId");

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 1, "Synthesiser" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 2, "Drum Machine" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 3, "Effect" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 4, "Sequencer" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 5, "Mixer" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 6, "Oscillator" });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[] { 7, "Utility" });
        }
    }
}
